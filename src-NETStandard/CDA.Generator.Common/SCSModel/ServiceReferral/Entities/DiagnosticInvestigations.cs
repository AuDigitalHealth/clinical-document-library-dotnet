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
using CDA.Generator.Common.SCSModel.ServiceReferral.Interfaces;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral
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
    public class DiagnosticInvestigationsV1 : IDiagnosticInvestigationsV1
    {
        #region Properties

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrativeDiagnosticInvestigations { get; set; }

        /// <summary>
        /// Pathology test results
        /// </summary>
        [DataMember]
        public List<PathologyTestResult> PathologyTestResult { get; set; }

        /// <summary>
        /// Imaging examination result
        /// </summary>
        [DataMember]
        public List<IImagingExaminationResult> ImagingExaminationResult { get; set; }

        /// <summary>
        /// Requested Service
        /// </summary>
        [DataMember]
        public List<IPendingDiagnosticInvestigation> RequestedService { get; set; }

        #endregion

        #region Constructors

        internal DiagnosticInvestigationsV1()
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
                if (vb.ArgumentRequiredCheck("RequestedService", RequestedService))
                {
                    for (var x = 0; x < RequestedService.Count; x++)
                    {
                        RequestedService[x].Validate(vb.Path + string.Format(".RequestedService[{0}]", x), vb.Messages);
                    }
                }
            }

            if (RequestedService == null && ImagingExaminationResult == null && PathologyTestResult == null)
            {
                vb.AddValidationMessage(vb.PathName, null, "Each instance of this Diagnostic Investigations section SHALL have at least one instance of 'PATHOLOGY TEST RESULT' OR 'IMAGING EXAMINATION RESULT' OR 'REQUESTED SERVICE'");
            }
        }

        #endregion
    }
}