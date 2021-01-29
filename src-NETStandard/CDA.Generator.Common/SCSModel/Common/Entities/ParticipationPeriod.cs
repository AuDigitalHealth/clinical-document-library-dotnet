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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Participation Period 
    /// </summary>
    public class ParticipationPeriod 
    {
        /// <summary>
        /// Interval
        /// </summary>
        public CdaInterval Interval { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public ISO8601DateTime Value { get; set; }

        /// <summary>
        /// Validates the CdaInterval.
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Encounter Period can only contain a Encounter Period NullFlavor or a Encounter Period
            var participationPeriodDictionary = new Dictionary<string, object>()
            {
                { "Interval", Interval },
                { "Value", Value }
            };
            vb.ChoiceCheck(participationPeriodDictionary);
        }

        #region Narrative

      /// <summary>
      /// The NarrativeText 
      /// </summary>
      public string NarrativeText()
      {
          return Interval != null ? Interval.NarrativeText() : Value.NarrativeText();
      }

        #endregion 


    }
   
}


