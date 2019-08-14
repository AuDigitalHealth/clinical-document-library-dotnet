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
using CDA.Generator.Common.SCSModel.ATS.ETP.Enum;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Entities
{
  /// <summary>
  /// The PrescriptionRequestItem class contains all the properties that CDA has identified for 
  /// a prescription request item
  /// 
  /// Please use the CreatePrescriptionRequestItem() method on the appropriate parent SCS object to 
  /// instantiate this class.
  /// </summary>
  [Serializable]
  [DataContract]
  public class PrescriptionRequestItem
  {
    #region Properties

    /// <summary>
    /// Custom Narrative
    /// </summary>
    [CanBeNull]
    [DataMember]
    public StrucDocText CustomNarrative { get; set; }

    /// <summary>
    /// Prescription Request Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier PrescriptionRequestItemIdentifier { get; set; }

    /// <summary>
    /// Prescription Request Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier DispenseItemIdentifier { get; set; }

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
    public String Formula { get; set; }

    /// <summary>
    /// The Directions, E.g. two tablets
    /// </summary>
    [CanBeNull]
    [DataMember]
    public String Directions { get; set; }

    /// <summary>
    /// Structured Dose
    /// </summary>
    [CanBeNull]
    public QuantityUnit StructuredDose { get; set; }

    /// <summary>
    /// Timing
    /// </summary>
    [CanBeNull]
    public Timing Timing { get; set; }

    /// <summary>
    /// Administration Details
    /// </summary>
    [CanBeNull]
    [DataMember]
    public AdministrationDetails AdministrationDetails { get; set; }

    /// <summary>
    /// Administration Details
    /// </summary>
    [CanBeNull]
    [DataMember]
    public QuantityUnit QuantityToDispense { get; set; }

    /// <summary>
    /// A boolean indicating if brand substitution is allowed
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Boolean? BrandSubstituteNotAllowed { get; set; }

    /// <summary>
    /// A PBS RPBS ItemCode
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
    /// PBS RPBS Manufacturer Code
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier PBSRPBSManufacturerCode { get; set; }

    /// <summary>
    /// PBS Extemporaneous Ingredient
    /// </summary>
    [CanBeNull]
    [DataMember]
    public List<PBSExtemporaneousIngredient> PBSExtemporaneousIngredient { get; set; }

    /// <summary>
    /// PBS / RPBS authority Prescription number
    /// </summary>
    [CanBeNull]
    [DataMember]
    public String PBSRPBSAuthorityPrescriptionNumber { get; set; }

    /// <summary>
    /// PBS RPBS authority approval number
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
    /// Additional comments
    /// </summary>
    [CanBeNull]
    [DataMember]
    public String AdditionalComments { get; set; }

    #endregion

    #region Constructors
    internal PrescriptionRequestItem()
    {
    }
    #endregion

    #region Validation
    /// <summary>
    /// Validates this Prescription Request Item
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages to date, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      if (vb.ArgumentRequiredCheck("PrescriptionRequestItemIdentifier", PrescriptionRequestItemIdentifier))
      {
        PrescriptionRequestItemIdentifier.Validate(vb.Path + "PrescriptionRequestItemIdentifier", vb.Messages);
      }

      if (vb.ArgumentRequiredCheck("DispenseItemIdentifier", DispenseItemIdentifier))
      {
        DispenseItemIdentifier.Validate(vb.Path + "DispenseItemIdentifier", vb.Messages);
      }

      if (vb.ArgumentRequiredCheck("TherapeuticGoodIdentification", TherapeuticGoodIdentification))
      {
        if (PBSRPBSItemCode != null && TherapeuticGoodIdentification.OriginalText.IsNullOrEmptyWhitespace())
        {
          vb.AddValidationMessage(vb.PathName, null, "TherapeuticGoodIdentification's OriginalText is a required field when PBSRPBSItemCode is present");
        }

        if (TherapeuticGoodIdentification != null) TherapeuticGoodIdentification.Validate(vb.Path + "TherapeuticGoodIdentification", vb.Messages);
      }

      vb.ArgumentRequiredCheck("Directions", Directions);

      if (StructuredDose != null)
      {
        StructuredDose.Validate(vb.Path + "Directions", vb.Messages);
      }

      if (Timing != null)
      {
        Timing.Validate(vb.Path + "Timing", vb.Messages);
      }

      if (vb.ArgumentRequiredCheck("QuantityToDispense", QuantityToDispense))
      {
        QuantityToDispense.ValidateDispensingUnit(vb.Path + "QuantityToDispense", vb.Messages);
      }

      if (AdministrationDetails != null)
      {
        AdministrationDetails.Validate(vb.Path + "AdministrationDetails", vb.Messages);
      }

      vb.ArgumentRequiredCheck("PBSPrescriptionType", QuantityToDispense);

      if (PBSCloseTheGapBenefit != null)
      {
        PBSCloseTheGapBenefit.Validate(vb.Path + "PBSCloseTheGapBenefit", vb.Messages);
      }

      if (PBSRPBSItemCode != null)
      {
        PBSRPBSItemCode.Validate(vb.Path + "PBSRPBSItemCode", vb.Messages);
      }

      if (PBSRPBSManufacturerCode != null)
      {
        PBSRPBSManufacturerCode.Validate(vb.Path + "PBSRPBSManufacturerCode", vb.Messages);
      }

      if (PBSExtemporaneousIngredient != null)
      {
        for (var x = 0; x < PBSExtemporaneousIngredient.Count; x++)
        {
          PBSExtemporaneousIngredient[x].Validate(vb.Path + string.Format("PBSExtemporaneousIngredient[{0}]", x), vb.Messages);
        }
      }

    }

    #endregion
  }
}