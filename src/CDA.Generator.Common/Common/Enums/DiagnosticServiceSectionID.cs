/*
 * Copyright 2013 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;


namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Diagnostic service section ID values (HL7 Diagnostic service section ID).
    /// </summary>
    [Serializable]
    [DataContract]
    public enum DiagnosticServiceSectionID
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        [EnumMember]
        Undefined,

        /// <summary>
        /// Audiology
        /// </summary>
        [EnumMember]
        [Name(Code = "AU", Name = "Audiology")]
        Audiology,

        /// <summary>
        /// Bedside ICU Monitoring
        /// </summary>
        [EnumMember]
        [Name(Code = "ICU", Name = "Bedside ICU Monitoring")]
        BedsideICUMonitoring,

        /// <summary>
        /// Blood Bank
        /// </summary>
        [EnumMember]
        [Name(Code = "BLB", Name = "Blood Bank")]
        BloodBank,

        /// <summary>
        /// Blood Gases
        /// </summary>
        [EnumMember]
        [Name(Code = "BG", Name = "Blood Gases")]
        BloodGases,

        /// <summary>
        /// Cardiac Catheterization
        /// </summary>
        [EnumMember]
        [Name(Code = "CTH", Name = "Cardiac Catheterization")]
        CardiacCatheterization,

        /// <summary>
        /// Cardiac Ultrasound
        /// </summary>
        [EnumMember]
        [Name(Code = "CUS", Name = "Cardiac Ultrasound")]
        CardiacUltrasound,

        /// <summary>
        /// CAT Scan
        /// </summary>
        [EnumMember]
        [Name(Code = "CT", Name = "CAT Scan")]
        CATScan,

        /// <summary>
        /// Chemistry
        /// </summary>
        [EnumMember]
        [Name(Code = "CH", Name = "Chemistry")]
        Chemistry,

        /// <summary>
        /// Cineradiograph
        /// </summary>
        [EnumMember]
        [Name(Code = "XRC", Name = "Cineradiograph")]
        Cineradiograph,

        /// <summary>
        /// Cytopathology
        /// </summary>
        [EnumMember]
        [Name(Code = "CP", Name = "Cytopathology")]
        Cytopathology,

        /// <summary>
        /// Electrocardiac (e.g., EKG, EEC, Holter)
        /// </summary>
        [EnumMember]
        [Name(Code = "EC", Name = "Electrocardiac (e.g., EKG, EEC, Holter)")]
        Electrocardiac,

        /// <summary>
        /// Electroneuro (EEG, EMG,EP,PSG)
        /// </summary>
        [EnumMember]
        [Name(Code = "EN", Name = "Electroneuro (EEG, EMG,EP,PSG)")]
        Electroneuro,

        /// <summary>
        /// Hematology
        /// </summary>
        [EnumMember]
        [Name(Code = "HM", Name = "Hematology")]
        Hematology,

        /// <summary>
        /// Immunology
        /// </summary>
        [EnumMember]
        [Name(Code = "IMM", Name = "Immunology")]
        Immunology,

        /// <summary>
        /// Laboratory
        /// </summary>
        [EnumMember]
        [Name(Code = "LAB", Name = "Laboratory")]
        Laboratory,

        /// <summary>
        /// Microbiology
        /// </summary>
        [EnumMember]
        [Name(Code = "MB", Name = "Microbiology")]
        Microbiology,

        /// <summary>
        /// Mycobacteriology
        /// </summary>
        [EnumMember]
        [Name(Code = "MCB", Name = "Mycobacteriology")]
        Mycobacteriology,

        /// <summary>
        /// Mycology
        /// </summary>
        [EnumMember]
        [Name(Code = "MYC", Name = "Mycology")]
        Mycology,

        /// <summary>
        /// Nuclear Magnetic Resonance
        /// </summary>
        [EnumMember]
        [Name(Code = "NMR", Name = "Nuclear Magnetic Resonance")]
        NuclearMagneticResonance,

        /// <summary>
        /// Nuclear Medicine Scan
        /// </summary>
        [EnumMember]
        [Name(Code = "NMS", Name = "Nuclear Medicine Scan")]
        NuclearMedicineScan,

        /// <summary>
        /// Nursing Service Measures
        /// </summary>
        [EnumMember]
        [Name(Code = "NRS", Name = "Nursing Service Measures")]
        NursingServiceMeasures,

        /// <summary>
        /// OB Ultrasound
        /// </summary>
        [EnumMember]
        [Name(Code = "OUS", Name = "OB Ultrasound")]
        OBUltrasound,

        /// <summary>
        /// Occupational Therapy
        /// </summary>
        [EnumMember]
        [Name(Code = "OT", Name = "Occupational Therapy")]
        OccupationalTherapy,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember]
        [Name(Code = "OTH", Name = "Other")]
        Other,

        /// <summary>
        /// Outside Lab
        /// </summary>
        [EnumMember]
        [Name(Code = "OSL", Name = "Outside Lab")]
        OutsideLab,

        /// <summary>
        /// Pharmacy
        /// </summary>
        [EnumMember]
        [Name(Code = "PHR", Name = "Pharmacy")]
        Pharmacy,

        /// <summary>
        /// Physical Therapy
        /// </summary>
        [EnumMember]
        [Name(Code = "PT", Name = "Physical Therapy")]
        PhysicalTherapy,

        /// <summary>
        /// Physician (Hx. Dx, admission note, etc.)
        /// </summary>
        [EnumMember]
        [Name(Code = "PHY", Name = "Physician (Hx. Dx, admission note, etc.)")]
        Physician,

        /// <summary>
        /// Pulmonary Function
        /// </summary>
        [EnumMember]
        [Name(Code = "PF", Name = "Pulmonary Function")]
        PulmonaryFunction,

        /// <summary>
        /// Radiation Therapy
        /// </summary>
        [EnumMember]
        [Name(Code = "RT", Name = "Radiation Therapy")]
        RadiationTherapy,

        /// <summary>
        /// Radiograph
        /// </summary>
        [EnumMember]
        [Name(Code = "RX", Name = "Radiograph")]
        Radiograph,

        /// <summary>
        /// Radiology
        /// </summary>
        [EnumMember]
        [Name(Code = "RAD", Name = "Radiology")]
        Radiology,

        /// <summary>
        /// Radiology Ultrasound
        /// </summary>
        [EnumMember]
        [Name(Code = "RUS", Name = "Radiology Ultrasound")]
        RadiologyUltrasound,

        /// <summary>
        /// Radiology Ultrasound
        /// </summary>
        [EnumMember]
        [Name(Code = "RC", Name = "Respiratory Care (therapy)")]
        RespiratoryCare,

        /// <summary>
        /// Serology
        /// </summary>
        [EnumMember]
        [Name(Code = "SR", Name = "Serology")]
        Serology,

        /// <summary>
        /// Surgical Pathology
        /// </summary>
        [EnumMember]
        [Name(Code = "SP", Name = "Surgical Pathology")]
        SurgicalPathology,

        /// <summary>
        /// Toxicology
        /// </summary>
        [EnumMember]
        [Name(Code = "TX", Name = "Toxicology")]
        Toxicology,

        /// <summary>
        /// Vascular Ultrasound
        /// </summary>
        [EnumMember]
        [Name(Code = "VUS", Name = "Vascular Ultrasound")]
        VascularUltrasound,

        /// <summary>
        /// Virology
        /// </summary>
        [EnumMember]
        [Name(Code = "VR", Name = "Virology")]
        Virology,

    }
}
