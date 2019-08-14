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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an Recommendations Information Provided class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class RecommendationsInformationProvided
    {
        /// <summary>
        /// A list of Recommendations Provided
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<RecommendationsProvided> RecommendationsProvided { get; set; }

        /// <summary>
        /// Information Provided
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InformationProvided InformationProvided { get; set; }

        /// <summary>
        /// Validates this RecommendationsInformationProvided for a Discharge Summary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("RecommendationsProvided", RecommendationsProvided))
            {
                if (RecommendationsProvided != null)
                    for (var x = 0; x < RecommendationsProvided.Count; x++)
                    {
                        if (RecommendationsProvided[x] != null)
                        {
                            RecommendationsProvided[x].Validate(
                                vb.Path + string.Format("RecommendationsProvided[{0}]", x), vb.Messages);
                        }
                    }
            }

            if (InformationProvided != null)
            {
                InformationProvided.Validate(vb.Path + "InformationProvided", vb.Messages);
            }
        }
    }
}