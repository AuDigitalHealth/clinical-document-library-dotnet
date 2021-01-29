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
    /// Role Code And Role Class Codes
    /// </summary>
    [Serializable]
    [DataContract]
    public enum RoleCodeAndRoleClassCodes
    {
        /// <summary>
        /// Agent
        /// </summary>
        [EnumMember]
        [Name(Code = "AGNT", Name = "agent", CodeSystem = "RoleClass")]
        Agent,

        /// <summary>
        /// Care Giver
        /// </summary>
        [EnumMember]
        [Name(Code = "SELF", Name = "Self", CodeSystem = "RoleCode")]
        Self
    }
}








