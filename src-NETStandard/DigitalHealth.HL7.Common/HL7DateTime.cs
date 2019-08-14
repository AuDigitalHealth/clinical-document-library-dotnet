using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DigitalHealth.HL7.Common
{
    /// <summary>
    /// An enumeration containing all 20 permissible HL7 TS formats.
    ///
    /// Each has a custom attribute containing the closest approximation possible within
    /// .NET Custom Date and Time Format Strings. The difference is an extraneous colon
    /// between the hour and minute in the timezone offset, which must be removed or
    /// added before use of the format string. HL7 does not have a colon, but .NET does.
    /// </summary>
    public enum HL7DateTimePrecision
    {
        [HL7DateTimeFormat("yyyyMMddHHmmss.ffffzzz")]
        TEN_THOUSANDTHS_OF_A_SECOND_TZ,

        [HL7DateTimeFormat("yyyyMMddHHmmss.fffzzz")]
        THOUSANDTHS_OF_A_SECOND_TZ,

        [HL7DateTimeFormat("yyyyMMddHHmmss.ffzzz")]
        HUNDREDTHS_OF_A_SECOND_TZ,

        [HL7DateTimeFormat("yyyyMMddHHmmss.fzzz")]
        TENTHS_OF_A_SECOND_TZ,

        [HL7DateTimeFormat("yyyyMMddHHmmsszzz")]
        SECOND_TZ,

        [HL7DateTimeFormat("yyyyMMddHHmmzzz")]
        MINUTE_TZ,

        [HL7DateTimeFormat("yyyyMMddHHzzz")]
        HOUR_TZ,

        [HL7DateTimeFormat("yyyyMMddzzz")]
        DAY_TZ,

        [HL7DateTimeFormat("yyyyMMzzz")]
        MONTH_TZ,

        [HL7DateTimeFormat("yyyyzzz")]
        YEAR_TZ,

        [HL7DateTimeFormat("yyyyMMddHHmmss.ffff")]
        TEN_THOUSANDTHS_OF_A_SECOND,

        [HL7DateTimeFormat("yyyyMMddHHmmss.fff")]
        THOUSANDTHS_OF_A_SECOND,

        [HL7DateTimeFormat("yyyyMMddHHmmss.ff")]
        HUNDREDTHS_OF_A_SECOND,

        [HL7DateTimeFormat("yyyyMMddHHmmss.f")]
        TENTHS_OF_A_SECOND,

        [HL7DateTimeFormat("yyyyMMddHHmmss")]
        SECOND,

        [HL7DateTimeFormat("yyyyMMddHHmm")]
        MINUTE,

        [HL7DateTimeFormat("yyyyMMddHH")]
        HOUR,

        [HL7DateTimeFormat("yyyyMMdd")]
        DAY,

        [HL7DateTimeFormat("yyyyMM")]
        MONTH,

        [HL7DateTimeFormat("yyyy")]
        YEAR
    }

    public static class Extensions
    {
        /// <summary>
        /// Extension Methods for retrieving a value from an attribute
        /// </summary>
        /// <typeparam HL7Name="T">The object / attribute identifier</typeparam>
        /// <typeparam HL7Name="TExpected">The expected return value identifier</typeparam>
        /// <param HL7Name="enumeration">The enum identifier that this method extends</param>
        /// <param HL7Name="expression">An expression specifying the property on the attribute you would like to retrun</param>
        /// <returns>The value as specified by the expression parameter</returns>
        public static TExpected GetAttributeValue<T, TExpected>
            (
                this Enum enumeration,
                Func<T, TExpected> expression
            )
            where T : Attribute
        {
            var attribute = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

            return attribute == null ? default(TExpected) : expression(attribute);
        }
    }

    public class HL7DateTime
    {
        /// <summary>
        /// Parses an HL7 timestamp into a DateTime.
        /// </summary>
        /// <param name="timestamp">An HL7 timestamp string.</param>
        /// <returns>A DateTime value.</returns>
        public static DateTime? Parse(string timestamp)
        {
            if (timestamp == null || timestamp.Length == 0)
            {
                return null;
            }

            try
            {
                timestamp = FixBrokenTimestamp(timestamp);

                // Add a colon between hours and minutes of the timezone offset.
                // This is necessary to use the "zzz" format specifier.
                string formatted = Regex.Replace(timestamp, @"(.+[+-]..)(..)", @"$1:$2");

                return DateTime.ParseExact(formatted, HL7DateTimeFormat.AllFormats, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None);
            }
            catch (FormatException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Converts a DateTime to an HL7 timestamp string, assuming a precision to the second and including a time zone offset.
        /// </summary>
        /// <param name="value">The DateTime value.</param>
        /// <returns>An HL7 timestamp string.</returns>
        public static string ToString(DateTime? value)
        {
            return ToString(value, HL7DateTimePrecision.SECOND_TZ);
        }

        /// <summary>
        /// Converts a DateTime to an HL7 timestamp string, using the given precision.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <param name="precision">The desired precision and whether to include the time zone offset.</param>
        /// <returns>An HL7 timestamp string.</returns>
        public static string ToString(DateTime? value, HL7DateTimePrecision precision)
        {
            if (value.HasValue)
            {
                string format = precision.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format);
                return value.Value.ToString(format).Replace(":", "");
            }
            return string.Empty;
        }

        /// <summary>
        /// Applies fixes to non-standard timestamp representations that are found in legacy
        /// systems
        /// </summary>
        /// <param name="timestamp">An HL7 timestamp string.</param>
        /// <returns>Fixed timestamp string.</returns>
        private static string FixBrokenTimestamp(string timestamp)
        {
            if (timestamp.Length >= 6 && timestamp.Substring(4, 2) == "00")
            {
                // The month was "00" so the precision is one year.
                timestamp = timestamp.Substring(0, 4);
            }
            else if (timestamp.Length >= 8 && timestamp.Substring(6, 2) == "00")
            {
                // The day was "00" so the precision is one month.
                timestamp = timestamp.Substring(0, 6);
            }
            else if (timestamp.Length >= 10 && timestamp.Substring(8, 2) == "24")
            {
                // The hour was "24" so the date must be incremented by one day and the hour set to "00".
                DateTime date = DateTime.ParseExact(timestamp.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None);
                string remainder = timestamp.Substring(10);
                timestamp = string.Format("{0:yyyyMMdd}00{1}", date.AddDays(1), remainder);
            }
            return timestamp;
        }
    }

    public class HL7DateTimeFormat : Attribute
    {
        /// <summary>
        /// An array containing Custom Date and Time Format Strings for all 20 permissible HL7 TS formats.
        /// Those with timezone first, and ordered from most precise to least precise.
        /// </summary>
        public static readonly string[] AllFormats =
        {
            HL7DateTimePrecision.TEN_THOUSANDTHS_OF_A_SECOND_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.THOUSANDTHS_OF_A_SECOND_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.HUNDREDTHS_OF_A_SECOND_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.TENTHS_OF_A_SECOND_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.SECOND_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.MINUTE_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.HOUR_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.DAY_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.MONTH_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.YEAR_TZ.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.TEN_THOUSANDTHS_OF_A_SECOND.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.THOUSANDTHS_OF_A_SECOND.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.HUNDREDTHS_OF_A_SECOND.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.TENTHS_OF_A_SECOND.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.SECOND.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.MINUTE.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.HOUR.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.DAY.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.MONTH.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format),
            HL7DateTimePrecision.YEAR.GetAttributeValue<HL7DateTimeFormat, string>(a => a.Format)
        };

        private string formatField;

        public HL7DateTimeFormat(string format)
        {
            this.formatField = format;
        }

        public string Format
        {
            get
            {
                return formatField;
            }
            set
            {
                formatField = value;
            }
        }
    }
}