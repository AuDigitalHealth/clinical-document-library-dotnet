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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IItem interface encapsulate the properties within a CDA document that are common to each 
    /// item entry
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Therapeutic good ID
        /// </summary>
        [CanBeNull]
        [OID(OID = "1.2.36.1.2001.1001.101.104.16115", Identifier = "VD-16115")]
        ICodableText TherapeuticGoodId { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        [CanBeNull]
        [OID(OID = "1.2.36.1.2001.1001.101.103.16272", Identifier = "DE-16272")]
        String Formula { get; set; }

        /// <summary>
        /// The quantity of the therapeutic good
        /// </summary>
        [CanBeNull]
        [OID(OID = "1.2.36.1.2001.1001.101.103.10145", Identifier = "DE-10145")]
        String QuantityOfTherapeuticGood { get; set; }

        /// <summary>
        /// A boolean indicating if brand substitution is allowed
        /// </summary>
        [CanBeNull]
        [OID(OID = "1.2.36.1.2001.1001.101.103.10107", Identifier = "DE-10107")]
        Boolean? BrandSubstituteAllowed { get; set; }

        /// <summary>
        /// The maximum numer of repeats
        /// </summary>
        [CanBeNull]
        [OID(OID = "1.2.36.1.2001.1001.101.103.10169", Identifier = "DE-10169")]
        Int32? MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// Additional comments
        /// </summary>
        [CanBeNull]
        [OID(OID = "1.2.36.1.2001.1001.101.103.16044", Identifier = "DE-16044")]
        String AdditionalComments { get; set; }
    }
}
