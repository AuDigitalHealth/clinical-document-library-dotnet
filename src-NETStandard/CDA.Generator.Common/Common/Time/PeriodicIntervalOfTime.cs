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
    /// Type : PIVL_TS
    /// Definition: An interval of time that recurs periodically. Periodic intervals have two
    /// properties, phase and period. The phase specifies the "interval prototype" that is repeated
    /// every period.
    /// </summary>
    public class PeriodicIntervalOfTime : ITime 
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
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>
        /// Alignment
        /// </value>
        [CanBeNull]
        [DataMember]
        public AlignmentCodes? Alignment { get; set; }

        /// <summary>
        /// Gets or sets the institution specified.
        /// </summary>
        /// <value>
        /// InstitutionSpecified
        /// </value>
        [CanBeNull]
        [DataMember]
        public bool? InstitutionSpecified { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>
        /// Phase
        /// </value>
        [CanBeNull]
        [DataMember]
        public CdaInterval Phase { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// Period
        /// </value>
        [CanBeNull]
        [DataMember]
        public Quantity Period { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// Frequency
        /// </value>
        [CanBeNull]
        [DataMember]
        public Frequency Frequency { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this PeriodicIntervalOfTime
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Phase == null && Frequency == null && Period == null && Value == null)
            {
              vb.AddValidationMessage(vb.PathName, null, "Need to specify a period or a frequency or a value");
            }

            if (Period != null)
              Period.Validate(path, messages);

            if (Frequency != null)
              Frequency.Validate(path, messages);

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

          if (Alignment.HasValue)
          {
            narrativeText += string.Format(" {0}", Alignment.Value.GetAttributeValue<NameAttribute, string>(a => a.Name));
          }

          if (InstitutionSpecified != null)
          {
            narrativeText += string.Format(" Institution Specified:{0}", InstitutionSpecified.Value.ToString(CultureInfo.InvariantCulture));
          }

          if (Phase != null)
          {
            narrativeText += string.Format(" repeating interval specifying the duration of each occurrence:{0}", Phase.NarrativeText());
          }

          if (Period != null)
          {
            narrativeText += string.Format(" frequency at which the periodic interval repeats:{0}", Period.NarrativeText);
          }

          if (Frequency != null)
          {
            narrativeText += string.Format(" number of repeats ratio:{0}", Frequency.NarrativeText);
          }

          return narrativeText;
        }

        #endregion 

    }
}
