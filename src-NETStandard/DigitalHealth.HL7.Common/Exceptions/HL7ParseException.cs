using System;

namespace DigitalHealth.HL7.Common.Exceptions
{
    /// <summary>
    /// This exception is thrown when HIPS fails to parse a received HL7 message.
    /// </summary>
    public class HL7ParseException : Exception
    {
        public HL7ParseException(string textMessage)
            : base(textMessage)
        {
        }
    }
}