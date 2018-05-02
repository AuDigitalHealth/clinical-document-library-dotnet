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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Pathology
{
  /// <summary>
  /// This class encapsulates all Test Specimen Detail
  /// </summary>
  public class TestSpecimenDetail
  {
    /// <summary>
    /// Date and Time of Collection (Collection DateTime)
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ISO8601DateTime CollectionDateTime { get; set; }

    #region Constructors
    internal TestSpecimenDetail()
    {
            
    }

    #endregion

    /// <summary>
    /// Validate Content for this Test Specimen Detail
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);
      vb.ArgumentRequiredCheck("CollectionDateTime", CollectionDateTime);
    }
  }
}
