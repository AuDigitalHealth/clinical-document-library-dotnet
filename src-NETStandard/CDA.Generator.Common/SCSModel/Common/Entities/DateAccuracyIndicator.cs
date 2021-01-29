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
using Enum = Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is represents the date accuracy indicator as defined by the CDA data model, it 
    /// provies a validation method and expects the variour accuracy indications to be passed in as 
    /// part of the constructor
    /// </summary>
    [Serializable]
    [DataContract]
    public class DateAccuracyIndicator
    {
        #region Properties
        /// <summary>
        /// A boolean indicating if the day is accurate
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? DayAccurate { get; set; }

        /// <summary>
        /// A boolean indicating if the month is accurate
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? MonthAccurate { get; set; }

        /// <summary>
        /// A boolean indicating if the year is accurate
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? YearAccurate { get; set; }
        #endregion

        #region Constructors
        internal DateAccuracyIndicator()
        {
            
        }

        /// <summary>
        /// Instantiates a date accuracy indicator, the accuracy of the day, month and year are 
        /// passed into this constructor as parameters
        /// </summary>
        /// <param name="dayAccurate">A boolean indicating if the day is accurate</param>
        /// <param name="monthAccurate">A boolean indicating if the month is accurate</param>
        /// <param name="yearAccurate">A boolean indicating if the year is accurate</param>
        public DateAccuracyIndicator(Boolean? dayAccurate, Boolean? monthAccurate, Boolean? yearAccurate)
        {
            DayAccurate = dayAccurate;
            MonthAccurate = monthAccurate;
            YearAccurate = yearAccurate;
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Converts and instance of this class into a DateAccuracy Enum.
        /// </summary>
        /// <returns></returns>
        public Enum.DateAccuracy ConvertToEnum()
        {
            String accuracyAsString;

            if(DayAccurate != null)
            {
                accuracyAsString = DayAccurate.Value ? "A" : "E";
            }
            else
            {
                accuracyAsString = "U";
            }

            if (MonthAccurate != null)
            {
                accuracyAsString += MonthAccurate.Value ? "A" : "E";
            }
            else
            {
                accuracyAsString += "U";
            }

            if (YearAccurate != null)
            {
                accuracyAsString += YearAccurate.Value ? "A" : "E";
            }
            else
            {
                accuracyAsString += "U";
            }

            return (Enum.DateAccuracy)System.Enum.Parse(typeof(Enum.DateAccuracy), accuracyAsString);
        }
        #endregion Methods

        #region Validation
        /// <summary>
        /// Validates this date accuracy indicator
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {

        }
        #endregion
    }
}
