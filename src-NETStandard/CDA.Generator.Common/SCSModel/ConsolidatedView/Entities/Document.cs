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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.ConsolidatedView.Entities
{
  /// <summary>
  /// Models the Document
  /// </summary>
  public class Document : IDocument, IDocumentWithHealthEventEnded
  {
      #region Properties

      /// <summary>
      /// A DateTimeHealthEventEnded for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ISO8601DateTime DateTimeHealthEventEnded { get; set; }

      /// <summary>
      /// A DateTimeAuthored for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ISO8601DateTime DateTimeAuthored { get; set; }

      /// <summary>
      /// A BusinessDocumentType  
      /// </summary>
      [CanBeNull]
      [DataMember]
      public CDADocumentType? BusinessDocumentType { get; set; }

      /// <summary>
      /// A BusinessDocumentType for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      public IParticipationDocumentAuthor Author { get; set; }

      /// <summary>
      /// A Link for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Link Link { get; set; }

      #endregion

      #region Constructors
      internal Document()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the CDA Context for Document
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        vb.ArgumentRequiredCheck("DateTimeAuthored", DateTimeAuthored);
        vb.ArgumentRequiredCheck("BusinessDocumentType", BusinessDocumentType);

        if (Author != null)
        {
           Author.Validate(vb.Path + "Author", messages);
        }

        if (vb.ArgumentRequiredCheck("Link", Link))
        {
           if (Link != null) Link.Validate(vb.Path + "Link", messages);
        }
  }

      #endregion

  }
}
