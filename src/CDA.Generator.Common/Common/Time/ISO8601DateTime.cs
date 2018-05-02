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
using System.Runtime.Serialization;
using CDA.Generator.Common.Common.Time;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// Specify a DateTime which will be formatted in ISO8601 compliant string.
    /// Eg: To express "5 Jan 2009, 5:25PM (UTC-04:30)"
    /// var time = new ISO8601DateTime(DateTime.Parse(
    ///     "5 Jan 2009 5:25 PM"),
    ///     Precision.Minute,
    ///     -new TimeSpan(4, 30, 0));
    /// </summary>
    [Serializable]
    [DataContract]
    public class ISO8601DateTime : ITime 
    {
        /// <summary>
        /// Precision of the date/time.
        /// </summary>
        public enum Precision
        {
            /// <summary>
            /// Year e.g. 1992
            /// </summary>
            Year,

            /// <summary>
            /// Month e.g. 199204
            /// </summary>
            Month,

            /// <summary>
            /// Day e.g. 19940402
            /// </summary>
            Day,

            /// <summary>
            /// Hour e.g. 1992040214
            /// </summary>
            Hour,

            /// <summary>
            /// Minute e.g. 199204021420
            /// </summary>
            Minute,

            /// <summary>
            /// Second e.g. 19920402142030
            /// </summary>
            Second,

            /// <summary>
            /// Millisecond e.g. 19920402142030.1244
            /// </summary>
            Millisecond
        }

        /// <summary>
        /// Date/time value.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Time zone of the date/time.
        /// </summary>
        public TimeSpan? TimeZone { get; set; }

        /// <summary>
        /// Defines the precision of the date/time. This determines how it is string field into CDA.
        /// </summary>
        public Precision? PrecisionIndicator { get; set; }

        /// <summary>
        /// Creates with date/time.
        /// NOTE: Any time that is more specific than a day SHALL include a time zone, this is defaulted to the local system time.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        public ISO8601DateTime(DateTime dateTime)
        {
            ISO8601DateTimeInitialise(dateTime, Precision.Second, null);
        }

        /// <summary>
        /// Creates with date/time and precision.
        /// NOTE: Any time that is more specific than a day SHALL include a time zone, this is defaulted to the local system time.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        /// <param name="precisionIndicator">Precision indicator.</param>
        public ISO8601DateTime(DateTime dateTime, Precision precisionIndicator)
        {
            ISO8601DateTimeInitialise(dateTime, precisionIndicator, null);
        }

        /// <summary>
        /// Creates with date/time, precision and time zone.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        /// <param name="precisionIndicator">Precision indicator.</param>
        /// <param name="timeZone">Time zone.</param>
        public ISO8601DateTime(DateTime dateTime, Precision precisionIndicator, TimeSpan timeZone)
        {
            ISO8601DateTimeInitialise(dateTime, precisionIndicator, timeZone);
        }

        /// <summary>
        /// Creates with date/time, precision and time zone.
        /// NOTE: Any time that is more specific than a day SHALL include a timezone, this is defaulted to the local system time.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        /// <param name="precisionIndicator">Precision indicator.</param>
        /// <param name="timeZone">Time zone </param>
        internal void ISO8601DateTimeInitialise(DateTime? dateTime, Precision? precisionIndicator, TimeSpan? timeZone)
        {
            if (dateTime.HasValue)
                this.DateTime = dateTime.Value;

            if (precisionIndicator.HasValue)
                this.PrecisionIndicator = precisionIndicator.Value;

            switch (PrecisionIndicator)
            {
                case Precision.Year:
                    break;
                case Precision.Month:
                    break;
                case Precision.Day:
                    break;
                default:
                    //Fixed up timezone when daylight savings
                    //this.TimeZone = timeZone.HasValue ? timeZone.Value : TimeZoneInfo.Local.BaseUtcOffset;
                    this.TimeZone = timeZone.HasValue ? timeZone.Value : TimeZoneInfo.Local.GetUtcOffset(DateTime);
                    break;
            }

        }

        /// <summary>
        /// Returns the string representation of the date/time.
        /// </summary>
        /// <returns>String representation of the ISO 8601 date/time.</returns>
        public new string ToString()
        {
            var format = "yyyyMMddHHmmss";
            
            switch (PrecisionIndicator)
            {
                case Precision.Year:
                    format = "yyyy";
                    break;
                case Precision.Month:
                    format = "yyyyMM";
                    break;
                case Precision.Day:
                    format = "yyyyMMdd";
                    break;
                case Precision.Hour:
                    format = "yyyyMMddHH";
                    break;
                case Precision.Minute:
                    format = "yyyyMMddHHmm";
                    break;
                case Precision.Second:
                    format = "yyyyMMddHHmmss";
                    break;
                case Precision.Millisecond:
                    format = "yyyyMMddHHmmss.ffff";
                    break;
            }

            var output = this.DateTime.ToString(format);

            if (TimeZone.HasValue)
            {
                output += string.Format("{0}{1:00}{2:00}",
                            TimeZone.Value.TotalMinutes >= 0 ? "+" : "-",
                            Math.Abs(TimeZone.Value.Hours),
                            Math.Abs(TimeZone.Value.Minutes));
            }

            return output;
        }

        /// <summary>
        /// Formats an ISO 8601 date/time for use within a narrative.
        /// </summary>
        /// <returns>Formatted string.</returns>
        public string NarrativeText()
        {
          // Get the date format based on the precision
          string format = "d MMM yyyy HH:mm";
          switch (PrecisionIndicator)
          {
            case Precision.Year:
              format = "yyyy";
              break;
            case Precision.Month:
              format = "MMM yyyy";
              break;
            case Precision.Day:
              format = "d MMM yyyy";
              break;
            case Precision.Hour:
              format = "d MMM yyyy HH";
              break;
            case Precision.Minute:
              format = "d MMM yyyy HH:mm";
              break;
            case Precision.Second:
              format = "d MMM yyyy HH:mm:ss";
              break;
            case Precision.Millisecond:
              format = "d MMM yyyy HH:mm:ss.ffff";
              break;
          }

          // Format the date
          var output = DateTime.ToString(format);

          // Add the time zone if it has been specified
          if (TimeZone.HasValue)
          {
            output += string.Format("{0}{1:00}{2:00}",
                                    TimeZone.Value.TotalMinutes >= 0 ? "+" : "-",
                                    Math.Abs(TimeZone.Value.Hours),
                                    Math.Abs(TimeZone.Value.Minutes));
          }

          return output;
        }

    }
}