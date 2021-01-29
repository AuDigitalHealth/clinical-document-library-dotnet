using System;
using System.Collections.Generic;
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

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This DispenseRecord object is a composition of the context and content objects that define
    /// a CDA Dispense Record document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any DispenseRecord
    /// objects that are required to build a valid DispenseRecord CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    public class DispenseRecord : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextDispenseRecordContent that contains the CDA Context for this Dispense Record document
        /// </summary>
        [DataMember]
        public ICDAContextDispenseRecord CDAContext { get; set; }

        /// <summary>
        /// An IDispenseRecordContent that contains the Content for this Dispense Record record
        /// </summary>
        [DataMember]
        public IDispenseRecordContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the Context for this Dispense Record record
        /// </summary>
        [DataMember]
        public IDispenseRecordContext SCSContext { get; set; }

        #endregion
 
        #region Constructors
        /// <summary>
        /// Instantiates a Dispense Record model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public DispenseRecord(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Dispense Record model; the status of this document will be set to final
        /// </summary>
        public DispenseRecord() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an DispenseRecord
        /// </summary>
        /// <returns>DispenseRecord</returns>
        public static DispenseRecord CreateDispenseRecord()
        {
            return new DispenseRecord();
        }

        /// <summary>
        /// Creates an DispenseRecord
        /// </summary>
        /// <returns>Dispense Record</returns>
        public static DispenseRecord CreateDispenseRecord(DocumentStatus documentStatus)
        {
            return new DispenseRecord
            {
                DocumentStatus = documentStatus
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextDispenseRecordContent) Context</returns>
        public static ICDAContextDispenseRecord CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IDispenseRecordContent) Content</returns>
        public static IDispenseRecordContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IDispenseRecordContext) Context</returns>
        public static IDispenseRecordContext CreateSCSContext()
        {
            return new Context();
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
        /// Creates a Dispense Item
        /// </summary>
        /// <returns>A codable text representing the Dispense Item</returns>
        public static DispenseItem CreateDispenseItem()
        {
          return new DispenseItem();
        }

        /// <summary>
        /// Creates a Therapeutic Good ID
        /// </summary>
        /// <returns>(ICodableText) CodableText</returns>
        public static ICodableText CreateTherapeuticGoodId()
        {
            return new CodableText();
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this DispenseRecord object and its child objects
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
            var dataContractSerializer = new DataContractSerializer(typeof(DispenseRecord));

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
        /// This method deserializes the xml document into an DispenseRecord object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static DispenseRecord DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            DispenseRecord DispenseRecord = null;

            var dataContractSerializer = new DataContractSerializer(typeof(DispenseRecord));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                DispenseRecord = (DispenseRecord)dataContractSerializer.ReadObject(memoryStream);
            }

            return DispenseRecord;
        }
        #endregion
    }
}