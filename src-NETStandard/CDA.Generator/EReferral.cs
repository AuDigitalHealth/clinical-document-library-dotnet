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
using System.Linq;
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
    /// This EReferral object is a composition of the context and content objects that define
    /// a CDA EReferral document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any E-Referral
    /// objects that are required to build a valid E-Referral CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    [TemplatePackage(DocumentName = "eReferral", TemplatePackages = ".13(1A) .14(1B) .15(2) .16(3A) .17(3B) .18(1A) .19(1B) .20(2) .21(3A) .22(3B)")]
    public class EReferral : BaseCDAModel
    {
        #region Properties
        /// <summary>
        /// An ICDAContextEReferral that contains the CDA context for this e-referral record
        /// </summary>
        [DataMember]
        public ICDAContextEReferral CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this e-referral record
        /// </summary>
        [DataMember]
        public IEReferralContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-referral record
        /// </summary>
        [DataMember]
        public IEReferralContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an E-Referral model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public EReferral(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an E-Referral model; the status of this document will be 
        /// set to final
        /// </summary>
        public EReferral() : this(DocumentStatus.Final)
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
        public static EReferral CreateEReferral()
        {
            return new EReferral();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextEReferral</returns>
        public static ICDAContextEReferral CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IEReferralContext</returns>
        public static IEReferralContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IEReferralContent</returns>
        public static IEReferralContent CreateSCSContent()
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
        /// Creates a participation for a referee
        ///</summary>
        ///<returns>(IParticipationReferee) Participation</returns>
        public static IParticipationReferee CreateReferee()
        {
            return new Participation();
        }

        ///<summary>
        /// Creates a participation for a usual general practitioner
        ///</summary>
        ///<returns>(IParticipationReferee) Participation</returns>
        public static IParticipationReferee CreateParticipantReferee()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participation for patient nominated contact.
        /// </summary>
        /// <returns>IParticipationPatientNominatedContact</returns>
        public static IParticipationPatientNominatedContact CreateParticipationPatientNominatedContact()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates the patient nominated contact.
        /// </summary>
        /// <returns></returns>
        public static IPatientNominatedContacts CreatePatientNominatedContact()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates the person nominated contact.
        /// </summary>
        /// <returns></returns>
        public static IPersonWithRelationship CreatePersonPatientNominatedContacts()
        {
            return new Person();
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
        /// Creates a participant constrained down to an referee
        /// </summary>
        /// <returns>(IParticipant) Participant</returns>
        public static IReferee CreateParticipantForReferee()
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
        /// <returns>(IMedicationsEReferral) Medications</returns>
        public static IMedicationsEReferral CreateMedications()
        {
            return new Medications();
        }

        /// <summary>
        /// Creates a medication object constrained down to a IMedication
        /// </summary>
        /// <returns>(IMedication) Medication</returns>
        public static IMedicationInstruction CreateMedication()
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
        /// Creates a medical history constrained down to a IMedicalHistoryWithoutExclusions
        /// </summary>
        /// <returns>(IMedicalHistoryWithoutExclusions) MedicalHistoryWithoutExclusions</returns>
        public static IMedicalHistory CreateMedicalHistory()
        {
            return new MedicalHistory();
        }

        /// <summary>
        /// Creates a medical history item
        /// </summary>
        /// <returns>MedicalHistoryItemy</returns>
        public static IMedicalHistoryItem CreateMedicalHistoryItem()
        {
            return new MedicalHistoryItem();
        }

        /// <summary>
        /// Creates a problem diagnosis
        /// </summary>
        /// <returns>ProblemDiagnosis</returns>
        public static IProblemDiagnosis CreateProblemDiagnosis()
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
        /// This method desterilizes the xml document into an eReferral object
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
