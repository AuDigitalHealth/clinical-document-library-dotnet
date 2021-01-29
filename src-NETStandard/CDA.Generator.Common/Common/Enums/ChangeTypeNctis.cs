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
    public enum ChangeTypeNctis
    {
        /// <summary>
        /// Unchanged
        /// </summary>
        [EnumMember]
        [Name(Code = "01", Name = "Unchanged", CodeSystem = "NCTISChangeTypeValues")]
        Unchanged,

        /// <summary>
        /// ContinueWithChange
        /// </summary>
        [EnumMember]
        [Name(Code = "02", Name = "Changed", CodeSystem = "NCTISChangeTypeValues")]
        Changed,

        /// <summary>
        /// Cease
        /// </summary>
        [EnumMember]
        [Name(Code = "03", Name = "Cancelled", CodeSystem = "NCTISChangeTypeValues")]
        Cancelled,

        /// <summary>
        /// Start
        /// </summary>
        [EnumMember]
        [Name(Code = "04", Name = "Prescribed", CodeSystem = "NCTISChangeTypeValues")]
        Prescribed,

        /// <summary>
        /// Ceased
        /// </summary>
        [EnumMember]
        [Name(Code = "05", Name = "Ceased", CodeSystem = "NCTISChangeTypeValues")]
        Ceased,

        /// <summary>
        /// Ceased
        /// </summary>
        [EnumMember]
        [Name(Code = "06", Name = "Suspended", CodeSystem = "NCTISChangeTypeValues")]
        Suspended
    }
}
