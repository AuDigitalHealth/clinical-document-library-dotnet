using System;
using System.Runtime.Serialization;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    [Serializable]
    public class MessageValidationException : Exception
    {
        public MessageValidationException()
        {
        }

        public MessageValidationException(string message) : base(message)
        {
        }

        public MessageValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MessageValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }}