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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the SCS specific content for a shared health summary
    /// </summary>
    public interface IConsolidatedViewContent
    {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

        /// <summary>
        /// Shared Health Summary Document Provenance (DOCUMENT PROVENANCE)
        /// </summary>
        [CanBeNull]
        IDocument SharedHealthSummaryDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeSharedHealthSummaryDocumentProvenance { get; set; }

        /// <summary>
        /// Advance Care Directive Custodian Document (Documents Provenance)
        /// </summary>
        [CanBeNull]
        IDocument AdvanceCareDirectiveCustodianDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeAdvanceCareDirectiveCustodianDocumentProvenance { get; set; }

        /// <summary>
        /// Advance Care Directive Custodian Document (Documents Provenance)
        /// </summary>
        [CanBeNull]
        List<IDocumentWithHealthEventEnded> NewDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeNewDocumentProvenance { get; set; }

       /// <summary>
        /// The reviewed adverse substance reactions
        /// </summary>
        [CanBeNull]
        IAdverseReactions SharedHealthSummaryAdverseReactions { get; set; }

        /// <summary>
        /// The reviewed medications
        /// </summary>
        [CanBeNull]
        IMedications SharedHealthSummaryMedicationInstructions { get; set; }

        /// <summary>
        /// The reviewed medical history
        /// </summary>
        [CanBeNull]
        IMedicalHistory SharedHealthSummaryMedicalHistory { get; set; }

        /// <summary>
        /// The reviewed immunisations
        /// </summary>
        [CanBeNull]
        Immunisations SharedHealthSummaryImunisations { get; set; }

        /// <summary>
        /// Recent Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        List<IDocumentWithHealthEventEnded> RecentDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeRecentDocumentProvenance { get; set; }

        /// <summary>
        /// Recent Diagnostic Test Result Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        List<IDocumentWithHealthEventEnded> RecentDiagnosticTestResultDocumentProvenance  { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeRecentDiagnosticTestResultDocumentProvenance { get; set; }

        /// <summary>
        /// Medicare Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        List<IDocument> MedicareDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeMedicareDocumentProvenance { get; set; }

        /// <summary>
        /// Consumer Entered Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        List<IDocument> ConsumerEnteredDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeConsumerEnteredDocumentProvenance { get; set; }

      /// <summary>
      /// Validates this Shared Health Summary content
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      void Validate(string path, List<ValidationMessage> messages);
    }
}
