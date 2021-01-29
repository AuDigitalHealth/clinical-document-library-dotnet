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
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using CDA.Generator.Common.SCSModel.ConsumerAchievements.Entities;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This ConsumerEnteredAchievements object is a composition of the context and content objects that define
    /// a CDA ConsumerEnteredAchievements document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any ConsumerEnteredAchievements
    /// objects that are required to build a valid ConsumerEnteredAchievements CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    public class ConsumerEnteredAchievements : BaseCDAModel
    {
        #region Properties
        /// <summary>
        /// An ICDAContextEReferral that contains the CDA context for this e-referral record
        /// </summary>
        [DataMember]
        public ICDAContextConsumerEnteredAchievements CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this e-referral record
        /// </summary>
        [DataMember]
        public IConsumerEnteredAchievementsContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-referral record
        /// </summary>
        [DataMember]
        public IConsumerEnteredAchievementsContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an ConsumerEnteredAchievements model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public ConsumerEnteredAchievements(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an ConsumerEnteredAchievements model; the status of this document will be 
        /// set to final
        /// </summary>
        public ConsumerEnteredAchievements() : this(DocumentStatus.Final)
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
        public static ConsumerEnteredAchievements CreateConsumerEnteredAchievements()
        {
            return new ConsumerEnteredAchievements();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextEReferral</returns>
        public static ICDAContextConsumerEnteredAchievements CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IEReferralContext</returns>
        public static IConsumerEnteredAchievementsContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IEReferralContent</returns>
        public static IConsumerEnteredAchievementsContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates a NonXmlBody (External Data)
        /// </summary>
        /// <returns>ExternalData</returns>
        private static ExternalData CreateNonXmlBody()
        {
            return new ExternalData();
        }

        /// <summary>
        /// Creates an author.
        /// </summary>
        /// <returns>IParticipationConsumerAuthor</returns>
        public static new IParticipationConsumerAuthor CreateAuthor()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an author.
        /// </summary>
        /// <returns>IParticipationInformationProvider</returns>
        public static IParticipationInformationProvider CreateInformationProvider()
        {
          return new Participation();
        }

        /// <summary>
        /// Creates a person constrained down to an IInformationProvider
        /// </summary>
        /// <returns>IInformationProvider</returns>
        public static IInformationProvider CreateParticipantForInformationProvider()
        {
          return new Participant();
        }

        /// <summary>
        /// Creates an Achievement
        /// </summary>
        /// <returns>Achievement</returns>
        public static Achievement CreateAchievement()
        {
          return new Achievement();
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
