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
using System.Globalization;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a quantity range.
    /// </summary>
    [Serializable]
    [DataContract]
    public class QuantityRange
    {
        #region Properties
        /// <summary>
        /// High
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Double? High { get; set; }

        /// <summary>
        /// Low
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Double? Low { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Units { get; set; }
        #endregion

        #region Constructors
        internal QuantityRange()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this quantity range
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            validationBuilder.ArgumentRequiredCheck("High", High);
            validationBuilder.ArgumentRequiredCheck("Low", Low);
            validationBuilder.ArgumentRequiredCheck("Unit", Units);
        }

        #endregion

        /// <summary>
        /// This property returns text that is appropriate for the narrative.
        /// </summary>
        public string NarrativeText
        {
            get
            {
                var narrative = String.Empty;

                narrative += High.HasValue ? "High: " + High.Value.ToString(CultureInfo.InvariantCulture) + Units + " " : String.Empty;
                narrative += Low.HasValue ? "Low: " + Low.Value.ToString(CultureInfo.InvariantCulture) + Units + " " : String.Empty;

                return narrative;
            }
        }
    }
}
