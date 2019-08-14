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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models Pharmaceutical Benefit Items  
  /// </summary>
  public class PharmaceuticalBenefitItems 
  {
      #region Properties

      /// <summary>
      /// Provide a custom Narrative 
      /// </summary>
      [CanBeNull]
      public StrucDocText CustomNarrative { get; set; }

      /// <summary>
      /// Pharmaceutical Benefit Item
      /// </summary>
      [CanBeNull]
      public List<PharmaceuticalBenefitItem> PharmaceuticalBenefitItemList { get; set; }

      #endregion

      #region Constructors
      internal PharmaceuticalBenefitItems()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the SCS Content for the Pharmaceutical Benefit Items
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (vb.ArgumentRequiredCheck("PharmaceuticalBenefitItemList", PharmaceuticalBenefitItemList))
         {
           for (var x = 0; x < PharmaceuticalBenefitItemList.Count; x++)
             PharmaceuticalBenefitItemList[x].Validate(vb.Path + string.Format("PharmaceuticalBenefitItemList[{0}]", x), vb.Messages);
         }
      }

      #endregion
  }
}
