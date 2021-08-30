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
    /// Interval width
    /// </summary>
    public class CdaIntervalOrAge
    {
        // Either an Age (Value,Unit)
        // Or CdaInterval (Date)

        /// <summary>
        /// Quantity
        /// </summary>
        [CanBeNull]
        public string Value { get; set; }

        /// <summary>
        /// Unit.
        /// </summary>
        [CanBeNull]
        public TimeUnitOfMeasure Unit { get; set; }

        /// <summary>
        /// Capture Date for Low value.
        /// </summary>
        [CanBeNull]
        public CdaInterval Interval { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        internal CdaIntervalOrAge()
        {
        }

        /// <summary>
        /// Creates an low interval 
        /// </summary>
        /// <param name="low">Low</param>
        public CdaIntervalOrAge(ISO8601DateTime low)
        {
            Validation.ValidateArgumentRequired("low", low);

            Interval = CdaInterval.CreateLow(low);
        }

        /// <summary>
        /// Creates an age
        /// </summary>
        /// <param name="low">Low</param>
        public CdaIntervalOrAge(string value, TimeUnitOfMeasure unit)
        {
            Validation.ValidateArgumentRequired("value", value);
            Validation.ValidateArgumentRequired("units", unit);

            Value = value;
            Unit = unit;
        }

    }
}
