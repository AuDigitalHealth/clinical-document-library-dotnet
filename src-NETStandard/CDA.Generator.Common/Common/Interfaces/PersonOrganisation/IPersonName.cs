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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA
{
    /// <summary>
    /// This interface defines and constrains the person object down to those properties that are applicable
    /// for a person name.
    /// </summary>
    public interface IPersonName
    {
        /// <summary>
        /// Titles
        /// </summary>
        [CanBeNull]
        List<string> Titles { get; set; }

        /// <summary>
        /// Given name
        /// </summary>
        [CanBeNull]
        List<String> GivenNames { get; set; }

        /// <summary>
        /// Family name
        /// </summary>
        [CanBeNull]
        String FamilyName { get; set; }

        /// <summary>
        /// Name suffix
        /// </summary>
        [CanBeNull]
        List<string> NameSuffix { get; set; }

        /// <summary>
        /// Name usage
        /// </summary>
        [CanBeNull]
        List<NameUsage> NameUsages { get; set; }

        /// <summary>
        /// Validates this person as a person name object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
