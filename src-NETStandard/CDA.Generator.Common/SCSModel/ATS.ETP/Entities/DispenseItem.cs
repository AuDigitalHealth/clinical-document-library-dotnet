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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Entities
{
    /// <summary>
    /// The EDispenseItem class contains all the properties that CDA has identified for an dispense item
    /// 
    /// Please use the CreateDispenseItem() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class DispenseItem 
    {
        #region Properties

        /// <summary>
        /// DateTime Of Dispense Event
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StatusCode StatusCode { get; set; }

        /// <summary>
        /// StatusCode
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTimeOfDispenseEvent{ get; set; }

        /// <summary>
        /// Dispense item ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier DispenseItemIdentifier { get; set; }

        /// <summary>
        /// The prescription item Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier PrescriptionItemIdentifier { get; set; }

        /// <summary>
        /// Therapeutic good ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TherapeuticGoodIdentification { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String LabelInstruction { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Formula { get; set; }

        /// <summary>
        /// A list of PbsExtemporaneousIngredient
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<PBSExtemporaneousIngredient> PBSExtemporaneousIngredient { get; set; }

        /// <summary>
        /// Quantity To Dispense
        /// </summary>
        [CanBeNull]
        [DataMember]
        public QuantityUnit QuantityToDispense { get; set; }

        /// <summary>
        /// A boolean indicating if brand substitution is Occurred
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? BrandSubstituteOccurred { get; set; }

        /// <summary>
        /// The maximum numer of repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// The number of times this item has been dispensed
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? NumberOfThisDispense { get; set; }

        /// <summary>
        /// A PBS RPBS ItemCode
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableTranslation PBSRPBSItemCode { get; set; }

        /// <summary>
        /// PBS RPBS Manufacturer Code
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier PBSRPBSManufacturerCode { get; set; }

        /// <summary>
        /// A PBSCloseTheGapBenefit
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier PBSCloseTheGapBenefit { get; set; }

        /// <summary>
        /// The claim category type, E.g. SafetyNetEntitlementCardBenefit
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ClaimCategoryType ClaimCategoryType { get; set; }

        /// <summary>
        /// Under Co-Payment
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ClaimCategoryType  UnderCoPayment { get; set; }

        /// <summary>
        /// A boolean indicating early suppy with pharmaceutical benefit 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? EarySupplyWithPharmaceuticalBenefit { get; set; }

        /// <summary>
        /// Additional comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AdditionalComments { get; set; }

        /// <summary>
        /// The setting or category in which the patient has received their medication
        /// </summary>
        [CanBeNull]
        [DataMember]
        public PatientCategory? PatientCategory { get; set; }

        /// <summary>
        /// A string assignment by the Australian Government to 
        /// identify a Residential Aged Care Facility
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String RACFId { get; set; }

        #endregion

        #region Constructors
        internal DispenseItem()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Dispense Item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("DispenseItemId", DispenseItemIdentifier);
            vb.ArgumentRequiredCheck("DateTimeOfDispenseEvent", DateTimeOfDispenseEvent);
            vb.ArgumentRequiredCheck("PrescriptionItemIdentifier", PrescriptionItemIdentifier);

            if (vb.ArgumentRequiredCheck("TherapeuticGoodIdentification", TherapeuticGoodIdentification))
            {
              if (PBSRPBSItemCode != null && TherapeuticGoodIdentification.OriginalText.IsNullOrEmptyWhitespace())
              {
                vb.AddValidationMessage(vb.PathName, null, "TherapeuticGoodIdentification's OriginalText is a required field when PBSRPBSItemCode is present");
              }
            }

            vb.ArgumentRequiredCheck("BrandSubstituteOccurred", BrandSubstituteOccurred);
            vb.ArgumentRequiredCheck("MaximumNumberOfRepeats", MaximumNumberOfRepeats);
            vb.ArgumentRequiredCheck("NumberOfThisDispense", NumberOfThisDispense);
            vb.ArgumentRequiredCheck("ClaimCategoryType", ClaimCategoryType);
            vb.ArgumentRequiredCheck("StatusCode", StatusCode);
            vb.ArgumentRequiredCheck("PatientCategory", PatientCategory);

            // PBS RPBS ManufacturerCode
            if (PBSRPBSManufacturerCode != null)
            {
                PBSRPBSManufacturerCode.Validate(path, messages);
            }

            // PBS Extemporaneous Ingredient
            if (PBSExtemporaneousIngredient != null)
            {
              if (PBSExtemporaneousIngredient != null)
                for (int index = 0; index < PBSExtemporaneousIngredient.Count; index++)
                {
                  var ingredient = PBSExtemporaneousIngredient[index];
                  ingredient.Validate(path + string.Format("PBSExtemporaneousIngredient[{0}]", index), vb.Messages);
                }
            }

            // Quantity To Dispense
            if (vb.ArgumentRequiredCheck("QuantityToDispense", QuantityToDispense))
            {
              QuantityToDispense.ValidateDispensingUnit(path, messages);
            }
        }

        #endregion
    }
}