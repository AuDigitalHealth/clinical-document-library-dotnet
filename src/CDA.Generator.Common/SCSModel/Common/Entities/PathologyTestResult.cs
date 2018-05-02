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
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an pathology test result
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Request))]
    [KnownType(typeof(ResultGroup))]
    public class PathologyTestResult
    {
        #region Properties

        /// <summary>
        /// xPreNarrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string XPreNarrative { get; set; }

        /// <summary>
        /// Test result name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TestResultName { get; set; }

        /// <summary>
        /// Diagnostic service
        /// </summary>
        [CanBeNull]
        [DataMember]
        public DiagnosticServiceSectionID? DiagnosticService { get; set; }

        /// <summary>
        /// Pathology test specimen detail
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<SpecimenDetail> TestSpecimenDetail { get; set; }

        /// <summary>
        /// The overall status of the test
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText OverallTestResultStatus { get; set; }

        /// <summary>
        /// Any clinical information that has been provided
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ClinicalInformationProvided { get; set; }

        /// <summary>
        /// A list of test result groups
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ITestResultGroup> ResultGroup { get; set; }

        /// <summary>
        /// A list of pathological diagnosis
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> PathologicalDiagnosis { get; set; }

        /// <summary>
        /// Conclusion
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Conclusion { get; set; }

        /// <summary>
        /// A list of test result representations
        /// </summary>
        [CanBeNull]
        [DataMember]
        public EncapsulatedData TestResultRepresentation { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string TestComment { get; set; }

        /// <summary>
        /// A list of test requests
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ITestRequest> TestRequestDetails { get; set; }

        /// <summary>
        /// Test Result Date / Time
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime ObservationDateTime { get; set; }

        /// <summary>
        /// CustomNarrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativePathologyTestResult { get; set; }

        /// <summary>
        /// Reporting Pathologist
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationReportingPathologist ReportingPathologist { get; set; }

        #endregion

        #region Constructors
        internal PathologyTestResult()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this pathology test result
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            validationBuilder.ArgumentRequiredCheck("ObservationDateTime", ObservationDateTime);

            validationBuilder.ArgumentRequiredCheck("OverallTestResultStatus", OverallTestResultStatus);

            validationBuilder.ArgumentRequiredCheck("PathologyTestSpecimenDetail", TestSpecimenDetail);

            if (TestSpecimenDetail != null && TestSpecimenDetail.Any())
            {
                TestSpecimenDetail.ForEach(testSpecimenDetail => testSpecimenDetail.Validate(validationBuilder.Path + "PathologyTestSpecimenDetail", messages, !XPreNarrative.IsNullOrEmptyWhitespace()));
            } 

            if (ResultGroup != null && ResultGroup.Any())
            {
                ResultGroup.ForEach(testResultGroup => testResultGroup.Validate(validationBuilder.Path + "PathologyTestResultGroup", messages));
            }

            if (validationBuilder.ArgumentRequiredCheck("TestResultName", TestResultName))
            {
                if (TestResultName != null) TestResultName.ValidateMandatory(validationBuilder.Path + "TestResultName", messages);
            }

            if (TestResultRepresentation != null)
            {
                TestResultRepresentation.Validate(validationBuilder.Path + "TestResultRepresentation", messages);
            }

            if (TestRequestDetails != null && TestRequestDetails.Any())
            {
                foreach (var testRequestDetails in TestRequestDetails)
                {
                    testRequestDetails.Validate(validationBuilder.Path + "TestRequestDetails", messages);
                }
            }
            
        }
        #endregion
    }
}
