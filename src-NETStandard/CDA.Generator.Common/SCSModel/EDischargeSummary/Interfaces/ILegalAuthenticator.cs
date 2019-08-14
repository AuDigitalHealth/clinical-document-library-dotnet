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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces
{
    /// <summary>
    /// The ILegalAuthenticator interface encapsulates all the properties that CDA requires for a participant
    /// </summary>
    public interface ILegalAuthenticator
        {
            /// <summary>
            /// This identifier is used to associated participants through the document
            /// </summary>
            [CanBeNull]
            Guid UniqueIdentifier { get; set; }

            /// <summary>
            /// Date/Time that the document was authenticated
            /// </summary>
            [CanBeNull]
            [DataMember]
            ISO8601DateTime DateTimeAuthenticated { get; set; }

            /// <summary>
            /// The IPersonIdentifier
            /// </summary>
            [CanBeNull]
            [DataMember]
            IPerson  Person  { get; set; }

            /// <summary>
            /// The organisation name
            /// </summary>
            [CanBeNull]
            [DataMember]
            IOrganisationName Organisation { get; set; }

            /// <summary>
            /// The address of the Authenticator
            /// </summary>
            [CanBeNull]
            [DataMember]
            List<IAddress> Addresses { get; set; }

            /// <summary>
            /// A list of electronic communication detail objects, E.g. Telephone numbers, Email addresses etc
            /// </summary>
            [CanBeNull]
            [DataMember]
            List<ElectronicCommunicationDetail> ElectronicCommunicationDetails { get; set; }

        /// <summary>
        /// Validates this Legal Authenticator Participant
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
