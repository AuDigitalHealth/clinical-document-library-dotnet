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
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This EventSummary object is a composition of the context and content objects that define
    /// a CDA EventSummary document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any event summary
    /// objects that are required to build a valid event summary CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [TemplatePackage(DocumentName = "Event Summary", TemplatePackages = ".12(3A) .13(3B) .14(3A) .15(3B)")]
    public class EventSummary : BaseCDAModel
    {
        #region Constants

        private const String HealthIdentifierQualifier = "1.2.36.1.2001.1003.0.";

        #endregion

        #region Properties
        /// <summary>
        /// An ICDAContextEventSummary that contains the CDA context for this event summary record
        /// </summary>
        [DataMember]
        public ICDAContextEventSummary CDAContext { get; set; }

        /// <summary>
        /// An IEventSummaryContent that contains the content for this event summary record
        /// </summary>
        [DataMember]
        public IEventSummaryContent SCSContent { get; set; }

        /// <summary>
        /// An IEventSummaryContext that contains the context for this event summary record
        /// </summary>
        [DataMember]
        public IEventSummaryContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an event summary model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public EventSummary(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an event summary model; the status of this document will be 
        /// set to final
        /// </summary>
        public EventSummary() : this(DocumentStatus.Final)
        {
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this EventSummary object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("CDAContext", CDAContext))
                CDAContext.ValidateWithMandatoryLegalAuthenticator(vb.Path + "CDAContext", vb.Messages);

            if (vb.ArgumentRequiredCheck("SCSContext", SCSContext))
                SCSContext.Validate(vb.Path + "SCSContext", vb.Messages);

            if (vb.ArgumentRequiredCheck("SCSContent", SCSContent))
                SCSContent.Validate(vb.Path + "SCSContent", vb.Messages);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates an EventSummary
        /// </summary>
        /// <returns>EventSummary</returns>
        public static EventSummary CreateEventSummary()
        {
            return new EventSummary();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextEventSummary</returns>
        public static ICDAContextEventSummary CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IEventSummaryContext</returns>
        public static IEventSummaryContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IEventSummaryContent</returns>
        public static IEventSummaryContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates a medications object constrained down to a IMedicationItem
        /// </summary>
        /// <returns>(IMedicationItem) Medications</returns>
        public static IMedicationItem CreateMedication()
        {
            return new Medication();
        }

        /// <summary>
        /// Creates a procedure
        /// </summary>
        /// <returns>Procedure</returns>
        public static Procedure CreateProcedure()
        {
            return new Procedure();
        }


        /// <summary>
        /// Creates a medical history item
        /// </summary>
        /// <returns>IMedicalHistoryItemEventSummary</returns>
        public static IMedicalHistoryItem CreateMedicalHistoryItem()
        {
            return new MedicalHistoryItem();
        }

        /// <summary>
        /// Creates a problem diagnosis
        /// </summary>
        /// <returns>IProblemDiagnosis</returns>
        public static IProblemDiagnosisEventSummary CreateProblemDiagnosis()
        {
            return new ProblemDiagnosis();
        }

        /// <summary>
        /// Creates a problem diagnosis identification
        /// </summary>
        /// <param name="code">Problem diagnosis identification code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>CodableText defining a problem diagnosis identification</returns>
        public static ICodableText CreateProblemDiagnosisIdentification(String code, CodingSystem? codeSystem, String displayName, String originalText)
        {
            if (codeSystem.HasValue)
            {
                return new CodableText
                {
                    DisplayName = displayName,
                    Code = code,
                    CodeSystemCode = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Code),
                    CodeSystemName = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Name),
                    CodeSystemVersion = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version),
                    OriginalText = originalText
                };
            }
            else
            {
                return new CodableText()
                {
                    OriginalText = originalText
                };
            }
        }

        /// <summary>
        /// Creates adverse reactions without exclusions.
        /// </summary>
        /// <returns></returns>
        public static IAdverseReactionsWithoutExclusions CreateAdverseReactions()
        {
            return new AdverseReactions();
        }

        /// <summary>
        /// Creates a reaction.
        /// </summary>
        /// <returns></returns>
        public static Reaction CreateReaction()
        {
            return new Reaction();
        }

        /// <summary>
        /// Creates an imaging examination result
        /// </summary>
        /// <returns>ImagingExaminationResult</returns>
        public new static IImagingExaminationResult CreateImagingExaminationResult()
        {
            return new ImagingExaminationResult();
        }
        
        /// <summary>
        /// Creates an Immunisation
        /// </summary>
        /// <returns>Immunisation</returns>
        public static IImmunisation CreateImmunisation()
        {
            return new Immunisation();
        }

        /// <summary>
        /// Creates a Requested Service
        /// </summary>
        /// <returns>RequestedService</returns>
        public static RequestedService CreateRequestedService()
        {
            return new RequestedService();
        }

        /// <summary>
        /// Create a Person for a Service Provider
        /// </summary>
        /// <returns>(IPerson) Person</returns>
        public static IPersonWithOrganisation CreatePersonForServiceProvider()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a DiagnosesIntervention
        /// </summary>
        /// <returns>DiagnosesIntervention</returns>
        public static DiagnosesIntervention CreateDiagnosesInterventions()
        {
            return new DiagnosesIntervention();
        }
        
        /// <summary>
        /// Creates event details
        /// </summary>
        /// <returns>EventDetails</returns>
        public static EventDetails CreateEventDetails()
        {
            return new EventDetails();
        }

        #region Serialisation Methods

        /// <summary>
        /// This method serializes this model into an XML document and returns this document
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument SerializeModel()
        {
            XmlDocument xmlDocument = null;
            var dataContractSerializer = new DataContractSerializer(typeof(EventSummary));

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
        /// This method deserializes the xml document into an EventSummary object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static EventSummary DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            EventSummary eventSummary = null;

            var dataContractSerializer = new DataContractSerializer(typeof(EventSummary));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                eventSummary = (EventSummary)dataContractSerializer.ReadObject(memoryStream);
            }

            return eventSummary;
        }
        #endregion

        #endregion
    }
}
