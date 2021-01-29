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
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
  /// <summary>
  /// The IDispenseItem interface contains all the properties that CDA has identified for a dispenseItem
  /// </summary>
  public interface IPCEHRDispenseItem 
  {
    #region Properties

    /// <summary>
    /// Status
    /// </summary>
    [CanBeNull]
    MedicationStatus? Status { get; set; }

    /// <summary>
    /// DateTime of Dispense Event (Medication Action DateTime)
    /// </summary>
    [CanBeNull]
    ISO8601DateTime DateTimeOfDispenseEvent { get; set; }

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
    /// Additional Dispensed Item Description (Additional Therapeutic Good Detail)
    /// </summary>
    [CanBeNull]
    String AdditionalDispensedItemDescription { get; set; }

    /// <summary>
    /// Label Instruction (Medication Action Instructions)
    /// </summary>
    [CanBeNull]
    String LabelInstruction { get; set; }

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
    /// Quantity to Dispense (AMOUNT OF MEDICATION) - Quantity Description
    /// </summary>
    [CanBeNull]
    [DataMember]
    String QuantityDescription { get; set; }

    /// <summary>
    /// Comment (Medication Instruction Comment)
    /// </summary>
    [CanBeNull]
    String Comment { get; set; }

    /// <summary>
    /// Brand Substitution Occurred
    /// </summary>
    [CanBeNull]
    Boolean? BrandSubstitutionOccurred { get; set; }

    /// <summary>
    /// Number of this Dispense
    /// </summary>
    [CanBeNull]
    [DataMember]
    int? NumberOfThisDispense { get; set; }

    /// <summary>
    /// Maximum Number of Repeats (Number of Repeats)
    /// </summary>
    [CanBeNull]
    [DataMember]
    int? MaximumNumberOfRepeats { get; set; }

    /// <summary>
    /// PBS Manufacturer Code (Administrative Manufacturer Code)
    /// </summary>
    [CanBeNull]
    Identifier PBSManufacturerCode { get; set; }

    /// <summary>
    /// Unique Pharmacy Prescription Number (Administrative System Identifier)
    /// </summary>
    [CanBeNull]
    String UniquePharmacyPrescriptionNumber { get; set; }

    /// <summary>
    /// Prescription Item Identifier
    /// </summary>
    [CanBeNull]
    Identifier PrescriptionItemIdentifier { get; set; }

    /// <summary>
    /// Dispense Item Identifier (Medication Action Instance Identifier)
    /// </summary>
    [CanBeNull]
    Identifier DispenseItemIdentifier { get; set; }

    /// <summary>
    /// Dispense Item Custom Narrative
    /// </summary>
    [CanBeNull]
    [DataMember]
    StrucDocText CustomNarrativeDispenseItem { get; set; }

    #endregion

    #region Validation

    /// <summary>
    /// Validates this Dispense Item
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages to date, these may be added to within this method</param>
    void Validate(string path, List<ValidationMessage> messages);

    #endregion
  }
}