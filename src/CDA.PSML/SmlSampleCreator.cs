using System;
using System.Collections.Generic;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.PCML.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;

namespace CDA.PSML
{
    public class SmlSampleCreator
    {
        /// <summary>
        /// This method populates an SML model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>SML</returns>
        public static SML PopulateSML_3A(string templatepackageid, string templatepackagever, bool mandatorySectionsOnly, bool includeMoreFields, bool incHpii = true)
        {
            var sharedMedsList = SML.CreateSML();

            // Include Logo
            sharedMedsList.IncludeLogo = false;

            // Set Creation Time
            sharedMedsList.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            ICDAContextSML cdaContext = SML.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);

            // Set Id  [1..1]
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
            // CDA Context Version [1..1]
            cdaContext.Version = "1";

            // Template Package Id used
            cdaContext.TemplatePackageId = BaseCDAModel.CreateIdentifier(templatepackageid, templatepackagever);

            // Custodian [1..1]
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            // Legal authenticator [1..1]
            cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
            GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

            //Optional sections
            if (!mandatorySectionsOnly)
            {
                // Information Recipients
                cdaContext.InformationRecipients = new List<IParticipationInformationRecipient>();

                var recipient1 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient1, RecipientType.Primary, mandatorySectionsOnly);

                var recipient2 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient2, RecipientType.Secondary, mandatorySectionsOnly);

                cdaContext.InformationRecipients.AddRange(new[] { recipient1, recipient2 });
            }


            sharedMedsList.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model

            // Setup and Populate the SCS Context model

            sharedMedsList.SCSContext = SML.CreateSCSContext();

            var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, mandatorySectionsOnly, incHpii);
            sharedMedsList.SCSContext.Author = authorHealthcareProvider;

            // Used for ALL Encounters to sync ids, codes, and locations
            Encounter encounter = new Encounter
            {
                EncounterId = Guid.NewGuid(),
                EncounterPeriod = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now.AddMinutes(-85)), new ISO8601DateTime(DateTime.Now)),
                EncounterClass = BaseCDAModel.CreateCodableText(ActEncounterCode.PreAdmission)
            };

            // Encompassing Encounter
            if (!mandatorySectionsOnly) sharedMedsList.SCSContext.Encounter = encounter;

            // Patient
            sharedMedsList.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(sharedMedsList.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            // Remove Address and Telecom - not allowed in SML - DONT POPULATE TO START WITH - other CDA docs allow this.
            sharedMedsList.SCSContext.SubjectOfCare.Participant.Addresses = null;
            sharedMedsList.SCSContext.SubjectOfCare.Participant.ElectronicCommunicationDetails = null;

            // Need one
            IParticipationPersonOrOrganisation person = SML.CreateParticipationPersonOrOrganisation();
            person.Participant = SML.CreateParticipantPersonOrOrganisation();
            person.Role = BaseCDAModel.CreateRole(HealthcareFacilityTypeCodes.GeneralPractice);
            person.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();

            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Grant";
            name1.GivenNames = new List<string> { "Sally", "Wally" };
            name1.Titles = new List<string> { "Miss" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            person.Participant.Person.PersonNames = new List<IPersonName> { name1 };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Identifier
            person.Participant.Person.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118")
            };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
            var address1 = BaseCDAModel.CreateAddress();

            // MUST BE BUSINESS
            address1.AddressPurpose = AddressPurpose.Business;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address1.AustralianAddress.State = AustralianState.NSW;
            address1.AustralianAddress.PostCode = "5555";
            address1.AustralianAddress.DeliveryPointId = 32568931;

            person.Participant.Addresses = new List<IAddress> { address1 };

            person.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Participant.Person.Organisation.Name = "Hay Bill Hospital";
            person.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;

            // To add scopingOrganization details
            person.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            person.Participant.Organisation.Name = "Hay Bill Hospital"; ;
            person.Participant.Organisation.Department = "Department";

            // New requirement to make address mandatory
            person.Participant.Person.Organisation.Addresses = new List<IAddress> { address1 };

            person.Participant.Person.Organisation.Identifiers = new List<Identifier>
            {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
                //BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null)
            };

            if (!mandatorySectionsOnly)
            {
                //populate with full person details

                // Subject of Care > Participant > Address
                // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Indigenous Status
                // Subject of Care > Participant > Electronic Communication Detail
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);
                var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "authen@globalauthens.com",
                    ElectronicCommunicationMedium.Email,
                    ElectronicCommunicationUsage.WorkPlace);

                person.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

                // Subject of Care > Participant > Entitlement
                var entitlement1 = BaseCDAModel.CreateEntitlement();
                entitlement1.Id =
                    BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement1.Type = EntitlementType.MedicareBenefits;
                entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                var entitlement2 = BaseCDAModel.CreateEntitlement();
                entitlement2.Id =
                    BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement2.Type = EntitlementType.MedicareBenefits;
                entitlement2.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                person.Participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };

                // Optional Participants
                sharedMedsList.SCSContext.Participant = new List<IParticipationPersonOrOrganisation>();

                sharedMedsList.SCSContext.Participant.Add(person);
            }

            #endregion

            #region Setup and populate the SCS Content model

            // Setup and populate the SCS Content model
            sharedMedsList.SCSContent = Nehta.VendorLibrary.CDA.Common.SML.CreateSCSContent();

            // Adverse Reactions
            sharedMedsList.SCSContent.AdverseReactions = CreateAdverseReactions(mandatorySectionsOnly);

            // Medications
            sharedMedsList.SCSContent.MedicationsSml = CreateMedications(mandatorySectionsOnly, includeMoreFields, encounter);

            #endregion

            return sharedMedsList;
        }

        /// <summary>
        /// Creates and Hydrates the adverse substance reactions section for SML.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedAdverseSubstanceReactions object</returns>
        private static IAdverseReactions CreateAdverseReactions(bool mandatorySectionsOnly)
        {
            var adverseReactions = BaseCDAModel.CreateAdverseSubstanceReactions();

            if (!mandatorySectionsOnly)
            {
                adverseReactions.AdverseSubstanceReaction = new List<Reaction>
                {
                    // Note: CdaIntervalOrAge should be time interval Precision to Month or Year, OR an Age
                    CreateAdverseReaction(
                        BaseCDAModel.CreateCodableText("86461001", CodingSystem.SNOMED, "Plant diterpene"),
                        BaseCDAModel.CreateCodableText("425898006", CodingSystem.SNOMED, "Rebaudioside"),
                        new CdaIntervalOrAge("3", TimeUnitOfMeasure.Year), mandatorySectionsOnly),
                    CreateAdverseReaction(
                        BaseCDAModel.CreateCodableText("117491007", CodingSystem.SNOMED, "trans-Nonachlor"),
                        BaseCDAModel.CreateCodableText("117491007", CodingSystem.SNOMED, "trans-Nonachlor"),
                        new CdaIntervalOrAge(new ISO8601DateTime(DateTime.Now.AddYears(-3), ISO8601DateTime.Precision.Month)), mandatorySectionsOnly)
                };
            }
            else
            {
                adverseReactions.EmptyReasonStatement = SML.CreateEmptyReasonStatement();
                // "notasked", "withheld", "unavailable"
                adverseReactions.EmptyReasonStatement.Value = NonClinicalEmptyReason.NotAsked;
                adverseReactions.EmptyReasonStatement.OriginalText = "We did not ask the patient.";
            }

            return adverseReactions;
        }

        /// <summary>
        /// Creates an adverse reaction.
        /// </summary>
        /// <param name="code">Code for the adverse reaction.</param>
        /// <param name="name">Name of the adverse reaction.</param>
        /// <returns></returns>
        private static Reaction CreateAdverseReaction(ICodableText substanceOrAgent, ICodableText substance, CdaIntervalOrAge onsetDate, bool mandatorySectionsOnly)
        {
            Reaction reaction = EReferral.CreateReaction();

            // This is the "Code for an allergy or intolerance statement"
            reaction.SubstanceOrAgent = substanceOrAgent;

            // Initialise
            reaction.ReactionEvent = BaseCDAModel.CreateReactionEvent();
            reaction.ReactionEvent.VerificationStatus = BaseCDAModel.CreateCodableText("confirmed", CodingSystem.HL7AllergyIntoleranceVerificationStatusCodes, "Confirmed");

            if (!mandatorySectionsOnly)
            {

                // This is the "Identification of the specific substance" - can be different from the medication ie the specific ingredient
                reaction.ReactionEvent.Substance = substance;
                reaction.ReactionEvent.Manifestations = new List<ICodableText>
                {
                    BaseCDAModel.CreateCodableText("21909001", CodingSystem.SNOMED, "Fetal viability"),
                    BaseCDAModel.CreateCodableText("15296000", CodingSystem.SNOMED, "Sterility")
                };

                // HL7 Reaction Type - If known use first two (HL7), other wise use the last one (SNOMED)
                reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("allergy", CodingSystem.HL7AllergyIntoleranceType, "Allergy");
                //reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("intolerance", CodingSystem.HL7AllergyIntoleranceType, "Intolerance");
                //reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

                // Onset Date
                reaction.ReactionEvent.ReactionOnsetDate = onsetDate;

                reaction.AdditionalComments = new List<NoteSML>
                {
                    CreateNote("Adverse Reaction Note", new ISO8601DateTime(DateTime.Now), mandatorySectionsOnly)
                };
            }

            return reaction;
        }

        /// <summary>
        /// Creates and Hydrates the CreateMedications for SML
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        private static IMedicationsSML CreateMedications(bool mandatorySectionsOnly, bool includeMoreFields, Encounter encounter)
        {
            var medicationList = BaseCDAModel.CreateMedicationsSml();

            medicationList.CurrentMedications = CreateCurrentMedicationList(mandatorySectionsOnly, includeMoreFields, encounter);

            if (includeMoreFields)
            {
                medicationList.CeasedMedications = CreateCeasedMedicationList(mandatorySectionsOnly, encounter);
            }

            return medicationList;
        }

        private static MedicationListSML CreateCurrentMedicationList(bool mandatorySectionsOnly, bool includeMoreFields, Encounter encounter)
        {
            MedicationListSML medicationList = new MedicationListSML();
            medicationList.MedicineItem = new List<MedicineItemSML>();

            // Author Role
            var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, false);
            medicationList.AuthorRole = authorHealthcareProvider;

            // Encounter
            // Only allowed to use these for PSMLv2
            // "1348931000168107", "1348961000168104", "1348951000168101" ,"1348921000168109"
            // "1348941000168103" ,"1348851000168109", "1378411000168109", "1407621000168104"
            medicationList.Encounter = CreateEncounter(encounter, "Community pharmacy medicine review for patient",
                BaseCDAModel.CreateCodableText("1348961000168104", CodingSystem.SNOMEDCT,
                    "Community pharmacy medicines review", "Community pharmacy medicines review"),
                BaseCDAModel.CreateCodableText("165002", CodingSystem.SNOMEDCT, "Accident-prone"), mandatorySectionsOnly);

            // Indicates whether one or more items in a medicines list are packed in a Dose Administration Aid (DAA).  0..1
            // Conformance Profile v2.0 - 029900 1..1
            medicationList.PackedInDaa = BaseCDAModel.CreateCodableText("1469421000168108", CodingSystem.SNOMEDCT,
                "No medicines packed in dose administration aid");

            if (includeMoreFields)
            {
                // Note - AdditionalListComments
                medicationList.AdditionalListComments = new List<NoteSML>
                {
                    CreateNote("Packed Medicines: No; Please review", new ISO8601DateTime(new DateTime(2007, 12, 25), ISO8601DateTime.Precision.Day), mandatorySectionsOnly),
                    CreateNote("List Comments 2", null, mandatorySectionsOnly)
                };
            }

            // Add three entries
            medicationList.MedicineItem.Add(CreateCurrentMedicationItem1(encounter));
            medicationList.MedicineItem.Add(CreateCurrentMedicationItem2(encounter));
            medicationList.MedicineItem.Add(CreateCurrentMedicationItem3(encounter));

            return medicationList;
        }

        private static MedicationListSML CreateCeasedMedicationList( bool mandatorySectionsOnly, Encounter encounter)
        {
            MedicationListSML medicationList = new MedicationListSML();
            medicationList.MedicineItem = new List<MedicineItemSML>();

            // Author Role
            var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, mandatorySectionsOnly);
            medicationList.AuthorRole = authorHealthcareProvider;

            // Encounter - 21/07/20 As per Conformance Req: 029541, there should be only one healthcare setting (against active, not ceased meds)
            //medicationList.Encounter

            // Note - AdditionalListComments - Not for Ceased
            //medicationList.AdditionalListComments 

            // Add one entry
            medicationList.MedicineItem.Add(CreateCeasedMedicationItem1());

            return medicationList;
        }

        private static MedicineItemSML CreateCurrentMedicationItem1(Encounter encounter)
        {
            MedicineItemSML medicationItem = new MedicineItemSML();
            // ChangeTypeFlag = HL7MedicineItemChangeCodes = Req: 028788
            // https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/medicine-item-change-from-practitioner-medicines-review-1
            medicationItem.ChangeTypeFlag = BaseCDAModel.CreateCodableText(MedicineItemChange.NoChange);
            medicationItem.Id = "0a431358-52f7-4cfa-9061-9cb4763bcba5";

			// MedicationStatus 
            medicationItem.MedicationStatus = BaseCDAModel.CreateCodableText(ActStatus.Active);

			// Medication
            MedicationSML medication = new MedicationSML
            {
                ItemCode = BaseCDAModel.CreateCodableText("53373011000036103", CodingSystem.AMTV3, "Ferro-Grad C", "Ferro-Grad C"),
                BrandName = "Ferro-Grad"
            };

            medication.Form = BaseCDAModel.CreateCodableText("154011000036109", CodingSystem.SNOMED, "tablet");

            medicationItem.Medication = medication;

            medicationItem.Taken = BaseCDAModel.CreateCodableText(MedicationStatementTaken.Yes);
            
            // Medicine Purpose (Why Taken) = Clinical Indication
            medicationItem.MedicinePurpose = new List<ICodableText>
            {
                BaseCDAModel.CreateCodableText(null, null, null, null, "Iron supplement", null, null),
            };

            medicationItem.Dosage = CreateDosageList(false, false);

            return medicationItem;
        }

        private static MedicineItemSML CreateCurrentMedicationItem2(Encounter encounter)
        {
            MedicineItemSML medicationItem = new MedicineItemSML
            {
                ChangeTypeFlag = BaseCDAModel.CreateCodableText(MedicineItemChange.Amended),
                Id = "fad829fb-3e6f-453a-931c-96e8e975cce6",
                MedicationStatus = BaseCDAModel.CreateCodableText(ActStatus.Active),
                // IF ChangeTypeFlag is Amended, ChangeDescription must equal "Dose increased", "Dose reduced" or "Dose changed"
                ChangeDescription = "Dose increased",
                WhereAdministeredCategory = BaseCDAModel.CreateCodableText("This was given in his kitchen")
            };
            
            medicationItem.Encounter = new EncounterSML();
            medicationItem.Encounter.EncounterDescription = "This is just a test encounter";
            medicationItem.Encounter.EncounterStatus = BaseCDAModel.CreateCodableText("completed", CodingSystem.SNOMEDCT, null, null, null);
            medicationItem.Encounter.EncounterPeriod = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Parse("12 Aug 2019"), ISO8601DateTime.Precision.Day),
                new ISO8601DateTime(DateTime.Parse("13 Aug 2019"), ISO8601DateTime.Precision.Day)
            );

            //medicationItem.Encounter = CreateEncounter(encounter, "This is just a test encounter",
            //    BaseCDAModel.CreateCodableText("1348961000168104", CodingSystem.SNOMEDCT, "Community pharmacy medicines review", "Community pharmacy medicines review"),
            //    BaseCDAModel.CreateCodableText("165002", CodingSystem.SNOMEDCT, "Accident-prone"));

            MedicationSML medication = new MedicationSML
            {
                ItemCode = BaseCDAModel.CreateCodableText("28152011000036108", CodingSystem.AMTV3, "amoxicillin 875 mg + clavulanic acid 125 mg tablet, 10", "Amoxicillin 875 mg + clavulanic acid 125 mg tablet, Augmentin Duo Forte")
            };

            medication.BrandName = "Augmentin Duo Forte";
            medication.GenericName = "Augmentin Duo Forte Generic Name Test";

            var manufacturerName = BaseCDAModel.CreateOrganisationName();
            manufacturerName.Name = "some name";
            medication.Manufacturer = (IOrganisation) manufacturerName;
            
            medication.Form = BaseCDAModel.CreateCodableText("261011000036101", CodingSystem.SNOMED, "modified release tablet");

            medication.BatchLotNumber = "LOT2323";
            medication.BatchExpirationDate = new ISO8601DateTime(DateTime.Parse("25 Dec 2008"), ISO8601DateTime.Precision.Day);

            IngredientsSML ingredient = new IngredientsSML();
            ingredient.Id = Guid.NewGuid().ToString();
            ingredient.IngredientCode = BaseCDAModel.CreateCodableText("21433011000036107", CodingSystem.AMTV3, "Paracetamol");
            ingredient.IngredientDescription = "Just common painkiller";
            ingredient.IngredientQuantity = BaseCDAModel.CreateRatio("876", "g", "2", null);
            medication.Ingredients = new List<IngredientsSML>
            {
                ingredient
            };
            
            medicationItem.Medication = medication;

            medicationItem.Taken = BaseCDAModel.CreateCodableText(MedicationStatementTaken.Yes);

            medicationItem.Dosage = CreateDosageList(true, false);

            medicationItem.EffectiveTimeTakenOrNot =
                BaseCDAModel.CreateIntervalHigh(new ISO8601DateTime(new DateTime(2019, 1, 20),
                    ISO8601DateTime.Precision.Day));

            // TODO: Informant
            var informantName = BaseCDAModel.CreatePersonName();
            informantName.FamilyName = "Sinclair";
            var informantId = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1003.0.8003611566708354");
            medicationItem.InformantPractitioner = new PractitionerSML
            {
                PersonNames = new List<IPersonName>() { informantName },
                Identifiers = new List<Identifier>() { informantId },
                Qualifications = "Bachelor of Pharmacy"
            };

            // Reason Not Taken
            //medicationItem.ReasonNotTaken = new List<ICodableText>
            //{
            //    BaseCDAModel.CreateCodableText("246720002", CodingSystem.SNOMED, "Poor focus"),
            //    BaseCDAModel.CreateCodableText("277797007", CodingSystem.SNOMED, "Thin skin")
            //};

            // Medicine Purpose (Why Taken) = Clinical Indication
            medicationItem.MedicinePurpose = new List<ICodableText>
                {
                    BaseCDAModel.CreateCodableText(null, null, null, null, "Chest infection", null, null),
                    // BaseCDAModel.CreateCodableText("161840007", CodingSystem.SNOMED, "Feeding problem due to illness")
                };

            // Additional Comments
            medicationItem.AdditionalComments = new List<NoteSML>
                {
                    CreateNote("Medication Item Note - Additional Comments", new ISO8601DateTime(DateTime.Now), false)
                };

            return medicationItem;
        }

        private static MedicineItemSML CreateCurrentMedicationItem3(Encounter encounter)
        {
            MedicineItemSML medicationItem = new MedicineItemSML();
            // ChangeTypeFlag = HL7MedicineItemChangeCodes = Req: 028788
            // https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/medicine-item-change-from-practitioner-medicines-review-1
            medicationItem.ChangeTypeFlag = BaseCDAModel.CreateCodableText(MedicineItemChange.NoChange);
            medicationItem.Id = "0a431358-52f7-4cfa-9061-9cb4763bcba5";

            // MedicationStatus 
            medicationItem.MedicationStatus = BaseCDAModel.CreateCodableText(ActStatus.Active);

            // Medication
            MedicationSML medication = new MedicationSML
            {
                ItemCode = BaseCDAModel.CreateCodableText("53373011000036103", CodingSystem.AMTV3, "Ferro-Grad C", "Ferro-Grad C"),
                BrandName = "Ferro-Grad"
            };

            medication.Form = BaseCDAModel.CreateCodableText("154011000036109", CodingSystem.SNOMED, "tablet");

            medicationItem.Medication = medication;

            medicationItem.Taken = BaseCDAModel.CreateCodableText(MedicationStatementTaken.Yes);

            // Medicine Purpose (Why Taken) = Clinical Indication
            medicationItem.MedicinePurpose = new List<ICodableText>
            {
                BaseCDAModel.CreateCodableText(null, null, null, null, "Iron supplement", null, null),
            };

            medicationItem.Dosage = CreateDosageList(false, true);

            return medicationItem;
        }

        private static MedicineItemSML CreateCeasedMedicationItem1()
        {
            MedicineItemSML medicationItem = new MedicineItemSML();

            // Not for ceased
            //medicationItem.Id = "ceasedMedId1";

            // ChangeTypeFlag = HL7MedicineItemChangeCodes = Req: 028788
            medicationItem.ChangeTypeFlag = BaseCDAModel.CreateCodableText(MedicineItemChange.Ceased);

            // Change Description - Reason
            medicationItem.ChangeDescription = "Patient no longer needs this medication.";

            // MedicationStatus 
            medicationItem.MedicationStatus = BaseCDAModel.CreateCodableText(ActStatus.Aborted);

            // Medication1
            medicationItem.Medication = new MedicationSML
            {
                // Local Coding System so put in local OID
                ItemCode = BaseCDAModel.CreateCodableText(null, "1.2.36.1.1005.800360", null, null, null, "RAMIPRIL(TAB) 2.5 mg, Tritace", null, null),
                BrandName = "testing"
            };

            // Taken
            medicationItem.Taken = BaseCDAModel.CreateCodableText(MedicationStatementTaken.No);

            // Effective Time - END DATE (High)
            medicationItem.EffectiveTimeTakenOrNot =
               BaseCDAModel.CreateIntervalHigh(new ISO8601DateTime(DateTime.Now.AddDays(-1), ISO8601DateTime.Precision.Day));

            // Not allowed for Ceased
            //medicationItem.InformantRelatedPerson
            //medicationItem.ReasonNotTaken 
            //medicationItem.MedicinePurpose 
            //medicationItem.AdditionalComments 
            
            return medicationItem;
        }

        //private static MedicineItemSML CreateCeasedMedicationItem2()
        //{
        //    MedicineItemSML medicationItem = new MedicineItemSML();

        //    // ChangeTypeFlag = HL7MedicineItemChangeCodes = Req: 028788
        //    medicationItem.ChangeTypeFlag = BaseCDAModel.CreateCodableText(MedicineItemChange.Ceased);

        //    // Change Description
        //    medicationItem.ChangeDescription = "Patient no longer needs this medication.";

        //    // MedicationStatus 
        //    medicationItem.MedicationStatus = BaseCDAModel.CreateCodableText(ActStatus.Completed);
        //    // Medication
        //    medicationItem.Medication = new MedicationSML
        //    {
        //        ItemCode = BaseCDAModel.CreateCodableText("1367601000168103", CodingSystem.AMTV3, "Panadol Night tablet"),
        //        BrandName = "testing"
        //    };

        //    // Taken
        //    medicationItem.Taken = BaseCDAModel.CreateCodableText(MedicationStatementTaken.No);

        //    // Effective Time - END DATE (High)
        //    medicationItem.EffectiveTimeTakenOrNot =
        //        BaseCDAModel.CreateIntervalHigh(new ISO8601DateTime(DateTime.Now.AddDays(-1), ISO8601DateTime.Precision.Day));


        //    return medicationItem;
        //}


        private static MedicationSML CreateMedication(bool mandatorySectionsOnly)
        {
            MedicationSML medication = new MedicationSML
            {
                ItemCode = BaseCDAModel.CreateCodableText("1367601000168103", CodingSystem.AMTV3, "Panadol Night tablet")
            };

            if (!mandatorySectionsOnly)
            {
                medication.BrandName = "Panadol Night tablet";
                medication.GenericName = "paracetamol 500 mg tablet";
                //TODO: Manufacturer
                //medication.Manufacturer;
                medication.Form = BaseCDAModel.CreateCodableText("385057009", CodingSystem.SNOMED, "Film-coated tablet");

                // Optional image
                medication.Image = "tablet.png";
            }

            var ingredient = new IngredientsSML
            {
                Id = Guid.NewGuid().ToString(),
                IngredientCode = BaseCDAModel.CreateCodableText("21433011000036107", CodingSystem.AMTV3, "Paracetamol"),
                IngredientDescription = "Paracetamol",
                IngredientQuantity = BaseCDAModel.CreateRatio("500", "mg", "1", "tablet")
            };

            medication.Ingredients = new List<IngredientsSML>
            {
                ingredient
            };

            return medication;
        }

        private static List<DosageSML> CreateDosageList(bool multiple, bool mandatorySectionsOnly)
        {
            if (multiple)
            {
                return new List<DosageSML>
                {
                    CreateDosage(false, 1),
                    CreateDosage(false, 2)
                };
            }
            else
            {
                return new List<DosageSML>
                {
                    CreateDosage(mandatorySectionsOnly, 1)
                };
            }

        }

        private static DosageSML CreateDosage(bool mandatorySectionsOnly, int sequence)
        {
            DosageSML dosage = new DosageSML
            {
                Instructions = "Take when required."
            };

            if (!mandatorySectionsOnly)
            {
                dosage.Sequence = sequence;
                dosage.DoseTiming = new ISO8601DateTime(DateTime.Now);
                dosage.AsNeeded = false;
                dosage.ClinicalFinding = BaseCDAModel.CreateCodableText();
                dosage.BodySite = BaseCDAModel.CreateCodableText();
                dosage.Route = BaseCDAModel.CreateCodableText("ORNEB", CodingSystem.SNOMEDCT, "Inhalation, nebulization, oral");
                dosage.AdministrationMethod = BaseCDAModel.CreateCodableText("415215001", CodingSystem.SNOMEDCT, "Puff");
                dosage.Dose = BaseCDAModel.CreateQuantity("1", "tablet");
                dosage.MaxDosePerPeriod = BaseCDAModel.CreateFrequency();
                dosage.MaxDosePerPeriod.Denominator = BaseCDAModel.CreateQuantity("1", "h");
                dosage.MaxDosePerPeriod.Numerator = BaseCDAModel.CreateQuantity("1", null);
                dosage.Rate = BaseCDAModel.CreateQuantity("1", "h");
                dosage.Instructions = "Some instructions";
            }

            return dosage;
        }

        private static EncounterSML CreateEncounter(Encounter encounterData, string description, ICodableText encounterType, ICodableText reason, bool mandatorySectionsOnly)
        {
            // 1..1
            var encounter = new EncounterSML
            {
                EncounterId = encounterData.EncounterId,
                EncounterStatus = BaseCDAModel.CreateCodableText(ActEncounterStatus.Completed)
            };

            // Date 1..1
            encounter.EncounterPeriod = encounterData.EncounterPeriod;

            // Type 1..1 Req: 029541
             encounter.EncounterType = encounterType;

            if (!mandatorySectionsOnly)
            {
                // Description 0..1
                if (description != null)
                {
                    encounter.EncounterDescription = description;
                }

                // Class 0..1
                if (encounterData.EncounterClass != null)
                {
                    encounter.EncounterClass = encounterData.EncounterClass;
                }

                //Reason 0..1
                if (reason != null)
                {
                    encounter.EncounterReason = reason;
                }
            }

            return encounter;
        }

        private static NoteSML CreateNote(string noteText, ISO8601DateTime dateTime, bool mandatorySectionsOnly)
        {
            var note = new NoteSML
            {
                NoteText = noteText
            };

            if (!mandatorySectionsOnly)
            {
                // NoteAuthor
                var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
                GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, mandatorySectionsOnly);
                note.NoteAuthor = authorHealthcareProvider;

                // Date
                if (dateTime != null)
                {
                    note.NoteDateTime = dateTime;
                }
            }

            return note;
        }
    }
}
