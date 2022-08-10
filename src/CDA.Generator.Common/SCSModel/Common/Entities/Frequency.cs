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
using Nehta.VendorLibrary.CDA.Common;
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
    public class Frequency 
    {
        ///// <summary>
        ///// NullFlavor
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public NullFlavor? NullFlavor { get; set; }

        /// <summary>
        /// Denominator
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity Denominator { get; set; }

        /// <summary>
        /// Numerator
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity Numerator { get; set; }
        
        /// <summary>
        /// Validates the Frequency
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Denominator != null)
            {
              Denominator.Validate(path, messages);
              vb.ArgumentRequiredCheck("Numerator", Numerator);
            }
        }

        /// <summary>
        /// This property returns text that is appropriate for the narrative
        /// </summary>
        public string NarrativeText
        {
          get
          {
            var narrative = String.Empty;

            //if (NullFlavor.HasValue)
            //{
            //  narrative += string.Format(" {0}", NullFlavor.GetAttributeValue<NameAttribute, string>(a => a.Name));
            //}

            if (Denominator != null)
            {
              narrative += !Denominator.Value.IsNullOrEmptyWhitespace() ? string.Format(" {0}", Denominator.Value) : String.Empty;
              narrative += !Denominator.UnitCode.IsNullOrEmptyWhitespace() ? string.Format(" {0}", Denominator.UnitCode)  : String.Empty;              
            }

            if (Numerator != null)
            {
              narrative += !Numerator.Value.IsNullOrEmptyWhitespace() ? string.Format(" {0}", Numerator.Value) : String.Empty;
              narrative += !Numerator.UnitCode.IsNullOrEmptyWhitespace() ? string.Format(" {0}", Numerator.UnitCode) : String.Empty;   
            }

            return narrative;
          }
        }
    }
}
