using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// This class builds a list of validation messages and contains the generic business logic for 
    /// validating objects.
    /// </summary>
    public class ValidationBuilder
    {
        #region Properties
        private string _path;

        /// <summary>
        /// The path to the property that is currently being validated
        /// </summary>
        public string Path {
            get
            {
                return _path;
            }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _path = value + ".";
                }
            }
        }

        /// <summary>
        /// The path to the property that is currently being validated without the trailing dot
        /// </summary>
        public string PathName
        {
            get
            {
                if (string.IsNullOrEmpty(_path))
                    return string.Empty;

                if (_path.LastIndexOf(".") > 0)
                {
                    return _path.Substring(0, _path.LastIndexOf("."));
                }
                
                return string.Empty;
            }
        }
        
        /// <summary>
        /// The list of validation messages
        /// </summary>
        public List<ValidationMessage> Messages { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// A generic constructor; where no validation messages, or path is passed in 
        /// 
        /// A new internal list of validation messages is instantiated as part of this constructor
        /// </summary>
        public ValidationBuilder()
        {
            Messages = new List<ValidationMessage>();
        }

        /// <summary>
        /// A generic constructor with no validation messages passed in; however the path is specified
        /// 
        /// A new internal list of validation messages is instantiated as part of this constructor
        /// </summary>
        /// <param name="path">The path to the property that is being validated</param>
        public ValidationBuilder(string path)
        {
            Messages = new List<ValidationMessage>();
            Path = path;
        }

        /// <summary>
        /// A generic constructor were both the path and the list of validation messages are passed in.
        /// </summary>
        /// <param name="messages">The list of validation messages to date</param>
        /// <param name="path">The path to the property that is being validated</param>
        public ValidationBuilder(string path, List<ValidationMessage> messages)
        {
            if (messages != null)
            {
                Messages = messages;
            }
            else
            {
                Messages = new List<ValidationMessage>();
            }

            Path = path;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method adds a validation message to the list of validation messages
        /// </summary>
        /// <param name="name">The name of the property that is being validated</param>
        /// <param name="value">The current value of the property that is being validated</param>
        /// <param name="message">The validation message</param>
        public void AddValidationMessage(string name, string value, string message)
        {
            Messages.Add
                (new ValidationMessage
                    {
                        Name = name,
                        Value = value,
                        Message = message
                    }
                );
        }

        /// <summary>
        /// This validation method performs a range check on a collection of objects.
        /// </summary>
        /// <param name="name">The name of the property that is being validates</param>
        /// <param name="value">The value of the property (E.g. the collection)</param>
        /// <param name="min">The acceptable minimum value</param>
        /// <param name="max">The acceptable maximum value</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool RangeCheck(string name, ICollection value, int min, int max)
        {
          if (value == null) return false;

            if (value.Count < min || value.Count > max)
            {
                Messages.Add
                    (new ValidationMessage
                        {
                            Name = Path + name,
                            Value = null,
                            Message = string.Format("Number of items must be between {0} to {1} (inclusive)", min, max)
                        }
                    );

                return false;
            }
            
            return true;
        }

        /// <summary>
        /// This validation method performs a check on two objects ensuring they do not match
        /// </summary>
        /// <typeparam name="T">The type of objects we are comparing</typeparam>
        /// <param name="name">The name of the property that is being validates</param>
        /// <param name="value">The value of the property (E.g. the collection)</param>
        /// <param name="expected">The value that is not expected</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool NoMatchCheck<T>(string name, T value, T expected)
        {
            if (expected == null && value == null)
                return true;

            if (expected.Equals(value))
            {
                Messages.Add
                    (new ValidationMessage
                        {
                            Name = Path + name,
                            Value = value,
                            Message = string.Format("Value must not be: {0}", expected)
                        }
                    );

                return false;
            }

            return true;
        }

        /// <summary>
        /// This validation method performs a match check on two objects
        /// </summary>
        /// <typeparam name="T">The type of objects we are comparing</typeparam>
        /// <param name="name">The name of the property that is being validates</param>
        /// <param name="value">The value of the property (E.g. the collection)</param>
        /// <param name="expected">The value that is expected</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool MatchCheck<T>(string name, T value, T expected)
        {
            if (!expected.Equals(value))
            {
                Messages.Add
                (new ValidationMessage
                        {
                            Name = Path + name,
                            Value = value,
                            Message = string.Format("Expected: {1} - Value: {0}", value, expected)
                        }
                );

                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Validates an HPIO number.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool HpioCheck(string name, string value, bool required)
        {
            if (required && !ArgumentRequiredCheck(name, value))
                return false;

            if (!Regex.IsMatch(value, @"^800362\d{10}$"))
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = value,
                        Message = "Argument must be a valid HPIO number"
                    });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates an HPIO number (including OID).
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool HpioOidCheck(string name, string value, bool required)
        {
             if (required && !ArgumentRequiredCheck(name, value))
                return false;

             if (!Regex.IsMatch(value, @"^(\d+\.)+800362\d{10}$"))
             {
                 Messages.Add(
                     new ValidationMessage()
                     {
                         Name = Path + name,
                         Value = value,
                         Message = "Argument must be an OID qualified HPIO number"
                     });
                 return false;
             }

             return true;
        }

        /// <summary>
        /// Validates an HPII number.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool HpiiCheck(string name, string value, bool required)
        {
            if (required && !ArgumentRequiredCheck(name, value))
                return false;

            if (!Regex.IsMatch(value, @"^800361\d{10}$"))
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = value,
                        Message = "Argument must be a valid HPII number"
                    });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates an IHI number.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool IhiCheck(string name, string value, bool required)
        {
            if (required && !ArgumentRequiredCheck(name, value))
                return false;

            if (!Regex.IsMatch(value, @"^800360\d{10}$"))
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = value,
                        Message = "Argument must be a valid IHI number"
                    });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a UUID value.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool UuidCheck(string name, UniqueId value, bool required)
        {
            if (required && !ArgumentRequiredCheck(name, value))
                return false;

            if (!Regex.IsMatch(value.ToString(), @"^(urn:uuid:)?[A-Za-z0-9]{8}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{12}$"))
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = value,
                        Message = "Argument must be a valid UUID in the format of xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
                    });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates an OID value.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool OidCheck(string name, string value, bool required)
        {
            if (required && !ArgumentRequiredCheck(name, value))
                return false;

            if (!required && value == null)
                return true;

            if (!Regex.IsMatch(value.ToString(), @"^\d+(\.\d+)+$"))
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = value,
                        Message = "Argument must be a valid OID"
                    });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a fully qualified identifier
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter to validate.</param>
        /// <param name="required">Indicates if the parameter is required.</param>
        /// <returns>A boolean indicating if the validation passed.</returns>
        public bool OidQualifiedIdCheck(string name, string value, bool required)
        {
            if (required && !ArgumentRequiredCheck(name, value))
                return false;

            if (!Regex.IsMatch(value.ToString(), @"^\d+(\.\d+)+$"))
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = value,
                        Message = "Argument must be a fully qualified identifier"
                    });
                return false;
            }

            return true;
        }

        /// <summary>
        /// This validation method performs a check to ensure that the collection passed in contains some items
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The collection to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck(string name, ICollection value)
        {
            if (value == null || value.Count == 0)
            {
                Messages.Add
                    (new ValidationMessage
                        {
                            Name = Path + name,
                            Value = value,
                            Message = "Argument is required"
                        }
                    );

                return false;
            }
            
            return true;
        }

        /// <summary>
        /// This validation method performs a check to ensure that the DateTime value is set
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="date">The DateTime value to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck(string name, DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                Messages.Add(
                    new ValidationMessage()
                    {
                        Name = Path + name,
                        Value = date.ToString(),
                        Message = "Argument is required"
                    }
                    );

                return false;
            }

            return true;
        }

        /// <summary>
        /// This validation method performs a check to ensure that the object passed in is not null or empty in the 
        /// case of a string
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The collection to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck(string name, object value)
        {
            return ArgumentRequiredCheck(name, value, "Argument is required");
        }

        /// <summary>
        /// This validation method performs a check to ensure that the object passed in is not null or empty in the 
        /// case of a string
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The collection to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck(string name, string value)
        {
            return ArgumentRequiredCheck(name, value, "Argument is required");
        }

        /// <summary>
        /// This validation method performs a check to ensure that the object passed in is not null or empty in the 
        /// case of a string
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The collection to check</param>
        /// <param name="message">The validation message to display</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck(string name, object value, string message)
        {
            if (value == null || (value is string && ((string)value).Trim() == string.Empty))
            {
                Messages.Add
                    (
                        new ValidationMessage
                        {
                            Name = Path + name,
                            Value = value,
                            Message = message
                        }
                    );

                return false;
            }

            return true;
        }

        /// <summary>
        /// This validation method performs a check to ensure that the enum passed in does not have a value
        /// of undefined
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The collection to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck(string name, Enum value)
        {
            if (value.ToString().ToUpper() == "UNDEFINED")
            {
                Messages.Add(new ValidationMessage()
                {
                    Name = Path + name,
                    Value = value,
                    Message = name + " must not be set to Undefined"
                });

                return false;
            }

            return true;
        }

        /// <summary>
        /// This validation method performs a check to ensure that the object passed in is not null
        /// </summary>
        /// <typeparam name="T">The type of object we are validating</typeparam>
        /// <param name="name">The property name</param>
        /// <param name="value">The collection to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ArgumentRequiredCheck<T>(string name, T? value)
            where T : struct
        {
            if (!value.HasValue)
            {
                Messages.Add
                    (new ValidationMessage
                        {
                            Name = Path + name,
                            Value = value,
                            Message = "Argument is required"
                        }
                    );

                return false;
            }

            return true;
        }

        /// <summary>
        /// This validation method performs a check to ensure that only one of the items within the 
        /// passed in key value pair is not null or empty in the case of a string
        /// </summary>
        /// <param name="nameValues">The key value pair of items to check</param>
        /// <returns>A boolean indicating if the validation passed</returns>
        public bool ChoiceCheck(Dictionary<string, object> nameValues)
        {
            int chosenCount = 0;
            string namesList = String.Empty;

            foreach (KeyValuePair<string, object> pair in nameValues)
            {
                if (pair.Value is string && ((string)pair.Value).Trim() != string.Empty)
                {
                    chosenCount++;
                }
                else if (!(pair.Value is string) && pair.Value != null)
                {
                    chosenCount++;
                }

                namesList += ", " + Path + pair.Key;
            }

            if (chosenCount != 1)
            {
                Messages.Add
                    (new ValidationMessage
                        {
                            Name = namesList.Substring(2),
                            Value = null,
                            Message = "One (and only one) of these must be specified"
                        }
                    );

                return false;
            }

            return true;
        }
        #endregion
    }

}
