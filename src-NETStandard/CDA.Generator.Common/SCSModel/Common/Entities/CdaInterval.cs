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

using System.Collections.Generic;
using System.Runtime.Serialization;
using CDA.Generator.Common.Common.Time;
using CDA.Generator.Common.Common.Time.Enum;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Interval.
    /// Allowable combinations: Low, Width, High, Centre, Low/High, Low/Width, High/Width, Centre/Width
    /// </summary>
    public class CdaInterval : ITime
    {
        /// <summary>
        /// showOngoingInNarrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? ShowOngoingInNarrative { get; set; }

        /// <summary>
        /// Operator
        /// </summary>
        [CanBeNull]
        [DataMember]
        public OperationTypes? Operator { get; private set; }

        /// <summary>
        /// NullFlavor
        /// </summary>
        [CanBeNull]
        [DataMember]
        public NullFlavor? NullFlavor { get; private set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [CanBeNull]
        [DataMember]
        public int? Value { get; private set; }

        /// <summary>
        /// Type of CdaInterval.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IntervalType Type { get; private set; }

        /// <summary>
        /// Low value.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime Low { get; private set; }

        /// <summary>
        /// Centre value.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime Center { get; private set; }

        /// <summary>
        /// High value.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime High { get; private set; }

        /// <summary>
        /// Interval width.
        /// </summary>
        public CdaIntervalWidth IntervalWidth { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        internal CdaInterval()
        {            
        }

        /// <summary>
        /// Creates an CdaInterval with low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateLow(ISO8601DateTime low)
        {            
            Validation.ValidateArgumentRequired("low", low);
            return CreateLow(low, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with low.
        /// </summary>
        /// <param name="value">Low.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateValue(ISO8601DateTime value)
        {
            Validation.ValidateArgumentRequired("value", value);
            return CreateValue(value, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateLow(ISO8601DateTime low, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("low", low);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.Low,
                Low = low
            };
        }

        /// <summary>
        /// Creates an CdaInterval with low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateValue(ISO8601DateTime low, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {

            Validation.ValidateArgumentRequired("value", value);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.Value,
                Low = low
            };
        }

        /// <summary>
        /// Creates an CdaInterval with a width.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateWidth(string width, TimeUnitOfMeasure unit)
        {
            Validation.ValidateArgumentRequired("width", width);
            Validation.ValidateArgumentRequired("unit", unit);

            return CreateWidth(width, unit, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with a width.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="value">Quantity.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param> 
        /// <returns>Interval.</returns>
        public static CdaInterval CreateWidth(string width, TimeUnitOfMeasure unit, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("width", width);
            Validation.ValidateArgumentRequired("unit", unit);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.Width,
                IntervalWidth = new CdaIntervalWidth(width, unit)
            };
        }

        /// <summary>
        /// Creates an CdaInterval with high.
        /// </summary>
        /// <param name="high">High.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateHigh(ISO8601DateTime high)
        {
            Validation.ValidateArgumentRequired("high", high);

            return CreateHigh(high, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with high.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <param name="high">High.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateHigh(ISO8601DateTime high, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("high", high);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.High,
                High = high
            };
        }

        /// <summary>
        /// Creates an CdaInterval with center.
        /// </summary>
        /// <param name="center">Center.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateCenter(ISO8601DateTime center)
        {
            Validation.ValidateArgumentRequired("center", center);

            return CreateCenter(center, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with center.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <param name="center">Center.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateCenter(ISO8601DateTime center, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("center", center);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.Center,
                Center = center
            };
        }

        /// <summary>
        /// Creates an CdaInterval with high and low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="high">High.</param>
        /// <returns>Interval.</returns>
        public static CdaInterval CreateLowHigh(ISO8601DateTime low, ISO8601DateTime high)
        {
            Validation.ValidateArgumentRequired("low", low);
            Validation.ValidateArgumentRequired("high", high);

            return CreateLowHigh(low, high, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with high and low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="high">High.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>Interval.</returns>
        public static CdaInterval CreateLowHigh(ISO8601DateTime low, ISO8601DateTime high, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("low", low);
            Validation.ValidateArgumentRequired("high", high);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.LowHigh,
                Low = low,
                High = high
            };
        }

        /// <summary>
        /// Creates an CdaInterval with low and width.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="value">Quantity.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateLowWidth(ISO8601DateTime low, string value, TimeUnitOfMeasure unit)
        {
            Validation.ValidateArgumentRequired("low", low);
            Validation.ValidateArgumentRequired("value", value);
            Validation.ValidateArgumentRequired("unit", unit);

            return CreateLowWidth(low, value, unit, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with low and width.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateLowWidth(ISO8601DateTime low, string width, TimeUnitOfMeasure unit, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("low", low);
            Validation.ValidateArgumentRequired("width", width);
            Validation.ValidateArgumentRequired("unit", unit);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.LowWidth,
                Low = low,
                IntervalWidth = new CdaIntervalWidth(width, unit)
            };
        }

        /// <summary>
        /// Creates an CdaInterval with high and width.
        /// </summary>
        /// <param name="high">High.</param>
        /// <param name="width">width.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateHighWidth(ISO8601DateTime high, string width, TimeUnitOfMeasure unit)
        {
            Validation.ValidateArgumentRequired("high", high);
            Validation.ValidateArgumentRequired("width", width);
            Validation.ValidateArgumentRequired("unit", unit);

            return CreateHighWidth(high, width, unit, null, null, null);
        }

        /// <summary>
        /// Creates an CdaInterval with high and width.
        /// </summary>
        /// <param name="high">High.</param>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateHighWidth(ISO8601DateTime high, string width, TimeUnitOfMeasure unit, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("high", high);
            Validation.ValidateArgumentRequired("width", width);
            Validation.ValidateArgumentRequired("unit", unit);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.HighWidth,
                High = high,
                IntervalWidth = new CdaIntervalWidth(width, unit)
            };
        }


        /// <summary>
        /// Creates an CdaInterval with center and width.
        /// </summary>
        /// <param name="center">Center.</param>
        /// <param name="value">Quantity.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateCenterWidth(ISO8601DateTime center, string value, TimeUnitOfMeasure unit)
        {
            Validation.ValidateArgumentRequired("center", center);
            Validation.ValidateArgumentRequired("value", value);
            Validation.ValidateArgumentRequired("unit", unit);

            return new CdaInterval
                       {
                           Type = IntervalType.CenterWidth,
                           Center = center,
                           IntervalWidth = new CdaIntervalWidth(value, unit)
                       };
        }

        /// <summary>
        /// Creates an CdaInterval with center and width.
        /// </summary>
        /// <param name="center">Center.</param>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateCenterWidth(ISO8601DateTime center, string width, TimeUnitOfMeasure unit, int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            Validation.ValidateArgumentRequired("center", center);
            Validation.ValidateArgumentRequired("width", width);
            Validation.ValidateArgumentRequired("unit", unit);

            return new CdaInterval
            {
                Operator = operatorType,
                NullFlavor = nullFlavor,
                Value = value,
                Type = IntervalType.CenterWidth,
                Center = center,
                IntervalWidth = new CdaIntervalWidth(width, unit)
            };
        }

        /// <summary>
        /// Validates the CdaInterval.
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
        }

        #region Narrative

      /// <summary>
      /// The NarrativeText for the Time
      /// </summary>
      public string NarrativeText()
        {
          return CdaIntervalFormatter.Format(this);
        }

        #endregion 


    }
   
}


