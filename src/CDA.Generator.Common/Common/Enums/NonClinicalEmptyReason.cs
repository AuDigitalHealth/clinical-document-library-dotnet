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
    /// HL7
    /// https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/non-clinical-empty-reason-2
    /// </summary>
    [Serializable]
    [DataContract]
    public enum NonClinicalEmptyReason
    {
        /// <summary>
        /// None known
        /// </summary>
        [EnumMember]
        [Name(Code = "notasked", Name = "Not Asked", CodeSystem = "HL7NonClinicalEmptyReason")]
        NotAsked,

        /// <summary>
        /// Not asked
        /// </summary>
        [EnumMember]
        [Name(Code = "withheld", Name = "Withheld", CodeSystem = "HL7NonClinicalEmptyReason")]
        Withheld,

        /// <summary>
        /// None Supplied
        /// </summary>
        [EnumMember]
        [Name(Code = "unavailable", Name = "Unavailable", CodeSystem = "HL7NonClinicalEmptyReason")]
        Unavailable
    }
}
