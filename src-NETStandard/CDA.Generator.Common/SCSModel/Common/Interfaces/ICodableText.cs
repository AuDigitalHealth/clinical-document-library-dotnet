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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface defines all the properties that make up a codable text
    /// </summary>
    public interface ICodableText
    {
        /// <summary>
        /// Code
        /// </summary>
        [CanBeNull]
        string Code { get; set; }

        /// <summary>
        /// Gets or sets the null flavour.
        /// </summary>
        /// <value>
        /// The null flavour.
        /// </value>
        NullFlavour? NullFlavour { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has code system.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has code system; otherwise, <c>false</c>.
        /// </value>
        bool HasCodeSystem { get; }

        /// <summary>
        /// Gets or sets the name of the code system.
        /// </summary>
        /// <value>
        /// The name of the code system.
        /// </value>
        [CanBeNull]
        string CodeSystemName { get; set; }

        /// <summary>
        /// Gets or sets the code system code.
        /// </summary>
        /// <value>
        /// The code system code.
        /// </value>
        [CanBeNull]
        string CodeSystemCode { get; set; }

        /// <summary>
        /// Gets or sets the code system version.
        /// </summary>
        /// <value>
        /// The code system version.
        /// </value>
        [CanBeNull]
        string CodeSystemVersion { get; set; }

        /// <summary>
        /// The dispaly name associated with the code
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [CanBeNull]
        string DisplayName { get; set; }

        /// <summary>
        /// A free text field that can be used in the absence of a code, code system and display name, 
        /// alternately it can be used to provide addational information
        /// </summary>
        [CanBeNull]
        string OriginalText { get; set; }

        /// <summary>
        /// A list of possible translations associated wtih this codbale text
        /// </summary>
        [CanBeNull]
        List<ICodableTranslation> Translations { get; set; }

        /// <summary>
        /// A List Of Qualifier Codes
        /// </summary>
        [CanBeNull]
        List<QualifierCode> QualifierCodes { get; set; }

        /// <summary>
        /// This property returns the display name if it set, otherwise it defaults to the original text property
        /// </summary>
        [CanBeNull]
        string NarrativeText { get; }

        /// <summary>
        /// Validates this codable text (field is Mandatory)
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void ValidateMandatory(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this codable text
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
