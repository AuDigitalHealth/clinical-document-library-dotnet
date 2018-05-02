using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// Clinical Document Common Sections
    /// </summary>
   [Serializable]
   [DataContract]
   public enum CommonSections
    {
      /// <summary>
      /// With Laterality
      /// </summary>
      [EnumMember]
      [Name(Code = "78615007", Name = "with laterality", CodeSystem = "SNOMED")]
      WithLaterality
    }
  }

