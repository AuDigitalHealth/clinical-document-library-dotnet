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
    /// The IParticipationSubjectOfCare interface defines the properties associated with a participation
    /// when the participation / participant is a Participation Subject Of Care
    /// </summary>
    public interface IParticipationSubjectOfCare
    {
        /// <summary>
        /// The subject of care
        /// </summary>
        [CanBeNull]
        ISubjectOfCare Participant { get; set; }

        /// <summary>
        /// Validates this subject of care
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this subject of care
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateV2(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this subject of care where address is optional
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        /// <param name="optionalDateOfBirth">'True' if optional, 'False' if mandatory</param>
        /// <param name="optionalAddress">'True' if optional, 'False' if mandatory</param>
        /// <param name="optionalGender">'True' if optional, 'False' if mandatory</param>
        void ValidateOptional(string path, bool optionalAddress, bool optionalGender, bool optionalDateOfBirth, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this subject of care
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateATS(string path, List<ValidationMessage> messages);

    }
}
