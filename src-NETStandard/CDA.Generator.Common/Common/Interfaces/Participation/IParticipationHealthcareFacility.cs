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
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.Interfaces
{
  /// <summary>
  /// The IParticipationHealthcareFacility interface contains all the properties that CDA has identified for a IParticipation Healthcare Facility
  /// </summary>
  public interface IParticipationHealthcareFacility : IParticipationDispenserOrganisation 
  {
    /// <summary>
    ///    The date or date and time that the Healthcare Organisation/Facility is involved in or associated with the
    ///    delivery of the healthcare services to the subject of care, or caring for his/her wellbeing.
    /// </summary>
    [CanBeNull]
    [DataMember]
    CdaInterval ParticipationPeriod { get; set; }

    /// <summary>
    /// The involvement or role of the participant in the related action from a healthcare 
    /// perspective rather than the specific participation perspective.
    /// </summary>
    [CanBeNull]
    ICodableText Role { get; set; }

    /// <summary>
    /// The participant organisation
    /// </summary>
    [CanBeNull]
    new IHealthcareFacility Participant { get; set; }

    /// <summary>
    /// Validates this IParticipationHealthcareFacility
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="mandatoryRole">True if the Role is Mandatory </param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    void Validate(string path, bool mandatoryRole,  List<ValidationMessage> messages);
  }
}
