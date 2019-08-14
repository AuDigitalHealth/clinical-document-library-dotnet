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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an entitlement
    /// </summary>
    [Serializable]
    [DataContract]
    public class Entitlement
    {
        #region Properties

        /// <summary>
        /// Entitlement ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier Id { get; set; }

        /// <summary>
        /// Entitlement type, E.g. PensionerConcession
        /// </summary>
        [DataMember]
        public EntitlementType Type { get; set; }

        /// <summary>
        /// Entitlement validity duration
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval ValidityDuration { get; set; }

        #endregion

        #region Constructors
        internal Entitlement()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this entitlement
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Id", Id))
            {
                if (Id != null) Id.Validate(vb.Path + "Id", vb.Messages);
            }

            vb.NoMatchCheck("Type", Type, EntitlementType.Undefined);
        }
        #endregion
    }
}
