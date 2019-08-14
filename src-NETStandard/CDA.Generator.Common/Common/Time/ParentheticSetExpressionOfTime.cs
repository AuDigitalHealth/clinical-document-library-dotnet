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
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.Common.Time.Enum;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.Common.Time
{
    /// <summary>
    /// Type : SXPR_TS
    /// Definition: A set-component that is itself made up of set of contained components that
    /// are evaluated as one value.
    /// </summary>
    public class ParentheticSetExpressionOfTime : ITime
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
        /// Gets or sets the comp.
        /// </summary>
        /// <value>
        /// Comp
        /// </value>
        [CanBeNull]
        [DataMember]
        public List<ITime> Comp { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this IParentheticSetExpressionOfTime
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Comp", Comp);
        }

        #endregion

        #region Narrative

        /// <summary>
        /// The Narrative Text for this item
        /// </summary>
        public string NarrativeText()
        {
          string narrativeText = string.Empty;

          if (Operator.HasValue)
          {
            narrativeText += string.Format(" {0}", Operator.Value.GetAttributeValue<NameAttribute, string>(a => a.Name));
          }

          if (Value != null)
          {
            narrativeText += string.Format(" {0}", Value.NarrativeText());
          }

          if (Comp != null)
            narrativeText = Comp.Aggregate(narrativeText, (current, time) => current + string.Format(" {0}", time.NarrativeText()));

          return narrativeText;
        }

        #endregion 
    }
}
