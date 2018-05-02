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
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
  /// This class is designed to encapsulate the properties within a CDA document that make up an MeasurementEntry 
    /// </summary>
    [Serializable]
    [DataContract]
    public class MeasurementEntry
    {
        /// <summary>
        /// Body Height Measure,
        /// </summary>
        public MeasurementComponent BodyHeightMeasure { get; set; }

        /// <summary>
        /// Body Weight Measure,
        /// </summary>
        public MeasurementComponent BodyWeightMeasure { get; set; }

        /// <summary>
        /// Head Circumference Measure 
        /// </summary>
        public MeasurementComponent HeadCircumferenceMeasure { get; set; }

        /// <summary>
        /// Body Mass Index
        /// </summary>
        public MeasurementComponent BodyMassIndex { get; set; }

        /// <summary>
        /// Document Link 
        /// </summary>
        [CanBeNull]
        public Link DocumentLink { get; set; }

        /// <summary>
        /// Observation Date
        /// </summary>
        [CanBeNull]
        public ISO8601DateTime ObservationDate { get; set; }

        #region Constructors
        internal MeasurementEntry()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this MeasurementEntry
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (DocumentLink != null)
            {
               if (DocumentLink != null) DocumentLink.Validate(path, messages);
            }

            if (BodyHeightMeasure != null)
            {
               BodyHeightMeasure.Validate(path, messages);
            }

            if (BodyWeightMeasure != null)
            {
              BodyWeightMeasure.Validate(path, messages);
            }

            if (HeadCircumferenceMeasure != null)
            {
              HeadCircumferenceMeasure.Validate(path, messages);
            }

            if (BodyMassIndex != null)
            {
              BodyMassIndex.Validate(path, messages);
            }

            //if (BodyHeightMeasure == null && BodyWeightMeasure == null && HeadCircumferenceMeasure == null && BodyMassIndex == null)
            //{
            //  vb.AddValidationMessage("Measurement component", null, "Please provide one of the following BodyHeightMeasure or BodyWeightMeasure or HeadCircumferenceMeasure or BodyMassIndex");
            //}

            if (BodyMassIndex != null)
            {
              BodyMassIndex.Validate(path, messages);
            }

            vb.ArgumentRequiredCheck("ObservationDate", ObservationDate);
        }

        #endregion
    }
}