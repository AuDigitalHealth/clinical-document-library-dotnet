﻿/*
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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IParticipant interface encapsulates all the properties that CDA requires for a participant
    /// </summary>
    public interface IParticipant
    {
        /// <summary>
        /// A list of Australian addresses
        /// </summary>
        [CanBeNull]
        List<IAddress> Addresses { get; set; }

        /// <summary>
        /// A list of electronic communication details, E.g. Telephone numbers, email addresses etc
        /// </summary>
        [CanBeNull]
        List<ElectronicCommunicationDetail> ElectronicCommunicationDetails { get; set; }

        /// <summary>
        /// The person who is the author of the document
        /// </summary>
        [CanBeNull]
        IPersonWithOrganisation Person { get; set; }

        /// <summary>
        /// The organisation associated with the author
        /// </summary>
        [CanBeNull]
        IOrganisation Organisation { get; set; }

        /// <summary>
        /// This identifier is used to associated participants through the document
        /// </summary>
        Guid UniqueIdentifier { get; }

        /// <summary>
        /// A list of entitlements
        /// </summary>
        [CanBeNull]
        List<Entitlement> Entitlements { get; set; }

        /// <summary>
        /// Qualifications
        /// </summary>
        [CanBeNull]
        [DataMember]
        string Qualifications { get; set; }

        /// <summary>
        /// Relationship to Subject of Care
        /// </summary>
        [DataMember]
        ICodableText RelationshipToSubjectOfCare { get; set; }

        /// <summary>
        /// Validates this IParticipant
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
