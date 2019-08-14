using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// PatholodyResultReportViewSections
    /// </summary>
   [Serializable]
   [DataContract]
   public enum DiagnosticImagingReportSections
    {
      /// <summary>
      /// Diagnostic Imaging
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16945", Name = "DIAGNOSTIC IMAGING", Title = "Diagnostic Imaging", Narrative = "This component contains the subsections Pathology Test Result(s) and Related Information and/or Authority to Post ", CodeSystem = "NCTIS")]
      DiagnosticImaging,

      /// <summary>
      /// Pathology Test Result
      /// </summary>
      [EnumMember]
      [Name(Code = "102.20159", Name = "Authority to Post", Title = "Authority to Post", CodeSystem = "NCTIS")]
      AuthorityToPost,

      /// <summary>
      /// Imaging Examination Result
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16145", Name = "Imaging Examination Result", Title = "Imaging Examination Result", CodeSystem = "NCTIS")]
      ImagingExaminationResult,

      /// <summary>
      /// Report Status
      /// </summary>
      [EnumMember]
      [Name(Code = "308552006", Name = "report status", CodeSystem = "SNOMED", Version = "20110531")]
      ReportStatus,

      /// <summary>
      /// Report Status
      /// </summary>
      [EnumMember]
      [Name(Code = "105.16633", Name = "Examination Procedure", CodeSystem = "NCTIS")]
      ExaminationProcedure,

      /// <summary>
      /// Examination Request Details
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16511", Name = "Examination Request Details", CodeSystem = "NCTIS")]
      ExaminationRequestDetails,

      /// <summary>
      /// Image Details
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16515", Name = "Image Details", CodeSystem = "NCTIS")]
      ImageDetails,

      /// <summary>
      /// Diagnostic imaging study
      /// </summary>
      [EnumMember]
      //[Name(Code = "18748-4", Name = "Diagnostic imaging study", Title = "Diagnostic imaging study", CodeSystem = "LOINC")]
      [Name(Code = "18748-4", Name = "Diagnostic imaging study",  CodeSystem = "LOINC")]
        DiagnosticImagingStudy,

      /// <summary>
      /// With Laterality
      /// </summary>
      [EnumMember]
      [Name(Code = "78615007", Name = "with laterality", CodeSystem = "SNOMED")]
      WithLaterality,

      /// <summary>
      /// Related Information
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692", Name = "Related Information", CodeSystem = "NCTIS")]
      RelatedInformation,

      /// <summary>
      /// Anatomical Site Details
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16996", Name = "Anatomical Site Details", CodeSystem = "NCTIS")]
      AnatomicalSiteDetails,

      /// <summary>
      /// Anatomical Region
      /// </summary>
      [EnumMember]
      [Name(Code = "103.17009", Name = "Anatomical Region", CodeSystem = "NCTIS")]
      AnatomicalRegion

    }
  }

