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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Medicare Number Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum MedicareNumberType
    {
        /// <summary>
        /// Individual Medicare Card Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.0.7", Name = "Individual Medicare Card Number")]
        IndividualMedicareCardNumber,

        /// <summary>
        /// Medicare Card Number
        /// </summary>
        [EnumMember]
        [Name(Code = "1.2.36.1.5001.1.0.7.1", Name = "Medicare Card Number")]
        MedicareCardNumber,

    }
}
