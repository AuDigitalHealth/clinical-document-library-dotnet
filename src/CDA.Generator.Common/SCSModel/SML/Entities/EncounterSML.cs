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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class EncounterSML
    {

        /// <summary>
        /// The Shared Id for SML
        /// </summary>
        [CanBeNull]
        public Guid EncounterId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [CanBeNull]
        public String EncounterDescription { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [CanBeNull]
        public ICodableText EncounterStatus { get; set; }

        /// <summary>
        /// Class
        /// </summary>
        [CanBeNull]
        public ICodableText EncounterClass { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [CanBeNull]
        public ICodableText EncounterType { get; set; }

        /// <summary>
        /// DateTime
        /// </summary>
        [CanBeNull]
        public CdaInterval EncounterPeriod { get; set; }

        /// <summary>
        /// Reason
        /// </summary>
        [CanBeNull]
        public ICodableText EncounterReason { get; set; }


        /// <summary>
        /// Validates this reviewed IMedication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
        }
    }
}
