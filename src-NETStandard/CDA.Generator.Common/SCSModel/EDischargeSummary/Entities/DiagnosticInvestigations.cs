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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a Diagnostic Investigations class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(ImagingExaminationResult))]
    public class DiagnosticInvestigations : SCSModel.DiagnosticInvestigations, IDiagnosticInvestigationsDischargeSummary
    {
        /// <summary>
        /// A list of Imaging examination results
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IImagingExaminationResult> IDiagnosticInvestigationsDischargeSummary.ImagingExaminationResult { get; set; }

        /// <summary>
        /// A list of Clinical Synopsis
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ClinicalSynopsis> ClinicalSynopsis { get; set; }

        /// <summary>
        /// Validates this diagnostic investigation for a Discharge Summary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IDiagnosticInvestigationsDischargeSummary.Validate(string path, List<ValidationMessage> messages)
        {
            if (PathologyTestResult != null && PathologyTestResult.Any())
            {
                PathologyTestResult.ForEach(pathologyTestResult => pathologyTestResult.Validate(path + ".PathologyTestResult", messages));
            }

            if (((IDiagnosticInvestigationsDischargeSummary)this).ImagingExaminationResult != null && ((IDiagnosticInvestigationsDischargeSummary)this).ImagingExaminationResult.Any())
            {
                ((IDiagnosticInvestigationsDischargeSummary)this).ImagingExaminationResult.ForEach(imagingExaminationResult => imagingExaminationResult.Validate(path + ".ImagingExaminationResult", messages));
            }

            if (ClinicalSynopsis != null && ClinicalSynopsis.Any())
            {
                ClinicalSynopsis.ForEach(clinicalSynopsis => clinicalSynopsis.Validate(path + ".ClinicalSynopsis", messages));
            }

            if (OtherTestResult != null && OtherTestResult.Any())
            {
              OtherTestResult.ForEach(otherTestResult  => otherTestResult.Validate(path + ".OtherTestResult", messages));
            }
        }
    }
}
