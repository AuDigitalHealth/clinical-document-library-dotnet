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

using System.Collections.Generic;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a AdverseReactions class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Statement))]
    [KnownType(typeof(AdverseReactions))]
    public class AdverseReactions  
    {
        #region Properties

        /// <summary>
        /// A List of AdverseReactions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IAdverseReactionDischargeSummary> Reactions { get; set; }

        /// <summary>
        /// An Exclusion Statement
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Statement ExclusionStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeAdverseReactions { get; set; }

        #endregion

        #region Constructors
        internal AdverseReactions()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this AdverseReactions
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Reactions != null)
            {
                Reactions.ForEach(adverseReactions => adverseReactions.Validate(vb.Path + "Reactions", vb.Messages));
            }

            // Exclusion statement choice
            var choiceCheck = new Dictionary<string, object>()
            {
                { "Reactions", Reactions },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(choiceCheck);

            if (ExclusionStatement != null)
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
        }

        #endregion
    }
}