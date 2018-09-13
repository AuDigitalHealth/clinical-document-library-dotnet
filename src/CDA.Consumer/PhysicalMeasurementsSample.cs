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
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;

namespace Nehta.VendorLibrary.CDA.Consumer
{
    /// <summary>
    /// This project is intended to demonstrate how a Physical Measurements Sample CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// PhysicalMeasurements class, and then populated with data as appropriate. The three sections that need to be
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
    public class PhysicalMeasurementsSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\PhysicalMeasurements.xml";
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
        public XmlDocument MinPopulatedConsumerEnteredMeasurementsSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var physicalMeasurements = PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements, true);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GeneratePhysicalMeasurements method 
                xmlDoc = CDAGenerator.GeneratePhysicalMeasurements(physicalMeasurements);

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
        public XmlDocument MaxPopulatedConsumerEnteredMeasurementsSample(string fileName)
        {
            XmlDocument xmlDoc = null;

            var physicalMeasurements = PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements, false);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GeneratePhysicalMeasurements method 
                xmlDoc = CDAGenerator.GeneratePhysicalMeasurements(physicalMeasurements);

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
        /// This sample populates only the mandatory Sections / Entries
        /// </summary>
        public XmlDocument MinPopulatedHealthcareProviderEnteredMeasurementsSample(string fileName)
        {
          XmlDocument xmlDoc = null;

          var physicalMeasurements = PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements, true);

          try
          {
            CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

            //Pass the E-Referral model into the GeneratePhysicalMeasurements method 
            xmlDoc = CDAGenerator.GeneratePhysicalMeasurements(physicalMeasurements);

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
        public XmlDocument MaxPopulatedHealthcareProviderEnteredMeasurementsSample(string fileName)
        {
          XmlDocument xmlDoc = null;

          var physicalMeasurements = PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements, false);

          try
          {
            CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

            //Pass the E-Referral model into the GeneratePhysicalMeasurements method 
            xmlDoc = CDAGenerator.GeneratePhysicalMeasurements(physicalMeasurements);

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
        /// This sample populates only the mandatory Sections / Entries
        /// </summary>
        public XmlDocument MinPopulatedPhysicalMeasurementsViewSample(string fileName)
        {
          XmlDocument xmlDoc = null;

          var physicalMeasurements = PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType.PhysicalMeasurementsView, true);

          try
          {
            CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

            //Pass the E-Referral model into the GeneratePhysicalMeasurements method 
            xmlDoc = CDAGenerator.GeneratePhysicalMeasurements(physicalMeasurements);

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
        public XmlDocument MaxPopulatedPhysicalMeasurementsViewSample(string fileName)
        {
          XmlDocument xmlDoc = null;

          var physicalMeasurements = PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType.PhysicalMeasurementsView, false);

          try
          {
            CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

            //Pass the E-Referral model into the GeneratePhysicalMeasurements method 
            xmlDoc = CDAGenerator.GeneratePhysicalMeasurements(physicalMeasurements);

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
      /// This method populates an PhysicalMeasurements model with either the mandatory sections only, or both the mandatory and optional sections
      /// </summary>
      /// <param name="physicalMeasurementsDocumentType">The physical Measurements Document Type</param>
      /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
      /// <returns>PhysicalMeasurements</returns>
      public static PhysicalMeasurements PopulatePhysicalMeasurements(PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, Boolean mandatorySectionsOnly)
        {
            var physicalMeasurements = PhysicalMeasurements.CreatePhysicalMeasurements(DocumentStatus.Final);

            // Structured Document Model Identifier
            physicalMeasurements.StructuredDocumentModelIdentifier = physicalMeasurementsDocumentType;

            // Include Logo
            physicalMeasurements.IncludeLogo = true;
            physicalMeasurements.LogoPath = OutputFolderPath;

            // Set Creation Time
            physicalMeasurements.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = PhysicalMeasurements.CreateCDAContext();
            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
            // Set Id  
            cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
            // CDA Context Version
            cdaContext.Version = "1";

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            CreateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            // Legal authenticator
            if (!mandatorySectionsOnly)
            {
                cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
                GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);
            }

            physicalMeasurements.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model

            // Setup and Populate the SCS Context model
            physicalMeasurements.SCSContext = PhysicalMeasurements.CreateSCSContext();

            // Select the correct Author for the Document Type
            switch (physicalMeasurements.StructuredDocumentModelIdentifier)
            {
                case PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements:
                    // Create Author Non Healthcare Provider
                    var authorNonHealthcareProvider = BaseCDAModel.CreateAuthorPerson();
                    GenericObjectReuseSample.HydrateAuthorNonHealthcareProvider(authorNonHealthcareProvider, true, mandatorySectionsOnly);
                    physicalMeasurements.SCSContext.Author = authorNonHealthcareProvider;
                break;
                case PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements:
                    // Create Author Healthcare Provider
                    var authorHealthcareProvider = BaseCDAModel.CreateAuthorHealthcareProvider();
                    GenericObjectReuseSample.HydrateAuthorHealthcareProvider(authorHealthcareProvider, mandatorySectionsOnly);
                    physicalMeasurements.SCSContext.Author = authorHealthcareProvider;
                break;
                case PhysicalMeasurementsDocumentType.PhysicalMeasurementsView:
                    // Create Author Device
                    var authorAuthoringDevice = BaseCDAModel.CreateAuthorAuthoringDevice();
                    GenericObjectReuseSample.HydrateAuthorDevice(authorAuthoringDevice,mandatorySectionsOnly);
                    physicalMeasurements.SCSContext.Author = authorAuthoringDevice;
                break;
            }

            physicalMeasurements.SCSContext.HealthcareFacility = BirthDetailsRecord.CreateHealthcareFacility();
            GenericObjectReuseSample.HydrateHealthcareFacility(physicalMeasurements.SCSContext.HealthcareFacility, mandatorySectionsOnly);

            physicalMeasurements.SCSContext.SubjectOfCare = CreateSubjectofCare(mandatorySectionsOnly);

            #endregion

            #region Setup and populate the SCS Content model

            // Setup and populate the SCS Content model
            physicalMeasurements.SCSContent = PhysicalMeasurements.CreateSCSContent();

            // Physical Measurement
            physicalMeasurements.SCSContent.PhysicalMeasurement = CreatePhysicalMeasurement(physicalMeasurements.StructuredDocumentModelIdentifier.Value, mandatorySectionsOnly);

            #endregion

            return physicalMeasurements;
        }

      /// <summary>
      /// Creates and Hydrates a custodian
      /// 
      /// Note: the data used within this method is intended as a guide and should be replaced.
      /// </summary>
      /// <returns>A Custodian</returns>
      public static void CreateCustodian(IParticipationCustodian participationCustodian, bool mandatoryOnly)
      {
        var custodian = BaseCDAModel.CreateParticipantCustodian();

        // custodian/assignedCustodian
        participationCustodian.Participant = custodian;

        // custodian/assignedCustodian/representedCustodianOrganization
        custodian.Organisation = BaseCDAModel.CreateOrganisationName();

        if (!mandatoryOnly)
        {
          // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
          custodian.Organisation.Identifiers = new List<Identifier> { 
                    BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.77777", null),
                     BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.PAIO, "8003640001000036") 
                };

          // custodian/assignedCustodian/representedCustodianOrganization/name
          custodian.Organisation.Name = "Burrill Lake Medical Centre";

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
      /// This method populates an PhysicalMeasurement model based on the Document Type with either the mandatory sections only, 
      /// or both the mandatory and optional sections
      /// </summary>
      /// <param name="mandatoryOnly">The mandatory sections only</param>
      /// <param name="physicalMeasurementsDocumentType">The Selected Document Type </param>
      /// <returns>PhysicalMeasurements</returns>
      public static PhysicalMeasurement CreatePhysicalMeasurement(PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, bool mandatoryOnly)
      {
        var physicalMeasurement = PhysicalMeasurements.CreatePhysicalMeasurement();

        switch (physicalMeasurementsDocumentType)
        {
          case PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements:
            // Allowable Information Provider combinations for Consumer Entered Measurements:
            // - Information Provider Non Health care Provider
            // - Information Provider Device

            physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
            if (!mandatoryOnly)
            {
               physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
               physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
               physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);              
            }  

            break;
          case PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements:
            // Allowable Information Provider combinations for Consumer Entered Measurements:
            // - Information Provider Health care Provider
            // - Information Provider Device

            physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);

            if (!mandatoryOnly)
            {
               physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
               physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
               physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
            }
            break;
          case PhysicalMeasurementsDocumentType.PhysicalMeasurementsView:
            // Allowable Information Provider combinations for Physical Measurements View:
            // - Information Provider Health care Provider
            // - Information Provider Non Health care Provider
            // - Information Provider Device

            physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);

            if (!mandatoryOnly)
            {
              physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
              physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
              physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
            }
            break;
        }

        return physicalMeasurement;
      }


      ///// <summary>
      ///// This method populates an PhysicalMeasurement model based on the Document Type with either the mandatory sections only, 
      ///// or both the mandatory and optional sections
      ///// </summary>
      ///// <param name="mandatoryOnly">The mandatory sections only</param>
      ///// <param name="physicalMeasurementsDocumentType">The Selected Document Type </param>
      ///// <returns>PhysicalMeasurements</returns>
      //public static PhysicalMeasurement CreatePhysicalMeasurement(PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, bool mandatoryOnly)
      //  {
      //    var physicalMeasurement = PhysicalMeasurements.CreatePhysicalMeasurement();

      //    switch (physicalMeasurementsDocumentType)
      //    {
      //      case PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements:
      //        // Allowable Information Provider combinations for Consumer Entered Measurements:
      //        // - Information Provider Non Health care Provider
      //        // - Information Provider Device
      //        physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);

      //        break;
      //      case PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements:
      //        // Allowable Information Provider combinations for Consumer Entered Measurements:
      //        // - Information Provider Health care Provider
      //        // - Information Provider Device
      //        physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        break;
      //      case PhysicalMeasurementsDocumentType.PhysicalMeasurementsView:
      //        // Allowable Information Provider combinations for Physical Measurements View:
      //        // - Information Provider Health care Provider
      //        // - Information Provider Non Health care Provider
      //        // - Information Provider Device
      //        physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //        break;
      //    }

      //    return physicalMeasurement;
      //  }

      ///// <summary>
      ///// This method populates an PhysicalMeasurement model based on the Document Type with either the mandatory sections only, 
      ///// or both the mandatory and optional sections
      ///// </summary>
      ///// <param name="mandatoryOnly">The mandatory sections only</param>
      ///// <param name="physicalMeasurementsDocumentType">The Selected Document Type </param>
      ///// <returns>PhysicalMeasurements</returns>
      //public static PhysicalMeasurement CreatePhysicalMeasurementTwo(PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, bool mandatoryOnly)
      //{
      //  var physicalMeasurement = PhysicalMeasurements.CreatePhysicalMeasurement();

      //  // Select the Document Type
      //  switch (physicalMeasurementsDocumentType)
      //  {
      //    case PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements:
      //      // Allowable Information Provider combinations for Consumer Entered Measurements:
      //      // - Information Provider Non Health care Provider
      //      // - Information Provider Device
      //      physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      break;
      //    case PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements:
      //      // Allowable Information Provider combinations for Consumer Entered Measurements:
      //      // - Information Provider Health care Provider
      //      // - Information Provider Device
      //      physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);

      //      break;
      //    case PhysicalMeasurementsDocumentType.PhysicalMeasurementsView:
      //      // Allowable Information Provider combinations for Physical Measurements View:
      //      // - Information Provider Health care Provider
      //      // - Information Provider Non Health care Provider
      //      // - Information Provider Device
      //      physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderDevice(mandatoryOnly), mandatoryOnly);
      //      break;
      //  }

      //  return physicalMeasurement;
      //}


      ///// <summary>
      ///// This method populates an PhysicalMeasurement model based on the Document Type with either the mandatory sections only, 
      ///// or both the mandatory and optional sections
      ///// </summary>
      ///// <param name="mandatoryOnly">The mandatory sections only</param>
      ///// <param name="physicalMeasurementsDocumentType">The Selected Document Type </param>
      ///// <returns>PhysicalMeasurements</returns>
      //public static PhysicalMeasurement CreatePhysicalMeasurementThree(PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, bool mandatoryOnly)
      //{
      //  PhysicalMeasurement physicalMeasurement = null;

      //  if (physicalMeasurementsDocumentType ==  PhysicalMeasurementsDocumentType.PhysicalMeasurementsView)
      //  {
      //    // Allowable Information Provider combinations for Physical Measurements View:
      //    // - Information Provider Health care Provider
      //    // - Information Provider Non Health care Provider
      //    // - Information Provider Device
      //      physicalMeasurement = PhysicalMeasurements.CreatePhysicalMeasurement();
      //      physicalMeasurement.HeadCircumference = CreateHeadCircumference(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyHeightLength = CreatePhysicalMeasurementBodyHeightLength(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyMassIndex = CreatePhysicalMeasurementBodyMassIndex(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //      physicalMeasurement.PhysicalMeasurementBodyWeight = CreatePhysicalMeasurementBodyWeight(CreateInformationProviderNonHealthcareProvider(mandatoryOnly), mandatoryOnly);
      //  }
      //  return physicalMeasurement;
      //}


      /// <summary>
      /// This method populates an HeadCircumference model based on the Document Type with either the mandatory sections only, 
      /// or both the mandatory and optional sections
      /// </summary>
      /// <param name="mandatoryOnly">The mandatory sections only</param>
      /// <param name="informationProvider">The informationProvider</param>
      /// <returns>HeadCircumference</returns>
      public static HeadCircumference CreateHeadCircumference(IInformationProviderCollection informationProvider, bool mandatoryOnly)
      {
        var headCircumference = PhysicalMeasurements.CreateHeadCircumference();

        // Name of Location (Anatomical Location Name)
        headCircumference.NameOfLocation = PhysicalMeasurements.CreateCodableText("69536005", CodingSystem.SNOMED, "Head structure", null, null);

        // Circumference
        headCircumference.Circumference = PhysicalMeasurements.CreateQuantity("40.00", "cm");

        // Body Part Circumference DateTime
        headCircumference.BodyPartCircumferenceDateTime = new ISO8601DateTime(DateTime.Now);

        if (!mandatoryOnly)
        {
          // Circumference Normal Status
          headCircumference.CircumferenceNormalStatus = HL7ObservationInterpretationNormality.Normal;

          // Circumference Reference Range Details
          headCircumference.CircumferenceReferenceRangeDetails = new List<ReferenceRangeDetails>
                                                                   {
                                                                        CreateReferanceRangeDetails(),
                                                                        CreateReferanceRangeDetails()
                                                                   };
          // Comment (Measurement Comment)
          headCircumference.Comment = "Comment (Measurement Comment)";

          headCircumference.ConfoundingFactor = new List<ICodableText>
                                                  {
                                                      PhysicalMeasurements.CreateCodableText("162721008", CodingSystem.SNOMED, "On examination - agitated", null, null),
                                                      PhysicalMeasurements.CreateCodableText("162721008", CodingSystem.SNOMED, "On examination - agitated", null, null)
                                                  };

          // Device
          headCircumference.Device = CreateDevice(mandatoryOnly);

          // The Information Provider
          headCircumference.InformationProvider = informationProvider;
        }

        return headCircumference;
      }

      /// <summary>
      /// This method populates an PhysicalMeasurementBodyWeight model based on the Document Type with either the mandatory sections only, 
      /// or both the mandatory and optional sections
      /// </summary>
      /// <param name="mandatoryOnly">The mandatory sections only</param>
      /// <param name="informationProvider">The informationProvider</param>
      /// <returns>PhysicalMeasurementBodyWeight</returns>
      public static PhysicalMeasurementBodyWeight CreatePhysicalMeasurementBodyWeight(IInformationProviderCollection informationProvider, bool mandatoryOnly)
      {
        var physicalMeasurementBodyWeight = PhysicalMeasurements.CreatePhysicalMeasurementBodyWeight();

        // Name of Location (Anatomical Location Name)
        physicalMeasurementBodyWeight.Weight = PhysicalMeasurements.CreateQuantity("5", "kg");

        // Body Part Circumference DateTime
        physicalMeasurementBodyWeight.BodyWeightDateTime = new ISO8601DateTime(DateTime.Now);

        // Body Part Circumference Instance Identifier
        physicalMeasurementBodyWeight.BodyWeightInstanceIdentifier = PhysicalMeasurements.CreateIdentifier(BaseCDAModel.CreateGuid(), "Body Weight Instance Identifier");

        if (!mandatoryOnly)
        {
          // Circumference
          physicalMeasurementBodyWeight.WeightNormalStatus = HL7ObservationInterpretationNormality.High;

          // Circumference Normal Status
          physicalMeasurementBodyWeight.WeightEstimationFormula = "Weight Estimation Formula";

          // Circumference Reference Range Details
          physicalMeasurementBodyWeight.WeightReferenceRangeDetails = new List<ReferenceRangeDetails>
                                                                   {
                                                                        CreateReferanceRangeDetails(),
                                                                        CreateReferanceRangeDetails()
                                                                   };
          // Pregnant?
          physicalMeasurementBodyWeight.Pregnant = true;

          // Comment (Measurement Comment)
          physicalMeasurementBodyWeight.Comment = "Comment (Measurement Comment)";

          // State of Dress
          physicalMeasurementBodyWeight.StateOfDress =  "Lightly clothed/underwear";

          physicalMeasurementBodyWeight.ConfoundingFactor = new List<ICodableText>
                                                  {
                                                      PhysicalMeasurements.CreateCodableText("162721008", CodingSystem.SNOMEDCT, "On examination - agitated", null, null),
                                                      PhysicalMeasurements.CreateCodableText("162721008", CodingSystem.SNOMEDCT, "On examination - agitated", null, null),
                                                  };

          // Device
          physicalMeasurementBodyWeight.Device = CreateDevice(mandatoryOnly);

          // The Information Provider
          physicalMeasurementBodyWeight.InformationProvider = informationProvider;
        }

        return physicalMeasurementBodyWeight;
      }

      /// <summary>
      /// This method populates an PhysicalMeasurementBodyHeightLength model based on the Document Type with either the mandatory sections only, 
      /// or both the mandatory and optional sections
      /// </summary>
      /// <param name="mandatoryOnly">The mandatory sections only</param>
      /// <param name="informationProvider">The informationProvider</param>
      /// <returns>PhysicalMeasurementBodyHeightLength</returns>
      public static PhysicalMeasurementBodyHeightLength CreatePhysicalMeasurementBodyHeightLength(IInformationProviderCollection informationProvider, bool mandatoryOnly)
      {
        var physicalMeasurementBodyHeightLength = PhysicalMeasurements.CreatePhysicalMeasurementBodyHeightLength();

        // Name of Location (Anatomical Location Name)
        physicalMeasurementBodyHeightLength.HeightLength = PhysicalMeasurements.CreateQuantity("30", "cm");

        // Body Height Length Date Time
        physicalMeasurementBodyHeightLength.BodyHeightLengthDateTime = new ISO8601DateTime(DateTime.Now);

        // Body Height Length Instance Identifier
        physicalMeasurementBodyHeightLength.BodyHeightLengthInstanceIdentifier = PhysicalMeasurements.CreateIdentifier(BaseCDAModel.CreateGuid(), "Body Height Length Instance Identifier");

        if (!mandatoryOnly)
        {          
          // Height Length Normal Status
          physicalMeasurementBodyHeightLength.HeightLengthNormalStatus = HL7ObservationInterpretationNormality.AbnormalAlert;

          // Height Length Reference Range Details
          physicalMeasurementBodyHeightLength.HeightLengthReferenceRangeDetails = new List<ReferenceRangeDetails>
                                                                                  {
                                                                                      CreateReferanceRangeDetails(),
                                                                                      CreateReferanceRangeDetails()
                                                                                  };



          // Position
          physicalMeasurementBodyHeightLength.Position = "Position";

          // Comment (Measurement Comment)
          physicalMeasurementBodyHeightLength.Comment = "Comment (Measurement Comment)";

          // Confounding Factors
          //physicalMeasurementBodyHeightLength.ConfoundingFactor = new List<ICodableText>
          //                                        {
          //                                            PhysicalMeasurements.CreateCodableText("162721008", CodingSystem.SNOMED, "On examination - agitated", null, null),
          //                                            PhysicalMeasurements.CreateCodableText("162721008", CodingSystem.SNOMED, "On examination - agitated", null, null)
          //                                        };

          physicalMeasurementBodyHeightLength.ConfoundingFactor = new List<string>
                                                  {
                                                      "On examination - agitated",
                                                      "On examination - agitated"
                                                  };

          // Device
          physicalMeasurementBodyHeightLength.Device = CreateDevice(mandatoryOnly);

          // The Information Provider
          physicalMeasurementBodyHeightLength.InformationProvider = informationProvider;
        }

        return physicalMeasurementBodyHeightLength;
      }

      /// <summary>
      /// This method populates an PhysicalMeasurementBodyMassIndex model based on the Document Type with either the mandatory sections only, 
      /// or both the mandatory and optional sections
      /// </summary>
      /// <param name="mandatoryOnly">The mandatory sections only</param>
      /// <param name="informationProvider">The informationProvider</param>
      /// <returns>PhysicalMeasurementBodyMassIndex</returns>
      public static PhysicalMeasurementBodyMassIndex CreatePhysicalMeasurementBodyMassIndex(IInformationProviderCollection informationProvider, bool mandatoryOnly)
      {
        var physicalMeasurementBodyHeightLength = PhysicalMeasurements.CreatePhysicalMeasurementBodyMassIndex();

        // Body Mass Index
        physicalMeasurementBodyHeightLength.BodyMassIndex = PhysicalMeasurements.CreateQuantity("84", "mg");

        // Body Mass Index Date Time
        physicalMeasurementBodyHeightLength.BodyMassIndexDateTime = new ISO8601DateTime(DateTime.Now);

        // Body Mass Index Instance Identifier
        physicalMeasurementBodyHeightLength.BodyMassIndexInstanceIdentifier = PhysicalMeasurements.CreateIdentifier(BaseCDAModel.CreateGuid(), "Body Mass Index Instance Identifier");

        if (!mandatoryOnly)
        {
          // Body Mass Index Normal Status
          physicalMeasurementBodyHeightLength.BodyMassIndexNormalStatus = HL7ObservationInterpretationNormality.Normal;

          // Circumference Reference Range Details
          physicalMeasurementBodyHeightLength.BodyMassIndexReferenceRangeDetails = new List<ReferenceRangeDetails>
                                                                   {
                                                                        CreateReferanceRangeDetails(),
                                                                        CreateReferanceRangeDetails()
                                                                   };
          // Comment (Measurement Comment)
          physicalMeasurementBodyHeightLength.Comment = "Comment (Measurement Comment)";

          // Comment (Measurement Comment)
          physicalMeasurementBodyHeightLength.Method = BaseCDAModel.CreateCodableText("Body Height Length");

          // Formula (BMI Calculation Formula)
          physicalMeasurementBodyHeightLength.Formula = "Formula (BMI Calculation Formula)";

          // The Information Provider
          physicalMeasurementBodyHeightLength.InformationProvider = informationProvider;
        }

        return physicalMeasurementBodyHeightLength;
      }

      /// <summary>
      /// This method populates an ReferanceRangeDetails model based on the Document Type with either the mandatory sections only, 
      /// or both the mandatory and optional sections
      /// </summary>
      /// <returns>ReferanceRangeDetails</returns>
      public static ReferenceRangeDetails CreateReferanceRangeDetails()
      {
        var referenceRangeDetails = PhysicalMeasurements.CreateReferenceRangeDetails();

        // Head Circumference Percentile (Circumference Reference Range 1..1 Meaning)
        referenceRangeDetails.ReferenceRangeMeaning = BaseCDAModel.CreateCodableText("260395002", CodingSystem.SNOMED, "normal range", null, null);

        // Circumference Reference Range
        referenceRangeDetails.ReferenceRange = BaseCDAModel.CreateQuantityRange(37.5, 90, "ml");

        return referenceRangeDetails;
      }
     

        /// <summary>
        /// Creates and Hydrates the Device
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Device object</returns>
        private static Device CreateDevice(Boolean mandatorySectionsOnly)
        {
          var author = PhysicalMeasurements.CreateDevice();

          // Document Author > Role
          author.Role = BaseCDAModel.CreateRole(NullFlavour.NotApplicable);

          // Document Author > Software Name
          author.SoftwareName = "PCEHR National Repository";

          return author;
        }

        /// <summary>
        /// Creates and Hydrates a Information Provider Device
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Device object</returns>
        private static InformationProviderDevice CreateInformationProviderDevice(Boolean mandatorySectionsOnly)
        {
          var author = PhysicalMeasurements.CreateInformationProviderDevice();

          // Document Author > Role
          author.Role = BaseCDAModel.CreateRole(NullFlavour.NotApplicable);

          // Document Author > Software Name
          author.SoftwareName = "PCEHR National Repository";

          if (!mandatorySectionsOnly)
          {
            // Participation Period
            author.ParticipationPeriod = new ISO8601DateTime(DateTime.Now);
          }

          return author;
        }


        /// <summary>
        /// Creates and Hydrates an Participation Information Provider Non Healthcare Provider
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Consumer Information Provider</returns>
        public static IParticipationInformationProviderNonHealthcareProvider CreateInformationProviderNonHealthcareProvider(bool mandatoryOnly)
        {
          var informationProvider = PhysicalMeasurements.CreateInformationProviderNonHealthcareProvider();

          var person = BaseCDAModel.CreatePerson();

          // Information Provider > Participant
          informationProvider.Participant = PhysicalMeasurements.CreateParticipantForInformationProviderHealthcareProvider();

          // Information Provider > Participant > Person > Person Name
          var name1 = BaseCDAModel.CreatePersonName();
          name1.FamilyName = "Ching";

          var name2 = BaseCDAModel.CreatePersonName();
          name2.FamilyName = "Wong";

          person.PersonNames = new List<IPersonName> { name1, name2 };

          // Information Provider > Participant > Electronic Communication Detail
          var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
              "0345754566",
              ElectronicCommunicationMedium.Telephone,
              ElectronicCommunicationUsage.WorkPlace);

          var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
              "authen@globalauthens.com",
              ElectronicCommunicationMedium.Email,
              ElectronicCommunicationUsage.WorkPlace);

          informationProvider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

          if (!mandatoryOnly)
          {
            // Information Provider > Participation Period
            informationProvider.ParticipationPeriod = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Parse("10/10/1992"), ISO8601DateTime.Precision.Day));

            // Information Provider > Participant > Entity Identifier
            person.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateIdentifier("InformationProviderNonHealthcareProvider", null, null, "1.2.3.4.5.66666", null),
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003605833334119") 
            };

            name1.GivenNames = new List<string> { "Good" };
            name1.Titles = new List<string> { "Doctor" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            name2.GivenNames = new List<string> { "Davey" };
            name2.Titles = new List<string> { "Brother" };
            name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

            // Information Provider > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();
            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
            address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var addressList = new List<IAddress> { address1, address2 };

            informationProvider.Participant.Addresses = addressList;

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

          informationProvider.Participant.Person = person;

          return informationProvider;
        }

        /// <summary>
        /// Creates and Hydrates an Participation Information Provider Healthcare Provider
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Consumer Information Provider</returns>
        public static IParticipationInformationProviderHealthcareProvider CreateInformationProviderHealthcareProvider(bool mandatoryOnly)
        {
          var informationProvider = PhysicalMeasurements.CreateInformationProviderHealthcareProvider();

          var person = BaseCDAModel.CreatePersonWithOrganisation();

          // Role
          informationProvider.Role = BaseCDAModel.CreateRole(Occupation.OtherNaturalandPhysicalScienceProfessionalsnfd);

          // Information Provider > Participant
          informationProvider.Participant = PhysicalMeasurements.CreateParticipantForInformationProviderHealthcareProvider();

          // Information Providerr > Participant > Person > Person Name
          var name1 = BaseCDAModel.CreatePersonName();
          name1.FamilyName = "Ching";

          var name2 = BaseCDAModel.CreatePersonName();
          name2.FamilyName = "Wong";

          person.PersonNames = new List<IPersonName> { name1, name2 };

          // Information Provider > Participant > Entity Identifier
          person.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateIdentifier("InformationProviderHealthcareProvider", null, null, "1.2.3.4.5.66666", null),
                BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, "8003605833334119") 
            };

          if (!mandatoryOnly)
          {
            // Information Provider > Participation Period
            informationProvider.ParticipationPeriod = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Parse("10/10/1992"), ISO8601DateTime.Precision.Day));

            name1.GivenNames = new List<string> { "Good" };
            name1.Titles = new List<string> { "Doctor" };
            name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

            name2.GivenNames = new List<string> { "Davey" };
            name2.Titles = new List<string> { "Brother" };
            name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

            // Information Provider > Participant > Address
            var address1 = BaseCDAModel.CreateAddress();
            address1.AddressPurpose = AddressPurpose.Residential;
            address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var address2 = BaseCDAModel.CreateAddress();
            address2.AddressPurpose = AddressPurpose.TemporaryAccommodation;
            address2.AustralianAddress = BaseCDAModel.CreateAustralianAddress();

            var addressList = new List<IAddress> { address1, address2 };

            informationProvider.Participant.Addresses = addressList;

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

            // Information Provider > Participant > Electronic Communication Detail
            var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "0345754566",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);

            var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
                "authen@globalauthens.com",
                ElectronicCommunicationMedium.Email,
                ElectronicCommunicationUsage.WorkPlace);

            informationProvider.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

            person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
            person.Organisation.Name = "Hay Bill Hospital";
            person.Organisation.NameUsage = OrganisationNameUsage.Other;

            person.Organisation.Identifiers = new List<Identifier> { 
              BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, "8003620833333789"),
              BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null)
              };

            person.Organisation.Department = "Some department service provider";
            person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(null, null, null, "Casual", null);
            person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);
            person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);
          }

          informationProvider.Participant.Person = person;

          return informationProvider;
        }

 
        /// <summary>
        /// Creates and Hydrates an SubjectofCare
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated SubjectofCare</returns>
        public static IParticipationSubjectOfCare CreateSubjectofCare(bool mandatoryOnly)
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
                     "SampleAuthority", 
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
          name2.Titles = new List<string> { "Mr" };
          name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

          person.PersonNames = new List<IPersonName> { name1, name2 };

          // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Sex
          person.Gender = Gender.Female;

          if (!mandatoryOnly)
          {
            // Subject of Care > Participant > Person or Organisation or Device > Person > Demographic Data > Date of Birth Detail > 
            // Date of Birth
            person.DateOfBirth = new ISO8601DateTime(DateTime.Now.AddYears(-57));

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
            address1.AustralianAddress.DeliveryPointId = 32568931;

            address2.InternationalAddress.AddressLine = new List<string> { "1 Overseas Street" };
            address2.InternationalAddress.Country = Country.NewCaledonia;
            address2.InternationalAddress.PostCode = "12345";
            address2.InternationalAddress.StateProvince = "Foreign Land";

            person.Age = 54;
            person.DateOfBirthCalculatedFromAge = true;
            person.DateOfBirthAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
            person.AgeAccuracyIndicator = true;
            person.BirthPlurality = 3;
            person.BirthOrder = 2;
            person.DateOfDeath = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Day);
            person.DateOfDeathAccuracyIndicator = new DateAccuracyIndicator(true, true, true);
            person.CountryOfBirth = Country.Australia;
            person.StateOfBirth = AustralianState.QLD;
            person.SourceOfDeathNotification = SourceOfDeathNotification.HealthcareProvider;
            person.IndigenousStatus = IndigenousStatus.AboriginalButNotTorresStraitIslanderOrigin;

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
