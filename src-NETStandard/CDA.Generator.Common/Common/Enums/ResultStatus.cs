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
    /// Result Status
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ResultStatus
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
        /// Registered [No result yet available]
        /// </summary>
        [EnumMember]
        [Name(Name = "Registered", Code = "1", Comment = "[No result yet available]", CodeSystem = "NCTISResultStatusValues")]
        Registered,

        /// <summary>
        /// Interim [This is an initial or interim result: data may be missing or verification not been performed]
        /// </summary>
        [EnumMember]
        [Name(Name = "Interim", Code = "2", Comment = "[This is an initial or interim result: data may be missing or verification not been performed]", CodeSystem = "NCTISResultStatusValues")]
        Interim,

        /// <summary>
        /// Final [The result is complete and verified by the responsible practitioner]
        /// </summary>
        [EnumMember]
        [Name(Name = "Final", Code = "3", Comment = "[The result is complete and verified by the responsible practitioner]", CodeSystem = "NCTISResultStatusValues")]
        Final,

        /// <summary>
        /// Amended [The result has been modified subsequent to being Final, and is complete and verified by the practitioner]
        /// </summary>
        [EnumMember]
        [Name(Name = "Amended", Code = "4", Comment = "[The result has been modified subsequent to being Final, and is complete and verified by the practitioner]", CodeSystem = "NCTISResultStatusValues")]
        Amended,

        /// <summary>
        /// Cancelled / Aborted [[The result is not available because the examination was not started or completed]
        /// </summary>
        [EnumMember]
        [Name(Name = "Cancelled / Aborted", Code = "5", Comment = "[The result is not available because the examination was not started or completed]", CodeSystem = "NCTISResultStatusValues")]
        CancelledOrAborted,
    }
}
