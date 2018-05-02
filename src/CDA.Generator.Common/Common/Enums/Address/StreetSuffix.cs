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
    /// Street Suffix
    /// </summary>
    [Serializable]
    [DataContract]
    public enum StreetSuffix
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
        /// Central
        /// </summary>
        [EnumMember]
		[Name(Code = "Cn", Name = "Central")]
        Central,

        /// <summary>
        /// East
        /// </summary>
        [EnumMember]
		[Name(Code = "E", Name = "East")]
        East,

        /// <summary>
        /// Extension
        /// </summary>
        [EnumMember]
		[Name(Code = "Ex", Name = "Extension")]
        Extension,

        /// <summary>
        /// Lower
        /// </summary>
        [EnumMember]
		[Name(Code = "Lr", Name = "Lower")]
        Lower,

        /// <summary>
        /// North
        /// </summary>
        [EnumMember]
		[Name(Code = "N", Name = "North")]
        North,

        /// <summary>
        /// North East
        /// </summary>
        [EnumMember]
		[Name(Code = "Ne", Name = "North East")]
        NorthEast,

        /// <summary>
        /// North West
        /// </summary>
        [EnumMember]
		[Name(Code = "Nw", Name = "North West")]
        NorthWest,

        /// <summary>
        /// South
        /// </summary>
        [EnumMember]
		[Name(Code = "S", Name = "South")]
        South,

        /// <summary>
        /// South East
        /// </summary>
        [EnumMember]
		[Name(Code = "Se", Name = "South East")]
        SouthEast,

        /// <summary>
        /// South West
        /// </summary>
        [EnumMember]
		[Name(Code = "Sw", Name = "South West")]
        SouthWest,

        /// <summary>
        /// Upper
        /// </summary>
        [EnumMember]
		[Name(Code = "Up", Name = "Upper")]
        Upper,

        /// <summary>
        /// West
        /// </summary>
        [EnumMember]
		[Name(Code = "W", Name = "West")]
        West 
    }
}
