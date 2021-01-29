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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// Defines a CDA test result
    /// </summary>
    public interface ITestResult
    {
        /// <summary>
        /// Test name
        /// </summary>
        [CanBeNull]
        ICodableText ResultName { get; set; }

        /// <summary>
        /// Test result
        /// </summary>
        [CanBeNull]
        ResultValue ResultValue { get; set; }

        /// <summary>
        /// Result ResultValue Range
        /// </summary>
        [CanBeNull]
        List<ResultValueReferenceRangeDetail> ResultValueReferenceRangeDetails { get; set; }
        
        /// <summary>
        /// Test result comments
        /// </summary>
        [CanBeNull]
        List<String> Comments { get; set; }

        /// <summary>
        /// Reference Range Guidance
        /// </summary>
        [CanBeNull]
        String ReferenceRangeGuidance { get; set; }

        /// <summary>
        /// Result value status
        /// </summary>
        [CanBeNull]
        HL7ObservationInterpretationNormality? NormalStatus { get; set; }
        
        /// <summary>
        /// Result status
        /// </summary>
        [CanBeNull]
        ICodableText ResultStatus { get; set; }

        /// <summary>
        /// Validates this test result
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
