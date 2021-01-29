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
    /// Source Of Death Notification
    /// </summary>
    [Serializable]
    [DataContract]
    public enum SourceOfDeathNotification
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
        /// Official death certificate or death register
        /// </summary>
        [EnumMember]
	    	[Name(Code="D" , Name = "Official death certificate or death register")]
        OfficialDeathCertificateOrDeathRegister,

        /// <summary>
        /// Healthcare provider
        /// </summary>
        [EnumMember]
        [Name(Code = "H", Name = "Health Care Provider")]
        HealthcareProvider,

        /// <summary>
        /// Relative
        /// </summary>
        [EnumMember]
        [Name(Code = "R", Name = "Relative")]
        Relative,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember]
        [Name(Code = "O", Name = "Other")]
        Other,

        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember]
        [Name(Code = "U", Name = "Unknown")]
        Unknown
    }
}
