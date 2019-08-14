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
    public enum ChildDevelopmentalQuestionData
    {
      /// <summary>
      /// Have you completed the health risk fact or questions on page3_2
      /// </summary>
      [EnumMember]
      [Name(Code = "0007", Name = "Have you completed the health risk factor questions on page 3.2?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      HaveYouCompletedTheHealthRiskFactOrQuestionsOnPage3_2,

      /// <summary>
      /// Are you concerned about your babys hearing
      /// </summary>
      [EnumMember]
      [Name(Code = "0016", Name = "Are you concerned about your baby's hearing?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AreYouConcernedAboutYourBabysHearing,

      /// <summary>
      /// Is anyone else concerned about your babys hearing
      /// </summary>
      [EnumMember]
      [Name(Code = "0017", Name = "Is anyone else concerned about your baby's hearing?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      IsAnyoneElseConcernedAboutYourBabysHearing,

      /// <summary>
      /// Are you concerned about your babys vision
      /// </summary>
      [EnumMember]
      [Name(Code = "0018", Name = "Are you concerned about your baby's vision?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      AreYouConcernedAboutYourBabysVision,

      /// <summary>
      /// Is your baby exposed to smoking in the home or car
      /// </summary>
      [EnumMember]
      [Name(Code = "0019", Name = "Is your baby exposed to smoking in the home or car?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      IsYourBabyExposedToSmokingInTheHomeOrCar,

      /// <summary>
      /// Is your baby placed on his her back for sleeping
      /// </summary>
      [EnumMember]
      [Name(Code = "0020", Name = "Is your baby placed on his/her back for sleeping?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      IsYourBabyPlacedOnHisHerBackForSleeping,

      /// <summary>
      /// Since this time yesterday did your baby receive breast milk
      /// </summary>
      [EnumMember]
      [Name(Code = "0021", Name = "Since this time yesterday, did your baby receive breast milk?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      SinceThisTimeYesterdayDidYourBabyReceiveBreastMilk,

      /// <summary>
      /// Since this time yesterday did your baby receive vitamins or mineral supplements or medicine
      /// </summary>
      [EnumMember]
      [Name(Code = "0022", Name = "Since this time yesterday, did your baby receive Vitamins OR mineral supplements OR medicine (if required)", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      SinceThisTimeYesterdayDidYourBabyReceiveVitaminsOrMineralSupplementsOrMedicine,

      /// <summary>
      /// Since this time yesterday did your baby receive plain water or sweetened flavoured water or fruit juice or tea infusions
      /// </summary>
      [EnumMember]
      [Name(Code = "0023", Name = "Since this time yesterday, did your baby receive Plain water OR sweetened / flavoured water OR fruit juice OR tea / infusions", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      SinceThisTimeYesterdayDidYourBabyReceivePlainWaterOrSweetenedFlavouredWaterOrFruitJuiceOrTeaInfusions,

      /// <summary>
      /// Since this time yesterday did your baby receive infant formula or other milk EG cows milk soy milk evaporated milk condensed milk etc
      /// </summary>
      [EnumMember]
      [Name(Code = "0024", Name = "Since this time yesterday, did your baby receive Infant formula OR other milk (e.g. cows milk, soy milk, evaporated milk, condensed milk etc.) ?", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      SinceThisTimeYesterdayDidYourBabyReceiveInfantFormulaOrOtherMilkEGCowsMilkSoyMilkEvaporatedMilkCondensedMilkEtc,

      /// <summary>
      /// Since this time yesterday did your baby receive solid or semisolid food
      /// </summary>
      [EnumMember]
      [Name(Code = "0025", Name = "Since this time yesterday, did your baby receive Solid OR semi-solid food", CodeSystem = "NewSouthWalesChildDevelopmentalQuestionData")]
      SinceThisTimeYesterdayDidYourBabyReceiveSolidOrSemisolidFood,

    }
  }

