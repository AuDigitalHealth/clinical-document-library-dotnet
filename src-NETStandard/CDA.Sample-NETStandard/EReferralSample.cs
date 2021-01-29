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

namespace Nehta.VendorLibrary.CDA.SampleNETStandard
{
    /// <summary>
    /// This project is intended to demonstrate how an EReferral CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// EReferral class, and then populated with data as appropriate. The three sections that need to be
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
    public class EReferralSample 
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static string OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\EReferral.xml";
            }
        }

        public static string PitNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PIT.txt";
            }
        }

        public static string StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }
       
        #endregion

        public EReferralSample()
        {
            // Set the Output Folder Path in the Generic Object Reuse Sample
            GenericObjectReuseSample.OutputFolderPath = OutputFolderPath;
        }

        /// <summary>
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateEReferralSample_1A(string fileName)
        {
            XmlDocument xmlDoc;

           var document = PopulateEReferral(true);

           // Hide Administrative Observations Section 
           document.ShowAdministrativeObservationsSection = false;

            document.SCSContent = EReferral.CreateSCSContent();
            document.SCSContent.Referee = CreateReferee(true);

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
                xmlDoc = CDAGenerator.GenerateEReferral(document);

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
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateEReferralSample_1B(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulateEReferral(true);

            document.SCSContent = EReferral.CreateSCSContent();
            document.SCSContent.Referee = CreateReferee(true);

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
                xmlDoc = CDAGenerator.GenerateEReferral(document);

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
        /// This sample populates only the mandatory Sections / Entries
        /// </summary>
        public XmlDocument MinPopulatedEReferralSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var eReferral = PopulateEReferral(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateEReferral method 
                xmlDoc = CDAGenerator.GenerateEReferral(eReferral);

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
        /// each of its optional entries.
        /// </summary>
        public XmlDocument MaxPopulatedEReferralSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var eReferral = PopulateEReferral(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateEReferral method 
                xmlDoc = CDAGenerator.GenerateEReferral(eReferral);

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
        public XmlDocument PopulateEReferralSample_1B_NarrativeExample(string fileName)
        {
            XmlDocument xmlDoc;

            var document = PopulateEReferral(true);

            document.SCSContent = EReferral.CreateSCSContent();
            document.SCSContent.Referee = CreateReferee(true);

            document.IncludeLogo = false;

            // Hide Administrative Observations Section 
            document.ShowAdministrativeObservationsSection = false;

            var narrativeOnlyDocumentList = new List<NarrativeOnlyDocument>();

            var narrativeOnlyDocument = BaseCDAModel.CreateNarrativeOnlyDocument();
            narrativeOnlyDocument.Title = "Title";

            // Build Narrative
            var sdt = new StrucDocText();
            sdt.content = new[]
            {
                new StrucDocContent {br = new[] {""}},
                new StrucDocContent {styleCode = "Bold Underline", Text = new[] {"Text"}},
                new StrucDocContent {Text = new[] {"Dear Mr Jones,"}, br = new[] {""}},
                new StrucDocContent {Text = new[] { "Thank you for seeing this 21 year old male who has had 2 episodes of cholecystitis in the last month.  " }, br = new[] {"", ""} },
                new StrucDocContent {Text = new[] {"He is currently unwell. "}, br = new[] {""}},
                new StrucDocContent {styleCode = "Underline", Text = new[] {"Date:"}, br = new[] {"", ""}},
                new StrucDocContent {Text = new[] {"12-Jan-2012"}},
                new StrucDocContent {styleCode = "Underline", Text = new[] {"Time:"}, br = new[] {""}},
                new StrucDocContent {Text = new[] {"12:00pm"}},
                new StrucDocContent {br = new[] {"", ""}},
            };

            sdt.table = new[]
            {
                new StrucDocTable
                {
                    caption = new StrucDocCaption {Text = new [] {"Reason For Referral"}},
                    tbody = new [] { new StrucDocTbody {tr = new [] { new StrucDocTr { td = new []{ new StrucDocTd { Text = new [] { "Referral reason" }} }} }} }
                },
                new StrucDocTable
                {
                    caption = new StrucDocCaption {Text = new [] {"Medications"}},
                    thead = new StrucDocThead {tr = new [] { new StrucDocTr { td = new []{ new StrucDocTd { Text = new [] { "Medication" }}, new StrucDocTd { Text = new[] { "Directions" } } }} }},
                    tbody = new [] { new StrucDocTbody {tr = new [] { new StrucDocTr { td = new []{ new StrucDocTd { Text = new [] { "paracetamol 500 mg + codeine phosphate 30 mg tablet" } }, new StrucDocTd { Text = new[] { "Dose:1, Frequency: 3 times daily" } } } } }} }
                },
                
            };



            sdt.renderMultiMedia = new[]
                                      {
                                      new StrucDocRenderMultiMedia
                                        {
                                          referencedObject = "efd92158-fea0-4e0b-93d9-e2913a63df8b",
                                          caption = new StrucDocCaption
                                                      {
                                                        Text = new[] {"Report.pdf"}
                                                      }
                                        }
                                    };

            sdt.linkHtml = new[]
                         {
                         new StrucDocLinkHtml
                           {
                             href = "Report.pdf",
                             Text = new[]
                                      {
                                        "Report.pdf"
                                      },
                           }
                       };

            narrativeOnlyDocument.Narrative = sdt;


            // Add One
            narrativeOnlyDocumentList.Add(narrativeOnlyDocument);

            document.SCSContent.NarrativeOnlyDocument = narrativeOnlyDocumentList;

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the document model into the Generate method 
                xmlDoc = CDAGenerator.GenerateEReferral(document);

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
        /// This method populates an eReferral model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>EReferral</returns>
        internal static EReferral PopulateEReferral(Boolean mandatorySectionsOnly)
        {
            var eReferral = EReferral.CreateEReferral();

            // Set Creation Time
            eReferral.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            // Include Logo
            eReferral.IncludeLogo = true;

            // Note:example  Populate ByteArray Logo
            eReferral.LogoByte = BaseCDAModel.FileToByteArray(System.IO.Path.Combine(OutputFolderPath, "Logo.png"));

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = EReferral.CreateCDAContext();
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

                var recipient1 = BaseCDAModel.CreateInformationRecipient();
                var recipient2 = BaseCDAModel.CreateInformationRecipient();

                GenericObjectReuseSample.HydrateRecipient(recipient1, RecipientType.Primary, mandatorySectionsOnly);
                GenericObjectReuseSample.HydrateRecipient(recipient2, RecipientType.Secondary, mandatorySectionsOnly);

                cdaContext.InformationRecipients = new List<IParticipationInformationRecipient> { recipient1, recipient2 };
            }

            eReferral.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            eReferral.SCSContext = EReferral.CreateSCSContext();

            eReferral.SCSContext.Author = BaseCDAModel.CreateAuthor();
            GenericObjectReuseSample.HydrateAuthor(eReferral.SCSContext.Author, mandatorySectionsOnly);

            eReferral.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(eReferral.SCSContext.SubjectOfCare, mandatorySectionsOnly, false);

            if (!mandatorySectionsOnly)
            {
              eReferral.SCSContext.PatientNominatedContacts = CreatePatientNominatedContacts(mandatorySectionsOnly);
            }

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model

            eReferral.SCSContent = EReferral.CreateSCSContent();

            // Referral detail
            eReferral.SCSContent.ReferralReason = "Referral reason";

            // Referral DateTime
            eReferral.SCSContent.ReferralDateTime = new ISO8601DateTime(DateTime.Now);

            // Validity Duration
            eReferral.SCSContent.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));

            // Referee
            eReferral.SCSContent.Referee = CreateReferee(mandatorySectionsOnly);

            // Medications
            eReferral.SCSContent.Medications = CreateMedications(mandatorySectionsOnly);

            // Medical history
            eReferral.SCSContent.MedicalHistory = CreateMedicalHistory(mandatorySectionsOnly);

            // Adverse reactions
            eReferral.SCSContent.AdverseReactions = CreateAdverseReactions(mandatorySectionsOnly);

            if (!mandatorySectionsOnly)
            {
                // UsualGP
                eReferral.SCSContent.UsualGP = CreateUsualGPOrganisation(mandatorySectionsOnly);

                // Diagnostic Investigations
                eReferral.SCSContent.DiagnosticInvestigations = CreateDiagnosticInvestigations(mandatorySectionsOnly);
            }

            #endregion

            return eReferral;
        }

        /// <summary>
        /// Creates the patient nominated contacts section.
        /// </summary>
        /// <returns></returns>
        private static IList<IParticipationPatientNominatedContact> CreatePatientNominatedContacts(Boolean mandatorySectionsOnly)
        {
            IParticipationPatientNominatedContact contact1 = CreatePatientNominatedContactPerson(
                "UsualGP", "Person", "8003610000021101", "1 Clinician Street", "0345754566", mandatorySectionsOnly);

            IParticipationPatientNominatedContact contact2 = CreatePatientNominatedContactPerson(
                "John", "Doe", "8003610000021101", "2 Clinician Street", "0345754888", mandatorySectionsOnly);

            IParticipationPatientNominatedContact contact3 = CreatePatientNominatedContactOrganisation(
                "Some Hospital", "Ward 1F", "8003620000021100", "1 Test Street", "0345754811", mandatorySectionsOnly);

            return new[] { contact1, contact2, contact3 };
        }

        private static IParticipationPatientNominatedContact CreatePatientNominatedContactOrganisation(string name, string department, string id, string addressLine, string phone, Boolean mandatorySectionsOnly)
        {
            IParticipationPatientNominatedContact nominatedContact = EReferral.CreateParticipationPatientNominatedContact();

            nominatedContact.Participant = EReferral.CreatePatientNominatedContact();

            IOrganisation organisation = EReferral.CreateOrganisation();
            organisation.Department = department;
            organisation.Name = name;
            organisation.NameUsage = OrganisationNameUsage.EnterpriseName;
            organisation.Identifiers = new List<Identifier>
                                           {
                                               BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, id)
                                           };

            nominatedContact.Participant.Organisation = organisation;

            // Address
            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Residential;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { addressLine };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            nominatedContact.Participant.Addresses = new List<IAddress> {address};

            // Communication
            var electronicCommunicationDetail = EReferral.CreateElectronicCommunicationDetail
                (
                   phone,
                   ElectronicCommunicationMedium.Telephone,
                   new List<ElectronicCommunicationUsage> { ElectronicCommunicationUsage.WorkPlace, ElectronicCommunicationUsage.Home }
                );

            nominatedContact.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> {electronicCommunicationDetail};

            nominatedContact.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.TemporarilyUnavailable);

            return nominatedContact;
        }


      /// <summary>
      /// Creates a patient nominated contact.
      /// </summary>
      /// <param name="givenName">Given name</param>
      /// <param name="familyName">Family name</param>
      /// <param name="hpii">Hpii</param>
      /// <param name="addressLine">Address line</param>
      /// <param name="phone">Phone</param>
      /// <param name="mandatorySectionsOnly">Populates only mandatory sections only </param>
      /// <returns>IParticipationPatientNominatedContact</returns>
      private static IParticipationPatientNominatedContact CreatePatientNominatedContactPerson(string givenName, string familyName, string hpii, string addressLine, string phone, Boolean mandatorySectionsOnly)
        {
            IParticipationPatientNominatedContact nominatedContact = EReferral.CreateParticipationPatientNominatedContact();

            nominatedContact.Participant = EReferral.CreatePatientNominatedContact();

            // Name
            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { givenName };
            personName.FamilyName = familyName;
            personName.Titles = new List<string> { "Dr" };
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            nominatedContact.Participant.Person = EReferral.CreatePersonPatientNominatedContacts();

            nominatedContact.Participant.Person.PersonNames = new List<IPersonName> { personName };

            // Address
            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { addressLine };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            nominatedContact.Participant.Addresses = new List<IAddress> { address };

            // Communication
            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(phone, ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

            nominatedContact.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            // Identifiers
            nominatedContact.Participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, hpii) };

            // Role
            nominatedContact.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

            return nominatedContact;
        }

        /// <summary>
        /// Creates and Hydrates a UsualGP
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>>A Hydrated UsualGP object</returns>
        private static IParticipationUsualGP CreateUsualGPOrganisation(Boolean mandatorySectionsOnly)
        {
            IParticipationUsualGP usualGP = EReferral.CreateUsualGP();

            var participant = EReferral.CreateParticipantForUsualGP();

            var organisation = BaseCDAModel.CreateOrganisation();
            organisation.Name = "Bay hill hospital (UsualGP Organisation)";
            organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;
            organisation.Department = "Department";

            organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000020052") };
            participant.Organisation = organisation;

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            var addressList = new List<IAddress>{ address };

            participant.Addresses = addressList;

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            usualGP.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

            usualGP.Participant = participant;

            return usualGP;
        }

        /// <summary>
        /// Creates and Hydrates a UsualGP
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>>A Hydrated UsualGP object</returns>
        private static IParticipationUsualGP CreateUsualGPPerson(Boolean mandatorySectionsOnly)
        {
            var usualGP = EReferral.CreateUsualGP();

            var participant = EReferral.CreateParticipantForUsualGP();

            var personName = BaseCDAModel.CreatePersonName();
            personName.GivenNames = new List<string> { "UsualGP" };
            personName.FamilyName = "Person";
            personName.Titles = new List<string> { "Dr" };
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            var person = BaseCDAModel.CreatePersonWithOrganisation();
            person.PersonNames = new List<IPersonName>();
            person.PersonNames.Add(personName);
            person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000021101") };

            participant.Person = person;

            var organisation = BaseCDAModel.CreateEmploymentOrganisation();
            organisation.Name = "Bay hill hospital";
            organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;
            organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000020052") };
            participant.Person.Organisation = organisation;

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            var addressList = new List<IAddress>{ address };

            participant.Addresses = addressList;

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail("0345754566", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };

            usualGP.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.Other);

            usualGP.Participant = participant;

            return usualGP;
        }
        

        /// <summary>
        /// Creates and Hydrates the Medications section for the E-Referral.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Medications object</returns>
        private static IMedicationsEReferral CreateMedications(bool mandatorySectionsOnly)
        {
            var medications = EReferral.CreateMedications();

            if (!mandatorySectionsOnly)
            {
                var medicationList = new List<IMedicationInstruction>();

                var medication1 = EReferral.CreateMedication();
                medication1.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: 3 times daily");

                medication1.Medicine = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMT, "paracetamol 500 mg + codeine phosphate 30 mg tablet");
                medicationList.Add(medication1);

                var medication2 = EReferral.CreateMedication();
                medication2.Directions =BaseCDAModel.CreateStructuredText( NullFlavour.TemporarilyUnavailable);

                medication2.Medicine = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMT, "paracetamol 500 mg + codeine phosphate 30 mg tablet");
                medicationList.Add(medication2);

                medications.MedicationsList = medicationList;
            } 
            else
            {
                medications.ExclusionStatement = EReferral.CreateExclusionStatement();
                medications.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }

            return medications;
        }

        /// <summary>
        /// Creates and Hydrates the medical history section for the E-Referral.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated MedicalHistory object</returns>
        private static IMedicalHistory CreateMedicalHistory(bool mandatorySectionsOnly)
        {
            var medicalHistory = EReferral.CreateMedicalHistory();

            if (!mandatorySectionsOnly)
            {
                // NOTE: This section demonstrates the different combinations of Procedure's, Medical History Item's & diagnosis

                var procedureList = new List<Procedure>();
                var medicalHistoryItems = new List<IMedicalHistoryItem>();
                var diagnosisList = new List<IProblemDiagnosis>();

                // Procedures
                var procedure = BaseCDAModel.CreateProcedure();
                procedure.Comment = "Procedure Comment";
                procedure.ProcedureName = BaseCDAModel.CreateCodableText("301040004", CodingSystem.SNOMED, "Primary closed wire fixation of fracture");
                procedure.ProcedureDateTime = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400)));
                procedureList.Add(procedure);

                var prodcedure2 = BaseCDAModel.CreateProcedure();
                prodcedure2.Comment = "Comment";
                prodcedure2.ProcedureName = BaseCDAModel.CreateCodableText("388544006", CodingSystem.SNOMED, "Crab specific IgE antibody measurement");
                procedureList.Add(prodcedure2);

                // Uncategorised Medical History Items
                var medicalHistoryItem = EReferral.CreateMedicalHistoryItem();
                medicalHistoryItem.DateTimeInterval = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                medicalHistoryItem.ShowOngoingInNarrative = true;
                medicalHistoryItem.ItemDescription = "Uncategorised Medical History item description";
                medicalHistoryItem.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem);

                var medicalHistoryItem1 = EReferral.CreateMedicalHistoryItem();
                medicalHistoryItem1.ShowOngoingInNarrative = true;
                medicalHistoryItem1.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem1.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem1);

                var medicalHistoryItem2 = EReferral.CreateMedicalHistoryItem();
                var ongoingInterval2 = CdaInterval.CreateLowHigh(
                                       new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day),
                                       new ISO8601DateTime(DateTime.Now.AddDays(0), ISO8601DateTime.Precision.Day));
                medicalHistoryItem2.DateTimeInterval = ongoingInterval2;
                medicalHistoryItem2.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem2.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem2);

                var medicalHistoryItem3 = EReferral.CreateMedicalHistoryItem();
                var ongoingInterval3 = CdaInterval.CreateHigh(
                                       new ISO8601DateTime(DateTime.Now.AddDays(200), ISO8601DateTime.Precision.Day));
                medicalHistoryItem3.DateTimeInterval = ongoingInterval3;
                medicalHistoryItem3.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem3.ItemComment = "Item Comment 4";
                medicalHistoryItems.Add(medicalHistoryItem3);

                // Problem Diagnosis
                var diagnosis = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("162311003", CodingSystem.SNOMED, "Heavy head");
                diagnosis.DateOfOnset = new ISO8601DateTime(DateTime.Now.AddYears(-2));
                diagnosis.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Now.AddYears(-1), ISO8601DateTime.Precision.Day);
                diagnosis.Comment = "Solved this";
                diagnosis.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis);

                var diagnosis1 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis1.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("258245003", CodingSystem.SNOMED, "G4 grade");
                diagnosisList.Add(diagnosis1);

                var diagnosis2 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis2.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("396275006", CodingSystem.SNOMED, "Osteoarthritis");
                diagnosisList.Add(diagnosis2);

                var diagnosis3 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis3.Comment = "Diuretic induced";
                diagnosis3.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("43408002", CodingSystem.SNOMED, "Red reflex");
                diagnosisList.Add(diagnosis3);

                var diagnosis4 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis4.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("286114007", CodingSystem.SNOMED, "Does not do dusting");
                diagnosis4.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("27 Feb 2007"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis4);

                var diagnosis5 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis5.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("248515009", CodingSystem.SNOMED, "Lump in lid margin");
                diagnosis5.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Jan 2000"), ISO8601DateTime.Precision.Day);
                diagnosis5.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis5);

                var diagnosis6 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis6.Comment = "Cementless";
                diagnosis6.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("200608005", CodingSystem.SNOMED, "Boil of back");
                diagnosis6.DateOfOnset = new ISO8601DateTime(DateTime.Parse("27 Feb 2007"), ISO8601DateTime.Precision.Day);
                diagnosis6.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis6);

                var diagnosis7 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis7.Comment = "T-score less than -3";
                diagnosis7.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("267032009", CodingSystem.SNOMED, "Tired all the time");
                diagnosis7.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Jan 2007"), ISO8601DateTime.Precision.Day);
                diagnosis7.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis7);

                var diagnosis8 = EReferral.CreateProblemDiagnosis();
                diagnosis8.Comment = "Comment";
                diagnosis8.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("36083008", CodingSystem.SNOMED, "Sick sinus syndrome");
                diagnosis8.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis8.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("01 Sep 2010"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis8);

                var diagnosis9 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis9.Comment = "Diagnosis Comment";
                diagnosis9.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("72940007", CodingSystem.SNOMED, "Acute abscess of areola");
                diagnosis9.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis9.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("01 Sep 2010"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis9);

                var diagnosis10 = BaseCDAModel.CreateProblemDiagnosis();
                diagnosis10.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("57883007", CodingSystem.SNOMED, "Renin test diet", "Renin test diet");

                diagnosis10.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis10.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis10);

                medicalHistory.MedicalHistoryItems = medicalHistoryItems;
                medicalHistory.Procedures = procedureList;
                medicalHistory.ProblemDiagnosis = diagnosisList;
            }

            return medicalHistory;
        }

        /// <summary>
        /// Creates and Hydrates the adverse substance reactions section for E-Referral.
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
                    CreateAdverseReaction(BaseCDAModel.CreateCodableText("86461001", CodingSystem.SNOMED , "Plant diterpene")),
                    CreateAdverseReaction(BaseCDAModel.CreateCodableText("117491007", CodingSystem.SNOMED , "trans-Nonachlor"))
                };

            } else
            {
                adverseReactions.ExclusionStatement = EReferral.CreateExclusionStatement();
                adverseReactions.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }

            return adverseReactions;
        }

        /// <summary>
        /// Creates an adverse reaction.
        /// </summary>
        /// <param name="code">Code for the adverse reaction.</param>
        /// <param name="name">Name of the adverse reaction.</param>
        /// <returns></returns>
        private static Reaction CreateAdverseReaction(ICodableText substanceOrAgent)
        {
            Reaction reaction = EReferral.CreateReaction();

            reaction.SubstanceOrAgent = substanceOrAgent;

            reaction.ReactionEvent = BaseCDAModel.CreateReactionEvent();
            reaction.ReactionEvent.Manifestations = new List<ICodableText>
            {
                BaseCDAModel.CreateCodableText("21909001", CodingSystem.SNOMED, "Fetal viability"),
                BaseCDAModel.CreateCodableText("15296000", CodingSystem.SNOMED, "Sterility")
            };

            reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

            return reaction;
        }

        private static List<RequestedService> CreateRequestedService(Boolean mandatorySectionsOnly)
        {
            var requestedServiceList = new List<RequestedService>();

            // Create Service Provider for a Person
            var requestedServicePerson = EReferral.CreateRequestedService();

            requestedServicePerson.RequestedServiceDescription = BaseCDAModel.CreateCodableText("399208008", CodingSystem.SNOMED, "Plain chest X-ray");
            requestedServicePerson.ServiceBookingStatus = EventTypes.Definition;
            // Create Duration
            requestedServicePerson.SubjectOfCareInstructionDescription = "Subject Of Care Instruction Description";
            requestedServicePerson.RequestedServiceDateTime = new ISO8601DateTime(DateTime.Now.AddDays(4));

            requestedServicePerson.ServiceCommencementWindow = BaseCDAModel.CreateInterval(
                new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day),
                new ISO8601DateTime(DateTime.Now.AddMonths(6), ISO8601DateTime.Precision.Day));

            // Create Person
            requestedServicePerson.ServiceProvider = CreateServiceProviderPerson(mandatorySectionsOnly);

            // Add to list
            requestedServiceList.Add(requestedServicePerson);

            // Create Service Provider for a Organisation
            var requestedServiceOrganisation = EReferral.CreateRequestedService();
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
            serviceProvider.Participant = participant;

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "Dr Jane Anderson";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            participant.Person = BaseCDAModel.CreatePersonHealthcareProvider();
            participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") };

            participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            participant.Person.Organisation.Name = "Bay Hill Hospital (ServiceProviderPerson)";
            participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") }; 

            participant.Person.PersonNames = new List<IPersonName> { personName };

            var electronicCommunicationDetail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var electronicCommunicationDetail2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "04355754566",
                ElectronicCommunicationMedium.Mobile,
                ElectronicCommunicationUsage.WorkPlace);

            participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail2 };

            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street test" };
            address.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address.AustralianAddress.State = AustralianState.QLD;
            address.AustralianAddress.PostCode = "5555";
            address.AustralianAddress.DeliveryPointId = 32568931;

            participant.Addresses = new List<IAddress>{ address, address };

            serviceProvider.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.Other);

            var entitlement = BaseCDAModel.CreateEntitlement();
            entitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.IndividualMedicareCardNumber, "42614955241");
            entitlement.Type = EntitlementType.MedicarePrescriberNumber;
            entitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));
            participant.Person.Entitlements = new List<SCSModel.Common.Entitlement> { entitlement, entitlement };

            participant.Person.Qualifications = "M.B.B.S., F.R.A.C.S.";

            participant.Person.Organisation.Addresses = new List<IAddress> { address, address };
            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail, electronicCommunicationDetail2 };

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
            serviceProvider.Participant.Organisation.Name = "Bay Hill Hospital (ServiceProviderOrganisation)";
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

            serviceProvider.Participant.Addresses = new List<IAddress> { address };

            serviceProvider.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.Other);

            return serviceProvider;
        }

        /// <summary>
        /// Creates and Hydrates a Service Provider Person
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationReferee CreateReferee(bool mandatorySectionsOnly)
        {
            var referee = EReferral.CreateReferee();

            var participant = EReferral.CreateParticipantForReferee();
            referee.Participant = participant;

            var personName = BaseCDAModel.CreatePersonName();
            personName.FamilyName = "Dr Jane Anderson";
            personName.NameUsages = new List<NameUsage> { NameUsage.Legal };

            participant.Person = EReferral.CreatePersonForServiceProvider();
            participant.Person.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003610000001145") };

            participant.Person.Organisation = EReferral.CreateEmploymentOrganisation();
            participant.Person.Organisation.Name = "Bay Hill Hospital (RefereePerson)";
            participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;
            participant.Person.Organisation.Identifiers = new List<Identifier> { BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620000045562") };

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

            participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { electronicCommunicationDetail };
            participant.Person.Organisation.Addresses = new List<IAddress> { address };

            referee.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.AskedButUnknown);

            participant.Addresses = new List<IAddress> { address };

            return referee;
        }

        /// <summary>
        /// Creates and Hydrates a Service Provider Organisation
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthProfessional object</returns>
        private static IParticipationReferee CreateRefereeOrganisation(bool mandatorySectionsOnly)
        {
            var serviceProvider = EReferral.CreateReferee();

            serviceProvider.Participant = EReferral.CreateParticipantForReferee();

            serviceProvider.Participant.Organisation = BaseCDAModel.CreateOrganisation();
            serviceProvider.Participant.Organisation.Name = "Bay Hill Hospital (RefereeOrganisation)";
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

            serviceProvider.Role = !mandatorySectionsOnly ? BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner) : BaseCDAModel.CreateRole(NullFlavour.NegativeInfinity);

            serviceProvider.Participant.Addresses = new List<IAddress> { address };

            return serviceProvider;
        }

        /// <summary>
        /// Creates and Hydrates the diagnostic investigations substance reactions section.
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated DiagnosticInvestigations object</returns>
        private static IDiagnosticInvestigations CreateDiagnosticInvestigations(Boolean mandatoryOnly)
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
                GenericObjectReuseSample.CreatePathologyResults(mandatoryOnly)
            };

            // Requested Service
            diagnosticInvestigations.RequestedService = CreateRequestedService(mandatoryOnly);

            // Other Test Result 
            diagnosticInvestigations.OtherTestResult = new List<OtherTestResult>
            {
                    GenericObjectReuseSample.CreateOtherTestResultAttachment(),
                    GenericObjectReuseSample.CreateOtherTestResultText()
            };

            return diagnosticInvestigations;
        }

        #endregion
    }
}
