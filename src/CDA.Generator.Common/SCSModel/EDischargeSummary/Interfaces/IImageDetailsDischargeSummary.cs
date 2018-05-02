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

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces
{
    /// <summary>
    /// The IImageDetailsDischargeSummary interface contains all the properties that CDA has identified for a discharge summary image
    /// </summary>
    public interface IImageDetailsDischargeSummary
    {
        /// <summary>
        /// A Category
        /// </summary>
        [CanBeNull]
        ICodableText Category { get; set; }

        /// <summary>
        /// A Test Name
        /// </summary>
        [CanBeNull]
        ICodableText TestName { get; set; }

        /// <summary>
        /// A list of Anatomical Sites
        /// </summary>
        [CanBeNull]
        List<ICodableText> AnatomicalSite { get; set; }

        /// <summary>
        /// A list of Views
        /// </summary>
        [CanBeNull]
        List<View> View { get; set; }

        /// <summary>
        /// Validates this Image Details for a discharge summary
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
