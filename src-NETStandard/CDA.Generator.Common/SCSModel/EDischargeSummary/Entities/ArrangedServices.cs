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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System;
using Nehta.VendorLibrary.CDA.Common.Enums;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a ArrangedServices class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Participation))]
    public class ArrangedServices
    {
        #region Properties

        /// <summary>
        /// Arranged Service Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ArrangedServiceDescription { get; set; }

        /// <summary>
        /// Service Commencement Window
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval ServiceCommencementWindow { get; set; }

        /// <summary>
        /// Service Booking Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public EventTypes Status { get; set; }

        /// <summary>
        /// Service Provider
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationServiceProvider ServiceProvider { get; set; }

        #endregion

        #region Constructors
        internal ArrangedServices()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this ArrangedServices object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ArrangedServiceDescription", ArrangedServiceDescription))
            {
                if (ArrangedServiceDescription != null)
                    ArrangedServiceDescription.ValidateMandatory(vb.Path + "ArrangedServiceDescription", vb.Messages);
            }

            if (ServiceProvider != null)
            {
                ServiceProvider.Validate(vb.Path + "ServiceProvider", vb.Messages);
            }

            vb.ArgumentRequiredCheck("Status", Status);
        }

        #endregion
    }
}