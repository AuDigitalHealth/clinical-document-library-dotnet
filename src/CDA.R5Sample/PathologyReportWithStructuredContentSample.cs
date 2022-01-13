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
using System.Linq;
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
using PathologyReportWithStructuredContent = Nehta.VendorLibrary.CDA.Common.PathologyReportWithStructuredContent;
using PathologyTestResult = Nehta.VendorLibrary.CDA.SCSModel.Common.PathologyTestResult;

namespace CDA.R5Samples
{
    /// <summary>
    /// This project is intended to demonstrate how an PathologyReportWithStructuredContent CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// PathologyReportWithStructuredContent class, and then populated with data as appropriate. The three sections that need to be
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

    public class PathologyReportWithStructuredContentSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static DateTime DateTimeNow { get; set; }

        public static string ImageFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\x-ray.jpg";
            }
        }

        public static string OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PathologyReportWithStructuredContent.xml";
            }
        }

        public static string AttachmentFileNameAndPath
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
        public XmlDocument MinPopulatedPathologyReportWithStructuredContent(string fileName)
        {
            XmlDocument xmlDoc;

            var ePathologyReportWithStructuredContent = PopulatedPathologyReportWithStructuredContent(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GeneratePathologyReportWithStructuredContent method 
                xmlDoc = CDAGenerator.GeneratePathologyReportWithStructuredContent(ePathologyReportWithStructuredContent);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings { Indent = true }))
                {
                    if (!fileName.IsNullOrEmptyWhitespace()) xmlDoc.Save(writer);
                }
            }
            catch (ValidationException ex)
            {
                //Catch any validation exceptions
                var validationMessages = ex.GetMessagesString();

                //Handle any validation errors as appropriate.
                throw;
            }

            return xmlDoc;
        }

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries
        /// </summary>
        public XmlDocument MaxPopulatedPathologyReportWithStructuredContent(string fileName)
        {
            XmlDocument xmlDoc;

            var ePathologyReportWithStructuredContent = PopulatedPathologyReportWithStructuredContent(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GeneratePathologyReportWithStructuredContent method 
                xmlDoc = CDAGenerator.GeneratePathologyReportWithStructuredContent(ePathologyReportWithStructuredContent);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings { Indent = true }))
                {
                    if (!fileName.IsNullOrEmptyWhitespace()) xmlDoc.Save(writer);
                }
            }
            catch (ValidationException ex)
            {
                //Catch any validation exceptions
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
        internal static PathologyReportWithStructuredContent PopulatedPathologyReportWithStructuredContent(Boolean mandatorySectionsOnly)
        {
            var pathologyReportWithStructuredContent = PathologyReportWithStructuredContent.CreatePathologyReportWithStructuredContent();

            // Include Logo
            pathologyReportWithStructuredContent.IncludeLogo = true;
            pathologyReportWithStructuredContent.LogoPath = OutputFolderPath;

            // Set Creation Time
            pathologyReportWithStructuredContent.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = PathologyReportWithStructuredContent.CreateCDAContext();
            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
            // CDA Context Version
            cdaContext.Version = "1";
            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, "Pathology Queensland", mandatorySectionsOnly);
            // Legal Authenticator
            cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
            GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

            pathologyReportWithStructuredContent.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            pathologyReportWithStructuredContent.SCSContext = PathologyReportWithStructuredContent.CreateSCSContext();

            // Author Health Care Provider
            pathologyReportWithStructuredContent.SCSContext.Author = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(pathologyReportWithStructuredContent.SCSContext.Author, "Pathology Queensland", mandatorySectionsOnly);

            // The Reporting Pathologist 0..1 to 1..*
            List<IParticipationReportingPathologist> pathologists = new List<IParticipationReportingPathologist>();
            pathologists.Add(CreateReportingPathologist(mandatorySectionsOnly));
            
            // Add a second for Max
            if (!mandatorySectionsOnly)
            {
                pathologists.Add(CreateReportingPathologist(mandatorySectionsOnly));
            }

            pathologyReportWithStructuredContent.SCSContext.ReportingPathologists = pathologists.ToList();

            
            // Order Details
            pathologyReportWithStructuredContent.SCSContext.OrderDetails = CreateOrderDetails(mandatorySectionsOnly);

            // Subject Of Care
            pathologyReportWithStructuredContent.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(pathologyReportWithStructuredContent.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            pathologyReportWithStructuredContent.SCSContent = PathologyReportWithStructuredContent.CreateSCSContent();

            // Pathology Test Result
            pathologyReportWithStructuredContent.SCSContent.PathologyTestResult = new List<PathologyTestResult>
            {
               CreatePathologyResults(mandatorySectionsOnly),
               CreatePathologyResults(mandatorySectionsOnly)
            };

            // Related Document
            pathologyReportWithStructuredContent.SCSContent.RelatedDocument = CreateRelatedDocument(mandatorySectionsOnly);

            #endregion

            return pathologyReportWithStructuredContent;
        }

        #region Pathology Structured Data Section

        /// <summary>
        /// Creates and hydrates the 'Pathology Test Results' section.
        /// </summary>
        /// <returns>A hydrated 'PathologyTestResult' object.</returns>
        public static PathologyTestResult CreatePathologyResults(Boolean mandatorySectionsOnly)
        {
            DateTimeNow = DateTime.Now;

            // Pathology test result
            PathologyTestResult pathologyTestResult = BaseCDAModel.CreatePathologyTestResult();

            // Test Result Name
            pathologyTestResult.TestResultName = BaseCDAModel.CreateCodableText("104950002", CodingSystem.SNOMED, "Sulfhaemoglobin measurement");

            // Diagnostic Service
            pathologyTestResult.DiagnosticService = DiagnosticServiceSectionID.Chemistry;

            // Overall Pathology Test Result Status
            pathologyTestResult.OverallTestResultStatus = BaseCDAModel.CreateCodableText(HL7ResultStatus.FinalResultsResultsStoredAndVerifiedCanOnlyBeChangedWithACorrectedResult);

            // Observation Date Time
            pathologyTestResult.ObservationDateTime = new ISO8601DateTime(DateTimeNow);

            if (!mandatorySectionsOnly)
            {
                // Clinical Information Provided
                pathologyTestResult.ClinicalInformationProvided = "Hepatitus";

                //Pathological Diagnosis
                pathologyTestResult.PathologicalDiagnosis = new List<ICodableText>
                                                            {
                                                                BaseCDAModel.CreateCodableText("17621005", CodingSystem.SNOMED, "Normal"),
                                                                BaseCDAModel.CreateCodableText("263654008", CodingSystem.SNOMED, "Abnormal")
                                                            };

                // Conclusion
                pathologyTestResult.Conclusion = "Test Result Group Conclusion";

                // Test Result Representation
                pathologyTestResult.TestResultRepresentation = BaseCDAModel.CreateEncapsulatedData();
                pathologyTestResult.TestResultRepresentation.ExternalData = BaseCDAModel.CreateExternalData(MediaType.PDF, AttachmentFileNameAndPath, "Test Result Representation");

                //
                // Demonstrating Text for EncapsulatedData
                //
                //pathologyTestResult.TestResultRepresentation = BaseCDAModel.CreateEncapsulatedData();
                //pathologyTestResult.TestResultRepresentation.Text = "Lipase 150 U/L (RR < 70)";


                // Test Comment
                pathologyTestResult.TestComment = "Test Result Group Comment";

                // Test request details one
                ITestRequest testRequestDetailsOne = BaseCDAModel.CreateTestRequest();

                // Requester Order Identifier
                testRequestDetailsOne.RequesterOrderIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.1.2001.1005.52.8003620833333789", "10523479");

                // LaboratoryTestResultIdentifier
                testRequestDetailsOne.LaboratoryTestResultIdentifier = BaseCDAModel.CreateInstanceIdentifier(BaseCDAModel.CreateGuid(), "Laboratory Test Result Identifier");

                // Tests Requested Name
                testRequestDetailsOne.TestsRequestedName = new List<ICodableText>
                                              {
                                                  BaseCDAModel.CreateCodableText("401324008", CodingSystem.SNOMED, "Urinary microscopy, culture and sensitivities"),
                                                  BaseCDAModel.CreateCodableText("401324008", CodingSystem.SNOMED, "Urinary microscopy, culture and sensitivities"),
                                              };


                // Test Request Details
                pathologyTestResult.TestRequestDetails = new List<ITestRequest>
                                              {
                                                  testRequestDetailsOne
                                              };

                // Result Group (PATHOLOGY TEST RESULT GROUP)
                pathologyTestResult.ResultGroup = new List<ITestResultGroup>
                                              {
                                                  CreateTestResultGroup(mandatorySectionsOnly)
                                              };
            }
            else
            {
                // Reporting Pathologist
                pathologyTestResult.ReportingPathologist = CreateReportingPathologist(mandatorySectionsOnly);
            }

            pathologyTestResult.TestSpecimenDetail = new List<SpecimenDetail>
            {
                CreateTestSpecimenDetail(mandatorySectionsOnly)
            };


            return pathologyTestResult;
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

            if (!mandatorySectionsOnly)
            {
                // Requester Order Identifier
                orderDetails.RequesterOrderIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003620833333789", "10523479");

                // Requester Order Identifier
                orderDetails.RequestedTestName = BaseCDAModel.CreateCodableText("26604007", CodingSystem.SNOMED, "Complete blood count");
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
            // Related Document
            RelatedDocument relatedDocument = PathologyReportWithStructuredContent.CreateRelatedDocument();

            // Examination Result Representation
            relatedDocument.ExaminationResultRepresentation = BaseCDAModel.CreateExternalData(MediaType.PDF, AttachmentFileNameAndPath, null); ;

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
            DocumentDetails documentDetails = BaseCDAModel.CreateDocumentDetails();

            // Report Identifier
            documentDetails.ReportIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.54.8003620833333789", "10523477");

            // Report Date 
            documentDetails.ReportDate = new ISO8601DateTime(DateTimeNow);

            // Result Status
            documentDetails.ReportStatus = BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.CorrectionToResults, "Correction To Results"); // or BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.CorrectionToResults)

            // Report Name 
            documentDetails.ReportDescription = "Full Blood Count";

            return documentDetails;
        }

        /// <summary>
        /// Creates and hydrates the 'Other Test Result' Attachment section.
        /// </summary>
        /// <returns>A hydrated 'OtherTestResult' object.</returns>
        public static OtherTestResult CreateOtherTestResultAttachment()
        {
            var otherTestResult = BaseCDAModel.CreateOtherTestResult();

            // Report Name
            otherTestResult.ReportName = BaseCDAModel.CreateCodableText("Report with Attachment");

            // Report Status
            otherTestResult.ReportStatus = BaseCDAModel.CreateCodableText(Hl7V3ResultStatus.NoOrderOnRecordForThisTest);

            // Report ExternalData
            ExternalData report = BaseCDAModel.CreateExternalData(MediaType.PDF,AttachmentFileNameAndPath,"Path File");
            otherTestResult.ReportContent = BaseCDAModel.CreateEncapsulatedData();
            otherTestResult.ReportContent.ExternalData = report;

            // Report Date
            otherTestResult.ReportDate = new ISO8601DateTime(DateTime.Now.AddDays(-2));

            return otherTestResult;
        }

        /// <summary>
        /// Creates and hydrates the 'Other Test Result' text only section.
        /// </summary>
        /// <returns>A hydrated 'OtherTestResult' object.</returns>
        public static OtherTestResult CreateOtherTestResultText()
        {
            var otherTestResult = BaseCDAModel.CreateOtherTestResult();
            // Report Name
            otherTestResult.ReportName = BaseCDAModel.CreateCodableText("Report Name");

            // Report Status
            otherTestResult.ReportStatus = BaseCDAModel.CreateCodableText(HL7ResultStatus.CorrectionToResults);

            // Report Content
            otherTestResult.ReportContent = BaseCDAModel.CreateEncapsulatedData();

            // Report Content
            otherTestResult.ReportContent.Text = "Report Content - Text";

            // Report Date
            otherTestResult.ReportDate = new ISO8601DateTime(DateTimeNow);

            return otherTestResult;
        }

        /// <summary>
        /// Creates and hydrates the 'Test Result Group' section.
        /// </summary>
        /// <returns>A hydrated 'Test Result Group' object.</returns>
        public static ITestResultGroup CreateTestResultGroup(Boolean mandatorySectionsOnly)
        {
            // Test Result Group
            ITestResultGroup testResultGroup = BaseCDAModel.CreateTestResultGroup();

            // Pathology TestResult Group Name
            testResultGroup.ResultGroupName = BaseCDAModel.CreateCodableText("18719-5", CodingSystem.LOINC, "Chemistry Studies (Set)");

            // Result (INDIVIDUAL PATHOLOGY TEST RESULT)
            testResultGroup.Results = new List<ITestResult>
            {
                CreateTestResult(mandatorySectionsOnly)
            };

            if (!mandatorySectionsOnly)
            {
                // Result Group Specimen Detail (SPECIMEN)
                testResultGroup.ResultGroupSpecimenDetail = CreateTestSpecimenDetail(mandatorySectionsOnly);
            }

            return testResultGroup;
        }

        /// <summary>
        /// Creates and hydrates the 'Test Result' section.
        /// </summary>
        /// <returns>A hydrated 'Test Result' object.</returns>
        public static ITestResult CreateTestResult(Boolean mandatorySectionsOnly)
        {
            // Result (INDIVIDUAL PATHOLOGY TEST RESULT)
            ITestResult resultGroup = BaseCDAModel.CreateTestResult();

            resultGroup.ResultName = BaseCDAModel.CreateCodableText("14682-9", CodingSystem.LOINC, "Creatinine [Moles/volume] in Serum or Plasma");

            // Individual Pathology Test Result Status
            resultGroup.ResultStatus = BaseCDAModel.CreateCodableText(HL7ResultStatus.FinalResultsResultsStoredAndVerifiedCanOnlyBeChangedWithACorrectedResult);

            // Result Value (INDIVIDUAL PATHOLOGY TEST RESULT VALUE)
            resultGroup.ResultValue = BaseCDAModel.CreateResultValue();

            // Test Result Value
            resultGroup.ResultValue.TestResultValue = BaseCDAModel.CreateQuantity("12.88", "ml");

            if (!mandatorySectionsOnly)
            {
                // Individual Pathology Test Result Comment
                resultGroup.Comments = new List<string> { "Sodium test result comments",
                                                          "More comments", 
                                                          "Another comment" };

                // Individual Pathology Test Result Reference Range Guidance
                resultGroup.ReferenceRangeGuidance = "Reference range guidance comments for Sodium; eg. the quantity should be between the high and low values";

                // Normal Status
                resultGroup.NormalStatus = HL7ObservationInterpretationNormality.Normal;

                // Individual Pathology Test Result Value Reference Ranges (REFERENCE RANGE DETAILS)
                var resultValueReferenceRangeDetail = BaseCDAModel.CreateResultValueReferenceRangeDetail();

                // Reference Range
                resultValueReferenceRangeDetail.Range = BaseCDAModel.CreateQuantityRange(50, 100, "ml");

                // Reference Range Meaning
                resultValueReferenceRangeDetail.ResultValueReferenceRangeMeaning = BaseCDAModel.CreateCodableText("75540009", CodingSystem.SNOMEDCT, "High");

                // Individual Pathology Test Result Value Reference Ranges (REFERENCE RANGE DETAILS)
                resultGroup.ResultValueReferenceRangeDetails = new List<ResultValueReferenceRangeDetail>
                                                               {
                                                                   resultValueReferenceRangeDetail, 
                                                                   resultValueReferenceRangeDetail, 
                                                               };
            }

            return resultGroup;
        }

        /// <summary>
        /// Creates and hydrates the 'SpecimenDetail' section.
        /// </summary>
        /// <returns>A hydrated 'SpecimenDetail' object.</returns>
        public static SpecimenDetail CreateTestSpecimenDetail(Boolean mandatorySectionsOnly)
        {
            // Test Specimen Detail (SPECIMEN)
            SpecimenDetail specimenDetailOne = BaseCDAModel.CreateSpecimenDetail();

            // Date and Time of Collection (Collection DateTime)
            specimenDetailOne.CollectionDateTime = new ISO8601DateTime(DateTimeNow);

            if (!mandatorySectionsOnly)
            {
                // Specimen Tissue Type
                specimenDetailOne.SpecimenTissueType = BaseCDAModel.CreateCodableText("85756007", CodingSystem.SNOMED, "Body tissue structure");

                // Collection Procedure
                specimenDetailOne.CollectionProcedure = BaseCDAModel.CreateCodableText("82078001", CodingSystem.SNOMED, "Collection of blood specimen for laboratory");

                // Anatomical Site (ANATOMICAL LOCATION)
                specimenDetailOne.AnatomicalSite = new List<AnatomicalSite>
                {
                    CreateAnatomicalSite(
                       BaseCDAModel.CreateCodableText("88738008", CodingSystem.SNOMED, "Subcutaneous tissue structure of lateral surface of index finger"),
                       BaseCDAModel.CreateCodableText("7771000", CodingSystem.SNOMED, "Left"),
                       new List<ExternalData>
                       {
                           BaseCDAModel.CreateExternalData(MediaType.JPEG, ImageFileNameAndPath, "Anatomical Site")
                       }
                    ),
                    CreateAnatomicalSite(
                       "Subcutaneous tissue structure of lateral surface of index finger"
                    ),
                };

                // Anatomical Location Description
                specimenDetailOne.PhysicalDescription = "Physical Details Description";

                // Physical Details (PHYSICAL PROPERTIES OF AN OBJECT)
                specimenDetailOne.PhysicalDetails = new List<PhysicalDetails> { 
                    BaseCDAModel.CreatePhysicalDetails("6", "ml", BaseCDAModel.CreateExternalData(MediaType.PDF, AttachmentFileNameAndPath, "Physical Details One"))
                };

                // Sampling Preconditions
                specimenDetailOne.SamplingPreconditions = BaseCDAModel.CreateCodableText("16985007", CodingSystem.SNOMED, "fasting");

                // Collection Setting
                specimenDetailOne.CollectionSetting = "Ward 1A";

                // Date and Time of Receipt (DateTime Received)
                specimenDetailOne.ReceivedDateTime = new ISO8601DateTime(DateTime.Now);

                // Parent Specimen Identifier
                specimenDetailOne.ParentSpecimenIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.84425496912", BaseCDAModel.CreateGuid());

                // Container Identifier
                specimenDetailOne.ContainerIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.84425496912", BaseCDAModel.CreateGuid());

                // Specimen Identifier
                specimenDetailOne.SpecimenIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.84425496912", BaseCDAModel.CreateGuid());

            }

            return specimenDetailOne;
        }

        /// <summary>
        /// Creates and Hydrates an atomicalSite
        /// </summary>
        /// <returns>AnatomicalSite</returns>
        private static AnatomicalSite CreateAnatomicalSite(string description, ICodableText side, List<ExternalData> images = null)
        {
            var anatomicalSite = BaseCDAModel.CreateAnatomicalSite();
            anatomicalSite.Description = description;

            // Specific Location
            anatomicalSite.SpecificLocation = BaseCDAModel.CreateSpecificLocation();

            // Side
            anatomicalSite.SpecificLocation.Side = side;

            // Images
            anatomicalSite.Images = images;

            return anatomicalSite;
        }

        /// <summary>
        /// Creates and Hydrates an atomicalSite
        /// </summary>
        /// <returns>AnatomicalSite</returns>
        private static AnatomicalSite CreateAnatomicalSite(string description)
        {
            var anatomicalSite = BaseCDAModel.CreateAnatomicalSite();
            anatomicalSite.Description = description;

            return anatomicalSite;
        }

        /// <summary>
        /// Creates and Hydrates an atomicalSite
        /// </summary>
        /// <returns>AnatomicalSite</returns>
        private static AnatomicalSite CreateAnatomicalSite(ICodableText nameOfLocation, ICodableText side, List<ExternalData> images = null)
        {
            // Anatomical Site
            var anatomicalSite = BaseCDAModel.CreateAnatomicalSite();

            // Specific Location
            anatomicalSite.SpecificLocation = BaseCDAModel.CreateSpecificLocation();

            // Name O fLocation
            anatomicalSite.SpecificLocation.NameOfLocation = nameOfLocation;

            // Side
            anatomicalSite.SpecificLocation.Side = side;

            // Images
            anatomicalSite.Images = images;

            return anatomicalSite;
        }

        #endregion

        #region Participants

        /// <summary>
        /// Creates and Hydrates a IParticipationReceivingLaboratory
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>IParticipationReceivingLaboratory</returns>
        private static IParticipationReportingPathologist CreateReportingPathologist(Boolean mandatoryOnly)
        {
            // Receiving Laboratory
            var reportingPathologist = PathologyReportWithStructuredContent.CreateReportingPathologist();

            // Document reportingPathologist > Participant
            reportingPathologist.Participant = PathologyReportWithStructuredContent.CreateParticipantForReportingPathologist();

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Participation Period
            reportingPathologist.ParticipationEndTime = new ISO8601DateTime(DateTime.Now);

            // Document reportingPathologist > Role
            reportingPathologist.Role = PathologyReportWithStructuredContent.CreateRole(Occupation.Pathologist);

            // Document reportingPathologist > Participant > Person or Organisation or Device > Person > Person Name
            var name = BaseCDAModel.CreatePersonName();
            name.FamilyName = "Healthy";

            person.PersonNames = new List<IPersonName> { name };

            // Document reportingPathologist > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();
            address1.AddressPurpose = AddressPurpose.Business;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.Business;
            address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var addressList = new List<IAddress> { address1, address2 };

            reportingPathologist.Participant.Addresses = addressList;

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

            // Participant > Entitlement
            var entitlement = BaseCDAModel.CreateEntitlement();
            var code = BaseCDAModel.CreateCodableText("11", CodingSystem.NCTISEntitlementTypeValues, "Medicare Pharmacy Approval Number", null, null);
            entitlement.Id = BaseCDAModel.CreateIdentifier("Pharmacy",
                                                           null,
                                                           "1234567892",
                                                           "1.2.36.174030967.1.3.2.1",
                                                           code);

            entitlement.Type = EntitlementType.MedicarePharmacyApprovalNumber;
            entitlement.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

            reportingPathologist.Participant.Entitlements = new List<Entitlement> { entitlement, entitlement };

            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Name = "Hay Bill Hospital";
            person.Organisation.NameUsage = OrganisationNameUsage.Other;
            person.Organisation.Department = "Some department service provider";
            person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(Hl7V3EmployeeJobClass.PartTime);
            person.Organisation.Occupation = PathologyReportWithStructuredContent.CreateRole(Occupation.GeneralMedicalPractitioner);
            person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);

            person.Organisation.Identifiers = new List<Identifier> { 
                 BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
                 BaseCDAModel.CreateIdentifier("NATA", null, "3715" , "1.2.36.1.2001.1005.12" , BaseCDAModel.CreateCodableText("XX",CodingSystem.HL7IdentifierType,"Organization identifier"))
              };

            // Document reportingPathologist > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> { 
                 BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118"),
            };

            if (!mandatoryOnly)
            {
                name.GivenNames = new List<string> { "Fitun" };
                name.Titles = new List<string> { "Dr" };
                name.NameUsages = new List<NameUsage> { NameUsage.Legal };

                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "2 Clinician Street" };
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";

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