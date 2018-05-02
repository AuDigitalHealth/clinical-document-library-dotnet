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
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
  /// <summary>
  /// This class models a Author - AssignedAuthoringDevice.
  /// </summary>
  public class AuthorAuthoringDevice : IAuthorCollection
  {
    /// <summary>
    /// Date Time Authored
    /// </summary>
    [CanBeNull]
    public ISO8601DateTime DateTimeAuthored { get; set; }

    /// <summary>
    /// Participation role.
    /// </summary>
    [CanBeNull]
    public ICodableText Role { get; set; }

    /// <summary>
    /// Identifiers
    /// </summary>
    [CanBeNull]
    public List<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Software Name
    /// </summary>
    [CanBeNull]
    public string SoftwareName { get; set; }

    #region Constructors

    internal AuthorAuthoringDevice()
    {

    }

    #endregion

    /// <summary>
    /// Validates this AuthorAuthoringDevice.
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    /// <param name="mandatoryIdentifier">Indicates whether the identifiers are mandatory for this validation </param>
    /// <param name="mandatoryDateTimeAuthored">Indicates whether the DateTimeAuthored is mandatory for this validation </param>
    public void Validate(string path, List<ValidationMessage> messages, bool mandatoryIdentifier, bool mandatoryDateTimeAuthored)
    {
      var vb = new ValidationBuilder(path, messages);

      if (mandatoryDateTimeAuthored)
      {
         vb.ArgumentRequiredCheck("DateTimeAuthored", DateTimeAuthored);
      }

      if (vb.ArgumentRequiredCheck("Role", Role))
      {
        if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
      }

      if (mandatoryIdentifier)
      {
        vb.ArgumentRequiredCheck("Identifiers", Identifiers);
      }

      if (Identifiers != null)
      {
        for (var x = 0; x < Identifiers.Count; x++)
        {
          Identifiers[x].Validate(vb.Path + string.Format("Identifier[{0}]", x), vb.Messages);
        }        

        // Check for PAI-D
        if (!Identifiers.Select(identifiers => identifiers.AssigningAuthorityName).Contains(HealthIdentifierType.PAID.GetAttributeValue<NameAttribute, string>(x => x.Code)))
        {
            vb.AddValidationMessage(vb.PathName, null, "At least one PAI-D Required");
        }
      }

      vb.ArgumentRequiredCheck("SoftwareName", SoftwareName);
    }
  }
}
