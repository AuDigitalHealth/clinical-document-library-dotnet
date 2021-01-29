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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The Immunisations class contains all the properties that CDA has identified 
    /// for a reviewed immunisation
    /// 
    /// Please use the CreateReviewedImmunisations() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Statement))]
    public class Immunisations
    {
        #region Properties

        /// <summary>
        /// A list of immunisations
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IImmunisation> AdministeredImmunisation { get; set; }

        /// <summary>
        /// An exclusion statement for the reviewed immunisations
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Statement ExclusionStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeImmunisation { get; set; }

        #endregion

        #region Constructors
        internal Immunisations()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Reviewed Immunisations
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages) 
        {
            var vb = new ValidationBuilder(path, messages);

            if (AdministeredImmunisation != null)
            {
                for (var x = 0; x < AdministeredImmunisation.Count; x++)
                {
                    AdministeredImmunisation[x].Validate
                        (
                            vb.Path + string.Format("AdministeredImmunisation[{0}]", x), vb.Messages
                        );
                }
            }
     
            if (ExclusionStatement != null)
            {
                ExclusionStatement.Validate(vb.Path + "GeneralStatement", vb.Messages);
            }

            // Recommendations exclusion statement choice
            var medicationsChoice = new Dictionary<string, object>()
            {
                { "AdministeredImmunisations", AdministeredImmunisation },
                { "ExclusionStatement", ExclusionStatement }
            };
            vb.ChoiceCheck(medicationsChoice);

        }
        #endregion
    }
}