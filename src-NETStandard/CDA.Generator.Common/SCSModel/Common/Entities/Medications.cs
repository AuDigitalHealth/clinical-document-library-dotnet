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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Please use the CreateMedications() method on the appropriate parent SCS object to instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Medication))]
    internal class Medications : IMedicationsEReferral, IMedications, IMedicationsSpecialistLetter
    {
        #region Properties
        /// <summary>
        /// Medications review
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Review MedicationsReview { get; set; }

        /// <summary>
        /// A list of Medications
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IMedicationInstruction> MedicationsList { get; set; }

        /// <summary>
        /// A list of used medications
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IMedicationInstruction> IMedicationsEReferral.MedicationsList { get; set; }

        /// <summary>
        /// A list of used medications
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IMedicationItem> IMedicationsSpecialistLetter.MedicationsList { get; set; }

        /// <summary>
        /// A list of reviewed medications
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IMedication> IMedications.Medications { get; set; }
        
        /// <summary>
        /// An exclusion statement
        /// </summary>
        [CanBeNull]
        [DataMember]
        [OID(Identifier = "DG-16136", OID = "1.2.36.1.2001.1001.101.102.16136")]
        public Statement ExclusionStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeMedications { get; set; }

        #endregion

        #region Constructors
        internal Medications()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this medications object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationsSpecialistLetter.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var medicationsList = ((IMedicationsSpecialistLetter)this).MedicationsList;

            if (medicationsList != null)
            {
                for (var x = 0; x < medicationsList.Count; x++)
                {
                    medicationsList[x].Validate(vb.Path + string.Format("MedicationsList[{0}]", x), vb.Messages);
                }
            }

            // Recommendations exclusion statement choice
            var medicationsChoice = new Dictionary<string, object>()
            {
                { "MedicationsList", medicationsList },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(medicationsChoice);

            // Validate exclusion statement
            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this medications object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationsEReferral.Validate(string path, List<ValidationMessage> messages) 
        {
            var vb = new ValidationBuilder(path, messages);

            var medicationsList = ((IMedicationsEReferral)this).MedicationsList;

            if (medicationsList != null)
            {
                for (var x = 0; x < medicationsList.Count; x++)
                {
                    medicationsList[x].Validate(vb.Path + string.Format("MedicationsList[{0}]", x), vb.Messages);
                }
            }

            // Medications exclusion statement choice
            var medicationsChoice = new Dictionary<string, object>()
            {
                { "MedicationsList", medicationsList },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(medicationsChoice);

            // Validate exclusion statement if present
            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this medications object as a reviewed medications
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedications.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var medicationsList = ((IMedications)this).Medications;

            if (medicationsList != null)
            {
                for (var x = 0; x < medicationsList.Count; x++)
                {
                    medicationsList[x].Validate(vb.Path + string.Format("MedicationsList[{0}]", x), vb.Messages);
                }
            }

            // Medications exclusion statement choice
            var adverseChoice = new Dictionary<string, object>()
            {
                { "Medications", medicationsList },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(adverseChoice);

            // Validate exclusion statement
            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }
        }
        #endregion
    }
}