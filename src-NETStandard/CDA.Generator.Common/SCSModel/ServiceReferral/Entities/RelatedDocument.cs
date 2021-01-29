using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// The Related Document class
    /// </summary>
    public class RelatedDocumentV1
    {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Document Target
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ExternalData DocumentTarget { get; set; }

        /// <summary>
        /// Document Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        public DocumentDetailsV1 DocumentDetails { get; set; }

        #region Constructors

        internal RelatedDocumentV1()
        {

        }

        #endregion

        /// <summary>
        /// Validates this test procedure name
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("DocumentTarget", DocumentTarget))
            {
                DocumentTarget.Validate(path + "DocumentTarget", messages);
            }

            if (vb.ArgumentRequiredCheck("DocumentDetails", DocumentDetails))
            {
                DocumentDetails.Validate(path + "DocumentDetails", messages);
            }
        }
    }
}
