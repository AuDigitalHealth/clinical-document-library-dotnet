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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// An Australian address
    /// </summary>
    [Serializable]
    [DataContract]
    public class AustralianAddress
    {
        #region Properties
        /// <summary>
        /// The address line as a collection of free text strings
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<String> UnstructuredAddressLines { get; set; }

        /// <summary>
        /// Unit type, E.g. Apartment
        /// </summary>
        [CanBeNull]
        [DataMember]
        public UnitType UnitType { get; set; }

        /// <summary>
        /// Unit number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String UnitNumber { get; set; }

        /// <summary>
        /// Address site name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AddressSiteName { get; set; }

        /// <summary>
        /// Level type, E.g. LowerGround or level 2
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AustralianAddressLevelType LevelType { get; set; }

        /// <summary>
        /// Level number, only applicable if the level type is set to "Level"
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String LevelNumber { get; set; }

        /// <summary>
        /// Street number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? StreetNumber { get; set; }

        /// <summary>
        /// Lot number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String LotNumber { get; set; }

        /// <summary>
        /// Street name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String StreetName { get; set; }

        /// <summary>
        /// Street type, E.g. Avenue
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StreetType StreetType { get; set; }

        /// <summary>
        /// Street suffix, E.g. SouthWest
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StreetSuffix StreetSuffix { get; set; }

        /// <summary>
        /// Postal delivery type, E.g. CommunityMailBag
        /// </summary>
        [CanBeNull]
        [DataMember]
        public PostalDeliveryType PostalDeliveryType { get; set; }

        /// <summary>
        /// Postal delivery number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String PostalDeliveryNumber { get; set; }

        /// <summary>
        /// Suburb, town or locality
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String SuburbTownLocality { get; set; }

        /// <summary>
        /// Australian state, E.g. NSW
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AustralianState State { get; set; }

        /// <summary>
        /// Post code
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String PostCode { get; set; }

        /// <summary>
        /// Delivery point ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? DeliveryPointId { get; set; }
        #endregion

        #region Constructors
        internal AustralianAddress()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this AustralianAddress
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {

        }
        #endregion
    }
}
