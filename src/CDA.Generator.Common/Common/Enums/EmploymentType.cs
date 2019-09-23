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
    /// Employment Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum EmploymentType
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
        /// Full Time
        /// </summary>
        [EnumMember]
        [Name(Code = "1", Name = "Full Time", CodeSystem = "METEOREmploymentType")]
        FullTime,

        /// <summary>
        /// Part Time
        /// </summary>
        [EnumMember]
		[Name(Code = "2", Name = "Part Time", CodeSystem = "METEOREmploymentType")]
        PartTime,

        /// <summary>
        /// Casual
        /// </summary>
        [EnumMember]
		[Name(Code = "3", Name = "Casual", CodeSystem = "METEOREmploymentType")]
        Casual,

        /// <summary>
        /// NotstatedInadequatelyDescribeds
        /// </summary>
        [EnumMember]
        [Name(Code = "9", Name = "Not stated/inadequately described", CodeSystem = "METEOREmploymentType")]
        NotstatedInadequatelyDescribed

    }
}
