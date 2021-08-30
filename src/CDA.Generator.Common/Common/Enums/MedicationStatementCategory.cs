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
    /// Medicine Item Change
    /// </summary>
    [Serializable]
    [DataContract]
    public enum MedicationStatementCategory
    {
        /// <summary>
        /// inpatient
        /// </summary>
        [EnumMember]
        [Name(Code = "inpatient", Name = "Inpatient", CodeSystem = "HL7MedicationStatementCategoryCodes")]
        Inpatient,

        /// <summary>
        /// outpatient
        /// </summary>
        [EnumMember]
        [Name(Code = "outpatient", Name = "Outpatient", CodeSystem = "HL7MedicationStatementCategoryCodes")]
        Outpatient,

        /// <summary>
        /// community
        /// </summary>
        [EnumMember]
        [Name(Code = "community", Name = "Community", CodeSystem = "HL7MedicationStatementCategoryCodes")]
        Community,

        /// <summary>
        /// patientspecified
        /// </summary>
        [EnumMember]
        [Name(Code = "patientspecified", Name = "Patient Specified", CodeSystem = "HL7MedicationStatementCategoryCodes")]
        PatientSpecified,

    }
}
