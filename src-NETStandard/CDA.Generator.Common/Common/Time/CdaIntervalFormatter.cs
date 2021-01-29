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
using CDA.Generator.Common.Common.Time.Enum;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// Formats the interval for the narrative.
    /// </summary>
    public static class CdaIntervalFormatter
    {
        /// <summary>
        /// Formats the interval.
        /// </summary>
        /// <param name="cdaInterval">Interval</param>
        /// <returns>Narrative text.</returns>
        public static string Format(CdaInterval cdaInterval)
        {
            if (cdaInterval == null)
            {
                return null;
            }

            if (cdaInterval.ShowOngoingInNarrative.HasValue && cdaInterval.ShowOngoingInNarrative.Value)
            {
                return "(ongoing)";
            }

            if (cdaInterval.Type == IntervalType.Low || cdaInterval.Type == IntervalType.Value)
            {
                // Low
                return cdaInterval.Low.NarrativeText();
            }
            if (cdaInterval.Type == IntervalType.Width)
            {
                // Width
                return Format(cdaInterval.IntervalWidth);
            }
            if (cdaInterval.Type == IntervalType.LowWidth)
            {
                // Low/Width
              return Format(cdaInterval.IntervalWidth) + " after " + cdaInterval.Low.NarrativeText();
            }
            if (cdaInterval.Type == IntervalType.LowHigh)
            {
                // Low/High
                return cdaInterval.Low.NarrativeText() + " -> " + cdaInterval.High.NarrativeText();
            }
            if (cdaInterval.Type == IntervalType.Center)
            {
                // Center
                return cdaInterval.Center.NarrativeText();
            }
            if (cdaInterval.Type == IntervalType.CenterWidth)
            {
                // Center/Width
              return cdaInterval.Center.NarrativeText() + " " + Format(cdaInterval.IntervalWidth) + "";
            }
            if (cdaInterval.Type == IntervalType.High)
            {
                // High
                return cdaInterval.High.NarrativeText();
            }
            if (cdaInterval.Type == IntervalType.HighWidth)
            {
                // High/Width
                return Format(cdaInterval.IntervalWidth) + " before " + cdaInterval.High.NarrativeText();
            }

            return null;
        }

        /// <summary>
        /// This is used for sorting purposes only in the narrative
        /// </summary>
        /// <param name="cdaInterval">Interval</param>
        /// <param name="showOngoingInNarrative">A Boolean of Show Ongoing In Narrative is set</param>
        /// <returns>Narrative text.</returns>
        public static DateTime GetFirstDateTimeOfDurrationForNarrativeSorting(CdaInterval cdaInterval, Boolean? showOngoingInNarrative)
        {
            if (showOngoingInNarrative.HasValue && showOngoingInNarrative.Value)
            {
                if (cdaInterval != null && cdaInterval.Low != null && cdaInterval.High == null)
                 {
                   return cdaInterval.Low.DateTime;
                 } 

                // NOTE: Add -1 day to nest the ongoing items correctly in the sorting order
                return DateTime.MaxValue.AddDays(-1);
            }

            if (cdaInterval != null)
            {
                if (cdaInterval.Type == IntervalType.Low)
                {
                    // Low
                    return cdaInterval.Low.DateTime;
                }
                if (cdaInterval.Type == IntervalType.LowWidth)
                {
                    // Low/Width
                    return cdaInterval.Low.DateTime;
                }
                if (cdaInterval.Type == IntervalType.LowHigh)
                {
                    if (cdaInterval.Low != null)
                    {
                        return cdaInterval.Low.DateTime;
                    }

                    if (cdaInterval.High != null)
                    {
                        return cdaInterval.High.DateTime;
                    }
                }
                if (cdaInterval.Type == IntervalType.Center)
                {
                    // Center
                    return cdaInterval.Center.DateTime;
                }
                if (cdaInterval.Type == IntervalType.CenterWidth)
                {
                    // Center/Width
                    return cdaInterval.Center.DateTime;
                }
                if (cdaInterval.Type == IntervalType.High)
                {
                    // High
                    return cdaInterval.High.DateTime;
                }
                if (cdaInterval.Type == IntervalType.HighWidth)
                {
                    // High/Width
                    return cdaInterval.High.DateTime;
                }
            }

            return DateTime.MaxValue;
        }


        /// <summary>
        /// Formats the interval width for the narrative.
        /// </summary>
        /// <param name="intervalWidth">CdaIntervalWidth</param>
        /// <returns>Narrative text.</returns>
        public static string Format(CdaIntervalWidth intervalWidth)
        {
            if (intervalWidth == null)
            {
                return null;
            }

            return intervalWidth.Value + " " + intervalWidth.Unit.GetAttributeValue<NameAttribute, string>(x => x.Name) + "(s)";
        }
    }
}
