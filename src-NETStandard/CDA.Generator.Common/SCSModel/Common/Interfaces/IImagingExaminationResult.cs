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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// the IImagingExaminationResultEReferral is designed to encapsulate the properties within a CDA document that
    /// make up an imaging examination result; as it pertains to an eReferral
    /// </summary>
    public interface IImagingExaminationResult
    {
        #region Properties
        /// <summary>
        /// Examination result name
        /// </summary>
        [CanBeNull]
        ICodableText ExaminationResultName { get; set; }

        /// <summary>
        /// Modality
        /// </summary>
        [CanBeNull]
        ICodableText Modality { get; set; }

        /// <summary>
        /// Anatomical site
        /// </summary>
        [CanBeNull]
        List<AnatomicalSite> AnatomicalSite { get; set; }

        /// <summary>
        /// Result status
        /// </summary>
        [CanBeNull]
        ICodableText ExaminationResultStatus { get; set; }

        /// <summary>
        /// Clinical information provided
        /// </summary>
        [CanBeNull]
        String ClinicalInformationProvided { get; set; }

        /// <summary>
        /// Findings
        /// </summary>
        [CanBeNull]
        String Findings { get; set; }

        /// <summary>
        /// A list of imaging results
        /// </summary>
        [CanBeNull]
        List<IImagingResultGroup> ResultGroup { get; set; }

        /// <summary>
        /// examination result representation
        /// </summary>
        [CanBeNull]
        string ExaminationResultRepresentation { get; set; }

        /// <summary>
        /// A list of examination requests
        /// </summary>
        [CanBeNull]
        List<IImagingExaminationRequest> ExaminationRequestDetails { get; set; }

        /// <summary>
        /// The date / time of the result
        /// </summary>
        [CanBeNull]
        ISO8601DateTime ResultDateTime { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeImagingExaminationResult { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this imaging examination result
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
        #endregion
    }
}
