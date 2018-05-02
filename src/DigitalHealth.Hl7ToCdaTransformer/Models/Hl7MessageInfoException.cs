using System;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    public class Hl7MessageInfoException : Exception
    {
        public Hl7MessageInfoException(string textMessage)
            : base(textMessage)
        {
        }
    }
}