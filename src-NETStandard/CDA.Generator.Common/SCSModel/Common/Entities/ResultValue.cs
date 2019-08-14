/*
 * Copyright 2013 NEHTA
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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an result value
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class ResultValue
    {
        #region Properties
        /// <summary>
        /// ResultValue as codable text
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ValueAsCodableText { get; set; }

        /// <summary>
        /// ResultValue as quantity
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity TestResultValue { get; set; }

        /// <summary>
        /// ResultValue as quantity range
        /// </summary>
        [CanBeNull]
        [DataMember]
        public QuantityRange ValueAsQuantityRange { get; set; }
        #endregion

        #region Constructors
        internal ResultValue()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this result value
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            if(ValueAsCodableText != null)
            {
                ValueAsCodableText.Validate(validationBuilder.Path + "ValueAsCodableText", messages);
            }

            if (TestResultValue != null)
            {
                TestResultValue.Validate(validationBuilder.Path + "TestResultValue", messages);
            }

            if (ValueAsQuantityRange != null)
            {
                ValueAsQuantityRange.Validate(validationBuilder.Path + "ValueAsQuantityRange", messages);
            }

            var choiceDictionary = new Dictionary<String, object>
                                       {
                                           {"ValueAsCodableText", ValueAsCodableText},
                                           {"ValueAsQuantity", TestResultValue},
                                           {"ValueAsQuantityRange", ValueAsQuantityRange}
                                       };

            validationBuilder.ChoiceCheck(choiceDictionary);
        }

        #endregion
    }
}
