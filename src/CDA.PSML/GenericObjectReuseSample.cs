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

namespace CDA.PSML
{
    /// <summary>
    /// Demonstrates the reuse of header objects in the CDA Library 
    /// </summary>
    public class GenericObjectReuseSample
    {
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

            participant.UniqueIdentifier = Guid.NewGuid();

            // Subject of Care > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Grant";
            name1.GivenNames = new List<string> { "Sally", "Wally" };
            name1.Titles = new List<string> { "Miss" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            person.PersonNames = new List<IPersonName> { name1 };

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
            person.Gender = Gender.Female;

            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Date of Birth Detail > 
            // Date of Birth
            person.DateOfBirth = new ISO8601DateTime(DateTime.Now.AddYears(-57));

            // Subject of Care > Participant > Entity Identifier
            person.Identifiers = new List<Identifier>
                {
                     BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003604444567894")
                };

            var address1 = BaseCDAModel.CreateAddress();

            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address1.AustralianAddress.State = AustralianState.NSW;
            address1.AustralianAddress.PostCode = "5555";
            address1.AustralianAddress.DeliveryPointId = 32568931;

            participant.Addresses = new List<IAddress> { address1 };

            person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;

            if (!mandatoryOnly)
            {
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

                participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };



                person.DateOfBirthCalculatedFromAge = true;
                person.DateOfBirthAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
                person.Age = 54;
                person.AgeUnitOfMeasure = AgeUnitOfMeasure.Year;

                person.AgeAccuracyIndicator = true;
                person.BirthPlurality = 3;
                person.BirthOrder = 2;

                // Subject of Care > Participant > Entitlement
                var entitlement1 = BaseCDAModel.CreateEntitlement();
                entitlement1.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement1.Type = EntitlementType.MedicareBenefits;
                entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                var entitlement2 = BaseCDAModel.CreateEntitlement();
                entitlement2.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement2.Type = EntitlementType.MedicareBenefits;
                entitlement2.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };

            }

            participant.Person = person;
            subjectOfCare.Participant = participant;
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
                    BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.6677", null),
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003614444567893")
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
                name1.Titles = new List<string> { "Doctor" };
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
                    BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.6677", null),
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

            // LegalAuthenticator/time/@value
            authenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Minute);

            // LegalAuthenticator/assignedEntity/assignedPerson
            authenticator.Participant.Person = BaseCDAModel.CreatePerson();

            var localAuthorityCode = BaseCDAModel.CreateCodableText("EI", CodingSystem.HL7IdentifierType, null, null, null);

            // LegalAuthenticator/assignedEntity/<Entity Identifier>
            authenticator.Participant.Person.Identifiers = new List<Identifier>
                {
                  BaseCDAModel.CreateIdentifier("LocalAuthority", null, "66666", "1.2.3.4.5", localAuthorityCode),
                  BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118")
                };

            //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
            var name1 = BaseCDAModel.CreatePersonName();
            name1.GivenNames = new List<string> { "Good" };
            name1.FamilyName = "Doctor";
            name1.Titles = new List<string> { "Doctor" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            authenticator.Participant.Person.PersonNames = new List<IPersonName> { name1 };

            if (!mandatoryOnly)
            {
                // LegalAuthenticator/assignedEntity/code
                authenticator.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

                // LegalAuthenticator/assignedEntity/<Address>
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
                    BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.55555", null),
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000001144")
                };
            }
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
            custodian.Organisation.Name = "Burrill Lake Medical Centre";
            // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
            custodian.Organisation.Identifiers = new List<Identifier> {
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789")
            };


            if (!mandatoryOnly)
            {

                // custodian/assignedCustodian/representedCustodianOrganization/name


                // custodian/assignedCustodian/representedCustodianOrganization/<Address>
                var address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Residential;
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
        /// Creates and Hydrates an IParticipationHealthcareFacility
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthcareFacility</returns>
        public static void HydrateHealthcareFacility(IParticipationHealthcareFacility participation, bool mandatoryOnly)
        {

            participation.ParticipationPeriod = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));

            participation.Participant = BaseCDAModel.CreateParticipantForHealthcareFacility();
            participation.Participant.Organisation = BaseCDAModel.CreateOrganisation();

            participation.Role = BaseCDAModel.CreateRole(HealthcareFacilityTypeCodes.ChildCareServices);

            // HealthcareFacility > Participant > Entity Identifier
            participation.Participant.Organisation.Identifiers = new List<Identifier> {
                    BaseCDAModel.CreateIdentifier("HealthcareFacility", null, null, "1.2.3.4.5.66666", null),
                    BaseCDAModel.CreateHealthIdentifier (HealthIdentifierType.HPIO, "8003620833333789")
                };

            // Organisation Name
            participation.Participant.Organisation.Name = "West End Healthiness";

            // HealthcareFacility > Address
            var address1 = BaseCDAModel.CreateAddress();
            address1.AddressPurpose = AddressPurpose.Business;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address1.AustralianAddress.State = AustralianState.QLD;
            address1.AustralianAddress.PostCode = "5555";
            address1.AustralianAddress.DeliveryPointId = 32568931;

            participation.Participant.Addresses = new List<IAddress> { address1 };

            if (!mandatoryOnly)
            {
                // Organisation Continued
                participation.Participant.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;
                participation.Participant.Organisation.Department = "General Health";

                participation.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
                {
                    BaseCDAModel.CreateElectronicCommunicationDetail("0712341234", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace),
                    BaseCDAModel.CreateElectronicCommunicationDetail("0712341236", ElectronicCommunicationMedium.Fax, ElectronicCommunicationUsage.WorkPlace),
                };
            }
        }

        /// <summary>
        /// Creates and Hydrates the Participation Author Healthcare Provider
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationAuthorHealthcareProvider object</returns>
        public static void HydrateAuthorHealthcareProvider(IParticipationAuthorHealthcareProvider author, bool mandatoryOnly)
        {
            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now));

            // Document Author > Role
            author.Role = BaseCDAModel.CreateRole(Occupation.MedicalLaboratoryScientist);

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthorHealthcareProvider();

            var person = BaseCDAModel.CreatePersonHealthcareProvider();

            // Document Author > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Doctor";

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Wong";

            person.PersonNames = new List<IPersonName> { name1, name2 };

            // Document Author > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();
            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.Business;
            address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var addressList = new List<IAddress> { address1, address2 };
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

            author.Participant.Addresses = addressList;


            // Document Author > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> { 
              //BaseCDAModel.CreateIdentifier("AuthorHealthcareProvider", null, null, "1.2.3.4.5.66666", null),
              BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118"),
          };


            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Name = "Hay Bill Hospital";
            person.Organisation.NameUsage = OrganisationNameUsage.Other;

            person.Organisation.Identifiers = new List<Identifier> {
              BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
              //BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null)
          };

            person.Organisation.Department = "Some department service provider";
            person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(null, null, null, "Casual", null);
            person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);

            if (!mandatoryOnly)
            {
                name1.GivenNames = new List<string> { "Good" };
                name1.Titles = new List<string> { "Doctor" };
                name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

                name2.GivenNames = new List<string> { "Davey" };
                name2.Titles = new List<string> { "Brother" };
                name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };




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


                // Prescriber > Participant > Entitlement
                var entitlement1 = BaseCDAModel.CreateEntitlement();

                var code = BaseCDAModel.CreateCodableText("11", CodingSystem.NCTISEntitlementTypeValues, "Medicare Pharmacy Approval Number", null, null);

                entitlement1.Id = BaseCDAModel.CreateIdentifier("pharmacy",
                                                                      null,
                                                                      "1234567892",
                                                                      "1.2.36.174030967.1.3.2.1",
                                                                      code);

                entitlement1.Type = EntitlementType.MedicarePharmacyApprovalNumber;
                entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                person.Entitlements = new List<Entitlement> { entitlement1, entitlement1 };

                // Qualifications
                person.Qualifications = "M.B.B.S., F.R.A.C.S.";
            }

            author.Participant.Person = person;
        }

        /// <summary>
        /// Creates and Hydrates the Participation Author Non Healthcare Provider
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationAuthorNonHealthcareProvider object</returns>
        public static IParticipationAuthorPerson HydrateAuthorNonHealthcareProvider(IParticipationAuthorPerson author, bool mandatoryIdentifier, bool mandatoryOnly)
        {
            var person = BaseCDAModel.CreatePersonNonHealthcareProvider();

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthorPerson();

            // Author Role
            author.Role = BaseCDAModel.CreateRole(RoleCodeAndRoleClassCodes.Self);

            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now));

            // Document Author > Participant > Person or Organisation or Device > Person > Person Name
            var name1 = BaseCDAModel.CreatePersonName();
            name1.FamilyName = "Doctor";

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Wong";

            person.PersonNames = new List<IPersonName> { name1, name2 };

            if (mandatoryIdentifier || !mandatoryOnly)
            {
                // Document Author > Participant > Entity Identifier
                person.Identifiers = new List<Identifier> {
              BaseCDAModel.CreateIdentifier("AuthorNonHealthcareProvider", null, null, "1.2.3.4.5.66666", null),
              BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118"),
           };
            }

            if (!mandatoryOnly)
            {

                name1.GivenNames = new List<string> { "Good" };
                name1.Titles = new List<string> { "Doctor" };
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
                address1.AustralianAddress.DeliveryPointId = 32568931;

                address2.AustralianAddress.UnstructuredAddressLines = new List<string> { "2 Clinician Street" };
                address2.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address2.AustralianAddress.State = AustralianState.QLD;
                address2.AustralianAddress.PostCode = "5555";
                address2.AustralianAddress.DeliveryPointId = 32568931;

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

            return author;
        }

        /// <summary>
        /// Creates and Hydrates the Author Authoring Device 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated AuthorAuthoringDevice object</returns>
        public static void HydrateAuthorDevice(AuthorAuthoringDevice author)
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
                                       BaseCDAModel.CreateIdentifier("AuthorDevice", null, null, "1.2.3.4.5.66666", null)
                                   };
        }

        /// <summary>
        /// Creates and Hydrates the Author Authoring Device 
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated AuthorAuthoringDevice object</returns>
        public static AuthorAuthoringDevice CreateAuthorDevice()
        {
            var author = BaseCDAModel.CreateAuthorAuthoringDevice();

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
                                       BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null)
                                   };


            return author;
        }

    }
}

