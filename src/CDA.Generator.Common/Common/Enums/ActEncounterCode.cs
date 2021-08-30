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
    /// Act Code
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ActEncounterCode
    {
        /// <summary>
        /// ambulatory
        /// </summary>
        [EnumMember]
        [Name(Code = "AMB", Name = "ambulatory", CodeSystem = "HL7ActEncounterCodes")]
        Ambulatory,

        /// <summary>
        /// emergency
        /// </summary>
        [EnumMember]
        [Name(Code = "EMER", Name = "emergency", CodeSystem = "HL7ActEncounterCodes")]
        Emergency,

        /// <summary>
        /// field
        /// </summary>
        [EnumMember]
        [Name(Code = "FLD", Name = "field", CodeSystem = "HL7ActEncounterCodes")]
        Field,

        /// <summary>
        /// home health
        /// </summary>
        [EnumMember]
        [Name(Code = "HH", Name = "home health", CodeSystem = "HL7ActEncounterCodes")]
        HomeHealth,

        /// <summary>
        /// inpatient encounter	
        /// </summary>
        [EnumMember]
        [Name(Code = "IMP", Name = "inpatient encounter", CodeSystem = "HL7ActEncounterCodes")]
        InpatientEncounter,

        /// <summary>
        /// inpatient acute
        /// </summary>
        [EnumMember]
        [Name(Code = "ACUTE", Name = "inpatient acute", CodeSystem = "HL7ActEncounterCodes")]
        InpatientAcute,

        /// <summary>
        /// inpatient non-acute
        /// </summary>
        [EnumMember]
        [Name(Code = "NONAC", Name = "inpatient non-acute", CodeSystem = "HL7ActEncounterCodes")]
        InpatientNonAcute,

        /// <summary>
        /// pre-admission
        /// </summary>
        [EnumMember]
        [Name(Code = "PRENC", Name = "pre-admission", CodeSystem = "HL7ActEncounterCodes")]
        PreAdmission,

        /// <summary>
        /// short stay
        /// </summary>
        [EnumMember]
        [Name(Code = "SS", Name = "short stay", CodeSystem = "HL7ActEncounterCodes")]
        ShortStay,

        /// <summary>
        /// virtual
        /// </summary>
        [EnumMember]
        [Name(Code = "VR", Name = "virtual", CodeSystem = "HL7ActEncounterCodes")]
        Virtual,


    }
}
