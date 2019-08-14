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
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;

namespace Nehta.VendorLibrary.CDA.Common
{
    
    /// <summary>
    /// This ConsolidatedView object is a composition of the context and content objects that define
    /// a CDA Consolidated View document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any ConsolidatedView
    /// objects that are required to build a valid Consolidated View CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    public class ConsolidatedView : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextConsolidatedView that contains the CDA Context for this Consolidated View document
        /// </summary>
        [DataMember]
        public ICDAContextConsolidatedView CDAContext { get; set; }

        /// <summary>
        /// An IConsolidatedViewContent that contains the SCS Content for this Consolidated View document
        /// </summary>
        [DataMember]
        public IConsolidatedViewContent SCSContent { get; set; }

        /// <summary>
        /// An IConsolidatedViewContext that contains the SCS Context for this Consolidated View document
        /// </summary>
        [DataMember]
        public IConsolidatedViewContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a Consolidated View model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public ConsolidatedView(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Consolidated View model; the status of this document will be 
        /// set to final
        /// </summary>
        public ConsolidatedView() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this ConsolidatedView object and its child objects
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
        /// Creates a ConsolidatedView
        /// </summary>
        /// <returns>ConsolidatedView</returns>
        public static ConsolidatedView CreateConsolidatedView()
        {
            return new ConsolidatedView();
        }

        /// <summary>
        /// Creates a ConsolidatedView
        /// </summary>
        /// <returns>ConsolidatedView</returns>
        public static ConsolidatedView CreateConsolidatedView(DocumentStatus documentStatus)
        {
            return new ConsolidatedView
            {
                DocumentStatus = documentStatus
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextConsolidatedView) Context</returns>
        public static ICDAContextConsolidatedView CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(IConsolidatedViewContent) Content</returns>
        public static IConsolidatedViewContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(IConsolidatedViewContext) Context</returns>
        public static IConsolidatedViewContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates an IDocument 
        /// </summary>
        /// <returns>Document</returns>
        public static IDocument CreateDocument()
        {
          return new Document();
        }

        /// <summary>
        /// Creates a IDocumentWithHealthEventEnded 
        /// </summary>
        /// <returns>Document</returns>
        public static IDocumentWithHealthEventEnded CreateDocumentWithHealthEventEnded()
        {
          return new Document();
        }

        /// <summary>
        /// Creates a problem diagnosis identification
        /// </summary>
        /// <param name="code">problem diagnosis identification code</param>
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
        /// Creates a problem diagnosis identification
        /// </summary>
        /// <param name="code">problem diagnosis identification code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>CodableText defining a problem diagnosis identification</returns>
        public static ICodableText CreateProblemDiagnosisType(String code, CodingSystem? codeSystem, String displayName, String originalText)
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
        /// Creates a participation
        /// </summary>
        /// <param name="code">participation code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>CodableText defining a participation</returns>
        public static ICodableText CreateParticipation(String code, CodingSystem? codeSystem, String displayName, String originalText)
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
        /// Creates an exclusion statement
        /// </summary>
        /// <returns>Statement</returns>
        public new static Statement CreateExclusionStatement()
        {
            return new Statement();
        }

        /// <summary>
        /// Creats a reviewed medication
        /// </summary>
        /// <returns>(IReviewedMedication) Medication</returns>
        public static IMedication CreateMedication()
        {
            return new Medication();
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
        /// Creates a reviewed medical history
        /// </summary>
        /// <returns>(IReviewedMedicalHistory) MedicalHistory</returns>
        public static IMedicalHistory CreateMedicalHistory()
        {
            return new MedicalHistory();
        }

        /// <summary>
        /// Creates a medical history item
        /// </summary>
        /// <returns>IMedicalHistoryItem</returns>
        public static IMedicalHistoryItem CreateMedicalHistoryItem()
        {
            return new MedicalHistoryItem();
        }

        /// <summary>
        /// Creates a problem diagnosis
        /// </summary>
        /// <returns>IProblemDiagnosis</returns>
        public static IProblemDiagnosis CreateProblemDiagnosis()
        {
            return new ProblemDiagnosis();
        }

        /// <summary>
        /// Creates an immunisation
        /// </summary>
        /// <returns>Immunisation</returns>
        public static IImmunisation CreateImmunisation()
        {
            return new Immunisation();
        }

        /// <summary>
        /// Creates a reviewed immunisations
        /// </summary>
        /// <returns>Immunisations</returns>
        public static Immunisations CreateReviewedImmunisations()
        {
            return new Immunisations();
        }

        /// <summary>
        /// Creates a reviewed adverse substance reactions
        /// </summary>
        /// <returns>(IAdverseReactions) AdverseSubstanceReactions</returns>
        public static IAdverseReactions CreateReviewedAdverseSubstanceReactions()
        {
            return new AdverseReactions();
        }

        /// <summary>
        /// Creates a reaction
        /// </summary>
        /// <returns>Reaction</returns>
        public static Reaction CreateReaction()
        {
            return new Reaction();
        }

        /// <summary>
        /// Creates a statement
        /// </summary>
        /// <returns>Statement</returns>
        public static Statement CreateStatement()
        {
            return new Statement();
        }

        /// <summary>
        /// Creates an adverse substance reaction
        /// </summary>
        /// <returns>Reaction</returns>
        public static Reaction CreateAdverseSubstanceReaction()
        {
            return new Reaction();
        }

        /// <summary>
        /// Creates a substance or agent
        /// </summary>
        /// <param name="code">role substance or agent</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this substance or agent</param>
        /// <returns>CodableText defining a substance or agent</returns>
        public new static ICodableText CreateSubstanceOrAgent(String code, CodingSystem? codeSystem, String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
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
        /// Creates a medical history review
        /// </summary>
        /// <returns>Review</returns>
        public static Review CreateMedicalHistoryReview()
        {
            return new Review();
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
            var dataContractSerializer = new DataContractSerializer(typeof(ConsolidatedView));
            
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
        /// This method deserializes the xml document into an Consolidated View object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static ConsolidatedView DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            ConsolidatedView ConsolidatedView = null;

            var dataContractSerializer = new DataContractSerializer(typeof(ConsolidatedView));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                ConsolidatedView = (ConsolidatedView)dataContractSerializer.ReadObject(memoryStream);
            }

            return ConsolidatedView;
        }
        #endregion
    }
}