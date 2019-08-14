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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Change type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ChangeTypeSnomed
    {
        /// <summary>
        /// Unchanged
        /// </summary>
        [EnumMember]
        [Name(Code = "105701000036103", Name = "Unchanged", CodeSystem = "SNOMED")]
        Unchanged,

        /// <summary>
        /// ContinueWithChange
        /// </summary>
        [EnumMember]
        [Name(Code = "105691000036103", Name = "Changed", CodeSystem = "SNOMED")]
        Changed,

        /// <summary>
        /// Cease
        /// </summary>
        [EnumMember]
        [Name(Code = "89925002", Name = "Cancelled", CodeSystem = "SNOMED")]
        Cancelled,

        /// <summary>
        /// Start
        /// </summary>
        [EnumMember]
        [Name(Code = "105681000036100", Name = "Prescribed", CodeSystem = "SNOMED")]
        Prescribed,

        /// <summary>
        /// Ceased
        /// </summary>
        [EnumMember]
        [Name(Code = "385656004", Name = "Ceased", CodeSystem = "SNOMED")]
        Ceased,

        /// <summary>
        /// Ceased
        /// </summary>
        [EnumMember]
        [Name(Code = "385655000", Name = "Suspended", CodeSystem = "SNOMED")]
        Suspended
    }
}
