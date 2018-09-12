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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how an EventSummary CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// EventSummary class, and then populated with data as appropriate. The three sections that need to be
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
    public class EventSummarySample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\EventSummary.xml";
            }
        }

        public static String PitNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PIT.txt";
            }
        }

        public static String StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

        #endregion

        /// <summary>
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateEventSummarySample_1A(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulatedEventSummary(true);

            // Hide Administrative Observations Section 
            document.ShowAdministrativeObservationsSection = false;

            document.SCSContent = EventSummary.CreateSCSContent();

            document.IncludeLogo = false;

            var structuredBodyFileList = new List<ExternalData>();

            var structuredBodyFile = BaseCDAModel.CreateStructuredBodyFile();
            structuredBodyFile.Caption = "Structured Body File";
            structuredBodyFile.ExternalDataMediaType = MediaType.PDF;
            structuredBodyFile.Path = StructuredFileAttachment;
            structuredBodyFileList.Add(structuredBodyFile);

            document.SCSContent.StructuredBodyFiles = structuredBodyFileList;

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the document model into the Generate method 
                xmlDoc = CDAGenerator.GenerateEventSummary(document);

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

        /// <summary>
        /// This sample populates only the mandatory sections / entries
        /// </summary>
        public XmlDocument MinPopulatedEventSummary(string fileName)
        {
            XmlDocument xmlDoc;

            var eEventSummary = PopulatedEventSummary(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GenerateEventSummary method 
                xmlDoc = CDAGenerator.GenerateEventSummary(eEventSummary);

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
        public XmlDocument MaxPopulatedEventSummary(string fileName)
        {
            XmlDocument xmlDoc;

            var eEventSummary = PopulatedEventSummary(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GenerateEventSummary method 
                xmlDoc = CDAGenerator.GenerateEventSummary(eEventSummary);

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

        #region Private Test Methods

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries depending on the  
        /// mandatorySectionsOnly Boolean
        /// </summary>
        internal static EventSummary PopulatedEventSummary(Boolean mandatorySectionsOnly)
        {
            var eventSummary = EventSummary.CreateEventSummary();

            // Include Logo
            eventSummary.IncludeLogo = true;
            eventSummary.LogoPath = OutputFolderPath;

            // Set Creation Time
            eventSummary.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = EventSummary.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid());

            // Set Id  
            if (!mandatorySectionsOnly)
            {
                cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid());
            }

            // CDA Context Version
            if (!mandatorySectionsOnly)
            {
                cdaContext.Version = "1";
            }

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
            GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

            eventSummary.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model

            // Setup and Populate the SCS Context model
            eventSummary.SCSContext = EventSummary.CreateSCSContext();

            eventSummary.SCSContext.Author = BaseCDAModel.CreateAuthor();
            GenericObjectReuseSample.HydrateAuthorV2(eventSummary.SCSContext.Author, mandatorySectionsOnly);

            eventSummary.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(eventSummary.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            // DateTime Health Event Started & DateTime Health Event Ended allowable combinations
            var dateTimeHealthEventEnded = new ISO8601DateTime(DateTime.Now);
            var dateTimeHealthEventStarted = new ISO8601DateTime(DateTime.Now.AddMonths(-12));

            eventSummary.SCSContext.EncounterPeriod = !mandatorySectionsOnly ? 
                BaseCDAModel.CreateLowHigh(dateTimeHealthEventStarted, dateTimeHealthEventEnded) : 
                BaseCDAModel.CreateHigh(dateTimeHealthEventEnded);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            eventSummary.SCSContent = EventSummary.CreateSCSContent();

            if (!mandatorySectionsOnly)
            {
                eventSummary.SCSContent.EventDetails = CreateEventDetails();

                // Adverse reactions
                eventSummary.SCSContent.AdverseReactions = CreateAdverseReactions();

                // Medications
                eventSummary.SCSContent.Medications = CreateMedications();

                // Medical history
                eventSummary.SCSContent.DiagnosesIntervention = CreateDiagnosesIntervention(mandatorySectionsOnly);

                // Immunisations
                eventSummary.SCSContent.Immunisations = CreateImmunisations();

                // Diagnostic Investigations
                eventSummary.SCSContent.DiagnosticInvestigations = CreateDiagnosticInvestigations(mandatorySectionsOnly);
            }

            #endregion

            return eventSummary;
        }

        /// <summary>
        /// Creates and Hydrates Event Details
        /// </summary>
        /// <returns>A Hydrated IImagingExaminationResult object</returns>
        private static EventDetails CreateEventDetails()
        {
            var eventDetails = EventSummary.CreateEventDetails();
            eventDetails.ClinicalSynopsisDescription = "laceration";
            return eventDetails;
        }

        /// <summary>
        /// Creates and Hydrates the immunisations section for the Event Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Event Summary object</returns>
        private static List<IImmunisation> CreateImmunisations()
        { 
            var immunisation =  BaseCDAModel.CreateCodableText("53705011000036109",CodingSystem.AMTV2, "Advil (ibuprofen 200 mg) tablet: sugar-coated, 1 tablet" );

            var immunisationList = new List<IImmunisation>
            {
                CreateImmunisation(DateTime.Parse("22 Dec 2009"), immunisation, null),
                CreateImmunisation(DateTime.Parse("22 Dec 2009"), immunisation, 1),
            };
 
            return immunisationList;
        }

      /// <summary>
      /// Creates an immunisation.
      /// </summary>
      /// <param name="date">Date of immunisation.</param>
      /// <param name="code">Code of immunisation.</param>
      /// <param name="codingSystem">Coding system for the code.</param>
      /// <param name="name">Name of immunisation.</param>
      /// <param name="sequenceNumber">The immunisation sequence number </param>
      /// <returns>Created immunisation.</returns>
        private static IImmunisation CreateImmunisation(DateTime date, ICodableText codableText, int? sequenceNumber)
        {
            var immunisation = EventSummary.CreateImmunisation();
            immunisation.DateTime = new ISO8601DateTime(date, ISO8601DateTime.Precision.Day);
            immunisation.Medicine = codableText;
            immunisation.SequenceNumber = sequenceNumber;
            return immunisation;
        }

        /// <summary>
        /// Creates and Hydrates the Medications section for the Event Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of Medications</returns>
        private static List<IMedicationItem> CreateMedications()
        {
            var medicationList = new List<IMedicationItem>
            {
                CreateMedication("23641011000036102", "paracetamol 500 mg + codeine phosphate 30 mg tablet", true),
                CreateMedication("45260011000036108", "dextropropoxyphene hydrochloride 32.5 mg + paracetamol 325 mg tablet", false)
            };

            return medicationList;
        }

      /// <summary>
      /// Creates a medication item.
      /// </summary>
      /// <param name="code">Medication code.</param>
      /// <param name="name">Medication name.</param>
      /// <param name="directionsNullFlavour">The Directions Null Flavour</param>
      /// <param name="recomendationOrChangeNullFlavour">The Recommendation Or Change Null Flavour</param>
      /// <param name="changeTypeNullFlavour">The Change Type Null Flavour</param>
      /// <returns></returns>
        private static IMedicationItem CreateMedication(string code, string name, bool showNullflavor)
        {
            IMedicationItem medication = EventSummary.CreateMedication();

            if (showNullflavor)
            {
                medication.Directions = BaseCDAModel.CreateStructuredText(NullFlavour.Other);
                medication.ChangeStatus = BaseCDAModel.CreateCodableText(NullFlavour.Other, CodingSystem.SNOMED, "Change made"); // Could not find ChangeStatus code for the provided refset
                medication.ChangeType = BaseCDAModel.CreateCodableText(NullFlavour.Other, CodingSystem.SNOMED, "Changed"); // Could not find ChangeType code for the provided refset
            }
            else
            {
                medication.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: 3 times daily");
                medication.ChangeStatus = BaseCDAModel.CreateCodableText(ChangeStatus.ChangeMade);
                medication.ChangeType = BaseCDAModel.CreateCodableText(ChangeTypeSnomed.Changed);
            }

            medication.ChangeDescription = "Change Description";
            medication.ChangeReason = BaseCDAModel.CreateStructuredText("Change Reason");
            medication.ClinicalIndication = "Clinical Indication";
            medication.Comment = "Comment";

            medication.Medicine = BaseCDAModel.CreateCodableText(code, CodingSystem.AMTV2, name);

            return medication;
        }

        /// <summary>
        /// Creates and Hydrates the Medications section for the Event Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of RequestedService</returns>
        private static List<RequestedService> CreateRequestedService(Boolean mandatorySectionsOnly)
        {
            var requestedServiceList = new List<RequestedService>();

            // Create Service Provider for a Person
            var requestedServicePerson = EventSummary.CreateRequestedService();
            requestedServicePerson.ServiceCommencementWindow = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day), 
                new ISO8601DateTime(DateTime.Now.AddMonths(6), ISO8601DateTime.Precision.Day));
            requestedServicePerson.RequestedServiceDescription = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMED, "Plain chest X-ray");
            requestedServicePerson.ServiceBookingStatus = EventTypes.AppointmentRequest;
            requestedServicePerson.SubjectOfCareInstructionDescription = "Subject Of Care Instruction Description";
            requestedServicePerson.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4), ISO8601DateTime.Precision.Day);
            requestedServicePerson.ServiceProvider = CreateServiceProviderPerson(mandatorySectionsOnly);

            // Add to list
            requestedServiceList.Add(requestedServicePerson);

            // Create Service Provider for a Organisation
            var requestedServiceOrganisation = EventSummary.CreateRequestedService();
            requestedServiceOrganisation.ServiceScheduled = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day);
            requestedServiceOrganisation.RequestedServiceDescription = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMED, "Plain chest X-ray");
            requestedServiceOrganisation.ServiceBookingStatus = EventTypes.Intent;
            requestedServiceOrganisation.SubjectOfCareInstructionDescription = "Subject Of Care Instruction Description";
            requestedServiceOrganisation.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4));
            requestedServiceOrganisation.ServiceProvider = CreateServiceProviderOrganisation(mandatorySectionsOnly);

            // Add to list
            requestedServiceList.Add(requestedServiceOrganisation);

            return requestedServiceList;
        }

        /// <summary>
        /// Creates and Hydrates a Service Provider Organisation
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationServiceProvider CreateServiceProviderOrganisation(Boolean mandatorySectionsOnly)
        {
            var serviceProvider = BaseCDAModel.CreateServiceProvider();

            serviceProvider.Participant = BaseCDAModel.CreateParticipantForServiceProvider();
            serviceProvider.Participant.Organisation= BaseCDAModel.CreateOrganisation();
            serviceProvider.Participant.Organisation.Name = "Bay Hill Hospital";
            serviceProvider.Participant.Organisation.NameUsage = OrganisationNameUsage.Other;

            serviceProvider.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") }; 

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            serviceProvider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Residential;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            serviceProvider.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.Other);

            serviceProvider.Participant.Addresses = new List<IAddress> { address, address };

            return serviceProvider;
        }

        /// <summary>
        /// Creates and Hydrates a Service Provider Person
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationServiceProvider CreateServiceProviderPerson(Boolean mandatorySectionsOnly)
        {
            var serviceProvider = BaseCDAModel.CreateServiceProvider();

            var participant = BaseCDAModel.CreateParticipantForServiceProvider();
            participant.Person = BaseCDAModel.CreatePersonHealthcareProvider();

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "Dr Jane Anderson";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };
            participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") }; 
            participant.Person.PersonNames = new List<IPersonName> { personName };    

            var electronicCommunicationDetail1 =  BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);
            var electronicCommunicationDetail2 =  BaseCDAModel.CreateElectronicCommunicationDetail("1234", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> 
            { 
                electronicCommunicationDetail1,
                electronicCommunicationDetail2
            };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string>
            {
                "1 Clinician Street"
            };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            serviceProvider.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.NegativeInfinity);

            participant.Addresses = new List<IAddress>
            {
                address, 
                address
            };

            var entitlement = BaseCDAModel.CreateEntitlement();
            entitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
            entitlement.Type = EntitlementType.MedicarePrescriberNumber;
            entitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));
            participant.Person.Entitlements = new List<Entitlement> { entitlement, entitlement };

            participant.Person.Qualifications = "M.B.B.S., F.R.A.C.S.";

            participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            participant.Person.Organisation.Name = "Hay Bill Hospital (ServiceProviderPerson)";
            participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            participant.Person.Organisation.Department = "Some department service provider";
            participant.Person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText("Service Provider Casual");
            participant.Person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            participant.Person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Service Provider Manager");
            participant.Person.Organisation.Addresses = new List<IAddress> { address };
            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>() { electronicCommunicationDetail1 , electronicCommunicationDetail2};

            serviceProvider.Participant = participant;

            return serviceProvider;
        }

        /// <summary>
        /// Creates and Hydrates the Diagnoses Intervention section for the Event Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated DiagnosesIntervention object</returns>
        private static DiagnosesIntervention CreateDiagnosesIntervention(Boolean mandatorySectionsOnly)
        {
            var diagnosesInterventions = EventSummary.CreateDiagnosesInterventions();

            if (!mandatorySectionsOnly)
            {
                // NOTE: This section demonstrates the different combinations of Procedure's, Medical History Item's & diagnosis

                var procedureList = new List<Procedure>();
                var medicalHistoryItems = new List<IMedicalHistoryItem>();
                var diagnosisList = new List<IProblemDiagnosisEventSummary>();

                // Procedures
                var procedure = EventSummary.CreateProcedure();
                procedure.Comment = "Procedure Comment";
                procedure.ProcedureName = BaseCDAModel.CreateCodableText("301040004", CodingSystem.SNOMED, "Primary closed wire fixation of fracture");
                procedure.ProcedureDateTime = CdaInterval.CreateLowHigh(new ISO8601DateTime(DateTime.Now.AddDays(-402), ISO8601DateTime.Precision.Day), new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                procedureList.Add(procedure);

                var prodcedure2 = EventSummary.CreateProcedure();
                prodcedure2.Comment = "Comment";
                prodcedure2.ProcedureName = BaseCDAModel.CreateCodableText("388544006", CodingSystem.SNOMED, "Crab RAST");
                prodcedure2.ProcedureDateTime = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                procedureList.Add(prodcedure2);

                var prodcedure3 = EventSummary.CreateProcedure();
                prodcedure3.Comment = "Comment";
                prodcedure3.ProcedureName = BaseCDAModel.CreateCodableText("388544006", CodingSystem.SNOMED, "Crab RAST");
                prodcedure3.ProcedureDateTime = CdaInterval.CreateHigh(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                prodcedure3.ShowOngoingInNarrative = true;
                procedureList.Add(prodcedure3);

                // Medical History Items
                var medicalHistoryItem = EventSummary.CreateMedicalHistoryItem();
                medicalHistoryItem.DateTimeInterval = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                medicalHistoryItem.ShowOngoingInNarrative = true;
                medicalHistoryItem.ItemDescription = "Medical history item description";
                medicalHistoryItem.ItemComment = "Medical History Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem);

                var medicalHistoryItem1 = EventSummary.CreateMedicalHistoryItem();
                medicalHistoryItem1.ShowOngoingInNarrative = true;
                medicalHistoryItem1.ItemDescription = "Medical history item description here";
                medicalHistoryItem1.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem1);

                var medicalHistoryItem2 = EventSummary.CreateMedicalHistoryItem();
                var ongoingInterval2 = CdaInterval.CreateLowHigh(
                                       new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day),
                                       new ISO8601DateTime(DateTime.Now.AddDays(200), ISO8601DateTime.Precision.Day));
                medicalHistoryItem2.DateTimeInterval = ongoingInterval2;
                medicalHistoryItem2.ItemDescription = "Medical history item description here";
                medicalHistoryItem2.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem2);

                var medicalHistoryItem3 = EventSummary.CreateMedicalHistoryItem();
                var ongoingInterval3 = CdaInterval.CreateHigh(
                                       new ISO8601DateTime(DateTime.Now.AddDays(200), ISO8601DateTime.Precision.Day));
                medicalHistoryItem3.DateTimeInterval = ongoingInterval3;
                medicalHistoryItem3.ItemDescription = "Medical history item description here";
                medicalHistoryItem3.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem3);

                // Problem Diagnosis for Event Summary
                var diagnosis = EventSummary.CreateProblemDiagnosis();
                diagnosis.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("282859003", CodingSystem.SNOMED, "Able to maintain a sitting position", "Able to maintain a sitting position");

                diagnosis.DateOfOnset = new ISO8601DateTime(DateTime.Now.AddYears(-2), ISO8601DateTime.Precision.Day);
                diagnosis.Comment = "Solved this";
                diagnosisList.Add(diagnosis);

                diagnosesInterventions.UncategorisedMedicalHistoryItem = medicalHistoryItems;
                diagnosesInterventions.Procedures = procedureList;
                diagnosesInterventions.ProblemDiagnosis = diagnosisList;
            }

            return diagnosesInterventions;
        }


        /// <summary>
        /// Creates and Hydrates the adverse substance reactions section for the Event Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of IAdverseReactionsEventSummay object</returns>
        private static IAdverseReactionsWithoutExclusions CreateAdverseReactions()
        {
            var reactions = EventSummary.CreateAdverseReactions();

            reactions.AdverseSubstanceReaction = new List<Reaction>
            {
                CreateAdverseReaction(BaseCDAModel.CreateCodableText("86461001", CodingSystem.SNOMED , "Plant diterpene")),
                CreateAdverseReaction(BaseCDAModel.CreateCodableText("117491007", CodingSystem.SNOMED , "trans-Nonachlor"))
            };

            return reactions;
        }

        /// <summary>
        /// Creates an adverse reaction.
        /// </summary>
        /// <param name="code">Code for the adverse reaction.</param>
        /// <param name="name">Name of the adverse reaction.</param>
        /// <returns></returns>
        private static Reaction CreateAdverseReaction(ICodableText substanceOrAgent)
        {
            Reaction reaction = EventSummary.CreateReaction();

            reaction.SubstanceOrAgent = substanceOrAgent;

            reaction.ReactionEvent = BaseCDAModel.CreateReactionEvent();
            reaction.ReactionEvent.Manifestations = new List<ICodableText>
            {
                BaseCDAModel.CreateCodableText("248265004", CodingSystem.SNOMED, "Work stress"),
                BaseCDAModel.CreateCodableText("425392003", CodingSystem.SNOMED, "Active advance directive")
            };

            reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

            return reaction;
        }

        /// <summary>
        /// Creates and hydrates the diagnostic investigations section.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A hydrated 'DiagnosticInvestigations' object.</returns>
        private static IDiagnosticInvestigations CreateDiagnosticInvestigations(Boolean mandatorySectionsOnly)
        {
            IDiagnosticInvestigations diagnosticInvestigations = BaseCDAModel.CreateDiagnosticInvestigations();

            diagnosticInvestigations.ImagingExaminationResult = new List<IImagingExaminationResult>
            {
                GenericObjectReuseSample.CreateImagingResults("Leg Image"),
                GenericObjectReuseSample.CreateImagingResults("Head Image")
            };

            // Pathology test results
            diagnosticInvestigations.PathologyTestResult = new List<PathologyTestResult> 
            {
                GenericObjectReuseSample.CreatePathologyResultsV2(PitNameAndPath),
                GenericObjectReuseSample.CreatePathologyResults(mandatorySectionsOnly)
            };

            // Other Test Result 
            diagnosticInvestigations.OtherTestResult = new List<OtherTestResult>
            {
                GenericObjectReuseSample.CreateOtherTestResultAttachment(),
                GenericObjectReuseSample.CreateOtherTestResultText()
            };
      
            // Requested Service
            diagnosticInvestigations.RequestedService = CreateRequestedService(mandatorySectionsOnly);

            return diagnosticInvestigations;
        }

        #endregion
    }
}