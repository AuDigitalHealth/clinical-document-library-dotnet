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
using System.Globalization;
using System.Linq;
using CDA.Generator.Common.Common.Time.Enum;
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using CDA.Generator.Common.SCSModel.CeHR.Entities;
using CDA.Generator.Common.SCSModel.ConsumerAchievements.Entities;
using CDA.Generator.Common.SCSModel.MedicareOverview.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.HL7.CDA;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using CDA.Generator.Common.SCSModel.ServiceReferral.Entities;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using ProblemDiagnosis = Nehta.VendorLibrary.CDA.SCSModel.Common.ProblemDiagnosis;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This is the first version of the NEHTA narrative generator. 
    /// 
    /// It implements the INarrativeGenerator and is used with the CDAGenerator to build the narrative
    /// for each CDA Section.
    /// </summary>
    public partial class CDANarrativeGenerator : INarrativeGenerator
    {
        #region Constants

        /// <summary>
        /// This constant indicates if an entry has no entries
        /// </summary>
        public const String SECTIONEMPTYTEXT = "This section contains no entries.";

        /// <summary>
        /// This constant indicates if an exclusion statement is included
        /// </summary>
        public const String SECTIONEXCLUSIONSTATEMENT = "This section contains an Exclusion Statement.";

        private const String DELIMITER = "<CR>";
        private const String DELIMITERBREAK = "<BR>";
        private const String DELIMITERBOLD = "<B>";
        private const String DELIMITEREMAILSTART = "<MAIL>";
        private const String DELIMITEREMAILEND = "</MAIL>";
        private const String XCOLWIDTHDATE = "xColWidthPx170";

        #endregion

        /// <summary>
        /// This method creates the narrative for the subject of care
        /// </summary>
        /// <param name="subjectOfCareParticipation">subjectOfCareParticipation</param>
        /// <param name="patientId">patientId</param>
        /// <param name="showEntitlements">Show the entitlements for the subjectOfCare</param>
        /// <param name="earliestDateForFiltering">Earliest Date For Filtering</param>
        /// <param name="latestDateForFiltering">Latest Date For Filtering</param>
        /// <param name="specialty">List of specialties</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IParticipationSubjectOfCare subjectOfCareParticipation, String patientId,
            Boolean showEntitlements, ISO8601DateTime earliestDateForFiltering, ISO8601DateTime latestDateForFiltering,
            List<ICodableText> specialty = null)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();

            if (subjectOfCareParticipation != null && subjectOfCareParticipation.Participant != null &&
                subjectOfCareParticipation.Participant.Person != null)
            {
                var person = subjectOfCareParticipation.Participant.Person;

                if (
                    person.DateOfBirthCalculatedFromAge.HasValue ||
                    person.DateOfBirthAccuracyIndicator != null ||
                    person.Age.HasValue ||
                    person.AgeAccuracyIndicator != null ||
                    person.BirthPlurality.HasValue ||
                    person.DateOfDeathAccuracyIndicator != null ||
                    person.MothersOriginalFamilyName != null ||
                    person.SourceOfDeathNotification != null ||
                    person.InterpreterRequired != null ||
                    specialty != null
                )
                {
                    var columnHeaders = new List<string> {"Field", "Value"};

                    //Date of Birth calculated from age
                    if (person.DateOfBirthCalculatedFromAge.HasValue)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Date of Birth is Calculated From Age",
                                (person.DateOfBirthCalculatedFromAge))
                        );

                    //Date of Birth accuracy indicatory
                    if (person.DateOfBirthAccuracyIndicator != null)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Date of Birth Accuracy Indicator",
                                person.DateOfBirthAccuracyIndicator)
                        );

                    //Age Accuracy Indicator
                    if (person.AgeAccuracyIndicator.HasValue)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Age Accuracy Indicator", person.AgeAccuracyIndicator)
                        );

                    //Birth Plurality
                    if (person.BirthPlurality.HasValue)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Birth Plurality", person.BirthPlurality, null)
                        );

                    //Age
                    if (person.Age.HasValue)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Age", person.Age,
                                person.AgeUnitOfMeasure.HasValue
                                    ? person.AgeUnitOfMeasure.Value.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Comment)
                                    : AgeUnitOfMeasure.Year.GetAttributeValue<NameAttribute, String>(x => x.Comment))
                        );

                    //Date of Death accuracy indicatory
                    if (person.DateOfDeathAccuracyIndicator != null)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Date of Death Accuracy Indicator",
                                person.DateOfDeathAccuracyIndicator)
                        );

                    // Source Of Death Notification
                    if (person.SourceOfDeathNotification.HasValue)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Source Of Death Notification",
                                person.SourceOfDeathNotification.GetAttributeValue<NameAttribute, String>(x => x.Name))
                        );

                    // MothersOriginalFamilyName
                    if (person.MothersOriginalFamilyName != null)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Mothers Original Family Name",
                                BuildPersonNames(new List<IPersonName> {person.MothersOriginalFamilyName}))
                        );

                    // Earliest Date For FilteringCDANarrativeGenerator
                    if (earliestDateForFiltering != null)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Earliest Date for Filtering", earliestDateForFiltering)
                        );

                    // Latest Date For Filtering
                    if (latestDateForFiltering != null)
                        narrative.Add
                        (
                            CreateNarrativeEntry("Latest Date for Filtering", latestDateForFiltering)
                        );

                    // Latest Date For Filtering
                    if (person.InterpreterRequired != null && person.InterpreterRequired.PreferredLanguage != null &&
                        person.InterpreterRequired.PreferredLanguage.Any())
                        narrative.Add
                        (
                            CreateNarrativeEntry("Interpreter Required",
                                string.Join<string>(", ", person.InterpreterRequired.PreferredLanguage))
                        );

                    // specialty
                    if (specialty != null)
                    {
                        var spec = specialty.Select(x => x.DisplayName).ToList();
                        narrative.Add
                        (
                            CreateNarrativeEntry("Specialty", string.Join<string>(", ", spec))
                        );
                    }


                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Observations details",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrative
                        )
                    );
                }
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the adverse subject reactions section
        /// </summary>
        /// <param name="allergiesAndAdverseReactions">allergiesAndAdverseReactions</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(List<Reaction> allergiesAndAdverseReactions)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();

            if (allergiesAndAdverseReactions != null && allergiesAndAdverseReactions.Any())
            {
                foreach (var reaction in allergiesAndAdverseReactions)
                {
                    var manfestationDesc = String.Empty;

                    if (reaction.ReactionEvent != null)
                        if (reaction.ReactionEvent.Manifestations != null &&
                            reaction.ReactionEvent.Manifestations.Any())
                        {
                            manfestationDesc = reaction.ReactionEvent.Manifestations.Aggregate(manfestationDesc,
                                (current, manifestation) => current + (manifestation.NarrativeText + DELIMITER));
                        }

                    narrative.Add(
                        new List<string>
                        {
                            reaction.SubstanceOrAgent != null
                                ? reaction.SubstanceOrAgent.NarrativeText
                                : String.Empty,
                            manfestationDesc
                        }
                    );
                }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Adverse Reactions",
                        null,
                        new[]
                        {
                            "Substance/Agent",
                            "Manifestations"
                        },
                        new string[0],
                        narrative
                    )
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Creates the narrative section for medications.
        /// </summary>
        /// <param name="medicationsSML"></param>
        /// <param name="isCurrentMedications"></param>
        /// <returns></returns>
        public StrucDocText CreateNarrative(MedicationListSML medicationsSML, bool isCurrentMedications)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<string>>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (medicationsSML != null && medicationsSML.MedicineItem.Any())
            {
                if (isCurrentMedications)
                {
                    // healthcare setting
                    List<List<string>> hcsettingList = new List<List<string>>();
                    if (medicationsSML.Encounter != null && medicationsSML.Encounter.EncounterType != null)
                    {
                        List<string> setting = new List<string>();
                        setting.Add(medicationsSML.Encounter.EncounterType.DisplayName);
                        hcsettingList.Add(setting);

                        strucDocTableList.Add
                        (
                            PopulateTable
                            (
                                "Healthcare setting",
                                null,
                                new[] { "Description" },
                                new string[0],
                                hcsettingList
                            )
                        );
                    }


                    // Current medications
                    var narativeHeader = new List<string>
                    {
                        "Medicine",
                        "Brand Name",
                        "Direction",
                        "Medicine Purpose",
                        "Medicine Status",
                        "Expected End Date",
                        "Special Instructions",
                        "Medicine Image"
                    };

                    foreach (MedicineItemSML medicineItemSml in medicationsSML.MedicineItem)
                    {
                        string medicinePurposeText = null;
                        if (medicineItemSml.MedicinePurpose != null)
                        {
                            foreach (ICodableText clinicalIndication in medicineItemSml.MedicinePurpose)
                            {
                                medicinePurposeText += clinicalIndication.NarrativeText + DELIMITER;
                            }
                        }

                        string commentNoteText = null;
                        if (medicineItemSml.AdditionalComments != null)
                        {
                            foreach (NoteSML additionalComment in medicineItemSml.AdditionalComments)
                            {
                                commentNoteText += additionalComment.NoteText + DELIMITER;
                            }
                        }

                        string instructions = string.Empty;
                        if (medicineItemSml.Dosage != null)
                        {
                            instructions = string.Join(DELIMITER, medicineItemSml.Dosage.Select(d => d.Instructions));
                        }

                        narrative.Add(new List<string>
                        {
                            medicineItemSml.Medication.ItemCode.NarrativeText,
                            medicineItemSml.Medication.BrandName ?? string.Empty,
                            instructions,
                            medicinePurposeText ?? string.Empty,
                            medicineItemSml.MedicationStatus.NarrativeText,
                            medicineItemSml.EffectiveTimeTakenOrNot?.NarrativeText() ?? string.Empty,
                            commentNoteText ?? string.Empty,
                            medicineItemSml.Medication.Image ?? string.Empty
                        });
                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Current Medications",
                            null,
                            narativeHeader.ToArray(),
                            new string[0],
                            narrative
                        )
                    );

                    List<List<string>> notesList = new List<List<string>>();
                    if (medicationsSML.AdditionalListComments != null)
                    {
                        foreach (NoteSML noteSml in medicationsSML.AdditionalListComments)
                        {
                            List<string> notes = new List<string>();
                            notes.Add(noteSml.NoteText);
                            notesList.Add(notes);
                        }

                        strucDocTableList.Add
                        (
                            PopulateTable
                            (
                                "Current Medication Notes",
                                null,
                                new[] {"Description"},
                                new string[0],
                                notesList
                            )
                        );
                    }
                }
                else
                {
                    // Ceased medications
                    var narativeHeader = new List<string>
                    {
                        "Ceased Medicine",
                        "Reason for Ceasing Medication",
                        "Ceased Date"
                    };

                    foreach (MedicineItemSML medicineItemSml in medicationsSML.MedicineItem)
                    {
                        string reasonNotTaken = null;
                        if (medicineItemSml.ChangeDescription != null && medicineItemSml.ChangeDescription.Any())
                        {
                            reasonNotTaken += medicineItemSml.ChangeDescription + DELIMITER;
                        }

                        narrative.Add(new List<string>
                        {
                            medicineItemSml.Medication.ItemCode.NarrativeText,
                            reasonNotTaken, 
                            medicineItemSml.EffectiveTimeTakenOrNot?.NarrativeText()
                        });
                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Ceased Medications",
                            null,
                            narativeHeader.ToArray(),
                            new string[0],
                            narrative
                        )
                    );
                }
            }

            var strucDocText = new StrucDocText();

            // Table
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the adverse subject reactions section
        /// </summary>
        /// <param name="adverseSubstanceReactions">allergiesAndAdverseReactions</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IAdverseReactions adverseSubstanceReactions)
        {
            // Empty reason specified
            if (adverseSubstanceReactions.EmptyReasonStatement != null)
            {
                return new StrucDocText
                {
                    paragraph = new StrucDocParagraph[]
                    {
                        new StrucDocParagraph
                        {
                            // TODO how is the empty reason formatted
                            Text = new string[]
                            {
                                adverseSubstanceReactions.EmptyReasonStatement.OriginalText,
                                adverseSubstanceReactions.EmptyReasonStatement.Value.ToString()
                            }
                        }, 
                    }
                };
            }

            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            var narativeHeader = new List<string>
            {
                "Substance/Agent",
                "Reaction Type",
                "Reaction",
                "Reaction Onset Date"
            };

            if (adverseSubstanceReactions != null)
            {
                // Adverse Substance Reaction
                if (adverseSubstanceReactions.AdverseSubstanceReaction != null)
                {
                    foreach (var adverserReaction in adverseSubstanceReactions.AdverseSubstanceReaction)
                    {
                        var manfestationDesc = String.Empty;

                        var reactionType = String.Empty;

                        if (adverserReaction.ReactionEvent != null)
                        {
                            if (adverserReaction.ReactionEvent.Manifestations != null)
                            {
                                foreach (var manifestation in adverserReaction.ReactionEvent.Manifestations)
                                {
                                    manfestationDesc += manifestation.NarrativeText + DELIMITER;
                                }
                            }

                            if (adverserReaction.ReactionEvent.ReactionType != null)
                            {
                                reactionType = adverserReaction.ReactionEvent.ReactionType.NarrativeText;
                            }
                        }

                        CdaIntervalOrAge intervalOrAge = adverserReaction.ReactionEvent.ReactionOnsetDate;
                        string onsetDate = null;
                        // Check if a date is there at all
                        if (intervalOrAge != null)
                        {
                            if (intervalOrAge.Interval != null)
                            {
                                onsetDate = intervalOrAge.Interval.NarrativeText();
                            }
                            else if (!string.IsNullOrWhiteSpace(intervalOrAge.Value))
                            {
                                if (int.TryParse(intervalOrAge.Value, out int ageValue))
                                {
                                    string units = intervalOrAge.Unit.ToString();
                                    if (ageValue > 1)
                                    {
                                        units = $"{units}s";
                                    }

                                    onsetDate = $"Aged: {intervalOrAge.Value} {units}";
                                }
                            }
                        }

                        narrative.Add(
                            new List<string>
                            {
                                adverserReaction.SubstanceOrAgent != null
                                    ? adverserReaction.SubstanceOrAgent.NarrativeText
                                    : String.Empty,
                                reactionType,
                                manfestationDesc,
                                onsetDate ?? string.Empty
                            }
                        );
                    }

                    StripEmptyColoums(ref narativeHeader, ref narrative, new List<int> {2});

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Adverse Reactions",
                            null,
                            narativeHeader.ToArray(),
                            new string[0],
                            narrative
                        )
                    );
                }

                // Exclusion statement
                if (adverseSubstanceReactions.ExclusionStatement != null)
                {
                    narrativeParagraph.Add(CreateExclusionStatementNarrative("Adverse Reactions",
                        adverseSubstanceReactions.ExclusionStatement));
                }

            }

            var strucDocText = new StrucDocText();

            // Table
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications"></param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(List<IMedication> medications)
        {
            var narrative = new List<List<String>>();
            var strucDocTableList = new List<StrucDocTable>();

            var narativeHeader = new List<string>
            {
                "Medication",
                "Directions",
                "Clinical Indication"
            };

            if (medications != null && medications.Any())
            {
                narrative = new List<List<String>>();

                foreach (var medication in medications)
                {
                    //medicine_list + clinical Indication
                    if (medication.Medicine != null)
                    {

                        var medicationList = new List<String>
                        {
                            medication.Medicine.NarrativeText,
                            medication.Directions != null ? medication.Directions.NarrativeText : null,
                            medication.ClinicalIndication
                        };

                        // Dynamical add comment is it is not null
                        if (!medication.Comment.IsNullOrEmptyWhitespace())
                        {
                            medicationList.Add(medication.Comment);

                            if (narativeHeader.Contains("Comment") == false)
                            {
                                narativeHeader.Add("Comment");
                            }
                        }

                        //medicine_list + clinical Indication
                        narrative.Add(medicationList);
                    }
                }

                // Close empty cells
                foreach (var narrativeEntry in narrative.Where(narrativeEntry =>
                    narativeHeader.Contains("Comment") && narrativeEntry.Count == 3))
                {
                    narrativeEntry.Add(string.Empty);
                }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Medications",
                        null,
                        narativeHeader.ToArray(),
                        new string[0],
                        narrative
                    )
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="reviewedMedications"></param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IMedications reviewedMedications)
        {
            var narrative = new List<List<string>>();
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (reviewedMedications != null)
            {
                if (reviewedMedications.Medications != null)
                {
                    narrative = new List<List<String>>();

                    var narativeHeader = new List<string>()
                    {
                        "Medication",
                        "Directions",
                        "Clinical Indication"
                    };


                    foreach (var medication in reviewedMedications.Medications)
                    {

                        //medicine_list + clinical Indication
                        if (medication.Medicine != null)
                        {
                            var medicationList = new List<String>
                            {
                                medication.Medicine.NarrativeText,
                                medication.Directions != null ? medication.Directions.NarrativeText : null,
                                medication.ClinicalIndication
                            };

                            // Dynamical add comment is it is not null
                            if (!medication.Comment.IsNullOrEmptyWhitespace())
                            {
                                medicationList.Add(medication.Comment);

                                if (narativeHeader.Contains("Comment") == false)
                                {
                                    narativeHeader.Add("Comment");
                                }
                            }

                            //medicine_list + clinical Indication
                            narrative.Add(medicationList);
                        }
                    }

                    // Close empty cells
                    foreach (var narrativeEntry in narrative.Where(narrativeEntry =>
                        narativeHeader.Contains("Comment") && narrativeEntry.Count == 3))
                    {
                        narrativeEntry.Add(string.Empty);
                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Medications",
                            null,
                            narativeHeader.ToArray(),
                            null,
                            narrative
                        )
                    );
                }

                // Exclusions
                if (reviewedMedications.ExclusionStatement != null)
                {
                    narrativeParagraph.Add(CreateExclusionStatementNarrative("Medications",
                        reviewedMedications.ExclusionStatement));
                }
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">medications</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IMedicationsEReferral medications)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (medications != null)
            {
                if (medications.MedicationsList != null)
                {
                    narrative = new List<List<String>>();

                    foreach (var medication in medications.MedicationsList)
                    {
                        if (medication != null)
                        {
                            // Medications
                            narrative.Add(
                                new List<string>
                                {
                                    medication.Medicine != null ? medication.Medicine.NarrativeText : String.Empty,
                                    medication.Directions != null ? medication.Directions.NarrativeText : String.Empty,
                                }
                            );
                        }

                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Medications",
                            null,
                            new[]
                            {
                                "Medication",
                                "Directions"
                            },
                            new string[0],
                            narrative
                        )
                    );
                }

                // Exclusion statement
                if (medications.ExclusionStatement != null)
                {
                    narrativeParagraph.Add(
                        CreateExclusionStatementNarrative("Medications", medications.ExclusionStatement));
                }

            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">IMedicationsSpecialistLetter</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IMedicationsSpecialistLetter medications)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (medications != null)
            {
                if (medications.MedicationsList != null)
                {
                    var narativeHeader = new List<string>()
                    {
                        "Medication",
                        "Directions",
                        "Clinical Indication",
                        "Change Status",
                        "Change Description",
                        "Change Reason",
                        "Comment"
                    };

                    narrative = new List<List<String>>();

                    foreach (var medication in medications.MedicationsList)
                    {
                        //string changeStatus;
                        //if (medication.ChangeType != null && medication.ChangeType.NullFlavour != null) // because if there is no change status, the fact of whether this is a recommendation or change is irrelevant
                        //  changeStatus = "No change";
                        //else
                        //{
                        //  changeStatus = medication.ChangeType != null ? medication.ChangeType.NarrativeText : string.Empty;
                        //  // if there's no change, or recommendation or change value, we don't say anything about it
                        //  if (medication.ChangeType != null && (medication.ChangeType.Code != ChangeType.Unchanged.GetAttributeValue<NameAttribute, string>(x => x.Code) || 
                        //                                       medication.ChangeType.Code != ChangeType.Unchanged.GetAttributeValue<NameAttribute, string>(x => x.Identifier)))
                        //  {
                        //      if (!(medication.RecommendationOrChange != null && medication.RecommendationOrChange.Code == RecomendationOrChange.TheChangeHasBeenMade.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                        //      changeStatus = "Recommendation: " + changeStatus;
                        //  }
                        //}

                        var medicationList = new List<String>
                        {
                            medication.Medicine != null ? medication.Medicine.NarrativeText : null,
                            medication.Directions != null ? medication.Directions.NarrativeText : null,
                            medication.ClinicalIndication,
                            medication.ChangeStatus != null ? medication.ChangeStatus.NarrativeText : null,
                            !medication.ChangeDescription.IsNullOrEmptyWhitespace()
                                ? medication.ChangeDescription
                                : null,
                            medication.ChangeReason != null ? medication.ChangeReason.NarrativeText : null,
                            !medication.Comment.IsNullOrEmptyWhitespace() ? medication.Comment : null
                        };

                        narrative.Add(medicationList);
                    }

                    StripEmptyColoums(ref narativeHeader, ref narrative, new List<int> {3, 4, 5, 6});

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Medications",
                            null,
                            narativeHeader.ToArray(),
                            new string[0],
                            narrative
                        )
                    );
                }

                // Exclusions
                if (medications.ExclusionStatement != null)
                {
                    narrativeParagraph.Add(
                        CreateExclusionStatementNarrative("Medications", medications.ExclusionStatement));
                }
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the medical history section
        /// </summary>
        /// <param name="medicalHistory">medicalHistory</param>
        /// <param name="showExclusionStatements">This indicates wether exclusion statements should be shown in the narrative</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(MedicalHistory medicalHistory, bool showExclusionStatements)
        {
            var strucDocItemList = new List<StrucDocItem>();
            StrucDocTable table = null;

            if (medicalHistory != null)
            {
                List<ProblemDiagnosis> problemDiagnosisList = null;
                if (medicalHistory.ProblemDiagnosis != null)
                {
                    problemDiagnosisList = medicalHistory.ProblemDiagnosis.ConvertAll(x => x as ProblemDiagnosis);
                }

                List<Procedure> proceduresList = null;
                if (medicalHistory.Procedures != null)
                {
                    proceduresList = medicalHistory.Procedures.ConvertAll(x => x);
                }

                List<MedicalHistoryItem> medicalHistoryItemList = null;
                if (medicalHistory.MedicalHistoryItems != null)
                {
                    medicalHistoryItemList =
                        medicalHistory.MedicalHistoryItems.ConvertAll(x => x as MedicalHistoryItem);
                }

                table = CreateNarrative(problemDiagnosisList, proceduresList, medicalHistoryItemList, true);

                if (showExclusionStatements)
                {
                    // PROCEDURE EXCLUSIONS
                    if (medicalHistory.ProceduresExclusionStatement != null)
                    {
                        strucDocItemList.Add(CreateExclusionStatement("Procedures",
                            medicalHistory.ProceduresExclusionStatement));
                    }

                    // PROBLEM DIAGNOSIS EXCLUSIONS
                    if (medicalHistory.ProblemDiagnosisExclusionStatement != null)
                    {
                        strucDocItemList.Add(CreateExclusionStatement("Problem Diagnosis",
                            medicalHistory.ProblemDiagnosisExclusionStatement));
                    }
                }
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (table != null)
            {
                strucDocText.table = new[] {table};
            }

            // Narrative Paragraph
            if (strucDocItemList.Any())
            {
                strucDocText.list = new[] {new StrucDocList {item = strucDocItemList.ToArray()}};
            }

            if (table == null && !strucDocItemList.Any())
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the reviewed immunisations section
        /// </summary>
        /// <param name="immunisations">reviewedImmunisations</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(Immunisations immunisations)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (immunisations != null)
            {

                //ADMINISTERED IMMUNISATION
                if (immunisations.AdministeredImmunisation != null && immunisations.AdministeredImmunisation.Any())
                {
                    strucDocTableList.AddRange
                    (
                        CreateNarrativeEntry(immunisations.AdministeredImmunisation)
                    );
                }

                //EXCLUSION STATEMENT
                if (immunisations.ExclusionStatement != null)
                {
                    narrativeParagraph.Add(
                        CreateExclusionStatementNarrative("Immunisations", immunisations.ExclusionStatement));
                }
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the response details section
        /// </summary>
        /// <param name="responseDetails">IResponseDetails</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(IResponseDetails responseDetails)
        {
            var strucDocItemList = new List<StrucDocItem>();

            if (responseDetails != null)
            {
                // ResponseDetails
                if (responseDetails.Procedures != null && responseDetails.Procedures.Any())
                {
                    foreach (var procedure in responseDetails.Procedures)
                    {
                        if (procedure.ProcedureName != null)
                            strucDocItemList.Add(new StrucDocItem
                            {
                                Text = new[]
                                    {string.Format("{0} {1}", procedure.ProcedureName.NarrativeText, "(procedure)")}
                            });
                    }
                }

                //Diagnoses 
                if (responseDetails.Diagnoses != null)
                {
                    foreach (var diagnoses in responseDetails.Diagnoses)
                    {
                        if (diagnoses != null)
                            strucDocItemList.Add(new StrucDocItem
                                {Text = new[] {string.Format("{0} {1}", diagnoses.NarrativeText, "(diagnoses)")}});
                    }
                }

                //Other Diagnoses 
                if (responseDetails.OtherDiagnosisEntries != null && responseDetails.OtherDiagnosisEntries.Any())
                {
                    foreach (var otherDiagnosisEntries in responseDetails.OtherDiagnosisEntries)
                    {
                        if (otherDiagnosisEntries != null)
                            strucDocItemList.Add(new StrucDocItem
                                {Text = new[] {string.Format("{0} {1}", otherDiagnosisEntries, "(other entry)")}});
                    }
                }
            }

            var responseNarrative = new List<String>();
            var strucDocTableList = new List<StrucDocTable>();
            responseNarrative.Add
            (
                !responseDetails.ResponseNarrative.IsNullOrEmptyWhitespace()
                    ? responseDetails.ResponseNarrative
                    : String.Empty
            );

            strucDocTableList.Add
            (
                PopulateTable
                (
                    "Response Narrative",
                    null,
                    null,
                    null,
                    new List<List<String>> {responseNarrative}
                )
            );


            var strucDocText = new StrucDocText();

            if (strucDocItemList.Any())
            {
                strucDocText.list = new[]
                {
                    new StrucDocList
                    {
                        caption = new StrucDocCaption {Text = new[] {"Diagnoses"}},
                        item = strucDocItemList.ToArray()
                    }
                };
            }

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the recommendations section
        /// </summary>
        /// <param name="recommendations">IRecommendations</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(IRecommendations recommendations)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (recommendations != null)
            {
                if (recommendations.Recommendation != null && recommendations.Recommendation.Any())
                {
                    narrative = new List<List<String>>();
                    var narrativeEntry = new List<String>();

                    recommendations.Recommendation.ForEach
                    (
                        recommendation =>
                        {
                            narrativeEntry = new List<String>();
                            narrativeEntry.Add(recommendation.Narrative);
                            narrativeEntry.Add(CreateDuration(recommendation.TimeFrame));

                            var addressee = string.Empty;

                            if (recommendation.Addressee != null
                                && recommendation.Addressee.Participant != null)
                            {
                                if (recommendation.Addressee.Participant.Person != null)
                                    addressee +=
                                        BuildPersonNames(recommendation.Addressee.Participant.Person.PersonNames);
                                else
                                {
                                    if (recommendation.Addressee.Participant.Organisation != null)
                                        addressee += recommendation.Addressee.Participant.Organisation.Name;
                                }

                                if (recommendation.Addressee.Role != null)
                                {
                                    addressee += string.Format(" ({0}) ", recommendation.Addressee.Role.NarrativeText);
                                }

                                addressee += CreateAddress(null,
                                    recommendation.Addressee.Participant.ElectronicCommunicationDetails);
                            }

                            narrativeEntry.Add(addressee);
                            narrative.Add(narrativeEntry);
                        }
                    );



                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Recommendations",
                            null,
                            new[] {"Recommendation", "Time frame", "Addressee"},
                            new String[] { },
                            narrative
                        )
                    );
                }

                // Exclusions
                if (recommendations.ExclusionStatement != null)
                {
                    narrativeParagraph.Add(CreateExclusionStatementNarrative("Exclusion Statement",
                        recommendations.ExclusionStatement));
                }
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the other Test Result section
        /// </summary>
        /// <param name="otherTestResult">OtherTestResult</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(OtherTestResult otherTestResult)
        {
            var strucDocText = new StrucDocText();
            var narrative = new List<List<Object>>();
            var strucDocTableList = new List<StrucDocTable>();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();
            var header = new[] {"Field", "Value"};

            if (otherTestResult != null)
            {
                // Report Date
                if (otherTestResult.ReportDate != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Date",
                            otherTestResult.ReportDate.NarrativeText()
                        }
                    );

                // Report Name
                if (otherTestResult.ReportName != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Name",
                            otherTestResult.ReportName.NarrativeText
                        }
                    );

                // Report Status 
                if (otherTestResult.ReportStatus != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Status",
                            otherTestResult.ReportStatus.NarrativeText
                        }
                    );

                // Report Content ExternalData
                // PW: 30/5/17 Should be LinkHtml 
                // PW: 23/01/19 Changed back to Encapsulated Data as added the observationMedia section in
                if (otherTestResult.ReportContent != null && otherTestResult.ReportContent.ExternalData != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Content",
                            CreateEncapsulatedData(otherTestResult.ReportContent.ExternalData, ref renderMultiMediaList)
                            //CreateSimpleHtmlLink(otherTestResult.ReportContent.ExternalData)
                        }
                    );

                // Report Content Text
                if (otherTestResult.ReportContent != null &&
                    !otherTestResult.ReportContent.Text.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Content",
                            otherTestResult.ReportContent.Text
                        }
                    );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Other Test Result",
                        null,
                        header,
                        null,
                        narrative
                    )
                );

                strucDocText.table = strucDocTableList.ToArray();

                if (renderMultiMediaList.Any())
                {
                    strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
                }
            }

            return strucDocText;

        }

        /// <summary>
        /// This method creates the narrative for the diagnostic investigations section
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigations</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(IDiagnosticInvestigations diagnosticInvestigations)
        {
            var strucDocTableList = new List<StrucDocTable>();

            if (diagnosticInvestigations != null)
            {
                //This doesn't do anything as this section contains sub sections that render the narrative.
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the pathology test result section
        /// </summary>
        /// <param name="pathologyTestResult">pathologyTestResult</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(PathologyTestResult pathologyTestResult)
        {
            var strucDocText = new StrucDocText();
            var narrative = new List<List<Object>>();
            var strucDocTableList = new List<StrucDocTable>();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();
            var header = new[] {"Field", "Value"};

            // Simple narrative
            if (!pathologyTestResult.XPreNarrative.IsNullOrEmptyWhitespace())
            {
                //PathologyTestResult TestResultName 
                if (pathologyTestResult.TestResultName != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Test Name",
                            pathologyTestResult.TestResultName.NarrativeText
                        }
                    );

                //Overall Test Result Status 
                if (pathologyTestResult.OverallTestResultStatus != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Result Status",
                            pathologyTestResult.OverallTestResultStatus.NarrativeText
                        }
                    );

                //Overall Test Result Status 
                if (pathologyTestResult.ObservationDateTime != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Date Time",
                            pathologyTestResult.ObservationDateTime.NarrativeText()
                        }
                    );

                //Comment 
                if (!pathologyTestResult.TestComment.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Comment",
                            pathologyTestResult.TestComment
                        }
                    );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Pathology Test Result",
                        null,
                        header,
                        null,
                        narrative
                    )
                );

                strucDocTableList.Add(populateTablexPreNarrative(pathologyTestResult.XPreNarrative));

                strucDocText.table = strucDocTableList.ToArray();

                return strucDocText;
            }

            // Complex Narrative
            if (pathologyTestResult != null)
            {
                if (pathologyTestResult.TestSpecimenDetail != null)
                {
                    strucDocTableList.AddRange
                    (
                        CreateNarrativeEntry(pathologyTestResult.TestSpecimenDetail, "Pathology Test Specimen Detail",
                            ref narrative, ref renderMultiMediaList)
                    );
                }

                if (pathologyTestResult.ResultGroup != null)
                {
                    List<List<Object>> pathologyTestResultsList;

                    //Create Organizer and relationships, these are nested within the observation entry below
                    foreach (var testResultGroup in pathologyTestResult.ResultGroup)
                    {
                        pathologyTestResultsList = new List<List<Object>>();

                        if (testResultGroup.Results != null)
                        {
                            foreach (var pathologyTestResults in testResultGroup.Results)
                            {
                                var resultValueReferenceRangeDetails = String.Empty;
                                var pathologyTestResultsComments = String.Empty;

                                if (pathologyTestResults.ResultValueReferenceRangeDetails != null)
                                {
                                    foreach (var resultValueReferenceRange in pathologyTestResults
                                        .ResultValueReferenceRangeDetails)
                                    {
                                        resultValueReferenceRangeDetails +=
                                            resultValueReferenceRange.Range.NarrativeText +
                                            (resultValueReferenceRange.ResultValueReferenceRangeMeaning != null
                                                ? string.Format(" ({0})",
                                                    resultValueReferenceRange.ResultValueReferenceRangeMeaning
                                                        .NarrativeText)
                                                : string.Empty) + DELIMITER;
                                    }
                                }

                                if (pathologyTestResults.Comments != null)
                                {
                                    pathologyTestResultsComments =
                                        pathologyTestResults.Comments.Aggregate(pathologyTestResultsComments,
                                            (current, comment) => current + (comment + DELIMITER));
                                }

                                if (!testResultGroup.ResultGroupName.NarrativeText.IsNullOrEmptyWhitespace())
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                            {"Result Group Name", testResultGroup.ResultGroupName.NarrativeText}
                                    );

                                if (pathologyTestResults.ResultName != null)
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                            {"Test Result Name", pathologyTestResults.ResultName.NarrativeText}
                                    );

                                if (pathologyTestResults.ResultValue != null)
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                        {
                                            "Test Result Value",

                                            (pathologyTestResults.ResultValue.ValueAsCodableText == null
                                                ? String.Empty
                                                : pathologyTestResults.ResultValue.ValueAsCodableText.NarrativeText +
                                                  DELIMITER) +
                                            (pathologyTestResults.ResultValue.TestResultValue == null
                                                ? String.Empty
                                                : pathologyTestResults.ResultValue.TestResultValue.NarrativeText +
                                                  DELIMITER) +
                                            (pathologyTestResults.ResultValue.ValueAsQuantityRange == null
                                                ? String.Empty
                                                : pathologyTestResults.ResultValue.ValueAsQuantityRange.NarrativeText +
                                                  DELIMITER)
                                        }
                                    );

                                if (pathologyTestResults.NormalStatus.HasValue)
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                        {
                                            "Reference Ranges - Normal Status",
                                            pathologyTestResults.NormalStatus.Value
                                                .GetAttributeValue<NameAttribute, string>(x => x.Name)
                                        }
                                    );


                                if (!resultValueReferenceRangeDetails.IsNullOrEmptyWhitespace())
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                        {
                                            "Reference Ranges - Reference Range Details",
                                            resultValueReferenceRangeDetails
                                        }
                                    );

                                if (!pathologyTestResultsComments.IsNullOrEmptyWhitespace())
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object> {"Test Result Comment", pathologyTestResultsComments}
                                    );

                                if (!pathologyTestResults.ReferenceRangeGuidance.IsNullOrEmptyWhitespace())
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                            {"Reference Range Guidance", pathologyTestResults.ReferenceRangeGuidance}
                                    );

                                if (pathologyTestResults.ResultStatus != null)
                                    pathologyTestResultsList.Add
                                    (
                                        new List<Object>
                                            {"Result Status", pathologyTestResults.ResultStatus.NarrativeText}
                                    );


                                var testResulColumnHeaders = new List<string> {"Field", "Value"};

                                strucDocTableList.Add
                                (
                                    PopulateTable
                                    (
                                        "Individual Test Result Group",
                                        null,
                                        testResulColumnHeaders.ToArray(),
                                        null,
                                        pathologyTestResultsList
                                    )
                                );

                                pathologyTestResultsList = new List<List<object>>();
                            }
                        }


                        if (testResultGroup.ResultGroupSpecimenDetail != null)
                        {
                            var specimenDetail = CreateNarrativeEntry(
                                new List<SpecimenDetail> {testResultGroup.ResultGroupSpecimenDetail},
                                "Result Group Specimen Detail", ref narrative, ref renderMultiMediaList);

                            if (specimenDetail != null)
                            {
                                strucDocTableList.Add(specimenDetail.FirstOrDefault());
                            }
                        }

                    }
                }

                var pathologicalDiagnosis = String.Empty;
                if (pathologyTestResult.PathologicalDiagnosis != null)
                {
                    foreach (var diagnosis in pathologyTestResult.PathologicalDiagnosis)
                    {
                        pathologicalDiagnosis += diagnosis.NarrativeText + DELIMITER;
                    }
                }

                var columnHeaders = new List<string> {"Field", "Value"};

                var narrativepathologyTestResult = new List<List<Object>>();

                // Observation Date Time
                if (pathologyTestResult.ObservationDateTime != null)
                    narrativepathologyTestResult.Add
                    (
                        new List<Object>
                            {"Observation DateTime", pathologyTestResult.ObservationDateTime.NarrativeText()}
                    );

                // Observation Date Time
                if (pathologyTestResult.TestResultName != null)
                    narrativepathologyTestResult.Add
                    (
                        new List<Object>
                            {"Pathology Test Result Name", pathologyTestResult.TestResultName.NarrativeText}
                    );

                // Diagnostic Service
                if (pathologyTestResult.DiagnosticService.HasValue)
                    narrativepathologyTestResult.Add
                    (
                        new List<Object>
                        {
                            "Diagnostic Service",
                            pathologyTestResult.DiagnosticService.Value.GetAttributeValue<NameAttribute, String>(x =>
                                x.Name)
                        }
                    );

                // Overall Pathology Test Result Status
                if (pathologyTestResult.OverallTestResultStatus != null)
                    narrativepathologyTestResult.Add
                    (
                        new List<Object>
                        {
                            "Overall Pathology Test Result Status",
                            pathologyTestResult.OverallTestResultStatus.NarrativeText
                        }
                    );

                // Clinical Information Provided
                if (!pathologyTestResult.ClinicalInformationProvided.IsNullOrEmptyWhitespace())
                    narrativepathologyTestResult.Add
                    (
                        new List<Object>
                            {"Clinical Information Provided", pathologyTestResult.ClinicalInformationProvided}
                    );

                // Pathological Diagnosis
                if (pathologyTestResult.PathologicalDiagnosis != null &&
                    pathologyTestResult.PathologicalDiagnosis.Any())
                    narrativepathologyTestResult.Add
                    (
                        new List<Object> {"Pathological Diagnosis", pathologicalDiagnosis}
                    );

                // Pathology Test Conclusion
                if (!pathologyTestResult.Conclusion.IsNullOrEmptyWhitespace())
                    narrativepathologyTestResult.Add
                    (
                        new List<Object> {"Pathology Test Conclusion", pathologyTestResult.Conclusion}
                    );

                // Test Comment
                if (!pathologyTestResult.TestComment.IsNullOrEmptyWhitespace())
                    narrativepathologyTestResult.Add
                    (
                        new List<Object> {"Test Comment", pathologyTestResult.TestComment}
                    );


                // Test Result Representation - PW: 30/5/17 Should be LinkHtml
                if (pathologyTestResult.TestResultRepresentation != null)
                    narrativepathologyTestResult.Add
                    (
                        //new List<Object> { "Test Result Representation", CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation, ref renderMultiMediaList) }
                        new List<Object>
                        {
                            "Test Result Representation",
                            CreateSimpleHtmlLink(pathologyTestResult.TestResultRepresentation.ExternalData)
                        }
                    );


                if (pathologyTestResult.TestRequestDetails != null && pathologyTestResult.TestRequestDetails.Any())
                {
                    var testRequestDetails = string.Empty;
                    foreach (var requestDetails in pathologyTestResult.TestRequestDetails)
                    {
                        if (requestDetails.TestsRequestedName != null && requestDetails.TestsRequestedName.Any())
                        {
                            foreach (var testsRequested in requestDetails.TestsRequestedName)
                            {
                                if (requestDetails.TestsRequestedName != null &&
                                    requestDetails.TestsRequestedName.Any())
                                {
                                    testRequestDetails += testsRequested.NarrativeText + DELIMITER;
                                }

                            }
                        }
                    }


                    string personName = null;
                    // Provider Person Name
                    if (pathologyTestResult.ReportingPathologist != null &&
                        pathologyTestResult.ReportingPathologist.Participant != null &&
                        pathologyTestResult.ReportingPathologist.Participant.Person != null)
                        personName =
                            BuildPersonNames(pathologyTestResult.ReportingPathologist.Participant.Person.PersonNames);

                    // Reporting Pathologist
                    if (!personName.IsNullOrEmptyWhitespace())
                        narrativepathologyTestResult.Add
                        (
                            new List<Object> {"Reporting Pathologist - Person Name", personName}
                        );

                    // Address
                    if ((pathologyTestResult.ReportingPathologist != null &&
                         pathologyTestResult.ReportingPathologist.Participant != null &&
                         pathologyTestResult.ReportingPathologist.Participant.Addresses != null &&
                         pathologyTestResult.ReportingPathologist.Participant.Addresses.Any()) ||
                        pathologyTestResult.ReportingPathologist != null && pathologyTestResult.ReportingPathologist
                            .Participant.ElectronicCommunicationDetails != null)
                        narrativepathologyTestResult.Add
                        (
                            new List<Object>
                            {
                                "Reporting Pathologist - Address / Contact",
                                CreateAddress(pathologyTestResult.ReportingPathologist.Participant.Addresses,
                                    pathologyTestResult.ReportingPathologist.Participant.ElectronicCommunicationDetails)
                            }
                        );


                    if (!testRequestDetails.IsNullOrEmptyWhitespace())
                    {
                        narrativepathologyTestResult.Add
                        (
                            new List<Object>
                            {
                                "Test Requested Name",
                                testRequestDetails
                            }
                        );
                    }

                }

                strucDocTableList.Insert
                (
                    0,
                    (
                        PopulateTable
                        (
                            "Pathology Test Result(s)",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrativepathologyTestResult
                        )
                    )
                );
            }

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.Any() ? strucDocTableList.ToArray() : null;
            }

            if (renderMultiMediaList.Any())
            {
                strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the imaging examination result section
        /// </summary>
        /// <param name="imagingExaminationResult">IImagingExaminationResult</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IImagingExaminationResult imagingExaminationResult)
        {
            List<List<Object>> narrative;
            var strucDocTableList = new List<StrucDocTable>();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            if (imagingExaminationResult != null)
            {

                if (imagingExaminationResult.AnatomicalSite != null)
                    strucDocTableList.Add(CreateAnatomicalSites(imagingExaminationResult.AnatomicalSite,
                        ref renderMultiMediaList));

                //Imaging examination name, modality, status, additional information provided and findings
                narrative = new List<List<Object>>();

                //Imaging Examination Result Name 
                if (imagingExaminationResult.ResultDateTime != null)
                {
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Imaging Examination Result Date Time",
                            imagingExaminationResult.ResultDateTime.NarrativeText()
                        }
                    );
                }

                //Imaging Examination Result Name 
                if (imagingExaminationResult.ExaminationResultName != null)
                {
                    narrative.Add
                    (
                        new List<Object> {"Result name", imagingExaminationResult.ExaminationResultName.NarrativeText}
                    );
                }

                //Imaging examination result modality
                if (imagingExaminationResult.Modality != null)
                    narrative.Add
                    (
                        new List<Object> {"Modality", imagingExaminationResult.Modality.NarrativeText}
                    );

                //Imaging examination result status
                if (imagingExaminationResult.ExaminationResultStatus != null)
                    narrative.Add
                    (
                        new List<Object>
                            {"Result Status", imagingExaminationResult.ExaminationResultStatus.NarrativeText}
                    );

                //Clinical information provided
                if (!imagingExaminationResult.ClinicalInformationProvided.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        new List<Object> {"Clinical Information", imagingExaminationResult.ClinicalInformationProvided}
                    );

                //Findings
                if (!imagingExaminationResult.Findings.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        new List<Object> {"Findings", imagingExaminationResult.Findings}
                    );

                //Result representation
                if (!imagingExaminationResult.ExaminationResultRepresentation.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        new List<Object>
                            {"Result Representation", imagingExaminationResult.ExaminationResultRepresentation}
                    );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Imaging Examination Result Details",
                        null,
                        new[]
                        {
                            "Field", "Value"
                        },
                        null,
                        narrative
                    )
                );


                //Examination result group
                if (imagingExaminationResult.ResultGroup != null && imagingExaminationResult.ResultGroup.Any())
                {
                    strucDocTableList.AddRange
                    (
                        CreateNarrativeEntry(imagingExaminationResult.ResultGroup, ref renderMultiMediaList)
                    );
                }

                //Examination request details
                if (imagingExaminationResult.ExaminationRequestDetails != null)
                {
                    strucDocTableList.AddRange
                    (
                        CreateNarrativeEntry(imagingExaminationResult.ExaminationRequestDetails,
                            ref renderMultiMediaList)
                    );
                }
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            if (renderMultiMediaList.Any())
            {
                strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the reason for referral section; or any section that takes in a 
        /// narrative and a date time along with a duration.
        /// </summary>
        /// <param name="dateTime">dateTime</param>
        /// <param name="duration">duration</param>
        /// <param name="narrative">narrative</param>
        /// <param name="heading">heading</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(ISO8601DateTime dateTime, CdaInterval duration, string narrative,
            String heading)
        {
            var narrativeText = new List<List<String>>();
            var strucDocTableList = new List<StrucDocTable>();
            var tableHeading = String.Empty;
            var strucDocText = new StrucDocText();
            var date = String.Empty;

            if (narrative != null && !narrative.IsNullOrEmptyWhitespace())
            {
                var durationAsString = String.Empty;
                var columHeaders = new List<String>();

                if (dateTime != null)
                {
                    columHeaders.Add("Date");
                    tableHeading = "Date ";

                    date = dateTime.NarrativeText();
                }

                if (duration != null)
                {
                    columHeaders.Add("Duration");

                    tableHeading += tableHeading == String.Empty ? "Duration" : "and Duration";

                    durationAsString = CreateDuration(duration);
                }

                var narrativeEntry = new List<String>();

                if (!date.IsNullOrEmptyWhitespace())
                {
                    narrativeEntry.Add(date);
                }

                if (!durationAsString.IsNullOrEmptyWhitespace())
                {
                    narrativeEntry.Add(durationAsString);
                }

                narrativeText.Add(narrativeEntry);

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Reason for Referral",
                        null,
                        null,
                        null,
                        new List<List<String>> {new List<string> {narrative}}
                    )
                );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        tableHeading,
                        null,
                        columHeaders.ToArray(),
                        null,
                        narrativeText
                    )
                );

                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the XML Body File
        /// </summary>
        /// <param name="externalData">externalData</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(ExternalData externalData)
        {
            var strucDocText = new StrucDocText();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            if (externalData != null)
            {
                if (!renderMultiMediaList.Select(multiMediaItem => multiMediaItem.referencedObject)
                    .Contains(externalData.ID))
                {
                    renderMultiMediaList.Add
                    (
                        externalData.ConvertToStrucDocRenderMultiMedia()
                    );
                }

                strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the XML Body File
        /// </summary>
        /// <param name="externalDataList">externalData</param>
        /// <returns>StrucDocText</returns>
        [NotNull]
        public StrucDocText CreateNarrative(List<ExternalData> externalDataList)
        {
            var strucDocText = new StrucDocText();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            if (
                externalDataList != null &&
                externalDataList.Any()
            )
            {
                externalDataList.ForEach
                (
                    externalData =>
                    {
                        if (!renderMultiMediaList.Select(multiMediaItem => multiMediaItem.referencedObject)
                            .Contains(externalData.ID))
                        {
                            renderMultiMediaList.Add
                            (
                                externalData.ConvertToStrucDocRenderMultiMedia()
                            );
                        }
                    }
                );

                // 31/05/2017 PW
                // HIPS Enhancement: put each rendered multimedia item into a separate paragraph.
                // Template Package rule needs to be fixed for 1A Specialist Letter which throws an error for the below (work around in place)
                // The next line was previously: strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
                strucDocText.paragraph = renderMultiMediaList.Select(item => new StrucDocParagraph()
                {
                    renderMultiMedia = new StrucDocRenderMultiMedia[] {item}
                }).ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Creates a narrative for IDischargeSummaryProblemDiagnosis
        /// </summary>
        /// <param name="problemDiagnosesThisVisit">A list of IDischargeSummaryProblemDiagnosis</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(
            SCSModel.DischargeSummary.ProblemDiagnosesThisVisit problemDiagnosesThisVisit)
        {
            var strucDocList = new List<StrucDocList>();
            var narrativeParagraph = new List<StrucDocParagraph>();
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();
            var header = new[] {"Type", "Description"};

            if (problemDiagnosesThisVisit.ProblemDiagnosis != null && problemDiagnosesThisVisit.ProblemDiagnosis.Any())
            {
                foreach (var problemDiagnoses in problemDiagnosesThisVisit.ProblemDiagnosis)
                {
                    // Medications
                    narrative.Add(
                        new List<object>
                        {
                            problemDiagnoses.ProblemDiagnosisType == null
                                ? String.Empty
                                : problemDiagnoses.ProblemDiagnosisType.NarrativeText,
                            problemDiagnoses.ProblemDiagnosisDescription == null
                                ? String.Empty
                                : problemDiagnoses.ProblemDiagnosisDescription.NarrativeText,
                        }
                    );
                }
            }

            if (narrative.Any())
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        null,
                        null,
                        header,
                        null,
                        narrative
                    )
                );


            // Add any exclusion statements
            if (problemDiagnosesThisVisit.ExclusionStatement != null)
            {
                narrativeParagraph.Add(CreateExclusionStatementNarrative("Problem/Diagnoses",
                    problemDiagnosesThisVisit.ExclusionStatement));
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocList.Any())
            {
                strucDocText.list = strucDocList.ToArray();
            }

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Creates a IDocumentWithHealthEventEnded narrative.
        /// </summary>
        /// <returns>List of tables for use with a StrucDocText</returns>
        public StrucDocText CreateNarrative(List<Document> documents)
        {
            StrucDocText strucDocText = null;
            var showAuthorInHeader = false;
            var showAuthorAddressInHeader = false;
            var showDateTimeHealthEventEndedInHeader = false;

            if (documents != null && documents.Any())
            {
                var strucDocTableList = new List<StrucDocTable>();

                var narrative = new List<List<object>>();

                // Add any problem diagnosis entries
                foreach (var document in documents)
                {
                    strucDocTableList = new List<StrucDocTable>();

                    if (document != null && document.Link != null && document.Link.TemplateId != null)
                    {
                        // Add Date Time Authored
                        var narrativeTable = new List<object>
                        {
                            document.DateTimeAuthored != null ? document.DateTimeAuthored.NarrativeText() : null
                        };

                        if (document.DateTimeHealthEventEnded != null)
                        {
                            showDateTimeHealthEventEndedInHeader = true;
                            narrativeTable.AddRange(
                                new List<object>
                                {
                                    document.DateTimeHealthEventEnded.NarrativeText(),
                                });
                        }

                        narrativeTable.AddRange(
                            new List<object>
                            {
                                CreateLink(document.Link)
                            });

                        if (document.Author != null && document.Author.Participant != null)
                        {
                            showAuthorInHeader = true;

                            var personOrganisation =
                                document.Author.Participant.Person != null &&
                                document.Author.Participant.Person.PersonNames != null
                                    ? BuildPersonNames(document.Author.Participant.Person.PersonNames)
                                    : null;
                            if (document.Author.Participant.Person != null &&
                                document.Author.Participant.Person.Organisation != null && !document.Author.Participant
                                    .Person.Organisation.Name.IsNullOrEmptyWhitespace())
                            {
                                personOrganisation = string.Format("{0}{1}({2})", personOrganisation, DELIMITERBREAK,
                                    document.Author.Participant.Person.Organisation.Name);
                            }

                            narrativeTable.AddRange(
                                new List<object>
                                {
                                    personOrganisation
                                });

                            if (document.Author.Participant.Addresses != null ||
                                document.Author.Participant.ElectronicCommunicationDetails != null)
                            {
                                showAuthorAddressInHeader = true;
                                narrativeTable.AddRange(
                                    new List<object>
                                    {
                                        CreateAddress(document.Author.Participant.Addresses,
                                            document.Author.Participant.ElectronicCommunicationDetails)
                                    });
                            }


                        }

                        narrative.Add(narrativeTable);
                    }
                }

                var header = new List<string>
                {
                    "Date Time Authored", "Health Event Ended", "Document", "Author / Organisation", "Address / Contact"
                };
                // Remove author to Table Header
                if (!showAuthorInHeader) header.Remove("Author / Organisation");

                // Remove author address to Table Header
                if (!showAuthorAddressInHeader) header.Remove("Address / Contact");

                // Remove author to Table Header
                if (!showDateTimeHealthEventEndedInHeader) header.Remove("Health Event Ended");

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Documents",
                        null,
                        header.ToArray(),
                        null,
                        narrative
                    )
                );


                strucDocText = new StrucDocText {table = strucDocTableList.ToArray()};
            }
            else
            {
                strucDocText = new StrucDocText
                {
                    paragraph = new[]
                        {new StrucDocParagraph {Text = new[] {"Not Known (Insufficient Information Available)"}}}
                };
            }

            return strucDocText;
        }

        /// <summary>
        /// Creates a IDocumentWithHealthEventEnded narrative.
        /// </summary>
        /// <returns>List of tables for use with a StrucDocText</returns>
        public StrucDocText CreateNarrative(List<IDocumentWithHealthEventEnded> documents)
        {
            var documentList = new List<Document>();
            if (documents != null)
            {
                // Add any problem diagnosis entries
                documentList.AddRange(documents.Select(document => document as Document));
            }

            return CreateNarrative(documentList);
        }

        /// <summary>
        /// Creates a IDocument narrative.
        /// </summary>
        /// <returns>List of tables for use with a StrucDocText</returns>
        public StrucDocText CreateNarrative(List<IDocument> documents)
        {
            var documentList = new List<Document>();
            if (documents != null)
            {
                // Add any problem diagnosis entries
                documentList.AddRange(documents.Select(document => document as Document));
            }

            return CreateNarrative(documentList);
        }

        /// <summary>
        /// CLINICAL INTERVENTIONS THIS VISIT
        /// </summary>
        /// <param name="clinicalIntervention">ClinicalIntervention</param>
        /// <returns></returns>
        public StrucDocText CreateNarrative(SCSModel.DischargeSummary.ClinicalIntervention clinicalIntervention)
        {
            var strucDocList = new List<StrucDocList>();

            if (clinicalIntervention != null)
            {
                if (clinicalIntervention.Description != null)
                {
                    foreach (var description in clinicalIntervention.Description)
                    {
                        // CLINICAL INTERVENTIONS THIS VISIT
                        var items = new List<StrucDocItem>();
                        items.Add(new StrucDocItem {Text = new[] {description.NarrativeText}});
                        strucDocList.Add(new StrucDocList {item = items.ToArray()});
                    }
                }

            }

            var strucDocText = new StrucDocText();

            if (strucDocList.Any())
            {
                strucDocText.list = strucDocList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// Clinical Synopsis
        /// </summary>
        /// <param name="clinicalSynopsis">Clinical Synopsis</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(SCSModel.DischargeSummary.ClinicalSynopsis clinicalSynopsis)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();

            if (clinicalSynopsis != null)
            {
                narrative = new List<List<String>>();

                // CLINICAL SYNOPSIS
                narrative.Add(
                    new List<string>
                    {
                        clinicalSynopsis.Description
                    }
                );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Clinical Summary",
                        null,
                        new[] {"Description"},
                        new string[0],
                        narrative
                    )
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// Current Medications On Discharge
        /// </summary>
        /// <param name="currentMedication">CurrentMedications</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(SCSModel.DischargeSummary.CurrentMedications currentMedication)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            // Add the current medications
            if (currentMedication.TherapeuticGoods != null && currentMedication.TherapeuticGoods.Any())
            {
                strucDocTableList.AddRange(CreateCurrentMedicationsNarrative(currentMedication.TherapeuticGoods));
            }

            // Add the exclusion statements
            if (currentMedication.ExclusionStatement != null)
            {
                narrativeParagraph.Add(CreateExclusionStatementNarrative("Current Medications",
                    currentMedication.ExclusionStatement));
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any()) // Structured Tables
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else if (narrativeParagraph.Any()) // Narrative Paragraph
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// Creates the medication narrative.
        /// </summary>
        /// <param name="therapeuticGoods">List of current medications.</param>
        /// <returns>List of tables for inclusion in the narrative.</returns>
        public static IList<StrucDocTable> CreateCurrentMedicationsNarrative(IList<ITherapeuticGood> therapeuticGoods)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();

            if (therapeuticGoods != null)
            {

                narrative = new List<List<String>>();

                var narativeHeader = new List<String>
                {
                    "Medicine",
                    "Directions",
                    "Duration",
                    "Status",
                    "Clinical Indication",
                    "Change Description",
                    "Change Reason",
                    "Quantity Supplied",
                    "Comments"
                };


                foreach (ITherapeuticGood therapeuticGood in therapeuticGoods)
                {
                    if (therapeuticGood != null && therapeuticGood.MedicationHistory != null)
                    {
                        // Current Medications On Discharge
                        var medicationList = new List<String>
                        {
                            therapeuticGood.TherapeuticGoodIdentification == null
                                ? null
                                : therapeuticGood.TherapeuticGoodIdentification.NarrativeText,
                            therapeuticGood.DoseInstruction,
                            therapeuticGood.MedicationHistory.MedicationDuration == null
                                ? "-"
                                : CreateIntervalText(therapeuticGood.MedicationHistory.MedicationDuration),
                            therapeuticGood.MedicationHistory.ItemStatus != null
                                ? therapeuticGood.MedicationHistory.ItemStatus.NarrativeText
                                : null,
                            therapeuticGood.ReasonForTherapeuticGood,
                            therapeuticGood.MedicationHistory.ChangesMade != null
                                ? therapeuticGood.MedicationHistory.ChangesMade.NarrativeText
                                : "-",
                            !therapeuticGood.MedicationHistory.ReasonForChange.IsNullOrEmptyWhitespace()
                                ? therapeuticGood.MedicationHistory.ReasonForChange
                                : "-",
                            therapeuticGood.UnitOfUseQuantityDispensed.IsNullOrEmptyWhitespace()
                                ? "-"
                                : therapeuticGood.UnitOfUseQuantityDispensed,
                            !therapeuticGood.AdditionalComments.IsNullOrEmptyWhitespace()
                                ? therapeuticGood.AdditionalComments
                                : "-"
                        };

                        //medicine_list + clinical Indication
                        narrative.Add(medicationList);
                    }
                }

                StripEmptyColoums(ref narativeHeader, ref narrative, new List<int> {4, 5, 6, 8});

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Current Medications On Discharge",
                        null,
                        narativeHeader.ToArray(),
                        new string[0],
                        narrative
                    )
                );

            }

            return strucDocTableList;
        }

        /// <summary>
        /// Ceased Medications On Discharge
        /// </summary>
        /// <param name="ceasedMedications">CeasedMedications</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(SCSModel.DischargeSummary.CeasedMedications ceasedMedications)
        {
            List<StrucDocTable> strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            // Add the ceased medications
            if (ceasedMedications.TherapeuticGoods != null && ceasedMedications.TherapeuticGoods.Any())
            {
                strucDocTableList.AddRange(CreatedCeasedMedicationsNarrative(ceasedMedications.TherapeuticGoods));
            }

            // Add the exclusion statements
            if (ceasedMedications.ExclusionStatement != null)
            {
                narrativeParagraph.Add(CreateExclusionStatementNarrative("Ceased Medications",
                    ceasedMedications.ExclusionStatement));
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            // Narrative Paragraph
            else if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// Creates the ceased medication narrative.
        /// </summary>
        /// <param name="therapeuticGoodCeaseds">List of ceased medications.</param>
        /// <returns>List of tables for inclusion in the narrative.</returns>
        public IList<StrucDocTable> CreatedCeasedMedicationsNarrative(
            IList<ITherapeuticGoodCeased> therapeuticGoodCeaseds)
        {
            var headerList = new List<String>()
            {
                "Medication",
                "Change Made",
                "Change Status",
                "Change Reason"
            };

            var strucDocTableList = new List<StrucDocTable>();

            if (therapeuticGoodCeaseds != null && therapeuticGoodCeaseds.Any())
            {
                var narrative = new List<List<String>>();

                foreach (ITherapeuticGoodCeased therapeuticGood in therapeuticGoodCeaseds)
                {
                    if (therapeuticGood != null && therapeuticGood.MedicationHistory != null)
                    {
                        // Ceased Medications On Discharge
                        narrative.Add(
                            new List<string>
                            {
                                therapeuticGood.TherapeuticGoodIdentification == null
                                    ? String.Empty
                                    : therapeuticGood.TherapeuticGoodIdentification.NarrativeText,
                                therapeuticGood.MedicationHistory.ChangesMade == null
                                    ? String.Empty
                                    : therapeuticGood.MedicationHistory.ChangesMade.NarrativeText,
                                therapeuticGood.MedicationHistory.ItemStatus == null
                                    ? String.Empty
                                    : therapeuticGood.MedicationHistory.ItemStatus.NarrativeText,
                                therapeuticGood.MedicationHistory.ReasonForChange
                            }
                        );
                    }
                }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Ceased Medications",
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );
            }

            return strucDocTableList;
        }

        /// <summary>
        /// Creates the narrative section for adverse reactions.
        /// </summary>
        /// <param name="adverseReactions">Adverse reactions</param>
        /// <returns></returns>
        public StrucDocText CreateNarrative(SCSModel.DischargeSummary.AdverseReactions adverseReactions)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (adverseReactions.Reactions != null && adverseReactions.Reactions.Any())
            {
                strucDocTableList.AddRange(CreateAdverseReactionsNarrative(adverseReactions.Reactions));
            }

            if (adverseReactions.ExclusionStatement != null)
            {
                narrativeParagraph.Add(CreateExclusionStatementNarrative("Adverse Reactions",
                    adverseReactions.ExclusionStatement));
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            // Narrative Paragraph
            if (narrativeParagraph.Any())
            {
                strucDocText.paragraph = narrativeParagraph.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Create narrative for IAdverseReactionDischargeSummary
        /// </summary>
        /// <param name="adverseReactions">A list of IAdverseReactionDischargeSummary's</param>
        /// <returns>StrucDocText</returns>
        public IList<StrucDocTable> CreateAdverseReactionsNarrative(
            ICollection<IAdverseReactionDischargeSummary> adverseReactions)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();

            if (adverseReactions != null && adverseReactions.Any())
            {
                narrative = new List<List<String>>();

                var narativeHeader = new List<string>
                {
                    "Substance/Agent",
                    "Manifestations",
                    "Adverse Reaction Type"
                };

                foreach (var adverseReaction in adverseReactions)
                {
                    var reactionDescriptions = String.Empty;

                    if (adverseReaction.ReactionDescriptions != null)
                    {
                        adverseReaction.ReactionDescriptions.ForEach
                        (
                            reaction =>
                            {
                                reactionDescriptions +=
                                    reaction == null ? String.Empty : reaction.NarrativeText + DELIMITER;
                            }
                        );
                    }


                    var adverseReactionEntry = new List<string>
                    {
                        adverseReaction.AgentDescription == null
                            ? String.Empty
                            : adverseReaction.AgentDescription.NarrativeText,
                        reactionDescriptions,
                        adverseReaction.AdverseReactionType != null
                            ? adverseReaction.AdverseReactionType.NarrativeText
                            : null
                    };


                    narrative.Add(adverseReactionEntry);
                }

                StripEmptyColoums(ref narativeHeader, ref narrative, null);

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Adverse Reactions",
                        null,
                        narativeHeader.ToArray(),
                        new string[0],
                        narrative
                    )
                );
            }

            return strucDocTableList;
        }

        /// <summary>
        /// Create Narrative for Alerts
        /// </summary>
        /// <param name="alerts">A list of Alerts</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(ICollection<SCSModel.DischargeSummary.Alert> alerts)
        {
            var strucItemList = new List<StrucDocItem>();

            if (alerts != null && alerts.Any())
            {
                foreach (var alert in alerts)
                {
                    strucItemList.Add(
                        new StrucDocItem
                        {
                            Text = new[]
                            {
                                string.Format("{0} ({1})",
                                    alert.AlertDescription == null
                                        ? String.Empty
                                        : alert.AlertDescription.NarrativeText,
                                    alert.AlertType == null ? String.Empty : alert.AlertType.NarrativeText)
                            }
                        }
                    );
                }

            }

            var strucDocText = new StrucDocText();

            if (strucItemList.Any())
            {
                strucDocText.list = new[] {new StrucDocList {item = strucItemList.ToArray()}};
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// IParticipationAcdCustodian
        /// </summary>
        /// <param name="participations">IParticipationAcdCustodian</param>
        /// <returns></returns>
        public StrucDocText CreateNarrative(IList<IParticipationAcdCustodian> participations)
        {
            List<List<String>> narrative;
            var headerList = new List<String>
            {
                "Date",
                "Date Of Birth",
                "Gender",
                "Person Name",
                "Organisation",
                "Address / Contact"
            };

            var strucDocTableList = new List<StrucDocTable>();

            if (participations != null && participations.Any())
            {
                narrative = new List<List<String>>();

                foreach (var participation in participations)
                {
                    var durationAsString = string.Empty;
                    if (participation.ParticipationPeriod != null)
                    {
                        durationAsString = CreateDuration(participation.ParticipationPeriod);
                    }

                    // Get organisation name
                    String organisationName = null;
                    if (participation.Participant != null)
                    {
                        if (participation.Participant.Organisation != null)
                            if (!participation.Participant.Organisation.Name.IsNullOrEmptyWhitespace())
                                organisationName = participation.Participant.Organisation.Name;

                        if (organisationName.IsNullOrEmptyWhitespace())
                            if (participation.Participant.Person != null)
                                if (participation.Participant.Person.Organisation != null)
                                    if (!participation.Participant.Person.Organisation.Name.IsNullOrEmptyWhitespace())
                                        organisationName = participation.Participant.Person.Organisation.Name;
                    }

                    narrative.Add(
                        new List<string>
                        {
                            durationAsString,
                            participation != null && participation.Participant != null &&
                            participation.Participant.Person != null &&
                            participation.Participant.Person.DateOfBirth != null
                                ? participation.Participant.Person.DateOfBirth.NarrativeText()
                                : String.Empty,
                            participation != null && participation.Participant != null &&
                            participation.Participant.Person != null && participation.Participant.Person.Gender != null
                                ? participation.Participant.Person.Gender.Value
                                    .GetAttributeValue<NameAttribute, String>(x => x.Name)
                                : String.Empty,
                            participation != null &&
                            participation.Participant != null &&
                            participation.Participant.Person != null
                                ? BuildPersonNames(
                                    participation.Participant.Person.PersonNames)
                                : String.Empty,
                            (organisationName.IsNullOrEmptyWhitespace() ? string.Empty : organisationName),
                            participation != null && participation.Participant != null &&
                            participation.Participant.Addresses != null
                                ? CreateAddress(participation.Participant.Addresses.Cast<IAddress>(),
                                    participation.Participant.ElectronicCommunicationDetails)
                                : string.Empty,
                        }
                    );

                }

                if (narrative.Any())
                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "ACD Custodian",
                            null,
                            headerList.ToArray(),
                            new string[0],
                            narrative
                        )
                    );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// ArrangedServices
        /// </summary>
        /// <param name="arrangedServices">ArrangedServices</param>
        /// <returns></returns>
        public StrucDocText CreateNarrative(ICollection<SCSModel.DischargeSummary.ArrangedServices> arrangedServices)
        {
            var narrative = new List<List<String>>();

            var strucDocTableList = new List<StrucDocTable>();

            var headerList = new List<String>()
            {
                "Description",
                "Commencement",
                "Status",
                "Name",
                "Details",
                "Phone",
                "Email"
            };

            if (arrangedServices != null && arrangedServices.Any())

                foreach (var arrangedService in arrangedServices)
                {
                    var durationAsString = string.Empty;
                    var organisationName = string.Empty;
                    var personOrganisationName = string.Empty;
                    var organisationRole = string.Empty;

                    if (arrangedService.ServiceCommencementWindow != null)
                    {
                        durationAsString = CreateDuration(arrangedService.ServiceCommencementWindow);
                    }

                    var name = string.Empty;
                    var details = string.Empty;

                    if (arrangedService.ServiceProvider != null && arrangedService.ServiceProvider != null)
                    {
                        if (arrangedService.ServiceProvider.Participant != null)
                        {
                            if (arrangedService.ServiceProvider.Participant.Organisation != null)
                            {
                                if (arrangedService.ServiceProvider.Role != null)
                                    organisationRole = arrangedService.ServiceProvider.Role.NarrativeText;

                                if (
                                    !arrangedService.ServiceProvider.Participant.Organisation.Name
                                        .IsNullOrEmptyWhitespace())
                                    organisationName = arrangedService.ServiceProvider.Participant.Organisation.Name;
                            }


                            if (organisationName.IsNullOrEmptyWhitespace())
                                if (arrangedService.ServiceProvider.Participant.Person != null)
                                    if (arrangedService.ServiceProvider.Participant.Person.Organisation != null)
                                        if (
                                            !arrangedService.ServiceProvider.Participant.Person.Organisation.Name
                                                .IsNullOrEmptyWhitespace())
                                            personOrganisationName =
                                                arrangedService.ServiceProvider.Participant.Person.Organisation.Name;
                        }

                        if (arrangedService.ServiceProvider.Participant.Person != null &&
                            arrangedService.ServiceProvider.Participant.Person.PersonNames.Any())
                            name = BuildPersonNames(arrangedService.ServiceProvider.Participant.Person.PersonNames);

                        if (arrangedService.ServiceProvider.Participant.Organisation != null &&
                            !organisationName.IsNullOrEmptyWhitespace())
                            name = organisationName;

                        if (!personOrganisationName.IsNullOrEmptyWhitespace())
                            details = personOrganisationName;

                        if (!organisationRole.IsNullOrEmptyWhitespace())
                            details = organisationRole;

                    }


                    var arrangedServiceEntry =
                        new List<string>
                        {
                            arrangedService.ArrangedServiceDescription != null
                                ? arrangedService.ArrangedServiceDescription.NarrativeText
                                : null,
                            durationAsString,
                            arrangedService.Status != EventTypes.Undefined
                                ? arrangedService.Status.GetAttributeValue<NameAttribute, String>(x => x.Name)
                                : null,
                            name,
                            details,
                            arrangedService.ServiceProvider != null &&
                            arrangedService.ServiceProvider.Participant != null &&
                            arrangedService.ServiceProvider.Participant.ElectronicCommunicationDetails != null
                                ? CreatePhone(
                                    arrangedService.ServiceProvider.Participant.ElectronicCommunicationDetails)
                                : null,
                            arrangedService.ServiceProvider != null &&
                            arrangedService.ServiceProvider.Participant != null &&
                            arrangedService.ServiceProvider.Participant.ElectronicCommunicationDetails != null
                                ? CreateEmail(
                                    arrangedService.ServiceProvider.Participant.ElectronicCommunicationDetails)
                                : null
                        };


                    narrative.Add(arrangedServiceEntry);

                }

            StripEmptyColoums(ref headerList, ref narrative, null);

            if (arrangedServices.Any())
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Follow-up Appointments",
                        null,
                        headerList.ToArray(),
                        null,
                        narrative
                    )
                );

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;

        }

        /// <summary>
        /// Create the Recommendations Information Provided Narrative
        /// </summary>
        /// <param name="recommendationsInformationProvided">RecommendationsInformationProvided</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(
            SCSModel.DischargeSummary.RecommendationsInformationProvided recommendationsInformationProvided)
        {
            var narrative = new List<List<String>>();
            var headerList = new List<String>()
            {
                "Recommendation",
                "Person responsible"
            };

            var strucDocTableList = new List<StrucDocTable>();

            if (recommendationsInformationProvided != null &&
                recommendationsInformationProvided.RecommendationsProvided != null &&
                recommendationsInformationProvided.RecommendationsProvided.Any())
            {
                // NOTE : RECOMMENDATION RECIPIENT is mandatory 
                foreach (var recommendationsProvided in recommendationsInformationProvided.RecommendationsProvided)
                {
                    if (recommendationsProvided != null && recommendationsProvided.RecommendationRecipient != null)
                    {
                        // Set the name field
                        var name = string.Empty;
                        if (recommendationsProvided.RecommendationRecipient.Participant.Person != null &&
                            recommendationsProvided.RecommendationRecipient.Participant.Person.PersonNames.Any())
                            name = BuildPersonNames(recommendationsProvided.RecommendationRecipient.Participant.Person
                                .PersonNames);

                        narrative.Add(
                            new List<string>
                            {
                                recommendationsProvided.RecommendationNote ?? null,
                                name
                            }
                        );
                    }
                }

                StripEmptyColoums(ref headerList, ref narrative, null);

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Recommendations",
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );
            }

            // INFORMATION PROVIDED TO PATIENT AND/OR FAMILY
            if (recommendationsInformationProvided != null &&
                recommendationsInformationProvided.InformationProvided != null && !recommendationsInformationProvided
                    .InformationProvided.InformationProvidedToRelevantParties.IsNullOrEmptyWhitespace())
            {
                narrative = new List<List<String>>();

                narrative.Add(
                    new List<string>
                    {
                        recommendationsInformationProvided.InformationProvided.InformationProvidedToRelevantParties
                    }
                );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Information Provided",
                        null,
                        new[] {"Description"},
                        new string[0],
                        narrative
                    )
                );
            }


            var strucDocText = new StrucDocText();
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the diagnostic investigations section
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigationsDischargeSummary</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(
            SCSModel.DischargeSummary.IDiagnosticInvestigationsDischargeSummary diagnosticInvestigations)
        {
            var strucDocTableList = new List<StrucDocTable>();

            if (diagnosticInvestigations != null)
            {
                //This doesn't do anything as this section contains sub sections that render the narrative.
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Generates the Prescription Item for the narrative
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="prescriber">IParticipationPrescriber</param>
        /// <param name="subjectOfCare">IParticipationSubjectOfCare</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(PCEHRPrescriptionItem item, IParticipationPrescriber prescriber,
            IParticipationSubjectOfCare subjectOfCare)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();
            var narrativeObject = new List<List<Object>>();
            var entitlementsColumnHeaders = new List<string>
                {"Entitlement Type", "Validity Duration", "Entitlement Number"};
            var narrativePrescriberEntitlements = new List<List<String>>();

            if (item != null)
            {
                var columnHeaders = new List<string> {"Field", "Value"};

                //DateTime Prescription Written
                if (item.DateTimePrescriptionWritten != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Date and Time Prescription Written", item.DateTimePrescriptionWritten)
                    );

                //Therapeutic Good Identification
                if (item.TherapeuticGoodId != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Identification", item.TherapeuticGoodId.NarrativeText)
                    );

                //Therapeutic Good Generic Name
                if (item.TherapeuticGoodGenericName != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Generic Name (Additional Therapeutic Good Detail)",
                            item.TherapeuticGoodGenericName)
                    );

                //Therapeutic Good Strength
                if (item.TherapeuticGoodStrength != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Strength (Additional Therapeutic Good Detail)",
                            item.TherapeuticGoodStrength)
                    );

                //DateTime Prescription Expires
                if (item.DateTimePrescriptionExpires != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Date and Time Prescription Expires", item.DateTimePrescriptionExpires)
                    );

                //Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Formula", item.Formula)
                    );

                //Formula
                if (item.Form != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Form", item.Form.NarrativeText)
                    );

                //Directions
                if (!item.Directions.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Directions", item.Directions)
                    );

                //Clinical Indication
                if (item.ClinicalIndication != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Clinical Indication", item.ClinicalIndication)
                    );

                //Route
                if (item.Route != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Route", item.Route.NarrativeText)
                    );

                //Quantity Description
                if (item.DispensingInformation != null &&
                    !item.DispensingInformation.QuantityDescription.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Quantity Description", item.DispensingInformation.QuantityDescription)
                    );

                //Maximum Number of Repeats
                if (item.DispensingInformation != null && item.DispensingInformation.MaximumNumberOfRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Maximum Number of Repeats (Number of Repeats)",
                            item.DispensingInformation.MaximumNumberOfRepeats, null)
                    );

                // Minimum Interval Between Repeats
                if (item.DispensingInformation != null &&
                    item.DispensingInformation.MinimumIntervalBetweenRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Minimum Interval Between Repeats",
                            CreateIntervalText(item.DispensingInformation.MinimumIntervalBetweenRepeats))
                    );

                // PBS Manufacturer Code
                if (item.PBSManufacturerCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Manufacturer Code (Administrative Manufacturer Code)",
                            item.PBSManufacturerCode.NarrativeText)
                    );

                // Brand Substitution Permitted
                if (item.DispensingInformation.BrandSubstitutionPermitted.HasValue)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Brand Substitution Permitted",
                            item.DispensingInformation.BrandSubstitutionPermitted)
                    );

                // Comment
                if (!item.Comment.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Comment", item.Comment)
                    );

                //Prescription Item Identifier
                if (item.PrescriptionItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescription Item Identifier",
                            item.PrescriptionItemIdentifier.NarrativeText)
                    );

                // Convert the List<string> to a List<Object>
                narrativeObject.AddRange(narrative.Select(narrativeItem =>
                    narrativeItem.Select(s => (object) s).ToList()));

                // Prescription Record Link
                if (item.PrescriptionRecordLink != null)
                    narrativeObject.Add(
                        new List<object>
                        {
                            "Prescription Record Link",
                            CreateLink(item.PrescriptionRecordLink)
                        });

                // Entitlements Prescriber
                if (prescriber != null && prescriber.Participant != null && prescriber.Participant.Person != null &&
                    prescriber.Participant.Person.Entitlements != null &&
                    prescriber.Participant.Person.Entitlements.Any())
                {
                    foreach (var entitlement in prescriber.Participant.Person.Entitlements)
                    {
                        CodableText codeableTextEntry = null;

                        if (entitlement.Type != EntitlementType.Undefined)
                        {
                            codeableTextEntry = new CodableText
                            {
                                Code = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                DisplayName = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                OriginalText = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                CodeSystem = CodingSystem.NCTISEntitlementTypeValues
                            };
                        }

                        narrativePrescriberEntitlements.Add
                        (
                            new List<String>
                            {
                                codeableTextEntry != null ? codeableTextEntry.NarrativeText : "Undefined Entitlement",
                                entitlement.ValidityDuration == null
                                    ? string.Empty
                                    : CreateIntervalText(entitlement.ValidityDuration),
                                entitlement.Id != null ? entitlement.Id.NarrativeText : String.Empty
                            }
                        );
                    }
                }

                strucDocTableList.AddRange
                (
                    new List<StrucDocTable>
                    {
                        PopulateTable
                        (
                            "Prescription Item",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrativeObject
                        ),
                        narrativePrescriberEntitlements.Any()
                            ? PopulateTable
                            (
                                "Prescriber Entitlement",
                                null,
                                entitlementsColumnHeaders.ToArray(),
                                null,
                                narrativePrescriberEntitlements
                            )
                            : null,
                    }
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for an IEDispenseItem
        /// </summary>
        /// <param name="item">A IEPrescriptionItem</param>
        /// <param name="dispenser">A IParticipationDispenser</param>
        /// <param name="dispenserOrganisation">A IParticipationDispenserOrganisation</param>
        /// <param name="subjectOfCare">A IParticipationSubjectOfCare</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(PCEHRDispenseItem item, IParticipationDispenser dispenser,
            IParticipationDispenserOrganisation dispenserOrganisation, IParticipationSubjectOfCare subjectOfCare)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();
            var narrativeObject = new List<List<Object>>();

            var entitlementsColumnHeaders = new List<string>
                {"Entitlement Type", "Validity Duration", "Entitlement Number"};
            var narrativePrescriberEntitlements = new List<List<String>>();

            if (item != null)
            {
                var columnHeaders = new List<string> {"Field", "Value"};

                //Therapeutic Good Identification
                if (item.TherapeuticGoodId != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Identification", item.TherapeuticGoodId.NarrativeText)
                    );

                //Therapeutic Good Strength
                if (!item.TherapeuticGoodStrength.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Strength", item.TherapeuticGoodStrength)
                    );

                //Therapeutic Good Generic Name
                if (!item.TherapeuticGoodGenericName.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Generic Name", item.TherapeuticGoodGenericName)
                    );

                //Additional Dispensed Item Description
                if (!item.AdditionalDispensedItemDescription.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Additional Dispensed Item Description",
                            item.AdditionalDispensedItemDescription)
                    );

                //Label Instruction
                if (!item.LabelInstruction.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Label Instruction", item.LabelInstruction)
                    );

                //Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Formula", item.Formula)
                    );

                //Form
                if (item.Form != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Form", item.Form.NarrativeText)
                    );

                //Quantity Description
                if (!item.QuantityDescription.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Quantity Description", item.QuantityDescription)
                    );

                //Comment
                if (!item.Comment.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Comment", item.Comment)
                    );

                // Brand Substitution Occurred
                if (item.BrandSubstitutionOccurred != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Brand Substitution Occurred", item.BrandSubstitutionOccurred)
                    );

                //Number of this Dispense
                if (item.NumberOfThisDispense.HasValue)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Number of this Dispense",
                            item.NumberOfThisDispense.Value.ToString(CultureInfo.InvariantCulture))
                    );

                //Maximum Number of Repeats
                if (item.MaximumNumberOfRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Maximum Number of Repeats", item.MaximumNumberOfRepeats, null)
                    );

                // PBS Manufacturer Code
                if (item.PBSManufacturerCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Manufacturer Code", item.PBSManufacturerCode.NarrativeText)
                    );


                // Unique Pharmacy Prescription Number
                if (!item.UniquePharmacyPrescriptionNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Unique Pharmacy Prescription Number",
                            item.UniquePharmacyPrescriptionNumber)
                    );

                //Prescription Item Identifier
                if (item.PrescriptionItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescription Item Identifier",
                            item.PrescriptionItemIdentifier.NarrativeText)
                    );

                //DateTime Of Dispense Event
                if (item.DateTimeOfDispenseEvent != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("DateTime of Dispense Event", item.DateTimeOfDispenseEvent)
                    );

                //DateTime Of Dispense Event
                if (item.DispenseItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Dispense Item Identifier", item.DispenseItemIdentifier.NarrativeText)
                    );

                // Convert the List<string> to a List<Object>
                narrativeObject.AddRange(narrative.Select(narrativeItem =>
                    narrativeItem.Select(s => (object) s).ToList()));

                // Prescription Record Link
                if (item.DispenseRecordLink != null)
                    narrativeObject.Add(
                        new List<object>
                        {
                            "Dispense Record Link",
                            CreateLink(item.DispenseRecordLink)
                        });

                // Entitlements Prescriber
                if (dispenserOrganisation != null && dispenserOrganisation.Participant != null &&
                    dispenserOrganisation.Participant.Entitlements != null &&
                    dispenserOrganisation.Participant.Entitlements.Any())
                {
                    foreach (var entitlement in dispenserOrganisation.Participant.Entitlements)
                    {
                        CodableText codeableTextEntry = null;

                        if (entitlement.Type != EntitlementType.Undefined)
                        {
                            codeableTextEntry = new CodableText
                            {
                                Code = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                DisplayName = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                OriginalText = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                CodeSystem = CodingSystem.NCTISEntitlementTypeValues
                            };
                        }

                        narrativePrescriberEntitlements.Add
                        (
                            new List<String>
                            {
                                codeableTextEntry != null ? codeableTextEntry.NarrativeText : "Undefined Entitlement",
                                entitlement.ValidityDuration == null
                                    ? string.Empty
                                    : CreateIntervalText(entitlement.ValidityDuration),
                                entitlement.Id != null ? entitlement.Id.NarrativeText : String.Empty
                            }
                        );
                    }
                }

                strucDocTableList.AddRange
                (
                    new List<StrucDocTable>
                    {
                        PopulateTable
                        (
                            "Dispense Item",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrativeObject
                        ),
                        narrativePrescriberEntitlements.Any()
                            ? PopulateTable
                            (
                                "Dispensing Organisation Entitlement",
                                null,
                                entitlementsColumnHeaders.ToArray(),
                                null,
                                narrativePrescriberEntitlements
                            )
                            : null,

                    }
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for an (PrescribingAndDispensingReports)
        /// </summary>
        /// <param name="prescribingAndDispensingReports">A PrescribingAndDispensingReports</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(PrescribingAndDispensingReports prescribingAndDispensingReports)
        {
            var strucDocTableList = new List<StrucDocTable>();

            if (prescribingAndDispensingReports.MedicationEntriesWithSummary != null)
            {
                foreach (var medicationEntriesWithSummary in prescribingAndDispensingReports
                    .MedicationEntriesWithSummary)
                {

                    var narrative = new List<List<String>>();
                    if (medicationEntriesWithSummary != null &&
                        medicationEntriesWithSummary.SummaryOfMedicationEntries != null)
                    {
                        var summary = medicationEntriesWithSummary.SummaryOfMedicationEntries;

                        var columnHeaders = new List<string> {"Field", "Value"};

                        if (summary.DateTimeOfLatestDispenseEvent != null)
                            narrative.Add
                            (
                                CreateNarrativeEntry("Date Time Of Latest Dispense Event",
                                    summary.DateTimeOfLatestDispenseEvent)
                            );

                        if (summary.DateTimeOfEarliestDispenseEvent != null)
                            narrative.Add
                            (
                                CreateNarrativeEntry("DateTime Of Earliest Dispense Event",
                                    summary.DateTimeOfEarliestDispenseEvent)
                            );

                        if (summary.DateTimePrescriptionWritten != null)
                            narrative.Add
                            (
                                CreateNarrativeEntry("Date Time Prescription Written",
                                    summary.DateTimePrescriptionWritten)
                            );

                        if (summary.MaximumNumberOfPermittedSupplies != null)
                            narrative.Add
                            (
                                CreateNarrativeEntry("Maximum Number Of Permitted Supplies",
                                    summary.MaximumNumberOfPermittedSupplies)
                            );

                        if (summary.TherapeuticGoodId != null)
                            narrative.Add
                            (
                                CreateNarrativeEntry("Therapeutic Good Identification",
                                    summary.TherapeuticGoodId.NarrativeText)
                            );

                        if (summary.TotalNumberOfKnownSupplies != null)
                            narrative.Add
                            (
                                CreateNarrativeEntry("Total Number Of Known Supplies",
                                    summary.TotalNumberOfKnownSupplies)
                            );

                        // Add body Height Narrative
                        strucDocTableList.Add
                        (
                            PopulateTable
                            (
                                "Prescribing and Dispensing Reports",
                                null,
                                columnHeaders.ToArray(),
                                null,
                                narrative
                            )
                        );
                    }
                }
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
                strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Generates the Prescription Item for the IObservationWeightHeight
        /// </summary>
        /// <param name="observation">IObservationWeightHeight</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IObservationWeightHeight observation)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();

            if (observation != null)
            {
                var columnHeaders = new List<string> {"Date", "Name", "Quantity"};

                // BodyWeight
                if (observation.BodyWeight != null)
                {
                    // Weight
                    if (observation.BodyWeight.Quantity != null && observation.BodyWeight.Quantity.Value != null)
                        narrative.Add
                        (
                            new List<String>
                            {
                                observation.BodyWeight.BodyWeightObservationTime != null
                                    ? observation.BodyWeight.BodyWeightObservationTime.NarrativeText()
                                    : String.Empty,
                                "Body Weight",
                                observation.BodyWeight.Quantity.Value + " " +
                                observation.BodyWeight.Quantity.UnitCode
                            }
                        );
                }

                // Weight
                if (observation.BodyHeight != null)
                {
                    narrative.Add
                    (
                        new List<String>
                        {
                            observation.BodyHeight.BodyHeightObservationTime != null
                                ? observation.BodyHeight.BodyHeightObservationTime.NarrativeText()
                                : String.Empty,
                            "Body Height",
                            observation.BodyHeight.Quantity.Value + " " +
                            observation.BodyHeight.Quantity.UnitCode != null
                                ? observation.BodyHeight.Quantity.UnitCode
                                : String.Empty
                        }
                    );
                }

                // Add body Height Narrative
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Observation Weight Height",
                        null,
                        columnHeaders.ToArray(),
                        null,
                        narrative
                    )
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for Event Details
        /// </summary>
        /// <param name="eventDetails">EventDetails</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(EventDetails eventDetails)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();

            if (eventDetails.ClinicalSynopsisDescription != null)
            {
                narrative.Add
                (
                    new List<String>
                    {
                        eventDetails.ClinicalSynopsisDescription
                    }
                );


                // Add body Height Narrative
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Clinical Summary",
                        null,
                        new[] {"Description"},
                        null,
                        narrative
                    )
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }


        /// <summary>
        /// This method creates the narrative for the medication items
        /// </summary>
        /// <param name="medications">A list of medicationItems</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IEnumerable<IMedicationItem> medications)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();

            if (medications != null)
            {
                var narativeHeader = new List<string>
                {
                    "Medication",
                    "Directions",
                    "Clinical Indication",
                    "Change Status",
                    "Change Description",
                    "Change Reason",
                    "Comment"
                };

                narrative = new List<List<String>>();

                foreach (var medication in medications)
                {
                    var medicationList = new List<String>
                    {
                        medication.Medicine != null ? medication.Medicine.NarrativeText : null,
                        medication.Directions != null ? medication.Directions.NarrativeText : null,
                        medication.ClinicalIndication,
                        medication.ChangeStatus != null && medication.ChangeType != null
                            ? string.Format("{0} - {1}", medication.ChangeType.NarrativeText,
                                medication.ChangeStatus.NarrativeText)
                            : null,
                        !medication.ChangeDescription.IsNullOrEmptyWhitespace() ? medication.ChangeDescription : null,
                        medication.ChangeReason != null ? medication.ChangeReason.NarrativeText : null,
                        !medication.Comment.IsNullOrEmptyWhitespace() ? medication.Comment : null
                    };

                    narrative.Add(medicationList);
                }

                StripEmptyColoums(ref narativeHeader, ref narrative, new List<int> {3, 4, 5, 6});

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Medications",
                        null,
                        narativeHeader.ToArray(),
                        new string[0],
                        narrative
                    )
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for Diagnoses Intervention
        /// </summary>
        /// <param name="diagnosesIntervention">A diagnoses Intervention item</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(DiagnosesIntervention diagnosesIntervention)
        {
            var strucDocText = new StrucDocText();

            List<ProblemDiagnosis> problemDiagnosisList = null;
            if (diagnosesIntervention.ProblemDiagnosis != null)
            {
                problemDiagnosisList = diagnosesIntervention.ProblemDiagnosis.ConvertAll(x => x as ProblemDiagnosis);
            }

            List<Procedure> proceduresList = null;
            if (diagnosesIntervention.Procedures != null)
            {
                proceduresList = diagnosesIntervention.Procedures.ConvertAll(x => x);
            }

            List<MedicalHistoryItem> medicalHistoryItemList = null;
            if (diagnosesIntervention.UncategorisedMedicalHistoryItem != null)
            {
                medicalHistoryItemList =
                    diagnosesIntervention.UncategorisedMedicalHistoryItem.ConvertAll(x => x as MedicalHistoryItem);
            }

            var table = CreateNarrative(problemDiagnosisList, proceduresList, medicalHistoryItemList, false);

            if (table != null)
            {
                strucDocText.table = new[] {table};
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for Diagnoses Intervention
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocTable CreateNarrative(List<ProblemDiagnosis> problemDiagnosisList,
            List<Procedure> proceduresList, List<MedicalHistoryItem> medicalHistoryItemList,
            bool dateOfResolutionRemission)
        {
            var headerList = new List<string>
            {
                "Item",
                "Date",
                "Comment"
            };

            StrucDocTable strucDocTable = null;

            var list = new List<KeyValuePair<DateTime, List<string>>>();

            // Add Problem Diagnosis for Diagnoses Intervention
            if (problemDiagnosisList != null && problemDiagnosisList.Any())
            {
                foreach (var problemDiagnosis in problemDiagnosisList)
                {
                    // display dateTime text
                    var dateTimeText = string.Empty;

                    // Sorting dateTime value
                    var startDate = DateTime.MaxValue;

                    if (problemDiagnosis.DateOfOnset != null && problemDiagnosis.DateOfResolutionRemission != null)
                    {
                        startDate = problemDiagnosis.DateOfOnset.DateTime;
                        var interval = CdaInterval.CreateLowHigh(problemDiagnosis.DateOfOnset,
                            problemDiagnosis.DateOfResolutionRemission);
                        dateTimeText = interval.NarrativeText();
                    }

                    // NOTE : Show DateOfResolutionRemission as the only date if DateOfOnset is missing
                    if (problemDiagnosis.DateOfOnset == null && problemDiagnosis.DateOfResolutionRemission != null)
                    {
                        startDate = problemDiagnosis.DateOfResolutionRemission.DateTime;
                        dateTimeText = problemDiagnosis.DateOfResolutionRemission.NarrativeText();
                    }

                    // NOTE : Where DateOfResolutionRemission is null
                    if (problemDiagnosis.DateOfOnset != null && problemDiagnosis.DateOfResolutionRemission == null)
                    {
                        dateTimeText = string.Format("{0} ->", problemDiagnosis.DateOfOnset.NarrativeText());

                        if (dateOfResolutionRemission == false)
                            dateTimeText = problemDiagnosis.DateOfOnset.NarrativeText();

                        startDate = problemDiagnosis.DateOfOnset.DateTime;
                    }

                    if (problemDiagnosis.ShowOngoingDateInNarrative.HasValue &&
                        problemDiagnosis.ShowOngoingDateInNarrative.Value)
                    {
                        // NOTE : Where DateOfResolutionRemission is null
                        if (problemDiagnosis.DateOfOnset != null &&
                            problemDiagnosis.DateOfResolutionRemission == null && dateOfResolutionRemission)
                        {
                            dateTimeText = string.Format("{0} -> (ongoing)",
                                problemDiagnosis.DateOfOnset.NarrativeText());
                        }
                        else
                        {
                            // NOTE: Add -1 day to nest the ongoing items correctly in the sorting order
                            startDate = DateTime.MaxValue.AddDays(-1);
                            dateTimeText = "(ongoing)";
                        }
                    }

                    list.Add(new KeyValuePair<DateTime, List<string>>
                        (
                            startDate,
                            new List<string>
                            {
                                problemDiagnosis.ProblemDiagnosisIdentification != null
                                    ? problemDiagnosis.ProblemDiagnosisIdentification.NarrativeText
                                    : null,
                                dateTimeText,
                                problemDiagnosis.Comment.IsNullOrEmptyWhitespace() ? null : problemDiagnosis.Comment

                            }
                        )
                    );
                }
            }

            // Add procedure for Diagnoses Intervention
            if (proceduresList != null && proceduresList.Any())
                foreach (var procedure in proceduresList)
                {
                    DateTime? procedureDateTime = null;

                    if (procedure.ProcedureDateTime != null)
                    {
                        if (procedure.ProcedureDateTime.High != null)
                        {
                            procedureDateTime = procedure.ProcedureDateTime.High.DateTime;
                        }
                        else if (procedure.ProcedureDateTime.Low != null)
                        {
                            procedureDateTime = procedure.ProcedureDateTime.Low.DateTime;
                        }
                        else if (procedure.ProcedureDateTime.Center != null)
                        {
                            procedureDateTime = procedure.ProcedureDateTime.Center.DateTime;
                        }
                    }

                    var keyValuePairDateTime = procedureDateTime ?? DateTime.MaxValue;
                    var keyValuePairNarrative = procedure.ProcedureDateTime != null
                        ? procedure.ProcedureDateTime.NarrativeText()
                        : null;

                    if (procedure.ShowOngoingInNarrative.HasValue && procedure.ShowOngoingInNarrative.Value)
                    {
                        if (procedure.ProcedureDateTime.Type == IntervalType.Low ||
                            procedure.ProcedureDateTime.Type == IntervalType.LowHigh)
                        {
                            keyValuePairDateTime = procedure.ProcedureDateTime.Low.DateTime;
                            keyValuePairNarrative = string.Format("{0} -> (ongoing)",
                                procedure.ProcedureDateTime.Low.NarrativeText());
                        }
                        else
                        {
                            keyValuePairDateTime = DateTime.MaxValue.AddDays(-1);
                            keyValuePairNarrative = "(ongoing)";
                        }
                    }

                    list.Add(new KeyValuePair<DateTime, List<string>>
                        (
                            keyValuePairDateTime,
                            new List<String>
                            {
                                procedure.ProcedureName != null ? procedure.ProcedureName.NarrativeText : null,
                                keyValuePairNarrative,
                                procedure.Comment.IsNullOrEmptyWhitespace() ? null : procedure.Comment
                            }
                        )
                    );
                }

            // Add MedicalHistoryItem
            if (medicalHistoryItemList != null && medicalHistoryItemList.Any())
                foreach (var medicalHistoryItem in medicalHistoryItemList)
                {
                    list.Add(new KeyValuePair<DateTime, List<string>>
                        (
                            medicalHistoryItem.DateTimeInterval != null ||
                            (medicalHistoryItem.ShowOngoingInNarrative.HasValue &&
                             medicalHistoryItem.ShowOngoingInNarrative.Value)
                                ? CdaIntervalFormatter.GetFirstDateTimeOfDurrationForNarrativeSorting(
                                    medicalHistoryItem.DateTimeInterval, medicalHistoryItem.ShowOngoingInNarrative)
                                : DateTime.MaxValue,
                            new List<string>
                            {
                                medicalHistoryItem.ItemDescription,
                                CreateDuration(medicalHistoryItem.DateTimeInterval,
                                    medicalHistoryItem.ShowOngoingInNarrative),
                                medicalHistoryItem.ItemComment,
                            }
                        )
                    );
                }

            // Sort List
            list.Sort(Compare);

            // Copy Sorted list into the narrative
            var narrative = list.Select(item => item.Value).ToList();

            StripEmptyColoums(ref headerList, ref narrative, new List<int> {2});

            if (narrative.Any())
                strucDocTable =
                (
                    PopulateTable
                    (
                        "Medical History",
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );

            return strucDocTable;
        }

        /// <summary>
        /// This method creates the narrative for Immunisations
        /// </summary>
        /// <param name="immunisations">A list of Immunisation items</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IEnumerable<IImmunisation> immunisations)
        {
            var strucDocText = new StrucDocText();

            if (immunisations != null && immunisations.Any())
            {
                strucDocText.table = CreateNarrativeEntry(immunisations).ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for Requested Services
        /// </summary>
        /// <param name="requestedService">A list of Requested Services</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(RequestedService requestedService)
        {
            var narrative = new List<List<String>>();
            var header = new[] {"Field", "Value"};

            var strucDocTableList = new List<StrucDocTable>();

            if (requestedService != null)
            {
                // Get organisation name
                String organisationName = null;
                if (requestedService.ServiceProvider != null)
                    if (requestedService.ServiceProvider.Participant != null)
                    {
                        if (requestedService.ServiceProvider.Participant.Organisation != null)
                            if (!requestedService.ServiceProvider.Participant.Organisation.Name
                                .IsNullOrEmptyWhitespace())
                                organisationName = requestedService.ServiceProvider.Participant.Organisation.Name;

                        if (organisationName.IsNullOrEmptyWhitespace())
                            if (requestedService.ServiceProvider.Participant.Person != null)
                                if (requestedService.ServiceProvider.Participant.Person.Organisation != null)
                                    if (!requestedService.ServiceProvider.Participant.Person.Organisation.Name
                                        .IsNullOrEmptyWhitespace())
                                        organisationName = requestedService.ServiceProvider.Participant.Person
                                            .Organisation.Name;
                    }

                String personName = null;
                // Provider Person Name
                if (requestedService.ServiceProvider != null && requestedService.ServiceProvider.Participant != null &&
                    requestedService.ServiceProvider.Participant.Person != null)
                    personName = BuildPersonNames(requestedService.ServiceProvider.Participant.Person.PersonNames);

                // Requested Service Description
                if (requestedService.RequestedServiceDescription != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Requested Service Description",
                            requestedService.RequestedServiceDescription.NarrativeText)
                    );

                // Service Requested DateTime
                if (requestedService.RequestedServiceDateTime != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Service Requested DateTime",
                            requestedService.RequestedServiceDateTime.NarrativeText())
                    );

                // DateTime Service Scheduled or Service Commencement Window
                if (requestedService.ServiceScheduled != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Service Scheduled", requestedService.ServiceScheduled.NarrativeText())
                    );

                // DateTime Service Scheduled or Service Commencement Window
                if (requestedService.ServiceCommencementWindow != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Service Commencement Window",
                            requestedService.ServiceCommencementWindow.NarrativeText())
                    );

                // Provider Organisation
                if (!organisationName.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Provider Organisation", organisationName)
                    );

                // Provider Person Name
                if (!personName.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Provider Person Name", personName)
                    );

                // Provider Person Name
                if ((requestedService.ServiceProvider != null && requestedService.ServiceProvider.Participant != null &&
                     requestedService.ServiceProvider.Participant.Addresses != null &&
                     requestedService.ServiceProvider.Participant.Addresses.Any()) ||
                    requestedService.ServiceProvider != null &&
                    requestedService.ServiceProvider.Participant.ElectronicCommunicationDetails != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Address / Contact",
                            CreateAddress(requestedService.ServiceProvider.Participant.Addresses,
                                requestedService.ServiceProvider.Participant.ElectronicCommunicationDetails))
                    );

                //  Booking Status
                if (requestedService.ServiceBookingStatus != EventTypes.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Booking Status",
                            requestedService.ServiceBookingStatus.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // Provider Person Name
                if (!requestedService.SubjectOfCareInstructionDescription.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Subject of Care Instruction Description",
                            requestedService.SubjectOfCareInstructionDescription)
                    );
            }

            strucDocTableList.Add
            (
                PopulateTable
                (
                    "Requested Service",
                    null,
                    header,
                    new string[0],
                    narrative
                )
            );

            var strucDocText = new StrucDocText();


            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the AustralianChildhoodImmunisationRegisterEntries section
        /// </summary>
        /// <param name="australianChildhoodImmunisationRegisterHistory">AustralianChildhoodImmunisationRegisterHistory</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(
            AustralianChildhoodImmunisationRegisterHistory australianChildhoodImmunisationRegisterHistory)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();

            var narativeHeader = new List<string>
            {
                "Type",
                "Date",
                "Dose"
            };

            if (australianChildhoodImmunisationRegisterHistory != null && australianChildhoodImmunisationRegisterHistory
                    .AustralianChildhoodImmunisationRegisterEntries != null &&
                australianChildhoodImmunisationRegisterHistory.AustralianChildhoodImmunisationRegisterEntries
                    .AustralianChildhoodImmunisationRegisterEntry != null)
            {
                foreach (var australianChildhoodImmunisationRegisterEntry in
                    australianChildhoodImmunisationRegisterHistory.AustralianChildhoodImmunisationRegisterEntries
                        .AustralianChildhoodImmunisationRegisterEntry)
                {
                    if (australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry != null)
                    {
                        var medicationList = new List<Object>
                        {
                            australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry.VaccineType.NarrativeText,
                            australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry != null &&
                            australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry
                                .DateVaccinationReceived != null
                                ? australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry
                                    .DateVaccinationReceived.NarrativeText()
                                : null,
                            australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry.VaccineDoseNumber
                                .HasValue
                                ? australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry
                                    .VaccineDoseNumber.Value.ToString(CultureInfo.InvariantCulture)
                                : null
                        };

                        narrative.Add(medicationList);
                    }

                    if (australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry != null)
                    {
                        var medicationList = new List<Object>
                        {
                            australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry.VaccineType.NarrativeText,
                            australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry != null &&
                            australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry
                                .DateVaccinationCancelled != null
                                ? australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry
                                    .DateVaccinationCancelled.NarrativeText()
                                : null,
                            australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry.VaccineDoseNumber
                                .HasValue
                                ? australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry
                                    .VaccineDoseNumber.Value.ToString(CultureInfo.InvariantCulture)
                                : null
                        };

                        narrative.Add(medicationList);
                    }
                }
            }

            strucDocTableList.Add
            (
                PopulateTable
                (
                    null,
                    null,
                    narativeHeader.ToArray(),
                    new string[0],
                    narrative
                )
            );

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the ExclusionStatement section
        /// </summary>
        /// <param name="exclusionStatement">ExclusionStatement</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(ExclusionStatement exclusionStatement)
        {
            var strucDocText = new StrucDocText();
            if (exclusionStatement != null)
            {
                strucDocText.paragraph = CreateParagraph(exclusionStatement.GeneralStatement);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the MedicareDVAFundedServices section
        /// </summary>
        /// <param name="medicareDVAFundedServices">MedicareDVAFundedServices</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(MedicareDVAFundedServices medicareDVAFundedServices)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();

            var narativeHeader = new List<string>
            {
                "Date",
                "Number",
                "Description",
                "Service Provider",
                "In Hospital?",
            };

            if (medicareDVAFundedServices != null && medicareDVAFundedServices.MedicareDVAFundedService != null)
            {
                foreach (var medicareDVAFundedService in medicareDVAFundedServices.MedicareDVAFundedService)
                {
                    if (medicareDVAFundedService != null)
                    {
                        var medicareList = new List<Object>
                        {
                            medicareDVAFundedService.DateOfService != null
                                ? medicareDVAFundedService.DateOfService.NarrativeText()
                                : null,
                            medicareDVAFundedService.MedicareMBSDVAItem != null &&
                            medicareDVAFundedService.MedicareMBSDVAItem.Code != null
                                ? medicareDVAFundedService.MedicareMBSDVAItem.Code
                                : null,
                            medicareDVAFundedService.MedicareMBSDVAItem != null
                                    ? medicareDVAFundedService.MedicareMBSDVAItem.NarrativeText
                                    : null,
                            medicareDVAFundedService.ServiceProvider != null &&
                            medicareDVAFundedService.ServiceProvider.Participant != null &&
                            medicareDVAFundedService.ServiceProvider.Participant.Person != null
                                ? BuildPersonNames(medicareDVAFundedService.ServiceProvider.Participant.Person
                                    .PersonNames)
                                : null,
                            medicareDVAFundedService.ServiceInHospitalIndicator.HasValue
                                ? medicareDVAFundedService.ServiceInHospitalIndicator.Value ? "Yes" : "No"
                                : null
                        };

                        narrative.Add(medicareList);
                    }
                }
            }

            strucDocTableList.Add
            (
                PopulateTable
                (
                    null,
                    null,
                    narativeHeader.ToArray(),
                    new string[0],
                    narrative
                )
            );

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the australianOrganDonorRegisterDetails section
        /// </summary>
        /// <param name="australianOrganDonorRegisterDetails">AustralianOrganDonorRegisterDetails</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(AustralianOrganDonorRegisterDetails australianOrganDonorRegisterDetails)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();

            // Australian Organ Donor Register Entry
            if (australianOrganDonorRegisterDetails.AustralianOrganDonorRegisterEntry != null)
            {
                // Organ And Tissue Donation Details
                if (australianOrganDonorRegisterDetails.AustralianOrganDonorRegisterEntry
                        .OrganAndTissueDonationDetails != null)
                {
                    var narrativeOrganAndTissueDonationDetails = new List<List<object>>();

                    var item = australianOrganDonorRegisterDetails.AustralianOrganDonorRegisterEntry;
                    var detail = australianOrganDonorRegisterDetails.AustralianOrganDonorRegisterEntry
                        .OrganAndTissueDonationDetails;

                    if (item.DonationDecision.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                DELIMITERBOLD + "Donor decision",
                                item.DonationDecision.Value ? "Yes" : "No"
                            }
                        );

                    if (item.DateOfInitialRegistration != null)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Date Of Initial Registration",
                                item.DateOfInitialRegistration.NarrativeText()
                            }
                        );

                    if (detail.BoneTissueIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Bone Tissue Indicator",
                                detail.BoneTissueIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.EyeTissueIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Eye Tissue Indicator",
                                detail.EyeTissueIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.HeartIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Heart Indicator",
                                detail.HeartIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.HeartValveIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Heart Valve Indicator",
                                detail.HeartValveIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );


                    if (detail.KidneyIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Kidney Indicator",
                                detail.KidneyIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.LiverIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Liver Indicator",
                                detail.LiverIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.LungsIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Lungs Indicator",
                                detail.LungsIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.PancreasIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Pancreas Indicator",
                                detail.PancreasIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    if (detail.SkinTissueIndicator.HasValue)
                        narrativeOrganAndTissueDonationDetails.Add
                        (
                            new List<object>
                            {
                                "Skin Tissue Indicator",
                                detail.SkinTissueIndicator.Value ? DELIMITERBOLD + "Yes" : "No"
                            }
                        );

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            null,
                            null,
                            new[] {"Field", "Value"},
                            null,
                            narrativeOrganAndTissueDonationDetails
                        )
                    );
                }

            }

            strucDocText.table = strucDocTableList.ToArray();
            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for the Achievement Section
        /// </summary>
        /// <param name="achievements">A List of Achievements</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(List<Achievement> achievements)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[] {"Field", "Value"};

            var count = 0;

            foreach (var achievement in achievements)
            {
                var narrative = new List<List<Object>>();

                count++;

                if (achievement != null)
                {
                    // Achievement Date
                    if (achievement.AchievementDate != null)
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Achievement Date",
                                achievement.AchievementDate.NarrativeText()
                            }
                        );

                    // Achievement Topic
                    if (achievement.AchievementTopic != null)
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Achievement Topic",
                                achievement.AchievementTopic
                            }
                        );

                    // Achievement Description
                    if (achievement.AchievementDescription != null)
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Achievement Description",
                                achievement.AchievementDescription
                            }
                        );

                    if (achievement.InformationProvider != null && achievement.InformationProvider.Participant != null)
                    {

                        var personOrganisation =
                            achievement.InformationProvider.Participant.Person != null &&
                            achievement.InformationProvider.Participant.Person.PersonNames != null
                                ? BuildPersonNames(achievement.InformationProvider.Participant.Person.PersonNames)
                                : null;

                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Information Provider",
                                personOrganisation
                            }
                        );

                        if (achievement.InformationProvider.Participant.Addresses != null ||
                            achievement.InformationProvider.Participant.ElectronicCommunicationDetails != null)
                        {
                            narrative.Add
                            (
                                new List<Object>
                                {
                                    "Information Provider - Details",
                                    CreateAddress(achievement.InformationProvider.Participant.Addresses,
                                        achievement.InformationProvider.Participant.ElectronicCommunicationDetails)
                                }
                            );
                        }
                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            string.Format("Achievement {0}", count.ToString(CultureInfo.InvariantCulture)),
                            null,
                            header,
                            null,
                            narrative
                        )
                    );
                }
            }

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Generates the Prescription Item for the narrative
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="prescriber">IParticipationPrescriber</param>
        /// <param name="subjectOfCare">IParticipationSubjectOfCare</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IEPrescriptionItem item, IParticipationPrescriber prescriber,
            IParticipationSubjectOfCare subjectOfCare)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();
            var narrativeTiming = new List<List<String>>();
            var narrativeAdministrationDetails = new List<List<String>>();
            var narrativeStructuredDose = new List<List<String>>();
            var narrativeQuantityToDispense = new List<List<String>>();
            var narrativePBSExtemporaneousIngredient = new List<List<String>>();
            var narrativeEntitlements = new List<List<String>>();

            if (item != null)
            {

                //DateTime Prescription Written
                if (item.DateTimePrescriptionWritten != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Date and Time Prescription Written", item.DateTimePrescriptionWritten)
                    );

                //DateTime Prescription Expires
                if (item.DateTimePrescriptionExpires != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Date and Time Prescription Expires", item.DateTimePrescriptionExpires)
                    );

                //Prescription Item Identifier
                if (item.PrescriptionItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescription Item Identifier",
                            item.PrescriptionItemIdentifier.NarrativeText)
                    );

                //Therapeutic Good Identification
                if (item.TherapeuticGoodIdentification != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Identification",
                            item.TherapeuticGoodIdentification.NarrativeText)
                    );

                //Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Formula", item.Formula)
                    );

                //Instruction
                if (!item.Directions.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Directions", item.Directions)
                    );

                if (item.StructuredDose != null)
                {
                    //Quantity of Therapeutic Good
                    if (item.StructuredDose.Quantity != null)
                        narrativeStructuredDose.Add
                        (
                            CreateNarrativeEntry("Quantity", item.StructuredDose.Quantity.NarrativeText)
                        );

                    //Structured Dose - Dose Unit
                    if (item.StructuredDose.Unit != null)
                        narrativeStructuredDose.Add
                        (
                            CreateNarrativeEntry("Dose Unit", item.StructuredDose.Unit.NarrativeText)
                        );

                    //Structured Dose - Quantity Description
                    if (item.StructuredDose.QuantityDescription != null)
                        narrativeStructuredDose.Add
                        (
                            CreateNarrativeEntry("Quantity Description", item.StructuredDose.QuantityDescription)
                        );
                }

                if (item.Timing != null)
                {
                    //Timing
                    if (!item.Timing.TimingDescription.IsNullOrEmptyWhitespace())
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("Timing Description", item.Timing.TimingDescription)
                        );

                    //Structured Timing EffectiveTime
                    if (item.Timing.StructuredTiming != null && item.Timing.StructuredTiming.EffectiveTime != null)
                    {

                        if (!item.Timing.StructuredTiming.NarrativeText.IsNullOrEmptyWhitespace())
                        {

                            narrativeTiming.Add
                            (
                                CreateNarrativeEntry("EffectiveTime", item.Timing.StructuredTiming.NarrativeText)
                            );

                        }
                        else
                        {
                            //Structured Timing EffectiveTime
                            narrativeTiming.Add
                            (
                                new List<string>
                                {
                                    "EffectiveTime",
                                    CreateTimingEntry(item.Timing.StructuredTiming.EffectiveTime)
                                }
                            );
                        }
                    }

                    //PRN
                    if (item.Timing.PRN.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("PRN", item.Timing.PRN.Value.ToString(CultureInfo.InvariantCulture))
                        );

                    //Timing - StartCriterion
                    if (item.Timing.StartCriterion.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StartCriterion",
                                item.Timing.StartCriterion.Value.ToString(CultureInfo.InvariantCulture))
                        );

                    //Timing - StartDate
                    if (item.Timing.StartDate != null)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StartDate", item.Timing.StartDate)
                        );

                    //Timing - StopCriterion
                    if (item.Timing.StopCriterion.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StopCriterion",
                                item.Timing.StopCriterion.Value.ToString(CultureInfo.InvariantCulture))
                        );

                    //Timing - StopDate
                    if (item.Timing.StopDate != null)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StopDate", item.Timing.StopDate)
                        );

                    if (item.Timing.NumberOfAdministrations != null)
                    {
                        var numberOfAdministrations = string.Empty;

                        //Timing - Number of Administrations - Numerator
                        if (item.Timing.NumberOfAdministrations.NullFlavor != null)
                            numberOfAdministrations +=
                                item.Timing.NumberOfAdministrations.NullFlavor.Value
                                    .GetAttributeValue<NameAttribute, String>(x => x.Name) + " ";

                        //Timing - Number of Administrations - Denominator
                        if (item.Timing.NumberOfAdministrations.Denominator != null)
                            numberOfAdministrations += item.Timing.NumberOfAdministrations.Denominator.NarrativeText;

                        //Timing - Number of Administrations - Numerator
                        if (item.Timing.NumberOfAdministrations.Numerator != null)
                        {
                            if (!numberOfAdministrations.IsNullOrEmptyWhitespace())
                                numberOfAdministrations += " - ";

                            numberOfAdministrations +=
                                item.Timing.NumberOfAdministrations.Numerator.NarrativeText + " ";
                        }

                        if (!numberOfAdministrations.IsNullOrEmptyWhitespace())
                            narrativeTiming.Add
                            (
                                CreateNarrativeEntry("Number of Administrations", numberOfAdministrations)
                            );
                    }

                    //Timing - LongTerm
                    if (item.Timing.LongTerm.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("Long Term",
                                item.Timing.LongTerm.Value.ToString(CultureInfo.InvariantCulture))
                        );
                }

                if (item.AdministrationDetails != null)
                {
                    //AdministrationDetails - Route
                    if (item.AdministrationDetails.Route != null)
                        narrativeAdministrationDetails.Add
                        (
                            CreateNarrativeEntry("Route", item.AdministrationDetails.Route.NarrativeText)
                        );

                    //AdministrationDetails - AnatomicalSite
                    if (item.AdministrationDetails.AnatomicalSite != null)
                        narrativeAdministrationDetails.Add
                        (
                            CreateNarrativeEntry("AnatomicalSite",
                                item.AdministrationDetails.AnatomicalSite.NarrativeText)
                        );

                    //AdministrationDetails - MedicationDeliveryMethod
                    if (item.AdministrationDetails.MedicationDeliveryMethod != null)
                        narrativeAdministrationDetails.Add
                        (
                            CreateNarrativeEntry("Medication Delivery Method",
                                item.AdministrationDetails.MedicationDeliveryMethod.NarrativeText)
                        );
                }

                if (item.QuantityToDispense != null)
                {
                    //QuantityToDispense - Quantity
                    if (item.QuantityToDispense.Quantity != null)
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Quantity", item.QuantityToDispense.Quantity.NarrativeText)
                        );

                    //QuantityToDispense - Dispensing Unit
                    if (item.QuantityToDispense.Unit != null)
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Dispensing Unit", item.QuantityToDispense.Unit.NarrativeText)
                        );

                    //QuantityToDispense - QuantityDescription
                    if (!item.QuantityToDispense.QuantityDescription.IsNullOrEmptyWhitespace())
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Quantity Description", item.QuantityToDispense.QuantityDescription)
                        );
                }

                //Brand Substitute Allowed
                if (item.BrandSubstituteNotAllowed != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Brand Substitute Not Allowed", item.BrandSubstituteNotAllowed)
                    );

                //Maximum Number of Repeats
                if (item.MaximumNumberOfRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Maximum Number of Repeats", item.MaximumNumberOfRepeats)
                    );

                // Minimum Interval Between Repeats
                if (item.MinimumIntervalBetweenRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Minimum Interval Between Repeats",
                            item.MinimumIntervalBetweenRepeats.NarrativeText)
                    );

                // PBS Prescription Type
                if (item.PBSPrescriptionType.HasValue)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Prescription Type",
                            item.PBSPrescriptionType.Value.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // Medical Benefit Category Type
                if (item.MedicalBenefitCategoryType.HasValue &&
                    item.MedicalBenefitCategoryType.Value != MedicalBenefitCategoryType.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Medical Benefit Category Type",
                            item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // PBS Close the Gap Benefit
                if (item.PBSCloseTheGapBenefit != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Close the Gap Benefit", item.PBSCloseTheGapBenefit.NarrativeText)
                    );

                // PBS/RPBS Item Code
                if (item.PBSRPBSItemCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Item Code",
                            string.Format("{0} {1}", item.PBSRPBSItemCode.Code,
                                !item.PBSRPBSItemCode.NarrativeText.IsNullOrEmptyWhitespace()
                                    ? string.Format("{0}", item.PBSRPBSItemCode.NarrativeText)
                                    : string.Empty))
                    );

                // PBS/RPBS Manufacturer Code
                if (item.PBSRPBSManufacturerCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Manufacturer Code", item.PBSRPBSManufacturerCode.NarrativeText)
                    );


                // PBS/RPBS Manufacturer Code
                if (item.PBSExtemporaneousIngredient != null)
                {
                    for (int index = 0; index < item.PBSExtemporaneousIngredient.Count; index++)
                    {
                        var extemporaneousIngredient = item.PBSExtemporaneousIngredient[index];

                        if (extemporaneousIngredient != null && extemporaneousIngredient.IngredientName != null ||
                            extemporaneousIngredient.IngredientQuantity != null)
                            narrativePBSExtemporaneousIngredient.Add
                            (
                                CreateNarrativeEntry(
                                    extemporaneousIngredient.IngredientName != null
                                        ? extemporaneousIngredient.IngredientName.NarrativeText
                                        : null,
                                    extemporaneousIngredient.IngredientQuantity != null
                                        ? extemporaneousIngredient.IngredientQuantity.NarrativeText
                                        : null)
                            );

                    }
                }

                // Grounds for Concurrent Supply
                if (item.GroundsForConcurrentSupply.HasValue &&
                    item.GroundsForConcurrentSupply != GroundsForConcurrentSupply.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Grounds for Concurrent Supply",
                            item.GroundsForConcurrentSupply.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // PBS/RPBS Authority Prescription Number
                if (!item.PBSRPBSAuthorityPrescriptionNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Authority Prescription Number",
                            item.PBSRPBSAuthorityPrescriptionNumber)
                    );

                // PBS/RPBS Authority Approval Number
                if (!item.PBSRPBSAuthorityApprovalNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Authority Approval Number", item.PBSRPBSAuthorityApprovalNumber)
                    );


                // Streamlined Authority Approval Number
                if (!item.StreamlinedAuthorityApprovalNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Streamlined Authority Approval Number",
                            item.StreamlinedAuthorityApprovalNumber)
                    );

                // State Authority Number
                if (item.StateAuthorityNumber != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("State Authority Number", item.StateAuthorityNumber.NarrativeText)
                    );


                // Reason for Therapeutic Good
                if (!item.ReasonForTherapeuticGood.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Reason for Therapeutic Good", item.ReasonForTherapeuticGood)
                    );

                // Additional Comments
                if (!item.AdditionalComments.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Additional Comments", item.AdditionalComments)
                    );

                // Dispense Item Identifier
                if (item.DispenseItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Dispense Item Identifier", item.DispenseItemIdentifier.NarrativeText)
                    );

                // Medication Instruction Identifier
                if (item.MedicationInstructionIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Medication Instruction Identifier",
                            item.MedicationInstructionIdentifier.NarrativeText)
                    );

                // The Prescription Note
                if (!item.NoteDetail.IsNullOrEmptyWhitespace())
                {
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescription Note Detail", item.NoteDetail)
                    );

                }

                // Entitlements Prescriber
                if (prescriber.Participant.Person.Entitlements != null &&
                    prescriber.Participant.Person.Entitlements.Count > 0)
                {
                    foreach (var entitlement in prescriber.Participant.Person.Entitlements)
                    {
                        CodableText codeableTextEntry = null;

                        if (entitlement.Type != EntitlementType.Undefined)
                        {
                            codeableTextEntry = new CodableText
                            {
                                Code = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                DisplayName = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                OriginalText = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                CodeSystem = CodingSystem.NCTISEntitlementTypeValues
                            };
                        }

                        narrativeEntitlements.Add
                        (
                            CreateNarrativeEntry(entitlement, codeableTextEntry)
                        );
                    }
                }

                strucDocTableList.AddRange
                (
                    new List<StrucDocTable>
                    {
                        narrative.Any()
                            ? PopulateTable
                            (
                                "Prescription Item",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrative
                            )
                            : null,
                        narrativeTiming.Any()
                            ? PopulateTable
                            (
                                "Timing",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeTiming
                            )
                            : null,
                        narrativeStructuredDose.Any()
                            ? PopulateTable
                            (
                                "Structured Dose",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeStructuredDose
                            )
                            : null,
                        narrativeQuantityToDispense.Any()
                            ? PopulateTable
                            (
                                "Quantity To Dispense",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeQuantityToDispense
                            )
                            : null,
                        narrativePBSExtemporaneousIngredient.Any()
                            ? PopulateTable
                            (
                                "PBS Extemporaneous Ingredient",
                                null,
                                new[] {"Ingredient Name", "Ingredient Quantity"},
                                null,
                                narrativePBSExtemporaneousIngredient
                            )
                            : null,
                        narrativeAdministrationDetails.Any()
                            ? PopulateTable
                            (
                                "Administration Details",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeAdministrationDetails
                            )
                            : null,
                        narrativeEntitlements.Any()
                            ? PopulateTable
                            (
                                "Entitlements",
                                null,
                                new[] {"Entitlements", "Value"},
                                null,
                                narrativeEntitlements
                            )
                            : null
                    }
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Count > 0)
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for an DispenseItemATS
        /// </summary>
        /// <param name="item">A DispenseItemATS</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(DispenseItem item)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();
            var narrativeQuantityToDispense = new List<List<String>>();
            var narrativePBSExtemporaneousIngredient = new List<List<String>>();

            if (item != null)
            {

                // Status Code
                if (item.StatusCode != StatusCode.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Status", item.StatusCode.ToString())
                    );

                //DispenseItemIdentifier Item Identifier
                if (item.DispenseItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Dispense Item Identifier", item.DispenseItemIdentifier.NarrativeText)
                    );

                //Prescription Item Identifier
                if (item.PrescriptionItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescription Item Identifier",
                            item.PrescriptionItemIdentifier.NarrativeText)
                    );

                //Therapeutic Good Identification
                if (item.TherapeuticGoodIdentification != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Identification",
                            item.TherapeuticGoodIdentification.NarrativeText)
                    );

                //Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Formula", item.Formula)
                    );

                //Label Instruction
                if (!item.LabelInstruction.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Label Instruction", item.LabelInstruction)
                    );

                if (item.QuantityToDispense != null)
                {
                    //QuantityToDispense - Quantity
                    if (item.QuantityToDispense.Quantity != null)
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Quantity", item.QuantityToDispense.Quantity.NarrativeText)
                        );

                    //QuantityToDispense - Dispensing Unit
                    if (item.QuantityToDispense.Unit != null)
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Dispensing Unit", item.QuantityToDispense.Unit.NarrativeText)
                        );
                    //QuantityToDispense - QuantityDescription
                    if (!item.QuantityToDispense.QuantityDescription.IsNullOrEmptyWhitespace())
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Quantity Description", item.QuantityToDispense.QuantityDescription)
                        );
                }

                //Brand Substitute Occurred
                if (item.BrandSubstituteOccurred != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Brand Substitute Occurred", item.BrandSubstituteOccurred)
                    );

                //Maximum Number of Repeats
                if (item.MaximumNumberOfRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Maximum Number of Repeats", item.MaximumNumberOfRepeats)
                    );

                //Maximum Number of Repeats
                if (item.MaximumNumberOfRepeats != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Number Of This Dispense", item.NumberOfThisDispense)
                    );

                // PBS Close the Gap Benefit
                if (item.PBSCloseTheGapBenefit != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Close the Gap Benefit", item.PBSCloseTheGapBenefit.NarrativeText)
                    );

                // PBS/RPBS Item Code
                if (item.PBSRPBSItemCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Item Code",
                            string.Format("{0} {1}", item.PBSRPBSItemCode.Code,
                                !item.PBSRPBSItemCode.NarrativeText.IsNullOrEmptyWhitespace()
                                    ? string.Format("{0}", item.PBSRPBSItemCode.NarrativeText)
                                    : string.Empty))
                    );

                // PBS/RPBS Manufacturer Code
                if (item.PBSRPBSManufacturerCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Manufacturer Code", item.PBSRPBSManufacturerCode.NarrativeText)
                    );

                // Claim Category Type
                if (item.ClaimCategoryType != ClaimCategoryType.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Claim Category Type",
                            item.ClaimCategoryType.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // Claim Category Type
                if (item.UnderCoPayment != ClaimCategoryType.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Under Co-payment",
                            item.UnderCoPayment.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // Claim Category Type
                if (item.EarySupplyWithPharmaceuticalBenefit.HasValue)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Early Supply With Pharmaceutical Benefit",
                            item.EarySupplyWithPharmaceuticalBenefit.Value.ToString(CultureInfo.InvariantCulture))
                    );


                // Additional Comments
                if (!item.AdditionalComments.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Additional Comments", item.AdditionalComments)
                    );


                // Patient Category
                if (item.PatientCategory.HasValue)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Patient Category",
                            item.PatientCategory.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // RACFId
                if (!item.RACFId.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("RACFId", item.RACFId)
                    );

                // PBS/RPBS Manufacturer Code
                if (item.PBSExtemporaneousIngredient != null)
                {
                    for (int index = 0; index < item.PBSExtemporaneousIngredient.Count; index++)
                    {
                        var extemporaneousIngredient = item.PBSExtemporaneousIngredient[index];

                        if (extemporaneousIngredient != null && extemporaneousIngredient.IngredientName != null ||
                            extemporaneousIngredient.IngredientQuantity != null)
                            narrativePBSExtemporaneousIngredient.Add
                            (
                                CreateNarrativeEntry(
                                    extemporaneousIngredient.IngredientName != null
                                        ? extemporaneousIngredient.IngredientName.NarrativeText
                                        : null,
                                    extemporaneousIngredient.IngredientQuantity != null
                                        ? extemporaneousIngredient.IngredientQuantity.NarrativeText
                                        : null)

                            );
                    }
                }

                strucDocTableList.AddRange
                (
                    new List<StrucDocTable>
                    {
                        narrative.Any()
                            ? PopulateTable
                            (
                                "Dispense Item",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrative
                            )
                            : null,
                        narrativeQuantityToDispense.Any()
                            ? PopulateTable
                            (
                                "Quantity To Dispense",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeQuantityToDispense
                            )
                            : null,
                        narrativePBSExtemporaneousIngredient.Any()
                            ? PopulateTable
                            (
                                "PBS Extemporaneous Ingredient",
                                null,
                                new[] {"Ingredient Name", "Ingredient Quantity"},
                                null,
                                narrativePBSExtemporaneousIngredient
                            )
                            : null
                    }
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Count > 0)
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// This method creates the narrative for the Pharmaceutical Benefit Items section
        /// </summary>
        /// <param name="pharmaceuticalBenefitItems">PharmaceuticalBenefitItems</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(PharmaceuticalBenefitItems pharmaceuticalBenefitItems)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();

            var narativeHeader = new List<string>
            {
                "Generic Name",
                "Brand",
                "Prescribed",
                "Supplied",
                "Form and Strength",
                "Quantity",
                "Repeats",
                "Code"
            };

            if (pharmaceuticalBenefitItems != null && pharmaceuticalBenefitItems.PharmaceuticalBenefitItemList != null)
            {
                foreach (var pharmaceuticalBenefitItem in pharmaceuticalBenefitItems.PharmaceuticalBenefitItemList)
                {
                    var medicationList = new List<Object>
                    {
                        pharmaceuticalBenefitItem.ItemGenericName,
                        pharmaceuticalBenefitItem.Brand.IsNullOrEmptyWhitespace()
                            ? null
                            : DELIMITERBOLD + pharmaceuticalBenefitItem.Brand,
                        pharmaceuticalBenefitItem.DateOfPrescribing != null
                            ? pharmaceuticalBenefitItem.DateOfPrescribing.NarrativeText()
                            : null,
                        pharmaceuticalBenefitItem.DateOfSupply != null
                            ? pharmaceuticalBenefitItem.DateOfSupply.NarrativeText()
                            : null,
                        pharmaceuticalBenefitItem.ItemFormAndStrength.IsNullOrEmptyWhitespace()
                            ? null
                            : pharmaceuticalBenefitItem.ItemFormAndStrength,
                        pharmaceuticalBenefitItem.Quantity.HasValue
                            ? pharmaceuticalBenefitItem.Quantity.Value.ToString(CultureInfo.InvariantCulture)
                            : null,
                        pharmaceuticalBenefitItem.NumberOfRepeats.HasValue
                            ? pharmaceuticalBenefitItem.NumberOfRepeats.Value.ToString(CultureInfo.InvariantCulture)
                            : null,
                        pharmaceuticalBenefitItem.PBSRPBSItemCode.IsNullOrEmptyWhitespace()
                            ? null
                            : pharmaceuticalBenefitItem.PBSRPBSItemCode
                    };

                    narrative.Add(medicationList);
                }
            }

            strucDocTableList.Add
            (
                PopulateTable
                (
                    null,
                    null,
                    narativeHeader.ToArray(),
                    new string[0],
                    narrative
                )
            );

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for an prescriberInstructionDetail Section
        /// </summary>
        /// <param name="prescriberInstructionDetail">A prescriberInstructionDetail</param>
        /// <param name="participationPrescriber">A IParticipationPrescriber</param>
        /// <param name="participationPrescriberOrganisation">A IParticipationPrescriberOrganisation</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(
            PrescriberInstructionDetail prescriberInstructionDetail,
            IParticipationPrescriber participationPrescriber,
            IParticipationPrescriberOrganisation participationPrescriberOrganisation
        )
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();

            if (prescriberInstructionDetail != null)
            {
                var columnHeaders = new List<string> {"Field", "Value"};

                if (prescriberInstructionDetail.PrescriberInstructionReceived != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Date and Time Prescriber Instruction Received",
                            prescriberInstructionDetail.PrescriberInstructionReceived)
                    );

                if (!String.IsNullOrEmpty(prescriberInstructionDetail.PrescriberInstruction))
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescriber Instruction",
                            prescriberInstructionDetail.PrescriberInstruction)
                    );

                if (prescriberInstructionDetail.PrescriberInstructionSource != PrescriberInstructionSource.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescriber Instruction Source",
                            prescriberInstructionDetail.PrescriberInstructionSource
                                .GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                if (prescriberInstructionDetail.PrescriberInstructionCommunicationMedium !=
                    PrescriberInstructionCommunicationMedium.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescriber Instruction Communication Medium",
                            prescriberInstructionDetail.PrescriberInstructionCommunicationMedium
                                .GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        null,
                        null,
                        columnHeaders.ToArray(),
                        null,
                        narrative
                    )
                );


                // Prescriber Instruction Detail
                if (prescriberInstructionDetail.PrescriberInstructionRecipient != null &&
                    prescriberInstructionDetail.PrescriberInstructionRecipient.Participant != null &&
                    prescriberInstructionDetail.PrescriberInstructionRecipient.Participant.Person != null &&
                    prescriberInstructionDetail.PrescriberInstructionRecipient.Participant.Person.PersonNames != null)
                {
                    var participant = prescriberInstructionDetail.PrescriberInstructionRecipient.Participant;

                    columnHeaders = new List<string>
                    {
                        "Provider Person Name",
                        "Provider Role",
                        "Qualifications",
                        "Address/Contact"
                    };

                    // Add narrative
                    narrative = new List<List<string>>();
                    narrative.Add(
                        new List<String>
                        {
                            participant.Person.PersonNames == null
                                ? null
                                : BuildPersonNames(participant.Person.PersonNames),
                            "Pharmacist",
                            // Fixed as per Spec
                            participant.Person.Qualifications,
                            CreateAddress(participant.Addresses, participant.ElectronicCommunicationDetails)

                        }
                    );

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Prescriber Instruction Recipient",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrative
                        )
                    );
                }

                // Prescriber
                if (participationPrescriber != null && participationPrescriber.Participant != null &&
                    participationPrescriber.Participant.Person != null)
                {
                    var participant = participationPrescriber.Participant;

                    columnHeaders = new List<string>
                    {
                        "Provider Person Name",
                        "Provider Role",
                        "Entitlements",
                        "Address/Contact"

                    };

                    // Add narrative
                    narrative = new List<List<string>>();
                    narrative.Add(
                        new List<String>
                        {
                            participant.Person == null ? null : BuildPersonNames(participant.Person.PersonNames),
                            participationPrescriber.Role != null
                                ? participationPrescriber.Role.NarrativeText
                                : String.Empty,
                            // Fixed as per Spec
                            participant.Person != null && participant.Person.Entitlements != null &&
                            participant.Person.Entitlements.Count > 0
                                ? CreateEntitlement(participant.Person.Entitlements)
                                : String.Empty,
                            CreateAddress(participant.Addresses, participant.ElectronicCommunicationDetails),
                            //participationPrescriber.Participant.Person.Occupation != null ? participationPrescriber.Participant.Person.Occupation.Value.GetAttributeValue<NameAttribute, String>(x => x.Name) : String.Empty,
                            //participationPrescriber.Participant.Person.Qualifications
                        }
                    );

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Prescriber",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrative
                        )
                    );
                }

                // Prescriber Organisation
                if (participationPrescriberOrganisation != null &&
                    participationPrescriberOrganisation.Participant != null)
                {
                    var participant = participationPrescriberOrganisation.Participant;

                    columnHeaders = new List<string>
                    {
                        "Organisation Role",
                        "Address/Contact",
                        "Organisation Name",
                        "Organisation Name Usage",
                        "Department/Unit"
                    };

                    IOrganisation organisation = null;
                    if (participationPrescriberOrganisation.Participant.Organisation != null)
                        organisation = participationPrescriberOrganisation.Participant.Organisation;

                    // Add narrative
                    narrative = new List<List<string>>();
                    narrative.Add(
                        new List<String>
                        {
                            participationPrescriberOrganisation.Role != null
                                ? participationPrescriberOrganisation.Role.NarrativeText
                                : String.Empty,
                            // Fixed as per Spec
                            CreateAddress(participant.Addresses, participant.ElectronicCommunicationDetails),
                            organisation != null ? organisation.Name : String.Empty,
                            organisation != null && organisation.NameUsage != null
                                ? organisation.NameUsage.Value.GetAttributeValue<NameAttribute, String>(x => x.Name)
                                : String.Empty,
                            organisation != null
                                ? participationPrescriberOrganisation.Participant.Organisation.Department
                                : String.Empty
                        }
                    );

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Prescriber Organisation",
                            null,
                            columnHeaders.ToArray(),
                            null,
                            narrative
                        )
                    );
                }
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Count > 0)
            {
                strucDocText.table = strucDocTableList.ToArray();
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for an PrescriptionRequestItem Section
        /// </summary>
        /// <param name="item">The PrescriptionRequestItem</param>
        /// <param name="subjectOfCare">The subjectOfCare</param>
        /// <param name="dispenserOrganisation">The dispenser Organisation</param>
        /// <param name="requesterNote">The requesterNote </param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(PrescriptionRequestItem item, IParticipationSubjectOfCare subjectOfCare,
            IParticipationDispenserOrganisation dispenserOrganisation, String requesterNote)
        {
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<String>>();
            var narrativeTiming = new List<List<String>>();
            var narrativeAdministrationDetails = new List<List<String>>();
            var narrativeStructuredDose = new List<List<String>>();
            var narrativeQuantityToDispense = new List<List<String>>();
            var narrativePBSExtemporaneousIngredient = new List<List<String>>();
            var narrativeEntitlements = new List<List<String>>();

            if (item != null)
            {

                //Prescription Request Item Identifier
                if (item.PrescriptionRequestItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Prescription Request Item Identifier",
                            item.PrescriptionRequestItemIdentifier.NarrativeText)
                    );

                //Dispense Item Identifier
                if (item.DispenseItemIdentifier != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Dispense Item Identifier", item.DispenseItemIdentifier.NarrativeText)
                    );

                //Therapeutic Good Identification
                if (item.TherapeuticGoodIdentification != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Therapeutic Good Identification",
                            item.TherapeuticGoodIdentification.NarrativeText)
                    );

                //Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Formula", item.Formula)
                    );

                //Instruction
                if (!item.Directions.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Directions", item.Directions)
                    );

                if (item.StructuredDose != null)
                {
                    //Quantity of Therapeutic Good
                    if (item.StructuredDose.Quantity != null)
                        narrativeStructuredDose.Add
                        (
                            CreateNarrativeEntry("Quantity", item.StructuredDose.Quantity.NarrativeText)
                        );

                    //Structured Dose - Dose Unit
                    if (item.StructuredDose.Unit != null)
                        narrativeStructuredDose.Add
                        (
                            CreateNarrativeEntry("Dose Unit", item.StructuredDose.Unit.NarrativeText)
                        );

                    //Structured Dose - Quantity Description
                    if (item.StructuredDose.QuantityDescription != null)
                        narrativeStructuredDose.Add
                        (
                            CreateNarrativeEntry("Quantity Description", item.StructuredDose.QuantityDescription)
                        );
                }

                if (item.Timing != null)
                {
                    //Timing
                    if (!item.Timing.TimingDescription.IsNullOrEmptyWhitespace())
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("Timing Description", item.Timing.TimingDescription)
                        );

                    //Structured Timing EffectiveTime
                    if (item.Timing.StructuredTiming != null && item.Timing.StructuredTiming.EffectiveTime != null)
                    {

                        if (!item.Timing.StructuredTiming.NarrativeText.IsNullOrEmptyWhitespace())
                        {

                            narrativeTiming.Add
                            (
                                CreateNarrativeEntry("EffectiveTime", item.Timing.StructuredTiming.NarrativeText)
                            );

                        }
                        else
                        {
                            //Structured Timing EffectiveTime
                            narrativeTiming.Add
                            (
                                new List<string>
                                {
                                    "EffectiveTime",
                                    CreateTimingEntry(item.Timing.StructuredTiming.EffectiveTime)
                                }
                            );
                        }
                    }

                    //PRN
                    if (item.Timing.PRN.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("PRN", item.Timing.PRN.Value.ToString(CultureInfo.InvariantCulture))
                        );

                    //Timing - StartCriterion
                    if (item.Timing.StartCriterion.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StartCriterion",
                                item.Timing.StartCriterion.Value.ToString(CultureInfo.InvariantCulture))
                        );

                    //Timing - StartDate
                    if (item.Timing.StartDate != null)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StartDate", item.Timing.StartDate)
                        );

                    //Timing - StopCriterion
                    if (item.Timing.StopCriterion.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StopCriterion",
                                item.Timing.StopCriterion.Value.ToString(CultureInfo.InvariantCulture))
                        );

                    //Timing - StopDate
                    if (item.Timing.StopDate != null)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("StopDate", item.Timing.StopDate)
                        );

                    if (item.Timing.NumberOfAdministrations != null)
                    {
                        var numberOfAdministrations = string.Empty;

                        //Timing - Number of Administrations - Numerator
                        if (item.Timing.NumberOfAdministrations.NullFlavor != null)
                            numberOfAdministrations +=
                                item.Timing.NumberOfAdministrations.NullFlavor.Value
                                    .GetAttributeValue<NameAttribute, String>(x => x.Name) + " ";

                        //Timing - Number of Administrations - Denominator
                        if (item.Timing.NumberOfAdministrations.Denominator != null)
                            numberOfAdministrations += item.Timing.NumberOfAdministrations.Denominator.NarrativeText;

                        //Timing - Number of Administrations - Numerator
                        if (item.Timing.NumberOfAdministrations.Numerator != null)
                        {
                            if (!numberOfAdministrations.IsNullOrEmptyWhitespace())
                                numberOfAdministrations += " - ";

                            numberOfAdministrations +=
                                item.Timing.NumberOfAdministrations.Numerator.NarrativeText + " ";
                        }

                        if (!numberOfAdministrations.IsNullOrEmptyWhitespace())
                            narrativeTiming.Add
                            (
                                CreateNarrativeEntry("Number of Administrations", numberOfAdministrations)
                            );
                    }

                    //Timing - LongTerm
                    if (item.Timing.LongTerm.HasValue)
                        narrativeTiming.Add
                        (
                            CreateNarrativeEntry("Long-Term",
                                item.Timing.LongTerm.Value.ToString(CultureInfo.InvariantCulture))
                        );
                }

                if (item.AdministrationDetails != null)
                {
                    //AdministrationDetails - Route
                    if (item.AdministrationDetails.Route != null)
                        narrativeAdministrationDetails.Add
                        (
                            CreateNarrativeEntry("Route", item.AdministrationDetails.Route.NarrativeText)
                        );

                    //AdministrationDetails - AnatomicalSite
                    if (item.AdministrationDetails.AnatomicalSite != null)
                        narrativeAdministrationDetails.Add
                        (
                            CreateNarrativeEntry("AnatomicalSite",
                                item.AdministrationDetails.AnatomicalSite.NarrativeText)
                        );

                    //AdministrationDetails - MedicationDeliveryMethod
                    if (item.AdministrationDetails.MedicationDeliveryMethod != null)
                        narrativeAdministrationDetails.Add
                        (
                            CreateNarrativeEntry("Medication Delivery Method",
                                item.AdministrationDetails.MedicationDeliveryMethod.NarrativeText)
                        );
                }

                if (item.QuantityToDispense != null)
                {
                    //QuantityToDispense - Quantity
                    if (item.QuantityToDispense.Quantity != null)
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Quantity", item.QuantityToDispense.Quantity.NarrativeText)
                        );

                    //QuantityToDispense - Dispensing Unit
                    if (item.QuantityToDispense.Unit != null)
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Dispensing Unit", item.QuantityToDispense.Unit.NarrativeText)
                        );

                    //QuantityToDispense - QuantityDescription
                    if (!item.QuantityToDispense.QuantityDescription.IsNullOrEmptyWhitespace())
                        narrativeQuantityToDispense.Add
                        (
                            CreateNarrativeEntry("Quantity Description", item.QuantityToDispense.QuantityDescription)
                        );
                }

                //Brand Substitute Allowed
                if (item.BrandSubstituteNotAllowed != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Brand Substitute Not Allowed", item.BrandSubstituteNotAllowed)
                    );

                // PBS Prescription Type
                if (item.PBSPrescriptionType.HasValue)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Prescription Type",
                            item.PBSPrescriptionType.Value.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // Medical Benefit Category Type
                if (item.MedicalBenefitCategoryType.HasValue &&
                    item.MedicalBenefitCategoryType.Value != MedicalBenefitCategoryType.Undefined)
                    narrative.Add
                    (
                        CreateNarrativeEntry("Medical Benefit Category Type",
                            item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x => x.Name))
                    );

                // PBS Close the Gap Benefit
                if (item.PBSCloseTheGapBenefit != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS Close the Gap Benefit", item.PBSCloseTheGapBenefit.NarrativeText)
                    );

                // PBS/RPBS Item Code
                if (item.PBSRPBSItemCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Item Code",
                            string.Format("{0} {1}", item.PBSRPBSItemCode.Code,
                                !item.PBSRPBSItemCode.NarrativeText.IsNullOrEmptyWhitespace()
                                    ? string.Format("{0}", item.PBSRPBSItemCode.NarrativeText)
                                    : string.Empty))
                    );

                // PBS/RPBS Manufacturer Code
                if (item.PBSRPBSManufacturerCode != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Manufacturer Code", item.PBSRPBSManufacturerCode.NarrativeText)
                    );


                // PBS/RPBS Manufacturer Code
                if (item.PBSExtemporaneousIngredient != null)
                {
                    for (int index = 0; index < item.PBSExtemporaneousIngredient.Count; index++)
                    {
                        var extemporaneousIngredient = item.PBSExtemporaneousIngredient[index];

                        if (extemporaneousIngredient != null && extemporaneousIngredient.IngredientName != null ||
                            extemporaneousIngredient.IngredientQuantity != null)
                            narrativePBSExtemporaneousIngredient.Add
                            (
                                CreateNarrativeEntry(
                                    extemporaneousIngredient.IngredientQuantity != null
                                        ? extemporaneousIngredient.IngredientQuantity.NarrativeText
                                        : null,
                                    extemporaneousIngredient.IngredientName != null
                                        ? extemporaneousIngredient.IngredientName.NarrativeText
                                        : null)
                            );

                    }
                }

                // PBS/RPBS Authority Prescription Number
                if (!item.PBSRPBSAuthorityPrescriptionNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Authority Prescription Number",
                            item.PBSRPBSAuthorityPrescriptionNumber)
                    );

                // PBS/RPBS Authority Approval Number
                if (!item.PBSRPBSAuthorityApprovalNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("PBS/RPBS Authority Approval Number", item.PBSRPBSAuthorityApprovalNumber)
                    );


                // Streamlined Authority Approval Number
                if (!item.StreamlinedAuthorityApprovalNumber.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Streamlined Authority Approval Number",
                            item.StreamlinedAuthorityApprovalNumber)
                    );

                // State Authority Number
                if (item.StateAuthorityNumber != null)
                    narrative.Add
                    (
                        CreateNarrativeEntry("State Authority Number", item.StateAuthorityNumber.NarrativeText)
                    );


                // Additional Comments
                if (!item.AdditionalComments.IsNullOrEmptyWhitespace())
                    narrative.Add
                    (
                        CreateNarrativeEntry("Additional Comments", item.AdditionalComments)
                    );

                // The Prescription Note
                if (!requesterNote.IsNullOrEmptyWhitespace())
                {
                    narrative.Add
                    (
                        CreateNarrativeEntry("Requester Note", requesterNote)
                    );
                }

                // Entitlements Prescriber
                if (dispenserOrganisation.Participant.Entitlements != null &&
                    dispenserOrganisation.Participant.Entitlements.Count > 0)
                {
                    foreach (var entitlement in dispenserOrganisation.Participant.Entitlements)
                    {
                        CodableText codeableTextEntry = null;

                        if (entitlement.Type != EntitlementType.Undefined)
                        {
                            codeableTextEntry = new CodableText
                            {
                                Code = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                DisplayName = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                OriginalText = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                CodeSystem = CodingSystem.NCTISEntitlementTypeValues
                            };
                        }

                        narrativeEntitlements.Add
                        (
                            CreateNarrativeEntry(entitlement, codeableTextEntry)
                        );
                    }
                }

                strucDocTableList.AddRange
                (
                    new List<StrucDocTable>
                    {
                        narrative.Any()
                            ? PopulateTable
                            (
                                "Prescription Request Item",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrative
                            )
                            : null,
                        narrativeTiming.Any()
                            ? PopulateTable
                            (
                                "Timing",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeTiming
                            )
                            : null,
                        narrativeStructuredDose.Any()
                            ? PopulateTable
                            (
                                "Structured Dose",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeStructuredDose
                            )
                            : null,
                        narrativeQuantityToDispense.Any()
                            ? PopulateTable
                            (
                                "Quantity To Dispense",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeQuantityToDispense
                            )
                            : null,
                        narrativePBSExtemporaneousIngredient.Any()
                            ? PopulateTable
                            (
                                "PBS Extemporaneous Ingredient",
                                null,
                                new[] {"Ingredient Name", "Ingredient Quantity"},
                                null,
                                narrativePBSExtemporaneousIngredient
                            )
                            : null,
                        narrativeAdministrationDetails.Any()
                            ? PopulateTable
                            (
                                "Administration Details",
                                null,
                                new[] {"Field", "Value"},
                                null,
                                narrativeAdministrationDetails
                            )
                            : null,
                        narrativeEntitlements.Any()
                            ? PopulateTable
                            (
                                "Entitlements",
                                null,
                                new[] {"Entitlements", "Value"},
                                null,
                                narrativeEntitlements
                            )
                            : null
                    }
                );
            }

            var strucDocText = new StrucDocText();

            if (strucDocTableList.Count > 0)
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Physical Measurements
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(PhysicalMeasurement physicalMeasurement)
        {
            var strucDocTableList = new List<StrucDocTable>();

            if (physicalMeasurement != null)
            {

                if (physicalMeasurement.HeadCircumference != null)
                {
                    var headCircumferenceNarrative = new List<List<String>>();

                    // Body Part Circumference Date Time
                    if (physicalMeasurement.HeadCircumference.BodyPartCircumferenceDateTime != null)
                    {
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Body Part Circumference DateTime",
                                physicalMeasurement.HeadCircumference.BodyPartCircumferenceDateTime)
                        );
                    }

                    // Name Of Location
                    if (physicalMeasurement.HeadCircumference.NameOfLocation != null)
                    {
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Name Of Location",
                                physicalMeasurement.HeadCircumference.NameOfLocation.NarrativeText)
                        );
                    }

                    // Circumference
                    if (physicalMeasurement.HeadCircumference.Circumference != null)
                    {
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Circumference",
                                physicalMeasurement.HeadCircumference.Circumference.NarrativeText)
                        );
                    }

                    // Circumference Normal Status
                    if (physicalMeasurement.HeadCircumference.CircumferenceNormalStatus != null)
                    {
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Circumference Normal Status",
                                physicalMeasurement.HeadCircumference.CircumferenceNormalStatus.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.Name))
                        );
                    }

                    // Circumference Reference Range Details
                    if (physicalMeasurement.HeadCircumference.CircumferenceReferenceRangeDetails != null &&
                        physicalMeasurement.HeadCircumference.CircumferenceReferenceRangeDetails.Any())
                    {
                        var narrativeText =
                            physicalMeasurement.HeadCircumference.CircumferenceReferenceRangeDetails.Aggregate(
                                string.Empty,
                                (current, circumferenceReferenceRangeDetails) =>
                                    current + DELIMITERBREAK + circumferenceReferenceRangeDetails.NarrativeText);

                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Circumference Reference Range Details", narrativeText)
                        );
                    }

                    // Comment (Measurement Comment)
                    if (physicalMeasurement.HeadCircumference.Comment != null)
                    {
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Comment (Measurement Comment)",
                                physicalMeasurement.HeadCircumference.Comment)
                        );
                    }

                    // Confounding Factor
                    if (physicalMeasurement.HeadCircumference.ConfoundingFactor != null &&
                        physicalMeasurement.HeadCircumference.ConfoundingFactor.Any())
                    {
                        var narrativeText = physicalMeasurement.HeadCircumference.ConfoundingFactor.Aggregate(
                            string.Empty,
                            (current, confoundingFactor) =>
                                current + (DELIMITERBREAK + confoundingFactor.NarrativeText));

                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Confounding Factor", narrativeText)
                        );
                    }

                    // Information Provider
                    if (physicalMeasurement.HeadCircumference.InformationProvider != null)
                    {
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Information Provider",
                                CreateInformationProvider(physicalMeasurement.HeadCircumference.InformationProvider))
                        );
                    }

                    if (physicalMeasurement.HeadCircumference.Device != null)
                        headCircumferenceNarrative.Add
                        (
                            CreateNarrativeEntry("Device",
                                string.Format("{0}", physicalMeasurement.HeadCircumference.Device.SoftwareName))
                        );

                    strucDocTableList.AddRange
                    (
                        new List<StrucDocTable>
                        {
                            headCircumferenceNarrative.Any()
                                ? PopulateTable
                                (
                                    "Head Circumference (BODY PART CIRCUMFERENCE)",
                                    null,
                                    new[] {"Field", "Value"},
                                    null,
                                    headCircumferenceNarrative
                                )
                                : null,
                        }
                    );
                }

                if (physicalMeasurement.PhysicalMeasurementBodyWeight != null)
                {
                    var physicalMeasurementBodyWeightNarrative = new List<List<String>>();

                    // Body Weight DateTime
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.BodyWeightDateTime != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Body Weight DateTime",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.BodyWeightDateTime)
                        );
                    }

                    // Weight
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.Weight != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Weight",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.Weight.NarrativeText)
                        );
                    }

                    // Weight Normal Status
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.WeightNormalStatus != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Weight Normal Status",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.WeightNormalStatus.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.Name))
                        );
                    }

                    // Weight Reference Range Details
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.WeightReferenceRangeDetails != null &&
                        physicalMeasurement.PhysicalMeasurementBodyWeight.WeightReferenceRangeDetails.Any())
                    {
                        var narrativeText =
                            physicalMeasurement.PhysicalMeasurementBodyWeight.WeightReferenceRangeDetails.Aggregate(
                                string.Empty,
                                (current, circumferenceReferenceRangeDetails) =>
                                    current + DELIMITERBREAK + circumferenceReferenceRangeDetails.NarrativeText);

                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Weight Reference Range Details", narrativeText)
                        );
                    }

                    // Comment (Measurement Comment)
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.Comment != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Comment (Measurement Comment)",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.Comment)
                        );
                    }

                    // Weight
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.StateOfDress != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("State Of Dress",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.StateOfDress)
                        );
                    }

                    // Pregnant?
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.Pregnant != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Pregnant?",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.Pregnant.ToString())
                        );
                    }

                    // Confounding Factor
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.ConfoundingFactor != null &&
                        physicalMeasurement.PhysicalMeasurementBodyWeight.ConfoundingFactor.Any())
                    {
                        var narrativeText =
                            physicalMeasurement.PhysicalMeasurementBodyWeight.ConfoundingFactor.Aggregate(string.Empty,
                                (current, confoundingFactor) =>
                                    current + (DELIMITERBREAK + confoundingFactor.NarrativeText));

                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Confounding Factor", narrativeText)
                        );
                    }

                    // Weight Estimation Formula
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.WeightEstimationFormula != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Weight Estimation Formula",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.WeightEstimationFormula)
                        );
                    }

                    // Body Weight Instance Identifier
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.BodyWeightInstanceIdentifier != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Body Weight Instance Identifier",
                                physicalMeasurement.PhysicalMeasurementBodyWeight.BodyWeightInstanceIdentifier
                                    .NarrativeText)
                        );
                    }

                    // Information Provider
                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.InformationProvider != null)
                    {
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Information Provider",
                                CreateInformationProvider(physicalMeasurement.PhysicalMeasurementBodyWeight
                                    .InformationProvider))
                        );
                    }

                    if (physicalMeasurement.PhysicalMeasurementBodyWeight.Device != null)
                        physicalMeasurementBodyWeightNarrative.Add
                        (
                            CreateNarrativeEntry("Device",
                                string.Format("{0}",
                                    physicalMeasurement.PhysicalMeasurementBodyWeight.Device.SoftwareName))
                        );

                    strucDocTableList.AddRange
                    (
                        new List<StrucDocTable>
                        {
                            physicalMeasurementBodyWeightNarrative.Any()
                                ? PopulateTable
                                (
                                    "Body Weight",
                                    null,
                                    new[] {"Field", "Value"},
                                    null,
                                    physicalMeasurementBodyWeightNarrative
                                )
                                : null,
                        }
                    );
                }

                if (physicalMeasurement.PhysicalMeasurementBodyHeightLength != null)
                {
                    var physicalMeasurementBodyHeightLengthNarrative = new List<List<String>>();

                    // Body Weight DateTime
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.BodyHeightLengthDateTime != null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Body Height Length Date Time",
                                physicalMeasurement.PhysicalMeasurementBodyHeightLength.BodyHeightLengthDateTime)
                        );
                    }

                    // Height/Length
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.HeightLength != null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Height/Length",
                                physicalMeasurement.PhysicalMeasurementBodyHeightLength.HeightLength.NarrativeText)
                        );
                    }

                    // Weight Normal Status
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.HeightLengthNormalStatus != null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Height Length Normal Status",
                                physicalMeasurement.PhysicalMeasurementBodyHeightLength.HeightLengthNormalStatus.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.Name))
                        );
                    }

                    // Height/length Reference Range Details
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.HeightLengthReferenceRangeDetails !=
                        null && physicalMeasurement.PhysicalMeasurementBodyHeightLength
                            .HeightLengthReferenceRangeDetails.Any())
                    {
                        var narrativeText =
                            physicalMeasurement.PhysicalMeasurementBodyHeightLength.HeightLengthReferenceRangeDetails
                                .Aggregate(string.Empty,
                                    (current, circumferenceReferenceRangeDetails) =>
                                        current + DELIMITERBREAK + circumferenceReferenceRangeDetails.NarrativeText);

                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Height/length Reference Range Details", narrativeText)
                        );
                    }

                    // Comment (Measurement Comment)
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.Comment != null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Comment (Measurement Comment)",
                                physicalMeasurement.PhysicalMeasurementBodyHeightLength.Comment)
                        );
                    }

                    // Position
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.Position != null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Position",
                                physicalMeasurement.PhysicalMeasurementBodyHeightLength.Position)
                        );
                    }

                    // Confounding Factor
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.ConfoundingFactor != null &&
                        physicalMeasurement.PhysicalMeasurementBodyHeightLength.ConfoundingFactor.Any())
                    {
                        var narrativeText =
                            physicalMeasurement.PhysicalMeasurementBodyHeightLength.ConfoundingFactor.Aggregate(
                                string.Empty,
                                (current, confoundingFactor) => current + (DELIMITERBREAK + confoundingFactor));

                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Confounding Factor", narrativeText)
                        );
                    }

                    // Body Weight Instance Identifier
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.BodyHeightLengthInstanceIdentifier !=
                        null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Body Height Length Instance Identifier",
                                physicalMeasurement.PhysicalMeasurementBodyHeightLength
                                    .BodyHeightLengthInstanceIdentifier.NarrativeText)
                        );
                    }

                    // Information Provider
                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.InformationProvider != null)
                    {
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Information Provider",
                                CreateInformationProvider(physicalMeasurement.PhysicalMeasurementBodyHeightLength
                                    .InformationProvider))
                        );
                    }

                    if (physicalMeasurement.PhysicalMeasurementBodyHeightLength.Device != null)
                        physicalMeasurementBodyHeightLengthNarrative.Add
                        (
                            CreateNarrativeEntry("Device",
                                string.Format("{0}",
                                    physicalMeasurement.PhysicalMeasurementBodyHeightLength.Device.SoftwareName))
                        );

                    strucDocTableList.AddRange
                    (
                        new List<StrucDocTable>
                        {
                            physicalMeasurementBodyHeightLengthNarrative.Any()
                                ? PopulateTable
                                (
                                    "Body Height/Length",
                                    null,
                                    new[] {"Field", "Value"},
                                    null,
                                    physicalMeasurementBodyHeightLengthNarrative
                                )
                                : null,
                        }
                    );
                }

                if (physicalMeasurement.PhysicalMeasurementBodyMassIndex != null)
                {
                    var physicalMeasurementBodyMassIndexNarrative = new List<List<String>>();

                    // Body Mass Index DateTime
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexDateTime != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Body Mass Index DateTime",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexDateTime)
                        );
                    }

                    // Body Mass Index
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndex != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Body Mass Index",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndex.NarrativeText)
                        );
                    }

                    // Body Mass Index Normal Status
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexNormalStatus.HasValue)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Body Mass Index Normal Status",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexNormalStatus.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.Name))
                        );
                    }

                    // Height/length Reference Range Details
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexReferenceRangeDetails !=
                        null && physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexReferenceRangeDetails
                            .Any())
                    {
                        var narrativeText =
                            physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexReferenceRangeDetails
                                .Aggregate(string.Empty,
                                    (current, circumferenceReferenceRangeDetails) =>
                                        current + DELIMITERBREAK + circumferenceReferenceRangeDetails.NarrativeText);

                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Body Mass Index Reference Range Details", narrativeText)
                        );
                    }

                    // Comment (Measurement Comment)
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.Comment != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Comment (Measurement Comment)",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.Comment)
                        );
                    }

                    // Method
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.Method != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Method",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.Method.NarrativeText)
                        );
                    }

                    // Formula (BMI Calculation Formula)
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.Formula != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Formula (BMI Calculation Formula)",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.Formula)
                        );
                    }

                    // Body Weight Instance Identifier
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexInstanceIdentifier != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Body Mass Index Instance Identifier",
                                physicalMeasurement.PhysicalMeasurementBodyMassIndex.BodyMassIndexInstanceIdentifier
                                    .NarrativeText)
                        );
                    }

                    // Information Provider
                    if (physicalMeasurement.PhysicalMeasurementBodyMassIndex.InformationProvider != null)
                    {
                        physicalMeasurementBodyMassIndexNarrative.Add
                        (
                            CreateNarrativeEntry("Information Provider",
                                CreateInformationProvider(physicalMeasurement.PhysicalMeasurementBodyMassIndex
                                    .InformationProvider))
                        );
                    }

                    strucDocTableList.AddRange
                    (
                        new List<StrucDocTable>
                        {
                            physicalMeasurementBodyMassIndexNarrative.Any()
                                ? PopulateTable
                                (
                                    "Body Mass Index",
                                    null,
                                    new[] {"Field", "Value"},
                                    null,
                                    physicalMeasurementBodyMassIndexNarrative
                                )
                                : null,
                        }
                    );
                }
            }

            var strucDocText = new StrucDocText();

            // Structured Tables
            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for the Measurement Information
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(MeasurementInformation measurementInformation)
        {
            StrucDocText strucDocText = null;
            List<StrucDocTable> strucDocTableList = null;

            if (measurementInformation != null)
            {

                strucDocTableList = new List<StrucDocTable>();

                var headerList = new List<String>()
                {
                    "Observation Date",
                    "Measurement Type",
                    "Measurement"
                };

                var narrative = new List<List<String>>();

                if (measurementInformation.HeadCircumference != null)
                    narrative.Add(new List<string>
                    {
                        measurementInformation.ObservationDate != null
                            ? measurementInformation.ObservationDate.NarrativeText()
                            : string.Empty,
                        "Head Circumference",
                        measurementInformation.HeadCircumference != null
                            ? measurementInformation.HeadCircumference.NarrativeText
                            : string.Empty
                    });

                if (measurementInformation.BodyHeight != null)
                    narrative.Add(new List<string>
                    {
                        measurementInformation.ObservationDate != null
                            ? measurementInformation.ObservationDate.NarrativeText()
                            : string.Empty,
                        "Body Height",
                        measurementInformation.BodyHeight != null
                            ? measurementInformation.BodyHeight.NarrativeText
                            : string.Empty
                    });

                if (measurementInformation.BodyWeight != null)
                    narrative.Add(new List<string>
                    {
                        measurementInformation.ObservationDate != null
                            ? measurementInformation.ObservationDate.NarrativeText()
                            : string.Empty,
                        "Body Weight",
                        measurementInformation.BodyWeight != null
                            ? measurementInformation.BodyWeight.NarrativeText
                            : string.Empty
                    });

                if (measurementInformation.BodyMassIndex != null)
                    narrative.Add(new List<string>
                    {
                        measurementInformation.ObservationDate != null
                            ? measurementInformation.ObservationDate.NarrativeText()
                            : string.Empty,
                        "Body Mass Index",
                        measurementInformation.BodyMassIndex != null
                            ? measurementInformation.BodyMassIndex.NarrativeText
                            : string.Empty
                    });


                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Measurement Information",
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );

                strucDocText = new StrucDocText();
                // Structured Tables
                if (strucDocTableList.Any() && narrative.Any())
                {
                    strucDocText.table = strucDocTableList.ToArray();
                }
                else
                {
                    strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
                }
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for the Measurement Information
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(List<MeasurementInformation> measurementInformations)
        {
            StrucDocText strucDocText = null;
            List<StrucDocTable> strucDocTableList = null;

            if (measurementInformations != null && measurementInformations.Any())
            {
                var headerList = new List<String>()
                {
                    "Observation Date",
                    "Head Circumference",
                    "Body Height",
                    "Body Mass Index",

                };

                var narrative = new List<List<String>>();
                foreach (var item in measurementInformations)
                {

                    strucDocText = new StrucDocText();
                    strucDocTableList = new List<StrucDocTable>();

                    narrative.Add(new List<string>
                        {
                            item.ObservationDate != null ? item.ObservationDate.NarrativeText() : string.Empty,
                            item.HeadCircumference != null ? item.HeadCircumference.NarrativeText : string.Empty,
                            item.BodyHeight != null ? item.BodyHeight.NarrativeText : string.Empty,
                            item.BodyWeight != null ? item.BodyWeight.NarrativeText : string.Empty,
                            item.BodyMassIndex != null ? item.BodyMassIndex.NarrativeText : string.Empty
                        }
                    );
                }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Measurement Information",
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );


                // Structured Tables
                if (strucDocTableList.Any())
                {
                    strucDocText.table = strucDocTableList.ToArray();
                }
            }
            else
            {
                strucDocText = new StrucDocText {Text = new[] {"This section contains no entries"}};
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for the Questionnaire
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(Questionnaire questionnaire)
        {
            StrucDocText strucDocText = null;
            List<StrucDocTable> strucDocTableList = null;

            if (questionnaire != null)
            {
                var headerList = new List<String>()
                {
                    "Date Time",
                    "Question",
                    "Responses"
                };

                strucDocText = new StrucDocText();
                strucDocTableList = new List<StrucDocTable>();

                var narrative = new List<List<String>>();

                if (questionnaire.AssessmentItems != null)
                    foreach (var assessmentItem in questionnaire.AssessmentItems)
                    {

                        string response = string.Empty;
                        if (!assessmentItem.FreeText.IsNullOrEmptyWhitespace())
                        {
                            response = assessmentItem.FreeText;
                        }

                        if (assessmentItem.AnswersValue.HasValue)
                        {
                            response =
                                assessmentItem.AnswersValue.Value.GetAttributeValue<NameAttribute, String>(x => x.Name);
                        }

                        narrative.Add(
                            new List<string>
                            {
                                assessmentItem.DateTime != null ? assessmentItem.DateTime.NarrativeText() : null,
                                assessmentItem.QuestionData != null ? assessmentItem.QuestionData.NarrativeText : null,
                                response
                            }
                        );
                    }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        questionnaire.SectionCode.HasValue
                            ? questionnaire.SectionCode.Value.GetAttributeValue<NameAttribute, String>(x => x.Name)
                            : null,
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );

                // Structured Tables
                if (strucDocTableList.Any())
                {
                    strucDocText.table = strucDocTableList.ToArray();
                }
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for the Health Check Assessment
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(HealthCheckAssesment healthCheckAssesment)
        {
            StrucDocText strucDocText = null;
            List<StrucDocTable> strucDocTableList = null;

            if (healthCheckAssesment != null)
            {
                var headerList = new List<String>()
                {
                    "Date Time",
                    "Question",
                    "Responses"
                };

                strucDocText = new StrucDocText();
                strucDocTableList = new List<StrucDocTable>();

                var narrative = new List<List<String>>();

                if (healthCheckAssesment.AssessmentItems != null)
                    foreach (var assessmentItem in healthCheckAssesment.AssessmentItems)
                    {

                        string response = string.Empty;
                        if (!assessmentItem.FreeText.IsNullOrEmptyWhitespace())
                        {
                            response = assessmentItem.FreeText;
                        }

                        if (assessmentItem.AnswersValue.HasValue)
                        {
                            response =
                                assessmentItem.AnswersValue.Value.GetAttributeValue<NameAttribute, String>(x => x.Name);
                        }

                        narrative.Add(
                            new List<string>
                            {
                                assessmentItem.DateTime != null ? assessmentItem.DateTime.NarrativeText() : null,
                                assessmentItem.QuestionData != null ? assessmentItem.QuestionData.NarrativeText : null,
                                response
                            }
                        );
                    }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        healthCheckAssesment.SectionCode.HasValue
                            ? healthCheckAssesment.SectionCode.Value.GetAttributeValue<NameAttribute, String>(x =>
                                x.Name)
                            : null,
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );

                // Structured Tables
                if (strucDocTableList.Any())
                {
                    strucDocText.table = strucDocTableList.ToArray();
                }
            }

            return strucDocText;
        }

        /// <summary>
        /// Create a Narrative for Birth Details
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(BirthDetails birthDetails)
        {
            StrucDocText strucDocText = null;
            List<StrucDocTable> strucDocTableList = null;
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            if (birthDetails != null)
            {
                var headerList = new List<String>()
                {
                    "Attachment Name",
                    "Attachment"
                };

                strucDocText = new StrucDocText();
                strucDocTableList = new List<StrucDocTable>();

                var narrative = new List<List<Object>>();

                foreach (var externalData in birthDetails.ExternalData)
                {

                    narrative.Add(
                        new List<Object>
                        {
                            externalData.Caption,
                            CreateEncapsulatedData(externalData, ref renderMultiMediaList)
                        }
                    );

                }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Attachments",
                        null,
                        headerList.ToArray(),
                        new string[0],
                        narrative
                    )
                );

                // Structured Tables
                if (strucDocTableList.Any())
                {
                    strucDocText.table = strucDocTableList.ToArray();
                }

                if (renderMultiMediaList.Any())
                {
                    strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
                }
            }


            return strucDocText;
        }




        /// <summary>
        /// Create a Narrative for Birth Details
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(EncapsulatedData pcml)
        {
            StrucDocText strucDocText = null;

            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            if (pcml != null)
            {
                var headerList = new List<String>()
                {
                    "Attachment Name",
                    "Attachment"
                };

                strucDocText = new StrucDocText();

                //pcml.ExternalData.Caption,
                CreateEncapsulatedData(pcml.ExternalData, ref renderMultiMediaList);

                var ai = new StrucDocRenderMultiMedia();
                ai.caption = new StrucDocCaption()
                {
                    Text = new string[] {pcml.ExternalData.Caption}
                };
                ai.referencedObject = pcml.ExternalData.ID;

                strucDocText.renderMultiMedia = new StrucDocRenderMultiMedia[] {ai};
            }


            return strucDocText;
        }


        /// <summary>
        /// Create a Narrative for List of QuestionnaireDocumentData
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(QuestionnaireDocumentData questionnaireDocumentData)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[] {"Assessment", "Status", "Creation Date", "Author"};

            if (questionnaireDocumentData != null)
            {
                var narrativeLink = new List<List<object>>();

                if (questionnaireDocumentData.DocumentLink != null || questionnaireDocumentData.Assessment.HasValue ||
                    questionnaireDocumentData.DocumentDate != null ||
                    !questionnaireDocumentData.AuthorName.IsNullOrEmptyWhitespace())
                {
                    narrativeLink.Add(
                        new List<object>
                        {
                            questionnaireDocumentData.DocumentLink != null
                                ? CreateLink(questionnaireDocumentData.DocumentLink)
                                : null,
                            questionnaireDocumentData.Assessment.HasValue
                                ? questionnaireDocumentData.Assessment.Value ? "Completed" : "Incomplete"
                                : null,
                            questionnaireDocumentData.DocumentDate != null
                                ? questionnaireDocumentData.DocumentDate.NarrativeText()
                                : null,
                            !questionnaireDocumentData.AuthorName.IsNullOrEmptyWhitespace()
                                ? questionnaireDocumentData.AuthorName
                                : null
                        });

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            null,
                            null,
                            header,
                            null,
                            narrativeLink
                        )
                    );
                }
                else
                {
                    strucDocText.paragraph = CreateParagraph(SECTIONEMPTYTEXT);
                }

                strucDocText.content = new[]
                {
                    new StrucDocContent
                    {
                        styleCode = "Bold Underline",
                        Text = new[]
                        {
                            questionnaireDocumentData.QuestionnairesData.GetAttributeValue<NameAttribute, String>(x =>
                                x.Title)
                        }
                    }
                };

                strucDocText.table = strucDocTableList.ToArray();

            }

            return strucDocText;

        }

        /// <summary>
        /// Create a Narrative for List of MeasurementEntry
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(List<MeasurementEntry> measurementEntrys)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeLink = new List<List<object>>();
            var header = new List<string>
                {"Observation Date", "Document", "Body height", "Body weight", "Head circumference", "Body mass index"};

            if (measurementEntrys != null && measurementEntrys.Any())
            {
                foreach (var measurementEntry in measurementEntrys)
                {
                    var narrativeList = new List<object>();

                    narrativeList.Add(measurementEntry.ObservationDate != null
                        ? measurementEntry.ObservationDate.NarrativeText()
                        : null);
                    narrativeList.Add(measurementEntry.DocumentLink != null
                        ? CreateLink(measurementEntry.DocumentLink)
                        : null);

                    // Body Height Measure Narrative
                    string bodyHeightMeasureNarrative = string.Empty;
                    if (measurementEntry.BodyHeightMeasure != null &&
                        measurementEntry.BodyHeightMeasure.ComponentValue != null)
                    {
                        bodyHeightMeasureNarrative = measurementEntry.BodyHeightMeasure.ComponentValue.NarrativeText;

                        if (measurementEntry.BodyHeightMeasure.PercentileValue != null)
                        {
                            bodyHeightMeasureNarrative += string.Format(" (Percentile: {0})",
                                measurementEntry.BodyHeightMeasure.PercentileValue.NarrativeText);
                        }

                    }

                    narrativeList.Add(bodyHeightMeasureNarrative);

                    // Body Weight Measure Narrative
                    string bodyWeightMeasureNarrative = string.Empty;
                    if (measurementEntry.BodyWeightMeasure != null &&
                        measurementEntry.BodyWeightMeasure.ComponentValue != null)
                    {
                        bodyWeightMeasureNarrative = measurementEntry.BodyWeightMeasure.ComponentValue.NarrativeText;

                        if (measurementEntry.BodyWeightMeasure.PercentileValue != null)
                        {
                            bodyWeightMeasureNarrative += string.Format(" (Percentile: {0})",
                                measurementEntry.BodyWeightMeasure.PercentileValue.NarrativeText);
                        }
                    }

                    narrativeList.Add(bodyWeightMeasureNarrative);

                    // Head Circumference Measure Narrative
                    string headCircumferenceMeasureNarrative = string.Empty;
                    if (measurementEntry.HeadCircumferenceMeasure != null &&
                        measurementEntry.HeadCircumferenceMeasure.ComponentValue != null)
                    {
                        headCircumferenceMeasureNarrative =
                            measurementEntry.HeadCircumferenceMeasure.ComponentValue.NarrativeText;

                        if (measurementEntry.HeadCircumferenceMeasure.PercentileValue != null)
                        {
                            headCircumferenceMeasureNarrative += string.Format(" (Percentile: {0})",
                                measurementEntry.HeadCircumferenceMeasure.PercentileValue.NarrativeText);
                        }
                    }

                    narrativeList.Add(headCircumferenceMeasureNarrative);

                    // Body Mass Index Narrative
                    string bodyMassIndexNarrative = string.Empty;
                    if (measurementEntry.BodyMassIndex != null && measurementEntry.BodyMassIndex.ComponentValue != null)
                    {
                        bodyMassIndexNarrative = measurementEntry.BodyMassIndex.ComponentValue.NarrativeText;

                        if (measurementEntry.BodyMassIndex.PercentileValue != null)
                        {
                            bodyMassIndexNarrative += string.Format(" (Percentile: {0})",
                                measurementEntry.BodyMassIndex.PercentileValue.NarrativeText);
                        }
                    }

                    narrativeList.Add(bodyMassIndexNarrative);

                    narrativeLink.Add(narrativeList);
                }

                StripEmptyColoums(ref header, ref narrativeLink, new List<int> {2, 3, 4, 5});

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        null,
                        null,
                        header.ToArray(),
                        null,
                        narrativeLink
                    )
                );
            }
            else
            {
                strucDocText.paragraph = CreateParagraph(SECTIONEXCLUSIONSTATEMENT);
            }

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create a narrative for DiagnosticImagingExaminationResult
        /// </summary>
        public StrucDocText CreateNarrative(IDiagnosticImagingExaminationResult imagingExaminationResult)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[]
            {
                "Image Date",
                "Observation Date",
                "Result",
                "Modality",
                "Procedure",
                "Anatomical Region",
                "Status"
            };

            var narrative = new List<List<Object>>();
            var anatomicalSiteNarrative = new List<List<String>>();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            if (imagingExaminationResult != null)
            {

                //  Reporting Radiologist
                if (imagingExaminationResult.ReportingRadiologistForImagingExaminationResult != null)
                    strucDocText.content = new[]
                    {
                        new StrucDocContent
                        {
                            Text = new[]
                            {
                                "Reporting Radiologist: ",
                                imagingExaminationResult.ReportingRadiologistForImagingExaminationResult,
                            }
                        }
                    };

                narrative.Add
                (
                    new List<Object>
                    {
                        imagingExaminationResult.ExaminationDetails != null &&
                        imagingExaminationResult.ExaminationDetails.ImageDateTime != null
                            ? XCOLWIDTHDATE + imagingExaminationResult.ExaminationDetails.ImageDateTime.NarrativeText()
                            : null,
                        imagingExaminationResult.ObservationDateTime != null
                            ? XCOLWIDTHDATE + imagingExaminationResult.ObservationDateTime.NarrativeText()
                            : null,
                        (imagingExaminationResult.RelatedImage != null
                            ? CreateExternalLink(imagingExaminationResult.RelatedImage,
                                imagingExaminationResult.ExaminationResultName.NarrativeText)
                            : null) ?? (object) imagingExaminationResult.ExaminationResultName.NarrativeText,
                        imagingExaminationResult.Modality != null
                            ? imagingExaminationResult.Modality.NarrativeText
                            : null,
                        !imagingExaminationResult.ExaminationProcedure.IsNullOrEmptyWhitespace()
                            ? imagingExaminationResult.ExaminationProcedure
                            : null,
                        imagingExaminationResult.AnatomicalRegion.HasValue
                            ? imagingExaminationResult.AnatomicalRegion.Value.GetAttributeValue<NameAttribute, String>(
                                x => x.Name)
                            : null,
                        imagingExaminationResult.OverallResultStatus != null
                            ? imagingExaminationResult.OverallResultStatus.NarrativeText
                            : null,
                    }
                );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Imaging Examination Result",
                        null,
                        header,
                        null,
                        narrative
                    )
                );

                // Anatomical Site (Anatomical location)
                var narativeHeader = new List<string>
                {
                    "Name of Location/Description",
                    "Side"
                };

                if (imagingExaminationResult.AnatomicalSite != null && imagingExaminationResult.AnatomicalSite.Any())
                {
                    foreach (var anatomicalSite in imagingExaminationResult.AnatomicalSite)
                    {
                        ICodableText nameOfLocationAndDescription = new CodableText();

                        if (anatomicalSite.SpecificLocation != null &&
                            anatomicalSite.SpecificLocation.NameOfLocation != null)
                            nameOfLocationAndDescription = anatomicalSite.SpecificLocation.NameOfLocation;

                        if (!anatomicalSite.Description.IsNullOrEmptyWhitespace())
                            nameOfLocationAndDescription.OriginalText = anatomicalSite.Description;

                        anatomicalSiteNarrative.Add(new List<String>
                        {
                            nameOfLocationAndDescription != null ? nameOfLocationAndDescription.NarrativeText : null,
                            anatomicalSite.SpecificLocation != null && anatomicalSite.SpecificLocation.Side != null
                                ? anatomicalSite.SpecificLocation.Side.NarrativeText
                                : null
                        });
                    }
                }

                if (anatomicalSiteNarrative.Any())
                {
                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            "Anatomical Site",
                            null,
                            narativeHeader.ToArray(),
                            null,
                            anatomicalSiteNarrative
                        )
                    );

                }

                if (renderMultiMediaList.Any())
                {
                    strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
                }
            }

            if (strucDocTableList.Any())
                strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create a narrative for AuthorityToPost
        /// </summary>
        public StrucDocText CreateNarrative(AuthorityToPost authorityToPost)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();
            var header = new[] {"Field", "Value"};

            if (authorityToPost != null)
            {
                // Report Identifier
                if (authorityToPost.ReportIdentifier != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Pathology Report Identifier (Document Instance Identifier)",
                            authorityToPost.ReportIdentifier.NarrativeText
                        }
                    );

                // Service Request Identifier
                if (authorityToPost.ServiceRequestIdentifier != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Service Request Identifier",
                            authorityToPost.ServiceRequestIdentifier.NarrativeText
                        }
                    );

                // Struc Doc Table List
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        string.Format("Authority To Post"),
                        null,
                        header,
                        null,
                        narrative
                    )
                );

                narrative = new List<List<Object>>();
                if (authorityToPost.Authoriser != null && authorityToPost.Authoriser.Participant != null)
                {
                    var personOrganisation =
                        authorityToPost.Authoriser.Participant.Person != null &&
                        authorityToPost.Authoriser.Participant.Person.PersonNames != null
                            ? BuildPersonNames(authorityToPost.Authoriser.Participant.Person.PersonNames)
                            : null;

                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Authoriser",
                            personOrganisation
                        }
                    );

                    if (authorityToPost.Authoriser.Participant.Addresses != null ||
                        authorityToPost.Authoriser.Participant.ElectronicCommunicationDetails != null)
                    {
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Authoriser - Details",
                                CreateAddress(authorityToPost.Authoriser.Participant.Addresses,
                                    authorityToPost.Authoriser.Participant.ElectronicCommunicationDetails)
                            }
                        );
                    }

                    if (authorityToPost.Authoriser.Participant.Person != null &&
                        authorityToPost.Authoriser.Participant.Person.Organisation != null && !authorityToPost
                            .Authoriser.Participant.Person.Organisation.Name.IsNullOrEmptyWhitespace())
                    {
                        narrative.Add(
                            new List<Object>
                            {
                                "Authoriser - Organisation Name",
                                authorityToPost.Authoriser.Participant.Person.Organisation.Name
                            }
                        );
                    }

                    if (authorityToPost.Authoriser.Participant.Entitlements != null ||
                        authorityToPost.Authoriser.Participant.Entitlements != null)
                    {
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Authoriser - Entitlements",
                                CreateEntitlement(authorityToPost.Authoriser.Participant.Entitlements)
                            }
                        );
                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            string.Format("Authoriser"),
                            null,
                            header,
                            null,
                            narrative
                        )
                    );
                }

                narrative = new List<List<Object>>();
                if (authorityToPost.Authorisee != null && authorityToPost.Authorisee.Participant != null)
                {
                    var organisation =
                        authorityToPost.Authorisee.Participant.Organisation != null && !authorityToPost.Authorisee
                            .Participant.Organisation.Name.IsNullOrEmptyWhitespace()
                            ? authorityToPost.Authorisee.Participant.Organisation.Name
                            : null;

                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Authorisee",
                            organisation
                        }
                    );

                    if (authorityToPost.Authorisee.Participant.Addresses != null ||
                        authorityToPost.Authorisee.Participant.ElectronicCommunicationDetails != null)
                    {
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Authorisee - Details",
                                CreateAddress(authorityToPost.Authorisee.Participant.Addresses,
                                    authorityToPost.Authorisee.Participant.ElectronicCommunicationDetails)
                            }
                        );
                    }

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            string.Format("Authorisee"),
                            null,
                            header,
                            null,
                            narrative
                        )
                    );
                }

                narrative = new List<List<Object>>();
                if (authorityToPost.Repository != null && authorityToPost.Repository != null)
                {
                    var softwareName = !authorityToPost.Repository.SoftwareName.IsNullOrEmptyWhitespace()
                        ? authorityToPost.Repository.SoftwareName
                        : null;

                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Software Name",
                            softwareName
                        }
                    );

                    strucDocTableList.Add
                    (
                        PopulateTable
                        (
                            string.Format("Repository"),
                            null,
                            header,
                            null,
                            narrative
                        )
                    );
                }
            }

            if (strucDocTableList.Any())
                strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create a narrative for information
        /// </summary>
        public StrucDocText CreateNarrative(Information information)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            var header = new[] {"Field", "Value"};

            if (information != null)
            {
                // Report Identifier
                if (information.ReportIdentifier != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Identifier",
                            information.ReportIdentifier.NarrativeText
                        }
                    );

                // Link Nature
                if (information.LinkNature != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Link Nature",
                            information.LinkNature.NarrativeText
                        }
                    );

                // Report Status
                if (information.ReportStatus.HasValue)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Report Status",
                            information.ReportStatus.Value.GetAttributeValue<NameAttribute, String>(x => x.Name)
                        }
                    );

                if (information.ExternalData != null)
                {
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Result Status - Image",
                            CreateEncapsulatedData(information.ExternalData, ref renderMultiMediaList)
                        }
                    );
                }

                // Struc Doc Table List
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Related Information",
                        null,
                        header,
                        null,
                        narrative
                    )
                );
            }


            if (renderMultiMediaList.Any())
            {
                strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
            }

            if (strucDocTableList.Any())
                strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Requested Services narrative
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(SCSModel.Pathology.RequestedService requestedService)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var narrative = new List<List<Object>>();
            var header = new[] {"Field", "Value"};

            if (requestedService != null)
            {
                // Requested Service DateTime
                if (requestedService.RequestedServiceDateTime != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Requested Service DateTime",
                            requestedService.RequestedServiceDateTime.NarrativeText()
                        }
                    );

                // Requested Service Description
                if (requestedService.RequestedServiceDescription != null)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Requested Service Description",
                            requestedService.RequestedServiceDescription.NarrativeText
                        }
                    );

                // Service Booking Status
                if (requestedService.ServiceBookingStatus.HasValue)
                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Service Booking Status",
                            requestedService.ServiceBookingStatus.Value.GetAttributeValue<NameAttribute, String>(x =>
                                x.Name)
                        }
                    );

                // Service Requester
                if (requestedService.ServiceRequester != null && requestedService.ServiceRequester.Participant != null)
                {
                    var personOrganisation =
                        requestedService.ServiceRequester.Participant.Person != null &&
                        requestedService.ServiceRequester.Participant.Person.PersonNames != null
                            ? BuildPersonNames(requestedService.ServiceRequester.Participant.Person.PersonNames)
                            : null;

                    narrative.Add
                    (
                        new List<Object>
                        {
                            "Reporting Pathologist",
                            personOrganisation
                        }
                    );

                    // Service Requester
                    if (requestedService.ServiceRequester.Participant.Addresses != null ||
                        requestedService.ServiceRequester.Participant.ElectronicCommunicationDetails != null)
                    {
                        narrative.Add
                        (
                            new List<Object>
                            {
                                "Reporting Pathologist - Details",
                                CreateAddress(requestedService.ServiceRequester.Participant.Addresses,
                                    requestedService.ServiceRequester.Participant.ElectronicCommunicationDetails)
                            }
                        );
                    }
                }

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Requested Service",
                        null,
                        header,
                        null,
                        narrative
                    )
                );
            }

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Related Document narrative
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IDocumentDetails documentDetails, DocumentType docType)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeImage = new List<List<Object>>();
            var narrativedDocumentProvenance = new List<List<Object>>();
            var renderMultiMediaList = new List<StrucDocRenderMultiMedia>();

            // Add Information For Reader
            if (docType == DocumentType.AdvanceCareInformationGoalsOfCare)
            {
                var textToUse = "Healthcare providers may have professional state and territory-specific legal obligations when reading Goals of Care documents stored on an individual's My Health Record.";
                StrucDocContent sdtText = new StrucDocContent()
                {
                    Text = new string[] { textToUse },
                    styleCode = "Italics"
                };

                // Create Table
                StrucDocTable InfoTable = PopulateTable
                ( "Information for reader",
                    null, null, null,
                    new List<List<Object>> { new List<Object> { null } }
                );

                // Now Add the text into a content tag
                InfoTable.tbody[0].tr[0].td[0].content = new StrucDocContent[] { sdtText };

                // Struc Doc Table List
                strucDocTableList.Add(InfoTable);
            }

            var header = new[] {"Document details", "Value"};

            if (documentDetails != null)
            {
                if (documentDetails.ExternalData != null)
                {
                    narrativeImage.Add
                    (
                        new List<Object>
                        {
                            "Document Target",
                            CreateSimpleHtmlLink(documentDetails.ExternalData)
                        }
                    );
                }

                // Struc Doc Table List
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Related Document Information",
                        null,
                        header,
                        null,
                        narrativeImage
                    )
                );
            }

            if (documentDetails != null && documentDetails.DocumentProvenance != null)
            {
                // Link Nature
                if (documentDetails.DocumentProvenance.DocumentType.HasValue)
                    narrativedDocumentProvenance.Add
                    (
                        new List<Object>
                        {
                            "Document Type",
                            documentDetails.DocumentProvenance.DocumentType.Value
                                .GetAttributeValue<NameAttribute, String>(x => x.Name)
                        }
                    );

                if (documentDetails.DocumentProvenance != null &&
                    documentDetails.DocumentProvenance.DocumentIdentifier != null)
                    narrativeImage.Add
                    (
                        new List<Object>
                        {
                            "Document Identifier",
                            documentDetails.DocumentProvenance.DocumentIdentifier.NarrativeText
                        }
                    );

                if (documentDetails.DocumentProvenance.Author != null)
                {
                    var author = documentDetails.DocumentProvenance.Author;

                    string organisationName = null, personName = null, addresses = null;

                    if (author.Participant != null)
                    {
                        if (author.Participant.Addresses != null && author.Participant.Addresses.Any() ||
                            author.Participant.ElectronicCommunicationDetails != null)

                            addresses = CreateAddress(author.Participant.Addresses,
                                author.Participant.ElectronicCommunicationDetails);

                        if (author.Participant.Person != null)
                        {
                            personName = BuildPersonNames(author.Participant.Person.PersonNames);
                        }
                    }

                    // Author Organisation
                    if (!organisationName.IsNullOrEmptyWhitespace())
                        narrativedDocumentProvenance.Add
                        (
                            new List<Object>
                            {
                                "Author Organisation",
                                organisationName
                            }
                        );

                    // Provider Person Name
                    if (!personName.IsNullOrEmptyWhitespace())
                        narrativedDocumentProvenance.Add
                        (
                            new List<Object>
                            {
                                "Author Person Name",
                                personName
                            }
                        );

                    // Addresses
                    if (!addresses.IsNullOrEmptyWhitespace())
                        narrativedDocumentProvenance.Add
                        (
                            new List<Object>
                            {
                                "Address / Contact",
                                addresses
                            }
                        );
                }

                // Struc Doc Table List
                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Document Provenance",
                        null,
                        header,
                        null,
                        narrativedDocumentProvenance
                    )
                );
            }

            if (renderMultiMediaList.Any())
            {
                strucDocText.renderMultiMedia = renderMultiMediaList.ToArray();
            }

            if (strucDocTableList.Any())
                strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Pathology Test Result
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(SCSModel.Pathology.PathologyTestResult pathologyTestResult)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[]
            {
                "Collection Date",
                "Observation Date",
                "Test Result Name",
                "Diagnostic Service",
                "Status",
            };

            if (pathologyTestResult.ReportingPathologistForTestResult != null)
                strucDocText.content = new[]
                {
                    new StrucDocContent
                    {
                        Text =
                            new[]
                            {
                                "Reporting Pathologist: ",
                                pathologyTestResult.ReportingPathologistForTestResult,
                            }
                    }
                };

            var narrative = new List<List<Object>>();

            if (pathologyTestResult != null)
            {
                narrative.Add
                (
                    new List<Object>
                    {
                        pathologyTestResult.TestSpecimenDetail != null &&
                        pathologyTestResult.TestSpecimenDetail.CollectionDateTime != null
                            ? XCOLWIDTHDATE + pathologyTestResult.TestSpecimenDetail.CollectionDateTime.NarrativeText()
                            : null,
                        pathologyTestResult.ObservationDateTime != null
                            ? XCOLWIDTHDATE + pathologyTestResult.ObservationDateTime.NarrativeText()
                            : null,
                        pathologyTestResult.TestResultName != null
                            ? pathologyTestResult.TestResultName.NarrativeText
                            : null,
                        pathologyTestResult.PathologyDiscipline.HasValue
                            ? pathologyTestResult.PathologyDiscipline.Value.GetAttributeValue<NameAttribute, String>(
                                x => x.Name)
                            : null,
                        pathologyTestResult.OverallTestResultStatus != null
                            ? pathologyTestResult.OverallTestResultStatus.NarrativeText
                            : null
                    }
                );

                strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Pathology Test Result",
                        null,
                        header,
                        null,
                        narrative
                    )
                );
            }

            if (strucDocTableList.Any())
                strucDocText.table = strucDocTableList.ToArray();

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Related Document / Reporting Pathologist
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IParticipationReportingPathologist reportingPathologist,
            RelatedDocument relatedDocument)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[] {"Report DateTime", "Report Name", "Reporting Pathologist", "Report Status"};
            var narrativeRelatedDocument = new List<List<Object>>();

            string reportingPathologistDisplay = string.Empty;

            if (reportingPathologist != null && reportingPathologist.Participant != null)
            {
                var reportingPathologistOrganisationName = string.Empty;
                var reportingPathologistPersonName =
                    reportingPathologist.Participant.Person != null &&
                    reportingPathologist.Participant.Person.PersonNames != null
                        ? BuildPersonNames(reportingPathologist.Participant.Person.PersonNames)
                        : null;

                if (reportingPathologist.Participant.Person != null &&
                    reportingPathologist.Participant.Person.Organisation != null && !reportingPathologist.Participant
                        .Person.Organisation.Name.IsNullOrEmptyWhitespace())
                {
                    reportingPathologistOrganisationName = string.Format("({0})",
                        reportingPathologist.Participant.Person.Organisation.Name);
                }

                reportingPathologistDisplay = string.Format("{0}{1}{2}", reportingPathologistPersonName, DELIMITERBREAK,
                    reportingPathologistOrganisationName);
            }

            if (relatedDocument.ExaminationResultRepresentation != null && relatedDocument.DocumentDetails != null &&
                relatedDocument.DocumentDetails.ReportDescription != null)
            {
                relatedDocument.ExaminationResultRepresentation.Caption =
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportDescription != null
                        ? relatedDocument.DocumentDetails.ReportDescription
                        : null;
            }

            narrativeRelatedDocument.Add
            (
                new List<Object>
                {
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportDate != null
                        ? XCOLWIDTHDATE + relatedDocument.DocumentDetails.ReportDate.NarrativeText()
                        : null,
                    relatedDocument.ExaminationResultRepresentation != null
                        ? CreateSimpleHtmlLink(relatedDocument.ExaminationResultRepresentation)
                        : null,
                    reportingPathologistDisplay,
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportStatus != null
                        ? relatedDocument.DocumentDetails.ReportStatus.NarrativeText
                        : null
                }
            );

            strucDocTableList.Add
            (
                PopulateTable
                (
                    null,
                    null,
                    header,
                    null,
                    narrativeRelatedDocument
                )
            );

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Related Document / Reporting Pathologist
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IList<IParticipationReportingPathologist> reportingPathologists,
            RelatedDocument relatedDocument)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[] { "Report DateTime", "Report Name", "Reporting Pathologist/s", "Report Status" };
            var narrativeRelatedDocument = new List<List<Object>>();

            string reportingPathologistDisplay = string.Empty;

            for (int i = 0; i < reportingPathologists.Count; i++)
            {
                if (reportingPathologists[i] != null && reportingPathologists[i].Participant != null)
                {
                    var reportingPathologistOrganisationName = string.Empty;
                    var reportingPathologistPersonName =
                        reportingPathologists[i].Participant.Person != null &&
                        reportingPathologists[i].Participant.Person.PersonNames != null
                            ? BuildPersonNames(reportingPathologists[i].Participant.Person.PersonNames)
                            : null;

                    if (reportingPathologists[i].Participant.Person != null &&
                        reportingPathologists[i].Participant.Person.Organisation != null && !reportingPathologists[i].Participant
                            .Person.Organisation.Name.IsNullOrEmptyWhitespace())
                    {
                        reportingPathologistOrganisationName = string.Format("({0})",
                            reportingPathologists[i].Participant.Person.Organisation.Name);
                    }

                    reportingPathologistDisplay += i > 0 ? DELIMITERBREAK : string.Empty;
                    reportingPathologistDisplay += string.Format("{0}{1}{2}", reportingPathologistPersonName, DELIMITERBREAK,
                        reportingPathologistOrganisationName);
                }
            }

            if (relatedDocument.ExaminationResultRepresentation != null && relatedDocument.DocumentDetails != null &&
                relatedDocument.DocumentDetails.ReportDescription != null)
            {
                relatedDocument.ExaminationResultRepresentation.Caption =
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportDescription != null
                        ? relatedDocument.DocumentDetails.ReportDescription
                        : null;
            }

            narrativeRelatedDocument.Add
            (
                new List<Object>
                {
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportDate != null
                        ? XCOLWIDTHDATE + relatedDocument.DocumentDetails.ReportDate.NarrativeText()
                        : null,
                    relatedDocument.ExaminationResultRepresentation != null
                        ? CreateSimpleHtmlLink(relatedDocument.ExaminationResultRepresentation)
                        : null,
                    reportingPathologistDisplay,
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportStatus != null
                        ? relatedDocument.DocumentDetails.ReportStatus.NarrativeText
                        : null
                }
            );

            strucDocTableList.Add
            (
                PopulateTable
                (
                    null,
                    null,
                    header,
                    null,
                    narrativeRelatedDocument
                )
            );

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Participation DI
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(IParticipationReportingRadiologist reportingRadiologist,
            RelatedDocument relatedDocument)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();
            var header = new[] {"Report Date", "Report", "Reporting Radiologist", "Status"};
            var relatedDocumentNarrative = new List<List<Object>>();

            string reportingRadiologistDisplay = string.Empty;

            if (reportingRadiologist != null && reportingRadiologist.Participant != null)
            {
                var reportingRadiologistOrganisationName = string.Empty;
                var reportingRadiologistPersonName =
                    reportingRadiologist.Participant.Person != null &&
                    reportingRadiologist.Participant.Person.PersonNames != null
                        ? BuildPersonNames(reportingRadiologist.Participant.Person.PersonNames)
                        : null;

                if (reportingRadiologist.Participant.Person != null &&
                    reportingRadiologist.Participant.Person.Organisation != null && !reportingRadiologist.Participant
                        .Person.Organisation.Name.IsNullOrEmptyWhitespace())
                {
                    reportingRadiologistOrganisationName = string.Format("({0})",
                        reportingRadiologist.Participant.Person.Organisation.Name);
                }

                reportingRadiologistDisplay = string.Format("{0}{1}{2}", reportingRadiologistPersonName, DELIMITERBREAK,
                    reportingRadiologistOrganisationName);
            }

            if (relatedDocument.ExaminationResultRepresentation != null && relatedDocument.DocumentDetails != null &&
                relatedDocument.DocumentDetails.ReportDescription != null)
            {
                relatedDocument.ExaminationResultRepresentation.Caption =
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportDescription != null
                        ? relatedDocument.DocumentDetails.ReportDescription
                        : null;
            }

            relatedDocumentNarrative.Add
            (
                new List<Object>
                {
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportDate != null
                        ? XCOLWIDTHDATE + relatedDocument.DocumentDetails.ReportDate.NarrativeText()
                        : null,
                    relatedDocument.ExaminationResultRepresentation != null
                        ? CreateSimpleHtmlLink(relatedDocument.ExaminationResultRepresentation)
                        : null,
                    reportingRadiologistDisplay,
                    relatedDocument.DocumentDetails != null && relatedDocument.DocumentDetails.ReportStatus != null
                        ? relatedDocument.DocumentDetails.ReportStatus.NarrativeText
                        : null
                }
            );

            strucDocTableList.Add
            (
                PopulateTable
                (
                    null,
                    null,
                    header,
                    null,
                    relatedDocumentNarrative
                )
            );


            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        #region Service Referral

        /// <summary>
        /// Create Service Referral Detail narrative
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(ServiceReferralDetail documentDetails)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();

            if (documentDetails?.RequestedService != null)
            {
                strucDocTableList.AddRange(documentDetails.RequestedService.Select(requestedService =>
                    CreateServiceNarative(requestedService as Service)));
            }

            if (documentDetails?.OtherAlerts != null)
            {
                strucDocTableList.AddRange(documentDetails.OtherAlerts.Select(CreateAlertNarative));
            }

            if (documentDetails?.RelatedDocument != null)
            {
                strucDocTableList.AddRange(documentDetails.RelatedDocument.Select(CreateRelatedDocumentNarative));
            }

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// Create Diagnostic Investigations V1 narrative
        /// </summary>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrative(List<IPendingDiagnosticInvestigation> service, string narrativeText)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();

            if (narrativeText != null)
            {
                strucDocText.paragraph = new[]
                {
                    new StrucDocParagraph
                    {
                        Text = new[]
                        {
                            narrativeText
                        }
                    }
                };
            }

            if (service != null)
            {
                strucDocTableList.AddRange(service.Select(s => CreateServiceNarative(s as Service)));
            }

            strucDocText.table = strucDocTableList.ToArray();

            return strucDocText;
        }

        /// <summary>
        /// Create Service Current Service narrative
        /// </summary>
        /// <returns>The StrucDocText</returns>
        public StrucDocText CreateNarrative(IList<ICurrentService> currentService)
        {
            var strucDocText = new StrucDocText();
            var strucDocTableList = new List<StrucDocTable>();

            if (currentService != null)
            {
                strucDocTableList.AddRange(currentService.Select(service => CreateServiceNarative(service as Service)));
            }

            if (strucDocTableList.Any())
            {
                strucDocText.table = strucDocTableList.ToArray();
            }

            return strucDocText;
        }

        /// <summary>
        /// The Related Document Narative
        /// </summary>
        /// <param name="relatedDocument">The Related Document</param>
        /// <returns>The StrucDocTable</returns>
        public StrucDocTable CreateRelatedDocumentNarative(RelatedDocumentV1 relatedDocument)
        {
            var header = new[] {"Document details", "Value"};
            var narrative = new List<List<object>>();

            if (relatedDocument.DocumentDetails?.DocumentTitle != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Document Title",
                        relatedDocument.DocumentDetails.DocumentTitle
                    }
                );
            }

            if (relatedDocument.DocumentTarget != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Document details",
                        CreateSimpleHtmlLink(relatedDocument.DocumentTarget)
                    }
                );
            }

            // Struc Doc Table List
            return PopulateTable
            (
                "Related Document Information",
                null,
                header,
                null,
                narrative
            );
        }

        /// <summary>
        /// The Alert Narative
        /// </summary>
        /// <param name="alert"></param>
        /// <returns>The StrucDocTable</returns>
        public StrucDocTable CreateAlertNarative(Alert alert)
        {
            var header = new[] {"Field", "Value"};
            var narrative = new List<List<object>>();

            if (alert.AlertType != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Alert Type",
                        alert.AlertType.NarrativeText
                    }
                );
            }

            if (alert.AlertDescription != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Alert Description",
                        alert.AlertDescription.NarrativeText
                    }
                );
            }

            // Struc Doc Table List
            return PopulateTable
            (
                "Other Alert",
                null,
                header,
                null,
                narrative
            );
        }

        /// <summary>
        /// The Service Narative
        /// </summary>
        /// <param name="alert"></param>
        /// <returns>The StrucDocTable</returns>
        public StrucDocTable CreateInterpreterRequiredAlertNarative(InterpreterRequiredAlert alert)
        {
            var header = new[] {"Field", "Value"};
            var narrative = new List<List<object>>();

            if (alert.PreferredLanguage != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Preferred Language",
                        string.Join(DELIMITERBREAK, alert.PreferredLanguage)
                    }
                );
            }

            // Struc Doc Table List
            return
                PopulateTable
                (
                    "Interpreter Required Alert",
                    null,
                    header,
                    null,
                    narrative
                );
        }

        /// <summary>
        /// The Service Narative
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public StrucDocTable CreateServiceNarative(Service service)
        {
            var header = new[] {"Field", "Value"};
            var narrative = new List<List<object>>();

            if (service.ReasonForService != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Reason for Service",
                        service.ReasonForService.NarrativeText
                    }
                );
            }

            if (service.ReasonForServiceDescription != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Reason for Service Description",
                        service.ReasonForServiceDescription
                    }
                );
            }

            if (service.ServiceCategory != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Service Category",
                        service.ServiceCategory.NarrativeText
                    }
                );
            }

            if (service.ServiceDescription != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Service Description",
                        service.ServiceDescription.NarrativeText
                    }
                );
            }

            if (service.ServiceComment != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Service Comment",
                        service.ServiceComment
                    }
                );
            }

            if (service.DateTimeServiceScheduled != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Date Time Service Scheduled",
                        service.DateTimeServiceScheduled.NarrativeText()
                    }
                );
            }

            if (service.RequestUrgency.HasValue)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Request Urgency",
                        service.RequestUrgency.Value.ToString()
                    }
                );
            }

            if (service.RequestUrgencyNotes != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Request Urgency Notes",
                        service.RequestUrgencyNotes
                    }
                );
            }


            if (service.ServiceCommencementWindow != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Service Commencement Window",
                        service.ServiceCommencementWindow.NarrativeText()
                    }
                );
            }

            // Service Booking Status
            narrative.Add
            (
                new List<object>
                {
                    "Service Booking Status",
                    service.ServiceBookingStatus.GetAttributeValue<NameAttribute, string>(x => x.Name)
                }
            );

            if (service.RequestValidityPeriod != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Request Validity Period",
                        service.RequestValidityPeriod.NarrativeText()
                    }
                );
            }

            if (service.SubjectOfCareInstructionDescription != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Subject Of Care Instruction Description",
                        string.Join(DELIMITERBREAK, service.SubjectOfCareInstructionDescription)
                    }
                );
            }

            if (service.RequestedServiceDateTime != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Requested Service DateTime",
                        service.RequestedServiceDateTime.NarrativeText()
                    }
                );
            }

            string serviceProviderText = null;
            if (service.ServiceProvider != null)
            {
                var serviceProviderOrganisationName = string.Empty;
                var address = string.Empty;

                var serviceProviderPersonName = service.ServiceProvider.Participant?.Person?.PersonNames != null
                    ? BuildPersonNames(service.ServiceProvider.Participant.Person.PersonNames)
                    : null;

                if (service.ServiceProvider?.Participant?.Person?.Organisation != null && !service.ServiceProvider
                        .Participant.Person.Organisation.Name.IsNullOrEmptyWhitespace())
                {
                    serviceProviderOrganisationName =
                        $"({service.ServiceProvider.Participant.Person.Organisation.Name})";
                }

                if (service.ServiceProvider?.Participant?.Organisation != null &&
                    !service.ServiceProvider.Participant.Organisation.Name.IsNullOrEmptyWhitespace())
                {
                    serviceProviderOrganisationName = $"{service.ServiceProvider.Participant.Organisation.Name}";
                }

                serviceProviderText = $"{serviceProviderPersonName} {serviceProviderOrganisationName}";

                if (service?.ServiceProvider?.Participant != null)
                {
                    address = CreateAddress(service.ServiceProvider.Participant.Addresses,
                        service.ServiceProvider.Participant.ElectronicCommunicationDetails);
                    serviceProviderText = $"{serviceProviderText}{DELIMITERBREAK}{address}";
                }
            }

            if (serviceProviderText != null)
            {
                narrative.Add
                (
                    new List<object>
                    {
                        "Service Provider",
                        serviceProviderText
                    }
                );
            }

            // Struc Doc Table List
            return
                PopulateTable
                (
                    "Requested Service",
                    null,
                    header,
                    null,
                    narrative
                );
        }



        #endregion
    }
}