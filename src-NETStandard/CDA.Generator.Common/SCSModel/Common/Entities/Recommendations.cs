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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up the details of a response
    /// </summary>
    [Serializable]
    [DataContract]
    public class Recommendations : IRecommendations
    {
        #region Properties

        /// <summary>
        /// Exclusion statement
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ExclusionStatement { get; set; }

        /// <summary>
        /// recomendation
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Recommendation> Recommendation { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeRecommendations { get; set; }

        #endregion

        #region Constructors
        internal Recommendations()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this set of recommendations
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Recommendation != null && Recommendation.Any())
            {
                Recommendation.ForEach(recomendation => recomendation.Validate(vb.Path + "Recommendation", messages));
            }

            // Recommendations exclusion statement choice
            var recommendationChoice = new Dictionary<string, object>()
            {
                { "Recommendation", Recommendation },
                { "GeneralStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(recommendationChoice);
        }

        #endregion
    }
}