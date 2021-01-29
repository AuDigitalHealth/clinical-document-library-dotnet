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

using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.Generator.Enums
{
    /// <summary>
    /// Document Status
    /// </summary>
    public enum DocumentStatus
    {
        /// <summary>
        /// The undefined enumeration is used to determine if a value has been explicity set.
        /// </summary>
        Undefined,

        /// <summary>
        /// Final
        /// </summary>
        [Name(Name = "Final", Code = "F")]
        Final,

        /// <summary>
        /// Interim
        /// </summary>
        [Name(Name = "Interim", Code = "I")]
        Interim,

        /// <summary>
        /// Withdrawn
        /// </summary>
        [Name(Name = "Withdrawn", Code = "W")]
        Withdrawn
    }
}
