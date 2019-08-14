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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces
{
    /// <summary>
    /// This interface encapsulates all the CDA specific context for a Current Services
    /// </summary>
    public interface ICurrentService
    {
        #region Properties

        /// <summary>
        /// Service Category
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText ServiceCategory { get; set; }

        /// <summary>
        /// Service Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText ServiceDescription { get; set; }

        /// <summary>
        /// Service Booking Status
        /// </summary>
        [DataMember]
        EventTypes ServiceBookingStatus { get; set; }

        /// <summary>
        /// Service Comment
        /// </summary>
        [CanBeNull]
        [DataMember]
        string ServiceComment { get; set; }

        /// <summary>
        ///  The Service Provider
        /// </summary>
        [CanBeNull]
        [DataMember]
        IParticipationPersonOrOrganisation ServiceProvider { get; set; }

        /// <summary>
        /// Requested Service DateTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        ISO8601DateTime RequestedServiceDateTime { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates this CDA Context for this Current Service
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        #endregion
    }
}
