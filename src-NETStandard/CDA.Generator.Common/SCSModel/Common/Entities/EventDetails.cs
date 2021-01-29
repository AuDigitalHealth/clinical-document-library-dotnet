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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// Event Details
    /// </summary>
    [Serializable]
    [DataContract]
    public class EventDetails
    {
        #region Properties

        /// <summary>
        /// Reason for Encounter Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ClinicalSynopsisDescription { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeEventDetails { get; set; }

        #endregion

        #region Constructors
        internal EventDetails()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates Event Details
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("ClinicalSynopsisDescription", ClinicalSynopsisDescription);
        }

        #endregion
    }
}
