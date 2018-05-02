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
    /// Medical Benefit Category Types
    /// </summary>
    [Serializable]
    [DataContract]
    [OID(OID = "1.2.36.1.2001.1001.101.103.16095", Identifier = "DE-16095")]
    public enum MedicalBenefitCategoryType
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
        /// PBS
        /// </summary>
        [EnumMember]
        [Name(Code = "1", Name = "PBS", Version = "1.2.36.1.2001.1001.101.104.16095")]
        PBS,

        /// <summary>
        /// RPBS
        /// </summary>
        [Name(Code = "2", Name = "RPBS", Version = "1.2.36.1.2001.1001.101.104.16095")]
        [EnumMember]
        RPBS,

        /// <summary>
        /// CTG
        /// </summary>
        [Name(Code = "3", Name = "CTG", Version = "1.2.36.1.2001.1001.101.104.16095")]
        [EnumMember]
        CTG,

        /// <summary>
        /// No Benefit
        /// </summary>
        [Name(Code = "9", Name = "No Benefit", Version = "1.2.36.1.2001.1001.101.104.10159")]
        [EnumMember]
        NoBenefit,

    }
}
