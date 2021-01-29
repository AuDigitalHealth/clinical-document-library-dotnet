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
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.Entities
{
  /// <summary>
  /// Document Provenance
  /// </summary>
  public class DocumentDetails
  {
      #region Properties

      /// <summary>
      /// Report Name
      /// </summary>
      [CanBeNull]
      [DataMember]
      public string ReportDescription { get; set; }

      /// <summary>
      /// Report Identifier
      /// </summary>
      [CanBeNull]
      [DataMember]
      public Identifier ReportIdentifier { get; set; }

      /// <summary>
      /// Report Date
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ISO8601DateTime ReportDate { get; set; }

      /// <summary>
      /// Report Status
      /// </summary>
      [CanBeNull]
      [DataMember]
      public ICodableText ReportStatus { get; set; }

      #endregion

      #region Constructors
      internal DocumentDetails()
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

        if (ReportIdentifier != null)
        {
            ReportIdentifier.Validate("ReportIdentifier", messages);
        }

        vb.ArgumentRequiredCheck("ReportDate", ReportDate);
        vb.ArgumentRequiredCheck("ReportName", ReportDescription);

        if (vb.ArgumentRequiredCheck("ReportStatus", ReportStatus))
        {
           ReportStatus.Validate(path, messages);
        }
      }

      #endregion

  }
}
