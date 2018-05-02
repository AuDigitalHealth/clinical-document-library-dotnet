/*
 * Copyright 2013 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or imp201112131459lied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// Formats ISO 8601 date/times for use within a narrative.
    /// </summary>
    public static class ISO8601DateTimeFormatter
    {
        /// <summary>
        /// Formats an ISO 8601 date/time for use within a narrative.
        /// </summary>
        /// <param name="dateTime">Date/time to format.</param>
        /// <returns>Formatted string.</returns>
        public static string Format(ISO8601DateTime dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            // Get the date format based on the precision
            string format = "dd MMM yyyy HH:mm";
            switch (dateTime.PrecisionIndicator)
            {
                case ISO8601DateTime.Precision.Year:
                    format = "yyyy";
                    break;
                case ISO8601DateTime.Precision.Month:
                    format = "MMM yyyy";
                    break;
                case ISO8601DateTime.Precision.Day:
                    format = "dd MMM yyyy";
                    break;
                case ISO8601DateTime.Precision.Hour:
                    format = "dd MMM yyyy HH";
                    break;
                case ISO8601DateTime.Precision.Minute:
                    format = "dd MMM yyyy HH:mm";
                    break;
                case ISO8601DateTime.Precision.Second:
                    format = "dd MMM yyyy HH:mm:ss";
                    break;
                case ISO8601DateTime.Precision.Millisecond: 
                    format = "dd MMM yyyy HH:mm:ss.ffff";
                    break;
            }

            // Format the date
            var output = dateTime.DateTime.ToString(format);

            // Add the time zone if it has been specified
            if (dateTime.TimeZone.HasValue)
            {
                output += " ";
                output += string.Format("{0}{1:00}{2:00}",
                            dateTime.TimeZone.Value.TotalMinutes >= 0 ? "+" : "-",
                            Math.Abs(dateTime.TimeZone.Value.Hours),
                            Math.Abs(dateTime.TimeZone.Value.Minutes));
            }

            return output;            
        }
    }
}
