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
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an HealthCheckAssesment 
    /// </summary>
    [Serializable]
    [DataContract]
    public class HealthCheckAssesment
    {
       #region Properties

      /// <summary>
      /// Section Title
      /// </summary>
      public QuestionnairesData? SectionCode { get; set; }

      /// <summary>
      /// A list AssessmentItems
      /// </summary>
      [CanBeNull]
      public List<AssessmentItem> AssessmentItems { get; set; }

      #endregion

       #region Constructors
        internal HealthCheckAssesment()
        {
        }
        #endregion

       #region Validation

        /// <summary>
        /// Validates this HealthCheckAssesment
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (AssessmentItems != null)
            for (var x = 0; x < AssessmentItems.Count; x++)
            {

              var assessmentItem = AssessmentItems[x];

              if (vb.ArgumentRequiredCheck(string.Format("AssessmentItems[{0}]", x), assessmentItem))
                AssessmentItems[x].Validate(vb.Path + string.Format("AssessmentItems[{0}]", x), vb.Messages);
            }
            vb.ArgumentRequiredCheck("SectionCode", SectionCode);
        }

        #endregion
    }
}