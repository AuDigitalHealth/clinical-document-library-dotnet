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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
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
    public class PCEHRPrescriptionItem : IPCEHRPrescriptionItem, IPCEHRPrescriptionItemView
    {
      #region Properties

      /// <summary>
      /// Therapeutic Good Identification
      /// </summary>
      [CanBeNull]
      public ICodableText TherapeuticGoodId { get; set; }

      /// <summary>
      /// Therapeutic Good Strength (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      public String TherapeuticGoodStrength { get; set; }

      /// <summary>
      /// Therapeutic Good Generic Name (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      public String TherapeuticGoodGenericName { get; set; }

      /// <summary>
      /// Directions
      /// </summary>
      [CanBeNull]
      public String Directions { get; set; }

      /// <summary>
      /// Formula
      /// </summary>
      [CanBeNull]
      public String Formula { get; set; }

      /// <summary>
      /// Ingredients and Form (CHEMICAL DESCRIPTION OF MEDICATION) - Form
      /// </summary>
      [CanBeNull]
      public ICodableText Form { get; set; }

      /// <summary>
      /// Reason For Therapeutic Good
      /// </summary>
      [CanBeNull]
      public String ClinicalIndication { get; set; }

      /// <summary>
      /// Administration Details (MEDICATION ADMINISTRATION) - Route
      /// </summary>
      [CanBeNull]
      public ICodableText Route { get; set; }

      /// <summary>
      /// Comment (Medication Instruction Comment)
      /// </summary>
      [CanBeNull]
      public String Comment { get; set; }

      /// <summary>
      /// Dispensing Information
      /// </summary>
      [CanBeNull]
      public DispensingInformation DispensingInformation { get; set; }

      /// <summary>
      /// PBS Manufacturer Code (Administrative Manufacturer Code)
      /// </summary>
      [CanBeNull]
      public Identifier PBSManufacturerCode { get; set; }

      /// <summary>
      /// DateTime Prescription Expires
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateTimePrescriptionExpires { get; set; }

      /// <summary>
      /// DateTime Prescription Expires
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateTimePrescriptionWritten { get; set; }

      /// <summary>
      /// DateTime Prescription Expires
      /// </summary>
      [CanBeNull]
      public Identifier PrescriptionItemIdentifier { get; set; }

      /// <summary>
      ///  Custom Narrative Prescription Item
      /// </summary>
      [CanBeNull]
      public StrucDocText CustomNarrativePrescriptionItem { get; set; }

      /// <summary>
      /// Prescription Record Link (LINK)
      /// </summary>
      [CanBeNull]
      public Link PrescriptionRecordLink { get; set; }

      #endregion

      #region Constructors
      internal PCEHRPrescriptionItem()
      {
      }
      #endregion

      #region Validation


      /// <summary>
      /// Validates this IPrescription Item 
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      void IPCEHRPrescriptionItemView.Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        vb.ArgumentRequiredCheck("DateTimePrescriptionWritten", DateTimePrescriptionWritten);

        if (vb.ArgumentRequiredCheck("PrescriptionRecordLink", PrescriptionRecordLink))
        {
            PrescriptionRecordLink.Validate(vb.Path + "PrescriptionRecordLink", vb.Messages);
        }

        Validate(path, messages);
      }

      /// <summary>
      /// Validates this IPrescription Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      void IPCEHRPrescriptionItem.Validate(string path, List<ValidationMessage> messages)
      {
          var vb = new ValidationBuilder(path, messages);

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
          TherapeuticGoodId.Validate(vb.Path + "PrescriptionItemIdentifier", vb.Messages);

          //1.	Ensure that if an AMT code is used as the primary code for Therapeutic Good Identification, then PBS Manufacturer Code is NOT ALLOWED to be present.
          if (TherapeuticGoodId.Code == CodingSystem.AMTV2.GetAttributeValue<NameAttribute, string>(x => x.Code) || TherapeuticGoodId.Code == CodingSystem.AMTV3.GetAttributeValue<NameAttribute, string>(x => x.Code))
          {
            if (PBSManufacturerCode != null)
            {
              vb.AddValidationMessage("PBSManufacturerCode", String.Empty, "If AMT code is used as the primary code for Therapeutic Good Identification, then PBS Manufacturer Code is NOT ALLOWED to be present");
            }
          }
        }

        if (vb.ArgumentRequiredCheck("DispensingInformation", DispensingInformation))
        {
          DispensingInformation.Validate(vb.Path + "DispensingInformation", vb.Messages);
        }

        if (Form != null)
        {
          Form.Validate(vb.Path + "Form", vb.Messages);
        }

        if (PBSManufacturerCode != null)
        {
          // 2.	Ensure that if a PBS Manufacturer Code is present, then a PBS Item Code MUST BE present as either the primary code or a translation (AMT SHALL NOT be the primary code in this case) for Therapeutic Good Identification
          bool foundPBSCode = false;

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

          PBSManufacturerCode.Validate(vb.Path + "PBSManufacturerCode", vb.Messages);

          if (PBSManufacturerCode.Root != CodingSystem.AustralianPBSManufacturerCode.GetAttributeValue<NameAttribute, string>(x => x.Code))
          {
            vb.AddValidationMessage("PBSManufacturerCode", String.Empty, "Ensure that the correct OID (1.2.36.1.2001.1005.23) is always used for the PBS Manufacturer Code when it is present");
          }

        }

        if (vb.ArgumentRequiredCheck("PrescriptionItemIdentifier", PrescriptionItemIdentifier))
        {
          PrescriptionItemIdentifier.Validate(vb.Path + "PrescriptionItemIdentifier", vb.Messages);
        }

      }

      #endregion

    }
}