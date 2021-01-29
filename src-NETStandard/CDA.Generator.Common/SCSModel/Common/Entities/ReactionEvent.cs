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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an reaction event
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class ReactionEvent
    {
        #region Properties

        /// <summary>
        /// A list of manifestations
        /// 
        /// SNOMED CT-AU
        ///  • 142341000036103 |Clinical manifestation
        /// reference set|
        ///  • 32570071000036102 |Clinical finding
        /// foundation reference set|
        /// 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> Manifestations { get; set; }

        /// <summary>
        /// The Reaction Type
        /// 
        /// ReactionType = 11000036103 |Adverse reaction type reference set
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ReactionType { get; set; }

        #endregion

        #region Constructors
        internal ReactionEvent()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this reaction event
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ReactionType != null)
            {
                ReactionType.Validate(path, messages);
            }

            if (vb.ArgumentRequiredCheck("Manifestations", Manifestations))
            {
                for (var x = 0; x < Manifestations.Count; x++)
                {
                    Manifestations[x].ValidateMandatory(
                        vb.Path + string.Format("Manifestations[{0}]", x), vb.Messages);
                }
            }
        }

        #endregion
    }
}