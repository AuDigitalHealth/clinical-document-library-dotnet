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
using System.Xml;
using System.Linq;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using CDA.Generator.Common;
using ProblemDiagnosis = Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.ProblemDiagnosis;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This extension of the helper CDAGeneratorHelper for Discharge Summary Version 
    /// </summary>
    public static partial class CDAGeneratorHelper
    {
        #region internal Methods - Participant

        /// <summary>
        /// Creates a Participant for a Discharge Summary header
        /// </summary>
        /// <param name="locationOfDischarge">The location of discharge</param>
        /// <param name="nominatedPrimaryHealthCareProviders">A list of nominatedPrimaryHealthCareProviders</param>
        /// <param name="otherParticipants">A list of other Participants</param>
        /// <returns>A list of POCD_MT000040Participant1's</returns>
        internal static List<POCD_MT000040Participant1> CreateParticipantsDischargeSummary(String locationOfDischarge, List<IParticipationNominatedPrimaryHealthCareProvider> nominatedPrimaryHealthCareProviders, List<IParticipationOtherParticipant> otherParticipants)
        {// 
          var participants = new List<POCD_MT000040Participant1>();

          // Setup Location participant for Discharge
          if (!locationOfDischarge.IsNullOrEmptyWhitespace())
          {
            participants.Add(CreateParticipantOrganisationForDischarge(locationOfDischarge));
          }

          // Setup Nominated Primary Healthcare Provider
          if (nominatedPrimaryHealthCareProviders != null && nominatedPrimaryHealthCareProviders.Any())
          {
              participants.AddRange(nominatedPrimaryHealthCareProviders.Select(CreateParticipation));
          }

          // Setup participants for Other Participant
          if (otherParticipants != null && otherParticipants.Any())
          {
            foreach (var otherParticipant in otherParticipants)
            {
              participants.Add(CreateParticipation(otherParticipant));
            }
          }
          return participants;
        }

        /// <summary>
        /// Creates a participant for an organisation
        /// </summary>
        /// <param name="organisation">The participant organisation</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipantOrganisationForDischarge(String organisation)
        {
            var returnParticipant = new POCD_MT000040Participant1
            {
                typeCode = ParticipationType.ORG,
                associatedEntity = new POCD_MT000040AssociatedEntity
                {
                    classCode = RoleClassAssociative.SDLOC,
                    id = CreateIdentifierArray
                        (
                            CreateGuid(),
                            null,
                            null,
                            null,
                            null
                        ),
                }
            };

          returnParticipant.associatedEntity.code = CreateCodedWithExtensionElement(null, null, null, organisation, null, null);

          return returnParticipant;
        }

        /// <summary>
        /// Creates a Participation Health Professional Entry
        /// </summary>
        /// <returns>POCD_MT000040Performer2</returns>
        internal static POCD_MT000040Performer2 CreateParticipation(IParticipationHealthProfessional participation)
        {
          POCD_MT000040Performer2 performer = null;

          if (participation != null && participation.Participant != null)
          {
            var castedParticipation = (IParticipation)participation;
            castedParticipation.Participant = (IParticipant)participation.Participant;

            if (castedParticipation.Participant != null)
            {
              if (participation.Participant.Person != null) castedParticipation.Participant.Person = participation.Participant.Person;
              if (participation.Participant.Addresses != null) castedParticipation.Participant.Addresses = participation.Participant.Addresses;
              if (participation.Participant.Organisation != null) castedParticipation.Participant.Organisation = participation.Participant.Organisation;
            }

            performer = CreatePerformer(castedParticipation, ParticipationPhysicalPerformer.PRF);
          }

          return performer;
        }

        /// <summary>
        /// Creates a Participation Nominated Primary Health Care Provider
        /// </summary>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipation(IParticipationNominatedPrimaryHealthCareProvider participation)
        {
          POCD_MT000040Participant1 returnParticipant = null;

          if (participation != null && participation.Participant != null)
          {
            var castedParticipation = (IParticipation)participation;
            castedParticipation.Participant = (IParticipant)participation.Participant;

            if (castedParticipation.Participant != null)
            {
              if (participation.Participant.Person != null) castedParticipation.Participant.Person = participation.Participant.Person;
              if (participation.Participant.Addresses != null) castedParticipation.Participant.Addresses = participation.Participant.Addresses;
              if (participation.Participant.Organisation != null) castedParticipation.Participant.Organisation = participation.Participant.Organisation;

            }

            returnParticipant = CreateParticipant(castedParticipation, ParticipationType.PART, RoleClassAssociative.PROV, new CE { code = "PCP" });
          }

          return returnParticipant;
        }

        /// <summary>
        /// Creates a Participation Other Participant
        /// </summary>
        /// <param name="participation">Participation Other Participant</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipation(IParticipationOtherParticipant participation)
        {
          POCD_MT000040Participant1 returnParticipant = null;

          if (participation != null && participation.Participant != null)
          {
            var castedParticipation = (IParticipation)participation;
            castedParticipation.Participant = (IParticipant)participation.Participant;

            if (castedParticipation.Participant != null)
            {
              if (participation.Participant.Person != null) castedParticipation.Participant.Person = participation.Participant.Person;
              if (participation.Participant.Addresses != null) castedParticipation.Participant.Addresses = participation.Participant.Addresses;
            }

            returnParticipant =
                CreateParticipant(castedParticipation, ParticipationType.PART,
                (RoleClassAssociative)Enum.Parse(typeof(RoleClassAssociative), participation.RoleType.GetAttributeValue<NameAttribute, string>(x => x.Code)),
                null);
          }

          return returnParticipant;
        }

        /// <summary>
        /// Creates a Responsible Health Professional for the Encounter Participant
        /// </summary>
        /// <param name="responsibleHealthProfessional">responsibleHealthProfessional</param>
        /// <returns>POCD_MT000040EncounterParticipant</returns>
        internal static POCD_MT000040EncounterParticipant CreateEncounterParticipant(IParticipationResponsibleHealthProfessional responsibleHealthProfessional)
        {
          var cdaResponsibleHealthProfessional = new POCD_MT000040EncounterParticipant
          {
            typeCode = x_EncounterParticipant.DIS,
            time = CreateIntervalTimestamp(responsibleHealthProfessional.ParticipationPeriod),
            assignedEntity = new POCD_MT000040AssignedEntity
            {
              id = CreateIdentifierArray(CreateGuid()),
              addr = responsibleHealthProfessional.Participant == null ? null : CreateAddressArray(responsibleHealthProfessional.Participant.Addresses)
            }
          };

          // Create Role
          if (responsibleHealthProfessional.Role != null)
            cdaResponsibleHealthProfessional.assignedEntity.code = CreateCodedWithExtensionElement(responsibleHealthProfessional.Role);

          if (responsibleHealthProfessional.Participant != null)
            cdaResponsibleHealthProfessional.assignedEntity.telecom = CreateTelecomunicationArray(responsibleHealthProfessional.Participant.ElectronicCommunicationDetails);

          if (responsibleHealthProfessional.Participant != null)
          {
            // Create assigned person
            cdaResponsibleHealthProfessional.assignedEntity.assignedPerson = responsibleHealthProfessional.Participant.Person == null ? null : new POCD_MT000040Person
            {
              asEntityIdentifier = responsibleHealthProfessional.Participant.Person.Identifiers == null ? null : CreateEntityIdentifier(responsibleHealthProfessional.Participant.Person.Identifiers).ToArray(),
              name = CreatePersonNameArray(responsibleHealthProfessional.Participant.Person.PersonNames),
              asEmployment = responsibleHealthProfessional.Participant.Person.Organisation == null ? null : new Employment
              {
                employerOrganization = CreateOrganisation(responsibleHealthProfessional.Participant.Person.Organisation)
              }
            };
          }

          return cdaResponsibleHealthProfessional;
        }


        #endregion

        #region internal Methods - Component

        /// <summary>
        /// Describes the general details about a stay in a healthcare facility as an admitted patient or as a
        /// patient managed in the Emergency Department without leading to admission.
        /// </summary>
        /// <param name="encounter">Encounter</param>
        /// <returns>POCD_MT000040Component1</returns>
        internal static POCD_MT000040Component1 CreateComponentOf(Encounter encounter)
        {
          if (encounter == null) return null;

          var component = new POCD_MT000040Component1 {encompassingEncounter = new POCD_MT000040EncompassingEncounter()};

            NullFlavor? nullFlavor = null;
          if (encounter.EncounterPeriodNullFlavor.HasValue)
            nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor), encounter.EncounterPeriodNullFlavor.GetAttributeValue<NameAttribute, string>(x => x.Code));

          component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(encounter.EncounterPeriod, nullFlavor);

          if (encounter.SeparationMode != null)
             component.encompassingEncounter.dischargeDispositionCode = CreateCodedWithExtensionElement
             (
                encounter.SeparationMode
             );

          return component;
        }

        /// <summary>
        /// The details of the healthcare encounter which instigated the creation of the discharge summary.
        /// </summary>
        /// <param name="eventDischargeSummary">The eventDischargeSummary</param>
        /// <param name="narrativeGenerator">The narrativeGenerator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(Event eventDischargeSummary, INarrativeGenerator narrativeGenerator)
        {
            // THIS PARTICULAR FUNCTION IS NO LONGER USED - See CDAGeneratorHelperLegacy
            POCD_MT000040Component3 component = null;

          if (eventDischargeSummary != null)
          {
            var components = new List<POCD_MT000040Component5>();
            var entryList = new List<POCD_MT000040Entry>();

            // Begin Event section
            component = new POCD_MT000040Component3
            {
              section = CreateSectionCodeTitle("101.16006", CodingSystem.NCTIS, "Event", 
              "This section may contain the following subsections Problems/Diagnoses This Visit, Clinical Interventions Performed This Visit and Clinical Summary and Diagnostic Investigations.")
            };

            if (eventDischargeSummary.CustomNarrativeEvent != null) component.section.text = eventDischargeSummary.CustomNarrativeEvent;

            if (eventDischargeSummary.ClinicalSynopsis != null)
            {
              components.Add(CreateComponent(eventDischargeSummary.ClinicalSynopsis, "102.15513.4.1.1", narrativeGenerator));
            }

            if (eventDischargeSummary.ProblemDiagnosesThisVisit != null)
            {
              components.Add(CreateComponent(eventDischargeSummary.ProblemDiagnosesThisVisit, narrativeGenerator));
            }

            if (eventDischargeSummary.ClinicalIntervention != null)
            {
              components.Add(CreateComponent(eventDischargeSummary.ClinicalIntervention, narrativeGenerator));
            }

            if (eventDischargeSummary.DiagnosticInvestigations != null)
            {
              components.Add(CreateComponent(eventDischargeSummary.DiagnosticInvestigations, narrativeGenerator));
            }

            component.section.component = components.ToArray();
            component.section.entry = entryList.ToArray();
          }

          return component;
        }

        /// <summary>
        /// Creates a administration observations component
        /// </summary>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <param name="customNarrative"> Provide a custom Narrative </param>
        /// <param name="patientId">The Patient id</param>
        /// <param name="specialty">A list of ICodableText's</param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateAdministrativeObservationsForDischargeSummary(IParticipationSubjectOfCare subjectOfCareParticipation, StrucDocText customNarrative, String patientId, List<ICodableText> specialty, INarrativeGenerator narrativeGenerator)
        {
          var component = CreateAdministrativeObservations
          (
              subjectOfCareParticipation,
              null,
              patientId,
              true,
              narrativeGenerator
          );

          //Setup the Specialty Entry for administrativeObservations
          if (specialty != null && specialty.Any())
          {
            var entries = new List<POCD_MT000040Entry>(component.section.entry.ToArray()) { CreateSpecialtyEntry(specialty) };
            component.section.entry = entries.ToArray();
          }

          //NARRATIVE
          if (narrativeGenerator != null)
          {
            component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(subjectOfCareParticipation, patientId, true, null, null, specialty);
          }

          return component;
        }

        /// <summary>
        /// Creates a Component for a Problem Diagnoses This Visit
        /// </summary>
        /// <param name="problemDiagnosesThisVisit">Problem Diagnoses This Visit</param>
        /// <param name="narrativeGenerator">The narrativeGenerator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateComponent(ProblemDiagnosesThisVisit problemDiagnosesThisVisit, INarrativeGenerator narrativeGenerator)
        {
          POCD_MT000040Component5 component = null;

          component = new POCD_MT000040Component5
          {
            section = CreateSectionCodeTitle("101.16142", CodingSystem.NCTIS, "Problems/Diagnoses This Visit")
          };

          var entries = new List<POCD_MT000040Entry>();

          // Create ExclusionStatements
          if (problemDiagnosesThisVisit.ExclusionStatement != null)
          {
            entries.Add(CreateExclusionStatement(problemDiagnosesThisVisit.ExclusionStatement, "103.16302.4.3.1"));
          }

          // Create ProblemDiagnosis
          if (problemDiagnosesThisVisit.ProblemDiagnosis != null)
          {
            entries.AddRange(CreateProblemDiagnosisEntries(problemDiagnosesThisVisit.ProblemDiagnosis));
          }

          // Add the entries
          component.section.entry = entries.ToArray();

          // Create the narrative 
          component.section.text = problemDiagnosesThisVisit.CustomNarrativeProblemDiagnosesThisVisit ?? narrativeGenerator.CreateNarrative(problemDiagnosesThisVisit);

          return component;
        }

        /// <summary>
        /// Creates a Component for a clinicalIntervention
        /// </summary>
        /// <param name="clinicalIntervention">A ClinicalIntervention</param>
        /// <param name="narrativeGenerator">A narrativeGenerator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateComponent(ClinicalIntervention clinicalIntervention, INarrativeGenerator narrativeGenerator)
        {
          POCD_MT000040Component5 component = null;

          if (clinicalIntervention != null)
          {
            component = new POCD_MT000040Component5
            {
              section = CreateSectionCodeTitle("101.20109", CodingSystem.NCTIS, "Clinical Interventions Performed This Visit")
            };

            var entries = new List<POCD_MT000040Entry>();
            if (clinicalIntervention.Description != null)
              foreach (var description in clinicalIntervention.Description)
              {
                var entry = CreateEntryProcedureEvent(CreateConceptDescriptor(description));
                entry.typeCode = x_ActRelationshipEntry.DRIV;
                entries.Add(entry);
              }

            if (entries.Any())
            {
              component.section.entry = entries.ToArray();
            }

            component.section.text = clinicalIntervention.CustomNarrativeClinicalIntervention ?? narrativeGenerator.CreateNarrative(clinicalIntervention);
          }

          return component;
        }

        /// <summary>
        /// Creates a Component for a ClinicalSynopsis
        /// </summary>
        /// <param name="clinicalSynopsis">A ClinicalSynopsis</param>
        /// <param name="code">A code</param>
        /// <param name="narrativeGenerator">A narrativeGenerator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateComponent(ClinicalSynopsis clinicalSynopsis, String code, INarrativeGenerator narrativeGenerator)
        {
          POCD_MT000040Component5 component = null;

          if (clinicalSynopsis != null)
          {

            component = new POCD_MT000040Component5
            {
              section = CreateSectionCodeTitle(code, CodingSystem.NCTIS, "Clinical Synopsis")
            };

            if (!clinicalSynopsis.Description.IsNullOrEmptyWhitespace())
            {
              component.section.entry = new[] 
                    {
                        CreateEntryActEvent
                        (
                            x_ActRelationshipEntry.DRIV,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            CreateConceptDescriptor
                                (
                                    "103.15582",
                                    CodingSystem.NCTIS,
                                    "Clinical Synopsis Description",
                                    null
                                ),
                            clinicalSynopsis.Description,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null
                        )
                    };
            }
            component.section.text = clinicalSynopsis.CustomNarrativeClinicalSynopsis ?? narrativeGenerator.CreateNarrative(clinicalSynopsis);
          }

          return component;
        }

        /// <summary>
        /// Creates a medications component
        /// </summary>
        /// <param name="medications">IMedicationsDischargeSummary</param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IMedicationsDischargeSummary medications, INarrativeGenerator narrativeGenerator)
        {
            //DS
          POCD_MT000040Component3 component = null;

          if (medications != null)
          {
            var medicationComponents = new List<POCD_MT000040Component5>();

            component = new POCD_MT000040Component3
            {
              section = CreateSectionCodeTitle("101.16022", CodingSystem.NCTIS, "Medications", "This section contains the following subsections: Current Medications On Discharge and Ceased Medications.")
            };

            if (medications.CustomNarrativeMedications != null) component.section.text = medications.CustomNarrativeMedications;

            // Create Current Medication
            if (medications.CurrentMedications != null)
            {
              var currentMedicationsComponent = new POCD_MT000040Component5
              {
                section = CreateSectionCodeTitle("101.16146.4.1.1", CodingSystem.NCTIS, "Current Medications on Discharge")
              };

              var entries = new List<POCD_MT000040Entry>();

              // Current Medication Exclusions
              if (medications.CurrentMedications.ExclusionStatement != null)
              {
                entries.Add(CreateExclusionStatement(medications.CurrentMedications.ExclusionStatement, "103.16302.4.3.2"));
              }

              if (medications.CurrentMedications.TherapeuticGoods != null)
              {
                entries.AddRange(CreateTherapeuticGoods(medications.CurrentMedications.TherapeuticGoods));
              }

              currentMedicationsComponent.section.entry = entries.ToArray();

              currentMedicationsComponent.section.text = medications.CurrentMedications.CustomNarrativeCurrentMedications ?? narrativeGenerator.CreateNarrative(medications.CurrentMedications);

              medicationComponents.Add(currentMedicationsComponent);
            }

            // Create Ceased Medications
            if (medications.CeasedMedications != null)
            {
              var ceasedMedicationsComponent = new POCD_MT000040Component5
              {
                section = CreateSectionCodeTitle("101.16146.4.1.2", CodingSystem.NCTIS, "Ceased Medications")
              };

              var entries = new List<POCD_MT000040Entry>();

              // Ceased Therapeutic Good 
              if (medications.CeasedMedications.ExclusionStatement != null)
              {
                entries.Add(CreateExclusionStatement(medications.CeasedMedications.ExclusionStatement, "103.16302.4.3.3"));
              }

              if (medications.CeasedMedications.TherapeuticGoods != null)
              {
                entries.AddRange(CreateTherapeuticGoods(medications.CeasedMedications.TherapeuticGoods));
              }

              ceasedMedicationsComponent.section.entry = entries.ToArray();

              ceasedMedicationsComponent.section.text = medications.CeasedMedications.CustomNarrativeCeasedMedications ?? narrativeGenerator.CreateNarrative(medications.CeasedMedications);

              medicationComponents.Add(ceasedMedicationsComponent);
            }

            component.section.component = medicationComponents.ToArray();
          }
          return component;
        }

        /// <summary>
        /// Creates a HealthProfile component
        /// </summary>
        /// <param name="healthProfile">HealthProfile</param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(HealthProfile healthProfile, INarrativeGenerator narrativeGenerator)
        {
            // For DS
          POCD_MT000040Component3 component = null;

          if (healthProfile != null)
          {
            var healthProfileComponents = new List<POCD_MT000040Component5>();

            component = new POCD_MT000040Component3
            {
              section = CreateSectionCodeTitle("101.16011", CodingSystem.NCTIS, "Health Profile", "This section contains the following subsections: Adverse Reactions and Alerts.")
            };

            if (healthProfile.CustomNarrativeHealthProfile != null) component.section.text = healthProfile.CustomNarrativeHealthProfile;

            // Create AdverseReactions
            if (healthProfile.AdverseReactions != null)
            {
              var entries = new List<POCD_MT000040Entry>();

              var healthProfileComponent = new POCD_MT000040Component5
              {
                section = CreateSectionCodeTitle("101.20113", CodingSystem.NCTIS, "Adverse Reactions")
              };

              // Current AdverseReactions Exclusions
              if (healthProfile.AdverseReactions.ExclusionStatement != null)
              {
                entries.Add(CreateExclusionStatement(healthProfile.AdverseReactions.ExclusionStatement, "103.16302.4.3.4"));
              }

              // Current Adverse Reactions
              if (healthProfile.AdverseReactions.Reactions != null && healthProfile.AdverseReactions.Reactions.Any())
              {
                entries.AddRange(CreateProblemDiagnosisEntries(healthProfile.AdverseReactions.Reactions));
              }

              // Set the entries
              healthProfileComponent.section.entry = entries.ToArray();

              // Create the narrative 
              healthProfileComponent.section.text = healthProfile.AdverseReactions.CustomNarrativeAdverseReactions ?? narrativeGenerator.CreateNarrative(healthProfile.AdverseReactions);

              healthProfileComponents.Add(healthProfileComponent);
            }

            // Current Alerts
            if (healthProfile.Alerts != null && healthProfile.Alerts.AlertList != null && healthProfile.Alerts.AlertList.Any())
            {
              var healthProfileComponent = new POCD_MT000040Component5
              {
                section = CreateSectionCodeTitle("101.20021", CodingSystem.NCTIS, "Alerts")
              };

              // Create Narrative for Current Adverse Reactions
              healthProfileComponent.section.text = healthProfile.Alerts.CustomNarrativeAlerts ?? narrativeGenerator.CreateNarrative(healthProfile.Alerts.AlertList);

              healthProfileComponent.section.entry = CreateAlerts(healthProfile.Alerts.AlertList).ToArray();
              healthProfileComponents.Add(healthProfileComponent);
            }

            component.section.component = healthProfileComponents.ToArray();
          }
          return component;
        }

        /// <summary>
        /// Creates a Plan component
        /// </summary>
        /// <param name="plan">Plan</param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(Plan plan, INarrativeGenerator narrativeGenerator)
        {
          POCD_MT000040Component3 component = null;

          if (plan != null)
          {
            var planComponents = new List<POCD_MT000040Component5>();

            component = new POCD_MT000040Component3
            {
              section = CreateSectionCodeTitle("101.16020", CodingSystem.NCTIS, "Plan", "This section contains the following subsections: Follow-up Appointments and Record Of Recommendations And Information Provided.")
            };

            if (plan.CustomNarrativePlan != null)
            {
              component.section.text = plan.CustomNarrativePlan;
            }

            if (plan.ArrangedServices != null && plan.ArrangedServices.Any())
            {
              var planComponent = new POCD_MT000040Component5
              {
                section = CreateSectionCodeTitle("101.16021", CodingSystem.NCTIS, "Arranged Services")
              };

              var entries = new List<POCD_MT000040Entry>();
              foreach (var arrangedService in plan.ArrangedServices)
              {

                var documentActMood =
                   (x_DocumentActMood)Enum.Parse
                   (
                       typeof(x_DocumentActMood),
                       arrangedService.Status != EventTypes.Undefined ? arrangedService.Status.GetAttributeValue<NameAttribute, string>(x => x.Code) : String.Empty
                   );

                var entry = CreateEntryActEvent
                     (
                         x_ActRelationshipEntry.DRIV,
                         x_ActClassDocumentEntryAct.ACT,
                         documentActMood,
                         CreateConceptDescriptor
                             (
                                arrangedService.ArrangedServiceDescription
                             ),
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null
                     );

                entry.act.effectiveTime = CreateIntervalTimestamp(arrangedService.ServiceCommencementWindow);

                // Setup Service Provider
                if (arrangedService.ServiceProvider != null)
                {
                  entry.act.performer = new[] 
                            { 
                                CreatePerformer(arrangedService.ServiceProvider) 
                            };

                  if (arrangedService.ServiceProvider.Participant != null &&
                       arrangedService.ServiceProvider.Participant.Person != null &&
                       arrangedService.ServiceProvider.Participant.Person.Entitlements != null)
                  {
                      component.section.coverage2 = CreateEntitlements(arrangedService.ServiceProvider.Participant.Person.Entitlements,
                                                                       arrangedService.ServiceProvider.Participant.UniqueIdentifier.ToString(),
                                                                       RoleClass.ASSIGNED,
                                                                       ParticipationType.HLD).ToArray();
                  }
                }
                entries.Add(entry);
              }

              // Create Narrative for ArrangedServices
              planComponent.section.text = plan.CustomNarrativeArrangedServices ?? narrativeGenerator.CreateNarrative(plan.ArrangedServices);

              planComponent.section.entry = entries.ToArray();
              planComponents.Add(planComponent);
            }

            // Create RecommendationsInformationProvided
            if (plan.RecommendationsInformationProvided != null)
            {
              var planComponent = new POCD_MT000040Component5
              {
                section = CreateSectionCodeTitle("101.20016", CodingSystem.NCTIS, "Record of Recommendations and Information Provided")
              };

              var entries = new List<POCD_MT000040Entry>();
              if (plan.RecommendationsInformationProvided.RecommendationsProvided != null)
                foreach (var recommendationProvided in plan.RecommendationsInformationProvided.RecommendationsProvided)
                {
                  if (recommendationProvided != null)
                  {
                    var entry = CreateEntryActEvent
                        (
                            x_ActRelationshipEntry.DRIV,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.PRP,
                            CreateConceptDescriptor
                                (
                                    "102.20016.4.1.1",
                                    CodingSystem.NCTIS,
                                    "Recommendations Provided",
                                    null
                                ),
                            recommendationProvided.RecommendationNote,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null
                        );

                    // Create Unique Id for the Act
                    entry.act.id[0].root = CreateGuid();

                    // Setup Recommendation Recipient
                    if (recommendationProvided.RecommendationRecipient != null)
                    {
                      entry.act.performer = new[]
                      {
                          CreateParticipation(recommendationProvided.RecommendationRecipient)
                      };
                    }
                    entries.Add(entry);
                  }
                }

              // Create Information Provided
              if (plan.RecommendationsInformationProvided.InformationProvided != null)
              {
                if (!plan.RecommendationsInformationProvided.InformationProvided.InformationProvidedToRelevantParties.IsNullOrEmptyWhitespace())
                {
                  var entry = CreateEntryActEvent
                     (
                         x_ActRelationshipEntry.DRIV,
                         x_ActClassDocumentEntryAct.INFRM,
                         x_DocumentActMood.EVN,
                         CreateConceptDescriptor
                             (
                                "102.20016.4.1.2",
                                CodingSystem.NCTIS,
                                "Information Provided",
                                null
                             ),
                         plan.RecommendationsInformationProvided.InformationProvided.InformationProvidedToRelevantParties,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null
                     );

                  // Create Unique Id for the Act
                  entry.act.id[0].root = CreateGuid();

                  entries.Add(entry);
                }
              }

              // Create Narrative for ArrangedServices
              planComponent.section.text = plan.CustomNarrativeRecommendationsInformationProvided ?? narrativeGenerator.CreateNarrative(plan.RecommendationsInformationProvided);

              planComponent.section.entry = entries.ToArray();
              planComponents.Add(planComponent);
            }
            component.section.component = planComponents.ToArray();
          }
          return component;
        }

        /// <summary>
        /// Creates a Diagnostic Investigations component
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigationsDischargeSummary</param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateComponent(IDiagnosticInvestigationsDischargeSummary diagnosticInvestigations, INarrativeGenerator narrativeGenerator)
        {
          POCD_MT000040Component5 component = null;

          var componentList = new List<POCD_MT000040Component5>();

          if (diagnosticInvestigations != null)
          {
            component = new POCD_MT000040Component5
            {
              section = CreateSectionCodeTitle("101.20117", CodingSystem.NCTIS, "Diagnostic Investigations", "")
            };

            if (diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations != null)
            {
              component.section.text = diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations;
            }

            //PATHOLOGY TEST RESULTS
            if (diagnosticInvestigations.PathologyTestResult != null)
            {
              foreach (var pathologyTestResult in diagnosticInvestigations.PathologyTestResult)
              {
                var relationshipList = new List<POCD_MT000040EntryRelationship>();
                var entryList = new List<POCD_MT000040Entry>();

                // Narrative
                //Create the Pathology Test Result Component and section
                var pathologyTestResultComponent = new POCD_MT000040Component5
                {
                    section = CreateSectionCodeTitle("102.16144", CodingSystem.NCTIS, "Pathology Test Result", "")
                };

                //Create relationships covering the test Specimen details
                if (pathologyTestResult.TestSpecimenDetail != null)
                {
                  relationshipList.AddRange
                      (
                          CreateRelationshipForEachSpecimenDetail(pathologyTestResult.TestSpecimenDetail, CDADocumentType.DischargeSummary)
                      );
                }

                //Create relationships covering the test result groups
                if (pathologyTestResult.ResultGroup != null)
                {
                  relationshipList.AddRange(CreateRelationshipForEachTestResultGroup(pathologyTestResult.ResultGroup, CDADocumentType.DischargeSummary));
                }

                //Create relationships covering the diagnostic Service
                if (pathologyTestResult.DiagnosticService != null)
                {
                  relationshipList.Add
                      (
                          CreateEntryRelationshipObservation
                          (
                                  x_ActRelationshipEntryRelationship.COMP,
                                  CreateConceptDescriptor
                                     (
                                         "310074003",
                                         CodingSystem.SNOMED,
                                         "pathology service",
                                         null,
                                         Constants.SnomedVersion20110531,
                                         null
                                     ),

                                  CreateConceptDescriptor
                                  (
                                      pathologyTestResult.DiagnosticService.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                      CodingSystem.HL7DiagnosticServiceSectionID,
                                      pathologyTestResult.DiagnosticService.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                      null
                                  )
                              )
                      );
                }

                //Create a relationship containing the status of the pathology test
                if (pathologyTestResult.OverallTestResultStatus != null)
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForTestResultStatus
                              (
                                  pathologyTestResult.OverallTestResultStatus,
                                  null
                              )
                      );
                }

                //Create a relationship containing the provided clinical information
                relationshipList.Add(CreateRelationshipForProvidedClinicalInformation(pathologyTestResult.ClinicalInformationProvided));

                //pathological diagnosis
                if (pathologyTestResult.PathologicalDiagnosis != null && pathologyTestResult.PathologicalDiagnosis.Any())
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForTestResultPathologicalDiagnosis(pathologyTestResult.PathologicalDiagnosis)
                      );
                }

                //Test conclusion
                if (!pathologyTestResult.Conclusion.IsNullOrEmptyWhitespace())
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForTestResultConclusion(pathologyTestResult.Conclusion, CDADocumentType.DischargeSummary)
                      );
                }

                //test comment
                if (!pathologyTestResult.TestComment.IsNullOrEmptyWhitespace())
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForTestResultComment
                              (
                                  pathologyTestResult.TestComment
                              )
                      );
                }

                //Test request details; includes the requested test name(s) and identifier(s)
                if (pathologyTestResult.TestRequestDetails != null && pathologyTestResult.TestRequestDetails.Any())
                {
                  foreach (var testRequest in pathologyTestResult.TestRequestDetails)
                  {
                    relationshipList.Add
                    (
                        CreateRelationshipForTestRequestDetails
                            (
                                testRequest
                            )
                    );
                  }
                }

                //pathology test result date time
                if (pathologyTestResult.ObservationDateTime != null)
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForTestResultDateTime
                              (
                                  pathologyTestResult.ObservationDateTime
                              )
                      );
                }

                //Create the observation entry with all the above relationships nested inside the observation
                entryList.Add
                    (
                        CreateEntryObservation
                        (
                            x_ActRelationshipEntry.COMP,
                            CreateConceptDescriptor
                                        (
                                            pathologyTestResult.TestResultName
                                        ),
                            null,
                            new List<ANY>
                                    {   
                                        pathologyTestResult.TestResultRepresentation != null && pathologyTestResult.TestResultRepresentation.ExternalData != null ? 
                                        CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation.ExternalData) : 
                                        (pathologyTestResult.TestResultRepresentation != null && !pathologyTestResult.TestResultRepresentation.Text.IsNullOrEmptyWhitespace() ? 
                                        CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation.Text) : null)
                                    },
                            null,
                            relationshipList
                        )
                );

                pathologyTestResultComponent.section.entry = entryList.ToArray();

                componentList.Add(pathologyTestResultComponent);

                pathologyTestResultComponent.section.text = pathologyTestResult.CustomNarrativePathologyTestResult ?? narrativeGenerator.CreateNarrative(pathologyTestResult);

              }
            }

            //IMAGING EXAMINATION RESULTS
            if (diagnosticInvestigations.ImagingExaminationResult != null && diagnosticInvestigations.ImagingExaminationResult.Any())
            {
              foreach (var imagingExaminationResult in diagnosticInvestigations.ImagingExaminationResult)
              {
                var relationshipList = new List<POCD_MT000040EntryRelationship>();
                var entryList = new List<POCD_MT000040Entry>();

                //Create the Imaging Examination Result Component and section
                var imagingExaminationResultComponent = new POCD_MT000040Component5
                {
                  section = CreateSectionCodeTitle("102.16145", CodingSystem.NCTIS, "Imaging Examination Result")
                };

                //Anatomical location image
                if (imagingExaminationResult.AnatomicalSite != null && imagingExaminationResult.AnatomicalSite.Any())
                {

                  foreach (var anatomicalSite in imagingExaminationResult.AnatomicalSite)
                  {
                    if (anatomicalSite.Images != null && anatomicalSite.Images.Any())
                    {
                      relationshipList.AddRange
                          (
                              CreateRelationshipsForEachImage(anatomicalSite.Images)
                          );
                    }
                  }
                }

                //Imaging examination result status
                if (imagingExaminationResult.ExaminationResultStatus != null)
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForTestResultStatus(imagingExaminationResult.ExaminationResultStatus, null)
                      );
                }

                //Clinical information provided
                if (!imagingExaminationResult.ClinicalInformationProvided.IsNullOrEmptyWhitespace())
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForProvidedClinicalInformation(imagingExaminationResult.ClinicalInformationProvided)
                      );
                }

                //Findings
                if (!imagingExaminationResult.Findings.IsNullOrEmptyWhitespace())
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForFindings(imagingExaminationResult.Findings)
                      );
                }

                //Examination result group
                if (imagingExaminationResult.ResultGroup != null && imagingExaminationResult.ResultGroup.Any())
                {
                  relationshipList.AddRange
                      (
                          CreateRelationshipForEachImagingResultGroup(imagingExaminationResult.ResultGroup)
                      );
                }

                //Examination result date / time 
                if (imagingExaminationResult.ResultDateTime != null)
                {
                  relationshipList.Add
                      (
                          CreateRelationshipForDateTime(imagingExaminationResult.ResultDateTime)
                      );
                }

                //Examination request details
                if (imagingExaminationResult.ExaminationRequestDetails != null)
                {
                  relationshipList.AddRange
                      (
                          CreateRelationshipForExaminationRequest(imagingExaminationResult.ExaminationRequestDetails)
                      );
                }

                //Create the observation entry with all the above relationships nested inside the observation
                entryList.Add
                    (
                        CreateEntryObservation
                        (
                            x_ActRelationshipEntry.COMP,
                            CreateConceptDescriptor
                                        (
                                            imagingExaminationResult.ExaminationResultName
                                        ),
                        new[] 
                                { 
                                        CreateCodedWithExtensionElement
                                        (
                                            imagingExaminationResult.Modality
                                        )
                                },
                        imagingExaminationResult.AnatomicalSite == null ? null : CreateConceptDescriptorsForAnatomicalSites(imagingExaminationResult.AnatomicalSite).ToArray(),
                        null,
                        null,
                        null,
                        relationshipList,
                        imagingExaminationResult.ExaminationResultRepresentation,
                        null,
                        null,
                        null,
                        null,
                        null
                        )
                );

                imagingExaminationResultComponent.section.entry = entryList.ToArray();
                imagingExaminationResultComponent.section.text = imagingExaminationResult.CustomNarrativeImagingExaminationResult ?? narrativeGenerator.CreateNarrative(imagingExaminationResult);
                componentList.Add(imagingExaminationResultComponent);
              }
            }

            // OTHER TEST RESULT 
            if (diagnosticInvestigations.OtherTestResult != null && diagnosticInvestigations.OtherTestResult.Any())
            {
              componentList.AddRange(diagnosticInvestigations.OtherTestResult.Select(otherTestResult => CreateOtherTestResult(otherTestResult, narrativeGenerator)));
            }

            component.section.component = componentList.ToArray();
          }

          return component;
        }

        #endregion

        #region internal Methods - Location

        /// <summary>
        /// Details pertaining to the identification of a Health Care Organisation/Facility which is involved in or associated
        /// with the delivery of the health care services to the patient, or caring for his/her wellbeing.
        /// </summary>
        /// <param name="facilityParticipation">A Facility Participation</param>
        /// <returns>POCD_MT000040Location</returns>
        internal static POCD_MT000040Location CreateLocation(IParticipationFacility facilityParticipation)
        {
          var location = new POCD_MT000040Location();
          location.typeCode = ParticipationTargetLocation.LOC;
          location.healthCareFacility = new POCD_MT000040HealthCareFacility();
          location.healthCareFacility.id = CreateIdentifierArray(CreateGuid());

          if (facilityParticipation.Role != null)
          {
            location.healthCareFacility.code = CreateCodedWithExtensionElement(facilityParticipation.Role);
            location.typeCode = ParticipationTargetLocation.LOC;
          }

          if (facilityParticipation.Participant != null && facilityParticipation.Participant.Organisation != null)
          {
            location.healthCareFacility.serviceProviderOrganization = CreateOrganisation(facilityParticipation.Participant.Organisation);
            location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.id = CreateIdentifierArray(CreateGuid());

            // Department
            location.healthCareFacility.serviceProviderOrganization.name = facilityParticipation.Participant.Organisation.Department.IsNullOrEmptyWhitespace() ? null : new ON[] {
                    CreateOrganisationName( facilityParticipation.Participant.Organisation.Department ) 
                };

            if (facilityParticipation.Participant.Addresses != null)
            {
              location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization.addr =
              CreateAddressArray
              (
                  facilityParticipation.Participant.Addresses
              );
            }

            // Add Electronic Communication Details
            if (facilityParticipation.Participant.ElectronicCommunicationDetails != null &&
                facilityParticipation.Participant.ElectronicCommunicationDetails.Any())
            {
              location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization.telecom =
              CreateTelecomunicationArray(facilityParticipation.Participant.ElectronicCommunicationDetails);
            }
          }

          return location;
        }

        #endregion

        #region internal Methods - Entry

        /// <summary>
        /// The clinical specialty under which the patient was treated during the encounter.
        /// </summary>
        /// <param name="specialty">A list of Specialty's</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateSpecialtyEntry(List<ICodableText> specialty)
        {
          POCD_MT000040Entry entry = null;
          if (specialty != null && specialty.Any())
          {
            entry = new POCD_MT000040Entry();

            // <!-- Begin Encounter - Specialty (Administrative Observations) -->
            entry =
                CreateEntryObservation
                    (
                    x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor
                        (
                            "103.16028",
                            CodingSystem.NCTIS,
                            "Specialty",
                            null
                        ),
                    null,
                    CreateConceptDescriptorsForCoadableText(specialty),
                    null,
                    null
                    );

          }
          return entry;
        }

        /// <summary>
        /// Create a List of Entries for IAdverseReactionDischargeSummary
        /// </summary>
        /// <param name="adverseReactionDischargeSummaries">A list of IAdverseReactionDischargeSummary</param>
        /// <returns>A list of POCD_MT000040Entry</returns>
        private static List<POCD_MT000040Entry> CreateProblemDiagnosisEntries(ICollection<IAdverseReactionDischargeSummary> adverseReactionDischargeSummaries)
        {
          var entries = new List<POCD_MT000040Entry>();

          foreach (var adverseReactionDischargeSummary in adverseReactionDischargeSummaries)
          {
            var entry = CreateEntryObservation
                (
                    x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor
                    (
                            "102.15517",
                            CodingSystem.NCTIS,
                            "Adverse Reaction",
                            null
                    ),
                    null,
                    CreateConceptDescriptorsForCoadableText(adverseReactionDischargeSummary.AdverseReactionType),
                    null,
                    null
                 );

            entry.observation.participant = new POCD_MT000040Participant2[] 
                 {
                    CreateParticipant2
                    ( 
                      CreateCodedWithExtensionElement(adverseReactionDischargeSummary.AgentDescription),
                      EntityDeterminer.INSTANCE
                    ) 
                 };

            // Create Create Entry Relationship Observation
            var relationshipList = new List<POCD_MT000040EntryRelationship>();
            if (adverseReactionDischargeSummary.ReactionDescriptions != null)
            {
              foreach (var reaction in adverseReactionDischargeSummary.ReactionDescriptions)
              {
                var entryRelationshipList = CreateEntryRelationshipObservation
                    (
                        x_ActRelationshipEntryRelationship.MFST,
                        ActClassObservation.OBS,
                        x_ActMoodDocumentObservation.EVN,
                        true,
                        null,
                        CreateConceptDescriptor
                            (
                              reaction
                            ),
                        null,
                        null,
                        null,
                        null
                    );
                entryRelationshipList.observation.id = null;
                relationshipList.Add(entryRelationshipList);
              }
            }

            entry.observation.entryRelationship = relationshipList.ToArray();
            entries.Add(entry);
          }

          return entries;
        }

        /// <summary>
        /// Create a list of entries for a list of allerts 
        /// </summary>
        /// <param name="alerts">A list of alerts</param>
        /// <returns>A list of POCD_MT000040Entry</returns>
        private static List<POCD_MT000040Entry> CreateAlerts(List<Alert> alerts)
        {
          var entries = new List<POCD_MT000040Entry>();

          foreach (var alert in alerts)
          {
            var entry = CreateEntryObservation
                (
                    x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor
                    (
                         alert.AlertType
                    ),
                    null,
                    CreateConceptDescriptorsForCoadableText(alert.AlertDescription),
                    null,
                    null
                 );
            entries.Add(entry);
          }
          return entries;
        }


        /// <summary>
        /// Create an entry for ITherapeuticGoodCeased
        /// </summary>
        /// <param name="therapeuticGood">ITherapeuticGoodCeased</param>
        /// <returns>POCD_MT000040Entry</returns>
        private static POCD_MT000040Entry CreateTherapeuticGood(ITherapeuticGoodCeased therapeuticGood)
        {
          var relationships = new List<POCD_MT000040EntryRelationship>();

          // Item status
          if (therapeuticGood.MedicationHistory != null && therapeuticGood.MedicationHistory.ItemStatus != null)
          {
            relationships.Add
            (
                CreateEntryRelationshipObservation
                (
                    x_ActRelationshipEntryRelationship.SUBJ,
                    true,
                    null,
                    CreateConceptDescriptor
                    (
                        therapeuticGood.MedicationHistory.ItemStatus
                    )
                  )
            );
          }

          // Changes Made
          if (therapeuticGood.MedicationHistory != null && therapeuticGood.MedicationHistory.ChangesMade != null)
          {
            var entryRelationshipObservation = CreateEntryRelationshipObservation
                (
                    x_ActRelationshipEntryRelationship.SPRT,
                    false,
                    null,
                    CreateConceptDescriptor
                    (
                        therapeuticGood.MedicationHistory.ChangesMade
                    ),
                    CreateIdentifierArray(therapeuticGood.MedicationHistory.ChangeDetail)
                );

            // Reason For Change
            entryRelationshipObservation.observation.entryRelationship = new[]
                {
                     CreateEntryRelationshipACT(
                         x_ActRelationshipEntryRelationship.RSON,
                         x_ActClassDocumentEntryAct.INFRM,
                         x_DocumentActMood.EVN, false,
                         CreateConceptDescriptor(
                             "103.10177",
                             CodingSystem.NCTIS,
                             "Reason for Change",
                             null),
                         CreateStructuredText(therapeuticGood.MedicationHistory.ReasonForChange, null),
                         CreateIdentifierArray(new UniqueId()))
                };

            relationships.Add(entryRelationshipObservation);
          }

          // Create Entry Substance Administration Event
          var entrySubstanceAdministrationEvent = CreateEntrySubstanceAdministrationEvent
              (
                  x_ActRelationshipEntry.DRIV,
                  x_DocumentSubstanceMood.EVN,
                  true,
                  null,
                  null,
                  null,
                  null,
                  CreateCodedWithExtensionElement
                      (
                          therapeuticGood.TherapeuticGoodIdentification
                      ),
                  null,
                  relationships,
                  "cancelled",
                  null
              );
          return entrySubstanceAdministrationEvent;
        }

        /// <summary>
        /// Create a entry for CreateTherapeuticGood
        /// </summary>
        /// <param name="therapeuticGood">ITherapeuticGood</param>
        /// <returns>POCD_MT000040Entry</returns>
        private static POCD_MT000040Entry CreateTherapeuticGood(ITherapeuticGood therapeuticGood)
        {
          var relationships = new List<POCD_MT000040EntryRelationship>();

          // Item status
          if (therapeuticGood.MedicationHistory != null && therapeuticGood.MedicationHistory.ItemStatus != null)
          {
            relationships.Add
            (
                CreateEntryRelationshipObservation
                (
                    x_ActRelationshipEntryRelationship.SUBJ,
                    true,
                    null,
                    CreateConceptDescriptor
                    (
                        therapeuticGood.MedicationHistory.ItemStatus
                    )
                  )
            );
          }

          // Reason for Therapeutic Good
          if (therapeuticGood.ReasonForTherapeuticGood != null)
          {
            relationships.Add
            (
               CreateEntryRelationshipACT
                (
                    x_ActRelationshipEntryRelationship.RSON,
                    x_ActClassDocumentEntryAct.INFRM,
                    x_DocumentActMood.RQO,
                    false,
                    CreateConceptDescriptor
                        (
                            "103.10141",
                            CodingSystem.NCTIS,
                            "Reason for Therapeutic Good",
                            null
                        ),
                    CreateStructuredText
                        (
                            therapeuticGood.ReasonForTherapeuticGood,
                            null
                        ),
                CreateIdentifierArray(new UniqueId())
                )
              );
          }

          // Unit of Use Quantity Dispensed
          if (!therapeuticGood.UnitOfUseQuantityDispensed.IsNullOrEmptyWhitespace())
          {
            relationships.Add
            (
                CreateEntryRelationshipSupply
                (
                    ActClassSupply.SPLY,
                    x_ActRelationshipEntryRelationship.REFR,
                    x_DocumentSubstanceMood.EVN,
                    false,
                    null,
                    CreateStructuredText(therapeuticGood.UnitOfUseQuantityDispensed, null),
                    null,
                    null,
                    null,
                    null
                )
            );
          }

          // Item status
          if (therapeuticGood.MedicationHistory != null && therapeuticGood.MedicationHistory.ChangesMade != null)
          {
            var entryRelationshipObservation = CreateEntryRelationshipObservation
                (
                    x_ActRelationshipEntryRelationship.SPRT,
                    false,
                    null,
                    CreateConceptDescriptor
                    (
                        therapeuticGood.MedicationHistory.ChangesMade
                    ),
                    CreateIdentifierArray(therapeuticGood.MedicationHistory.ChangeDetail)
                );


            // Reason For Change
            if (therapeuticGood.MedicationHistory.ReasonForChange != null)
            {
              entryRelationshipObservation.observation.entryRelationship = new[]
                    {
                         CreateEntryRelationshipACT(
                             x_ActRelationshipEntryRelationship.RSON,
                             x_ActClassDocumentEntryAct.INFRM,
                             x_DocumentActMood.EVN, false,
                             CreateConceptDescriptor(
                                 "103.10177",
                                 CodingSystem.NCTIS, 
                                 "Reason for Change",
                                 null),
                             CreateStructuredText(therapeuticGood.MedicationHistory.ReasonForChange, null),
                             CreateIdentifierArray(new UniqueId()))
                    };
            }

            relationships.Add(entryRelationshipObservation);
          }

          // Additional comments
          if (!therapeuticGood.AdditionalComments.IsNullOrEmptyWhitespace())
          {
            relationships.Add(

                               CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.INFRM,
                                    x_DocumentActMood.EVN,
                                    false,
                                    CreateConceptDescriptor
                                        (
                                            "103.16044",
                                            CodingSystem.NCTIS,
                                            "Additional Comments",
                                            null
                                        ),
                                    CreateStructuredText(therapeuticGood.AdditionalComments, null),
                                CreateIdentifierArray(new UniqueId())
                                )
                               );
          }

          // Create Entry Substance Administration Event
          var entrySubstanceAdministrationEvent = CreateEntrySubstanceAdministrationEvent
              (
                  x_ActRelationshipEntry.DRIV,
                  x_DocumentSubstanceMood.EVN,
                  true,
                  CreateStructuredText(therapeuticGood.DoseInstruction, null),
                  therapeuticGood.MedicationHistory.MedicationDuration,
                  null,
                  CreateCodedWithExtensionElement
                      (
                          therapeuticGood.TherapeuticGoodIdentification
                      ),
                  null,
                  relationships,
                  null
              );

          return entrySubstanceAdministrationEvent;
        }


        /// <summary>
        /// Create a list of Entry's for ITherapeuticGood
        /// </summary>
        /// <param name="therapeuticGoods">A list of ITherapeuticGood</param>
        /// <returns>A list of entries</returns>
        private static List<POCD_MT000040Entry> CreateTherapeuticGoods(List<ITherapeuticGood> therapeuticGoods)
        {
          if (therapeuticGoods == null)
            return null;

          var entryList = new List<POCD_MT000040Entry>();

          // Current Therapeutic Good 
          foreach (var currentTherapeuticGood in therapeuticGoods)
          {
            entryList.Add(CreateTherapeuticGood(currentTherapeuticGood));
          }

          return entryList;
        }

        /// <summary>
        /// Create a list of entries for ITherapeuticGoodCeased
        /// </summary>
        /// <param name="therapeuticGoods">A list of ITherapeuticGoodCeased</param>
        /// <returns>A list of entries</returns>
        private static List<POCD_MT000040Entry> CreateTherapeuticGoods(List<ITherapeuticGoodCeased> therapeuticGoods)
        {
          var entryList = new List<POCD_MT000040Entry>();

          // Current Therapeutic Good 
          foreach (var ceasedTherapeuticGood in therapeuticGoods)
          {
            entryList.Add(CreateTherapeuticGood(ceasedTherapeuticGood));
          }

          return entryList;
        }

        private static IEnumerable<POCD_MT000040Entry> CreateProcedureEntries(ICollection<Procedure> procedureList)
        {
          var entryList = new List<POCD_MT000040Entry>();

          if (procedureList != null && procedureList.Any())
          {
            foreach (var procedure in procedureList)
            {
              var relationshipList = new List<POCD_MT000040EntryRelationship>
                                               {
                                                   String.IsNullOrEmpty(procedure.Comment) ? null
                                                       : CreateEntryRelationshipACT(
                                                           x_ActRelationshipEntryRelationship.COMP,
                                                           x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN,
                                                           false,
                                                           CreateConceptDescriptor("103.15595", CodingSystem.NCTIS, "Procedure Comment", null),
                                                           CreateStructuredText(procedure.Comment, null), null)
                                               };

              //Procedure code and start date.
              entryList.Add(CreateEntryProcedureEvent(x_ActRelationshipEntry.COMP,
                                                      CreateConceptDescriptor(procedure.ProcedureName),
                                                      procedure.ProcedureDateTime, relationshipList));
            }
          }

          return entryList;
        }

        private static POCD_MT000040Entry CreateMedicalHistoryReviewEntry(DateTime? reviewDateTime)
        {
          var entry = new POCD_MT000040Entry();

          if (reviewDateTime.HasValue)
          {
            entry = CreateEntryActEvent(x_ActRelationshipEntry.COMP, x_ActClassDocumentEntryAct.ACT,
                                        x_DocumentActMood.EVN,
                                        CreateConceptDescriptor("102.16576.120.1.3", CodingSystem.NCTIS,
                                                                "Medical History Review", null),
                                        reviewDateTime.Value.ToString(DATE_TIME_SHORT_FORMAT));
          }

          return entry;
        }

        private static POCD_MT000040Entry CreateExclusionStatement(string exclusionStatement, string exclusionStatementCode, string displayName)
        {
          var entry = new POCD_MT000040Entry();

          if (exclusionStatement != null)
          {
            entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                           CreateConceptDescriptor(exclusionStatementCode, CodingSystem.NCTIS, displayName, null), null,
                                           new List<ANY> { CreateStructuredText(exclusionStatement, null) }, null,
                                           null);
          }
          return entry;
        }

        private static POCD_MT000040Entry CreateExclusionStatementStructuredText<T>(T defaultValue, string exclusionStatement) where T : struct, IConvertible
        {
          if (!typeof(T).IsEnum)
          {
            throw new ArgumentException("T must be an enumerated type which can be mapped to a CodingSystem");
          }

          var enumeration = defaultValue as Enum;

          ICodableText exclusionStatementCodableText = new CodableText
          {
            DisplayName = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Name),
            Code = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Code),
            CodeSystemCode = ((CodingSystem)Enum.Parse(typeof(CodingSystem), enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem))).GetAttributeValue<NameAttribute, string>(x => x.Code),
            CodeSystemName = ((CodingSystem)Enum.Parse(typeof(CodingSystem), enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem))).GetAttributeValue<NameAttribute, string>(x => x.Name),
            CodeSystemVersion = ((CodingSystem)Enum.Parse(typeof(CodingSystem), enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem))).GetAttributeValue<NameAttribute, string>(x => x.Version),
            OriginalText = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Title)
          };

          var entry = new POCD_MT000040Entry();

          if (exclusionStatement != null)
          {
            entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                           CreateConceptDescriptor(exclusionStatementCodableText), null,
                                           new List<ANY> { CreateStructuredText(exclusionStatement, null) }, null,
                                           null);
          }
          return entry;
        }

        private static POCD_MT000040Entry CreateExclusionStatement(Statement exclusionStatement, string exclusionStatementCode)
        {
          var entry = new POCD_MT000040Entry();

          if (exclusionStatement != null)
          {
            entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                           CreateConceptDescriptor(exclusionStatementCode, CodingSystem.NCTIS, "Global Statement", null), null,
                                           new List<ANY>
                                                   {
                                                       CreateConceptDescriptor(exclusionStatement.Value != null ? exclusionStatement.Value.GetAttributeValue<NameAttribute, string>(x => x.Code) : String.Empty, CodingSystem.NCTISGlobalStatementValues,
                                                       exclusionStatement.Value != null ? exclusionStatement.Value.GetAttributeValue<NameAttribute, string>(x => x.Name) : String.Empty, null)
                                                   }, null, null);
          }
          return entry;
        }

        private static POCD_MT000040Entry CreateEmptyStatement(EmptyReason emptyReason)
        {
            var entry = new POCD_MT000040Entry();

            if (emptyReason != null)
            {
                entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                    CreateConceptDescriptor("ASSERTION",  CodingSystem.ActCode, "Assertion", null), null,
                    new List<ANY>
                    {
                        CreateConceptDescriptor(emptyReason.Value.GetAttributeValue<NameAttribute, string>(x => x.Code),
                            CodingSystem.HL7NonClinicalEmptyReason,
                            emptyReason.Value.GetAttributeValue<NameAttribute, string>(x => x.Name),
                            emptyReason.OriginalText, null)

                    }, null, null);
            }
            return entry;
        }

        /// <summary>
        /// Creates a list of entries for a Discharge Summary Problem Diagnosis object
        /// </summary>
        /// <param name="problemDiagnosisList">A list of  Discharge Summary Problem Diagnosis </param>
        /// <returns></returns>
        private static IEnumerable<POCD_MT000040Entry> CreateProblemDiagnosisEntries(IEnumerable<IDischargeSummaryProblemDiagnosis> problemDiagnosisList)
        {
          var entries = new List<POCD_MT000040Entry>();

          foreach (var problemDiagnosis in problemDiagnosisList)
          {
            entries.Add(CreateEntryObservation
                (
                    x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor
                    (
                      problemDiagnosis.ProblemDiagnosisType
                    ),
                    null,
                    CreateConceptDescriptorsForCoadableText(problemDiagnosis.ProblemDiagnosisDescription),
                    null,
                    null
                 )
            );
          }

          return entries;
        }


        private static void CreateProblemDiagnosisEntry(ProblemDiagnosis problemDiagnosis, ref List<POCD_MT000040Entry> entryList)
        {
          var relationshipList = new List<POCD_MT000040EntryRelationship>();

          var dateOfResolutionRemission = problemDiagnosis.DateOfResolutionRemission != null && problemDiagnosis.DateOfResolutionRemission != null ? problemDiagnosis.DateOfResolutionRemission.ToString() : String.Empty;

          //Date of resolution / Remission
          if (problemDiagnosis.DateOfResolutionRemission != null)
            relationshipList.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.SUBJ, false,
                                                                    null,
                                                                    CreateConceptDescriptor("103.15510",
                                                                                            CodingSystem.NCTIS,
                                                                                            "Date of Resolution/Remission",
                                                                                            null), null, null,
                                                                    CreateIntervalTimestamp(null, null, null, null, dateOfResolutionRemission, null), null));

          //Problem / Diagnosis comment
          if (!String.IsNullOrEmpty(problemDiagnosis.Comment))
            relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                            x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN,
                                                            false,
                                                            CreateConceptDescriptor("103.16545", CodingSystem.NCTIS,
                                                                                    "Problem/Diagnosis Comment",
                                                                                    null),
                                                            CreateStructuredText(problemDiagnosis.Comment, null),
                                                            null));

          //Date of Onset + Problem interpretation
          entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                               CreateConceptDescriptor("282291009", CodingSystem.SNOMED, "Diagnosis interpretation", null),
                                               problemDiagnosis.DateOfOnset != null ? CreateIntervalTimestamp(problemDiagnosis.DateOfOnset.ToString(),null, null, null, null, null) : null,
                                               new List<ANY>
                                                     {
                                                         CreateConceptDescriptor(problemDiagnosis.ProblemDiagnosisIdentification)
                                                     }, null,
                                               relationshipList));
        }

        private static void CreateProblemDiagnosisEntry(IProblemDiagnosis problemDiagnosis, ref List<POCD_MT000040Entry> entryList)
        {
          var relationshipList = new List<POCD_MT000040EntryRelationship>();

          var dateOfResolutionRemission = problemDiagnosis.DateOfResolutionRemission != null
                                              ? problemDiagnosis.DateOfResolutionRemission.ToString()
                                              : String.Empty;

          //Date of resolution / Remission
          if (problemDiagnosis.DateOfResolutionRemission != null)
            relationshipList.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.SUBJ, false,
                                                                    null,
                                                                    CreateConceptDescriptor("103.15510", CodingSystem.NCTIS, "Date of Resolution/Remission", null), null, null,
                                                                    CreateIntervalTimestamp(null, null, null, null, dateOfResolutionRemission, null), null));

          //Problem / Diagnosis comment
          if (!String.IsNullOrEmpty(problemDiagnosis.Comment))
            relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                            x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN,
                                                            false,
                                                            CreateConceptDescriptor("103.16545", CodingSystem.NCTIS, "Problem/Diagnosis Comment", null),
                                                            CreateStructuredText(problemDiagnosis.Comment, null),
                                                            null));

          //Date of Onset + Problem interpretation
          entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                               CreateConceptDescriptor("282291009", CodingSystem.SNOMED, "Diagnosis interpretation", null),
                                               problemDiagnosis.DateOfOnset != null ? CreateIntervalTimestamp(problemDiagnosis.DateOfOnset.ToString(), null, null, null, null, null) : null,
                                               new List<ANY>
                                                     {
                                                         CreateConceptDescriptor(
                                                             problemDiagnosis.ProblemDiagnosisIdentification)
                                                     }, null,
                                               relationshipList));
        }


        private static IEnumerable<POCD_MT000040Entry> CreateProblemDiagnosisEntries(
            ICollection<IProblemDiagnosis> problemDiagnosisList)
        {
          var entryList = new List<POCD_MT000040Entry>();

          if (problemDiagnosisList != null && problemDiagnosisList.Any())
          {
            foreach (var diagnosis in problemDiagnosisList)
            {
              CreateProblemDiagnosisEntry(diagnosis, ref entryList);
            }
          }

          return entryList;
        }

        #endregion

        #region internal Methods - ANY

        /// <summary>
        /// Create Concept Descriptors For Coadable Texts
        /// </summary>
        /// <param name="coadableTexts">A list of ICodableText's</param>
        /// <returns>A list of Any</returns>
        private static List<ANY> CreateConceptDescriptorsForCoadableText(ICollection<ICodableText> coadableTexts)
        {
          List<ANY> componentDescriptionList = null;

          if (coadableTexts != null && coadableTexts.Any())
          {
            componentDescriptionList = new List<ANY>();

            foreach (var coadableText in coadableTexts)
            {
              var cd = CreateConceptDescriptor(coadableText);
              componentDescriptionList.Add(cd);
            }
          }

          return componentDescriptionList;
        }

        /// <summary>
        /// Create Concept Descriptors For CoadableText
        /// </summary>
        /// <param name="coadableText">A ICodableText</param>
        /// <returns>A list of Any</returns>
        private static List<ANY> CreateConceptDescriptorsForCoadableText(ICodableText coadableText)
        {
          return CreateConceptDescriptorsForCoadableText(new List<ICodableText>() { coadableText });
        }

        #endregion

        #region internal Methods - Coverage

        /// <summary>
        /// Create the entitlements for the Other Participants
        /// </summary>
        /// <param name="entitlements"> A list of entitlements</param>
        /// <param name="id">The id for the entitlement</param>
        /// <returns>A list of Coverage2</returns>
        private static List<Coverage2> CreateEntitlementsOtherParticipant(ICollection<Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement> entitlements, String id)
        {
          return CreateEntitlements(entitlements, id, RoleClass.PART, ParticipationType.HLD);
       }

        #endregion

        #region internal Methods - Entry Relationship

        private static POCD_MT000040EntryRelationship CreateRelationshipForProvidedClinicalInformation(
            String providedClinicalInformation)
        {
          POCD_MT000040EntryRelationship relationship = null;

          if (!String.IsNullOrEmpty(providedClinicalInformation))
          {
            relationship = CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                      x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN, false,
                                                      CreateConceptDescriptor("55752-0", CodingSystem.LOINC, "Clinical information", null),
                                                      CreateStructuredText(providedClinicalInformation, null), null);
          }

          return relationship;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForProvidedClinicalInformation(
            String providedClinicalInformation, ref List<StrucDocTable> strucDocTableList)
        {
          POCD_MT000040EntryRelationship relationship = null;

          if (!String.IsNullOrEmpty(providedClinicalInformation))
         {
            relationship = CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                      x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN, false,
                                                      CreateConceptDescriptor("55752-0", CodingSystem.LOINC, "Clinical information", null),
                                                      CreateStructuredText(providedClinicalInformation, null), null);
          }

          return relationship;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForDateTime(ISO8601DateTime dateTime)
        {
          POCD_MT000040EntryRelationship relationship = null;

          if (dateTime != null)
          {
            relationship = new POCD_MT000040EntryRelationship
            {
              typeCode = x_ActRelationshipEntryRelationship.COMP,
              observation = CreateObservation(ActClassObservation.OBS, x_ActMoodDocumentObservation.EVN,
                                              CreateConceptDescriptor("103.16589", CodingSystem.NCTIS,
                                               "Imaging Examination Result DateTime", null), dateTime, null)
            };
          }
          return relationship;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForExaminationRequest(ICollection<IImagingExaminationRequest> request)
        {
          List<POCD_MT000040EntryRelationship> relationships = null;

          if (request != null && request.Any())
          {
            relationships = new List<POCD_MT000040EntryRelationship>();

            foreach (var imageExaminationRequest in request)
            {

              POCD_MT000040EntryRelationship relationship = null;

              //Populate the narrative for the date / time
              relationship = CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.SUBJ,
                                                        x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN,
                                                        true,
                                                        CreateConceptDescriptor("102.16511", CodingSystem.NCTIS,
                                                                                "Examination Request Details",
                                                                                null), null, null);

              //Relationship 
              var relationshipList = relationship.act.entryRelationship != null ? relationship.act.entryRelationship.ToList() : new List<POCD_MT000040EntryRelationship>();

              var studyIdentifierEntryRelationships = new List<POCD_MT000040EntryRelationship>();

              // Examination Requested Name
              if (imageExaminationRequest.ExaminationRequestedName != null && imageExaminationRequest.ExaminationRequestedName.Any())
              {
                foreach (var examinationRequestedName in imageExaminationRequest.ExaminationRequestedName)
                {
                  relationshipList.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.REFR,
                                                                           CreateConceptDescriptor("103.16512",
                                                                                                   CodingSystem.NCTIS,
                                                                                                   "Examination Requested Name",
                                                                                                   null), null,
                                                                           CreateStructuredText(examinationRequestedName, null)));
                }
              }

              //Report ID
              if (imageExaminationRequest.ReportIdentifier != null)
              {
                relationshipList.Add(CreateEntryRelationshipObservation(
                    x_ActRelationshipEntryRelationship.COMP, ActClassObservation.OBS,
                    x_ActMoodDocumentObservation.EVN,
                    CreateConceptDescriptor("103.16514", CodingSystem.NCTIS, "Report Identifier", null),
                    CreateIdentifierArray(imageExaminationRequest.ReportIdentifier)));
              }

              //Image Details
              if (imageExaminationRequest.ImageDetails != null && imageExaminationRequest.ImageDetails.Any())
              {
                foreach (var imageDetail in imageExaminationRequest.ImageDetails)
                {
                  studyIdentifierEntryRelationships.Add(
                      CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP,
                                                         ActClassObservation.OBS,
                                                         x_ActMoodDocumentObservation.EVN, false,
                                                         imageDetail.DateTime,
                                                         CreateConceptDescriptor("102.16515", CodingSystem.NCTIS, "Image Details", null), null,
                                                         CreateIdentifierArray(imageDetail.ImageIdentifier),
                                                         new List<ANY>
                                                                       {
                                                                           CreateConceptDescriptor(imageDetail.ImageViewName)
                                                                       },
                                                         new List<POCD_MT000040EntryRelationship>
                                                                       {
                                                                           imageDetail.SeriesIdentifier != null ? CreateEntryRelationshipACT(
                                                                                   x_ActRelationshipEntryRelationship.REFR,
                                                                                   x_ActClassDocumentEntryAct.ACT,
                                                                                   x_DocumentActMood.EVN, false,
                                                                                   CreateConceptDescriptor("103.16517",
                                                                                                           CodingSystem.NCTIS,
                                                                                                           "DICOM Series Identifier",
                                                                                                           null), null,
                                                                                   CreateIdentifierArray(imageDetail.SeriesIdentifier))
                                                                               : null,
                                                                           !String.IsNullOrEmpty(imageDetail.SubjectPosition) ? CreateEntryRelationshipObservation(
                                                                                   x_ActRelationshipEntryRelationship.REFR,
                                                                                   CreateConceptDescriptor("103.16519",
                                                                                                           CodingSystem.NCTIS,
                                                                                                           "Subject Position",
                                                                                                           null),
                                                                                   CreateStructuredText(imageDetail.SubjectPosition, null))
                                                                               : null,
                                                                           imageDetail.Image != null ? CreateEntryRelationshipObservationMedia(x_ActRelationshipEntryRelationship.SPRT, imageDetail.Image) : null
                                                                       }));
                }
              }

              //DICOM study ID
              if (imageExaminationRequest.StudyIdentifier != null)
              {
                relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.SUBJ,
                                                                x_ActClassDocumentEntryAct.ACT,
                                                                x_DocumentActMood.EVN, false,
                                                                CreateConceptDescriptor("103.16513",
                                                                                        CodingSystem.NCTIS,
                                                                                        "DICOM Study Identifier",
                                                                                        null), null,
                                                                CreateIdentifierArray(
                                                                    imageExaminationRequest.StudyIdentifier),
                                                                studyIdentifierEntryRelationships));
              }

              relationship.act.entryRelationship = relationshipList.ToArray();
              relationships.Add(relationship);
            }
          }
          return relationships;
        }

        /// <summary>
        /// Create a EntryRelationshipSupply for supply
        /// </summary>
        /// <param name="classCode">ActClassSupply</param>
        /// <param name="moodCode">x_DocumentSubstanceMood</param>
        /// <param name="code">CD</param>
        /// <param name="text">ST</param>
        /// <param name="effectiveTime">DateTime</param>
        /// <param name="typeCode">typeCode</param>
        /// <param name="inversionIndSpecified">inversionIndSpecified</param>
        /// <param name="sequenceNumber">sequenceNumber</param>
        /// <param name="independentInd">independentInd</param>
        /// <param name="id">ID</param>
        /// <returns>POCD_MT000040EntryRelationship</returns>
        private static POCD_MT000040EntryRelationship CreateEntryRelationshipSupply(ActClassSupply classCode, x_ActRelationshipEntryRelationship typeCode, x_DocumentSubstanceMood moodCode, Boolean inversionIndSpecified, CD code, ST text, DateTime? effectiveTime, int? sequenceNumber, Boolean? independentInd, String id)
        {
          var entryRelationship = new POCD_MT000040EntryRelationship
          {
            typeCode = typeCode,
            inversionInd = inversionIndSpecified,
            inversionIndSpecified = inversionIndSpecified,
            supply = new POCD_MT000040Supply
            {
              classCode = classCode,
              moodCode = moodCode,
              id = id.IsNullOrEmptyWhitespace() ? CreateIdentifierArray(new UniqueId()) : CreateIdentifierArray(id),
              code = code,
              text = text,
              independentInd = independentInd.HasValue ? new BL { value = false, valueSpecified = true } : null
            },
            sequenceNumber = sequenceNumber.HasValue ? CreateIntegerElement(sequenceNumber.Value, NullFlavor.NA, false) : null,
          };

          if (effectiveTime.HasValue)
          {
            entryRelationship.act.effectiveTime = CreateIntervalTimestamp(effectiveTime, null);
          }

          return entryRelationship;
        }


        #endregion 

        #region internal Methods - Qualifications

        /// <summary>
        /// Returns Qualifications 
        /// </summary>
        /// <param name="qualification">A qualification string</param>
        /// <returns>Qualifications</returns>
        internal static Qualifications CreateQualifications(String qualification)
        {
          var qualifications = new Qualifications();
          qualifications.classCode = EntityClass.QUAL;
          qualifications.code = CreateCodedWithExtensionElement(null, null, null, qualification, null, null);

          return qualifications;
        }

        #endregion
     }
}



