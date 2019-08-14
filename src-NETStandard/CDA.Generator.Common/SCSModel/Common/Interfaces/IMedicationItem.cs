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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
  /// The IMedicationItem interface encapsulates medication information that is used to
    /// populate medication related sections / information with CDA documents
    /// </summary>
    public interface IMedicationItem
    {
        /// <summary>
        /// The medicine as a codableText
        /// </summary>
        [CanBeNull]
        ICodableText Medicine { get; set; }

        /// <summary>
        /// The directions of use for this medication
        /// </summary>
        [CanBeNull]
        StructuredText Directions { get; set; }

        /// <summary>
        /// The clinical indication
        /// </summary>
        [CanBeNull]
        String ClinicalIndication { get; set; }

        /// <summary>
        /// Comments associated with this medication
        /// </summary>
        [CanBeNull]
        String Comment { get; set; }

        /// <summary>
        /// Change type
        /// </summary>
        [CanBeNull]
        ICodableText ChangeType { get; set; }

        /// <summary>
        /// Recommendation or change
        /// NOTE: Replaces RecommendationOrChange for SL/ER/DIS
        /// </summary>
        [CanBeNull]
        ICodableText ChangeStatus { get; set; }

        /// <summary>
        /// Change description
        /// </summary>
        [CanBeNull]
        String ChangeDescription { get; set; }

        /// <summary>
        /// Change reason
        /// </summary>
        [CanBeNull]
        StructuredText ChangeReason { get; set; }
        
        /// <summary>
        /// Validates this Medication Item object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
