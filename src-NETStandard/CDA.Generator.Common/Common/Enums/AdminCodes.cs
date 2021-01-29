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
    /// Change type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AdminCodes
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
        /// Additional Comments
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16044", Name = "Additional Comments")]
        AdditionalComments,

        /// <summary>
        /// Medication Instruction Comment - NOT USED (Was used by SR but not any more)
        /// </summary>
        //[EnumMember]
        //[Name(Code = "103.16044", Name = "Medication Instruction Comment")]
        //MedicationInstructionComment,

        /// <summary>
        /// Administrative Observations
        /// </summary>
        [EnumMember]
        [Name(Code = "102.16080", Name = "Administrative Observations")]
        AdministrativeObservations,

        /// <summary>
        /// Age
        /// </summary>
        [EnumMember]
        [Name(Code = "103.20109", Name = "Age")]
        Age,

        /// <summary>
        /// Age Accuracy Indicator
        /// </summary>
        [EnumMember]
        [Name(Code = "102.16242", Name = "Age Accuracy Indicator")]
        AgeAccuracyIndicator,

        /// <summary>
        /// Birth Plurality
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16249", Name = "Birth Plurality")]
        BirthPlurality,

        /// <summary>
        /// Brand Substitution Occurred
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16064", Name = "Brand Substitution Occurred")]
        BrandSubstitutionOccurred,

        /// <summary>
        /// Date of Birth Accuracy Indicator
        /// </summary>
        [EnumMember]
        [Name(Code = "102.16234", Name = "Date of Birth Accuracy Indicator")]
        DateOfBirthAccuracyIndicator,

        /// <summary>
        /// Date of Birth is Calculated From Age
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16233", Name = "Date of Birth is Calculated From Age")]
        DateOfBirthIsCalculatedFromAge,

        /// <summary>
        /// Formula
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16272", Name = "Formula")]
        Formula,

        /// <summary>
        /// PBS Close the Gap Benefit
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16095.3", Name = "PBS Close the Gap Benefit")]
        PBSCloseTheGapBenefit,

        /// <summary>
        /// PBS/RPBS Authority Prescription Number
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16719", Name = "PBS/RPBS Authority Prescription Number")]
        PBSRPBSAuthorityPrescriptionNumber,

        /// <summary>
        /// PBS/RPBS Authority Approval Number
        /// </summary>
        [EnumMember]
        [Name(Code = "103.10159", Name = "PBS/RPBS Authority Approval Number")]
        PBSRPBSAuthorityApprovalNumber,

        /// <summary>
        /// Prescriber Instruction Communication Medium
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16297", Name = "Prescriber Instruction Communication Medium")]
        PrescriberInstructionCommunicationMedium,

        /// <summary>
        /// Prescriber Instruction Detail
        /// </summary>
        [EnumMember]
        [Name(Code = "102.16290", Name = "Prescriber Instruction Detail")]
        PrescriberInstructionDetail,

        /// <summary>
        /// Prescriber Instruction Source
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16295", Name = "Prescriber Instruction Source")]
        PrescriberInstructionSource,

        /// <summary>
        /// Prescription Request Item
        /// </summary>
        [EnumMember]
        [Name(Code = "102.16211", Name = "Prescription Request Item")]
        PrescriptionRequestItem,

        /// <summary>
        /// Qualifications
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16268", Name = "Qualifications")]
        Qualifications,

        /// <summary>
        /// Reason for Therapeutic Good
        /// </summary>
        [EnumMember]
        [Name(Code = "103.10141", Name = "Reason for Therapeutic Good")]
        ReasonForTherapeuticGood,

        /// <summary>
        /// State Authority Number
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16018", Name = "State Authority Number")]
        StateAuthorityNumber,

        /// <summary>
        /// Dispense Item
        /// </summary>
        [EnumMember]
        [Name(Code = "102.16211", Name = "Dispense Item")]
        DispenseItem,

        /// <summary>
        /// Label Instruction
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16109", Name = "Label Instruction")]
        LabelInstruction,

        /// <summary>
        /// Early Supply With Pharmaceutical Benefit
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.2001.1001.101.104.16060", Name = "Claim Category Type Values")]
        UnderCoPayment,

        /// <summary>
        /// Early Supply With Pharmaceutical Benefit
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16348", Name = "Early Supply With Pharmaceutical Benefit")]
        EarlySupplyWithPharmaceuticalBenefit,

        /// <summary>
        /// Streamlined Authority Approval Number
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16718", Name = "Streamlined Authority Approval Number")]
        StreamlinedAuthorityApprovalNumber,

        /// <summary>
        /// Medication Instruction Identifier
        /// </summary>
        [EnumMember]
        [Name(Code = "103.16444", Name = "Medication Instruction Identifier")]
        MedicationInstructionIdentifier,

        /// <summary>
        /// Patient Category
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.1.3.2.5", Name = "Patient Category")]
        PatientCategory,

        /// <summary>
        /// RACF ID
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.2001.1005.44", Name = "RACF Id")]
        RACFId,
    }
}
