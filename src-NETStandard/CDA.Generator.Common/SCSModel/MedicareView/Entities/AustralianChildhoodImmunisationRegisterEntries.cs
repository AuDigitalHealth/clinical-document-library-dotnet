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
  /// Models Australian Childhood Immunisation Register Entries for Medicare Overview  
  /// </summary>
  public class AustralianChildhoodImmunisationRegisterEntries
  {
      #region Properties

      /// <summary>
      /// Provide a custom Narrative 
      /// </summary>
      [CanBeNull]
      public StrucDocText CustomNarrative { get; set; }

      /// <summary>
      /// Vaccine Administration Entry
      /// </summary>
      [CanBeNull]
      public List<AustralianChildhoodImmunisationRegisterEntry> AustralianChildhoodImmunisationRegisterEntry { get; set; }

      /// <summary>
      /// A list of Australian Childhood Immunisation Register Document Provenance Link 
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Link AustralianChildhoodImmunisationRegisterDocumentLink { get; set; }

      #endregion

      #region Constructors
      internal AustralianChildhoodImmunisationRegisterEntries()
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

        if (vb.ArgumentRequiredCheck("AustralianChildhoodImmunisationRegisterEntry", AustralianChildhoodImmunisationRegisterEntry))
        {
            for (var x = 0; x < AustralianChildhoodImmunisationRegisterEntry.Count; x++)
            {
                AustralianChildhoodImmunisationRegisterEntry[x].Validate(vb.Path + string.Format("AustralianChildhoodImmunisationRegisterEntry[{0}]", x), vb.Messages);

                //Check to see if BOTH exist in single entry
                if (AustralianChildhoodImmunisationRegisterEntry[x].VaccineAdministrationEntry != null && AustralianChildhoodImmunisationRegisterEntry[x].VaccineCancellationEntry != null)
                    vb.AddValidationMessage("AustralianChildhoodImmunisationRegisterEntry",null, "Exactly one 'VACCINE ADMINISTRATION ENTRY' OR exactly one 'VACCINE CANCELLATION ENTRY' SHALL be present per instance of the parent choice ('AUSTRALIAN CHILDHOOD IMMUNISATION REGISTER ENTRY').");
            }

            //AustralianChildhoodImmunisationRegisterEntry[0].
            //PW: 28/08/2016
            //THIS IS WRONG - SHOULD ALLOW MULTIPLE ENTRIES, BUT JUST A SINGLE 'ENTRY' or 'CANCELLATION' WITHIN an ENTRY
            //if (AustralianChildhoodImmunisationRegisterEntry.Count != 1)
            //{
                //vb.AddValidationMessage("AustralianChildhoodImmunisationRegisterEntry",null, "Exactly one 'VACCINE ADMINISTRATION ENTRY' OR exactly one 'VACCINE CANCELLATION ENTRY' SHALL be present per instance of the parent choice ('AUSTRALIAN CHILDHOOD IMMUNISATION REGISTER ENTRY').");
            //}
        }

        if (vb.ArgumentRequiredCheck("AustralianChildhoodImmunisationRegisterDocumentProvenanceLink", AustralianChildhoodImmunisationRegisterDocumentLink))
        {
          AustralianChildhoodImmunisationRegisterDocumentLink.Validate("AustralianChildhoodImmunisationRegisterDocumentProvenanceLink", messages);
        }
      }

      #endregion

  }
}
