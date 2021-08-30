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
    public enum MedicationStatementTaken
    {
        /// <summary>
        /// y
        /// </summary>
        [EnumMember]
        [Name(Code = "y", Name = "Yes", CodeSystem = "HL7MedicationStatementTakenCodes")]
        Yes,

        /// <summary>
        /// n
        /// </summary>
        [EnumMember]
        [Name(Code = "n", Name = "No", CodeSystem = "HL7MedicationStatementTakenCodes")]
        No,

        /// <summary>
        /// unk
        /// </summary>
        [EnumMember]
        [Name(Code = "unk", Name = "Unknown", CodeSystem = "HL7MedicationStatementTakenCodes")]
        Unknown,

        /// <summary>
        /// patienantspecified
        /// </summary>
        [EnumMember]
        [Name(Code = "na", Name = "Not Applicable", CodeSystem = "HL7MedicationStatementTakenCodes")]
        NotApplicable,

    }
}
