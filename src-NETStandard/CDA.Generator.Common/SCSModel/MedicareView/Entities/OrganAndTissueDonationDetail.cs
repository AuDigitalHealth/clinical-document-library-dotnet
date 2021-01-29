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
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models a Organ And Tissue Donation Detail
  /// </summary>
  public class OrganAndTissueDonationDetail
  {
    #region Properties

    /// <summary>
    /// Bone Tissue Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? BoneTissueIndicator { get; set; }

    /// <summary>
    /// Eye Tissue Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? EyeTissueIndicator { get; set; }

    /// <summary>
    /// Heart Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? HeartIndicator { get; set; }

    /// <summary>
    /// Heart Valve Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? HeartValveIndicator { get; set; }

    /// <summary>
    /// Heart Valve Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? KidneyIndicator { get; set; }

    /// <summary>
    /// Liver Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? LiverIndicator { get; set; }

    /// <summary>
    /// Lungs Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? LungsIndicator { get; set; }

    /// <summary>
    /// Pancreas Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? PancreasIndicator { get; set; }

    /// <summary>
    /// Pancreas Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? SkinTissueIndicator { get; set; }

    #endregion

    #region Constructors
    internal OrganAndTissueDonationDetail()
    {
    }
    #endregion

    #region Validation

    /// <summary>
    /// Validate the SCS Content for the Organ And Tissue Donation Detail
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      vb.ArgumentRequiredCheck("BoneTissueIndicator", BoneTissueIndicator);
      vb.ArgumentRequiredCheck("EyeTissueIndicator", EyeTissueIndicator);
      vb.ArgumentRequiredCheck("HeartIndicator", HeartIndicator);
      vb.ArgumentRequiredCheck("HeartValveIndicator", HeartValveIndicator);
      vb.ArgumentRequiredCheck("KidneyIndicator", KidneyIndicator);
      vb.ArgumentRequiredCheck("LiverIndicator", LiverIndicator);
      vb.ArgumentRequiredCheck("LungsIndicator", LungsIndicator);
      vb.ArgumentRequiredCheck("PancreasIndicator", PancreasIndicator);
      vb.ArgumentRequiredCheck("SkinTissueIndicator", SkinTissueIndicator);
    }

    #endregion

  }
}
