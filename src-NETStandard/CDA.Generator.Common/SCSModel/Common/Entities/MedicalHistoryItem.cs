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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a medical history item
    /// </summary>
    [Serializable]
    [DataContract]
    public class MedicalHistoryItem : IMedicalHistoryItem  
    {
        #region Properties

        /// <summary>
        /// Show Ongoing In Narrative
        /// NOTE: This field will always show (Ongoing) in the narrative.
        ///       It will show '(ongoing)' also if DateOfOnset is set eg.. 14 Mar 2012 08:27+1000 -> (ongoing)
        ///       Otherwise it will only show '(ongoing)'
        /// 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public bool? ShowOngoingInNarrative { get; set; }

        /// <summary>
        /// Diagnosis or procedure name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String DiagnosisOrProcedureName { get; set; }

        /// <summary>
        /// Date / Time recorded
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval DateTimeInterval { get; set; }

        /// <summary>
        /// A list of author IDs
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<String> AuthorIDs { get; set; }

        /// <summary>
        /// Medical History Item Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ItemDescription { get; set; }

        /// <summary>
        /// Medical History Item Comment
        /// </summary>
        [CanBeNull]
        public String ItemComment { get; set; }

        #endregion

        #region Constructors

        internal MedicalHistoryItem()
        {
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this medical history item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicalHistoryItem.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("ItemDescription", ItemDescription);
        }

        #endregion
    }
}