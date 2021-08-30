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
    /// Act Status
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ActStatus
    {
        /// <summary>
        /// active
        /// </summary>
        [EnumMember]
        [Name(Code = "active", Name = "Active", CodeSystem = "HL7ActStatusCodes")]
        Active,

        /// <summary>
        /// completed
        /// </summary>
        [EnumMember]
        [Name(Code = "completed", Name = "Completed", CodeSystem = "HL7ActStatusCodes")]
        Completed,

        /// <summary>
        /// nullified
        /// </summary>
        [EnumMember]
        [Name(Code = "nullified", Name = "Nullified", CodeSystem = "HL7ActStatusCodes")]
        Nullified,

        /// <summary>
        /// aborted
        /// </summary>
        [EnumMember]
        [Name(Code = "aborted", Name = "Aborted", CodeSystem = "HL7ActStatusCodes")]
        Aborted,

        /// <summary>
        /// suspended
        /// </summary>
        [EnumMember]
        [Name(Code = "suspended", Name = "Suspended", CodeSystem = "HL7ActStatusCodes")]
        Suspended,

        /// <summary>
        /// new
        /// </summary>
        [EnumMember] 
        [Name(Code = "new", Name = "New", CodeSystem = "HL7ActStatusCodes")]
        New,

    }
}
