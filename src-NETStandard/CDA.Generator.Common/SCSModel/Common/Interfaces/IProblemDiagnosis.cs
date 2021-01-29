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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
  /// The IProblemDiagnosis interface contains all the properties that CDA has identified for a Problem Diagnosis 
    /// </summary>
    public interface IProblemDiagnosis
    {
        /// <summary>
        /// Problem or diagnosis ID
        /// </summary>
        [CanBeNull]
        ICodableText ProblemDiagnosisIdentification { get; set; }

        /// <summary>
        /// Date Of Resolution Remission
        /// </summary>
        [CanBeNull]
        ISO8601DateTime DateOfResolutionRemission { get; set; }

        /// <summary>
        /// Date of onset
        /// </summary>
        [CanBeNull]
        ISO8601DateTime DateOfOnset { get; set; }

        /// <summary>
        /// Show Ongoing In Narrative
        /// NOTE: This field will always show (Ongoing) in the narrative.
        ///       It will show '(ongoing)' also if DateOfOnset is set eg.. 14 Mar 2012 08:27+1000 -> (ongoing)
        ///       Otherwise it will only show '(ongoing)'
        /// 
        /// </summary>
        [CanBeNull]
        [DataMember]
        bool? ShowOngoingDateInNarrative { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [CanBeNull]
        String Comment { get; set; }

        /// <summary>
        /// Validates this Problem Diagnosis 
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
