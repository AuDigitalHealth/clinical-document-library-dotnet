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
using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an Event
    /// 
    /// Please use the CreateEvent() method on the appropriate parent SCS object to instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(DiagnosticInvestigations))]
    public class Event  
    {
        #region Properties

        /// <summary>
        /// The Encounter
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Encounter Encounter  { get; set; }

        /// <summary>
        /// The Problem Diagnosis for this E-DischargeSummary
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ProblemDiagnosesThisVisit ProblemDiagnosesThisVisit { get; set; }

        /// <summary>
        /// The Clinical Interventions for this E-DischargeSummary
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ClinicalIntervention ClinicalIntervention { get; set; }

        /// <summary>
        /// The Clinical Synopsis for this E-DischargeSummary
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ClinicalSynopsis ClinicalSynopsis { get; set; }

        /// <summary>
        /// The Diagnostic Investigations for this E-DischargeSummary, not modeled in the Implementation guid
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IDiagnosticInvestigationsDischargeSummary DiagnosticInvestigations { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeEvent { get; set; }

        #endregion

        #region Constructors
        internal Event()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Event
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages) 
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Encounter", Encounter))
            {
                if (Encounter != null) Encounter.Validate(vb.Path + "Encounter", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ProblemDiagnosesThisVisit", ProblemDiagnosesThisVisit))
            {
                if (ProblemDiagnosesThisVisit != null)
                    ProblemDiagnosesThisVisit.Validate(vb.Path + "ProblemDiagnosesThisVisit", vb.Messages);
            }

            if (ClinicalIntervention != null)
            {
                ClinicalIntervention.Validate(vb.Path + "ClinicalIntervention", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ClinicalSynopsis", ClinicalSynopsis))
            {
                if (ClinicalSynopsis != null) ClinicalSynopsis.Validate(vb.Path + "ClinicalSynopsis", vb.Messages);
            }

            if (((Event)this).DiagnosticInvestigations != null)
            {
                ((Event)this).DiagnosticInvestigations.Validate(vb.Path + "DiagnosticInvestigations", vb.Messages);
            }
        }
        #endregion
    }
}