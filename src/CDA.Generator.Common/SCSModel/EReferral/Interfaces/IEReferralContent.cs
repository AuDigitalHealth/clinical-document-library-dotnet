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
using CDA.Generator.Common.SCSModel.Common.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the SCS specific context for an E-Referral
    /// </summary>
    public interface IEReferralContent
    {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
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
        /// The date / time of the referral
        /// </summary>
        [CanBeNull]
        ISO8601DateTime ReferralDateTime { get; set; }

        /// <summary>
        /// The valid duration of the referral
        /// </summary>
        [CanBeNull]
        CdaInterval ValidityDuration { get; set; }

        /// <summary>
        /// The referral reason
        /// </summary>
        [CanBeNull]
        string ReferralReason { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeReferralDetail { get; set; }

        /// <summary>
        /// The usual GP
        /// </summary>
        [CanBeNull]
        IParticipationUsualGP UsualGP { get; set; }

        /// <summary>
        /// The referee
        /// </summary>
        [CanBeNull]
        IParticipationReferee Referee { get; set; }

        /// <summary>
        /// Medical history
        /// </summary>
        [CanBeNull]
        IMedicalHistory MedicalHistory { get; set; }

        /// <summary>
        /// Medications history
        /// </summary>
        [CanBeNull]
        IMedicationsEReferral Medications { get; set; }

        /// <summary>
        /// Adverse substance reactions
        /// </summary>
        [CanBeNull]
        IAdverseReactions AdverseReactions { get; set; }

        /// <summary>
        /// Diagnostic investigations
        /// </summary>
        [CanBeNull]
        IDiagnosticInvestigations DiagnosticInvestigations { get; set; }

        /// <summary>
        /// Validate the SCS Content for this E-Referral
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
