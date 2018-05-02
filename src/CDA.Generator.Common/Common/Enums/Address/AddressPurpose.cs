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
    /// Address Purpose
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AddressPurpose
    {
        /// <summary>
        /// NotStated/Unknown/InadequatelyDescribed
        /// </summary>
        [EnumMember]
        NotStatedUnknownInadequatelyDescribed,

        /// <summary>
        /// Business
        /// </summary>
        [EnumMember]
        [Name(Code = "WP", Name = "Business")]
        Business,

        /// <summary>
        /// Mailing or Postal
        /// </summary>
        [EnumMember]
        [Name(Code = "PST", Name = "Mailing or Postal")]
        MailingOrPostal,

        /// <summary>
        /// Temporary Accommodation
        /// </summary>
        [EnumMember]
        [Name(Code = "TMP", Name = "Temporary Accommodation")]
        TemporaryAccommodation,

        /// <summary>
        /// Residential
        /// </summary>
        [EnumMember]
        [Name(Code = "H", Name = "Residential")]
        Residential

    }
}
