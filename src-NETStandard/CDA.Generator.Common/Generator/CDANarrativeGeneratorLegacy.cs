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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

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
        /// <summary>
        /// This method creates the narrative for the medications section
        /// </summary>
        /// <param name="medications">IMedicationsSpecialistLetter</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrativeLegacy(IMedicationsSpecialistLetter medications)
        {
            List<List<String>> narrative;
            var strucDocTableList = new List<StrucDocTable>();
            var narrativeParagraph = new List<StrucDocParagraph>();

            if (medications != null)
            {
                if (medications.MedicationsList != null)
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

                    foreach (var medication in medications.MedicationsList)
                    {
                        string changeStatus;
                        if (medication.ChangeType != null && medication.ChangeType.NullFlavour != null) // because if there is no change status, the fact of whether this is a recommendation or change is irrelevant
                            changeStatus = "No change";
                        else
                        {
                            changeStatus = medication.ChangeType != null ? medication.ChangeType.NarrativeText : string.Empty;
                            // if there's no change, or recommendation or change value, we don't say anything about it
                            if (medication.ChangeType != null && (medication.ChangeType.Code != ChangeTypeNctis.Unchanged.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                            {
                                if (!(medication.ChangeStatus != null && medication.ChangeStatus.Code == RecomendationOrChange.TheChangeHasBeenMade.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                                    changeStatus = "Recommendation: " + changeStatus;
                            }
                        }

                        var medicationList = new List<String>
                                              {
                                                  medication.Medicine != null ? medication.Medicine.NarrativeText : null,
                                                  medication.Directions != null ? medication.Directions.NarrativeText : null,
                                                  medication.ClinicalIndication,
                                                  changeStatus,
                                                  !medication.ChangeDescription.IsNullOrEmptyWhitespace() ? medication.ChangeDescription : null,
                                                  medication.ChangeReason != null ? medication.ChangeReason.NarrativeText : null,
                                                  !medication.Comment.IsNullOrEmptyWhitespace() ? medication.Comment : null
                                              };

                        narrative.Add(medicationList);
                    }

                    StripEmptyColoums(ref narativeHeader, ref narrative, new List<int> { 3, 4, 5, 6 });

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
                    narrativeParagraph.Add(CreateExclusionStatementNarrative("Medications", medications.ExclusionStatement));
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
        /// This method creates the narrative for the medication items
        /// </summary>
        /// <param name="medications">A list of medicationItems</param>
        /// <returns>StrucDocText</returns>
        public StrucDocText CreateNarrativeLegacy(IEnumerable<IMedicationItem> medications)
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
                    string changeStatus;
                    if (medication.ChangeType != null && medication.ChangeType.NullFlavour != null) // because if there is no change status, the fact of whether this is a recommendation or change is irrelevant
                        changeStatus = "No change information";
                    else
                    {
                        changeStatus = medication.ChangeType != null ? medication.ChangeType.NarrativeText : string.Empty;
                        // if there's no change, or recommendation or change value, we don't say anything about it
                        if (medication.ChangeType != null &&
                            (medication.ChangeType.Code != ChangeTypeNctis.Unchanged.GetAttributeValue<NameAttribute, string>(x => x.Code)

                            ) && (medication.ChangeStatus == null || medication.ChangeStatus.NullFlavour == null))
                        {
                            if (medication.ChangeStatus != null && medication.ChangeStatus.Code == RecomendationOrChange.TheChangeHasBeenMade.GetAttributeValue<NameAttribute, string>(x => x.Code))
                                changeStatus = changeStatus + " (done)";
                            else
                                changeStatus = "Recommendation: " + changeStatus + " (not done)";
                        }
                    }

                    var medicationList = new List<String>
                                              {
                                                  medication.Medicine != null ? medication.Medicine.NarrativeText : null,
                                                  medication.Directions != null ? medication.Directions.NarrativeText : null,
                                                  medication.ClinicalIndication,
                                                  changeStatus,
                                                  !medication.ChangeDescription.IsNullOrEmptyWhitespace() ? medication.ChangeDescription : null,
                                                  medication.ChangeReason != null ? medication.ChangeReason.NarrativeText : null,
                                                  !medication.Comment.IsNullOrEmptyWhitespace() ? medication.Comment : null
                                              };

                    narrative.Add(medicationList);
                }

                StripEmptyColoums(ref narativeHeader, ref narrative, new List<int> { 3, 4, 5, 6 });

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
    }
}
    
