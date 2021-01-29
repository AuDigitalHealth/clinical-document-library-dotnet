using System;

namespace DigitalHealth.HL7.Common.Exceptions
{
    public class HL7MessageInfoException : Exception
    {
        public HL7MessageInfoException(string textMessage)
            : base(textMessage)
        {
        }
    }
}