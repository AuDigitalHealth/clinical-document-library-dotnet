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
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The Participation Legal Authenticator defines the properties associated with a participation
    /// when the participation / participant is a Participation Legal Authenticator
    /// </summary>
    public interface IParticipationLegalAuthenticator
    {
        /// <summary>
        /// The involvement or role of the participant in the related action from a healthcare 
        /// perspective rather than the specific participation perspective.
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText Role { get; set; }

        /// <summary>
        /// The IInformation Recipient
        /// </summary>
        [CanBeNull]
        ILegalAuthenticator Participant { get; set; }

        /// <summary>
        /// Validates this Legal Authenticator
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this Legal Authenticator for ATS
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateATS(string path, List<ValidationMessage> messages);

    }
}
