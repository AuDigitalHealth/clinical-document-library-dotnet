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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IImagingResultGroup interface contains all the properties that CDA has identified for an image result group
    /// </summary>
    public interface IImagingResultGroup
    {
        /// <summary>
        /// Result group name
        /// </summary>
        [CanBeNull]
        ICodableText ResultGroupName { get; set; }

        /// <summary>
        /// A list of immaging results
        /// </summary>
        [CanBeNull]
        List<IImagingResult> Results { get; set; }

        /// <summary>
        /// Anatomical Site
        /// </summary>
        [CanBeNull]
        AnatomicalSite AnatomicalSite { get; set; } 

        /// <summary>
        /// Validates this Imaging Result Group
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
