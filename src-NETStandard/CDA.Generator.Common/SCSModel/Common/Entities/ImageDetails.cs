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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that are common to each item entry
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(DischargeSummary.ImageDetails))]
    public class ImageDetails : IImageDetails, IDiagnosticImageDetails
    {
        #region Properties
        /// <summary>
        /// The Image ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InstanceIdentifier ImageIdentifier { get; set; }

        /// <summary>
        /// The DICOM series ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InstanceIdentifier SeriesIdentifier { get; set; }

        /// <summary>
        /// Image View Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ImageViewName { get; set; }

        /// <summary>
        /// Subject position
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String SubjectPosition { get; set; }

        /// <summary>
        /// The image date / time
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTime { get; set; }

        /// <summary>
        /// The image
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ExternalData Image { get; set; }
        #endregion

        #region Constructors
        internal ImageDetails()
        {

        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this image detail for Image Details
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            if (SeriesIdentifier != null)
            {
                SeriesIdentifier.Validate(validationBuilder.Path + "ImageIdentifier", messages);
            }

            if (ImageIdentifier != null)
            {
                ImageIdentifier.Validate(validationBuilder.Path + "ImageIdentifier", messages);
            }

            if (ImageViewName != null)
            {
                ImageViewName.Validate(validationBuilder.Path + "ImageViewName", messages);
            }

            if (Image != null)
            {
                Image.Validate(validationBuilder.Path + "Image", messages);
            }
        }

        /// <summary>
        /// Validates this image detail for IDiagnosticImageDetails
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IDiagnosticImageDetails.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("DateTime", DateTime);

            if (Image != null)
            {
                Image.Validate(vb.Path + "Image", messages);
            }
        }

        

        #endregion
    }
}