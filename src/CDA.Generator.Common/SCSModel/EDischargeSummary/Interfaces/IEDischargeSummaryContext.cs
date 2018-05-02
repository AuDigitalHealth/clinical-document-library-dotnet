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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the CDA specific content for an E-DischargeSummary
    /// </summary>
    public interface IEDischargeSummaryContext
    {
        /// <summary>
        /// Date Time Attested
        /// </summary>
        [CanBeNull]
        ISO8601DateTime Attested { get; set; }

        /// <summary>
        /// The author
        /// </summary>
        [CanBeNull]
        IParticipationDocumentAuthor Author { get; set; }

        /// <summary>
        /// The subject of care
        /// </summary>
        [CanBeNull]
        IParticipationSubjectOfCare SubjectOfCare { get; set; }

        /// <summary>
        /// The Facility
        /// </summary>
        [CanBeNull]
        IParticipationFacility Facility { get; set; }

        /// <summary>
        /// HealthEventIdentification
        /// </summary>
        [CanBeNull]
        HealthEventIdentification HealthEventIdentification { get; set; }

        /// <summary>
        /// Validate the CDA Content for this E-DischargeSummary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
