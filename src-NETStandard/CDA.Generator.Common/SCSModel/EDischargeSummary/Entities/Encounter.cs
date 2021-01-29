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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an Encounter
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Participation))]
    [KnownType(typeof(CodableText))]
    public class Encounter
    {
        #region Properties

        /// <summary>
        /// The Duration of the EncounterPeriod
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval EncounterPeriod { get; set; }

        /// <summary>
        /// NullFlavor for the EncounterPeriod 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public NullFlavour? EncounterPeriodNullFlavor { get; set; } 

        /// <summary>
        /// The SeparationMode
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText SeparationMode { get; set; }

        /// <summary>
        /// A list of Specialty's
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> Specialty { get; set; }

        /// <summary>
        /// The Location Of Discharge
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String LocationOfDischarge { get; set; }

        /// <summary>
        /// The Responsible Health Professional
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationResponsibleHealthProfessional ResponsibleHealthProfessional { get; set; }

        /// <summary>
        /// A List of Other Participants
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IParticipationOtherParticipant> OtherParticipants { get; set; }

        #endregion

        #region Constructors
        internal Encounter()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Encounter
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)  
        {
            var vb = new ValidationBuilder(path, messages);

            // Encounter Period can only contain a Encounter Period Null Flavor or a Encounter Period
            var encounterPeriod = new Dictionary<string, object>()
            {
                { "EncounterPeriod", EncounterPeriod },
                { "EncounterPeriodNullFlavor", EncounterPeriodNullFlavor }
            };
            vb.ChoiceCheck(encounterPeriod);


            if (vb.ArgumentRequiredCheck(vb.Path + "SeparationMode", SeparationMode))
            {
                SeparationMode.Validate(vb.Path + "SeparationMode", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck(vb.Path + "ResponsibleHealthProfessional", ResponsibleHealthProfessional))
            {
                if (ResponsibleHealthProfessional != null)
                    ResponsibleHealthProfessional.Validate(vb.Path + "ResponsibleHealthProfessional", messages);
            }

            if (vb.ArgumentRequiredCheck(vb.Path + "Specialty", Specialty))
            {
                if (Specialty != null)
                    Specialty.ForEach(specialty => specialty.Validate(vb.Path + "Specialty", messages));
            }

            if (OtherParticipants != null)
            {
                OtherParticipants.ForEach(otherParticipants => otherParticipants.Validate(vb.Path + "OtherParticipants", messages));
            }

        }
        #endregion
    }
}