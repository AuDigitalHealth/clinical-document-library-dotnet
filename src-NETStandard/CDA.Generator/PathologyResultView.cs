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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This PathologyResultView object is a composition of the context and content objects that define
    /// a CDA Prescription And Dispense View document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any PathologyResultView
    /// objects that are required to build a valid Prescription And Dispense View Record CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    public class PathologyResultView : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextPathologyResultView that contains the CDA Context for this Prescription Record document
        /// </summary>
        [DataMember]
        public ICDAContextPathologyResultView CDAContext { get; set; }

        /// <summary>
        /// An IPathologyResultViewContent that contains the SCS Content for this Prescription Record document
        /// </summary>
        [DataMember]
        public IPathologyResultViewContent SCSContent { get; set; }

        /// <summary>
        /// An IPathologyResultViewContext that contains the SCS Context for this Prescription Record document
        /// </summary>
        [DataMember]
        public IPathologyResultViewContext SCSContext { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a Consolidated View model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public PathologyResultView(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Consolidated View model; the status of this document will be 
        /// set to final
        /// </summary>
        public PathologyResultView() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this PathologyResultView object and its child objects
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
        public static PathologyResultView CreatePathologyResultView()
        {
          return new PathologyResultView();
        }

        /// <summary>
        /// Creates a MedicareOverview
        /// </summary>
        /// <returns>MedicareOverview</returns>
        public static PathologyResultView CreatePathologyResultView(DocumentStatus documentStatus)
        {
          return new PathologyResultView
          {
             DocumentStatus = documentStatus
          };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextPathologyResultView) Context</returns>
        public static ICDAContextPathologyResultView CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IPathologyResultViewContent) Content</returns>
        public static IPathologyResultViewContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IPathologyResultViewContext) Context</returns>
        public static IPathologyResultViewContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a CreateDispensingInformation 
        /// </summary>
        /// <returns>DispensingInformation</returns>
        public static DispensingInformation CreateDispensingInformation()
        {
          return new DispensingInformation();
        }

        /// <summary>
        /// Creates a CreateMedicationEntriesWithSummary 
        /// </summary>
        /// <returns>MedicationEntriesWithSummary</returns>
        public static MedicationEntriesWithSummary CreateMedicationEntriesWithSummary()
        {
          return new MedicationEntriesWithSummary();
        }

        /// <summary>
        /// Creates a CreateSummaryOfMedicationEntries
        /// </summary>
        /// <returns>SummaryOfMedicationEntries</returns>
        public static SummaryOfMedicationEntries CreateSummaryOfMedicationEntries()
        {
          return new SummaryOfMedicationEntries();
        }

        /// <summary>
        /// Creates a CreateMedicationEntry
        /// </summary>
        /// <returns>MedicationEntry</returns>
        public static MedicationEntry CreateMedicationEntry()
        {
          return new MedicationEntry();
        }

        /// <summary>
        /// Creates a CreateMedicationEntry
        /// </summary>
        /// <returns>MedicationEntry</returns>
        public static IPCEHRDispenseItemView CreateDispenseItemView()
        {
          return new PCEHRDispenseItem();
        }

        /// <summary>
        /// Creates a CreateMedicationEntry
        /// </summary>
        /// <returns>IPrescriptionItemView</returns>
        public static IPCEHRPrescriptionItemView CreatePrescriptionItemView()
        {
          return new PCEHRPrescriptionItem();
        }

        /// <summary>
        /// Creates a CreateMedicationEntry
        /// </summary>
        /// <returns>PrescribingAndDispensingReports</returns>
        public static PrescribingAndDispensingReports CreatePrescribingAndDispensingReports()
        {
          return new PrescribingAndDispensingReports();
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
            var dataContractSerializer = new DataContractSerializer(typeof(PathologyResultView));
            
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
        public static PathologyResultView DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            PathologyResultView PathologyResultView = null;

            var dataContractSerializer = new DataContractSerializer(typeof(PathologyResultView));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                PathologyResultView = (PathologyResultView)dataContractSerializer.ReadObject(memoryStream);
            }

            return PathologyResultView;
        }
        #endregion
    }
}