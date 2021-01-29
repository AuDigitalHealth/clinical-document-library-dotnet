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
using System.Collections.Generic;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA
{
    /// <summary>
    /// This interface defines a IOrganisation
    /// </summary>
    public interface IOrganisation
    {
        #region Properties

        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Organisation name
        /// </summary>
        [CanBeNull]
        String Name { get; set; }

        /// <summary>
        /// The department of interest, within the organization 
        /// </summary>
        [CanBeNull]
        String Department { get; set; }

        /// <summary>
        /// The name usage for this organization, E.g. Legal, External etc
        /// </summary>
        [CanBeNull]
        OrganisationNameUsage? NameUsage { get; set; }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this IOrganisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
        #endregion
    }
}
