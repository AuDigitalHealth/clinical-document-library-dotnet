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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.Interfaces
{
  /// <summary>
  /// The IParticipationAuthorPerson interface contains all the properties that CDA has identified for a Participation Author Non Healthcare Provider
  /// </summary>
  public interface IParticipationAuthorPerson : IParticipationDocumentAuthor, IAuthorCollection
  {
    /// <summary>
    /// The author person
    /// </summary>
    [CanBeNull]
    new IAuthorPerson Participant { get; set; }

    /// <summary>
    /// Validates this Participation Author Person
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    new void Validate(string path, List<ValidationMessage> messages);
  }
}
