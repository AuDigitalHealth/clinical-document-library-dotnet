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
        /// ResultValue as codable text CD
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ValueAsCodableText { get; set; }

        /// <summary>
        /// ResultValue as quantity PQ
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity TestResultValue { get; set; }

        /// <summary>
        /// ResultValue as quantity range IVL_PQ
        /// </summary>
        [CanBeNull]
        [DataMember]
        public QuantityRange ValueAsQuantityRange { get; set; }

        /// <summary>
        /// ResultValue as boolean BL
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? ValueAsBoolean { get; set; }

        /// <summary>
        /// ResultValue as string ST
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ValueAsString { get; set; }

        /// <summary>
        /// ResultValue as integer INT
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? ValueAsInteger { get; set; }


        /// <summary>
        /// ResultValue as Ratio
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Ratio ValueAsRatio { get; set; }

        /// <summary>
        /// ResultValue as PPD (ParametricProbabilityDistribution)
        /// Looks very complex to implement - Not doing unless requested
        /// </summary>
        //[CanBeNull]
        //[DataMember]
        //public ParametricProbabilityDistribution ValueAsPPD { get; set; }

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

            if (ValueAsCodableText != null)
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
            
            if (ValueAsBoolean != null || ValueAsString != null || ValueAsInteger != null)
            {
                // If not null, then no further validation required
            }

            if (ValueAsRatio != null)
            {
                ValueAsRatio.Validate(validationBuilder.Path + "ValueAsRatio", messages);
            }

            var choiceDictionary = new Dictionary<String, object>
                                       {
                                           {"ValueAsCodableText", ValueAsCodableText},
                                           {"ValueAsQuantity", TestResultValue},
                                           {"ValueAsQuantityRange", ValueAsQuantityRange},
                                           {"ValueAsBoolean", ValueAsBoolean},
                                           {"ValueAsString", ValueAsString},
                                           {"ValueAsInteger", ValueAsInteger},
                                           {"ValueAsRatio", ValueAsRatio},
                                       };

            validationBuilder.ChoiceCheck(choiceDictionary);
        }

        #endregion
    }
}
