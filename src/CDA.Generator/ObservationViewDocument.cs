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
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.CeHR.Entities;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This ObservationViewDocument object is a composition of the context and content objects that define
    /// a CDA ObservationViewDocument document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any ObservationViewDocument
    /// objects that are required to build a valid ObservationViewDocument CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    public class ObservationViewDocument : BaseCDAModel
    {
        #region Properties
        /// <summary>
      /// An ICDAContextEReferral that contains the CDA context for this ICDAContextObservationViewDocument record
        /// </summary>
        [DataMember]
        public ICDAContextObservationViewDocument CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this IObservationViewDocumentContent record
        /// </summary>
        [DataMember]
        public IObservationViewDocumentContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this IObservationViewDocumentContext record
        /// </summary>
        [DataMember]
        public IObservationViewDocumentContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an E-Referral model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public ObservationViewDocument(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an E-Referral model; the status of this document will be 
        /// set to final
        /// </summary>
        public ObservationViewDocument()
            : this(DocumentStatus.Final)
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
        /// Creates an EReferral
        /// </summary>
        /// <returns>EReferral</returns>
        public static ObservationViewDocument CreateObservationViewDocument()
        {
            return new ObservationViewDocument();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextObservationViewDocument</returns>
        public static ICDAContextObservationViewDocument CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IObservationViewDocumentContext</returns>
        public static IObservationViewDocumentContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IObservationViewDocumentContent</returns>
        public static IObservationViewDocumentContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates a MeasurementComponent
        /// </summary>
        /// <returns>MeasurementComponent</returns>
        public static MeasurementComponent CreateMeasurementComponent()
        {
          return new MeasurementComponent();
        }

        /// <summary>
        /// Creates a MeasurementEntry
        /// </summary>
        /// <returns>MeasurementComponent</returns>
        public static MeasurementEntry CreateMeasurementEntry()
        {
          return new MeasurementEntry();
        }

        #endregion
    }
}
