using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Enum
  {
    /// <summary>
    /// Answers Data
    /// </summary>
    [Serializable]
    [DataContract]
     public enum AnswersData
    {
      /// <summary>
      /// Yes/No
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "Yes/No", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      YesNo,

      /// <summary>
      /// Normal/Refer
      /// </summary>
      [EnumMember]
      [Name(Code = "2", Name = "Normal/Refer", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      NormalRefer,

      /// <summary>
      /// Free Text
      /// </summary>
      [EnumMember]
      [Name(Code = "3", Name = "Free Text", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      FreeText,

      /// <summary>
      /// No/Yes/A Little
      /// </summary>
      [EnumMember]
      [Name(Code = "4", Name = "No/Yes/A Little", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      NoYesALittle,

      /// <summary>
      /// Cannot Recall/Yes/No
      /// </summary>
      [EnumMember]
      [Name(Code = "5", Name = "Cannot Recall/Yes/No", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      CannotRecallYesNo,

      /// <summary>
      /// Date
      /// </summary>
      [EnumMember]
      [Name(Code = "6", Name = "Date", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      Date,

      /// <summary>
      /// Pass/Refer
      /// </summary>
      [EnumMember]
      [Name(Code = "7", Name = "Pass/Refer", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      PassRefer,

      /// <summary>
      /// Pass/Refer
      /// </summary>
      [EnumMember]
      [Name(Code = "8", Name = "Required/Not Required", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      RequiredNotRequired,

      /// <summary>
      /// Telephone Number
      /// </summary>
      [EnumMember]
      [Name(Code = "9", Name = "Telephone Number", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      TelephoneNumber,

      /// <summary>
      /// Normal/Review/Refer
      /// </summary>
      [EnumMember]
      [Name(Code = "10", Name = "Normal/Review/Refer", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      NormalReviewRefer,

      /// <summary>
      /// Number
      /// </summary>
      [EnumMember]
      [Name(Code = "11", Name = "Number", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      Number,

      /// <summary>
      /// Yes/No/Concerns/No Concerns
      /// </summary>
      [EnumMember]
      [Name(Code = "12", Name = "Yes/No/Concerns/No Concerns", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      YesNoConcernsNoConcerns,

      /// <summary>
      /// Normal/Review/Refer/Under Treatment
      /// </summary>
      [EnumMember]
      [Name(Code = "13", Name = "Normal/Review/Refer/Under Treatment", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      NormalReviewReferUnderTreatment,

      /// <summary>
      /// Mother/Father/Unaccompanied/Other
      /// </summary>
      [EnumMember]
      [Name(Code = "14", Name = "Mother/Father/Unaccompanied/Other", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      MotherFatherUnaccompaniedOther,

      /// <summary>
      /// PQ Kg
      /// </summary>
      [EnumMember]
      [Name(Code = "15", Name = "PQ Kg", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      PQKg,

      /// <summary>
      /// PQ Cm
      /// </summary>
      [EnumMember]
      [Name(Code = "16", Name = "PQ Cm", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      PQCm,

      /// <summary>
      /// Yes/No/Unsure
      /// </summary>
      [EnumMember]
      [Name(Code = "16", Name = "Yes/No/Unsure", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      YesNoUnsure,

      /// <summary>
      /// Concerns /No Concerns
      /// </summary>
      [EnumMember]
      [Name(Code = "18", Name = "Concerns /No Concerns", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersData")]
      ConcernsNoConcerns,
    }
  }

