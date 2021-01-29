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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an request group
    /// 
    /// This class implements several interfaces and can be constrained into these interfaces to 
    /// provide specific implementations of the request object
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Result))]
    internal class ResultGroup : ITestResultGroup, IImagingResultGroup
    {
        #region Properties

        /// <summary>
        /// Result group name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ResultGroupName { get; set; }

        /// <summary>
        /// A list of pathology test results
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Result> Results { get; set; }

        /// <summary>
        /// A list of pathology test results
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<ITestResult> ITestResultGroup.Results { get; set; }

        /// <summary>
        /// A list of pathology test results
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IImagingResult> IImagingResultGroup.Results { get; set; }

        /// <summary>
        /// Result group specimen detail
        /// </summary>
        [CanBeNull]
        [DataMember]
        public SpecimenDetail ResultGroupSpecimenDetail { get; set; }

        /// <summary>
        /// Anatomical Site
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AnatomicalSite AnatomicalSite { get; set; } 
        #endregion

        #region Constructors
        internal ResultGroup()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this result group
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void ITestResultGroup.Validate(string path, List<ValidationMessage> messages)
        {
            var resultGroup = (ITestResultGroup)this;
            var results = resultGroup.Results;

            var validationBuilder = new ValidationBuilder(path, messages);

            if(validationBuilder.ArgumentRequiredCheck("ResultGroupName", ResultGroupName))
            {
                if (ResultGroupName != null)
                    ResultGroupName.Validate(validationBuilder.Path + "ResultGroupName", messages);
            }

            if (validationBuilder.ArgumentRequiredCheck("Results", results))
            {
                if (validationBuilder.RangeCheck("Results", results, 1, Int32.MaxValue))
                {
                    if (results != null) results.ForEach(testResult => testResult.Validate(validationBuilder.Path + "Results", messages));
                }
            }

            if (ResultGroupSpecimenDetail != null)
            {
                ResultGroupSpecimenDetail.Validate(validationBuilder.Path + "ResultGroupSpecimenDetail", messages);
            }

        }

        /// <summary>
        /// Validates this result group
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IImagingResultGroup.Validate(string path, List<ValidationMessage> messages)
        {
            var resultGroup = (IImagingResultGroup)this;
            var results = resultGroup.Results;

            var validationBuilder = new ValidationBuilder(path, messages);

            if (validationBuilder.ArgumentRequiredCheck("ResultGroupName", ResultGroupName))
            {
                if (ResultGroupName != null)
                    ResultGroupName.ValidateMandatory(validationBuilder.Path + "ResultGroupName", messages);
            }

            if (validationBuilder.ArgumentRequiredCheck("Results", results))
            {
                if (validationBuilder.RangeCheck("Results", results, 1, Int32.MaxValue))
                {
                    if (results != null) results.ForEach(testResult => testResult.Validate(validationBuilder.Path + "Results", messages));
                }
            }

            if ( AnatomicalSite != null)
            {
                 if (AnatomicalSite != null) AnatomicalSite.Validate(validationBuilder.Path + "AnatomicalSite", messages);
            }
        }
        #endregion
    }
}
