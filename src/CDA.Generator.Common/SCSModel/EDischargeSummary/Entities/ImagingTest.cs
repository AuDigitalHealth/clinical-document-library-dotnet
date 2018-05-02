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
using System.Linq;
using JetBrains.Annotations;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that pertain to a ImagingTest
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(ImageDetails))]
    [KnownType(typeof(CodableText))]
    public class ImagingTest
    {
        #region Properties
        /// <summary>
        ///  A list of Image Details for a Discharge Summary ImagingTest
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IImageDetailsDischargeSummary> ImagingDetails { get; set; }

        /// <summary>
        /// Imaging Quality
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ImagingQuality { get; set; }

        /// <summary>
        /// Overall Finding
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText OverallFinding { get; set; }

        /// <summary>
        /// The Protocol
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Protocol Protocol { get; set; }

        #endregion

        #region Constructors
        internal ImagingTest()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Imaging Test
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ImagingDetails != null && ImagingDetails.Any())
            {
                ImagingDetails.ForEach(pathologyTestResult => pathologyTestResult.Validate(path + ".ImagingDetails", messages));
            }

            if (ImagingQuality != null)
            {
                ImagingQuality.Validate(path + ".ImagingQuality", messages);
            }

            if (OverallFinding != null)
            {
                OverallFinding.Validate(path + ".OverallFinding", messages);
            }

            if (Protocol != null)
            {
                Protocol.Validate(path + ".Protocol", messages);
            }
        }
        #endregion

    }
}
