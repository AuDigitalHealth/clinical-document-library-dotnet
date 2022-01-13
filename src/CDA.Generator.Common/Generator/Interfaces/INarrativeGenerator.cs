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
using CDA.Generator.Common.SCSModel.AdvanceCareInformation.Entities;
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using CDA.Generator.Common.SCSModel.CeHR.Entities;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.SCSModel.ConsumerAchievements.Entities;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using System.Collections.Generic;
using CDA.Generator.Common.SCSModel.MedicareOverview.Entities;
using CDA.Generator.Common.SCSModel.ServiceReferral.Entities;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This interface encapsulates and methods for generating the narrative for each various of the CDA Sections.
    /// 
    /// This interface specifies methods that take in SCS model objects and build HL7 objects for the narrative from 
    /// the SCS objects.
    /// </summary>
    public interface INarrativeGenerator
    {
        /// <summary>
        /// This method creates the narrative for the subject of care
        /// </summary>
        /// <param name="subjectOfCareParticipation">subjectOfCareParticipation</param>
        /// <param name="patientId">patientId</param>
        /// <param name="showEntitlements">showEntitlements</param>
        /// <param name="earliestDateForFiltering">Earliest Date For Filtering</param>
        /// <param name="latestDateForFiltering">Latest Date For Filtering</param>
        /// <param name="specialty">List of specialties</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IParticipationSubjectOfCare subjectOfCareParticipation, String patientId, Boolean showEntitlements, ISO8601DateTime earliestDateForFiltering, ISO8601DateTime latestDateForFiltering, List<ICodableText> specialty = null);

        /// <summary>
        /// This method creates the narrative for the adverse subject reactions section
        /// </summary>
        /// <param name="allergiesAndAdverseReactions">List of reactions</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<Reaction> allergiesAndAdverseReactions);

        /// <summary>
        /// This method creates the narrative for the adverse subject reactions section
        /// </summary>
        /// <param name="adverseSubstanceReactions">adverseSubstanceReactions</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IAdverseReactions adverseSubstanceReactions);

        /// <summary>
        /// This method creates the narrative for the current medications section
        /// </summary>
        /// <param name="medicationsSML"></param>
        /// <returns></returns>
        StrucDocText CreateNarrative(MedicationListSML medicationsSML, bool isCurrentMedications);

        /// <summary>
        /// This method creates the narrative for the reviewed medications section
        /// </summary>
        /// <param name="reviewedMedications"></param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IMedications reviewedMedications);

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">A list of Medications</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<IMedication> medications);

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">A list of Medications</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrativeLegacy(IEnumerable<IMedicationItem> medications);

        /// <summary>
        /// This method creates the narrative for the otherTestResult section
        /// </summary>
        /// <param name="otherTestResult">Other Test Results</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(OtherTestResult otherTestResult);

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">IMedicationsEReferral</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IMedicationsEReferral medications);

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">IMedicationsSpecialistLetter</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IMedicationsSpecialistLetter medications);

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">IMedicationsSpecialistLetter</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrativeLegacy(IMedicationsSpecialistLetter medications);

        /// <summary>
        /// This method creates the narrative for the medical history section
        /// </summary>
        /// <param name="medicalHistory">medicalHistory</param>
        /// <param name="showExclusionStatements">This indicates wether exclusion statements should be shown in the narrative</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(MedicalHistory medicalHistory, bool showExclusionStatements);

        /// <summary>
        /// This method creates the narrative for the response details section
        /// </summary>
        /// <param name="responseDetails">IResponseDetails</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IResponseDetails responseDetails);

        /// <summary>
        /// This method creates the narrative for the recommendations section
        /// </summary>
        /// <param name="recommendations">IRecommendations</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IRecommendations recommendations);

        /// <summary>
        /// This method creates the narrative for the MedicareDVAFundedServices section
        /// </summary>
        /// <param name="medicareDVAFundedServices">MedicareDVAFundedServices</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(MedicareDVAFundedServices medicareDVAFundedServices);

        /// <summary>
        /// This method creates the narrative for the documents section
        /// </summary>
        /// <param name="documents">documents</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<IDocumentWithHealthEventEnded> documents);

        /// <summary>
        /// This method creates the narrative for the documents section
        /// </summary>
        /// <param name="documents">documents</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<IDocument> documents);

        /// <summary>
        /// This method creates the narrative for the reviewed immunisations section
        /// </summary>
        /// <param name="immunisations">reviewedImmunisations</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Immunisations immunisations);

        /// <summary>
        /// This method creates the narrative for the diagnostic investigations section
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigations</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IDiagnosticInvestigations diagnosticInvestigations);

        /// <summary>
        /// This method creates the narrative for the Pharmaceutical Benefit Items section
        /// </summary>
        /// <param name="pharmaceuticalBenefitItems">PharmaceuticalBenefitItems</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(PharmaceuticalBenefitItems pharmaceuticalBenefitItems);

        /// <summary>
        /// This method creates the narrative for the pathology test result section
        /// </summary>
        /// <param name="pathologyTestResult">pathologyTestResult</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(PathologyTestResult pathologyTestResult);

        /// <summary>
        /// This method creates the narrative for the imaging examination result section
        /// </summary>
        /// <param name="imagingExaminationResult">IImagingExaminationResult</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IImagingExaminationResult imagingExaminationResult);

        /// <summary>
        /// This method creates the narrative for the reason for referral section; or any section that takes in a 
        /// narrative and a date time along with a duration.
        /// </summary>
        /// <param name="dateTime">dateTime</param>
        /// <param name="duration">duration</param>
        /// <param name="narrative">narrative</param>
        /// <param name="heading">heading</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(ISO8601DateTime dateTime, CdaInterval duration, string narrative, String heading);

        /// <summary>
        /// This method creates the narrative for the XML Body File
        /// </summary>
        /// <param name="externalData">externalData</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(ExternalData externalData);

        /// <summary>
        /// This method creates the narrative for the XML Body File
        /// </summary>
        /// <param name="externalDataList">externalData</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<ExternalData> externalDataList);

        /// <summary>
        /// This method creates the narrative for Event Details
        /// </summary>
        /// <param name="eventDetails">EventDetails</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(EventDetails eventDetails);

        /// <summary>
        /// This method creates the narrative for Medication Items
        /// </summary>
        /// <param name="medicationItems">A list of IMedication Items</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IEnumerable<IMedicationItem> medicationItems);

        /// <summary>
        /// This method creates the narrative for Diagnoses Interventions
        /// </summary>
        /// <param name="diagnosesIntervention">A DiagnosesIntervention item</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(DiagnosesIntervention diagnosesIntervention);

        /// <summary>
        /// This method creates the narrative for Immunisations
        /// </summary>
        /// <param name="immunisations">A Immunisation item</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IEnumerable<IImmunisation> immunisations);

        /// <summary>
        /// This method creates the narrative for RequestedServices
        /// </summary>
        /// <param name="requestedService">A list of RequestedServices</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(RequestedService requestedService);

        /// <summary>
        /// This method creates the narrative for RequestedServices
        /// </summary>
        /// <param name="reportingRadiologist">A the ReportingRadiologist</param>
        /// <param name="relatedDocument">RelatedDocument</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IParticipationReportingRadiologist reportingRadiologist, RelatedDocument relatedDocument);

        #region eDischargeSummmary

        /// <summary>
        /// This method creates the narrative for the Problem Diagnosis  
        /// </summary>
        /// <param name="problemDiagnosesThisVisit">Problem diagnosis this visit</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(SCSModel.DischargeSummary.ProblemDiagnosesThisVisit problemDiagnosesThisVisit);

        /// <summary>
        /// This method creates the narrative for Clinical Intervention
        /// </summary>
        /// <param name="clinicalIntervention">A ClinicalIntervention</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.ClinicalIntervention clinicalIntervention);

        /// <summary>
        /// This method creates the narrative for Clinical Synopsis
        /// </summary>
        /// <param name="clinicalSynopsis">A ClinicalSynopsis</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.ClinicalSynopsis clinicalSynopsis);

        /// <summary>
        /// This method creates the narrative for a Current Medication 
        /// </summary>
        /// <param name="currentMedication">CurrentMedications</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.CurrentMedications currentMedication);

        /// <summary>
        /// This method creates the narrative for a Ceased Medication 
        /// </summary>
        /// <param name="ceasedMedications">CeasedMedications</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.CeasedMedications ceasedMedications);

        /// <summary>
        /// Creates the narrative section for adverse reactions in discharge summary.
        /// </summary>
        /// <param name="adverseReactions">Adverse reactions</param>
        /// <returns></returns>
        StrucDocText CreateNarrative(SCSModel.DischargeSummary.AdverseReactions adverseReactions);

        /// <summary>
        /// This method creates the narrative for a Alerts    
        /// </summary>
        /// <param name="alerts">A list of Alerts</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(ICollection<Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Alert> alerts);

        /// <summary>
        /// This method creates the narrative for a arrangedServices   
        /// </summary>
        /// <param name="arrangedServices">A list of ArrangedServices</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(ICollection<Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.ArrangedServices> arrangedServices);

        /// <summary>
        /// This method creates the narrative for a RecommendationsInformationProvided   
        /// </summary>
        /// <param name="recommendationsInformationProvided">RecommendationsInformationProvided</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.RecommendationsInformationProvided recommendationsInformationProvided);

        /// <summary>
        /// This method creates the narrative for the diagnostic investigations section
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigationsDischargeSummary</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.IDiagnosticInvestigationsDischargeSummary diagnosticInvestigations);

        /// <summary>
        /// This method creates the narrative for the ExclusionStatement section
        /// </summary>
        /// <param name="exclusionStatement">ExclusionStatement</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(ExclusionStatement exclusionStatement);

        #endregion

        #region ACD Custodian Record

        /// <summary>
        /// This method creates the narrative for IParticipationAcdCustodians
        /// </summary>
        /// <param name="custodianParticipations">A list of custodian entries</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IList<IParticipationAcdCustodian> custodianParticipations);

        #endregion

        #region NPDR 

        /// <summary>
        /// Create a Narrative for an IEPrescriptionItem
        /// </summary>
        /// <param name="item">A IEPrescriptionItem</param>
        /// <param name="prescriber">A IParticipationPrescriber</param>
        /// <param name="subjectOfCare">A IParticipationSubjectOfCare</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(PCEHRPrescriptionItem item, IParticipationPrescriber prescriber, IParticipationSubjectOfCare subjectOfCare);

        /// <summary>
        /// Create a Narrative for an IEDispenseItem
        /// </summary>
        /// <param name="item">A IEPrescriptionItem</param>
        /// <param name="dispenser">A IParticipationDispenser</param>
        /// <param name="dispenserOrganisation">A IParticipationDispenserOrganisation</param>
        /// <param name="subjectOfCare">A IParticipationSubjectOfCare</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(PCEHRDispenseItem item, IParticipationDispenser dispenser, IParticipationDispenserOrganisation dispenserOrganisation, IParticipationSubjectOfCare subjectOfCare);

        /// <summary>
        /// Create a Narrative for an (PrescribingAndDispensingReports)
        /// </summary>
        /// <param name="prescribingAndDispensingReports">A PrescribingAndDispensingReports</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(PrescribingAndDispensingReports prescribingAndDispensingReports);

        #endregion

        #region ATS ETP

        /// <summary>
        /// Create a Narrative for an IEPrescriptionItem
        /// </summary>
        /// <param name="item">A IEPrescriptionItem</param>
        /// <param name="prescriber">A IParticipationPrescriber</param>
        /// <param name="subjectOfCare">A IParticipationSubjectOfCare</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IEPrescriptionItem item, IParticipationPrescriber prescriber, IParticipationSubjectOfCare subjectOfCare);

        /// <summary>
        /// Create a Narrative for an DispenseItemATS
        /// </summary>
        /// <param name="item">A DispenseItemATS</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(DispenseItem item);

        /// <summary>
        /// Create a Narrative for an IObservationWeightHeight
        /// </summary>
        /// <param name="observation">IObservationWeightHeight</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IObservationWeightHeight observation);

        /// <summary>
        /// Create a Narrative for an prescriberInstructionDetail Section
        /// </summary>
        /// <param name="prescriberInstructionDetail">A prescriberInstructionDetail</param>
        /// <param name="participationPrescriber">A IParticipationPrescriber</param>
        /// <param name="participationPrescriberOrganisation">A IParticipationPrescriberOrganisation</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative
            (
                PrescriberInstructionDetail prescriberInstructionDetail,
                IParticipationPrescriber participationPrescriber,
                IParticipationPrescriberOrganisation participationPrescriberOrganisation
            );


        /// <summary>
        /// Create a Narrative for an PrescriptionRequestItem Section
        /// </summary>
        /// <param name="item">The PrescriptionRequestItem</param>
        /// <param name="subjectOfCare">The subjectOfCare</param>
        /// <param name="dispensingOrganisation">The DispensingOrganisation</param>
        /// <param name="requesterNote">The requesterNote </param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative
            (
                PrescriptionRequestItem item,
                IParticipationSubjectOfCare subjectOfCare,
                IParticipationDispenserOrganisation dispensingOrganisation,
                String requesterNote
            );

        #endregion

        #region Medicare

        /// <summary>
        /// Create a Narrative for a Medciare Overview
        /// </summary>
        /// <param name="australianChildhoodImmunisationRegisterHistory">AustralianChildhoodImmunisationRegisterHistory</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(AustralianChildhoodImmunisationRegisterHistory australianChildhoodImmunisationRegisterHistory);

        /// <summary>
        /// Create a Narrative for an Medciare Overview
        /// </summary>
        /// <param name="australianOrganDonorRegisterDetails">A AustralianOrganDonorRegisterDetails</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(AustralianOrganDonorRegisterDetails australianOrganDonorRegisterDetails);

        #endregion

        #region Consumer Entered Achievements

        /// <summary>
        /// Create a Narrative for the Achievement Section
        /// </summary>
        /// <param name="achievements">A List of Achievements</param>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<Achievement> achievements);

        #endregion

        #region Physical Measurements

        /// <summary>
        /// Create a Narrative for the Physical Measurements
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(PhysicalMeasurement physicalMeasurements);

        #endregion

        #region CeHR

        /// <summary>
        /// Create a Narrative for the Measurement Information
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(MeasurementInformation measurementInformation);

        /// <summary>
        /// Create a Narrative for the Health Check Assesment
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(HealthCheckAssesment healthCheckAssesment);

        /// <summary>
        /// Create a Narrative for the Questionnaire
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Questionnaire questionnaire);

        /// <summary>
        /// Create a Narrative for the Measurement Information
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<MeasurementInformation> measurementInformations);

        /// <summary>
        /// Create a Narrative for BirthDetails
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(BirthDetails birthDetails);

        /// <summary>
        /// Create a Narrative for QuestionnaireDocumentData
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(QuestionnaireDocumentData questionnaireDocumentData);

        /// <summary>
        /// Create a Narrative for measurementEntry
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<MeasurementEntry> measurementEntry);


        #endregion PathologyTestResult  

        #region Diagnostic Imaging Report

        /// <summary>
        /// Create a Narrative for Imaging Examination Results
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IDiagnosticImagingExaminationResult imagingExaminationResults);

        /// <summary>
        /// Create a Narrative for Authority To Post 
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(AuthorityToPost authorityToPost);

        /// <summary>
        /// Create a Narrative for Related Information 
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(Information information);

        #endregion

        #region Pathology Test Report

        /// <summary>
        /// Create a Narrative for PathologyTestResults
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(SCSModel.Pathology.PathologyTestResult pathologyTestResult);

        /// <summary>
        /// Create a Narrative for PathologyTestResults
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IParticipationReportingPathologist reportingPathologist, RelatedDocument relatedDocument);

        /// <summary>
        /// Create a Narrative for PathologyTestResults
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IList<IParticipationReportingPathologist> reportingPathologists, RelatedDocument relatedDocument);

        /// <summary>
        /// Create a Narrative for Requested Services 
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(SCSModel.Pathology.RequestedService requestedServices);

        #endregion

        #region Advance Care Information

        /// <summary>
        /// Create a Narrative for a RelatedDocument
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IDocumentDetails documentDetails, DocumentType docType);

        #endregion

        # region Service Referral

        /// <summary>
        /// Create a Narrative for Service Referral Detail 
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(ServiceReferralDetail documentDetails);

        /// <summary>
        /// Create a Narrative for Current Service
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(IList<ICurrentService> currentService);

        /// <summary>
        /// Create Diagnostic Investigations V1 narrative
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(List<IPendingDiagnosticInvestigation> service, string narrativeText);

        #endregion


        #region PCML


        /// <summary>
        /// Create a Narrative for the Questionnaire
        /// </summary>
        /// <returns>StrucDocText</returns>
        StrucDocText CreateNarrative(EncapsulatedData pcmlData);



        #endregion


    }
}
