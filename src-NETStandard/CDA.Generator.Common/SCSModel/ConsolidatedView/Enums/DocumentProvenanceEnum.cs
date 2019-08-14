using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// DocumentProvenance
    /// </summary>
    [Serializable]
    [DataContract]
    public enum DocumentProvenanceEnum
    {
      /// <summary>
      /// Shared health summary document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720.160.1.1", Name = "Shared Health Summary Document Provenance", CodeSystem = "NCTIS")]
      SharedHealthSummaryDocumentProvenance,

      /// <summary>
      /// Advance care directive custodian document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720.160.1.2", Name = "Advance Care Directive Custodian Document Provenance", CodeSystem = "NCTIS")]
      AdvanceCareDirectiveCustodianDocumentProvenance,

      /// <summary>
      /// New document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720.160.1.3", Name = "New Document Provenance", CodeSystem = "NCTIS")]
      NewDocumentProvenance,

      /// <summary>
      /// Recent document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720.160.1.4", Name = "Recent Document Provenance", CodeSystem = "NCTIS")]
      RecentDocumentProvenance,

      /// <summary>
      /// Recent diagnostic test result document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.5", Name = "Recent Diagnostic Test Result Document Provenance", CodeSystem = "NCTIS")]
      RecentDiagnosticTestResultDocumentProvenance,

      /// <summary>
      /// Medicare document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720.160.1.6", Name = "Medicare Document Provenance", CodeSystem = "NCTIS")]
      MedicareDocumentProvenance,

      /// <summary>
      /// Consumer entered document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720.160.1.7", Name = "Consumer Entered Document Provenance", CodeSystem = "NCTIS")]
      ConsumerEnteredDocumentProvenance,
    }
  }

