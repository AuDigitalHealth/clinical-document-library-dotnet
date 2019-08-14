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
    /// Claim Category Type
    /// </summary>
    [Serializable]
    [DataContract]
    [OID(OID = "1.2.36.1.2001.1001.101.103.16060", Identifier = "DE-16060")]
    public enum ClaimCategoryType
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
        /// General Benefit
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16060", Identifier = "DE-16060")]
        [EnumMember]
        [Name(Code = "1", Name = "G - General Benefit", Comment = "General Benefit")]
        GeneralBenefit,

        /// <summary>
        /// Concessional or Safety Net Concession Benefit
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16060", Identifier = "DE-16060")]
        [EnumMember]
        [Name(Code = "2", Name = "C - Concessional or Safety Net Concession Benefit", Comment = "Concessional or Safety Net Concession Benefit")]
        ConcessionalOrSafetyNetConcessionBenefit,

        /// <summary>
        /// Safety net Entitlement Card Benefit
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16060", Identifier = "DE-16060")]
        [EnumMember]
        [Name(Code = "3", Name = "E - Safety Net Entitlement Card Benefit", Comment = "Safety net Entitlement Card Benefit")]
        SafetyNetEntitlementCardBenefit,

        /// <summary>
        /// RPBS Benefit
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16060", Identifier = "DE-16060")]
        [EnumMember]
        [Name(Code = "4", Name = "R - RPBS Benefit", Comment = "RPBS Benefit")]
        RPBSBenefit,

        /// <summary>
        /// Closing the Gap Benefit
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16060", Identifier = "DE-16060")]
        [EnumMember]
        [Name(Code = "5", Name = "CTG - Closing the Gap Benefit", Comment = "Closing the Gap Benefit")]
        ClosingTheGapBenefit,

        /// <summary>
        /// this item is not covered by any Medicare registered benefit
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16060", Identifier = "DE-16060")]
        [EnumMember]
        [Name(Code = "9", Name = "No benefit - This item is not covered by any Medicare registered benefit", Comment = "this item is not covered by any Medicare registered benefit")]
        NoBenefit
    }
}
