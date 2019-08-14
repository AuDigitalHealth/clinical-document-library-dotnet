﻿/*
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
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.PCML.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;

namespace CDA.PCML
{
    /// <summary>
    /// This project is intended to demonstrate how a PCML (Pharmacy Shared Medicines) Sample CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// PCML class, and then populated with data as appropriate. The three sections that need to be
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
  
    public class PCMLSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PCML.xml";
            }
        }

        public static String StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

        // Note: Place this in any string field and and this will insert a break
        private const String DELIMITERBREAK = "<BR>";

        #endregion

        /// <summary>
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulatedPCMLSample()
        {
            XmlDocument xmlDoc = null;

            var PCML = PopulatePCML_1A(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                xmlDoc = CDAGenerator.GeneratePCML(PCML);

                xmlDoc.Save(OutputFileNameAndPath);
            }
            catch (ValidationException ex)
            {
                //Catch any validation exceptions
                Console.WriteLine(ex.Messages);

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
        public XmlDocument MinPopulatedPCMLAuthorHealthcareProviderSample_1A(string fileName)
        {
            XmlDocument xmlDoc = null;

            var PCML = PopulatePCML_1A(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Child Parent Questionnaire model into the GeneratePCML method 
                xmlDoc = CDAGenerator.GeneratePCML(PCML);

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
        public XmlDocument MaxPopulatedPCMLAuthorHealthcareProviderSample_1A(string fileName)
        {
            XmlDocument xmlDoc = null;

            var PCML = PopulatePCML_1A(false);
            
            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Child Parent Questionnaire model into the GeneratePCML method 
                xmlDoc = CDAGenerator.GeneratePCML(PCML);

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


        // ***** 1B documents - NOT REQUIRED YET ******

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries.
        /// </summary>
        public XmlDocument MinPopulatedPCMLAuthorHealthcareProviderSample_1B(string fileName)
        {
            XmlDocument xmlDoc = null;

            var PCML = PopulatePCML_1B(true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Child Parent Questionnaire model into the GeneratePCML method 
                xmlDoc = CDAGenerator.GeneratePCML(PCML);

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

        public XmlDocument MaxPopulatedPCMLAuthorHealthcareProviderSample_1B(string fileName)
        {
            XmlDocument xmlDoc = null;

            var PCML = PopulatePCML_1B(false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the PCML model into the GeneratePCML method 
                xmlDoc = CDAGenerator.GeneratePCML(PCML);

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


        #region Populate Methods
        /// <summary>
        /// This method populates an PCML model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>PCML</returns>
        public static Nehta.VendorLibrary.CDA.Common.PCML PopulatePCML_1A(Boolean mandatorySectionsOnly)
        {
            var pharmacyCuratedMedsList = Nehta.VendorLibrary.CDA.Common.PCML.CreatePCML();

              // Include Logo
            pharmacyCuratedMedsList.IncludeLogo = false;

              // Set Creation Time
            pharmacyCuratedMedsList.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            ICDAContextPCML cdaContext = Nehta.VendorLibrary.CDA.Common.PCML.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
            
            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            //Optional sections
            if (!mandatorySectionsOnly)
            {
                // Set Id  
                cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
                // CDA Context Version
                cdaContext.Version = "1";

                // Legal authenticator
                cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
                GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

                // Information Recipients
                cdaContext.InformationRecipients = new List<IParticipationInformationRecipient>();

                var recipient1 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient1, RecipientType.Primary, mandatorySectionsOnly);

                var recipient2 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient2, RecipientType.Secondary, mandatorySectionsOnly);

                cdaContext.InformationRecipients.AddRange(new[] { recipient1, recipient2 });
            }


            pharmacyCuratedMedsList.CDAContext = cdaContext;
            #endregion

            
            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            pharmacyCuratedMedsList.SCSContext = Nehta.VendorLibrary.CDA.Common.PCML.CreateSCSContext();

            var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, mandatorySectionsOnly);
            pharmacyCuratedMedsList.SCSContext.Author = authorHealthcareProvider;

            //Cannot use as a device : prohibited by CORE Level One
            //pharmacyCuratedMedsList.SCSContext.Author = GenericObjectReuseSample.CreateAuthorDevice();

            if (!mandatorySectionsOnly)
            {
                // Context>Encounter>HEALTHCARE FACILITY 
                pharmacyCuratedMedsList.SCSContext.Encounter = new Encounter
                {
                    HealthcareFacility = PopulateHealthcareFacility(mandatorySectionsOnly)
                };
            }

            pharmacyCuratedMedsList.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(pharmacyCuratedMedsList.SCSContext.SubjectOfCare, mandatorySectionsOnly);


            IParticipationPersonOrOrganisation person = Nehta.VendorLibrary.CDA.Common.PCML.CreateParticipationPersonOrOrganisation();
            person.Participant = Nehta.VendorLibrary.CDA.Common.PCML.CreateParticipantPersonOrOrganisation();
            person.Role = BaseCDAModel.CreateRole(HealthcareFacilityTypeCodes.AgedCareResidentialServices);
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

            person.Participant.Addresses  = new List<IAddress> { address1 };
            
            person.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            

            person.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Participant.Person.Organisation.Name = "Hay Bill Hospital";
            person.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;

            // New requirement to make address mandatory
            person.Participant.Person.Organisation.Addresses = new List<IAddress> { address1 };

            person.Participant.Person.Organisation.Identifiers = new List<Identifier> {
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
                entitlement1.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement1.Type = EntitlementType.MedicareBenefits;
                entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                var entitlement2 = BaseCDAModel.CreateEntitlement();
                entitlement2.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement2.Type = EntitlementType.MedicareBenefits;
                entitlement2.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                person.Participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };

                // Optional Participants
                pharmacyCuratedMedsList.SCSContext.Participant = new List<IParticipationPersonOrOrganisation>();
                pharmacyCuratedMedsList.SCSContext.Participant.Add(person);
            }

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            pharmacyCuratedMedsList.SCSContent = Nehta.VendorLibrary.CDA.Common.PCML.CreateSCSContent();

            pharmacyCuratedMedsList.SCSContent.EncapsulatedData = BaseCDAModel.CreateEncapsulatedData();

            ExternalData report1 = BaseCDAModel.CreateExternalData();
            report1.ExternalDataMediaType = MediaType.PDF;
            report1.Path = StructuredFileAttachment;
            report1.Caption = "Attachment One";


            pharmacyCuratedMedsList.SCSContent.EncapsulatedData.ExternalData = report1;
            #endregion

            return pharmacyCuratedMedsList;
        }


        /// <summary>
        /// This method populates an PCML model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>PCML</returns>
        public static Nehta.VendorLibrary.CDA.Common.PCML PopulatePCML_1B(Boolean mandatorySectionsOnly)
        {
            var pharmacyCuratedMedsList = Nehta.VendorLibrary.CDA.Common.PCML.CreatePCML();

            // Include Logo
            pharmacyCuratedMedsList.IncludeLogo = true;
			pharmacyCuratedMedsList.LogoPath = OutputFolderPath;

            // Set Creation Time
            pharmacyCuratedMedsList.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = Nehta.VendorLibrary.CDA.Common.PCML.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            //Optional sections
            if (!mandatorySectionsOnly)
            {
                // Set Id  
                cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
                // CDA Context Version
                cdaContext.Version = "1";

                // Legal authenticator
                cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
                GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

                // Information Recipients
                cdaContext.InformationRecipients = new List<IParticipationInformationRecipient>();

                var recipient1 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient1, RecipientType.Primary, mandatorySectionsOnly);

                var recipient2 = BaseCDAModel.CreateInformationRecipient();
                GenericObjectReuseSample.HydrateRecipient(recipient2, RecipientType.Secondary, mandatorySectionsOnly);

                cdaContext.InformationRecipients.AddRange(new[] { recipient1, recipient2 });
            }

            pharmacyCuratedMedsList.CDAContext = cdaContext;
            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            pharmacyCuratedMedsList.SCSContext = Nehta.VendorLibrary.CDA.Common.PCML.CreateSCSContext();

            var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
            GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, mandatorySectionsOnly);
            pharmacyCuratedMedsList.SCSContext.Author = authorHealthcareProvider;

            //Cannot use as a device : prohibited by CORE Level One
            //pharmacyCuratedMedsList.SCSContext.Author = GenericObjectReuseSample.CreateAuthorDevice();

            if (!mandatorySectionsOnly)
            {
                // Context>Encounter>HEALTHCARE FACILITY 
                pharmacyCuratedMedsList.SCSContext.Encounter = new Encounter
                {
                    HealthcareFacility = PopulateHealthcareFacility(mandatorySectionsOnly)
                };
            }

            pharmacyCuratedMedsList.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(pharmacyCuratedMedsList.SCSContext.SubjectOfCare, mandatorySectionsOnly);


            IParticipationPersonOrOrganisation person = Nehta.VendorLibrary.CDA.Common.PCML.CreateParticipationPersonOrOrganisation();
            person.Participant = Nehta.VendorLibrary.CDA.Common.PCML.CreateParticipantPersonOrOrganisation();
            person.Role = BaseCDAModel.CreateRole(HealthcareFacilityTypeCodes.AgedCareResidentialServices);
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

            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
            address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
            address1.AustralianAddress.State = AustralianState.NSW;
            address1.AustralianAddress.PostCode = "5555";
            address1.AustralianAddress.DeliveryPointId = 32568931;

            person.Participant.Addresses = new List<IAddress> { address1 };

            person.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();

            person.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Participant.Person.Organisation.Name = "Hay Bill Hospital";
            person.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.Other;

            // New requirement to make address mandatory
            person.Participant.Person.Organisation.Addresses = new List<IAddress> { address1 };

            person.Participant.Person.Organisation.Identifiers = new List<Identifier> {
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
                entitlement1.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement1.Type = EntitlementType.MedicareBenefits;
                entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                var entitlement2 = BaseCDAModel.CreateEntitlement();
                entitlement2.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, "1234567881");
                entitlement2.Type = EntitlementType.MedicareBenefits;
                entitlement2.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

                person.Participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };

                // Optional Participants
                pharmacyCuratedMedsList.SCSContext.Participant = new List<IParticipationPersonOrOrganisation>();
                pharmacyCuratedMedsList.SCSContext.Participant.Add(person);
            }

            #endregion

            #region Setup and populate the SCS Content model

            // Setup and populate the SCS Content model
            pharmacyCuratedMedsList.SCSContent = Nehta.VendorLibrary.CDA.Common.PCML.CreateSCSContent();

            //Use Custom Narrative instead of Attachment - 1B
            //pharmacyCuratedMedsList.SCSContent.EncapsulatedData = BaseCDAModel.CreateEncapsulatedData();
            pharmacyCuratedMedsList.ShowAdministrativeObservationsSection = false;
            pharmacyCuratedMedsList.ShowAdministrativeObservationsNarrativeAndTitle = false;

            // Build Narrative
            var sdt = new StrucDocText();

            sdt.table = new[]
            {
                new StrucDocTable
                {
                    caption = new StrucDocCaption {Text = new [] {"Patient History"}},
                    tbody = new [] { new StrucDocTbody
                    {
                        tr = new [] { new StrucDocTr { td = new []{ AddTd("Allergies"), AddTd("Nil Known") } } ,
                                      new StrucDocTr { td = new []{ AddTd("Diagnosis"), AddTd("CVA, TIA, Hypertension") } } }
                    } }
                },
                new StrucDocTable
                {
                    caption = new StrucDocCaption {Text = new [] {"Medications"}},
                    thead = new StrucDocThead {tr = new [] { new StrucDocTr { td = new [] { AddTd("Drug", "BoldxColWidthPx200"), AddTd("Direction", "BoldxColWidthPx200"), AddTd("B'fast", "BoldxColWidthPx20"), AddTd("Lunch", "BoldxColWidthPx20"), AddTd("Dinner", "BoldxColWidthPx20"), AddTd("B'time", "BoldxColWidthPx20"), AddTd("Indication", "BoldxColWidthPx200"), AddTd("Special Instructions", "BoldxColWidthPx220") } } }},
                    tbody = new [] { new StrucDocTbody {tr = new []
                    {
                        new StrucDocTr { td = new []{ AddTd("LanoxnPG62.5mcgTb"), AddTd("One on alternate morning"), AddTd("1"), AddTd(""), AddTd(""), AddTd(""), AddTd("Heart rate"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Paralgin500mgTb"), AddTd("One four times daily"), AddTd("1"), AddTd("1"), AddTd("1"), AddTd("1"), AddTd("Pain"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("CandesartanSZ16mg"), AddTd("One in the morning"), AddTd("1"), AddTd(""), AddTd(""), AddTd(""), AddTd("High Blood Pressume"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Rispa 0.5mg Tab"), AddTd("One at bedtime"), AddTd(""), AddTd(""), AddTd(""), AddTd("1"), AddTd("Behavioural Disorder"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Apo-Rabeprzle20mgTb"), AddTd("One in the morning"), AddTd("1"), AddTd(""), AddTd(""), AddTd(""), AddTd("Barrett's Oesaphagus"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Sorbolene Cream"), AddTd("Apply twice daily for dry skin after bath"), AddTd("Ap"), AddTd(""), AddTd("Ap"), AddTd(""), AddTd("Dry Skin"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Liquifilm TearsEyDrp"), AddTd("Instill one drop four times daily into both eyes"), AddTd("1"), AddTd("1"), AddTd("1"), AddTd(""), AddTd("Dry Eyes"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Keflex 500mg Cap"), AddTd("One three times daily for seven days"), AddTd("1"), AddTd("1"), AddTd("1"), AddTd(""), AddTd(""), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Coloxyl/SennaTab"), AddTd("One at dinner when necessary if BNO 3/7"), AddTd("1"), AddTd("1"), AddTd("1"), AddTd("1"), AddTd("Constipation"), AddTd("") } },
                        new StrucDocTr { td = new []{ AddTd("Fosamax70mgTab"), AddTd("One on Thursday morning half an hour before food"), AddTd("1"), AddTd(""), AddTd(""), AddTd(""), AddTd("Asteoporosis"), AddTd("") } },
                    }} }
                }
            };


            // Save Custom Text
            var narrativeOnlyDocument = BaseCDAModel.CreateNarrativeOnlyDocument();
            narrativeOnlyDocument.Title = "Patient Medication Record";
            narrativeOnlyDocument.Narrative = sdt;

            pharmacyCuratedMedsList.SCSContent.CustomNarrativePcmlRecord = new List<NarrativeOnlyDocument>();
            pharmacyCuratedMedsList.SCSContent.CustomNarrativePcmlRecord.Add(narrativeOnlyDocument);

            #endregion


            return pharmacyCuratedMedsList;
        }

        private static StrucDocTd AddTd(string text, string style = "")
        {
            return new StrucDocTd {Text = new[] {text}, styleCode = style };
        }

        /// <summary>
        /// Creates and Hydrates an IParticipationHealthcareFacility
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated IParticipationHealthcareFacility</returns>
        public static IParticipationHealthcareFacility PopulateHealthcareFacility(bool mandatoryOnly)
        {
            var participation = Nehta.VendorLibrary.CDA.Common.PCML.CreateHealthcareFacility();

            participation.ParticipationPeriod = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));

            participation.Participant = BaseCDAModel.CreateParticipantForHealthcareFacility();
            participation.Participant.Organisation = BaseCDAModel.CreateOrganisation();

            participation.Role = BaseCDAModel.CreateCodableText(NullFlavour.NoInformation);

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
                // Electronic Communication Details
                participation.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
                {
                    BaseCDAModel.CreateElectronicCommunicationDetail("0712341234", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace),
                    BaseCDAModel.CreateElectronicCommunicationDetail("0712341236", ElectronicCommunicationMedium.Fax, ElectronicCommunicationUsage.WorkPlace),
                };

                // Organisation Continued
                participation.Participant.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;
                participation.Participant.Organisation.Department = "General Health";
            }

            return participation;
        }

        #endregion
    }
}
