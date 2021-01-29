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
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// The IDiagnosticInvestigationsDischargeSummary interface contains all the properties that CDA has identified for a diagnostic investigations
    /// section; specific to an Discharge Summary.
    /// </summary>
    public interface IDiagnosticInvestigationsDischargeSummary
    {
        #region Properties
        /// <summary>
        /// A list of Pathology test results
        /// </summary>
        [CanBeNull]
        List<PathologyTestResult> PathologyTestResult { get; set; }

        /// <summary>
        /// A list of Imaging examination results
        /// </summary>
        [CanBeNull]
        List<IImagingExaminationResult> ImagingExaminationResult { get; set; }

        /// <summary>
        /// Other Test Result
        /// </summary>
        [CanBeNull]
        List<OtherTestResult> OtherTestResult { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeDiagnosticInvestigations { get; set; }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this IDiagnosticInvestigationsDischargeSummary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
        #endregion
    }
}
