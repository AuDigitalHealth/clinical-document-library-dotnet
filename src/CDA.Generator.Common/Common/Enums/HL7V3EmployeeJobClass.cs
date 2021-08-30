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
    /// HL7 V3 Employee Job Class
    /// </summary>
    [Serializable]
    [DataContract]
    public enum Hl7V3EmployeeJobClass
    {
        /// <summary>
        /// Employment in which the employee is expected to work at least a standard work week (defined by the US Bureau of Labor Statistics as 35-44 hours per week)
        /// </summary>
        [EnumMember]
        [Name(Name = "full-time", Code = "FT", CodeSystem = "HL7EmployeeJobClass")]
        FullTime,

        /// <summary>
        /// Employment in which the employee is expected to work less than a standard work week (defined by the US Bureau of Labor Statistics as 35-44 hours per week)
        /// </summary>
        [EnumMember]
        [Name(Name = "part-time", Code = "PT", CodeSystem = "HL7EmployeeJobClass")]
        PartTime,
    }
}
