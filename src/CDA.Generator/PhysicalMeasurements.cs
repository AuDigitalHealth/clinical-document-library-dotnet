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

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using CDA.Generator.Common.SCSModel.Interfaces;
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This AdvanceCareDirectiveCustodianRecord object is a composition of the context and content objects that define
    /// a CDA AdvanceCareDirectiveCustodianRecord document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any AdvanceCareDirectiveCustodianRecord
    /// objects that are required to build a valid AdvanceCareDirectiveCustodianRecord CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    public class PhysicalMeasurements : BaseCDAModel
    {
        #region Properties

        /// <summary> 
        /// Document Type for the Physical Measurements document
        /// </summary>
        [DataMember]
        public PhysicalMeasurementsDocumentType? StructuredDocumentModelIdentifier { get; set; }

        /// <summary>
        /// An ICDAContextEReferral that contains the CDA context for this e-referral record
        /// </summary>
        [DataMember]
        public ICDAContextPhysicalMeasurements CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this e-referral record
        /// </summary>
        [DataMember]
        public IPhysicalMeasurementsContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-referral record
        /// </summary>
        [DataMember]
        public IPhysicalMeasurementsContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an E-Referral model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public PhysicalMeasurements(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an E-Referral model; the status of this document will be 
        /// set to final
        /// </summary>
        public PhysicalMeasurements() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this EReferral object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("StructuredDocumentModelIdentifier", StructuredDocumentModelIdentifier);

            if (vb.ArgumentRequiredCheck("CDAContext", CDAContext))
                CDAContext.Validate(vb.Path + "CDAContext", vb.Messages);

            if (vb.ArgumentRequiredCheck("SCSContext", SCSContext))
              if (StructuredDocumentModelIdentifier != null)
                SCSContext.Validate(vb.Path + "SCSContext", StructuredDocumentModelIdentifier.Value, vb.Messages);

          if (vb.ArgumentRequiredCheck("SCSContent", SCSContent))
                SCSContent.Validate(vb.Path + "SCSContent", vb.Messages);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates an EReferral
        /// </summary>
        /// <returns>EReferral</returns>
        public static PhysicalMeasurements CreatePhysicalMeasurements()
        {
            return new PhysicalMeasurements();
        }

        /// <summary>
        /// Creates a PhysicalMeasurements
        /// </summary>
        /// <returns>PhysicalMeasurements</returns>
        public static PhysicalMeasurements CreatePhysicalMeasurements(DocumentStatus documentStatus)
        {
            return new PhysicalMeasurements
            {
                DocumentStatus = documentStatus
            };
        }
 
        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextEReferral</returns>
        public static ICDAContextPhysicalMeasurements CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IEReferralContext</returns>
        public static IPhysicalMeasurementsContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IEReferralContent</returns>
        public static IPhysicalMeasurementsContent CreateSCSContent()
        {
          return new Content();
        }

        /// <summary>
        /// Creates HeadCircumference object
        /// </summary>
        /// <returns>HeadCircumference</returns>
        public static HeadCircumference CreateHeadCircumference()
        {
          return new HeadCircumference();
        }

        /// <summary>
        /// Creates PhysicalMeasurement object
        /// </summary>
        /// <returns>PhysicalMeasurement</returns>
        public static PhysicalMeasurement CreatePhysicalMeasurement()
        {
          return new PhysicalMeasurement();
        }

        /// <summary>
        /// Creates PhysicalMeasurementBodyHeightLength object
        /// </summary>
        /// <returns>PhysicalMeasurementBodyHeightLength</returns>
        public static PhysicalMeasurementBodyHeightLength CreatePhysicalMeasurementBodyHeightLength()
        {
          return new PhysicalMeasurementBodyHeightLength();
        }

        /// <summary>
        /// Creates PhysicalMeasurementBodyMassIndex object
        /// </summary>
        /// <returns>PhysicalMeasurementBodyMassIndex</returns>
        public static PhysicalMeasurementBodyMassIndex CreatePhysicalMeasurementBodyMassIndex()
        {
          return new PhysicalMeasurementBodyMassIndex();
        }

        /// <summary>
        /// Creates PhysicalMeasurementBodyWeight object
        /// </summary>
        /// <returns>PhysicalMeasurementBodyWeight</returns>
        public static PhysicalMeasurementBodyWeight CreatePhysicalMeasurementBodyWeight()
        {
          return new PhysicalMeasurementBodyWeight();
        }

        /// <summary>
        /// Creates ReferenceRangeDetails. object
        /// </summary>
        /// <returns>ReferenceRangeDetails.</returns>
        public static ReferenceRangeDetails CreateReferenceRangeDetails()
        {
          return new ReferenceRangeDetails();
        }

        /// <summary>
        /// Creates a Participation constrained down to an IParticipationHealthcareFacility
        /// </summary>
        /// <returns>(Participation) IParticipationHealthcareFacility</returns>
        public static IParticipationHealthcareFacility CreateHealthcareFacility()
        {
          return new Participation();
        }

        /// <summary>
        /// Creates a InformationProviderDevice
        /// </summary>
        /// <returns>InformationProviderDevice</returns>
        public static InformationProviderDevice CreateInformationProviderDevice()
        {
          return new InformationProviderDevice();
        }

        /// <summary>
        /// Creates a Device
        /// </summary>
        /// <returns>Device</returns>
        public static Device CreateDevice()
        {
          return new Device();
        }


        /// <summary>
        /// Creates a Participation constrained down to an IParticipationInformationProviderHealthcareProvider
        /// </summary>
        /// <returns>(Participation) IParticipationInformationProviderHealthcareProvider</returns>
        public static IParticipationInformationProviderHealthcareProvider CreateInformationProviderHealthcareProvider()
        {
          return new Participation();
        }

        /// <summary>
        /// Creates a Participation constrained down to an IParticipationInformationProviderNonHealthcareProvider
        /// </summary>
        /// <returns>(Participation) IParticipationInformationProviderHealthcareProvider</returns>
        public static IParticipationInformationProviderNonHealthcareProvider CreateInformationProviderNonHealthcareProvider()
        {
          return new Participation
                   {
                       Role = CreateCodableText("AGNT", "2.16.840.1.113883.5.110", "HL7RoleClass", null, "agent", null, null)
                   };
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
            var dataContractSerializer = new DataContractSerializer(typeof(EReferral));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument = new XmlDocument();

                dataContractSerializer.WriteObject(memoryStream, this);

                memoryStream.Seek(0, SeekOrigin.Begin);

                xmlDocument.Load(memoryStream);
            }

            return xmlDocument;
        }

        /// <summary>
        /// This method deserializes the xml document into an eReferral object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static EReferral DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            EReferral eReferral = null;

            var dataContractSerializer = new DataContractSerializer(typeof(EReferral));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                eReferral = (EReferral)dataContractSerializer.ReadObject(memoryStream);
            }

            return eReferral;
        }
        #endregion
    }
}
