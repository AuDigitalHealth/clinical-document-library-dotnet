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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Pathology
{
       /// <summary>
      /// This class that encapsulates all the SCS specific context for an Pathology Test Result
      /// </summary>
      public class PathologyTestResult
      {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Reporting Pathologist Name Narrative Only
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ReportingPathologistForTestResult { get; set; }

        /// <summary>
        /// Test Result Name (Pathology Test Result Name)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TestResultName { get; set; }

        /// <summary>
        /// Department Code (Diagnostic Service)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public DiagnosticService? PathologyDiscipline { get; set; }

        /// <summary>
        /// Overall Test Result Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText OverallTestResultStatus { get; set; }

        /// <summary>
        /// Test Specimen Detail
        /// </summary>
        [CanBeNull]
        [DataMember]
        public TestSpecimenDetail TestSpecimenDetail { get; set; }

        /// <summary>
        /// Pathology Test Result DateTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime ObservationDateTime { get; set; }

        #region Constructors

        internal PathologyTestResult()
        {

        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate the Content for this Pathology Result Report
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("TestResultName", TestResultName))
            {
               TestResultName.Validate(vb.Path + "TestResultName", vb.Messages);
            }

            vb.ArgumentRequiredCheck("OverallTestResultStatus", OverallTestResultStatus);

            vb.ArgumentRequiredCheck("DepartmentCode", PathologyDiscipline);

            if (vb.ArgumentRequiredCheck("TestSpecimenDetail", TestSpecimenDetail))
            {
               TestSpecimenDetail.Validate(vb.Path + "TestSpecimenDetail", vb.Messages);
            }

            vb.ArgumentRequiredCheck("ObservationDateTime", ObservationDateTime);
        }

        #endregion
      }
}

