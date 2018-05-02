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
    /// HL7 V3 Result Status
    /// </summary>
    [Serializable]
    [DataContract]
    public enum Hl7V3ResultStatus
    {
        /// <summary>
        /// Correction to results
        /// </summary>
        [EnumMember]
        [Name(Name = "Correction to results", Code = "C", CodeSystem = "HL7ResultStatus")]
        CorrectionToResults,

        /// <summary>
        /// Final results; results stored and verified. Can only be changed with a corrected result.
        /// </summary>
        [EnumMember]
        [Name(Name = "Final results; results stored and verified. Can only be changed with a corrected result.", Code = "F", CodeSystem = "HL7ResultStatus")]
        FinalResults,

        /// <summary>
        /// No results available; specimen received, procedure incomplete
        /// </summary>
        [EnumMember]
        [Name(Name = "No results available; specimen received, procedure incomplete", Code = "I", CodeSystem = "HL7ResultStatus")]
        NoResultsAvailableSpecimenReceivedProcedureIncomplete,

        /// <summary>
        /// Order received; specimen not yet received
        /// </summary>
        [EnumMember]
        [Name(Name = "Order received; specimen not yet received", Code = "O", CodeSystem = "HL7ResultStatus")]
        OrderReceived,

        /// <summary>
        /// Preliminary: A verified early result is available, final results not yet obtained
        /// </summary>
        [EnumMember]
        [Name(Name = "Preliminary: A verified early result is available, final results not yet obtained", Code = "P", CodeSystem = "HL7ResultStatus")]
        Preliminary,

        /// <summary>
        /// Results stored; not yet verified
        /// </summary>
        [EnumMember]
        [Name(Name = "Results stored; not yet verified", Code = "R", CodeSystem = "HL7ResultStatus")]
        ResultsStored,

        /// <summary>
        /// No results available; procedure scheduled, but not done
        /// </summary>
        [EnumMember]
        [Name(Name = "No results available; procedure scheduled, but not done", Code = "S", CodeSystem = "HL7ResultStatus")]
        NoResultsAvailableProcedureScheduledButNotDone,

        /// <summary>
        /// Some, but not all, results available
        /// </summary>
        [EnumMember]
        [Name(Name = "Some, but not all, results available", Code = "A", CodeSystem = "HL7ResultStatus")]
        SomeButNotAllResultsAvailable,

        /// <summary>
        /// No results available; Order cancelled
        /// </summary>
        [EnumMember]
        [Name(Name = "No results available; Order canceled", Code = "X", CodeSystem = "HL7ResultStatus")]
        NoResultsAvailableOrderCanceled,

        /// <summary>
        /// No order on record for this test. (Used only on queries)
        /// </summary>
        [EnumMember]
        [Name(Name = "No order on record for this test. (Used only on queries)", Code = "Y", CodeSystem = "HL7ResultStatus")]
        NoOrderOnRecordForThisTest,

        /// <summary>
        /// No record of this patient. (Used only on queries)
        /// </summary>
        [EnumMember]
        [Name(Name = "No record of this patient. (Used only on queries)", Code = "Z", CodeSystem = "HL7ResultStatus")]
        NoRecordOfThisPatient,
    }
}
