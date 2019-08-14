using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Enum
  {
    /// <summary>
    /// Answers Value
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AnswersValue
    {
      /// <summary>
      /// The yes
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "Yes", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      Yes,

      /// <summary>
      /// The no
      /// </summary>
      [EnumMember]
      [Name(Code = "2", Name = "No", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      No,

      /// <summary>
      /// The concerns
      /// </summary>
      [EnumMember]
      [Name(Code = "3", Name = "Concerns", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      Concerns,

      /// <summary>
      /// The no concerns
      /// </summary>
      [EnumMember]
      [Name(Code = "4", Name = "No Concerns", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      NoConcerns,

      /// <summary>
      /// The normal
      /// </summary>
      [EnumMember]
      [Name(Code = "6", Name = "Normal", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      Normal,

      /// <summary>
      /// The review
      /// </summary>
      [EnumMember]
      [Name(Code = "7", Name = "Review", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      Review,

      /// <summary>
      /// The refer
      /// </summary>
      [EnumMember]
      [Name(Code = "8", Name = "Refer", CodeSystem = "NewSouthWalesChildDevelopmentalAnswersValue")]
      Refer

    }
  }

