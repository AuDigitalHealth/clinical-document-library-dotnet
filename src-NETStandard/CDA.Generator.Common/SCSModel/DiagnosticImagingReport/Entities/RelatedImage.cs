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
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.DiagnosticImagingReport.Entities
{
  /// <summary>
    /// This class encapsulates the Related Image
  /// </summary>
  public class RelatedImage
  {

    /// <summary>
    /// Image Location
    /// </summary>
    [CanBeNull]
    [DataMember]
    public string ImageUrl { get; set; }

    /// <summary>
    /// MediaType
    /// </summary>
    [CanBeNull]
    [DataMember]
    public MediaType? MediaType { get; set; }

    #region Constructors

    internal RelatedImage()
    {
    }

    #endregion

    /// <summary>
    /// Validate the Content for this Related Image
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);
      vb.ArgumentRequiredCheck("ImageLocation", ImageUrl);
    }
  }
}
