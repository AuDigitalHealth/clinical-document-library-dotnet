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
    /// Telecommunication Address Usage
    /// </summary>
    [Serializable]
    [DataContract]
    public enum TelecommunicationAddressUsage
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
        /// Home
        /// </summary>
        [EnumMember]
		[Name(Code = "H", Name = "Home")]
        Home,

        /// <summary>
        /// Home Primary
        /// </summary>
        [EnumMember]
        [Name(Code = "HP", Name = "Primary Home")]
        HomePrimary,

        /// <summary>
        /// Home Vacation
        /// </summary>
        [EnumMember]
        [Name(Code = "HV", Name = "Vacation Home")]
        HomeVacation,

        /// <summary>
        /// Work Place
        /// </summary>
        [EnumMember]
        [Name(Code = "WP", Name = "Workplace")]
        WorkPlace,

        /// <summary>
        /// Answering Answering
        /// </summary>
        [EnumMember]
        [Name(Code = "AS", Name = "Answering Service")]
        AnsweringService,

        /// <summary>
        /// Emergency Contact
        /// </summary>
        [EnumMember]
        [Name(Code = "EC", Name = "Emergency Contact")]
        EmergencyContact,

        /// <summary>
        /// Mobile Contact
        /// </summary>
        [EnumMember]
        [Name(Code = "MC", Name = "Mobile Contact")]
        MoblieContact,

        /// <summary>
        /// Pager
        /// </summary>
        [EnumMember]
        [Name(Code = "PG", Name = "Pager")]
        Pager
    }
}
