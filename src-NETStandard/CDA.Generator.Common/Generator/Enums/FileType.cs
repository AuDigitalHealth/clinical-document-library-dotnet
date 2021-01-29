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

namespace Nehta.VendorLibrary.CDA.Generator.Enums
{
    /// <summary>
    /// ExternalData Type
    /// </summary>
    public enum FileStorageType
    {
        /// <summary>
        /// The undefined enumeration is used to determine if a value has been explicity set.
        /// </summary>
        Undefined,

        /// <summary>
        /// The file is to be embeded within the CDA document
        /// </summary>
        Embed,

        /// <summary>
        /// The file is to be referenced from within the CDA document
        /// </summary>
        Reference
    }
}
