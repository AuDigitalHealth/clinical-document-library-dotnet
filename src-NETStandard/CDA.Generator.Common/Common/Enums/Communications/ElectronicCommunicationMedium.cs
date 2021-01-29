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
    /// Electronic Communication Medium
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ElectronicCommunicationMedium 
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
        /// Telephone
        /// </summary>
        [EnumMember]
		[Name(Name = "Phone")]
        Telephone,

        /// <summary>
        /// Fax
        /// </summary>
        [EnumMember]
		[Name(Name = "Fax")]
        Fax,

        /// <summary>
        /// Email
        /// </summary>
        [EnumMember]
		[Name(Name = "Email")]
        Email,

        /// <summary>
        /// FTP
        /// </summary>
        [EnumMember]
		[Name(Name = "FTP")]
        FTP,

        /// <summary>
        /// HTTP
        /// </summary>
        [EnumMember]
	    [Name(Name = "HTTP")]
        HTTP,

        /// <summary>
        /// MLLP
        /// </summary>
        [EnumMember]
		[Name(Name = "MLLP")]
        MLLP,

        /// <summary>
        /// Modem
        /// </summary>
        [EnumMember]
		[Name(Name = "Modem")]
        Modem,

        /// <summary>
        /// NFS
        /// </summary>
        [EnumMember]
		[Name(Name = "NFS")]
        NFS,

        /// <summary>
        /// Telnet
        /// </summary>
        [EnumMember]
		[Name(Name = "Telnet")]
        Telnet,

        /// <summary>
        /// Mobile
        /// </summary>
        [EnumMember]
		[Name(Name = "Mobile")]
        Mobile,

        /// <summary>
        /// Page
        /// </summary>
        [EnumMember]
		[Name(Name = "Page")]
        Page,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember]
		[Name(Name = "Other")]
        Other
    }
}
