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

using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;


namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IParticipationPrescriberInstructionRecipient interface defines the properties associated with a participation
    /// when the participation / participant is a Prescriber Instruction Recipient
    /// </summary>
    public interface IParticipationPrescriberInstructionRecipient
    {
        /// <summary>
        /// The  Prescriber Instruction Recipient
        /// </summary>
        [CanBeNull]
        IPrescriberInstructionRecipient Participant { get; set; }

        /// <summary>
        /// Validates this Participation Prescriber Instruction Recipient
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
