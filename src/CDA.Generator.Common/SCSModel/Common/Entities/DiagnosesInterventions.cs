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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using System.Runtime.Serialization;
using System;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// The DiagnosesInterventions interface encapsulate the properties within a CDA document that make up 
    /// a medical history,
    /// </summary>
    [Serializable]
    [DataContract]
    public class DiagnosesIntervention
    {
        #region Properties

        /// <summary>
        /// Problem diagnosis
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IProblemDiagnosisEventSummary> ProblemDiagnosis { get; set; }

        /// <summary>
        /// A list of procedures
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Procedure> Procedures { get; set; }

        /// <summary>
        /// a list of medical history items that contain any relevant medical history
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IMedicalHistoryItem> UncategorisedMedicalHistoryItem { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeDiagnosesIntervention { get; set; }

        #endregion

        #region Constructors
        internal DiagnosesIntervention()
        {

        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Diagnoses Interventions
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ProblemDiagnosis != null)
            {
                for (var x = 0; x < ProblemDiagnosis.Count; x++)
                {
                    ProblemDiagnosis[x].Validate(
                        vb.Path + string.Format("ProblemDiagnosis[{0}]", x), vb.Messages);
                }
            }

            if (Procedures != null)
            {
                for (var x = 0; x < Procedures.Count; x++)
                {
                    Procedures[x].Validate(
                        vb.Path + string.Format("Procedures[{0}]", x), vb.Messages);
                }
            }

            if (UncategorisedMedicalHistoryItem != null)
            {
                for (var x = 0; x < UncategorisedMedicalHistoryItem.Count; x++)
                {
                    UncategorisedMedicalHistoryItem[x].Validate(
                        vb.Path + string.Format("MedicalHistoryItem[{0}]", x), vb.Messages);
                }
            }
        }

        #endregion
    }
}
