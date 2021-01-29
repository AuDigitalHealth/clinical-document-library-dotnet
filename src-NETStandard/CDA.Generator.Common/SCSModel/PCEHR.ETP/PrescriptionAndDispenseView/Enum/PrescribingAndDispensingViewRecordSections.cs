using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// View Record Sections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PrescribingAndDispensingViewRecordSections
    {
      /// <summary>
      /// The exclusion statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.179.1.1", Name = "Exclusion Statement", CodeSystem = "NCTIS")]
      ExclusionStatement,

      /// <summary>
      /// The prescribing and dispensing reports
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16794", Name = "Prescribing and Dispensing Reports", CodeSystem = "NCTIS")]
      PrescribingAndDispensingReports,

      /// <summary>
      /// The medication entries with summary
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16795", Name = "MEDICATION ENTRIES WITH SUMMARY", CodeSystem = "NCTIS")]
      MedicationEntriesWithSummary,

      /// <summary>
      /// The summary of medication entries
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16798", Name = "SUMMARY OF MEDICATION ENTRIES", CodeSystem = "NCTIS")]
      SummaryOfMedicationEntries,

      /// <summary>
      /// The therapeutic good identification
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10194", Name = "Therapeutic Good Identification", CodeSystem = "NCTIS")]
      TherapeuticGoodIdentification,

      /// <summary>
      /// The date time prescription written
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16799", Name = "DateTime Prescription Written", CodeSystem = "NCTIS")]
      DateTimePrescriptionWritten,

      /// <summary>
      /// The date time of earliest dispense event
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16801", Name = "DateTime of Earliest Dispense Event", CodeSystem = "NCTIS")]
      DateTimeOfEarliestDispenseEvent,

      /// <summary>
      /// The date time of latest dispense event
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16802", Name = "DateTime of Latest Dispense Event", CodeSystem = "NCTIS")]
      DateTimeOfLatestDispenseEvent,

      /// <summary>
      /// The total number of known supplies
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16804", Name = "Total Number of Known Supplies", CodeSystem = "NCTIS")]
      TotalNumberOfKnownSupplies,

      /// <summary>
      /// The maximum number of permitted supplies
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16805", Name = "Maximum Number of Permitted Supplies", CodeSystem = "NCTIS")]
      MaximumNumberOfPermittedSupplies,

      /// <summary>
      /// The dispense record link
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.179.1.1", Name = "Dispense Record Link", CodeSystem = "NCTIS")]
      DispenseRecordLink,

      /// <summary>
      /// The prescription record link
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.179.1.2", Name = "Prescription Record Link", CodeSystem = "NCTIS")]
      PrescriptionRecordLink
    }
  }
