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
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The DispenseItem class contains all the properties that CDA has identified for an dispense item
    /// 
    /// Please use the CreateDispenseItem() method on the appropriate parent SCS object to instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class PCEHRDispenseItem : IPCEHRDispenseItem, IPCEHRDispenseItemView
    {
      #region Properties

      /// <summary>
      /// Status
      /// </summary>
      [CanBeNull]
      [DataMember]
      public MedicationStatus? Status { get; set; }

      /// <summary>
      /// DateTime of Dispense Event (Medication Action DateTime)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ISO8601DateTime DateTimeOfDispenseEvent { get; set; }

      /// <summary>
      /// Therapeutic Good Identification
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ICodableText TherapeuticGoodId { get; set; }

      /// <summary>
      /// Therapeutic Good Strength (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public String TherapeuticGoodStrength { get; set; }

      /// <summary>
      /// Therapeutic Good Generic Name (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public String TherapeuticGoodGenericName { get; set; }

      /// <summary>
      /// Additional Dispensed Item Description (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public String AdditionalDispensedItemDescription { get; set; }

      /// <summary>
      /// Label Instruction (Medication Action Instructions)
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
      /// Ingredients and Form (CHEMICAL DESCRIPTION OF MEDICATION) - Form
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ICodableText Form { get; set; }

      /// <summary>
      /// Quantity to Dispense (AMOUNT OF MEDICATION) - Quantity Description
      /// </summary>
      [CanBeNull]
      [DataMember]
      public String QuantityDescription { get; set; }

      /// <summary>
      /// Comment (Medication Instruction Comment)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public String Comment { get; set; }

      /// <summary>
      /// Brand Substitution Occurred
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Boolean? BrandSubstitutionOccurred{ get; set; }

      /// <summary>
      /// Number of this Dispense
      /// </summary>
      [CanBeNull]
      [DataMember]
      public int? NumberOfThisDispense { get; set; }

      /// <summary>
      /// Maximum Number of Repeats (Number of Repeats)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public int? MaximumNumberOfRepeats { get; set; }

      /// <summary>
      /// PBS Manufacturer Code (Administrative Manufacturer Code)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Identifier PBSManufacturerCode { get; set; }

      /// <summary>
      /// Unique Pharmacy Prescription Number (Administrative System Identifier)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public String UniquePharmacyPrescriptionNumber { get; set; }

      /// <summary>
      /// Prescription Item Identifier
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Identifier PrescriptionItemIdentifier { get; set; }

      /// <summary>
      /// Dispense Item Identifier (Medication Action Instance Identifier)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Identifier DispenseItemIdentifier  { get; set; }

      /// <summary>
      /// Dispense Item Custom Narrative
      /// </summary>
      [CanBeNull]
      [DataMember]
      public StrucDocText  CustomNarrativeDispenseItem { get; set; }

      /// <summary>
      /// DispenseItem - Dispense Record Link (LINK)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Link  DispenseRecordLink { get; set; }

      #endregion

      #region Constructors
      internal PCEHRDispenseItem()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validates this Dispense Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      void IPCEHRDispenseItem.Validate(string path, List<ValidationMessage> messages)
      {
        Validate(path, messages);
      }

      /// <summary>
      /// Validates this Dispense Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      void IPCEHRDispenseItemView.Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (vb.ArgumentRequiredCheck("DispenseRecordLink", DispenseRecordLink))
        {
          DispenseRecordLink.Validate(vb.Path + "DispenseRecordLink", vb.Messages);
        }

         Validate(path, messages);
      }

      /// <summary>
      /// This is a generic validation function which is called by both interfaces 
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("TherapeuticGoodId", TherapeuticGoodId))
          {
              TherapeuticGoodId.Validate(vb.Path + "TherapeuticGoodId", vb.Messages);

              //1.	Ensure that if an AMT code is used as the primary code for Therapeutic Good Identification, then PBS Manufacturer Code is NOT ALLOWED to be present.
              if (TherapeuticGoodId.Code == CodingSystem.AMTV2.GetAttributeValue<NameAttribute, string>(x => x.Code) || TherapeuticGoodId.Code == CodingSystem.AMTV3.GetAttributeValue<NameAttribute, string>(x => x.Code))
              {
                  if (PBSManufacturerCode != null)
                  {
                     vb.AddValidationMessage("PBSManufacturerCode", String.Empty, "If AMT code is used as the primary code for Therapeutic Good Identification, then PBS Manufacturer Code is NOT ALLOWED to be present");
                  }
              }
          }

          if (vb.ArgumentRequiredCheck("DispenseItemIdentifier", DispenseItemIdentifier))
          {
              DispenseItemIdentifier.Validate(vb.Path + "DispenseItemIdentifier", vb.Messages);
          }

          if (Form != null)
          {
              Form.Validate(vb.Path + "Form", vb.Messages);
          }

          if (PBSManufacturerCode != null)
          {
            // 2.	Ensure that if a PBS Manufacturer Code is present, then a PBS Item Code MUST BE present as either the primary code or a translation (AMT SHALL NOT be the primary code in this case) for Therapeutic Good Identification
            var foundPBSCode = false;

            if (TherapeuticGoodId.Translations != null)
            {
              foreach (var translations in TherapeuticGoodId.Translations.Where(translations => translations.Code == CodingSystem.PBSCode.GetAttributeValue<NameAttribute, string>(x => x.Code) ||
                                                                                                translations.CodeSystemName == CodingSystem.PBSCode.GetAttributeValue<NameAttribute, string>(x => x.Name)))
              {
                foundPBSCode = true;
              }
            }

            if (TherapeuticGoodId.Code == CodingSystem.PBSCode.GetAttributeValue<NameAttribute, string>(x => x.Code) || TherapeuticGoodId.CodeSystemName == CodingSystem.PBSCode.GetAttributeValue<NameAttribute, string>(x => x.Name))
            {
                foundPBSCode = true;
            }

            if (foundPBSCode == false)
            {
              vb.AddValidationMessage("PBSManufacturerCode", String.Empty, "If a PBS Manufacturer Code is present, then a PBS Item Code MUST BE present as either the primary code or a translation for the for Therapeutic Good Identification");
            }

            if (PBSManufacturerCode.Root != CodingSystem.AustralianPBSManufacturerCode.GetAttributeValue<NameAttribute, string>(x => x.Code))
            {
              vb.AddValidationMessage("PBSManufacturerCode", String.Empty, "Ensure that the correct OID (1.2.36.1.2001.1005.23) is always used for the PBS Manufacturer Code when it is present");
            }

            PBSManufacturerCode.Validate(vb.Path + "PBSManufacturerCode", vb.Messages);
          }

          if (PrescriptionItemIdentifier != null)
          {
            PrescriptionItemIdentifier.Validate(vb.Path + "PrescriptionItemIdentifier", vb.Messages);
          }

         vb.ArgumentRequiredCheck("Status", Status);
         vb.ArgumentRequiredCheck("DateTimeOfDispenseEvent", DateTimeOfDispenseEvent);

      }

        #endregion
    }
}