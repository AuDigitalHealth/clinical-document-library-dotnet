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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that are common to ImageDetails
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Common.ImageDetails))]
    public class ImageDetails : Common.ImageDetails, IImageDetailsDischargeSummary
    {
        #region Properties

        /// <summary>
        /// CodableText Category 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Category { get; set; }

        /// <summary>
        /// CodableText Test Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TestName { get; set; }

        /// <summary>
        /// The Image Anatomical Site
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> AnatomicalSite { get; set; }

        /// <summary>
        /// The View
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<View> View { get; set; }

        #endregion

        #region Constructors
        internal ImageDetails()
        {

        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Image Details for discharge summary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IImageDetailsDischargeSummary.Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            if (Category != null)
            {
                Category.Validate(validationBuilder.Path + ".Category", messages);
            }

            if (TestName != null)
            {
                TestName.Validate(validationBuilder.Path + ".TestName", messages);
            }

            if (AnatomicalSite != null && AnatomicalSite.Any())
            {
                AnatomicalSite.ForEach(anatomicalSite => anatomicalSite.Validate(path + ".AnatomicalSite", messages));
            }

            if (View != null && View.Any())
            {
                View.ForEach(view => view.Validate(path + ".View", messages));
            }

        }
        #endregion
    }
}