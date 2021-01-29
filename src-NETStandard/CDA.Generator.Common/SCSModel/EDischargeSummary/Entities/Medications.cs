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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that extend the Medications class for discharge summary
    /// </summary>
    [Serializable]
    [DataContract]
    internal class Medications : Common.Medications, IMedicationsDischargeSummary
    {
        #region Properties

        /// <summary>
        /// The Current Medications
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CurrentMedications CurrentMedications { get; set; }

        /// <summary>
        /// The Ceased Medicationss
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CeasedMedications CeasedMedications { get; set; }

        #endregion

        #region Constructors
        internal Medications()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this medications object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationsDischargeSummary.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("CurrentMedications", CurrentMedications))
            {
                if (CurrentMedications != null) CurrentMedications.Validate(vb.Path + "CurrentMedications", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("CeasedMedications", CeasedMedications))
            {
                if (CeasedMedications != null) CeasedMedications.Validate(vb.Path + "CeasedMedications", vb.Messages);
            }
        }

         #endregion
    }
}