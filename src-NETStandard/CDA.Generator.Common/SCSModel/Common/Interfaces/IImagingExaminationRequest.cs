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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
  /// The IImagingExaminationRequest interface contains all the properties that CDA has identified for a Imaging Examination Request
    /// </summary>
    public interface IImagingExaminationRequest
    {
        /// <summary>
        /// The name(s) of the requested examination(s)
        /// </summary>
        [CanBeNull]
        List<String> ExaminationRequestedName { get; set; }

        /// <summary>
        /// The DICOM study identifier
        /// </summary>
        [CanBeNull]
        InstanceIdentifier StudyIdentifier { get; set; }

        /// <summary>
        /// Report identifier
        /// </summary>
        [CanBeNull]
        InstanceIdentifier ReportIdentifier { get; set; }

        /// <summary>
        /// Image Details
        /// </summary>
        [CanBeNull]
        List<IImageDetails> ImageDetails { get; set; }

        /// <summary>
        /// Validates this Imaging Examination Request
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
