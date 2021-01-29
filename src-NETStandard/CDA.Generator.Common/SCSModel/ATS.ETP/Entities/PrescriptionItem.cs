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
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Entities
{
    /// <summary>
    /// The PrescriptionItem class contains all the properties that CDA has identified for 
    /// a prescription item
    /// 
    /// Please use the CreatePrescriptionItem() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    internal class PrescriptionItem : IEPrescriptionItem
    {
        #region Properties

        /// <summary>
        /// Custom Narrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Therapeutic good ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TherapeuticGoodId { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Formula { get; set; }

        /// <summary>
        /// The quantity of the therapeutic good
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String QuantityOfTherapeuticGood { get; set; }

        /// <summary>
        /// A boolean indicating if brand substitution is allowed
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? BrandSubstituteNotAllowed { get; set; }

        /// <summary>
        /// The maximum numer of repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// Additional comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AdditionalComments { get; set; }

        /// <summary>
        /// DateTime Prescription Written
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTimePrescriptionWritten { get; set; }

        /// <summary>
        /// Date Time Prescription Expires
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTimePrescriptionExpires { get; set; }

        /// <summary>
        /// The prescription item Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier PrescriptionItemIdentifier { get; set; }

        /// <summary>
        /// Therapeutic Good Identification
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TherapeuticGoodIdentification { get; set; }

        /// <summary>
        /// PBSR PBS Manufacturer Code
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier PBSRPBSManufacturerCode { get; set; }

        /// <summary>
        /// PbsExtemporaneousIngredient
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<PBSExtemporaneousIngredient> PBSExtemporaneousIngredient { get; set; }

        /// <summary>
        /// A PBSCloseTheGapBenefit
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier PBSCloseTheGapBenefit { get; set; }

        /// <summary>
        /// A PBS RPBS ItemCode
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableTranslation PBSRPBSItemCode { get; set; }

        /// <summary>
        /// The Instruction
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Directions { get; set; }

        /// <summary>
        /// Structured Dose
        /// </summary>
        [CanBeNull]
        [DataMember]
        public QuantityUnit StructuredDose { get; set; }

        /// <summary>
        /// Timing
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Timing Timing { get; set; }

        /// <summary>
        /// Quantity To Dispense
        /// </summary>
        public QuantityUnit QuantityToDispense { get; set; }

        /// <summary>
        /// The minimum interval between repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity MinimumIntervalBetweenRepeats { get; set; }

        /// <summary>
        /// PBS Prescription Type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public PBSPrescriptionTypeValues? PBSPrescriptionType { get; set; }

        /// <summary>
        /// The medical benefit category type, E.g. PBS
        /// </summary>
        [CanBeNull]
        [DataMember]
        public MedicalBenefitCategoryType? MedicalBenefitCategoryType { get; set; }

        /// <summary>
        /// Grounds for concurrent supply
        /// </summary>
        [CanBeNull]
        [DataMember]
        public GroundsForConcurrentSupply? GroundsForConcurrentSupply { get; set; }

        /// <summary>
        /// PBS / RPBS authority approval number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String PBSRPBSAuthorityPrescriptionNumber { get; set; }

        /// <summary>
        /// PBS / RPBS authority Approval number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String PBSRPBSAuthorityApprovalNumber { get; set; }

        /// <summary>
        /// Streamlined Authority
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String StreamlinedAuthorityApprovalNumber { get; set; }

        /// <summary>
        /// State authority number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier StateAuthorityNumber { get; set; }

        /// <summary>
        /// The Reason for the therapeutic good
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ReasonForTherapeuticGood { get; set; }

        /// <summary>
        /// Dispense Item Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier DispenseItemIdentifier { get; set; }

        /// <summary>
        /// Medication Instruction Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier MedicationInstructionIdentifier { get; set; }

        /// <summary>
        /// Observations
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IObservation Observations { get; set; }

        /// <summary>
        /// Prescription note detail
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String NoteDetail { get; set; }

        /// <summary>
        /// Administration Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AdministrationDetails AdministrationDetails { get; set; }

        /// <summary>
        /// Body weight
        /// </summary>
        [CanBeNull]
        [DataMember]
        public BodyWeight ObservationBodyWeight { get; set; }

        /// <summary>
        /// Body height
        /// </summary>
        [CanBeNull]
        [DataMember]
        public BodyHeight ObservationBodyHeight { get; set; }

        #endregion

        #region Constructors
        internal PrescriptionItem()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Prescription Item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
           var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("DateTimePrescriptionExpires", DateTimePrescriptionExpires))
            {
               if (DateTimePrescriptionExpires.PrecisionIndicator == null || DateTimePrescriptionExpires.PrecisionIndicator.Value != ISO8601DateTime.Precision.Day || DateTimePrescriptionExpires.TimeZone != null)
              {
                vb.AddValidationMessage(vb.PathName, null, "SHALL include a complete date (century, year, month and day)");
              }
            }

            vb.ArgumentRequiredCheck("PrescriptionItemIdentifier", PrescriptionItemIdentifier);

            vb.ArgumentRequiredCheck("Directions", Directions);

            if (vb.ArgumentRequiredCheck("TherapeuticGoodIdentification", TherapeuticGoodIdentification))
            {
              TherapeuticGoodIdentification.Validate(vb.Path + "TherapeuticGoodIdentification", messages);

              if (PBSRPBSItemCode != null && TherapeuticGoodIdentification.OriginalText.IsNullOrEmptyWhitespace())
              {
                vb.AddValidationMessage(vb.PathName, null, "TherapeuticGoodIdentification's OriginalText is a required field when PBSRPBSItemCode is present");
              }

              if (TherapeuticGoodIdentification.Translations != null)
              {
                vb.AddValidationMessage(vb.PathName,null, "Translations can not be set for TherapeuticGoodIdentification please use PBS/RPBS Item Code instead");
              }
            }

            if (StructuredDose != null)
            {
              StructuredDose.Validate(vb.Path + "StructuredDose", messages);
            }

            if (Timing != null)
            {
              Timing.Validate(vb.Path + "Timing", messages);

              if (Timing.TimingDescription.IsNullOrEmptyWhitespace())
              {
                vb.AddValidationMessage(vb.PathName, string.Empty, "If TIMING is included, Timing Description SHALL be fully and automatically derived");
              }
            }

            if  (PBSCloseTheGapBenefit != null)
            {
                PBSCloseTheGapBenefit.Validate(vb.Path + "PBSCloseTheGapBenefit", messages);
            }

            if (AdministrationDetails != null)
            {
                AdministrationDetails.Validate(vb.Path + "AdministrationDetails]", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("QuantityToDispense", QuantityToDispense))
            {
              QuantityToDispense.ValidateDispensingUnit(vb.Path + "QuantityToDispense", messages);
            }

            vb.ArgumentRequiredCheck("MaximumNumberOfRepeats", MaximumNumberOfRepeats);

            if (MinimumIntervalBetweenRepeats != null)
            {
               MinimumIntervalBetweenRepeats.Validate(vb.Path + "MinimumIntervalBetweenRepeats", messages);
            }

            vb.ArgumentRequiredCheck("PBSPrescriptionType", PBSPrescriptionType);

            if (vb.ArgumentRequiredCheck("MedicalBenefitCategoryType", MedicalBenefitCategoryType))
            {
               if (MedicalBenefitCategoryType.Value == Nehta.VendorLibrary.CDA.Common.Enums.MedicalBenefitCategoryType.CTG)
               {
                 vb.AddValidationMessage(vb.PathName, null, "Medical Benefit Category Type of CTG is not valid for this document");
               }
            }

            if (PBSRPBSItemCode != null)
            {
              PBSRPBSItemCode.Validate(vb.Path + "PBSRPBSItemCode", messages);
            }

            if (PBSRPBSManufacturerCode != null)
            {
                PBSRPBSManufacturerCode.Validate(vb.Path + "PBSRPBSManufacturerCode", messages);

                if (MedicalBenefitCategoryType.HasValue && !(MedicalBenefitCategoryType.Value == Nehta.VendorLibrary.CDA.Common.Enums.MedicalBenefitCategoryType.PBS ||
                    MedicalBenefitCategoryType.Value == Nehta.VendorLibrary.CDA.Common.Enums.MedicalBenefitCategoryType.RPBS))
                {
                  vb.AddValidationMessage(vb.PathName, null, "PBSRPBSManufacturerCode SHALL be present on an e-prescription where the Medical Benefit Type Category is one of the following: PBS  RPBS ");
                }
            }

            vb.ArgumentRequiredCheck("GroundsForConcurrentSupply", GroundsForConcurrentSupply);

            if (PBSExtemporaneousIngredient != null)
            {
              for (var x = 0; x < PBSExtemporaneousIngredient.Count; x++)
              {
                PBSExtemporaneousIngredient[x].Validate(vb.Path + string.Format("PBSExtemporaneousIngredient[{0}]", x), vb.Messages);
              }
            }
        
            if (StateAuthorityNumber != null)
            {
                StateAuthorityNumber.Validate(vb.Path + "StateAuthorityNumber", messages);

                if (StateAuthorityNumber.Extension.IsNullOrEmptyWhitespace())
                {
                  vb.AddValidationMessage(vb.PathName, null, "Extension is required for StateAuthorityNumber");
                }
            }

            if (MedicationInstructionIdentifier != null)
            {
              MedicationInstructionIdentifier.Validate(vb.Path + "MedicationInstructionIdentifier", messages);
            }

            if (DispenseItemIdentifier != null)
            {
              DispenseItemIdentifier.Validate(vb.Path + "DispenseItemIdentifier", messages);
            }
      
            if (Observations != null)
            {
              Observations.Validate(vb.Path + "Observations", messages);
            }

        }
        #endregion
    }
}