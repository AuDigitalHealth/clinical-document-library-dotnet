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
    /// Name Usage
    /// </summary>
    [Serializable]
    [DataContract]
    public enum NameUsage
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
        /// Preferred Name Indicator
        /// </summary>
        [EnumMember]
        [Name(Code = "L", Name = "Preferred Name Indicator")]
        PreferredNameIndicator,
        
        /// <summary>
        /// Legal
        /// </summary>
        [EnumMember]
		[Name(Code = "L", Name = "Registered Name (Legal Name)")]
        Legal,

        /// <summary>
        /// Certificate
        /// </summary>
        [EnumMember]
		[Name(Code = "C", Name = "Reporting Name")]
        ReportingName,

        /// <summary>
        /// External
        /// </summary>
        [EnumMember]
		[Name(Code = "NB", Name = "Newborn Name")]
        NewbornName,

        /// <summary>
        /// Artist
        /// </summary>
        [EnumMember]
		[Name(Code = "A", Name = "Professional or Business Name")]
        ProfessionalOrBusinessName,

        /// <summary>
        /// Maiden Name
        /// </summary>
        [EnumMember]
		[Name(Code = "M", Name = "Maiden Name (Name at birth)")]
        MaidenName,

        /// <summary>
        /// Pseudonym
        /// </summary>
        [EnumMember]
		[Name(Code = "P", Name = "Other Name (Al")]
        OtherName,
    }
}
