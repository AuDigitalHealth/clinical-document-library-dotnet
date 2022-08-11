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
using System.Globalization;
using System.Linq;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Common;
using CDA.Generator.Common;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This helper class is used to aid in converting the various SCS and CDA model objects 
     /// into an actual CDA Document.  
    /// </summary>
    public static partial class CDAGeneratorHelper
    {
        /// <summary>
        /// Creates an adverse substance reactions component
        /// </summary>
        /// <param name="allergyAndAdverseReactions">List of adverse reactions</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(List<Reaction> allergyAndAdverseReactions, StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();
            if (allergyAndAdverseReactions != null && allergyAndAdverseReactions.Any())
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.20113", CodingSystem.NCTIS, "Adverse Reactions")
                };
                component.section.title = new ST { Text = new[] { "Allergies and Adverse Reactions" } };

                foreach (var reaction in allergyAndAdverseReactions)
                {
                    var adverseReactionsRelationships = new List<POCD_MT000040EntryRelationship>();

                    if (reaction.ReactionEvent != null && reaction.ReactionEvent.Manifestations != null)
                    {
                        var manifestationRelationships = reaction.ReactionEvent.Manifestations.Select(manifestation => CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.MFST, true, CreateConceptDescriptor(manifestation))).ToList();

                        adverseReactionsRelationships.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.CAUS, CreateConceptDescriptor("102.16474", CodingSystem.NCTIS, "Reaction Event", null), manifestationRelationships));
                    }

                    if (reaction.SubstanceOrAgent != null)
                        entryList.Add(CreateEntryActEvent(x_ActRelationshipEntry.COMP,
                                                          x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN,
                                                          CreateConceptDescriptor("102.15517", CodingSystem.NCTIS, "Adverse Reaction", null),
                                                          CreateParticipant2Array(CreateCodedWithExtensionElement(reaction.SubstanceOrAgent),
                                                            null),
                                                          adverseReactionsRelationships, 
                                                          reaction.Id));
                }

                component.section.entry = entryList.ToArray();
            }

            if (component != null && component.section != null)
            {
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(allergyAndAdverseReactions);
            }

            return component;
        }

        /// <summary>
        /// Creates an adverse substance reactions component
        /// </summary>
        /// <param name="adverseSubstanceReactions">IAdverseSubstanceReactions</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <param name="exclusionStatementCode">Exclusion statement code</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(AdverseReactions adverseSubstanceReactions, INarrativeGenerator narrativeGenerator, string exclusionStatementCode)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (adverseSubstanceReactions != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.20113", CodingSystem.NCTIS, "Adverse Reactions")
                };

                // Adverse Substance Reaction
                if (adverseSubstanceReactions.AdverseSubstanceReaction != null)
                {
                    foreach (var adverserReaction in adverseSubstanceReactions.AdverseSubstanceReaction)
                    {
                        var adverseReactionsRelationships = new List<POCD_MT000040EntryRelationship>();
                        POCD_MT000040EntryRelationship entryRelationshipObservation = null;

                        if (adverserReaction.ReactionEvent != null && adverserReaction.ReactionEvent.Manifestations != null)
                        {
                            var manifestationRelationships = new List<POCD_MT000040EntryRelationship>();

                            foreach (var manifestation in adverserReaction.ReactionEvent.Manifestations)
                            {
                                manifestationRelationships.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.MFST, true, CreateConceptDescriptor(manifestation)));
                            }

                            entryRelationshipObservation = CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.CAUS, 
                                CreateConceptDescriptor("102.16474", CodingSystem.NCTIS, "Reaction Event", null), 
                                manifestationRelationships);
                        }

                        if (adverserReaction.ReactionEvent != null && adverserReaction.ReactionEvent.ReactionType != null)
                        {
                            if (entryRelationshipObservation != null)
                            {
                                entryRelationshipObservation.observation.value = new List<ANY>
                                {
                                    CreateConceptDescriptor(adverserReaction.ReactionEvent.ReactionType)
                                }.ToArray();
                            }
                        }

                        adverseReactionsRelationships.Add(entryRelationshipObservation);

                        if (adverserReaction.SubstanceOrAgent != null)
                            entryList.Add(CreateEntryActEvent(x_ActRelationshipEntry.COMP,
                                                              x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN,
                                                              CreateConceptDescriptor("102.15517", CodingSystem.NCTIS, "Adverse Reaction", null),
                                                              CreateParticipant2Array(CreateCodedWithExtensionElement(adverserReaction.SubstanceOrAgent), null),
                                                              adverseReactionsRelationships, 
                                                              adverserReaction.Id));
                    }
                }
                else
                {
                    // Exclusions
                    if (adverseSubstanceReactions.ExclusionStatement != null)
                    {
                        entryList.Add(CreateExclusionStatement(adverseSubstanceReactions.ExclusionStatement, exclusionStatementCode));
                    }
                }

                component.section.entry = entryList.ToArray();
            }

            if (component != null && component.section != null)
            {
                component.section.text = adverseSubstanceReactions.CustomNarrativeAdverseReactions ?? narrativeGenerator.CreateNarrative(adverseSubstanceReactions);
            }

            return component;
        }

        /// <summary>
        /// Creates an Diagnoses Intervention
        /// </summary>
        /// <param name="diagnosesIntervention">Diagnoses Interventions</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(DiagnosesIntervention diagnosesIntervention, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (diagnosesIntervention != null)
            {
                component = new POCD_MT000040Component3 { section = CreateSectionCodeTitle("101.16117", CodingSystem.NCTIS, "Diagnoses/Interventions") };

                //PROBLEM / DIAGNOSIS
                if (diagnosesIntervention.ProblemDiagnosis != null && diagnosesIntervention.ProblemDiagnosis.Any())
                {
                    foreach (var diagnosis in diagnosesIntervention.ProblemDiagnosis) CreateProblemDiagnosisEntry(diagnosis as ProblemDiagnosis, ref entryList);
                }

                // PROCEDURES
                if (diagnosesIntervention.Procedures != null && diagnosesIntervention.Procedures.Any())
                {
                    entryList.AddRange(CreateProcedureEntries(diagnosesIntervention.Procedures));
                }

                // MEDICAL HISTORY ITEM
                if (diagnosesIntervention.UncategorisedMedicalHistoryItem != null && diagnosesIntervention.UncategorisedMedicalHistoryItem.Any())
                {
                    entryList.AddRange(CreateProcedureEntries(diagnosesIntervention.UncategorisedMedicalHistoryItem,
                                                              new CodableText
                                                              {
                                                                  Code = "102.16627",
                                                                  CodeSystem = CodingSystem.NCTIS,
                                                                  DisplayName = "Medical History Item"
                                                              }));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = diagnosesIntervention.CustomNarrativeDiagnosesIntervention ?? narrativeGenerator.CreateNarrative(diagnosesIntervention);
            }
            return component;
        }

        /// <summary>
        /// Creates a Diagnostic Investigations component
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigationsDischargeSummary</param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateComponentLegacy(Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.IDiagnosticInvestigationsDischargeSummary diagnosticInvestigations, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component5 component = null;

            var componentList = new List<POCD_MT000040Component5>();

            if (diagnosticInvestigations != null)
            {
                // create table for narrative summary
                var strucDocText = new StrucDocText();
                var narrative = new List<List<String>>();
                var strucDocTableList = new List<StrucDocTable>();

                var headerList = new List<String>()
                {
                    "Test Name",
                    "Date",
                    "Result",
                };

                // Path results
                if (diagnosticInvestigations.PathologyTestResult != null)
                {
                    foreach (var path in diagnosticInvestigations.PathologyTestResult)
                    {
                        var diagnostics = new List<string>
                        {
                            path.TestResultName?.NarrativeText,
                            path.ObservationDateTime?.NarrativeText(),
                            path.OverallTestResultStatus?.NarrativeText
                        };
                        narrative.Add(diagnostics);

                    }
                }

                //DI results
                if (diagnosticInvestigations.ImagingExaminationResult != null)
                {
                    foreach (var di in diagnosticInvestigations.ImagingExaminationResult)
                    {
                        var diagnostics = new List<string>
                            {
                                di.ExaminationResultName?.NarrativeText,
                                di.ResultDateTime?.NarrativeText(),
                                di.ExaminationResultStatus?.NarrativeText
                            };

                        narrative.Add(diagnostics);
                    }
                }

                //Other results
                if (diagnosticInvestigations.OtherTestResult != null)
                {
                    foreach (var other in diagnosticInvestigations.OtherTestResult)
                    {
                        var diagnostics = new List<string>
                            {
                                other.ReportName?.NarrativeText,
                                other.ReportDate?.NarrativeText(),
                                other.ReportStatus?.NarrativeText
                            };

                        narrative.Add(diagnostics);
                    }
                }

                // Any entries added - create table
                if (narrative.Count > 0)
                {
                    strucDocTableList.Add
                    (
                        CDANarrativeGenerator.PopulateTable
                        (
                            "Selected Investigation Results",
                            null,
                            headerList.ToArray(),
                            null,
                            narrative
                        )
                    );
                }

                // Create Narrative - if you want to add text
                //strucDocText.paragraph = CDANarrativeGenerator.CreateParagraph("Some text");
                if (strucDocTableList.Any())
                {
                    strucDocText.table = strucDocTableList.ToArray();
                }
                
                component = new POCD_MT000040Component5
                {
                    section = CreateSectionCodeTitle("101.20117", CodingSystem.NCTIS, "Diagnostic Investigations", "Diagnostic Investigations", strucDocText)
                };

                //Override with custom narrative
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
                                    CreateRelationshipForEachSpecimenDetailLegacy(pathologyTestResult.TestSpecimenDetail, CDADocumentType.DischargeSummary)
                                );
                        }

                        //Create relationships covering the test result groups
                        if (pathologyTestResult.ResultGroup != null)
                        {
                            relationshipList.AddRange(CreateRelationshipForEachTestResultGroupLegacy(pathologyTestResult.ResultGroup, CDADocumentType.DischargeSummary));
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
                                    CreateRelationshipForTestResultStatusLegacy
                                        (
                                            pathologyTestResult.OverallTestResultStatus
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
                                    CreateRelationshipForTestResultPathologicalDiagnosisLegacy(pathologyTestResult.PathologicalDiagnosis)
                                );
                        }

                        //Test conclusion
                        if (!pathologyTestResult.Conclusion.IsNullOrEmptyWhitespace())
                        {
                            relationshipList.Add
                                (
                                    CreateRelationshipForTestResultConculsionLegacy(pathologyTestResult.Conclusion)
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
                                    CreateRelationshipForTestRequestDetailsLegacy
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
                                    CreateRelationshipForTestResultStatusLegacy(imagingExaminationResult.ExaminationResultStatus)
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
                                    CreateRelationshipForEachImagingResultGroupLegacy(imagingExaminationResult.ResultGroup)
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
                                    CreateRelationshipForExaminationRequestLegacy(imagingExaminationResult.ExaminationRequestDetails)
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
                                imagingExaminationResult.AnatomicalSite == null ? null : CreateConceptDescriptorsForAnatomicalSitesLegacy(imagingExaminationResult.AnatomicalSite).ToArray(),
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
                    componentList.AddRange(diagnosticInvestigations.OtherTestResult.Select(otherTestResult => CreateOtherTestResultLegacy(otherTestResult, narrativeGenerator)));
                }

                component.section.component = componentList.ToArray();
            }

            return component;
        }

        /// <summary>
        /// Creates a reviewed medications component
        /// </summary>
        /// <param name="medications">medications</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(IMedications medications, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null)
            {
                component = new POCD_MT000040Component3 { section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medications") };

                var relationshipList = new List<POCD_MT000040EntryRelationship>();

                if (medications.Medications != null)
                {
                    foreach (var medication in medications.Medications)
                    {
                        // Medications History
                        relationshipList.Clear();

                        //Clinical Indication
                        if (!medication.ClinicalIndication.IsNullOrEmptyWhitespace())
                        {
                            relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.RSON,
                                                                            x_ActClassDocumentEntryAct.INFRM,
                                                                            x_DocumentActMood.EVN, false,
                                                                            CreateConceptDescriptor("103.10141",
                                                                                                    CodingSystem.NCTIS,
                                                                                                    "Clinical Indication",
                                                                                                    null),
                                                                            CreateStructuredText(medication.ClinicalIndication, null),
                                                                            null));
                        }

                        //Additional Comments
                        if (!medication.Comment.IsNullOrEmptyWhitespace())
                        {
                            relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                                            x_ActClassDocumentEntryAct.INFRM,
                                                                            x_DocumentActMood.EVN, false,
                                                                            CreateConceptDescriptor(AdminCodes.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                                                                                    CodingSystem.NCTIS, AdminCodes.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Name), null),
                                                                            CreateStructuredText(medication.Comment, null), null));
                        }

                        //medicine_list + directions_list
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                                                                              x_DocumentSubstanceMood.EVN, false,
                                                                              CreateStructuredText(medication.Directions), null,
                                                                              null, null,
                                                                              medication.Medicine == null ? null
                                                                                  : CreateCodedWithExtensionElement(
                                                                                      medication.Medicine.Code,
                                                                                      medication.Medicine.CodeSystemCode,
                                                                                      medication.Medicine.CodeSystemName,
                                                                                      medication.Medicine.CodeSystemVersion,
                                                                                      medication.Medicine.DisplayName,
                                                                                      medication.Medicine.OriginalText,
                                                                                      medication.Medicine.Translations,
                                                                                      null),
                                                                              null,
                                                                              relationshipList, null));
                    }
                }

                // Exclusions
                if (medications.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(medications.ExclusionStatement, "103.16302.120.1.2"));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = medications.CustomNarrativeMedications ?? narrativeGenerator.CreateNarrative(medications);

            }
            return component;
        }

        /// <summary>
        /// Creates a medications component
        /// </summary>
        /// <param name="medications">A list of Medication Items</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(List<IMedicationItem> medications, StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null && medications.Any())
            {
                component = new POCD_MT000040Component3 { section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medications") };

                if (medications.Any())
                {
                    foreach (var medication in medications)
                    {
                        var relationshipList = new List<POCD_MT000040EntryRelationship>();

                        relationshipList.AddRange(CreateRelationshipForMedicationLegacy(medication, "Comment"));

                        // Medications
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                                                                              x_DocumentSubstanceMood.EVN, false,
                                                                              CreateStructuredText(medication.Directions),
                                                                              null, null, null,
                                                                              medication.Medicine == null ? null
                                                                                  : CreateCodedWithExtensionElement(
                                                                                      medication.Medicine.Code,
                                                                                      medication.Medicine.CodeSystemCode,
                                                                                      medication.Medicine.CodeSystemName,
                                                                                      medication.Medicine.CodeSystemVersion,
                                                                                      medication.Medicine.DisplayName,
                                                                                      medication.Medicine.OriginalText,
                                                                                      medication.Medicine.Translations,
                                                                                      null),
                                                                              null, relationshipList,
                                                                              null, "active", null, null, null, null, null, null));
                    }
                }

                component.section.entry = entryList.ToArray();
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrativeLegacy(medications);
            }
            return component;
        }

        /// <summary>
        /// Creates a medical history component
        /// </summary>
        /// <param name="medicalHistory">IMedicalHistory</param>
        /// <param name="showExclusionStatements">
        /// This indicates wether exclusion statements should be included abiding 
        /// by the Guidelines for appropriate use of exclusion statements (Supplementary Guidance for Implementers)
        /// </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(MedicalHistory medicalHistory, bool showExclusionStatements, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medicalHistory != null)
            {
                component = new POCD_MT000040Component3 { section = CreateSectionCodeTitle("101.16117", CodingSystem.NCTIS, "Medical History") };

                //PROBLEM / DIAGNOSIS
                if (medicalHistory.ProblemDiagnosis != null && medicalHistory.ProblemDiagnosis.Any())
                    entryList.AddRange(CreateProblemDiagnosisEntries(medicalHistory.ProblemDiagnosis));

                if (showExclusionStatements)
                {
                    // PROBLEM DIAGNOSIS EXCLUSIONS
                    if (medicalHistory.ProblemDiagnosisExclusionStatement != null)
                        entryList.Add(CreateExclusionStatement(medicalHistory.ProblemDiagnosisExclusionStatement, "103.16302.120.1.3"));

                    // PROCEDURE EXCLUSIONS
                    if (medicalHistory.ProceduresExclusionStatement != null)
                        entryList.Add(CreateExclusionStatement(medicalHistory.ProceduresExclusionStatement, "103.16302.120.1.4"));
                }

                // PROCEDURES
                if (medicalHistory.Procedures != null && medicalHistory.Procedures.Any())
                    entryList.AddRange(CreateProcedureEntries(medicalHistory.Procedures));

                //OTHER MEDICAL HISTORY
                if (medicalHistory.MedicalHistoryItems != null && medicalHistory.MedicalHistoryItems.Any())
                    entryList.AddRange(CreateProcedureEntries(medicalHistory.MedicalHistoryItems,
                                                              new CodableText
                                                              {
                                                                  Code = "102.16627",
                                                                  CodeSystem = CodingSystem.NCTIS,
                                                                  DisplayName = "Other Medical History Item"
                                                              }));

                component.section.entry = entryList.ToArray();
                component.section.text = medicalHistory.CustomNarrativeMedicalHistory ?? narrativeGenerator.CreateNarrative(medicalHistory, showExclusionStatements);
            }

            return component;
        }

        /// <summary>
        /// Creates a Diagnostic Investigations component
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigations</param>
        /// <param name="cdaDocumentType">The document type </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(IDiagnosticInvestigations diagnosticInvestigations, CDADocumentType? cdaDocumentType, INarrativeGenerator narrativeGenerator)
        {
            // For REF, SPEC
            POCD_MT000040Component3 component = null;

            var componentList = new List<POCD_MT000040Component5>();

            if (diagnosticInvestigations != null)
            {
                if (diagnosticInvestigations.OtherTestResult != null && diagnosticInvestigations.OtherTestResult.Any())
                {
                    componentList.AddRange(diagnosticInvestigations.OtherTestResult.Select(otherTestResult => CreateOtherTestResultLegacy(otherTestResult, narrativeGenerator)));
                }

                // Diagnostic Investigations
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.20117", CodingSystem.NCTIS, "Diagnostic Investigations", "This section contains the following subsections: Requested Service, Pathology Test Result and Imaging Examination Result."),
                };

                if (diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations != null) component.section.text = diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations;

                // REQUESTED SERVICE
                if (diagnosticInvestigations.RequestedService != null && diagnosticInvestigations.RequestedService.Any())
                {
                    componentList.AddRange(CreateComponent(diagnosticInvestigations.RequestedService, narrativeGenerator).ToArray());
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
                            section = CreateSectionCodeTitle("102.16144", CodingSystem.NCTIS, "Pathology Test Result")
                        };

                        //Create relationships covering the test Specimen details
                        if (pathologyTestResult.TestSpecimenDetail != null)
                        {
                            relationshipList.AddRange(CreateRelationshipForEachSpecimenDetailLegacy(pathologyTestResult.TestSpecimenDetail, cdaDocumentType));
                        }

                        //Create relationships covering the test result groups
                        if (pathologyTestResult.ResultGroup != null)
                        {
                            relationshipList.AddRange(CreateRelationshipForEachTestResultGroupLegacy(pathologyTestResult.ResultGroup, cdaDocumentType));
                        }

                        //Create relationships covering the diagnostic Service
                        if (pathologyTestResult.DiagnosticService != null)
                        {
                            relationshipList.Add(
                                CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP,
                                                                   CreateConceptDescriptor("310074003", CodingSystem.SNOMED, "pathology service", null, Constants.SnomedVersion20110531, null),
                                                                   CreateConceptDescriptor(pathologyTestResult.DiagnosticService.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                                                   CodingSystem.HL7DiagnosticServiceSectionID, pathologyTestResult.DiagnosticService.GetAttributeValue<NameAttribute, String>(x => x.Name), null)));
                        }

                        //Create a relationship containing the status of the pathology test
                        if (pathologyTestResult.OverallTestResultStatus != null)
                        {
                            relationshipList.Add(CreateRelationshipForTestResultStatusLegacy(pathologyTestResult.OverallTestResultStatus));
                        }

                        //Create a relationship containing the provided clinical information
                        relationshipList.Add(CreateRelationshipForProvidedClinicalInformation(pathologyTestResult.ClinicalInformationProvided));

                        //pathological diagnosis
                        if (pathologyTestResult.PathologicalDiagnosis != null && pathologyTestResult.PathologicalDiagnosis.Any())
                        {
                            relationshipList.Add(CreateRelationshipForTestResultPathologicalDiagnosisLegacy(pathologyTestResult.PathologicalDiagnosis));
                        }

                        //Test conclusion
                        if (!String.IsNullOrEmpty(pathologyTestResult.Conclusion))
                        {
                            relationshipList.Add(CreateRelationshipForTestResultConculsionLegacy(pathologyTestResult.Conclusion));
                        }

                        //test comment
                        if (!String.IsNullOrEmpty(pathologyTestResult.TestComment))
                        {
                            relationshipList.Add(CreateRelationshipForTestResultComment(pathologyTestResult.TestComment));
                        }

                        //Test request details; includes the requested test name(s) and identifier(s)
                        if (pathologyTestResult.TestRequestDetails != null && pathologyTestResult.TestRequestDetails.Any())
                        {
                            relationshipList.AddRange(pathologyTestResult.TestRequestDetails.Select(CreateRelationshipForTestRequestDetailsLegacy));
                        }

                        //pathology test result date time
                        if (pathologyTestResult.ObservationDateTime != null)
                        {
                            relationshipList.Add(CreateRelationshipForTestResultDateTime(pathologyTestResult.ObservationDateTime));
                        }

                        //Create the observation entry with all the above relationships nested inside the observation
                        entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                                             CreateConceptDescriptor(pathologyTestResult.TestResultName),
                                                             null,
                            // Taken out DateTime now, because it did not appear to map to anything
                                                             new List<ANY>
                                                              {   
                                                                  pathologyTestResult.TestResultRepresentation != null && pathologyTestResult.TestResultRepresentation.ExternalData != null ? 
                                                                  CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation.ExternalData) : 
                                                                  (pathologyTestResult.TestResultRepresentation != null && !pathologyTestResult.TestResultRepresentation.Text.IsNullOrEmptyWhitespace() ? 
                                                                  CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation.Text) : null)
                                                              },
                                                             null,
                                                             relationshipList));

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
                        if (imagingExaminationResult.AnatomicalSite != null &&
                            imagingExaminationResult.AnatomicalSite.Any())
                        {
                            foreach (var anatomicalSite in imagingExaminationResult.AnatomicalSite.Where(anatomicalSite => anatomicalSite.Images != null && anatomicalSite.Images.Any()))
                            {
                                relationshipList.AddRange(CreateRelationshipsForEachImage(anatomicalSite.Images));
                            }
                        }

                        //Imaging examination result status
                        if (imagingExaminationResult.ExaminationResultStatus != null)
                        {
                            relationshipList.Add(CreateRelationshipForTestResultStatusLegacy(imagingExaminationResult.ExaminationResultStatus));
                        }

                        //Clinical information provided
                        if (!String.IsNullOrEmpty(imagingExaminationResult.ClinicalInformationProvided))
                        {
                            relationshipList.Add(CreateRelationshipForProvidedClinicalInformation(imagingExaminationResult.ClinicalInformationProvided));
                        }

                        //Findings
                        if (!String.IsNullOrEmpty(imagingExaminationResult.Findings))
                        {
                            relationshipList.Add(CreateRelationshipForFindings(imagingExaminationResult.Findings));
                        }

                        //Examination result group
                        if (imagingExaminationResult.ResultGroup != null && imagingExaminationResult.ResultGroup.Any())
                        {
                            relationshipList.AddRange(CreateRelationshipForEachImagingResultGroupLegacy(imagingExaminationResult.ResultGroup));
                        }

                        //Examination result date / time 
                        if (imagingExaminationResult.ResultDateTime != null)
                        {
                            relationshipList.Add(CreateRelationshipForDateTime(imagingExaminationResult.ResultDateTime));
                        }

                        //Examination request details
                        if (imagingExaminationResult.ExaminationRequestDetails != null)
                        {
                            relationshipList.AddRange(CreateRelationshipForExaminationRequestLegacy(imagingExaminationResult.ExaminationRequestDetails));
                        }

                        //Create the observation entry with all the above relationships nested inside the observation
                        entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                                                             CreateConceptDescriptor(
                                                                 imagingExaminationResult.ExaminationResultName),
                                                             new[]
                                                                 {
                                                                     CreateCodedWithExtensionElement(imagingExaminationResult.Modality)
                                                                 },
                                                             imagingExaminationResult.AnatomicalSite == null ? null : CreateConceptDescriptorsForAnatomicalSitesLegacy(
                                                             imagingExaminationResult.AnatomicalSite).ToArray(),
                                                             null,
                                                             null,
                                                             null,
                                                             relationshipList,
                                                             imagingExaminationResult.ExaminationResultRepresentation,
                                                             null,
                                                             null,
                                                             null,
                                                             null,
                                                             null));

                        imagingExaminationResultComponent.section.entry = entryList.ToArray();
                        imagingExaminationResultComponent.section.text = imagingExaminationResult.CustomNarrativeImagingExaminationResult ?? narrativeGenerator.CreateNarrative(imagingExaminationResult);

                        componentList.Add(imagingExaminationResultComponent);
                    }
                }

                component.section.component = componentList.ToArray();
            }

            return component;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForExaminationRequestLegacy(ICollection<IImagingExaminationRequest> request)
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
                                                                   CreateConceptDescriptor("103.16515", CodingSystem.NCTIS, "Image Details", null), null,
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

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForEachSpecimenDetailLegacy(ICollection<SpecimenDetail> specimenDetails, CDADocumentType? documentType)
        {
            var relationshipList = new List<POCD_MT000040EntryRelationship>();

            if (specimenDetails != null && specimenDetails.Any())
            {
                relationshipList.AddRange(specimenDetails.Select(specimenDetail => new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.SUBJ, observation = 
                    CreateSpecimenDetailLegacy(specimenDetail, 
                    CreateTestSpecimenDetailCodeLegacy(documentType))
                }));
            }
            return relationshipList;
        }

        /// <summary>
        /// Creates an Other Test Result
        /// </summary>
        /// <param name="otherTestResult">An OtherTestResult object</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateOtherTestResultLegacy(OtherTestResult otherTestResult, INarrativeGenerator narrativeGenerator)
        {
            var relationshipList = new List<POCD_MT000040EntryRelationship>();
            var entryList = new List<POCD_MT000040Entry>();

            //Create the otherTestResultComponent and section
            var otherTestResultComponent = new POCD_MT000040Component5
            {
                section = CreateSectionCodeTitle("102.16029", CodingSystem.NCTIS, "Diagnostic Investigation")
            };

            if (otherTestResult.ReportStatus != null)
                relationshipList.Add(CreateRelationshipForTestResultStatusLegacy(otherTestResult.ReportStatus));

            if (otherTestResult.ReportContent != null && otherTestResult.ReportContent.ExternalData != null)
            {
                var imageEntryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    observationMedia = CreateObservationMedia(otherTestResult.ReportContent.ExternalData)
                };
                relationshipList.Add(imageEntryRelationship);
            }

            //Create the observation entry with all the above relationships nested inside the observation
            var entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                CreateConceptDescriptor(otherTestResult.ReportName),
                otherTestResult.ReportDate,
                relationshipList
            );

            // Report Content
            if (otherTestResult.ReportContent != null)
            {
                // Encapsulated Text
                if (!otherTestResult.ReportContent.Text.IsNullOrEmptyWhitespace())
                {
                    entry.observation.value = new ANY[] {CreateEncapsulatedData(otherTestResult.ReportContent.Text)};
                }
                // External Data
                else if (otherTestResult.ReportContent.ExternalData != null)
                {
                    entry.observation.value = new ANY[]
                        {CreateEncapsulatedData(otherTestResult.ReportContent.ExternalData)};
                }
            }

            entryList.Add(entry);

            otherTestResultComponent.section.text = otherTestResult.CustomNarrative ?? narrativeGenerator.CreateNarrative(otherTestResult);
            otherTestResultComponent.section.entry = entryList.ToArray();

            return otherTestResultComponent;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiserLegacy(
             x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
             x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
             CS testStatus, ITestResultGroup testResultGroup, CDADocumentType? cdaDocumentType)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                organizer =
                    new POCD_MT000040Organizer
                    {
                        author = null,
                        classCode = classCode,
                        code = code,
                        component = null,
                        effectiveTime = null,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        informant = null,
                        moodCode = moodCode,
                        nullFlavor = NullFlavor.NA,
                        nullFlavorSpecified = false,
                        statusCode = testStatus
                    },
            };

            var component4List = new List<POCD_MT000040Component4>();

            if (testResultGroup != null && testResultGroup.ResultGroupSpecimenDetail != null)
            {
                component4List.Add(new POCD_MT000040Component4
                {
                    observation = CreateSpecimenDetailLegacy(testResultGroup.ResultGroupSpecimenDetail, CreateResultGroupSpecimenDetailCodeLegacy(cdaDocumentType))
                });
            }

            if (testResultGroup != null && testResultGroup.Results != null && testResultGroup.Results.Any())
            {
                foreach (var testResult in testResultGroup.Results)
                {
                    var component4 = new POCD_MT000040Component4
                    {
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                id = CreateIdentifierArray(CreateGuid(), null),
                                code = CreateConceptDescriptor(testResult.ResultName)
                            }
                    };

                    //REFERENCE RANGE DETAILS
                    if ((testResult.ResultValueReferenceRangeDetails != null &&
                         testResult.ResultValueReferenceRangeDetails.Any()) || testResult.ResultValue != null)
                    {
                        component4.observation.value =
                            new List<ANY> { CreateResultValueAny(testResult.ResultValue, null) }.ToArray();

                        component4.observation.interpretationCode =
                            new List<CE> { CreateCodedWithExtensionElement(testResult.NormalStatus, null) }.ToArray();

                        if (testResult.ResultValueReferenceRangeDetails != null &&
                            testResult.ResultValueReferenceRangeDetails.Any())
                        {
                            var referenceRange = new List<POCD_MT000040ReferenceRange>();

                            foreach (var resultValueReferenceRangeDetail in testResult.ResultValueReferenceRangeDetails)
                            {
                                referenceRange.Add(new POCD_MT000040ReferenceRange
                                {
                                    typeCode = ActRelationshipType.REFV,
                                    typeCodeSpecified = true,
                                    observationRange =
                                new POCD_MT000040ObservationRange
                                {
                                    classCode = ActClassObservation.OBS,
                                    moodCode = ActMood.EVNCRT,
                                    moodCodeSpecified = true,
                                    code =
                            CreateConceptDescriptor(
                                resultValueReferenceRangeDetail.
                                    ResultValueReferenceRangeMeaning),
                                    value =
                            CreateIntervalPhysicalQuantity(
                                resultValueReferenceRangeDetail.Range
                            )
                                },
                                });
                            }
                            component4.observation.referenceRange = referenceRange.ToArray();
                        }
                    }

                    var relationships = new List<POCD_MT000040EntryRelationship>();

                    //Create a relationship containing the status of the pathology test
                    if (testResult.ResultStatus != null)
                    {
                        relationships.Add(CreateRelationshipForTestResultStatusLegacy(testResult.ResultStatus));
                    }

                    //COMMENTS
                    if (testResult.Comments != null && testResult.Comments.Any())
                    {
                        foreach (var comment in testResult.Comments)
                        {
                            relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                                         x_ActClassDocumentEntryAct.INFRM,
                                                                         x_DocumentActMood.EVN, false,
                                                                         CreateConceptDescriptor("281296001",
                                                                                                 CodingSystem.SNOMED,
                                                                                                 "result comments",
                                                                                                 null,
                                                                                                 Constants.SnomedVersion20110531,
                                                                                                 null),
                                                                         CreateStructuredText(comment, null), null));
                        }
                    }

                    //REFERENCE RANGE GUIDANCE
                    if (testResult.ReferenceRangeGuidance != null)
                    {
                        relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                                     x_ActClassDocumentEntryAct.INFRM,
                                                                     x_DocumentActMood.EVN, false,
                                                                     CreateConceptDescriptor("281298000",
                                                                                             CodingSystem.SNOMED,
                                                                                             "reference range comments",
                                                                                             null,
                                                                                             Constants.SnomedVersion20110531,
                                                                                             null),
                                                                     CreateStructuredText(testResult.ReferenceRangeGuidance, null), null));
                    }

                    component4.observation.entryRelationship = relationships.ToArray();

                    component4List.Add(component4);
                }

                entryRelationship.organizer.component = component4List.ToArray();
            }

            return entryRelationship;
        }

        private static List<CD> CreateConceptDescriptorsForAnatomicalSitesLegacy(ICollection<AnatomicalSite> anatomicalSites)
        {
            List<CD> componentDescriptionList = null;

            if (anatomicalSites != null && anatomicalSites.Any())
            {
                componentDescriptionList = (from anatomicalSite in anatomicalSites where anatomicalSite != null select CreateConceptDescriptorForAnatomicalSiteLegacy(anatomicalSite)).ToList();
            }

            return componentDescriptionList;
        }

        private static CD CreateConceptDescriptorForAnatomicalSiteLegacy(AnatomicalSite anatomicalSite)
        {
            CD cd = null;

            if (anatomicalSite != null)
            {
                if (anatomicalSite.SpecificLocation != null)
                {
                    if (anatomicalSite.SpecificLocation.NameOfLocation != null)
                    {
                        cd = CreateConceptDescriptor(anatomicalSite.SpecificLocation.NameOfLocation);
                    }

                    var codedValue = CreateCodedValue
                        (
                            "78615007",
                            CodingSystem.SNOMED,
                            "with laterality",
                            null,
                            Constants.SnomedVersion20110531,
                            null
                        );

                    if (anatomicalSite.SpecificLocation.Side != null)
                    {
                        if (cd == null) cd = new CD();

                        cd.qualifier = new List<CR> 
                                          {
                                              new CR 
                                                   { 
                                                       name =  codedValue,
                                                       value = CreateConceptDescriptor(anatomicalSite.SpecificLocation.Side)
                                                   }
                                           }.ToArray();
                    }
                }

                if (!string.IsNullOrEmpty(anatomicalSite.Description))
                {
                    if (cd == null) cd = new CD();

                    cd.originalText = CreateEncapsulatedData(anatomicalSite.Description);
                }
            }

            return cd;
        }



        private static POCD_MT000040Entry CreateEntryObservationLegacy(x_ActRelationshipEntry actRelationshipEntry,
                                                                   CD code,
                                                                   ISO8601DateTime effectiveTime,
                                                                   List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {

            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (effectiveTime != null)
            {
                entry.observation.effectiveTime = CreateIntervalTimestamp(null, null, null, null, effectiveTime.ToString(), null);
            }

            if (entryRelationshipList.Any())
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            return entry;
        }


        private static POCD_MT000040Entry CreateEntryObservationLegacy(x_ActRelationshipEntry? actRelationshipEntry, CD code,
                                                                 ISO8601DateTime effectiveTime, List<ANY> anyList, CE ceCode,
                                                                 List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            var entry = new POCD_MT000040Entry
            {
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (actRelationshipEntry.HasValue)
            {
                entry.typeCode = actRelationshipEntry.Value;
            }

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (effectiveTime != null)
            {
                entry.observation.effectiveTime = new IVL_TS { value = effectiveTime.ToString() };
            }

            if (anyList != null && anyList.Any())
            {
                entry.observation.value = anyList.ToArray();
            }

            if (ceCode != null)
            {
                entry.observation.participant = CreateParticipant2Array(ceCode, EntityDeterminer.INSTANCE);
            }

            if (entryRelationshipList != null)
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            return entry;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForEachTestResultGroupLegacy(IEnumerable<ITestResultGroup> testResultGroups, CDADocumentType? cdaDocumentType)
        {
            List<POCD_MT000040EntryRelationship> relationshipList = null;

            if (testResultGroups != null)
            {
                relationshipList = new List<POCD_MT000040EntryRelationship>();

                //Create Organizer and relationships, these are nested within the observation entry below
                foreach (var testResultGroup in testResultGroups)
                {
                    var entryRelationshipOrganiser =
                        CreateEntryRelationshipOrganiserLegacy(x_ActRelationshipEntryRelationship.COMP,
                                                         x_ActClassDocumentEntryOrganizer.BATTERY, ActMood.EVN, false,
                                                         CreateConceptDescriptor(testResultGroup.ResultGroupName), null,
                                                         StatusCode.Completed, testResultGroup, cdaDocumentType);

                    relationshipList.Add(entryRelationshipOrganiser);
                }
            }

            return relationshipList;
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationLegacy()
        {
            var entry = new POCD_MT000040Entry();

            entry.typeCode = x_ActRelationshipEntry.DRIV;
            entry.substanceAdministration = new POCD_MT000040SubstanceAdministration();
            entry.substanceAdministration.moodCode = x_DocumentSubstanceMood.RQO;
            entry.substanceAdministration.classCode = ActClass.SBADM;
            entry.substanceAdministration.statusCode = CreateCodeSystem("active", null, null, null, null, null);

            return entry;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiserLegacy(
             x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
             x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
             StatusCode overallTestStatus, ITestResultGroup testResultGroup, CDADocumentType? cdaDocumentType)
        {
            var testStatus = CreateCodeSystem(overallTestStatus.GetAttributeValue<NameAttribute, String>(x => x.Name), null, null, null, null, null);

            return CreateEntryRelationshipOrganiserLegacy(actRelationshipEntryRelationshipTypeCode, classCode, moodCode, inversion, code, text, testStatus, testResultGroup, cdaDocumentType);
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForEachImagingResultGroupLegacy(ICollection<IImagingResultGroup> resultGroups)
        {
            List<POCD_MT000040EntryRelationship> relationshipList = null;

            if (resultGroups != null && resultGroups.Any())
            {
                relationshipList = resultGroups.Select(resultGroup => 
                    CreateEntryRelationshipOrganiserLegacy(x_ActRelationshipEntryRelationship.COMP, 
                                                     x_ActClassDocumentEntryOrganizer.BATTERY,
                                                     ActMood.EVN,
                                                     null,
                                                     CreateConceptDescriptor(resultGroup.ResultGroupName), 
                                                     null, 
                                                     resultGroup.Results, 
                                                     resultGroup.AnatomicalSite, 
                                                     StatusCode.Completed)).ToList();
            }

            return relationshipList;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiserLegacy(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
            IEnumerable<IImagingResult> imagingResults, AnatomicalSite anatomicalSite, StatusCode overallTestStatus)
        {
            var testStatus = CreateCodeSystem(overallTestStatus.GetAttributeValue<NameAttribute, String>(x => x.Name), null, null, null, null, null);

            return CreateEntryRelationshipOrganiserLegacy(actRelationshipEntryRelationshipTypeCode, classCode, moodCode, inversion, code, text, imagingResults, anatomicalSite, testStatus);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiserObservationLegacy(x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, CD code, string statusCode, List<POCD_MT000040Component4> components)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationship,
                organizer = new POCD_MT000040Organizer
                {
                    classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                    moodCode = ActMood.EVN,
                    code = code,
                    component = components != null ? components.ToArray() : null,
                    statusCode = new CS { code = statusCode }
                }
            };

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiserLegacy(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
            IEnumerable<IImagingResult> imagingResults, AnatomicalSite anatomicalSite, CS testStatus)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                organizer =
                    new POCD_MT000040Organizer
                    {
                        author = null,
                        classCode = classCode,
                        code = code,
                        component = null,
                        effectiveTime = null,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        informant = null,
                        moodCode = moodCode,
                        nullFlavor = NullFlavor.NA,
                        nullFlavorSpecified = false,
                        statusCode = testStatus
                    },
            };

            //Imaging Results
            if (imagingResults != null && imagingResults.Any())
            {
                var component4List = new List<POCD_MT000040Component4>();

                foreach (var imagingResult in imagingResults)
                {
                    var component4 = new POCD_MT000040Component4
                    {
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                id = CreateIdentifierArray(CreateGuid(), null),
                                code = CreateConceptDescriptor(imagingResult.ResultName)
                            }
                    };

                    //REFERENCE RANGE DETAILS
                    if ((imagingResult.ResultValueReferenceRangeDetails != null &&
                         imagingResult.ResultValueReferenceRangeDetails.Any()) || imagingResult.ResultValue != null)
                    {
                        component4.observation.value =
                            new List<ANY> { CreateResultValueAny(imagingResult.ResultValue, null) }.ToArray();

                        component4.observation.interpretationCode =
                            new List<CE> { CreateCodedWithExtensionElement(imagingResult.NormalStatus, null) }.
                                ToArray();

                        if (imagingResult.ResultValueReferenceRangeDetails != null &&
                            imagingResult.ResultValueReferenceRangeDetails.Any())
                        {
                            var referenceRange = new List<POCD_MT000040ReferenceRange>();

                            foreach (
                                var resultValueReferenceRangeDetail in imagingResult.ResultValueReferenceRangeDetails)
                            {
                                referenceRange.Add(new POCD_MT000040ReferenceRange
                                {
                                    typeCode = ActRelationshipType.REFV,
                                    typeCodeSpecified = true,
                                    observationRange =
                                new POCD_MT000040ObservationRange
                                {
                                    classCode = ActClassObservation.OBS,
                                    moodCode = ActMood.EVNCRT,
                                    moodCodeSpecified = true,
                                    code =
                            CreateConceptDescriptor(
                                resultValueReferenceRangeDetail.
                                    ResultValueReferenceRangeMeaning),
                                    value =
                            CreateIntervalPhysicalQuantity(
                                resultValueReferenceRangeDetail.Range
                                        //Result ResultValue Reference Range; the data range for the associated meaning
                            )
                                },
                                });
                            }
                            component4.observation.referenceRange = referenceRange.ToArray();
                        }
                    }

                    //COMMENTS
                    var relationships = new List<POCD_MT000040EntryRelationship>();

                    if (imagingResult.Comments != null && imagingResult.Comments.Any())
                    {
                        foreach (var comment in imagingResult.Comments)
                        {
                            relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                                         x_ActClassDocumentEntryAct.INFRM,
                                                                         x_DocumentActMood.EVN, false,
                                                                         CreateConceptDescriptor("281296001",
                                                                                                 CodingSystem.SNOMED,
                                                                                                 "result comments",
                                                                                                 null,
                                                                                                 Constants.SnomedVersion20110531,
                                                                                                 null),
                                                                         CreateStructuredText(comment, null), null));
                        }
                    }

                    //ANATOMICAL SITE
                    if (anatomicalSite != null)
                    {
                        component4.observation.targetSiteCode = new[]
                                                                    {
                                                                        CreateConceptDescriptorForAnatomicalSiteLegacy(anatomicalSite)
                                                                    };

                        if (anatomicalSite.Images != null && anatomicalSite.Images.Any())
                        {
                            relationships.AddRange(anatomicalSite.Images.Select(image => CreateEntryRelationshipObservationMedia(x_ActRelationshipEntryRelationship.REFR, image)));
                        }
                    }

                    component4.observation.entryRelationship = relationships.ToArray();

                    component4List.Add(component4);
                }

                entryRelationship.organizer.component = component4List.ToArray();
            }

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultConculsionLegacy(string conclusion)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (!String.IsNullOrEmpty(conclusion))
            {
                relationship = CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.REFR, false,
                                                                  CreateConceptDescriptor("386344002",
                                                                                          CodingSystem.SNOMED,
                                                                                          "laboratory findings data interpretation",
                                                                                          null,
                                                                                          Constants.SnomedVersion20110531,
                                                                                           null),
                                                                  new List<ANY> { CreateStructuredText(conclusion, null) });
            }

            return relationship;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestRequestDetailsLegacy(ITestRequest request)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (request != null)
            {
                relationship = CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.SUBJ,
                                                          x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN, true,
                                                          CreateConceptDescriptor("102.16160", CodingSystem.NCTIS, "Test Request Details", null), null,
                                                          CreateIdentifierArray(request.LaboratoryTestResultIdentifier));

                var entryRelationships = new List<POCD_MT000040EntryRelationship>();

                if (request.TestsRequestedName != null)
                {
                    foreach (var testRequested in request.TestsRequestedName)
                    {
                        entryRelationships.Add(
                            CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP,
                                                               ActClassObservation.OBS, x_ActMoodDocumentObservation.RQO,
                                                               false, null,
                                                               CreateConceptDescriptor("103.11017", CodingSystem.NCTIS, "Test Requested Name", null),
                                                               null, null,
                                                               new List<ANY>() { CreateConceptDescriptor(testRequested) },
                                                               null));
                    }
                }

                relationship.act.entryRelationship = entryRelationships.ToArray();
            }

            return relationship;
        }

 
        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultPathologicalDiagnosisLegacy(ICollection<ICodableText> pathologicalDiagnosis)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (pathologicalDiagnosis != null && pathologicalDiagnosis.Any())
            {
                var conceptDescriptorList = pathologicalDiagnosis.Select(CreateConceptDescriptor).Cast<ANY>().ToList();

                relationship = CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.REFR, false,
                                                                    CreateConceptDescriptor("88101002",
                                                                                            CodingSystem.SNOMED,
                                                                                            "pathology diagnosis", null,
                                                                                            Constants.SnomedVersion20110531,
                                                                                            null),
                                                                    conceptDescriptorList);

            }

            return relationship;
        }

        private static ICodableText CreateTestSpecimenDetailCodeLegacy(CDADocumentType? documentType)
        {
            var code = "102.16156.2.2.1";

            if (documentType.HasValue)
            {
                switch (documentType.Value)
                {
                    case CDADocumentType.SpecialistLetter: code = "102.16156.132.1.1";
                        break;
                }
            }

            return CreateCodableText(code, CodingSystem.NCTIS, "Test Specimen Detail", null);
        }

        private static ICodableText CreateResultGroupSpecimenDetailCodeLegacy(CDADocumentType? documentType)
        {
            var code = "102.16156.2.2.2";

            if (documentType.HasValue)
            {
                switch (documentType.Value)
                {
                    case CDADocumentType.SpecialistLetter: code = "102.16156.132.1.2";
                        break;
                }
            }

            return CreateCodableText(code, CodingSystem.NCTIS, "Result Group Specimen Detail", null);
        }

        private static POCD_MT000040Observation CreateSpecimenDetailLegacy(SpecimenDetail specimenDetail, ICodableText code)
        {
            POCD_MT000040Observation observation = null;

            if (specimenDetail != null)
            {
                observation = new POCD_MT000040Observation
                {
                    classCode = ActClassObservation.OBS,
                    moodCode = x_ActMoodDocumentObservation.EVN,
                    code = CreateConceptDescriptor(code),
                    id = CreateIdentifierArray(CreateGuid(), null)
                };

                if (specimenDetail.CollectionProcedure != null)
                {
                    observation.methodCode =
                        new List<CE> { CreateCodedWithExtensionElement(specimenDetail.CollectionProcedure) }.ToArray
                            ();
                }

                if (specimenDetail.CollectionDateTime != null)
                {
                    observation.effectiveTime = new IVL_TS
                    {
                        value = specimenDetail.CollectionDateTime.ToString()
                    };
                }

                //Anatomical Site
                if (specimenDetail.AnatomicalSite != null && specimenDetail.AnatomicalSite.Any())
                {
                    observation.targetSiteCode = CreateConceptDescriptorsForAnatomicalSitesLegacy(specimenDetail.AnatomicalSite).ToArray();
                }

                var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

                SpecimenDetailEntryRelationships(specimenDetail, ref entryRelationshipList);

                observation.specimen = new List<POCD_MT000040Specimen> { CreateSpecimenDetailIdentifiersLegacy(specimenDetail) }.ToArray();

                //Sub entry Relationship
                if (entryRelationshipList != null)
                {
                    observation.entryRelationship = entryRelationshipList.ToArray();
                }

                if (specimenDetail.CollectionDateTime != null)
                {
                    observation.effectiveTime = CreateIntervalTimestamp(specimenDetail.CollectionDateTime, null);
                }
            }

            return observation;
        }

        private static POCD_MT000040Specimen CreateSpecimenDetailIdentifiersLegacy(SpecimenDetail specimenDetail)
        {
            POCD_MT000040Specimen specimen = null;

            if (specimenDetail != null)
            {
                if (specimenDetail.SpecimenIdentifier != null ||
                    specimenDetail.PhysicalDescription != null ||
                    specimenDetail.ParentSpecimenIdentifier != null ||
                    specimenDetail.PhysicalDetails != null ||
                    specimenDetail.ContainerIdentifier != null ||
                    specimenDetail.SpecimenTissueType != null)
                {

                    var physicalDetailsList = new List<List<String>>();

                    specimen = new POCD_MT000040Specimen
                    {
                        specimenRole = new POCD_MT000040SpecimenRole
                        {
                            id = specimenDetail.SpecimenIdentifier == null ? null : CreateIdentifierArray(specimenDetail.SpecimenIdentifier)
                        }
                    };

                    if (
                        specimenDetail.SpecimenIdentifier != null ||
                        specimenDetail.PhysicalDescription != null ||
                        specimenDetail.ContainerIdentifier != null ||
                        specimenDetail.PhysicalDetails != null
                        )
                    {
                        specimen.specimenRole.specimenPlayingEntity = new POCD_MT000040PlayingEntity
                        {
                            code = CreateCodedWithExtensionElement(specimenDetail.SpecimenTissueType),
                            desc = CreateStructuredText(specimenDetail.PhysicalDescription, null),
                            asSpecimenInContainer = specimenDetail.ContainerIdentifier == null ? null : new SpecimenInContainer
                            {
                                classCode = EntityClass.CONT,
                                container = new Container
                                {
                                    id = CreateIdentifierElement(specimenDetail.ContainerIdentifier)
                                }
                            }
                        };
                    }


                    if (specimenDetail.PhysicalDetails != null)
                    {
                        var pqList = new List<PQ>();

                        foreach (var physicalDetail in specimenDetail.PhysicalDetails)
                        {
                            if (physicalDetail.WeightVolume != null)
                                pqList.Add(CreatePhysicalQuantity(physicalDetail.WeightVolume));

                            // Narrative
                            physicalDetailsList.Add
                                (
                                new List<string> 
                                    {
                                        physicalDetail.WeightVolume == null ? (physicalDetail.WeightVolume == null ? String.Empty : physicalDetail.WeightVolume.Units) : physicalDetail.WeightVolume.Units
                                    }
                                );
                        }

                        specimen.specimenRole.specimenPlayingEntity.quantity = pqList.ToArray();
                    }
                }
            }

            return specimen;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultStatusLegacy(ICodableText resultStatus)
        {
            return CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP, false,
                                                      CreateConceptDescriptor("308552006", CodingSystem.SNOMED, "report status", null, Constants.SnomedVersion20110531, null),
                                                      new List<ANY>
                                                          {
                                                              CreateConceptDescriptor(resultStatus)
                                                          },
                                                      false);
        }

        private static POCD_MT000040Entry CreateEntryObservationLegacy(x_ActRelationshipEntry actRelationshipEntry, CD code,
                                                         ISO8601DateTime effectiveTimeLow, List<ANY> anyList, CE ceCode,
                                                         List<POCD_MT000040EntryRelationship> entryRelationshipList,
                                                         BodyHeight bodyHeight, bool? booleanValue)
        {
            var entry = CreateEntryObservationLegacy(actRelationshipEntry, code, effectiveTimeLow, anyList, ceCode, entryRelationshipList);

            if (booleanValue.HasValue)
            {
                entry.observation.value = new ANY[] { new BL { value = booleanValue.Value, valueSpecified = true } };
            }

            if (bodyHeight != null)
            {
                entry.observation.value = new ANY[]
                                              {
                                                 CreatePhysicalQuantity(bodyHeight.Quantity)
                                              };
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryObservationLegacy(x_ActRelationshipEntry actRelationshipEntry, CD code,
                                                         ISO8601DateTime effectiveTimeLow, List<ANY> anyList, CE ceCode,
                                                         List<POCD_MT000040EntryRelationship>
                                                         entryRelationshipList, BodyWeight bodyWeight)
        {
            var entry = CreateEntryObservationLegacy(actRelationshipEntry, code, effectiveTimeLow, anyList, ceCode, entryRelationshipList);

            if (bodyWeight != null)
            {
                if (bodyWeight.Quantity != null)
                    if (bodyWeight.Quantity.Value != null)
                        entry.observation.value = new ANY[]
                                              {
                                                new PQ
                                                  {
                                                    unit = bodyWeight.Quantity.Units,
                                                    value =
                                                      bodyWeight.Quantity == null && !bodyWeight.Quantity.Value.IsNullOrEmptyWhitespace()
                                                        ? null
                                                        : bodyWeight.Quantity.Value
                                                  }
                                              };
            }

            return entry;
        }

        #region Medication

        /// <summary>
        /// Creates a medications component
        /// </summary>
        /// <param name="medications">IMedicationsSpecialistLetter</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(IMedicationsSpecialistLetter medications, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null)
            {
                component = new POCD_MT000040Component3 { section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medications") };

                if (medications.MedicationsList != null)
                {
                    foreach (var medication in medications.MedicationsList)
                    {
                        var relationshipList = new List<POCD_MT000040EntryRelationship>();

                        relationshipList.AddRange(CreateRelationshipForMedicationLegacy(medication, "Additional Comments"));

                        bool? negationInd = null;
                        if ((medication.ChangeType != null && medication.ChangeType.Code == ChangeTypeNctis.Ceased.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                            negationInd = true;

                        // Medications
                        entryList.Add(CreateEntrySubstanceAdministrationEventLegacy(x_ActRelationshipEntry.COMP,
                                                                              x_DocumentSubstanceMood.EVN, false,
                                                                              CreateStructuredText(medication.Directions),
                                                                              null, null, null,
                                                                              medication.Medicine == null ? null
                                                                                  : CreateCodedWithExtensionElement(
                                                                                      medication.Medicine.Code,
                                                                                      medication.Medicine.CodeSystemCode,
                                                                                      medication.Medicine.CodeSystemName,
                                                                                      medication.Medicine.CodeSystemVersion,
                                                                                      medication.Medicine.DisplayName,
                                                                                      medication.Medicine.OriginalText,
                                                                                      medication.Medicine.Translations,
                                                                                      null),
                                                                              null,
                                                                              relationshipList,
                                                                              negationInd));
                    }
                }

                // Exclusions
                if (medications.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(medications.ExclusionStatement, "103.16302.132.1.1"));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = medications.CustomNarrativeMedications ?? narrativeGenerator.CreateNarrativeLegacy(medications);
            }
            return component;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForMedicationLegacy(IMedicationItem medication, string commentDisplayName)
        {
            var changeTypeRelationshipList = new List<POCD_MT000040EntryRelationship>
                                                 {
                                                     CreateEntryRelationshipObservation(
                                                         x_ActRelationshipEntryRelationship.COMP,
                                                         CreateConceptDescriptor("103.16595", CodingSystem.NCTIS, "Recommendation or Change", null),
                                                         CreateConceptDescriptor(medication.ChangeStatus)),
                                                         medication.ChangeReason == null
                                                         ? null
                                                         : CreateEntryRelationshipACT(
                                                             x_ActRelationshipEntryRelationship.RSON,
                                                             x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN,
                                                             false,
                                                             CreateConceptDescriptor("103.10177", CodingSystem.NCTIS, "Change Reason", null),
                                                             CreateStructuredText(medication.ChangeReason), null),
                                                 };


            var relationshipList = new List<POCD_MT000040EntryRelationship>
                                       {
                                           String.IsNullOrEmpty(medication.ClinicalIndication)
                                               ? null
                                               : CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.RSON,
                                                                            x_ActClassDocumentEntryAct.INFRM,
                                                                            x_DocumentActMood.EVN, false,
                                                                            CreateConceptDescriptor("103.10141",
                                                                                                    CodingSystem.NCTIS,
                                                                                                    "Clinical Indication",
                                                                                                    null),
                                                                            CreateStructuredText(medication.ClinicalIndication, null),
                                                                            null),
                                           String.IsNullOrEmpty(medication.Comment)
                                               ? null
                                               : CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                                                            x_ActClassDocumentEntryAct.INFRM,
                                                                            x_DocumentActMood.EVN, false,
                                                                            CreateConceptDescriptor("103.16044",
                                                                                                    CodingSystem.NCTIS,
                                                                                                    commentDisplayName,
                                                                                                    null),
                                                                            CreateStructuredText(medication.Comment,
                                                                                                 null),
                                                                            CreateIdentifierArray(CreateGuid())),
                                           CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.SPRT,
                                                                              false,
                                                                              CreateConceptDescriptor("103.16593",
                                                                                                      CodingSystem.NCTIS,
                                                                                                      "Change Type",
                                                                                                      null),
                                                                              medication.ChangeDescription,
                                                                              new List<ANY>
                                                                                  {
                                                                                      CreateConceptDescriptor(medication.ChangeType)
                                                                                  },
                                                                              changeTypeRelationshipList)
                                       };

            return relationshipList;
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEventLegacy(x_ActRelationshipEntry? actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode, ST text, string low, string high, ISO8601DateTime effectiveTime, CE cEcode, int? repeatNumber, List<POCD_MT000040EntryRelationship> relationships, Boolean? negationIndicator = null, string statusCode = "active", List<Subject1> subjects = null, string id = null, string genericName = null, ICodableText formCode = null, ICodableText routeCode = null, Identifier manufacturerOrganizationId = null)
        {
            var entry = new POCD_MT000040Entry
            {
                substanceAdministration = new POCD_MT000040SubstanceAdministration
                {
                    classCode = ActClass.SBADM,
                    moodCode = substanceMood,
                    id = CreateIdentifierArray
                        (
                            String.IsNullOrEmpty(id) ? CreateGuid() : id,
                            null
                        ),
                    negationInd = negationIndicator.HasValue && negationIndicator.Value,
                    negationIndSpecified = negationIndicator.HasValue,
                    consumable = new POCD_MT000040Consumable
                    {
                        manufacturedProduct = new POCD_MT000040ManufacturedProduct
                        {
                            manufacturedMaterial = new POCD_MT000040Material
                            {
                                code = cEcode,
                                name = genericName.IsNullOrEmptyWhitespace() ? null : new EN { Text = new[] { genericName } },
                                formCode = CreateConceptDescriptor(formCode)
                            },
                            manufacturerOrganization = manufacturerOrganizationId != null ? new POCD_MT000040Organization
                            {
                                id = CreateIdentifierArray(manufacturerOrganizationId)
                            } : null
                        }
                    }
                }
            };

            if (actRelationshipEntry.HasValue)
            {
                entry.typeCode = actRelationshipEntry.Value;
            }

            if (showStatusCode)
            {
                entry.substanceAdministration.statusCode = CreateCodeSystem(statusCode, null, null, null, null, null);
            }

            if (text != null)
            {
                entry.substanceAdministration.text = text;
            }

            if (routeCode != null)
            {
                entry.substanceAdministration.routeCode = CreateCodedWithExtensionElement(routeCode);
            }

            if (!String.IsNullOrEmpty(low) || !String.IsNullOrEmpty(high))
            {
                entry.substanceAdministration.effectiveTime = new SXCM_TS[]
                                                                  {
                                                                      CreateIntervalTimestamp
                                                                      (
                                                                            low, 
                                                                            high, 
                                                                            null, 
                                                                            null,
                                                                            null, 
                                                                            null
                                                                       )
                                                                  };
            }
            if (effectiveTime != null)
            {
                entry.substanceAdministration.effectiveTime = new[]
                                                                  {
                                                                      new SXCM_TS
                                                                          {
                                                                              value = effectiveTime.ToString()
                                                                          }
                                                                  };
            }

            if (relationships != null)
            {
                entry.substanceAdministration.entryRelationship = relationships.ToArray();
            }

            if (subjects != null && subjects.Any())
            {
                entry.substanceAdministration.consumable.manufacturedProduct.subjectOf1 = subjects.ToArray();
            }

            if (repeatNumber.HasValue)
            {
                entry.substanceAdministration.repeatNumber = new IVL_INT
                {
                    ItemsElementName = new[] { ItemsChoiceType5.high },
                    Items = new INT[] { new IVXB_INT { value = repeatNumber.Value.ToString(CultureInfo.InvariantCulture) } }
                };
            }

            return entry;
        }

        /// <summary>
        /// The details of the healthcare encounter which instigated the creation of the discharge summary.
        /// </summary>
        /// <param name="eventDischargeSummary">The eventDischargeSummary</param>
        /// <param name="narrativeGenerator">The narrativeGenerator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentLegacy(SCSModel.DischargeSummary.Event eventDischargeSummary,
            INarrativeGenerator narrativeGenerator)
        {
            // For DS
            POCD_MT000040Component3 component = null;

            if (eventDischargeSummary != null)
            {
                var components = new List<POCD_MT000040Component5>();
                var entryList = new List<POCD_MT000040Entry>();

                // Begin Event section
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.16006", CodingSystem.NCTIS, "Event", "This section contains the following subsections: Clinical Summary, Problems/Diagnoses This Visit, Procedures and Diagnostic Investigations.")
                };

                if (eventDischargeSummary.CustomNarrativeEvent != null)
                    component.section.text = eventDischargeSummary.CustomNarrativeEvent;

                if (eventDischargeSummary.ClinicalSynopsis != null)
                {
                    components.Add(CreateComponent(eventDischargeSummary.ClinicalSynopsis, "102.15513.4.1.1",
                        narrativeGenerator));
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
                    components.Add(CreateComponentLegacy(eventDischargeSummary.DiagnosticInvestigations,
                        narrativeGenerator));
                }

                component.section.component = components.ToArray();
                component.section.entry = entryList.ToArray();
            }

            return component;
        }

        #endregion

    }
}



