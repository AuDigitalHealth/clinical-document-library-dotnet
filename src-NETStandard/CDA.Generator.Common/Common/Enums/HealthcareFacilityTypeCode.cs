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

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// HealthcareFacilityTypeCodes
    /// </summary>
    [Serializable]
    [DataContract]
    public enum HealthcareFacilityTypeCodes
    {
        /// <summary>
        /// The Physiotherapy Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8533", Name = "Physiotherapy Services", CodeSystem = "Anzsic2006")]
        PhysiotherapyServices,

        /// <summary>
        /// Pathology and Diagnostic Imaging Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8520", Name = "Pathology and Diagnostic Imaging Services", CodeSystem = "Anzsic2006")]
        PathologyAndDiagnosticImagingServices,

        /// <summary>
        /// Child Care Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8710", Name = "Child Care Services", CodeSystem = "Anzsic2006")]
        ChildCareServices,

        /// <summary>
        /// Other Social Assistance Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8790", Name = "Other Social Assistance Services", CodeSystem = "Anzsic2006")]
        OtherSocialAssistanceServices,

        /// <summary>
        /// Call Centre Operation
        /// </summary>
        [EnumMember]
        [Name(Code = "7294", Name = "Call Centre Operation", CodeSystem = "Anzsic2006")]
        CallCentreOperation,

        /// <summary>
        /// Mental Health Hospitals
        /// </summary>
        [EnumMember]
        [Name(Code = "8402", Name = "Mental Health Hospitals", CodeSystem = "Anzsic2006")]
        MentalHealthHospitals,

        /// <summary>
        /// Electronic Information Storage Services
        /// </summary>
        [EnumMember]
        [Name(Code = "5922", Name = "Electronic Information Storage Services", CodeSystem = "Anzsic2006")]
        ElectronicInformationStorageServices,

        /// <summary>
        /// Computer System Design and Related Services
        /// </summary>
        [EnumMember]
        [Name(Code = "7000", Name = "Computer System Design and Related Services", CodeSystem = "Anzsic2006")]
        ComputerSystemDesignAndRelatedServices,

        /// <summary>
        /// Computer System Design and Related Services
        /// </summary>
        [EnumMember]
        [Name(Code = "7291", Name = "Office Administrative Services", CodeSystem = "Anzsic2006")]
        OfficeAdministrativeServices,

        /// <summary>
        /// Scientific Research Services
        /// </summary>
        [EnumMember]
        [Name(Code = "6910", Name = "Scientific Research Services", CodeSystem = "Anzsic2006")]
        ScientificResearchServices,

        /// <summary>
        /// Chiropractic and Osteopathic Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8534", Name = "Chiropractic and Osteopathic Services", CodeSystem = "Anzsic2006")]
        ChiropracticAndOsteopathicServices,

        /// <summary>
        /// General Practice
        /// </summary>
        [EnumMember]
        [Name(Code = "8511", Name = "General Practice", CodeSystem = "Anzsic2006")]
        GeneralPractice,

        /// <summary>
        /// Higher Education
        /// </summary>
        [EnumMember]
        [Name(Code = "8102", Name = "Higher Education", CodeSystem = "Anzsic2006")]
        HigherEducation,

        /// <summary>
        /// Other Residential Care Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8609", Name = "Other Residential Care Services", CodeSystem = "Anzsic2006")]
        OtherResidentialCareServices,

        /// <summary>
        /// Corporate Head Office Management Services
        /// </summary>
        [EnumMember]
        [Name(Code = "6961", Name = "Corporate Head Office Management Services", CodeSystem = "Anzsic2006")]
        CorporateHeadOfficeManagementServices,

        /// <summary>
        /// Ambulance Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8591", Name = "Ambulance Services", CodeSystem = "Anzsic2006")]
        AmbulanceServices,

        /// <summary>
        /// Data Processing and Web Hosting Services
        /// </summary>
        [EnumMember]
        [Name(Code = "5921", Name = "Data Processing and Web Hosting Services", CodeSystem = "Anzsic2006")]
        DataProcessingAndWebHostingServices,

        /// <summary>
        /// General Health Administration
        /// </summary>
        [EnumMember]
        [Name(Code = "7561", Name = "General Health Administration", CodeSystem = "Anzsic2006")]
        GeneralHealthAdministration,

        /// <summary>
        /// Local Government Healthcare Administration
        /// </summary>
        [EnumMember]
        [Name(Code = "7531", Name = "Local Government Healthcare Administration", CodeSystem = "Anzsic2006")]
        LocalGovernmentHealthcareAdministration,

        /// <summary>
        /// Health Insurance
        /// </summary>
        [EnumMember]
        [Name(Code = "6321", Name = "Health Insurance", CodeSystem = "Anzsic2006")]
        HealthInsurance,

        /// <summary>
        /// Aged Care Residential Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8601", Name = "Aged Care Residential Services", CodeSystem = "Anzsic2006")]
        AgedCareResidentialServices,

        /// <summary>
        /// Health and Fitness Centres and Gymnasia Operation
        /// </summary>
        [EnumMember]
        [Name(Code = "9111", Name = "Health and Fitness Centres and Gymnasia Operation", CodeSystem = "Anzsic2006")]
        HealthAndFitnessCentresAndGymnasiaOperation,

        /// <summary>
        /// Transport
        /// </summary>
        [EnumMember]
        [Name(Code = "4623", Name = "Transport", CodeSystem = "Anzsic2006")]
        Transport,

        /// <summary>
        /// State Government Healthcare Administration
        /// </summary>
        [EnumMember]
        [Name(Code = "7521", Name = "State Government Healthcare Administration", CodeSystem = "Anzsic2006")]
        StateGovernmentHealthcareAdministration,

        /// <summary>
        /// Other Healthcare Services nec
        /// </summary>
        [EnumMember]
        [Name(Code = "8599", Name = "Other Healthcare Services nec", CodeSystem = "Anzsic2006")]
        OtherHealthcareServicesnec,

        /// <summary>
        /// Retail Pharmacy
        /// </summary>
        [EnumMember]
        [Name(Code = "4271", Name = "Retail Pharmacy", CodeSystem = "Anzsic2006")]
        RetailPharmacy,

        /// <summary>
        /// Internet Service Providers and Web Search Portals
        /// </summary>
        [EnumMember]
        [Name(Code = "5910", Name = "Internet Service Providers and Web Search Portals", CodeSystem = "Anzsic2006")]
        InternetServiceProvidersAndWebSearchPortals,

        /// <summary>
        /// Provision and administration of public health program
        /// </summary>
        [EnumMember]
        [Name(Code = "7562", Name = "Provision and administration of public health program", CodeSystem = "Anzsic2006")]
        ProvisionAndAdministrationOfPublicHealthProgram,

        /// <summary>
        /// Hospitals (except Psychiatric Hospitals)
        /// </summary>
        [EnumMember]
        [Name(Code = "8401", Name = "Hospitals (except Psychiatric Hospitals)", CodeSystem = "Anzsic2006")]
        HospitalsExceptPsychiatricHospitals,

        /// <summary>
        /// Other Allied Health Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8539", Name = "Other Allied Health Services", CodeSystem = "Anzsic2006")]
        OtherAlliedHealthServices,

        /// <summary>
        /// Central Government Healthcare Administration
        /// </summary>
        [EnumMember]
        [Name(Code = "7511", Name = "Central Government Healthcare Administration", CodeSystem = "Anzsic2006")]
        CentralGovernmentHealthcareAdministration,

        /// <summary>
        /// Other Professional, Scientific and Technical Services n.e.c.
        /// </summary>
        [EnumMember]
        [Name(Code = "6999", Name = "Other Professional, Scientific and Technical Services n.e.c.", CodeSystem = "Anzsic2006")]
        OtherProfessionalScientificAndTechnicalServices,

        /// <summary>
        /// Specialist Medical Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8512", Name = "Specialist Medical Services", CodeSystem = "Anzsic2006")]
        SpecialistMedicalServices,

        /// <summary>
        /// Dental Services
        /// </summary>
        [EnumMember]
        [Name(Code = "8531", Name = "Dental Services", CodeSystem = "Anzsic2006")]
        DentalServices,

        /// <summary>
        /// Optometry and Optical Dispensing
        /// </summary>
        [EnumMember]
        [Name(Code = "8532", Name = "Optometry and Optical Dispensing", CodeSystem = "Anzsic2006")]
        OptometryAndOpticalDispensing,
    }
}
