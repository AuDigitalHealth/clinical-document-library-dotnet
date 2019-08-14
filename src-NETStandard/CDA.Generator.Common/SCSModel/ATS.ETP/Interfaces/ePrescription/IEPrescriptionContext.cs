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
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces
{
    /// <summary>
    /// This interface encapsulates all the SCS specific context for an EPrescription record
    /// </summary>
    public interface IEPrescriptionContext
    {
        /// <summary>
        /// A participation - prescriber
        /// </summary>
        [CanBeNull]
        IParticipationPrescriber Prescriber { get; set; }

        /// <summary>
        /// A participation - Prescriber organisation
        /// </summary>
        [CanBeNull]
        IParticipationPrescriberOrganisation PrescriberOrganisation { get; set; }

        /// <summary>
        /// A participation - Subject of care
        /// </summary>
        [CanBeNull]
        IParticipationSubjectOfCare SubjectOfCare { get; set; }

        /// <summary>
        /// Validates this CDA Context for this Prescription Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
