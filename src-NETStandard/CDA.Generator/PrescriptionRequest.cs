using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This PrescriptionRequest object is a composition of the context and content objects that define
    /// a CDA Prescription Request document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any PrescriptionRequest
    /// objects that are required to build a valid PrescriptionRequest CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    public class EPrescriptionRequest : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextPrescriptionRequestContent that contains the CDA Context for this prescription request document
        /// </summary>
        [DataMember]
        public ICDAContextPrescriptionRequestContent CDAContext { get; set; }

        /// <summary>
        /// An IPrescriptionRequestContext that contains the Context for this prescription request record
        /// </summary>
        [DataMember]
        public IPrescriptionRequestContext SCSContext { get; set; }

        /// <summary>
        /// An IPrescriptionRequestContent that contains the Content for this prescription request record
        /// </summary>
        [DataMember]
        public IPrescriptionRequestContent SCSContent { get; set; }

        #endregion
 
        #region Constructors
        /// <summary>
        /// Instantiates a Prescription Request model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public EPrescriptionRequest(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Prescription Request model; the status of this document will be 
        /// set to final
        /// </summary>
        public EPrescriptionRequest() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an EPrescriptionRequest
        /// </summary>
        /// <returns>EPrescriptionRequest</returns>
        public static EPrescriptionRequest CreatePrescriptionRequest()
        {
            return new EPrescriptionRequest();
        }

        /// <summary>
        /// Creates an EPrescriptionRequest
        /// </summary>
        /// <returns>EPrescriptionRequest</returns>
        public static EPrescriptionRequest CreatePrescriptionRequest(DocumentStatus documentStatus)
        {
            return new EPrescriptionRequest
            {
                DocumentStatus = documentStatus
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextPrescriptionRequestContent) Context</returns>
        public static ICDAContextPrescriptionRequestContent CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IPrescriptionRequestContent) Content</returns>
        public static IPrescriptionRequestContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IPrescriptionRequestContext) Context</returns>
        public static IPrescriptionRequestContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a Prescriber Instruction Detail
        /// </summary>
        /// <returns>PrescriberInstructionDetail</returns>
        public static PrescriberInstructionDetail CreatePrescriberInstructionDetail()
        {
            return new PrescriberInstructionDetail();
        }

        /// <summary>
        /// Creates a prescriber instruction recipient
        /// </summary>
        /// <returns></returns>
        public static IParticipationPrescriberInstructionRecipient CreatePrescriberInstructionRecipient()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participant for a prescriber instruction recipient
        /// </summary>
        /// <returns>(IPrescriberInstructionRecipient) Participant</returns>
        public static IPrescriberInstructionRecipient CreateParticipantForPrescriberInstructionRecipient()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a PrescriptionRequestItem
        /// </summary>
        /// <returns>PrescriptionRequestItem</returns>
        public static PrescriptionRequestItem CreatePrescriptionRequestItem()
        {
            return new PrescriptionRequestItem();
        }

        /// <summary>
        /// Creates a Person Prescriber Instruction Recipient
        /// </summary>
        /// <returns>IPersonPrescriberInstructionRecipient</returns>
        public static IPersonPrescriberInstructionRecipient CreatePersonPrescriberInstructionRecipient()
        {
            return new Person();
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
        /// Validates this PrescriptionRequest object and its child objects
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
            var dataContractSerializer = new DataContractSerializer(typeof(EPrescriptionRequest));

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
        /// This method deserializes the xml document into an PrescriptionRequest object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static EPrescriptionRequest DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            EPrescriptionRequest prescriptionRequest = null;

            var dataContractSerializer = new DataContractSerializer(typeof(EPrescriptionRequest));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                prescriptionRequest = (EPrescriptionRequest)dataContractSerializer.ReadObject(memoryStream);
            }
            return prescriptionRequest;
        }

        #endregion
    }
}