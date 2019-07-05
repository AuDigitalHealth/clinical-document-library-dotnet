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

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how a Consumer Entered Health Summary CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// ConsumerEnteredHealthSummary class, and then populated with data as appropriate. The three sections that need to be
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
    public class ConsumerEnteredHealthSummarySample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\ConsumerEnteredHealthSummary.xml";
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
        public XmlDocument MinPopulatedConsumerEnteredHealthSummarySample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var consumerEnteredHealthSummary = PopulateConsumerEnteredHealthSummary(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateConsumerEnteredHealthSummary method 
                xmlDoc = CDAGenerator.GenerateConsumerEnteredHealthSummary(consumerEnteredHealthSummary);

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
        public XmlDocument MaxPopulatedConsumerEnteredHealthSummarySample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var consumerEnteredHealthSummary = PopulateConsumerEnteredHealthSummary(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateConsumerEnteredHealthSummary method 
                xmlDoc = CDAGenerator.GenerateConsumerEnteredHealthSummary(consumerEnteredHealthSummary);

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
        /// This method populates an consumerEnteredHealthSummary model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>ConsumerEnteredHealthSummary</returns>
        public static ConsumerEnteredHealthSummary PopulateConsumerEnteredHealthSummary(Boolean mandatorySectionsOnly)
        {
            var consumerEnteredHealthSummary = ConsumerEnteredHealthSummary.CreateConsumerEnteredHealthSummary();

            // Include Logo
            consumerEnteredHealthSummary.IncludeLogo = true;
            consumerEnteredHealthSummary.LogoPath = OutputFolderPath;

            // Set Creation Time
            consumerEnteredHealthSummary.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = ConsumerEnteredHealthSummary.CreateCDAContext();
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

            consumerEnteredHealthSummary.CDAContext = cdaContext;
            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            consumerEnteredHealthSummary.SCSContext = ConsumerEnteredHealthSummary.CreateSCSContext();

            consumerEnteredHealthSummary.SCSContext.Author = ConsumerEnteredHealthSummary.CreateAuthor();
            PopulateAuthor(consumerEnteredHealthSummary.SCSContext.Author, mandatorySectionsOnly);

            consumerEnteredHealthSummary.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(consumerEnteredHealthSummary.SCSContext.SubjectOfCare, mandatorySectionsOnly, false);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            consumerEnteredHealthSummary.SCSContent = ConsumerEnteredHealthSummary.CreateSCSContent();

            // Allergies And Adverse Reactions
            consumerEnteredHealthSummary.SCSContent.AllergiesAndAdverseReactions = CreateAllergiesAndAdverseReactions(mandatorySectionsOnly);

            // Medications
            consumerEnteredHealthSummary.SCSContent.Medications = CreateMedications(mandatorySectionsOnly);

            #endregion

            return consumerEnteredHealthSummary;
        }

        public static void PopulateAuthor(IParticipationConsumerAuthor author, bool mandatoryOnly)
        {
            var person = BaseCDAModel.CreatePerson();

            // Document Author > Participation Period
            author.AuthorParticipationPeriodOrDateTimeAuthored = new ISO8601DateTime(DateTime.Now);

            // Document Author > Role
            author.Role = BaseCDAModel.CreateRole(Occupation.MedicalOncologist);

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
            name1.FamilyName = "Doctor";

            var name2 = BaseCDAModel.CreatePersonName();
            name2.FamilyName = "Wong";

            person.PersonNames = new List<IPersonName>() { name1, name2 };
        
            if (!mandatoryOnly)
            {
                author.Participant.RelationshipToSubjectOfCare = BaseCDAModel.CreateRole(Occupation.MedicalOncologist);

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
            }

            author.Participant.Person = person;      
        }

        private static List<Reaction> CreateAllergiesAndAdverseReactions(bool mandatoryOnly)
        {
            if (mandatoryOnly)
            {
                return null;
            }
            else
            {
                var reaction1 = BaseCDAModel.CreateReaction();
                reaction1.Id = Guid.NewGuid().ToString();
                reaction1.SubstanceOrAgent = BaseCDAModel.CreateCodableText("SubstanceOrAgent");
                reaction1.ReactionEvent = BaseCDAModel.CreateReactionEvent();

                var reaction2 = BaseCDAModel.CreateReaction();
                reaction2.Id = Guid.NewGuid().ToString();
                reaction2.SubstanceOrAgent = BaseCDAModel.CreateCodableText("SubstanceOrAgent");
                reaction2.ReactionEvent = BaseCDAModel.CreateReactionEvent();

                reaction1.ReactionEvent.Manifestations = new List<ICodableText>
                {
                    BaseCDAModel.CreateCodableText("Manifestation")
                };

                reaction2.ReactionEvent.Manifestations = new List<ICodableText>
                {
                    BaseCDAModel.CreateCodableText("Manifestation")
                };

                return new List<Reaction>
                {
                    reaction1,
                    reaction2
                };
            }
        }

        private static List<IMedication> CreateMedications(bool mandatoryOnly)
        {
            if (mandatoryOnly)
                return null;
            else
            {
                var medication1 = ConsumerEnteredHealthSummary.CreateMedication();
                medication1.Id = Guid.NewGuid().ToString();
                medication1.Directions = BaseCDAModel.CreateStructuredText("Directions");
                medication1.ClinicalIndication = "clinicalIndication";
                medication1.Comment = "Comment";
                medication1.Medicine = BaseCDAModel.CreateCodableText("Medicine");

                var medication2 = ConsumerEnteredHealthSummary.CreateMedication();
                medication2.Id = Guid.NewGuid().ToString();
                medication2.Directions = BaseCDAModel.CreateStructuredText("Directions");
                medication2.ClinicalIndication = "clinicalIndication";
                medication2.Comment = "Comment";
                medication2.Medicine = BaseCDAModel.CreateCodableText("Medicine");

                var list = new List<IMedication>
                {
                    medication1,
                    medication2
                };

                return list;
            }
        }


        #endregion
    }
}
