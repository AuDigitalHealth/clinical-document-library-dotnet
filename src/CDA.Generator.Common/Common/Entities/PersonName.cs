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
using JetBrains.Annotations;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// Class for representing a person name.
    /// </summary>
    [Serializable]
    [DataContract]
    public class PersonName : IPersonName
    {
        /// <summary>
        /// Titles
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<string> Titles { get; set; }

        /// <summary>
        /// Given name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<String> GivenNames { get; set; }

        /// <summary>
        /// Family name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String FamilyName { get; set; }

        /// <summary>
        /// Name suffix
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<string> NameSuffix { get; set; }

        /// <summary>
        /// Name usage
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<NameUsage> NameUsages { get; set; }

        #region Constructors

        internal PersonName()
        {
        }

        #endregion

        /// <summary>
        /// Validates this Person as an PersonName
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            //vb.ArgumentRequiredCheck("FamilyName", FamilyName);
        }
    }
}
