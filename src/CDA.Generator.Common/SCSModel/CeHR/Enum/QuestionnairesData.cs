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
    public enum QuestionnairesData
    {
      /// <summary>
      /// NSW Family health history and risk factors
      /// </summary>
      [EnumMember]
      [Name(Code = "01", Name = "NSW Family health history and risk factors", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWFamilyHealthHistoryAndRiskFactors,

      /// <summary>
      /// NSW Questions for parents about hearing
      /// </summary>
      [EnumMember]
      [Name(Code = "02", Name = "NSW Questions for parents about hearing", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParentsAboutHearing,

      /// <summary>
      /// NSW Questions for parents - 1 to 4 week check
      /// </summary>
      [EnumMember]
      [Name(Code = "03", Name = "NSW Questions for parents - 1 to 4 week check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents1To4WeekCheck,

      /// <summary>
      /// NSW Questions for parents - 6 to 8 week check
      /// </summary>
      [EnumMember]
      [Name(Code = "04", Name = "NSW Questions for parents - 6 to 8 week check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents6To8WeekCheck,

      /// <summary>
      /// NSW Questions for parents - 6 month check
      /// </summary>
      [EnumMember]
      [Name(Code = "05", Name = "NSW Questions for parents - 6 month check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents6MonthCheck,

      /// <summary>
      /// NSW Questions for parents - 12 month check
      /// </summary>
      [EnumMember]
      [Name(Code = "06", Name = "NSW Questions for parents - 12 month check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents12MonthCheck,

      /// <summary>
      /// NSW Questions for parents - 18 month check
      /// </summary>
      [EnumMember]
      [Name(Code = "07", Name = "NSW Questions for parents - 18 month check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents18MonthCheck,

      /// <summary>
      /// NSW Questions for parents - 2 year check
      /// </summary>
      [EnumMember]
      [Name(Code = "08", Name = "NSW Questions for parents - 2 year check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents2YearCheck,

      /// <summary>
      /// NSW Questions for parents - 3 year check
      /// </summary>
      [EnumMember]
      [Name(Code = "09", Name = "NSW Questions for parents - 3 year check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents3YearCheck,

      /// <summary>
      /// NSW Questions for parents - 4 year check
      /// </summary>
      [EnumMember]
      [Name(Code = "10", Name = "NSW Questions for parents - 4 year check", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Health Check Assessment")]
      NSWQuestionsForParents4YearCheck,

      /// <summary>
      /// NSW New born exam
      /// </summary>
      [EnumMember]
      [Name(Code = "11", Name = "NSW New born exam", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")] 
      NSWNewBornExam,

      /// <summary>
      /// NSW Birth details
      /// </summary>
      [EnumMember]
      [Name(Code = "12", Name = "NSW Birth details", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWBirthDetails,

      /// <summary>
      /// NSW Birth details
      /// </summary>
      [EnumMember]
      [Name(Code = "14", Name = "NSW Child health check - 6 to 8 week", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck6To8Week,

      /// <summary>
      /// NSW Child health check - 6 month
      /// </summary>
      [EnumMember]
      [Name(Code = "15", Name = "NSW Child health check - 6 month", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck6Month,

      /// <summary>
      /// NSW Child health check - 12 month
      /// </summary>
      [EnumMember]
      [Name(Code = "16", Name = "NSW Child health check - 12 month", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck12Month,

      /// <summary>
      /// NSW Child health check - 18 month
      /// </summary>
      [EnumMember]
      [Name(Code = "17", Name = "NSW Child health check - 18 month", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck18Month,

      /// <summary>
      /// NSW Child health check - 2 Years
      /// </summary>
      [EnumMember]
      [Name(Code = "18", Name = "NSW Child health check - 2 year", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck2Years,

      /// <summary>
      /// NSW Child health check - 3 Years
      /// </summary>
      [EnumMember]
      [Name(Code = "19", Name = "NSW Child health check - 3 year", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck3Years,

      /// <summary>
      /// NSW Child health check - 4 Years
      /// </summary>
      [EnumMember]
      [Name(Code = "20", Name = "NSW Child health check - 4 year", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWChildHealthCheck4Years,

      /// <summary>
      /// NSW State Infant Screening - Hearing (SWISH)
      /// </summary>
      [EnumMember]
      [Name(Code = "21", Name = "NSW State Infant Screening - Hearing (SWISH)", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionnairesData", Title = "Child Parent Questionnaire")]
      NSWStateInfantScreeningHearingSWISH,
    }
  }

