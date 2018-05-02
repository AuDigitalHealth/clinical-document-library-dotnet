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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA
{
    /// <summary>
    /// This interface defines and constrains the person object down to those properties that are applicable
    /// for a dispenser.
    /// </summary>
    public interface IPersonDispenser
    {
        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Person Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IPersonName> PersonNames { get; set; }

        /// <summary>
        /// Qualifications
        /// </summary>
        [CanBeNull]
        String Qualifications { get; set; }

        /// <summary>
        /// Organisation
        /// </summary>
        [CanBeNull]
        [DataMember]
        Occupation? Occupation { get; set; }

        /// <summary>
        /// Validates this IPersonDispenser
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this IPersonDispenser for ATS
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateATS(string path, List<ValidationMessage> messages);

    }
}
