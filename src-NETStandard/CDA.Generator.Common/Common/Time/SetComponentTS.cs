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
using System.Runtime.Serialization;
using CDA.Generator.Common.Common.Time.Enum;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.Common.Time
{
    /// <summary>
    /// Type : SXCM_TS
    /// Definition: A set of points in time, specifying the timing of events and actions and the
    /// cyclical validity-patterns that may exist for certain kinds of information, such as phone
    /// numbers (evening, daytime), addresses (so called "snowbirds," residing closer to the
    /// equator during winter and farther from the equator during summer) and office hours.
    /// </summary>
    public class SetComponentTS : ITime 
    {
        #region properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime Value { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        [CanBeNull]
        [DataMember]
        public OperationTypes? Operator { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this ISetComponentTS
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (Value == null)
          {
            vb.AddValidationMessage(vb.PathName, null, "Need to specify a value");
          }
        }

        #endregion

        #region Narrative

        /// <summary>
        /// The Narrative Text for this item
        /// </summary>
        public string NarrativeText()
        {
          string narrativeText = string.Empty;

          if (Value != null)
          {
            narrativeText += string.Format(" {0}", Value.NarrativeText());
          }

          if (Operator.HasValue)
          {
            narrativeText += string.Format(" {0}", Operator.Value.GetAttributeValue<NameAttribute, string>(a => a.Name));
          }

          return narrativeText;
        }

        #endregion 
    }
}
