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
using CDA.Generator.Common.SCSModel.Common.Entities;
using CDA.Generator.Common.SCSModel.ServiceReferral.Interfaces;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how a Service Referral CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// ServiceReferral class, and then populated with data as appropriate. The three sections that need to be
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
    /// E.g. Shared Health Summary specific CDA sections or entries; for example Subject of Care.
    /// 
    /// The SCS Content typically contains information that is to be represented with the body of the document.
    /// </summary>
    public class ServiceReferralSample
    {
        #region Properties

        /// <summary>
        /// The OutputFolderPath for the CDA Document
        /// </summary>
        public static string OutputFolderPath { get; set; }

        /// <summary>
        /// The Attachment File Name and Path
        /// </summary>
        public static string AttachmentFileNameAndPath => OutputFolderPath + @"\attachment.pdf";

        /// <summary>
        /// The Pit Name And Path
        /// </summary>
        public static string PitNameAndPath => OutputFolderPath + @"\PIT.txt";

        #endregion

        /// <summary>
        /// This sample populates only the mandatory Sections / Entries;
        /// </summary>
        public XmlDocument MinPopulatedServiceReferralSample(string fileName)
        {
            XmlDocument xmlDoc;

            var serviceReferral = PopulateServiceReferral(true);

            try
            {
                //Pass the Shared health Summary model into the GenerateServiceReferral method 
                xmlDoc = CDAGenerator.GenerateServiceReferral(serviceReferral);

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
        public XmlDocument MaxPopulatedServiceReferralSample(string fileName)
        {
            XmlDocument xmlDoc;

            var serviceReferral = PopulateServiceReferral(false);

            try
            {
                // Pass the Shared health Summary model into the GenerateServiceReferral method 
                xmlDoc = CDAGenerator.GenerateServiceReferral(serviceReferral);

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
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateServiceReferralSample_1A(string fileName)
        {
            XmlDocument xmlDoc;

            var serviceReferral = PopulateServiceReferral(true);
            serviceReferral.SCSContent = ServiceReferral.CreateSCSContent();

            // Hide Administrative Observations Section 
            serviceReferral.ShowAdministrativeObservationsSection = false;

            serviceReferral.IncludeLogo = false;

            var structuredBodyFileList = new List<ExternalData>();

            var structuredBodyFile = BaseCDAModel.CreateStructuredBodyFile();
            structuredBodyFile.Caption = "Structured Body File";
            structuredBodyFile.ExternalDataMediaType = MediaType.PDF;
            structuredBodyFile.Path = AttachmentFileNameAndPath;
            structuredBodyFileList.Add(structuredBodyFile);

            serviceReferral.SCSContent.StructuredBodyFiles = structuredBodyFileList;

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateEReferral method 
                xmlDoc = CDAGenerator.GenerateServiceReferral(serviceReferral);

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
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateServiceReferralSample_1B(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulateServiceReferral(true);

            document.SCSContent = ServiceReferral.CreateSCSContent();

            document.IncludeLogo = false;

            // Hide Administrative Observations Section 
            document.ShowAdministrativeObservationsSection = false;

            var narrativeOnlyDocumentList = new List<NarrativeOnlyDocument>();

            var narrativeOnlyDocument = BaseCDAModel.CreateNarrativeOnlyDocument();
            narrativeOnlyDocument.Title = "Title";
            narrativeOnlyDocument.Narrative = new StrucDocText { Text = new[] { "Narrative" } };

            // Add One
            narrativeOnlyDocumentList.Add(narrativeOnlyDocument);

            document.SCSContent.NarrativeOnlyDocument = narrativeOnlyDocumentList;

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the document model into the Generate method 
                xmlDoc = CDAGenerator.GenerateServiceReferral(document);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings() { Indent = true }))
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

        #region Private Methods

        /// <summary>
        /// This method populates a shared health summary model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>ServiceReferral</returns>
        internal static ServiceReferral PopulateServiceReferral(bool mandatorySectionsOnly)
        {
            var serviceReferral = ServiceReferral.CreateServiceReferral(DocumentStatus.Final);

            // Include Logo
            serviceReferral.IncludeLogo = true;
            serviceReferral.LogoPath = OutputFolderPath;

            // Set Creation Time
            serviceReferral.DocumentCreationTime = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = ServiceReferral.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid());

            // Title
            serviceReferral.Title = "Service Referral";

            // Information Recipients
            cdaContext.InformationRecipients = new List<IParticipationInformationRecipient>();

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                // Version
                cdaContext.Version = "1";

                // Set Id  
                cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid());

                // Legal Authenticator
                cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
                GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);
            }

            var recipient = BaseCDAModel.CreateInformationRecipient();
            GenericObjectReuseSample.HydrateRecipient(recipient, RecipientType.Primary, mandatorySectionsOnly);

            // Information Recipients
            cdaContext.InformationRecipients.AddRange(new[]
            {
                    recipient
            });

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            serviceReferral.ShowAdministrativeObservationsSection = !mandatorySectionsOnly;

            serviceReferral.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            serviceReferral.SCSContext = ServiceReferral.CreateSCSContext();

            serviceReferral.SCSContext.Author = BaseCDAModel.CreateAuthor();
            GenericObjectReuseSample.HydrateAuthorV2(serviceReferral.SCSContext.Author, mandatorySectionsOnly);

            // Subject of Care
            serviceReferral.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(serviceReferral.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            // Optional Items
            if (!mandatorySectionsOnly)
            {
                // Patient Nominated Contact
                var patientNominatedContact = new List<IParticipationPersonOrOrganisation>();
                patientNominatedContact.AddRange(new List<IParticipationPersonOrOrganisation>
                {
                    CreatePersonHealthcareProvider(mandatorySectionsOnly, false, false),
                    CreatePersonNonHealthcareProvider(mandatorySectionsOnly),
                    CreateOrganisation(mandatorySectionsOnly)
                });

                serviceReferral.SCSContext.PatientNominatedContact = patientNominatedContact;

                // Primary Care Provider
                serviceReferral.SCSContext.PrimaryCareProvider = CreatePersonHealthcareProvider(mandatorySectionsOnly, true);

                // Patient Nominated Contact
                var interestedParty = new List<IParticipationPersonOrOrganisation>();
                interestedParty.AddRange(new List<IParticipationPersonOrOrganisation>
                {
                    CreatePersonHealthcareProvider(mandatorySectionsOnly),
                    CreateOrganisation(mandatorySectionsOnly)
                });
                serviceReferral.SCSContext.InterestedParty = interestedParty;

                // Primary Care Provider
                serviceReferral.SCSContext.Referee = CreateOrganisation(mandatorySectionsOnly);
            }

            #endregion

            #region Setup and populate the SCS Content model

            // Setup and populate the SCS Content model
            serviceReferral.SCSContent = ServiceReferral.CreateSCSContent();

            // Service Referral Detail
            serviceReferral.SCSContent.ServiceReferralDetail = ServiceReferral.CreateServiceReferralDetail();

            // Requested Service
            serviceReferral.SCSContent.ServiceReferralDetail.RequestedService = new List<IRequestedService>
            {
                CreateRequestedService(mandatorySectionsOnly, true),
                CreateRequestedService(mandatorySectionsOnly, false),
            };

            // Optional Items
            if (!mandatorySectionsOnly)
            {
                // Related Document
                serviceReferral.SCSContent.ServiceReferralDetail.RelatedDocument = new List<RelatedDocumentV1>
                {
                    CreateRelatedDocument(mandatorySectionsOnly),
                    CreateRelatedDocument(mandatorySectionsOnly),
                };

                // Other Alerts
                serviceReferral.SCSContent.ServiceReferralDetail.OtherAlerts = new List<Alert>
                {
                       // Alert One
                       CreateAlert(),

                       // Alert Two
                       CreateAlert()
                };

                serviceReferral.SCSContent.CurrentService = new List<ICurrentService>
                {
                    // Pending Current Service with organization
                    CreateCurrentService(mandatorySectionsOnly),
                    CreateCurrentService(mandatorySectionsOnly),
                };

                // Reviewed medical history
                serviceReferral.SCSContent.MedicalHistory = CreateMedicalHistory(mandatorySectionsOnly);

                // Adverse reactions
                serviceReferral.SCSContent.AdverseReactions = CreateAdverseReactions(mandatorySectionsOnly);

                // Reviewed medications
                serviceReferral.SCSContent.Medications = CreateMedications(mandatorySectionsOnly);

                // Diagnostic Investigations
                serviceReferral.SCSContent.DiagnosticInvestigations = CreateDiagnosticInvestigationsV1(mandatorySectionsOnly);
            }

            #endregion

            return serviceReferral;
        }

        /// <summary>
        /// Creates and Hydrates the Requested Service section for a Service Referral
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of RequestedService</returns>
        private static IRequestedService CreateRequestedService(bool mandatorySectionsOnly, bool isOrganisation)
        {
            // Requested Service Person
            var requestedService = ServiceReferral.CreateRequestedService();

            // Service Booking Status
            requestedService.ServiceBookingStatus = EventTypes.AppointmentRequest;

            // Service Provider
            requestedService.ServiceProvider = !isOrganisation ? CreatePersonHealthcareProvider(mandatorySectionsOnly, true) : CreateOrganisation(mandatorySectionsOnly);

            // Requested Service DateTime
            requestedService.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4), ISO8601DateTime.Precision.Day);

            // Reason for Service
            requestedService.ReasonForService = BaseCDAModel.CreateCodableText("426721006", CodingSystem.SNOMED, "X-ray of right ankle");

            // Reason for Service Category
            requestedService.ServiceCategory = BaseCDAModel.CreateCodableText("102.20158", CodingSystem.NCTIS, "Requested Service");

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                // Reason for Service Description
                requestedService.ReasonForServiceDescription = "Reason For Service Description";

                // Service Description
                requestedService.ServiceDescription = BaseCDAModel.CreateCodableText("241646009", CodingSystem.SNOMED, "MRI of cervical spine");

                // Request Urgency
                requestedService.RequestUrgency = true;

                // Request Urgency Notes
                requestedService.RequestUrgencyNotes = "This is urgent because the patient has had sudden onset of severe headache, with minor neurological impairment and is at risk of quick deterioration.";

                // Service Commencement Window
                requestedService.ServiceCommencementWindow = BaseCDAModel.CreateInterval(
                    new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day),
                    new ISO8601DateTime(DateTime.Now.AddMonths(6), ISO8601DateTime.Precision.Day));

                // Request Validity Period
                requestedService.RequestValidityPeriod = BaseCDAModel.CreateInterval(
                    new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day),
                    new ISO8601DateTime(DateTime.Now.AddMonths(6), ISO8601DateTime.Precision.Day));
            }

            return requestedService;
        }

        /// <summary>
        /// Creates and Hydrates the Current Service section for a Service Referral
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of CurrentService</returns>
        private static ICurrentService CreateCurrentService(bool mandatorySectionsOnly)
        {
            // Requested Service Person
            var requestedService = ServiceReferral.CreateCurrentService();

            // Service Booking Status
            requestedService.ServiceBookingStatus = EventTypes.Event;

            // Service Provider
            requestedService.ServiceProvider = CreateOrganisation(mandatorySectionsOnly);

            // Requested Service DateTime
            requestedService.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4), ISO8601DateTime.Precision.Day);

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                // Reason for Service Category
                requestedService.ServiceCategory = BaseCDAModel.CreateCodableText("102.20158", CodingSystem.NCTIS, "Requested Service");

                // Service Description
                requestedService.ServiceDescription = BaseCDAModel.CreateCodableText("241646009", CodingSystem.SNOMED, "MRI of cervical spine");

                // Service Comment
                requestedService.ServiceComment = "Service Comment";
            }

            return requestedService;
        }

        /// <summary>
        /// Creates and Hydrates the Pending Diagnostic Investigation section for a Service Referral
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of IPendingDiagnosticInvestigation</returns>
        private static IPendingDiagnosticInvestigation CreatePendingDiagnosticInvestigation(bool mandatorySectionsOnly, bool isOrganisation)
        {
            // Requested Service
            var requestedService = ServiceReferral.CreatePendingDiagnosticInvestigation();

            // Service Booking Status
            requestedService.ServiceBookingStatus = EventTypes.AppointmentRequest;

            // Requested Service DateTime
            requestedService.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4), ISO8601DateTime.Precision.Day);

            // Service Description
            requestedService.ServiceDescription = BaseCDAModel.CreateCodableText("241646009", CodingSystem.SNOMED, "MRI of cervical spine");

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                // ServiceProvider
                requestedService.ServiceProvider = !isOrganisation ? CreatePersonHealthcareProvider(mandatorySectionsOnly, true) : CreateOrganisation(mandatorySectionsOnly);

                // Reason for Service
                requestedService.ReasonForService = BaseCDAModel.CreateCodableText("426721006", CodingSystem.SNOMED, "X-ray of right ankle");

                // Reason for Service Description
                requestedService.ReasonForServiceDescription = "Reason For Service Description";

                // Date Time Service Scheduled
                requestedService.DateTimeServiceScheduled = new ISO8601DateTime(DateTime.Now.AddDays(4), ISO8601DateTime.Precision.Day);

                // Service Comment
                requestedService.ServiceComment = "Service Comment";

                // Subject Of Care Instruction Description
                requestedService.SubjectOfCareInstructionDescription = new []
                {
                    "Subject of Care Instruction Description one",
                    "Subject of Care Instruction Description two"
                };

                // Service Commencement Window
                requestedService.ServiceCommencementWindow = BaseCDAModel.CreateInterval(
                    new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day),
                    new ISO8601DateTime(DateTime.Now.AddMonths(6), ISO8601DateTime.Precision.Day));
            }

            return requestedService;
        }

        /// <summary>
        /// Creates an  Alert  
        /// </summary>
        /// <returns>Alert</returns>
        public static Alert CreateAlert()
        {
            // Create Alert
            var alert = ServiceReferral.CreateAlert();

            // Alert Type
            alert.AlertType = BaseCDAModel.CreateCodableText("736911000168103", CodingSystem.SNOMED, "http://snomed.info/sct/32506021000036107/version/20161231", "Clinical alert", null, null, null);

            // Alert Description
            alert.AlertDescription = BaseCDAModel.CreateCodableText("426721006", CodingSystem.SNOMED, "http://snomed.info/sct/32506021000036107/version/20161231", "X-ray of right ankle", null, null, null);

            return alert;
        }

        /// <summary>
        /// Creates a Related Document.
        /// </summary>
        /// <returns>RelatedDocumentV1</returns>
        public static RelatedDocumentV1 CreateRelatedDocument(bool mandatorySectionsOnly)
        {
            RelatedDocumentV1 relatedDocument = ServiceReferral.CreateRelatedDocumentV1();

            // Pathology PDF
            ExternalData attachmentPdf = BaseCDAModel.CreateExternalData();
            attachmentPdf.ExternalDataMediaType = MediaType.PDF;
            attachmentPdf.Path = AttachmentFileNameAndPath;
            relatedDocument.DocumentTarget = attachmentPdf;
            relatedDocument.DocumentTarget.Caption = "Download";

            // Document Details
            var documentDetails = ServiceReferral.CreateDocumentDetailsV1();

            // Document Title
            documentDetails.DocumentTitle = "Related Document";

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                documentDetails.DocumentType = BaseCDAModel.CreateCodableText("100.16998", CodingSystem.NCTIS, "Advance Care Planning Document");
            }

            relatedDocument.DocumentDetails = documentDetails;

            return relatedDocument;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed medications section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedMedications object</returns>
        private static IMedications CreateMedications(bool mandatorySectionsOnly)
        {
            var medicationList = new List<IMedication>();

            var medications = ServiceReferral.CreateMedications();

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                var medication = BaseCDAModel.CreateMedication();
                medication.ClinicalIndication = "Diuretic induced hypokalemia";
                medication.Comment = "Taken with food";
                medication.Directions = BaseCDAModel.CreateStructuredText("2 tablets once daily oral");
                medication.Medicine = BaseCDAModel.CreateCodableText("5884011000036107", CodingSystem.AMTV2, "Span K (potassium chloride 600 mg (8 mmol potassium)) tablet: modified release, 1 tablet");
                medicationList.Add(medication);                                                                 

                var medication1 = BaseCDAModel.CreateMedication();
                medication1.ClinicalIndication = "Arthritis pain management";
                medication1.Comment = "Swallow whole";
                medication1.Directions = BaseCDAModel.CreateStructuredText("2 tablets three times per day");
                medication1.Medicine = BaseCDAModel.CreateCodableText("5848011000036106", CodingSystem.AMTV2, "Panadol Osteo (paracetamol 665 mg) tablet: modified release, 1 tablet");
                medicationList.Add(medication1);

                var medication2 = BaseCDAModel.CreateMedication();
                medication2.ClinicalIndication = "Fluid retention";
                medication2.Comment = "Take in the morning";
                medication2.Directions = BaseCDAModel.CreateStructuredText("1 tablet once daily oral");
                medication2.Medicine = BaseCDAModel.CreateCodableText("40288011000036101", CodingSystem.AMTV2, "Lasix (frusemide 40 mg/4 mL) injection: solution, ampoule");
                medicationList.Add(medication2);

                var medication3 = BaseCDAModel.CreateMedication();
                medication3.ClinicalIndication = "COPD";
                medication3.Directions = BaseCDAModel.CreateStructuredText("1 inhalation per day");
                medication3.Medicine = BaseCDAModel.CreateCodableText("7113011000036100", CodingSystem.AMTV2, "Spiriva (tiotropium (as bromide monohydrate) 18 microgram) inhalation: powder for, 1 capsule");
                medicationList.Add(medication3);

                var medication4 = BaseCDAModel.CreateMedication();
                medication4.ClinicalIndication = "Depression";
                medication4.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: 3 times daily");
                medication4.Medicine = BaseCDAModel.CreateCodableText("32481000036107", CodingSystem.AMTV2, "Exatrust (exemestane 25 mg) tablet: film-coated, 1 tablet");
                medicationList.Add(medication4);

                var medication5 = BaseCDAModel.CreateMedication();
                medication5.ClinicalIndication = "Depression";
                medication5.Directions = BaseCDAModel.CreateStructuredText(NullFlavour.PositiveInfinity);
                medication5.Medicine = BaseCDAModel.CreateCodableText("32481000036107", CodingSystem.AMTV2, "Exatrust (exemestane 25 mg) tablet: film-coated, 1 tablet");
                medicationList.Add(medication5);
                medications.Medications = medicationList;

            } else
            {
                medications.ExclusionStatement = BaseCDAModel.CreateStatement();
                medications.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneKnown;  
            }

            return medications;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed adverse substance reactions section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedAdverseSubstanceReactions object</returns>
        private static IAdverseReactions CreateAdverseReactions(bool mandatorySectionsOnly)
        {
            var adverseReactions = BaseCDAModel.CreateAdverseReactions();

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                var reaction = BaseCDAModel.CreateReaction();

                reaction.ReactionEvent = BaseCDAModel.CreateReactionEvent();
                reaction.SubstanceOrAgent = BaseCDAModel.CreateCodableText("391739009", CodingSystem.SNOMED, "Aloe");

                reaction.ReactionEvent.Manifestations = new List<ICodableText>
                {
                    BaseCDAModel.CreateCodableText("20262006", CodingSystem.SNOMED, "Ataxia"),
                    BaseCDAModel.CreateCodableText("285599002", CodingSystem.SNOMED, "Trunk nerve lesion")
                };

                reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

                var reaction2 = BaseCDAModel.CreateReaction();
                reaction2.SubstanceOrAgent = BaseCDAModel.CreateCodableText("372725003", CodingSystem.SNOMED, "Phenoxymethylpenicillin");
                reaction2.ReactionEvent = BaseCDAModel.CreateReactionEvent();

                reaction2.ReactionEvent.Manifestations = new List<ICodableText>
                {
                    BaseCDAModel.CreateCodableText("20262006", CodingSystem.SNOMED, "Ataxia"),
                    BaseCDAModel.CreateCodableText("285599002", CodingSystem.SNOMED, "Trunk nerve lesion")
                };

                adverseReactions.AdverseSubstanceReaction = new List<Reaction>
                {
                    reaction, 
                    reaction2
                };
            } 
            else
            {
                adverseReactions.ExclusionStatement = BaseCDAModel.CreateStatement();
                adverseReactions.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }

            return adverseReactions;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed medical history section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedMedicalHistory object</returns>
        private static IMedicalHistory CreateMedicalHistory(bool mandatorySectionsOnly)
        {
            var medicalHistory = BaseCDAModel.CreateMedicalHistory();

            // Optional Fields
            if (!mandatorySectionsOnly)
            {
                // NOTE: This section demonstrates the different combinations of Procedure's, Medical History Item's & diagnosis
                var procedureList = new List<Procedure>();
                var medicalHistoryItems = new List<IMedicalHistoryItem>();
                var diagnosisList = new List<IProblemDiagnosis>();

                // Procedures
                var procedure1 = BaseCDAModel.CreateProcedure();
                procedure1.Comment = "L Procedure Comment";
                procedure1.ProcedureName = BaseCDAModel.CreateCodableText("301040004", CodingSystem.SNOMED, "Primary closed wire fixation of fracture");
                procedure1.ProcedureDateTime = CdaInterval.CreateLowHigh(new ISO8601DateTime(DateTime.Now.AddDays(-402), ISO8601DateTime.Precision.Day),
                                                                         new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                procedure1.ShowOngoingInNarrative = true;
                procedureList.Add(procedure1);

                var prodcedure2 = BaseCDAModel.CreateProcedure();
                prodcedure2.Comment = "Comment";
                prodcedure2.ProcedureName = BaseCDAModel.CreateCodableText("388544006", CodingSystem.SNOMED, "Crab RAST");
                prodcedure2.ProcedureDateTime = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                procedureList.Add(prodcedure2);

                // Uncategorized Medical History Items
                var medicalHistoryItem = BaseCDAModel.CreateMedicalHistoryItem();
                medicalHistoryItem.DateTimeInterval = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                medicalHistoryItem.ShowOngoingInNarrative = true;
                medicalHistoryItem.ItemDescription = "Uncategorised Medical History item description";
                medicalHistoryItem.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem);

                var medicalHistoryItem1 = BaseCDAModel.CreateMedicalHistoryItem();
                medicalHistoryItem1.ShowOngoingInNarrative = true;
                medicalHistoryItem1.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem1.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem1);

                var medicalHistoryItem2 = BaseCDAModel.CreateMedicalHistoryItem();
                var ongoingInterval2 = CdaInterval.CreateLowHigh(
                                       new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day),
                                       new ISO8601DateTime(DateTime.Now.AddDays(200), ISO8601DateTime.Precision.Day));

                medicalHistoryItem2.DateTimeInterval = ongoingInterval2;
                medicalHistoryItem2.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem2.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem2);

                var medicalHistoryItem3 = BaseCDAModel.CreateMedicalHistoryItem();
                var ongoingInterval3 = CdaInterval.CreateHigh(new ISO8601DateTime(DateTime.Now.AddDays(200), ISO8601DateTime.Precision.Day));
                medicalHistoryItem3.DateTimeInterval = ongoingInterval3;
                medicalHistoryItem3.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem3.ItemComment = "Item Comment 4";
                medicalHistoryItems.Add(medicalHistoryItem3);

                // Problem Diagnosis
                var diagnosis = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("57883007", CodingSystem.SNOMED, "Renin test diet");
                diagnosis.DateOfOnset = new ISO8601DateTime(DateTime.Now.AddYears(-2), ISO8601DateTime.Precision.Day);
                diagnosis.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Now.AddYears(-1), ISO8601DateTime.Precision.Day);
                diagnosis.Comment = "Solved this";
                diagnosis.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis);

                var diagnosis1 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis1.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("72940007", CodingSystem.SNOMED, "Acute abscess of areola");
                diagnosisList.Add(diagnosis1);

                var diagnosis2 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis2.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("36083008", CodingSystem.SNOMED, "Sick sinus syndrome");
                diagnosisList.Add(diagnosis2);

                var diagnosis3 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis3.Comment = "Diuretic induced";
                diagnosis3.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("162311003", CodingSystem.SNOMED, "Heavy head");
                diagnosisList.Add(diagnosis3);

                var diagnosis4 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis4.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("258245003", CodingSystem.SNOMED, "G4 grade");
                diagnosis4.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("27 Feb 2007"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis4);

                var diagnosis5 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis5.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("396275006", CodingSystem.SNOMED, "Osteoarthritis");
                diagnosis5.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Jan 2000"), ISO8601DateTime.Precision.Day);
                diagnosis5.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis5);

                var diagnosis6 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis6.Comment = "Cementless";
                diagnosis6.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("43408002", CodingSystem.SNOMED, "Red reflex");
                diagnosis6.DateOfOnset = new ISO8601DateTime(DateTime.Parse("27 Feb 2007"), ISO8601DateTime.Precision.Day);
                diagnosis6.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis6);

                var diagnosis7 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis7.Comment = "T-score less than -3";
                diagnosis7.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("286114007", CodingSystem.SNOMED, "Does not do dusting");
                diagnosis7.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Jan 2007"), ISO8601DateTime.Precision.Day);
                diagnosis7.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis7);

                var diagnosis8 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis8.Comment = "Comment";
                diagnosis8.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("248515009", CodingSystem.SNOMED, "Lump in lid margin");
                diagnosis8.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis8.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("01 Sep 2010"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis8);

                var diagnosis9 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis9.Comment = "Diagnosis Comment";
                diagnosis9.ShowOngoingDateInNarrative = true;
                diagnosis9.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("200608005", CodingSystem.SNOMED, "Boil of back");
                diagnosis9.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis9.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("01 Sep 2010"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis9);

                var diagnosis10 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis10.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("267032009", CodingSystem.SNOMED, "Tired all the time");
                diagnosis10.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis10.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis10);

                medicalHistory.MedicalHistoryItems = medicalHistoryItems;
                medicalHistory.Procedures = procedureList;
                medicalHistory.ProblemDiagnosis = diagnosisList;

            }
            else
            {
                // NOTE : NotAsked is not a valid entry in context of a Shared Health Summary
                medicalHistory.ProblemDiagnosisExclusionStatement = BaseCDAModel.CreateStatement();
                medicalHistory.ProblemDiagnosisExclusionStatement.Value = NCTISGlobalStatementValues.NoneKnown;

                medicalHistory.ProceduresExclusionStatement = BaseCDAModel.CreateStatement();
                medicalHistory.ProceduresExclusionStatement.Value = NCTISGlobalStatementValues.NoneKnown;
            }

            return medicalHistory;
        }

        /// <summary>
        /// Creates and Hydrates the diagnostic investigations v1 substance reactions section.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated DiagnosticInvestigationsV1 object</returns>
        private static IDiagnosticInvestigationsV1 CreateDiagnosticInvestigationsV1(bool mandatoryOnly)
        {
            // Diagnostic Investigations
            IDiagnosticInvestigationsV1 diagnosticInvestigations = ServiceReferral.CreateDiagnosticInvestigationsV1();

            // Imaging Examination Result
            diagnosticInvestigations.ImagingExaminationResult = new List<IImagingExaminationResult>
            {
                // Imaging Results
                GenericObjectReuseSample.CreateImagingResults("Leg Image"),
                GenericObjectReuseSample.CreateImagingResults("Head Image")
            };

            // Pathology test results
            diagnosticInvestigations.PathologyTestResult = new List<PathologyTestResult>
            {
                // Pathology Results
                GenericObjectReuseSample.CreatePathologyResults(PitNameAndPath),
                GenericObjectReuseSample.CreatePathologyResults(mandatoryOnly)
            };

            // Requested Service
            diagnosticInvestigations.RequestedService = new List<IPendingDiagnosticInvestigation>
            {
                // Pending Diagnostic Investigation with organisation
                CreatePendingDiagnosticInvestigation(mandatoryOnly, true),

                // Pending Diagnostic Investigation with person 
                CreatePendingDiagnosticInvestigation(mandatoryOnly, false)
            };

            return diagnosticInvestigations;
        }

        #endregion

        #region Providers

        /// NOTE: The IParticipationPersonOrOrganisation is a global Participant.
        ///       This implementation provides only minimum validation because this Participant is shared between multiple locations and can be relaxed for B2B transactions.

        /// <summary>
        /// Creates and Hydrates a Person Health-care Provider (IParticipationPersonOrOrganisation)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationPersonOrOrganisation</returns>
        private static IParticipationPersonOrOrganisation CreatePersonHealthcareProvider(bool mandatoryOnly, bool showEntitlementsAndQualifications = false, bool showIdentifier = true)
        {
            var nominatedPrimaryHealthCareProvider = ServiceReferral.CreateParticipationPersonOrOrganisation();
            nominatedPrimaryHealthCareProvider.Role = BaseCDAModel.CreateRole(Occupation.AgedOrDisabledCarer);
            var participant = ServiceReferral.CreateParticipantPersonOrOrganisation();
            participant.Person = BaseCDAModel.CreatePersonWithOrganisation();

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "PersonHealthcareProvider";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };
            participant.Person.PersonNames = new List<IPersonName> { personName, personName };

            if (showIdentifier)
            {
                // Document Author > Participant > Entity Identifier
                participant.Person.Identifiers = new List<Identifier>
                {
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101")
                };
            }

            // Set Up Address
            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            // Set Up Electronic Communication Detail
            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            // Employment Organisation
            var organisation = BaseCDAModel.CreateEmploymentOrganisation();
            organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            organisation.Name = "Super Healthy Hospital";
            organisation.NameUsage = OrganisationNameUsage.Other;
            organisation.Department = "Endocrinology";
            organisation.Addresses = new List<IAddress> { address, address };
            organisation.EmploymentType = BaseCDAModel.CreateCodableText("Casual");
            organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Manager");
            organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail };

            // Optional items
            if (!mandatoryOnly)
            {
                participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
                {
                    electronicCommunicationDetail, electronicCommunicationDetail
                };

                participant.Addresses = new List<IAddress> { address, address };

                if (showEntitlementsAndQualifications)
                {
                    // Entitlement
                    var entitlement = BaseCDAModel.CreateEntitlement();

                    entitlement.Id = BaseCDAModel.CreateIdentifier("Pharmacy",
                                                                    null,
                                                                    "1234567892",
                                                                    "1.2.36.174030967.1.3.2.1",
                                                                    null);

                    entitlement.Type = EntitlementType.MedicarePrescriberNumber;
                    entitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));
                    participant.Entitlements = new List<Entitlement> { entitlement };

                    // Qualifications
                    participant.Qualifications = "M.B.B.S., F.R.A.C.S.";
                }
            }

            // Assign organisation
            participant.Person.Organisation = organisation;

            // Assign participant
            nominatedPrimaryHealthCareProvider.Participant = participant;

            return nominatedPrimaryHealthCareProvider;
        }

        /// NOTE: The IParticipationPersonOrOrganisation is a global Participant.
        ///       This implementation provides only minimum validation because this Participant is shared between multiple locations and can be relaxed for B2B transactions.
        /// <summary>
        /// Creates and Hydrates a Person Non Healthcare Provider (IParticipationPersonOrOrganisation)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationPersonOrOrganisation</returns>
        private static IParticipationPersonOrOrganisation CreatePersonNonHealthcareProvider(bool mandatoryOnly, bool showEntitlementsAndQualifications = false)
        {
            var nominatedPrimaryHealthCareProvider = ServiceReferral.CreateParticipationPersonOrOrganisation();
            nominatedPrimaryHealthCareProvider.Role = BaseCDAModel.CreateRole(Occupation.AgedOrDisabledCarer);

            var participant = ServiceReferral.CreateParticipantPersonOrOrganisation();
            participant.Person = BaseCDAModel.CreatePersonWithOrganisation();

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "PersonNonHealthcareProvider";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            participant.Person.PersonNames = new List<IPersonName> { personName };

            // Optional items
            if (!mandatoryOnly)
            {
                // Relationship To Subject Of Care
                participant.RelationshipToSubjectOfCare = BaseCDAModel.CreateCodableText("116154003", CodingSystem.SNOMED, "Patient");

                var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);

                participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
                {
                    electronicCommunicationDetail,
                    electronicCommunicationDetail
                };

                var address = BaseCDAModel.CreateAddress();
                address.AddressPurpose = AddressPurpose.Business;
                address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address.AustralianAddress.State = AustralianState.QLD;
                address.AustralianAddress.PostCode = "5555";
                address.AustralianAddress.DeliveryPointId = 32568931;

                participant.Addresses = new List<IAddress> { address };

                var organisation = BaseCDAModel.CreateEmploymentOrganisation();
                organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
                organisation.Name = "Super Healthy Hospital";
                organisation.NameUsage = OrganisationNameUsage.Other;
                organisation.Department = "Endocrinology";
                organisation.Addresses = new List<IAddress> { address };
                organisation.EmploymentType = BaseCDAModel.CreateCodableText("Casual");
                organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
                organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Manager");
                organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

                participant.Person.Organisation = organisation;

                if (showEntitlementsAndQualifications)
                {
                    // Entitlement
                    var entitlement = BaseCDAModel.CreateEntitlement();

                    entitlement.Id = BaseCDAModel.CreateIdentifier("Pharmacy", null, "1234567892", "1.2.36.174030967.1.3.2.1", null);
                    entitlement.Type = EntitlementType.MedicarePrescriberNumber;
                    entitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));
                    participant.Entitlements = new List<Entitlement> { entitlement };

                    // Qualifications
                    participant.Qualifications = "M.B.B.S., F.R.A.C.S.";
                }
            }

            nominatedPrimaryHealthCareProvider.Participant = participant;

            return nominatedPrimaryHealthCareProvider;
        }

        /// NOTE: The IParticipationPersonOrOrganisation is a global Participant.
        ///       This implementation provides only minimum validation because this Participant is shared between multiple locations and can be relaxed for B2B transactions.

        /// <summary>
        /// Creates and Hydrates an Organisation
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationPersonOrOrganisation</returns>
        private static IParticipationPersonOrOrganisation CreateOrganisation(bool mandatoryOnly)
        {
            var provider = ServiceReferral.CreateParticipationPersonOrOrganisation();
            provider.Role = BaseCDAModel.CreateRole("HOSP", CodingSystem.HL7ServiceDeliveryLocationRoleType, "Hospital", null);

            provider.Participant = ServiceReferral.CreateParticipantPersonOrOrganisation();

            var organisation = BaseCDAModel.CreateOrganisation();
            organisation.Name = "Organisation";

            organisation.Identifiers = new List<Identifier>
                {
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562")
                };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);
            provider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail };

            if (!mandatoryOnly)
            {
                organisation.NameUsage = OrganisationNameUsage.Other;
                organisation.Department = "Organisation Department";

                var address = BaseCDAModel.CreateAddress();
                address.AddressPurpose = AddressPurpose.Business;
                address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address.AustralianAddress.State = AustralianState.QLD;
                address.AustralianAddress.PostCode = "5555";
                address.AustralianAddress.DeliveryPointId = 32568931;

                provider.Participant.Addresses = new List<IAddress> { address, address };
            }

            provider.Participant.Organisation = organisation;

            return provider;
        }

        #endregion
    }
}
