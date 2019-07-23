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
using System.IO;
using System.Linq;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.Common;
using PathologyTestResult = Nehta.VendorLibrary.CDA.SCSModel.Common.PathologyTestResult;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// Demonstrates the reuse of header objects in the CDA Library 
    /// </summary>
    public class GenericObjectReuseSample
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

      public static string ResultFileNameAndPath
      {
        get
        {
          return OutputFolderPath + @"\path1234.pdf";
        }
      }

        public static string OtherResultFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\other1234.pdf";
            }
        }

        #endregion

        /// <summary>
        /// Creates and Hydrates an Subject Of Care demonstrating its usage with multiple CDA Libraries 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated SubjectOfCare</returns>
        public IParticipationSubjectOfCare PopulateSubjectOfCare()
        {
            IParticipationSubjectOfCare genericSubjectOfCare = BaseCDAModel.CreateSubjectOfCare();

            HydrateSubjectofCare(genericSubjectOfCare, false, false);

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var specialistLetter = new SpecialistLetter(DocumentStatus.Interim)
                                       {
                                           SCSContext = SpecialistLetter.CreateSCSContext()
                                       };

            specialistLetter.SCSContext.SubjectOfCare = genericSubjectOfCare;

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var dischargeSummary = new EDischargeSummary(DocumentStatus.Interim)
                                       {
                                           SCSContext = EDischargeSummary.CreateSCSContext()
                                       };

            dischargeSummary.SCSContext.SubjectOfCare = genericSubjectOfCare;

            return genericSubjectOfCare;
        }

        /// <summary>
        /// Creates and Hydrates an Author demonstrating its usage with multiple CDA Libraries 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Author</returns>
        public IParticipationDocumentAuthor PopulateAuthor()
        {
            IParticipationDocumentAuthor genericAuthor = BaseCDAModel.CreateAuthor();

            HydrateAuthor(genericAuthor, false);

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var specialistLetter = new SpecialistLetter(DocumentStatus.Interim)
            {
                SCSContext = SpecialistLetter.CreateSCSContext()
            };

            specialistLetter.SCSContext.Author = genericAuthor;

            return genericAuthor;
        }

        /// <summary>
        /// Creates and Hydrates an Authenticator demonstrating its usage with multiple CDA Libraries 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Authenticator</returns>
        public IParticipationLegalAuthenticator PopulateAuthenticator()
        {
            var genericAuthenticator = BaseCDAModel.CreateAuthenticator();

            HydrateAuthenticator(genericAuthenticator, false);

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var specialistLetter = new SpecialistLetter(DocumentStatus.Interim)
            {
                CDAContext = SpecialistLetter.CreateCDAContext()
            };

            specialistLetter.CDAContext.LegalAuthenticator = genericAuthenticator;

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var dischargeSummary = new EDischargeSummary(DocumentStatus.Interim)
            {
                CDAContext = EDischargeSummary.CreateCDAContext()
            };

            dischargeSummary.CDAContext.LegalAuthenticator = genericAuthenticator;

            return genericAuthenticator;
        }

        /// <summary>
        /// Creates and Hydrates an Custodian demonstrating its usage with multiple CDA Libraries 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Custodian</returns>
        public IParticipationCustodian PopulateCustodian()
        {
            IParticipationCustodian genericCustodian = BaseCDAModel.CreateCustodian();

            HydrateCustodian(genericCustodian, false);

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var specialistLetter = new SpecialistLetter(DocumentStatus.Interim)
            {
                CDAContext = SpecialistLetter.CreateCDAContext()
            };

            specialistLetter.CDAContext.Custodian = genericCustodian;

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var dischargeSummary = new EDischargeSummary(DocumentStatus.Interim)
            {
                CDAContext = EDischargeSummary.CreateCDAContext()
            };

            dischargeSummary.CDAContext.Custodian = genericCustodian;

            return genericCustodian;
        }

        /// <summary>
        /// Creates and Hydrates an Recipient demonstrating its usage with multiple CDA Libraries 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Recipient</returns>
        public IParticipationInformationRecipient PopulateRecipient()
        {
            var genericRecipient = BaseCDAModel.CreateInformationRecipient();

            HydrateRecipient(genericRecipient, RecipientType.Primary, false);

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var specialistLetter = new SpecialistLetter(DocumentStatus.Interim)
            {
                CDAContext = SpecialistLetter.CreateCDAContext()
            };

            specialistLetter.CDAContext.InformationRecipients = new List<IParticipationInformationRecipient>
            {
                genericRecipient
            };

            //The requirement to instantiate the objects with the factory ensures that the correct implementations
            //of each interface are instantiated; e.g. that the correct content and context are created.
            var dischargeSummary = new EDischargeSummary(DocumentStatus.Interim)
            {
                CDAContext = EDischargeSummary.CreateCDAContext()
            };

            dischargeSummary.CDAContext.InformationRecipients = new List<IParticipationInformationRecipient>
            {
                genericRecipient
            };

            return genericRecipient;
        }
        
        /// <summary>
        /// Creates and Hydrates an SubjectofCare
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated SubjectofCare</returns>
        public static void HydrateSubjectofCare(IParticipationSubjectOfCare subjectOfCare, bool mandatoryOnly, bool shs)
        {
            var participant = BaseCDAModel.CreateParticipantForSubjectOfCare();

            // Subject of Care > Participant > Person or Organisation or Device > Person
            var person = BaseCDAModel.CreatePersonForSubjectOfCare();

            // Subject of Care > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();

            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            
            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
            address2.InternationalAddress = BaseCDAModel.CreateInternationalAddress();
            
            participant.Addresses = new List<IAddress> { address1, address2 };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Harding";
            name1.GivenNames = new List<string> { "Frank", "Troy" };
            name1.Titles = new List<string> { "Mr" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Harding";
            name2.GivenNames = new List<string> { "Frank", "Tobie" };
            name2.Titles = new List<string> { "Mr" };
            name2.NameUsages = new List<NameUsage> { NameUsage.Legal };

            person.PersonNames = new List<IPersonName> { name1, name2 };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
            person.Gender = Gender.Male;

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Date of Birth Detail > 
            // Date of Birth
            person.DateOfBirth = new ISO8601DateTime(DateTime.Now.AddYears(-57),ISO8601DateTime.Precision.Millisecond);
            
            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Indigenous Status
            person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;   

            // Subject of Care > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> 
            { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003608666701594"),

                // Other Health Identifiers

                //BaseCDAModel.CreateMedicalRecordNumber("123456", "1.2.36.1.2001.1005.29.8003620833333789", "Croydon GP Centre"),
                // NOTE : ONLY 11 digit Individual Medicare Card Number's is permitted in the Entity Identifier
                //BaseCDAModel.CreateMedicareNumber(MedicareNumberType.IndividualMedicareCardNumber, "59501704511"),
                //BaseCDAModel.CreateDvaNumber("123456789", EntitlementType.RepatriationHealthGoldBenefits)
            };

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

            if (!mandatoryOnly)
            {
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.NSW;
                address1.AustralianAddress.PostCode = "5555";
                address1.AustralianAddress.DeliveryPointId = 32568931;
                
                address2.InternationalAddress.AddressLine = new List<string> { "1 Overseas Street" };
                address2.InternationalAddress.Country = Country.NewCaledonia;
                address2.InternationalAddress.PostCode = "12345";
                address2.InternationalAddress.StateProvince = "Foreign Land";

                person.DateOfBirthCalculatedFromAge = true;
                person.DateOfBirthAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
                person.Age = 54;
                person.AgeUnitOfMeasure = AgeUnitOfMeasure.Year;

                person.AgeAccuracyIndicator = true;
                person.BirthPlurality = 3;
                person.BirthOrder = 2;
                person.DateOfDeath = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day);
                person.DateOfDeathAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
                person.CountryOfBirth = Country.Australia;
                person.StateOfBirth = AustralianState.QLD;
                
                // Subject of Care > Participant > Entitlement
                var entitlement = BaseCDAModel.CreateEntitlement();
                entitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement.Type = EntitlementType.MedicareBenefits;
                entitlement.ValidityDuration = BaseCDAModel.CreateIntervalHigh(new ISO8601DateTime(DateTime.Today.AddMonths(15)));
                participant.Entitlements = new List<Entitlement> { entitlement };

                // Subject of Care > Participant > Person or Organisation or Device > Person > Source Of Death Notification
                person.SourceOfDeathNotification = SourceOfDeathNotification.Relative;

                var mothersOriginalFamilyName = BaseCDAModel.CreatePersonName();
                mothersOriginalFamilyName.FamilyName = "Jones";

                // Subject of Care > Participant > Person or Organisation or Device > Person > Mothers Original Family Name
                person.MothersOriginalFamilyName = mothersOriginalFamilyName;

                // Don't include in SHS
                if (!shs) person.InterpreterRequired = CreateInterpreterRequiredAlert();


            } else
            {
                address1.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
                address2.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
            }

            participant.Person = person;
            subjectOfCare.Participant = participant;
        }

        /// <summary>
        /// Creates a Interpreter Required Alert (COMMUNICATION ALERT)
        /// </summary>
        /// <returns>Interpreter Required Alert</returns>
        public static InterpreterRequiredAlert CreateInterpreterRequiredAlert()
        {
            // Create Interpreter Required Alert
            var interpreterRequiredAlert = ServiceReferral.CreateInterpreterRequiredAlert();

            // Preferred Language
            interpreterRequiredAlert.PreferredLanguage = new[]
            {
               "en-AU",
               "fr-CN"
            };

            return interpreterRequiredAlert;
        }

        /// <summary>
        /// Creates and Hydrates an author
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated author</returns>
        public static void HydrateAuthorWithEntitlements(IParticipationDocumentAuthor author, bool mandatoryOnly)
        {
            HydrateAuthor(author, mandatoryOnly);
            
            if (!mandatoryOnly)
            {
                // Subject of Care > Participant > Entitlement
                var entitlement1 = BaseCDAModel.CreateEntitlement();
                entitlement1.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement1.Type = EntitlementType.MedicareBenefits;
                entitlement1.ValidityDuration = BaseCDAModel.CreateIntervalHigh(new ISO8601DateTime(DateTime.Today.AddMonths(15)));

                author.Participant.Entitlements = new List<Entitlement> { entitlement1, entitlement1 };

                author.Participant.Qualifications = "Qualifications";
            }
        }

        /// <summary>
        /// Creates and Hydrates an author
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated author</returns>
        public static void HydrateAuthorV2(IParticipationDocumentAuthor author, bool mandatoryOnly)
        {
            // Person With Organisation
            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Document Author > Role
            author.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

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
                 BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
                 BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null)
            };
            person.Organisation.Name = "Good Hospital";
            person.Organisation.Addresses = addressList;

            // Document Author > Participant > Electronic Communication Detail
            var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "authen@globalauthens.com",
                ElectronicCommunicationMedium.Email,
                ElectronicCommunicationUsage.WorkPlace);

            // Document Author > Participant > Person or Organisation or Device > Person > Organisation > ElectronicCommunicationDetails
            person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

            if (!mandatoryOnly)
            {
                // Document Author > Participation Period
                author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(
                    BaseCDAModel.CreateLowHigh(new ISO8601DateTime(DateTime.Now.AddMinutes(-30), ISO8601DateTime.Precision.Second), new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second)));

                person.Organisation.Department = "Surgical Ward";
                person.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;

                name1.GivenNames = new List<string> { "Good" };
                name1.Titles = new List<string> { "Dr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.PreferredNameIndicator };

                name2.GivenNames = new List<string> { "Davey" };
                name2.Titles = new List<string> { "Br" };
                name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

                // Document Author > Participant > Person or Organisation or Device > Person > Organisation > Addresses
                author.Participant.Addresses = addressList;

                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                address1.AustralianAddress.DeliveryPointId = 32568931;
                address1.AddressAbsentIndicator = null;

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "2 Clinician Street" };
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";
                address2.AustralianAddress.DeliveryPointId = 32568931;
                address2.AddressAbsentIndicator = null;

                // Document Author > Participant > Person or Organisation or Device > Person > Organisation > ElectronicCommunicationDetails
                author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> 
                {
                   coms1,
                   coms2 
                };

                // Subject of Care > Participant > Entitlement
                var entitlement = BaseCDAModel.CreateEntitlement();
                entitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "2296818481");
                entitlement.Type = EntitlementType.MedicareBenefits;
                entitlement.ValidityDuration = BaseCDAModel.CreateIntervalHigh(new ISO8601DateTime(DateTime.Today.AddMonths(15), ISO8601DateTime.Precision.Day));

                author.Participant.Entitlements = new List<Entitlement> { entitlement, entitlement };

                author.Participant.Qualifications = "Qualifications";
            }
            else
            {
                // Document Author > Participation Period
                author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second));
            }

            author.Participant.Person = person;       
        }

        /// <summary>
        /// Creates and Hydrates an author
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated author</returns>
        public static void HydrateAuthor(IParticipationDocumentAuthor author, bool mandatoryOnly)
        {
            var person = BaseCDAModel.CreatePersonWithOrganisation();
            
            // Document Author > Role
            author.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthor();

            // Document Author > Participant > Entity Identifier
            person.Identifiers = new List<Identifier>();

            // IHI
            person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101"));
    
//          // Care Agency Employee Identifier
//          person.Identifiers.Add(
//            BaseCDAModel.CreateIdentifier(
//            "Care Agency Employee Identifier",
//             HealthcareIdentifierGeographicArea.LocalClientIdentifier,
//             null,
//             "1.2.36.1.2001.1007.4.9123453453453458",
//             null));
           
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

            // Document Author > Participant > Per-son or Organisation or Device > Person > Employment Detail
            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
                BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null)
            };
            person.Organisation.Name = "Good Hospital";

            person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };
            person.Organisation.Addresses = new List<IAddress> { address1, address2 };

            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second));

            if (!mandatoryOnly)
            {
                person.Organisation.Department = "Surgical Ward";
                person.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;

                name1.GivenNames = new List<string> { "Good" };
                name1.Titles = new List<string> { "Dr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.PreferredNameIndicator };

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
        }

        /// <summary>
        /// Creates and Hydrates a custodian
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Custodian</returns>
        public static void HydrateCustodian(IParticipationCustodian participationCustodian, bool mandatoryOnly)
        {
            var custodian = BaseCDAModel.CreateParticipantCustodian();

            // custodian/assignedCustodian
            participationCustodian.Participant = custodian;

            // custodian/assignedCustodian/representedCustodianOrganization
            custodian.Organisation = BaseCDAModel.CreateOrganisationName();

            // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
            custodian.Organisation.Identifiers = new List<Identifier> { 
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003621231166540") 
                };

            // custodian/assignedCustodian/representedCustodianOrganization/name
            custodian.Organisation.Name = "Burrill Lake Medical Centre";

            if (!mandatoryOnly)
            {
                // custodian/assignedCustodian/representedCustodianOrganization/<Address>
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Custodian Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                address1.AustralianAddress.DeliveryPointId = 32568931;

                custodian.Address = address1;

                // custodian/assignedCustodian/representedCustodianOrganization/<Electronic Communication Detail>
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);
                custodian.ElectronicCommunicationDetail = coms1;
            }
        }

        /// <summary>
        /// Creates and Hydrates a list of recipients
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated list of recipients</returns>
        public static void HydrateRecipient(IParticipationInformationRecipient recipient, RecipientType recipientType, bool mandatoryOnly)
        {
            recipient.Participant = BaseCDAModel.CreateParticipantForInformationRecipient();

            // informationRecipient/@typeCode
            recipient.Participant.RecipientType = recipientType;

            // informationRecipient/intendedRecipient
            recipient.Participant.Person = BaseCDAModel.CreatePerson();

            if (!mandatoryOnly)
            {
                // informationRecipient/intendedRecipient/informationRecipient/<Entity Identifier>
                recipient.Participant.Person.Identifiers = new List<Identifier> { 
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101") 
                };

                // informationRecipient/intendedRecipient/<Address>
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Residential;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                address1.AustralianAddress.DeliveryPointId = 32568931;

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
                address2.InternationalAddress = BaseCDAModel.CreateInternationalAddress();
                address2.InternationalAddress.AddressLine = new List<string> { "1 Overseas Street" };
                address2.InternationalAddress.Country = Country.Albania;
                address2.InternationalAddress.PostCode = "12345";
                address2.InternationalAddress.StateProvince = "Foreign Land";

                var addressList = new List<IAddress> { address1, address2 };

                recipient.Participant.Addresses = addressList;

                // informationRecipient/intendedRecipient/<Electronic Communication Detail>
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);

                var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "authen@globalauthens.com",
                    ElectronicCommunicationMedium.Email,
                    ElectronicCommunicationUsage.WorkPlace);

                recipient.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

                // informationRecipient/intendedRecipient/informationRecipient/<Person Name>

                var name1 = BaseCDAModel.CreatePersonName();
                name1.GivenNames = new List<string> { "Information" };
                name1.FamilyName = "Recipient";
                name1.Titles = new List<string> { "Dr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

                var name2 = BaseCDAModel.CreatePersonName();
                name2.GivenNames = new List<string> { "Information" };
                name2.FamilyName = "Recipient";
                name2.Titles = new List<string> { "Mr" };
                name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

                recipient.Participant.Person.PersonNames = new List<IPersonName> { name1, name2 };

                // informationRecipient/intendedRecipient/receivedOrganization
                recipient.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

                // informationRecipient/intendedRecipient/receivedOrganization/name
                recipient.Participant.Organisation.Name = "Specialist Clinics";

                recipient.Participant.Organisation.Identifiers = new List<Identifier> { 
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") 
                };
            }
        }

        /// <summary>
        /// Creates and Hydrates an Authenticator
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated authenticator</returns>
        public static void HydrateAuthenticator(IParticipationLegalAuthenticator authenticator)
        {
            HydrateAuthenticator(authenticator, true);
        }

        /// <summary>
        /// Creates and Hydrates an Authenticator
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated authenticator</returns>
        public static void HydrateAuthenticator(IParticipationLegalAuthenticator authenticator, bool mandatoryOnly)
        {
            // LegalAuthenticator/assignedEntity
            authenticator.Participant = BaseCDAModel.CreateParticipantForLegalAuthenticator();

            // LegalAuthenticator/time/@value
            authenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second);

            // LegalAuthenticator/assignedEntity/assignedPerson
            authenticator.Participant.Person = BaseCDAModel.CreatePerson();

            // LegalAuthenticator/assignedEntity/<Entity Identifier>
            authenticator.Participant.Person.Identifiers = new List<Identifier> 
            { 
               BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118") 
            };

            //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
            var name1 = BaseCDAModel.CreatePersonName();
            name1.GivenNames = new List<string> { "Good" };
            name1.FamilyName = "Doctor";
            name1.Titles = new List<string> { "Dr" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            authenticator.Participant.Person.PersonNames = new List<IPersonName> { name1 };

            if (!mandatoryOnly)
            {
                // LegalAuthenticator/assignedEntity/code
                authenticator.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

                // LegalAuthenticator/assignedEntity/<Address>
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                address1.AustralianAddress.DeliveryPointId = 32568931;

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.Business;
                address2.InternationalAddress = BaseCDAModel.CreateInternationalAddress();
                address2.InternationalAddress.AddressLine = new List<string> { "1 Overseas Street" };
                address2.InternationalAddress.Country = Country.Albania;
                address2.InternationalAddress.PostCode = "12345";
                address2.InternationalAddress.StateProvince = "Foreign Land";

                var addressList = new List<IAddress> { address1, address2 };

                authenticator.Participant.Addresses = addressList;

                // LegalAuthenticator/assignedEntity/<Electronic Communication Detail>
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);

                var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "authen@globalauthens.com",
                    ElectronicCommunicationMedium.Email,
                    ElectronicCommunicationUsage.WorkPlace);

                authenticator.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> 
                { 
                    coms1, 
                    coms2 
                };

                // LegalAuthenticator/assignedEntity/representedOrganization
                authenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

                // LegalAuthenticator/assignedEntity/representedOrganization/name
                authenticator.Participant.Organisation.Name = "Oz Health Clinic";

                // LegalAuthenticator/assignedEntity/representedOrganization/<Entity Identifier>
                authenticator.Participant.Organisation.Identifiers = new List<Identifier> 
                { 
                    BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.55555", null),
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000001144") 
                };
            }
        }

        #region Diagnostic Investigations Sections

        public static PathologyTestResult CreatePathologyResultsV2(string simpleNarrativePath)
        {
          // Pathology test result
          var pathologyTestResult = BaseCDAModel.CreatePathologyTestResult();

          if (!simpleNarrativePath.IsNullOrEmptyWhitespace()) pathologyTestResult.XPreNarrative = File.ReadAllText(simpleNarrativePath);

          // Observation DateTime
          pathologyTestResult.ObservationDateTime = new ISO8601DateTime(DateTime.Now.AddDays(-2));

          // Overall Test Result Status
          pathologyTestResult.OverallTestResultStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

          var specimenDetail = BaseCDAModel.CreateSpecimenDetail();

         // Collection DateTime
          specimenDetail.CollectionDateTime = new ISO8601DateTime(DateTime.Now.AddDays(-2));

          // Test Specimen Detail
          pathologyTestResult.TestSpecimenDetail = new List<SpecimenDetail> { specimenDetail };         

          // Test Result Name
          pathologyTestResult.TestResultName = BaseCDAModel.CreateCodableText("275711006", CodingSystem.SNOMED, "Serum chemistry test");

          // Test Comment
          pathologyTestResult.TestComment = "Comment";

          return pathologyTestResult;
        }

        public static PathologyTestResult CreatePathologyResults(string simpleNarrativePath)
        {
            // Pathology test result
            var pathologyTestResult = BaseCDAModel.CreatePathologyTestResult();

            if (!simpleNarrativePath.IsNullOrEmptyWhitespace()) pathologyTestResult.XPreNarrative = File.ReadAllText(simpleNarrativePath);

            // Observation DateTime
            pathologyTestResult.ObservationDateTime = new ISO8601DateTime(DateTime.Now.AddDays(-2));

            // Overall Test Result Status
            pathologyTestResult.OverallTestResultStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

            var specimenDetail = BaseCDAModel.CreateSpecimenDetail();

            // Collection DateTime
            specimenDetail.CollectionDateTime = new ISO8601DateTime(DateTime.Now.AddDays(-2));

            // Test Specimen Detail
            pathologyTestResult.TestSpecimenDetail = new List<SpecimenDetail> { specimenDetail };

            // Test Result Name
            pathologyTestResult.TestResultName = BaseCDAModel.CreateCodableText("18719-5", CodingSystem.LOINC, "Chemistry studies (set)");

            // Test Comment
            pathologyTestResult.TestComment = "test";

            return pathologyTestResult;
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
            pathologyTestResult.TestResultName = BaseCDAModel.CreateCodableText("275711006", CodingSystem.SNOMED, "Serum chemistry test");

            // Diagnostic Service
            pathologyTestResult.DiagnosticService = DiagnosticServiceSectionID.Chemistry;

            // Overall Pathology Test Result Status
            pathologyTestResult.OverallTestResultStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

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
                                                                BaseCDAModel.CreateCodableText("85531003", CodingSystem.SNOMED, "Abnormal")
                                                            };

                // Conclusion
                pathologyTestResult.Conclusion = "Test Result Group Conclusion";

                // Test Result Representation
                pathologyTestResult.TestResultRepresentation = BaseCDAModel.CreateEncapsulatedData();
                pathologyTestResult.TestResultRepresentation.ExternalData = BaseCDAModel.CreateExternalData(MediaType.PDF, ResultFileNameAndPath, "Test Result Representation");

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

            pathologyTestResult.TestSpecimenDetail = new List<SpecimenDetail>
            {
                CreateTestSpecimenDetail(mandatorySectionsOnly)
            };

            return pathologyTestResult;
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
            otherTestResult.ReportStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

            // Report ExternalData
            otherTestResult.ReportContent = BaseCDAModel.CreateEncapsulatedData();
            otherTestResult.ReportContent.ExternalData = BaseCDAModel.CreateExternalData(MediaType.PDF, OtherResultFileNameAndPath, "Other File"); ;

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
            otherTestResult.ReportStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

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

            resultGroup.ResultName = BaseCDAModel.CreateCodableText("14682-9", CodingSystem.LOINC, "Serum Creatinine");

            // Individual Pathology Test Result Status
            resultGroup.ResultStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

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
                };

                // Anatomical Location Description
                specimenDetailOne.PhysicalDescription = "Physical Details Description";

                // Physical Details (PHYSICAL PROPERTIES OF AN OBJECT)
                specimenDetailOne.PhysicalDetails = new List<PhysicalDetails> { 
                    BaseCDAModel.CreatePhysicalDetails("6", "ml")
                };

                // Sampling Preconditions
                specimenDetailOne.SamplingPreconditions = BaseCDAModel.CreateCodableText("16985007", CodingSystem.SNOMED, "fasting");

                // Collection Setting
                specimenDetailOne.CollectionSetting = "Ward 1A";

                // Date and Time of Receipt (DateTime Received)
                specimenDetailOne.ReceivedDateTime = new ISO8601DateTime(DateTime.Now);

                // Parent Specimen Identifier
                specimenDetailOne.ParentSpecimenIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.45364", BaseCDAModel.CreateGuid());

                // Container Identifier
                specimenDetailOne.ContainerIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.45364", "CNH45218964");

                // Specimen Identifier
                specimenDetailOne.SpecimenIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.36.45364", BaseCDAModel.CreateGuid());

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

        #endregion

        /// <summary>
        /// Creates and hydrates the 'Imaging Examination Results' section.
        /// </summary>
        /// <param name="name">Name of the imaging examination results.</param>
        /// <returns>A hydrated 'IImagingExaminationResult' object.</returns>
        public static IImagingExaminationResult CreateImagingResults(string name)
        {
          // Imaging results 
          IImagingResult imagingResult1 = BaseCDAModel.CreateImagingResult();
          imagingResult1.Comments = new List<string> { "Femur measured during ultrasound scan.", "Legs measured during ultrasound scan." };
          imagingResult1.ResultName = BaseCDAModel.CreateCodableText("14682-9", CodingSystem.LOINC, "Serum Creatinine");
          imagingResult1.ResultValue = BaseCDAModel.CreateResultValue();
          imagingResult1.ResultValue.ValueAsCodableText = BaseCDAModel.CreateCodableText("371573008", CodingSystem.SNOMED, "Ultrasonography");
          imagingResult1.NormalStatus = HL7ObservationInterpretationNormality.Normal;
          imagingResult1.ResultValueReferenceRangeDetails = new List<ResultValueReferenceRangeDetail>
                                                                  {
                                                                      CreateReferenceRange("260395002", "Normal range", "ml", 17, 13), 
                                                                      CreateReferenceRange("75540009", "High", "ml", 50, 100)
                                                                  };

          // Imaging results 
          IImagingResult imagingResult2 = BaseCDAModel.CreateImagingResult();
          imagingResult2.Comments = new List<string> { "Neck measured during ultrasound scan.", "Neck measured during ultrasound scan." };
          imagingResult2.ResultName = BaseCDAModel.CreateCodableText("241453007", CodingSystem.SNOMED, "US scan of neck");
          imagingResult2.ResultValue = BaseCDAModel.CreateResultValue();
          imagingResult2.ResultValue.ValueAsCodableText = BaseCDAModel.CreateCodableText("52250000", CodingSystem.SNOMED, "X-ray");
          imagingResult2.NormalStatus = HL7ObservationInterpretationNormality.Normal;
          imagingResult2.ResultValueReferenceRangeDetails = new List<ResultValueReferenceRangeDetail>
                                                                  {
                                                                      CreateReferenceRange("260395002", "Normal range", "kg", 15, 18), 
                                                                      CreateReferenceRange("75540009", "High", "kg", 60, 110)
                                                                  };

          // Image Details
          IImageDetails imageDetails1 = BaseCDAModel.CreateImageDetails();
          imageDetails1.DateTime = new ISO8601DateTime(DateTime.Now);
          imageDetails1.ImageIdentifier = BaseCDAModel.CreateInstanceIdentifier(BaseCDAModel.CreateGuid(), "Image Identifer");
          imageDetails1.SeriesIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.3.1.2.2654654654654564", "Series Identifier");
          imageDetails1.SubjectPosition = "Supine";
          imageDetails1.Image = BaseCDAModel.CreateExternalData(MediaType.JPEG, ImageFileNameAndPath, "Image Details 1 - X-Ray");

          // Image details 
          IImageDetails imageDetails2 = BaseCDAModel.CreateImageDetails();
          imageDetails2.DateTime = new ISO8601DateTime(DateTime.Now);
          imageDetails2.SubjectPosition = "Sublime";
          imageDetails2.SeriesIdentifier = BaseCDAModel.CreateInstanceIdentifier("1.2.3.1.2.2654654654654564", "Series Identifier");
          imageDetails2.Image = BaseCDAModel.CreateExternalData(MediaType.JPEG, ImageFileNameAndPath, "Image Details 2 - X-Ray");

          // Imaging result group 
          IImagingResultGroup imagingResultGroup1 = BaseCDAModel.CreateImagingResultGroup();
          imagingResultGroup1.Results = new List<IImagingResult> { imagingResult1 };
          imagingResultGroup1.ResultGroupName = BaseCDAModel.CreateCodableText("268445003", CodingSystem.SNOMED, "ultrasound scan - obstetric");
          imagingResultGroup1.AnatomicalSite = CreateAnatomicalSite(
                  BaseCDAModel.CreateCodableText("88738008", CodingSystem.SNOMED, "Subcutaneous tissue structure of lateral surface of index finger"),
                  BaseCDAModel.CreateCodableText("7771000", CodingSystem.SNOMED, "Left"),
                  new List<ExternalData>
                  {
                      BaseCDAModel.CreateExternalData(MediaType.JPEG, ImageFileNameAndPath, "Imaging Examination Result - X-Ray 1")
                  }
              ); 

          // Imaging result group 
          IImagingResultGroup imagingResultGroup2 = BaseCDAModel.CreateImagingResultGroup();
          imagingResultGroup2.Results = new List<IImagingResult> { imagingResult2 };
          imagingResultGroup2.ResultGroupName = BaseCDAModel.CreateCodableText("371573008", CodingSystem.SNOMED, "Ultrasonography");
          imagingResultGroup2.AnatomicalSite = CreateAnatomicalSite(
                 BaseCDAModel.CreateCodableText("88738008", CodingSystem.SNOMED, "Subcutaneous tissue structure of lateral surface of index finger"),
                 BaseCDAModel.CreateCodableText("7771000", CodingSystem.SNOMED, "Left")
              ); 

            // Imaging examination result 
            IImagingExaminationResult imagingExaminationResult = BaseCDAModel.CreateImagingExaminationResult();

            // Clinical Information Provided
            imagingExaminationResult.ClinicalInformationProvided = "Patient pregnant. Confirm dates. Estimate from LNMP 18 weeks.";

            // Examination Request Details
            imagingExaminationResult.ExaminationRequestDetails = new List<IImagingExaminationRequest>
                                                                     {
                                                                         CreateImagingExaminationRequest("A/U Obstetric - Dating", imageDetails1),
                                                                         CreateImagingExaminationRequest("A/U Skin - Stuff", new List<IImageDetails> 
                                                                             { 
                                                                               imageDetails2 
                                                                             })
                                                                     };

            // Examination Result Name
            imagingExaminationResult.ExaminationResultName = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMEDCT, "Plain chest X-ray", name, null);

            // Findings
            imagingExaminationResult.Findings = "Normal obstetric ultrasound with foetal biometry indicating getational age of 18W0D.";

            // Modality
            imagingExaminationResult.Modality = BaseCDAModel.CreateCodableText("363680008", CodingSystem.SNOMED, "X-ray");

            // Result Date Time
            imagingExaminationResult.ResultDateTime = new ISO8601DateTime(DateTime.Now.AddDays(-2));

            // Anatomical Site
            imagingExaminationResult.AnatomicalSite = new List<AnatomicalSite> { CreateAnatomicalSite(
                 BaseCDAModel.CreateCodableText("88738008", CodingSystem.SNOMED, "Subcutaneous tissue structure of lateral surface of index finger"),
                 BaseCDAModel.CreateCodableText("7771000", CodingSystem.SNOMED, "Left")
              )};

            // Examination Result Status
            imagingExaminationResult.ExaminationResultStatus = BaseCDAModel.CreateCodableText(ResultStatus.Final);

            // Examination Result Representation
            imagingExaminationResult.ExaminationResultRepresentation = "Result Representation - Rich text representation of the entire result as issued by the diagnostic service";


            imagingExaminationResult.ResultGroup = new List<IImagingResultGroup> { imagingResultGroup1 };
            //imagingExaminationResult.ResultGroup = new List<IImagingResultGroup> { imagingResultGroup1, imagingResultGroup2 };

            return imagingExaminationResult;
        }

        /// Creates and Hydrates an atomicalSite
        /// </summary>
        /// <returns>AnatomicalSite</returns>
        /// 
        ///  NOTE: Newer IG Guides do not permit the function below because of the following constraint. 
        ///        This function is only here for backwards compatibility 
        /// 
        ///  Each instance of
        /// 
        ///  Anatomical Site (ANATOMICAL LOCATION)
        ///  SHALL contain
        ///  either one instance
        ///  of SPECIFIC
        ///  LOCATION or one instance
        ///  of Anatomical
        ///  Location Description.
        /// 
        private static AnatomicalSite CreateAnatomicalSite(String description, ICodableText nameOfLocation, ICodableText side, List<ExternalData> images)
        {
            var anatomicalSite = BaseCDAModel.CreateAnatomicalSite();
            anatomicalSite.Description = description;
            anatomicalSite.SpecificLocation = BaseCDAModel.CreateAnatomicalLocation();
            anatomicalSite.SpecificLocation.NameOfLocation = nameOfLocation;
            anatomicalSite.SpecificLocation.Side = side;
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


        /// <summary>
        /// Creates and Hydrates a Reference Range
        /// </summary>
        /// <param name="code">The Code</param>
        /// <param name="name">The Name</param>
        /// <param name="units">The Unit</param>
        /// <param name="high">The high value</param>
        /// <param name="low">The Low Quantity</param>
        /// <returns>A Hydrated ResultValueReferenceRangeDetail object</returns>
        private static ResultValueReferenceRangeDetail CreateReferenceRange(String code, String name, String units, Double? high, Double? low)
        {
          var resultValueReferenceRangeDetail = BaseCDAModel.CreateResultValueReferenceRangeDetail();

          resultValueReferenceRangeDetail.Range = BaseCDAModel.CreateQuantityRange(high,low,units);

          resultValueReferenceRangeDetail.ResultValueReferenceRangeMeaning = BaseCDAModel.CreateCodableText(code, CodingSystem.SNOMED, name);

          return resultValueReferenceRangeDetail;
        }

        /// <summary>
        /// Creates and hydrates a 'IImagingExaminationRequest' object.
        /// </summary>
        /// <param name="examinationRequestName">Name of the examination request.</param>
        /// <param name="imageDetails">Image details.</param>
        /// <returns>A hydrated 'IImagingExaminationRequest' object</returns>
        private static IImagingExaminationRequest CreateImagingExaminationRequest(String examinationRequestName, IImageDetails imageDetails)
        {
          return CreateImagingExaminationRequest(examinationRequestName, new List<IImageDetails> { imageDetails });
        }


        /// <summary>
        /// Creates and hydrates an 'IImagingExaminationRequest' object.
        /// </summary>
        /// <param name="examinationRequestName">Name of the examination request.</param>
        /// <param name="imageDetails">List of 'ImageDetails'.</param>
        /// <returns>A hydrated 'IImagingExaminationRequest' object.</returns>
        private static IImagingExaminationRequest CreateImagingExaminationRequest(string examinationRequestName, List<IImageDetails> imageDetails)
        {
          var imagingExaminationRequest = BaseCDAModel.CreateImagingExaminationRequest();
          imagingExaminationRequest.ExaminationRequestedName = new List<String> { examinationRequestName, "another name" };
          imagingExaminationRequest.ReportIdentifier = BaseCDAModel.CreateInstanceIdentifier(BaseCDAModel.CreateGuid(), "3355552BHU-23.3");

          if (imageDetails != null && imageDetails.Any())
          {
            int index = 1;
            foreach (IImageDetails imageDetail in imageDetails)
            {
              imageDetail.ImageViewName = BaseCDAModel.CreateCodableText(index + " X-Ray - " + examinationRequestName);
              index++;
            }

            imagingExaminationRequest.ImageDetails = imageDetails;
          }

          imagingExaminationRequest.StudyIdentifier = imagingExaminationRequest.StudyIdentifier = BaseCDAModel.CreateInstanceIdentifier(BaseCDAModel.CreateGuid(), "Accession Number Group: 0008  Element: 0050");

          return imagingExaminationRequest;
        }

        #endregion

    }
}
