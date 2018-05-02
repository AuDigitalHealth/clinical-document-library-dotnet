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
    /// Australian State
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AustralianState
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        [EnumMember]
        Undefined,

        /// <summary>
        /// New South Wales
        /// </summary>
        [EnumMember][Name(Code = "NSW", Name = "New South Wales")]
        NSW,

        /// <summary>
        /// Victoria
        /// </summary>
        [EnumMember]
        [Name(Code = "VIC", Name = "Victoria")]
        VIC,

        /// <summary>
        /// Queensland
        /// </summary>
        [EnumMember]
        [Name(Code = "QLD", Name = "Queensland")]
        QLD,

        /// <summary>
        /// South Australia
        /// </summary>
        [EnumMember]
        [Name(Code = "SA", Name = "South Australia")]
        SA,

        /// <summary>
        /// Western Australia
        /// </summary>
        [EnumMember]
        [Name(Code = "WA", Name = "Western Australia")]
        WA,

        /// <summary>
        /// Tasmania
        /// </summary>
        [EnumMember]
        [Name(Code = "TAS", Name = "Tasmania")]
        TAS,

        /// <summary>
        /// Northern Territory
        /// </summary>
        [EnumMember]
        [Name(Code = "NT", Name = "Northern Territory")]
        NT,

        /// <summary>
        /// Australia Capital Territory
        /// </summary>
        [EnumMember]
        [Name(Code = "ACT", Name = "Australia Capital Territory")]
        ACT
    }
}
