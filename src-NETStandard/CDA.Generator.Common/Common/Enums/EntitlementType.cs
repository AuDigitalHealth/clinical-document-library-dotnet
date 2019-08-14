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
    /// Entitlement Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum EntitlementType
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
        /// Medicare Benefits
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "1", Name = "Medicare Benefits")]
        MedicareBenefits,

        /// <summary>
        /// Pensioner Concession
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "2", Name = "Pensioner Concession")]
        PensionerConcession,

        /// <summary>
        /// Commonwealth Seniors Concession
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
        [Name(Code = "3", Name = "Commonwealth Seniors Health Concession")]
        CommonwealthSeniorsConcesion,

        /// <summary>
        /// Health Care Concession
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "4", Name = "Health Care Concession")]
        HealthCareConcession,

        /// <summary>
        /// Repatriation Health Gold Benefits
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "5", Name = "Repatriation Health Gold Benefits")]
        RepatriationHealthGoldBenefits,

        /// <summary>
        /// Repatriation Health White Benefits
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "6", Name = "Repatriation Health White Benefits")]
        RepatriationHealthWhiteBenefits,

        /// <summary>
        /// Repatriation Health Orange Benefits
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "7", Name = "Repatriation Health Orange Benefits")]
        RepatriationHealthOrangeBenefits,

        /// <summary>
        /// Safety Net Concession
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "8", Name = "Safety Net Concession")]
        SafetyNetConcession,

        /// <summary>
        /// Safety Net Entitlement
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
		[Name(Code = "9", Name = "Safety Net Entitlement")]
        SafetyNetEntitlement,

        /// <summary>
        /// Medicare Prescriber Number
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
        [Name(Code = "10", Name = "Medicare Prescriber Number")]
        MedicarePrescriberNumber,

        /// <summary>
        /// Medicare Pharmacy Approval Number
        /// </summary>
        [OID(OID = "1.2.36.1.2001.1001.101.104.16047", Identifier = "VD-16047")]
        [EnumMember]
        [Name(Code = "11", Name = "Medicare Pharmacy Approval Number")]
        MedicarePharmacyApprovalNumber,

    }
}
