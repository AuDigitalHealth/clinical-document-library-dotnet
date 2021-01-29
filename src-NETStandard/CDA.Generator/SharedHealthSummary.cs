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
    /// This SharedHealthSummary object is a composition of the context and content objects that define
    /// a CDA Shared Health Summary document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any SharedHealthSummary
    /// objects that are required to build a valid Shared Health Summary CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [TemplatePackage(DocumentName = "Shared Health Summary", TemplatePackages = ".9(3A) .9(3B) .10(3A) .11(3B)")]
    public class SharedHealthSummary : BaseCDAModel
    {
        #region Properties

        /// <summary>
        /// An ICDAContextSharedHealthSummary that contains the CDA Context for this Shared Health Summary document
        /// </summary>
        [DataMember]
        public ICDAContextSharedHealthSummary CDAContext { get; set; }

        /// <summary>
        /// An ISharedHealthSummaryContent that contains the SCS Content for this Shared Health Summary document
        /// </summary>
        [DataMember]
        public ISharedHealthSummaryContent SCSContent { get; set; }

        /// <summary>
        /// An ISharedHealthSummaryContext that contains the SCS Context for this Shared Health Summary document
        /// </summary>
        [DataMember]
        public ISharedHealthSummaryContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a Shared Health Summary model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public SharedHealthSummary(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates a Shared Health Summary model; the status of this document will be 
        /// set to final
        /// </summary>
        public SharedHealthSummary()
            : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this SharedHealthSummary object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("CDAContext.LegalAuthenticator", CDAContext.LegalAuthenticator);

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
        /// Creates a SharedHealthSummary
        /// </summary>
        /// <returns>SharedHealthSummary</returns>
        public static SharedHealthSummary CreateSharedHealthSummary()
        {
            return new SharedHealthSummary();
        }

        /// <summary>
        /// Creates a SharedHealthSummary
        /// </summary>
        /// <returns>SharedHealthSummary</returns>
        public static SharedHealthSummary CreateSharedHealthSummary(DocumentStatus documentStatus)
        {
            return new SharedHealthSummary
            {
                DocumentStatus = documentStatus
            };
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextSharedHealthSummary) Context</returns>
        public static ICDAContextSharedHealthSummary CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates an SCS content
        /// </summary>
        /// <returns>(ISharedHealthSummaryContent) Content</returns>
        public static ISharedHealthSummaryContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates an SCS context
        /// </summary>
        /// <returns>(ISharedHealthSummaryContext) Context</returns>
        public static ISharedHealthSummaryContext CreateSCSContext()
        {
            return new Context();
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
        /// Creates medications
        /// </summary>
        /// <returns>(IMedications) Medications</returns>
        public static IMedications CreateMedications()
        {
            return new Medications();
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
        /// Creates a statement
        /// </summary>
        /// <returns>Statement</returns>
        public new static Statement CreateExclusionStatement()
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
        /// Creates an identifier
        /// </summary>
        /// <returns>Identifier</returns>
        public static Identifier CreateIdentifier()
        {
            return new Identifier();
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
            var dataContractSerializer = new DataContractSerializer(typeof(SharedHealthSummary));
            
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
        /// This method deserializes the xml document into an shared health summary object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static SharedHealthSummary DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            SharedHealthSummary sharedHealthSummary = null;

            var dataContractSerializer = new DataContractSerializer(typeof(SharedHealthSummary));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                sharedHealthSummary = (SharedHealthSummary)dataContractSerializer.ReadObject(memoryStream);
            }

            return sharedHealthSummary;
        }
        #endregion
    }
}