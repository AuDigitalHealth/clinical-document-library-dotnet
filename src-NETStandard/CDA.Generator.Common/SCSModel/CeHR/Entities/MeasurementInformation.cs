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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an MeasurementInformation 
    /// </summary>
    [Serializable]
    [DataContract]
    public class MeasurementInformation
    {
        #region Properties

        /// <summary>
        /// Observation Date
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime ObservationDate { get; set; }

        /// <summary>
        /// Head Circumference
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity HeadCircumference { get; set; }

        /// <summary>
        /// Body Height
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity BodyHeight { get; set; }

        /// <summary>
        /// Body Weight
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity BodyWeight { get; set; }

        /// <summary>
        /// Body Mass Index
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity BodyMassIndex { get; set; }

        /// <summary>
        /// Body Weight
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        #endregion

        #region Constructors
        internal MeasurementInformation()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this MeasurementInformation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (HeadCircumference == null && BodyHeight == null && BodyMassIndex == null && BodyWeight == null)
            {
              vb.AddValidationMessage("MeasurementInformation", null, "Please provide an entry for MeasurementInformation");
            }

            if (HeadCircumference != null)
                HeadCircumference.Validate(path, messages);

            if (BodyHeight != null)
                BodyHeight.Validate(path, messages);

            if (BodyMassIndex != null)
                BodyMassIndex.Validate(path, messages);

            if (BodyWeight != null)
                BodyWeight.Validate(path, messages);
        }

        #endregion
    }
}