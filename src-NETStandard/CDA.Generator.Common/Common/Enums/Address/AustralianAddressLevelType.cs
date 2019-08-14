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
    /// Australian Address Level Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AustralianAddressLevelType
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
        /// Basement
        /// </summary>
        [EnumMember]
        [Name(Code = "B", Name = "Basement")]
        Basement,

        /// <summary>
        /// Floor
        /// </summary>
        [EnumMember]
        [Name(Code = "FL", Name = "Floor")]
        Floor,

        /// <summary>
        /// Ground
        /// </summary>
        [EnumMember]
        [Name(Code = "G", Name = "Ground")]
        Ground,

        /// <summary>
        /// Level
        /// </summary>
        [EnumMember]
        [Name(Code = "L", Name = "Level")]
        Level,

        /// <summary>
        /// LowerGround
        /// </summary>
        [EnumMember]
        [Name(Code = "G", Name = "LowerGround")]
        LowerGround,

        /// <summary>
        /// Mezzanine
        /// </summary>
        [EnumMember]
        [Name(Code = "M", Name = "Mezzanine")]
        Mezzanine,

        /// <summary>
        /// UpperGround
        /// </summary>
        [EnumMember]
        [Name(Code = "UG", Name = "UpperGround")]
        UpperGround,

    }
}
