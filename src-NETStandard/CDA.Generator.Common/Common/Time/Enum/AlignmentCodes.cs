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
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.Common.Time.Enum
{
    /// <summary>
    /// Alignment Codes
    /// </summary>
    public enum AlignmentCodes
    {
      /// <summary>
      /// Year
      /// </summary>
      [Name(Code = "CY", Name = "year")]
      Year,

      /// <summary>
      /// Form the set-difference.
      /// </summary>
      [Name(Code = "MY", Name = "month of the year")]
      MonthOfTheYear,

      /// <summary>
      /// Month (continuous)
      /// </summary>
      [Name(Code = "CM", Name = "month (continuous)")]
      MonthContinuous,

      /// <summary>
      /// Week (continuous)
      /// </summary>
      [Name(Code = "CW", Name = "week (continuous)")]
      WeekContinuous,

      /// <summary>
      /// Week of the year
      /// </summary>
      [Name(Code = "WY", Name = "week of the year")]
      WeekOfTheYear,

      /// <summary>
      /// Day of the month
      /// </summary>
      [Name(Code = "DM", Name = "day of the month")]
      DayOfTheMonth,

      /// <summary>
      /// Day (continuous)
      /// </summary>
      [Name(Code = "CD", Name = "day (continuous)")]
      DayContinuous,

      /// <summary>
      /// Day of the year
      /// </summary>
      [Name(Code = "DY", Name = "day of the year")]
      DayOfTheYear,

      /// <summary>
      /// Day of the week (begins with Monday
      /// </summary>
      [Name(Code = "DW", Name = "day of the week (begins with Monday)")]
      DayOfTheWeekBeginsWithMonday,

      /// <summary>
      /// Hour of the day
      /// </summary>
      [Name(Code = "HD", Name = "hour of the day")]
      HourOfTheDay,

      /// <summary>
      /// Hour (continuous)
      /// </summary>
      [Name(Code = "DW", Name = "hour (continuous)")]
      HourContinuous,

      /// <summary>
      /// Minute of the hour
      /// </summary>
      [Name(Code = "NH", Name = "minute of the hour")]
      MinuteOfTheHour,

      /// <summary>
      /// Minute (continuous)
      /// </summary>
      [Name(Code = "CN", Name = "minute (continuous)")]
      MinuteContinuous,

      /// <summary>
      /// Second of the minute
      /// </summary>
      [Name(Code = "SN", Name = "second of the minute")]
      SecondOfTheMinute,

      /// <summary>
      /// Second (continuous)
      /// </summary>
      [Name(Code = "CS", Name = "second (continuous)")]
      SecondContinuous
     }
}
