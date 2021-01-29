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
using System.Linq;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an AdverseReaction class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Common.AdverseReactions))]
    [KnownType(typeof(CodableText))]
    public class AdverseReaction : Common.AdverseReactions, IAdverseReactionDischargeSummary
    {
        #region Properties

        /// <summary>
        /// The Agent Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText AgentDescription { get; set; }

        /// <summary>
        /// The Adverse Reaction Type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText AdverseReactionType { get; set; }

        /// <summary>
        /// A list of Reaction Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> ReactionDescriptions { get; set; }

        #endregion

        #region Constructors
        internal AdverseReaction()
        {
        }
        #endregion

        #region Validation


        /// <summary>
        /// Validates this IAdverseSubstanceReactionsDischargeSummary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IAdverseReactionDischargeSummary.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("AgentDescription", AgentDescription))
            {
                if (AgentDescription != null) AgentDescription.ValidateMandatory(path + ".AgentDescription", messages);
            }

            if (vb.ArgumentRequiredCheck("AdverseReactionType", AdverseReactionType))
            {
                if (AdverseReactionType != null) AdverseReactionType.Validate(path + ".AdverseReactionType", messages);
            }

            if (ReactionDescriptions != null && ReactionDescriptions.Any())
            {
                    for (var x = 0; x < ReactionDescriptions.Count; x++)
                    {
                        if (ReactionDescriptions[x] != null)
                        {
                            ReactionDescriptions[x].ValidateMandatory(
                                vb.Path + string.Format("ReactionDescriptions[{0}]", x), vb.Messages);
                        }
                    }
            }
        }

        #endregion
    }
}