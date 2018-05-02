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

using Nehta.VendorLibrary.Common;
using JetBrains.Annotations;
using System.Runtime.Serialization;
using Nehta.HL7.CDA;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class encapsulates all the CDA specific context for a Frequency
    /// </summary>
    public class FrequencyQuantity 
    {
        /// <summary>
        /// NullFlavor
        /// </summary>
        [CanBeNull]
        [DataMember]
        public NullFlavor? NullFlavor { get; set; }

        /// <summary>
        /// Denominator.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity Denominator { get; set; }

        /// <summary>
        /// Numerator.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity Numerator { get; set; }
        
        /// <summary>
        /// Validates the FrequencyQuantity
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (NullFlavor.HasValue && Denominator != null && Numerator != null)
            {
              vb.AddValidationMessage(vb.Path, null, "If nullFlavor is specified then Denominator and Numerator SHALL not be included");
            }
        }
    }
}
