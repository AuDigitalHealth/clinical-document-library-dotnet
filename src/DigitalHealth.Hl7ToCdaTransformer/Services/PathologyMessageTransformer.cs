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
    internal class PathologyMessageTransformer : MessageTransformerBase
    {
        /// <summary>
        /// Create a PathologyTransformResult instance (containing a PathologyResultReport and a ReportAttachment instance) from a HL7 V2 pathology message. 
        /// The PathologyTransformResult instance is then used to generate a CDA document.
        /// </summary>
        /// <param name="hl7GenericMessage">The HL7 V2 message to transform.</param>
        /// <param name="metadata">Mandatory information to supplement the transform.</param>
        /// <param name="reportData">Report data.</param>
        /// <returns></returns>
        internal PathologyTransformResult Transform(HL7GenericMessage hl7GenericMessage, PathologyMetadata metadata, byte[] reportData = null)
        {
            PathologyResultReport pathologyResultReport = PathologyResultReport.CreatePathologyResultReport();
            
            // Include Logo
            pathologyResultReport.IncludeLogo = false;

            // Set Creation Time
            pathologyResultReport.DocumentCreationTime = GetResultsReportStatusChange(hl7GenericMessage);

            // Document Status
            pathologyResultReport.DocumentStatus = GetReportStatus(hl7GenericMessage);

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
            cdaContext.LegalAuthenticator = CreateLegalAuthenticator(hl7GenericMessage);

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
            pathologyResultReport.SCSContext.Author = CreateAuthor(hl7GenericMessage);

            // The Reporting Pathologist
            pathologyResultReport.SCSContext.ReportingPathologist = CreateReportingPathologist(hl7GenericMessage);

            // Order Details
            pathologyResultReport.SCSContext.OrderDetails = CreateOrderDetails(hl7GenericMessage);

            // Subject Of Care
            pathologyResultReport.SCSContext.SubjectOfCare = CreateSubjectOfCare(hl7GenericMessage);

            #endregion

            #region Setup and populate the SCS Content model

            // SCS Content model
            pathologyResultReport.SCSContent = PathologyResultReport.CreateSCSContent();

            ReportAttachment reportAttachment = GetReportAttachment(hl7GenericMessage, reportData);

            // Pathology Test Result
            pathologyResultReport.SCSContent.PathologyTestResult = CreatePathologyTestResults(hl7GenericMessage);

            // Related Document
            pathologyResultReport.SCSContent.RelatedDocument = CreateRelatedDocument(hl7GenericMessage, reportAttachment);

            #endregion

            FillInAdditionalMetadata(pathologyResultReport, metadata);

            return new PathologyTransformResult
            {
                PathologyResultReport = pathologyResultReport,
                Attachment = reportAttachment
            };
        }

        /// <summary>
        /// Fill in additional required information in the PathologyResultReport.
        /// </summary>
        /// <param name="report">The PathologyResultReport to complete.</param>
        /// <param name="metadata">Metadata information.</param>
        internal static void FillInAdditionalMetadata(PathologyResultReport report, PathologyMetadata metadata)
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
        /// Create the pathology test results from information in the HL7 V2 message.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns>List of PathologyTestResult</returns>
        internal static IList<PathologyTestResult> CreatePathologyTestResults(HL7GenericMessage genericMessage)
        {
            IList<PathologyTestResult> pathologyTestResults = new List<PathologyTestResult>();

            foreach (var orderGroup in genericMessage.Order)
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
        /// Create the pathology test result from information in the HL7 V2 message.
        /// </summary>
        /// <param name="obrSegment">OBR segment.</param>
        /// <returns>PathologyTestResult</returns>
        internal static PathologyTestResult CreatePathologyTestResult(OBR obrSegment)
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
        /// Create the reporting pathologist from information in the HL7 V2 message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IParticipationReportingPathologist CreateReportingPathologist(HL7GenericMessage message)
        {
            var nameCn = message.Order.First().Observation.First().ObservationsReportID.PrincipalResultInterpreter.name;
            
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
        
    }
}