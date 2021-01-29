using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// PatholodyResultReportViewSections
    /// </summary>
   [Serializable]
   [DataContract]
   public enum PatholodyResultReportSections
    {
      /// <summary>
        /// Order Details
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16997", Name = "Order Details", CodeSystem = "NCTIS")]
      OrderDetails,

      /// <summary>
      /// Result Group (PATHOLOGY TEST entryRelationship[res_gp]/organizer/code RESULT GROUP) > Pathology Test Result Group Name
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16428", Name = "Pathology Test Result Group Name", CodeSystem = "NCTIS")]
      PathologyTestResultGroupName,

      /// <summary>
      /// PATHOLOGY TEST RESULT > Test Result Name (Pathology Test Result Name)
      /// </summary>
      [EnumMember]
      [Name(Code = "103.11017", Name = "Pathology Test Result Name", CodeSystem = "NCTIS")]
      PathologyTestResultName,

      /// <summary>
      /// Pathology Service
      /// </summary>
      [EnumMember]
      [Name(Code = "310074003", Name = "pathology service", CodeSystem = "SNOMED")]
      PathologyService,

      /// <summary>
      /// report status
      /// </summary>
      [EnumMember]
      [Name(Code = "308552006", Name = "report status", CodeSystem = "SNOMED")]
      OverallTestResultStatus,

      /// <summary>
      /// Pathology
      /// </summary>
      [EnumMember]
      [Name(Code = "101.20018", Name = "Pathology", Title = "Pathology", Narrative = "This component contains the subsections Pathology Test Result and Authority to Post", CodeSystem = "NCTIS")]
      Pathology,

      /// <summary>
      /// Pathology Test Result
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16144", Name = "Pathology Test Result", Title = "Pathology Test Result", CodeSystem = "NCTIS")]
      PathologyTestResult,

      /// <summary>
      /// Pathology Test Result
      /// </summary>
      [EnumMember]
      [Name(Code = "102.20158", Name = "Requested Service", Title = "Requested Service", CodeSystem = "NCTIS")]
      RequestedService,

      /// <summary>
      /// Overall Pathology Test Result Status
      /// </summary>
      [EnumMember]
      [Name(Code = "308552006", Name = "report status", CodeSystem = "SNOMEDCT", Version = "20110531")]
      ReportStatus,

      /// <summary>
      /// PATHOLOGY TEST RESULT > Pathology Test Result DateTime
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16605", Name = "Pathology Test Result DateTime", CodeSystem = "NCTIS")]
      PathologyTestResultDateTime,

      /// <summary>
      /// REQUESTED SERVICE > Requested Service DateTime
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16635", Name = "Requested Service DateTime", CodeSystem = "NCTIS")]
      RequestedServiceDateTime,

      /// <summary>
      /// Test Specimen Detail
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16156", Name = "Specimen", CodeSystem = "NCTIS")]
      Specimen,

      /// <summary>
      /// Pathology Test Result
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16982", Name = "Document Use Authorisation", Title = "Document Use Authorisation", CodeSystem = "NCTIS")]
      AuthorityToPost,

      /// <summary>
      /// Authoriser Instruction
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16988", Name = "Authoriser Instruction", CodeSystem = "NCTIS")]
      AuthoriserInstruction,

      /// <summary>
      /// Authoriser Instruction Values
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "Post document", CodeSystem = "NCTISAuthoriserInstructionValues")]
      PostDocument,

      /// <summary>
      /// Document Instance Identifier
      /// </summary>
      [EnumMember]
      [Name(Code = "103.20101", Name = "Document Instance Identifier", Title = "Document Instance Identifier", CodeSystem = "NCTIS")]
      DocumentInstanceIdentifier,

      /// <summary>
      /// Service Request Identifier
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16986", Name = "Service Request Identifier", Title = "Service Request Identifier", CodeSystem = "NCTIS")]
      ServiceRequestIdentifier,

      /// <summary>
      /// Pathology study
      /// </summary>
      [EnumMember]
      [Name(Code = "11526-1", Name = "Pathology study", CodeSystem = "LOINC")]
      PathologyStudy,
    }
  }

