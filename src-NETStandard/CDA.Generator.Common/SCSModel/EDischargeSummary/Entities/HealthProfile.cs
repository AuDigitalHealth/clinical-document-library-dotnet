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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a Health Profile class
    /// </summary>
    [Serializable]
    [DataContract]
    public class HealthProfile
    {
        /// <summary>
        /// A list of IParticipationNominatedPrimaryHealthCareProvider
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IParticipationNominatedPrimaryHealthCareProvider> NominatedPrimaryHealthCareProviders { get; set; }

        /// <summary>
        /// A list of AdverseSubstanceReactions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AdverseReactions AdverseReactions { get; set; }

        /// <summary>
        /// A list of Alerts
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Alerts Alerts { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeHealthProfile { get; set; }

        /// <summary>
        /// Validates this HealthProfile
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (NominatedPrimaryHealthCareProviders != null)
            {
                if (NominatedPrimaryHealthCareProviders != null)
                    foreach (var nominatedPrimaryHealthCareProviders in NominatedPrimaryHealthCareProviders)
                    {
                        nominatedPrimaryHealthCareProviders.Validate(vb.Path + "NominatedPrimaryHealthCareProvider", vb.Messages);
                    }
            }

            if (vb.ArgumentRequiredCheck("AdverseReactions", AdverseReactions))
            {
                if (AdverseReactions != null) AdverseReactions.Validate(vb.Path + "AdverseReactions", vb.Messages);
            }

            if (Alerts != null)
            {
                Alerts.Validate(vb.Path + "Alerts", vb.Messages);
            }
        }
    }
}
