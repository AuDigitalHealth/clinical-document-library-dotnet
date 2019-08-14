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
using CDA.Generator.Common.SCSModel.DiagnosticImagingReport.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an imaging examination result.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(ResultGroup))]
    [KnownType(typeof(Request))]
    [KnownType(typeof(DischargeSummary.ImagingExaminationResult))]
    public class ImagingExaminationResult : IImagingExaminationResult, IDiagnosticImagingExaminationResult
    {
        #region Properties

        /// <summary>
        /// Examination result name
        /// </summary>
        [DataMember]
        public ICodableText ExaminationResultName { get; set; }

        /// <summary>
        /// Reporting Radiologist For Imaging Examination Result Name - Narrative Only (IPCDOCS-72)
        /// </summary>
        [DataMember]
        public string ReportingRadiologistForImagingExaminationResult { get; set; }

        /// <summary>
        /// Modality
        /// </summary>
        [DataMember]
        public ICodableText Modality { get; set; }

        /// <summary>
        /// Anatomical site
        /// </summary>
        [DataMember]
        IList<IAnatomicalSiteExtended> IDiagnosticImagingExaminationResult.AnatomicalSite { get; set; }

        /// <summary>
        /// Anatomical site
        /// </summary>
        [DataMember]
        public List<AnatomicalSite> AnatomicalSite { get; set; }

        /// <summary>
        /// Result status
        /// </summary>
        [DataMember]
        public ICodableText ExaminationResultStatus { get; set; }

        /// <summary>
        /// Diagnostic Image Details
        /// </summary>
        [DataMember]
        public IDiagnosticImageDetails DiagnosticImageDetails { get; set; }

        /// <summary>
        /// Clinical information provided
        /// </summary>
        [DataMember]
        public String ClinicalInformationProvided { get; set; }

        /// <summary>
        /// Findings
        /// </summary>
        [DataMember]
        public String Findings { get; set; }

        /// <summary>
        /// A list of imaging results
        /// </summary>
        [DataMember]
        public List<IImagingResultGroup> ResultGroup { get; set; }

        /// <summary>
        /// examination result representation
        /// </summary>
        [DataMember]
        public String ExaminationResultRepresentation { get; set; }

        /// <summary>
        /// A list of examination requests
        /// </summary>
        [DataMember]
        public List<IImagingExaminationRequest> ExaminationRequestDetails { get; set; }

        /// <summary>
        /// Observation Date Time
        /// </summary>
        [DataMember]
        public ISO8601DateTime ObservationDateTime { get; set; }

        /// <summary>
        /// Examination procedures
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ExaminationProcedure { get; set; }

        /// <summary>
        /// The date / time of the result
        /// </summary>
        [DataMember]
        public ISO8601DateTime ResultDateTime { get; set; }

        /// <summary>
        /// Overall Result Status
        /// </summary>
        [DataMember]
        public  HL7ResultStatus? OverallResultStatus { get; set; }

        /// <summary>
        /// Result Status
        /// </summary>
        [DataMember]
        ICodableText IDiagnosticImagingExaminationResult.OverallResultStatus { get; set; }

        /// <summary>
        /// Examination Details
        /// </summary>
        [DataMember]
        public ExaminationDetails ExaminationDetails { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrativeImagingExaminationResult { get; set; }

        /// <summary>
        /// Related Image
        /// </summary>
        [DataMember]
        public RelatedImage RelatedImage { get; set; }

        /// <summary>
        /// The anatomical region
        /// </summary>
        [DataMember]
        public AnatomicalRegion? AnatomicalRegion { get; set; }

        #endregion

        #region Constructors
        internal ImagingExaminationResult()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this imaging examination result
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ExaminationResultName", ExaminationResultName))
            {
                if (ExaminationResultName != null) ExaminationResultName.ValidateMandatory(vb.Path + "ExaminationResultName", messages);
            }

            vb.ArgumentRequiredCheck("ResultDateTime", ResultDateTime);

            if (Modality != null)
            {
                Modality.Validate(vb.Path + "Modality", messages);
            }

            if (AnatomicalSite != null && AnatomicalSite.Any())
            {
                AnatomicalSite.ForEach(anatomicalSite => anatomicalSite.Validate(vb.Path + "AnatomicalSite", messages));
            }

            if (ExaminationRequestDetails != null)
            {
                ExaminationRequestDetails.ForEach(ExaminationRequestDetail => ExaminationRequestDetail.Validate(vb.Path + "ExaminationRequestDetails", messages));
            }

            if (ResultGroup != null && ResultGroup.Any())
            {
                ResultGroup.ForEach(imagingResultGroup => imagingResultGroup.Validate(vb.Path + "ResultGroup", messages));
            }
        }

        /// <summary>
        /// Validates this imaging examination result
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IDiagnosticImagingExaminationResult.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var diagnosticImagingExaminationResult = (IDiagnosticImagingExaminationResult)this;

            vb.ArgumentRequiredCheck("OverallResultStatus", diagnosticImagingExaminationResult.OverallResultStatus);
           
            if (vb.ArgumentRequiredCheck("ExaminationResultName", ExaminationResultName))
            {
                ExaminationResultName.ValidateMandatory(vb.Path + "ExaminationResultName", messages);
            }

            if (vb.ArgumentRequiredCheck("Modality", Modality))
            {
                Modality.Validate(vb.Path + "Modality", messages);
            }

            if (diagnosticImagingExaminationResult.AnatomicalSite != null)
            {
                for (int index = 0; index < diagnosticImagingExaminationResult.AnatomicalSite.Count; index++)
                {
                    diagnosticImagingExaminationResult.AnatomicalSite[index].Validate(vb.Path + string.Format("AnatomicalSite[{0}]", index), messages);
                }
            }

            // Optional Related Image
            if (RelatedImage != null)
            {
                RelatedImage.Validate(vb.Path + "RelatedImage", messages); // Checks that the ImageUrl is included
            }

            vb.ArgumentRequiredCheck("ExaminationProcedure", ExaminationProcedure);

            if (vb.ArgumentRequiredCheck("ExaminationDetails", ExaminationDetails))
            {
                ExaminationDetails.Validate(vb.Path + "ExaminationDetails", messages);
            }

            vb.ArgumentRequiredCheck("ObservationDateTime", ObservationDateTime);
        }

        #endregion
    }
}
