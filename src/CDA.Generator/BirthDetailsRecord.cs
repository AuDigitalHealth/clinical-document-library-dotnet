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
using CDA.Generator.Common.SCSModel.CeHR.Entities;
using CDA.Generator.Common.SCSModel.CeHR.Enum;
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
    /// This BirthDetailsRecord object is a composition of the context and content objects that define
    /// a CDA BirthDetailsRecord document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any BirthDetailsRecord
    /// objects that are required to build a valid BirthDetailsRecord CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    public class BirthDetailsRecord : BaseCDAModel
    {
        #region Properties
        /// <summary>
        /// An ICDAContextEReferral that contains the CDA context for this e-referral record
        /// </summary>
        [DataMember]
        public ICDAContextBirthDetailsRecord CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this e-referral record
        /// </summary>
        [DataMember]
        public IBirthDetailsRecordContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-referral record
        /// </summary>
        [DataMember]
        public IBirthDetailsRecordContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an E-Referral model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public BirthDetailsRecord(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an E-Referral model; the status of this document will be 
        /// set to final
        /// </summary>
        public BirthDetailsRecord()
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
        public static BirthDetailsRecord CreateBirthDetailsRecord()
        {
            return new BirthDetailsRecord();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextEReferral</returns>
        public static ICDAContextBirthDetailsRecord CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IEReferralContext</returns>
        public static IBirthDetailsRecordContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IEReferralContent</returns>
        public static IBirthDetailsRecordContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates a MeasurementInformation
        /// </summary>
        /// <returns>MeasurementInformation</returns>
        public static MeasurementInformation CreateMeasurementInformation()
        {
          return new MeasurementInformation();
        }

        /// <summary>
        /// Creates a HealthCheckAssesment
        /// </summary>
        /// <returns>HealthCheckAssesment</returns>
        public static HealthCheckAssesment CreateHealthCheckAssesment()
        {
          return new HealthCheckAssesment();
        }

        /// <summary>
        /// Creates a AssessmentItem
        /// </summary>
        /// <returns>AssessmentItem</returns>
        public static AssessmentItem CreateAssessmentItem()
        {
          return new AssessmentItem();
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
        /// Creates a Birth Details object constrained  
        /// </summary>
        /// <returns>(BirthDetails) BirthDetails</returns>
        public static BirthDetails CreateBirthDetails()
        {
          return new BirthDetails();
        }

        /// <summary>
        /// Creates a Create Question
        /// </summary>
        /// <returns>AssessmentItem</returns>
        public static ICodableText CreateAssesmentItemTitle(HealthCheckAssessmentQuestion healthCheckAssessmentQuestion)
        {
          return new CodableText
          {
            DisplayName = healthCheckAssessmentQuestion.GetAttributeValue<NameAttribute, string>(x => x.Name),
            Code = healthCheckAssessmentQuestion.GetAttributeValue<NameAttribute, string>(x => x.Code),
            CodeSystemCode = ((CodingSystem)Enum.Parse(typeof(CodingSystem), healthCheckAssessmentQuestion.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem))).GetAttributeValue<NameAttribute, string>(x => x.Code),
            CodeSystemName = ((CodingSystem)Enum.Parse(typeof(CodingSystem), healthCheckAssessmentQuestion.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem))).GetAttributeValue<NameAttribute, string>(x => x.Name),
            CodeSystemVersion = ((CodingSystem)Enum.Parse(typeof(CodingSystem), healthCheckAssessmentQuestion.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem))).GetAttributeValue<NameAttribute, string>(x => x.Version)
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
