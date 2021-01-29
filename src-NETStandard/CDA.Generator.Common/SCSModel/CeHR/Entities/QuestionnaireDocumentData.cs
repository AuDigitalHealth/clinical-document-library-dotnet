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
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an QuestionnaireDocumentData 
    /// </summary>
    [Serializable]
    [DataContract]
    public class QuestionnaireDocumentData
    {
     #region Properties

      /// <summary>
      /// Document Link 
      /// </summary>
      [CanBeNull]
      public QuestionnairesData? QuestionnairesData { get; set; }

      /// <summary>
      /// Document Link 
      /// </summary>
      [CanBeNull]
      public Link DocumentLink { get; set; }

      /// <summary>
      /// Assessment
      /// </summary>
      [CanBeNull]
      public bool? Assessment { get; set; }

      /// <summary>
      /// Author Name
      /// </summary>
      [CanBeNull]
      public string AuthorName { get; set; }

      /// <summary>
      /// Document Date
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DocumentDate { get; set; }

      #endregion

     #region Constructors

      internal QuestionnaireDocumentData()
        {
        }

     #endregion

     #region Validation

        /// <summary>
        /// Validates this Questionnaire Document Data
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          vb.ArgumentRequiredCheck("QuestionnairesData", QuestionnairesData);
          //vb.ArgumentRequiredCheck("DocumentLink", DocumentLink);
          //vb.ArgumentRequiredCheck("Assessment", Assessment);
          //vb.ArgumentRequiredCheck("AuthorName", AuthorName);
          //vb.ArgumentRequiredCheck("DocumentDate", DocumentDate);
        }

        #endregion
    }
}