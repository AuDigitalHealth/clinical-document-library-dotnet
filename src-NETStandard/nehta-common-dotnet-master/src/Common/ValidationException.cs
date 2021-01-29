using System;
using System.Collections.Generic;
using System.Text;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// This class extends the generic application exception and provides a list of validation messages
    /// 
    /// The validation messages are populated by the validation builder; which eventually throws this 
    /// validation exception with a full list of validation messages.
    /// </summary>
    public class ValidationException : ApplicationException
    {
        ///<summary>
        /// the default error message that is associated with this validation exception
        ///</summary>
        public const String DefaultErrorMessage = "Validation Exception: Please cast this exception back to a ValidationException and call the GetMessagesString method to view the validation errors";
        
        /// <summary>
        /// The list of validation messages associated with this exception.
        /// </summary>
        public List<ValidationMessage> Messages;

        private ValidationException()
        {
        }

        /// <summary>
        /// This constructor takes in a list of validation messages
        /// </summary>
        /// <param name="messages"></param>
        public ValidationException(List<ValidationMessage> messages)
            : this(messages, DefaultErrorMessage)
        {
        }

        /// <summary>
        /// This creates a validation exception for a single error message.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <param name="message">The error message.</param>
        public ValidationException(string name, string value, string message)
            : base(DefaultErrorMessage)
        {
            this.Messages = new List<ValidationMessage>();

            this.Messages.Add(new ValidationMessage()
            {
                Name = name,
                Value = value,
                Message = message
            });
        }

        /// <summary>
        /// This contstructor takes in a list of validation messages
        /// </summary>
        /// <param name="messages">A list of validation messages</param>
        /// <param name="errorMessage">The errorMessage</param>
        public ValidationException(List<ValidationMessage> messages, String errorMessage)
            : base(errorMessage)
        {
            Messages = messages;
        }

        /// <summary>
        /// Returns the list of validation messages as a String.
        /// </summary>
        /// <returns></returns>
        public string GetMessagesString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var message in Messages)
            {
                if (message.Name != null)
                    stringBuilder.Append(string.Format("{0} - {1}\n", message.Name, message.Message));
                else
                    stringBuilder.Append(message.Message);
            }

            return stringBuilder.ToString();
        }
    }
}
