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
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This class is specific to a E-Referral and contains the diagnostic investigations
    /// 
    /// Please use the CreateDiagnosticInvestigations() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(ImagingExaminationResult))]
    [KnownType(typeof(DischargeSummary.DiagnosticInvestigations))]
    public class DiagnosticInvestigations : IDiagnosticInvestigations
    {
        #region Properties
        /// <summary>
        /// Pathology test results
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<PathologyTestResult> PathologyTestResult { get; set; }

        /// <summary>
        /// Imaging examination result
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IImagingExaminationResult> ImagingExaminationResult { get; set; }

        /// <summary>
        /// Requested Service
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<RequestedService> RequestedService { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeDiagnosticInvestigations { get; set; }

        /// <summary>
        /// Other Test Result
        /// </summary>
        [CanBeNull]
        public List<OtherTestResult> OtherTestResult { get; set; }

        #endregion

        #region Constructors
        internal DiagnosticInvestigations()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this diagnostic investigation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (PathologyTestResult != null && PathologyTestResult.Any())
            {
                PathologyTestResult.ForEach(pathologyTestResult => pathologyTestResult.Validate(path + ".PathologyTestResult", messages));
            }

            if (ImagingExaminationResult != null && ImagingExaminationResult.Any())
            {
                ImagingExaminationResult.ForEach(imagingExaminationResult => imagingExaminationResult.Validate(path + ".ImagingExaminationResult", messages));
            }

            if (RequestedService != null && RequestedService.Any())
            {
                RequestedService.ForEach(requestedService => requestedService.Validate(path + ".RequestedService", messages));
            }

            if (RequestedService == null && ImagingExaminationResult == null && PathologyTestResult == null && OtherTestResult == null)
            {
                vb.AddValidationMessage(vb.PathName, null, "Each instance of this Diagnostic Investigations section SHALL have at least one instance of 'PATHOLOGY TEST RESULT' OR 'IMAGING EXAMINATION RESULT' OR 'REQUESTED SERVICE OR 'OTHER TEST RESULT'");
            }

            if (OtherTestResult != null && OtherTestResult.Any())
            {
              OtherTestResult.ForEach(otherTestResult => otherTestResult.Validate(path + ".OtherTestResult", messages));
            }
        }

        #endregion
    }
}