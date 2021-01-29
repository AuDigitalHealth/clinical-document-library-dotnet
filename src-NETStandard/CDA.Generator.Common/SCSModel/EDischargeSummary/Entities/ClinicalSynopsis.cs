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
using System.Text;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an persons Clinical Synopsis.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ClinicalSynopsis
    {
        #region Properties

        /// <summary>
        /// The Description for Clinical Synopsis
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Description { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeClinicalSynopsis { get; set; }

        #endregion

        #region Constructors
        internal ClinicalSynopsis()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates the Clinical Synopsis
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("Description", Description);
        }
        #endregion
    }
}
