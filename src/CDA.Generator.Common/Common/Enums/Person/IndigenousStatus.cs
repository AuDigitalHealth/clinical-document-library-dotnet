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
    /// Indigenous Status
    /// </summary>
    [Serializable]
    [DataContract]
    public enum IndigenousStatus
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
        /// Aboriginal But Not Torres Strait Islander Origin
        /// </summary>
        [EnumMember]
		[Name(Code = "1", Name = "Aboriginal but not Torres Strait Islander origin")]
        AboriginalButNotTorresStraitIslanderOrigin,

        /// <summary>
        /// Torres Strait Islander But Not Aboriginal Origin
        /// </summary>
        [EnumMember]
		[Name(Code = "2", Name = "Torres Strait Islander but not Aboriginal origin")]
        TorresStraitIslanderButNotAboriginalOrigin,

        /// <summary>
        /// Both Aboriginal And Torres Strait Islander Origin
        /// </summary>
        [EnumMember]
		[Name(Code = "3", Name = "Both Aboriginal and Torres Strait Islander origin")]
        BothAboriginalAndTorresStraitIslanderOrigin,

        /// <summary>
        /// Neither Aboriginal Nor Torres Strait Islander Origin
        /// </summary>
        [EnumMember]
	    [Name(Code = "4", Name = "Neither Aboriginal nor Torres Strait Islander origin")]
        NeitherAboriginalNorTorresStraitIslanderOrigin,

        /// <summary>
        /// Not Stated Or Inadequately Described
        /// </summary>
        [EnumMember]
		[Name(Code = "9", Name = "Not stated/inadequately described")]
        NotStatedOrInadequatelyDescribed,
    }
}
