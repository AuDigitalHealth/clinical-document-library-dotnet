/*
 * Copyright 2013 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Xml;
using CDA.Generator.Common.SCSModel.Entities;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;

namespace CDA.R5Samples
{
    /// <summary>
    /// This project is intended to demonstrate how an DiagnosticImagingReport CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// DiagnosticImagingReport class, and then populated with data as appropriate. The three sections that need to be
    /// created and hydrated with data are:
    /// 
    ///     CDA Context (Clinical Document Architecture - Context)
    ///     SCS Context (Structured Document Template - Context)
    ///     SCS Content (Structured Document Template - Content)
    /// 
    /// The CDA Context typically contains information that is to be represented within the header of the document
    /// that is not encapsulated with the SCS context.
    /// E.g. Generic CDA sections or entries; for example custodian.
    /// 
    /// The SCS Context typically contains information that is to be represented within the header of the document
    /// and relates specifically to the type of document that is to be created.
    /// E.g. E-Referral specific CDA sections or entries; for example Subject of Care.
    /// 
    /// The SCS Content typically contains information that is to be represented with the body of the document.
    /// </summary>

    public class DiagnosticImagingReportSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static string OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\DiagnosticImagingReport.xml";
            }
        }

        public static string ImageFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\x-ray.jpg";
            }
        }

        public static String AttachmentFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

       #endregion

        /// <summary>
        /// This sample populates only the mandatory sections / entries
        /// </summary>
        public XmlDocument MinPopulatedDiagnosticImagingReport(string fileName)
        {
            XmlDocument xmlDoc;

            var eDiagnosticImagingReport = PopulatedDiagnosticImagingReport(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                // Pass the Event Summary model into the GenerateDiagnosticImagingReport method 
                xmlDoc = CDAGenerator.GenerateDiagnosticImagingReport(eDiagnosticImagingReport);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings { Indent = true }))
                {
                    if (!fileName.IsNullOrEmptyWhitespace()) xmlDoc.Save(writer);
                }
            }
            catch (ValidationException ex)
            {
                // Catch any validation exceptions
                var validationMessages = ex.GetMessagesString();

                // Handle any validation errors as appropriate.
                throw;
            }

            return xmlDoc;
        }
    
        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries
        /// </summary>
        public XmlDocument MaxPopulatedDiagnosticImagingReport(string fileName)
        {
            XmlDocument xmlDoc;

            var eDiagnosticImagingReport = PopulatedDiagnosticImagingReport(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                // Pass the Event Summary model into the GenerateDiagnosticImagingReport method 
                xmlDoc = CDAGenerator.GenerateDiagnosticImagingReport(eDiagnosticImagingReport);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings { Indent = true }))
                {
                    if (!fileName.IsNullOrEmptyWhitespace()) xmlDoc.Save(writer);
                }
            }
            catch (ValidationException ex)
            {
                // Catch any validation exceptions
                var validationMessages = ex.GetMessagesString();

                //Handle any validation errors as appropriate.
                throw;
            }

            return xmlDoc;
        }

        #region Private Test Methods

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries depending on the  
        /// mandatorySectionsOnly Boolean
        /// </summary>
        internal static DiagnosticImagingReport PopulatedDiagnosticImagingReport(Boolean mandatorySectionsOnly)
        {
            var diagnosticImagingReport = DiagnosticImagingReport.CreateDiagnosticImagingReport();

            // Include Logo
            diagnosticImagingReport.IncludeLogo = true;

            // Set Creation Time
            diagnosticImagingReport.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = DiagnosticImagingReport.CreateCDAContext();
            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid());
            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid());
            // CDA Context Version
            cdaContext.Version = "1";

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, "Queensland Diagnostic Services", mandatorySectionsOnly);

            // Legal Authenticator 
            cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
            GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

            diagnosticImagingReport.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            diagnosticImagingReport.SCSContext = DiagnosticImagingReport.CreateSCSContext();

            // Reporting Radiologist
            diagnosticImagingReport.SCSContext.ReportingRadiologist = CreateReportingRadiologist(mandatorySectionsOnly);

            // Order Details
            diagnosticImagingReport.SCSContext.OrderDetails = CreateOrderDetails(mandatorySectionsOnly);

            // Author Health Care Provider
            diagnosticImagingReport.SCSContext.Author = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(diagnosticImagingReport.SCSContext.Author, "Queensland Diagnostic Services", mandatorySectionsOnly);

            // Subject Of Care
            diagnosticImagingReport.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(diagnosticImagingReport.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            #endregion

            #region Setup and populate the SCS Content model

            // Setup and populate the SCS Content model
            diagnosticImagingReport.SCSContent = DiagnosticImagingReport.CreateSCSContent();

            // Imaging Examination Results
            diagnosticImagingReport.SCSContent.ImagingExaminationResults = new List<IDiagnosticImagingExaminationResult>
            {
               CreateDiagnosticImagingResults(mandatorySectionsOnly, true),
               CreateDiagnosticImagingResults(mandatorySectionsOnly, false)
            };

            // Related Information
            diagnosticImagingReport.SCSContent.RelatedDocument = CreateRelatedDocument(mandatorySectionsOnly);
  
            #endregion

            return diagnosticImagingReport;
        }

        /// <summary>
        /// Creates and hydrates the 'OrderDetails' section.
        /// </summary>
        /// <param name="mandatorySectionsOnly">Includes on the mandatory items</param>
        /// <returns>A hydrated 'OrderDetails' object.</returns>
        public static OrderDetails CreateOrderDetails(Boolean mandatorySectionsOnly)
        {
            // Order Details
            var orderDetails = DiagnosticImagingReport.CreateOrderDetails();

            // Requester Order Identifier
            orderDetails.AccessionNumber = DiagnosticImagingReport.CreateAccessionNumber("8003620833333789", "10523475");

            if (!mandatorySectionsOnly)
            {
                // Requester Order Identifier - Note: Use BaseCDAModel.CreateIdentifier for a non default root element eg.. BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003621231166540", "23451");
                orderDetails.RequesterOrderIdentifier = DiagnosticImagingReport.CreateRequesterOrderIdentifier("8003620833333789", "23451");
            }

            // Requester
            orderDetails.Requester = GenericObjectReuseSample.CreateRequester(mandatorySectionsOnly);

            return orderDetails;
        }

        /// <summary>
        /// Creates a Related Document.
        /// </summary>
        /// <returns>RelatedDocument</returns>
        public static RelatedDocument CreateRelatedDocument(Boolean mandatorySectionsOnly)
        {
            RelatedDocument relatedDocument = PathologyResultReport.CreateRelatedDocument();

            // Pathology PDF
            ExternalData attachmentPdf = BaseCDAModel.CreateExternalData();
            attachmentPdf.ExternalDataMediaType = MediaType.PDF;
            attachmentPdf.Path = AttachmentFileNameAndPath;
            relatedDocument.ExaminationResultRepresentation = attachmentPdf;

            // Document Provenance
            relatedDocument.DocumentDetails = CreateDocumentProvenance(mandatorySectionsOnly);

            return relatedDocument;
        }

        /// <summary>
        /// Creates a Document Provenance.
        /// </summary>
        /// <returns>DocumentProvenance</returns>
        public static DocumentDetails CreateDocumentProvenance(Boolean mandatorySectionsOnly)
        {
            DocumentDetails documentDetails = DiagnosticImagingReport.CreateDocumentProvenance();

            // Result Status
            documentDetails.ReportDate = new ISO8601DateTime(DateTime.Now);

            // Result Status
            documentDetails.ReportStatus = BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.FinalResults, "Final Results"); // or BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.FinalResults)

            // Result Status
            documentDetails.ReportDescription = "Report Description";

            return documentDetails;
        }

        /// <summary>
        /// Creates and hydrates the 'Examination Details' section.
        /// </summary>
        /// <param name="mandatorySectionsOnly">Includes on the mandatory sections</param>
        /// <returns>A hydrated 'CreateExaminationDetails' object.</returns>
        public static ExaminationDetails CreateExaminationDetails(Boolean mandatorySectionsOnly)
        {
            var examinationDetails = DiagnosticImagingReport.CreateExaminationDetails();

            // Image DateTime
            examinationDetails.ImageDateTime = new ISO8601DateTime(DateTime.Now);

            return examinationDetails;
        }

        /// <summary>
        /// Creates and hydrates the 'Imaging Examination Results' section.
        /// </summary>
        /// <param name="mandatorySectionsOnly">Includes on the mandatory sections</param>
        /// <param name="showDescription">Determines wether the anatomicalSite.Description is displayed or not</param>
        /// <returns>A hydrated 'IImagingExaminationResult' object.</returns>
        public static IDiagnosticImagingExaminationResult CreateDiagnosticImagingResults(Boolean mandatorySectionsOnly, bool showDescription)
        {
            var diagnosticImagingExaminationResult = DiagnosticImagingReport.CreateDiagnosticImagingExaminationResult();

            // Examination Result Name
            diagnosticImagingExaminationResult.ExaminationResultName = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMED, "Plain chest X-ray");

            // Begin Modality (Imaging Modality)
            diagnosticImagingExaminationResult.Modality = BaseCDAModel.CreateCodableText("363680008", CodingSystem.SNOMED, "X-ray");

            // Observation Date Time
            diagnosticImagingExaminationResult.ObservationDateTime = new ISO8601DateTime(DateTime.Now);

            // ExaminationDetails
            diagnosticImagingExaminationResult.ExaminationDetails = CreateExaminationDetails(mandatorySectionsOnly);

            // Examination Procedure
            diagnosticImagingExaminationResult.ExaminationProcedure = "The examination was carried out using the particular procedure.";

            // Overall Result Status
            diagnosticImagingExaminationResult.OverallResultStatus = BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.FinalResults, "Final Results"); // or BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.FinalResults)

            if (!mandatorySectionsOnly)
            {
                // Please note optional field - Note: This field is only displayed in the Narrative
                diagnosticImagingExaminationResult.ReportingRadiologistForImagingExaminationResult = "DR Arreza Araceli";

                // Related Image
                diagnosticImagingExaminationResult.RelatedImage = BaseCDAModel.CreateRelatedImage("http://bassendeanwellness.com.au/sites/default/files/Chest%2520X-Ray%2520Image.jpg", MediaType.JPEG);

                // Anatomical Site
                var anatomicalSite = DiagnosticImagingReport.CreateAnatomicalSiteExtended();

                // Anatomical Location
                anatomicalSite.SpecificLocation = BaseCDAModel.CreateAnatomicalLocation();

                if (showDescription)
                {
                   // Description
                   anatomicalSite.Description = "Chest/Thorax";
                }
                else
                {
                    // Anatomical Location - Name Of Location
                    anatomicalSite.SpecificLocation.NameOfLocation = BaseCDAModel.CreateCodableText("302551006", CodingSystem.SNOMED, "Entire thorax");

                    // Anatomical Location - Side
                    anatomicalSite.SpecificLocation.Side = BaseCDAModel.CreateCodableText("7771000", CodingSystem.SNOMED, "left");

                    // Anatomical Region
                    diagnosticImagingExaminationResult.AnatomicalRegion = AnatomicalRegion.Chest;
                }

                // Assign Anatomical Site
                diagnosticImagingExaminationResult.AnatomicalSite = new List<IAnatomicalSiteExtended> 
                { 
                    anatomicalSite, 
                    anatomicalSite 
                };
            }

            return diagnosticImagingExaminationResult;
        }

        #region participants 

        /// <summary>
        /// Creates and Hydrates a IParticipationReceivingLaboratory
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>IParticipationReceivingLaboratory</returns>
        private static IParticipationReportingRadiologist CreateReportingRadiologist(Boolean mandatoryOnly)
        {
            // Receiving Laboratory
            var reportingPathologist = BaseCDAModel.CreateReportingRadiologist();

            // Document reportingPathologist > Participant
            reportingPathologist.Participant = BaseCDAModel.CreateParticipantForReportingRadiologist();

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Participation Period
            reportingPathologist.ParticipationEndTime = new ISO8601DateTime(DateTime.Now);

            // Document reportingPathologist > Role
            reportingPathologist.Role = DiagnosticImagingReport.CreateRole(Occupation.MedicalLaboratoryScientist);

            // Document reportingPathologist > Participant > Person or Organisation or Device > Person > Person Name
            var name = BaseCDAModel.CreatePersonName();
            name.FamilyName = "Radiologist";
            person.PersonNames = new List<IPersonName> { name };

            // Document reportingPathologist > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> { 
                  BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118"),
          };

            // Employment Organisation
            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Name = "Hay Bill Hospital";
            person.Organisation.NameUsage = OrganisationNameUsage.Other;
            person.Organisation.Department = "Some department service provider";
            person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText("Casual");
            person.Organisation.Occupation = DiagnosticImagingReport.CreateRole(Occupation.GeneralMedicalPractitioner);
            person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Radiologist");

            person.Organisation.Identifiers = new List<Identifier> { 
              BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
              BaseCDAModel.CreateIdentifier("SampleReportingId", null, null, "1.2.3.4.5.66666", null)
          };

            if (!mandatoryOnly)
            {
                // Document reportingPathologist > Participant > Electronic Communication Detail
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);

                var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "authen@globalauthens.com",
                    ElectronicCommunicationMedium.Email,
                    ElectronicCommunicationUsage.WorkPlace);

                reportingPathologist.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

                // Document reportingPathologist > Participant > Address
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.Business;
                address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var addressList = new List<IAddress> { address1, address2 };
                reportingPathologist.Participant.Addresses = addressList;

                // Participant > Entitlement 1
                var medicarePharmacyApprovalNumberEntitlement = BaseCDAModel.CreateEntitlement();
                var code = BaseCDAModel.CreateCodableText("11", CodingSystem.NCTISEntitlementTypeValues, "Medicare Pharmacy Approval Number", null, null);
                medicarePharmacyApprovalNumberEntitlement.Id = BaseCDAModel.CreateIdentifier("Pharmacy",
                                                                    null,
                                                                    "1234567892",
                                                                    "1.2.36.174030967.1.3.2.1",
                                                                    code);
                medicarePharmacyApprovalNumberEntitlement.Type = EntitlementType.MedicarePharmacyApprovalNumber;
                medicarePharmacyApprovalNumberEntitlement.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                // Participant > Entitlement 2
                var medicarePrescriberNumberEntitlement = BaseCDAModel.CreateEntitlement();
                var medicarePharmacyApprovalNumberCode = BaseCDAModel.CreateCodableText("10", CodingSystem.NCTISEntitlementTypeValues, "Medicare Prescriber Number", null, null);
                medicarePrescriberNumberEntitlement.Id = BaseCDAModel.CreateIdentifier("Prescriber",
                                                                    null,
                                                                    "049960CT",
                                                                    "1.2.36.174030967.0.3",
                                                                    medicarePharmacyApprovalNumberCode);

                medicarePrescriberNumberEntitlement.Type = EntitlementType.MedicarePrescriberNumber;
                medicarePrescriberNumberEntitlement.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                reportingPathologist.Participant.Entitlements = new List<Entitlement> { medicarePharmacyApprovalNumberEntitlement, medicarePrescriberNumberEntitlement };

                name.GivenNames = new List<string> { "Good" };
                name.Titles = new List<string> { "Doctor" };
                name.NameUsages = new List<NameUsage> { NameUsage.Legal };

                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                address1.AustralianAddress.DeliveryPointId = 32568931;

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "2 Clinician Street" };
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";
                address2.AustralianAddress.DeliveryPointId = 32568931;

                // Qualifications
                reportingPathologist.Participant.Qualifications = "FRACGP";
            }

            reportingPathologist.Participant.Person = person;

            return reportingPathologist;
        }

        #endregion

        #endregion
    }
}