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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Nehta.VendorLibrary.CDA
{
    /// <summary>
    /// This interface defines the properties that are common to all addresses.
    /// </summary>
    public interface IBaseAddress
    {
        /// <summary>
        /// An enumeration representing a null flavour for the No Fixed Address field
        /// </summary>
        [CanBeNull]
        AddressAbsentIndicator? AddressAbsentIndicator { get; set; }

        /// <summary>
        /// Address purpose
        /// </summary>
        [CanBeNull]
        AddressPurpose AddressPurpose { get; set; }
    }
}
