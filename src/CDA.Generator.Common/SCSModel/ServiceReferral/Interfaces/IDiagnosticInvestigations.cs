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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ServiceReferral.Interfaces
{
    /// <summary>
    /// The IDiagnosticInvestigations interface contains all the properties that CDA has identified for a diagnostic investigations section.
    /// </summary>
    public interface IDiagnosticInvestigationsV1
    {
        #region Properties

        /// <summary>
        /// Pathology test results
        /// </summary>
        [CanBeNull]
        List<PathologyTestResult> PathologyTestResult { get; set; }

        /// <summary>
        /// Imaging examination result
        /// </summary>
        [CanBeNull]
        List<IImagingExaminationResult> ImagingExaminationResult { get; set; }

        /// <summary>
        /// Requested Service
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IPendingDiagnosticInvestigation> RequestedService { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeDiagnosticInvestigations { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this diagnostic investigation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        #endregion
    }
}
