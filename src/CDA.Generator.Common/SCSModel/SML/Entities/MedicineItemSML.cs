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
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class MedicineItemSML
    {
        /// <summary>
        /// The medication Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Change description
        /// </summary>
        [CanBeNull]
        public String ChangeDescription { get; set; }

        /// <summary>
        /// Change type
        /// </summary>
        [CanBeNull]
        public ICodableText ChangeTypeFlag { get; set; }

        /// <summary>
        /// Encounter
        /// </summary>
        [CanBeNull]
        public EncounterSML Encounter { get; set; }

        /// <summary>
        /// medication status
        /// </summary>
        [CanBeNull]
        public ICodableText MedicationStatus { get; set; }

        /// <summary>
        /// Where administered category
        /// </summary>
        [CanBeNull]
        public ICodableText WhereAdministeredCategory { get; set; }

        /// <summary>
        /// The medicine as a codableText
        /// </summary>
        [CanBeNull]
        public MedicationSML Medication { get; set; }

        /// <summary>
        /// The medicine as a codableText
        /// </summary>
        [CanBeNull]
        public CdaInterval EffectiveTimeTakenOrNot { get; set; }

        /// <summary>
        /// Informant (related person)
        /// </summary>
        [CanBeNull]
        public RelatedPersonSML InformantRelatedPerson { get; set; }

        /// <summary>
        /// Informant (practitioner)
        /// </summary>
        [CanBeNull]
        public PractitionerSML InformantPractitioner { get; set; }
        
        /// <summary>
        /// Taken
        /// </summary>
        [CanBeNull]
        public ICodableText Taken { get; set; }

        /// <summary>
        /// Reason Not Taken
        /// </summary>
        [CanBeNull]
        public List<ICodableText> ReasonNotTaken { get; set; }

        /// <summary>
        /// Medicine Purpose = The clinical indication why taken
        /// </summary>
        [CanBeNull]
        public List<ICodableText> MedicinePurpose { get; set; }

        /// <summary>
        /// AdditionalComments
        /// </summary>
        [CanBeNull]
        public List<NoteSML> AdditionalComments { get; set; }

        /// <summary>
        /// Dosage
        /// </summary>
        [CanBeNull]
        public List<DosageSML> Dosage { get; set; }

        /// <summary>
        /// Type of informant.
        /// </summary>
        public enum InformantTypeEnum
        {
            /// <summary>
            /// Person who is related to patient
            /// </summary>
            RelatedPerson,

            /// <summary>
            /// Practitioner
            /// </summary>
            Practitioner,

            /// <summary>
            /// Patient informant
            /// </summary>
            Patient
        }

        /// <summary>
        /// Validates this reviewed IMedication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
        }
    }
}
