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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a medical history,
    /// 
    /// This class implements several interfaces and can be cast into these interfaces to constrain
    /// the medical history to a particular type of CDA medical history
    /// 
    /// Please use the CreateMedicalHistory() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class MedicalHistory : IMedicalHistory
    {
        #region Properties

        /// <summary>
        /// An exclusion statement to associate with the problem diagnosis
        /// </summary>
        [DataMember]
        public Statement ProblemDiagnosisExclusionStatement { get; set; }

        /// <summary>
        /// An exclusion statement to associate with the procedures
        /// </summary>
        [DataMember]
        public Statement ProceduresExclusionStatement { get; set; }

        /// <summary>
        /// A list of problem diagnosis
        /// </summary>
        [DataMember]
        public List<IProblemDiagnosis> ProblemDiagnosis { get; set; }

        /// <summary>
        /// A list of procedures
        /// </summary>
        [DataMember]
        public List<Procedure> Procedures { get; set; }

        /// <summary>
        /// A list of medical history items that contain any - Uncategorised MedicalHistory Items
        /// </summary>
        [DataMember]
        public List<IMedicalHistoryItem> MedicalHistoryItems { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrativeMedicalHistory { get; set; }

        #endregion

        #region Constructors
        internal MedicalHistory()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this medical history for the SHS 1.4
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        /// <param name="showExclusionStatements">Validate showExclusionStatements</param>
        public void ValidateShs(string path, List<ValidationMessage> messages, bool showExclusionStatements)
        {
            var vb = new ValidationBuilder(path, messages);
            Validate(path, messages, showExclusionStatements);

           var medicalHistory = (IMedicalHistory) this;

           if (medicalHistory.ProblemDiagnosis != null)
            {
                for (var x = 0; x < medicalHistory.ProblemDiagnosis.Count; x++)
                {
                    if (medicalHistory.ProblemDiagnosis[x].DateOfOnset != null &&
                        medicalHistory.ProblemDiagnosis[x].DateOfOnset.PrecisionIndicator != ISO8601DateTime.Precision.Day &&
                        medicalHistory.ProblemDiagnosis[x].DateOfOnset.PrecisionIndicator != ISO8601DateTime.Precision.Month &&
                        medicalHistory.ProblemDiagnosis[x].DateOfOnset.PrecisionIndicator != ISO8601DateTime.Precision.Year)
                    {
                        vb.AddValidationMessage(vb.Path + string.Format("ProblemDiagnosis[{0}].DateOfOnset", x), null, "The effectiveTime/@value SHALL NOT include a time.");
                    }

                    if (medicalHistory.ProblemDiagnosis[x].DateOfResolutionRemission != null &&
                        medicalHistory.ProblemDiagnosis[x].DateOfResolutionRemission.PrecisionIndicator != ISO8601DateTime.Precision.Day &&
                        medicalHistory.ProblemDiagnosis[x].DateOfResolutionRemission.PrecisionIndicator != ISO8601DateTime.Precision.Month &&
                        medicalHistory.ProblemDiagnosis[x].DateOfResolutionRemission.PrecisionIndicator != ISO8601DateTime.Precision.Year)
                    {
                        vb.AddValidationMessage(vb.Path + string.Format("ProblemDiagnosis[{0}].DateOfResolutionRemission", x), null, "The value SHALL NOT include a time.");
                    }
                }
            }

           if (medicalHistory.ProblemDiagnosisExclusionStatement != null && medicalHistory.ProblemDiagnosisExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
           {
               vb.AddValidationMessage(vb.Path + "Problem Diagnosis Exclusion Statement", null, "The value/@code SHALL NOT be 02.");
           }

           if (medicalHistory.ProceduresExclusionStatement != null && medicalHistory.ProceduresExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
           {
               vb.AddValidationMessage(vb.Path + "Procedures Exclusion Statement", null, "The value/@code SHALL NOT be 02.");
           }

           if (medicalHistory.Procedures != null)
            {
                for (var x = 0; x < medicalHistory.Procedures.Count; x++)
                {
                    vb.ArgumentRequiredCheck(string.Format("Procedures[{0}].ProcedureDateTime", x), medicalHistory.Procedures[x].ProcedureDateTime);
                }
            }

       
        }

        /// <summary>
        /// Validates this medical history
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        /// <param name="showExclusionStatements">Validate showExclusionStatements</param>
        public void Validate(string path, List<ValidationMessage> messages, bool showExclusionStatements)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ProblemDiagnosis != null)
            {
                for (var x = 0; x < ProblemDiagnosis.Count; x++)
                {
                    ProblemDiagnosis[x].Validate(vb.Path + string.Format("ProblemDiagnosis[{0}]", x), vb.Messages);
                }
            }

            // Validate problem diagnosis exclusion statement
            if (ProblemDiagnosisExclusionStatement != null)
            {
                ProblemDiagnosisExclusionStatement.Validate(vb.Path + "ProblemDiagnosisExclusionStatement", vb.Messages);
            }

            if (Procedures != null)
            {
                for (var x = 0; x < Procedures.Count; x++)
                {
                    Procedures[x].Validate(vb.Path + string.Format("Procedures[{0}]", x), vb.Messages);
                }
            }

            // Validate procedures exclusion statement
            if (ProceduresExclusionStatement != null)
            {
                ProceduresExclusionStatement.Validate(vb.Path + "ProceduresExclusionStatement", vb.Messages);
            }

            if (MedicalHistoryItems != null)
            {
                for (var x = 0; x < MedicalHistoryItems.Count; x++)
                {
                    MedicalHistoryItems[x].Validate(vb.Path + string.Format("OtherMedicalHistory[{0}]", x), vb.Messages);
                }
            }

            #region Guidelines for appropriate use of exclusion statements

            if (showExclusionStatements)
            {
                if (ProceduresExclusionStatement == null && ProblemDiagnosisExclusionStatement == null)
                {
                    if (ProblemDiagnosis == null && Procedures == null && MedicalHistoryItems == null)
                    {
                        vb.AddValidationMessage(vb.PathName, null, "The software SHALL create both an 'Exclusion Statement – Procedures' and 'Exclusion Statement – Problems and Diagnoses' when there are no entries for all of 'Procedure', 'Problem/Diagnosis' and 'Other Medical History Item'.");
                    }
                }

                if (ProblemDiagnosis != null && ProblemDiagnosisExclusionStatement != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "Only one ProblemDiagnosis or a ProblemDiagnosisExclusionStatement may be included");
                }

                if (Procedures != null && ProceduresExclusionStatement != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "Only one Procedures or a ProceduresExclusionStatement may be included");
                }

                // PROBLEM DIAGNOSIS EXCLUSIONS
                if (ProblemDiagnosis == null && MedicalHistoryItems == null && ProblemDiagnosisExclusionStatement == null)
                    vb.AddValidationMessage(vb.PathName, null, "The software SHALL create an 'Exclusion Statement – Problems and Diagnoses' when there are no entries for both 'Problem/Diagnosis' and 'Other Medical History Item'.");

                // PROCEDURE EXCLUSIONS
                if (Procedures == null && MedicalHistoryItems == null && ProceduresExclusionStatement == null)
                    vb.AddValidationMessage(vb.PathName, null, "The software SHALL create an 'Exclusion Statement – Procedures' when there are no entries for both 'Procedure' and 'Other Medical History Item'.");

                // OtherMedicalHistoryItems
                if (MedicalHistoryItems != null && (ProceduresExclusionStatement != null || ProblemDiagnosisExclusionStatement != null))
                    vb.AddValidationMessage(vb.PathName, null, "The software SHALL NOT create an exclusion statement when there are any entries for 'Other Medical History Item'.");

                #region Check for empty entry

                if (MedicalHistoryItems != null && !MedicalHistoryItems.Any())
                {
                    vb.AddValidationMessage(vb.PathName, null, "MedicalHistoryItems is currently empty, this list needs at least one item");
                }

                if (ProblemDiagnosis != null && !ProblemDiagnosis.Any())
                {
                    vb.AddValidationMessage(vb.PathName, null, "ProblemDiagnosis is currently empty, this list needs at least one item");
                }

                if (Procedures != null && !Procedures.Any())
                {
                    vb.AddValidationMessage(vb.PathName, null, "Procedures is currently empty, this list needs at least one item");
                }

                #endregion


            }
            else
            {
                if (ProceduresExclusionStatement != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "Medical History - ProceduresExclusionStatement is not applicable to this CDA Document Type");
                }

                if (ProblemDiagnosisExclusionStatement != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "Medical History - ProblemDiagnosisExclusionStatement is not applicable to this CDA Document Type");
                }
            }

            #endregion
        }

        #endregion
    }

}