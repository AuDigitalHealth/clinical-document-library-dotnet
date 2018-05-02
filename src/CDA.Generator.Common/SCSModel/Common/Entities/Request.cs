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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a request
    /// 
    /// This class implements several interfaces and can be constrained into these interfaces to 
    /// provide specific implementations of the request object
    /// 
    /// This class is a composition and requesters, and as such only one of the request
    /// properties is required to be set
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(ImageDetails))]
    internal class Request : ITestRequest, IImagingExaminationRequest
    {
        #region Properties

        /// <summary>
        /// Requester Order Identifier
        /// </summary>
        public InstanceIdentifier RequesterOrderIdentifier { get; set; }

        /// <summary>
        /// Laboratory test result ID
        /// </summary>
        [DataMember]
        public InstanceIdentifier LaboratoryTestResultIdentifier { get; set; }

        /// <summary>
        /// A list of the tests requested
        /// </summary>
        [DataMember]
        public List<ICodableText> TestsRequestedName { get; set; }

        /// <summary>
        /// The name(s) of the requested examination(s)
        /// </summary>
        [DataMember]
        public List<string> ExaminationRequestedName { get; set; }

        /// <summary>
        /// The DICOM study identifier
        /// </summary>
        [DataMember]
        public InstanceIdentifier StudyIdentifier { get; set; }

        /// <summary>
        /// Report identifier
        /// </summary>
        [DataMember]
        public InstanceIdentifier ReportIdentifier { get; set; }

        /// <summary>
        /// Image Details
        /// </summary>
        [DataMember]
        public List<IImageDetails> ImageDetails { get; set; }
        #endregion

        #region Constructors
        internal Request()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this request
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IImagingExaminationRequest.Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            if (RequesterOrderIdentifier != null)
            {
                RequesterOrderIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (ReportIdentifier != null)
            {
                ReportIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (StudyIdentifier != null)
            {
                StudyIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (LaboratoryTestResultIdentifier != null)
            {
                LaboratoryTestResultIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (ImageDetails != null && ImageDetails.Any())
            {
                foreach (var imageDetail in ImageDetails)
                {
                    imageDetail.Validate(validationBuilder.Path, messages);   
                }
            }
        }

        /// <summary>
        /// Validates this request
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void ITestRequest.Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            if (RequesterOrderIdentifier != null)
            {
                RequesterOrderIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (ReportIdentifier != null)
            {
                ReportIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (StudyIdentifier != null)
            {
                StudyIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (RequesterOrderIdentifier != null)
            {
                RequesterOrderIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (TestsRequestedName != null && TestsRequestedName.Any())
            {
                foreach (var testsRequestedName in TestsRequestedName)
                {
                    testsRequestedName.Validate(path, messages);
                }
            }

            if (RequesterOrderIdentifier != null && TestsRequestedName == null)
            {
                validationBuilder.AddValidationMessage("RequesterOrderIdentifier", null, "This value requires that TestsRequestedName be populated");
            }

            if (LaboratoryTestResultIdentifier != null)
            {
                LaboratoryTestResultIdentifier.Validate(validationBuilder.Path, messages);
            }

            if (ImageDetails != null && ImageDetails.Any())
            {
                foreach (var imageDetail in ImageDetails)
                {
                    imageDetail.Validate(validationBuilder.Path, messages);
                }
            }
        }
        #endregion
    }
}
