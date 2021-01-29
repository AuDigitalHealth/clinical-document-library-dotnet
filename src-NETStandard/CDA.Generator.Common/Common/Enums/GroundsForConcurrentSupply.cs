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
    /// Grounds For Concurrent Supply
    /// </summary>
    [Serializable]
    [DataContract]
    public enum GroundsForConcurrentSupply
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
        /// Supply is in accord with Regulation 24 of the National Health (Pharmaceutical Benefits) Regualtions 1960
        /// </summary>
        [EnumMember]
		    [Name(Code = "1", Name = @"Pursuant to Regulation 24")]
        PursuantToRegulation24,

        /// <summary>
        /// Supply is in accord with the 'Hardship Conditions' provision of PRBS prescribing guidelines
        /// </summary>
        [EnumMember]
        [Name(Code = "2", Name = "Hardship Conditions Apply")]
        HardshipConditionsApply,

        /// <summary>
        /// There are no ground for concurrent supply
        /// </summary>
        [EnumMember]
        [Name(Code = "9", Name = "No Grounds")]
        NoGrounds
    }
}
