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

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces
{
    /// <summary>
    /// The Participation Health Professional who has the overall responsibility for the care given to the patient 
    /// </summary>
    public interface IParticipationHealthProfessional
    {
        /// <summary>
        /// The role of the Participant
        /// </summary>
        [CanBeNull]
        ICodableText Role { get; set; }

        /// <summary>
        /// The Participant
        /// </summary>
        [CanBeNull]
        IHealthPersonOrOrganisation Participant { get; set; }

        /// <summary>
        /// Validates thisv Participation Health Professional
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
