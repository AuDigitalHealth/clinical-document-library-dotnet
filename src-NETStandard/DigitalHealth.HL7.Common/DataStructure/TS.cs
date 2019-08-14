using System;
using DigitalHealth.HL7.Common.Exceptions;

namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Time Stamp. Contains the exact time of an event, including the date and time.
    /// </summary>
    public class TS
    {
        /// <summary>
        /// Contains the exact time of an event, including the date and time. The date
        /// portion of a time stamp follows the rules of a date field and the time portion
        /// follows the rules of a time field. The specific data representations used in the
        /// HL7 encoding rules are compatible with ISO 8824-1987(E).
        /// </summary>
        public string timeofanevent;

        /// <summary>
        /// In prior versions of HL7, an optional second component indicates the
        /// degree of precision of the time stamp (Y = year, L = month, D = day, H =
        /// hour, M = minute, S = second). This optional second component is retained
        /// only for purposes of backward compatibility.
        /// </summary>
        public string degreeofprecision;

        /// <summary>
        /// Property to access the DateTime of this timestamp.
        /// Setting this property will assume a precision to the second and include a time zone offset.
        /// To use a custom precision, call the SetDateTime method.
        /// </summary>
        public DateTime? TimestampValue
        {
            get
            {
                try
                {
                    return HL7DateTime.Parse(timeofanevent);
                }
                catch (Exception ex)
                {
                    throw new HL7MessageErrorException(ex.Message);
                }
            }
            set
            {
                timeofanevent = HL7DateTime.ToString(value);
            }
        }

        /// <summary>
        /// Sets the date of this timestamp with the specified precision.
        /// </summary>
        /// <param name="dateTime">Date/time value or null</param>
        /// <param name="precision">Precision format (year, month, day, hour, minute, 1 second, 100 milliseconds, 10 milliseconds, 1 milliseconds or 100 nanoseconds, with or without time zone)</param>
        public void SetDateTime(DateTime? dateTime, HL7DateTimePrecision precision)
        {
            timeofanevent = HL7DateTime.ToString(dateTime, precision);

            // the precision is indicated by limiting the number of digits used, not using degreeofprecision.
            degreeofprecision = null;
        }
    }
}