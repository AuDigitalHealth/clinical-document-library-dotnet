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
using System.Collections.Generic;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.ATS.ETP.Enum;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces
{
    /// <summary>
    /// This interface contains all the properties that CDA has identified for 
    /// a prescription item
    /// </summary>
    public interface IEPrescriptionItem
    {
        /// <summary>
        /// Custom Narrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Date / Time the prescription is Written
        /// </summary>
        [CanBeNull]
        [DataMember]
        ISO8601DateTime DateTimePrescriptionWritten { get; set; }

        /// <summary>
        /// Date / Time the prescription expires
        /// </summary>
        [CanBeNull]
        [DataMember]
        ISO8601DateTime DateTimePrescriptionExpires { get; set; }

        /// <summary>
        /// The prescription item ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier PrescriptionItemIdentifier { get; set; }

        /// <summary>
        /// Therapeutic good ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText TherapeuticGoodIdentification { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        [CanBeNull]
        [DataMember]
        String Formula { get; set; }

        /// <summary>
        /// The Directions, E.g. two tablets
        /// </summary>
        [CanBeNull]
        [DataMember]
        String Directions { get; set; }

        /// <summary>
        /// Structured Dose
        /// </summary>
        [CanBeNull]
        [DataMember]
        QuantityUnit StructuredDose { get; set; }

        /// <summary>
        /// Timing
        /// </summary>
        [CanBeNull]
        [DataMember]
        Timing Timing { get; set; }

        /// <summary>
        /// Administration Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        AdministrationDetails AdministrationDetails { get; set; }

        /// <summary>
        /// Administration Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        QuantityUnit QuantityToDispense { get; set; }

        /// <summary>
        /// A boolean indicating if brand substitution is allowed
        /// </summary>
        [CanBeNull]
        [DataMember]
        Boolean? BrandSubstituteNotAllowed { get; set; }

        /// <summary>
        /// The maximum numer of repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        Int32? MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// The minimum interval between repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        Quantity MinimumIntervalBetweenRepeats { get; set; }

        /// <summary>
        /// A PBS RPBS ItemCode
        /// </summary>
        [CanBeNull]
        [DataMember]
        PBSPrescriptionTypeValues? PBSPrescriptionType { get; set; }

        /// <summary>
        /// The medical benefit category type, E.g. PBS
        /// </summary>
        [CanBeNull]
        [DataMember]
        MedicalBenefitCategoryType? MedicalBenefitCategoryType { get; set; }

        /// <summary>
        /// A PBSCloseTheGapBenefit
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier PBSCloseTheGapBenefit { get; set; }

        /// <summary>
        /// A PBS RPBS ItemCode
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableTranslation PBSRPBSItemCode { get; set; }

        /// <summary>
        /// PBS RPBS Manufacturer Code
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier PBSRPBSManufacturerCode { get; set; }

        /// <summary>
        /// A list of PbsExtemporaneousIngredient
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<PBSExtemporaneousIngredient> PBSExtemporaneousIngredient { get; set; }

        /// <summary>
        /// Grounds for concurrent supply
        /// </summary>
        [CanBeNull]
        [DataMember]
        GroundsForConcurrentSupply? GroundsForConcurrentSupply { get; set; }

        /// <summary>
        /// PBS / RPBS authority Prescription number
        /// </summary>
        [CanBeNull]
        [DataMember]
        String PBSRPBSAuthorityPrescriptionNumber { get; set; }

        /// <summary>
        /// PBS / RPBS authority Approval number
        /// </summary>
        [CanBeNull]
        [DataMember]
        String PBSRPBSAuthorityApprovalNumber { get; set; }

        /// <summary>
        /// Streamlined Authority
        /// </summary>
        [CanBeNull]
        [DataMember]
        String StreamlinedAuthorityApprovalNumber { get; set; }

        /// <summary>
        /// State authority number
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier StateAuthorityNumber { get; set; }

        /// <summary>
        /// The reason for the therapeutic good
        /// </summary>
        [CanBeNull]
        [DataMember]
        String ReasonForTherapeuticGood { get; set; }

        /// <summary>
        /// Additional comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        String AdditionalComments { get; set; }

        /// <summary>
        /// Dispense Item Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier DispenseItemIdentifier { get; set; }

        /// <summary>
        /// Medication Instruction Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier MedicationInstructionIdentifier { get; set; }

        /// <summary>
        /// Administration Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        IObservation Observations { get; set; }

        /// <summary>
        /// Prescription note detail
        /// </summary>
        [CanBeNull]
        [DataMember]
        String NoteDetail { get; set; }

        /// <summary>
        /// Validates this CDA Context for this Prescription Item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
