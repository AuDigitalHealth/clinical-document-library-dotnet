using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// DocumentProvenance
    /// </summary>
    [Serializable]
    [DataContract]
    public enum DocumentSections
    {
      /// <summary>
      /// Shared Health Summary Document
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.1", Name = "Shared Health Summary Document", Title = "Shared Health Summary", CodeSystem = "NCTIS")]
      SharedHealthSummary,

      /// <summary>
      /// Advance Care Directive Custodian Document
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.2", Name = "Advance Care Directive Custodian Document", Title = "Advance Care Directive", CodeSystem = "NCTIS")]
      AdvanceCareDirective,

      /// <summary>
      /// New Documents
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.3", Name = "New Documents", Title = "New Documents", CodeSystem = "NCTIS")]
      NewDocument,

      /// <summary>
      /// Recent Documents
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.4", Name = "Recent Documents", Title = "Recent Documents", CodeSystem = "NCTIS")]
      RecentDocument,

      /// <summary>
      /// Recent Diagnostic Test Result Documents
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.5", Name = "Recent Diagnostic Test Result Documents", Title = "Recent Diagnostic Reports", CodeSystem = "NCTIS")]
      RecentDiagnosticTestResult,

      /// <summary>
      /// Medicare Documents
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.6", Name = "Medicare Documents", Title = "Medicare", CodeSystem = "NCTIS")]
      Medicare,

      /// <summary>
      /// Consumer Entered Documents
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16722.160.1.7", Name = "Consumer Entered Documents", Title = "Consumer Entered Information", CodeSystem = "NCTIS")]
      ConsumerEntered,
    }
  }

