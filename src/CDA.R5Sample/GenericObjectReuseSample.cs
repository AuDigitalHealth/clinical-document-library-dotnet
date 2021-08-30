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
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;

namespace CDA.R5Samples
{
    /// <summary>
    /// Demonstrates the reuse of header objects in the CDA Library 
    /// </summary>
    public class GenericObjectReuseSample
    {
        #region Properties

          public static string OutputFolderPath { get; set; }

          public static String ImageFileNameAndPath
          {
            get
            {
              return OutputFolderPath + @"\x-ray.jpg";
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

            HydrateSubjectofCare(genericSubjectOfCare, false);

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
        /// Creates and Hydrates the Participation Author Healthcare Provider
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationAuthorHealthcareProvider object</returns>
        public static void HydrateAuthorHealthcareProvider(IParticipationAuthorHealthcareProvider author, string organisationName, bool mandatoryOnly)
        {
          // Document Author > Participant
          author.Participant = BaseCDAModel.CreateParticipantForAuthorHealthcareProvider();

          var person = BaseCDAModel.CreatePersonHealthcareProvider();

            author.AuthorParticipationPeriodOrDateTimeAuthored = mandatoryOnly ? 
                BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second)) : 
                BaseCDAModel.CreateParticipationPeriod(BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second), new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second)));

            // Document Author > Role = AddressPurpose.Residential
          author.Role = BaseCDAModel.CreateRole(Occupation.MedicalLaboratoryScientist, CodingSystem.ANZSCORevision1);

          // Document Author > Participant > Person or Organisation or Device > Person > Person Name (Note: 1..* in ACI)
          var name = BaseCDAModel.CreatePersonName();

          if (!mandatoryOnly)
          {
              // Not providing a family name will insert a nullflavor of 'NI'
              name.FamilyName = "Doctor family name";
          }
            
          person.PersonNames = new List<IPersonName> { name, name };

          // Document Author > Participant > Entity Identifier
          person.Identifiers = new List<Identifier>
          {
             BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118")
          };

          person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
          person.Organisation.Name = organisationName;

          person.Organisation.Identifiers = new List<Identifier> { 
              BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
          };

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

          if (!mandatoryOnly)
          {
              // Document Author > Participant > Address (Note: optional in ACI)
              var address1 = BaseCDAModel.CreateAddress();
              address1.AddressPurpose = AddressPurpose.Business;
              address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

              var address2 = BaseCDAModel.CreateAddress();
              address2.AddressPurpose = AddressPurpose.Business;
              address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

              // Document Author > Participant > Addresses
              author.Participant.Addresses = new List<IAddress> { address1, address2 };

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

              person.Organisation.NameUsage = OrganisationNameUsage.Other;

              person.Organisation.Department = "Some department service provider";
              person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(Hl7V3EmployeeJobClass.FullTime);
              person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner, CodingSystem.ANZSCORevision1);
              person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);

              person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
              {
                  coms1, coms2
              };

              person.Organisation.Addresses = new List<IAddress>
              {
                 address1, address2
              };

              // Prescriber > Participant > Entitlement
              var entitlement = BaseCDAModel.CreateEntitlement();

              entitlement.Id = BaseCDAModel.CreateIdentifier("Pharmacy",
                                                               null,
                                                               "1234567892",
                                                               "1.2.36.174030967.1.3.2.1",
                                                               null);

              entitlement.Type = EntitlementType.MedicarePrescriberNumber;
              entitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now.AddYears(1)));

              person.Entitlements = new List<Entitlement> { entitlement, entitlement };

              // Qualifications
              person.Qualifications = "M.B.B.S., F.R.A.C.S.";
          }

          author.Participant.Person = person;
        }

        /// <summary>
        /// Creates and Hydrates a IParticipationRequester
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>IParticipationRequester</returns>
        public static IParticipationRequester CreateRequester(Boolean mandatoryOnly)
        {
            // Receiving Laboratory
            var requester = BaseCDAModel.CreateRequester();

            // Document Requester> Participant
            requester.Participant = BaseCDAModel.CreateParticipantForRequester();

            var person = BaseCDAModel.CreatePersonWithOrganisation();

            // Participation Period
            requester.ParticipationEndTime = new ISO8601DateTime(DateTime.Now);

            // Document Requester> Role
            requester.Role = BaseCDAModel.CreateRole(Occupation.MedicalLaboratoryScientist, CodingSystem.ANZSCORevision1);

            // Document Requester> Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Requester";

            person.PersonNames = new List<IPersonName> { name1 };

            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Name = "Requester";

            if (!mandatoryOnly)
            {
                // Null or empty provides the null flavor for asEmployment
                person.Organisation.Name = "Requester Organisation Name";

                person.PersonNames.Add(name1);

                // Participant > Entitlement
                var entitlement = BaseCDAModel.CreateEntitlement();
                var code = BaseCDAModel.CreateCodableText("11", CodingSystem.NCTISEntitlementTypeValues, "Pharmacy", null, null);
                entitlement.Id = BaseCDAModel.CreateIdentifier("Pharmacy",
                                                               null,
                                                               "1234567892",
                                                               "1.2.36.174030967.1.3.2.1",
                                                               code);

                entitlement.Type = EntitlementType.MedicarePharmacyApprovalNumber;
                entitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));

                requester.Participant.Entitlements = new List<Entitlement> { entitlement, entitlement };

                // Document Requester> Participant > Electronic Communication Detail
                var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "0345754566",
                    ElectronicCommunicationMedium.Telephone,
                    ElectronicCommunicationUsage.WorkPlace);
                var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                    "authen@globalauthens.com",
                    ElectronicCommunicationMedium.Email,
                    ElectronicCommunicationUsage.WorkPlace);

                requester.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

                // Document Requester> Participant > Address
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.Business;
                address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var addressList = new List<IAddress> { address1, address2 };

                requester.Participant.Addresses = addressList;

                person.Organisation.NameUsage = OrganisationNameUsage.Other;
                person.Organisation.Department = "Some department requester";
                person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(Hl7V3EmployeeJobClass.FullTime);
                person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner, CodingSystem.ANZSCORevision1);
                person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText("Manager");

                person.Identifiers = new List<Identifier>
                {
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118"),
                };

                person.Organisation.Identifiers = new List<Identifier>
                {
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
                    BaseCDAModel.CreateIdentifier("Test Authority", null, null, "2.999.12345678907", null)
                };

                name1.GivenNames = new List<string> {"Fitun"};
                name1.Titles = new List<string> {"Healthy"};
                name1.NameUsages = new List<NameUsage> {NameUsage.Legal};

                address1.AustralianAddress.UnstructuredAddressLines = new List<string> {"1 Clinician Street"};
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> {"2 Clinician Street"};
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";

                // Qualifications
                requester.Participant.Qualifications = "FRACGP";
            }

            requester.Participant.Person = person;

            return requester;
        }

        /// <summary>
        /// Creates and Hydrates the Author Authoring Device 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated AuthorAuthoringDevice object</returns>
        public static void HydrateAuthorDevice(AuthorAuthoringDevice author, Boolean mandatorySectionsOnly)
        {
          // Date Time Authored
          author.DateTimeAuthored = new ISO8601DateTime(DateTime.Now);

          // Document Author > Role
          author.Role = BaseCDAModel.CreateRole(NullFlavour.NotApplicable);

          // Document Author > Software Name
          author.SoftwareName = "PCEHR National Repository";

          // Document Author > Participant > Entity Identifier
          author.Identifiers = new List<Identifier>
                                   {
                                       BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.PAID, "8003640001000036"),
                                       BaseCDAModel.CreateIdentifier("Test Authority", null, null, "2.999.1234567890", null)
                                   };
        }

        /// <summary>
        /// Creates and Hydrates the Participation Author Non Healthcare Provider
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationAuthorNonHealthcareProvider object</returns>
        public static IParticipationAuthorPerson HydrateAuthorNonHealthcareProvider(IParticipationAuthorPerson author, bool mandatoryOnly)
        {
            var person = BaseCDAModel.CreatePersonNonHealthcareProvider();

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthorPerson();

            // Author Role
            author.Role = BaseCDAModel.CreateRole(RoleCodeAndRoleClassCodes.Self);

            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second));

            // Document Author > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Healthy";

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Wong";

            person.PersonNames = new List<IPersonName> { name1, name2 };

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

            if (!mandatoryOnly)
            {
                name1.GivenNames = new List<string> { "Fitun" };
                name1.Titles = new List<string> { "Dr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

                name2.GivenNames = new List<string> { "Davey" };
                name2.Titles = new List<string> { "Brother" };
                name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

                // Document Author > Participant > Address
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.Business;
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
            }

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

            author.Participant.Person = person;

            return author;
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
            author.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, CodingSystem.ANZSCORevision1);

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthor();

            // Document Author > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118") 
            };

            // Document Author > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();
            address1.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
            address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
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

            // Document Author > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Smith";

            person.PersonNames = new List<IPersonName> { name1 };

            // Document Author > Participant > Per-son or Organisation or Device > Person > Employment Detail
            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
                BaseCDAModel.CreateIdentifier("Test Authority", null, null, "2.999.1234567890", null)
            };

            // Organisation Name
            person.Organisation.Name = "Good Hospital";

            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second));

            if (!mandatoryOnly)
            {
                person.Organisation.Department = "Surgical Ward";
                person.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;

                name1.GivenNames = new List<string> { "Fitun" };
                name1.Titles = new List<string> { "Dr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                address1.AddressAbsentIndicator = null;

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "2 Clinician Street" };
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";
                address2.AddressAbsentIndicator = null;
            }

            author.Participant.Person = person;
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
        public static void HydrateSubjectofCare(IParticipationSubjectOfCare subjectOfCare, bool mandatoryOnly)
        {
            var participant = BaseCDAModel.CreateParticipantForSubjectOfCare();

            // Subject of Care > Participant > Person or Organisation or Device > Person
            var person = BaseCDAModel.CreatePersonForSubjectOfCare();

            // Subject of Care > Participant > Address
            var address = BaseCDAModel.CreateAddress();
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            participant.Addresses = new List<IAddress> { address, address };
            //address.AddressAbsentIndicator = AddressAbsentIndicator.NotIndicated;
            //participant.Addresses = new List<IAddress> { address };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
            person.Gender = Gender.Male;

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Date of Birth Detail > 
            // Date of Birth
            person.DateOfBirth = new ISO8601DateTime(DateTime.Now.AddYears(-57));

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Indigenous Status
            person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;

            // Subject of Care > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> 
            { 
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003608666701594"),
                BaseCDAModel.CreateMedicalRecordNumber("123456", "1.2.3.4", "Croydon GP Centre"),

                // NOTE : ONLY 11 digit Individual Medicare Card Number's is permitted in the Entity Identifier
                BaseCDAModel.CreateIndividualMedicareNumber("59501704511"),
            };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Harding";

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Harding";

            person.PersonNames = new List<IPersonName> { name1, name2 };

            if (!mandatoryOnly)
            {
                address.AddressPurpose = AddressPurpose.Residential;

                name1.GivenNames = new List<string> { "Frank", "Troy" };
                name1.Titles = new List<string> { "Mr" };
                name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

                name2.GivenNames = new List<string> { "Frank", "Tobie" };
                name2.Titles = new List<string> { "Mr" };
                name2.NameUsages = new List<NameUsage> { NameUsage.Legal };

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

                address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address.AustralianAddress.State = AustralianState.NSW;
                address.AustralianAddress.PostCode = "5555";

                person.DateOfBirthCalculatedFromAge = true;
                person.DateOfBirthAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
                person.Age = 54;
                person.AgeUnitOfMeasure = AgeUnitOfMeasure.Year;

                person.AgeAccuracyIndicator = true;
                person.BirthPlurality = 3;
                person.BirthOrder = 2;

                // DateOfDeath & DateOfDeathAccuracyIndicator is not permitted in ACI so set to null in AdvanceCareInformationSample.cs.
                person.DateOfDeath = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day);
                person.DateOfDeathAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
                person.SourceOfDeathNotification = SourceOfDeathNotification.Relative;

                person.CountryOfBirth = Country.Australia;
                person.StateOfBirth = AustralianState.QLD;

                var mothersOriginalFamilyName = BaseCDAModel.CreatePersonName();
                mothersOriginalFamilyName.FamilyName = "Jones";

                // Subject of Care > Participant > Person or Organisation or Device > Person > Mothers Original Family Name
                person.MothersOriginalFamilyName = mothersOriginalFamilyName;
                
                // Subject of Care > Participant > Entitlement
                var entitlement1 = BaseCDAModel.CreateEntitlement();
                entitlement1.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement1.Type = EntitlementType.MedicareBenefits;
                entitlement1.ValidityDuration = BaseCDAModel.CreateHigh(new ISO8601DateTime(DateTime.Now.AddMonths(15), ISO8601DateTime.Precision.Day));

                var entitlement2 = BaseCDAModel.CreateEntitlement();
                entitlement2.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement2.Type = EntitlementType.MedicareBenefits;
                entitlement2.ValidityDuration = BaseCDAModel.CreateHigh(new ISO8601DateTime(DateTime.Now.AddMonths(15), ISO8601DateTime.Precision.Day));

                participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };

            } else
            {
              address.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
            }

            participant.Person = person;
            subjectOfCare.Participant = participant;
        }
        
        /// <summary>
        /// Creates and Hydrates a custodian
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Custodian</returns>
        public static void HydrateCustodian(IParticipationCustodian participationCustodian, string organisationName, bool mandatoryOnly)
        {
            var custodian = BaseCDAModel.CreateParticipantCustodian();

            // custodian/assignedCustodian
            participationCustodian.Participant = custodian;

            // custodian/assignedCustodian/representedCustodianOrganization
            custodian.Organisation = BaseCDAModel.CreateOrganisationName();

            // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
            custodian.Organisation.Identifiers = new List<Identifier> { 
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.PAIO, "8003640001000036") 
                };

            // custodian/assignedCustodian/representedCustodianOrganization/name
            custodian.Organisation.Name = organisationName;

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
        /// Creates and Hydrates a custodian
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Custodian</returns>
        public static void HydrateCustodian(IParticipationCustodian participationCustodian, string organisationName,string hpio, bool mandatoryOnly)
        {
            var custodian = BaseCDAModel.CreateParticipantCustodian();

            // custodian/assignedCustodian
            participationCustodian.Participant = custodian;

            // custodian/assignedCustodian/representedCustodianOrganization
            custodian.Organisation = BaseCDAModel.CreateOrganisationName();

            // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
            custodian.Organisation.Identifiers = new List<Identifier> {
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, hpio)
                };

            // custodian/assignedCustodian/representedCustodianOrganization/name
            custodian.Organisation.Name = organisationName;

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
                    BaseCDAModel.CreateIdentifier("Test Authority", null, null, "2.999.1234567890", null),
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003614444567893") 
                };

                // informationRecipient/intendedRecipient/<Address>
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";

                var address2 = BaseCDAModel.CreateAddress();
                address2.AddressPurpose = AddressPurpose.Business;
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
                    BaseCDAModel.CreateIdentifier("Test Authority", null, null, "2.999.1234567890", null),
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003621231167886") 
                };
            }
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

            // LegalAuthenticator/assignedEntity/assignedPerson
            authenticator.Participant.Person = BaseCDAModel.CreatePerson();

            // LegalAuthenticator/time/@value
            authenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second);

            //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
            var name1 = BaseCDAModel.CreatePersonName();
            name1.GivenNames = new List<string> { "Fitun" };
            name1.FamilyName = "Healthy";
            name1.Titles = new List<string> { "Dr" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            authenticator.Participant.Person.PersonNames = new List<IPersonName> { name1 };

            // LegalAuthenticator/assignedEntity/<Entity Identifier>
            authenticator.Participant.Person.Identifiers = new List<Identifier> 
            { 
               BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118") 
            };

            if (!mandatoryOnly)
            {
                // LegalAuthenticator/assignedEntity/code
                authenticator.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, CodingSystem.ANZSCORevision1);

                // LegalAuthenticator/assignedEntity/<Address>
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Business;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";

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
                authenticator.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

                // LegalAuthenticator/assignedEntity/representedOrganization
                authenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

                // LegalAuthenticator/assignedEntity/representedOrganization/name
                authenticator.Participant.Organisation.Name = "Oz Health Clinic";

                // LegalAuthenticator/assignedEntity/representedOrganization/<Entity Identifier>
                authenticator.Participant.Organisation.Identifiers = new List<Identifier> 
                { 
                    BaseCDAModel.CreateIdentifier("Test Authority", null, null, "2.999.1234567890", null),
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000001144") 
                };
            }
        }
    }
}
