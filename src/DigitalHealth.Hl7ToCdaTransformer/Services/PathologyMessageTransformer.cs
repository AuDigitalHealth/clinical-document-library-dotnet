using System;
using System.Collections.Generic;
using System.Linq;
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
using PathologyTestResult = Nehta.VendorLibrary.CDA.SCSModel.Pathology.PathologyTestResult;


namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    internal class PathologyMessageTransformer
    {
        /// <summary>
        /// Create a PathologyTransformResult instance (containing a PathologyResultReport and a ReportAttachment instance) from a HL7 V2 pathology message. 
        /// The PathologyTransformResult instance is then used to generate a CDA document.
        /// </summary>
        /// <param name="hl7GenericPathMessage">The HL7 V2 pathology message to transform.</param>
        /// <param name="metadata">Mandatory information to supplement the transform.</param>
        /// <param name="reportData">Report data.</param>
        /// <returns></returns>
        public PathologyTransformResult Transform(HL7GenericPathMessage hl7GenericPathMessage, PathologyMetadata metadata, byte[] reportData = null)
        {
            PathologyResultReport pathologyResultReport = PathologyResultReport.CreatePathologyResultReport();
            
            // Include Logo
            pathologyResultReport.IncludeLogo = false;

            // Set Creation Time
            pathologyResultReport.DocumentCreationTime = GetResultsReportStatusChange(hl7GenericPathMessage);

            // Document Status
            pathologyResultReport.DocumentStatus = GetReportStatus(hl7GenericPathMessage);

            #region Setup and populate the CDA context model

            // CDA Context model
            var cdaContext = PathologyResultReport.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);

            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);

            // CDA Context Version
            cdaContext.Version = "1";

            // Legal Authenticator
            cdaContext.LegalAuthenticator = CreateLegalAuthenticator(hl7GenericPathMessage);

            // Custodian 
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            ICustodian custodian = BaseCDAModel.CreateParticipantCustodian();
            cdaContext.Custodian.Participant = custodian;

            pathologyResultReport.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model

            // SCS Context model
            pathologyResultReport.SCSContext = PathologyResultReport.CreateSCSContext();

            // Author Health Care Provider
            pathologyResultReport.SCSContext.Author = CreateAuthor(hl7GenericPathMessage);

            // The Reporting Pathologist
            pathologyResultReport.SCSContext.ReportingPathologist = CreateReportingPathologist(hl7GenericPathMessage);

            // Order Details
            pathologyResultReport.SCSContext.OrderDetails = CreateOrderDetails(hl7GenericPathMessage);

            // Subject Of Care
            pathologyResultReport.SCSContext.SubjectOfCare = CreateSubjectOfCare(hl7GenericPathMessage);

            #endregion

            #region Setup and populate the SCS Content model

            // SCS Content model
            pathologyResultReport.SCSContent = PathologyResultReport.CreateSCSContent();

            ReportAttachment reportAttachment = GetReportAttachment(hl7GenericPathMessage, reportData);

            // Pathology Test Result
            pathologyResultReport.SCSContent.PathologyTestResult = CreatePathologyTestResults(hl7GenericPathMessage);

            // Related Document
            pathologyResultReport.SCSContent.RelatedDocument = CreateRelatedDocument(hl7GenericPathMessage, reportAttachment);

            #endregion

            FillInAdditionalMetadata(pathologyResultReport, metadata);

            return new PathologyTransformResult
            {
                PathologyResultReport = pathologyResultReport,
                Attachment = reportAttachment
            };
        }

        /// <summary>
        /// Gets the report attachment.
        /// </summary>
        /// <param name="genericPathMessage"></param>
        /// <param name="reportData">Report data.</param>
        /// <returns></returns>
        private static ReportAttachment GetReportAttachment(HL7GenericPathMessage genericPathMessage, byte[] reportData)
        {
            if (reportData != null)
            {
                OBX attachmentObx = genericPathMessage.Order.Last().Observation.Last().Result.SingleOrDefault(s => s.ValueType == "RP"
                    && s.ObservationIdentifier.identifier.ToUpper() == TransformerConstants.PdfType);

                return new ReportAttachment
                {
                    Data = reportData,
                    Filename = attachmentObx.ObservationValue.First()
                };
            }
            else
            {
                OBX attachmentObx = genericPathMessage.Order.Last().Observation.Last().Result.Single(s => s.ValueType == "ED"
                    && s.ObservationIdentifier.identifier.ToUpper() == TransformerConstants.PdfType);

                return new ReportAttachment
                {
                    Data = Convert.FromBase64String(attachmentObx.ObservationValue[4]),
                    Filename = TransformerConstants.DefaultFilename
                };
            }
        }

        /// <summary>
        /// Fill in additional required information in the PathologyResultReport.
        /// </summary>
        /// <param name="report">The PathologyResultReport to complete.</param>
        /// <param name="metadata">Metadata information.</param>
        private static void FillInAdditionalMetadata(PathologyResultReport report, PathologyMetadata metadata)
        {
            report.SCSContext.OrderDetails.RequesterOrderIdentifier = metadata.RequesterOrderIdentifier;
            report.SCSContent.RelatedDocument.DocumentDetails.ReportIdentifier = metadata.ReportIdentifier;

            // Author
            report.SCSContext.Author.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            report.SCSContext.Author.Participant.Person.Organisation.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, metadata.AuthorOrganisationHpio)
            };

            // Reporting pathologist
            report.SCSContext.ReportingPathologist.Role = PathologyResultReport.CreateRole(metadata.ReportingPathologist.Role, CodingSystem.ANZSCO);
            report.SCSContext.ReportingPathologist.Participant.Addresses = metadata.ReportingPathologist.Address;
            report.SCSContext.ReportingPathologist.Participant.ElectronicCommunicationDetails = metadata.ReportingPathologist.ContactDetails;
            report.SCSContext.ReportingPathologist.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            report.SCSContext.ReportingPathologist.Participant.Person.Organisation.Identifiers = new List<Identifier> {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, metadata.ReportingPathologist.OrganisationHpio )
            };
        }

        /// <summary>
        /// Create the subject of care from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static IParticipationSubjectOfCare CreateSubjectOfCare(HL7GenericPathMessage message)
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
                if (!EnumHelper.TryGetEnumValue<IndigenousStatus, NameAttribute>(a => a.Code == pid.Race[0].identifier, out indigenousStatus))
                {
                    throw new ArgumentException("No matching IndigenousStatus value found");                    
                }

                person.IndigenousStatus = indigenousStatus;
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
        /// Create the legal authenticator from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static IParticipationLegalAuthenticator CreateLegalAuthenticator(HL7GenericPathMessage message)
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
        /// Create the order details from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static OrderDetails CreateOrderDetails(HL7GenericPathMessage message)
        {
            // Order Details
            var orderDetails = DiagnosticImagingReport.CreateOrderDetails();

            // Requester
            orderDetails.Requester = CreateRequester(message);

            return orderDetails;
        }

        /// <summary>
        /// Gets the report name.
        /// </summary>
        /// <param name="genericPathMessage">The HL7 V2 pathology message.</param>
        /// <returns>Report name.</returns>
        private static string GetReportName(HL7GenericPathMessage genericPathMessage)
        {
            IList<string> resultName = new List<string>();
            foreach (var orderGroup in genericPathMessage.Order)
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
        /// Gets the document creation time.
        /// </summary>
        /// <param name="genericPathMessage">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static ISO8601DateTime GetReportDate(HL7GenericPathMessage genericPathMessage)
        {
            IList<DateTime> testResultDateTimes = new List<DateTime>();

            foreach (var orderGroup in genericPathMessage.Order)
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
        /// Gets the test result name.
        /// </summary>
        /// <param name="universalServiceId">Universal Service Id</param>
        /// <returns>ICodableText</returns>
        private static ICodableText GetTestResultName(CE universalServiceId)
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
        /// Create the pathology test results from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="genericPathMessage">The HL7 V2 pathology message.</param>
        /// <returns>List of PathologyTestResult</returns>
        private static IList<PathologyTestResult> CreatePathologyTestResults(HL7GenericPathMessage genericPathMessage)
        {
            IList<PathologyTestResult> pathologyTestResults = new List<PathologyTestResult>();

            foreach (var orderGroup in genericPathMessage.Order)
            {
                foreach (var observationGroup in orderGroup.Observation)
                {
                    OBR obrSegment = observationGroup.ObservationsReportID;

                    if (obrSegment.UniversalServiceID.identifier != TransformerConstants.ReportText)
                    {
                        PathologyTestResult testResult = CreatePathologyTestResult(obrSegment);
                        pathologyTestResults.Add(testResult);
                    }
                }                
            }

            return pathologyTestResults;
        }

        /// <summary>
        /// Create the pathology test result from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="obrSegment">OBR segment.</param>
        /// <returns>PathologyTestResult</returns>
        private static PathologyTestResult CreatePathologyTestResult(OBR obrSegment)
        {
            PathologyTestResult testResult = PathologyResultReport.CreatePathologyTestResult();

            // Test Result Name
            testResult.TestResultName = GetTestResultName(obrSegment.UniversalServiceID);

            // Pathology Discipline
            DiagnosticService diagnosticService;
            if (!EnumHelper.TryGetEnumValue<DiagnosticService, NameAttribute>(attribute => attribute.Code == obrSegment.DiagnosticServSectID, out diagnosticService))
            {
                diagnosticService = DiagnosticService.Laboratory;
            }
            testResult.PathologyDiscipline = diagnosticService;

            // Report Status
            Hl7V3ResultStatus hl7V3ResultStatus;
            if (!EnumHelper.TryGetEnumValue<Hl7V3ResultStatus, NameAttribute>(attribute => attribute.Code == obrSegment.ResultStatus, out hl7V3ResultStatus))
            {
                throw new ArgumentException("No matching Hl7V3ResultStatus value found");
            }
            testResult.OverallTestResultStatus = BaseCDAModel.CreateResultStatus(hl7V3ResultStatus);

            // Test Specimen Detail
            testResult.TestSpecimenDetail = PathologyResultReport.CreateTestSpecimenDetail();
            testResult.TestSpecimenDetail.CollectionDateTime = new ISO8601DateTime(obrSegment.ObservationDateTime.TimestampValue.Value);

            // Pathology Test Result Date Time
            testResult.ObservationDateTime = new ISO8601DateTime(obrSegment.ObservationDateTime.TimestampValue.Value);

            return testResult;
        }

        /// <summary>
        /// Create the related document from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <param name="reportAttachment">Report attachment.</param>
        /// <returns>RelatedDocument</returns>
        private static RelatedDocument CreateRelatedDocument(HL7GenericPathMessage message, ReportAttachment reportAttachment)
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
        /// Get the related document status.
        /// </summary>
        /// <param name="genericPathMessage">The HL7 V2 pathology message.</param>
        /// <returns>Status</returns>
        private static string GetRelatedDocumentStatus(HL7GenericPathMessage genericPathMessage)
        {
            string relatedDocumentStatus = null;

            foreach (OrderGroup orderGroup in genericPathMessage.Order)
            {
                relatedDocumentStatus = orderGroup.Observation.Any(i => i.ObservationsReportID.ResultStatus == "P" || 
                    i.ObservationsReportID.ResultStatus == "C") ? (orderGroup.Observation.Any(i => i.ObservationsReportID.ResultStatus == "P") ? "P" : "C") : "F";
            }

            return relatedDocumentStatus;
        }

        /// <summary>
        /// Gets the report status.
        /// </summary>
        /// <param name="genericPathMessage"></param>
        /// <returns>DocumentStatus</returns>
        private static DocumentStatus GetReportStatus(HL7GenericPathMessage genericPathMessage)
        {
            string reportStatus = null;
            foreach (OrderGroup orderGroup in genericPathMessage.Order)
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
        /// Create the reporting pathologist from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static IParticipationReportingPathologist CreateReportingPathologist(HL7GenericPathMessage message)
        {
            var nameCn = message.Order.First().Observation.First().ObservationsReportID.PrincipalResultInterpreter.name;

            // Receiving Laboratory
            var reportingPathologist = PathologyResultReport.CreateReportingPathologist();

            // Document reportingPathologist > Participant
            reportingPathologist.Participant = PathologyResultReport.CreateParticipantForReportingPathologist();

            // Participation Period
            reportingPathologist.ParticipationEndTime = GetResultsReportStatusChange(message);

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Document reportingPathologist > Participant > Person or Organisation or Device > Person > Person Name
            person.PersonNames = new List<IPersonName> { GetPersonNameFromCn(nameCn) };

            // Document reportingPathologist > Participant > Entity Identifier
            person.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, nameCn.IDnumberST)
            };

            reportingPathologist.Participant.Person = person;

            return reportingPathologist;
        }

        /// <summary>
        /// Create the requester from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static IParticipationRequester CreateRequester(HL7GenericPathMessage message)
        {
            OBR obrSegment = message.Order.First().Observation.First().ObservationsReportID;

            XCN orderingProvider = obrSegment.OrderingProvider;

            // Receiving Laboratory
            var requester = BaseCDAModel.CreateRequester();

            // Document Requester> Participant
            requester.Participant = BaseCDAModel.CreateParticipantForRequester();

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Participation Period
            requester.ParticipationEndTime = GetResultsReportStatusChange(message);

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
        /// Create the author from information in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static IParticipationAuthorHealthcareProvider CreateAuthor(HL7GenericPathMessage message)
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

        /// <summary>
        /// Gets the WhenResultsRptStatusChange time from OBR22 in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static ISO8601DateTime GetResultsReportStatusChange(HL7GenericPathMessage genericMessage)
        {
            DateTime dateTime = genericMessage.Order.First().Observation.First()
                .ObservationsReportID.ResultsRptStatusChngDateTime.First().TimestampValue.Value;

            return new ISO8601DateTime(dateTime);
        }

        /// <summary>
        /// Gets the PrincipalResultInterpreter from OBR32 in the HL7 V2 pathology message.
        /// </summary>
        /// <param name="message">The HL7 V2 pathology message.</param>
        /// <returns></returns>
        private static IPerson GetPrincipalResultInterpreter(HL7GenericPathMessage message)
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
        private static IPersonName GetPersonNameFromCn(CN name)
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
        /// Gets an equivalent Gender enum value from a HL7 V2 genderCode.
        /// </summary>
        /// <param name="genderCode">The HL7 V2 gender code.</param>
        /// <returns></returns>
        private static Gender GetGender(string genderCode)
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
        /// Creates a PersonName instance from an XPN instance.
        /// </summary>
        /// <param name="xpn">Name.</param>
        /// <returns>IPersonName</returns>
        private static IPersonName GetPersonName(XPN xpn)
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
        private static IAddress GetAustralianAddress(XAD xad)
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
    }
}