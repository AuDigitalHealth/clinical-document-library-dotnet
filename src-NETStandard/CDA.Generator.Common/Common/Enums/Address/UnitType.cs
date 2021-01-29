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
    /// Unit Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum UnitType
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
        /// Apartment
        /// </summary>
        [EnumMember]
		[Name(Code = "Apt", Name = "Apartment")]
        Apartment,

        /// <summary>
        /// Cottage
        /// </summary>
        [EnumMember]
		[Name(Code = "Ctge", Name = "Cottage")]
        Cottage,

        /// <summary>
        /// Duplex
        /// </summary>
        [EnumMember]
		[Name(Code = "Dup", Name = "Duplex")]
        Duplex,

        /// <summary>
        /// Flat
        /// </summary>
        [EnumMember]
		[Name(Code = "F", Name = "Flat")]
        Flat,

        /// <summary>
        /// Factory
        /// </summary>
        [EnumMember]
		[Name(Code = "Fy", Name = "Factory")]
        Factory,

        /// <summary>
        /// House
        /// </summary>
        [EnumMember]
		[Name(Code = "Hse", Name = "House")]
        House,

        /// <summary>
        /// Kiosk
        /// </summary>
        [EnumMember]
		[Name(Code = "Ksk", Name = "Kiosk")]
        Kiosk,

        /// <summary>
        /// Marine Berth
        /// </summary>
        [EnumMember]
		[Name(Code = "Mb", Name = "Marine Berth")]
        MarineBerth,

        /// <summary>
        /// Off Maisonette Office
        /// </summary>
        [EnumMember]
		[Name(Code = "Msnt", Name = "Off Maisonette Office")]
        MaisonetteOffice,

        /// <summary>
        /// Penthouse
        /// </summary>
        [EnumMember]
		[Name(Code = "Pths", Name = "Penthouse")]
        Penthouse,

        /// <summary>
        /// Room
        /// </summary>
        [EnumMember]
		[Name(Code = "Rm", Name = "Room")]
        Room,

        /// <summary>
        /// Suite
        /// </summary>
        [EnumMember]
		[Name(Code = "Se", Name = "Suite")]
        Suite,

        /// <summary>
        /// Shed
        /// </summary>
        [EnumMember]
		[Name(Code = "Shed", Name = "Shed")]
        Shed,

        /// <summary>
        /// Shop
        /// </summary>
        [EnumMember]
		[Name(Code = "Shop", Name = "Shop")]
        Shop,

        /// <summary>
        /// Site
        /// </summary>
        [EnumMember]
		[Name(Code = "Site", Name = "Site")]
        Site,

        /// <summary>
        /// Stall
        /// </summary>
        [EnumMember]
		[Name(Code = "Sl", Name = "Stall")]
        Stall,

        /// <summary>
        /// Studio
        /// </summary>
        [EnumMember]
		[Name(Code = "Stu", Name = "Studio")]
        Studio
    }
}
