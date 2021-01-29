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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Pathology
{
  /// <summary>
  /// This class encapsulates the Requested Service
  /// </summary>
  public class RequestedService
  {
    /// <summary>
    /// Provide a custom Narrative 
    /// </summary>
    [CanBeNull]
    [DataMember]
    public StrucDocText CustomNarrative { get; set; }

    /// <summary>
    /// Pathology Test Result GroupName
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText RequestedServiceDescription { get; set; }

    /// <summary>
    /// Service Booking Status
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ServiceBookingStatus? ServiceBookingStatus { get; set; }

    /// <summary>
    /// Service Requester
    /// </summary>
    [CanBeNull]
    [DataMember]
    public IParticipationPathologyServiceRequester ServiceRequester { get; set; }

    /// <summary>
    /// Request Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier RequestIdentifier { get; set; }

    /// <summary>
    /// Requested Service DateTime
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ISO8601DateTime RequestedServiceDateTime { get; set; }

    /// <summary>
    /// Requested Service Instance Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier RequestedServiceInstanceIdentifier { get; set; }

    /// <summary>
    /// Detailed Clinical Model Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier DetailedClinicalModelIdentifier { get; set; }

    #region Constructors
    internal RequestedService()
    {
            
    }
    #endregion

    /// <summary>
    /// Validate the Content for this Requested Service
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      if (vb.ArgumentRequiredCheck("RequestedServiceDescription", RequestedServiceDescription))
      {
        RequestedServiceDescription.Validate(vb.Path + "RequestedServiceDescription", vb.Messages);
      }

      vb.ArgumentRequiredCheck("ServiceBookingStatus", ServiceBookingStatus);

      if (vb.ArgumentRequiredCheck("ServiceRequester", ServiceRequester))
      {
        ServiceRequester.Validate(vb.Path + "ServiceRequester", vb.Messages);
      }

      vb.ArgumentRequiredCheck("RequestedServiceDateTime", RequestedServiceDateTime);

      if (vb.ArgumentRequiredCheck("RequestIdentifier", RequestIdentifier))
      {
         RequestIdentifier.Validate(vb.Path + "RequestIdentifier", vb.Messages);
      }

      if (vb.ArgumentRequiredCheck("RequestedServiceInstanceIdentifier", RequestedServiceInstanceIdentifier))
      {
          RequestedServiceInstanceIdentifier.Validate(vb.Path + "RequestedServiceInstanceIdentifier", vb.Messages);
      }

      if (vb.ArgumentRequiredCheck("DetailedClinicalModelIdentifier", DetailedClinicalModelIdentifier))
      {
          DetailedClinicalModelIdentifier.Validate(vb.Path + "DetailedClinicalModelIdentifier", vb.Messages);
      }
    }
  }
}
