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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The MedicationEntriesWithSummary class contains all the properties that CDA has identified for 
    /// MedicationEntriesWithSummary item
    /// 
    /// Please use the MedicationEntries() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class MedicationEntriesWithSummary
    {
      #region Properties

      /// <summary>
      /// A General Statement for Medciare Overview Exclusion Statement (EXCLUSION STATEMENT)
      /// </summary>
      [CanBeNull]
      [DataMember]
      public string SectionTitle { get; set; }

      /// <summary>
      /// Summary Of Medication Entries
      /// </summary>
      public SummaryOfMedicationEntries SummaryOfMedicationEntries { get; set; }

      /// <summary>
      /// Medication Entries
      /// </summary>
      public List<MedicationEntry> MedicationEntries { get; set; }


      #endregion

      #region Constructors
      internal MedicationEntriesWithSummary()
      {
          
      }
      #endregion

      #region Validation
      /// <summary>
      /// Validates this Medication Entries With Summary Object
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        vb.ArgumentRequiredCheck("SectionTitle", SectionTitle);

        if (vb.ArgumentRequiredCheck("SummaryOfMedicationEntries", SummaryOfMedicationEntries))
           SummaryOfMedicationEntries.Validate(vb.Path + "SummaryOfMedicationEntries", vb.Messages);

        if (vb.ArgumentRequiredCheck("MedicationEntries", MedicationEntries))
        {
          var prescriptionItem = 0;
          var despenseItem = 0;

          for (var x = 0; x < MedicationEntries.Count; x++)
          {
            if (MedicationEntries[x] != null)
            {
              if (MedicationEntries[x].DispenseItem != null)
                despenseItem++;              

              if (MedicationEntries[x].PrescriptionItem != null)
                prescriptionItem++;

              MedicationEntries[x].Validate(string.Format("{0}MedicationEntries[{1}]", vb.Path, x), vb.Messages);
            }
          }

          if (prescriptionItem > 1)
          {
            vb.AddValidationMessage(vb.Path + "MedicationEntriesWithSummary", null, "Can contain at most 1 prescription item");
          }

        }
      }

      #endregion

    }
}