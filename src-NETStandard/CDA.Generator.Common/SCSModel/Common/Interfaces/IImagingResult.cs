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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IImagingResult interface encapsulates all the properties that CDA has identified for an imaging result
    /// </summary>
    public interface IImagingResult
    {
        /// <summary>
        /// Result name
        /// </summary>
        [CanBeNull]
        ICodableText ResultName { get; set; }

        /// <summary>
        /// Result value
        /// </summary>
        [CanBeNull]
        ResultValue ResultValue { get; set; }

        /// <summary>
        /// The status associated with the Result value
        /// </summary>
        [CanBeNull]
        HL7ObservationInterpretationNormality? NormalStatus { get; set; }

        /// <summary>
        /// A list of result value reference ranges
        /// </summary>
        [CanBeNull]
        List<ResultValueReferenceRangeDetail> ResultValueReferenceRangeDetails { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [CanBeNull]
        List<String> Comments { get; set; }

        /// <summary>
        /// Validates this Imaging Result
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
