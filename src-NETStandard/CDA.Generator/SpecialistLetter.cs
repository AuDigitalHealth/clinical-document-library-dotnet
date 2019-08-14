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
    /// This SpecialistLetter object is a composition of the context and content objects that define
    /// a CDA specialist letter document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any specialist letter
    /// objects that are required to build a valid specialist letter CDA document
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [TemplatePackage(DocumentName = "Specialist Letter", TemplatePackages = ".23(1A) .24(1B) .25(2) .26(3A) .27(3B) .28(1A) .29(1B) .30(2) .31(3A) .32(3B)")]  
    public class SpecialistLetter : BaseCDAModel
    {
        #region Properties
        /// <summary>
        /// An ICDAContextEReferral that contains the CDA context for this e-referral record
        /// </summary>
        [DataMember]
        public ICDAContextSpecialistLetter CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this e-referral record
        /// </summary>
        [DataMember]
        public ISpecialistLetterContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-referral record
        /// </summary>
        [DataMember]
        public ISpecialistLetterContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an specialist letter model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public SpecialistLetter(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an specialist letter model; the status of this document will be 
        /// set to final
        /// </summary>
        public SpecialistLetter()
            : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this SpecialistLetter object and its child objects
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
        /// Creates a SpecialistLetter
        /// </summary>
        /// <returns>SpecialistLetter</returns>
        public static SpecialistLetter CreateSpecialistLetter()
        {
            return new SpecialistLetter();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextSpecialistLetter</returns>
        public static ICDAContextSpecialistLetter CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>ISpecialistLetterContext</returns>
        public static ISpecialistLetterContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>ISpecialistLetterContent</returns>
        public static ISpecialistLetterContent CreateSCSContent()
        {
            return new Content();
        }

        ///<summary>
        /// Creates a participation for a usual general practitioner
        ///</summary>
        ///<returns>(IParticipationUsualGP) Participation</returns>
        public static IParticipationUsualGP CreateUsualGP()
        {
            return new Participation();
        }

        ///<summary>
        /// Creates a participation for a referrer
        ///</summary>
        ///<returns>(IParticipationReferrer) Participation</returns>
        public static IParticipationReferrer CreateReferrer()
        {
            return new Participation();
        }

        ///<summary>
        /// Creates a participation for a usual general practitioner
        ///</summary>
        ///<returns>(IParticipationReferrer)Participation</returns>
        public static IParticipationReferrer CreateParticipantReferrer()
        {
            return new Participation();
        }
        
        
        /// <summary>
        /// Creates a participant constrained down to an usual general practitioner
        /// </summary>
        /// <returns>(IParticipant) Participant</returns>
        public static IUsualGP CreateParticipantForUsualGP()
        {
            return new Participant();    
        }

        /// <summary>
        /// Creates a participant constrained down to an referrer
        /// </summary>
        /// <returns>(IReferrer) Participant</returns>
        public static IReferrer CreateParticipantForReferrer()
        {
            return new Participant();
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
        /// Creates a medications object constrained down to a IMedicationsEReferral
        /// </summary>
        /// <returns>(IMedicationsSpecialistLetter) Medications</returns>
        public static IMedicationsSpecialistLetter CreateMedications()
        {
            return new Medications();
        }

        /// <summary>
        /// Creates a medication object constrained down to a IMedicationItem
        /// </summary>
        /// <returns>(IMedicationItem) Medication</returns>
        public static IMedicationItem CreateMedication()
        {
            return new Medication();
        }

        /// <summary>
        /// Creates a procedure
        /// </summary>
        /// <returns>Procedure</returns>
        public static IProcedureName CreateProcedure()
        {
            return new Procedure();
        }

        /// <summary>
        /// Creates a problem diagnosis
        /// </summary>
        /// <returns>ProblemDiagnosis</returns>
        public static ProblemDiagnosis CreateProblemDiagnosis()
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
            var codableText = new CodableText();

            if (codeSystem.HasValue)
            {
                codableText.DisplayName = displayName;
                codableText.Code = code;
                codableText.CodeSystemCode = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Code);
                codableText.CodeSystemName = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Name);
                codableText.CodeSystemVersion = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version);
            }

            if (!originalText.IsNullOrEmptyWhitespace())
                codableText.OriginalText = originalText;

            return codableText;
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
        /// Creates adverse reactions without exclusions.
        /// </summary>
        /// <returns></returns>
        public static IAdverseReactionsWithoutExclusions CreateAdverseReactionsWithoutExclusions()
        {
            return new AdverseReactions();
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
        /// Creates ar review object for a reviewed adverse substance reaction
        /// </summary>
        /// <returns>Review</returns>
        public static Review CreateReviewedAdverseSubstanceReaction()
        {
            return new Review();
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
        /// Creats a response details
        /// </summary>
        /// <returns>(IResponseDetails)Response</returns>
        public static IResponseDetails CreateResponseDetails()
        {
            return new Response();
        }

        /// <summary>
        /// Creats a recommendation
        /// </summary>
        /// <returns>(IRecommendations) Recommendations</returns>
        public static IRecommendations CreateRecommendations()
        {
            return new Recommendations();
        }

        /// <summary>
        /// Creates a Recommendation
        /// </summary>
        /// <returns>Recomendation</returns>
        public static Recommendation CreateRecommendation()
        {
            return new Recommendation();
        }

        /// <summary>
        /// Creates a participation for an Addressee
        /// </summary>
        /// <returns>IParticipation</returns>
        public static IParticipationAddressee CreateAddressee()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Addressee
        /// </summary>
        /// <returns>(IAddressee) Participant</returns>
        public static IAddressee CreateParticipantAddressee()
        {
            return new Participant();
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
            var dataContractSerializer = new DataContractSerializer(typeof(SpecialistLetter));

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
        /// This method deserializes the xml document into an specialist letter object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static SpecialistLetter DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            SpecialistLetter specialistLetter = null;

            var dataContractSerializer = new DataContractSerializer(typeof(SpecialistLetter));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                specialistLetter = (SpecialistLetter)dataContractSerializer.ReadObject(memoryStream);
            }

            return specialistLetter;
        }
        #endregion
    }
}
