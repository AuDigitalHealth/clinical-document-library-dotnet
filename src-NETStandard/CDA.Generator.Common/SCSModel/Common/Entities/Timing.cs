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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a Timing oject
    /// </summary>
    [Serializable]
    [DataContract]
    public class Timing
    {
        /// <summary>
        /// Timing Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string TimingDescription { get; set; }

        /// <summary>
        /// Structured Timing
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StructuredTiming StructuredTiming { get; set; }       

        /// <summary>
        /// PRN
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? PRN { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime StartDate { get; set; }

        /// <summary>
        /// Stop Date
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime StopDate { get; set; }

        /// <summary>
        /// Start Criterion
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? StartCriterion { get; set; }

        /// <summary>
        /// Stop Date
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? StopCriterion { get; set; }

        /// <summary>
        /// Number of Administrations
        /// </summary>
        [CanBeNull]
        [DataMember]
        public FrequencyQuantity NumberOfAdministrations { get; set; }

        /// <summary>
        /// Long-Term
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? LongTerm { get; set; }

        /// <summary>
        /// Validates Timing
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (StructuredTiming != null)
            {
                StructuredTiming.Validate(vb.Path + "StructuredTiming", messages);
            }

            if (NumberOfAdministrations != null)
            {
                NumberOfAdministrations.Validate(vb.Path + "NumberOfAdministrations", messages);
            }          
 
        }
    }
}
