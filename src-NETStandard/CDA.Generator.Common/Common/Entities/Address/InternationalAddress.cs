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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    ///  An International address
    /// </summary>
    [Serializable]
    [DataContract]
    public class InternationalAddress
    {
        #region properties
        /// <summary>
        /// The address line as a collection of free text strings
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<String> AddressLine { get; set; }

        /// <summary>
        /// State or Province
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String StateProvince { get; set; }

        /// <summary>
        /// Post code
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String PostCode { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Country Country { get; set; }
        #endregion

        #region Constructors
        internal InternationalAddress()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this InternationalAddress
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
        }
        #endregion
    }
}
