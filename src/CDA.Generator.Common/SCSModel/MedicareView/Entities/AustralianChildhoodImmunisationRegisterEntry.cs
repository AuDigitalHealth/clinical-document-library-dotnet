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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models Australian Childhood Immunisation Register Entries for Medciare Overview  
  /// </summary>
  public class AustralianChildhoodImmunisationRegisterEntry 
  {
      #region Properties

      /// <summary>
      /// Vaccine Administration Entry
      /// </summary>
      [CanBeNull]
      public VaccineAdministrationEntry VaccineAdministrationEntry { get; set; }

      /// <summary>
      /// Vaccine Cancellation Entry List
      /// </summary>
      [CanBeNull]
      public VaccineCancellationEntry VaccineCancellationEntry { get; set; }

      /// <summary>
      /// Vaccine Cancellation Reason
      /// </summary>
      [CanBeNull]
      public List<VaccineCancellationReason> VaccineCancellationReason { get; set; }

      #endregion

      #region Constructors
      internal AustralianChildhoodImmunisationRegisterEntry()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the SCS Content for the Australian Childhood Immunisation Register Entries
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (VaccineAdministrationEntry == null && VaccineCancellationEntry == null)
          vb.AddValidationMessage(vb.Path + "VaccineAdministrationEntry or VaccineCancellationEntry", null, "Please specify an VaccineAdministrationEntry or a VaccineCancellationEntry.");

        if (VaccineAdministrationEntry != null && VaccineCancellationEntry != null)
          vb.AddValidationMessage(vb.Path + "VaccineAdministrationEntry and VaccineCancellationEntry", null, "Both the VaccineAdministrationEntry and the VaccineCancellationEntry have been specified; only one instance of these is allowed.");

        if (VaccineCancellationReason != null && VaccineCancellationEntry == null)
          vb.AddValidationMessage(vb.Path + "VaccineCancellationReason or VaccineCancellationReason", null, "A VaccineCancellationEntry must be exist for a VaccineCancellationReason to be included.");

        if (VaccineCancellationReason != null)
        {
            for (var x = 0; x < VaccineCancellationReason.Count; x++) VaccineCancellationReason[x].Validate(vb.Path + string.Format("VaccineCancellationReason[{0}]", x), vb.Messages);
        }

        if (VaccineAdministrationEntry != null)
        {
           VaccineAdministrationEntry.Validate("VaccineAdministrationEntry", messages);
        }

        if (VaccineCancellationEntry != null)
        {
           VaccineCancellationEntry.Validate("VaccineCancellationEntry", messages);
        }
      }

      #endregion

  }
}
