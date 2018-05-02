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
using System.Runtime.Serialization;
using System.Xml;
using CDA.Generator.Common.SCSModel.MedicareOverview.Entities;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    
    /// <summary>
    /// This MedicareOverview object is a composition of the context and content objects that define
    /// a CDA Consolidated View document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any MedicareOverview
    /// objects that are required to build a valid Consolidated View CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    public class MedicareOverview : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextMedicareOverview that contains the CDA Context for this Consolidated View document
        /// </summary>
        [DataMember]
        public ICDAContextMedicareOverview CDAContext { get; set; }

        /// <summary>
        /// An IMedicareOverviewContent that contains the SCS Content for this Consolidated View document
        /// </summary>
        [DataMember]
        public IMedicareOverviewContent SCSContent { get; set; }

        /// <summary>
        /// An IMedicareOverviewContext that contains the SCS Context for this Consolidated View document
        /// </summary>
        [DataMember]
        public IMedicareOverviewContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a Consolidated View model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public MedicareOverview(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Consolidated View model; the status of this document will be 
        /// set to final
        /// </summary>
        public MedicareOverview() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this MedicareOverview object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("CDAContext", CDAContext))
                CDAContext.Validate(vb.Path + "CDAContext", vb.Messages);

            if (vb.ArgumentRequiredCheck("SCSContext", SCSContext))
                SCSContext.Validate(vb.Path + "SCSContext", vb.Messages);

            if (vb.ArgumentRequiredCheck("SCSContent", SCSContent))
                SCSContent.Validate(vb.Path + "SCSContent", vb.Messages);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a MedicareOverview
        /// </summary>
        /// <returns>MedicareOverview</returns>
        public static MedicareOverview CreateMedicareOverview()
        {
            return new MedicareOverview();
        }

        /// <summary>
        /// Creates a MedicareOverview
        /// </summary>
        /// <returns>MedicareOverview</returns>
        public static MedicareOverview CreateMedicareOverview(DocumentStatus documentStatus)
        {
            return new MedicareOverview
            {
                DocumentStatus = documentStatus
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextMedicareOverview) Context</returns>
        public static ICDAContextMedicareOverview CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IMedicareOverviewContent) Content</returns>
        public static IMedicareOverviewContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IMedicareOverviewContext) Context</returns>
        public static IMedicareOverviewContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates Medicare DVA Funded Services 
        /// </summary>
        /// <returns>(MedicareDVAFundedServicesHistory ) Context</returns>
        public static MedicareDVAFundedServicesHistory CreateMedicareDVAFundedServicesHistory()
        {
          return new MedicareDVAFundedServicesHistory();
        }

        /// <summary>
        /// Creates Pharmaceutical Benefits History
        /// </summary>
        /// <returns>(PharmaceuticalBenefitsHistory) Context</returns>
        public static PharmaceuticalBenefitsHistory CreatePharmaceuticalBenefitsHistory()
        {
          return new PharmaceuticalBenefitsHistory();
        }

        /// <summary>
        /// Creates Australian Childhood Immunisation Register History
        /// </summary>
        /// <returns>(AustralianChildhoodImmunisationRegisterHistory ) Context</returns>
        public static AustralianChildhoodImmunisationRegisterHistory CreateAustralianChildhoodImmunisationRegisterHistory()
        {
          return new AustralianChildhoodImmunisationRegisterHistory();
        }

        /// <summary>
        /// Creates a Vaccine Administration Entry
        /// </summary>
        /// <returns>(VaccineAdministrationEntry) Context</returns>
        public static VaccineAdministrationEntry CreateVaccineAdministrationEntry()
        {
          return new VaccineAdministrationEntry();
        }

        /// <summary>
        /// Creates a Vaccine Cancellation Entry
        /// </summary>
        /// <returns>(VaccineAdministrationEntry) Context</returns>
        public static VaccineCancellationEntry CreateVaccineCancellationEntry()
        {
          return new VaccineCancellationEntry();
        }

        /// <summary>
        /// Creates a Vaccine Cancellation Reason
        /// </summary>
        /// <returns>(VaccineCancellationReason) Context</returns>
        public static VaccineCancellationReason CreateVaccineCancellationReason()
        {
          return new VaccineCancellationReason();
        }

        /// <summary>
        /// Creates a Australian Childhood Immunisation Register Entries
        /// </summary>
        /// <returns>(AustralianChildhoodImmunisationRegisterEntries) Context</returns>
        public static AustralianChildhoodImmunisationRegisterEntries CreateAustralianChildhoodImmunisationRegisterEntries()
        {
          return new AustralianChildhoodImmunisationRegisterEntries();
        }

        /// <summary>
        /// Creates a Australian Childhood Immunisation Register Entry
        /// </summary>
        /// <returns>(AustralianChildhoodImmunisationRegisterEntry) Context</returns>
        public static AustralianChildhoodImmunisationRegisterEntry CreateAustralianChildhoodImmunisationRegisterEntry()
        {
          return new AustralianChildhoodImmunisationRegisterEntry();
        }

        /// <summary>
        /// Creates an Pharmaceutical Benefit Items 
        /// </summary>
        /// <returns>(CreatePharmaceuticalBenefitItems ) Context</returns>
        public static PharmaceuticalBenefitItems CreatePharmaceuticalBenefitItems()
        {
          return new PharmaceuticalBenefitItems();
        }

        /// <summary>
        /// Creates an Medicare DVA Funded Services
        /// </summary>
        /// <returns>(MedicareDVAFundedServices ) Context</returns>
        public static MedicareDVAFundedServices CreateMedicareDVAFundedServices()
        {
          return new MedicareDVAFundedServices();
        }

        /// <summary>
        /// Creates an Medicare DVA Funded Service
        /// </summary>
        /// <returns>(MedicareDVAFundedService) Context</returns>
        public static MedicareDVAFundedService CreateMedicareDVAFundedService()
        {
          return new MedicareDVAFundedService();
        }

        /// <summary>
        /// Creates an Pharmaceutical Benefit Items
        /// </summary>
        /// <returns>(CreatePharmaceuticalBenefitItem ) Context</returns>
        public static PharmaceuticalBenefitItem CreatePharmaceuticalBenefitItem()
        {
          return new PharmaceuticalBenefitItem();
        }

        /// <summary>
        /// Creates AustralianOrganDonorRegisterDecisionInformation 
        /// </summary>
        /// <returns>(AustralianChildhoodImmunisationRegisterHistory) Context</returns>
        public static AustralianOrganDonorRegisterDecisionInformation CreateAustralianOrganDonorRegisterComponent()
        {
          return new AustralianOrganDonorRegisterDecisionInformation();
        }

        /// <summary>
        /// Creates AustralianOrganDonorRegisterDetails 
        /// </summary>
        /// <returns>(AustralianOrganDonorRegisterDetails) Context</returns>
        public static AustralianOrganDonorRegisterDetails CreateAustralianOrganDonorRegisterDetails()
        {
          return new AustralianOrganDonorRegisterDetails();
        }

        /// <summary>
        /// Creates AustralianOrganDonorRegisterEntry 
        /// </summary>
        /// <returns>(AustralianOrganDonorRegisterEntry) Context</returns>
        public static AustralianOrganDonorRegisterEntry CreateAustralianOrganDonorRegisterEntry()
        {
          return new AustralianOrganDonorRegisterEntry();
        }

        /// <summary>
        /// Creates OrganAndTissueDonationDetail 
        /// </summary>
        /// <returns>(OrganAndTissueDonationDetail) Context</returns>
        public static OrganAndTissueDonationDetail CreateOrganAndTissueDonationDetail()
        {
          return new OrganAndTissueDonationDetail();
        }

        #endregion

        #region Serialisation Methods
        /// <summary>
        /// This method serializes this model into an XML document and returns this document
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument SerializeModel()
        {
            XmlDocument xmlDocument = null;
            var dataContractSerializer = new DataContractSerializer(typeof(MedicareOverview));
            
            using(var memoryStream = new MemoryStream())
            {
                xmlDocument = new XmlDocument();

                dataContractSerializer.WriteObject(memoryStream, this);

                memoryStream.Seek(0, SeekOrigin.Begin);

                xmlDocument.Load(memoryStream);
            }

            return xmlDocument;
        }

        /// <summary>
        /// This method deserializes the xml document into an Consolidated View object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static MedicareOverview DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            MedicareOverview MedicareOverview = null;

            var dataContractSerializer = new DataContractSerializer(typeof(MedicareOverview));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                MedicareOverview = (MedicareOverview)dataContractSerializer.ReadObject(memoryStream);
            }

            return MedicareOverview;
        }
        #endregion
    }
}