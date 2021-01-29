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
    /// Role Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum RoleType
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
        /// Affiliate
        /// </summary>
        [EnumMember]
        [Name(Code = "AFFL", Name = "Affiliate")]
        Affiliate,

        /// <summary>
        /// Agent
        /// </summary>
        [EnumMember]
        [Name(Code = "AGNT", Name = "Agent")]
        Agent,

        /// <summary>
        /// Assigned Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "ASSIGNED", Name = "Assigned Entity")]
        AssignedEntity,

        /// <summary>
        /// Commissioning Party
        /// </summary>
        [EnumMember]
        [Name(Code = "COMPAR", Name = "Commissioning Party")]
        CommissioningParty,

        /// <summary>
        /// Contact
        /// </summary>
        [EnumMember]
        [Name(Code = "CON", Name = "Contact")]
        Contact,

        /// <summary>
        /// Emergency Contact
        /// </summary>
        [EnumMember]
        [Name(Code = "ECON", Name = "Emergency Contact")]
        EmergencyContact,
  
        /// <summary>
        /// Next of Kin
        /// </summary>
        [EnumMember]
        [Name(Code = "NOK", Name = "Next of Kin")]
        NextOfKin,

        /// <summary>
        /// Signing Authority
        /// </summary>
        [EnumMember]
        [Name(Code = "SGNOFF", Name = "Signing Authority")]
        SigningAuthority,
   
        /// <summary>
        /// Guardian
        /// </summary>
        [EnumMember]
        [Name(Code = "GUARD", Name = "Guardian")]
        Guardian1,

        /// <summary>
        /// Guardian
        /// </summary>
        [EnumMember]
        [Name(Code = "GUAR", Name = "Guardian")]
        Guardian2,
    
        /// <summary>
        /// Citizen
        /// </summary>
        [EnumMember]
        [Name(Code = "CIT", Name = "Citizen")]
        Citizen,

        /// <summary>
        /// Covered Party
        /// </summary>
        [EnumMember]
        [Name(Code = "COVPTY", Name = "Covered Party")]
        CoveredParty,
     
        /// <summary>
        /// Personal Relationship
        /// </summary>
        [EnumMember]
        [Name(Code = "PRS", Name = "Personal Relationship")]
        PersonalRelationship,

        /// <summary>
        /// Care Giver
        /// </summary>
        [EnumMember]
        [Name(Code = "CAREGIVER", Name = "Care Giver")]
        CareGiver,
    }
}








