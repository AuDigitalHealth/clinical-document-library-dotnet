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
using System.Text;
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
        public String UnitCode { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String UnitDisplayName { get; set; }

        /// <summary>
        /// Whether the range includes the bounds e.g. "less than or equal to 5" or excludes the bounds e.g. "less than 5".
        /// </summary>
        [DataMember]
        public bool Inclusive { get; set; } = true;
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
            
            if (!High.HasValue)
            {
                // If you don't have a high you must have a low
                validationBuilder.ArgumentRequiredCheck("Low", Low);
            }
            
            if (!Low.HasValue)
            {
                // If you don't have a low you must have a high
                validationBuilder.ArgumentRequiredCheck("High", High);
            }

            validationBuilder.ArgumentRequiredCheck("UnitCode", UnitCode);

            if (High.HasValue && Low.HasValue && !Inclusive)
            {
                validationBuilder.AddValidationMessage("Inclusive", Inclusive.ToString(), "Bounded intervals must be inclusive");
            }
        }

        #endregion

        /// <summary>
        /// This property returns text that is appropriate for the narrative.
        /// </summary>
        public string NarrativeText
        {
            get
            {
                var narrative = new StringBuilder();

                if (High.HasValue && Low.HasValue) // bounded
                {
                    narrative.Append(Low.Value.ToString(CultureInfo.InvariantCulture));
                    narrative.Append(" - ");
                    narrative.Append(High.Value.ToString(CultureInfo.InvariantCulture));
                    narrative.Append(" ");
                    narrative.Append(UnitDisplayName ?? UnitCode);
                }
                else if (High.HasValue && !Low.HasValue) // right-bounded
                {
                    narrative.Append("<");
                    if (Inclusive) narrative.Append("=");
                    narrative.Append(" ");
                    narrative.Append(High.Value.ToString(CultureInfo.InvariantCulture));
                    narrative.Append(" ");
                    narrative.Append(UnitDisplayName ?? UnitCode);
                }
                else if (Low.HasValue && !High.HasValue) // left-bounded
                {
                    narrative.Append(">");
                    if (Inclusive) narrative.Append("=");
                    narrative.Append(" ");
                    narrative.Append(Low.Value.ToString(CultureInfo.InvariantCulture));
                    narrative.Append(" ");
                    narrative.Append(UnitDisplayName ?? UnitCode);
                }
                else // unbounded
                {
                    narrative.Append("[unbounded interval]");
                }

                return narrative.ToString();
            }
        }
    }
}
