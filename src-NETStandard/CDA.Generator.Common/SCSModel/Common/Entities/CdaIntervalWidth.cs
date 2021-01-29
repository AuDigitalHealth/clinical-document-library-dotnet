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

using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Interval width
    /// </summary>
    public class CdaIntervalWidth    
    {
        /// <summary>
        /// Quantity
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Unit.
        /// </summary>
        public TimeUnitOfMeasure Unit { get; set; }
        
        /// <summary>
        /// Creates an interval width
        /// </summary>
        /// <param name="value">Quantity</param>
        /// <param name="unit">Unit</param>
        public CdaIntervalWidth(string value, TimeUnitOfMeasure unit)
        {
            Validation.ValidateArgumentRequired("value", value);
            Validation.ValidateArgumentRequired("units", unit);

            Value = value;
            Unit = unit;
        }

    }
}
