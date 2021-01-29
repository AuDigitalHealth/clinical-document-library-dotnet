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
  /// Models Australian Childhood Immunisation Register History (ACIR) Component  for Medicare Overview  
  /// </summary>
  public class AustralianChildhoodImmunisationRegisterHistory 
  {
      #region Properties

      /// <summary>
      /// An Exclusion Statement
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ExclusionStatement ExclusionStatement { get; set; }

      /// <summary>
      /// Australian a list of  Childhood Immunisation Register Entries
      /// </summary>
      [CanBeNull]
      [DataMember]
      public AustralianChildhoodImmunisationRegisterEntries AustralianChildhoodImmunisationRegisterEntries { get; set; }

      #endregion

      #region Constructors
      internal AustralianChildhoodImmunisationRegisterHistory()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the SCS Content for the Australian Childhood Immunisation Register History Entries
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (ExclusionStatement == null && AustralianChildhoodImmunisationRegisterEntries == null)
          vb.AddValidationMessage(vb.Path + "ExclusionStatement or AustralianChildhoodImmunisationRegister", null, "Please specify an ExclusionStatement or a AustralianChildhoodImmunisationRegisterEntry.");

        if (ExclusionStatement != null && AustralianChildhoodImmunisationRegisterEntries != null)
          vb.AddValidationMessage(vb.Path + "ExclusionStatement and AustralianChildhoodImmunisationRegisterEntry", null, "Both the ExclusionStatement and the AustralianChildhoodImmunisationRegisterEntry have been specified; only one instance of these is allowed.");

        if (ExclusionStatement != null)
          ExclusionStatement.Validate("ExclusionStatement", messages);

        if (AustralianChildhoodImmunisationRegisterEntries != null)
          AustralianChildhoodImmunisationRegisterEntries.Validate(vb.Path + "AustralianChildhoodImmunisationRegisterEntries", vb.Messages);

      }

      #endregion

  }
}
