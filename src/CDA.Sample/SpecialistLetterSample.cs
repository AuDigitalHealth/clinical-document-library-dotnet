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

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how an SpecialistLetter CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// SpecialistLetter class, and then populated with data as appropriate. The three sections that need to be
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
    public class SpecialistLetterSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\SpecialistLetter.xml";
            }
        }

        public static String StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

        public static String PitNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PIT.txt";
            }
        }

        #endregion

        /// <summary>
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateSpecialistLetterSample_1A(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulateSpecialistLetter(true);

            // Hide Administrative Observations Section 
            document.ShowAdministrativeObservationsSection = false;

            document.SCSContent = SpecialistLetter.CreateSCSContent();

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
                xmlDoc = CDAGenerator.GenerateSpecialistLetter(document);

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
        public XmlDocument PopulateSpecialistLetterSample_1B(string fileName)
        {
            XmlDocument xmlDoc = null;

            var document = PopulateSpecialistLetter(true);
            document.SCSContent = SpecialistLetter.CreateSCSContent();

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
                xmlDoc = CDAGenerator.GenerateSpecialistLetter(document);

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
        public XmlDocument MinPopulatedSpecialistLetterSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var specialistLetter = PopulateSpecialistLetter(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateSpecialistLetter method 
                xmlDoc = CDAGenerator.GenerateSpecialistLetter(specialistLetter);

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
        /// each of its optional entries.
        /// </summary>
        public XmlDocument MaxPopulatedSpecialistLetterSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var specialistLetter = PopulateSpecialistLetter(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateSpecialistLetter method 
                xmlDoc = CDAGenerator.GenerateSpecialistLetter(specialistLetter);

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
        
        #region Populate Methods

        /// <summary>
        /// This method populates a specialistLetter
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>SpecialistLetter</returns>
        internal static SpecialistLetter PopulateSpecialistLetter(Boolean mandatorySectionsOnly)
        {
            var specialistLetter = SpecialistLetter.CreateSpecialistLetter();

            // Set Creation Time
            specialistLetter.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            // Include Logo
            specialistLetter.IncludeLogo = true;
            specialistLetter.LogoPath = OutputFolderPath;
            
            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = SpecialistLetter.CreateCDAContext();
            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
            // CDA Context Version
            cdaContext.Version = "1";

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            // Legal authenticator
            if (!mandatorySectionsOnly)
            {
                cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
                GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);
            }

            // Create information recipient
            if (!mandatorySectionsOnly)
            {
                var recipient1 = BaseCDAModel.CreateInformationRecipient();
                var recipient2 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient1, RecipientType.Primary, mandatorySectionsOnly);
                GenericObjectReuseSample.HydrateRecipient(recipient2, RecipientType.Secondary, mandatorySectionsOnly);
                cdaContext.InformationRecipients = new List<IParticipationInformationRecipient> { recipient1, recipient2 };
            }

            specialistLetter.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model

            specialistLetter.SCSContext = SpecialistLetter.CreateSCSContext();

            specialistLetter.SCSContext.Author = BaseCDAModel.CreateAuthor();
            GenericObjectReuseSample.HydrateAuthor(specialistLetter.SCSContext.Author, mandatorySectionsOnly);

            specialistLetter.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(specialistLetter.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            specialistLetter.SCSContext.DateTimeSubjectSeen = new ISO8601DateTime(DateTime.Now);

            if (!mandatorySectionsOnly)
            {
                specialistLetter.SCSContext.Referrer = CreateReferrer(mandatorySectionsOnly);
                specialistLetter.SCSContext.UsualGP = CreateUsualGPPerson(mandatorySectionsOnly);
            }
            #endregion

            #region Setup and populate the SCS Content model

            specialistLetter.SCSContent = SpecialistLetter.CreateSCSContent();

            // Response details
            specialistLetter.SCSContent.ResponseDetails = CreateResponseDetails(mandatorySectionsOnly);

            // Recommendations
            specialistLetter.SCSContent.Recommendations = CreateRecommendations(mandatorySectionsOnly);

            // Medications
            specialistLetter.SCSContent.Medications = CreateMedications(mandatorySectionsOnly);

            if (!mandatorySectionsOnly)
            {
                // Adverse reactions
                specialistLetter.SCSContent.AdverseReactions = CreateAdverseReactions();

                // Diagnostic Investigations
                specialistLetter.SCSContent.DiagnosticInvestigations = CreateDiagnosticInvestigations(mandatorySectionsOnly);
            }

            #endregion

            return specialistLetter;
        }

        /// <summary>
        /// Creates and Hydrates the adverse substance reactions section for the Event Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated List of IAdverseReactionsEventSummay object</returns>
        private static IAdverseReactionsWithoutExclusions CreateAdverseReactions()
        {
            var reactions = SpecialistLetter.CreateAdverseReactionsWithoutExclusions();
            
            reactions.AdverseSubstanceReaction = new List<Reaction>
            {
                CreateAdverseReaction(BaseCDAModel.CreateCodableText("86461001", CodingSystem.SNOMED, "Plant diterpene")),
                CreateAdverseReaction(BaseCDAModel.CreateCodableText("117491007", CodingSystem.SNOMED, "trans-Nonachlor"))
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
            Reaction reaction = SpecialistLetter.CreateReaction();

            reaction.SubstanceOrAgent = substanceOrAgent;

            reaction.ReactionEvent = BaseCDAModel.CreateReactionEvent();

            reaction.ReactionEvent.Manifestations = new List<ICodableText>
            {
                BaseCDAModel.CreateCodableText("305505004", CodingSystem.SNOMED, "Under care of pathologist"),
                BaseCDAModel.CreateCodableText("170753006", CodingSystem.SNOMED, "Understands diet - diabetes")
            };

            reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

            return reaction;
        }

        /// <summary>
        /// Creates and Hydrates a UsualGP
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated UsualGP object</returns>
        private static IParticipationUsualGP CreateUsualGPPerson(Boolean mandatorySectionsOnly)
        {
            var usualGP = SpecialistLetter.CreateUsualGP();

            var participant = SpecialistLetter.CreateParticipantForUsualGP();

            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { "Information (UsualGPPerson)" };
            personName.FamilyName = "Recipient";
            personName.Titles = new List<string> { "Doctor" };
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            var person = BaseCDAModel.CreatePersonWithOrganisation();
            person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101") };

            person.PersonNames = new List<IPersonName>();
            person.PersonNames.Add(personName);

            participant.Person = person;

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            var addressList = new List<IAddress>
                                {
                                    address
                                };

            participant.Addresses = addressList;

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail
                (
                   "0345754566",
                   ElectronicCommunicationMedium.Telephone,
                   ElectronicCommunicationUsage.WorkPlace
                );

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            if (!mandatorySectionsOnly)
            {
              usualGP.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            }
            else
            {
              usualGP.Role = BaseCDAModel.CreateRole(NullFlavour.NoInformation);
            }

            var organisation = BaseCDAModel.CreateEmploymentOrganisation();
            organisation.Name = "Bay hill hospital";
            organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;

            participant.Person.Organisation = organisation;
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000020052") };
            participant.Person.Organisation.Addresses = new List<IAddress> { address };
            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };
            usualGP.Participant = participant;

            return usualGP;
        }

        /// <summary>
        /// Creates and Hydrates a Referee
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Referee object</returns>
        private static IParticipationReferrer CreateReferrer(Boolean mandatorySectionsOnly)
        {
            var referrer = SpecialistLetter.CreateReferrer();

            referrer.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

            referrer.ParticipationPeriod = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day), 
                new ISO8601DateTime(DateTime.Now.AddDays(200), ISO8601DateTime.Precision.Day));

            var participant = SpecialistLetter.CreateParticipantForReferrer();

            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { "Referrer" };
            personName.FamilyName = "1";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };
            personName.Titles = new List<string> { "Doctor" };

            var person = BaseCDAModel.CreatePersonWithOrganisation();
            person.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101")
            };

            person.PersonNames = new List<IPersonName>();
            person.PersonNames.Add(personName);

            participant.Person = person;

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            var addressList = new List<IAddress> { address };

            participant.Addresses = addressList;

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail
                (
                   "0345754566",
                   ElectronicCommunicationMedium.Telephone,
                   ElectronicCommunicationUsage.WorkPlace
                );

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            var organisation = BaseCDAModel.CreateEmploymentOrganisation();
            organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000020052") };
            organisation.Name = "Burrill Lake Medical Centre";
            organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;

            organisation.Addresses = new List<IAddress> { address };
            organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            person.Organisation = organisation;

            referrer.Participant = participant;

            return referrer;
        }

        /// <summary>
        /// Creates and Hydrates a Response Details object
        /// </summary>
        /// <returns>(IResponseDetails) Response</returns>
        private static IResponseDetails CreateResponseDetails(Boolean mandatorySectionsOnly)
        {
            var responseDetails = SpecialistLetter.CreateResponseDetails();

            responseDetails.Diagnoses = new List<ICodableText>
            {
                BaseCDAModel.CreateCodableText("236629009", CodingSystem.SNOMED, "Chronic radiation cystitis"),
                BaseCDAModel.CreateCodableText("33134003", CodingSystem.SNOMED, "Abscess of forehead")
            };

            if (!mandatorySectionsOnly)
            {
              var procedure1 = SpecialistLetter.CreateProcedure();
              procedure1.ProcedureName = BaseCDAModel.CreateCodableText("268400002", CodingSystem.SNOMED, "12 lead ECG");

              var procedure2 = SpecialistLetter.CreateProcedure();
              procedure2.ProcedureName = BaseCDAModel.CreateCodableText("120214004", CodingSystem.SNOMED, "Anaesthesia for procedure on perineum");

              responseDetails.Procedures = new List<IProcedureName> { procedure1, procedure2 };

              responseDetails.OtherDiagnosisEntries = new List<string>
                {
                    "Text description of first Diagnosis Procedure Entry",
                    "Text description of second Diagnosis Procedure Entry"
                };
            }

            responseDetails.ResponseNarrative = "Response narrative";

            return responseDetails;
        }

        /// <summary>
        /// Creates and Hydrates a Recommendations object
        /// </summary>
        /// <returns>(IRecommendations) Recommendations</returns>
        private static IRecommendations CreateRecommendations(Boolean mandatorySectionsOnly)
        {
            var recommendations = SpecialistLetter.CreateRecommendations();

            if (!mandatorySectionsOnly)
            {
                var recomendationPerson = SpecialistLetter.CreateRecommendation();
                recomendationPerson.Narrative = "Recommendation Narrative Person";
                recomendationPerson.TimeFrame = BaseCDAModel.CreateInterval(
                    new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day), 
                    new ISO8601DateTime(DateTime.Now.AddMonths(4), ISO8601DateTime.Precision.Day));
                recomendationPerson.Addressee = CreateAddresseePerson(mandatorySectionsOnly);

                var recomendationOrganisation = SpecialistLetter.CreateRecommendation();
                recomendationOrganisation.Narrative = "Recommendation Narrative Organisation";
                recomendationOrganisation.TimeFrame = BaseCDAModel.CreateInterval("4", TimeUnitOfMeasure.Month);
                recomendationOrganisation.Addressee = CreateAddresseeOrganisation(mandatorySectionsOnly);

                recommendations.Recommendation = new List<Recommendation>
                                                 {
                                                     recomendationPerson,
                                                     recomendationOrganisation
                                                 };
            }
            else
            {
                recommendations.ExclusionStatement = "No Recommendations are supplied";
            }

            return recommendations;
        }

        /// <summary>
        /// Create Address Organisation
        /// </summary>
        /// <returns>(IParticipationAddressee) Addressee</returns>
        private static IParticipationAddressee CreateAddresseePerson(Boolean mandatorySectionsOnly)
        {
            var addressee = SpecialistLetter.CreateAddressee();
            addressee.Participant = SpecialistLetter.CreateParticipantAddressee();
   
            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { "Tom" };
            personName.FamilyName = "Smith";
            personName.Titles = new List<string> { "Dr" };
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            var person = BaseCDAModel.CreatePersonWithOrganisation();
            person.PersonNames = new List<IPersonName> { personName };
            
            addressee.Participant.Person = person;
            addressee.Participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101") }; 

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.PostCode = "4012";
            address.AustralianAddress.StreetName = "Johnson St";
            address.AustralianAddress.StreetNumber = 12;
            address.AustralianAddress.StreetSuffix = StreetSuffix.South;
            address.AustralianAddress.StreetType = StreetType.Street;

            addressee.Participant.Addresses = new List<IAddress> { address };

            var electronicCommunicationDetails = new List<ElectronicCommunicationDetail>
            {
                BaseCDAModel.CreateElectronicCommunicationDetail("134567891", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace),
                BaseCDAModel.CreateElectronicCommunicationDetail("Tom@optusnet.com.au", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.WorkPlace)
            };

            addressee.Participant.ElectronicCommunicationDetails = electronicCommunicationDetails;

            if (!mandatorySectionsOnly)
            {
                 addressee.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            } 
              else
            {
                 addressee.Role = BaseCDAModel.CreateRole(NullFlavour.NotApplicable);
            }

            // Employment detail
            addressee.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            addressee.Participant.Person.Organisation.Name = "Bay Hill Hospita";
            addressee.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            addressee.Participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            addressee.Participant.Person.Organisation.Department = "Some department person";
            addressee.Participant.Person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText("Person Casual");
            addressee.Participant.Person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            addressee.Participant.Person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Person Manager");

            addressee.Participant.Person.Organisation.Addresses = new List<IAddress> { address };
            addressee.Participant.Person.Organisation.ElectronicCommunicationDetails = electronicCommunicationDetails;

            return addressee;
        }

        /// <summary>
        /// Create Addressee Organisation
        /// </summary>
        /// <returns>(IParticipationAddressee) Addressee</returns>
        private static IParticipationAddressee CreateAddresseeOrganisation(Boolean mandatorySectionsOnly)
        {
            var addressee = SpecialistLetter.CreateAddressee();
            addressee.Participant = SpecialistLetter.CreateParticipantAddressee();

            addressee.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            addressee.Participant.Organisation.Name = "Bay Hill Hospital";
            addressee.Participant.Organisation.NameUsage = OrganisationNameUsage.Other;
            addressee.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            addressee.Participant.Organisation.Department = "Some department organisation";

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Residential;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.PostCode = "4012";
            address.AustralianAddress.StreetName = "Johnson St";
            address.AustralianAddress.StreetNumber = 12;
            address.AustralianAddress.StreetSuffix = StreetSuffix.South;
            address.AustralianAddress.StreetType = StreetType.Street;

            addressee.Participant.Addresses = new List<IAddress>
                                                  {
                                                      address
                                                  };

            addressee.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
            {
                BaseCDAModel.CreateElectronicCommunicationDetail("dgdfg@optusnet.com.au", ElectronicCommunicationMedium.Email, ElectronicCommunicationUsage.Home),
                BaseCDAModel.CreateElectronicCommunicationDetail("134567891", ElectronicCommunicationMedium.Telephone, new List<ElectronicCommunicationUsage>
                {
                    ElectronicCommunicationUsage.PrimaryHome, ElectronicCommunicationUsage.MobileContact
                }),
                BaseCDAModel.CreateElectronicCommunicationDetail("675675675676",ElectronicCommunicationMedium.Telephone, new List<ElectronicCommunicationUsage>
                {
                    ElectronicCommunicationUsage.MobileContact
                }),
            };

            if (!mandatorySectionsOnly)
            {
              addressee.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            }
              else
            {
              addressee.Role = BaseCDAModel.CreateRole(Occupation.FreightHandlerRailOrRoad);
            }

            return addressee;
        }    

        /// <summary>
        /// Creates and Hydrates the Medications section for the E-Referral.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Medications object</returns>
        private static IMedicationsSpecialistLetter CreateMedications(Boolean mandatorySectionsOnly)
        {
            var medications = SpecialistLetter.CreateMedications();

            if (!mandatorySectionsOnly)
            {
                var medicationList = new List<IMedicationItem>();

                var medication1 = SpecialistLetter.CreateMedication();
                medication1.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: 3 times daily");

                // NOTE: ChangeStatus replaces RecommendationOrChange
                medication1.ChangeStatus = BaseCDAModel.CreateCodableText(RecomendationOrChange.ARecommendationToMakeTheChange);
                medication1.ChangeType = BaseCDAModel.CreateCodableText(ChangeTypeNctis.Changed);
                medication1.ChangeReason = BaseCDAModel.CreateStructuredText("Change reason");
                medication1.ClinicalIndication = "Clinical indication";
                medication1.Comment = "Some comment";
                medication1.ChangeDescription = "Recommendation: Change description";

                medication1.Medicine = BaseCDAModel.CreateCodableText
                (
                    "23641011000036102",
                    CodingSystem.AMTV2,
                    "paracetamol 500 mg + codeine phosphate 30 mg tablet",
                    null,
                    null
                );
                medicationList.Add(medication1);

                var medication2 = SpecialistLetter.CreateMedication();
                medication2.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: 3 times daily");
                medication2.ChangeStatus = BaseCDAModel.CreateCodableText(NullFlavour.NotApplicable); 
                medication2.ChangeType = BaseCDAModel.CreateCodableText(NullFlavour.NoInformation);
                medication2.ChangeReason = BaseCDAModel.CreateStructuredText("Change reason");
                medication2.ClinicalIndication = "Clinical indication";
                medication2.Comment = "Some comment";
                medication2.ChangeDescription = "Change description";

                medication2.Medicine = BaseCDAModel.CreateCodableText
                    (
                        "22589011000036109",
                        CodingSystem.AMTV2,
                        "paracetamol 240 mg/5 mL oral liquid",
                        null,
                        null
                    );
                medicationList.Add(medication2);

                medications.MedicationsList = medicationList;
            } 
            else
            {
                medications.ExclusionStatement = SpecialistLetter.CreateExclusionStatement();
                medications.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }

            return medications;
        }

        /// <summary>
        /// Creates a list of requested services.
        /// </summary>
        /// <returns>List of requested services.</returns>
        private static List<RequestedService> CreateRequestedService(Boolean mandatorySectionsOnly)
        {
            var requestedServiceList = new List<RequestedService>();

            // Create Service Provider for a Person
            var requestedServicePerson = SpecialistLetter.CreateRequestedService();
            requestedServicePerson.ServiceCommencementWindow = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day),
                new ISO8601DateTime(DateTime.Now.AddMonths(6), ISO8601DateTime.Precision.Day));

            requestedServicePerson.RequestedServiceDescription = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMED, "Plain chest X-ray");
            requestedServicePerson.ServiceBookingStatus = EventTypes.Definition;
            // Create Duration
            requestedServicePerson.SubjectOfCareInstructionDescription = "Subject Of Care Instruction Description";
            requestedServicePerson.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4));
            // Create Person
            requestedServicePerson.ServiceProvider = CreateServiceProviderPerson(mandatorySectionsOnly);

            // Add to requested service list
            requestedServiceList.Add(requestedServicePerson);

            // Create Service Provider for a Organisation
            var requestedServiceOrganisation = SpecialistLetter.CreateRequestedService();
            requestedServiceOrganisation.RequestedServiceDescription = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMED, "Plain chest X-ray");
            requestedServiceOrganisation.ServiceBookingStatus = EventTypes.Intent;
            requestedServiceOrganisation.ServiceScheduled = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day);
            requestedServiceOrganisation.SubjectOfCareInstructionDescription = "Subject Of Care Instruction Description";
            requestedServiceOrganisation.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4));
            requestedServiceOrganisation.ServiceProvider = CreateServiceProviderOrganisation(mandatorySectionsOnly);

            // Add to list
            requestedServiceList.Add(requestedServiceOrganisation);

            return requestedServiceList;
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

            participant.Person.PersonNames = new List<IPersonName> { personName };
            participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var electronicCommunicationDetailEmail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "Jane@Anderson.com.au",
                ElectronicCommunicationMedium.Email,
                ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetailEmail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            participant.Addresses = new List<IAddress> { address, address };

            serviceProvider.Role = BaseCDAModel.CreateRole(!mandatorySectionsOnly ? Occupation.GeneralMedicalPractitioner : Occupation.FreightHandlerRailOrRoad);

            participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            participant.Person.Organisation.Name = "Bay Hill Hospital";
            participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };
            participant.Person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText("Casual");
            participant.Person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            participant.Person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Manager");
            participant.Person.Organisation.Addresses = new List<IAddress> { address, address };
            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetailEmail };

            serviceProvider.Participant = participant;

            return serviceProvider;
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

            serviceProvider.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            serviceProvider.Participant.Organisation.Name = "Bay Hill Hospital";
            serviceProvider.Participant.Organisation.NameUsage = OrganisationNameUsage.Other;
            serviceProvider.Participant.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            serviceProvider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Residential;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            serviceProvider.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.NotAsked);

            serviceProvider.Participant.Addresses = new List<IAddress> { address };

            return serviceProvider;
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
                GenericObjectReuseSample.CreatePathologyResults(PitNameAndPath),
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
