using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
   /// Result Status
    /// </summary>
   [Serializable]
   [DataContract]
    public enum HL7ResultStatus
    {
        /// <summary>
        /// Correction to results
        /// </summary>
        [EnumMember]
        [Name(Code = "C", Name = "Correction to results", CodeSystem = "HL7ResultStatus")]
        CorrectionToResults,

        /// <summary>
        /// Final results; results stored and verified. Can only be changed with a corrected result
        /// </summary>
        [EnumMember]
        [Name(Code = "F", Name = "Final results; results stored and verified. Can only be changed with a corrected result.", CodeSystem = "HL7ResultStatus")]
        FinalResultsResultsStoredAndVerifiedCanOnlyBeChangedWithACorrectedResult,

        /// <summary>
        /// No results available; specimen received, procedure incomplete
        /// </summary>
        [EnumMember]
        [Name(Code = "I", Name = "No results available; specimen received, procedure incomplete", CodeSystem = "HL7ResultStatus")]
        NoResultsAvailableSpecimenReceivedProcedureIncomplete,

        /// <summary>
        /// Order received; specimen not yet received
        /// </summary>
        [EnumMember]
        [Name(Code = "O", Name = "Order received; specimen not yet received", CodeSystem = "HL7ResultStatus")]
        OrderReceivedSpecimenNotYetReceived,

        /// <summary>
        /// Preliminary: A verified early result is available, final results not yet obtained
        /// </summary>
        [EnumMember]
        [Name(Code = "P", Name = "Preliminary: A verified early result is available, final results not yet obtained", CodeSystem = "HL7ResultStatus")]
        PreliminaryAVerifiedEarlyResultIsAvailableFinalResultsNotYetObtained,

        /// <summary>
        /// Results stored; not yet verified
        /// </summary>
        [EnumMember]
        [Name(Code = "R", Name = "Results stored; not yet verified", CodeSystem = "HL7ResultStatus")]
        ResultsStoredNotYetVerified,

        /// <summary>
        /// No results available; procedure scheduled, but not done
        /// </summary>
        [EnumMember]
        [Name(Code = "S", Name = "No results available; procedure scheduled, but not done", CodeSystem = "HL7ResultStatus")]
        NoResultsAvailableProcedureScheduledButNotDone,

        /// <summary>
        /// Some, but not all, results available
        /// </summary>
        [EnumMember]
        [Name(Code = "A", Name = "Some, but not all, results available", CodeSystem = "HL7ResultStatus")]
        SomeButNotAllResultsAvailable,

        /// <summary>
        /// No results available; Order cancelled
        /// </summary>
        [EnumMember]
        [Name(Code = "X", Name = "No results available; Order canceled", CodeSystem = "HL7ResultStatus")]
        NoResultsAvailableOrderCanceled,


        /// <summary>
        /// Results stored; not yet verified
        /// </summary>
        [EnumMember]
        [Name(Code = "Y", Name = "No order on record for this test. (Used only on queries)", CodeSystem = "HL7ResultStatus")]
        NoOrderOnRecordForThisTest,

        /// <summary>
        /// No record of this patient. (Used only on queries)
        /// </summary>
        [EnumMember]
        [Name(Code = "Z", Name = "No record of this patient. (Used only on queries)", CodeSystem = "HL7ResultStatus")]
        NoRecordOfThisPatient,



  
    }
  }

