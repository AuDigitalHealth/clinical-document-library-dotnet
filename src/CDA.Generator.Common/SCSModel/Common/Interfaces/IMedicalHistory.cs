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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IMedicalHistory interface encapsulate the properties within a CDA document that make up a medical history
    /// </summary>
    public interface IMedicalHistory
    {

        /// <summary>
        /// An exclusion statement to associate with the problem diagnosis
        /// </summary>
        [CanBeNull]
        Statement ProblemDiagnosisExclusionStatement { get; set; }

        /// <summary>
        /// An exclusion statement to associate with the procedures
        /// </summary>
        [CanBeNull]
        Statement ProceduresExclusionStatement { get; set; }

        /// <summary>
        /// Problem diagnosis
        /// </summary>
        [CanBeNull]
        List<IProblemDiagnosis> ProblemDiagnosis { get; set; }

        /// <summary>
        /// A list of procedures
        /// </summary>
        [CanBeNull]
        List<Procedure> Procedures { get; set; }

        /// <summary>
        /// A list of medical history items that contain any - Uncategorised MedicalHistory Items
        /// </summary>
        [CanBeNull]
        List<IMedicalHistoryItem> MedicalHistoryItems { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeMedicalHistory { get; set; }

        /// <summary>
        /// Validates this medical history
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        /// <param name="showExclusionStatements">Validate showExclusionStatements</param>
        void Validate(string path, List<ValidationMessage> messages, bool showExclusionStatements);

        /// <summary>
        /// Validates this medical history
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        /// <param name="showExclusionStatements">Validate showExclusionStatements</param>
        void ValidateShs(string path, List<ValidationMessage> messages, bool showExclusionStatements);
    }
}
