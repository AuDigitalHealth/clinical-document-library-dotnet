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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Medications))]

    internal class Content : Common.Content, IEDischargeSummaryContent
    {
        #region Discharge Summary Properties
        /// <summary>
        /// The Event
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Event Event { get; set; }

        /// <summary>
        /// The Medications
        /// </summary>
        ///         
        [CanBeNull]
        [DataMember]
        IMedicationsDischargeSummary IEDischargeSummaryContent.Medications { get; set; }

        /// <summary>
        /// The HealthProfile
        /// </summary>
        [CanBeNull]
        [DataMember]
        public HealthProfile HealthProfile { get; set; }

        /// <summary>
        /// The Plan
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Plan Plan { get; set; }
        #endregion

        #region Constructors
        internal Content()
        {

        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Content class
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IEDischargeSummaryContent.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = ((IEDischargeSummaryContent)this);

            if (
                  castedContent.NarrativeOnlyDocument != null && castedContent.NarrativeOnlyDocument.Any() &&
                    (
                        castedContent.HealthProfile != null ||
                        castedContent.Medications != null ||
                        castedContent.Plan != null ||
                        castedContent.StructuredBodyFiles != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NarrativeOnlyDocument", null, "Both structured body components and a NarrativeOnlyDocument have been specified; only one instance of these is allowed.");
            }

            if (
                    castedContent.StructuredBodyFiles != null && castedContent.StructuredBodyFiles.Any() &&
                    (
                        castedContent.HealthProfile  != null ||
                        castedContent.Medications != null ||
                        castedContent.Plan != null ||
                        castedContent.NarrativeOnlyDocument != null
                    )
                )
            {
                vb.AddValidationMessage(vb.Path + "NonXmlBody", null, "Both structured XML body and a structured XML body attachment have been specified; only one instance of these is allowed.");
            }

            if ((castedContent.StructuredBodyFiles == null || !castedContent.StructuredBodyFiles.Any()) && (castedContent.NarrativeOnlyDocument == null || !castedContent.NarrativeOnlyDocument.Any()))
            {
                if (vb.ArgumentRequiredCheck("Event", Event))
                {
                    if (Event != null) Event.Validate(vb.Path + "Event", vb.Messages);
                }

                if (vb.ArgumentRequiredCheck("Medications", castedContent.Medications))
                {
                    if (castedContent.Medications != null) castedContent.Medications.Validate(vb.Path + "Medications", vb.Messages);
                }

                if (vb.ArgumentRequiredCheck("HealthProfile", HealthProfile))
                {
                    if (HealthProfile != null) HealthProfile.Validate(vb.Path + "HealthProfile", vb.Messages);
                }

                if (vb.ArgumentRequiredCheck("Plan", Plan))
                {
                    if (Plan != null) Plan.Validate(vb.Path + "Plan", vb.Messages);
                }
            } else
            {

                if (vb.ArgumentRequiredCheck("Event", Event))
                {
                    if (vb.ArgumentRequiredCheck("Event.Encounter", Event.Encounter))
                    {
                        vb.ArgumentRequiredCheck("Event.Encounter", Event.Encounter.EncounterPeriod);
                        vb.ArgumentRequiredCheck("Event.Encounter", Event.Encounter.SeparationMode);

                        if (vb.ArgumentRequiredCheck("Event.Encounter.ResponsibleHealthProfessional", Event.Encounter.ResponsibleHealthProfessional))
                        {
                            Event.Encounter.ResponsibleHealthProfessional.Validate(vb.Path + "Plan", vb.Messages);
                        }
                    }
                }

            }

        }

        #endregion
    }
}