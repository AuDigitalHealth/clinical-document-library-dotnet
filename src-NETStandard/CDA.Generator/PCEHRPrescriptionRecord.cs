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
using CDA.Generator.Common.Common.Attributes;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This PCEHRPrescriptionRecord object is a composition of the context and content objects that define
    /// a CDA Prescription Record document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any PCEHRPrescriptionRecord
    /// objects that are required to build a valid Prescription Record CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [TemplatePackage(DocumentName = "NPDR Prescription", TemplatePackages = ".4(3A) .5(3A)")]
    public class PCEHRPrescriptionRecord : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextPrescriptionRecord that contains the CDA Context for this Prescription Record document
        /// </summary>
        [DataMember]
        public ICDAContextPCEHRPrescriptionRecord CDAContext { get; set; }

        /// <summary>
        /// An IPrescriptionRecordContent that contains the SCS Content for this Prescription Record document
        /// </summary>
        [DataMember]
        public IPCEHRPrescriptionRecordContent SCSContent { get; set; }

        /// <summary>
        /// An IPrescriptionRecordContext that contains the SCS Context for this Prescription Record document
        /// </summary>
        [DataMember]
        public IPCEHRPrescriptionRecordContext SCSContext { get; set; }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this PCEHRPrescriptionRecord object and its child objects
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
        /// Creates a PCEHRPrescriptionRecord
        /// </summary>
        /// <returns>PCEHRPrescriptionRecord</returns>
        public static PCEHRPrescriptionRecord CreatePrescriptionRecord()
        {
            return new PCEHRPrescriptionRecord
            {
              DocumentStatus = DocumentStatus.Undefined
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextPrescriptionRecord) Context</returns>
        public static ICDAContextPCEHRPrescriptionRecord CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IPrescriptionRecordContent) Content</returns>
        public static IPCEHRPrescriptionRecordContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IPrescriptionRecordContext) Context</returns>
        public static IPCEHRPrescriptionRecordContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a Prescription Item
        /// </summary>
        /// <returns>PrescriptionItem</returns>
        public static IPCEHRPrescriptionItem CreatePrescriptionItem()
        {
            return new PCEHRPrescriptionItem();
        }

        /// <summary>
        /// Creates a CreateDispensingInformation 
        /// </summary>
        /// <returns>DispensingInformation</returns>
        public static DispensingInformation CreateDispensingInformation()
        {
          return new DispensingInformation();
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
            var dataContractSerializer = new DataContractSerializer(typeof(PCEHRPrescriptionRecord));
            
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
        /// This method deserializes the xml document into an Prescription Record object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static PCEHRPrescriptionRecord DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            PCEHRPrescriptionRecord pcehrPrescriptionRecord = null;

            var dataContractSerializer = new DataContractSerializer(typeof(PCEHRPrescriptionRecord));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                pcehrPrescriptionRecord = (PCEHRPrescriptionRecord)dataContractSerializer.ReadObject(memoryStream);
            }

            return pcehrPrescriptionRecord;
        }
        #endregion
    }
}