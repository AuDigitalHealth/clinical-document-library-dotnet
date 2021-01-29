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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a Medication History class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class MedicationHistory : IMedicationHistory, IMedicationHistoryCeased
    {
        #region Properties

        /// <summary>
        /// The Item Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ItemStatus { get; set; }

        /// <summary>
        /// Change Detail
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ChangeDetail { get; set; }

        /// <summary>
        /// The Changes Made
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ChangesMade { get; set; }

        /// <summary>
        /// The Reason For Change
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ReasonForChange { get; set; }

        /// <summary>
        /// The Medication Duration
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval MedicationDuration { get; set; }

        #endregion

        #region Constructors
        internal MedicationHistory()
        {
            ChangeDetail = Guid.NewGuid().ToString();
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Medication History object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationHistoryCeased.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ItemStatus", ItemStatus))
            {
                if (ItemStatus != null) ItemStatus.Validate(path + ".ItemStatus", messages);
            }

            if (vb.ArgumentRequiredCheck("ChangesMade", ChangesMade))
            {
                if (ChangesMade != null) ChangesMade.Validate(path + ".ChangesMade", messages);
            }

            vb.ArgumentRequiredCheck("ReasonForChange", ReasonForChange);
        }

        /// <summary>
        /// Validates this object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationHistory.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ItemStatus", ItemStatus))
            {
                if (ItemStatus != null) ItemStatus.ValidateMandatory(path + ".ItemStatus", messages);
            }

            if (vb.ArgumentRequiredCheck("ChangesMade", ChangesMade))
            {
                if (ChangesMade != null) ChangesMade.ValidateMandatory(path + ".ChangesMade", messages);
            }
        }

         #endregion
    }
}
