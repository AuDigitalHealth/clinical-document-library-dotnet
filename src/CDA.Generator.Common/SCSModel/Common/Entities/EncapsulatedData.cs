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
using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common.Entities
{
    /// <summary>
  /// This class represents a EncapsulatedData item. The file can either be text or reference an external file.
    /// </summary>
    [Serializable]
    [DataContract]
    public class EncapsulatedData
    {
        #region Properties

        /// <summary>
        /// An ExternalData element for a External Attachment
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ExternalData ExternalData { get; set; }
     
        /// <summary>
        /// The ability to add text for an Encapsulated Data element
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Text { get; set; }

        #endregion

        #region Constructors
        internal EncapsulatedData()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this EncapsulatedData
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ExternalData == null && Text.IsNullOrEmptyWhitespace())
            {
               vb.AddValidationMessage(path + ".ExternalData", string.Empty, "Please specify exactly one ExternalData or a Text object for Encapsulated Data");
            }

            if (ExternalData != null && !Text.IsNullOrEmptyWhitespace())
            {
               vb.AddValidationMessage(path + ".ExternalData", string.Empty, "Please specify exactly one ExternalData or a Text object for Encapsulated Data");
            }

            if (ExternalData != null)
            {
               ExternalData.Validate(path + ".ExternalData", messages);
            }
        }
        #endregion

    }
}

