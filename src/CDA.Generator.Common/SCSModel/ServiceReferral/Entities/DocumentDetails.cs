using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Document Details Calss
    /// </summary>
    public class DocumentDetailsV1
    {
        /// <summary>
        /// Document Type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText DocumentType { get; set; }

        /// <summary>
        /// Document Title
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string DocumentTitle { get; set; }

       
        #region Constructors

        internal DocumentDetailsV1()
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

            DocumentType?.Validate(path + "DocumentType", messages);
            vb.ArgumentRequiredCheck("DocumentTitle", DocumentTitle);
        }
    }
}
