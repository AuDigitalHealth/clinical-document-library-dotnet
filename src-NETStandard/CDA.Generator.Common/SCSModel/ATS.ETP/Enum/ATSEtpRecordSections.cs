using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Enum
{
    /// <summary>
    /// ATS Etp Record Section
    /// </summary>
    [Serializable]
    [DataContract]
     public enum ATSEtpRecordSection
    {
      /// <summary>
      /// Additional Comments
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16044", Name = "Additional Comments", Title = "Additional Comments", CodeSystem = "NCTIS")]
      AdditionalComments,

      /// <summary>
      /// Administrative Observations 
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16080", Name = "Administrative Observations", Title = "Administrative Observations", CodeSystem = "NCTIS")]
      AdministrativeObservations,

      /// <summary>
      /// Administrative Observations 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.20109", Name = "Age", Title = "Age", CodeSystem = "NCTIS")]
      Age,

      /// <summary>
      /// Age Accuracy Indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16279", Name = "Age Accuracy Indicator", Title = "Age Accuracy Indicator", CodeSystem = "NCTIS")]
      AgeAccuracyIndicator,

      /// <summary>
      /// Birth Plurality
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16249", Name = "Birth Plurality", Title = "Birth Plurality", CodeSystem = "NCTIS")]
      BirthPlurality,

      /// <summary>
      /// Date of Birth Accuracy Indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16234", Name = "Date of Birth Accuracy Indicator", Title = "Date of Birth Accuracy Indicator", CodeSystem = "NCTIS")]
      DateOfBirthAccuracyIndicator,

      /// <summary>
      /// Date Of Birth Is Calculated From Age
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16233", Name = "Date of Birth is Calculated From Age", Title = "Date of Birth is Calculated From Age", CodeSystem = "NCTIS")]
      DateOfBirthIsCalculatedFromAge,

      /// <summary>
      /// DateTime Prescription Expires
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10104", Name = "DateTime Prescription Expires", Title = "DateTime Prescription Expires", CodeSystem = "NCTIS")]
      DateTimePrescriptionExpires,

      /// <summary>
      /// Formula
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16272", Name = "Formula", Title = "Formula", CodeSystem = "NCTIS")]
      Formula,

      /// <summary>
      /// Long-Term
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16440", Name = "Long-Term", Title = "Long-Term", CodeSystem = "NCTIS")]
      LongTerm,

      /// <summary>
      /// Medication Instruction Identifier
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16444", Name = "Medication Instruction Identifier", Title = "Medication Instruction Identifier", CodeSystem = "NCTIS")]
      MedicationInstructionIdentifier,

      /// <summary>
      /// Observations 
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16280", Name = "Observations", Title = "Observations", CodeSystem = "NCTIS")]
      Observations,

      /// <summary>
      /// PBS Close the Gap Benefit  
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16095.3", Name = "PBS Close the Gap Benefit", Title = "PBS Close the Gap Benefit", CodeSystem = "NCTIS")]
      PBSCloseTheGapBenefit ,

      /// <summary>
      /// PBS/RPBS Authority Prescription Number 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16719", Name = "PBS/RPBS Authority Prescription Number", Title = "PBS/RPBS Authority Prescription Number", CodeSystem = "NCTIS")]
      PBSRPBSAuthorityPrescriptionNumber ,

      /// <summary>
      /// PBS/RPBS Authority Approval Number 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10159", Name = "PBS/RPBS Authority Approval Number", Title = "PBS/RPBS Authority Approval Number", CodeSystem = "NCTIS")]
      PBSRPBSAuthorityApprovalNumber,

      /// <summary>
      /// Prescription
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16102", Name = "Prescription", Title = "Prescription", CodeSystem = "NCTIS")]
      Prescription,

      /// <summary>
      /// Prescription Item
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16211", Name = "Prescription Item", Title = "Prescription Item", CodeSystem = "NCTIS")]
      PrescriptionItem,

      /// <summary>
      /// Prescription Request Item
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16211", Name = "Prescription Request Item", Title = "Prescription Request Item", CodeSystem = "NCTIS")]
      PrescriptionRequestItem,

      /// <summary>
      /// Prescription Note Detail
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16212", Name = "Prescription Note Detail", Title = "Prescription Note Detail", CodeSystem = "NCTIS")]
      PrescriptionNoteDetail,

      /// <summary>
      /// Requester Note
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16212", Name = "Requester Note", Title = "Requester Note", CodeSystem = "NCTIS")]
      RequesterNote,

      /// <summary>
      /// Qualifications 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16268", Name = "Qualifications", Title = "Qualifications", CodeSystem = "NCTIS")]
      Qualifications,

      /// <summary>
      /// Reason for Therapeutic Good 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10141", Name = "Reason for Therapeutic Good", Title = "Reason for Therapeutic Good", CodeSystem = "NCTIS")]
      ReasonForTherapeuticGood,

      /// <summary>
      /// State Authority Number 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16018", Name = "State Authority Number", Title = "State Authority Number", CodeSystem = "NCTIS")]
      StateAuthorityNumber ,

      /// <summary>
      /// Start Criterion 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16436", Name = "Start Criterion", Title = "Start Criterion", CodeSystem = "NCTIS")]
      StartCriterion ,

      /// <summary>
      /// Stop Criterion 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16434", Name = "Stop Criterion", Title = "Stop Criterion", CodeSystem = "NCTIS")]
      StopCriterion,

      /// <summary>
      /// Streamlined Authority Approval Number 
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16718", Name = "Streamlined Authority Approval Number", Title = "Streamlined Authority Approval Number", CodeSystem = "NCTIS")]
      StreamlinedAuthorityApprovalNumber,

      /// <summary>
      /// Quantity
      /// </summary>
      [EnumMember]
      [Name(Code = "246205007", Name = "Quantity", CodeSystem = "SNOMED")]
      Quantity,

      /// <summary>
      /// Quantity
      /// </summary>
      [EnumMember]
      [Name(Code = "246205007", Name = "Quantity", CodeSystem = "SNOMEDCT")]
      QuantitySnomedCt,

      /// <summary>
      /// Timing Description
      /// </summary>
      [EnumMember]
      [Name(Code = "246512002", Name = "Timing", CodeSystem = "SNOMEDCT")]
      TimingDescription,

      /// <summary>
      /// Body Height
      /// </summary>
      [EnumMember]
      [Name(Code = "50373000", Name = "Body Height", Title = "Body Weight", CodeSystem = "SNOMED", Version = "20101130")]
      BodyHeight,

      /// <summary>
      /// Body Weight
      /// </summary>
      [EnumMember]
      [Name(Code = "27113001", Name = "Body Weight", Title = "Body Weight", CodeSystem = "SNOMED", Version = "20101130")]
      BodyWeight,
    }
  }

