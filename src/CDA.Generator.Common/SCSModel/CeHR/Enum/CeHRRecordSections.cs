using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
      /// <summary>
    /// CeHR RecordSections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum CeHRRecordSections
    {
      /// <summary>
      /// Physical Measurements
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16491", Name = "Physical Measurements", Title = "Physical Measurements", CodeSystem = "NCTIS")]
      PhysicalMeasurements,

      /// <summary>
      /// Consumer Entered Measurement Information
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "Consumer Entered Measurement Information", CodeSystem = "PCEHRCustomDataComponent")]
      ConsumerEnteredMeasurementInformation,

      /// <summary>
      /// Provider Entered Measurement Information
      /// </summary>
      [EnumMember]
      [Name(Code = "2", Name = "Provider Entered Measurement Information", CodeSystem = "PCEHRCustomDataComponent")]
      ProviderEnteredMeasurementInformation,

      /// <summary>
      /// Head Circumference
      /// </summary>
      [EnumMember]
      [Name(Code = "363812007", Name = "Head circumference", Title = "Head Circumference", CodeSystem = "SNOMED")]
      HeadCircumference,

      /// <summary>
      /// Body Height Measure
      /// </summary>
      [EnumMember]
      [Name(Code = "50373000", Name = "Body height measure", Title = "Body Height", CodeSystem = "SNOMED")]
      BodyHeight,

      /// <summary>
      /// Body Weight
      /// </summary>
      [EnumMember]
      [Name(Code = "27113001", Name = "Body weight", Title = "Body Weight", CodeSystem = "SNOMED")]
      BodyWeight,

      /// <summary>
      /// Body Mass Index
      /// </summary>
      [EnumMember]
      [Name(Code = "60621009", Name = "Body mass index", Title = "Body Mass Index", CodeSystem = "SNOMED")]
      BodyMassIndex,

      /// <summary>
      /// Head Circumference
      /// </summary>
      [EnumMember]
      [Name(Code = "363812007", Name = "Head circumference", Title = "Head Circumference", CodeSystem = "SNOMEDCT")]
      CTHeadCircumference,

      /// <summary>
      /// Body Height Measure
      /// </summary>
      [EnumMember]
      [Name(Code = "50373000", Name = "Body height measure", Title = "Body Height", CodeSystem = "SNOMEDCT")]
      CTBodyHeight,

      /// <summary>
      /// Body Weight
      /// </summary>
      [EnumMember]
      [Name(Code = "27113001", Name = "Body weight", Title = "Body Weight", CodeSystem = "SNOMEDCT")]
      CTBodyWeight,

      /// <summary>
      /// Body Mass Index
      /// </summary>
      [EnumMember]
      [Name(Code = "60621009", Name = "Body mass index", Title = "Body Mass Index", CodeSystem = "SNOMEDCT")]
      CTBodyMassIndex,

      /// <summary>
      /// Birth Details 
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16939", Name = "Birth details", Title = "Birth Details", CodeSystem = "NCTIS")]
      BirthDetails,

      /// <summary>
      /// Document Link
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.179.1.2", Name = "Record Link", CodeSystem = "NCTIS")]
      RecordLink,

      /// <summary>
      /// PCEHR_AUTID
      /// </summary>
      [EnumMember]
      [Name(Code = "PCEHR_AUTID", Name = "authorname", CodeSystem = "PCEHR_ControlCodes")]
      PCEHR_AUTID,

      /// <summary>
      /// IncompleteFlag
      /// </summary>
      [EnumMember]
      [Name(Code = "I", Name = "IncompleteFlag", CodeSystem = "PCEHRAssignedIdentifierRepository")]
      IncompleteFlag,

      /// <summary>
      /// PCEHR_DOCTIME
      /// </summary>
      [EnumMember]
      [Name(Code = "PCEHR_DOCTIME", Name = "Document Date",  CodeSystem = "PCEHR_ControlCodes")]
      PCEHR_DOCTIME,

      /// <summary>
      /// Body Height Measure
      /// </summary>
      [EnumMember]
      [Name(Code = "50373000", Name = "Body height measure", CodeSystem = "SNOMED")]
      BodyHeightMeasure,

      /// <summary>
      /// Percentile
      /// </summary>
      [EnumMember]
      [Name(Code = "415068004", Name = "Percentile value", CodeSystem = "SNOMED")]
      Percentile,

      /// <summary>
      /// Exclusion Statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.172.1.1", Name = "Exclusion Statement", Title = "Exclusion Statement", CodeSystem = "NCTIS")]
      ExclusionStatement,

      /// <summary>
      /// Global Statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16135.172.1.1", Name = "Global Statement", CodeSystem = "NCTIS")]
      GlobalStatement
    }
  }

