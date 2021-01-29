using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ServiceReferral.Entities
{
    /// <summary>
    /// The Service Referral Detail Class
    /// </summary>
    public class ServiceReferralDetail
    {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Requested Service
        /// </summary>
        [CanBeNull]
        public IList<IRequestedService> RequestedService { get; set; }

        /// <summary>
        /// Alert
        /// </summary>
        [CanBeNull]
        public IList<Alert> OtherAlerts { get; set; }

        /// <summary>
        /// Related Document
        /// </summary>
        [CanBeNull]
        public IList<RelatedDocumentV1> RelatedDocument { get; set; }

        #region Constructors

        internal ServiceReferralDetail()
        {

        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this Service Referral Detail object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("RequestedService", RequestedService))
            {
                for (var x = 0; x < RequestedService.Count; x++)
                {
                    RequestedService[x].Validate(vb.Path + $"RequestedService[{x}]", vb.Messages);
                }
            }

            if (OtherAlerts != null)
            {
                for (var x = 0; x < OtherAlerts.Count; x++)
                {
                    OtherAlerts[x].Validate(vb.Path + $"OtherAlerts[{x}]", vb.Messages);
                }
            }

            if (OtherAlerts != null)
            {
                for (var x = 0; x < OtherAlerts.Count; x++)
                {
                    OtherAlerts[x].Validate(vb.Path + $"OtherAlerts[{x}]", vb.Messages);
                }
            }

            if (RelatedDocument != null)
            {
                for (var x = 0; x < RelatedDocument.Count; x++)
                {
                    RelatedDocument[x].Validate(vb.Path + $"RelatedDocument[{x}]", vb.Messages);
                }
            }

        }

        #endregion

    }
}
