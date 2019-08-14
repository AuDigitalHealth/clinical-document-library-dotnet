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

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using CDA.Generator.Common.Common.Time.Enum;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.Common.Time
{
    /// <summary>
    /// Type : EIVL_TS
    /// Definition: Specifies a periodic interval of time where the recurrence is based on activities
    /// of daily living or other important events that are time-related but not fully determined by
    /// time.
    /// </summary>
    public class EventRelatedIntervalOfTime : ITime 
    {
        #region properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// Value
        /// </value>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime Value { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// Operator
        /// </value>
        [CanBeNull]
        [DataMember]
        public OperationTypes? Operator { get; set; }

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>
        /// Event
        /// </value>
        [CanBeNull]
        [DataMember]
        public EventCodes? Event { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// Offset
        /// </value>
        [CanBeNull]
        [DataMember]
        public QuantityRange Offset { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this IEventRelatedIntervalOfTime
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        /// </summary>
        public void Validate(string path, List<ValidationMessage> messages)
        {
        }

        /// <summary>
        /// The Narrative Text for this item
        /// </summary>
        public string NarrativeText()
        {
          string narrativeText = string.Empty;

          if (Offset != null)
          {
            string offset = string.Empty;

            offset += Offset.High.HasValue ? string.Format(" {0} {1} before", Offset.High.Value.ToString(CultureInfo.InvariantCulture), Offset.Units) : string.Empty;
            offset += Offset.Low.HasValue ? string.Format(" {0} {1} after", Offset.Low.Value.ToString(CultureInfo.InvariantCulture), Offset.Units) : string.Empty;

            narrativeText += offset;
          }

          if (Value != null)
          {
            narrativeText +=  string.Format(" {0}", Value.NarrativeText());
          }

          if (Operator.HasValue)
          {
            narrativeText += string.Format(" {0}", Operator.Value.GetAttributeValue<NameAttribute, string>(a => a.Name));
          }

          if (Event.HasValue)
          {
            narrativeText += string.Format(" {0}", Event.GetAttributeValue<NameAttribute, string>(a => a.Name));
          }

          return narrativeText;
        }
     

        #endregion
    }
}
