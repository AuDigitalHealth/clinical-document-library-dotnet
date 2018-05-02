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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The Dispensing Information class contains all the properties that CDA has identified for 
    /// a Dispense item
    /// 
    /// Please use the CreateDispensingInformation() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class DispensingInformation  
    {
        #region Properties

        /// <summary>
        /// Quantity to Dispense (AMOUNT OF MEDICATION) - Quantity Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String QuantityDescription { get; set; }

        /// <summary>
        /// Maximum Number of Repeats (Number of Repeats)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public int? MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// Minimum Interval Between Repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval MinimumIntervalBetweenRepeats { get; set; }

        /// <summary>
        /// Brand Substitution Permitted
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? BrandSubstitutionPermitted { get; set; }

        #endregion

        #region Constructors

        internal DispensingInformation()
        {
            
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Dispensing Information object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);
            validationBuilder.ArgumentRequiredCheck("QuantityDescription", QuantityDescription);

            if (MinimumIntervalBetweenRepeats != null)
            {
                if (MinimumIntervalBetweenRepeats.IntervalWidth == null)
                {
                  validationBuilder.AddValidationMessage(validationBuilder.PathName, null, "MinimumIntervalBetweenRepeats must be and IntervalWidth and use a mesure of time");
                }
            }
        }
        #endregion
    }
}