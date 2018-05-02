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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models Medicare DVA Funded Service for Medciare Overview 
  /// </summary>
  public class MedicareDVAFundedService
  {
    #region Properties

    /// <summary>
    /// Provide a Date of Service
    /// </summary>
    [CanBeNull]
    public ISO8601DateTime DateOfService { get; set; }

    /// <summary>
    /// Medicare MBS/DVA Item
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText MedicareMBSDVAItem { get; set; }

    /// <summary>
    /// Service in Hospital Indicator
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? ServiceInHospitalIndicator { get; set; }

    /// <summary>
    /// Service Requester
    /// </summary>
    [CanBeNull]
    [DataMember]
    public IParticipationServiceRequester ServiceRequester { get; set; }

    /// <summary>
    /// Service Provider
    /// </summary>
    [CanBeNull]
    [DataMember]
    public IParticipationServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Medicare/DVA Funded Services Document Provenance (LINK)
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Link MedicareDVAFundedServicesDocumentLink { get; set; }

    #endregion

    #region Constructors
    internal MedicareDVAFundedService()
    {
    }
    #endregion

    #region Validation

    /// <summary>
    /// Validate the SCS Content for the Medicare DVA Funded Service
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      vb.ArgumentRequiredCheck("DateOfService", DateOfService);

      if (vb.ArgumentRequiredCheck("MedicareMBSDVAItem", MedicareMBSDVAItem))
      {
         MedicareMBSDVAItem.Validate(path + "MedicareMBSDVAItem", messages);
      }

      if (ServiceRequester != null)
      {
        ServiceRequester.Validate(path + "ServiceRequester", messages);
      }

      if (ServiceProvider != null)
      {
        ServiceProvider.Validate(path + "ServiceProvider", messages);
      }

      vb.ArgumentRequiredCheck("MedicareDVAFundedServicesDocumentProvenance", MedicareDVAFundedServicesDocumentLink);

      if (MedicareDVAFundedServicesDocumentLink != null)
      {
        MedicareDVAFundedServicesDocumentLink.Validate(vb.Path + "MedicareDVAFundedService", vb.Messages);
      }
    }

    #endregion

  }
}
