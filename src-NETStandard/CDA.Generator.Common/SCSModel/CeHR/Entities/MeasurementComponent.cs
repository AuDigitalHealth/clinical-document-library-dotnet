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
using CDA.Generator.Common.SCSModel.CeHR.Enum;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an MeasurementComponent  
    /// </summary>
    [Serializable]
    [DataContract]
    public class MeasurementComponent 
    {
      #region MeasurementComponent 

        /// <summary>
        /// Component Value 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity ComponentValue { get; set; }

        /// <summary>
        /// Percentile Value
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity PercentileValue { get; set; }

        #endregion

        #region Constructors
        internal MeasurementComponent()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Measurement Component
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("ComponentValue", ComponentValue);
        }

        #endregion
    }
}