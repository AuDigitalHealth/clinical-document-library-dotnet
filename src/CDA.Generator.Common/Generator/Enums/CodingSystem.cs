/*
 * Copyright 2013 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, softwareM
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.Generator.Enums
{
    /// <summary>
    /// Coding System
    /// </summary>
  public enum CodingSystem
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        Undefined,

        /// <summary>
        /// NCTIS Data Components
        /// </summary>
        [Name(Name = "NCTIS Data Components", Code = "1.2.36.1.2001.1001.101")]
        NCTIS,

        /// <summary>
        /// NCTIS Change Type Values
        /// </summary>
        [Name(Name = "NCTIS Change Type Values", Code = "1.2.36.1.2001.1001.101.104.16592")]
        NCTISChangeTypeValues,

        /// <summary>
        /// NCTIS Document Status Values
        /// </summary>
        [Name(Name = "NCTIS Document Status Values", Code = "1.2.36.1.2001.1001.101.104.20104")]
        NCTISDocumentStatusValues,

        /// <summary>
        /// NCTIS External Concepts
        /// </summary>
        [Name(Name = "NCTIS External concepts", Code = "1.2.36.1.2001.1005")]
        NCTISExternalConcepts,

        ///<summary>
        /// NCTIS External Concept – Australian Vaccine Code
        ///</summary>
        [Name(Name = "Australian Vaccine Code", Code = "1.2.36.1.2001.1005.17")]
        NCTISExternalConceptAustralianVaccineCode,

        /// <summary>
        ///Health Care Client Source Of Death Notification
        /// </summary>
        [Name(Name = "AS 5017-2006 Health Care Client Source of Death Notification", Code = "2.16.840.1.113883.13.64")]
        HealthCareClientSourceOfDeathNotification,

        /// <summary>
        /// NCTIS Result Status Values
        /// </summary>
        [Name(Name = "NCTIS Result Status Values", Code = "1.2.36.1.2001.1001.101.104.16501")]
        NCTISResultStatusValues,

        /// <summary>
        /// NCTIS Entitlement Type Values
        /// </summary>
        [Name(Name = "NCTIS Entitlement Type Values", Code = "1.2.36.1.2001.1001.101.104.16047")]
        NCTISEntitlementTypeValues,

        /// <summary>
        /// NCTIS Global Statement Values
        /// </summary>
        [Name(Name = "NCTIS Global Statement Values", Code = "1.2.36.1.2001.1001.101.104.16299")]
        NCTISGlobalStatementValues,

        /// <summary>
        /// NCTIS Admin codes
        /// </summary>
        [Name(Name = "NCTIS Admin codes", Code = "1.2.36.1.2001.1001.101.104.16502")]
        NCTISAdmin,

        /// <summary>
        /// NCTIS Request Urgency Values
        /// </summary>
        [Name(Name = "NCTIS Request Urgency Values", Code = "1.2.36.1.2001.1001.101.104.16127")]
        NCTISRequestUrgencyValues,

        /// <summary>
        /// Australian Medicines Terminology (AMT)
        /// </summary>
        [Name(Name = "Australian Medicines Terminology (AMT)", Code = "1.2.36.1.2001.1004.100")]
        AMT,

        /// <summary>
        /// Australian Medicines Terminology (AMT)
        /// </summary>
        [Name(Name = "Australian Medicines Terminology (AMT) v2", Code = "1.2.36.1.2001.1004.100")]
        AMTV2,

        /// <summary>
        /// Australian Medicines Terminology (AMT)
        /// </summary>
        [Name(Name = "Australian Medicines Terminology (AMT) v3", Code = "2.16.840.1.113883.6.96")]
        AMTV3,

        /// <summary>
        /// Snomed SNOMED CT-AU
        /// </summary>
        [Name(Name = "SNOMED CT-AU", Code = "2.16.840.1.113883.6.96")]
        SNOMED,

        /// <summary>
        /// Snomed CT
        /// </summary>
        [Name(Name = "SNOMED CT", Code = "2.16.840.1.113883.6.96")]
        SNOMEDCT,

        /// <summary>
        /// METeOR
        /// </summary>
        [Name(Name = "METeOR Indigenous Status", Code = "2.16.840.1.113883.3.879.291036")]
        METEOR,

        /// <summary>
        /// METEOREmploymentType
        /// </summary>
        [Name(Name = "METeOR Employment type", Code = "2.16.840.1.113883.3.879.314867")]
        METEOREmploymentType,

        /// <summary>
        /// Episode of admitted patient care-separation mode
        /// </summary>
        [Name(Name = "AIHW Mode of Separation", Code = "2.16.840.1.113883.13.65")]
        AIHW,

        /// <summary>
        /// 1220.0 - ANZSCO - Australian and New Zealand Standard Classification of Occupations, First Edition, 2006
        /// </summary>
        [Name(Name = "1220.0 - ANZSCO - Australian and New Zealand Standard Classification of Occupations, First Edition, 2006", Code = "2.16.840.1.113883.13.62")]
        ANZSCO,

        /// <summary>
        /// 1220.0 - ANZSCO - Australian and New Zealand Standard Classification of Occupations, First Edition, Revision 1
        /// </summary>
        [Name(Name = "1220.0 - ANZSCO - Australian and New Zealand Standard Classification of Occupations, First Edition, Revision 1", Code = "2.16.840.1.113883.13.62")]
        ANZSCORevision1,

        /// <summary>
        /// LOINC
        /// </summary>
        [Name(Name = "LOINC", Code = "2.16.840.1.113883.6.1")]
        LOINC,

        /// <summary>
        /// AS 5017-2006 Health Care Client Identifier Sex
        /// </summary>
        [Name(Name = "AS 5017-2006 Health Care Client Identifier Sex", Code = "2.16.840.1.113883.13.68")]
        Gender,

        ///<summary>
        /// HL7 - Observation Interpretation Normality
        ///</summary>
        [Name(Name = "HL7 ObservationInterpretationNormality", Code = "2.16.840.1.113883.5.83")]
        HL7ObservationInterpretationNormality,

        ///<summary>
        /// HL7 - Substance Admin Substitution
        ///</summary>
        [Name(Name = "HL7:SubstanceAdminSubstitution", Code = "2.16.840.1.113883.5.1070")]
        HL7SubstanceAdminSubstitution,

        ///<summary>
        /// HL7 v3 - EmployeeJobClass
        ///</summary>
        [Name(Name = "HL7:EmployeeJobClass", Code = "2.16.840.1.113883.5.1059")]
        HL7EmployeeJobClass,

        ///<summary>
        /// HL7 - Observation Interpretation Normality
        ///</summary>
        [Name(Name = "HL7 ServiceDeliveryLocationRoleType", Code = "2.16.840.1.113883.1.11.17660")]
        HL7ServiceDeliveryLocationRoleType,

        ///<summary>
        /// HL7 - PersonalRelationshipRoleType
        ///</summary>
        [Name(Name = "HL7 PersonalRelationshipRoleType", Code = "2.16.840.1.113883.5.111")]
        HL7PersonalRelationshipRoleType,

        /// <summary>
        /// HL7 - Diagnostic Service Section ID
        /// </summary>
        [Name(Name = "HL7 Diagnostic service section ID", Code = "2.16.840.1.113883.12.74")]
        HL7DiagnosticServiceSectionID,    

        ///<summary>
        /// NCTIS Concurrent Supply Grounds Values
        ///</summary>
        [Name(Name = "NCTIS Concurrent Supply Grounds Values", Code = "1.2.36.1.2001.1001.101.104.16085")]
        NCTISConcurrentSupplyGroundsValues,

        ///<summary>
        /// Claim Category Type Values
        ///</summary>
        [Name(Name = "Claim Category Type Values", Code = "1.2.36.1.2001.1001.101.104.16060")]
        ClaimCategoryTypeValues,

        ///<summary>
        /// NCTIS Medical Benefit Category Type Values
        ///</summary>
        [Name(Name = "NCTIS Medical Benefit Category Type Values", Code = "1.2.36.1.2001.1001.101.104.16095")]
        NCTISMedicalBenefitCategoryTypeValues,

        ///<summary>
        /// NCTIS PBS Prescription Type
        ///</summary>
        [Name(Name = "PBS/RPBS Authority Prescription", Code = "1.2.36.1.5001.1.1.3.2.6", CodeSystem = "Prescription Type Values")]
        NCTISPBSPrescriptionType,

        ///<summary>
        /// Identifier Type (HL7)
        ///</summary>
        [Name(Name = "Identifier Type (HL7)", Code = "2.16.840.1.113883.12.203")]
        HL7IdentifierType,

        /// <summary>
        /// Result Status
        /// </summary>
        [Name(Name = "Result Status", Code = "2.16.840.1.113883.5.83")]
        ResultStatus,

        /// <summary>
        /// NCTIS Recommendation or Change Values
        /// </summary>
        [Name(Name = "NCTIS Recommendation or Change Values", Code = "1.2.36.1.2001.1001.101.104.16594")]
        NCTISRecommendationOrChangeValues,

        /// <summary>
        /// ICD-10 
        /// </summary>
        [Name(Name = "ICD-10", Code = "2.16.840.1.113883.6.3")]
        ICD10,

        /// <summary>
        /// ICD-10-AM
        /// </summary>
        [Name(Name = "ICD-10-AM", Code = "2.16.840.1.113883.6.135")]
        ICD10AM,

        /// <summary>
        /// MIMS (MIMS Integrated Data Solution)
        /// </summary>
        [Name(Name = "MIMS", Code = "1.2.36.1.2001.1005.11.1")]
        MIMS,

        /// <summary>
        /// ICPC2
        /// </summary>
        [Name(Name = "ICPC2", Code = "2.16.840.1.113883.6.139.1")]
        ICPC2,

        /// <summary>
        /// ICPC2+
        /// </summary>
        [Name(Name = "ICPC2+", Code = "2.16.840.1.113883.6.140.1")]
        ICPC2Plus,

        /// <summary>
        /// DOCLE
        /// </summary>
        [Name(Name = "DOCLE", Code = "1.2.36.1.2001.1005.13")]
        DOCLE,

        /// <summary>
        /// PBS Code
        /// </summary>
        [Name(Name = "Australian PBS Code", Code = "1.2.36.1.2001.1005.22")]
        PBSCode,

        /// <summary>
        /// PBS Manufacturer Code
        /// </summary>
        [Name(Name = "Australian PBS Manufacturer Code", Code = "1.2.36.1.2001.1005.23")]
        AustralianPBSManufacturerCode,

        /// <summary>
        /// Australian PBS Code
        /// </summary>
        [Name(Name = "Australian PBS Code", Code = "1.2.36.1.2001.1005.22")]
        AustralianPBSCode,

        /// <summary>
        /// MBS Code
        /// </summary>
        [Name(Name = "Australian MBS Code", Code = "1.2.36.1.2001.1005.21")]
        MBSCode,

        /// <summary>
        /// HL7 table N
        /// </summary>
        [Name(Name = "HL7 table N", Code = "2.16.840.1.113883.12.N")]
        HL7tableN,

        /// <summary>
        /// Vaccine Cancellation Reason Type Values
        /// </summary>
        [Name(Name = "Vaccine Cancellation Reason Type Values", Code = "1.2.36.1.2001.1001.101.104.16755")]
        VaccineCancellationReasonTypeValues,

        /// <summary>
        /// PCEHR Assigned Identifier - Repository
        /// </summary>
        [Name(Name = "PCEHR Identifiers", Code = "1.2.36.1.2001.1007", Title = "PCEHR Assigned Identifier - Repository")]
        PCEHRAssignedIdentifierRepository,

        /// <summary>
        /// ActCode
        /// </summary>
        [Name(Name = "ActCode", Code = "2.16.840.1.113883.5.4")]
        ActCode,

        /// <summary>
        /// HL7 RoleCode and RoleClass codes
        /// </summary>
        [Name(Name = "RoleClass", Code = "2.16.840.1.113883.5.110")]
        RoleClass,

        /// <summary>
        /// HL7 RoleCode and RoleClass codes
        /// </summary>
        [Name(Name = "RoleCode", Code = "2.16.840.1.113883.5.111")]
        RoleCode,

        /// <summary>
        /// New South Wales Child Developmental Questionnaires Data
        /// </summary>
        [Name(Name = "PCEHR Custom Data Component", Code = "1.2.36.1.2001.1005.45")]
        PCEHRCustomDataComponent,

        /// <summary>
        /// New South Wales Child Developmental Questionnaires Data
        /// </summary>
        [Name(Name = "New South Wales Child Developmental Questionnaires Data", Code = "1.2.36.1.2001.1005.42.2.5")]
        NewSouthWalesChildDevelopmentalQuestionnairesData,

        /// <summary>
        /// New South Wales Child Developmental Question Data
        /// </summary>
        [Name(Name = "New South Wales Child Developmental Question Data", Code = "1.2.36.1.2001.1005.42.2.1")]
        NewSouthWalesChildDevelopmentalQuestionData,

        /// <summary>
        ///  New South Wales Child Developmental Answers Data
        /// </summary>
        [Name(Name = "New South Wales Child Developmental Answers Data", Code = "1.2.36.1.2001.1005.42.2.2")]
        NewSouthWalesChildDevelopmentalAnswersData,

        /// <summary>
        ///  New South Wales Child Developmental Answers Value
        /// </summary>
        [Name(Name = "New South Wales Child Developmental Answers Value", Code = "1.2.36.1.2001.1005.42.2.6")]
        NewSouthWalesChildDevelopmentalAnswersValue,

        /// <summary>
        /// PCEHR_AUTID
        /// </summary>
        [Name(Name = "PCEHR_ControlCodes", Code = "1.2.3.4.1")]
        PCEHR_ControlCodes,

        /// <summary>
        /// Prescription Type Values
        /// </summary>
        [Name(Name = "Prescription Type Values", Code = "1.2.36.1.5001.1.1.3.2.6")]
        PrescriptionTypeValues,

        /// <summary>
        /// HL7 result Status
        /// </summary>
        [Name(Name = "HL7 Result Status", Code = "2.16.840.1.113883.12.123")]
        HL7ResultStatus,

        /// <summary>
        /// ANZSIC 2006
        /// </summary>
        [Name(Name = "ANZSIC 2006", Code = "1.2.36.1.2001.1005.47")]
        Anzsic2006,

        /// <summary>
        /// ISO 13606-3:2009
        /// </summary>
        [Name(Name = "ISO 13606-3:2009 Health informatics - Electronic health record communication - Part 3: Reference archetypes and term lists [ISO2009a]", Code = "ISO 13606-3:2009")]
        ISO1360632009,

        /// <summary>
        /// NCTIS Authoriser Instruction Values
        /// </summary>
        [Name(Name = "NCTIS Authoriser Instruction Values", Code = "1.2.36.1.2001.1001.101.104.16987")]
        NCTISAuthoriserInstructionValues,

        /// <summary>
        ///HL7RoleClass
        /// </summary>
        [Name(Name = "HL7RoleClass", Code = "2.16.840.1.113883.5.110")]
        HL7RoleClass,

        /// <summary>
        /// HL7 Diagnostic service section ID
        /// </summary>
        [Name(Name = "HL7 Diagnostic service section ID", Code = "2.16.840.1.113883.12.74")]
        DiagnosticService,

        /// <summary>
        /// NCTIS: Admin Codes - Anatomical Region
        /// </summary>
        [Name(Name = "NCTIS Anatomical Region Values", Code = "1.2.36.1.2001.1001.101.104.17008")]
        NCTISAnatomicalRegionValues,


        /// <summary>
        /// ALL HL7 OID references are here
        /// https://confluence.hl7australia.com/display/AFR/HL7+AU+OID+Registry
        /// </summary>




        /// <summary>
        ///HL7: Non-Clinical Empty Reason
        /// https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/non-clinical-empty-reason-2
        /// </summary>
        [Name(Name = "ListEmptyReasons", Code = "2.16.840.1.113883.4.642.4.1106")]
        HL7NonClinicalEmptyReason,

        /// <summary>
        ///HL7: Allergy Intolerance Type
        /// http://hl7.org/fhir/R4/valueset-allergy-intolerance-type.html
        /// </summary>
        [Name(Name = "AllergyIntoleranceType", Code = "2.16.840.1.113883.4.642.4.132")]
        HL7AllergyIntoleranceType,

        /// <summary>
        ///HL7: Allergy Intolerance Clinical Status Codes
        /// http://hl7.org/fhir/R4/valueset-allergyintolerance-clinical.html
        /// </summary>
        [Name(Name = "AllergyIntoleranceClinicalStatusCodes", Code = "2.16.840.1.113883.4.642.4.1373")]
        HL7AllergyIntoleranceClinicalStatusCodes,

        /// <summary>
        ///HL7: Allergy Intolerance Verification Status Codes
        /// http://hl7.org/fhir/R4/valueset-allergyintolerance-verification.html
        /// </summary>
        [Name(Name = "AllergyIntoleranceVerificationStatusCodes", Code = "2.16.840.1.113883.4.642.4.1371")]
        HL7AllergyIntoleranceVerificationStatusCodes,

        /// <summary>
        ///HL7: Medication Statement Taken
        /// http://hl7.org/fhir/STU3/valueset-medication-statement-taken.html
        /// http://hl7.org/fhir/STU3/codesystem-medication-statement-taken.html
        /// </summary>
        [Name(Name = "MedicationStatementTaken", Code = "2.16.840.1.113883.4.642.1.358")]
        HL7MedicationStatementTakenCodes,

        /// <summary>
        ///HL7: Act Status
        /// https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/medication-act-status-hl7-v3-1
        /// https://www.hl7.org/fhir/v3/ActStatus/cs.html
        /// https://www.hl7.org/documentcenter/public/standards/vocabulary/vocabulary_tables/infrastructure/vocabulary/vs_ActCode.html
        /// </summary>
        [Name(Name = "v3.ActStatus", Code = "2.16.840.1.113883.5.14")]
        HL7ActStatusCodes,


        /// <summary>
        ///HL7: Act Encounter Status Coes
        /// https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/encounter-act-status-hl7-v3-1
        /// 
        /// </summary>
        [Name(Name = "v3.ActStatus", Code = "2.16.840.1.113883.5.14")]
        HL7ActEncounterStatusCodes,

        /// <summary>
        ///HL7: Act Encounter Code
        /// https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/encounter-act-status-hl7-v3-1
        /// </summary>
        [Name(Name = "ActEncounterCode", Code = "2.16.840.1.113883.5.4")]
        HL7ActEncounterCodes,

        /// <summary>
        ///HL7:Medicine Item Change Status Codes
        /// https://api.healthterminologies.gov.au/integration/v2/fhir/ValueSet/medicine-item-change-from-practitioner-medicines-review-1
        /// http://hl7.org.au/fhir/CodeSystem/medicine-item-change
        /// </summary>
        [Name(Name = "MedicineItemChange", Code = "2.16.840.1.113883.2.3.4.1.2.6")]
        HL7MedicineItemChangeCodes,

        /// <summary>
        ///HL7: Medication Statement Category
        /// 
        /// </summary>
        [Name(Name = "MedicationStatementCategory", Code = "2.16.840.1.113883.4.642.4.1120")]
        HL7MedicationStatementCategoryCodes,
        

    }
}
