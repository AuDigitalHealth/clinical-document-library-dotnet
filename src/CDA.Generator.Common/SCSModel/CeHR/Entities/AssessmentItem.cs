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
using CDA.Generator.Common.SCSModel.CeHR.Enum;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an AssesmentItem 
    /// </summary>
    [Serializable]
    [DataContract]
    public class AssessmentItem
    {
        #region Assesment Item Title

        /// <summary>
        /// Questionnaires Data
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText QuestionData { get; set; }

        /// <summary>
        /// Assesment Item Title
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AnswersData? AnswersData { get; set; }

        /// <summary>
        /// AnswersValue
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AnswersValue? AnswersValue { get; set; }

        /// <summary>
        /// Date Time
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTime { get; set; }

        /// <summary>
        /// Assesment Item
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string FreeText { get; set; }

        #endregion

        #region Constructors
        internal AssessmentItem()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this AssessmentItem
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("QuestionData", QuestionData);
            vb.ArgumentRequiredCheck("AnswersData", AnswersData);
  
            // Medications exclusion statement choice
            var adverseChoice = new Dictionary<string, object>()
            {
                { "AnswersValue", AnswersValue },
                { "FreeText", FreeText }
            };

            vb.ChoiceCheck(adverseChoice);
        }

        #endregion
    }
}