using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Enum
  {
    /// <summary>
    /// PBSPrescriptionTypeValues
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PBSPrescriptionTypeValues
    {
      /// <summary>
      /// PBS/RPBS Standard Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "PBS/RPBS Standard Prescription")]
      PbsRpbsStandardPrescription,

      /// <summary>
      /// PBS/RPBS Authority Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "2", Name = "PBS/RPBS Authority Prescription")]
      PbsRpbsAuthorityPrescription,
  
      /// <summary>
      /// PBS/RPBS Dental Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "3", Name = "PBS/RPBS Dental Prescription")]
      PbsRpbsDentalPrescription,

       /// <summary>
      /// PBS/RPBS Optometrist Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "4", Name = "PBS/RPBS Optometrist Prescription")]
      PbsRpbsOptometristPrescription,
 
      /// <summary>
      /// PBS/RPBS Optometrist Authority Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "5", Name = "PBS/RPBS Optometrist Authority Prescription")]
      PbsRpbsOptometristAuthorityPrescription,

      /// <summary>
      /// PBS/RPBS Optometrist Authority Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "6", Name = "PBS/RPBS Nurse Practitioner and Midwives Prescription")]
      PbsRpbsNursePractitionerAndMidwivesPrescription,

      /// <summary>
      /// PBS/RPBS Nurse Practitioner and Midwives Authority Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "7", Name = "PBS/RPBS Nurse Practitioner and Midwives Authority Prescription")]
      PbsRpbsNursePractitionerAndMidwivesAuthorityPrescription,

      /// <summary>
      /// PBS/RPBS Public Hospital Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "8", Name = "PBS/RPBS Public Hospital Prescription")]
      PbsRpbsPublicHospitalPrescription,

      /// <summary>
      /// PBS/RPBS Public Hospital Dental Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "9", Name = "PBS/RPBS Public Hospital Dental Prescription")]
      PbsRpbsPublicHospitalDentalPrescription,

      /// <summary>
      /// PBS/RPBS Emergency Treatment Supply
      /// </summary>
      [EnumMember]
      [Name(Code = "10", Name = "PBS/RPBS Emergency Treatment Supply")]
      PbsRpbsEmergencyTreatmentSupply,

      /// <summary>
      /// Non PBS/RPBS Prescription (Private)
      /// </summary>
      [EnumMember]
      [Name(Code = "11", Name = "Non PBS/RPBS Prescription (Private)")]
      NonPbsRpbsPrescriptionPrivate,
    }
  }

