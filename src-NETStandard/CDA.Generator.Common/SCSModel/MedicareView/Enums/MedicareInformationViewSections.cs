using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// VaccineCancellationReasonTypeValues
    /// </summary>
    [Serializable]
    [DataContract]
   public enum VaccineCancellationReasonTypeValues
    {
      /// <summary>
      /// Natural Immunity
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "Natural Immunity", Comment = "The subject has developed a natural immunity to the antigen")]
      NaturalImmunity,

      /// <summary>
      /// Medical Contraindication
      /// </summary>
      [EnumMember]
      [Name(Code = "2", Name = "Medical Contraindication", Comment = "The subject displayed contraindications to administering the vaccine", CodeSystem = "NCTIS")]
      MedicalContraindication,
    }
  }

