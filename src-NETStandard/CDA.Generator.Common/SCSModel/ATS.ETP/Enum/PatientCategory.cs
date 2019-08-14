using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Enum
  {
    /// <summary>
    /// PatientCategory
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PatientCategory
    {
      /// <summary>
      /// continued dispensing patient
      /// </summary>
      [EnumMember]
      [Name(Code = "D", Name = "continued dispensing patient")]
      ContinuedDispensingPatient,

      /// <summary>
      /// paperless private hospital patient
      /// </summary>
      [EnumMember]
      [Name(Code = "H", Name = "paperless private hospital patient")]
      PaperlessPrivateHospitalPatient,

      /// <summary>
      /// public hospital patient
      /// </summary>
      [EnumMember]
      [Name(Code = "B", Name = "public hospital patient")]
      PublicHospitalPatient,

      /// <summary>
      /// nursing home patient
      /// </summary>
      [EnumMember]
      [Name(Code = "N", Name = "nursing home patient")]
      NursingHomePatient,

      /// <summary>
      /// paperless public hospital patient
      /// </summary>
      [EnumMember]
      [Name(Code = "C", Name = "paperless public hospital patient")]
      PaperlessPublicHospitalPatient,

      /// <summary>
      /// community patient
      /// </summary>
      [EnumMember]
      [Name(Code = "0", Name = "community patient")]
      CommunityPatient,

      /// <summary>
      /// hospital patient not identified by any of the above(R) residential aged care facility patient (paperless)
      /// </summary>
      [EnumMember]
      [Name(Code = "1", Name = "hospital patient not identified by any of the above(R) residential aged care facility patient (paperless)")]
      HospitalPatientNotIdentifiedByAnyOfTheAbove,
    }
  }

