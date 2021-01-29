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
    public class StructuredText 
    {
        #region Properties
        /// <summary>
        /// Text
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the null flavour
        /// </summary>
        /// <value>
        /// The null flavour.
        /// </value>
        [CanBeNull]
        [DataMember]
        public NullFlavour? NullFlavour { get; set; }

        /// <summary>
        /// This property returns the display name if it set, otherwise it defaults to the original text property
        /// </summary>
        public string NarrativeText
        {
            get
            {
              string returnString = null;

              if (!Text.IsNullOrEmptyWhitespace())
              {
                 returnString = Text;
              }
              else if (NullFlavour.HasValue)
              {
                  returnString = NullFlavour.Value.GetAttributeValue<NameAttribute, String>(x => x.Name);
              }
              return returnString;
            }
        }
        #endregion

        #region Constructors
        internal StructuredText()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this StructuredText text (field is Mandatory)
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
           var vb = new ValidationBuilder(path, messages);

           // Directions can only contain a Directions or a NullFlavour 
           var choiceCheck = new Dictionary<string, object>()
            {
                { "Text", Text },
                { "NullFlavour", NullFlavour }
            };
           vb.ChoiceCheck(choiceCheck);

        }

        #endregion
    }
}
