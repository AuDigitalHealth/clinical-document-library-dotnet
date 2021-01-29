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

using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an persons physical details
    /// 
    /// E.g. Height, weight etc
    /// </summary>
    [DataContract]
    public class PhysicalDetails
    {
        #region Properties
        /// <summary>
        /// Weight/Volume
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity WeightVolume { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ExternalData Image { get; set; }

        #endregion

        #region Constructors
        internal PhysicalDetails()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates the physical details
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (WeightVolume != null)
            {
                WeightVolume.Validate(vb.Path + "WeightVolume", vb.Messages);
            }

            if (Image != null)
            {
                Image.Validate(path + ".Image", messages);
            }

        }
        #endregion
    }
}