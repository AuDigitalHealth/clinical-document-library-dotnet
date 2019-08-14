using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// EtpRecordSections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum EtpRecordSections
    {
      /// <summary>
      /// The additional comments
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16044", Name = "Additional Comments", Title = "Additional Comments", CodeSystem = "NCTIS")]
      AdditionalComments,

      /// <summary>
      /// The administrative observations
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16080", Name = "Administrative Observations", Title = "Administrative Observations", CodeSystem = "NCTIS")]
      AdministrativeObservations,

      /// <summary>
      /// The age
      /// </summary>
      [EnumMember]
      [Name(Code = "103.20109", Name = "Age", Title = "Age", CodeSystem = "NCTIS")]
      Age,

      /// <summary>
      /// The age accuracy indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16279", Name = "Age Accuracy Indicator", Title = "Age Accuracy Indicator", CodeSystem = "NCTIS")]
      AgeAccuracyIndicator,

      /// <summary>
      /// The birth plurality
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16249", Name = "Birth Plurality", Title = "Birth Plurality", CodeSystem = "NCTIS")]
      BirthPlurality,

      /// <summary>
      /// The date of birth accuracy indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16234", Name = "Date of Birth Accuracy Indicator", Title = "Date of Birth Accuracy Indicator", CodeSystem = "NCTIS")]
      DateOfBirthAccuracyIndicator,

      /// <summary>
      /// The date of birth is calculated from age
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16233", Name = "Date of Birth is Calculated From Age", Title = "Date of Birth is Calculated From Age", CodeSystem = "NCTIS")]
      DateOfBirthIsCalculatedFromAge,

      /// <summary>
      /// The date time prescription expires
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10104", Name = "DateTime Prescription Expires", Title = "DateTime Prescription Expires", CodeSystem = "NCTIS")]
      DateTimePrescriptionExpires,

      /// <summary>
      /// The formula
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16272", Name = "Formula", Title = "Formula", CodeSystem = "NCTIS")]
      Formula,

      /// <summary>
      /// The therapeutic good strength
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16769.171.1.1", Name = "Therapeutic Good Strength", Title = "Therapeutic Good Strength", CodeSystem = "NCTIS")]
      TherapeuticGoodStrength,

      /// <summary>
      /// The prescription item
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16211", Name = "Prescription Item", Title = "Prescription Item", CodeSystem = "NCTIS")]
      PrescriptionItem,

      /// <summary>
      /// The dispense item
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16210", Name = "Dispense Item", Title = "Dispense Item", CodeSystem = "NCTIS")]
      DispenseItem,

      /// <summary>
      /// The qualifications
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16268", Name = "Qualifications", Title = "Qualifications", CodeSystem = "NCTIS")]
      Qualifications,

      /// <summary>
      /// The reason for therapeutic good
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10141", Name = "Reason for Therapeutic Good", Title = "Reason for Therapeutic Good", CodeSystem = "NCTIS")]
      ReasonForTherapeuticGood,

      /// <summary>
      /// The quantity description
      /// </summary>
      [EnumMember]
      [Name(Code = "246205007", Name = "Quantity", Title = "Quantity", CodeSystem = "SNOMED")]
      QuantityDescription,

      /// <summary>
      /// The unique pharmacy prescription number
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16786", Name = "Unique Pharmacy Prescription Number", Title = "Unique Pharmacy Prescription Number", CodeSystem = "NCTIS")]
      UniquePharmacyPrescriptionNumber,

      /// <summary>
      /// The label instruction
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16109", Name = "Label Instruction", Title = "Label Instruction", CodeSystem = "NCTIS")]
      LabelInstruction,

      /// <summary>
      /// The brand substitution occured
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16064", Name = "Brand Substitution Occurred", Title = "Brand Substitution Occurred", CodeSystem = "NCTIS")]
      BrandSubstitutionOccured
    }
  }

