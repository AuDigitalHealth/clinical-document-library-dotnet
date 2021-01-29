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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ConsolidatedView.Entities
{
  /// <summary>
  /// A Link class for the generation of Link references in CDA
  /// </summary>
  public class Link
  {
      #region Properties

      [CanBeNull]
      [DataMember]
      private Guid? _id;

      /// <summary>
      /// This identifier is used to associated participants through the document.
      /// </summary>
      [CanBeNull]
      public Guid id
      {
        get
        {
          if (_id == null)
          {
            var guid = Guid.NewGuid();
            while (true)
            {
               var firstCharacter = guid.ToString()[0];
               if (Char.IsLetter(firstCharacter))
               {
                  break;
               }

              guid = Guid.NewGuid();
            }

           _id = guid;
          }

          return _id.Value;
        }
        set
        {
          _id = value;
        }
      }

      /// <summary>
      /// The ClincalDocument/id of the target document. 
      /// </summary>
      [CanBeNull]
      [DataMember]
       public string RepositoryIdentifier { get; set; }

      /// <summary>
      /// The Document Identifier
      /// </summary>
      [CanBeNull]
      [DataMember]
      public string DocumentIdentifier { get; set; }

      /// <summary>
      /// The Document Identifier Extension
      /// </summary>
      [CanBeNull]
      [DataMember]
      public string DocumentIdentifierExtension { get; set; }

      /// <summary>
      /// The CDA Implementation Guide's templateId (ClinicalDocument/ tempalteId) of the target document.
      /// </summary>
      [CanBeNull]
      [DataMember]
      public CDADocumentType? TemplateId { get; set; }

      #endregion

      #region Constructors
      internal Link()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the CDA Context for Link
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("RepositoryIdentifier", RepositoryIdentifier);
            vb.ArgumentRequiredCheck("DocumentIdentifier", DocumentIdentifier);
            vb.ArgumentRequiredCheck("TemplateId", TemplateId);
        }

      #endregion
  }
}
