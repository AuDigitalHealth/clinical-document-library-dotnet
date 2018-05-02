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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// the details of a response
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Procedure))]
    public class Response : IResponseDetails
    {
        #region Properties
        /// <summary>
        /// Procedures
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IProcedureName> Procedures { get; set; }

        /// <summary>
        /// Diagnoses
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> Diagnoses  { get; set; }

        /// <summary>
        /// Other Diagnosis Entries
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<string> OtherDiagnosisEntries { get; set; }

        /// <summary>
        /// Response Narrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ResponseNarrative { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeResponseDetails { get; set; }
        #endregion

        #region Constructors
        internal Response()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Response
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if ((Procedures == null && Diagnoses == null && OtherDiagnosisEntries == null)  ||
               (Procedures == null || !Procedures.Any()) && (Diagnoses == null || !Diagnoses.Any()) && (OtherDiagnosisEntries == null || !OtherDiagnosisEntries.Any()))
            {
              vb.AddValidationMessage("Response Details", null, "Response Details - 'section' tag shall have either an 'PROCEDURE' or 'DIAGNOSIS' or 'OTHER DIAGNOSIS PROCEDURE ENTRY'");
            }

            if (Procedures != null)
            {
                Procedures.ForEach(procedure => procedure.Validate(vb.Path + "Procedure", messages));
            }

            if (Diagnoses  != null && Diagnoses.Any())
            {
                Diagnoses .ForEach(diagnosis => diagnosis.Validate(vb.Path + "Diagnosis", messages));
            }

            if (OtherDiagnosisEntries != null && OtherDiagnosisEntries.Any())
            {
                for (int x = 0; x < OtherDiagnosisEntries.Count; x++)
                {
                    vb.ArgumentRequiredCheck(string.Format("OtherDiagnosisEntries[{0}]", x), OtherDiagnosisEntries[x]);
                }
            }

            vb.ArgumentRequiredCheck("ResponseNarrative", ResponseNarrative);
        }

        #endregion
    }
}