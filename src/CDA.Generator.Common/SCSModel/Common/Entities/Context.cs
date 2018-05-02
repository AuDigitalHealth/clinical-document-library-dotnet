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
using System.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;
using CDA.Generator.Common.Common.Time.Enum;
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Participation))]
    [KnownType(typeof(CodableText))]
    internal class Context : IPCEHRPrescriptionRecordContext, IPrescriptionRequestContext, IPCEHRDispenseRecordContext, ISharedHealthSummaryContext, 
        IEReferralContext, ISpecialistLetterContext, IEventSummaryContext, IAcdCustodianRecordContext, IConsumerEnteredHealthSummaryContext,
        IConsumerEnteredNotesContext, IConsolidatedViewContext, IMedicareOverviewContext, IPrescriptionAndDispenseViewContext,
        IConsumerEnteredAchievementsContext, IEPrescriptionContext, IPhysicalMeasurementsContext, IDispenseRecordContext, INSWHealthCheckAssessmentContext,
        IPersonalHealthObservationContext, IConsumerQuestionnaireContext, IBirthDetailsRecordContext, IChildHealthCheckScheduleViewContext, IObservationViewDocumentContext,
        IPathologyResultViewContext, IPathologyResultReportContext, IDiagnosticImagingReportContext, IAdvanceCareInformationContext, IPathologyReportWithStructuredContentContext,
        IServiceReferralContext
    {
        #region Properties

        [DataMember]
        IParticipationConsumerAuthor IConsumerEnteredHealthSummaryContext.Author { get; set; }

        IParticipationAuthorHealthcareProvider IPathologyReportWithStructuredContentContext.Author { get; set; }

        [DataMember]
        public IParticipationSubjectOfCare SubjectOfCare { get; set; }

        [DataMember]
        IAuthorCollection IPhysicalMeasurementsContext.Author { get; set; }

        [DataMember]
        IAuthorCollection IBirthDetailsRecordContext.Author { get; set; }

        [DataMember]
        IParticipationConsumerAuthor IConsumerEnteredNotesContext.Author { get; set; }

        [DataMember]
        public IParticipationDocumentAuthor Author { get; set; }

        [DataMember]
        IParticipationConsumerAuthor IConsumerEnteredAchievementsContext.Author { get; set; }

        [DataMember]
        AuthorAuthoringDevice IConsolidatedViewContext.Author { get; set; }

        [DataMember]
        IParticipationAuthorHealthcareProvider IPathologyResultReportContext.Author { get; set; }

        [DataMember]
        IParticipationAuthorHealthcareProvider IDiagnosticImagingReportContext.Author { get; set; }

        [CanBeNull]
        [DataMember]
        public AuthorAuthoringDevice AuthorDevice { get; set; }

        [DataMember]
        AuthorAuthoringDevice IPrescriptionAndDispenseViewContext.Author { get; set; }

        [DataMember]
        AuthorAuthoringDevice IPathologyResultViewContext.Author { get; set; }

        [DataMember]
        AuthorAuthoringDevice IMedicareOverviewContext.Author { get; set; }

        [DataMember]
        AuthorAuthoringDevice IChildHealthCheckScheduleViewContext.Author { get; set; }

        [DataMember]
        AuthorAuthoringDevice IObservationViewDocumentContext.Author { get; set; }

        [CanBeNull]
        public IParticipationUploadAuthoriser UploadAuthoriser { get; set; }

        [CanBeNull]
        [DataMember]
        public ISO8601DateTime Attested { get; set; }

        [DataMember]
        public CdaInterval EncounterPeriod { get; set; }

        [DataMember]
        public NullFlavour? EncounterPeriodNullFlavor { get; set; } 

        [DataMember]
        public IParticipationPrescriber Prescriber { get; set; }

        [DataMember]
        public IParticipationPrescriberOrganisation PrescriberOrganisation { get; set; }

        [DataMember]
        public IParticipationDispenser Dispenser { get; set; }

        [DataMember]
        public IParticipationDispenserOrganisation DispenserOrganisation { get; set; }

        [DataMember]
        IAuthorCollection IAdvanceCareInformationContext.Author { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPCEHRPrescriptionRecordContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IAdvanceCareInformationContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPathologyResultReportContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IDiagnosticImagingReportContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IDispenseRecordContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPrescriptionRequestContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPrescriptionAndDispenseViewContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPathologyResultViewContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPCEHRDispenseRecordContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare ISharedHealthSummaryContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IEReferralContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IAcdCustodianRecordContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IConsumerEnteredNotesContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IConsumerEnteredHealthSummaryContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IConsolidatedViewContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IMedicareOverviewContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IConsumerEnteredAchievementsContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IEPrescriptionContext.SubjectOfCare { get; set; }

        [DataMember]
        IList<IParticipationPatientNominatedContact> IEReferralContext.PatientNominatedContacts { get; set; }

        [DataMember]
        IParticipationSubjectOfCare ISpecialistLetterContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IEventSummaryContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPhysicalMeasurementsContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare INSWHealthCheckAssessmentContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IPersonalHealthObservationContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IConsumerQuestionnaireContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IBirthDetailsRecordContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IChildHealthCheckScheduleViewContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IObservationViewDocumentContext.SubjectOfCare { get; set; }

        [DataMember]
        IParticipationSubjectOfCare IServiceReferralContext.SubjectOfCare { get; set; }

        [DataMember]
        public IParticipationUsualGP UsualGP { get; set; }

        [DataMember]
        public IParticipationReferrer Referrer { get; set; }

        [DataMember]
        public IParticipationHealthcareFacility HealthcareFacility { get; set; }

        [DataMember]
        public ISO8601DateTime DateTimeSubjectSeen { get; set; }

        [DataMember]
        public string PrescriptionRequestIdentifier { get; set; }

        [DataMember]
        public ISO8601DateTime DateTimePrescriptionRequestWritten { get; set; }

        /// <summary>
        /// Earliest Date for Filtering (DateTime Health Event Started)
        /// </summary>
        [DataMember]
        public ISO8601DateTime EarliestDateForFiltering { get; set; }

        /// <summary>
        /// Latest Date for Filtering (DateTime Health Event Ended)
        /// </summary>
        [DataMember]
        public ISO8601DateTime LatestDateForFiltering { get; set; }

        /// <summary>
        /// Related Document
        /// </summary>
        [DataMember]
        public RelatedDocumentV1 RelatedDocument { get; set; }

        #region 

        [DataMember]
        public IList<IParticipationPersonOrOrganisation> PatientNominatedContact { get; set; }

        [DataMember]
        public IParticipationPersonOrOrganisation PrimaryCareProvider { get; set; }

        [DataMember]
        public IList<IParticipationPersonOrOrganisation> InterestedParty { get; set; }

        /// <summary>
        /// The Referee for the CDA document
        /// </summary>
        public IParticipationPersonOrOrganisation Referee { get; set; }

        #endregion

        #region Diagnostic Imaging Report 

        /// <summary>
        /// Order Details
        /// </summary>
        [DataMember]
        public OrderDetails OrderDetails { get; set; }

        /// <summary>
        /// Pathology Test Result
        /// </summary>
        [DataMember]
        public IList<PathologyTestResult> PathologyTestResult { get; set; }

        /// <summary>
        /// Reporting Radiologist
        /// </summary>
        [DataMember]
        public IParticipationReportingRadiologist ReportingRadiologist { get; set; }

        #endregion

        #region Pathology Result Report

        /// <summary>
        /// The Reporting Pathologist
        /// </summary>
        [DataMember]
        public IParticipationReportingPathologist ReportingPathologist { get; set; }

        #endregion

        #endregion

        #region Constructors
        internal Context()
        {
        }
        #endregion

        #region Validation

        void IAdvanceCareInformationContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IAdvanceCareInformationContext)this).SubjectOfCare;
            var author = ((IAdvanceCareInformationContext)this).Author;


            if (vb.ArgumentRequiredCheck("Author", author))
            {
                var foundAuthor = false;

                if (author is AuthorAuthoringDevice)
                {
                    foundAuthor = true;
                    ((AuthorAuthoringDevice)author).Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages, false, true);

                    vb.AddValidationMessage(vb.PathName, null, "AuthorAuthoringDevice not permitted for AdvanceCareInformation");
                }

                // Both types are of type Participation so use the Participant to determine the type 
                if (author is Participation)
                {
                    var authorNonHealthcareProvider = author as IParticipationAuthorPerson;

                    if (authorNonHealthcareProvider.Participant != null)
                    {
                        foundAuthor = true;
                        authorNonHealthcareProvider.Validate(vb.Path + "IParticipationAuthorNonHealthcareProvider", vb.Messages);

                        if (authorNonHealthcareProvider.Participant != null && authorNonHealthcareProvider.Participant.Addresses != null)
                        {
                            foreach (var address in authorNonHealthcareProvider.Participant.Addresses.Where(address => address.AddressPurpose != AddressPurpose.Business))
                            {
                                vb.AddValidationMessage(path + "Participation Author Non Healthcare Provider", null, "Address - Must have an Address Use of 'Business'");
                            }
                        }


                    }

                    var authorHealthcareProvider = author as IParticipationAuthorHealthcareProvider;

                    if (authorHealthcareProvider.Participant != null)
                    {
                        foundAuthor = true;
                        authorHealthcareProvider.Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages);

                        if (authorHealthcareProvider.Participant != null && authorHealthcareProvider.Participant.Person != null)
                        {
                            vb.ArgumentRequiredCheck("authorHealthcareProvider.Participant.Person.Organisation", authorHealthcareProvider.Participant.Person.Organisation);
                        }
                    }
                }

                if (!foundAuthor)
                {
                    vb.AddValidationMessage(vb.PathName, null, "Please provide an Author");
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);

                if (subjectOfCare.Participant.Person.DateOfDeath != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "subjectOfCare.Participant.Person.DateOfDeath not permitted for AdvanceCareInformation");
                }

                if (subjectOfCare.Participant.Person.DateOfDeathAccuracyIndicator != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "subjectOfCare.Participant.Person.DateOfDeathAccuracyIndicator not permitted for AdvanceCareInformation");
                }

                if (subjectOfCare.Participant.Person.SourceOfDeathNotification != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "subjectOfCare.Participant.Person.SourceOfDeathNotification not permitted for AdvanceCareInformation");
                }
            }
        }


        void IAcdCustodianRecordContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedParticipation = ((IAcdCustodianRecordContext)this);

            if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
            {
                castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages);
            }

            var subjectOfCare = ((IAcdCustodianRecordContext)this).SubjectOfCare;

            if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
            {
                if (castedParticipation.Author is AuthorAuthoringDevice)
                {
                    ((AuthorAuthoringDevice)castedParticipation.Author).Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages, false, true);
                }

                // Both types are of type Participation so use the Participant to determine the type 
                if (castedParticipation.Author is Participation)
                {
                    var authorNonHealthcareProvider = castedParticipation.Author as IParticipationAuthorPerson;

                    if (authorNonHealthcareProvider.Participant != null)
                    {
                        authorNonHealthcareProvider.Validate(vb.Path + "IParticipationAuthorNonHealthcareProvider", vb.Messages);
                    }

                    var authorHealthcareProvider = castedParticipation.Author as IParticipationAuthorHealthcareProvider;

                    if (authorHealthcareProvider.Participant != null)
                    {
                        authorHealthcareProvider.Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages);
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                    subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);

                    // IndigenousStatus
                    if (subjectOfCare.Participant != null)
                        if (subjectOfCare.Participant.Person != null)
                            vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", subjectOfCare.Participant.Person.IndigenousStatus);
            }
        }

        void IPathologyResultReportContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IPathologyResultReportContext)this).SubjectOfCare;
            var author = ((IPathologyResultReportContext)this).Author;


            // Validate author at this level, and not at Participation / Participant level because it is different from the other documents.
            if (vb.ArgumentRequiredCheck("author", author))
            {
                author.Validate(vb.Path + "author", vb.Messages);

                if (author.Participant != null)
                {

                    if (author.Participant.Person != null)
                    {
                        vb.ArgumentRequiredCheck("author.Participant.Person.Organisation", author.Participant.Person.Organisation);
                        vb.ArgumentRequiredCheck("author.Participant.Person.PersonNames", author.Participant.Person.PersonNames);
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                    subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("ReportingPathologist", ReportingPathologist))
            {
                ReportingPathologist.Validate(vb.Path + "ReportingPathologist", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("OrderDetails", OrderDetails))
            {
                OrderDetails.Validate(vb.Path + "OrderDetails", vb.Messages);
            }
        }

        void IPathologyReportWithStructuredContentContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IPathologyReportWithStructuredContentContext)this).SubjectOfCare;
            var author = ((IPathologyReportWithStructuredContentContext)this).Author;


            // Validate author at this level, and not at Participation / Participant level because it is different from the other documents.
            if (vb.ArgumentRequiredCheck("author", author))
            {
                author.Validate(vb.Path + "author", vb.Messages);

                if (author.Participant != null)
                {

                    if (author.Participant.Person != null)
                    {
                        vb.ArgumentRequiredCheck("author.Participant.Person.Organisation", author.Participant.Person.Organisation);
                        vb.ArgumentRequiredCheck("author.Participant.Person.PersonNames", author.Participant.Person.PersonNames);
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                    subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);
                }
            }

            if (ReportingPathologist != null)
            {
                ReportingPathologist.Validate(vb.Path + "ReportingPathologist", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("OrderDetails", OrderDetails))
            {
                OrderDetails.Validate(vb.Path + "OrderDetails", vb.Messages);
            }
        }

        void IDiagnosticImagingReportContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var subjectOfCare = ((IDiagnosticImagingReportContext)this).SubjectOfCare;
          var author = ((IDiagnosticImagingReportContext)this).Author;

          // Validate author at this level, and not at Participation / Participant level because it is different from the other documents.
          if (vb.ArgumentRequiredCheck("author", author))
          {
              author.Validate(vb.Path + "author", vb.Messages);

              if (author.Participant != null)
              {
                  if (author.Participant.Person != null)
                  {
                      vb.ArgumentRequiredCheck("author.Participant.Person.Organisation", author.Participant.Person.Organisation);
                      vb.ArgumentRequiredCheck("author.Participant.Person.PersonNames", author.Participant.Person.PersonNames);
                  }
              }
          }

          // Subject Of Care
          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
              if (subjectOfCare != null)
              {
                  subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);
              }
          }

          // Radiologist
          if (vb.ArgumentRequiredCheck("ReportingRadiologist", ReportingRadiologist))
          {
              ReportingRadiologist.Validate(vb.Path + "ReportingRadiologist", vb.Messages);
          }

          // Order Details
          if (vb.ArgumentRequiredCheck("OrderDetails", OrderDetails))
          {
              OrderDetails.Validate(vb.Path + "OrderDetails", vb.Messages);

              vb.ArgumentRequiredCheck("AccessionNumber", OrderDetails.AccessionNumber);
          }
        }

        void IEventSummaryContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IEventSummaryContext)this).SubjectOfCare;

            if (vb.ArgumentRequiredCheck("EncounterPeriod", EncounterPeriod))
            {
                if (EncounterPeriod != null && (EncounterPeriod.Type != IntervalType.High && EncounterPeriod.Type != IntervalType.LowHigh))
                {
                    vb.AddValidationMessage(vb.Path + "EncounterPeriod", null, "Only a high or high/low value is permitted for EncounterPeriod representing (DateTime Health Event Started and/or DateTime Health Event Ended)");
                }
            }

            // Validate author at this level, and not at Participation / Participant level because it is different from the other
            // documents.
            if (vb.ArgumentRequiredCheck("Author", Author))
            {
                Author.ValidateV2(vb.Path + "Author", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                    subjectOfCare.ValidateV2(vb.Path + "SubjectOfCare", vb.Messages);
                }
            }
        }

        void ISharedHealthSummaryContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((ISharedHealthSummaryContext)this).SubjectOfCare;

            // Validate author at this level, and not at Participation / Participant level because it is different from the other
            // documents.
            if (vb.ArgumentRequiredCheck("Author", Author))
            {
                Author.ValidateV2(vb.Path + "Author", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                        subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);

                        if (subjectOfCare.Participant != null && subjectOfCare.Participant.Person != null)
                        {
                            vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", subjectOfCare.Participant.Person.IndigenousStatus);
                        }
                }
            }
        }

        void IEReferralContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IEReferralContext)this).SubjectOfCare;

            // Validate author at this level, and not at Participation / Participant level because it is different from the other
            // documents.
            if (vb.ArgumentRequiredCheck("Author", Author))
            {
                Author.Validate(vb.Path + "Author", vb.Messages);

                if (Author.Participant != null)
                {
                    vb.ArgumentRequiredCheck(vb.Path + "Author.Participant.ElectronicCommunicationDetails", Author.Participant.ElectronicCommunicationDetails);
                    vb.ArgumentRequiredCheck(vb.Path + "Author.Participant.Addresses", Author.Participant.Addresses);

                    if (Author.Participant.Person != null)
                        vb.ArgumentRequiredCheck("Author.Participant.Person.Organisation", Author.Participant.Person.Organisation);
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                    subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);

                    // Check electronic communication details here because it's only needed in eReferral
                    // Removed check as per Conformance Profile v1.6
                    //if (subjectOfCare.Participant != null)
                        //vb.ArgumentRequiredCheck("SubjectOfCare.Participant.ElectronicCommunicationDetails", subjectOfCare.Participant.ElectronicCommunicationDetails);

                    if (subjectOfCare.Participant != null && subjectOfCare.Participant.Person != null)
                    {
                        vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", subjectOfCare.Participant.Person.IndigenousStatus);
                    }
                }
            }

            // Patient nominated contacts
            if (((IEReferralContext) this).PatientNominatedContacts != null)
            {
                var participationPatientNominatedContacts = ((IEReferralContext)this).PatientNominatedContacts;
                if (participationPatientNominatedContacts != null)
                    foreach (IParticipationPatientNominatedContact contact in participationPatientNominatedContacts)
                    {
                        contact.Validate(vb.Path + "PatientNominatedContact", vb.Messages);
                    }
            }
        }

        void ISpecialistLetterContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((ISpecialistLetterContext)this).SubjectOfCare;

            vb.ArgumentRequiredCheck("DateTimeSubjectSeen", DateTimeSubjectSeen);

            // Validate author at this level, and not at Participation / Participant level because it is different from the other
            // documents.
            if (vb.ArgumentRequiredCheck("Author", Author))
            {
                Author.Validate(vb.Path + "Author", vb.Messages);

                if (Author.Participant != null)
                {
                    vb.ArgumentRequiredCheck(vb.Path + "Author.Participant.ElectronicCommunicationDetails", Author.Participant.ElectronicCommunicationDetails);
                    vb.ArgumentRequiredCheck(vb.Path + "Author.Participant.Addresses", Author.Participant.Addresses);

                    if (Author.Participant.Person != null)
                        vb.ArgumentRequiredCheck("Author.Participant.Person.Organisation", Author.Participant.Person.Organisation);
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                subjectOfCare.Validate(path, messages);
            }
            
            if (Referrer != null)
            {
                Referrer.Validate(vb.Path + "Referrer", vb.Messages);
            }

            if (UsualGP != null)
            {
                if (UsualGP != null) UsualGP.Validate(vb.Path + "UsualGP", vb.Messages);
            }

            if (Referrer != null)
            {
                if (Referrer != null) Referrer.Validate(vb.Path + "Referrer", vb.Messages);
            }
        }

        void IPCEHRDispenseRecordContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IPCEHRDispenseRecordContext)this).SubjectOfCare;

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null) subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);  
            }

            if (vb.ArgumentRequiredCheck("Dispenser", Dispenser))
            {
                if (Dispenser != null) Dispenser.Validate(vb.Path + "Dispenser", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("DispenserOrganisation", DispenserOrganisation))
            {
                if (DispenserOrganisation != null) DispenserOrganisation.Validate(vb.Path + "DispenserOrganisation", vb.Messages);
            }
        }

        void IPCEHRPrescriptionRecordContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var subjectOfCare = ((IPCEHRPrescriptionRecordContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("Prescriber", Prescriber))
          {
            if (Prescriber != null) Prescriber.Validate(vb.Path + "Prescriber", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("PrescriberOrganisation", PrescriberOrganisation))
          {
            if (PrescriberOrganisation != null) PrescriberOrganisation.Validate(vb.Path + "PrescriberOrganisation", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null) subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);
          }
        }

        void IConsumerEnteredNotesContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedParticipation = ((IConsumerEnteredNotesContext)this);

            if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
            {
              castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages);
            }

            var subjectOfCare = ((IConsumerEnteredNotesContext)this).SubjectOfCare;

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                  subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, true, true, vb.Messages);
                }
            }
        }

        void IConsumerEnteredHealthSummaryContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedParticipation = ((IConsumerEnteredHealthSummaryContext)this);

            if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
            {
               castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", castedParticipation.SubjectOfCare))
            {
              if (castedParticipation.SubjectOfCare != null)
                {
                  castedParticipation.SubjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);

                    // IndigenousStatus
                  if (castedParticipation.SubjectOfCare.Participant != null)
                    if (castedParticipation.SubjectOfCare.Participant.Person != null)
                      vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", castedParticipation.SubjectOfCare.Participant.Person.IndigenousStatus);
                }
            }
        }

        void IConsolidatedViewContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((IConsolidatedViewContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
              castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages, false, true);
          }

          if (vb.ArgumentRequiredCheck("SubjectOfCare", castedParticipation.SubjectOfCare))
          {
            if (castedParticipation.SubjectOfCare != null)
            {
              castedParticipation.SubjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Person != null)
              {
                 vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", castedParticipation.SubjectOfCare.Participant.Person.IndigenousStatus);
                 vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.Age", castedParticipation.SubjectOfCare.Participant.Person.Age);
              }
            }
          }
        }

        void IPrescriptionAndDispenseViewContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);
          var castedParticipation = ((IPrescriptionAndDispenseViewContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            vb.ArgumentRequiredCheck("Author.Identifiers", castedParticipation.Author.Identifiers);
            castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages, true, true);
          }

          if (vb.ArgumentRequiredCheck("SubjectOfCare", castedParticipation.SubjectOfCare))
          {
            if (castedParticipation.SubjectOfCare != null)
            {
              castedParticipation.SubjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Addresses != null)
              {
                var addresses = castedParticipation.SubjectOfCare.Participant.Addresses;

                for (var x = 0; x < addresses.Count; x++)
                {
                  if (addresses[x].InternationalAddress != null || addresses[x].AustralianAddress == null)
                    vb.AddValidationMessage(vb.Path + string.Format("Addresses[{0}]", x), null, "Australian address required.");
                }
              }

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Person != null)
              {
                vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.Age", castedParticipation.SubjectOfCare.Participant.Person.Age);
              }
            }
          }
        }

        void IPathologyResultViewContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);
          var castedParticipation = ((IPrescriptionAndDispenseViewContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            vb.ArgumentRequiredCheck("Author.Identifiers", castedParticipation.Author.Identifiers);
            castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages, true, true);
          }

          if (vb.ArgumentRequiredCheck("SubjectOfCare", castedParticipation.SubjectOfCare))
          {
            if (castedParticipation.SubjectOfCare != null)
            {
              castedParticipation.SubjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Addresses != null)
              {
                var addresses = castedParticipation.SubjectOfCare.Participant.Addresses;

                for (var x = 0; x < addresses.Count; x++)
                {
                  if (addresses[x].InternationalAddress != null || addresses[x].AustralianAddress == null)
                    vb.AddValidationMessage(vb.Path + string.Format("Addresses[{0}]", x), null, "Australian address required.");
                }
              }

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Person != null)
              {
                vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.Age", castedParticipation.SubjectOfCare.Participant.Person.Age);
              }
            }
          }
        }

        void IMedicareOverviewContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);
          var castedParticipation = ((IMedicareOverviewContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
              castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages, false, true);
          }

          if (vb.ArgumentRequiredCheck("SubjectOfCare", castedParticipation.SubjectOfCare))
          {
            if (castedParticipation.SubjectOfCare != null)
            {
              castedParticipation.SubjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Addresses != null)
              {
                var addresses = castedParticipation.SubjectOfCare.Participant.Addresses;

                for (var x = 0; x < addresses.Count; x++)
                {
                  if (addresses[x].InternationalAddress != null || addresses[x].AustralianAddress == null)
                    vb.AddValidationMessage(vb.Path + string.Format("Addresses[{0}]", x), null, "Australian address required.");
                }
              }

              if (castedParticipation.SubjectOfCare.Participant != null && castedParticipation.SubjectOfCare.Participant.Person != null)
              {
                vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", castedParticipation.SubjectOfCare.Participant.Person.IndigenousStatus);
                vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.Age", castedParticipation.SubjectOfCare.Participant.Person.Age);
              }
            }
          }
        }

        void IConsumerEnteredAchievementsContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((IConsumerEnteredAchievementsContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages);

            if (castedParticipation.Author.Participant != null && castedParticipation.Author.Participant.RelationshipToSubjectOfCare != null)
            {
              vb.AddValidationMessage(vb.Path + "SubjectOfCare.Participant.Person.RelationshipToSubjectOfCare", null, "RelationshipToSubjectOfCare is not valid for ConsumerEnteredAchievements");
            }
          }

          var subjectOfCare = castedParticipation.SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);
          }
        }


        void IEPrescriptionContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var subjectOfCare = ((IEPrescriptionContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("Prescriber", Prescriber))
          {
            if (Prescriber != null)
            {
              Prescriber.ValidateATS(vb.Path + "Prescriber", vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
                subjectOfCare.ValidateATS(vb.Path + "SubjectOfCare", vb.Messages);

              if (subjectOfCare.Participant != null && subjectOfCare.Participant.Addresses != null)
              {
                foreach (var address in subjectOfCare.Participant.Addresses.Where(address => address.AddressPurpose != AddressPurpose.TemporaryAccommodation && address.AddressPurpose != AddressPurpose.Residential))
                {
                  vb.AddValidationMessage(path + "SubjectOfCare", null, "Address can only have an Address Use of 'Temporary' or 'Residential'");
                }
              }
            }
          }

          if (vb.ArgumentRequiredCheck("PrescriberOrganisation", PrescriberOrganisation))
          {
            if (PrescriberOrganisation != null) PrescriberOrganisation.ValidateATS(vb.Path + "PrescriberOrganisation", vb.Messages);
          }
        }

        void IDispenseRecordContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var subjectOfCare = ((IDispenseRecordContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateATS(vb.Path + "SubjectOfCare", vb.Messages);

              if (subjectOfCare.Participant != null && subjectOfCare.Participant.Addresses != null)
              {
                foreach (var address in subjectOfCare.Participant.Addresses.Where(address => address.AddressPurpose != AddressPurpose.TemporaryAccommodation && address.AddressPurpose != AddressPurpose.Residential))
                {
                  vb.AddValidationMessage(path + "SubjectOfCare", null, "Address can only have an Address Use of 'Temporary' or 'Residential'");
                }
              }
            }
          }

          if (vb.ArgumentRequiredCheck("Dispenser", Dispenser))
          {
            if (Dispenser != null) Dispenser.ValidateATS(vb.Path + "Dispenser", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("DispenserOrganisation", DispenserOrganisation))
          {
            if (DispenserOrganisation != null) DispenserOrganisation.ValidateATS(vb.Path + "DispenserOrganisation", vb.Messages);
          }
        }

        void IPrescriptionRequestContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var subjectOfCare = ((IPrescriptionRequestContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateATS(vb.Path + "SubjectOfCare", vb.Messages);

              if (subjectOfCare.Participant != null && subjectOfCare.Participant.Addresses != null)
              {
                foreach (var address in subjectOfCare.Participant.Addresses.Where(address => address.AddressPurpose != AddressPurpose.TemporaryAccommodation && address.AddressPurpose != AddressPurpose.Residential))
                {
                  vb.AddValidationMessage(path + "SubjectOfCare", null, "Address can only have an Address Use of 'Temporary' or 'Residential'");
                }
              }
            }
          }

          if (vb.ArgumentRequiredCheck("Prescriber", Prescriber))
          {
            if (Prescriber != null) Prescriber.ValidateATS(vb.Path + "Prescriber", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("PrescriberOrganisation", PrescriberOrganisation))
          {
            if (PrescriberOrganisation != null) PrescriberOrganisation.ValidateATS(vb.Path + "PrescriberOrganisation", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("Dispenser", Dispenser))
          {
            if (Dispenser != null)
            {
              Dispenser.ValidateATS(vb.Path + "Dispenser", vb.Messages);

              if (subjectOfCare != null && subjectOfCare.Participant != null)
                  vb.RangeCheck("Addresses", subjectOfCare.Participant.Addresses, 0, 1);
            }
          }

          if (vb.ArgumentRequiredCheck("DispenserOrganisation", DispenserOrganisation))
          {
            if (DispenserOrganisation != null) DispenserOrganisation.ValidateATS(vb.Path + "DispenserOrganisation", vb.Messages);
          }

          vb.ArgumentRequiredCheck("DateTimePrescriptionRequestWritten", DateTimePrescriptionRequestWritten);
          vb.ArgumentRequiredCheck("PrescriptionRequestIdentifier", PrescriptionRequestIdentifier);
        }

        void IPhysicalMeasurementsContext.Validate(string path, PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedParticipation = ((IPhysicalMeasurementsContext)this);

            if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
            {
              if (castedParticipation.Author is AuthorAuthoringDevice)
              {
                  ((AuthorAuthoringDevice)castedParticipation.Author).Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages, false, true);

                  if (physicalMeasurementsDocumentType == PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements || physicalMeasurementsDocumentType == PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements)
                  {
                    vb.AddValidationMessage(path + "Author", null, "AuthorAuthoringDevice can only be used for the PhysicalMeasurementsView document type"); 
                  }
              }

              // Both types are of type Participation so use the Participant to determin the type 
              if (castedParticipation.Author is Participation)
              {
                var authorNonHealthcareProvider = castedParticipation.Author as IParticipationAuthorPerson;

                if (authorNonHealthcareProvider.Participant != null)
                {
                  authorNonHealthcareProvider.Validate(vb.Path + "IParticipationAuthorNonHealthcareProvider", vb.Messages);

                  // Mandatory Identifier
                  if (authorNonHealthcareProvider.Participant != null && authorNonHealthcareProvider.Participant.Person != null)
                  {
                      vb.ArgumentRequiredCheck("Person.Identifiers", authorNonHealthcareProvider.Participant.Person.Identifiers);
                  }

                  if (physicalMeasurementsDocumentType == PhysicalMeasurementsDocumentType.PhysicalMeasurementsView || physicalMeasurementsDocumentType == PhysicalMeasurementsDocumentType.HealthcareProviderEnteredMeasurements)
                  {
                    vb.AddValidationMessage(path + "Author", null, "IParticipationAuthorNonHealthcareProvider can only be used for the ConsumerEnteredMeasurements document type");
                  }
                }

                var authorHealthcareProvider = castedParticipation.Author as IParticipationAuthorHealthcareProvider;

                if (authorHealthcareProvider.Participant != null)
                {
                  authorHealthcareProvider.Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages);

                  if (physicalMeasurementsDocumentType == PhysicalMeasurementsDocumentType.ConsumerEnteredMeasurements || physicalMeasurementsDocumentType == PhysicalMeasurementsDocumentType.PhysicalMeasurementsView)
                  {
                    vb.AddValidationMessage(path + "Author", null, "IParticipationAuthorHealthcareProvider can only be used for the HealthcareProviderEnteredMeasurements document type");
                  }
                }
              }
            }

            var subjectOfCare = ((IPhysicalMeasurementsContext)this).SubjectOfCare;

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
              if (subjectOfCare != null)
              {
                subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, true, vb.Messages);
              }
            }

            if (HealthcareFacility != null)
            {
                HealthcareFacility.Validate(vb.Path + "HealthcareFacility", false, vb.Messages);    
            }
        }

        void IBirthDetailsRecordContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedParticipation = ((IBirthDetailsRecordContext)this);

            if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
            {
                if (castedParticipation.Author is AuthorAuthoringDevice)
                {
                    ((AuthorAuthoringDevice)castedParticipation.Author).Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages, false, true);
                }

                // Both types are of type Participation so use the Participant to determine the type 
                if (castedParticipation.Author is Participation)
                {
                    var authorNonHealthcareProvider = castedParticipation.Author as IParticipationAuthorPerson;

                    if (authorNonHealthcareProvider.Participant != null)
                    {
                        authorNonHealthcareProvider.Validate(vb.Path + "IParticipationAuthorNonHealthcareProvider", vb.Messages);
                    }

                    var authorHealthcareProvider = castedParticipation.Author as IParticipationAuthorHealthcareProvider;

                    if (authorHealthcareProvider.Participant != null)
                    {
                        authorHealthcareProvider.Validate(vb.Path + "IParticipationAuthorHealthcareProvider", vb.Messages);
                    }
                }
            }

            var subjectOfCare = castedParticipation.SubjectOfCare;

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                    subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, false, false, vb.Messages);
                }
            }

            var healthcareFacility = castedParticipation.HealthcareFacility;

            if (healthcareFacility != null)
            {
               healthcareFacility.Validate(vb.Path + "HealthcareFacility", true, vb.Messages);
            }
        }

      #region CeHR

        void INSWHealthCheckAssessmentContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((INSWHealthCheckAssessmentContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            castedParticipation.Author.ValidateOptional(vb.Path + "Author", true, vb.Messages);
          }

          var subjectOfCare = ((INSWHealthCheckAssessmentContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, true, true, vb.Messages);
            }
          }
        }

        void IChildHealthCheckScheduleViewContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((IChildHealthCheckScheduleViewContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages, true, true);
          }

          var subjectOfCare = ((IChildHealthCheckScheduleViewContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, true, true, vb.Messages);
            }
          }
        }

        void IObservationViewDocumentContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((IObservationViewDocumentContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages, true, true);
          }

          var subjectOfCare = ((IObservationViewDocumentContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, true, true, vb.Messages);
            }
          }
        }


        void IPersonalHealthObservationContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((IPersonalHealthObservationContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            castedParticipation.Author.Validate(vb.Path + "Author", vb.Messages);
          }

          var subjectOfCare = ((IPersonalHealthObservationContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, true, true, vb.Messages);
            }
          }
        }

        void IConsumerQuestionnaireContext.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedParticipation = ((IConsumerQuestionnaireContext)this);

          if (vb.ArgumentRequiredCheck("Author", castedParticipation.Author))
          {
            castedParticipation.Author.ValidateOptional(vb.Path + "Author", true, vb.Messages);
          }

          var subjectOfCare = ((IConsumerQuestionnaireContext)this).SubjectOfCare;

          if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
          {
            if (subjectOfCare != null)
            {
              subjectOfCare.ValidateOptional(vb.Path + "SubjectOfCare", true, true, true, vb.Messages);
            }
          }
        }

        #endregion

        #region Service Referral

        void IServiceReferralContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var serviceReferral = (IServiceReferralContext)this;

            // Validate author at this level, and not at Participation / Participant level because it is different from the other documents.
            if (vb.ArgumentRequiredCheck("Author", Author))
            {
                Author?.ValidateV3(vb.Path + "Author", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", serviceReferral.SubjectOfCare))
            {
                if (serviceReferral.SubjectOfCare != null)
                {
                    serviceReferral.SubjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);

                    if (serviceReferral.SubjectOfCare.Participant?.Person != null)
                    {
                        vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", serviceReferral.SubjectOfCare.Participant.Person.IndigenousStatus);
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("Author", serviceReferral.Author))
            {
                serviceReferral.Author?.ValidateV3(vb.Path + "Author", vb.Messages);
            }

            if (PatientNominatedContact != null && PatientNominatedContact.Any())
            {
                foreach (var patientNominatedContact in serviceReferral.PatientNominatedContact)
                {
                    patientNominatedContact.Validate(vb.Path + "PatientNominatedContact", vb.Messages);
                }
            }

            PrimaryCareProvider?.Validate(vb.Path + "PrimaryCareProvider", vb.Messages);

            if (InterestedParty != null && InterestedParty.Any())
            {
                foreach (var interestedParty in serviceReferral.InterestedParty)
                {
                    interestedParty.Validate(vb.Path + "InterestedParty", vb.Messages);
                }
            }

        }

        #endregion

        #endregion
    }
}