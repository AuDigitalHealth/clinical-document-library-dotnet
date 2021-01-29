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
    /// Mode of separation values.
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ModeOfSeparation
    {
        /// <summary>
        /// Discharge To Another Acute Hospital
        /// </summary>
        [EnumMember]
        [Name(Code = "1", Name = "Other Hospital", Comment = "Discharge/transfer to (an)other acute hospital" )]
        OtherHospital,

        /// <summary>
        /// Discharge To Aged Care Service
        /// </summary>
        [EnumMember]
        [Name(Code = "2", Name = "Aged Care Service", Comment = "Discharge/transfer to a residential aged care service, unless this is the usual place of residence")]
        AgedCareService,

        /// <summary>
        /// Discharge To Another Psychiatric Hospital
        /// </summary>
        [EnumMember]
        [Name(Code = "3", Name = "Psychiatric Care", Comment = "Discharge/transfer to (an)other psychiatric hospital")]
        PsychiatricCare,

        /// <summary>
        /// Discharge To Other Health Care Accommodation
        /// </summary>
        [EnumMember]
        [Name(Code = "4", Name = "Health Service", Comment = "Discharge/transfer to other health care accommodation (includes mothercraft hospitals)")]
        HealthService,

        /// <summary>
        /// Statistical Discharge
        /// </summary>
        [EnumMember]
        [Name(Code = "5", Name = "Administrative Discharge", Comment = "Statistical discharge - type change")]
        AdministrativeDischarge,

        /// <summary>
        /// Left Against Medical Advice
        /// </summary>
        [EnumMember]
        [Name(Code = "6", Name = "Self-discharged", Comment = "Left against medical advice/discharge at own risk")]
        SelfDischarged,

        /// <summary>
        /// Statistical Discharge From Leave
        /// </summary>
        [EnumMember]
        [Name(Code = "7", Name = "Administrative from Leave", Comment = "Statistical discharge from leave")]
        AdministrativeFromLeave,

        /// <summary>
        /// Died
        /// </summary>
        [EnumMember]
        [Name(Code = "8", Name = "Deceased", Comment = "Died")]
        Deceased,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember]
        [Name(Code = "9", Comment = "Other (includes discharge to usual residence, own accommodation/welfare institution (includes prisons, hostels and group homes providing primarily welfare services))", Name = "Other/Home")]
        OtherHome
    }
}
