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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an result
    /// 
    /// This class implements several interfaces and can be constrained into these interfaces to 
    /// provide specific implementations of the result object
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    internal class Result : ITestResult, IImagingResult
    {
        #region Properties

        [DataMember]
        public ICodableText ResultName { get; set; }

        [DataMember]
        public ResultValue ResultValue { get; set; }

        [DataMember]
        public HL7ObservationInterpretationNormality? NormalStatus { get; set; }

        [DataMember]
        public List<ResultValueReferenceRangeDetail> ResultValueReferenceRangeDetails { get; set; }

        [DataMember]
        public List<string> Comments { get; set; }

        [DataMember]
        public string ReferenceRangeGuidance { get; set; }

        [DataMember]
        public ICodableText ResultStatus { get; set; }
        #endregion

        #region Constructors
        internal Result()
        {
        }
        #endregion

        #region Validation
        void ITestResult.Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            validationBuilder.ArgumentRequiredCheck("OverallResultStatus", ResultStatus);
            
            if(validationBuilder.ArgumentRequiredCheck("ResultName", ResultName))
            {
                if (ResultName != null) ResultName.ValidateMandatory(validationBuilder.Path + "ResultName", messages);
            }

            if (ResultValue != null)
            {
                ResultValue.Validate(validationBuilder.Path + "ResultValue", messages);
            }

            if(ResultValueReferenceRangeDetails != null && ResultValueReferenceRangeDetails.Any())
            {
                ResultValueReferenceRangeDetails.ForEach(resultValueReferenceRangeDetail => resultValueReferenceRangeDetail.Validate(validationBuilder.Path + "ResultValueReferenceRangeDetails", messages));
            }
        }

        void IImagingResult.Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            if (validationBuilder.ArgumentRequiredCheck("ResultName", ResultName))
            {
                if (ResultName != null) ResultName.Validate(validationBuilder.Path + "ResultName", messages);
            }

            if (ResultValue != null)
            {
                ResultValue.Validate(validationBuilder.Path + "ResultValue", messages);
            }

            if (ResultValueReferenceRangeDetails != null && ResultValueReferenceRangeDetails.Any())
            {
                ResultValueReferenceRangeDetails.ForEach(resultValueReferenceRangeDetail => resultValueReferenceRangeDetail.Validate(validationBuilder.Path + "ResultValueReferenceRangeDetails", messages));
            }
        }
        #endregion
    }
}
