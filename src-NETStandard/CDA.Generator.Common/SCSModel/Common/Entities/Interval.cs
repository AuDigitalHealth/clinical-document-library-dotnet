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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that pertain to a Interval
    /// </summary>
    [Serializable]
    [DataContract]
    public class Interval
    {
        #region Properties

        /// <summary>
        /// UnitOfMeasure
        /// </summary>
        [CanBeNull]
        [DataMember]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// Interval
        /// </summary>
        [CanBeNull]
        [DataMember]
        public int? Value { get; set; }

        #endregion

        #region Constructors
        internal Interval()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this interval
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            validationBuilder.ArgumentRequiredCheck("UnitOfMeasure", UnitOfMeasure);
            validationBuilder.ArgumentRequiredCheck("Quantity", Value);
        }
        #endregion
    }
}
