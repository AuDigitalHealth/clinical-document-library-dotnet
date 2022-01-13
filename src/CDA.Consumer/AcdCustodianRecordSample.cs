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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how an Advance Care Directive Custodian Record CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// AcdCustodianRecord class, and then populated with data as appropriate. The three sections that need to be
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
    public class AcdCustodianRecordSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\AcdCustodianRecord.xml";
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
        public XmlDocument MinPopulatedAcdCustodianRecordSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var acdCustodianRecord = PopulateAcdCustodianRecord(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateAcdCustodianRecord method 
                xmlDoc = CDAGenerator.GenerateAcdCustodianRecord(acdCustodianRecord);

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
        public XmlDocument MaxPopulatedAcdCustodianRecordSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var acdCustodianRecord = PopulateAcdCustodianRecord(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateAcdCustodianRecord method 
                xmlDoc = CDAGenerator.GenerateAcdCustodianRecord(acdCustodianRecord);

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
        /// This method populates an acdCustodianRecord model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>AcdCustodianRecord</returns>
        public static AcdCustodianRecord PopulateAcdCustodianRecord(Boolean mandatorySectionsOnly)
        {
            var acdCustodianRecord = AcdCustodianRecord.CreateAcdCustodianRecord();

            // Include Logo
            acdCustodianRecord.IncludeLogo = true;
            acdCustodianRecord.LogoPath = OutputFolderPath;

            // Set Creation Time
            acdCustodianRecord.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = AcdCustodianRecord.CreateCDAContext();
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

            acdCustodianRecord.CDAContext = cdaContext;
            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            acdCustodianRecord.SCSContext = AcdCustodianRecord.CreateSCSContext();

            acdCustodianRecord.SCSContext.Author = BaseCDAModel.CreateAuthor();
            GenericObjectReuseSample.HydrateAuthor(acdCustodianRecord.SCSContext.Author, mandatorySectionsOnly);

            // If only mandatory sections are required, then remove person employment
            if (mandatorySectionsOnly)
            {
                acdCustodianRecord.SCSContext.Author.Participant.Person.Organisation = null;
            }

            acdCustodianRecord.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(acdCustodianRecord.SCSContext.SubjectOfCare, mandatorySectionsOnly, false);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            acdCustodianRecord.SCSContent = AcdCustodianRecord.CreateSCSContent();

            // ACD Custodian Record
            acdCustodianRecord.SCSContent.AcdCustodians = CreateAcdCustodians(mandatorySectionsOnly);

            #endregion

            return acdCustodianRecord;
        }

        /// <summary>
        /// Creates the patient nominated contacts section.
        /// </summary>
        /// <returns></returns>
        private static IList<IParticipationAcdCustodian> CreateAcdCustodians(bool mandatoryOnly)
        {
            IParticipationAcdCustodian contact1 = CreateAcdCustodianPerson(
                "UsualGP", "Person", "8003610000021101", "1 Clinician Street", "0345754566", mandatoryOnly);

            IParticipationAcdCustodian contact2 = CreateAcdCustodianPerson(
                "John", "Doe", "8003610000021101", "2 Clinician Street", "0345754888", mandatoryOnly);

            IParticipationAcdCustodian contact3 = CreateAcdCustodianOrganisation(
                "Some Hospital", "Ward 1F", "8003620000021100", "1 Test Street", "0345754811", mandatoryOnly);

            return new[] { contact1, contact2, contact3 };
        }

        private static IParticipationAcdCustodian CreateAcdCustodianOrganisation(string name, string department, string id,
            string addressLine, string phone, bool mandatoryOnly)
        {
            var custodianParticipation = AcdCustodianRecord.CreateParticipationAcdCustodian();

            custodianParticipation.ParticipationPeriod = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now.AddDays(-50)),
                new ISO8601DateTime(DateTime.Now));

            custodianParticipation.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            custodianParticipation.Participant = AcdCustodianRecord.CreateAcdCustodian();

            IOrganisation organisation = BaseCDAModel.CreateOrganisation();
            organisation.Name = name;
            organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, id) };

            custodianParticipation.Participant.Organisation = organisation;

            if (!mandatoryOnly)
            {
                organisation.Department = department;
                organisation.NameUsage = OrganisationNameUsage.EnterpriseName;

                // Address
                IAddressAustralian address1 = BaseCDAModel.CreateAddress();
                address1.AddressPurpose = AddressPurpose.Residential;
                address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
                address1.AustralianAddress.UnstructuredAddressLines = new List<string> { addressLine };
                address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address1.AustralianAddress.State = AustralianState.QLD;
                address1.AustralianAddress.PostCode = "5555";
                custodianParticipation.Participant.Addresses = new List<IAddressAustralian> { address1 };

                // Communication
                var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail
                    (
                       phone,
                       ElectronicCommunicationMedium.Telephone,
                       ElectronicCommunicationUsage.WorkPlace
                    );
                custodianParticipation.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };
            }

            return custodianParticipation;
        }


        /// <summary>
        /// Creates a patient nominated contact.
        /// </summary>
        /// <param name="givenName">Given name</param>
        /// <param name="familyName">Family name</param>
        /// <param name="hpii">Hpii</param>
        /// <param name="addressLine">Address line</param>
        /// <param name="phone">Phone</param>
        /// <param name="mandatoryOnly">Show mandatory objects</param>
        /// <returns>IParticipationAcdCustodian</returns>
        private static IParticipationAcdCustodian CreateAcdCustodianPerson(
            string givenName, string familyName, string hpii, string addressLine, string phone, bool mandatoryOnly)
        {
            IParticipationAcdCustodian paticipation = AcdCustodianRecord.CreateParticipationAcdCustodian();

            paticipation.ParticipationPeriod = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now.AddDays(-50)),
                new ISO8601DateTime(DateTime.Now));

            paticipation.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            paticipation.Participant = AcdCustodianRecord.CreateAcdCustodian();

            // Name
            var person = BaseCDAModel.CreatePersonConsumer();
            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { givenName };
            personName.FamilyName = familyName;
            personName.Titles = new List<string> { "Dr" };
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };
            person.PersonNames = new List<IPersonName> { personName };

            paticipation.Participant.Person = person;

            // Address
            IAddressAustralian address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { addressLine };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";

            paticipation.Participant.Addresses = new List<IAddressAustralian> { address };

            // Identifiers
            paticipation.Participant.Person.Identifiers = new List<Identifier>
                                  {
                                      BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, hpii)
                                  };


            if (!mandatoryOnly)
            {
                person.DateOfBirth = new ISO8601DateTime(DateTime.Now);
                person.Gender = Gender.Male;

                // Communication
                var electronicCommunicationDetail = AcdCustodianRecord.CreateElectronicCommunicationDetail
                    (
                       phone,
                       ElectronicCommunicationMedium.Telephone,
                       ElectronicCommunicationUsage.WorkPlace
                    );
                paticipation.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> 
                                { 
                                    electronicCommunicationDetail 
                                };

                person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
                person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000021100") };
                person.Organisation.Name = "Super Healthy Hospital";
                person.Organisation.NameUsage = OrganisationNameUsage.EnterpriseName;
                person.Organisation.Department = "Endocrinology";

                person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(Hl7V3EmployeeJobClass.PartTime);
                person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
                person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "SMO", null);
            }

            return paticipation;
        }

        #endregion
    }
}
