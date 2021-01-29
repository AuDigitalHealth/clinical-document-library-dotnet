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
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IPrescriptionItem interface contains all the properties that CDA has identified for a IPrescriptionItem
    /// </summary>
    public interface IPCEHRPrescriptionItem  
    {
      #region Properties

      /// <summary>
      /// Therapeutic Good Identification
      /// </summary>
      [CanBeNull]
      ICodableText TherapeuticGoodId { get; set; }

      /// <summary>
      /// Therapeutic Good Strength (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      String TherapeuticGoodStrength { get; set; }

      /// <summary>
      /// Therapeutic Good Generic Name (Additional Therapeutic Good Detail)
      /// </summary>
      [CanBeNull]
      String TherapeuticGoodGenericName { get; set; }

      /// <summary>
      /// Directions
      /// </summary>
      [CanBeNull]
      String Directions { get; set; }

      /// <summary>
      /// Formula
      /// </summary>
      [CanBeNull]
      String Formula { get; set; }

      /// <summary>
      /// Ingredients and Form (CHEMICAL DESCRIPTION OF MEDICATION) - Form
      /// </summary>
      [CanBeNull]
      ICodableText Form { get; set; }

      /// <summary>
      /// Clinical Indication
      /// </summary>
      [CanBeNull]
      String ClinicalIndication { get; set; }

      /// <summary>
      /// Administration Details (MEDICATION ADMINISTRATION) - Route
      /// </summary>
      [CanBeNull]
      ICodableText Route { get; set; }

      /// <summary>
      /// Comment (Medication Instruction Comment)
      /// </summary>
      [CanBeNull]
      String Comment { get; set; }

      /// <summary>
      /// Dispensing Information
      /// </summary>
      [CanBeNull]
      DispensingInformation DispensingInformation { get; set; }

      /// <summary>
      /// PBS Manufacturer Code (Administrative Manufacturer Code)
      /// </summary>
      [CanBeNull]
      Identifier PBSManufacturerCode { get; set; }

      /// <summary>
      /// DateTime Prescription Expires
      /// </summary>
      [CanBeNull]
      ISO8601DateTime DateTimePrescriptionExpires { get; set; }

      /// <summary>
      /// DateTime Prescription Expires
      /// </summary>
      [CanBeNull]
      Identifier PrescriptionItemIdentifier { get; set; }

      /// <summary>
      ///  Custom Narrative Prescription Item
      /// </summary>
      [CanBeNull]
      StrucDocText CustomNarrativePrescriptionItem { get; set; }

      #endregion

      #region Validation
      /// <summary>
      /// Validates this Prescription Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      void Validate(string path, List<ValidationMessage> messages);

      #endregion

    }
}