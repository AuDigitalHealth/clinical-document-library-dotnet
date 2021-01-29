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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IParticipationPrescriberOrganisation interface defines the properties associated with a participation
    /// when the participation / participant is a prescriber organisation
    /// </summary>
    public interface IParticipationPrescriberOrganisation
    {
        /// <summary>
        /// The participation role
        /// </summary>
        [CanBeNull]
        ICodableText Role { get; set; }

        /// <summary>
        /// The prescriber organisation
        /// </summary>
        [CanBeNull]
        IPrescriberOrganisation Participant { get; set; }

        /// <summary>
        /// Validates this CDA Context for this Participation Prescriber Organisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this CDA Context for this Participation Prescriber Organisation for ATS
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateATS(string path, List<ValidationMessage> messages);
    }
}
