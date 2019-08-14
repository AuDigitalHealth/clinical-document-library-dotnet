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

using System.Collections.Generic;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models a Pharmaceutical Benefit Item
  /// </summary>
  public class PharmaceuticalBenefitItem 
  {
      #region Properties

      /// <summary>
      /// PBS/RPBS Item Code
      /// </summary>
      [CanBeNull]
      public string PBSRPBSItemCode { get; set; }

      /// <summary>
      /// PBS/RPBS Manufacturer Code
      /// </summary>
      [CanBeNull]
      public string PBSRPBSManufacturerCode { get; set; }

      /// <summary>
      /// Brand (Pharmaceutical Item Brand)
      /// </summary>
      [CanBeNull]
      public string Brand { get; set; }

      /// <summary>
      /// Item Generic Name (Pharmaceutical Item Generic Name)
      /// </summary>
      [CanBeNull]
      public string ItemGenericName { get; set; }

      /// <summary>
      /// Item Form and Strength (Pharmaceutical Item Form and Strength)
      /// </summary>
      [CanBeNull]
      public string ItemFormAndStrength { get; set; }

      /// <summary>
      /// Date of Supply
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateOfSupply { get; set; }

      /// <summary>
      /// Date of Prescribing
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateOfPrescribing { get; set; }

      /// <summary>
      /// Quantity
      /// </summary>
      public int? Quantity { get; set; }

      /// <summary>
      /// Number of Repeats
      /// </summary>
      public int? NumberOfRepeats { get; set; }

      /// <summary>
      /// A list of Pharmaceutical Benefit Items Document Link
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Link PharmaceuticalBenefitItemDocumentLink { get; set; }

      #endregion

      #region Constructors
      internal PharmaceuticalBenefitItem()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the SCS Content for a Pharmaceutical Benefit Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        vb.ArgumentRequiredCheck("PBSRPBSItemCode", PBSRPBSItemCode);
        vb.ArgumentRequiredCheck("Brand", Brand);
        vb.ArgumentRequiredCheck("ItemGenericName", ItemGenericName);
        vb.ArgumentRequiredCheck("ItemFormAndStrength", ItemFormAndStrength);
        vb.ArgumentRequiredCheck("DateOfSupply", DateOfSupply);
        vb.ArgumentRequiredCheck("DateOfPrescribing", DateOfPrescribing);
        vb.ArgumentRequiredCheck("Quantity", Quantity);
        vb.ArgumentRequiredCheck("NumberOfRepeats", NumberOfRepeats);

        if (vb.ArgumentRequiredCheck("PharmaceuticalBenefitItemDocumentLink", PharmaceuticalBenefitItemDocumentLink))
          PharmaceuticalBenefitItemDocumentLink.Validate(vb.Path + "PharmaceuticalBenefitItemDocumentLink", vb.Messages);
      }

      #endregion

  }
}
