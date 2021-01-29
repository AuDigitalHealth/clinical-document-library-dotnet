/*
 * Copyright 2012 NEHTA
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.VendorLibrary.Common.Properties;
using System.Text.RegularExpressions;
using System.Collections;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// Helper class used for validation.
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Validates that the string argument matches an expected string.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="expected">The expected value of the argument.</param>
        public static void ValidateStringMatch(string name, string value, string expected)
        {
            if (value != expected)
                throw new ArgumentException(string.Format("{0} should match value of '{1}'", name, expected), name);
        }

        /// <summary>
        /// Validates that at least one of multiple arguments is provided. Arguments and names are provided
        /// in a dictionary.
        /// </summary>
        /// <param name="nameValues">A dictionary containing the argument names and values.</param>
        public static void ValidateArgumentAtLeastOneRequired(Dictionary<string, object> nameValues)
        {
            bool isValid = false;
            string namesList = "";

            foreach (KeyValuePair<string, object> pair in nameValues)
            {
                if (pair.Value is string && ((string)pair.Value).Trim() != string.Empty)
                    isValid = true;
                else if (!(pair.Value is string) && pair.Value != null)
                    isValid = true;

                namesList += ", " + pair.Key;
            }

            if (!isValid)
                throw new ArgumentException(Resources.ParameterAtLeastOneRequired, namesList.Substring(2));
        }

        /// <summary>
        /// Validates that the array contains at least one element.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="array">The value of the argument to validate.</param>
        public static void ValidateArgumentRequired(string name, object[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException(Resources.ParameterRequired, name);
        }

        /// <summary>
        /// Validates that the object is not null, and that if it is a string it contains a value
        /// and is not an empty string.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        public static void ValidateArgumentRequired(string name, object value) 
        {
            if (value == null || (value is string && ((string)value).Trim() == string.Empty))
                throw new ArgumentException(Resources.ParameterRequired, name);
        }

        /// <summary>
        /// Validates that a collection object has at least one value in it.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The collection object.</param>
        public static void ValidateArgumentRequired(string name, ICollection value)
        {
            if (value == null || value.Count <= 0)
                throw new ArgumentException(Resources.ParameterAtLeastOneRequired, name);
        }

        /// <summary>
        /// Validates that the object is null or that if it is a string it contains a value
        /// and is an empty string.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        public static void ValidateArgumentNotAllowed(string name, object value)
        {
            if (value != null)
                throw new ArgumentException(Resources.ParameterNotAllowed, name);
        }

        /// <summary>
        /// Validates that the parameter is not specified, as denoted by the "isSpecified" field.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        /// <param name="isSpecified">The [field]isSpecified field.</param>
        public static void ValidateArgumentNotAllowed(string name, bool isSpecified)
        {
            if (isSpecified != false)
                throw new ArgumentException(Resources.ParameterNotAllowed, name);
        }

        /// <summary>
        /// Validates that the string array contains at least one element which is not an empty string.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="values">The value of the argument to validate.</param>
        public static void ValidateArgumentRequired(string name, string[] values)
        {
            if (values == null || values.Length == 0 || string.IsNullOrEmpty(values[0].Trim()))
                throw new ArgumentException(Resources.ParameterRequired, name);
        }

        /// <summary>
        /// Validates that a date time has been set (by checking that it is not the minimum value).
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="date">The DateTime value.</param>
        public static void ValidateDateTime(string name, DateTime date)
        {
            if (date == DateTime.MinValue)
                throw new ArgumentException(Resources.ParameterDateRequired, name);
        }

        /// <summary>
        /// Validates that an integer is within acceptable range.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="lowerBound">The inclusive lower bound to validate against.</param>
        /// <param name="upperBound">The inclusive upper bound to validate against.</param>
        /// <param name="value">The value to be validated.</param>
        public static void ValidateNumberRange(string name, int? lowerBound, int? upperBound, int value)
        {
            if (lowerBound != null && value < lowerBound)
                throw new ArgumentException(string.Format(Resources.ParameterGreaterThan, value), name);
            else if (upperBound != null && value > upperBound)
                throw new ArgumentException(string.Format(Resources.ParameterLessThan, value), name);
        }

        /// <summary>
        /// Validates that the argument is a UUID.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="required">Indicates if the argument is required.</param>
        public static void ValidateUUID(string name, string value, bool required)
        {
            if (required) ValidateArgumentRequired(name, value);

            if (!value.StartsWith("urn:uuid:"))
                throw new ArgumentException(Resources.ParameterInvalidUUID, name);
        }

        /// <summary>
        /// Validates that an argument is a valid qualified HPII.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="required">Indicates if the argument is required.</param>
        public static void ValidateHPII(string name, string value, bool required)
        {
            if (required) ValidateArgumentRequired(name, value);

            if (!value.StartsWith(HIQualifiers.HPIIQualifier))
                throw new ArgumentException(string.Format(Resources.ParameterInvalidHPII, HIQualifiers.HPIIQualifier), name);
        }

        /// <summary>
        /// Validates that an argument is a valid qualified HPIO.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="required">Indicates if the argument is required.</param>
        public static void ValidateHPIO(string name, string value, bool required)
        {
            if (required) ValidateArgumentRequired(name, value);

            if (!value.StartsWith(HIQualifiers.HPIOQualifier))
                throw new ArgumentException(string.Format(Resources.ParameterInvalidHPIO, HIQualifiers.HPIOQualifier), name);
        }

        /// <summary>
        /// Validates that an argument is a valid qualified IHI.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="required">Indicates if the argument is required.</param>
        public static void ValidateIHI(string name, string value, bool required)
        {
            if (required) ValidateArgumentRequired(name, value);

            if (!value.StartsWith(HIQualifiers.IHIQualifier))
                throw new ArgumentException(string.Format(Resources.ParameterInvalidIHI, HIQualifiers.IHIQualifier), name);
        }

        /// <summary>
        /// Validates that a DateTime is a time in the past (a tolerance of 2 minutes is set).
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="required">Indicates if the argument is required.</param>
        public static void ValidatePastDateTime(string name, DateTime value, bool required)
        {
            if (required) ValidateDateTime(name, value);

            DateTime comparison = DateTime.Now.AddMinutes(2);

            if (value > comparison)
                throw new ArgumentException(Resources.ParameterPastDate, name);
        }

        /// <summary>
        /// Validates that a string is of a particular length.
        /// </summary>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="value">The value of the string to validate.</param>
        /// <param name="length">The length that the string has to be.</param>
        /// <param name="required">Indicates if the argument is required.</param>
        public static void ValidateStringLength(string name, string value, int length, bool required)
        {
            if (required) ValidateArgumentRequired(name, value);
            
            if (!string.IsNullOrEmpty(value) && value.Length != length)
                throw new ArgumentException(string.Format(Resources.ParameterStringLength, length), name);
        }

        /// <summary>
        /// Calculates a valid identifier with a correct LUHN check digit
        /// </summary>
        /// <param name="healthIdentifier">15 digit identifier as a string</param>
        /// <returns>16 digit identifier with a valid LUHN check digit in the 16th position.</returns>
        public static string CalculateLuhnCheckDigit(string healthIdentifier)
        {
            string healthIdentifierOutput = "";
            int sum = 0;
            bool mod2 = true;

            if (healthIdentifier == null)
            {
                throw new ArgumentException("INVALID Health Identifier - null value");
            }

            //Remove unwanted characters (space, '-')
            healthIdentifier = healthIdentifier.Replace(" ", "").Replace("-", "");

            if (healthIdentifier.Length == 15)
            {
                try
                {

                    for (int i = 14; i >= 0; i--)
                    {
                        int curr = Convert.ToInt32(healthIdentifier.Substring(i, 1));
                        if (mod2)
                        {
                            curr *= 2;
                            if (curr > 9)
                                curr -= 9;
                        }
                        mod2 = !mod2;
                        sum += curr;
                    }
                    int check = Convert.ToInt32(sum.ToString().Substring(sum.ToString().Length - 1, 1));
                    if (check > 0)
                        check = 10 - check;

                    healthIdentifierOutput = healthIdentifier + check.ToString();
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("ERROR calcualting health identifier LUNH check digit: {0}", healthIdentifier));
                }
            }
            else
            {
                throw new ArgumentException(string.Format("INVALID Health Identifier - must be 15 digits : {0}", healthIdentifier));
            }

            return (healthIdentifierOutput);
        }

        /// <summary>
        /// Validates a 16 digit LUHN checked health identifier
        /// </summary>
        /// <param name="healthIdentifierWithLuhnCheckDigit">16 digit health identifier that includes a LUHN check digit.</param>
        /// <returns>Boolean : True if input is a valid LUHN checked number, False otherwise.</returns>
        public static bool ValidateLuhnCheckDigit(string healthIdentifierWithLuhnCheckDigit)
        {
            string generatedHealthIdentifier = string.Empty;

            if (healthIdentifierWithLuhnCheckDigit == null)
            {
                throw new ArgumentException("INVALID Health Identifier - null value");
            }

            //Remove unwanted characters (space, '-')
            healthIdentifierWithLuhnCheckDigit = healthIdentifierWithLuhnCheckDigit.Replace(" ", "").Replace("-", "");

            try
            {
                var tempHealthIdentifier = healthIdentifierWithLuhnCheckDigit;
                if (healthIdentifierWithLuhnCheckDigit.Length > 15)
                {
                    tempHealthIdentifier = healthIdentifierWithLuhnCheckDigit.Substring(0, 15);
                }
                generatedHealthIdentifier = CalculateLuhnCheckDigit(tempHealthIdentifier);
            }
            catch (Exception)
            {
                return false;
            }

            return (generatedHealthIdentifier == healthIdentifierWithLuhnCheckDigit);
        }

        

    }
}
