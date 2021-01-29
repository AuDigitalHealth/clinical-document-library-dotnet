using System;

namespace DigitalHealth.HL7.Common.Exceptions
{
    public class HL7MessageErrorException : Exception
    {
        public HL7MessageErrorException(string textMessage)
            : base(textMessage)
        {
        }
    }
}