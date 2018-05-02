using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
   /// Anatomical Region
    /// </summary>
   [Serializable]
   [DataContract]
   public enum AnatomicalRegion
    {
      /// <summary>
      /// Head
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "Head", CodeSystem = "NCTISAnatomicalRegionValues")]
      Head,

      /// <summary>
      /// Neck
      /// </summary>
      [EnumMember]
      [Name(Code = "2", Name = "Neck", CodeSystem = "NCTISAnatomicalRegionValues")]
      Neck,

      /// <summary>
      /// Chest
      /// </summary>
      [EnumMember]
      [Name(Code = "3", Name = "Chest", CodeSystem = "NCTISAnatomicalRegionValues")]
      Chest,

      /// <summary>
      /// Cardiac
      /// </summary>
      [EnumMember]
      [Name(Code = "4", Name = "Cardiac", CodeSystem = "NCTISAnatomicalRegionValues")]
      Cardiac,

      /// <summary>
      /// Breast
      /// </summary>
      [EnumMember]
      [Name(Code = "5", Name = "Breast", CodeSystem = "NCTISAnatomicalRegionValues")]
      Breast,

      /// <summary>
      /// Abdomen
      /// </summary>
      [EnumMember]
      [Name(Code = "6", Name = "Abdomen", CodeSystem = "NCTISAnatomicalRegionValues")]
      Abdomen,

      /// <summary>
      /// Pelvis
      /// </summary>
      [EnumMember]
      [Name(Code = "7", Name = "Pelvis", CodeSystem = "NCTISAnatomicalRegionValues")]
      Pelvis,

      /// <summary>
      /// Upper Limb
      /// </summary>
      [EnumMember]
      [Name(Code = "8", Name = "Upper Limb", CodeSystem = "NCTISAnatomicalRegionValues")]
      UpperLimb,

      /// <summary>
      /// Lower Limb
      /// </summary>
      [EnumMember]
      [Name(Code = "9", Name = "Lower Limb", CodeSystem = "NCTISAnatomicalRegionValues")]
      LowerLimb,

      /// <summary>
      /// Cervical Spine
      /// </summary>
      [EnumMember]
      [Name(Code = "10", Name = "Cervical Spine", CodeSystem = "NCTISAnatomicalRegionValues")]
      CervicalSpine,

      /// <summary>
      /// Thoracic Spine
      /// </summary>
      [EnumMember]
      [Name(Code = "11", Name = "Thoracic Spine", CodeSystem = "NCTISAnatomicalRegionValues")]
      ThoracicSpine,

      /// <summary>
      /// Lumbar Spine
      /// </summary>
      [EnumMember]
      [Name(Code = "12", Name = "Lumbar Spine", CodeSystem = "NCTISAnatomicalRegionValues")]
      LumbarSpine,

      /// <summary>
      /// Whole Body
      /// </summary>
      [EnumMember]
      [Name(Code = "13", Name = "Whole Body", CodeSystem = "NCTISAnatomicalRegionValues")]
      WholeBody,
    }
  }

