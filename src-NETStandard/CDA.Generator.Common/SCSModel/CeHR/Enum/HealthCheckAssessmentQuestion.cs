using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Enum
  {
    /// <summary>
    /// Assesment
    /// </summary>
    [Serializable]
    [DataContract]
     public enum HealthCheckAssessmentQuestion
    {
      /// <summary>
      /// Head circumference
      /// </summary>
      [EnumMember]
      [Name(Code = "0162", Name = "Head Circumference", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      HeadCircumference,

      /// <summary>
      /// Body height
      /// </summary>
      [EnumMember]
      [Name(Code = "0163", Name = "Body Height", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      BodyHeight,

      /// <summary>
      /// Body weight
      /// </summary>
      [EnumMember]
      [Name(Code = "0162", Name = "Body Weight", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      BodyWeight,

      /// <summary>
      /// Body mass index
      /// </summary>
      [EnumMember]
      [Name(Code = "0151", Name = "Body mass index (BMI)", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      BodyMassIndex,

      /// <summary>
      /// Eyes observation
      /// </summary>
      [EnumMember]
      [Name(Code = "0143", Name = "Eyes: Observation", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      EyesObservation,

      /// <summary>
      /// Eyes fixation
      /// </summary>
      [EnumMember]
      [Name(Code = "0144", Name = "Eyes: Fixation", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      EyesFixation,

      /// <summary>
      /// Eyes corneal reflexes
      /// </summary>
      [EnumMember]
      [Name(Code = "0145", Name = "Eyes: Corneal reflexes", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      EyesCornealReflexes,

      /// <summary>
      /// Eyes response to each eye being covered separately
      /// </summary>
      [EnumMember]
      [Name(Code = "0146", Name = "Eyes: Response to each eye being covered separately", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      EyesResponseToEachEyeBeingCoveredSeparately,

      /// <summary>
      /// Eyes ocular movements
      /// </summary>
      [EnumMember]
      [Name(Code = "0148", Name = "Eyes: Ocular movements", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      EyesOcularMovements,

      /// <summary>
      /// Oral health lift the lip check
      /// </summary>
      [EnumMember]
      [Name(Code = "0149", Name = "Oral health 'Lift the lip' check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      OralHealthLiftTheLipCheck,

      /// <summary>
      /// Parent questions completed
      /// </summary>
      [EnumMember]
      [Name(Code = "0131", Name = "Parent questions completed?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      ParentQuestionsCompleted,

      /// <summary>
      /// Age appropriate immunisation completed as per schedule
      /// </summary>
      [EnumMember]
      [Name(Code = "0147", Name = "Age appropriate immunisation completed as per schedule?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AgeAppropriateImmunisationCompletedAsPerSchedule,

      /// <summary>
      /// Are there any risk factors hearing
      /// </summary>
      [EnumMember]
      [Name(Code = "0133", Name = "Are there any risk factors: Hearing", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AreThereAnyRiskFactorsHearing,

      /// <summary>
      /// Are there any risk factors vision
      /// </summary>
      [EnumMember]
      [Name(Code = "0134", Name = "Are there any risk factors: Vision", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AreThereAnyRiskFactorsVision,

      /// <summary>
      /// Are there any risk factors oral health
      /// </summary>
      [EnumMember]
      [Name(Code = "0136", Name = "Are there any risk factors: Oral Health", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AreThereAnyRiskFactorsOralHealth,

      /// <summary>
      /// Outcome
      /// </summary>
      [EnumMember]
      [Name(Code = "0015", Name = "Outcome", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      Outcome,

      /// <summary>
      /// Appropriate health information discussed
      /// </summary>
      [EnumMember]
      [Name(Code = "0137", Name = "Appropriate health information discussed?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AppropriateHealthInformationDiscussed,

      /// <summary>
      /// Comments
      /// </summary>
      [EnumMember]
      [Name(Code = "0138", Name = "Comments", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      Comments,

      /// <summary>
      /// Action taken
      /// </summary>
      [EnumMember]
      [Name(Code = "0139", Name = "Action taken", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      ActionTaken,

      /// <summary>
      /// Name of doctor or nurse
      /// </summary>
      [EnumMember]
      [Name(Code = "0140", Name = "Name of doctor or nurse", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      NameOfDoctorOrNurse,

      /// <summary>
      /// Venue
      /// </summary>
      [EnumMember]
      [Name(Code = "0141", Name = "Venue", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      Venue,

      /// <summary>
      /// Date of check
      /// </summary>
      [EnumMember]
      [Name(Code = "0142", Name = "Date of check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      DateOfCheck,
    }
  }

