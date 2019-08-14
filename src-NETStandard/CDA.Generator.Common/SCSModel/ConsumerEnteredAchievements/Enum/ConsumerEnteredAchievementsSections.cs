using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// Consumer Entered Achievements Sections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ConsumerEnteredAchievementsSections
    {
      /// <summary>
      /// Achievements
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16815", Name = "Achievements", CodeSystem = "NCTIS", Identifier = "1.2.36.1.2001.1001.101.101.16815")]
      Achievements,

      /// <summary>
      /// Achievement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16823", Name = "Achievement", CodeSystem = "NCTIS", Identifier = "1.2.36.1.2001.1001.101.102.16823")]
      Achievement,

    }
  }

