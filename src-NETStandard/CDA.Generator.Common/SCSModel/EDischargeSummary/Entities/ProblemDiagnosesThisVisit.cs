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

using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a ProblemDiagnoses This Visit
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Statement))]
    [KnownType(typeof(ProblemDiagnosis))]

    public class ProblemDiagnosesThisVisit
    {
        #region Properties

        /// <summary>
        /// A list of Discharge Summary Problem Diagnosis
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IDischargeSummaryProblemDiagnosis> ProblemDiagnosis { get; set; }

        /// <summary>
        /// A list of Exclusion Statements
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Statement ExclusionStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeProblemDiagnosesThisVisit { get; set; }

        #endregion

        #region Constructors
        internal ProblemDiagnosesThisVisit()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Discharge Summary problem/diagnosis This Visit
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ProblemDiagnosis != null)
            {
                ProblemDiagnosis.ForEach(problemDiagnosis => problemDiagnosis.Validate(vb.Path + "ProblemDiagnosis", vb.Messages));
            }

            // Problem diagnosis exclusion statement choice
            var problemDiagnosisChoice = new Dictionary<string, object>()
            {
                { "ProblemDiagnosis", ProblemDiagnosis },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(problemDiagnosisChoice);

            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }
        }

        #endregion
    }
}