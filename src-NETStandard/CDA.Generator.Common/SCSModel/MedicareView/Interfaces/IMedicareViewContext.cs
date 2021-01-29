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

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the CDA specific content for an E-Referral
    /// </summary>
    public interface IMedicareOverviewContext
    {
        /// <summary>
        /// The author
        /// </summary>
        [CanBeNull]
        AuthorAuthoringDevice Author { get; set; }

        /// <summary>
        /// The subject of care
        /// </summary>
        [CanBeNull]
        IParticipationSubjectOfCare SubjectOfCare { get; set; }

        /// <summary>
        /// Earliest Date for Filtering
        /// </summary>
        [CanBeNull]
        ISO8601DateTime EarliestDateForFiltering { get; set; }

        /// <summary>
        /// Latest Date for Filtering
        /// </summary>
        [CanBeNull]
        ISO8601DateTime LatestDateForFiltering { get; set; }

        /// <summary>
        /// Validate the CDA Content for this Medicare Overview Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
