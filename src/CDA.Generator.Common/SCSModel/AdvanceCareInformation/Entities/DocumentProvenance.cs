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
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.AdvanceCareInformation.Entities
{
  /// <summary>
  /// Document Provenance
  /// </summary>
  public class DocumentProvenance
  {
      #region Properties

      /// <summary>
      /// Document Type
      /// </summary>
      [CanBeNull]
      [DataMember]
      public DocumentType? DocumentType { get; set; }

      /// <summary>
      /// Document Identifier
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Identifier DocumentIdentifier { get; set; }
      
      /// <summary>
      /// Author Non Healthcare Provider
      /// </summary>
      [CanBeNull]
      [DataMember]
      public IParticipationAuthorPerson Author { get; set; }

      #endregion

      #region Constructors

      internal DocumentProvenance()
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

        vb.ArgumentRequiredCheck("DocumentType", DocumentType);

        if (DocumentIdentifier != null)
            DocumentIdentifier.Validate(vb.Path + "DocumentIdentifier", messages);

        if (vb.ArgumentRequiredCheck("Author", Author))
        {
            Author.Validate(vb.Path + "Author", messages);
        }
     }

      #endregion

  }
}
