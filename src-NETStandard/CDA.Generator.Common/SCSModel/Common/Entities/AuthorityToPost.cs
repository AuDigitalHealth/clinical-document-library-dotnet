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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
  /// <summary>
  /// This class encapsulates the Authority To Post
  /// </summary>
  public class AuthorityToPost
  {
    /// <summary>
    /// Provide a custom Narrative 
    /// </summary>
    [CanBeNull]
    [DataMember]
    public bool? ExcludeNarrative { get; set; }

    /// <summary>
    /// Provide a custom Narrative 
    /// </summary>
    [CanBeNull]
    [DataMember]
    public StrucDocText CustomNarrative { get; set; }

    /// <summary>
    /// Pathology Report Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier ReportIdentifier { get; set; }

    /// <summary>
    /// Service Request Identifier
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Identifier ServiceRequestIdentifier { get; set; }

    /// <summary>
    /// Service Request Identifier Code
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText ServiceRequestIdentifierCode { get; set; }

    #region Participants

    /// <summary>
    /// A Participation Authoriser
    /// </summary>
    [CanBeNull]
    [DataMember]
    public IParticipationAuthoriser Authoriser { get; set; }

    /// <summary>
    /// A Participation Authorisee
    /// </summary>
    [CanBeNull]
    [DataMember]
    public IParticipationAuthorisee Authorisee { get; set; }

    /// <summary>
    /// A Participation Repository
    /// </summary>
    [CanBeNull]
    [DataMember]
    public Repository Repository { get; set; }

    #endregion

    #region Constructors
    internal AuthorityToPost()
    {
            
    }
    #endregion

    /// <summary>
    /// Validate the Content for this Requested Service
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

        if (vb.ArgumentRequiredCheck("ReportIdentifier", ReportIdentifier))
        {
            ReportIdentifier.Validate(path + "ReportIdentifier", messages);
        }

        if (ServiceRequestIdentifier != null)
        {
            ServiceRequestIdentifier.Validate(path + "ServiceRequestIdentifier", messages);
        }

        if (ServiceRequestIdentifierCode != null)
        {
            ServiceRequestIdentifierCode.Validate(path + "ServiceRequestIdentifierCode", messages);
        }

        if ((ServiceRequestIdentifier != null && ServiceRequestIdentifierCode == null) && (ServiceRequestIdentifier == null && ServiceRequestIdentifierCode != null))
        {
            vb.AddValidationMessage(vb.Path + "ServiceRequestIdentifier & ServiceRequestIdentifierCode", null, "ServiceRequestIdentifier & ServiceRequestIdentifierCode must be provided together and represent the item (ServiceRequestIdentifier)");
        }

        if (vb.ArgumentRequiredCheck("Authoriser", Authoriser))
        {
            Authoriser.Validate(vb.Path + "Authoriser", vb.Messages);
        }

        if (vb.ArgumentRequiredCheck("Authorisee", Authorisee))
        {
            Authorisee.Validate(vb.Path + "Authorisee", vb.Messages);
        }

        if (vb.ArgumentRequiredCheck("Repository", Authorisee))
        {
            Repository.Validate(vb.Path + "Repository", vb.Messages);
        }
    }
  }
}
