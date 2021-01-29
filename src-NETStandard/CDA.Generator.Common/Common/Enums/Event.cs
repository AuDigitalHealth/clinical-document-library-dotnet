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

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Event Type
    /// </summary>
    public enum EventTypes
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        Undefined,

        /// <summary>
        /// Event
        /// </summary>
        [Name(Code = "EVN", Name = "Event")]
        Event,

        /// <summary>
        /// Intent
        /// </summary>
        [Name(Code = "INT", Name = "Intent")]
        Intent,

        /// <summary>
        /// Appointment
        /// </summary>
        [Name(Code = "APT", Name = "Appointment")]
        Appointment,

        /// <summary>
        /// AppointmentRequest
        /// </summary>
        [Name(Code = "ARQ", Name = "Appointment Request")]
        AppointmentRequest,

        /// <summary>
        /// Promise
        /// </summary>
        [Name(Code = "PRMS", Name = "Promise")]
        Promise,

        /// <summary>
        /// Proposal
        /// </summary>
        [Name(Code = "PRP", Name = "Proposal")]
        Proposal,

        /// <summary>
        /// Request
        /// </summary>
        [Name(Code = "RQO", Name = "Request")]
        Request,

        /// <summary>
        /// Definition
        /// </summary>
        [Name(Code = "DEF", Name = "Definition")]
        Definition
    }
}
