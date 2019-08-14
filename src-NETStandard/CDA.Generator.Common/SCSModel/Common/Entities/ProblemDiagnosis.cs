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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an problem diagnosis
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Statement))]
    [KnownType(typeof(DischargeSummary.ProblemDiagnosis))]
    public class ProblemDiagnosis : IProblemDiagnosis, IProblemDiagnosisEventSummary
    {
        #region Properties
        /// <summary>
        /// Problem or diagnosis ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ProblemDiagnosisIdentification { get; set; }

        /// <summary>
        /// Date of onset
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateOfOnset { get; set; }

        /// <summary>
        /// Date of remission
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateOfResolutionRemission { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Comment { get; set; }

        /// <summary>
        /// Show Ongoing In Narrative
        /// NOTE: This field will always show (Ongoing) in the narrative.
        ///       It will show '(ongoing)' also if DateOfOnset is set eg.. 14 Mar 2012 08:27+1000 -> (ongoing)
        ///       Otherwise it will only show '(ongoing)'
        /// 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? ShowOngoingDateInNarrative { get; set; }
        
        /// <summary>
        /// Problem Diagnosis Type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ProblemDiagnosisType { get; set; }

        /// <summary>
        /// Problem Diagnosis Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ProblemDiagnosisDescription { get; set; }

        #endregion

        #region Constructors
        internal ProblemDiagnosis()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this problem / diagnosis
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ProblemDiagnosisIdentification", ProblemDiagnosisIdentification))
            {
                if (ProblemDiagnosisIdentification != null)
                    ProblemDiagnosisIdentification.ValidateMandatory(vb.Path + "ProblemDiagnosisIdentification", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this problem / diagnosis
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IProblemDiagnosis.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ProblemDiagnosisIdentification", ProblemDiagnosisIdentification))
                if (ProblemDiagnosisIdentification != null) ProblemDiagnosisIdentification.Validate(vb.Path + "ProblemDiagnosisIdentification", vb.Messages);
        }

        /// <summary>
        /// Validates this problem / diagnosis
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IProblemDiagnosisEventSummary.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("ProblemDiagnosisIdentification", ProblemDiagnosisIdentification))
          {
            if (ProblemDiagnosisIdentification != null)  ProblemDiagnosisIdentification.Validate(vb.Path + "ProblemDiagnosisIdentification", vb.Messages);
          }

          if (DateOfOnset != null)
          {
              if (DateOfOnset.PrecisionIndicator.HasValue && (DateOfOnset.PrecisionIndicator.Value == ISO8601DateTime.Precision.Millisecond ||
                                                              DateOfOnset.PrecisionIndicator.Value == ISO8601DateTime.Precision.Minute || 
                                                              DateOfOnset.PrecisionIndicator.Value == ISO8601DateTime.Precision.Second))
              {
                  vb.AddValidationMessage(path + ".DateOfOnset", null, "The value SHALL NOT include a time");
              }
          }

        }
        
        #endregion
    }
}