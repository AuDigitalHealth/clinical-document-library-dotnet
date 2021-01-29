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
    /// Identifier Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum HealthIdentifierType
    {
        /// <summary>
        /// National Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "HPI-O", Name = "National Identifier", Extension = "1.2.36.1.2001.1003.0.")]
        HPIO,

        /// <summary>
        /// National Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "HPI-I", Name = "National Identifier", Extension = "1.2.36.1.2001.1003.0.")]
        HPII,

        /// <summary>
        /// National Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "IHI", Name = "National Identifier", Extension = "1.2.36.1.2001.1003.0.")]
        IHI,

        /// <summary>
        /// National Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "PAI-O", Name = "National Identifier", Extension = "1.2.36.1.2001.1007.1.")]
        PAIO,

        /// <summary>
        /// National Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "PAI-R", Name = "National Identifier", Extension = "1.2.36.1.2001.1007.10.")]
        PAIR,

        /// <summary>
        /// National Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "PAI-D", Name = "National Identifier", Extension = "1.2.36.1.2001.1007.20.")]
        PAID
    }
}
