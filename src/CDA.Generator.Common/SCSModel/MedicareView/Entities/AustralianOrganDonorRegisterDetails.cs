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
  /// Models a Australian Organ Donor Register Details 
  /// </summary>
  public class AustralianOrganDonorRegisterDetails
  {
    #region Properties

    /// <summary>
    /// Provide a custom Narrative 
    /// </summary>
    [CanBeNull]
    public StrucDocText CustomNarrative { get; set; }

    /// <summary>
    /// Australian Organ Donor Register Entry
    /// </summary>
    [CanBeNull]
    [DataMember]
    public AustralianOrganDonorRegisterEntry AustralianOrganDonorRegisterEntry { get; set; }

    /// <summary>
    /// A list of Pharmaceutical Benefit Items Document Provenance
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Link AustralianOrganDonorRegisterDetailsDocumentLink { get; set; }

    #endregion

    #region Constructors
    internal AustralianOrganDonorRegisterDetails()
    {
    }
    #endregion

    #region Validation

    /// <summary>
    /// Validate the SCS Content for the Australian Organ Donor Register Details 
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      if (vb.ArgumentRequiredCheck("AustralianOrganDonorRegisterEntry", AustralianOrganDonorRegisterEntry))
      {
        AustralianOrganDonorRegisterEntry.Validate("AustralianOrganDonorRegisterEntry", messages);
      }

      if (vb.ArgumentRequiredCheck("AustralianOrganDonorRegisterDetailsProvenanceLink", AustralianOrganDonorRegisterDetailsDocumentLink))
      {
        AustralianOrganDonorRegisterDetailsDocumentLink.Validate("AustralianOrganDonorRegisterDetailsProvenanceLink", messages);
      }

    }

    #endregion

  }
}
