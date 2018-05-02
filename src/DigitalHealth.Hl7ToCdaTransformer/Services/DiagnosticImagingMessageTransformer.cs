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
using Nehta.VendorLibrary.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    internal class DiagnosticImagingMessageTransformer : MessageTransformerBase
    {
        /// <summary>
        /// Create a DiagnosticImagingTransformResult instance (containing a DiagnosticImagingReport and a ReportAttachment instance) from a 
        /// HL7 V2 diagnostic imaging message. The DiagnosticImagingTransformResult instance is then used to generate a CDA document.
        /// </summary>
        /// <param name="hl7GenericMessage">The HL7 V2 message to transform.</param>
        /// <param name="metadata">Mandatory information to supplement the transform.</param>
        /// <param name="reportData">Report data.</param>
        /// <returns></returns>
        internal DiagnosticImagingTransformResult Transform(HL7GenericMessage hl7GenericMessage, DiagnosticImagingMetadata metadata, byte[] reportData = null)
        {
            DiagnosticImagingReport diagnosticImagingReport = DiagnosticImagingReport.CreateDiagnosticImagingReport();

            // Include Logo
            diagnosticImagingReport.IncludeLogo = false;

            // Set Creation Time
            diagnosticImagingReport.DocumentCreationTime = GetResultsReportStatusChange(hl7GenericMessage);

            // Document Status
            diagnosticImagingReport.DocumentStatus = GetReportStatus(hl7GenericMessage);
            
            #region Setup and populate the CDA context model

            // CDA Context model
            var cdaContext = DiagnosticImagingReport.CreateCDAContext();

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

            diagnosticImagingReport.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model

            // SCS Context model
            diagnosticImagingReport.SCSContext = DiagnosticImagingReport.CreateSCSContext();

            // Author Health Care Provider
            diagnosticImagingReport.SCSContext.Author = CreateAuthor(hl7GenericMessage);

            // The Reporting Radiologist
            diagnosticImagingReport.SCSContext.ReportingRadiologist = CreateReportingRadiologist(hl7GenericMessage);

            // Order Details
            diagnosticImagingReport.SCSContext.OrderDetails = CreateOrderDetails(hl7GenericMessage);

            // Subject Of Care
            diagnosticImagingReport.SCSContext.SubjectOfCare = CreateSubjectOfCare(hl7GenericMessage);

            #endregion

            #region Setup and populate the SCS Content model

            // SCS Content model
            diagnosticImagingReport.SCSContent = DiagnosticImagingReport.CreateSCSContent();

            ReportAttachment reportAttachment = GetReportAttachment(hl7GenericMessage, reportData);

            // Imaging Examination Results
            diagnosticImagingReport.SCSContent.ImagingExaminationResults = CreateImagingExaminationResults(hl7GenericMessage);

            // Related Document
            diagnosticImagingReport.SCSContent.RelatedDocument = CreateRelatedDocument(hl7GenericMessage, reportAttachment);

            #endregion
            
            FillInAdditionalMetadata(diagnosticImagingReport, metadata);

            return new DiagnosticImagingTransformResult
            {
                DiagnosticImagingReport = diagnosticImagingReport,
                Attachment = reportAttachment
            };
            
        }

        /// <summary>
        /// Create the reporting radiologist from information in the HL7 V2 diagnostic imaging message.
        /// </summary>
        /// <param name="message">The HL7 V2 message.</param>
        /// <returns></returns>
        internal static IParticipationReportingRadiologist CreateReportingRadiologist(HL7GenericMessage message)
        {
            var nameCn = message.Order.First().Observation.First().ObservationsReportID.PrincipalResultInterpreter.name;
            
            var reportingRadiologist = DiagnosticImagingReport.CreateReportingRadiologist();

            // Document reportingRadiologist > Participant
            reportingRadiologist.Participant = DiagnosticImagingReport.CreateParticipantForReportingRadiologist();

            // Participation Period
            reportingRadiologist.ParticipationEndTime = GetResultsReportStatusChange(message);

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Document reportingRadiologist > Participant > Person or Organisation or Device > Person > Person Name
            person.PersonNames = new List<IPersonName> { GetPersonNameFromCn(nameCn) };

            // Document reportingRadiologist > Participant > Entity Identifier
            person.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, nameCn.IDnumberST)
            };

            reportingRadiologist.Participant.Person = person;

            return reportingRadiologist;
        }

        /// <summary>
        /// Fill in additional required information in the DiagnosticImagingReport.
        /// </summary>
        /// <param name="report">The DiagnosticImagingReport to complete.</param>
        /// <param name="metadata">Metadata information.</param>
        internal static void FillInAdditionalMetadata(DiagnosticImagingReport report, DiagnosticImagingMetadata metadata)
        {
            report.SCSContext.OrderDetails.RequesterOrderIdentifier = metadata.RequesterOrderIdentifier;
            report.SCSContext.OrderDetails.AccessionNumber = metadata.AccessionNumber;

            // Author
            report.SCSContext.Author.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            report.SCSContext.Author.Participant.Person.Organisation.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, metadata.AuthorOrganisationHpio)
            };

            // Reporting radiologist
            report.SCSContext.ReportingRadiologist.Role = DiagnosticImagingReport.CreateRole(metadata.ReportingRadiologist.Role, CodingSystem.ANZSCO);
            report.SCSContext.ReportingRadiologist.Participant.Addresses = metadata.ReportingRadiologist.Address;
            report.SCSContext.ReportingRadiologist.Participant.ElectronicCommunicationDetails = metadata.ReportingRadiologist.ContactDetails;
            report.SCSContext.ReportingRadiologist.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            report.SCSContext.ReportingRadiologist.Participant.Person.Organisation.Identifiers = new List<Identifier> {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, metadata.ReportingRadiologist.OrganisationHpio )
            };
        }
        
        /// <summary>
        /// Create the diagnostic imaging examination test results from information in the HL7 V2 message.
        /// </summary>
        /// <param name="genericMessage">The HL7 V2 message.</param>
        /// <returns>List of DiagnosticImagingExaminationResult</returns>
        internal IList<IDiagnosticImagingExaminationResult> CreateImagingExaminationResults(HL7GenericMessage hl7GenericMessage)
        {
            IList<IDiagnosticImagingExaminationResult> imagingTestResults = new List<IDiagnosticImagingExaminationResult>();

            foreach (var order in hl7GenericMessage.Order)
            {
                foreach (var observation in order.Observation)
                {
                    OBR obrSegment = observation.ObservationsReportID;
                    
                    if (obrSegment.UniversalServiceID.identifier != TransformerConstants.ReportText)
                    {
                        IDiagnosticImagingExaminationResult testResult = DiagnosticImagingReport.CreateDiagnosticImagingExaminationResult();

                        // ReportingRadiologistForImagingExaminationResult
                        var principalResultInterpreter = hl7GenericMessage.Order.First().Observation.First().ObservationsReportID.PrincipalResultInterpreter.name;
                        testResult.ReportingRadiologistForImagingExaminationResult = GetNameStringFromCN(principalResultInterpreter);

                        // ExaminationResultName
                        testResult.ExaminationResultName = GetTestResultName(obrSegment.UniversalServiceID);
                        
                        // Modality
                        DiagnosticService diagnosticService;
                        if (EnumHelper.TryGetEnumValue<DiagnosticService, NameAttribute>(
                            attribute => attribute.Code == obrSegment.DiagnosticServSectID, out diagnosticService))
                        {
                            testResult.Modality = BaseCDAModel.CreateCodableText(obrSegment.DiagnosticServSectID,
                                CodingSystem.HL7DiagnosticServiceSectionID,
                                diagnosticService.GetAttributeValue<NameAttribute, string>(a => a.Name));
                        }
                        else
                        {
                            testResult.Modality = BaseCDAModel.CreateCodableText(obrSegment.DiagnosticServSectID);
                        }
                        
                        // Examination Procedure (Validate this, must be present)
                        if (obrSegment.UniversalServiceID != null &&
                            !string.IsNullOrEmpty(obrSegment.UniversalServiceID.text))
                        {
                            testResult.ExaminationProcedure = obrSegment.UniversalServiceID.text;
                        }
                        
                        // Examination Details (Validate this, must be present)
                        testResult.ExaminationDetails = DiagnosticImagingReport.CreateExaminationDetails();
                        testResult.ExaminationDetails.ImageDateTime = new ISO8601DateTime(obrSegment.ObservationDateTime.TimestampValue.GetValueOrDefault());
                        
                        // Observation Date Time
                        testResult.ObservationDateTime = new ISO8601DateTime(obrSegment.ObservationDateTime.TimestampValue.GetValueOrDefault());

                        // Overall Result Status
                        Hl7V3ResultStatus resultStatus;
                        if (EnumHelper.TryGetEnumValue<Hl7V3ResultStatus, NameAttribute>(
                            attribute => attribute.Code == obrSegment.ResultStatus, out resultStatus))
                        {
                            testResult.OverallResultStatus = BaseCDAModel.CreateResultStatus(resultStatus);
                        }
                        else
                        {
                            testResult.OverallResultStatus = BaseCDAModel.CreateCodableText(NullFlavour.NoInformation);
                        }

                        imagingTestResults.Add(testResult);
                    }
                }
            }

            return imagingTestResults;
        }


    }

}
