/*
 * Copyright 2020 ADHA
 *
 * Licensed under the Agency Open Source (Apache) License; you may not use this
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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class encapsulates a FHIR emptyReason
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class EmptyReason 
    {
        #region Properties

        /// <summary>
        /// List of codes
        /// </summary>
        [CanBeNull]
        [DataMember]
        public NonClinicalEmptyReason Value { get; set; }

        /// <summary>
        /// Further text about why empty
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string OriginalText { get; set; }

        #endregion

        #region Constructors
        internal EmptyReason()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this statement
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Statement.Value", Value);
        }

        #endregion
    }
}