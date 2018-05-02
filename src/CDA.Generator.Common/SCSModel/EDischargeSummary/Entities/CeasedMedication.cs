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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a CeasedMedications class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Statement))]
    [KnownType(typeof(TherapeuticGood))]
    public class CeasedMedications
    {
        /// <summary>
        /// A list of Therapeutic Goods
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ITherapeuticGoodCeased> TherapeuticGoods { get; set; }

        /// <summary>
        /// An exclusion statement
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Statement ExclusionStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeCeasedMedications { get; set; }

        /// <summary>
        /// Validates this Ceased Medications object for a Discharge Summary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (TherapeuticGoods != null)
            {
                TherapeuticGoods.ForEach(therapeuticGoods => therapeuticGoods.Validate(vb.Path + "TherapeuticGoods", messages));
            }

            // Exclusion statement choice
            var choiceCheck = new Dictionary<string, object>()
            {
                { "TherapeuticGoods", TherapeuticGoods },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(choiceCheck);

            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }
        }
    }
}
