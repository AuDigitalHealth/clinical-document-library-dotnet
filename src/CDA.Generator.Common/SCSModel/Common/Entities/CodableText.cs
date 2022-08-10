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
using System.Runtime.Serialization;
using System.Collections.Generic;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document conform to a coded text
    /// 
    /// This any entry that may contain a code and an associated coding system.
    /// 
    /// This class doesn't enforce the use of a code and allows for a free text, OptionalText property
    /// that can be used in the absence of a code and coding system.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class CodableText : ICodableText, ICodableTranslation
    {
        #region Properties
        /// <summary>
        /// Code
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <value>
        /// CodeSystem
        /// </value>
        public CodingSystem CodeSystem 
        { 
            set
            {
                CodeSystemCode = value.GetAttributeValue<NameAttribute, String>(x => x.Code);
                CodeSystemName = value.GetAttributeValue<NameAttribute, String>(x => x.Name);
                CodeSystemVersion = value.GetAttributeValue<NameAttribute, String>(x => x.Version);
            }
        }

        /// <summary>
        /// Gets or sets the null flavour
        /// </summary>
        /// <value>
        /// The null flavour.
        /// </value>
        [DataMember]
        public NullFlavour? NullFlavour { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has code system.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has code system; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasCodeSystem
        {
            get
            {
                if (CodeSystemCode == null)
                    CodeSystemCode = string.Empty;

                if (CodeSystemName == null)
                    CodeSystemName = string.Empty;

                if (!CodeSystemCode.IsNullOrEmptyWhitespace())
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets or sets the name of the code system.
        /// </summary>
        /// <value>
        /// The name of the code system.
        /// </value>
        [DataMember]
        public string CodeSystemName { get; set; }

        /// <summary>
        /// Gets or sets the code system code.
        /// </summary>
        /// <value>
        /// The code system code.
        /// </value>
        [DataMember]
        public string CodeSystemCode { get; set; }

        /// <summary>
        /// Gets or sets the code system version.
        /// </summary>
        /// <value>
        /// The code system version.
        /// </value>
        [DataMember]
        public string CodeSystemVersion { get; set; }

        /// <summary>
        /// The display name associated with the code
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// A free text field that can be used in the absence of a code, code system and display name, 
        /// alternately it can be used to provide additional information
        /// </summary>
        [DataMember]
        public string OriginalText { get; set; }

        /// <summary>
        /// A list of possible translations associated with this codeable text
        /// </summary>
        [DataMember]
        public List<ICodableTranslation> Translations { get; set; }

        /// <summary>
        /// A list of qualifier codes
        /// </summary>
        [DataMember]
        public List<QualifierCode> QualifierCodes { get; set; }

        /// <summary>
        /// This property returns the display name if it set, otherwise it defaults to the original text property
        /// </summary>
        public string NarrativeText
        {
            get
            {
              var returnString = !OriginalText.IsNullOrEmptyWhitespace() ? OriginalText : !DisplayName.IsNullOrEmptyWhitespace() ? DisplayName : string.Empty;
              if (NullFlavour.HasValue && OriginalText.IsNullOrEmptyWhitespace())
              {
                  returnString = NullFlavour.Value.GetAttributeValue<NameAttribute, string>(x => x.Name);
              }

              return returnString;
            }
        }

        #endregion

        #region Constructors

        internal CodableText()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this codable text (field is Mandatory)
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void ValidateMandatory(string path, List<ValidationMessage> messages)
        {
           var vb = new ValidationBuilder(path, messages);

           if (NullFlavour.HasValue)
           {
               //vb.AddValidationMessage(vb.PathName + "NullFlavour", null, "NullFlavour can not be specified for this coadable text field");
               return;
           }

            if (QualifierCodes != null)
            {
                foreach (var qualifierCode in QualifierCodes)
                {
                    qualifierCode.Validate(path, messages);
                }
            }

            if (QualifierCodes != null && OriginalText.IsNullOrEmptyWhitespace())
            {
                vb.AddValidationMessage(vb.PathName + "QualifierCodes & OriginalText", null, "OriginalText must be provided with QualifierCodes");
            }

            Validate(path, messages);           
        }

        /// <summary>
        /// Validates this codable text
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (!HasCodeSystem && !Code.IsNullOrEmptyWhitespace())
            {
                vb.AddValidationMessage(vb.PathName, null, "Code can only be provided if a CodeSystem is specified");
            }

            if (!HasCodeSystem && !DisplayName.IsNullOrEmptyWhitespace())
            {
                vb.AddValidationMessage(vb.PathName, null, "DisplayName can only be provided if a CodeSystem is specified");
            }

            if (DisplayName.IsNullOrEmptyWhitespace() && OriginalText.IsNullOrEmptyWhitespace() && NullFlavour == null)
            {
                vb.AddValidationMessage(vb.PathName, null, "Either OriginalText or DisplayName or a NullFlavour must be provided");
            }

            if (Translations != null)
            {
                foreach (var translation in Translations)
                {
                    translation.Validate(path, messages);
                }
            }

            if (QualifierCodes != null)
            {
                foreach (var qualifierCode in QualifierCodes)
                {
                    qualifierCode.Validate(path, messages);
                }
            }

            if (QualifierCodes != null && OriginalText.IsNullOrEmptyWhitespace())
            {
                vb.AddValidationMessage(vb.PathName + "QualifierCodes & OriginalText", null, "OriginalText must be provided with QualifierCodes");
            }
        }

        #endregion
    }
}
