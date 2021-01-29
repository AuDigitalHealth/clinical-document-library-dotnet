using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// PatholodyResultReportViewSections
    /// </summary>
   [Serializable]
   [DataContract]
   public enum AdvanceCareInformationSections
    {
      /// <summary>
      /// Pathology
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16973", Name = "Advance Care Information Section", Title = "Advance Care Information Section", Narrative = "This component contains the subsections Related Document and Document Provenance", CodeSystem = "NCTIS")]
       AdvanceCareInformationSection,

      /// <summary>
      /// Related Document
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16971", Name = "Related Document",  CodeSystem = "NCTIS")]
      RelatedDocument,

      /// <summary>
      /// LinkNature
      /// </summary>
      [EnumMember]
      [Name(Code = "LINK-E0", Name = "The target [instance of a DCM or document] is an alternative documentary form of the source [DCM instance], such as re-expression of the same clinical information or additional supplementary explanatory information.",  CodeSystem = "ISO1360632009")]
      LinkNature,

      /// <summary>
      /// Document Title
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16720", Name = "Document Provenance", CodeSystem = "NCTIS")]
      DocumentProvenance,

      /// <summary>
      /// Document Title
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16966", Name = "Document Title", CodeSystem = "NCTIS")]
      DocumentTitle,

      /// <summary>
      /// Document Title
      /// </summary>
      [EnumMember]
      [Name(Code = "103.20104", Name = "Document Status", CodeSystem = "NCTIS")]
      DocumentStatus,
    }
  }

