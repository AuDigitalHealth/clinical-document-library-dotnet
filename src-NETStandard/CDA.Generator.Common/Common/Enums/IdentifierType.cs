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
    public enum IdentifierType
    {
        /// <summary>
        /// Individual Medicare Card Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.0.7", Name = "Individual Medicare Card Number")]
        IndividualMedicareCardNumber,

        /// <summary>
        /// Medicare Card Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.0.7.1", Name = "Medicare Card Number")]
        MedicareCardNumber,

        /// <summary>
        /// Medicare Card Issue Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.0.7.2", Name = "Medicare Card Issue Number")]
        MedicareCardIssueNumber,

        /// <summary>
        /// Medicare Card Individual Reference Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.0.7.3", Name = "Medicare Card Individual Reference Number")]
        MedicareCardIndividualReferenceNumber,

        /// <summary>
        /// PBS/RPBS Authority Prescription Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.1.3.2.2", Name = "PBS/RPBS Authority Prescription Number")]
        PBSRPBSAuthorityPrescriptionNumber,

        /// <summary>
        /// PBS/RPBS Authority Approval Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.1.3.2.3", Name = "PBS/RPBS Authority Approval Number")]
        PBSRPBSAuthorityApprovalNumber,

        /// <summary>
        /// Medicare Provider Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.174030967.0.2", Name = "Medicare provider number")]
        MedicareProviderNumber,

        /// <summary>
        /// Prescriber Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.174030967.0.3", Name = "Medicare prescriber number")]
        PrescriberNumber,

        /// <summary>
        /// Prescriber Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.174030967.1.3.2.1", Name = "Medicare pharmacy approval number")]
        MedicarePharmacyApprovalNumber,

        /// <summary>
        /// Prescriber Number
        /// </summary>
        [EnumMember]
        [Name(Code = "2.16.840.1.113883.3.879.270098", Name = "Centrelink customer reference number")]
        CentrelinkCustomerReferenceNumber


    }
}
