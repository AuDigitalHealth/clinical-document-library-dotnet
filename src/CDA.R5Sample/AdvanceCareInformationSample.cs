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
using CDA.Generator.Common.SCSModel.AdvanceCareInformation.Entities;
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.R5Samples
{
    /// <summary>
    /// This project is intended to demonstrate how an AdvanceCareInformation CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// AdvanceCareInformation class, and then populated with data as appropriate. The three sections that need to be
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

    public class AdvanceCareInformationSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\AdvanceCareInformation.xml";
            }
        }

        public static String AttachmentFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

       #endregion

        /// <summary>
        /// This sample populates only the mandatory sections / entries
        /// </summary>
        public XmlDocument MinPopulatedAdvanceCareInformation(string fileName, AuthorType authorType)
        {
            XmlDocument xmlDoc = null;

            var advanceCareInformation = PopulatedAdvanceCareInformation(true, authorType);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GenerateAdvanceCareInformation method 
                xmlDoc = CDAGenerator.GenerateAdvanceCareInformation(advanceCareInformation);

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
        public XmlDocument MaxPopulatedAdvanceCareInformation(string fileName, AuthorType authorType)
        {
            XmlDocument xmlDoc = null;

            var advanceCareInformation = PopulatedAdvanceCareInformation(false, authorType);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Event Summary model into the GenerateAdvanceCareInformation method 
                xmlDoc = CDAGenerator.GenerateAdvanceCareInformation(advanceCareInformation);

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

        #region Private Test Methods

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries depending on the  
        /// mandatorySectionsOnly Boolean
        /// </summary>
        internal static AdvanceCareInformation PopulatedAdvanceCareInformation(Boolean mandatorySectionsOnly, AuthorType authorType)
        {
            var advanceCareInformation = AdvanceCareInformation.CreateAdvanceCareInformation();

            // Include Logo
            advanceCareInformation.IncludeLogo = true;
            advanceCareInformation.LogoPath = OutputFolderPath;

            // Set Creation Time
            advanceCareInformation.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = AdvanceCareInformation.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid());

            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid());

            // CDA Context Version
            cdaContext.Version = "1";

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, "Organisation Name", mandatorySectionsOnly);

            // Legal Authenticator
            cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
            GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

            advanceCareInformation.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            advanceCareInformation.SCSContext = AdvanceCareInformation.CreateSCSContext();

            // Switch on the author enumerator.
            switch (authorType)
            {
                case AuthorType.AuthorHealthcareProvider:
                    // Create Author Healthcare Provider
                    var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
                    GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, "Organisation Name", mandatorySectionsOnly);
                    advanceCareInformation.SCSContext.Author = authorHealthcareProvider;
                    break;

                case AuthorType.AuthorNonHealthcareProvider:
                    // Create Author Non Healthcare Provider
                    var authorNonHealthcareProvider = BaseCDAModel.CreateAuthorPerson();
                    GenericObjectReuseSample.HydrateAuthorNonHealthcareProvider(authorNonHealthcareProvider, mandatorySectionsOnly);
                    advanceCareInformation.SCSContext.Author = authorNonHealthcareProvider;
                    break;
            }

            advanceCareInformation.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(advanceCareInformation.SCSContext.SubjectOfCare, mandatorySectionsOnly);

            // REMOVE THESE FIELDS AS NOT ALLOWED IN ACI
            advanceCareInformation.SCSContext.SubjectOfCare.Participant.Person.DateOfDeath = null;
            advanceCareInformation.SCSContext.SubjectOfCare.Participant.Person.DateOfDeathAccuracyIndicator = null;
            advanceCareInformation.SCSContext.SubjectOfCare.Participant.Person.SourceOfDeathNotification = null;

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            advanceCareInformation.SCSContent = AdvanceCareInformation.CreateSCSContent();

            // Related Information
            advanceCareInformation.SCSContent.DocumentDetails = CreateRelatedDocument(mandatorySectionsOnly);

            #endregion

            return advanceCareInformation;
        }

        /// <summary>
        /// Creates and hydrates the 'RelatedInformation' section.
        /// </summary>
        /// <param name="mandatorySectionsOnly">Includes on the mandatory items</param>
        /// <returns>A hydrated 'RelatedInformation' object.</returns>
        public static IDocumentDetails CreateRelatedDocument(Boolean mandatorySectionsOnly)
        {
            var relatedDocument = AdvanceCareInformation.CreateRelatedDocument();

            // Examination Result Representation (Document Target)
            relatedDocument.ExternalData = BaseCDAModel.CreateExternalData();
            relatedDocument.ExternalData.ExternalDataMediaType = MediaType.PDF;
            relatedDocument.ExternalData.Path = AttachmentFileNameAndPath;
            relatedDocument.ExternalData.Caption = "Attachment";

            // Document Provenance
            relatedDocument.DocumentProvenance = CreateDocumentProvenance(mandatorySectionsOnly);

            return relatedDocument;
        }

        /// <summary>
        /// Creates and hydrates the 'DocumentProvenances' section.
        /// </summary>
        /// <param name="mandatorySectionsOnly">Includes on the mandatory sections</param>
        /// <returns>A hydrated 'DocumentProvenance' object.</returns>
        public static DocumentProvenance CreateDocumentProvenance(bool mandatorySectionsOnly)
        {
            var documentProvenance = AdvanceCareInformation.CreateDocumentProvenance();

            // Document Type
            documentProvenance.DocumentType = DocumentType.AdvanceCarePlanDirectiveAndSubstituteDecisionMaker;

            // Author of the CDA document
            documentProvenance.Author = CreateDocumentAuthor(mandatorySectionsOnly);

            if (!mandatorySectionsOnly)
            {
               // Document Identifier
               documentProvenance.DocumentIdentifier = BaseCDAModel.CreateIdentifier("Document Identifier", null, null, "1.2.3.4.5.66666", null);
            }

            return documentProvenance;
        }

        /// <summary>
        /// Creates and Hydrates an author
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>An Hydrated author</returns>
        public static IParticipationAuthorPerson CreateDocumentAuthor(bool mandatoryOnly)
        {
            // Create IParticipationAuthorPerson
            var author = BaseCDAModel.CreateAuthorPerson();

            // Document Author > Participant
            author.Participant = BaseCDAModel.CreateParticipantForAuthorPerson();

            // Create Person
            var person = BaseCDAModel.CreatePerson();

            // Document Author > Participation Period
            // This element will hold the same value as target Shared Health Summary > Date- Time Attested (ClinicalDocument/ legalAuthenticator/ time)
            author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second));

            // Document Author > Role
            author.Role = BaseCDAModel.CreateCodableText(NullFlavour.NotApplicable);

            // Document Author > Participant > Person or Organisation or Device > Person > Person Name
            var name = BaseCDAModel.CreatePersonName();
            name.FamilyName = "Smith";

            // Document Author > Person Names 
            person.PersonNames = new List<IPersonName> { name, name };

            var electronicCommunicationDetailPhone = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var electronicCommunicationDetailEmail = BaseCDAModel.CreateElectronicCommunicationDetail(
                "BayHill@BayHill.com.au",
                ElectronicCommunicationMedium.Email,
                ElectronicCommunicationUsage.WorkPlace);

            author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
            {
                electronicCommunicationDetailPhone, electronicCommunicationDetailEmail
            };

            if (!mandatoryOnly)
            {
                // Document Author > Participant > Entity Identifier
                person.Identifiers = new List<Identifier> { 
                    BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null), 
                    BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118") 
                };

                // Document Author > Participant > Address
                var address = BaseCDAModel.CreateAddress();
                address.AddressAbsentIndicator = AddressAbsentIndicator.NoFixedAddressIndicator;
                address.AddressPurpose = AddressPurpose.Residential;
                address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

                var addressList = new List<IAddress> { address, address };

                author.Participant.Addresses = addressList;

                name.GivenNames = new List<string> { "Good" };
                name.Titles = new List<string> { "Doctor" };
                name.NameUsages = new List<NameUsage> { NameUsage.Legal };

                address.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
                address.AustralianAddress.SuburbTownLocality = "Nehtaville";
                address.AustralianAddress.State = AustralianState.QLD;
                address.AustralianAddress.PostCode = "5555";
                address.AustralianAddress.DeliveryPointId = 32568931;
                address.AddressAbsentIndicator = null;
            }

            author.Participant.Person = person;

            return author;
        }

        #endregion

    }
}