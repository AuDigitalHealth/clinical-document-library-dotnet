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
    /// NCTIS Global Statement Values
    /// </summary>
    [Serializable]
    [DataContract]
    public enum NCTISGlobalStatementValues
    {
        /// <summary>
        /// None known
        /// </summary>
        [EnumMember]
        [Name(Code = "01", Name = "None known", CodeSystem = "NCTISGlobalStatementValues")]
        NoneKnown,

        /// <summary>
        /// Not asked
        /// </summary>
        [EnumMember]
        [Name(Code = "02", Name = "Not asked", CodeSystem = "NCTISGlobalStatementValues")]
        NotAsked,

        /// <summary>
        /// None Supplied
        /// </summary>
        [EnumMember]
        [Name(Code = "03", Name = "None Supplied", CodeSystem = "NCTISGlobalStatementValues")]
        NoneSupplied
    }
}
