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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IParticipation interface encapsulates all the properties that CDA requires for a participation
    /// </summary>
    public interface IParticipation
    {
        /// <summary>
        /// The involvement or role of the participant in the related action from a healthcare 
        /// perspective rather than the specific participation perspective.
        /// </summary>
        [CanBeNull]
        ICodableText Role { get; set; }

        /// <summary>
        /// The participant
        /// </summary>
        [CanBeNull]
        IParticipant Participant { get; set; }

        /// <summary>
        /// The Participation EndTime
        /// </summary>
        [CanBeNull]
        ISO8601DateTime ParticipationEndTime { get; set; }

        /// <summary>
        /// The ParticipationPeriod
        /// </summary>
        [CanBeNull]
        CdaInterval ParticipationPeriod { get; set; }

        /// <summary>
        /// The AtuhorParticipationPeriod
        /// </summary>
        [CanBeNull]
        ISO8601DateTime AuthorParticipationPeriodOrDateTimeAuthored { get; set; }

        /// <summary>
        /// The Role Type
        /// </summary>
        [CanBeNull]
        RoleType RoleType { get; set; }
    }
}
