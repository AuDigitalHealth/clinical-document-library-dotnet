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
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Please use the CreateAdverseSubstanceReaction() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [KnownType(typeof(DischargeSummary.AdverseReaction))]
    public class AdverseReactions : IAdverseReactions, IAdverseReactionsWithoutExclusions
    {
        #region Properties

        /// <summary>
        /// Adverse substance reactions review
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Review AdverseSubstanceReactionsReview { get; set; }
        
        /// <summary>
        /// A list of adverse substance reactions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Reaction> AdverseSubstanceReaction { get; set; }
        
        /// <summary>
        /// Exclusion statement
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Statement ExclusionStatement { get; set; }

        /// <summary>
        /// EmptyReason statement (for SML)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public EmptyReason EmptyReasonStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeAdverseReactions { get; set; }
        #endregion

        #region Constructors
        internal AdverseReactions()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Adverse Substance Reaction
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages) 
        {
            var vb = new ValidationBuilder(path, messages);

            if (AdverseSubstanceReaction != null)
            {
                for (var x = 0; x < AdverseSubstanceReaction.Count; x++)
                {
                    AdverseSubstanceReaction[x].Validate(vb.Path + string.Format("AdverseSubstanceReaction[{0}]", x), vb.Messages);
                }
            }

            // Adverse reactions exclusion statement choice
            var adverseChoice = new Dictionary<string, object>()
            {
                { "AdverseSubstanceReaction", AdverseSubstanceReaction },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(adverseChoice);

            // Validate exclusion statement
            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this Adverse Substance Reaction
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IAdverseReactionsWithoutExclusions.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (AdverseSubstanceReaction != null)
          {
            for (var x = 0; x < AdverseSubstanceReaction.Count; x++)
            {
              AdverseSubstanceReaction[x].Validate(vb.Path + string.Format("AdverseSubstanceReaction[{0}]", x), vb.Messages);
            }
          }

          // AdverseSubstanceReaction
          if (AdverseSubstanceReaction == null || !AdverseSubstanceReaction.Any())
          {
            vb.AddValidationMessage(vb.PathName + ".AdverseSubstanceReaction", null, "At least one AdverseSubstanceReaction must be specified");
          }

          // Validate exclusion statement
          if (ExclusionStatement != null)
          {
             ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
          }
        }

        #endregion
    }
}