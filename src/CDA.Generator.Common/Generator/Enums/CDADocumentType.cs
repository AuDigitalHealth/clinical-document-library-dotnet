﻿/*
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

using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.Generator.Enums
{
    /// <summary>
    /// CDA Document Type
    /// </summary>
    public enum CDADocumentType
    {
        #region Common Template Ids for all Documents

        /// <summary>
        /// CDA Rendering Specification = Template id for Spec
        /// </summary>
        [Name(Name = "CDA Rendering Specification", CodeSystem = "", Code = "", TemplateIdentifier = "1.2.36.1.2001.1001.100.149", Version = "1.0", Title = "CDA Rendering Specification")]
        CdaRenderingSpecification,

        /// <summary>
        /// ClinicalDocument = Template id for a ClinicalDocument - against FHIR Template Specs
        /// </summary>
        [Name(Name = "ClinicalDocument", CodeSystem = "", Code = "", TemplateIdentifier = "1.2.36.1.2001.1001.102.101.100033", Version = "1.0", Title = "ClinicalDocument")]
        ClinicalDocumentTemplate,

        /// <summary>
        /// Common conformance profile v1.7
        /// </summary>
        [Name(Name = "Common conformance profile v1.7", CodeSystem = "", Code = "", TemplateIdentifier = "1.2.36.1.2001.1001.100.1003.100000", Version = "1.0", Title = "Common conformance profile v1.7")]
        CommonConformanceProfileV17,

        #endregion


        #region Continuity Of Care

        /// <summary>
        /// Patient Summary
        /// </summary>
        [Name(Name = "Patient summary", CodeSystem = "LOINC", Code = "60591-5", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.120", Version = "1.4", Title = "Shared Health Summary")]
        SharedHeathSummary,

        /// <summary>
        /// Discharge Summarization Notes
        /// </summary>
        [Name(Name = "Discharge Summarization Note", CodeSystem = "LOINC", Code = "18842-5", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.4", Version = "3.4", Title = "Discharge Summary")]
        DischargeSummary,

        /// <summary>
        /// SpecialistLetter
        /// </summary>
        [Name(Name = "Letter", CodeSystem = "LOINC", Code = "51852-2", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.132", Version = "1.3", Title = "Specialist Letter")]
        SpecialistLetter,

        /// <summary>
        /// e-Referral
        /// </summary>
        [Name(Name = "Referral note", CodeSystem = "LOINC", Code = "57133-1", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.2", Version = "2.2", Title = "e-Referral")]
        EReferral,

        /// <summary>
        /// Service Referral
        /// </summary>
        [Name(Name = "Referral note", CodeSystem = "LOINC", Code = "57133-1", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.231", Version = "1.1", Title = "Service Referral")]
        ServiceReferral,

        /// <summary>
        /// EventSummary
        /// </summary>
        [Name(Name = "Summary of episode note", CodeSystem = "LOINC", Code = "34133-9", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.136", Version = "1.3", Title = "Event Summary")]
        EventSummary,

        #endregion


        #region Medicare Documents

        /// <summary>
        /// Medicare DVA Benefits Report
        /// </summary>
        [Name(Name = "Medicare/DVA Benefits Report", CodeSystem = "NCTIS", Code = "100.16644", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.140", Version = "1.1", Title = "Medicare/DVA Benefits Report")]
        MedicareDvaBenefitsReport,

        /// <summary>
        /// Australian Organ Donor Register
        /// </summary>
        [Name(Name = "Australian Organ Donor Register", CodeSystem = "NCTIS", Code = "100.16671", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.147", Version = "1.1", Title = "Australian Organ Donor Register")]
        AustralianOrganDonorRegister,

        /// <summary>
        /// Australian Childhood Immunisation Register
        /// </summary>
        [Name(Name = "Australian Childhood Immunisation Register", CodeSystem = "NCTIS", Code = "100.16659", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.144", Version = "1.1", Title = "Australian Childhood Immunisation Register")]
        AustralianChildhoodImmunisationRegister,

        /// <summary>
        /// Australian Immunisation Register
        /// </summary>
        [Name(Name = "Australian Immunisation Register", CodeSystem = "NCTIS", Code = "100.17042", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.234", Version = "1.0", Title = "Australian Immunisation Register")]
        AustralianImmunisationRegister,

        /// <summary>
        /// Pharmaceutical Benefits Report
        /// </summary>
        [Name(Name = "Pharmaceutical Benefits Report", CodeSystem = "NCTIS", Code = "100.16650", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.142", Version = "1.1", Title = "Pharmaceutical Benefits Report")]
        PharmaceuticalBenefitsReport,

        /// <summary>
        /// Advance Care Directive Custodian Record 
        /// </summary>
        [Name(Name = "Advance Care Directive Custodian Record", CodeSystem = "NCTIS", Code = "100.16696", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.156", Version = "1.0", Title = "Advance Care Directive Custodian")]
        AdvanceCareDirectiveCustodianRecord,

        #endregion


        #region Consumer Documents

        /// <summary>
        /// Consumer Entered Health Summary
        /// </summary>
        [Name(Name = "Consumer Entered Health Summary", CodeSystem = "NCTIS", Code = "100.16685", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.153", Version = "1.0", Title = "Personal Health Summary")]
        ConsumerEnteredHealthSummary,

        /// <summary>
        /// Consumer Entered Notes
        /// </summary>
        [Name(Name = "Consumer Entered Notes", CodeSystem = "NCTIS", Code = "100.16681", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.151", Version = "1.0", Title = "Personal Health Note")]
        ConsumerEnteredNotes,

        /// <summary>
        /// Consumer Entered Achievements
        /// </summary>
        [Name(Name = "Consumer Entered Achievements", CodeSystem = "NCTIS", Code = "100.16812", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.183", Version = "1.0", Title = "Consumer Entered Achievements")]
        ConsumerEnteredAchievements,

        /// <summary>
        /// NSW Consumer Entered Achievements
        /// </summary>
        [Name(Name = "Consumer Entered Achievements", CodeSystem = "NCTIS", Code = "100.16812", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.206", Version = "1.0", Title = "Consumer Entered Achievements")]
        NSWConsumerEnteredAchievements,

        #endregion


        #region View Documents

        /// <summary>
        /// Consolidated View
        /// </summary>
        [Name(Name = "Consolidated View", CodeSystem = "NCTIS", Code = "100.16725", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.160", Version = "1.0", Title = "Consolidated View")]
        ConsolidatedView,

        /// <summary>
        /// Medicare View
        /// </summary>
        [Name(Name = "Medicare Overview", CodeSystem = "NCTIS", Code = "100.16767", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.172", Version = "1.1", Title = "Medicare Overview")]
        MedicareOverview,

        #endregion


        #region Prescription+Dispense Documents

        /// <summary>
        /// PCEHR Prescription Record
        /// </summary>
        [Name(Name = "PCEHR Prescription Record", CodeSystem = "NCTIS", Code = "100.16764", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.170", Version = "1.0", Title = "eHealth Prescription Record")]
        PrescriptionRecord,

        /// <summary>
        /// PCEHR Dispense Record
        /// </summary>
        [Name(Name = "PCEHR Dispense Record", CodeSystem = "NCTIS", Code = "100.16765", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.171", Version = "1.0", Title = "eHealth Dispense Record")]
        DispenseRecord,

        /// <summary>
        /// PCEHR Prescription And Dispense View
        /// </summary>
        [Name(Name = "PCEHR Prescription and Dispense View", CodeSystem = "NCTIS", Code = "100.16789", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.179", Version = "1.0", Title = "PCEHR Prescription and Dispense View")]
        PrescriptionAndDispenseView,

        /// <summary>
        /// PCEHR Prescription Record
        /// </summary>
        [Name(Name = "Prescription Document", CodeSystem = "LOINC", Code = "64287-6", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.74", Version = "2.1", Title = "Prescription Document")]
        EPrescription,

        /// <summary>
        /// Medication Dispensed
        /// </summary>
        [Name(Name = "Medication Dispensed", CodeSystem = "LOINC", Code = "60593-1", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.75", Version = "2.1", Title = "Medication Dispensed")]
        EDispenseRecord,

        /// <summary>
        /// Medication Dispensed
        /// </summary>
        [Name(Name = "Prescription Request", CodeSystem = "NCTIS", Code = "100.16285", TemplateIdentifier = "1.2.36.1.2001.1001.101.100.1002.101", Version = "1.1", Title = "Prescription Request")]
        EPrescriptionRequest,

        #endregion


        #region Path and DI Documents

        /// <summary>
        /// Pathology Result View
        /// </summary>
        [Name(Name = "Pathology Result View", CodeSystem = "NCTIS", Code = "100.16873", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.213", Version = "1.0", Title = "Pathology Result View")]
        PathologyResultView,

        /// <summary>
        /// Pathology Result Report
        /// </summary>
        [Name(Name = "Pathology Report", CodeSystem = "NCTIS", Code = "100.32001", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.220", Version = "1.0", Title = "Pathology Report")]
        PathologyResultReport,

        /// <summary>
        /// Pathology Result Report
        /// </summary>
        [Name(Name = "Pathology Report", CodeSystem = "NCTIS", Code = "100.32001", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.220", Version = "2.0", Title = "Pathology Report")]
        PathologyReportWithStructuredContent,

        /// <summary>
        /// Diagnostic Imaging Report
        /// </summary>
        [Name(Name = "Diagnostic Imaging Report", CodeSystem = "NCTIS", Code = "100.16957", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.222", Version = "1.0", Title = "Diagnostic Imaging Report")]
        DiagnosticImagingReport,

        #endregion


        #region Advance Care Information 

        /// <summary>
        /// Advance Care Information
        /// </summary>
        [Name(Name = "Advance Care Information", CodeSystem = "NCTIS", Code = "100.16975", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.226", Version = "1.0", Title = "Advance Care Information")]
        AdvanceCareInformation,

        /// <summary>
        /// Advance Care Information Subtype = Advance Care Planning Document (Same as ACI as that was the first doc)
        /// </summary>
        [Name(Name = "Advance Care Planning Document", CodeSystem = "NCTIS", Code = "100.16998", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.226", Version = "1.0", Title = "Advance Care Planning Document")]
        AdvanceCarePlanning,

        /// <summary>
        /// Advance Care Information Subtype = Goals of Care Document
        /// </summary>
        [Name(Name = "Goals of Care Document", CodeSystem = "NCTIS", Code = "100.32016", TemplateIdentifier = "1.2.36.1.2001.1001.100.1003.100001", Version = "1.0", Title = "Goals of Care Document")]
        GoalsOfCare,

        #endregion


        #region Pharmacist Shared Medicines List / Shared Medicines List

        /// <summary>
        /// Core Level One = base Template for Level One Docs
        /// </summary>
        [Name(Name = "Core Level One", CodeSystem = "", Code = "", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.218", Version = "1.1", Title = "Core Level One")]
        CoreLevelOne,

        /// <summary>
        /// Pharmacist Shared Medicines List
        /// </summary>
        [Name(Name = "Pharmacist Shared Medicines List", CodeSystem = "LOINC", Code = "56445-0", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.237", Version = "1.0", Title = "Pharmacist Shared Medicines List")]
        PharmacistSharedMedicinesList,

        /// <summary>
        /// Shared Medicines List - Pharmacist  v2 (using FHIR models)
        /// </summary>
        [Name(Name = "Pharmacist Shared Medicines List", CodeSystem = "LOINC", Code = "56445-0", TemplateIdentifier = "1.2.36.1.2001.1001.102.101.100065", Version = "1.0", Title = "Pharmacist Shared Medicines List")]
        PharmacistSharedMedicinesListV2HPII,

        /// <summary>
        /// Shared Medicines List - Pharmacist  v2 (using FHIR models)
        /// </summary>
        [Name(Name = "Pharmacist Shared Medicines List", CodeSystem = "LOINC", Code = "56445-0", TemplateIdentifier = "1.2.36.1.2001.1001.102.101.100065", Version = "1.0", Title = "Pharmacist Shared Medicines List")]
        PharmacistSharedMedicinesListV2NoHPII,

        /// <summary>
        /// PSML v2 conformance profile
        /// </summary>
        [Name(Name = "PSML v2 conformance profile", CodeSystem = "", Code = "", TemplateIdentifier = "1.2.36.1.2001.1001.100.1003.100002", Version = "1.0", Title = "PSML v2 conformance profile HPI-I default")]
        PharmacistSharedMedicinesListV2ConformanceProfileHPII,

        /// <summary>
        /// PSML v2 conformance profile
        /// </summary>
        [Name(Name = "PSML v2 conformance profile", CodeSystem = "", Code = "", TemplateIdentifier = "1.2.36.1.2001.1001.100.1003.100003", Version = "1.0", Title = "PSML v2 conformance profile HPI-I relaxed")]
        PharmacistSharedMedicinesListV2ConformanceProfileNoHPII,

        #endregion

        #region Aged Care Transfer Summary

        /// <summary>
        /// Residential Care Transfer Reason - 1A
        /// </summary>
        [Name(Name = "Residential Care Transfer Reason", CodeSystem = "NCTIS", Code = "100.32044", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.32044", Version = "1.0", Title = "Residential Care Transfer Reason")]
        ResidentialCareTransferReason,

        /// <summary>
        /// Residential Care Health Summary - 1A
        /// </summary>
        [Name(Name = "Residential Care Health Summary", CodeSystem = "NCTIS", Code = "100.32049", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.32049", Version = "1.0", Title = "Residential Care Health Summary")]
        ResidentialCareHealthSummary,

        /// <summary>
        /// Residential Care Medication Chart - 1B
        /// </summary>
        [Name(Name = "Residential Care Medication Chart", CodeSystem = "NCTIS", Code = "100.32046", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.32046", Version = "1.0", Title = "Residential Care Medication Chart")]
        ResidentialCareMedicationChart,

        #endregion

        #region Aged Care Support Plan

        /// <summary>
        /// Aged Care Support Plan - Support at Home - 1A
        /// </summary>
        [Name(Name = "Aged Care Support Plan", CodeSystem = "NCTIS", Code = "100.32052", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.32052", Version = "1.0", Title = "Aged Care Support Plan")]
        AgedCareSupportPlanSupportAtHome,

        #endregion

        #region CeHR 

        /// <summary>
        /// Consumer Entered Measurements
        /// </summary>
        [Name(Name = "Consumer Entered Measurements", CodeSystem = "NCTIS", Code = "100.16870", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.208", Version = "1.1", Title = "Consumer Entered Measurements")]
        ConsumerEnteredMeasurements,

        /// <summary>
        /// Healthcare Provider Entered Measurements
        /// </summary>
        [Name(Name = "Healthcare Provider Entered Measurements", CodeSystem = "NCTIS", Code = "100.16871", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.209", Version = "1.0", Title = "Healthcare Provider Entered Measurements")]
        HealthcareProviderEnteredMeasurements,

        /// <summary>
        /// Physical Measurements View
        /// </summary>
        [Name(Name = "Physical Measurements View", CodeSystem = "NCTIS", Code = "100.16872", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.210", Version = "1.0", Title = "Physical Measurements View")]
        PhysicalMeasurementsView,

        /// <summary>
        /// NSW Health Check Assessment
        /// </summary>
        [Name(Name = "Health Check Assessment", CodeSystem = "NCTIS", Code = "100.16920", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.216", Version = "1.1", Title = "NSW Health Check Assessment")]
        NSWHealthCheckAssessment,

        /// <summary>
        /// Child Parent Questionnaire
        /// </summary>
        [Name(Name = "Child Parent Questionnaire", CodeSystem = "NCTIS", Code = "100.16919", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.215", Version = "1.1", Title = "NSW Child Parent Questionnaire")]
        ConsumerQuestionnaire,

        /// <summary>
        /// Birth Details
        /// </summary>
        [Name(Name = "Birth Details", CodeSystem = "NCTIS", Code = "100.16939", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.214", Version = "1.0", Title = "Birth Details")]
        BirthDetails,

        /// <summary> 
        /// Observation View
        /// </summary>
        [Name(Name = "Observation View", CodeSystem = "NCTIS", Code = "100.16872", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.210", Version = "1.1", Title = "Observation View")]
        ObservationView,

        /// <summary> 
        /// Observation View
        /// </summary>
        [Name(Name = "Health Check Schedule View", CodeSystem = "NCTIS", Code = "100.16940", TemplateIdentifier = "1.2.36.1.2001.1001.100.1002.219", Version = "1.1", Title = "Health Check Schedule View")]
        ChildHealthCheckScheduleView,

        #endregion


    }
}
