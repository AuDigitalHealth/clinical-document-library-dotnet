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
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class MedicationListSML
    {

        /// <summary>
        /// AuthorRole
        /// </summary>
        [CanBeNull]
        public IParticipationAuthorHealthcareProvider AuthorRole { get; set; }


        /// <summary>
        /// Change type
        /// </summary>
        [CanBeNull]
        public ICodableText PackedInDaa { get; set; }

        /// <summary>
        /// Encounter
        /// </summary>
        [CanBeNull]
        public EncounterSML Encounter { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        [CanBeNull]
        public List<NoteSML> AdditionalListComments { get; set; }


        /// <summary>
        /// A list of used medications
        /// </summary>
        [CanBeNull]
        public List<MedicineItemSML> MedicineItem { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeMedications { get; set; }

        /// <summary>
        /// Validates this reviewed IMedication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
        }
    }
}
