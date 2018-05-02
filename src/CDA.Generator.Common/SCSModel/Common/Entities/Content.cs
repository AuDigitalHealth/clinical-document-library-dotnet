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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using CDA.Generator.Common.SCSModel.CeHR.Entities;
using CDA.Generator.Common.SCSModel.Common.Entities;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.SCSModel.ConsumerAchievements.Entities;
using CDA.Generator.Common.SCSModel.Interfaces;
using CDA.Generator.Common.SCSModel.MedicareOverview.Entities;
using CDA.Generator.Common.SCSModel.PCEHR.ETP.Interfaces.DispenseRecord;
using CDA.Generator.Common.SCSModel.ServiceReferral.Entities;
using CDA.Generator.Common.SCSModel.ServiceReferral.Interfaces;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Response))]
    [KnownType(typeof(Participation))]
    [KnownType(typeof(Medications))]
    [KnownType(typeof(DiagnosticInvestigations))]
    [KnownType(typeof(Item))]
    [KnownType(typeof(PCEHRPrescriptionItem))]
    [KnownType(typeof(MedicalHistoryItem))]
    [KnownType(typeof(Observation))]
    [KnownType(typeof(ProblemDiagnosis))]
    [KnownType(typeof(AdverseReactions))]
    [KnownType(typeof(Immunisation))]
    [KnownType(typeof(MedicalHistory))]
    [KnownType(typeof(Recommendations))]
    [KnownType(typeof(Reaction))]
    internal class Content : IPCEHRPrescriptionRecordContent, IPrescriptionRequestContent, IPCEHRDispenseRecordContent, ISharedHealthSummaryContent,
        IEReferralContent, ISpecialistLetterContent, IEventSummaryContent, IAcdCustodianRecordContent, IConsumerEnteredHealthSummaryContent,
        IConsumerEnteredNotesContent, IConsolidatedViewContent, IMedicareOverviewContent, IPrescriptionAndDispenseViewContent, IConsumerEnteredAchievementsContent,
        IEPrescriptionContent, IPhysicalMeasurementsContent, IDispenseRecordContent, INSWHealthCheckAssessmentContent, IPersonalHealthObservationContent,
        IConsumerQuestionnaireContent, IBirthDetailsRecordContent, IChildHealthCheckScheduleViewContent, IObservationViewDocumentContent, IPathologyResultViewContent,
        IPathologyResultReportContent, IDiagnosticImagingReportContent, IAdvanceCareInformationContent, IPathologyReportWithStructuredContentContent,
        IServiceReferralContent, IPCMLContent
    {

        #region Properties

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Custom Narrative Pathology Report With Structured Content
        /// </summary>
        public StrucDocText CustomNarrativePathologyReportWithStructuredContent { get; set; }

        IList<PathologyTestResult> IPathologyReportWithStructuredContentContent.PathologyTestResult { get; set; }

        /// <summary>
        /// Related Document
        /// </summary>
        public RelatedDocument RelatedDocument { get; set; }

        /// <summary>
        /// Custom Narrative Current Service
        /// </summary>
        public StrucDocText CustomNarrativeCurrentService { get; set; }

        /// <summary>
        /// A list of referenced documents that is the payload for this CDA document
        /// </summary>
        [DataMember]
        public List<ExternalData> StructuredBodyFiles { get; set; }

        /// <summary>
        /// A Narrative Only 1B Document
        /// </summary>
        [DataMember]
        public List<NarrativeOnlyDocument> NarrativeOnlyDocument { get; set; }

        #region SpecialistLetter Members

        [CanBeNull]
        [DataMember]
        public IResponseDetails ResponseDetails { get; set; }

        [CanBeNull]
        [DataMember]
        public IRecommendations Recommendations { get; set; }

        [CanBeNull]
        [DataMember]
        IMedicationsSpecialistLetter ISpecialistLetterContent.Medications { get; set; }

        [CanBeNull]
        [DataMember]
        IDiagnosticInvestigations ISpecialistLetterContent.DiagnosticInvestigations { get; set; }

        [CanBeNull]
        [DataMember]
        IAdverseReactionsWithoutExclusions ISpecialistLetterContent.AdverseReactions { get; set; }

        #endregion

        #region IDispenseRecordContent Members

        [CanBeNull]
        [DataMember]
        public IPCEHRDispenseItem DispenseItem { get; set; }

        #endregion

        #region IEPrescriptionRequestContent Members
        [CanBeNull]
        [DataMember]
        public PrescriberInstructionDetail PrescriberInstructionDetail { get; set; }
        [CanBeNull]
        [DataMember]
        public PrescriptionRequestItem PrescriptionRequestItem { get; set; }
        [CanBeNull]
        [DataMember]
        public String RequesterNote { get; set; }

        #endregion

        #region ATS Members
        [CanBeNull]
        [DataMember]
        public IPCEHRPrescriptionItem PrescriptionItem { get; set; }

        [CanBeNull]
        [DataMember]
        public IObservation Observation { get; set; }

        [CanBeNull]
        [DataMember]
        DispenseItem IDispenseRecordContent.DispenseItem { get; set; }

        #endregion

        #region ISharedHealthSummaryContent Members

        [CanBeNull]
        [DataMember]
        public IAdverseReactions AdverseReactions { get; set; }

        [CanBeNull]
        [DataMember]
        public IMedications Medications { get; set; }

        [CanBeNull]
        [DataMember]
        public IMedicalHistory MedicalHistory { get; set; }

        [CanBeNull]
        [DataMember]
        Immunisations ISharedHealthSummaryContent.Immunisations { get; set; }

        #endregion

        #region IReferralContent Members
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime ReferralDateTime { get; set; }

        [CanBeNull]
        [DataMember]
        public string ReferralReason { get; set; }

        [CanBeNull]
        [DataMember]
        public CdaInterval ValidityDuration { get; set; }

        [CanBeNull]
        [DataMember]
        IMedicationsEReferral IEReferralContent.Medications { get; set; }

        [CanBeNull]
        [DataMember]
        IMedicalHistory IEReferralContent.MedicalHistory { get; set; }

        [CanBeNull]
        [DataMember]
        IDiagnosticInvestigations IEReferralContent.DiagnosticInvestigations { get; set; }

        [CanBeNull]
        [DataMember]
        public IParticipationUsualGP UsualGP { get; set; }

        [CanBeNull]
        [DataMember]
        public IParticipationReferee Referee { get; set; }

        [CanBeNull]
        [DataMember]
        public IParticipationReferrer Referrer { get; set; }

        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeReferralDetail { get; set; }
        #endregion

        #region AcdCustodianRecord Members

        [CanBeNull]
        [DataMember]
        public IList<IParticipationAcdCustodian> AcdCustodians { get; set; }

        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeACDCustodianEntries { get; set; }

        #endregion

        #region ConsumerEnteredHealthSummary Members

        [CanBeNull]
        [DataMember]
        public List<Reaction> AllergiesAndAdverseReactions { get; set; }

        List<IMedication> IConsumerEnteredHealthSummaryContent.Medications { get; set; }

        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeMedications { get; set; }

        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeAllergiesAndAdverseReactions { get; set; }

        #endregion

        #region ConsumerEnteredNotes Members

        [CanBeNull]
        [DataMember]
        public string Title { get; set; }

        [CanBeNull]
        [DataMember]
        public string Description { get; set; }

        [CanBeNull]
        public StrucDocText CustomNarrativeConsumerEnteredNote { get; set; }

        public StrucDocText CustomNarrativeConsumerQuestionnaire { get; set; }

        #endregion

        #region EventSummary

        [CanBeNull]
        [DataMember]
        public EventDetails EventDetails { get; set; }

        [CanBeNull]
        [DataMember]
        IAdverseReactionsWithoutExclusions IEventSummaryContent.AdverseReactions { get; set; }

        [DataMember]
        List<IMedicationItem> IEventSummaryContent.Medications { get; set; }

        [CanBeNull]
        [DataMember]
        public DiagnosesIntervention DiagnosesIntervention { get; set; }

        [CanBeNull]
        [DataMember]
        public List<IImmunisation> Immunisations { get; set; }

        [CanBeNull]
        [DataMember]
        public IDiagnosticInvestigations DiagnosticInvestigations { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeImmunisations { get; set; }

        #endregion

        #region ConsolidatedViewContent

        /// <summary>
        /// Shared Health Summary Document Provenance (DOCUMENT PROVENANCE)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IDocument SharedHealthSummaryDocumentProvenance { get; set; }

        /// <summary>
        /// Advance Care Directive Custodian Document (Documents Provenance)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IDocument AdvanceCareDirectiveCustodianDocumentProvenance { get; set; }

        /// <summary>
        /// Advance Care Directive Custodian Document (Documents Provenance)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IDocumentWithHealthEventEnded> NewDocumentProvenance { get; set; }

        /// <summary>
        /// The reviewed adverse substance reactions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IAdverseReactions SharedHealthSummaryAdverseReactions { get; set; }

        /// <summary>
        /// The reviewed medications
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IMedications SharedHealthSummaryMedicationInstructions { get; set; }

        /// <summary>
        /// The reviewed medical history
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IMedicalHistory SharedHealthSummaryMedicalHistory { get; set; }

        /// <summary>
        /// The reviewed immunisations
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Immunisations SharedHealthSummaryImunisations { get; set; }

        /// <summary>
        /// Recent Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IDocumentWithHealthEventEnded> RecentDocumentProvenance { get; set; }

        /// <summary>
        /// Recent Diagnostic Test Result Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IDocumentWithHealthEventEnded> RecentDiagnosticTestResultDocumentProvenance { get; set; }

        /// <summary>
        /// Medicare Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IDocument> MedicareDocumentProvenance { get; set; }

        /// <summary>
        /// Consumer Entered Documents (Documents Provenance)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IDocument> ConsumerEnteredDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeSharedHealthSummaryDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeAdvanceCareDirectiveCustodianDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeNewDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeRecentDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeRecentDiagnosticTestResultDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeMedicareDocumentProvenance { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeConsumerEnteredDocumentProvenance { get; set; }

        #endregion

        #region MedicareOverview

        /// <summary>
        /// Medicare View Exclusion Statement
        /// </summary>
        [CanBeNull]
        public ExclusionStatement ExclusionStatement { get; set; }

        /// <summary>
        /// Medicare DVA Funded Services
        /// </summary>
        [CanBeNull]
        public MedicareDVAFundedServicesHistory MedicareDvaFundedServicesHistory { get; set; }

        /// <summary>
        /// Pharmaceutical Benefit Items
        /// </summary>
        [CanBeNull]
        public PharmaceuticalBenefitsHistory PharmaceuticalBenefitsHistory { get; set; }

        /// <summary>
        /// Australian Childhood Immunisation Register Component
        /// </summary>
        [CanBeNull]
        public AustralianChildhoodImmunisationRegisterHistory AustralianChildhoodImmunisationRegisterHistory { get; set; }

        /// <summary>
        /// Australian OrganDonor Register Decision Information
        /// </summary>
        [CanBeNull]
        public AustralianOrganDonorRegisterDecisionInformation AustralianOrganDonorRegisterDecisionInformation { get; set; }

        #endregion

        #region Pathology Report

        /// <summary>
        /// Pathology Test Result
        /// </summary>
        [DataMember]
        public IList<Pathology.PathologyTestResult> PathologyTestResult { get; set; }

        /// <summary>
        /// Pathology Test Result Custom Narrative
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrativePathologyTestResult { get; set; }

        /// <summary>
        /// AuthorityToPost
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AuthorityToPost AuthorityToPost { get; set; }

        /// <summary>
        /// Related Document
        /// </summary>
        [CanBeNull]
        [DataMember]
        RelatedDocument IDiagnosticImagingReportContent.RelatedDocument { get; set; }

        /// <summary>
        /// Related Document
        /// </summary>
        [CanBeNull]
        [DataMember]
        RelatedDocument IPathologyResultReportContent.RelatedDocument { get; set; }

        #endregion

        #region Pathology Report With Structured Content 

        /// <summary>
        /// Author
        /// </summary>
        public IParticipationAuthorHealthcareProvider Author { get; set; }

        /// <summary>
        /// Subject Of Care
        /// </summary>
        public IParticipationSubjectOfCare SubjectOfCare { get; set; }

        /// <summary>
        /// Reporting Pathologist
        /// </summary>
        public IParticipationReportingPathologist ReportingPathologist { get; set; }

        /// <summary>
        /// Order Details
        /// </summary>
        public OrderDetails OrderDetails { get; set; }

        #endregion

        #region Advance Care Information

        /// <summary>
        /// Related Document
        /// </summary>
        [DataMember]
        public IDocumentDetails DocumentDetails { get; set; }

        /// <summary>
        /// Provide a custom Narrative for Advance Care Information Section
        /// </summary>
        [DataMember]
        public InstanceIdentifier AdvanceCareInformationSectionInstanceIdentifier { get; set; }

        /// <summary>
        /// Advance Care Information Section
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrativeAdvanceCareInformationSection { get; set; }

        #endregion

        #region Diagnostic Imaging Report

        /// <summary>
        /// Imaging Examination Results
        /// </summary>
        [DataMember]
        public IList<IDiagnosticImagingExaminationResult> ImagingExaminationResults { get; set; }

        /// <summary>
        /// Related Information
        /// </summary>
        [DataMember]
        public IRelatedInformation RelatedInformation { get; set; }

        /// <summary>
        /// Custom Narrative for ImagingExaminationResultsCustomNarrative
        /// </summary>
        [DataMember]
        public StrucDocText DiagnosticImagingCustomNarrative { get; set; }

        #endregion 

        #region PrescriptionAndDispenseViewContent

        /// <summary>
        /// Prescribing And Dispensing Reports
        /// </summary>
        [CanBeNull]
        public PrescribingAndDispensingReports PrescribingAndDispensingReports { get; set; }

        #endregion

        #region ConsumerEnteredAchievements

        /// <summary>
        /// Provide a Section Title
        /// </summary>
        [CanBeNull]
        public string SectionTitle { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeAchievements { get; set; }

        /// <summary>
        /// A List of Achievements
        /// </summary>
        [CanBeNull]
        public List<Achievement> Achievements { get; set; }

        #endregion

        #region IEPrescriptionContent Members
        [CanBeNull]
        [DataMember]
        IEPrescriptionItem IEPrescriptionContent.PrescriptionItem { get; set; }

        [CanBeNull]
        [DataMember]
        IObservationWeightHeight IEPrescriptionContent.Observation { get; set; }

        #endregion

        #region IPhysicalMeasurementsContent Members

        public StrucDocText CustomNarrativePhysicalMeasurements { get; set; }

        public PhysicalMeasurement PhysicalMeasurement { get; set; }

        #endregion

        #region Structured Content Free Document

        public StrucDocText CustomNarrativeBirthDetailsRecord { get; set; }

        #endregion

        #region CeHR

        public MeasurementInformation MeasurementInformation { get; set; }

        public HealthCheckAssesment HealthCheckAssesment { get; set; }

        public StrucDocText CustomNarrativeNSWHealthCheckAssessment { get; set; }

        public StrucDocText CustomNarrativePersonalHealthObservation { get; set; }

        public StrucDocText CustomNarrativeNSWChildeHealthRecordBirthDetails { get; set; }

        public List<MeasurementInformation> MeasurementInformations { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public BirthDetails BirthDetails { get; set; }

        public List<QuestionnaireDocumentData> QuestionnaireDocuments { get; set; }

        public List<MeasurementEntry> ConsumerEnteredMeasurementEntry { get; set; }

        public List<MeasurementEntry> ProviderEnteredMeasurementInformationEntry { get; set; }

        #endregion

        #region Service Referral

        [DataMember]
        public ServiceReferralDetail ServiceReferralDetail { get; set; }

        [DataMember]
        IDiagnosticInvestigationsV1 IServiceReferralContent.DiagnosticInvestigations { get; set; }

        [DataMember]
        public IList<ICurrentService> CurrentService { get; set; }

        #endregion

        #region PCML

        [DataMember]
        public EncapsulatedData EncapsulatedData { get; set; }

        [DataMember]
        public StrucDocText CustomNarrativePCMLRecord { get; set; }

        #endregion


        #endregion

        #region Constructors
        internal Content()
        {
        }
        #endregion

        #region Validation



        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (
                StructuredBodyFiles != null &&
                StructuredBodyFiles.Any() &&
                (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
            )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                EncapsulatedData.Validate(path,messages);
            }
        }

        void IAdvanceCareInformationContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = (IAdvanceCareInformationContent)this;

            if (vb.ArgumentRequiredCheck("DocumentDetails", castedContent.DocumentDetails))
            {
                castedContent.DocumentDetails.Validate(vb.Path + "DocumentDetails", vb.Messages);
            }
        }

        void IDiagnosticImagingReportContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IDiagnosticImagingReportContent)this);

            if (vb.ArgumentRequiredCheck("ImagingExaminationResults", castedContent.ImagingExaminationResults))
            {
                for (var x = 0; x < castedContent.ImagingExaminationResults.Count; x++)
                    castedContent.ImagingExaminationResults[x].Validate(vb.Path + string.Format("ImagingExaminationResults[{0}]", x), vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("RelatedDocument", castedContent.RelatedDocument))
            {
                castedContent.RelatedDocument.Validate(vb.Path + "RelatedDocument", vb.Messages);


                if (castedContent.RelatedDocument.DocumentDetails != null && castedContent.RelatedDocument.DocumentDetails.ReportIdentifier != null)
                {
                    vb.AddValidationMessage(vb.Path + "RelatedDocument.DocumentDetails.ReportIdentifier", null, "ReportIdentifier not permitted for Diagnostic Imaging Report");
                }
            }
        }

        void IPathologyReportWithStructuredContentContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPathologyReportWithStructuredContentContent)this);

            if (vb.ArgumentRequiredCheck("PathologyTestResult", castedContent.PathologyTestResult))
            {
                if (!castedContent.PathologyTestResult.Any())
                {
                    vb.AddValidationMessage("PathologyTestResult", null, "Please provide a PathologyTestResult element");
                }
                else
                {
                    foreach (var pathologyTestResult in castedContent.PathologyTestResult)
                    {
                        if (!pathologyTestResult.DiagnosticService.HasValue)
                        {
                            vb.AddValidationMessage("PathologyTestResult.DiagnosticService", null, "Please provide a DiagnosticService element");
                        }
                    }
                }

                for (var y = 0; y < castedContent.PathologyTestResult.Count; y++)
                {
                    castedContent.PathologyTestResult[y].Validate(vb.Path + string.Format("PathologyTestResult {0}", y), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("RelatedDocument", castedContent.RelatedDocument))
            {
                castedContent.RelatedDocument.Validate(vb.Path + "RelatedDocument", vb.Messages);

                vb.ArgumentRequiredCheck("ReportIdentifier", castedContent.RelatedDocument);
            }
        }


        void IPathologyResultReportContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPathologyResultReportContent)this);

            if (vb.ArgumentRequiredCheck("PathologyTestResult", castedContent.PathologyTestResult))
            {
                if (!castedContent.PathologyTestResult.Any())
                {
                    vb.AddValidationMessage("PathologyTestResult", null, "Please provide a PathologyTestResult element");
                }

                for (var y = 0; y < castedContent.PathologyTestResult.Count; y++)
                {
                    castedContent.PathologyTestResult[y].Validate(vb.Path + string.Format("PathologyTestResult {0}", y), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("RelatedDocument", castedContent.RelatedDocument))
            {
                castedContent.RelatedDocument.Validate(vb.Path + "RelatedDocument", vb.Messages);

                vb.ArgumentRequiredCheck("ReportIdentifier", castedContent.RelatedDocument);
            }
        }

        void IEventSummaryContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IEventSummaryContent)this);

            if (
                StructuredBodyFiles != null && StructuredBodyFiles.Any() &&
                (
                    castedContent.EventDetails != null
                )
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (
                    StructuredBodyFiles == null || StructuredBodyFiles.Count == 0
                )
            {
                if (castedContent.EventDetails != null)
                {
                    castedContent.EventDetails.Validate(vb.Path + "EventDetails", vb.Messages);
                }

                if (castedContent.AdverseReactions != null)
                {
                    castedContent.AdverseReactions.Validate(vb.Path + "AdverseReactions", vb.Messages);
                }

                if (castedContent.Medications != null && castedContent.Medications != null)
                {
                    for (var x = 0; x < castedContent.Medications.Count; x++)
                    {
                        castedContent.Medications[x].Validate(vb.Path + string.Format("Medications.MedicationsList[{0}]", x), vb.Messages);
                    }
                }

                if (castedContent.DiagnosesIntervention != null)
                {
                    castedContent.DiagnosesIntervention.Validate(vb.Path + "DiagnosesIntervention", vb.Messages);

                    if (castedContent.DiagnosesIntervention.UncategorisedMedicalHistoryItem == null && castedContent.DiagnosesIntervention.ProblemDiagnosis == null && castedContent.DiagnosesIntervention.Procedures == null)
                    {
                        vb.AddValidationMessage(vb.Path + "DiagnosesIntervention", null, "Please provide a MedicalHistoryItem or a ProblemDiagnosis or a Procedure");
                    }
                }

                if (castedContent.Immunisations != null)
                {
                    for (var x = 0; x < castedContent.Immunisations.Count; x++)
                    {
                        castedContent.Immunisations[x].Validate(vb.Path + string.Format("Immunisations[{0}]", x), vb.Messages);
                    }
                }

                if (castedContent.DiagnosticInvestigations != null)
                {
                    castedContent.DiagnosticInvestigations.Validate(vb.Path + "DiagnosticInvestigation", vb.Messages);
                }
            }
        }

        void ISharedHealthSummaryContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((ISharedHealthSummaryContent)this);

            if (
                StructuredBodyFiles != null &&
                StructuredBodyFiles.Any() &&
                (
                    castedContent.MedicalHistory != null ||
                    castedContent.Medications != null ||
                    castedContent.AdverseReactions != null ||
                    castedContent.Immunisations != null
                )
            )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (
                    StructuredBodyFiles == null ||
                    StructuredBodyFiles.Count == 0
                )
            {

                if (vb.ArgumentRequiredCheck("AdverseReactions", AdverseReactions))
                {
                    AdverseReactions.Validate(vb.Path + "AdverseReactions", vb.Messages);

                    if (AdverseReactions.ExclusionStatement != null &&
                        AdverseReactions.ExclusionStatement.Value.HasValue &&
                        AdverseReactions.ExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
                    {
                        vb.AddValidationMessage("AdverseReactions.ExclusionStatement", null, "The value/@code SHALL NOT be 02.");
                    }
                }

                if (vb.ArgumentRequiredCheck("Medications", Medications))
                {
                    Medications.Validate(vb.Path + "Medications", vb.Messages);

                    if (Medications.ExclusionStatement != null &&
                        Medications.ExclusionStatement.Value.HasValue &&
                        Medications.ExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
                    {
                        vb.AddValidationMessage("Medications.ExclusionStatement", null, "The value/@code SHALL NOT be 02.");
                    }
                }

                if (vb.ArgumentRequiredCheck("MedicalHistory", MedicalHistory))
                {
                    MedicalHistory.ValidateShs(vb.Path + "MedicalHistory", vb.Messages, true);
                }

                if (vb.ArgumentRequiredCheck("Immunisations", castedContent.Immunisations))
                {
                    castedContent.Immunisations.Validate(vb.Path + "Immunisations", vb.Messages);

                    if (castedContent.Immunisations != null && castedContent.Immunisations.ExclusionStatement != null && castedContent.Immunisations.ExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
                    {
                        vb.AddValidationMessage("Immunisations.ExclusionStatement", null, "The value/@code SHALL NOT be 02.");
                    }
                }
            }
        }

        void IAcdCustodianRecordContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IAcdCustodianRecordContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    castedContent.AcdCustodians != null
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                if (vb.ArgumentRequiredCheck("AcdCustodians", castedContent.AcdCustodians))
                {
                    for (var x = 0; x < castedContent.AcdCustodians.Count; x++)
                    {
                        var currentAcdCustodians = castedContent.AcdCustodians[x];

                        if (vb.ArgumentRequiredCheck(string.Format("AcdCustodians[{0}]", x), currentAcdCustodians))
                            currentAcdCustodians.Validate(vb.Path + string.Format("AcdCustodians[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        void IEReferralContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IEReferralContent)this);

            if (
                  castedContent.NarrativeOnlyDocument != null && castedContent.NarrativeOnlyDocument.Any() &&
                    (
                        castedContent.MedicalHistory != null ||
                        castedContent.Medications != null ||
                        castedContent.AdverseReactions != null ||
                        castedContent.DiagnosticInvestigations != null ||
                        castedContent.StructuredBodyFiles != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NarrativeOnlyDocument", null, "Both structured body components and a NarrativeOnlyDocument have been specified; only one instance of these is allowed.");
            }

            if (
                  castedContent.StructuredBodyFiles != null && castedContent.StructuredBodyFiles.Any() &&
                    (
                        castedContent.MedicalHistory != null ||
                        castedContent.Medications != null ||
                        castedContent.AdverseReactions != null ||
                        castedContent.DiagnosticInvestigations != null ||
                        castedContent.NarrativeOnlyDocument != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if ((castedContent.StructuredBodyFiles == null || !castedContent.StructuredBodyFiles.Any()) && (castedContent.NarrativeOnlyDocument == null || !castedContent.NarrativeOnlyDocument.Any()))
            {
                // Referral detail
                if (castedContent.UsualGP != null)
                {
                    castedContent.UsualGP.Validate(vb.Path + "usualGP", messages);
                }

                if (vb.ArgumentRequiredCheck("Referee", castedContent.Referee))
                {
                    castedContent.Referee.Validate(vb.Path + "Referee", vb.Messages);
                }

                if (vb.ArgumentRequiredCheck("ValidityDuration", castedContent.ValidityDuration))
                {
                    castedContent.ValidityDuration.Validate(vb.Path + "ValidityDuration", vb.Messages);
                }

                vb.ArgumentRequiredCheck("ReferralDateTime", castedContent.ReferralDateTime);

                vb.ArgumentRequiredCheck("ReferralReason", castedContent.ReferralReason);

                // Medical history
                if (vb.ArgumentRequiredCheck("MedicalHistory", castedContent.MedicalHistory))
                {
                    castedContent.MedicalHistory.Validate(vb.Path + "MedicalHistory", vb.Messages, false);
                }

                // Medications
                if (vb.ArgumentRequiredCheck("Medications", castedContent.Medications))
                {
                    castedContent.Medications.Validate(vb.Path + "Medications", vb.Messages);
                }

                // Adverse reactions
                if (vb.ArgumentRequiredCheck("AdverseReactions", castedContent.AdverseReactions))
                {
                    AdverseReactions.Validate(vb.Path + "AdverseReactions", vb.Messages);
                }

                // Diagnostic investigations
                if (castedContent.DiagnosticInvestigations != null)
                {
                    castedContent.DiagnosticInvestigations.Validate(vb.Path + "DiagnosticInvestigations", vb.Messages);
                }
            }
            else
            {
                if (vb.ArgumentRequiredCheck("Referee", castedContent.Referee))
                {
                    castedContent.Referee.Validate(vb.Path + "Referee", vb.Messages);
                }
            }
        }

        void ISpecialistLetterContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((ISpecialistLetterContent)this);

            if (
                  castedContent.NarrativeOnlyDocument != null && castedContent.NarrativeOnlyDocument.Any() &&
                    (
                        castedContent.DiagnosticInvestigations != null ||
                        castedContent.Medications != null ||
                        castedContent.Recommendations != null ||
                        castedContent.ResponseDetails != null ||
                        castedContent.AdverseReactions != null ||
                        castedContent.StructuredBodyFiles != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NarrativeOnlyDocument", null, "Both structured body components and a NarrativeOnlyDocument have been specified; only one instance of these is allowed.");
            }

            if (castedContent.StructuredBodyFiles != null && castedContent.StructuredBodyFiles.Any() &&
                    (
                        castedContent.DiagnosticInvestigations != null ||
                        castedContent.Medications != null ||
                        castedContent.Recommendations != null ||
                        castedContent.ResponseDetails != null ||
                        castedContent.AdverseReactions != null ||
                        castedContent.NarrativeOnlyDocument != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }


            if ((castedContent.StructuredBodyFiles == null || !castedContent.StructuredBodyFiles.Any()) && (castedContent.NarrativeOnlyDocument == null || !castedContent.NarrativeOnlyDocument.Any()))
            {
                // Response details
                if (vb.ArgumentRequiredCheck("Response Details", castedContent.ResponseDetails))
                {
                    castedContent.ResponseDetails.Validate(vb.Path + "ResponseDetails", vb.Messages);
                }

                // Recommendations
                if (vb.ArgumentRequiredCheck("Recommendations", castedContent.Recommendations))
                {
                    castedContent.Recommendations.Validate(vb.Path + "Recommendations", vb.Messages);
                }

                // Medications
                if (vb.ArgumentRequiredCheck("Medications", castedContent.Medications))
                {
                    castedContent.Medications.Validate(vb.Path + "Medications", vb.Messages);
                }

                // Newly identified allergies and adverse reactions
                if (castedContent.AdverseReactions != null)
                {
                    castedContent.AdverseReactions.Validate(vb.Path + "AdverseReactions", vb.Messages);
                }

                // Diagnostic investigations
                if (castedContent.DiagnosticInvestigations != null)
                {
                    castedContent.DiagnosticInvestigations.Validate(vb.Path + "DiagnosticInvestigations", vb.Messages);
                }
            }
        }

        void IPCEHRPrescriptionRecordContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPCEHRPrescriptionRecordContent)this);

            if (vb.ArgumentRequiredCheck("PrescriptionItem", castedContent.PrescriptionItem))
            {
                castedContent.PrescriptionItem.Validate(vb.Path + "PrescriptionItem", vb.Messages);
            }
        }

        void IPrescriptionRequestContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPrescriptionRequestContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (
                        castedContent.PrescriberInstructionDetail != null ||
                        castedContent.PrescriptionRequestItem != null ||
                        !castedContent.RequesterNote.IsNullOrEmptyWhitespace()
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (
                    StructuredBodyFiles == null ||
                    StructuredBodyFiles.Count == 0
                )
            {

                if (vb.ArgumentRequiredCheck("PrescriberInstructionDetail", castedContent.PrescriberInstructionDetail))
                {
                    if (castedContent.PrescriberInstructionDetail != null)
                        castedContent.PrescriberInstructionDetail.Validate(vb.Path + "PrescriberInstructionDetail", vb.Messages);
                }

                if (vb.ArgumentRequiredCheck("PrescriptionRequestItem", castedContent.PrescriptionRequestItem))
                {
                    if (castedContent.PrescriptionRequestItem != null)
                        castedContent.PrescriptionRequestItem.Validate(vb.Path + "PrescriptionRequestItem", vb.Messages);
                }
            }
        }

        void IPCEHRDispenseRecordContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPCEHRDispenseRecordContent)this);

            if (vb.ArgumentRequiredCheck("DispenseItem", castedContent.DispenseItem))
            {
                if (castedContent.DispenseItem != null) castedContent.DispenseItem.Validate(vb.Path + "DispenseItem", vb.Messages);
            }
        }

        void IConsumerEnteredNotesContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IConsumerEnteredNotesContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                vb.ArgumentRequiredCheck("Title", Title);
                vb.ArgumentRequiredCheck("Description", Description);
            }
        }

        void IConsumerEnteredHealthSummaryContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = (IConsumerEnteredHealthSummaryContent)this;

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (AdverseReactions != null || Medications != null)
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                if (castedContent.AllergiesAndAdverseReactions != null)
                {
                    for (var x = 0; x < castedContent.AllergiesAndAdverseReactions.Count; x++)
                    {
                        var currentAllergy = castedContent.AllergiesAndAdverseReactions[x];

                        if (vb.ArgumentRequiredCheck(string.Format("AllergiesAndAdverseReactions[{0}]", x), currentAllergy))
                            currentAllergy.Validate(vb.Path + string.Format("AllergiesAndAdverseReactions[{0}]", x), vb.Messages);
                        {
                            if (currentAllergy.SubstanceOrAgent != null)
                                currentAllergy.SubstanceOrAgent.Validate(path, messages);
                        }

                        if (currentAllergy != null &&
                            currentAllergy.ReactionEvent != null &&
                            currentAllergy.ReactionEvent.Manifestations != null)
                        {
                            for (var y = 0; y < currentAllergy.ReactionEvent.Manifestations.Count; y++)
                            {
                                var manifestation = currentAllergy.ReactionEvent.Manifestations[y];
                                manifestation.Validate(string.Format("{0}.Manifestations[{1}]", path, y), messages);
                            }
                        }
                    }
                }

                if (castedContent.Medications != null)
                {
                    for (var x = 0; x < castedContent.Medications.Count; x++)
                    {
                        if (vb.ArgumentRequiredCheck(string.Format("Medications[{0}]", x), castedContent.Medications[x]))
                        {
                            castedContent.Medications[x].Validate(vb.Path + string.Format("Medications[{0}]", x), vb.Messages);
                        }
                    }
                }
            }
        }

        void IConsolidatedViewContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IConsolidatedViewContent)this);

            if (vb.ArgumentRequiredCheck("SharedHealthSummaryAdverseReactions", castedContent.SharedHealthSummaryAdverseReactions))
            {
                castedContent.SharedHealthSummaryAdverseReactions.Validate(vb.Path + "SharedHealthSummaryAdverseReactions", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SharedHealthSummaryMedicationInstructions", castedContent.SharedHealthSummaryMedicationInstructions))
            {
                castedContent.SharedHealthSummaryMedicationInstructions.Validate(vb.Path + "SharedHealthSummaryMedicationInstructions", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SharedHealthSummaryMedicalHistory", castedContent.SharedHealthSummaryMedicalHistory))
            {
                castedContent.SharedHealthSummaryMedicalHistory.Validate(vb.Path + "SharedHealthSummaryMedicalHistory", vb.Messages, true);
            }

            if (vb.ArgumentRequiredCheck("SharedHealthSummaryImmunisations", castedContent.SharedHealthSummaryImunisations))
            {
                castedContent.SharedHealthSummaryImunisations.Validate(vb.Path + "SharedHealthSummaryImmunisations", vb.Messages);
            }

            if (castedContent.SharedHealthSummaryDocumentProvenance != null)
            {
                castedContent.SharedHealthSummaryDocumentProvenance.Validate(vb.Path + "SharedHealthSummaryDocumentProvenance[{0}]", vb.Messages);
                if (vb.ArgumentRequiredCheck("SharedHealthSummaryDocumentProvenance.Author", castedContent.SharedHealthSummaryDocumentProvenance.Author))
                {
                    if (castedContent.SharedHealthSummaryDocumentProvenance.Author.Participant != null)
                    {
                        vb.ArgumentRequiredCheck("SharedHealthSummaryDocumentProvenance.Author.Participant.Addresses", castedContent.SharedHealthSummaryDocumentProvenance.Author.Participant.Addresses);
                        vb.ArgumentRequiredCheck("SharedHealthSummaryDocumentProvenance.Author.Participant.ElectronicCommunicationDetails", castedContent.SharedHealthSummaryDocumentProvenance.Author.Participant.ElectronicCommunicationDetails);
                        if (castedContent.SharedHealthSummaryDocumentProvenance.Author.Participant.Person != null)
                            vb.ArgumentRequiredCheck("SharedHealthSummaryDocumentProvenance.Author.Participant.Person.Organisation", castedContent.SharedHealthSummaryDocumentProvenance.Author.Participant.Person.Organisation);
                    }
                }
            }

            if (castedContent.AdvanceCareDirectiveCustodianDocumentProvenance != null)
            {
                castedContent.AdvanceCareDirectiveCustodianDocumentProvenance.Validate(vb.Path + "AdvanceCareDirectiveCustodianDocumentProvenance", vb.Messages);
            }

            if (castedContent.ConsumerEnteredDocumentProvenance != null)
            {
                for (var x = 0; x < castedContent.ConsumerEnteredDocumentProvenance.Count; x++)
                {
                    castedContent.ConsumerEnteredDocumentProvenance[x].Validate(vb.Path + string.Format("ConsumerEnteredDocumentProvenance[{0}]", x), vb.Messages);
                    vb.ArgumentRequiredCheck(string.Format("ConsumerEnteredDocumentProvenance[{0}].Author", x), castedContent.ConsumerEnteredDocumentProvenance[x].Author);
                }
            }

            if (castedContent.MedicareDocumentProvenance != null)
            {
                for (var x = 0; x < castedContent.MedicareDocumentProvenance.Count; x++)
                {
                    castedContent.MedicareDocumentProvenance[x].Validate(vb.Path + string.Format("MedicareDocumentProvenance[{0}]", x), vb.Messages);
                    if (castedContent.MedicareDocumentProvenance[x].Author != null)
                    {
                        vb.AddValidationMessage(vb.Path + string.Format(".MedicareDocumentProvenance[{0}]", x), null, "Medicare Document Provenance section can not contain an Author");
                    }
                }
            }

            if (castedContent.AdvanceCareDirectiveCustodianDocumentProvenance != null)
            {
                castedContent.AdvanceCareDirectiveCustodianDocumentProvenance.Validate(vb.Path + "AdvanceCareDirectiveCustodianDocumentProvenance", vb.Messages);
                if (castedContent.AdvanceCareDirectiveCustodianDocumentProvenance.Author != null)
                {
                    vb.AddValidationMessage(vb.Path + ".AdvanceCareDirectiveCustodianDocumentProvenance", null, "Advance Care Directive Custodian Document Provenance section can not contain an Author");
                }
            }

            if (castedContent.NewDocumentProvenance != null)
            {
                for (var x = 0; x < castedContent.NewDocumentProvenance.Count; x++)
                {
                    castedContent.NewDocumentProvenance[x].Validate(vb.Path + string.Format("NewDocumentProvenance[{0}]", x), vb.Messages);
                    vb.ArgumentRequiredCheck(string.Format("NewDocumentProvenance[{0}].Author", x), castedContent.NewDocumentProvenance[x].Author);
                }
            }

            if (castedContent.RecentDiagnosticTestResultDocumentProvenance != null)
            {
                for (var x = 0; x < castedContent.RecentDiagnosticTestResultDocumentProvenance.Count; x++)
                {
                    castedContent.RecentDiagnosticTestResultDocumentProvenance[x].Validate(vb.Path + string.Format("RecentDiagnosticTestResultDocumentProvenance[{0}]", x), vb.Messages);
                    vb.ArgumentRequiredCheck(string.Format("RecentDiagnosticTestResultDocumentProvenance[{0}].Author", x), castedContent.RecentDiagnosticTestResultDocumentProvenance[x].Author);
                }
            }

            if (castedContent.RecentDocumentProvenance != null)
            {
                for (var x = 0; x < castedContent.RecentDocumentProvenance.Count; x++)
                {
                    castedContent.RecentDocumentProvenance[x].Validate(vb.Path + string.Format("RecentDocumentProvenance[{0}]", x), vb.Messages);
                    vb.ArgumentRequiredCheck(string.Format("RecentDocumentProvenance[{0}].Author", x), castedContent.RecentDocumentProvenance[x].Author);
                }
            }
        }

        void IPrescriptionAndDispenseViewContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPrescriptionAndDispenseViewContent)this);

            if ((castedContent.ExclusionStatement == null && castedContent.PrescribingAndDispensingReports == null) || (castedContent.ExclusionStatement != null && castedContent.PrescribingAndDispensingReports != null))
            {
                vb.AddValidationMessage(vb.Path + "MedicationEntriesWithSummary", null,
                                        "Each instance of this composition SHALL have one instance of EXCLUSION STATEMENT or one instance of 'Prescribing and Dispensing Reports' but not instances of both.");
            }

            if (castedContent.ExclusionStatement != null)
            {
                if (vb.ArgumentRequiredCheck("ExclusionStatement", castedContent.ExclusionStatement))
                    castedContent.ExclusionStatement.Validate(vb.Path + "ExclusionStatement", vb.Messages);
            }
            else
            {
                if (PrescribingAndDispensingReports != null)
                {
                    PrescribingAndDispensingReports.Validate(vb.Path + "PrescribingAndDispensingReports", vb.Messages);
                }
            }
        }

        void IPathologyResultViewContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPathologyResultViewContent)this);
            castedContent.Validate(path, messages);
        }

        //public void Validate(string path, List<ValidationMessage> messages)
        //{
        //    var vb = new ValidationBuilder(path, messages);
        //}

        void IMedicareOverviewContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IMedicareOverviewContent)this);

            if (ExclusionStatement == null && MedicareDvaFundedServicesHistory == null && PharmaceuticalBenefitsHistory == null &&
                AustralianChildhoodImmunisationRegisterHistory == null && AustralianOrganDonorRegisterDecisionInformation == null)
                vb.AddValidationMessage(vb.Path + "IMedicareOverviewContent", null, "This IMedicareOverviewContent model SHALL have exactly one instance of 'Medciare Overview Exclusion Statement OR SHALL " +
                                                  "have one instance of each of the following sections: MedicareDVAFundedServicesHistory, PharmaceuticalBenefitsHistory, AustralianChildhoodImmunisationRegisterHistory, AustralianOrganDonorRegisterDecisionInformation");


            if (ExclusionStatement != null &&
               (MedicareDvaFundedServicesHistory != null ||
                PharmaceuticalBenefitsHistory != null ||
                AustralianChildhoodImmunisationRegisterHistory != null ||
                AustralianOrganDonorRegisterDecisionInformation != null)
               )
                vb.AddValidationMessage(vb.Path + "IMedicareOverviewContent", null, "This IMedicareOverviewContent model SHALL have exactly one instance of 'Medciare Overview Exclusion Statement OR SHALL " +
                                                      "have one instance of each of the following sections: MedicareDVAFundedServicesHistory, PharmaceuticalBenefitsHistory, AustralianChildhoodImmunisationRegisterHistory, AustralianOrganDonorRegisterDecisionInformation");


            if (ExclusionStatement != null)
            {
                if (vb.ArgumentRequiredCheck("ExclusionStatement", castedContent.ExclusionStatement))
                    castedContent.ExclusionStatement.Validate(vb.Path + "ExclusionStatement", vb.Messages);

            }
            else
            {
                if (vb.ArgumentRequiredCheck("MedicareDVAFundedServicesHistory", castedContent.MedicareDvaFundedServicesHistory))
                    castedContent.MedicareDvaFundedServicesHistory.Validate(vb.Path + "MedicareDVAFundedServicesHistory", vb.Messages);

                if (vb.ArgumentRequiredCheck("PharmaceuticalBenefitsHistory", castedContent.PharmaceuticalBenefitsHistory))
                    castedContent.PharmaceuticalBenefitsHistory.Validate(vb.Path + "PharmaceuticalBenefitsHistory", vb.Messages);

                if (vb.ArgumentRequiredCheck("AustralianChildhoodImmunisationRegisterHistory", castedContent.AustralianChildhoodImmunisationRegisterHistory))
                    castedContent.AustralianChildhoodImmunisationRegisterHistory.Validate(vb.Path + "AustralianChildhoodImmunisationRegisterHistory", vb.Messages);

                if (vb.ArgumentRequiredCheck("AustralianOrganDonorRegisterDecisionInformation", castedContent.AustralianOrganDonorRegisterDecisionInformation))
                    castedContent.AustralianOrganDonorRegisterDecisionInformation.Validate(vb.Path + "AustralianOrganDonorRegisterDecisionInformation", vb.Messages);

            }
        }

        void IConsumerEnteredAchievementsContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IConsumerEnteredAchievementsContent)this);

            vb.ArgumentRequiredCheck("SectionTitle", SectionTitle);

            if (vb.ArgumentRequiredCheck("Achievements", castedContent.Achievements))
            {
                for (var x = 0; x < castedContent.Achievements.Count; x++)
                {
                    castedContent.Achievements[x].Validate(vb.Path + string.Format("Achievements[{0}]", x), vb.Messages);
                }
            }
        }

        void IEPrescriptionContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IEPrescriptionContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Count > 0 &&
                    (
                        castedContent.PrescriptionItem != null ||
                        castedContent.Observation != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (
                  StructuredBodyFiles == null || StructuredBodyFiles.Count == 0
                )
            {

                if (vb.ArgumentRequiredCheck("PrescriptionItem", castedContent.PrescriptionItem))
                {
                    if (castedContent.PrescriptionItem != null)
                        castedContent.PrescriptionItem.Validate(vb.Path + "PrescriptionItem", vb.Messages);
                }

                if (castedContent.Observation != null)
                {
                    castedContent.Observation.Validate(vb.Path + "Observation", vb.Messages);
                }
            }
        }

        void IPhysicalMeasurementsContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPhysicalMeasurementsContent)this);

            if (vb.ArgumentRequiredCheck("PhysicalMeasurement", castedContent.PhysicalMeasurement))
            {
                castedContent.PhysicalMeasurement.Validate(vb.Path + string.Format("PhysicalMeasurement"), vb.Messages);
            }
        }

        void IDispenseRecordContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IDispenseRecordContent)this);

            if (vb.ArgumentRequiredCheck("DispenseItem", castedContent.DispenseItem))
            {
                castedContent.DispenseItem.Validate(path, messages);
            }
        }

        void IBirthDetailsRecordContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {

                if (BirthDetails != null)
                {
                    BirthDetails.Validate(path, messages);
                }
            }
        }

        #region CeHR

        void INSWHealthCheckAssessmentContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((INSWHealthCheckAssessmentContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                if (MeasurementInformation != null)
                {
                    MeasurementInformation.Validate(path, messages);
                }

                if (HealthCheckAssesment != null)
                {
                    HealthCheckAssesment.Validate(path, messages);
                }
            }
        }

        void IChildHealthCheckScheduleViewContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IChildHealthCheckScheduleViewContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                if (castedContent.QuestionnaireDocuments != null)
                {
                    for (var x = 0; x < castedContent.QuestionnaireDocuments.Count; x++)
                    {
                        var questionnaireDocuments = castedContent.QuestionnaireDocuments[x];

                        if (vb.ArgumentRequiredCheck(string.Format("QuestionnaireDocuments[{0}]", x), questionnaireDocuments))
                            questionnaireDocuments.Validate(vb.Path + string.Format("QuestionnaireDocuments[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        void IObservationViewDocumentContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IObservationViewDocumentContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                if (castedContent.ConsumerEnteredMeasurementEntry != null)
                {
                    for (var x = 0; x < castedContent.ConsumerEnteredMeasurementEntry.Count; x++)
                    {
                        var consumerEnteredMeasurementEntry = castedContent.ConsumerEnteredMeasurementEntry[x];

                        if (vb.ArgumentRequiredCheck(string.Format("ConsumerEnteredMeasurementEntry[{0}]", x), consumerEnteredMeasurementEntry))
                            consumerEnteredMeasurementEntry.Validate(vb.Path + string.Format("ConsumerEnteredMeasurementEntry[{0}]", x), vb.Messages);
                    }
                }

                if (castedContent.ProviderEnteredMeasurementInformationEntry != null)
                {
                    for (var x = 0; x < castedContent.ProviderEnteredMeasurementInformationEntry.Count; x++)
                    {
                        var providerEnteredMeasurementInformationEntry = castedContent.ProviderEnteredMeasurementInformationEntry[x];

                        if (vb.ArgumentRequiredCheck(string.Format("ProviderEnteredMeasurementInformationEntry[{0}]", x), providerEnteredMeasurementInformationEntry))
                            providerEnteredMeasurementInformationEntry.Validate(vb.Path + string.Format("ProviderEnteredMeasurementInformationEntry[{0}]", x), vb.Messages);
                    }
                }

                //if (castedContent.ConsumerEnteredMeasurementEntry == null && castedContent.ProviderEnteredMeasurementInformationEntry == null)
                //{
                //  vb.AddValidationMessage("ConsumerEnteredMeasurementEntry, ProviderEnteredMeasurementInformationEntry", null, "Please provide a Measurement Information Entry");
                //}

            }
        }

        void IConsumerQuestionnaireContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {
                if (Questionnaire != null)
                {
                    Questionnaire.Validate(path, messages);
                }
            }
        }

        void IPersonalHealthObservationContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IPersonalHealthObservationContent)this);

            if (
                    StructuredBodyFiles != null &&
                    StructuredBodyFiles.Any() &&
                    (!Title.IsNullOrEmptyWhitespace() || !Description.IsNullOrEmptyWhitespace())
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (StructuredBodyFiles == null || StructuredBodyFiles.Count == 0)
            {

                vb.ArgumentRequiredCheck("MeasurementInformations", castedContent.MeasurementInformations);

                if (MeasurementInformation != null)
                {
                    for (var x = 0; x < castedContent.MeasurementInformations.Count; x++)
                    {
                        var measurementInformations = castedContent.MeasurementInformations[x];

                        if (vb.ArgumentRequiredCheck(string.Format("MeasurementInformations[{0}]", x), measurementInformations))
                            measurementInformations.Validate(vb.Path + string.Format("MeasurementInformations[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        #endregion

        #region Service Referral

        void IServiceReferralContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = (IServiceReferralContent)this;

            if (
                StructuredBodyFiles != null && StructuredBodyFiles.Any() &&
               (
                    castedContent.Medications != null ||
                    castedContent.AdverseReactions != null ||
                    castedContent.MedicalHistory != null ||
                    castedContent.CurrentService != null ||
                    castedContent.DiagnosticInvestigations != null ||
                    castedContent.ServiceReferralDetail != null
                )
            )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if (
                    StructuredBodyFiles == null ||
                    StructuredBodyFiles.Count == 0
                )
            {
                if (AdverseReactions != null)
                {
                    AdverseReactions.Validate(vb.Path + "AdverseReactions", vb.Messages);

                    if (AdverseReactions.ExclusionStatement != null &&
                        AdverseReactions.ExclusionStatement.Value.HasValue &&
                        AdverseReactions.ExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
                    {
                        vb.AddValidationMessage("AdverseReactions.ExclusionStatement", null, "The value/@code SHALL NOT be 02.");
                    }
                }

                if (Medications != null)
                {
                    Medications.Validate(vb.Path + "Medications", vb.Messages);

                    if (Medications.ExclusionStatement?.Value != null && Medications.ExclusionStatement.Value == NCTISGlobalStatementValues.NotAsked)
                    {
                        vb.AddValidationMessage("Medications.ExclusionStatement", null, "The value/@code SHALL NOT be 02.");
                    }
                }

                castedContent.DiagnosticInvestigations?.Validate(vb.Path + "DiagnosticInvestigations", vb.Messages);

                castedContent.ServiceReferralDetail?.Validate(vb.Path + "ServiceReferralDetail", vb.Messages);

                if (castedContent.MedicalHistory != null)
                {
                    MedicalHistory.ValidateShs(vb.Path + "MedicalHistory", vb.Messages, true);
                }

                if (castedContent.CurrentService != null)
                {
                    for (var x = 0; x < castedContent.CurrentService.Count; x++)
                    {
                        castedContent.CurrentService[x].Validate(vb.Path + $"CurrentService[{x}]", vb.Messages);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}