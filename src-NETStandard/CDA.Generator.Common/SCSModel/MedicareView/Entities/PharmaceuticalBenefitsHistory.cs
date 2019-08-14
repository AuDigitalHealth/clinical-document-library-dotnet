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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models Pharmaceutical Benefits History for Medciare Overview  
  /// </summary>
  public class PharmaceuticalBenefitsHistory 
  {
      #region Properties

      /// <summary>
      /// An Exclusion Statement
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ExclusionStatement ExclusionStatement { get; set; }

      /// <summary>
      /// Pharmaceutical Benefit Items
      /// </summary>
      [CanBeNull]
      [DataMember]
      public PharmaceuticalBenefitItems PharmaceuticalBenefitItems { get; set; }

      #endregion

      #region Constructors
      internal PharmaceuticalBenefitsHistory()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the SCS Content for Pharmaceutical Benefits History
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (ExclusionStatement != null)
        {
          ExclusionStatement.Validate("ExclusionStatement", vb.Messages);
        }

        if (PharmaceuticalBenefitItems != null)
        {
          PharmaceuticalBenefitItems.Validate("PharmaceuticalBenefitItems", vb.Messages);
        }

        if (ExclusionStatement == null && PharmaceuticalBenefitItems == null)
        {
          vb.AddValidationMessage(vb.Path + "ExclusionStatement or PharmaceuticalBenefitItems", null, "Please specify an ExclusionStatement or a PharmaceuticalBenefitItems item");
        }

        if (ExclusionStatement != null && PharmaceuticalBenefitItems != null)
        {
          vb.AddValidationMessage(vb.Path + "ExclusionStatement and PharmaceuticalBenefitItems", null, "Both the ExclusionStatement and the PharmaceuticalBenefitItems items have been specified; only one instance of these is allowed.");
        }
      }

      #endregion

  }
}
