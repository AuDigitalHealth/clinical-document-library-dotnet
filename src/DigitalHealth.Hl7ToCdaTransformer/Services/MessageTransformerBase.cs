using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDA.Generator.Common.SCSModel.Entities;
using CDA.Generator.Common.SCSModel.Interfaces;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.HL7.Common.DataStructure;
using DigitalHealth.HL7.Common.Message;
using DigitalHealth.HL7.Common.Segment;
using DigitalHealth.HL7.Common.SegmentGroup;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    /// <summary>
    /// Base class for transformer functionality.
    /// </summary>
    internal class MessageTransformerBase
    {
        /// <summary>
        /// Get formatted name from a CN instance.
        /// </summary>
        /// <param name="cn">CN instance.</param>
        /// <returns>Formatted name string.</returns>
        internal static string GetNameStringFromCN(CN cn)
        {
            if (cn != null)
            {
                return (string.IsNullOrEmpty(cn.prefix) ? string.Empty : cn.prefix + " ") + cn.givenname + " " + cn.familyname + (string.IsNullOrEmpty(cn.suffix) ? string.Empty : " " + cn.suffix);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the test result name.
        /// </summary>
        /// <param name="universalServiceId">Universal Service Id</param>
        /// <returns>ICodableText</returns>
        internal static ICodableText GetTestResultName(CE universalServiceId)
        {
            string code;
            string text;

            if (string.IsNullOrWhiteSpace(universalServiceId.alternatetext) &&
                string.IsNullOrWhiteSpace(universalServiceId.alternateidentifier) &&
                string.IsNullOrWhiteSpace(universalServiceId.nameofalternatecodingsystem))
            {
                code = universalServiceId.identifier;
                text = universalServiceId.text;
            }
            else
            {
                code = universalServiceId.alternateidentifier;
                text = universalServiceId.alternatetext;
            }

            return BaseCDAModel.CreateCodableText(code, null, string.Empty, text);
        }

        /// <summary>
        /// Create the order details from information in the HL7 V2  message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static OrderDetails CreateOrderDetails(HL7GenericMessage message)
        {
            // Order Details
            var orderDetails = DiagnosticImagingReport.CreateOrderDetails();
            
            // Requester
            orderDetails.Requester = CreateRequester(message);

            return orderDetails;
        }

        /// <summary>
        /// Create the requester from information in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IParticipationRequester CreateRequester(HL7GenericMessage message)
        {
            OBR obrSegment = message.Order.First().Observation.First().ObservationsReportID;

            XCN orderingProvider = obrSegment.OrderingProvider;
            
            var requester = BaseCDAModel.CreateRequester();

            // Document Requester> Participant
            requester.Participant = BaseCDAModel.CreateParticipantForRequester();

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Participation Period
            requester.ParticipationEndTime = GetQuantityTimingStartDateTime(message);

            // Document Requester > Role
            requester.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner, CodingSystem.ANZSCO);

            // Document Requester > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = orderingProvider.familylastname.familyname;
            name1.GivenNames = new List<string> { orderingProvider.givenname };
            name1.Titles = new List<string> { orderingProvider.prefix };

            person.PersonNames = new List<IPersonName> { name1 };

            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();

            requester.Participant.Person = person;

            return requester;
        }

        /// <summary>
        /// Create the subject of care from information in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IParticipationSubjectOfCare CreateSubjectOfCare(HL7GenericMessage message)
        {
            IParticipationSubjectOfCare subjectOfCare = BaseCDAModel.CreateSubjectOfCare();

            var pid = message.PatientIdentification;

            var participant = BaseCDAModel.CreateParticipantForSubjectOfCare();

            // Subject of Care > Participant > Person or Organisation or Device > Person
            var person = BaseCDAModel.CreatePersonForSubjectOfCare();

            // Subject of Care > Participant > Address
            var hl7Addresses = pid.PatientAddress;

            if (hl7Addresses != null && hl7Addresses.Length > 0)
            {
                participant.Addresses = new List<IAddress>();

                for (int x = 0; x < hl7Addresses.Length; x++)
                {
                    participant.Addresses.Add(GetAustralianAddress(hl7Addresses[x]));
                }
            }

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
            if (pid.Sex?.identifier != null)
            {
                person.Gender = GetGender(pid.Sex.identifier);
            }

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Date of Birth Detail > 
            // Date of Birth
            if (pid.DateTimeOfBirth != null && pid.DateTimeOfBirth.TimestampValue.HasValue)
            {
                person.DateOfBirth = new ISO8601DateTime(pid.DateTimeOfBirth.TimestampValue.Value);
            }

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Indigenous Status
            if (pid.Race != null && pid.Race.Length > 0)
            {
                IndigenousStatus indigenousStatus;
                if (!EnumHelper.TryGetEnumValue<IndigenousStatus, NameAttribute>(a => a.Code == pid.Race[0].identifier,
                    out indigenousStatus))
                {
                    throw new ArgumentException("No matching IndigenousStatus value found");
                }

                person.IndigenousStatus = indigenousStatus;
            }
            else
            {
                person.IndigenousStatus = IndigenousStatus.NotStatedOrInadequatelyDescribed;
            }

            // Subject of Care > Participant > Person or Organisation or Device > Person > Person Name
            if (pid.PatientName != null && pid.PatientName.Length > 0)
            {
                person.PersonNames = new List<IPersonName>();

                for (int x = 0; x < pid.PatientName.Length; x++)
                {
                    person.PersonNames.Add(GetPersonName(pid.PatientName[x]));
                }
            }

            // Identifier
            if (pid.PatientIdentifierList != null && pid.PatientIdentifierList.Length > 0)
            {
                foreach (var identifier in pid.PatientIdentifierList)
                {
                    if (identifier.assigningauthority.namespaceID == "AUSHIC" && identifier.identifiertypecode == "NI")
                    {
                        person.Identifiers = new List<Identifier>
                        {
                            BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, identifier.ID)
                        };
                    }

                }
            }

            participant.Person = person;
            subjectOfCare.Participant = participant;

            return subjectOfCare;
        }

        /// <summary>
        /// Creates a PersonName instance from an XPN instance.
        /// </summary>
        /// <param name="xpn">Name.</param>
        /// <returns>IPersonName</returns>
        internal static IPersonName GetPersonName(XPN xpn)
        {
            if (xpn == null)
                return null;

            var name = BaseCDAModel.CreatePersonName();

            if (xpn.familylastname != null)
                name.FamilyName = xpn.familylastname.familyname;
            name.GivenNames = new List<string>();
            name.GivenNames.Add(xpn.givenname);
            name.NameUsages = new List<NameUsage>();

            if (xpn.nametypecode != null)
            {
                switch (xpn.nametypecode)
                {
                    case "A":
                    case "L":
                    case "C":
                    case "I":
                        name.NameUsages.Add(NameUsage.Legal);
                        break;
                    case "D":
                        name.NameUsages.Add(NameUsage.ReportingName);
                        break;
                    case "M":
                        name.NameUsages.Add(NameUsage.MaidenName);
                        break;
                    case "B":
                        name.NameUsages.Add(NameUsage.NewbornName);
                        break;
                    case "S":
                    case "P":
                    case "T":
                        name.NameUsages.Add(NameUsage.OtherName);
                        break;
                    case "N":
                        name.NameUsages.Add(NameUsage.PreferredNameIndicator);
                        break;
                    default:
                        name.NameUsages.Add(NameUsage.Undefined);
                        break;
                }
            }
            else
            {
                name.NameUsages = new List<NameUsage>() { NameUsage.Undefined };
            }

            return name;
        }

        /// <summary>
        /// Creates an AustralianAddress instance from an XAD instance.
        /// </summary>
        /// <param name="name">The HL7 V2 XAD instance.</param>
        /// <returns></returns>
        internal static IAddress GetAustralianAddress(XAD xad)
        {
            AustralianState addressState;
            var addressStateSpecified = Enum.TryParse(xad.stateorprovince, out addressState);

            var address = BaseCDAModel.CreateAddress();
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.PostCode = xad.ziporpostalcode;
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { xad.streetaddress };
            address.AustralianAddress.SuburbTownLocality = xad.city;
            address.AustralianAddress.State = AustralianState.QLD;
            if (addressStateSpecified)
                address.AustralianAddress.State = addressState;

            address.AddressPurpose = AddressPurpose.NotStatedUnknownInadequatelyDescribed;
            switch (xad.addresstype)
            {
                case "H":
                case "P":
                case "BR":
                    address.AddressPurpose = AddressPurpose.Residential;
                    break;
                case "B":
                case "RH":
                case "O":
                case "L":
                    address.AddressPurpose = AddressPurpose.Business;
                    break;
                case "M":
                    address.AddressPurpose = AddressPurpose.MailingOrPostal;
                    break;
                case "C":
                    address.AddressPurpose = AddressPurpose.TemporaryAccommodation;
                    break;
            }

            return address;
        }

        /// <summary>
        /// Gets an equivalent Gender enum value from a HL7 V2 genderCode.
        /// </summary>
        /// <param name="genderCode">The HL7 V2 gender code.</param>
        /// <returns></returns>
        internal static Gender GetGender(string genderCode)
        {
            Gender gender = Gender.NotStated;

            switch (genderCode)
            {
                case "F":
                    gender = Gender.Female;
                    break;
                case "O":
                case "A":
                    gender = Gender.IntersexOrIndeterminate;
                    break;
                case "M":
                    gender = Gender.Male;
                    break;
            }

            return gender;
        }

        /// <summary>
        /// Gets the report attachment.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <param name="reportData">Report data.</param>
        /// <returns></returns>
        internal static ReportAttachment GetReportAttachment(HL7GenericMessage genericMessage, byte[] reportData)
        {
            if (reportData != null)
            {
                OBX attachmentObx = genericMessage.Order.Last().Observation.Last().Result.SingleOrDefault(s => s.ValueType == "RP"
                                                                                                               && s.ObservationIdentifier.identifier.ToUpper() == TransformerConstants.PdfType);

                return new ReportAttachment
                {
                    Data = reportData,
                    Filename = attachmentObx.ObservationValue.First()
                };
            }
            else
            {
                OBX attachmentObx = genericMessage.Order.Last().Observation.Last().Result.Single(s => s.ValueType == "ED"
                                                                                                      && s.ObservationIdentifier.identifier.ToUpper() == TransformerConstants.PdfType);

                return new ReportAttachment
                {
                    Data = Convert.FromBase64String(attachmentObx.ObservationValue[4]),
                    Filename = TransformerConstants.DefaultFilename
                };
            }
        }

        /// <summary>
        /// Create the related document from information in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <param name="reportAttachment">Report attachment.</param>
        /// <returns>RelatedDocument</returns>
        internal static RelatedDocument CreateRelatedDocument(HL7GenericMessage message, ReportAttachment reportAttachment)
        {
            RelatedDocument relatedDocument = PathologyResultReport.CreateRelatedDocument();
            
            // Pathology PDF
            var attachmentPdf = BaseCDAModel.CreateExternalData();
            attachmentPdf.ExternalDataMediaType = reportAttachment.MediaType;
            attachmentPdf.ByteArrayInput = new ByteArrayInput
            {
                ByteArray = reportAttachment.Data,
                FileName = reportAttachment.Filename
            };
            relatedDocument.ExaminationResultRepresentation = attachmentPdf;

            DocumentDetails documentDetails = BaseCDAModel.CreateDocumentDetails();

            // Report Date 
            documentDetails.ReportDate = GetReportDate(message);

            // Result Status
            string status = GetRelatedDocumentStatus(message);
            Hl7V3ResultStatus resultStatus;
            if (!EnumHelper.TryGetEnumValue<Hl7V3ResultStatus, NameAttribute>(attribute => attribute.Code == status, out resultStatus))
            {
                throw new ArgumentException("No matching Hl7V3ResultStatus value found");
            }
            documentDetails.ReportStatus = BaseCDAModel.CreateResultStatus(resultStatus);

            // Report Name 
            documentDetails.ReportDescription = GetReportName(message);

            relatedDocument.DocumentDetails = documentDetails;

            return relatedDocument;
        }

        /// <summary>
        /// Gets the document creation time.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static ISO8601DateTime GetReportDate(HL7GenericMessage genericMessage)
        {
            IList<DateTime> testResultDateTimes = new List<DateTime>();

            foreach (var orderGroup in genericMessage.Order)
            {
                foreach (var observationGroup in orderGroup.Observation)
                {
                    OBR obrSegment = observationGroup.ObservationsReportID;

                    if (obrSegment.UniversalServiceID.identifier != TransformerConstants.ReportText)
                    {
                        testResultDateTimes.Add(obrSegment.ResultsRptStatusChngDateTime.Where(r => r.TimestampValue.HasValue)
                            .OrderBy(r => r.TimestampValue.Value)
                            .Single()
                            .TimestampValue.Value);
                    }
                }
            }

            DateTime reportDate = testResultDateTimes.Max();

            return reportDate.TimeOfDay.TotalSeconds == 0
                ? new ISO8601DateTime(reportDate, ISO8601DateTime.Precision.Day)
                : new ISO8601DateTime(reportDate);
        }

        /// <summary>
        /// Get the related document status.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns>Status</returns>
        internal static string GetRelatedDocumentStatus(HL7GenericMessage genericMessage)
        {
            string relatedDocumentStatus = null;

            foreach (OrderGroup orderGroup in genericMessage.Order)
            {
                relatedDocumentStatus = orderGroup.Observation.Any(i => i.ObservationsReportID.ResultStatus == "P" ||
                                                                        i.ObservationsReportID.ResultStatus == "C") ? (orderGroup.Observation.Any(i => i.ObservationsReportID.ResultStatus == "P") ? "P" : "C") : "F";
            }

            return relatedDocumentStatus;
        }

        /// <summary>
        /// Gets the report name.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns>Report name.</returns>
        internal static string GetReportName(HL7GenericMessage genericMessage)
        {
            IList<string> resultName = new List<string>();
            foreach (var orderGroup in genericMessage.Order)
            {
                foreach (var observationGroup in orderGroup.Observation)
                {
                    OBR obrSegment = observationGroup.ObservationsReportID;

                    if (obrSegment.UniversalServiceID.identifier != TransformerConstants.ReportText)
                    {
                        resultName.Add($"{obrSegment.UniversalServiceID.alternatetext ?? obrSegment.UniversalServiceID.text}");
                    }
                }
            }

            return string.Join(",", resultName);
        }

        /// <summary>
        /// Gets the WhenResultsRptStatusChange time from OBR22 in the HL7 V2 message.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static ISO8601DateTime GetResultsReportStatusChange(HL7GenericMessage genericMessage)
        {
            DateTime dateTime = genericMessage.Order.First().Observation.First()
                .ObservationsReportID.ResultsRptStatusChngDateTime.First().TimestampValue.Value;

            return new ISO8601DateTime(dateTime);
        }

        /// <summary>
        /// Gets the start dateTime from OBR22 in the HL7 V2 message.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static ISO8601DateTime GetQuantityTimingStartDateTime(HL7GenericMessage genericMessage)
        {
            DateTime dateTime = genericMessage.Order.First().Observation.First()
                .ObservationsReportID.QuantityTiming.startdatetime.TimestampValue.Value;

            return new ISO8601DateTime(dateTime);
        }

        /// <summary>
        /// Gets the report status.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns>DocumentStatus</returns>
        internal static DocumentStatus GetReportStatus(HL7GenericMessage message)
        {
            string reportStatus = null;
            foreach (OrderGroup orderGroup in message.Order)
            {
                reportStatus = orderGroup.Observation.Any(i => i.ObservationsReportID.ResultStatus == "P") ? "I" : "F";
            }

            switch (reportStatus)
            {
                case "I":
                    return DocumentStatus.Interim;
                case "F":
                    return DocumentStatus.Final;
                default:
                    return DocumentStatus.Final;
            }
        }

        /// <summary>
        /// Create the legal authenticator from information in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IParticipationLegalAuthenticator CreateLegalAuthenticator(HL7GenericMessage message)
        {
            IParticipationLegalAuthenticator authenticator = BaseCDAModel.CreateAuthenticator();

            // LegalAuthenticator/assignedEntity
            authenticator.Participant = BaseCDAModel.CreateParticipantForLegalAuthenticator();

            // LegalAuthenticator/assignedEntity/assignedPerson
            // authenticator.Participant.Person = MapAssignedPerson(message);
            authenticator.Participant.Person = GetPrincipalResultInterpreter(message);

            // LegalAuthenticator/time/@value
            // V2 MAP : OBR-22
            authenticator.Participant.DateTimeAuthenticated = GetResultsReportStatusChange(message);

            return authenticator;
        }

        /// <summary>
        /// Gets the PrincipalResultInterpreter from OBR32 in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IPerson GetPrincipalResultInterpreter(HL7GenericMessage message)
        {
            var person = BaseCDAModel.CreatePerson();

            var nameCn = message.Order.First().Observation.First().ObservationsReportID.PrincipalResultInterpreter.name;

            person.PersonNames = new List<IPersonName>
            {
                GetPersonNameFromCn(nameCn)
            };

            person.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, nameCn.IDnumberST)
            };

            return person;
        }

        /// <summary>
        /// Creates a PersonName instance from a CN instance.
        /// </summary>
        /// <param name="name">The HL7 V2 CN instance.</param>
        /// <returns></returns>
        internal static IPersonName GetPersonNameFromCn(CN name)
        {
            IPersonName personName = BaseCDAModel.CreatePersonName();

            personName.FamilyName = name.familyname;
            personName.GivenNames = new List<string> { name.givenname };
            if (!string.IsNullOrWhiteSpace(name.prefix))
            {
                personName.Titles = new List<string> { name.prefix };
            }

            return personName;
        }

        /// <summary>
        /// Create the author from information in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IParticipationAuthorHealthcareProvider CreateAuthor(HL7GenericMessage message)
        {
            IParticipationAuthorHealthcareProvider author = BaseCDAModel.CreateAuthorHealthcareProvider();

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthorHealthcareProvider();

            // V2 MAP : OBR-22
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(GetResultsReportStatusChange(message));

            // Document Author > Role = AddressPurpose.Residential
            author.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, CodingSystem.ANZSCORevision1);

            // Document Author > Participant > Person or Organisation or Device > Person > Person Name (Note: 1..* in ACI) 
            var person = GetPrincipalResultInterpreter(message);

            author.Participant.Person = (IPersonHealthcareProvider)person;

            return author;
        }
    }
}
