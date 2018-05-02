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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The PrescribingAndDispensingReports class contains all the properties that CDA has identified for 
  /// a PrescribingAndDispensingReports
    /// 
    /// Please use the PrescribingAndDispensingReports() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class PrescribingAndDispensingReports
    {
      #region Properties

      /// <summary>
      /// Summary Of Medication Entries
      /// </summary>
      [CanBeNull]
      public string SectionTitle { get; set; }

      /// <summary>
      /// Summary Of Medication Entries
      /// </summary>
      [CanBeNull]
      public StrucDocText CustomNarrative { get; set; }

      /// <summary>
      /// Medication Entries With Summary
      /// </summary>
      [CanBeNull]
      public List<MedicationEntriesWithSummary> MedicationEntriesWithSummary { get; set; }

      #endregion

      #region Constructors
      internal PrescribingAndDispensingReports()
      {
      }

      #endregion

      #region Validation
      /// <summary>
      /// Validates this Prescribing And Dispensing Reports Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        vb.ArgumentRequiredCheck("SectionTitle", SectionTitle);

        if (vb.ArgumentRequiredCheck("MedicationEntriesWithSummary", MedicationEntriesWithSummary))
          for (var x = 0; x < MedicationEntriesWithSummary.Count; x++)
            if (vb.ArgumentRequiredCheck(string.Format("MedicationEntriesWithSummary[{0}]", x), MedicationEntriesWithSummary[x]))
              MedicationEntriesWithSummary[x].Validate(vb.Path + string.Format("MedicationEntriesWithSummary[{0}]", x), vb.Messages);
      }

      #endregion

    }
}