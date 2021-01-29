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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;


namespace Nehta.VendorLibrary.CDA.SCSModel.Pathology
{
  /// <summary>
  /// This class encapsulates the Result Group
  /// </summary>
  public class Result
  {
    /// <summary>
    /// Individual Pathology Test Result Name
      /// The codes recommended for pathology terminology by The RCPA are LOINC codes and can be found at located at: http://www.rcpa.edu.au/Library/Practising-Pathology/PTIS/APUTS-Downloads
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText IndividualPathologyTestResultName { get; set; }

    /// <summary>
    /// Result Status
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ResultStatus ResultStatus { get; set; }

    #region Constructors
    internal Result()
    {
            
    }
    #endregion

    /// <summary>
    /// Validate the Content for this Result Group
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      vb.ArgumentRequiredCheck("IndividualPathologyTestResultName", IndividualPathologyTestResultName);
    }
  }
}
