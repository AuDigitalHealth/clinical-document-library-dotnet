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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models a Australian Organ Donor Register Entry
  /// </summary>
  public class AustralianOrganDonorRegisterEntry
  {
    #region Properties

    /// <summary>
    /// Date of Initial Registration
    /// </summary>
    [CanBeNull]
    public ISO8601DateTime DateOfInitialRegistration { get; set; }

    /// <summary>
    /// Donation Decision
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? DonationDecision { get; set; }

    /// <summary>
    /// Organ And Tissue Donation Details
    /// </summary>
    [CanBeNull]
    [DataMember]
    public OrganAndTissueDonationDetail OrganAndTissueDonationDetails { get; set; }

    #endregion

    #region Constructors
    internal AustralianOrganDonorRegisterEntry()
    {
    }
    #endregion

    #region Validation

    /// <summary>
    /// Validate the SCS Content for the Australian Organ Donor Register Entry
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      vb.ArgumentRequiredCheck("DateOfInitialRegistration", DateOfInitialRegistration);
      vb.ArgumentRequiredCheck("DonationDecision", DonationDecision);

      if (OrganAndTissueDonationDetails != null)
      {
        OrganAndTissueDonationDetails.Validate(path + "OrganAndTissueDonationDetails", messages);
      }
    }

    #endregion

  }
}
