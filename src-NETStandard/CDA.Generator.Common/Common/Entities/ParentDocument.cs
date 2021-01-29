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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.CDAModel.Entities
{
  /// <summary>
  /// The Dispensing class contains all the properties that CDA has identified for 
  /// a Parent Document Item
  /// 
  /// Please use the CreateParentDocument() method on the appropriate parent SCS object to 
  /// instantiate this class.
  /// </summary>
  [Serializable]
  [DataContract]
  public class ParentDocument
  {
    #region Properties

    /// <summary>
    /// The Releated Document Type
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ReleatedDocumentType? ReleatedDocumentType { get; set; }

    /// <summary>
    /// Represents the unique instance identifier of a clinical document.
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier DocumentId { get; set; }

    /// <summary>
    /// The code specifying the particular kind of document (e.g. History and 0..1
    /// Physical, Discharge Summary, Progress Note).
    /// </summary>
    [CanBeNull]
    [DataMember]
    public CDADocumentType? DocumentType { get; set; }

    /// <summary>
    /// The CDA Parent Set Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier SetId { get; set; }

    /// <summary>
    /// The version for this CDA document
    /// </summary>
    [CanBeNull]
    [DataMember]
    public string VersionNumber { get; set; }

    #endregion

    #region Constructors

    internal ParentDocument()
    {

    }
    #endregion

    #region Validation

    /// <summary>
    /// Validates this Parent Document
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages to date, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      vb.ArgumentRequiredCheck("ReleatedDocumentType", ReleatedDocumentType);
      vb.ArgumentRequiredCheck("DocumentId", DocumentId);
    }

    #endregion
  }
}
