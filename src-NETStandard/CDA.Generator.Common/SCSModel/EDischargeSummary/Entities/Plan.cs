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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a Plan
    /// </summary>
    [Serializable]
    [DataContract]
    public class Plan
    {
        /// <summary>
        /// A list of ArrangedServices
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ArrangedServices> ArrangedServices { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeArrangedServices { get; set; }

        /// <summary>
        /// Record of recommendations and information provided
        /// </summary>
        [CanBeNull]
        [DataMember]
        public RecommendationsInformationProvided RecommendationsInformationProvided { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeRecommendationsInformationProvided { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativePlan { get; set; }

        /// <summary>
        /// Validates this Plan for a Discharge Summary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Model here does not represent the spec, No point creating a AdverseReactions if it not intended to be used
            if (ArrangedServices != null)
            {
                ArrangedServices.ForEach(arrangedServices => arrangedServices.Validate(vb.Path + "ArrangedServices", vb.Messages));
            }

            if (vb.ArgumentRequiredCheck("RecommendationsInformationProvided", RecommendationsInformationProvided))
            {
                if (RecommendationsInformationProvided != null)
                    RecommendationsInformationProvided.Validate(vb.Path + "RecommendationsInformationProvided", vb.Messages);
            }
        }
    }
}