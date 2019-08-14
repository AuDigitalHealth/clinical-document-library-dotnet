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
    /// This class is designed to encapsulate the properties within a CDA document that make up an PhysicalMeasurement 
    /// </summary>
    [Serializable]
    [DataContract]
    public class PhysicalMeasurement
    {
        #region Properties

        /// <summary>
        /// Head Circumference
        /// </summary>
        [CanBeNull]
        [DataMember]
        public HeadCircumference HeadCircumference { get; set; }

        /// <summary>
        /// Physical Measurement Body Weight
        /// </summary>
        [CanBeNull]
        [DataMember]
        public PhysicalMeasurementBodyWeight PhysicalMeasurementBodyWeight { get; set; }

        /// <summary>
        /// Physical Measurement Body Height Length
        /// </summary>
        [CanBeNull]
        public PhysicalMeasurementBodyHeightLength PhysicalMeasurementBodyHeightLength { get; set; }

        /// <summary>
        /// Physical Measurement Body Height Length
        /// </summary>
        [CanBeNull]
        public PhysicalMeasurementBodyMassIndex PhysicalMeasurementBodyMassIndex { get; set; }     

        #endregion

        #region Constructors
        internal PhysicalMeasurement()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this PhysicalMeasurement item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            int tally = 0;

            if (HeadCircumference != null)
            {
                tally++;
                HeadCircumference.Validate(vb.Path + "HeadCircumference", vb.Messages);
            }

            if (PhysicalMeasurementBodyWeight != null)
            {
                tally++;
                PhysicalMeasurementBodyWeight.Validate(vb.Path + "PhysicalMeasurementBodyWeight", vb.Messages);
            }

            if (PhysicalMeasurementBodyHeightLength != null)
            {
               tally++;
               PhysicalMeasurementBodyHeightLength.Validate(vb.Path + "PhysicalMeasurementBodyHeightLength", vb.Messages);
            }

            if (PhysicalMeasurementBodyMassIndex != null)
            {
               tally++;
               PhysicalMeasurementBodyMassIndex.Validate(vb.Path + "PhysicalMeasurementBodyMassIndex", vb.Messages);
            }

            if (tally == 0)
            {
              vb.AddValidationMessage(vb.Path + "Physical Measurement", null, "Each instance of this Choice SHALL contain exactly one 'Head Circumference (BODY PART CIRCUMFERENCE)' OR exactly one 'BODY WEIGHT' OR exactly one 'BODY HEIGHT/LENGTH' OR exactly one 'BODY MASS INDEX'.");

            }
        }

        #endregion
    }
}