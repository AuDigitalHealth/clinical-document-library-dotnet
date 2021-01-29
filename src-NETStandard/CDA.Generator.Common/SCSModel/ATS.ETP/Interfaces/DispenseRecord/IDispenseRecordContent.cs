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
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces 
{
    /// <summary>
    /// This interface encapsulates all the SCS specific content for a dispense record
    /// </summary>
    public interface IDispenseRecordContent
    {
        /// <summary>
        /// Custom Narrative Administrative Observations
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

        /// <summary>
        /// Dispense item
        /// </summary>
        [CanBeNull]
        DispenseItem DispenseItem { get; set; }

        /// <summary>
        /// Validates this IDispenseRecordContent
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
