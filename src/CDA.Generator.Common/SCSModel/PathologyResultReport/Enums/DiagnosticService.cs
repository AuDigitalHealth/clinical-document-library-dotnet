using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
    /// Diagnostic Service
    /// </summary>
    [Serializable]
    [DataContract]
    public enum DiagnosticService
    {
      /// <summary>
      /// Audiology
      /// </summary>
      [EnumMember]
      [Name(Code = "AU", Name = "Audiology", CodeSystem = "DiagnosticService")]
      Audiology,

      /// <summary>
      /// Bedside ICU Monitoring
      /// </summary>
      [EnumMember]
      [Name(Code = "ICU", Name = "Bedside ICU Monitoring", CodeSystem = "DiagnosticService")]
      BedsideICUMonitoring,

      /// <summary>
      /// Blood Bank
      /// </summary>
      [EnumMember]
      [Name(Code = "BLB", Name = "Blood Bank", CodeSystem = "DiagnosticService")]
      BloodBank,

      /// <summary>
      /// Blood Gases
      /// </summary>
      [EnumMember]
      [Name(Code = "BG", Name = "Blood Gases", CodeSystem = "DiagnosticService")]
      BloodGases,

      /// <summary>
      /// Cardiac Catheterization
      /// </summary>
      [EnumMember]
      [Name(Code = "CTH", Name = "Cardiac Catheterization", CodeSystem = "DiagnosticService")]
      CardiacCatheterization,

      /// <summary>
      /// Cardiac Ultrasound
      /// </summary>
      [EnumMember]
      [Name(Code = "CUS", Name = "Cardiac Ultrasound", CodeSystem = "DiagnosticService")]
      CardiacUltrasound,

      /// <summary>
      /// CAT Scan
      /// </summary>
      [EnumMember]
      [Name(Code = "CT", Name = "CAT Scan", CodeSystem = "DiagnosticService")]
      CATScan,

      /// <summary>
      /// Chemistry
      /// </summary>
      [EnumMember]
      [Name(Code = "CH", Name = "Chemistry", CodeSystem = "DiagnosticService")]
      Chemistry,

      /// <summary>
      /// Cineradiograph
      /// </summary>
      [EnumMember]
      [Name(Code = "XRC", Name = "Cineradiograph", CodeSystem = "DiagnosticService")]
      Cineradiograph,

      /// <summary>
      /// Cytopathology
      /// </summary>
      [EnumMember]
      [Name(Code = "CP", Name = "Cytopathology", CodeSystem = "DiagnosticService")]
      Cytopathology,

      /// <summary>
      /// Electrocardiac (e.g., EKG, EEC, Holter)
      /// </summary>
      [EnumMember]
      [Name(Code = "EC", Name = "Electrocardiac (e.g., EKG, EEC, Holter)", CodeSystem = "DiagnosticService")]
      Electrocardiac,

      /// <summary>
      /// Electroneuro (EEG, EMG,EP,PSG)
      /// </summary>
      [EnumMember]
      [Name(Code = "EN", Name = "Electroneuro (EEG, EMG,EP,PSG)", CodeSystem = "DiagnosticService")]
      Electroneuro,

      /// <summary>
      /// Hematology
      /// </summary>
      [EnumMember]
      [Name(Code = "HM", Name = "Hematology", CodeSystem = "DiagnosticService")]
      Hematology,

      /// <summary>
      /// Immunology
      /// </summary>
      [EnumMember]
      [Name(Code = "IMM", Name = "Immunology", CodeSystem = "DiagnosticService")]
      Immunology,

      /// <summary>
      /// Laboratory
      /// </summary>
      [EnumMember]
      [Name(Code = "LAB", Name = "Laboratory", CodeSystem = "DiagnosticService")]
      Laboratory,

      /// <summary>
      /// Microbiology
      /// </summary>
      [EnumMember]
      [Name(Code = "MB", Name = "Microbiology", CodeSystem = "DiagnosticService")]
      Microbiology,

      /// <summary>
      /// Mycobacteriology
      /// </summary>
      [EnumMember]
      [Name(Code = "MCB", Name = "Mycobacteriology", CodeSystem = "DiagnosticService")]
      Mycobacteriology,

      /// <summary>
      /// Mycology
      /// </summary>
      [EnumMember]
      [Name(Code = "MYC", Name = "Mycology", CodeSystem = "DiagnosticService")]
      Mycology,

      /// <summary>
      /// Nuclear Magnetic Resonance
      /// </summary>
      [EnumMember]
      [Name(Code = "NMR", Name = "Nuclear Magnetic Resonance", CodeSystem = "DiagnosticService")]
      NuclearMagneticResonance,

      /// <summary>
      /// Nuclear Medicine Scan
      /// </summary>
      [EnumMember]
      [Name(Code = "NMS", Name = "Nuclear Medicine Scan", CodeSystem = "DiagnosticService")]
      NuclearMedicineScan,

      /// <summary>
      /// Nursing Service Measures
      /// </summary>
      [EnumMember]
      [Name(Code = "NRS", Name = "Nursing Service Measures", CodeSystem = "DiagnosticService")]
      NursingServiceMeasures,

      /// <summary>
      /// OB Ultrasound
      /// </summary>
      [EnumMember]
      [Name(Code = "OUS", Name = "OB Ultrasound", CodeSystem = "DiagnosticService")]
      OBUltrasound,

      /// <summary>
      /// Occupational Therapy
      /// </summary>
      [EnumMember]
      [Name(Code = "OT", Name = "Occupational Therapy", CodeSystem = "DiagnosticService")]
      OccupationalTherapy,

      /// <summary>
      /// Other
      /// </summary>
      [EnumMember]
      [Name(Code = "OTH", Name = "Other", CodeSystem = "DiagnosticService")]
      Other,

      /// <summary>
      /// Outside Lab
      /// </summary>
      [EnumMember]
      [Name(Code = "OSL", Name = "Outside Lab", CodeSystem = "DiagnosticService")]
      OutsideLab,

      /// <summary>
      /// Pharmacy
      /// </summary>
      [EnumMember]
      [Name(Code = "PHR", Name = "Pharmacy", CodeSystem = "DiagnosticService")]
      Pharmacy,

      /// <summary>
      /// Physical Therapy
      /// </summary>
      [EnumMember]
      [Name(Code = "PT", Name = "Physical Therapy", CodeSystem = "DiagnosticService")]
      PhysicalTherapy,

      /// <summary>
      /// Physician
      /// </summary>
      [EnumMember]
      [Name(Code = "PHY", Name = "Physician (Hx. Dx, admission note, etc.)", CodeSystem = "DiagnosticService")]
      Physician,

      /// <summary>
      /// Pulmonary Function
      /// </summary>
      [EnumMember]
      [Name(Code = "PF", Name = "Pulmonary Function", CodeSystem = "DiagnosticService")]
      PulmonaryFunction,

      /// <summary>
      /// Radiation Therapy
      /// </summary>
      [EnumMember]
      [Name(Code = "RT", Name = "Radiation Therapy", CodeSystem = "DiagnosticService")]
      RadiationTherapy,

      /// <summary>
      /// Radiograph
      /// </summary>
      [EnumMember]
      [Name(Code = "RX", Name = "Radiograph", CodeSystem = "DiagnosticService")]
      Radiograph,

      /// <summary>
      /// Radiology
      /// </summary>
      [EnumMember]
      [Name(Code = "RAD", Name = "Radiology", CodeSystem = "DiagnosticService")]
      Radiology,

      /// <summary>
      /// Radiology Ultrasound
      /// </summary>
      [EnumMember]
      [Name(Code = "RUS", Name = "Radiology Ultrasound", CodeSystem = "DiagnosticService")]
      RadiologyUltrasound,

      /// <summary>
      /// Respiratory Care (therapy)
      /// </summary>
      [EnumMember]
      [Name(Code = "RC", Name = "Respiratory Care (therapy)", CodeSystem = "DiagnosticService")]
      RespiratoryCare,

      /// <summary>
      /// Serology
      /// </summary>
      [EnumMember]
      [Name(Code = "SR", Name = "Serology", CodeSystem = "DiagnosticService")]
      Serology,

      /// <summary>
      /// Surgical Pathology
      /// </summary>
      [EnumMember]
      [Name(Code = "SP", Name = "Surgical Pathology", CodeSystem = "DiagnosticService")]
      SurgicalPathology,

      /// <summary>
      /// Toxicology
      /// </summary>
      [EnumMember]
      [Name(Code = "TX", Name = "Toxicology", CodeSystem = "DiagnosticService")]
      Toxicology,

      /// <summary>
      /// Vascular Ultrasound
      /// </summary>
      [EnumMember]
      [Name(Code = "VUS", Name = "Vascular Ultrasound", CodeSystem = "DiagnosticService")]
      VascularUltrasound,

      /// <summary>
      /// Virology
      /// </summary>
      [EnumMember]
      [Name(Code = "VR", Name = "Virology", CodeSystem = "DiagnosticService")]
      Virology,
    }
  }

