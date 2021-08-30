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
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.PCML.Entities;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the CDA specific content for an PSML
    /// </summary>
    public interface IPCMLContext
    {
        /// <summary>
        /// The subject of care
        /// </summary>
        IParticipationSubjectOfCare SubjectOfCare { get; set; }

        /// <summary>
        /// The author
        /// </summary>
        IAuthorCollection Author { get; set; }

        /// <summary>
        /// Document Title
        /// </summary>
        [CanBeNull]
        string DocumentTitle { get; set; }


        /// <summary>
        /// Participation Healthcare Facility
        /// </summary>
        [CanBeNull]
        Encounter Encounter { get; set; }

        /// <summary>
        /// Participant
        /// </summary>
        [CanBeNull]
        IList<IParticipationPersonOrOrganisation> Participant { get; set; }

        /// <summary>
        /// Validate the CDA Content for this PSML
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }


}
