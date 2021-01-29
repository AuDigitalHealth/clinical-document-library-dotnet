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

using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IParticipationDispenserOrganisation interface defines the properties associated with a participation
    /// when the participation / participant is a dispenser organisation
    /// </summary>
    public interface IParticipationDispenserOrganisation
    {
        /// <summary>
        /// The participant organisation
        /// </summary>
        [CanBeNull]
        IDispenserOrganisation Participant { get; set; }

        /// <summary>
        /// Validates this Participation Dispenser Organisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this Participation Dispenser Organisation for an ATS
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateATS(string path, List<ValidationMessage> messages);
    }
}
