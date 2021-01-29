using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
 {
    /// <summary>
    /// Service Referral Sections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ServiceReferralSections
    {
      /// <summary>
      /// Service Referral Detail
      /// </summary>
      [EnumMember]
      [Name(Code = "101.17032", Name = "Service Referral Detail", Title = "Referral Detail", CodeSystem = "NCTIS")]
      ServiceReferralDetail,

      /// <summary>
      /// Service Description
      /// </summary>
      [EnumMember]
      [Name(Code = "103.20117", Name = "Service Description", CodeSystem = "NCTIS")]
      ServiceDescription,

      /// <summary>
      /// Request Urgency
      /// </summary>
      [EnumMember]
      [Name(Code = "01", Name = "Urgent", CodeSystem = "NCTISRequestUrgencyValues")]
      RequestUrgency,

      /// <summary>
      /// Request Urgency Notes
      /// </summary>
      [EnumMember]
      [Name(Code = "103.17022", Name = "Request Urgency Notes", CodeSystem = "NCTIS")]
      RequestUrgencyNotes,

      /// <summary>
      /// Service Comment
      /// </summary>
      [EnumMember]
      [Name(Code = "103.17035", Name = "Service Comment", CodeSystem = "NCTIS")]
      ServiceComment,

        /// <summary>
        /// Request Validity Period
        /// </summary>
        [EnumMember]
      [Name(Code = "103.16132", Name = "Request Validity Period", CodeSystem = "NCTIS")]
      RequestValidityPeriod,

      /// <summary>
      /// Requested Service Datetime
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16635", Name = "Requested Service DateTime", CodeSystem = "NCTIS")]
      RequestedServiceDatetime,

      /// <summary>
      /// Interpreter Required
      /// </summary>
      [EnumMember]
      [Name(Code = "102.17040", Name = "Interpreter Required", CodeSystem = "NCTIS")]
      InterpreterRequired,

      /// <summary>
      /// Related Document
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16971", Name = "Related Document", CodeSystem = "NCTIS")]
      RelatedDocument,

      /// <summary>
      /// Document Title
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16966", Name = "Document Title", CodeSystem = "NCTIS")]
      DocumentTitle,

      /// <summary>
      /// Current Services
      /// </summary>
      [EnumMember]
      [Name(Code = "101.21021", Name = "Current Services", CodeSystem = "NCTIS", Title = "Current Services")]
      CurrentServices,

      /// <summary>
      /// Subject of Care Instruction Description
      /// </summary>
      [EnumMember]
      [Name(Code = "103.10146", Name = "Subject of Care Instruction Description", CodeSystem = "NCTIS")]
      SubjectOfCareInstructionDescription,

      /// <summary>
      /// Current Services
      /// </summary>
      [EnumMember]
      [Name(Code = "102.20158", Name = "Requested Service", CodeSystem = "NCTIS")]
      RequestedService

    }
}

