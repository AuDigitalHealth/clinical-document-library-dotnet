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

using System.Collections.Generic;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface defines and constrains the person object down to those properties that are applicable for a IPersonConsumer
    /// </summary>
    public interface IPersonConsumer : IPersonWithOrganisation
    {
        /// <summary>
        /// Date of birth
        /// </summary>
        [CanBeNull]
        ISO8601DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [CanBeNull]
        Gender? Gender { get; set; }

        /// <summary>
        /// Validates this Person Consumer  
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        new void Validate(string path, List<ValidationMessage> messages);
    }
}
