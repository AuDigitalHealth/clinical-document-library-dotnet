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
    /// Healthcare identifier geographic area values.
    /// </summary>
    public enum HealthcareIdentifierGeographicArea
    {
        /// <summary>
        /// National Identifier
        /// </summary>
        [Name(Name = "National Identifier", Code = "National Identifier")]
        NationalIdentifier,

        /// <summary>
        /// State or Territory Identifier
        /// </summary>
        [Name(Name = "State or Territory Identifier", Code = "S")]
        StateOrTerritoryIdentifier,

        /// <summary>
        /// Area/Region/District Identifier
        /// </summary>
        [Name(Name = "Area/Region/District Identifier", Code = "A")]
        AreaRegionDistrictIdentifier,

        /// <summary>
        /// Local Client (Unit Record) Identifier
        /// </summary>
        [Name(Name = "Local Client (Unit Record) Identifier", Code = "L")]
        LocalClientIdentifier
    }
}
