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
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;
using PathologyTestResult = Nehta.VendorLibrary.CDA.SCSModel.Pathology.PathologyTestResult;

namespace CDA.R5Samples
{
    /// <summary>
    /// This project is intended to demonstrate how an PathologyResultReport CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// PathologyResultReport class, and then populated with data as appropriate. The three sections that need to be
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

    public class PathologyResultReportSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PathologyResultReport.xml";
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
        public XmlDocument MinPopulatedPathologyResultReport(string fileName)
        {
            XmlDocument xmlDoc;

            var ePathologyResultReport = PopulatedPathologyResultReport(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GeneratePathologyResultReport method 
                xmlDoc = CDAGenerator.GeneratePathologyResultReport(ePathologyResultReport);

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
        public XmlDocument MaxPopulatedPathologyResultReport(string fileName)
        {
            XmlDocument xmlDoc;

            var ePathologyResultReport = PopulatedPathologyResultReport(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GeneratePathologyResultReport method 
                xmlDoc = CDAGenerator.GeneratePathologyResultReport(ePathologyResultReport);

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
        internal static PathologyResultReport PopulatedPathologyResultReport(Boolean mandatorySectionsOnly)
        {
            var pathologyResultReport = PathologyResultReport.CreatePathologyResultReport();

            // Include Logo
            pathologyResultReport.IncludeLogo = true;
            pathologyResultReport.LogoPath = OutputFolderPath;

            // Set Creation Time
            pathologyResultReport.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = PathologyResultReport.CreateCDAContext();
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

            pathologyResultReport.CDAContext = cdaContext;

            #endregion

            // Setup and Populate the SCS Context model
            #region Setup and Populate the SCS Context model

            pathologyResultReport.SCSContext = PathologyResultReport.CreateSCSContext();

            // Author Health Care Provider
            pathologyResultReport.SCSContext.Author = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(pathologyResultReport.SCSContext.Author, "Pathology Queensland", mandatorySectionsOnly);

            // The Reporting Pathologist
            pathologyResultReport.SCSContext.ReportingPathologist = CreateReportingPathologist(mandatorySectionsOnly);

            // Order Details
            pathologyResultReport.SCSContext.OrderDetails = CreateOrderDetails(mandatorySectionsOnly);

            // Subject Of Care
            pathologyResultReport.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(pathologyResultReport.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            pathologyResultReport.SCSContent = PathologyResultReport.CreateSCSContent();

            // Pathology Test Result
            pathologyResultReport.SCSContent.PathologyTestResult = new List<PathologyTestResult>
            {
               CreatePathologyTestResult(mandatorySectionsOnly ? null : "DR Arreza Araceli"),
               CreatePathologyTestResult(mandatorySectionsOnly ?  null : "DR Arshi Lakdawala")
            };

            // Related Document
            pathologyResultReport.SCSContent.RelatedDocument = CreateRelatedDocument(mandatorySectionsOnly);

            #endregion

            return pathologyResultReport;
        }

        /// <summary>
        /// Creates a pathology test result.
        /// </summary>
        /// <returns>PathologyTestResult</returns>
        private static PathologyTestResult CreatePathologyTestResult(string reportingPathologistName)
        {
          PathologyTestResult testResult = PathologyResultReport.CreatePathologyTestResult();

          // Please note optional field - Note: This field is only displayed in the Narrative
          testResult.ReportingPathologistForTestResult = reportingPathologistName;

          // Test Result Name
          testResult.TestResultName = BaseCDAModel.CreateCodableText("275711006", CodingSystem.SNOMED, "Serum chemistry test");

          // Department Code
          testResult.PathologyDiscipline = DiagnosticService.Laboratory;

          // Department Code
          testResult.OverallTestResultStatus = BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.Preliminary, "Preliminary"); // or BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.Preliminary)

          // Test Specimen Detail
          testResult.TestSpecimenDetail = CreateTestSpecimenDetail();

          // Pathology Test Result Date Time
          testResult.ObservationDateTime = new ISO8601DateTime(DateTime.Parse("27 Feb 2007 13:00"));

          return testResult;
        }

        /// <summary>
        /// Creates a pathology test result.
        /// </summary>
        /// <returns>PathologyTestResult</returns>
        private static TestSpecimenDetail CreateTestSpecimenDetail()
        {
          var testSpecimenDetail = PathologyResultReport.CreateTestSpecimenDetail();

          // Date and Time Of Collection
          testSpecimenDetail.CollectionDateTime = new ISO8601DateTime(DateTime.Parse("27 Feb 2007 13:00"));

          return testSpecimenDetail;
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
            orderDetails.RequesterOrderIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003620833333789", "10523479");

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
            var attachmentPdf = BaseCDAModel.CreateExternalData();
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
            DocumentDetails documentDetails = BaseCDAModel.CreateDocumentDetails();

            // Report Identifier
            documentDetails.ReportIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.54.8003620833333789", "10523477");

            // Report Date 
            documentDetails.ReportDate = new ISO8601DateTime(DateTime.Now);

            // Result Status
            documentDetails.ReportStatus = BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.CorrectionToResults, "Correction To Results"); // or BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.CorrectionToResults)

            // Report Name 
            documentDetails.ReportDescription = "Full Blood Count";

            return documentDetails;
        }

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
          var reportingPathologist = PathologyResultReport.CreateReportingPathologist();

          // Document reportingPathologist > Participant
          reportingPathologist.Participant = PathologyResultReport.CreateParticipantForReportingPathologist();

          var person = BaseCDAModel.CreatePersonWithOrganisation();

          // Participation Period
          reportingPathologist.ParticipationEndTime = new ISO8601DateTime(DateTime.Now);

          // Document reportingPathologist > Role
          reportingPathologist.Role = PathologyResultReport.CreateRole(Occupation.Pathologist);

          // Document reportingPathologist > Participant > Person or Organisation or Device > Person > Person Name
          var name = BaseCDAModel.CreatePersonName();
          name.FamilyName = "Doctor";

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
          person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(null, null, null, "Casual", null);
          person.Organisation.Occupation = PathologyResultReport.CreateRole(Occupation.GeneralMedicalPractitioner);
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