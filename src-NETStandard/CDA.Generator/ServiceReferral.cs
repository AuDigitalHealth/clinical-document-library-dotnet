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
using CDA.Generator.Common.SCSModel.ServiceReferral.Entities;
using CDA.Generator.Common.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;
using CDAContext = Nehta.VendorLibrary.CDA.CDAModel.CDAContext;
using Content = Nehta.VendorLibrary.CDA.SCSModel.Common.Content;
using Context = Nehta.VendorLibrary.CDA.SCSModel.Common.Context;
using Medications = Nehta.VendorLibrary.CDA.SCSModel.Common.Medications;
using Participant = Nehta.VendorLibrary.CDA.SCSModel.Common.Participant;
using Participation = Nehta.VendorLibrary.CDA.SCSModel.Common.Participation;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This ServiceReferral object is a composition of the context and content objects that define
    /// a CDA Service Referral document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any ServiceReferral
    /// objects that are required to build a valid Service Referral CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [TemplatePackage(DocumentName = "Service Referral", TemplatePackages = "")]
    public class ServiceReferral : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextServiceReferral that contains the CDA Context for this Service Referral document
        /// </summary>
        [DataMember]
        public ICDAContextServiceReferral CDAContext { get; set; }

        /// <summary>
        /// An IServiceReferralContent that contains the SCS Content for this Service Referral document
        /// </summary>
        [DataMember]
        public IServiceReferralContent SCSContent { get; set; }

        /// <summary>
        /// An IServiceReferralContext that contains the SCS Context for this Service Referral document
        /// </summary>
        [DataMember]
        public IServiceReferralContext SCSContext { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a Service Referral model; the status of this CDA document will be set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public ServiceReferral(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Service Referral model; the status of this document will be set to final
        /// </summary>
        public ServiceReferral() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this ServiceReferral object and its child objects
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
        /// Creates a ServiceReferral
        /// </summary>
        /// <returns>ServiceReferral</returns>
        public static ServiceReferral CreateServiceReferral()
        {
            return new ServiceReferral();
        }

        /// <summary>
        /// Creates a ServiceReferral
        /// </summary>
        /// <returns>ServiceReferral</returns>
        public static ServiceReferral CreateServiceReferral(DocumentStatus documentStatus)
        {
            return new ServiceReferral
            {
                DocumentStatus = documentStatus
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextServiceReferral) Context</returns>
        public static ICDAContextServiceReferral CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IServiceReferralContent) Content</returns>
        public static IServiceReferralContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IServiceReferralContext) Context</returns>
        public static IServiceReferralContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates an exclusion statement
        /// </summary>
        /// <returns>Statement</returns>
        public new static Statement CreateExclusionStatement()
        {
            return new Statement();
        }

        /// <summary>
        /// Creates medications
        /// </summary>
        /// <returns>(IMedications) Medications</returns>
        public static IMedications CreateMedications()
        {
            return new Medications();
        }

        /// <summary>
        /// Current Service
        /// </summary>
        /// <returns>(ICurrentService) Current Service</returns>
        public static ICurrentService CreateCurrentService()
        {
            return new Service();
        }

        /// <summary>
        /// Pending Diagnostic Investigation
        /// </summary>
        /// <returns>(IPendingDiagnosticInvestigation) Pending Diagnostic Investigation</returns>
        public static IPendingDiagnosticInvestigation CreatePendingDiagnosticInvestigation()
        {
            return new Service();
        }

        /// <summary>
        /// Requested Service
        /// </summary>
        /// <returns>(IRequestedService) Requested Service</returns>
        public static IRequestedService CreateRequestedService()
        {
            return new Service();
        }

        /// <summary>
        /// Related Document V1
        /// </summary>
        /// <returns>(RelatedDocumentV1) Related Document V1</returns>
        public static RelatedDocumentV1 CreateRelatedDocumentV1()
        {
            return new RelatedDocumentV1();
        }

        /// <summary>
        /// Document Details V1
        /// </summary>
        /// <returns>(DocumentDetailsV1) Document Details V1</returns>
        public static DocumentDetailsV1 CreateDocumentDetailsV1()
        {
            return new DocumentDetailsV1();
        }

        /// <summary>
        /// Interpreter Required Alert
        /// </summary>
        /// <returns>(DocumentDetailsV1) Document Details V1</returns>
        public static InterpreterRequiredAlert CreateInterpreterRequiredAlert()
        {
            return new InterpreterRequiredAlert();
        }

        /// <summary>
        /// Diagnostic Investigations V1
        /// </summary>
        /// <returns>(DiagnosticInvestigationsV1) Diagnostic Investigations V1</returns>
        public static IDiagnosticInvestigationsV1 CreateDiagnosticInvestigationsV1()
        {
            return new DiagnosticInvestigationsV1();
        }

        /// <summary>
        /// Participation Person or Organisation
        /// </summary>
        /// <returns>(IParticipationPersonOrOrganisation) Participation Person or Organisation</returns>
        public static IParticipationPersonOrOrganisation CreateParticipationPersonOrOrganisation()
        {
            return new Participation();
        }

        /// <summary>
        /// Participant Person or Organisation
        /// </summary>
        /// <returns>(IParticipationPersonOrOrganisation) Participant Person or Organisation</returns>
        public static IPersonOrOrganisation CreateParticipantPersonOrOrganisation()
        {
            return new Participant();
        }

        /// <summary>
        /// Service Referral Detail
        /// </summary>
        /// <returns>(ServiceReferralDetail) The Service Referra lDetail </returns>
        public static ServiceReferralDetail CreateServiceReferralDetail()
        {
            return new ServiceReferralDetail();
        }

        /// <summary>
        /// Alert
        /// </summary>
        /// <returns>(Alert) Alert</returns>
        public static Alert CreateAlert()
        {
            return new Alert();
        }

        #endregion

        #region Serialisation Methods

        /// <summary>
        /// This method serializes this model into an XML document and returns this document
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument SerializeModel()
        {
            XmlDocument xmlDocument;
            var dataContractSerializer = new DataContractSerializer(typeof(ServiceReferral));

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
        /// This method deserializes the xml document into an Service Referral object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static ServiceReferral DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            ServiceReferral serviceReferral;

            var dataContractSerializer = new DataContractSerializer(typeof(ServiceReferral));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                serviceReferral = (ServiceReferral)dataContractSerializer.ReadObject(memoryStream);
            }

            return serviceReferral;
        }
        #endregion
    }
}