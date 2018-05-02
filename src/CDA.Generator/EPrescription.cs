using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This EPrescription object is a composition of the context and content objects that define
    /// a CDA EPrescription document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any EPrescription
    /// objects that are required to build a valid EPrescription CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    public class EPrescription : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextEPrescription that contains the CDA Context for this ePrescription document
        /// </summary>
        [DataMember]
        public ICDAContextEPrescription CDAContext { get; set; }

        /// <summary>
        /// An IEPrescriptionContext that contains the content for this ePrescription record
        /// </summary>
        [DataMember]
        public IEPrescriptionContext SCSContext { get; set; }

        /// <summary>
        /// An IEPrescriptionContent that contains the content for this ePrescription record
        /// </summary>
        [DataMember]
        public IEPrescriptionContent SCSContent { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an ePrescription
        /// </summary>
        /// <returns>EPrescription</returns>
        public static EPrescription CreatePrescription()
        {
          return new EPrescription
                   {
                        DocumentStatus = DocumentStatus.Undefined
                   };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextEPrescription) Context</returns>
        public static ICDAContextEPrescription CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IEPrescriptionContent) Content</returns>
        public static IEPrescriptionContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IEPrescriptionContext) Context</returns>
        public static IEPrescriptionContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a Prescription Item
        /// </summary>
        /// <returns>(IEPrescriptionItem) PrescriptionItem</returns>
        public static IEPrescriptionItem CreatePrescriptionItem()
        {
          return new PrescriptionItem();
        }

        /// <summary>
        /// Creates a AdministrationDetails
        /// </summary>
        /// <returns>AdministrationDetails</returns>
        public static AdministrationDetails CreateAdministrationDetails()
        {
            return new AdministrationDetails();
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this EPrescription object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("CDAContext", CDAContext))
            {
                CDAContext.Validate(vb.Path + "CDAContext", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SCSContext", SCSContext))
            {
                SCSContext.Validate(vb.Path + "SCSContext", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SCSContent", SCSContent))
            {
              SCSContent.Validate(vb.Path + "SCSContent", vb.Messages);
            }
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
            var dataContractSerializer = new DataContractSerializer(typeof(EPrescription));

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
        /// This method deserializes the xml document into an ePrescription object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static EPrescription DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            EPrescription ePrescription = null;

            var dataContractSerializer = new DataContractSerializer(typeof(EPrescription));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                ePrescription = (EPrescription)dataContractSerializer.ReadObject(memoryStream);
            }

            return ePrescription;
        }
        #endregion
    }
}