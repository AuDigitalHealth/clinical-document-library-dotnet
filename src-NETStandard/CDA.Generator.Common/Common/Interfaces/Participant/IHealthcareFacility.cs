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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.Interfaces
{
    /// <summary>
  /// The IHealthcareFacility interface contains all the properties that CDA has identified for a Healthcare Facility
    /// </summary>
  public interface IHealthcareFacility 
    {
      /// <summary>
      /// This identifier is used to associated participants through the document.
      /// </summary>
      [CanBeNull]
      Guid UniqueIdentifier { get; set; }

      /// <summary>
      /// An Address
      /// </summary>
      [CanBeNull]
      List<IAddress> Addresses { get; set; }

      /// <summary>
      /// A list of electronic communication deatils, E.g. Telephone numbers, email addresses etc
      /// </summary>
      [CanBeNull]
      List<ElectronicCommunicationDetail> ElectronicCommunicationDetails { get; set; }

      /// <summary>
      /// The organisation responsible for dispensing the medication
      /// </summary>
      [CanBeNull]
      IOrganisation Organisation { get; set; }

      /// <summary>
      /// Validates this IHealthcareFacility
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      void Validate(string path, List<ValidationMessage> messages);
    }
}
