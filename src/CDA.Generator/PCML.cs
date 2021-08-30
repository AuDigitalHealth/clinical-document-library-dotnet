using System.Collections.Generic;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This PCML object is a composition of the context and content objects that define
    /// a CDA PCML document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any PCML
    /// objects that are required to build a valid PCML CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    public class PCML : BaseCDAModel
    {
        #region Properties
        /// <summary>
        /// An ICDAContextEReferral that contains the CDA context for this e-referral record
        /// </summary>
        [DataMember]
        public ICDAContextPCML CDAContext { get; set; }

        /// <summary>
        /// An IEReferralContent that contains the content for this e-referral record
        /// </summary>
        [DataMember]
        public IPCMLContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-referral record
        /// </summary>
        [DataMember]
        public IPCMLContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an E-Referral model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public PCML(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an E-Referral model; the status of this document will be 
        /// set to final
        /// </summary>
        public PCML() : this(DocumentStatus.Final)
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
        public static PCML CreatePCML()
        {
            return new PCML();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>ICDAContextEReferral</returns>
        public static ICDAContextPCML CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>IEReferralContext</returns>
        public static IPCMLContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>IEReferralContent</returns>
        public static IPCMLContent CreateSCSContent()
        {
            return new Content();
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
        /// Creates a participation
        /// </summary>
        /// <returns></returns>
        public static IParticipationPersonOrOrganisation CreateParticipationPersonOrOrganisation()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a person or organisation
        /// </summary>
        /// <returns></returns>
        public static IPersonOrOrganisation CreateParticipantPersonOrOrganisation()
        {
            return new Participant();
        }


        #endregion
    }
}