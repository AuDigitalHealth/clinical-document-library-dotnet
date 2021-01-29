using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// DocumentProvenance
    /// </summary>
    [Serializable]
    [DataContract]
     public enum MedicareOverviewSections
    {
      /// <summary>
      /// The australian organ donor register details document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.172.1.2", Name = "Australian Organ Donor Register Details Document Link", CodeSystem = "NCTIS")]
      AustralianOrganDonorRegisterDetailsDocumentLink,

      /// <summary>
      /// The australian organ donor register entry
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16652", Name = "Australian Organ Donor Register Entry", CodeSystem = "NCTIS")]
      AustralianOrganDonorRegisterEntry,

      /// <summary>
      /// The australian organ donor register details
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16670", Name = "Australian Organ Donor Register Details", Title = "Australian Organ Donor Register - AODR", CodeSystem = "NCTIS")]
      AustralianOrganDonorRegisterDetails,

      /// <summary>
      /// The australian childhood immunisation register entries
      /// </summary>
      [EnumMember]
      [Name(Code = "101.17039", Name = "Australian Immunisation Register Entries", Title = "Australian Immunisation Register - AIR", CodeSystem = "NCTIS")]
      AustralianChildhoodImmunisationRegisterEntries,

      /// <summary>
      /// Vaccine Cancellation Reason
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16748", Name = "Vaccine Cancellation Reason", CodeSystem = "NCTIS")]
      VaccineCancellationReason,

      /// <summary>
      /// The medicare DVA funded services history
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16780", Name = "Medicare/DVA Funded Services History", Title = "Medicare Services - MBS and DVA Items", CodeSystem = "NCTIS")]
      MedicareDVAFundedServicesHistory,

      /// <summary>
      /// The pharmaceutical benefit history
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16778", Name = "Pharmaceutical Benefits History", Title = "Prescription Information - PBS and RPBS", CodeSystem = "NCTIS")]
      PharmaceuticalBenefitHistory,

      /// <summary>
      /// The pharmaceutical benefits items
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16649", Name = "Pharmaceutical Benefit Items", Title = "Prescription Information - PBS and RPBS", CodeSystem = "NCTIS")]
      PharmaceuticalBenefitsItems,

      /// <summary>
      /// The medicare DVA funded services
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16643", Name = "Medicare/DVA Funded Services", Title = "Medicare Services - MBS and DVA Items", CodeSystem = "NCTIS")]
      MedicareDVAFundedServices,

      /// <summary>
      /// The pharmaceutical benefits item
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16674", Name = "Pharmaceutical Benefit Item", CodeSystem = "NCTIS")]
      PharmaceuticalBenefitsItem,

      /// <summary>
      /// The medicare MBSDVA item
      /// </summary>
      [EnumMember]
      [Name(Code = "11709", Name = "Australian MBS Code", CodeSystem = "MBSCode")]
      MedicareMBSDVAItem,

      /// <summary>
      /// The australian childhood immunisation register history
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16776", Name = "Australian Immunisation Register History", Title = "Australian Immunisation Register - AIR", CodeSystem = "NCTIS")]
      AustralianChildhoodImmunisationRegisterHistory,

      /// <summary>
      /// The australian organ donor register component
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16774", Name = "Australian Organ Donor Register Decision Information", Title = "Australian Organ Donor Register - AODR", CodeSystem = "NCTIS")]
      AustralianOrganDonorRegisterComponent,

      /// <summary>
      /// The item form and strength
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16677", Name = "Item Form and Strength", CodeSystem = "NCTIS")]
      ItemFormAndStrength,

      /// <summary>
      /// The service in hospital indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16642", Name = "Service In Hospital Indicator", CodeSystem = "NCTIS")]
      ServiceInHospitalIndicator,

      /// <summary>
      /// The medicare DVA funded services document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.172.1.4", Name = "Medicare/DVA Funded Services Document Provenance", CodeSystem = "NCTIS")]
      MedicareDVAFundedServicesDocumentLink,

      /// <summary>
      /// The pharmaceutical benefit items document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.172.1.3", Name = "Pharmaceutical Benefit Items Document Provenance", CodeSystem = "NCTIS")]
      PharmaceuticalBenefitItemsDocumentLink,

      /// <summary>
      /// The australian childhood immunisation register entries document provenance
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16692.172.1.1", Name = "Australian Immunisation Register Entries Document Link", CodeSystem = "NCTIS")]
      AustralianChildhoodImmunisationRegisterEntriesDocument,

      /// <summary>
      /// The medicare overview exclusion statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.172.1.3", Name = "Medicare Overview Exclusion Statement", CodeSystem = "NCTIS")]
      MedicareOverviewExclusionStatement,

      /// <summary>
      /// The pharmaceutical benefit items exclusion statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.172.1.4", Name = "Exclusion Statement", CodeSystem = "NCTIS")]
      PharmaceuticalBenefitItemsExclusionStatement,

      /// <summary>
      /// The Medicare DVA funded services exclusion statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.172.1.5", Name = "Exclusion Statement", CodeSystem = "NCTIS")]
      MedicareDVAFundedServicesExclusionStatement,

      /// <summary>
      /// The Australian organ donor register details exclusion statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.172.1.2", Name = "Exclusion Statement", CodeSystem = "NCTIS")]
      AustralianOrganDonorRegisterDetailsExclusionStatement,

      /// <summary>
      /// The Australian childhood immunisation register history exclusion statement
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16134.172.1.1", Name = "Exclusion Statement", CodeSystem = "NCTIS")]
      AustralianChildhoodImmunisationRegisterHistoryExclusionStatement,

      /// <summary>
      /// The donation decision
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16657", Name = "Donation Decision", CodeSystem = "NCTIS")]
      DonationDecision,

      /// <summary>
      /// The organ and tissue donation details
      /// </summary>
      [EnumMember]
      [Name(Code = "102.16660", Name = "Organ and Tissue Donation Details", CodeSystem = "NCTIS")]
      OrganAndTissueDonationDetails,

      /// <summary>
      /// The bone tissue indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16661", Name = "Bone Tissue Indicator", CodeSystem = "NCTIS")]
      BoneTissueIndicator,

      /// <summary>
      /// The eye tissue indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16662", Name = "Eye Tissue Indicator", CodeSystem = "NCTIS")]
      EyeTissueIndicator,

      /// <summary>
      /// The heart indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16663", Name = "Heart Indicator", CodeSystem = "NCTIS")]
      HeartIndicator,

      /// <summary>
      /// The heart valve indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16664", Name = "Heart Valve Indicator", CodeSystem = "NCTIS")]
      HeartValveIndicator,

      /// <summary>
      /// The kidney indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16665", Name = "Kidney Indicator", CodeSystem = "NCTIS")]
      KidneyIndicator,

      /// <summary>
      /// The liver indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16666", Name = "Liver Indicator", CodeSystem = "NCTIS")]
      LiverIndicator,

      /// <summary>
      /// The lungs indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16667", Name = "Lungs Indicator", CodeSystem = "NCTIS")]
      LungsIndicator,

      /// <summary>
      /// The pancreas indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16668", Name = "Pancreas Indicator", CodeSystem = "NCTIS")]
      PancreasIndicator,

      /// <summary>
      /// The skin tissue indicator
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16669", Name = "Skin Tissue Indicator", CodeSystem = "NCTIS")]
      SkinTissueIndicator,
    }
  }

