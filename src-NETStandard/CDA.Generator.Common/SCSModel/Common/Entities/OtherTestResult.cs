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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
  /// <summary>
  /// This class is designed to encapsulate the properties within a CDA document that make up 
  /// an Other Test Result
  /// </summary>
  [Serializable]
  [DataContract]
  public class OtherTestResult
  {
    #region Properties

    /// <summary>
    /// ReportName
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText ReportName { get; set; }

    /// <summary>
    /// ReportStatus
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText ReportStatus { get; set; }

    /// <summary>
    /// ReportDate
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ISO8601DateTime ReportDate { get; set; }

    /// <summary>
    /// ReportDate
    /// </summary>
    [CanBeNull]
    [DataMember]
    public EncapsulatedData ReportContent { get; set; }

    /// <summary>
    /// CustomNarrative
    /// </summary>
    [CanBeNull]
    [DataMember]
    public StrucDocText CustomNarrative { get; set; }

    #endregion

    #region Constructors
    internal OtherTestResult()
    {
    }
    #endregion

    #region Validation
    /// <summary>
    /// Validates this Other Test Result
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages to date, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      vb.ArgumentRequiredCheck("ReportName", ReportName);

      vb.ArgumentRequiredCheck("ReportContent", ReportContent);

      if (ReportContent != null)
      {
        ReportContent.Validate(path + ".ReportContent", messages);
      }
    }

    #endregion
  }
}
