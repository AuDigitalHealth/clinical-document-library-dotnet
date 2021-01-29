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
  /// This interface encapsulates all the CDA specific context for an IDocument
  /// </summary>
  public interface IDocument
  {
      #region Properties

      /// <summary>
      /// A DateTimeAuthored for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      ISO8601DateTime DateTimeAuthored { get; set; }

      /// <summary>
      /// A BusinessDocumentType  
      /// </summary>
      [CanBeNull]
      [DataMember]
      CDADocumentType? BusinessDocumentType { get; set; }

      /// <summary>
      /// A BusinessDocumentType for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      IParticipationDocumentAuthor Author { get; set; }

      /// <summary>
      /// A Link for consolidated view documents
      /// </summary>
      [CanBeNull]
      [DataMember]
      Link Link { get; set; }

      #endregion

      #region Validation

      /// <summary>
      /// Validate the CDA Context for a IDocument
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      void Validate(string path, List<ValidationMessage> messages);

      #endregion

  }
}
