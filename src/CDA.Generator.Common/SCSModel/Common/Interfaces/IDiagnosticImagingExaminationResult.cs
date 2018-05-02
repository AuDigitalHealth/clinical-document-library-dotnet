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
using CDA.Generator.Common.SCSModel.DiagnosticImagingReport.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// the IDiagnosticImagingExaminationResult is designed to encapsulate the properties within a CDA document that
    /// make up an imaging examination result; as it pertains to an eReferral
    /// </summary>
    public interface IDiagnosticImagingExaminationResult
    {
        #region Properties

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeImagingExaminationResult { get; set; }

        /// <summary>
        /// Reporting Radiologist For Imaging Examination Result Name - Narrative Only (IPCDOCS-72)
        /// </summary>
        [CanBeNull]
        [DataMember]
        string ReportingRadiologistForImagingExaminationResult { get; set; }

        /// <summary>
        /// Examination result name
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText ExaminationResultName { get; set; }

        /// <summary>
        /// Modality
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText Modality { get; set; }

        /// <summary>
        /// Anatomical site
        /// </summary>
        [CanBeNull]
        [DataMember]
        IList<IAnatomicalSiteExtended> AnatomicalSite { get; set; }

        /// <summary>
        /// The anatomical region
        /// </summary>
        [CanBeNull]
        [DataMember]
        AnatomicalRegion? AnatomicalRegion { get; set; }

        /// <summary>
        /// Examination Procedure
        /// </summary>
        [CanBeNull]
        [DataMember]
        string ExaminationProcedure { get; set; }

        /// <summary>
        /// Examination Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        ExaminationDetails ExaminationDetails { get; set; }

        /// <summary>
        /// The date / time of the result
        /// </summary>
        [CanBeNull]
        ISO8601DateTime ObservationDateTime { get; set; }

        /// <summary>
        /// Overall Result Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText OverallResultStatus { get; set; }

        /// <summary>
        /// Related Image
        /// </summary>
        RelatedImage RelatedImage { get; set; }

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
