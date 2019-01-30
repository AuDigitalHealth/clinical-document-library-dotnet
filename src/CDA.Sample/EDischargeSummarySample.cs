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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how an EDischargeSummary CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// EDischargeSummary class, and then populated with data as appropriate. The three sections that need to be
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
    /// E.g. E-DischargeSummary specific CDA sections or entries; for example Subject of Care.
    /// 
    /// The SCS Content typically contains information that is to be represented with the body of the document.
    /// </summary>
    public class EDischargeSummarySample
    {

        #region Properties

        public static string OutputFolderPath { get; set; }

        private string DELIMITERBREAK = "<BR>";

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\EDischargeSummary.xml";
            }
        }

        public  static String xPreNameAndPath
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
        public XmlDocument PopulateEDischargeSummarySample_1A(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulateDischargeSummary(true);

            document.SCSContent = EDischargeSummary.CreateSCSContent();

            // Hide Administrative Observations Section 
            document.ShowAdministrativeObservationsSection = false;

            // Add mandatory elements to the discharge 1A document
            document.SCSContent.Event = EDischargeSummary.CreateEvent();
            var encounter = EDischargeSummary.CreateEncounter();
            encounter.ResponsibleHealthProfessional = CreateResponsibleHealthProfessional(false);
            encounter.EncounterPeriod = CdaInterval.CreateHigh(new ISO8601DateTime(DateTime.Now));
            encounter.SeparationMode = BaseCDAModel.CreateSeparationMode(ModeOfSeparation.AdministrativeDischarge);
            document.SCSContent.Event.Encounter = encounter;

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
                xmlDoc = CDAGenerator.GenerateEDischargeSummary(document);

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
        /// This example show an example of populating a 1B document
        /// </summary>
        public XmlDocument PopulateEDischargeSummarySample_1B(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulateDischargeSummary(true);
            document.SCSContent = EDischargeSummary.CreateSCSContent();

            // Hide Administrative Observations Section 
            document.ShowAdministrativeObservationsSection = false;

            document.IncludeLogo = false;

            // Add mandatory elements to the discharge 1A document
            document.SCSContent.Event = EDischargeSummary.CreateEvent();
            var encounter = EDischargeSummary.CreateEncounter();
            encounter.ResponsibleHealthProfessional = CreateResponsibleHealthProfessional(false);
            encounter.EncounterPeriod = CdaInterval.CreateHigh(new ISO8601DateTime(DateTime.Now));
            encounter.SeparationMode = BaseCDAModel.CreateSeparationMode(ModeOfSeparation.AdministrativeDischarge);
            document.SCSContent.Event.Encounter = encounter;

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
                xmlDoc = CDAGenerator.GenerateEDischargeSummary(document);

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
        /// This sample populates only the mandatory Sections / Entries
        /// </summary>
        public XmlDocument MinPopulatedEDischargeSummary(string fileName)
        {
            XmlDocument xmlDoc;

            var eDischargeSummary = PopulateDischargeSummary(true);

            try
            {
                //Pass the E-Discharge model into the GenerateEDischarge method 
                xmlDoc = CDAGenerator.GenerateEDischargeSummary(eDischargeSummary);

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
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries
        /// </summary>
        public XmlDocument MaxPopulatedEDischargeSummary(string fileName)
        {
            XmlDocument xmlDoc;

            var eDischargeSummary = PopulateDischargeSummary(false);

            try
            {
                //Pass the E-Discharge model into the GenerateEDischarge method 
                xmlDoc = CDAGenerator.GenerateEDischargeSummary(eDischargeSummary);

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
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        internal static EDischargeSummary PopulateDischargeSummary(Boolean mandatoryOnly)
        {
            var eDischargeSummary = EDischargeSummary.CreateEDischargeSummary();
            
            #region Setup and populate the CDA context model
            // Setup and populate the CDA context model
            ICDAContextEDischargeSummary cdaContext = EDischargeSummary.CreateCDAContext();
            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
            // CDA Context Version
            cdaContext.Version = "1";

            // Include Logo
            eDischargeSummary.IncludeLogo = true;
            eDischargeSummary.LogoPath = OutputFolderPath;

            // Set Creation Time
            eDischargeSummary.DocumentCreationTime = new ISO8601DateTime(DateTime.Now.AddHours(9));

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatoryOnly);

            if (!mandatoryOnly)
            {
                // Legal authenticator
                cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
                GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatoryOnly);

                // Create information recipient
                var recipient1 = BaseCDAModel.CreateInformationRecipient();
                var recipient2 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient1, RecipientType.Primary, mandatoryOnly);
                GenericObjectReuseSample.HydrateRecipient(recipient2, RecipientType.Secondary, mandatoryOnly);
                cdaContext.InformationRecipients = new List<IParticipationInformationRecipient> { recipient1, recipient2 };
            }

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model

            // Create CreateSCSContext
            eDischargeSummary.SCSContext = EDischargeSummary.CreateSCSContext();

            // Create Author
            eDischargeSummary.SCSContext.Author = CreateAuthor(mandatoryOnly);

            // Create Subject Of Care
            eDischargeSummary.SCSContext.SubjectOfCare = EDischargeSummary.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(eDischargeSummary.SCSContext.SubjectOfCare, mandatoryOnly, false);

            // Create Facility
            eDischargeSummary.SCSContext.Facility = CreateFacility(mandatoryOnly);

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            eDischargeSummary.SCSContent = EDischargeSummary.CreateSCSContent();

            // Create Event
            eDischargeSummary.SCSContent.Event = CreateEvent(mandatoryOnly);

            // Create Medication
            eDischargeSummary.SCSContent.Medications = CreateMedications(mandatoryOnly);

            // Create Health Profile
            eDischargeSummary.SCSContent.HealthProfile = CreateHealthProfile(mandatoryOnly);

            // Create Plan
            eDischargeSummary.SCSContent.Plan = CreatePlan(mandatoryOnly);

            eDischargeSummary.CDAContext = cdaContext;

            #endregion

            return eDischargeSummary;
        }

        /// <summary>
        /// Creates and Hydrates an Facility (IFacility)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Facility</returns>
        private static IParticipationFacility CreateFacility(Boolean mandatoryOnly)
        {
            var facility = EDischargeSummary.CreateFacility();
            facility.Participant = EDischargeSummary.CreateParticipantForFacility();

            facility.Role = BaseCDAModel.CreateRole(HealthcareFacilityTypeCodes.ChiropracticAndOsteopathicServices);

            facility.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            facility.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003627500000328") };
            facility.Participant.Organisation.Name = "Canberra Hospital";

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;

            facility.Participant.Addresses = new List<IAddress> 
            { 
                address
            };

            facility.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
                {
                    BaseCDAModel.CreateElectronicCommunicationDetail("0262442229", ElectronicCommunicationMedium.Page, ElectronicCommunicationUsage.WorkPlace)
                };

            if (!mandatoryOnly)
            {
                address.AustralianAddress.StreetNumber = 12;
                address.AustralianAddress.StreetName = "Yamba";
                address.AustralianAddress.StreetType = StreetType.Avenue;
                address.AustralianAddress.SuburbTownLocality = "Garran";
                address.AustralianAddress.State = AustralianState.ACT;
                address.AustralianAddress.PostCode = "2605";

                facility.Participant.Addresses = new List<IAddress> 
                { 
                    address
                };

                facility.Participant.Organisation.Department = "Department Name";

                facility.Participant.Organisation.NameUsage = OrganisationNameUsage.Unknown;
            } 

            return facility;
        }

        /// <summary>
        /// Creates and Hydrates an Event section
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Event object</returns>
        private static Event CreateEvent(Boolean mandatoryOnly)
        {
            var dischargeSummaryEvent = EDischargeSummary.CreateEvent();

            dischargeSummaryEvent.Encounter = CreateEncounter(mandatoryOnly);
            dischargeSummaryEvent.ProblemDiagnosesThisVisit = CreateProblemDiagnosesThisVisit(mandatoryOnly);
            dischargeSummaryEvent.ClinicalSynopsis = CreateClinicalSynopsis();

            if (mandatoryOnly == false)
            {
                dischargeSummaryEvent.ClinicalIntervention = CreateClinicalIntervention();
                dischargeSummaryEvent.DiagnosticInvestigations = CreateDiagnosticInvestigations();
            }

            return dischargeSummaryEvent;
        }

        /// <summary>
        /// Creates and Hydrates an Encounter section
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Encounter object</returns>
        private static Encounter CreateEncounter(Boolean mandatoryOnly)
        {
            var encounter = EDischargeSummary.CreateEncounter();

            encounter.ResponsibleHealthProfessional = CreateResponsibleHealthProfessional(mandatoryOnly);

            encounter.SeparationMode = BaseCDAModel.CreateSeparationMode(ModeOfSeparation.AdministrativeDischarge);

            // Create Specialty1
            encounter.Specialty = new List<ICodableText>();
            ICodableText specialty1 = BaseCDAModel.CreateCodableText("394582007", CodingSystem.SNOMED, "Dermatology");
            ICodableText specialty2 = BaseCDAModel.CreateCodableText("408459003", CodingSystem.SNOMED, "Paediatric cardiology");

            encounter.Specialty.Add(specialty1);
            encounter.Specialty.Add(specialty2);

            if (!mandatoryOnly)
            {
                encounter.LocationOfDischarge = "Ward B";
                encounter.OtherParticipants = CreateOtherParticipants(mandatoryOnly);
                encounter.EncounterPeriod = CdaInterval.CreateHigh(new ISO8601DateTime(DateTime.Now));
            } 
              else
            {
                encounter.EncounterPeriodNullFlavor = NullFlavour.NotApplicable;
            }

            return encounter;
        }

        /// <summary>
        /// Creates and Hydrates a Responsible Health Professional
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ResponsibleHealthProfessional object</returns>
        private static IParticipationResponsibleHealthProfessional CreateResponsibleHealthProfessional(Boolean mandatoryOnly)
        {
            var responsibleHealthProfessional = EDischargeSummary.CreateResponsibleHealthProfessional();
            responsibleHealthProfessional.Role = BaseCDAModel.CreateRole(Occupation.SurgeonGeneral);

            responsibleHealthProfessional.Participant = EDischargeSummary.CreateParticipantForResponsibleHealthProfessional();
            responsibleHealthProfessional.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "Smith";

            responsibleHealthProfessional.Participant.Person.PersonNames = new List<IPersonName> { personName };

            responsibleHealthProfessional.Participant.Person.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145"),
            };
            
            if (!mandatoryOnly)
            {
                personName.NameUsages = new List<NameUsage> { NameUsage.Legal };
                personName.GivenNames = new List<string> { "John", "Middle" };
                personName.Titles = new List<string> { "Miss" };

                responsibleHealthProfessional.ParticipationPeriod = BaseCDAModel.CreateInterval("12", TimeUnitOfMeasure.Year);

                var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

                responsibleHealthProfessional.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

                var address = BaseCDAModel.CreateAddress();
                address.AddressPurpose = AddressPurpose.Business;
                address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address.AustralianAddress.State = AustralianState.QLD;
                address.AustralianAddress.PostCode = "5555";
                address.AustralianAddress.DeliveryPointId = 32568931;

                responsibleHealthProfessional.Participant.Addresses =
                    new List<IAddress> 
                    {
                        address, 
                    };

                responsibleHealthProfessional.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
                responsibleHealthProfessional.Participant.Person.Organisation.Identifiers = new List<Identifier>
                {
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000021100")
                };

                responsibleHealthProfessional.Participant.Person.Organisation.Name = "Super Healthy Hospital";
                responsibleHealthProfessional.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.EnterpriseName;
                responsibleHealthProfessional.Participant.Person.Organisation.Department = "Endocrinology";
                responsibleHealthProfessional.Participant.Person.Organisation.Addresses = new List<IAddress> { address };
                responsibleHealthProfessional.Participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };
            } 

            return responsibleHealthProfessional;
        }

        /// <summary>
        /// Creates and Hydrates a Service Provider Organisation
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationServiceProvider CreateServiceProviderOrganisation(Boolean mandatoryOnly)
        {
            var serviceProvider = BaseCDAModel.CreateServiceProvider();

            serviceProvider.Participant = BaseCDAModel.CreateParticipantForServiceProvider();

            serviceProvider.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            serviceProvider.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000001136") };
            serviceProvider.Participant.Organisation.Name = "Bay Hill Hospital";
            serviceProvider.Participant.Organisation.NameUsage = OrganisationNameUsage.EnterpriseName;
            serviceProvider.Participant.Organisation.Department = "Department";

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);
            var electronicCommunicationDetailEmail = BaseCDAModel.CreateElectronicCommunicationDetail("BayHill@BayHill.com.au", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.WorkPlace);

            serviceProvider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetailEmail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Residential;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            serviceProvider.Participant.Addresses = new List<IAddress> 
            { 
                address 
            };

            serviceProvider.Role = !mandatoryOnly ? BaseCDAModel.CreateRole("Hospital") : BaseCDAModel.CreateRole(NullFlavour.Other);

            return serviceProvider;
        }

        /// <summary>
        /// Creates and Hydrates a Service Provider Person
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationServiceProvider CreateServiceProviderPerson(Boolean mandatoryOnly)
        {
            var serviceProvider = BaseCDAModel.CreateServiceProvider();

            var participant = BaseCDAModel.CreateParticipantForServiceProvider();
            participant.Person = BaseCDAModel.CreatePersonHealthcareProvider();
            participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") }; 

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "Anderson";
            personName.GivenNames = new List<string> { "Jane"};
            personName.Titles = new List<string> { "Dr"};
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            participant.Person.PersonNames = new List<IPersonName> { personName };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var electronicCommunicationDetail2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0414754566",
                ElectronicCommunicationMedium.Mobile,
                ElectronicCommunicationUsage.WorkPlace);

            var electronicCommunicationDetail3 = BaseCDAModel.CreateElectronicCommunicationDetail("Jane@Hospital.com", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail2, electronicCommunicationDetail3 };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.Business;
            address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address2.AustralianAddress.State = AustralianState.QLD;
            address2.AustralianAddress.PostCode = "5555";
            address2.AustralianAddress.DeliveryPointId = 32568931;

            participant.Addresses = new List<IAddress>
            {
                address, address2
            };

            participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            participant.Person.Organisation.Name = "Super Healthy Hospital";
            participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            participant.Person.Organisation.Department = "Anaesthesia";
            participant.Person.Organisation.Addresses = new List<IAddress> { address, address2};
            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail2, electronicCommunicationDetail3 };

            serviceProvider.Role = !mandatoryOnly ? BaseCDAModel.CreateRole(Occupation.SurgeonGeneral) : BaseCDAModel.CreateRole(NullFlavour.Other);

            serviceProvider.Participant = participant;

            return serviceProvider;
        }

        /// <summary>
        /// Creates and Hydrates a CreateRecommendationRecipientPerson
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationHealthProfessional CreateRecommendationRecipientPerson(Boolean mandatoryOnly)
        {
            var recommendationRecipient = EDischargeSummary.CreateRecommendationRecipient();

            recommendationRecipient.Participant = EDischargeSummary.CreateHealthPersonOrOrganisation();
            recommendationRecipient.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();

            recommendationRecipient.Participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") };

            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { "Alison" };
            personName.FamilyName = "Hodgers";
            personName.Titles = new List<string> { "Dr" };
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            recommendationRecipient.Participant.Person.PersonNames = new List<IPersonName> { personName };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            recommendationRecipient.Participant.Addresses = new List<IAddress> { address };

            if (!mandatoryOnly)
            {
                var electronicCommunicationDetail1 = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

                var electronicCommunicationDetail2 = BaseCDAModel.CreateElectronicCommunicationDetail("Alison@Hodgers.com", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.WorkPlace);

                recommendationRecipient.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail1, electronicCommunicationDetail2 };

                recommendationRecipient.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();

                recommendationRecipient.Participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };

                recommendationRecipient.Participant.Person.Organisation.Name = "Super Healthy Hospital";
                recommendationRecipient.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.EnterpriseName;
                recommendationRecipient.Participant.Person.Organisation.Department = "Urology";
                recommendationRecipient.Participant.Person.Organisation.Addresses = new List<IAddress> { address };
                recommendationRecipient.Participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail1, electronicCommunicationDetail2};

                recommendationRecipient.Role = BaseCDAModel.CreateRole(Occupation.Urologist);
            } else
            {
                recommendationRecipient.Role = BaseCDAModel.CreateRole(NullFlavour.Other);
            }

            return recommendationRecipient;
        }

        /// <summary>
        /// Creates and Hydrates a Create Recommendation Recipient Organisation
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationHealthProfessional CreateRecommendationRecipientOrganisation(Boolean mandatoryOnly)
        {
            var recommendationRecipient = EDischargeSummary.CreateRecommendationRecipient();

            recommendationRecipient.Participant = EDischargeSummary.CreateParticipantForRecommendationRecipient();

            recommendationRecipient.Participant.Organisation = BaseCDAModel.CreateOrganisation();

            recommendationRecipient.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };

            recommendationRecipient.Participant.Organisation.Name = "Super Healthy Hospital";
            recommendationRecipient.Participant.Organisation.NameUsage = OrganisationNameUsage.Other;
            recommendationRecipient.Participant.Organisation.Department = "Endocrinology";

            var electronicCommunicationDetail1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var electronicCommunicationDetail2 = BaseCDAModel.CreateElectronicCommunicationDetail("SuperHealthyHospital@Hospital.com", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.WorkPlace);

            recommendationRecipient.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail1, electronicCommunicationDetail2 };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Residential;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            recommendationRecipient.Participant.Addresses = new List<IAddress> { address };

            if (!mandatoryOnly)
            {
                recommendationRecipient.Role = BaseCDAModel.CreateCodableText("HOSP", CodingSystem.HL7ServiceDeliveryLocationRoleType, "Hospital");
            }
                else
            {
                recommendationRecipient.Role = BaseCDAModel.CreateRole(NullFlavour.NotApplicable);
            }

            return recommendationRecipient;
        }

        /// <summary>
        /// Creates and Hydrates a Participation Other Participant Object (IParticipationOtherParticipant)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of IParticipationOtherParticipant objects</returns>
        private static List<IParticipationOtherParticipant> CreateOtherParticipants(Boolean mandatoryOnly)
        {
            // Other Participant 
            var otherParticipant = EDischargeSummary.CreateOtherParticipant();

            otherParticipant.Role = BaseCDAModel.CreateRole(Occupation.NursePractitioner);

            otherParticipant.RoleType = RoleType.CareGiver;

            otherParticipant.Participant = EDischargeSummary.CreateParticipantForOtherParticipant();
            otherParticipant.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();

            otherParticipant.Participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") }; 

            otherParticipant.ParticipationPeriod = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now), 
                new ISO8601DateTime(DateTime.Now.AddDays(5)));

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "OtherParticipant";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            otherParticipant.Participant.Person.PersonNames = new List<IPersonName> { personName };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

            otherParticipant.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "Other 1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            var addressList = new List<IAddress>{ address };

            otherParticipant.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            otherParticipant.Participant.Person.Organisation.Name = "Super Healthy Hospital";
            otherParticipant.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.EnterpriseName;
            otherParticipant.Participant.Person.Organisation.Department = "Endocrinology";
            otherParticipant.Participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") }; 
            otherParticipant.Participant.Person.Organisation.Addresses = new List<IAddress> { address };
            otherParticipant.Participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            otherParticipant.Participant.Addresses = addressList;

            return new List<IParticipationOtherParticipant> { otherParticipant };
        }

        /// <summary>
        /// Creates and Hydrates a Nominated Primary Health Care Provider (IParticipationNominatedPrimaryHealthCareProvider)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated list of IParticipationNominatedPrimaryHealthProvider</returns>
        private static List<IParticipationNominatedPrimaryHealthCareProvider> CreateNominatedPrimaryHealthCareProviders(Boolean mandatoryOnly)
        {
            return new List<IParticipationNominatedPrimaryHealthCareProvider> 
            { 
                CreateNominatedPrimaryHealthCareProviderPerson(mandatoryOnly), 
                CreateNominatedPrimaryHealthCareProviderOrganisation(mandatoryOnly) 
            };
        }

        /// <summary>
        /// Creates and Hydrates a Nominated Primary Health Care Provider (IParticipationNominatedPrimaryHealthCareProvider)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationNominatedPrimaryHealthProvider</returns>
        private static IParticipationNominatedPrimaryHealthCareProvider CreateNominatedPrimaryHealthCareProviderPerson(Boolean mandatoryOnly)
        {
            var nominatedPrimaryHealthCareProvider = EDischargeSummary.CreateNominatedPrimaryHealthCareProvider();
            nominatedPrimaryHealthCareProvider.Role = BaseCDAModel.CreateRole(Occupation.AgedOrDisabledCarer);

            var participant = EDischargeSummary.CreateParticipantForNominatedPrimaryHealthProvider();
            participant.Person = BaseCDAModel.CreatePersonWithOrganisation();
            participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") }; 

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "NominatedPrimaryHealthCareProviderPerson";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };
            
            participant.Person.PersonNames = new List<IPersonName> { personName };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            participant.Addresses = new List<IAddress> { address };

            participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            participant.Person.Organisation.Name = "Super Healthy Hospital";
            participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            participant.Person.Organisation.Department = "Endocrinology";
            participant.Person.Organisation.Addresses = new List<IAddress> { address };
            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            nominatedPrimaryHealthCareProvider.Participant = participant;

            return nominatedPrimaryHealthCareProvider;
        }

        /// <summary>
        /// Creates and Hydrates a Nominated Primary Health Care Provider Organisation
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationNominatedPrimaryHealthCareProvider</returns>
        private static IParticipationNominatedPrimaryHealthCareProvider CreateNominatedPrimaryHealthCareProviderOrganisation(Boolean mandatoryOnly)
        {
            var nominatedPrimaryHealthCareProvider = EDischargeSummary.CreateNominatedPrimaryHealthCareProvider();
            nominatedPrimaryHealthCareProvider.Role = BaseCDAModel.CreateRole("HOSP", CodingSystem.HL7ServiceDeliveryLocationRoleType, "Hospital", null);

            nominatedPrimaryHealthCareProvider.Participant = EDischargeSummary.CreateParticipantForNominatedPrimaryHealthProvider();

            nominatedPrimaryHealthCareProvider.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            nominatedPrimaryHealthCareProvider.Participant.Organisation.Name = "Bay Hill Hospital (NominatedPrimaryHealthCareProviderOrganisation)";
            nominatedPrimaryHealthCareProvider.Participant.Organisation.NameUsage = OrganisationNameUsage.Other;
            nominatedPrimaryHealthCareProvider.Participant.Organisation.Department = "Health Department";

            nominatedPrimaryHealthCareProvider.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") }; 

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566",ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

            nominatedPrimaryHealthCareProvider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            nominatedPrimaryHealthCareProvider.Participant.Addresses = new List<IAddress> { address };

            return nominatedPrimaryHealthCareProvider;
        }

        /// <summary>
        /// Creates and Hydrates a Discharge Summary Problem Diagnosis (IDischargeSummaryProblemDiagnosis)
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of IDischargeSummaryProblemDiagnosis object</returns>
        private static List<IDischargeSummaryProblemDiagnosis> CreateProblemDiagnosis()
        {
            // Problem Diagnosis
            var problemDiagnosis1 = EDischargeSummary.CreateProblemDiagnosis();
            problemDiagnosis1.ProblemDiagnosisDescription = BaseCDAModel.CreateCodableText("88706003", CodingSystem.SNOMED, "Infection caused by Spirura");
            problemDiagnosis1.ProblemDiagnosisType = BaseCDAModel.CreateCodableText("295433003", CodingSystem.SNOMED, "Accidental bisacodyl overdose");

            var problemDiagnosis2 = EDischargeSummary.CreateProblemDiagnosis();
            problemDiagnosis2.ProblemDiagnosisDescription = BaseCDAModel.CreateCodableText("240701006", CodingSystem.SNOMED, "Systemic aspergillosis");
            problemDiagnosis2.ProblemDiagnosisType = BaseCDAModel.CreateCodableText("8319008", CodingSystem.SNOMED, "Principal diagnosis");

            return new List<IDischargeSummaryProblemDiagnosis> { problemDiagnosis1, problemDiagnosis2 };
        }

        /// <summary>
        /// Creates and Hydrates a Problem Diagnoses This Visit
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ProblemDiagnosesThisVisit object</returns>
        private static ProblemDiagnosesThisVisit CreateProblemDiagnosesThisVisit(Boolean mandatoryOnly)
        {
            var problemDiagnosesThisVisit = EDischargeSummary.CreateProblemDiagnosesThisVisit();

            if (mandatoryOnly == false)
            {
                problemDiagnosesThisVisit.ProblemDiagnosis = CreateProblemDiagnosis();
            }
            else
            {
                // GeneralStatement
                var exclusionStatement = EDischargeSummary.CreateExclusionStatement();
                exclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
                problemDiagnosesThisVisit.ExclusionStatement = exclusionStatement;
            }

            return problemDiagnosesThisVisit;
        }

        /// <summary>
        /// Creates and Hydrates a Clinical Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ClinicalSynopsis object</returns>
        private static ClinicalSynopsis CreateClinicalSynopsis()
        {
            // All Mandatory
            var clinicalSynopsis = EDischargeSummary.CreateClinicalSynopsis();
            clinicalSynopsis.Description = "Admitted for elective, right Total Knee Replacement (cemented prosthesis). Day 3, developed bilateral basal atelectasis. The FBC showed high WCC (20.0) and high neutrophils (16.0). Commenced on doxycycline and chest physio. Due to mild anaemia prior to surgery and subsequent operative blood loss, required a blood transfusion of three units. Subsequently made steady progress, regaining good mobility in his knee and is able to mobilise with the aid of a stick. Right knee Xray showed no fracture or dislocation, with the total knee prosthesis well positioned post surgery.";
            return clinicalSynopsis;
        }

        /// <summary>
        /// Creates and Hydrates a Clinical Intervention
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ClinicalIntervention object</returns>
        private static ClinicalIntervention CreateClinicalIntervention()
        {
            var clinicalIntervention = EDischargeSummary.CreateClinicalIntervention();

            clinicalIntervention.Description =
            new List<ICodableText> 
                { 
                    BaseCDAModel.CreateCodableText("179344006", CodingSystem.SNOMED , "Primary cemented total knee replacement"),
                    BaseCDAModel.CreateCodableText("88705004", CodingSystem.SNOMED , "Insulin C-peptide measurement"),
                    BaseCDAModel.CreateCodableText("71493000", CodingSystem.SNOMED , "Transfusion of packed red blood cells")
                };

            return clinicalIntervention;
        }

        /// <summary>
        /// Creates and Hydrates a Medication for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IMedicationsDischargeSummary object</returns>
        private static IMedicationsDischargeSummary CreateMedications(Boolean mandatoryOnly)
        {
            var medications = EDischargeSummary.CreateMedications();

            // Create Current Medications
            medications.CurrentMedications = EDischargeSummary.CreateCurrentMedications();
            // Create Ceased Medications
            medications.CeasedMedications = EDischargeSummary.CreateCeasedMedications();

            if (mandatoryOnly == false)
            {
                medications.CurrentMedications.TherapeuticGoods = CreateTherapeuticGoodsCurrent();
                medications.CeasedMedications.TherapeuticGoods = CreateTherapeuticGoodCeased();
            } 
            else
            {
                medications.CeasedMedications.ExclusionStatement = CreateExclusionStatement();
                medications.CurrentMedications.ExclusionStatement = CreateExclusionStatement();;
            }

            return medications;
        }

        /// <summary>
        /// Creates and Hydrates a TherapeuticGood for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of ITherapeuticGood object</returns>
        private static List<ITherapeuticGood> CreateTherapeuticGoodsCurrent()
        { 
            var therapeuticGood1 = EDischargeSummary.CreateTherapeuticGood();
            therapeuticGood1.TherapeuticGoodIdentification = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMTV3, "paracetamol 500 mg + codeine phosphate 30 mg tablet");
            therapeuticGood1.DoseInstruction = "1 tablet once daily oral";
            therapeuticGood1.UnitOfUseQuantityDispensed = "2 tablets";
            therapeuticGood1.ReasonForTherapeuticGood = "Pneumonia";
            therapeuticGood1.AdditionalComments = "Additional Comments";

            // Create MedicationHistory
            therapeuticGood1.MedicationHistory = EDischargeSummary.CreateMedicationHistory();
            therapeuticGood1.MedicationHistory.ItemStatus = BaseCDAModel.CreateCodableText("New");
            therapeuticGood1.MedicationHistory.ChangesMade = BaseCDAModel.CreateCodableText("Dose decreased from 2 tablet 4 a day");
            therapeuticGood1.MedicationHistory.ReasonForChange = "Due to hypotension";
            therapeuticGood1.MedicationHistory.MedicationDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now.AddDays(5)));

            var therapeuticGood2 = EDischargeSummary.CreateTherapeuticGood();
            therapeuticGood2.TherapeuticGoodIdentification = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMTV3, "paracetamol 500 mg + codeine phosphate 30 mg tablet");
            therapeuticGood2.DoseInstruction = "2 tablets daily oral";
            therapeuticGood2.UnitOfUseQuantityDispensed = "4 tablets";
            therapeuticGood2.ReasonForTherapeuticGood = "Stress";

            // Create MedicationHistory
            therapeuticGood2.MedicationHistory = EDischargeSummary.CreateMedicationHistory();
            therapeuticGood2.MedicationHistory.ItemStatus = BaseCDAModel.CreateCodableText("New");
            therapeuticGood2.MedicationHistory.ChangesMade = BaseCDAModel.CreateCodableText("Dose decreased from 1 tablet Twice a day");
            therapeuticGood2.MedicationHistory.ReasonForChange = "Stress";
            therapeuticGood2.MedicationHistory.MedicationDuration = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now),
                new ISO8601DateTime(DateTime.Now.AddDays(500)));


            return new List<ITherapeuticGood> { therapeuticGood1, therapeuticGood2 };
        }

        /// <summary>
        /// Creates and Hydrates a TherapeuticGoodCeased for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of ITherapeuticGoodCeased</returns>
        private static List<ITherapeuticGoodCeased> CreateTherapeuticGoodCeased()
        {

            var therapeuticGood1 = EDischargeSummary.CreateTherapeuticGoodCeased();
            therapeuticGood1.TherapeuticGoodIdentification = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMTV3, "paracetamol 500 mg + codeine phosphate 30 mg tablet");
            // Create MedicationHistory
            therapeuticGood1.MedicationHistory = EDischargeSummary.CreateMedicationHistoryCeased();
            therapeuticGood1.MedicationHistory.ItemStatus = BaseCDAModel.CreateCodableText("Ceased");
            therapeuticGood1.MedicationHistory.ChangesMade = BaseCDAModel.CreateCodableText("Changes Made");
            therapeuticGood1.MedicationHistory.ReasonForChange = "No longer required";

            var therapeuticGood2 = EDischargeSummary.CreateTherapeuticGoodCeased();
            therapeuticGood2.TherapeuticGoodIdentification = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMTV3, "paracetamol 500 mg + codeine phosphate 30 mg tablet");
            // Create MedicationHistory
            therapeuticGood2.MedicationHistory = EDischargeSummary.CreateMedicationHistoryCeased();
            therapeuticGood2.MedicationHistory.ItemStatus = BaseCDAModel.CreateCodableText("Ceased");
            therapeuticGood2.MedicationHistory.ChangesMade = BaseCDAModel.CreateCodableText("Changes Made");
            therapeuticGood2.MedicationHistory.ReasonForChange = "No longer needed";

            return new List<ITherapeuticGoodCeased> { therapeuticGood1, therapeuticGood2 };
        }

        /// <summary>
        /// Creates and Hydrates a GeneralStatement
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of IStatements</returns>
        private static Statement CreateExclusionStatement()
        {
            var exclusionStatement = EDischargeSummary.CreateExclusionStatement();
            exclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            return exclusionStatement;
        }

        /// <summary>
        /// Creates a HealthProfile for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>Hydrated HealthProfile object</returns>
        private static HealthProfile CreateHealthProfile(Boolean mandatoryOnly)
        {
            // Create Nominated Primary Health Care Providers
            var healthProfile = EDischargeSummary.CreateHealthProfile();
            // Create AdverseReactions
            healthProfile.AdverseReactions = EDischargeSummary.CreateAdverseReactions();

            if (mandatoryOnly == false)
            {
                healthProfile.NominatedPrimaryHealthCareProviders = CreateNominatedPrimaryHealthCareProviders(mandatoryOnly);
                healthProfile.AdverseReactions.Reactions = CreateReactions();
                healthProfile.Alerts = CreateAlerts();
            }
            else
            {
                healthProfile.AdverseReactions.ExclusionStatement = CreateExclusionStatement();
            }

            return healthProfile;
        }

        /// <summary>
        /// Creates a Adverse Reaction for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of AdverseReaction</returns>
        private static List<IAdverseReactionDischargeSummary> CreateReactions()
        {
            // Create AdverseReaction
            var adverseReaction1 = EDischargeSummary.CreateReactions();
            adverseReaction1.AgentDescription = BaseCDAModel.CreateCodableText("373270004", CodingSystem.SNOMED, "Penicillin -class of antibiotic-");
            adverseReaction1.AdverseReactionType = BaseCDAModel.CreateCodableText("64305001", CodingSystem.SNOMED, "Urticaria");
            adverseReaction1.ReactionDescriptions = new List<ICodableText>() 
            { 
                BaseCDAModel.CreateCodableText("64305001", CodingSystem.SNOMED, "Urticaria"),
                BaseCDAModel.CreateCodableText("22943007", CodingSystem.SNOMED, "Trunk structure"),
                BaseCDAModel.CreateCodableText("182281004", CodingSystem.SNOMED, "Entire lower limb"),
                BaseCDAModel.CreateCodableText("16932000", CodingSystem.SNOMED, "Nausea and vomiting")
            };

            // Create AdverseReaction
            var adverseReaction2 = EDischargeSummary.CreateReactions();
            adverseReaction2.AgentDescription = BaseCDAModel.CreateCodableText("372826007", CodingSystem.SNOMED, "Metoprolol");
            adverseReaction2.AdverseReactionType = BaseCDAModel.CreateCodableText("64305001", CodingSystem.SNOMED, "Urticaria");
            adverseReaction2.ReactionDescriptions = new List<ICodableText> 
            { 
               BaseCDAModel.CreateCodableText("155585005", CodingSystem.SNOMED, "Chronic obstructive airways disease NOS")
            };

            return new List<IAdverseReactionDischargeSummary> { adverseReaction1, adverseReaction2 };
        }

        /// <summary>
        /// Creates a Alert for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of Alerts</returns>
        private static Alerts CreateAlerts()
        {
            var alerts  = EDischargeSummary.CreateAlerts();

            // Create Alert
            var alert1 = EDischargeSummary.CreateAlert();
            alert1.AlertDescription = BaseCDAModel.CreateCodableText("78648007", CodingSystem.SNOMED, "At risk for infection");
            alert1.AlertType = BaseCDAModel.CreateCodableText("74188005", CodingSystem.SNOMED, "Medical");

            // Create Alert
            var alert2 = EDischargeSummary.CreateAlert();
            alert2.AlertDescription = BaseCDAModel.CreateCodableText("78648007", CodingSystem.SNOMED, "At risk for infection");
            alert2.AlertType = BaseCDAModel.CreateCodableText("74188005", CodingSystem.SNOMED, "Medical");

            alerts.AlertList = new List<Alert> { alert1, alert2 };

            return alerts;
        }

        /// <summary>
        /// Creates a Plan for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Plan Object</returns>
        private static Plan CreatePlan(Boolean mandatoryOnly)
        {
            // Create Plan
            var plan = EDischargeSummary.CreatePlan();
            plan.RecommendationsInformationProvided = CreateRecommendationsInformationProvided(mandatoryOnly);

            if (!mandatoryOnly)
            {
                plan.ArrangedServices = CreateArrangedServices(mandatoryOnly);
            }

            return plan;
        }

        /// <summary>
        /// Creates a Arranged Services for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of ArrangedServices</returns>
        private static List<ArrangedServices> CreateArrangedServices(Boolean mandatoryOnly)
        {
            // Create Arranged Services Organisation
            var arrangedServices = EDischargeSummary.CreateArrangedServices();
            arrangedServices.ArrangedServiceDescription = BaseCDAModel.CreateCodableText("Orthopaedic outpatient clinic appointment for 4 weeks post-discharge progress review");
            arrangedServices.ServiceCommencementWindow = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now.AddDays(20)));

            arrangedServices.Status = EventTypes.Appointment;

            // Create Service Provider
            arrangedServices.ServiceProvider = CreateServiceProviderPerson(mandatoryOnly);

            // Create Arranged Services Person
            var arrangedServices1 = EDischargeSummary.CreateArrangedServices();
            arrangedServices1.ArrangedServiceDescription = BaseCDAModel.CreateCodableText("Orthopaedic outpatient clinic appointment for 4 weeks post-discharge progress review");
            arrangedServices1.ServiceCommencementWindow = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now),
                new ISO8601DateTime(DateTime.Now.AddDays(60)));

            arrangedServices1.Status = EventTypes.Appointment;

            // Create Service Provider
            arrangedServices1.ServiceProvider = CreateServiceProviderOrganisation(mandatoryOnly);

            return new List<ArrangedServices> { arrangedServices, arrangedServices1 };
        }

        /// <summary>
        /// Creates a Recommendations Information Provided for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated RecommendationsInformationProvided object</returns>
        private static RecommendationsInformationProvided CreateRecommendationsInformationProvided(Boolean mandatoryOnly)
        {
            // Create Recommendations Information Provided
            var recommendationsInformationProvided = EDischargeSummary.CreateRecommendationsInformationProvided();

            recommendationsInformationProvided.RecommendationsProvided = CreateRecommendationsProvided(mandatoryOnly);

            if (!mandatoryOnly)
            {
                recommendationsInformationProvided.InformationProvided = CreateInformationProvided();
            }

            return recommendationsInformationProvided;
        }

        /// <summary>
        /// Creates a Recommendations Provided for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of RecommendationsProvided</returns>
        private static List<RecommendationsProvided> CreateRecommendationsProvided(Boolean mandatoryOnly)
        {

            // Create Recommendations Provided Person
            var recommendationsProvidedPerson = EDischargeSummary.CreateRecommendationsProvided();
            recommendationsProvidedPerson.RecommendationNote = "Please remove the staples on Aug 24 2010. Please ensure aspirin is recommenced 3 days post discharge. Please follow-up anaemia.";
            recommendationsProvidedPerson.RecommendationRecipient = CreateRecommendationRecipientPerson(mandatoryOnly);

            // Create Recommendations Provided Organisation
            var recommendationsProvidedOrganisation = EDischargeSummary.CreateRecommendationsProvided();
            recommendationsProvidedOrganisation.RecommendationNote = "Please remove from waiting list";
            recommendationsProvidedOrganisation.RecommendationRecipient = CreateRecommendationRecipientOrganisation(mandatoryOnly);

            return new List<RecommendationsProvided>
            {
                recommendationsProvidedPerson, recommendationsProvidedOrganisation
            };
        }

        /// <summary>
        /// Creates a Information Provided for Discharge Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated InformationProvided object</returns>
        private static InformationProvided CreateInformationProvided()
        {
            // Create Information Provided
            var informationProvided = EDischargeSummary.CreateInformationProvided();
            informationProvided.InformationProvidedToRelevantParties = "Patient was given a brochure explaining the expected post-op recovery following a total knee replacement. The physiotherapist provided a list of home exercises. The good prognosis for return to activity was discussed with the patient - likely to be able to walk with a stick at six weeks.";
            return informationProvided;
        }

        /// <summary>
        /// Creates and Hydrates the diagnostic investigations substance reactions section 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated DiagnosticInvestigations object</returns>
        private static IDiagnosticInvestigationsDischargeSummary CreateDiagnosticInvestigations()
        {
            var diagnosticInvestigations = EDischargeSummary.CreateDiagnosticInvestigations();

            diagnosticInvestigations.ImagingExaminationResult = new List<IImagingExaminationResult>
            {
                GenericObjectReuseSample.CreateImagingResults("Image chest x-ray 1"),
                //GenericObjectReuseSample.CreateImagingResults("Image chest x-ray 2"),
            };

            diagnosticInvestigations.PathologyTestResult = new List<PathologyTestResult> 
            {
                GenericObjectReuseSample.CreatePathologyResults(xPreNameAndPath),
                GenericObjectReuseSample.CreatePathologyResults(false)
            };

            // Other Test Result 
            diagnosticInvestigations.OtherTestResult = new List<OtherTestResult>
            {
                    GenericObjectReuseSample.CreateOtherTestResultAttachment(),
                    GenericObjectReuseSample.CreateOtherTestResultText()
            };

            return diagnosticInvestigations;
        }

        /// <summary>
        /// Creates and Hydrates an author
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated author</returns>
        public static IParticipationDocumentAuthor CreateAuthor(bool mandatoryOnly)
        {
          var author = BaseCDAModel.CreateAuthor();

          var person = BaseCDAModel.CreatePersonWithOrganisation();

          // Document Author > Role
          author.Role = BaseCDAModel.CreateRole(Occupation.SurgeonGeneral);

          // Document Author > Participant
          author.Participant = BaseCDAModel.CreateParticipantForAuthor();

          // Document Author > Participant > Entity Identifier
          person.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118")
            };

          // Document Author > Participant > Address
          var address1 = BaseCDAModel.CreateAddress();
          address1.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
          address1.AddressPurpose = AddressPurpose.Business;
          address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

          var address2 = BaseCDAModel.CreateAddress();
          address2.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
          address2.AddressPurpose = AddressPurpose.Business;
          address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

          var addressList = new List<IAddress> { address1, address2 };

          author.Participant.Addresses = addressList;

          // Document Author > Participant > Electronic Communication Detail
          var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
              "0345754566",
              ElectronicCommunicationMedium.Telephone,
              ElectronicCommunicationUsage.WorkPlace);
          var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
              "authen@globalauthens.com",
              ElectronicCommunicationMedium.Email,
              ElectronicCommunicationUsage.WorkPlace);

          author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

          // Document Author > Participant > Per-son or Organisation or Device > Person > Person Name
          var name1 = BaseCDAModel.CreatePersonName();
          name1.FamilyName = "Smith";

          var name2 = BaseCDAModel.CreatePersonName();
          name2.Titles = new List<string> { "Sir" };
          name2.FamilyName = "Wong";
          name2.NameSuffix = new List<string> { "III" };

          person.PersonNames = new List<IPersonName> { name1, name2 };

          // Document Author > Participant > Person or Organisation or Device > Person > Employment Detail
          person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
          person.Organisation.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789")
            };
          person.Organisation.Name = "Good Hospital";

          person.Organisation.Addresses = addressList;
          person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

          // Document Author > Participation Period
          author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second));

          if (!mandatoryOnly)
          {
            person.Organisation.Department = "Surgical Ward";
            person.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;

            name1.GivenNames = new List<string> { "Good" };
            name1.Titles = new List<string> { "Dr" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            name2.GivenNames = new List<string> { "Davey" };
            name2.Titles = new List<string> { "Br" };
            name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

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

          }

          author.Participant.Person = person;

          return author;
        }

        #endregion
    }
}
