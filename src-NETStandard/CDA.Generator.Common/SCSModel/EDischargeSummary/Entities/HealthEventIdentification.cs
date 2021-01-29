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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an Health Event Identification class
    /// </summary>
    [Serializable]
    [DataContract]
    public class HealthEventIdentification
    {
        /// <summary>
        /// The Identification Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier Identifier { get; set; }

        /// <summary>
        /// Health event started date/time.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime HealthEventStarted { get; set; }

        /// <summary>
        /// Health event ended date/time.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime HealthEventEnded { get; set; }

        /// <summary>
        /// Validates this Health Event Identification
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("HealthEventStarted", HealthEventStarted);
        }
    }
}
