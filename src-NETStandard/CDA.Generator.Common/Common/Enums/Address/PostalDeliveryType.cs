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

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Postal Delivery Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PostalDeliveryType
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        [EnumMember]
        Undefined,

        /// <summary>
        /// Care-Of Post Office Also Known As Poste Restante)
        /// </summary>
        [EnumMember]
		[Name(Code = "CarePo", Name = "Care-Of Post Office Also Known As Poste Restante)")]
        CareOfPostOfficeOrPosteRestante,

        /// <summary>
        /// Community Mail Agent
        /// </summary>
        [EnumMember]
        [Name(Code = "CMA", Name = "Community Mail Agent")]
        CommunityMailAgent,

        /// <summary>
        /// Community Mail Bag
        /// </summary>
        [EnumMember]
        [Name(Code = "CMB", Name = "Community Mail Bag")]
        CommunityMailBag,

        /// <summary>
        /// General Post Office Box
        /// </summary>
        [EnumMember]
        [Name(Code = "GPO", Name = "General Post Office Box")]
        GeneralPostOfficeBox,

        /// <summary>
        /// Locked Mail Bag Service
        /// </summary>
        [EnumMember]
        [Name(Code = "Locked Bag", Name = "Locked Mail Bag Service")]
        LockedMailBagService,

        /// <summary>
        /// Post Office Box
        /// </summary>
        [EnumMember]
        [Name(Code = "PO Box", Name = "Post Office Box")]
        PostOfficeBox,

        /// <summary>
        /// Mail Service
        /// </summary>
        [EnumMember]
		[Name(Code = "MS", Name = "Mail Service")]
        MailService,

        /// <summary>
        /// Private Mail Bag Service
        /// </summary>
        [EnumMember]
        [Name(Code = "Care PO", Name = "Poste Restante (also known as Care-of Post Office)")]
        CareOfPostOffice,

        /// <summary>
        /// Private Mail Bag Service
        /// </summary>
        [EnumMember]
        [Name(Code = "Private Bag", Name = "Private Mail Bag Service")]
        PrivateMailBagService,

        /// <summary>
        /// Roadside Delivery
        /// </summary>
        [EnumMember]
        [Name(Code = "RSD", Name = "Roadside Delivery")]
        RoadsideDelivery,

        /// <summary>
        /// Roadside Mail Box/Bag
        /// </summary>
        [EnumMember]
        [Name(Code = "RMB", Name = "Roadside Mail Box/Bag")]
        RoadsideMailBoxOrBag,

        /// <summary>
        /// Roadside Mail Service
        /// </summary>
        [EnumMember]
        [Name(Code = "RMS", Name = "Roadside Mail Service")]
        RoadsideMailService
    }
}
