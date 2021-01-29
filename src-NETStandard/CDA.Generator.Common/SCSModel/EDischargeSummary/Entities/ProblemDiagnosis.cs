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

using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;
using System;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an problem diagnosis
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Common.ProblemDiagnosis))]
    [KnownType(typeof(CodableText))]
    public class ProblemDiagnosis : Common.ProblemDiagnosis, IDischargeSummaryProblemDiagnosis
    {
        #region Properties

        #endregion

        #region Constructors
        internal ProblemDiagnosis()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Discharge Summary problem / diagnosis
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IDischargeSummaryProblemDiagnosis.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ProblemDiagnosisType", ProblemDiagnosisType))
            {
                if (ProblemDiagnosisType != null)
                    ProblemDiagnosisType.ValidateMandatory(vb.Path + "ProblemDiagnosisType", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ProblemDiagnosisDescription", ProblemDiagnosisDescription))
            {
                if (ProblemDiagnosisDescription != null)
                    ProblemDiagnosisDescription.ValidateMandatory(vb.Path + "ProblemDiagnosisDescription", vb.Messages);
            }
        }

        #endregion
    }
}