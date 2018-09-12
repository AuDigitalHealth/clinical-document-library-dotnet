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
using System.IO;
using System.Linq;
using System.Xml;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.PCML.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This CDAGenerator class is used to convert the various SCS and CDA model objects 
    /// into an actual CDA Document
    /// </summary>
    public class CDAGenerator
    {
        #region Properties

        private static INarrativeGenerator _narrativeGenerator;
        /// <summary>
        /// This property holds the narrative generator
        /// 
        /// The narrative generator is used to generate the narrative for each section within the 
        /// CDA document
        /// </summary>
        public static INarrativeGenerator NarrativeGenerator
        {
            get
            {
                if (_narrativeGenerator == null)
                {
                    _narrativeGenerator = new CDANarrativeGenerator(); 
                }

                return _narrativeGenerator;
            }
            set
            {
                _narrativeGenerator = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a CDAGenerator, this is the base from which all CDA document models are converted
        /// into CDA XML documents.
        /// </summary>
        public CDAGenerator() : this(new CDANarrativeGenerator())
        {
           
        }

        /// <summary>
        /// Instantiates a CDAGenerator, this is the base from which all CDA document models are converted
        /// into CDA XML documents.
        /// </summary>
        public CDAGenerator(INarrativeGenerator narrativeGenerator)
        {
            if(narrativeGenerator == null)
            {
                throw new ArgumentException("The narrative Generator must not be null");
            }

            NarrativeGenerator = narrativeGenerator;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Generates an event summary CDA (XML) document from the event summary model
        /// </summary>
        /// <param name="eventSummary">EventSummary</param>
        /// <returns>XmlDocument (CDA - EventSummary)</returns>
        public static XmlDocument GenerateEventSummary(EventSummary eventSummary)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("eventSummary", eventSummary))
            {
                eventSummary.Validate("eventSummary", vb.Messages);

                LogoSetupAndValidation(eventSummary.LogoPath, eventSummary.LogoByte, eventSummary.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var participants = new List<POCD_MT000040Participant1>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (  
                    eventSummary.DocumentCreationTime,
                    CDADocumentType.EventSummary,
                    eventSummary.CDAContext.DocumentId,
                    eventSummary.CDAContext.SetId,
                    eventSummary.CDAContext.Version,
                    DocumentStatus.Final,
                    eventSummary.Title
                );

            //SETUP the Legal Authenticator
            POCD_MT000040LegalAuthenticator legalAuthenticator = null;
            if (eventSummary.CDAContext.LegalAuthenticator != null)
            {
                legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(eventSummary.CDAContext.LegalAuthenticator);
            }

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(eventSummary.CDAContext.Custodian);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(eventSummary.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(eventSummary.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            //SETUP encompassing encounter
            clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(eventSummary.SCSContext.EncounterPeriod, null);

            //XML BODY FILE
            components.Add
                (
                    CDAGeneratorHelper.CreateStructuredBodyFileComponent(eventSummary.SCSContent.StructuredBodyFiles, NarrativeGenerator)
                );

            // Setup EventDetails
            components.Add(CDAGeneratorHelper.CreateComponent(eventSummary.SCSContent.EventDetails, NarrativeGenerator));

            // Setup Adverse Reactions
            components.Add(CDAGeneratorHelper.CreateComponent((SCSModel.Common.AdverseReactions)eventSummary.SCSContent.AdverseReactions, NarrativeGenerator, null));

            // Setup Medications
            components.Add(CDAGeneratorHelper.CreateComponent(eventSummary.SCSContent.Medications,eventSummary.SCSContent.CustomNarrativeMedications, NarrativeGenerator));

            // Setup Diagnoses Interventions
            components.Add(CDAGeneratorHelper.CreateComponent(eventSummary.SCSContent.DiagnosesIntervention, NarrativeGenerator));

            // Setup Immunisations
            components.Add(CDAGeneratorHelper.CreateComponent(eventSummary.SCSContent.Immunisations, eventSummary.SCSContent.CustomNarrativeImmunisations, NarrativeGenerator));

            // Setup Diagnostic Investigations
            components.Add(CDAGeneratorHelper.CreateComponent(eventSummary.SCSContent.DiagnosticInvestigations, CDADocumentType.EventSummary, NarrativeGenerator));

            //STRUCTURED BODY
            //SETUP administrative observations component
            if (!(eventSummary.ShowAdministrativeObservationsSection.HasValue && eventSummary.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (   eventSummary.SCSContext.Author,
                                eventSummary.SCSContext.SubjectOfCare,
                                eventSummary.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                (eventSummary.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && eventSummary.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                            )
                    );

            // Generate and return the Specialist SpecialistLetter
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, eventSummary.IncludeLogo, eventSummary.LogoByte, typeof(EventSummary));
        }

        /// <summary>
        /// Generates a BirthDetailsRecord document from the GenerateBirthDetailsRecord model
        /// </summary>
        /// <param name="birthDetailsRecord">The GenerateBirthDetailsRecord object</param>
        /// <returns>XmlDocument (CDA - GenerateBirthDetailsRecord)</returns>
        public static XmlDocument GenerateBirthDetailsRecord(BirthDetailsRecord birthDetailsRecord)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("BirthDetailsRecord", birthDetailsRecord))
          {
            birthDetailsRecord.Validate("BirthDetailsRecord", vb.Messages);

            LogoSetupAndValidation(birthDetailsRecord.LogoPath,birthDetailsRecord.LogoByte, birthDetailsRecord.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  birthDetailsRecord.DocumentCreationTime,
                  CDADocumentType.BirthDetails,
                  birthDetailsRecord.CDAContext.DocumentId,
                  birthDetailsRecord.CDAContext.SetId,
                  birthDetailsRecord.CDAContext.Version,
                  birthDetailsRecord.DocumentStatus,
                  birthDetailsRecord.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(birthDetailsRecord.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(birthDetailsRecord.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(birthDetailsRecord.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(birthDetailsRecord.CDAContext.Custodian);

          //SETUP the HealthcareFacility
          if (birthDetailsRecord.SCSContext.HealthcareFacility != null && birthDetailsRecord.SCSContext.HealthcareFacility.Participant != null)
            clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(birthDetailsRecord.SCSContext.HealthcareFacility);

          // Setup Measurement Information
          if (birthDetailsRecord.SCSContent.BirthDetails != null)
            components.Add(CDAGeneratorHelper.CreateComponent(birthDetailsRecord.SCSContent.BirthDetails, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(birthDetailsRecord.ShowAdministrativeObservationsSection.HasValue && birthDetailsRecord.ShowAdministrativeObservationsSection.Value == false))
          components.Add
              (
                  CDAGeneratorHelper.CreateAdministrativeObservations
                      (
                          birthDetailsRecord.SCSContext.SubjectOfCare,
                          birthDetailsRecord.SCSContext.Author as IParticipationAuthorHealthcareProvider,
                          birthDetailsRecord.SCSContent.CustomNarrativeAdministrativeObservations,
                          clinicalDocument.recordTarget[0].patientRole.id[0].root,
                          "1.2.36.1.2001.1001.101.102.16080",
                          (birthDetailsRecord.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && birthDetailsRecord.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                      )
              );

          //Generate and return the GeneratePersonalHealthObservation
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, birthDetailsRecord.IncludeLogo, birthDetailsRecord.LogoByte, typeof(BirthDetailsRecord));
        }

        /// <summary>
        /// Generates PCML
        /// </summary>
        /// <param name="pcml">The GenerateBirthDetailsRecord object</param>
        /// <returns>XmlDocument (CDA - GenerateBirthDetailsRecord)</returns>
        public static XmlDocument GeneratePCML(PCML pcml)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("pcml", pcml))
            {
                pcml.Validate("pcml", vb.Messages);

                LogoSetupAndValidation(pcml.LogoPath, pcml.LogoByte, pcml.IncludeLogo, vb);
            }

            //if (vb.Messages.Any())
            //    throw new ValidationException(vb.Messages);


            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var participants = new List<POCD_MT000040Participant1>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    pcml.DocumentCreationTime,
                    CDADocumentType.PharmacistCuratedMedicinesList,
                    pcml.CDAContext.DocumentId,
                    pcml.CDAContext.SetId,
                    pcml.CDAContext.Version,
                    pcml.DocumentStatus,
                    pcml.Title
                ); 
            

            if (pcml.SCSContext.Participant != null)
            { 
                foreach (var patientNominatedContact in pcml.SCSContext.Participant)
                {
                    participants.Add(CDAGeneratorHelper.CreateParticipant(patientNominatedContact,
                        ParticipationType.PART, RoleClassAssociative.PROV, new CE { code = "PCP" }, 
                        pcml.SCSContext?.SubjectOfCare?.Participant?.UniqueIdentifier.ToString()));
                }
            }

            if (pcml.CDAContext.InformationRecipients != null)
            {
                recipients.AddRange(pcml.CDAContext.InformationRecipients.Select(interestedParty =>
                    CDAGeneratorHelper.CreateInformationRecipient(interestedParty)));
            }

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(pcml.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(pcml.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(pcml.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();
            
            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(pcml.CDAContext.Custodian);

            //SETUP the Information Recipients
            clinicalDocument.informationRecipient = recipients.ToArray();

            //SETUP the HealthcareFacility
            if (pcml.SCSContext.Encounter != null &&  pcml.SCSContext.Encounter.HealthcareFacility != null && pcml.SCSContext.Encounter.HealthcareFacility.Participant != null)
                clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(pcml.SCSContext.Encounter.HealthcareFacility);

            //SETUP administrative observations component
            //if (!(pcml.ShowAdministrativeObservationsSection.HasValue && pcml.ShowAdministrativeObservationsSection.Value == false))
            //    components.Add
            //    (
            //        CDAGeneratorHelper.CreateAdministrativeObservations
            //        (
            //            pcml.SCSContext.SubjectOfCare,
            //            pcml.SCSContext.Author as IParticipationAuthorHealthcareProvider,
            //            pcml.SCSContent.CustomNarrativeAdministrativeObservations,
            //            clinicalDocument.recordTarget[0].patientRole.id[0].root,
            //            "1.2.36.1.2001.1001.101.102.16080",
            //            (pcml.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && pcml.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator
            //        )
            //    );

            if (pcml.SCSContent.EncapsulatedData != null)
            { 
                components.Add(CDAGeneratorHelper.CreateComponent(pcml.SCSContent.EncapsulatedData, NarrativeGenerator));
            }


            //Generate and return the GeneratePersonalHealthObservation
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, pcml.IncludeLogo, pcml.LogoByte, typeof(BirthDetailsRecord));
        }

        /// <summary>
        /// Generates a Consumer Entered Health Summary CDA (XML) document
        /// </summary>
        /// <param name="consumerEnteredHealthSummary">The ConsumerEnteredHealthSummary object</param>
        /// <returns>XmlDocument (CDA - ConsumerEnteredHealthSummary)</returns>
        public static XmlDocument GenerateConsumerEnteredHealthSummary(ConsumerEnteredHealthSummary consumerEnteredHealthSummary)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("consumerEnteredHealthSummary", consumerEnteredHealthSummary))
            {
                consumerEnteredHealthSummary.Validate("consumerEnteredHealthSummary", vb.Messages);

                LogoSetupAndValidation(consumerEnteredHealthSummary.LogoPath, consumerEnteredHealthSummary.LogoByte, consumerEnteredHealthSummary.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var participants = new List<POCD_MT000040Participant1>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    consumerEnteredHealthSummary.DocumentCreationTime,
                    CDADocumentType.ConsumerEnteredHealthSummary,
                    consumerEnteredHealthSummary.CDAContext.DocumentId,
                    consumerEnteredHealthSummary.CDAContext.SetId,
                    consumerEnteredHealthSummary.CDAContext.Version,
                    consumerEnteredHealthSummary.DocumentStatus,
                    consumerEnteredHealthSummary.Title
                );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(consumerEnteredHealthSummary.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(consumerEnteredHealthSummary.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(consumerEnteredHealthSummary.SCSContext.Author, consumerEnteredHealthSummary.SCSContext.SubjectOfCare.Participant.UniqueIdentifier));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(consumerEnteredHealthSummary.CDAContext.Custodian);

            //XML BODY FILE
            components.Add
                (
                    CDAGeneratorHelper.CreateStructuredBodyFileComponent(consumerEnteredHealthSummary.SCSContent.StructuredBodyFiles, NarrativeGenerator)
                );

            components.Add(CDAGeneratorHelper.CreateComponent(consumerEnteredHealthSummary.SCSContent.AllergiesAndAdverseReactions, consumerEnteredHealthSummary.SCSContent.CustomNarrativeAllergiesAndAdverseReactions ,NarrativeGenerator));

            components.Add(CDAGeneratorHelper.CreateComponent(consumerEnteredHealthSummary.SCSContent.Medications, consumerEnteredHealthSummary.SCSContent.CustomNarrativeMedications, NarrativeGenerator));

            //SETUP administrative observations component
            if (!(consumerEnteredHealthSummary.ShowAdministrativeObservationsSection.HasValue && consumerEnteredHealthSummary.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (
                                null,
                                consumerEnteredHealthSummary.SCSContext.SubjectOfCare,
                                consumerEnteredHealthSummary.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                (consumerEnteredHealthSummary.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && consumerEnteredHealthSummary.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator  
                            )
                    );

            //Generate and return the E-Referral.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, consumerEnteredHealthSummary.IncludeLogo, consumerEnteredHealthSummary.LogoByte, typeof(ConsumerEnteredHealthSummary));
        }

        /// <summary>
        /// Generates a Consumer Entered Notes CDA (XML) document from the ConsumerEnteredNotes model
        /// </summary>
        /// <param name="consumerEnteredNotes">The ConsumerEnteredNotes object</param>
        /// <returns>XmlDocument (CDA - ConsumerEnteredNotes)</returns>
        public static XmlDocument GenerateConsumerEnteredNotes(ConsumerEnteredNotes consumerEnteredNotes)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("consumerEnteredNotes", consumerEnteredNotes))
            {
                consumerEnteredNotes.Validate("consumerEnteredNotes", vb.Messages);

                LogoSetupAndValidation(consumerEnteredNotes.LogoPath, consumerEnteredNotes.LogoByte, consumerEnteredNotes.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var participants = new List<POCD_MT000040Participant1>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    consumerEnteredNotes.DocumentCreationTime,
                    CDADocumentType.ConsumerEnteredNotes,
                    consumerEnteredNotes.CDAContext.DocumentId,
                    consumerEnteredNotes.CDAContext.SetId,
                    consumerEnteredNotes.CDAContext.Version,
                    consumerEnteredNotes.DocumentStatus,
                    consumerEnteredNotes.Title
                );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(consumerEnteredNotes.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(consumerEnteredNotes.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(consumerEnteredNotes.SCSContext.Author, consumerEnteredNotes.SCSContext.SubjectOfCare.Participant.UniqueIdentifier));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(consumerEnteredNotes.CDAContext.Custodian);

            //XML BODY FILE
            components.Add
                (
                    CDAGeneratorHelper.CreateStructuredBodyFileComponent(consumerEnteredNotes.SCSContent.StructuredBodyFiles, NarrativeGenerator)
                );

            // Build content
            if (consumerEnteredNotes.SCSContent != null && (!consumerEnteredNotes.SCSContent.Title.IsNullOrEmptyWhitespace() || !consumerEnteredNotes.SCSContent.Description.IsNullOrEmptyWhitespace()))
            components.Add(new POCD_MT000040Component3
            {
                section = new POCD_MT000040Section
                {
                  code = CDAGeneratorHelper.CreateCodedWithExtensionElement("102.15513", CodingSystem.NCTIS, "Consumer Entered Note", null, null, null),
                    title = new ST { Text = new [] { consumerEnteredNotes.SCSContent.Title } },
                    text = consumerEnteredNotes.SCSContent.CustomNarrativeConsumerEnteredNote ?? new StrucDocText { Text = new [] { consumerEnteredNotes.SCSContent.Description }}
                }
            });

            //SETUP administrative observations component
            if (!(consumerEnteredNotes.ShowAdministrativeObservationsSection.HasValue && consumerEnteredNotes.ShowAdministrativeObservationsSection.Value == false))
           components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (
                                null,
                                consumerEnteredNotes.SCSContext.SubjectOfCare,
                                consumerEnteredNotes.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                (consumerEnteredNotes.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && consumerEnteredNotes.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                            )
                    );

            //Generate and return the E-Referral.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, consumerEnteredNotes.IncludeLogo, consumerEnteredNotes.LogoByte, typeof(ConsumerEnteredNotes));
        }

        /// <summary>
        /// Generates an Advance Care Directive Custodian Record CDA (XML) document from the ACD Custodian Record model
        /// </summary>
        /// <param name="acdCustodianRecord">The Advance Care Directive Custodian Record object</param>
        /// <returns>XmlDocument (CDA - AcdCustodianRecord)</returns>
        public static XmlDocument GenerateAcdCustodianRecord(AcdCustodianRecord acdCustodianRecord)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("acdCustodianRecord", acdCustodianRecord))
            {
                acdCustodianRecord.Validate("acdCustodianRecord", vb.Messages);

                LogoSetupAndValidation(acdCustodianRecord.LogoPath, acdCustodianRecord.LogoByte, acdCustodianRecord.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var participants = new List<POCD_MT000040Participant1>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    acdCustodianRecord.DocumentCreationTime,
                    CDADocumentType.AdvanceCareDirectiveCustodianRecord,
                    acdCustodianRecord.CDAContext.DocumentId,
                    acdCustodianRecord.CDAContext.SetId,
                    acdCustodianRecord.CDAContext.Version,
                    acdCustodianRecord.DocumentStatus,
                    acdCustodianRecord.Title
                );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(acdCustodianRecord.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(acdCustodianRecord.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(acdCustodianRecord.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(acdCustodianRecord.CDAContext.Custodian);

            //XML BODY FILE
            components.Add
                (
                    CDAGeneratorHelper.CreateStructuredBodyFileComponent(acdCustodianRecord.SCSContent.StructuredBodyFiles, NarrativeGenerator)
                );
  
            // Set up ACD custodian records
            components.Add(CDAGeneratorHelper.CreateComponent(acdCustodianRecord.SCSContent.AcdCustodians, acdCustodianRecord.SCSContent.CustomNarrativeACDCustodianEntries ,NarrativeGenerator));

            //SETUP administrative observations component
            if (!(acdCustodianRecord.ShowAdministrativeObservationsSection.HasValue && acdCustodianRecord.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (
                                acdCustodianRecord.SCSContext.Author,
                                acdCustodianRecord.SCSContext.SubjectOfCare,
                                acdCustodianRecord.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                (acdCustodianRecord.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && acdCustodianRecord.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator    
                            )
                    );

            //Generate and return the E-Referral.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, acdCustodianRecord.IncludeLogo, acdCustodianRecord.LogoByte, typeof(AcdCustodianRecord));
        }

        /// <summary>
        /// Generates an E-Referral CDA (XML) document from the e-referral model
        /// </summary>
        /// <param name="eReferral">EReferral</param>
        /// <returns>XmlDocument (CDA - EReferral)</returns>
        public static XmlDocument GenerateEReferral(EReferral eReferral)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("eReferral", eReferral))
            {
                eReferral.Validate("eReferral", vb.Messages);

                LogoSetupAndValidation(eReferral.LogoPath, eReferral.LogoByte, eReferral.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var participants = new List<POCD_MT000040Participant1>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    eReferral.DocumentCreationTime,
                    CDADocumentType.EReferral,
                    eReferral.CDAContext.DocumentId,
                    eReferral.CDAContext.SetId,
                    eReferral.CDAContext.Version, 
                    eReferral.DocumentStatus,
                    eReferral.Title
                );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(eReferral.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(eReferral.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();
            
            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(eReferral.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            // SETUP the patient nominated contacts
            if (eReferral.SCSContext.PatientNominatedContacts != null && eReferral.SCSContext.PatientNominatedContacts.Any())
            {
              participants.AddRange(eReferral.SCSContext.PatientNominatedContacts.Select(CDAGeneratorHelper.CreateParticipant));
            }

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(eReferral.CDAContext.Custodian);

            //SETUP the recipients   
            if (eReferral.CDAContext.InformationRecipients != null && eReferral.CDAContext.InformationRecipients.Any())
            {
              recipients.AddRange(eReferral.CDAContext.InformationRecipients.Select(CDAGeneratorHelper.CreateInformationRecipient));
            }

            //SETUP the participants    
            participants.Add(CDAGeneratorHelper.CreateParticipant(eReferral.SCSContent.Referee));

            // SETUP UsualGP's
            participants.Add(CDAGeneratorHelper.CreateParticipant(eReferral.SCSContent.UsualGP));

            //XML BODY FILE
            components.Add
            (
                CDAGeneratorHelper.CreateStructuredBodyFileComponent(eReferral.SCSContent.StructuredBodyFiles, NarrativeGenerator)
            );

            //Add Narrative Only Document 
            if (eReferral.SCSContent.NarrativeOnlyDocument != null && eReferral.SCSContent.NarrativeOnlyDocument.Any())
            components.AddRange
            (
               CDAGeneratorHelper.CreateNarrativeOnlyDocument(eReferral.SCSContent.NarrativeOnlyDocument)
            );

            //SETUP referral reason
            components.Add
                (
                    CDAGeneratorHelper.CreateComponentForReferralReason
                        (
                            eReferral.SCSContent.ReferralDateTime,
                            eReferral.SCSContent.ValidityDuration,
                            eReferral.SCSContent.ReferralReason,
                            eReferral.SCSContent.CustomNarrativeReferralDetail,
                            NarrativeGenerator
                        )
                );

            //setup adverse substance reactions
            components.Add(CDAGeneratorHelper.CreateComponentLegacy((SCSModel.Common.AdverseReactions)eReferral.SCSContent.AdverseReactions, NarrativeGenerator, "103.16302.2.2.2"));

            //SETUP the components (e.g. the body)  
            //setup the medications
            components.Add(CDAGeneratorHelper.CreateComponent(eReferral.SCSContent.Medications, NarrativeGenerator));

            //setup the medical history
            components.Add(CDAGeneratorHelper.CreateComponentLegacy(eReferral.SCSContent.MedicalHistory as MedicalHistory, true, NarrativeGenerator));

            //setup diagnostic investigations
            components.Add(CDAGeneratorHelper.CreateComponentLegacy(eReferral.SCSContent.DiagnosticInvestigations, CDADocumentType.EReferral, NarrativeGenerator));

            //SETUP administrative observations component
            if (!(eReferral.ShowAdministrativeObservationsSection.HasValue && eReferral.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (
                                eReferral.SCSContext.Author,
                                eReferral.SCSContext.SubjectOfCare,
                                eReferral.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                eReferral.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && eReferral.ShowAdministrativeObservationsNarrativeAndTitle.Value == false ? null : NarrativeGenerator  
                            )
                    );

            //Generate and return the E-Referral.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, eReferral.IncludeLogo, eReferral.LogoByte, typeof(EReferral));
        }

        /// <summary>
        /// Generates a Shared Health Summary CDA (XML) document from the shared health summary model
        /// </summary>
        /// <param name="sharedHealthSummary">SharedHealthSummary</param>
        /// <returns>XmlDocument (CDA - Shared Health Summary)</returns>
        public static XmlDocument GenerateSharedHealthSummary(SharedHealthSummary sharedHealthSummary)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("sharedHealthSummary", sharedHealthSummary))
            {
                sharedHealthSummary.Validate("sharedHealthSummary", vb.Messages);

                LogoSetupAndValidation(sharedHealthSummary.LogoPath, sharedHealthSummary.LogoByte, sharedHealthSummary.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    sharedHealthSummary.DocumentCreationTime,
                    CDADocumentType.SharedHeathSummary,
                    sharedHealthSummary.CDAContext.DocumentId,
                    sharedHealthSummary.CDAContext.SetId,
                    sharedHealthSummary.CDAContext.Version,
                    sharedHealthSummary.DocumentStatus,
                    sharedHealthSummary.Title
                );
            
            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(sharedHealthSummary.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(sharedHealthSummary.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(sharedHealthSummary.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(sharedHealthSummary.CDAContext.Custodian);

            //XML body file
            components.Add
            (
                CDAGeneratorHelper.CreateStructuredBodyFileComponent(sharedHealthSummary.SCSContent.StructuredBodyFiles, NarrativeGenerator)
            );

            components.Add(CDAGeneratorHelper.CreateComponent((SCSModel.Common.AdverseReactions)sharedHealthSummary.SCSContent.AdverseReactions, NarrativeGenerator, "103.16302.120.1.1"));
            components.Add(CDAGeneratorHelper.CreateComponent(sharedHealthSummary.SCSContent.Medications, NarrativeGenerator, CDADocumentType.SharedHeathSummary));
            components.Add(CDAGeneratorHelper.CreateComponent(sharedHealthSummary.SCSContent.MedicalHistory as MedicalHistory, true, NarrativeGenerator));
            components.Add(CDAGeneratorHelper.CreateComponent(sharedHealthSummary.SCSContent.Immunisations, NarrativeGenerator));

            //SETUP administrative observations component
            if (!(sharedHealthSummary.ShowAdministrativeObservationsSection.HasValue && sharedHealthSummary.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            sharedHealthSummary.SCSContext.Author,
                            sharedHealthSummary.SCSContext.SubjectOfCare,
                            sharedHealthSummary.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (sharedHealthSummary.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && sharedHealthSummary.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                        )
                );

            //Generate and return the SHS.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, null, components, nonXmlBody, sharedHealthSummary.IncludeLogo, sharedHealthSummary.LogoByte, typeof(SharedHealthSummary));
        }

        /// <summary>
        /// Generates a Specialist SpecialistLetter CDA (XML) document from the specialistLetter model
        /// </summary>
        /// <param name="specialistLetter">SpecialistLetter</param>
        /// <returns>XmlDocument (CDA - Specialist SpecialistLetter)</returns>
        public static XmlDocument GenerateSpecialistLetter(SpecialistLetter specialistLetter)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("specialistLetter", specialistLetter))
            {
                specialistLetter.Validate("specialistLetter", vb.Messages);

                LogoSetupAndValidation(specialistLetter.LogoPath, specialistLetter.LogoByte, specialistLetter.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var participants = new List<POCD_MT000040Participant1>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    specialistLetter.DocumentCreationTime ,
                    CDADocumentType.SpecialistLetter,
                    specialistLetter.CDAContext.DocumentId,
                    specialistLetter.CDAContext.SetId,
                    specialistLetter.CDAContext.Version,
                    specialistLetter.DocumentStatus,
                    specialistLetter.Title
                );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(specialistLetter.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(specialistLetter.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(specialistLetter.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(specialistLetter.CDAContext.Custodian);

            //SETUP the participants    
            participants.Add(CDAGeneratorHelper.CreateParticipant(specialistLetter.SCSContext.Referrer));

            // SETUP the UsualGP
            participants.Add(CDAGeneratorHelper.CreateParticipant(specialistLetter.SCSContext.UsualGP));

            //SETUP encompassing encounter
            clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(specialistLetter.SCSContext.DateTimeSubjectSeen);

            //SETUP the recipients   
            if (specialistLetter.CDAContext.InformationRecipients != null)
            {
                foreach (var recipient in specialistLetter.CDAContext.InformationRecipients)
                {
                    recipients.Add(CDAGeneratorHelper.CreateInformationRecipient(recipient));
                }
            }

            //XML BODY FILE
            components.Add
                (
                    CDAGeneratorHelper.CreateStructuredBodyFileComponent(specialistLetter.SCSContent.StructuredBodyFiles, NarrativeGenerator)
                );

            //Add Narrative Only Document 
            if (specialistLetter.SCSContent.NarrativeOnlyDocument != null && specialistLetter.SCSContent.NarrativeOnlyDocument.Any())
                components.AddRange
                (
                   CDAGeneratorHelper.CreateNarrativeOnlyDocument(specialistLetter.SCSContent.NarrativeOnlyDocument)
                 );

            //SETUP the components (e.g. the body)  
            //setup the response details
            components.Add(CDAGeneratorHelper.CreateComponent(specialistLetter.SCSContent.ResponseDetails, NarrativeGenerator));

            //setup the recommendations
            components.Add(CDAGeneratorHelper.CreateComponent(specialistLetter.SCSContent.Recommendations, NarrativeGenerator));

            //setup adverse reactions
            components.Add(CDAGeneratorHelper.CreateComponentLegacy((SCSModel.Common.AdverseReactions)specialistLetter.SCSContent.AdverseReactions, NarrativeGenerator, null));

            //setup the medications
            components.Add(CDAGeneratorHelper.CreateComponentLegacy(specialistLetter.SCSContent.Medications, NarrativeGenerator));

            //setup diagnostic investigations
            components.Add(CDAGeneratorHelper.CreateComponentLegacy(specialistLetter.SCSContent.DiagnosticInvestigations, CDADocumentType.SpecialistLetter, NarrativeGenerator));

            //SETUP administrative observations component
            if (!(specialistLetter.ShowAdministrativeObservationsSection.HasValue && specialistLetter.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (
                                specialistLetter.SCSContext.Author,
                                specialistLetter.SCSContext.SubjectOfCare,
                                specialistLetter.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                (specialistLetter.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && specialistLetter.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                            )
                    );

            //Generate and return the Specialist SpecialistLetter
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, specialistLetter.IncludeLogo, specialistLetter.LogoByte, typeof(SpecialistLetter));
        }

        /// <summary>
        /// Generates a Specialist SpecialistLetter CDA (XML) document from the eDischargeSummary model
        /// </summary>
        /// <param name="eDischargeSummary">EDischargeSummary</param>
        /// <returns>XmlDocument (CDA - Specialist SpecialistLetter)</returns>
        public static XmlDocument GenerateEDischargeSummary(EDischargeSummary eDischargeSummary)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("eDischargeSummary", eDischargeSummary))
            {
                eDischargeSummary.Validate("eDischargeSummary", vb.Messages);

                LogoSetupAndValidation(eDischargeSummary.LogoPath, eDischargeSummary.LogoByte, eDischargeSummary.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            var encounterParticipant = new List<POCD_MT000040EncounterParticipant>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    eDischargeSummary.DocumentCreationTime,
                    CDADocumentType.DischargeSummary,
                    eDischargeSummary.CDAContext.DocumentId,
                    eDischargeSummary.CDAContext.SetId,
                    eDischargeSummary.CDAContext.Version,
                    eDischargeSummary.DocumentStatus,
                    eDischargeSummary.Title
                );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(eDischargeSummary.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(eDischargeSummary.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(eDischargeSummary.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(eDischargeSummary.CDAContext.Custodian);

            //SETUP encompassingEncounter for an Encounter
            if (eDischargeSummary.SCSContent.Event != null)
                clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(eDischargeSummary.SCSContent.Event.Encounter);
            else
            {
                if (eDischargeSummary.SCSContext.Facility != null)
                {
                    clinicalDocument.componentOf = new POCD_MT000040Component1
                    {
                        encompassingEncounter = new POCD_MT000040EncompassingEncounter()
                    };
                }
            }

            clinicalDocument.componentOf.encompassingEncounter.location = CDAGeneratorHelper.CreateLocation(eDischargeSummary.SCSContext.Facility);
            
            //SETUP the Responsible Health Professional
            if (eDischargeSummary.SCSContent.Event != null)
                if (eDischargeSummary.SCSContent.Event.Encounter != null)
                    encounterParticipant.Add(CDAGeneratorHelper.CreateEncounterParticipant(eDischargeSummary.SCSContent.Event.Encounter.ResponsibleHealthProfessional));
            clinicalDocument.componentOf.encompassingEncounter.encounterParticipant = encounterParticipant.ToArray();

            var participants = CDAGeneratorHelper.CreateParticipantsDischargeSummary
                (
                    eDischargeSummary.SCSContent.Event != null
                        ? eDischargeSummary.SCSContent.Event.Encounter.LocationOfDischarge
                        : null,
                        eDischargeSummary.SCSContent.HealthProfile != null ?  eDischargeSummary.SCSContent.HealthProfile.NominatedPrimaryHealthCareProviders : null,
                        eDischargeSummary.SCSContent.Event != null ? eDischargeSummary.SCSContent.Event.Encounter.OtherParticipants : null
                );

            //SETUP the recipients   
            if (eDischargeSummary.CDAContext.InformationRecipients != null)
            {
                recipients.AddRange(eDischargeSummary.CDAContext.InformationRecipients.Select(CDAGeneratorHelper.CreateInformationRecipient));
            }

            //XML BODY FILE
            components.Add
                (
                    CDAGeneratorHelper.CreateStructuredBodyFileComponent(eDischargeSummary.SCSContent.StructuredBodyFiles, NarrativeGenerator)
                );

            //Add Narrative Only Document 
            if (eDischargeSummary.SCSContent.NarrativeOnlyDocument != null && eDischargeSummary.SCSContent.NarrativeOnlyDocument.Any())
                components.AddRange
                (
                   CDAGeneratorHelper.CreateNarrativeOnlyDocument(eDischargeSummary.SCSContent.NarrativeOnlyDocument)
                 );

            //Setup the Health Profile 
            components.Add(CDAGeneratorHelper.CreateComponent(eDischargeSummary.SCSContent.HealthProfile, NarrativeGenerator));

            //Setup the Event
            if ((eDischargeSummary.SCSContent.StructuredBodyFiles == null && eDischargeSummary.SCSContent.NarrativeOnlyDocument == null))
            components.Add(CDAGeneratorHelper.CreateComponentLegacy(eDischargeSummary.SCSContent.Event, NarrativeGenerator));

            //Setup the Medications 
            components.Add(CDAGeneratorHelper.CreateComponent(eDischargeSummary.SCSContent.Medications, NarrativeGenerator));

            //Setup the Plan
            components.Add(CDAGeneratorHelper.CreateComponent(eDischargeSummary.SCSContent.Plan, NarrativeGenerator));

            //SETUP administrative observations component
            if (!(eDischargeSummary.ShowAdministrativeObservationsSection.HasValue && eDischargeSummary.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservationsForDischargeSummary
                            (
                                eDischargeSummary.SCSContext.SubjectOfCare,
                                eDischargeSummary.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                eDischargeSummary.SCSContent.Event != null && eDischargeSummary.SCSContent.Event.Encounter != null ? eDischargeSummary.SCSContent.Event.Encounter.Specialty : null,
                                (eDischargeSummary.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && eDischargeSummary.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                            )
                    );

            //Generate and return the Specialist SpecialistLetter
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, eDischargeSummary.IncludeLogo, eDischargeSummary.LogoByte, typeof(EDischargeSummary));
        }

        /// <summary>
        /// Generates a ConsolidatedView CDA (XML) document from the Consolidated View model
        /// </summary>
        /// <param name="consolidatedView">SharedHealthSummary</param>
        /// <returns>XmlDocument (CDA - Shared Health Summary)</returns>
        public static XmlDocument GenerateConsolidatedView(ConsolidatedView consolidatedView)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("consolidatedView", consolidatedView))
            {
                consolidatedView.Validate("consolidatedView", vb.Messages);

                LogoSetupAndValidation(consolidatedView.LogoPath, consolidatedView.LogoByte, consolidatedView.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    consolidatedView.DocumentCreationTime,
                    CDADocumentType.ConsolidatedView,
                    consolidatedView.CDAContext.DocumentId,
                    consolidatedView.CDAContext.SetId,
                    consolidatedView.CDAContext.Version,
                    consolidatedView.DocumentStatus,
                    consolidatedView.Title
                );

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(consolidatedView.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(consolidatedView.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(consolidatedView.CDAContext.Custodian);

            components.Add(CDAGeneratorHelper.CreateComponent(new List<IDocument> { consolidatedView.SCSContent.SharedHealthSummaryDocumentProvenance }, consolidatedView.SCSContent.CustomNarrativeSharedHealthSummaryDocumentProvenance, DocumentSections.SharedHealthSummary, DocumentProvenanceEnum.SharedHealthSummaryDocumentProvenance, NarrativeGenerator));
            components.Add(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.NewDocumentProvenance, consolidatedView.SCSContent.CustomNarrativeNewDocumentProvenance, DocumentSections.NewDocument, DocumentProvenanceEnum.NewDocumentProvenance, NarrativeGenerator));

            // The following three items are the same as SHS only they have a different component - section - title
            components.Add(CDAGeneratorHelper.UpdateComponentTitle(CDAGeneratorHelper.CreateComponent((SCSModel.Common.AdverseReactions)consolidatedView.SCSContent.SharedHealthSummaryAdverseReactions, NarrativeGenerator, "103.16302.120.1.1"), "Allergies and Adverse Reactions"));
            components.Add(CDAGeneratorHelper.UpdateComponentTitle(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.SharedHealthSummaryMedicationInstructions, NarrativeGenerator, CDADocumentType.ConsolidatedView), "Medicines"));
            components.Add(CDAGeneratorHelper.UpdateComponentTitle(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.SharedHealthSummaryMedicalHistory as MedicalHistory, true, NarrativeGenerator), "Current and Past Medical History"));
            
            // The title is the same as the SHS
            components.Add(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.SharedHealthSummaryImunisations, NarrativeGenerator));

            components.Add(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.MedicareDocumentProvenance, consolidatedView.SCSContent.CustomNarrativeMedicareDocumentProvenance ,DocumentSections.Medicare, DocumentProvenanceEnum.MedicareDocumentProvenance, NarrativeGenerator));
            components.Add(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.RecentDocumentProvenance, consolidatedView.SCSContent.CustomNarrativeRecentDocumentProvenance, DocumentSections.RecentDocument, DocumentProvenanceEnum.RecentDocumentProvenance, NarrativeGenerator));
            components.Add(CDAGeneratorHelper.CreateComponent(new List<IDocument> { consolidatedView.SCSContent.AdvanceCareDirectiveCustodianDocumentProvenance },consolidatedView.SCSContent.CustomNarrativeAdvanceCareDirectiveCustodianDocumentProvenance, DocumentSections.AdvanceCareDirective, DocumentProvenanceEnum.AdvanceCareDirectiveCustodianDocumentProvenance, NarrativeGenerator));
            components.Add(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.RecentDiagnosticTestResultDocumentProvenance,consolidatedView.SCSContent.CustomNarrativeRecentDiagnosticTestResultDocumentProvenance, DocumentSections.RecentDiagnosticTestResult, DocumentProvenanceEnum.RecentDiagnosticTestResultDocumentProvenance, NarrativeGenerator));
            components.Add(CDAGeneratorHelper.CreateComponent(consolidatedView.SCSContent.ConsumerEnteredDocumentProvenance, consolidatedView.SCSContent.CustomNarrativeConsumerEnteredDocumentProvenance, DocumentSections.ConsumerEntered, DocumentProvenanceEnum.ConsumerEnteredDocumentProvenance, NarrativeGenerator));

            //SETUP administrative observations component
            if (!(consolidatedView.ShowAdministrativeObservationsSection.HasValue && consolidatedView.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            null,
                            consolidatedView.SCSContext.SubjectOfCare,
                            consolidatedView.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (consolidatedView.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && consolidatedView.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                        )
                );

            //Generate and return the SHS.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, null, authenticators, recipients, null, components, nonXmlBody, consolidatedView.IncludeLogo, consolidatedView.LogoByte, typeof(ConsolidatedView));
        }

        /// <summary>
        /// Generates a MedicareOverview CDA (XML) document from the Medicare View model
        /// </summary>
        /// <param name="medicareOverview">MedicareOverview</param>
        /// <returns>XmlDocument (CDA - Shared Health Summary)</returns>
        public static XmlDocument GenerateMedicareOverview(MedicareOverview medicareOverview)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("MedicareOverview", medicareOverview))
          {
              medicareOverview.Validate("MedicareOverview", vb.Messages);

              LogoSetupAndValidation(medicareOverview.LogoPath, medicareOverview.LogoByte, medicareOverview.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  medicareOverview.DocumentCreationTime,
                  CDADocumentType.MedicareOverview,
                  medicareOverview.CDAContext.DocumentId,
                  medicareOverview.CDAContext.SetId,
                  medicareOverview.CDAContext.Version,
                  medicareOverview.DocumentStatus,
                  medicareOverview.Title
              );

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(medicareOverview.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(medicareOverview.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(medicareOverview.CDAContext.Custodian);

          // Medicare View Exclusion Statement
          components.Add(CDAGeneratorHelper.CreateMedicareViewExclusionStatement(medicareOverview.SCSContent.ExclusionStatement, NarrativeGenerator));

          // Pharmaceutical Benefit Items 
          components.Add(CDAGeneratorHelper.CreateComponent(medicareOverview.SCSContent.PharmaceuticalBenefitsHistory, NarrativeGenerator));

          // Australian Childhood Immunisation Register Component 
          components.Add(CDAGeneratorHelper.CreateComponent(medicareOverview.SCSContent.AustralianChildhoodImmunisationRegisterHistory, NarrativeGenerator));

          // Australian OrganDonor Register Component 
          components.Add(CDAGeneratorHelper.CreateComponent(medicareOverview.SCSContent.AustralianOrganDonorRegisterDecisionInformation, NarrativeGenerator));

          // Medicare DVA Funded Services 
          components.Add(CDAGeneratorHelper.CreateComponent(medicareOverview.SCSContent.MedicareDvaFundedServicesHistory, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(medicareOverview.ShowAdministrativeObservationsSection.HasValue && medicareOverview.ShowAdministrativeObservationsSection.Value == false))
          components.Add
          (
                CDAGeneratorHelper.CreateAdministrativeObservationsFiltering
                    (
                        medicareOverview.SCSContext.SubjectOfCare,
                        medicareOverview.SCSContent.CustomNarrativeAdministrativeObservations,
                        clinicalDocument.recordTarget[0].patientRole.id[0].root,
                        medicareOverview.SCSContext.EarliestDateForFiltering,
                        medicareOverview.SCSContext.LatestDateForFiltering,
                        (medicareOverview.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && medicareOverview.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                    )
            );

          //Generate and return the SHS.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, null, authenticators, recipients, null, components, nonXmlBody, medicareOverview.IncludeLogo, medicareOverview.LogoByte, typeof(MedicareOverview));
        }

        /// <summary>
        /// Generates a PCEHRPrescriptionRecord CDA (XML) document from the Prescription Record model
        /// </summary>
        /// <param name="pcehrPrescriptionRecord">A PCEHRPrescriptionRecord</param>
        /// <returns>XmlDocument (CDA - Prescription Record)</returns>
        public static XmlDocument GeneratePcehrPrescriptionRecord(PCEHRPrescriptionRecord pcehrPrescriptionRecord)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("PCEHRPrescriptionRecord", pcehrPrescriptionRecord))
          {
              pcehrPrescriptionRecord.Validate("PCEHRPrescriptionRecord", vb.Messages);

              LogoSetupAndValidation(pcehrPrescriptionRecord.LogoPath, pcehrPrescriptionRecord.LogoByte, pcehrPrescriptionRecord.IncludeLogo, vb);

              if (pcehrPrescriptionRecord.DocumentStatus != DocumentStatus.Undefined)
              {
                vb.AddValidationMessage("DocumentStatus", null, "DocumentStatus is depreciated from this document, please set the DocumentStatus to undefined");
              }
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  pcehrPrescriptionRecord.DocumentCreationTime,
                  CDADocumentType.PrescriptionRecord,
                  pcehrPrescriptionRecord.CDAContext.DocumentId,
                  pcehrPrescriptionRecord.CDAContext.SetId,
                  pcehrPrescriptionRecord.CDAContext.Version,
                  null,
                  pcehrPrescriptionRecord.Title
              );

          // Setup the Parent Document
          if (pcehrPrescriptionRecord.CDAContext.ParentDocuments != null && pcehrPrescriptionRecord.CDAContext.ParentDocuments.Any())
          clinicalDocument.relatedDocument = CDAGeneratorHelper.CreateRelatedDocument
            (
              pcehrPrescriptionRecord.CDAContext.ParentDocuments
            ).ToArray();

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(pcehrPrescriptionRecord.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          if (pcehrPrescriptionRecord.SCSContent.PrescriptionItem != null)
            authors.Add(CDAGeneratorHelper.CreateAuthor(pcehrPrescriptionRecord.SCSContext.Prescriber));
          clinicalDocument.author = authors.ToArray();

          //SETUP encompassing encounter
          clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(pcehrPrescriptionRecord.SCSContext.PrescriberOrganisation);

          //SETUP the Legal Authenticator
          POCD_MT000040LegalAuthenticator legalAuthenticator = null;
          if (pcehrPrescriptionRecord.CDAContext.LegalAuthenticator != null)
          {
            legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(pcehrPrescriptionRecord.CDAContext.LegalAuthenticator);
          }

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(pcehrPrescriptionRecord.CDAContext.Custodian);

          // ETP Narrative for AdministrativeObservations
          StrucDocText prescriptionNarrative = NarrativeGenerator.CreateNarrative(pcehrPrescriptionRecord.SCSContext.SubjectOfCare, clinicalDocument.recordTarget[0].patientRole.id[0].root, false, null, null);

          // Setup Prescription Item
          components.Add(CDAGeneratorHelper.CreateComponent(pcehrPrescriptionRecord.SCSContent.PrescriptionItem as PCEHRPrescriptionItem, pcehrPrescriptionRecord.SCSContext.Prescriber, pcehrPrescriptionRecord.SCSContext.PrescriberOrganisation, pcehrPrescriptionRecord.SCSContext.SubjectOfCare, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(pcehrPrescriptionRecord.ShowAdministrativeObservationsSection.HasValue && pcehrPrescriptionRecord.ShowAdministrativeObservationsSection.Value == false))
          components.Add
              (
                  CDAGeneratorHelper.CreateAdministrativeObservations
                      (
                          pcehrPrescriptionRecord.SCSContext.SubjectOfCare,
                          pcehrPrescriptionRecord.SCSContent.CustomNarrativeAdministrativeObservations ?? prescriptionNarrative,
                          clinicalDocument.recordTarget[0].patientRole.id[0].root,
                          false,
                          (pcehrPrescriptionRecord.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && pcehrPrescriptionRecord.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator    
                      )
              );

          //Generate and return the SHS.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, null, components, nonXmlBody, pcehrPrescriptionRecord.IncludeLogo, pcehrPrescriptionRecord.LogoByte, typeof(PCEHRPrescriptionRecord));
        }

        /// <summary>
        /// Generates a PCEHRDispenseRecord CDA (XML) document from the Dispense Record model
        /// </summary>
        /// <param name="pcehrDispenseRecord">A PCEHRDispenseRecord</param>
        /// <returns>XmlDocument (CDA - Dispense Record)</returns>
        public static XmlDocument GeneratePcehrDispenseRecord(PCEHRDispenseRecord pcehrDispenseRecord)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("PCEHRDispenseRecord", pcehrDispenseRecord))
          {
            pcehrDispenseRecord.Validate("PCEHRDispenseRecord", vb.Messages);

            LogoSetupAndValidation(pcehrDispenseRecord.LogoPath, pcehrDispenseRecord.LogoByte, pcehrDispenseRecord.IncludeLogo, vb);

            if (pcehrDispenseRecord.DocumentStatus != DocumentStatus.Undefined)
            {
              vb.AddValidationMessage("DocumentStatus", null, "DocumentStatus is depreciated from this document, please set the DocumentStatus to undefined");
            }
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  pcehrDispenseRecord.DocumentCreationTime,
                  CDADocumentType.DispenseRecord,
                  pcehrDispenseRecord.CDAContext.DocumentId,
                  pcehrDispenseRecord.CDAContext.SetId,
                  pcehrDispenseRecord.CDAContext.Version,
                  null,
                  pcehrDispenseRecord.Title
              );

          // Setup the Parent Document
          if (pcehrDispenseRecord.CDAContext.ParentDocuments != null && pcehrDispenseRecord.CDAContext.ParentDocuments.Any())
          clinicalDocument.relatedDocument = CDAGeneratorHelper.CreateRelatedDocument
            (
              pcehrDispenseRecord.CDAContext.ParentDocuments
            ).ToArray();

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(pcehrDispenseRecord.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(pcehrDispenseRecord.SCSContext.Dispenser));
          clinicalDocument.author = authors.ToArray();

          //SETUP encompassing encounter
          clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(pcehrDispenseRecord.SCSContext.DispenserOrganisation);

          //SETUP the Legal Authenticator
          POCD_MT000040LegalAuthenticator legalAuthenticator = null;
          if (pcehrDispenseRecord.CDAContext.LegalAuthenticator != null)
          {
             legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(pcehrDispenseRecord.CDAContext.LegalAuthenticator);
          }

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(pcehrDispenseRecord.CDAContext.Custodian);

          // Setup DispenseItem Item
          components.Add(CDAGeneratorHelper.CreateComponent(pcehrDispenseRecord.SCSContent.DispenseItem as PCEHRDispenseItem, pcehrDispenseRecord.SCSContext.Dispenser, pcehrDispenseRecord.SCSContext.DispenserOrganisation, pcehrDispenseRecord.SCSContext.SubjectOfCare, NarrativeGenerator));

          // ETP Narrative for AdministrativeObservations
          var dispenseNarrative = NarrativeGenerator.CreateNarrative(pcehrDispenseRecord.SCSContext.SubjectOfCare, clinicalDocument.recordTarget[0].patientRole.id[0].root, false, null, null);

          //SETUP administrative observations component
          if (!(pcehrDispenseRecord.ShowAdministrativeObservationsSection.HasValue && pcehrDispenseRecord.ShowAdministrativeObservationsSection.Value == false))
          components.Add
              (
                  CDAGeneratorHelper.CreateAdministrativeObservations
                      (
                          pcehrDispenseRecord.SCSContext.SubjectOfCare,
                          pcehrDispenseRecord.SCSContent.CustomNarrativeAdministrativeObservations ?? dispenseNarrative,
                          clinicalDocument.recordTarget[0].patientRole.id[0].root,
                          false,
                          (pcehrDispenseRecord.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && pcehrDispenseRecord.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator   
                      )
              );

          //Generate and return the SHS.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, null, components, nonXmlBody, pcehrDispenseRecord.IncludeLogo, pcehrDispenseRecord.LogoByte, typeof(PCEHRDispenseRecord));
        }

        /// <summary>
        /// Generates a Prescription And Dispense View CDA (XML) document from the Prescription And Dispense View
        /// </summary>
        /// <param name="prescriptionAndDispenseView">A PrescriptionAndDispenseView</param>
        /// <returns>XmlDocument (CDA - Dispense Record)</returns>
        public static XmlDocument GeneratePcehrPrescriptionAndDispenseView(PrescriptionAndDispenseView prescriptionAndDispenseView)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("PrescriptionAndDispenseView", prescriptionAndDispenseView))
          {
            prescriptionAndDispenseView.Validate("PrescriptionAndDispenseView", vb.Messages);

            LogoSetupAndValidation(prescriptionAndDispenseView.LogoPath, prescriptionAndDispenseView.LogoByte, prescriptionAndDispenseView.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  prescriptionAndDispenseView.DocumentCreationTime,
                  CDADocumentType.PrescriptionAndDispenseView,
                  prescriptionAndDispenseView.CDAContext.DocumentId,
                  prescriptionAndDispenseView.CDAContext.SetId,
                  prescriptionAndDispenseView.CDAContext.Version,
                  prescriptionAndDispenseView.DocumentStatus,
                  prescriptionAndDispenseView.Title
              );

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(prescriptionAndDispenseView.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(prescriptionAndDispenseView.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(prescriptionAndDispenseView.CDAContext.Custodian);

          // Medicare View Exclusion Statement
          components.Add(CDAGeneratorHelper.CreatePrescriptionAndDispenseViewExclusionStatement(prescriptionAndDispenseView.SCSContent.ExclusionStatement, NarrativeGenerator));

          // Setup Prescription And Dispense View
          components.Add(CDAGeneratorHelper.CreateComponent(prescriptionAndDispenseView.SCSContent.PrescribingAndDispensingReports, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(prescriptionAndDispenseView.ShowAdministrativeObservationsSection.HasValue && prescriptionAndDispenseView.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservationsFiltering
                        (
                            prescriptionAndDispenseView.SCSContext.SubjectOfCare,
                            prescriptionAndDispenseView.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            prescriptionAndDispenseView.SCSContext.EarliestDateForFiltering,
                            prescriptionAndDispenseView.SCSContext.LatestDateForFiltering,
                            (prescriptionAndDispenseView.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && prescriptionAndDispenseView.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the SHS.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, null, authenticators, recipients, null, components, nonXmlBody, prescriptionAndDispenseView.IncludeLogo, prescriptionAndDispenseView.LogoByte, typeof(PrescriptionAndDispenseView));
        }

        /// <summary>
        /// Generates a Prescription And Dispense View CDA (XML) document from the Prescription And Dispense View
        /// </summary>
        /// <param name="pathologyResultView">A PathologyResultView</param>
        /// <returns>XmlDocument (CDA - Dispense Record)</returns>
        public static XmlDocument GeneratePCEHRPathologyResultView(PathologyResultView pathologyResultView)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("PathologyResultView", pathologyResultView))
          {
            pathologyResultView.Validate("PathologyResultView", vb.Messages);

            LogoSetupAndValidation(pathologyResultView.LogoPath, pathologyResultView.LogoByte, pathologyResultView.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  pathologyResultView.DocumentCreationTime,
                  CDADocumentType.PathologyResultView,
                  pathologyResultView.CDAContext.DocumentId,
                  pathologyResultView.CDAContext.SetId,
                  pathologyResultView.CDAContext.Version,
                  pathologyResultView.DocumentStatus,
                  pathologyResultView.Title
              );

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(pathologyResultView.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(pathologyResultView.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(pathologyResultView.CDAContext.Custodian);

          // Medicare View Exclusion Statement
          components.Add(CDAGeneratorHelper.CreatePathologyResultViewExclusionStatement(pathologyResultView.SCSContent.ExclusionStatement, NarrativeGenerator));

          //// Setup Prescription And Dispense View
          //components.Add(CDAGeneratorHelper.CreateComponent(pathologyResultView.SCSContent.PrescribingAndDispensingReports, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(pathologyResultView.ShowAdministrativeObservationsSection.HasValue && pathologyResultView.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                  (
                      CDAGeneratorHelper.CreateAdministrativeObservationsFiltering
                          (
                              pathologyResultView.SCSContext.SubjectOfCare,
                              pathologyResultView.SCSContent.CustomNarrativeAdministrativeObservations,
                              clinicalDocument.recordTarget[0].patientRole.id[0].root,
                              pathologyResultView.SCSContext.EarliestDateForFiltering,
                              pathologyResultView.SCSContext.LatestDateForFiltering,
                              (pathologyResultView.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && pathologyResultView.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator
                          )
                  );

          //Generate and return the SHS.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, null, authenticators, recipients, null, components, nonXmlBody, pathologyResultView.IncludeLogo, pathologyResultView.LogoByte, typeof(PathologyResultView));
        }

        /// <summary>
        /// Generates a Physical Measurements document
        /// </summary>
        /// <param name="physicalMeasurements">The PhysicalMeasurements object</param>
        /// <returns>XmlDocument (CDA - ConsumerEnteredAchievements)</returns>
        public static XmlDocument GeneratePhysicalMeasurements(PhysicalMeasurements physicalMeasurements)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("physicalMeasurements", physicalMeasurements))
          {
            physicalMeasurements.Validate("physicalMeasurements", vb.Messages);

            LogoSetupAndValidation(physicalMeasurements.LogoPath,physicalMeasurements.LogoByte, physicalMeasurements.IncludeLogo, vb);

            vb.ArgumentRequiredCheck("DocumentStatus", physicalMeasurements.DocumentStatus);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  physicalMeasurements.DocumentCreationTime,
                  (CDADocumentType)Enum.Parse(typeof(CDADocumentType), physicalMeasurements.StructuredDocumentModelIdentifier.Value.ToString()),   
                  physicalMeasurements.CDAContext.DocumentId,
                  physicalMeasurements.CDAContext.SetId,
                  physicalMeasurements.CDAContext.Version,
                  physicalMeasurements.DocumentStatus,
                  physicalMeasurements.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(physicalMeasurements.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(physicalMeasurements.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          if (physicalMeasurements.SCSContext.SubjectOfCare != null && physicalMeasurements.SCSContext.SubjectOfCare.Participant != null)
              authors.Add(CDAGeneratorHelper.CreateAuthor(physicalMeasurements.SCSContext.Author));

          clinicalDocument.author = authors.ToArray();

          //SETUP the HealthcareFacility
          if (physicalMeasurements.SCSContext.HealthcareFacility != null && physicalMeasurements.SCSContext.HealthcareFacility.Participant != null)
             clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(physicalMeasurements.SCSContext.HealthcareFacility);
         
          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(physicalMeasurements.CDAContext.Custodian);

          // Physical Measurements
          components.AddRange(CDAGeneratorHelper.CreateComponent(physicalMeasurements.SCSContent.PhysicalMeasurement, physicalMeasurements.SCSContent.CustomNarrativePhysicalMeasurements, NarrativeGenerator));

          var authorHealthcareProvider = physicalMeasurements.SCSContext.Author as IParticipationAuthorHealthcareProvider;

          // Set authorHealthcareProvider to null if it is not of the type IParticipationAuthorHealthcareProvider
          if (authorHealthcareProvider != null && authorHealthcareProvider.Participant == null)
          {
            authorHealthcareProvider = null;
          }
           
          //SETUP administrative observations component
          if (!(physicalMeasurements.ShowAdministrativeObservationsSection.HasValue && physicalMeasurements.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            physicalMeasurements.SCSContext.SubjectOfCare,
                            authorHealthcareProvider,
                            physicalMeasurements.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            "1.2.36.1.2001.1001.101.102.16080",
                            (physicalMeasurements.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && physicalMeasurements.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the E-Referral.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, physicalMeasurements.IncludeLogo, physicalMeasurements.LogoByte, typeof(PhysicalMeasurements));
        }

        /// <summary>
        /// Generates a Consumer Entered Achievements CDA (XML) document
        /// </summary>
        /// <param name="consumerEnteredAchievements">The ConsumerEnteredAchievements object</param>
        /// <returns>XmlDocument (CDA - ConsumerEnteredAchievements)</returns>
        public static XmlDocument GenerateConsumerEnteredAchievements(ConsumerEnteredAchievements consumerEnteredAchievements)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("consumerEnteredAchievements", consumerEnteredAchievements))
          {
            consumerEnteredAchievements.Validate("consumerEnteredAchievements", vb.Messages);

            LogoSetupAndValidation(consumerEnteredAchievements.LogoPath,consumerEnteredAchievements.LogoByte, consumerEnteredAchievements.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  consumerEnteredAchievements.DocumentCreationTime,
                  CDADocumentType.ConsumerEnteredAchievements,
                  consumerEnteredAchievements.CDAContext.DocumentId,
                  consumerEnteredAchievements.CDAContext.SetId,
                  consumerEnteredAchievements.CDAContext.Version,
                  consumerEnteredAchievements.DocumentStatus,
                  consumerEnteredAchievements.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(consumerEnteredAchievements.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(consumerEnteredAchievements.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          if (consumerEnteredAchievements.SCSContext.SubjectOfCare != null && consumerEnteredAchievements.SCSContext.SubjectOfCare.Participant != null)
              authors.Add(CDAGeneratorHelper.CreateAuthor(consumerEnteredAchievements.SCSContext.Author, consumerEnteredAchievements.SCSContext.SubjectOfCare.Participant.UniqueIdentifier));

          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(consumerEnteredAchievements.CDAContext.Custodian);

          components.Add(CDAGeneratorHelper.CreateComponent(consumerEnteredAchievements.SCSContent.Achievements, 
                                                            consumerEnteredAchievements.SCSContent.SectionTitle, 
                                                            consumerEnteredAchievements.SCSContent.CustomNarrativeAchievements, 
                                                            NarrativeGenerator));

          //SETUP administrative observations component
          if (!(consumerEnteredAchievements.ShowAdministrativeObservationsSection.HasValue && consumerEnteredAchievements.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            null,
                            consumerEnteredAchievements.SCSContext.SubjectOfCare,
                            consumerEnteredAchievements.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (consumerEnteredAchievements.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && consumerEnteredAchievements.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the E-Referral.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, consumerEnteredAchievements.IncludeLogo, consumerEnteredAchievements.LogoByte, typeof(ConsumerEnteredAchievements));
        }

        /// <summary>
        /// Generates a Consumer Entered Achievements CDA (XML) document
        /// </summary>
        /// <param name="consumerEnteredAchievements">The ConsumerEnteredAchievements object</param>
        /// <returns>XmlDocument (CDA - ConsumerEnteredAchievements)</returns>
        public static XmlDocument GenerateNSWConsumerEnteredAchievements(ConsumerEnteredAchievements consumerEnteredAchievements)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("NSWconsumerEnteredAchievements", consumerEnteredAchievements))
          {
            consumerEnteredAchievements.Validate("NSWconsumerEnteredAchievements", vb.Messages);

            LogoSetupAndValidation(consumerEnteredAchievements.LogoPath, consumerEnteredAchievements.LogoByte, consumerEnteredAchievements.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  consumerEnteredAchievements.DocumentCreationTime,
                  CDADocumentType.NSWConsumerEnteredAchievements,
                  consumerEnteredAchievements.CDAContext.DocumentId,
                  consumerEnteredAchievements.CDAContext.SetId,
                  consumerEnteredAchievements.CDAContext.Version,
                  consumerEnteredAchievements.DocumentStatus,
                  consumerEnteredAchievements.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(consumerEnteredAchievements.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(consumerEnteredAchievements.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          if (consumerEnteredAchievements.SCSContext.SubjectOfCare != null && consumerEnteredAchievements.SCSContext.SubjectOfCare.Participant != null)
            authors.Add(CDAGeneratorHelper.CreateAuthor(consumerEnteredAchievements.SCSContext.Author, consumerEnteredAchievements.SCSContext.SubjectOfCare.Participant.UniqueIdentifier));

          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(consumerEnteredAchievements.CDAContext.Custodian);

          components.Add(CDAGeneratorHelper.CreateComponent(consumerEnteredAchievements.SCSContent.Achievements,
                                                            consumerEnteredAchievements.SCSContent.SectionTitle,
                                                            consumerEnteredAchievements.SCSContent.CustomNarrativeAchievements,
                                                            NarrativeGenerator));

          //SETUP administrative observations component
          if (!(consumerEnteredAchievements.ShowAdministrativeObservationsSection.HasValue && consumerEnteredAchievements.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            null,
                            consumerEnteredAchievements.SCSContext.SubjectOfCare,
                            consumerEnteredAchievements.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (consumerEnteredAchievements.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && consumerEnteredAchievements.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the E-Referral.
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, consumerEnteredAchievements.IncludeLogo, consumerEnteredAchievements.LogoByte, typeof(ConsumerEnteredAchievements));
        }

        /// <summary>
        /// Generates an Pathology Result Report CDA (XML) document from the Pathology Report With Structured Content model
        /// </summary>
        /// <param name="pathologyReportWithStructuredContent">PathologyReportWithStructuredContent</param>
        /// <returns>XmlDocument (CDA - PathologyReportWithStructuredContent)</returns>
        public static XmlDocument GeneratePathologyReportWithStructuredContent(PathologyReportWithStructuredContent pathologyReportWithStructuredContent)
        {
            var vb = new ValidationBuilder();

            var entitlements = new Dictionary<string, List<Entitlement>>();

            if (vb.ArgumentRequiredCheck("PathologyReportWithStructuredContent", pathologyReportWithStructuredContent))
            {
                pathologyReportWithStructuredContent.Validate("PathologyReportWithStructuredContent", vb.Messages);

                LogoSetupAndValidation(pathologyReportWithStructuredContent.LogoPath, pathologyReportWithStructuredContent.LogoByte, pathologyReportWithStructuredContent.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var participants = new List<POCD_MT000040Participant1>();
            var authenticators = new List<POCD_MT000040Authenticator>();

            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    pathologyReportWithStructuredContent.DocumentCreationTime,
                    CDADocumentType.PathologyReportWithStructuredContent,
                    pathologyReportWithStructuredContent.CDAContext.DocumentId,
                    pathologyReportWithStructuredContent.CDAContext.SetId,
                    pathologyReportWithStructuredContent.CDAContext.Version,
                    DocumentStatus.Final,
                    pathologyReportWithStructuredContent.Title
                );

            //SETUP the Legal Authenticator
            POCD_MT000040LegalAuthenticator legalAuthenticator = null;
            if (pathologyReportWithStructuredContent.CDAContext.LegalAuthenticator != null)
            {
                legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(pathologyReportWithStructuredContent.CDAContext.LegalAuthenticator);
            }

            if (pathologyReportWithStructuredContent.SCSContext.OrderDetails != null)
            {
                clinicalDocument.inFulfillmentOf = new[]
                {
                  CDAGeneratorHelper.CreateinFulfillmentOf(pathologyReportWithStructuredContent.SCSContext.OrderDetails)
                };

                // SETUP the patient nominated contacts
                if (pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester != null)
                {
                    participants.Add(CDAGeneratorHelper.CreateParticipant(pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester));

                    if (pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester.Participant != null && pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester.Participant.Person != null && pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester.Participant.Entitlements != null)
                        entitlements.Add(pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester.Participant.UniqueIdentifier.ToString(), pathologyReportWithStructuredContent.SCSContext.OrderDetails.Requester.Participant.Entitlements);
                }
            }

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(pathologyReportWithStructuredContent.CDAContext.Custodian);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(pathologyReportWithStructuredContent.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            if (pathologyReportWithStructuredContent.SCSContext.Author != null)
            {
                authors.Add(CDAGeneratorHelper.CreateAuthor(pathologyReportWithStructuredContent.SCSContext.Author));

                if (pathologyReportWithStructuredContent.SCSContext.Author.Participant != null && pathologyReportWithStructuredContent.SCSContext.Author.Participant.Person != null && pathologyReportWithStructuredContent.SCSContext.Author.Participant.Person.Entitlements != null)
                    entitlements.Add(pathologyReportWithStructuredContent.SCSContext.Author.Participant.UniqueIdentifier.ToString(), pathologyReportWithStructuredContent.SCSContext.Author.Participant.Person.Entitlements);
            }

            clinicalDocument.author = authors.ToArray();

            // Setup Observations
            components.Add(CDAGeneratorHelper.CreateComponent(pathologyReportWithStructuredContent.SCSContent.PathologyTestResult,
                                                              pathologyReportWithStructuredContent.SCSContext.ReportingPathologist,
                                                              pathologyReportWithStructuredContent.SCSContent.RelatedDocument,
                                                              pathologyReportWithStructuredContent.SCSContent.CustomNarrative,
                                                              NarrativeGenerator));

            //STRUCTURED BODY
            //SETUP administrative observations component
            if (!(pathologyReportWithStructuredContent.ShowAdministrativeObservationsSection.HasValue && pathologyReportWithStructuredContent.ShowAdministrativeObservationsSection.Value == false))
                components.Add
                        (
                            CDAGeneratorHelper.CreateAdministrativeObservations
                                (
                                    pathologyReportWithStructuredContent.SCSContext.SubjectOfCare,
                                    entitlements,
                                    pathologyReportWithStructuredContent.SCSContent.CustomNarrativeAdministrativeObservations,
                                    clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                    false,
                                    (pathologyReportWithStructuredContent.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && pathologyReportWithStructuredContent.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator
                                )
                        );

            // Generate and return the Specialist Letter
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, pathologyReportWithStructuredContent.IncludeLogo, pathologyReportWithStructuredContent.LogoByte, typeof(PathologyReportWithStructuredContent));
        }

        /// <summary>
        /// Generates an Pathology Result Report CDA (XML) document from the Pathology Result Report model
        /// </summary>
        /// <param name="pathologyResultReport">PathologyResultReport</param>
        /// <returns>XmlDocument (CDA - PathologyResultReport)</returns>
        public static XmlDocument GeneratePathologyResultReport(PathologyResultReport pathologyResultReport)
        {
          var vb = new ValidationBuilder();

          var entitlements = new Dictionary<string, List<Entitlement>>();

          if (vb.ArgumentRequiredCheck("PathologyResultReport", pathologyResultReport))
          {
            pathologyResultReport.Validate("PathologyResultReport", vb.Messages);

            LogoSetupAndValidation(pathologyResultReport.LogoPath, pathologyResultReport.LogoByte, pathologyResultReport.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var participants = new List<POCD_MT000040Participant1>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  pathologyResultReport.DocumentCreationTime,
                  CDADocumentType.PathologyResultReport,
                  pathologyResultReport.CDAContext.DocumentId,
                  pathologyResultReport.CDAContext.SetId,
                  pathologyResultReport.CDAContext.Version,
                  DocumentStatus.Final,
                  pathologyResultReport.Title
              );

          //SETUP the Legal Authenticator
          POCD_MT000040LegalAuthenticator legalAuthenticator = null;
          if (pathologyResultReport.CDAContext.LegalAuthenticator != null)
          {
            legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(pathologyResultReport.CDAContext.LegalAuthenticator);
          }

          if (pathologyResultReport.SCSContext.OrderDetails != null)
          {
              clinicalDocument.inFulfillmentOf = new []
              {
                  CDAGeneratorHelper.CreateinFulfillmentOf(pathologyResultReport.SCSContext.OrderDetails)
              };

              // SETUP the patient nominated contacts
              if (pathologyResultReport.SCSContext.OrderDetails.Requester != null)
              {
                  participants.Add(CDAGeneratorHelper.CreateParticipant(pathologyResultReport.SCSContext.OrderDetails.Requester));

                  if (pathologyResultReport.SCSContext.OrderDetails.Requester.Participant != null && pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person != null && pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Entitlements != null)
                      entitlements.Add(pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.UniqueIdentifier.ToString(), pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Entitlements);
              }
          }

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(pathologyResultReport.CDAContext.Custodian);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(pathologyResultReport.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          if (pathologyResultReport.SCSContext.Author != null)
          {
             authors.Add(CDAGeneratorHelper.CreateAuthor(pathologyResultReport.SCSContext.Author));

             if (pathologyResultReport.SCSContext.Author.Participant != null && pathologyResultReport.SCSContext.Author.Participant.Person != null && pathologyResultReport.SCSContext.Author.Participant.Person.Entitlements != null)
                 entitlements.Add(pathologyResultReport.SCSContext.Author.Participant.UniqueIdentifier.ToString(), pathologyResultReport.SCSContext.Author.Participant.Person.Entitlements);
         }

          clinicalDocument.author = authors.ToArray();

          // Setup Observations
          components.Add(CDAGeneratorHelper.CreateComponent(pathologyResultReport.SCSContent.PathologyTestResult,
                                                            pathologyResultReport.SCSContext.ReportingPathologist,
                                                            pathologyResultReport.SCSContent.RelatedDocument, 
                                                            pathologyResultReport.SCSContent.CustomNarrative,
                                                            NarrativeGenerator));

          //STRUCTURED BODY
          //SETUP administrative observations component
          if (!(pathologyResultReport.ShowAdministrativeObservationsSection.HasValue && pathologyResultReport.ShowAdministrativeObservationsSection.Value == false))
            components.Add
                    (
                        CDAGeneratorHelper.CreateAdministrativeObservations
                            (
                                pathologyResultReport.SCSContext.SubjectOfCare,
                                entitlements,
                                pathologyResultReport.SCSContent.CustomNarrativeAdministrativeObservations,
                                clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                false,
                                (pathologyResultReport.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && pathologyResultReport.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator
                            )
                    );

          // Generate and return the Specialist Letter
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, pathologyResultReport.IncludeLogo, pathologyResultReport.LogoByte, typeof(PathologyResultReport));
        }

        /// <summary>
        /// Generates an Diagnostic Imaging Report (XML) document from the Diagnostic Result Report model
        /// </summary>
        /// <param name="advanceCareInformation">The AdvanceCareInformation</param>
        /// <returns>XmlDocument (CDA - AdvanceCareInformation)</returns>
        public static XmlDocument GenerateAdvanceCareInformation(AdvanceCareInformation advanceCareInformation)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("AdvanceCareInformation", advanceCareInformation))
            {
                advanceCareInformation.Validate("AdvanceCareInformation", vb.Messages);

                LogoSetupAndValidation(advanceCareInformation.LogoPath, advanceCareInformation.LogoByte, advanceCareInformation.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var participants = new List<POCD_MT000040Participant1>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    advanceCareInformation.DocumentCreationTime,
                    CDADocumentType.AdvanceCareInformation,
                    advanceCareInformation.CDAContext.DocumentId,
                    advanceCareInformation.CDAContext.SetId,
                    advanceCareInformation.CDAContext.Version,
                    DocumentStatus.Final,
                    advanceCareInformation.Title
                );

            //SETUP the Legal Authenticator
            POCD_MT000040LegalAuthenticator legalAuthenticator = null;
            if (advanceCareInformation.CDAContext.LegalAuthenticator != null)
                legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(advanceCareInformation.CDAContext.LegalAuthenticator);

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(advanceCareInformation.CDAContext.Custodian);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(advanceCareInformation.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            // NOTE: NEED TO MAP Requester Order Identifier once it has been modelled in the IG
            //SETUP the author
            if (advanceCareInformation.SCSContext.Author != null)
                authors.Add(CDAGeneratorHelper.CreateAuthor(advanceCareInformation.SCSContext.Author));

            clinicalDocument.author = authors.ToArray();

            // Setup the Advance Care Information Component 
            if (advanceCareInformation.SCSContent.DocumentDetails != null)
                components.Add(
                    CDAGeneratorHelper.CreateComponent(advanceCareInformation.SCSContent.DocumentDetails, 
                                                       advanceCareInformation.SCSContent.CustomNarrativeAdvanceCareInformationSection, 
                                                       NarrativeGenerator));

            //STRUCTURED BODY
            //SETUP administrative observations component
            if (!(advanceCareInformation.ShowAdministrativeObservationsSection.HasValue && !advanceCareInformation.ShowAdministrativeObservationsSection.Value))
                components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            null,
                            advanceCareInformation.SCSContext.SubjectOfCare,
                            advanceCareInformation.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (advanceCareInformation.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && advanceCareInformation.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator
                        )
                );

            // Generate and return the Specialist SpecialistLetter
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, advanceCareInformation.IncludeLogo, advanceCareInformation.LogoByte, typeof(AdvanceCareInformation));
        }

        /// <summary>
        /// Generates an Diagnostic Imaging Report (XML) document from the Diagnostic Result Report model
        /// </summary>
        /// <param name="diagnosticImagingReport">The DiagnosticImagingReport</param>
        /// <returns>XmlDocument (CDA - DiagnosticImagingReport)</returns>
        public static XmlDocument GenerateDiagnosticImagingReport(DiagnosticImagingReport diagnosticImagingReport)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("DiagnosticImagingReport", diagnosticImagingReport))
            {
                diagnosticImagingReport.Validate("DiagnosticImagingReport", vb.Messages);

                LogoSetupAndValidation(diagnosticImagingReport.LogoPath, diagnosticImagingReport.LogoByte, diagnosticImagingReport.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var entitlements = new Dictionary<string, List<Entitlement>>();

            var authors = new List<POCD_MT000040Author>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var participants = new List<POCD_MT000040Participant1>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
                (
                    diagnosticImagingReport.DocumentCreationTime,
                    CDADocumentType.DiagnosticImagingReport,
                    diagnosticImagingReport.CDAContext.DocumentId,
                    diagnosticImagingReport.CDAContext.SetId,
                    diagnosticImagingReport.CDAContext.Version,
                    DocumentStatus.Final,
                    diagnosticImagingReport.Title
                );

            //SETUP the Legal Authenticator
            POCD_MT000040LegalAuthenticator legalAuthenticator = null;
            if (diagnosticImagingReport.CDAContext.LegalAuthenticator != null)
                legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(diagnosticImagingReport.CDAContext.LegalAuthenticator);

            if (diagnosticImagingReport.SCSContext.OrderDetails != null)
            {
                // Order Details
                clinicalDocument.inFulfillmentOf = new[]
                {
                    CDAGeneratorHelper.CreateinFulfillmentOf(diagnosticImagingReport.SCSContext.OrderDetails)
                };

                // SETUP the patient nominated contacts
                if (diagnosticImagingReport.SCSContext.OrderDetails.Requester != null)
                {
                    participants.Add(CDAGeneratorHelper.CreateParticipant(diagnosticImagingReport.SCSContext.OrderDetails.Requester));

                    if (diagnosticImagingReport.SCSContext.OrderDetails.Requester.Participant != null && 
                        diagnosticImagingReport.SCSContext.OrderDetails.Requester.Participant.Person != null && 
                        diagnosticImagingReport.SCSContext.OrderDetails.Requester.Participant.Entitlements != null)
                        entitlements.Add(diagnosticImagingReport.SCSContext.OrderDetails.Requester.Participant.UniqueIdentifier.ToString(), diagnosticImagingReport.SCSContext.OrderDetails.Requester.Participant.Entitlements);
                }
            }

            // SETUP Observations
            components.Add(CDAGeneratorHelper.CreateComponent(diagnosticImagingReport.SCSContent.ImagingExaminationResults,
                                                              diagnosticImagingReport.SCSContext.ReportingRadiologist,
                                                              diagnosticImagingReport.SCSContent.RelatedDocument,
                                                              diagnosticImagingReport.SCSContent.DiagnosticImagingCustomNarrative,
                                                              NarrativeGenerator));

            //SETUP the author
            if (diagnosticImagingReport.SCSContext.Author != null)
            {
                authors.Add(CDAGeneratorHelper.CreateAuthor(diagnosticImagingReport.SCSContext.Author));

                if (diagnosticImagingReport.SCSContext.Author.Participant != null && 
                    diagnosticImagingReport.SCSContext.Author.Participant.Person != null && 
                    diagnosticImagingReport.SCSContext.Author.Participant.Person.Entitlements != null)
                    entitlements.Add(diagnosticImagingReport.SCSContext.Author.Participant.UniqueIdentifier.ToString(), diagnosticImagingReport.SCSContext.Author.Participant.Person.Entitlements);
            }

            clinicalDocument.author = authors.ToArray();

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(diagnosticImagingReport.CDAContext.Custodian);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(diagnosticImagingReport.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //STRUCTURED BODY
            //SETUP administrative observations component
            if (!(diagnosticImagingReport.ShowAdministrativeObservationsSection.HasValue && 
                diagnosticImagingReport.ShowAdministrativeObservationsSection.Value == false))
                components.Add
                        (
                            CDAGeneratorHelper.CreateAdministrativeObservations
                                (
                                    diagnosticImagingReport.SCSContext.SubjectOfCare,
                                    entitlements,
                                    diagnosticImagingReport.SCSContent.CustomNarrativeAdministrativeObservations,
                                    clinicalDocument.recordTarget[0].patientRole.id[0].root,
                                    false,
                                    (diagnosticImagingReport.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && diagnosticImagingReport.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator
                                )
                        );

            // Generate and return the Specialist SpecialistLetter
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, diagnosticImagingReport.IncludeLogo, diagnosticImagingReport.LogoByte, typeof(DiagnosticImagingReport));
        }

        #endregion

        #region ATS ETP

        /// <summary>
        /// Generates an E-Prescription CDA (XML) document from the e-Prescription model
        /// </summary>
        /// <param name="ePrescription">EPrescription</param>
        /// <returns>XmlDocument (CDA - EPrescription)</returns>
        public static XmlDocument GenerateAtsPrescription(EPrescription ePrescription)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("EPrescription", ePrescription))
          {
              ePrescription.Validate("EPrescription", vb.Messages);

              if (ePrescription.CDAContext.Version != "1")
              {
                 vb.AddValidationMessage("ePrescription.CDAContext.Version", null, "ePrescription.CDAContext.Version must have a value of 1");
              }

              if (ePrescription.DocumentStatus != DocumentStatus.Undefined)
              {
                vb.AddValidationMessage("DocumentStatus", null, "DocumentStatus is depreciated from this document, please set the DocumentStatus to undefined");
              }
          }

          if (vb.Messages.Count > 0)
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var participants = new List<POCD_MT000040Participant1>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  ePrescription.DocumentCreationTime,
                  CDADocumentType.EPrescription,
                  ePrescription.CDAContext.DocumentId,
                  ePrescription.CDAContext.SetId,
                  ePrescription.CDAContext.Version,
                  null,
                  ePrescription.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(ePrescription.CDAContext.LegalAuthenticator);

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(ePrescription.CDAContext.Custodian);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(ePrescription.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(ePrescription.SCSContext.Prescriber));
          clinicalDocument.author = authors.ToArray();

          //SETUP encompassing encounter
          clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(ePrescription.SCSContext.PrescriberOrganisation);

          // ETP Narrative for AdministrativeObservations
          var ePrescriptionNarrative = NarrativeGenerator.CreateNarrative(ePrescription.SCSContext.SubjectOfCare, clinicalDocument.recordTarget[0].patientRole.id[0].root, false, null, null);

          // Setup Prescription Item
          components.Add(CDAGeneratorHelper.CreateComponent(ePrescription.SCSContent.PrescriptionItem, ePrescription.SCSContext.Prescriber, ePrescription.SCSContext.PrescriberOrganisation, ePrescription.SCSContext.SubjectOfCare, NarrativeGenerator));

          // Setup Observations
          components.Add(CDAGeneratorHelper.CreateComponent(ePrescription.SCSContent.Observation, NarrativeGenerator));

          // STRUCTURED BODY
          // SETUP administrative observations component
          if (!(ePrescription.ShowAdministrativeObservationsSection.HasValue && ePrescription.ShowAdministrativeObservationsSection.Value == false))
          components.Add
              (
                  CDAGeneratorHelper.CreateAdministrativeObservations
                      (
                          ePrescription.SCSContext.SubjectOfCare,
                          ePrescription.SCSContent.CustomNarrativeAdministrativeObservations ?? ePrescriptionNarrative,
                          clinicalDocument.recordTarget[0].patientRole.id[0].root,
                          false,
                          (ePrescription.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && ePrescription.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator  
                      )
              );

          //Generate and return the Specialist Letter
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, ePrescription.IncludeLogo, ePrescription.LogoByte, typeof(EPrescription));
        }

        /// <summary>
        /// Generates a Dispense Record CDA (XML) document from the Dispense Record Model
        /// </summary>
        /// <param name="dispenseRecord">DispenseRecord</param>
        /// <returns>XmlDocument (CDA - Dispense Record)</returns>
        public static XmlDocument GenerateAtsDispenseRecord(DispenseRecord dispenseRecord)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("DispenseRecord", dispenseRecord))
          {
             dispenseRecord.Validate("DispenseRecord", vb.Messages);
          }

          if (vb.Messages.Count > 0)
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var participants = new List<POCD_MT000040Participant1>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  dispenseRecord.DocumentCreationTime,
                  CDADocumentType.EDispenseRecord,
                  dispenseRecord.CDAContext.DocumentId,
                  dispenseRecord.CDAContext.SetId,
                  dispenseRecord.CDAContext.Version,
                  null,
                  dispenseRecord.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(dispenseRecord.CDAContext.LegalAuthenticator);

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(dispenseRecord.CDAContext.Custodian);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(dispenseRecord.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(dispenseRecord.SCSContext.Dispenser));
          clinicalDocument.author = authors.ToArray();

          //SETUP the participants    
          clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(dispenseRecord.SCSContext.DispenserOrganisation);

          // Setup Dispense Item
          components.Add(CDAGeneratorHelper.CreateComponent(dispenseRecord.SCSContent.DispenseItem, dispenseRecord.SCSContext.SubjectOfCare, dispenseRecord.SCSContext.DispenserOrganisation, NarrativeGenerator));

          // STRUCTURED BODY
          // SETUP administrative observations component
          if (!(dispenseRecord.ShowAdministrativeObservationsSection.HasValue && dispenseRecord.ShowAdministrativeObservationsSection.Value == false))
          components.Add
              (
                  CDAGeneratorHelper.CreateAdministrativeObservations
                      (
                          dispenseRecord.SCSContext.SubjectOfCare,
                          dispenseRecord.SCSContent.CustomNarrativeAdministrativeObservations,
                          clinicalDocument.recordTarget[0].patientRole.id[0].root,
                          false,
                          (dispenseRecord.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && dispenseRecord.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                      )
              );

          //Generate and return the Specialist Letter
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, dispenseRecord.IncludeLogo, dispenseRecord.LogoByte, typeof(DispenseRecord));
        }


        /// <summary>
        /// Generates a Prescription Request CDA (XML) document from the prescription Request model
        /// </summary>
        /// <param name="prescriptionRequest">PrescriptionRequest</param>
        /// <returns>XmlDocument (CDA - Prescription Request)</returns>
        public static XmlDocument GenerateAtsPrescriptionRequest(EPrescriptionRequest prescriptionRequest)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("PrescriptionRequest", prescriptionRequest))
          {
            prescriptionRequest.Validate("ePrescriptionRequest", vb.Messages);
          }

          if (vb.Messages.Count > 0)
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var participants = new List<POCD_MT000040Participant1>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  prescriptionRequest.DocumentCreationTime,
                  CDADocumentType.EPrescriptionRequest,
                  prescriptionRequest.CDAContext.DocumentId,
                  prescriptionRequest.CDAContext.SetId,
                  prescriptionRequest.CDAContext.Version,
                  null,
                  prescriptionRequest.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(prescriptionRequest.CDAContext.LegalAuthenticator);

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(prescriptionRequest.CDAContext.Custodian);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(prescriptionRequest.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(prescriptionRequest.SCSContext.Dispenser));
          clinicalDocument.author = authors.ToArray();

          //SETUP the participants    
          clinicalDocument.componentOf = CDAGeneratorHelper.CreateComponentOf(prescriptionRequest.SCSContext.DispenserOrganisation);

          // Setup Prescriber Instruction Detail
          components.Add(CDAGeneratorHelper.CreateComponent(
                                                prescriptionRequest.SCSContent.PrescriberInstructionDetail,
                                                prescriptionRequest.SCSContext.Prescriber,
                                                prescriptionRequest.SCSContext.PrescriberOrganisation,
                                                NarrativeGenerator));

         //  Setup Prescription Request Item
          components.Add(CDAGeneratorHelper.CreateComponent
              (
                  prescriptionRequest.SCSContent.PrescriptionRequestItem,
                  prescriptionRequest.SCSContext.SubjectOfCare,
                  prescriptionRequest.SCSContext.DispenserOrganisation,
                  prescriptionRequest.SCSContent.RequesterNote,
                  NarrativeGenerator
              ));


          // STRUCTURED BODY
          // SETUP administrative observations component
          if (!(prescriptionRequest.ShowAdministrativeObservationsSection.HasValue && !prescriptionRequest.ShowAdministrativeObservationsSection.Value))
          components.Add
              (
                  CDAGeneratorHelper.CreateAdministrativeObservations
                      (
                          prescriptionRequest.SCSContext.SubjectOfCare,
                          prescriptionRequest.SCSContent.CustomNarrativeAdministrativeObservations,
                          clinicalDocument.recordTarget[0].patientRole.id[0].root,
                          false,
                          (prescriptionRequest.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && prescriptionRequest.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                      )
              );

          //Generate and return the Specialist Letter
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, prescriptionRequest.IncludeLogo, prescriptionRequest.LogoByte, typeof(EPrescriptionRequest));
        }

        #endregion 

        #region CeHR

        /// <summary>
        /// Generates a NSW Health Check Assessment (XML) document from the NSWHealthCheckAssessment model
        /// </summary>
        /// <param name="healthCheckAssessment">The NSWHealthCheckAssessment object</param>
        /// <returns>XmlDocument (CDA - NSWHealthCheckAssessment)</returns>
        public static XmlDocument GenerateNSWHealthCheckAssessment(NSWHealthCheckAssessment healthCheckAssessment)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("NSWHealthCheckAssessment", healthCheckAssessment))
          {
            healthCheckAssessment.Validate("NSWHealthCheckAssessment", vb.Messages);

            LogoSetupAndValidation(healthCheckAssessment.LogoPath, healthCheckAssessment.LogoByte, healthCheckAssessment.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  healthCheckAssessment.DocumentCreationTime,
                  CDADocumentType.NSWHealthCheckAssessment,
                  healthCheckAssessment.CDAContext.DocumentId,
                  healthCheckAssessment.CDAContext.SetId,
                  healthCheckAssessment.CDAContext.Version,
                  healthCheckAssessment.DocumentStatus,
                  healthCheckAssessment.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(healthCheckAssessment.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(healthCheckAssessment.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(healthCheckAssessment.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(healthCheckAssessment.CDAContext.Custodian);

          // Setup Prescriber Instruction Detail
          if (healthCheckAssessment.SCSContent.MeasurementInformation != null)  
          components.Add(CDAGeneratorHelper.CreateComponent(healthCheckAssessment.SCSContent.MeasurementInformation, NarrativeGenerator));

          // Setup Prescriber Instruction Detail
          if (healthCheckAssessment.SCSContent.HealthCheckAssesment != null)
            components.Add(CDAGeneratorHelper.CreateComponent(healthCheckAssessment.SCSContent.HealthCheckAssesment, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(healthCheckAssessment.ShowAdministrativeObservationsSection.HasValue && healthCheckAssessment.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                  (
                      CDAGeneratorHelper.CreateAdministrativeObservations
                          (
                             healthCheckAssessment.SCSContext.Author,
                              healthCheckAssessment.SCSContext.SubjectOfCare,
                              healthCheckAssessment.SCSContent.CustomNarrativeAdministrativeObservations,
                              clinicalDocument.recordTarget[0].patientRole.id[0].root,
                              (healthCheckAssessment.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && healthCheckAssessment.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                          )
                  );

          //Generate and return the NSW Health Check Assessment
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, healthCheckAssessment.IncludeLogo, healthCheckAssessment.LogoByte, typeof(NSWHealthCheckAssessment));
        }


        /// <summary>
        /// Generates a Child Health Check Schedule View (XML) document from the ChildHealthCheckScheduleView model
        /// </summary>
        /// <param name="childHealthCheckScheduleView">The ChildHealthCheckScheduleView object</param>
        /// <returns>XmlDocument (CDA - ChildHealthCheckScheduleView)</returns>
        public static XmlDocument GenerateChildHealthCheckScheduleView(ChildHealthCheckScheduleView childHealthCheckScheduleView)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("ChildHealthCheckScheduleView", childHealthCheckScheduleView))
          {
            childHealthCheckScheduleView.Validate("ChildHealthCheckScheduleView", vb.Messages);

            LogoSetupAndValidation(childHealthCheckScheduleView.LogoPath,childHealthCheckScheduleView.LogoByte, childHealthCheckScheduleView.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  childHealthCheckScheduleView.DocumentCreationTime,
                  CDADocumentType.ChildHealthCheckScheduleView,
                  childHealthCheckScheduleView.CDAContext.DocumentId,
                  childHealthCheckScheduleView.CDAContext.SetId,
                  childHealthCheckScheduleView.CDAContext.Version,
                  childHealthCheckScheduleView.DocumentStatus,
                  childHealthCheckScheduleView.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(childHealthCheckScheduleView.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(childHealthCheckScheduleView.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(childHealthCheckScheduleView.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(childHealthCheckScheduleView.CDAContext.Custodian);

          // Setup Questionnaire Documents
          if (childHealthCheckScheduleView.SCSContent.QuestionnaireDocuments != null)
            components.AddRange(CDAGeneratorHelper.CreateComponent(childHealthCheckScheduleView.SCSContent.QuestionnaireDocuments, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(childHealthCheckScheduleView.ShowAdministrativeObservationsSection.HasValue && childHealthCheckScheduleView.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            null,
                            childHealthCheckScheduleView.SCSContext.SubjectOfCare,
                            childHealthCheckScheduleView.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (childHealthCheckScheduleView.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && childHealthCheckScheduleView.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the NSW Health Check Assessment
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, childHealthCheckScheduleView.IncludeLogo, childHealthCheckScheduleView.LogoByte, typeof(ChildHealthCheckScheduleView));
        }

        /// <summary>
        /// Generates a Observation View Document (XML) document from the ObservationViewDocument model
        /// </summary>
        /// <param name="observationViewDocument">The ObservationViewDocument object</param>
        /// <returns>XmlDocument (CDA - ObservationViewDocument)</returns>
        public static XmlDocument GenerateObservationViewDocument(ObservationViewDocument observationViewDocument)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("ObservationViewDocument", observationViewDocument))
          {
            observationViewDocument.Validate("ObservationViewDocument", vb.Messages);

            LogoSetupAndValidation(observationViewDocument.LogoPath, observationViewDocument.LogoByte, observationViewDocument.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  observationViewDocument.DocumentCreationTime,
                  CDADocumentType.ObservationView,
                  observationViewDocument.CDAContext.DocumentId,
                  observationViewDocument.CDAContext.SetId,
                  observationViewDocument.CDAContext.Version,
                  observationViewDocument.DocumentStatus,
                  observationViewDocument.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(observationViewDocument.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(observationViewDocument.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(observationViewDocument.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(observationViewDocument.CDAContext.Custodian);

          // Consumer Entered Measurement Entry
          if (observationViewDocument.SCSContent.ConsumerEnteredMeasurementEntry != null)
            components.Add(CDAGeneratorHelper.CreateComponent(observationViewDocument.SCSContent.ConsumerEnteredMeasurementEntry, CeHRRecordSections.ConsumerEnteredMeasurementInformation, NarrativeGenerator));

          // Provider Entered Measurement Information Entry
          if (observationViewDocument.SCSContent.ProviderEnteredMeasurementInformationEntry != null)
            components.Add(CDAGeneratorHelper.CreateComponent(observationViewDocument.SCSContent.ProviderEnteredMeasurementInformationEntry, CeHRRecordSections.ProviderEnteredMeasurementInformation, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(observationViewDocument.ShowAdministrativeObservationsSection.HasValue && observationViewDocument.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            null,
                            observationViewDocument.SCSContext.SubjectOfCare,
                            observationViewDocument.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (observationViewDocument.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && observationViewDocument.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the NSW Health Check Assessment
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, observationViewDocument.IncludeLogo, observationViewDocument.LogoByte, typeof(ObservationViewDocument));
        }

        /// <summary>
        /// Generates a Child Parent Questionnaire (XML) document from the ConsumerQuestionnaire model
        /// </summary>
        /// <param name="consumerQuestionnaire">The ConsumerQuestionnaire object</param>
        /// <returns>XmlDocument (CDA - ConsumerQuestionnaire)</returns>
        public static XmlDocument GenerateConsumerQuestionnaire(ConsumerQuestionnaire consumerQuestionnaire)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("ConsumerQuestionnaire", consumerQuestionnaire))
          {
            consumerQuestionnaire.Validate("ConsumerQuestionnaire", vb.Messages);

            LogoSetupAndValidation(consumerQuestionnaire.LogoPath, consumerQuestionnaire.LogoByte, consumerQuestionnaire.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  consumerQuestionnaire.DocumentCreationTime,
                  CDADocumentType.ConsumerQuestionnaire,
                  consumerQuestionnaire.CDAContext.DocumentId,
                  consumerQuestionnaire.CDAContext.SetId,
                  consumerQuestionnaire.CDAContext.Version,
                  consumerQuestionnaire.DocumentStatus,
                  consumerQuestionnaire.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(consumerQuestionnaire.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(consumerQuestionnaire.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(consumerQuestionnaire.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(consumerQuestionnaire.CDAContext.Custodian);

          // Child Parent Questionnaire
          if (consumerQuestionnaire.SCSContent.Questionnaire != null)
            components.Add(CDAGeneratorHelper.CreateComponent(consumerQuestionnaire.SCSContent.Questionnaire, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(consumerQuestionnaire.ShowAdministrativeObservationsSection.HasValue && consumerQuestionnaire.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            consumerQuestionnaire.SCSContext.Author,
                            consumerQuestionnaire.SCSContext.SubjectOfCare,
                            consumerQuestionnaire.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (consumerQuestionnaire.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && consumerQuestionnaire.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return theConsumerQuestionnaire
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, consumerQuestionnaire.IncludeLogo, consumerQuestionnaire.LogoByte, typeof(ConsumerQuestionnaire));
        }

        /// <summary>
        /// Generates a NSW Health Check Assessment (XML) document from the GeneratePersonalHealthObservation model
        /// </summary>
        /// <param name="personalHealthObservation">The GeneratePersonalHealthObservation object</param>
        /// <returns>XmlDocument (CDA - GeneratePersonalHealthObservation)</returns>
        public static XmlDocument GeneratePersonalHealthObservation(PersonalHealthObservation personalHealthObservation)
        {
          var vb = new ValidationBuilder();

          if (vb.ArgumentRequiredCheck("PersonalHealthObservation", personalHealthObservation))
          {
            personalHealthObservation.Validate("PersonalHealthObservation", vb.Messages);

            LogoSetupAndValidation(personalHealthObservation.LogoPath,personalHealthObservation.LogoByte, personalHealthObservation.IncludeLogo, vb);
          }

          if (vb.Messages.Any())
          {
            var ve = new ValidationException(vb.Messages);
            var messageString = ve.GetMessagesString();

            throw new ValidationException(vb.Messages);
          }

          var authors = new List<POCD_MT000040Author>();
          var recipients = new List<POCD_MT000040InformationRecipient>();
          var participants = new List<POCD_MT000040Participant1>();
          var components = new List<POCD_MT000040Component3>();
          var patients = new List<POCD_MT000040RecordTarget>();
          var authenticators = new List<POCD_MT000040Authenticator>();
          POCD_MT000040NonXMLBody nonXmlBody = null;

          //SETUP the clinical document object
          var clinicalDocument = CDAGeneratorHelper.CreateDocument
              (
                  personalHealthObservation.DocumentCreationTime,
                  CDADocumentType.ConsumerEnteredMeasurements,
                  personalHealthObservation.CDAContext.DocumentId,
                  personalHealthObservation.CDAContext.SetId,
                  personalHealthObservation.CDAContext.Version,
                  personalHealthObservation.DocumentStatus,
                  personalHealthObservation.Title
              );

          //SETUP the Legal Authenticator
          var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(personalHealthObservation.CDAContext.LegalAuthenticator);

          //SETUP the patient
          patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(personalHealthObservation.SCSContext.SubjectOfCare));
          clinicalDocument.recordTarget = patients.ToArray();

          //SETUP the author
          authors.Add(CDAGeneratorHelper.CreateAuthor(personalHealthObservation.SCSContext.Author));
          clinicalDocument.author = authors.ToArray();

          //SETUP the Custodian
          clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(personalHealthObservation.CDAContext.Custodian);

          // Setup Measurement Informations
          if (personalHealthObservation.SCSContent.MeasurementInformations != null)  
          components.Add(CDAGeneratorHelper.CreateComponent(personalHealthObservation.SCSContent.MeasurementInformations, NarrativeGenerator));

          //SETUP administrative observations component
          if (!(personalHealthObservation.ShowAdministrativeObservationsSection.HasValue && personalHealthObservation.ShowAdministrativeObservationsSection.Value == false))
          components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            personalHealthObservation.SCSContext.Author,
                            personalHealthObservation.SCSContext.SubjectOfCare,
                            personalHealthObservation.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            (personalHealthObservation.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && personalHealthObservation.ShowAdministrativeObservationsNarrativeAndTitle.Value == false) ? null : NarrativeGenerator 
                        )
                );

          //Generate and return the GeneratePersonalHealthObservation
          return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, personalHealthObservation.IncludeLogo, personalHealthObservation.LogoByte, typeof(PersonalHealthObservation));
        }

        #endregion

        #region

        /// <summary>
        /// Generates a Service Referral CDA (XML) document
        /// </summary>
        /// <param name="serviceReferral">ServiceReferral</param>
        /// <returns>XmlDocument (CDA - Service Referral)</returns>
        public static XmlDocument GenerateServiceReferral(ServiceReferral serviceReferral)
        {
            var vb = new ValidationBuilder();

            if (vb.ArgumentRequiredCheck("ServiceReferral", serviceReferral))
            {
                serviceReferral.Validate("ServiceReferral", vb.Messages);

                LogoSetupAndValidation(serviceReferral.LogoPath, serviceReferral.LogoByte, serviceReferral.IncludeLogo, vb);
            }

            if (vb.Messages.Any())
            {
                var ve = new ValidationException(vb.Messages);
                var messageString = ve.GetMessagesString();

                throw new ValidationException(vb.Messages);
            }

            var authors = new List<POCD_MT000040Author>();
            var participants = new List<POCD_MT000040Participant1>();
            var recipients = new List<POCD_MT000040InformationRecipient>();
            var components = new List<POCD_MT000040Component3>();
            var patients = new List<POCD_MT000040RecordTarget>();
            var authenticators = new List<POCD_MT000040Authenticator>();
            var coverages = new List<Coverage2>();

            POCD_MT000040NonXMLBody nonXmlBody = null;

            //SETUP the clinical document object
            var clinicalDocument = CDAGeneratorHelper.CreateDocument
            (
                serviceReferral.DocumentCreationTime,
                CDADocumentType.ServiceReferral,
                serviceReferral.CDAContext.DocumentId,
                serviceReferral.CDAContext.SetId,
                serviceReferral.CDAContext.Version,
                serviceReferral.DocumentStatus,
                serviceReferral.Title
            );

            //SETUP the Legal Authenticator
            var legalAuthenticator = CDAGeneratorHelper.CreateLegalAuthenticator(serviceReferral.CDAContext.LegalAuthenticator);

            //SETUP the patient
            patients.Add(CDAGeneratorHelper.CreateSubjectOfCare(serviceReferral.SCSContext.SubjectOfCare));
            clinicalDocument.recordTarget = patients.ToArray();

            //SETUP the author
            authors.Add(CDAGeneratorHelper.CreateAuthor(serviceReferral.SCSContext.Author));
            clinicalDocument.author = authors.ToArray();

            if (serviceReferral.SCSContext.PatientNominatedContact != null && serviceReferral.SCSContext.PatientNominatedContact.Any())
            {
                foreach (var patientNominatedContact in serviceReferral.SCSContext.PatientNominatedContact)
                {
                    participants.Add(CDAGeneratorHelper.CreateParticipant(patientNominatedContact, ParticipationType.IRCP, RoleClassAssociative.CON, null, serviceReferral.SCSContext?.SubjectOfCare?.Participant?.UniqueIdentifier.ToString()));
                }

                foreach (var primaryCareProvider in serviceReferral.SCSContext.PatientNominatedContact)
                {
                    // Entitlements
                    if (primaryCareProvider?.Participant?.Entitlements != null && primaryCareProvider.Participant.Entitlements.Any())
                        coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                            primaryCareProvider.Participant.Entitlements, 
                            primaryCareProvider.Participant.UniqueIdentifier.ToString(), 
                            RoleClass.ASSIGNED, 
                            ParticipationType.HLD));
                }
            }

            if (serviceReferral.SCSContext.PrimaryCareProvider != null)
            {
                //SETUP the Primary Care Provider   
                participants.Add(CDAGeneratorHelper.CreateParticipant(serviceReferral.SCSContext.PrimaryCareProvider, ParticipationType.PART, RoleClassAssociative.PROV, new CE { code = "PCP" }, serviceReferral.SCSContext?.SubjectOfCare?.Participant?.UniqueIdentifier.ToString()));

                // Entitlements
                if (serviceReferral.SCSContext.PrimaryCareProvider?.Participant?.Entitlements != null && serviceReferral.SCSContext.PrimaryCareProvider.Participant.Entitlements.Any())
                    coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                        serviceReferral.SCSContext.PrimaryCareProvider.Participant.Entitlements,
                        serviceReferral.SCSContext.PrimaryCareProvider.Participant.UniqueIdentifier.ToString(),
                        RoleClass.ASSIGNED,
                        ParticipationType.HLD));
            }

            if (serviceReferral.SCSContext.InterestedParty != null && serviceReferral.SCSContext.InterestedParty.Any())
            {
                recipients.AddRange(serviceReferral.SCSContext.InterestedParty.Select(interestedParty => CDAGeneratorHelper.CreateInformationRecipient(interestedParty, x_InformationRecipient.TRC, serviceReferral.SCSContext?.SubjectOfCare?.Participant?.UniqueIdentifier.ToString())));

                foreach (var interestedParty in serviceReferral.SCSContext.InterestedParty)
                {
                    // Entitlements
                    if (interestedParty?.Participant?.Entitlements != null && interestedParty.Participant.Entitlements.Any())
                        coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                            interestedParty.Participant.Entitlements,
                            interestedParty.Participant.UniqueIdentifier.ToString(),
                            RoleClass.ASSIGNED,
                            ParticipationType.HLD));
                }
            }

            if (serviceReferral.SCSContext.Referee != null)
            {
                //SETUP the Referee 
                recipients.Add(CDAGeneratorHelper.CreateInformationRecipient(serviceReferral.SCSContext.Referee, x_InformationRecipient.TRC, serviceReferral.SCSContext?.SubjectOfCare?.Participant?.UniqueIdentifier.ToString()));

                // Entitlements
                if (serviceReferral.SCSContext.Referee?.Participant?.Entitlements != null && serviceReferral.SCSContext.Referee.Participant.Entitlements.Any())
                    coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                        serviceReferral.SCSContext.Referee.Participant.Entitlements,
                        serviceReferral.SCSContext.Referee.Participant.UniqueIdentifier.ToString(),
                        RoleClass.ASSIGNED,
                        ParticipationType.HLD));
            }

            //SETUP the Custodian
            clinicalDocument.custodian = CDAGeneratorHelper.CreateCustodian(serviceReferral.CDAContext.Custodian);

            //XML body file
            components.Add
            (
                CDAGeneratorHelper.CreateStructuredBodyFileComponent(serviceReferral.SCSContent.StructuredBodyFiles, NarrativeGenerator)
            );

            // Service Referral Detail 
            if (serviceReferral.SCSContent.ServiceReferralDetail != null)
            {
                components.Add(CDAGeneratorHelper.CreateComponent(serviceReferral.SCSContent.ServiceReferralDetail, NarrativeGenerator));

                if (serviceReferral.SCSContent.ServiceReferralDetail.RequestedService != null && serviceReferral.SCSContent.ServiceReferralDetail.RequestedService.Any())
                {
                    foreach (var requestedService in serviceReferral.SCSContent.ServiceReferralDetail.RequestedService)
                    {
                        // Entitlements
                        if (requestedService?.ServiceProvider?.Participant?.Entitlements != null && requestedService.ServiceProvider.Participant.Entitlements.Any())
                            coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                                requestedService.ServiceProvider.Participant.Entitlements,
                                requestedService.ServiceProvider.Participant.UniqueIdentifier.ToString(),
                                RoleClass.ASSIGNED,
                                ParticipationType.HLD));
                    }
                }
            }

            //Setup the recipients   
            if (serviceReferral.CDAContext.InformationRecipients != null)
            {
                recipients.AddRange(serviceReferral.CDAContext.InformationRecipients.Select(CDAGeneratorHelper.CreateInformationRecipient));
            }

            // Current Service
            if (serviceReferral.SCSContent.CurrentService != null)
            {
                components.Add(CDAGeneratorHelper.CreateComponent(serviceReferral.SCSContent.CurrentService, serviceReferral.SCSContent.CustomNarrativeCurrentService, NarrativeGenerator));

                foreach (var currentService in serviceReferral.SCSContent.CurrentService)
                {
                    // Entitlements
                    if (currentService?.ServiceProvider?.Participant?.Entitlements != null && currentService.ServiceProvider.Participant.Entitlements.Any())
                        coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                            currentService.ServiceProvider.Participant.Entitlements,
                            currentService.ServiceProvider.Participant.UniqueIdentifier.ToString(),
                            RoleClass.ASSIGNED,
                            ParticipationType.HLD));
                }
            }

            // Adverse Reactions
            if (serviceReferral.SCSContent.AdverseReactions != null)
            {
                components.Add(CDAGeneratorHelper.CreateComponent((SCSModel.Common.AdverseReactions)serviceReferral.SCSContent.AdverseReactions, NarrativeGenerator, "103.16302.120.1.1"));
            }

            // Medications 
            if (serviceReferral.SCSContent.Medications != null)
            {
                components.Add(CDAGeneratorHelper.CreateComponent(serviceReferral.SCSContent.Medications, NarrativeGenerator, CDADocumentType.ServiceReferral));
            }

            // Diagnostic Investigations 
            if (serviceReferral.SCSContent.DiagnosticInvestigations != null)
            {
                components.Add(CDAGeneratorHelper.CreateComponent(serviceReferral.SCSContent.DiagnosticInvestigations, NarrativeGenerator));

                if (serviceReferral.SCSContent.DiagnosticInvestigations?.RequestedService != null && serviceReferral.SCSContent.DiagnosticInvestigations.RequestedService.Any())
                {
                    foreach (var requestedService in serviceReferral.SCSContent.DiagnosticInvestigations.RequestedService)
                    {
                        // Entitlements
                        if (requestedService?.ServiceProvider?.Participant?.Entitlements != null && requestedService.ServiceProvider.Participant.Entitlements.Any())

                            coverages.AddRange(CDAGeneratorHelper.CreateEntitlements(
                                requestedService.ServiceProvider.Participant.Entitlements,
                                requestedService.ServiceProvider.Participant.UniqueIdentifier.ToString(),
                                RoleClass.ASSIGNED,
                                ParticipationType.HLD));
                    }
                }
            }

            //SETUP administrative observations component
            if (!(serviceReferral.ShowAdministrativeObservationsSection.HasValue && serviceReferral.ShowAdministrativeObservationsSection.Value == false))
                components.Add
                (
                    CDAGeneratorHelper.CreateAdministrativeObservations
                        (
                            serviceReferral.SCSContext.Author,
                            serviceReferral.SCSContext.SubjectOfCare,
                            serviceReferral.SCSContent.CustomNarrativeAdministrativeObservations,
                            clinicalDocument.recordTarget[0].patientRole.id[0].root,
                            serviceReferral.ShowAdministrativeObservationsNarrativeAndTitle.HasValue && serviceReferral.ShowAdministrativeObservationsNarrativeAndTitle.Value == false ? null : NarrativeGenerator,
                            coverages
                        )
                );

            //Add Narrative Only Document 
            if (serviceReferral.SCSContent.NarrativeOnlyDocument != null && serviceReferral.SCSContent.NarrativeOnlyDocument.Any())
                components.AddRange
                (
                   CDAGeneratorHelper.CreateNarrativeOnlyDocument(serviceReferral.SCSContent.NarrativeOnlyDocument)
                 );

            //Generate and return the SHS.
            return CDAGeneratorHelper.CreateXml(clinicalDocument, authors, legalAuthenticator, authenticators, recipients, participants, components, nonXmlBody, serviceReferral.IncludeLogo, serviceReferral.LogoByte, typeof(ServiceReferral));
        }

    #endregion

    #region Setup and Validation

    /// <summary>
    /// Verifies that the logo path location is a valid path and is included in the bin directory
    /// </summary>
    /// <returns>XmlDocument (CDA - EventSummary)</returns>
    public static void LogoSetupAndValidation(string logoPath, byte[] logoByte, bool includeLogo, ValidationBuilder vb)
    {
      if (includeLogo)
      {
        bool UserProvidedLogoFileExists = false;
        bool DefaultLogoFileExists = File.Exists("Logo.png");

        //Use the Logo in the path provided else use the default. Note that a logoPath of "." is the same as not provided
        if (!logoPath.IsNullOrEmptyWhitespace() && logoPath != ".")
        {
          if (File.Exists(System.IO.Path.Combine(logoPath, "Logo.png")))
          {
            //If the user of the library has set a location for the Logo 'logoPath' and it exists then copy that
            //logo file to the Application directory / bin
            File.Copy(System.IO.Path.Combine(logoPath, "Logo.png"), "Logo.png", true);
            UserProvidedLogoFileExists = true;
          }
          else
          {
            //No Logo file found in the path they provided, So they wanted to use their Logo file but it can not be found, so error.
            vb.AddValidationMessage(vb.PathName, string.Empty, string.Format("The provided logo path '{0}' does not contain an image", logoPath));
          }
        }

        //Can only have one or either the User provided Logo file or the User provided Logo Byte Array
        //This detects that we have both.
        if (logoByte != null && UserProvidedLogoFileExists)
        {
          vb.AddValidationMessage("Logo", null, "The LogoPath and LogoByte are Mutually exclusive, please pass a file to the file path or provide an byte array entry");
        }

        //Check we have either a Logo Bytes or a logo file regardless of which logo file it is, default or user provided file
        //If all are null or false then we have no Logo at all and yet includeLogo was set to true, so error
        if (logoByte == null && !UserProvidedLogoFileExists && !DefaultLogoFileExists)
        {
          vb.AddValidationMessage(vb.PathName, string.Empty, "Logo.png needs to be included in the output directory or include a byte array if 'IncludeLogo' is true");
        }
      }
    }
    #endregion
  }
}
