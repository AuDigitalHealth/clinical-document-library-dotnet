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
    public enum OrganisationNameUsage
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
        /// Organizational unit/section/division name
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGU", Name = "Organizational unit/section/division name")]
        OrganisationalUnit,

        /// <summary>
        /// Service location name
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGS", Name = "Service location name")]
        ServiceLocationName,

        /// <summary>
        /// Business Name
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGB", Name = "Business name")]
        BusinessName,

        /// <summary>
        /// Locally used name
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGL", Name = "Locally used name")]
        LocallyUsedName,

        /// <summary>
        /// Abbreviated name
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGA", Name = "Abbreviated name")]
        AbbreviatedName,

        /// <summary>
        /// Enterprise name
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGE", Name = "Enterprise name")]
        EnterpriseName,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGX", Name = "Other")]
        Other,

        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember]
		    [Name(Code = "ORGY", Name = "Unknown")]
        Unknown
    }
}
