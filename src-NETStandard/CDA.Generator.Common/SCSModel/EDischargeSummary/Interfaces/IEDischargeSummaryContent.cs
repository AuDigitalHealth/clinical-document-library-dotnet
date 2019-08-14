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
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.Common.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the SCS specific context for an IEDischargeSummaryContent
    /// </summary>
    public interface IEDischargeSummaryContent
    {

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

        /// <summary>
        /// A list of referenced documents that is the payload for this CDA document
        /// </summary>
        [CanBeNull]
        List<ExternalData> StructuredBodyFiles { get; set; }

        /// <summary>
        /// A Narrative Only 1B Document
        /// </summary>
        [CanBeNull]
        List<NarrativeOnlyDocument> NarrativeOnlyDocument { get; set; }

        /// <summary>
        /// The Event
        /// </summary>
        [CanBeNull]
        Event Event { get; set; }

        /// <summary>
        /// The Medications for this E-DischargeSummary
        /// </summary>
        [CanBeNull]
        IMedicationsDischargeSummary Medications { get; set; }

        /// <summary>
        /// The HealthProfile for this E-DischargeSummary
        /// </summary>
        [CanBeNull]
        HealthProfile HealthProfile { get; set; }

        /// <summary>
        /// The Plan for this E-DischargeSummary
        /// </summary>
        [CanBeNull]
        Plan Plan { get; set; }

        /// <summary>
        /// Validate this SCS Content for this E-DischargeSummary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
