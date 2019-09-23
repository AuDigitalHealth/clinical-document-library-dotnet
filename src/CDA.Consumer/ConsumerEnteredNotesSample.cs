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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how a Consumer Entered Notes Sample CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// ConsumerEnteredNotes class, and then populated with data as appropriate. The three sections that need to be
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
    public class ConsumerEnteredNotesSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\ConsumerEnteredNotes.xml";
            }
        }

        public static String ImageFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\x-ray.jpg";
            }
        }

        public static String StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

        public static String ResultFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\path1234.pdf";
            }
        }

        // Note: Place this in any string field and and this will insert a break
        private const String DELIMITERBREAK = "<BR>";

        #endregion

        /// <summary>
        /// This sample populates only the mandatory Sections / Entries
        /// </summary>
        public XmlDocument MinPopulatedConsumerEnteredNotesSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var consumerEnteredNotes = PopulateConsumerEnteredNotes(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateConsumerEnteredNotes method 
                xmlDoc = CDAGenerator.GenerateConsumerEnteredNotes(consumerEnteredNotes);

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
        public XmlDocument MaxPopulatedConsumerEnteredNotesSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var consumerEnteredNotes = PopulateConsumerEnteredNotes(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateConsumerEnteredNotes method 
                xmlDoc = CDAGenerator.GenerateConsumerEnteredNotes(consumerEnteredNotes);

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
        /// This method populates an consumerEnteredNotes model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>ConsumerEnteredNotes</returns>
        public static ConsumerEnteredNotes PopulateConsumerEnteredNotes(Boolean mandatorySectionsOnly)
        {
            var consumerEnteredNotes = ConsumerEnteredNotes.CreateConsumerEnteredNotes();

            // Include Logo
            consumerEnteredNotes.IncludeLogo = true;
            consumerEnteredNotes.LogoPath = OutputFolderPath;

            // Set Creation Time
            consumerEnteredNotes.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = ConsumerEnteredNotes.CreateCDAContext();
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

            consumerEnteredNotes.CDAContext = cdaContext;
            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            consumerEnteredNotes.SCSContext = ConsumerEnteredNotes.CreateSCSContext();

            consumerEnteredNotes.SCSContext.Author = ConsumerEnteredNotes.CreateAuthor();
            PopulateAuthor(consumerEnteredNotes.SCSContext.Author, mandatorySectionsOnly);

            consumerEnteredNotes.SCSContext.SubjectOfCare = PopulateSubjectofCare(mandatorySectionsOnly);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            consumerEnteredNotes.SCSContent = ConsumerEnteredNotes.CreateSCSContent();

            consumerEnteredNotes.SCSContent.Title = "My Health Summary";
            consumerEnteredNotes.SCSContent.Description = "I have been really healthy all my life.";

            #endregion

            return consumerEnteredNotes;
        }

        public static void PopulateAuthor(IParticipationConsumerAuthor author, bool mandatoryOnly)
        {
            var person = BaseCDAModel.CreatePerson();

            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = new ISO8601DateTime(DateTime.Now);

            // Document Author > Role
            author.Role = BaseCDAModel.CreateRole(Occupation.MedicalLaboratoryScientist);

            // Document Author > Participant
            author.Participant = ConsumerEnteredHealthSummary.CreateParticipantForAuthor();

            // Document Author > Participant > Entity Identifier
            person.Identifiers = new List<Identifier>();

            if (!mandatoryOnly)
            {
                // IHI
                person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003604444567894"));
            }
            else
            {
                // Care Agency Employee Identifier
                person.Identifiers.Add(
                    BaseCDAModel.CreateIdentifier(
                    "Care Agency Employee Identifier",
                    HealthcareIdentifierGeographicArea.LocalClientIdentifier,
                    null,
                    "1.2.36.1.2001.1007.4.9123453453453458",
                    null));
            }

            // Document Author > Participant > Per-son or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Healthy";

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Wong";

            person.PersonNames = new List<IPersonName>() { name1, name2 };

            if (!mandatoryOnly)
            {
                author.Participant.RelationshipToSubjectOfCare = BaseCDAModel.CreateRole(Occupation.MedicalLaboratoryScientist);

                name1.GivenNames = new List<string> { "Fitun" };
                name1.Titles = new List<string> { "Dr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

                name2.GivenNames = new List<string> { "Davey" };
                name2.Titles = new List<string> { "Brother" };
                name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

                // Document Author > Participant > Address
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Residential;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
                address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var addressList = new List<IAddress> { address1, address2 };

                author.Participant.Addresses = addressList;

                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "2 Clinician Street" };
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";

                // Document Author > Participant > Elec-tronic Communication Detail
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);
                var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "authen@globalauthens.com",
                    ElectronicCommunicationMedium.Email,
                    ElectronicCommunicationUsage.WorkPlace);

                author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };
            }

            author.Participant.Person = person;
        }

        /// <summary>
        /// Creates and Hydrates an SubjectofCare
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated SubjectofCare</returns>
        public static IParticipationSubjectOfCare PopulateSubjectofCare(bool mandatoryOnly)
        {
          var subjectOfCare = BaseCDAModel.CreateSubjectOfCare();

          var participant = BaseCDAModel.CreateParticipantForSubjectOfCare();

          // Subject of Care > Participant > Person or Organisation or Device > Person
          var person = BaseCDAModel.CreatePersonForSubjectOfCare();

          // Subject of Care > Participant > Entity Identifier
          person.Identifiers = new List<Identifier> 
            { 
                 BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003604444567894"),
                 BaseCDAModel.CreateMedicalRecordNumber("123456", "1.2.3.4", "Croydon GP Centre"),
                 // NOTE : ONLY 11 digit Individual Medicare Card Number's is permitted in the Entity Identifier
                 BaseCDAModel.CreateMedicareNumber(MedicareNumberType.IndividualMedicareCardNumber,"59501704511"),
                 BaseCDAModel.CreateIdentifier
                 (
                     "Test Authority", 
                     HealthcareIdentifierGeographicArea.StateOrTerritoryIdentifier, 
                     "457456", 
                     "1.22.333.444.55555", 
                 BaseCDAModel.CreateCodableText("1.1.1.1.1.1", CodingSystem.NCTIS, "DisplayName", "Original Text", null)
                 )
            };

          // Subject of Care > Participant > Person or Organisation or Device > Person > Person Name
          var name1 = BaseCDAModel.CreatePersonName();
          name1.FamilyName = "Grant";
          name1.GivenNames = new List<string> { "Sally", "Wally" };
          name1.Titles = new List<string> { "Miss" };
          name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

          var name2 = BaseCDAModel.CreatePersonName();
          name2.FamilyName = "Grant";
          name2.GivenNames = new List<string> { "Wally" };
          name2.Titles = new List<string> { "Mrs" };
          name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

          person.PersonNames = new List<IPersonName> { name1, name2 };

          // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Indigenous Status
          person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;

          // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Indigenous Status
          person.Gender = Gender.Female;

          // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Date of Birth Detail > 
          // Date of Birth
          person.DateOfBirth = new ISO8601DateTime(DateTime.Now.AddYears(-57));

          if (!mandatoryOnly)
          {
            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
            person.Gender = Gender.Female;

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Age Detail -> Age
            person.Age = 54;

            // Subject of Care > Participant > Electronic Communication Detail
            var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);
            var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "authen@globalauthens.com",
                ElectronicCommunicationMedium.Email,
                ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

            // Subject of Care > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();

            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
            address2.InternationalAddress = BaseCDAModel.CreateInternationalAddress();

            participant.Addresses = new List<IAddress> { address1, address2 };

            address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address1.AustralianAddress.State = AustralianState.NSW;
            address1.AustralianAddress.PostCode = "5555";

            address2.InternationalAddress.AddressLine = new List<string> { "1 Overseas Street" };
            address2.InternationalAddress.Country = Country.NewCaledonia;
            address2.InternationalAddress.PostCode = "12345";
            address2.InternationalAddress.StateProvince = "Foreign Land";

            person.DateOfBirthCalculatedFromAge = true;
            person.DateOfBirthAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
            person.AgeAccuracyIndicator = true;
            person.BirthPlurality = 3;
            person.BirthOrder = 2;
            person.DateOfDeath = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day);
            person.DateOfDeathAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
            person.CountryOfBirth = Country.Australia;
            person.StateOfBirth = AustralianState.QLD;

            // Subject of Care > Participant > Entitle-ment
            var entitlement1 = BaseCDAModel.CreateEntitlement();
            entitlement1.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
            entitlement1.Type = EntitlementType.MedicareBenefits;
            entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

            var entitlement2 = BaseCDAModel.CreateEntitlement();
            entitlement2.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "2244567891");
            entitlement2.Type = EntitlementType.MedicareBenefits;
            entitlement2.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

            var mothersOriginalFamilyName = BaseCDAModel.CreatePersonName();
            mothersOriginalFamilyName.FamilyName = "Jones";

            // Subject of Care > Participant > Person or Organisation or Device > Person > Mothers Original Family Name
            person.MothersOriginalFamilyName = mothersOriginalFamilyName;

            participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };
          }

          participant.Person = person;
          subjectOfCare.Participant = participant;

          return subjectOfCare;
        }

        #endregion
    }
}
