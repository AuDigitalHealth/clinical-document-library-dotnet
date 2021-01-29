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
using CDA.Generator.Common.Common.Time;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a StructuredTiming oject
    /// </summary>
    [Serializable]
    [DataContract]
    public class StructuredTiming
    {
        #region Properties

        /// <summary>
        /// EffectiveTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ITime> EffectiveTime { get; set; }

        /// <summary>
        /// EffectiveTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string NarrativeText { get; set; }

        #region UnspecifiedTypes

        ///// <summary>
        ///// Intervention Frequency Range
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public ISO8601DateTime InterventionFrequencyRange { get; set; }

        ///// <summary>
        ///// Intervention Interval Range
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public List<ISO8601DateTime> InterventionIntervalRange { get; set; }

        ///// <summary>
        ///// Intervention Time
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public List<ISO8601DateTime> InterventionTime { get; set; }

        ///// <summary>
        ///// Intervention Day of Week
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public List<ISO8601DateTime> InterventionDayOfWeek { get; set; }

        ///// <summary>
        ///// Intervention Day of Month
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public List<ISO8601DateTime> InterventionDayOfMonth { get; set; }

        ///// <summary>
        ///// Intervention Date
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public List<ISO8601DateTime> InterventionDate { get; set; }
        #endregion

        #endregion

        #region Constructors
        internal StructuredTiming()
        {
        }
        #endregion

        /// <summary>
        /// Validates this StructuredTiming
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var timeList = new List<SXCM_TS>();
          if (EffectiveTime != null)
            foreach (var time in EffectiveTime)
            {
              if (time != null)
                switch (time.GetType().Name)
                {
                  case "EventRelatedIntervalOfTime":
                    var eventRelatedIntervalOfTime = time as EventRelatedIntervalOfTime;
                    if (eventRelatedIntervalOfTime != null) eventRelatedIntervalOfTime.Validate(path + ".EventRelatedIntervalOfTime", messages);
                    break;
                  case "CdaInterval":
                    var cdaInterval = time as CdaInterval;
                    if (cdaInterval != null) cdaInterval.Validate(path,messages);
                    break;
                  case "ParentheticSetExpressionOfTime":
                    var parentheticSetExpressionOfTime = time as ParentheticSetExpressionOfTime;
                    if (parentheticSetExpressionOfTime != null) parentheticSetExpressionOfTime.Validate(path + ".ParentheticSetExpressionOfTime", messages);
                    break;
                  case "PeriodicIntervalOfTime":
                    var periodicIntervalOfTime = time as PeriodicIntervalOfTime;
                    if (periodicIntervalOfTime != null) periodicIntervalOfTime.Validate(path + ".PeriodicIntervalOfTime", messages);
                    break;
                  case "SetComponentTS":
                    var setComponentTs = time as SetComponentTS;
                    if (setComponentTs != null) setComponentTs.Validate(path + ".SetComponentTS", messages);
                    break;
                  case "ISO8601DateTime":
                    // Validated on Setup
                    break;
                }

            }
        }
    }
}