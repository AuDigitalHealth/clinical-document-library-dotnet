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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an identifier
    /// </summary>
    [Serializable]
    [DataContract]
    public class Identifier
    {
        #region Properties

        /// <summary>
        /// The OID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public const string OID = @"[0-2](\.(0|[1-9][0-9]*))*";

        /// <summary>
        /// The UID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public const string UID = @"[0-9a-zA-Z]{8}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{12}";

        #endregion

        /// <summary>
        /// Root, E.g. 1.2.36.174030967.0.5
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Root { get; set; }

        /// <summary>
        /// Extension, E.g. the Medicare number, HPII, HPIO etc
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        /// Assigning authority name, E.g. Australian Medicare
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string AssigningAuthorityName { get; set; }

        /// <summary>
        /// Assigning geographic area
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string AssigningGeographicArea { get; set; }

        /// <summary>
        /// Assigning geographic area
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Code { get; set; }

        /// <summary>
        /// NullFlavour
        /// </summary>
        [CanBeNull]
        [DataMember]
        public NullFlavour? NullFlavour { get; set; }

        /// <summary>
        /// Validates this identifier
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (NullFlavour.HasValue)
            {
                if (!Root.IsNullOrEmptyWhitespace())
                    vb.AddValidationMessage(vb.PathName, null, "Root can not be provided with a NullFlavour within an Identifier");

                if (!Extension.IsNullOrEmptyWhitespace())
                    vb.AddValidationMessage(vb.PathName, null, "Extension can not be provided with a NullFlavour within an Identifier");

                if (!AssigningAuthorityName.IsNullOrEmptyWhitespace())
                    vb.AddValidationMessage(vb.PathName, null, "AssigningAuthorityName can not be provided with a NullFlavour within an Identifier");

                if (!AssigningGeographicArea.IsNullOrEmptyWhitespace())
                    vb.AddValidationMessage(vb.PathName, null, "AssigningGeographicArea can not be provided with a NullFlavour within an Identifier");

                if (Code != null)
                    vb.AddValidationMessage(vb.PathName, null, "Code can not be provided with a NullFlavour within an Identifier");

                if (NullFlavour.Value != CDA.Common.Enums.NullFlavour.Unknown)
                    vb.AddValidationMessage(vb.PathName, null, "NullFlavour for Identifier can only be specified as Unknown (UNK)");
            }
            else
            {
                if (vb.ArgumentRequiredCheck("Root", Root))
                {
                    // Check that the Root is a valid OID or UUID
                    if (!(System.Text.RegularExpressions.Regex.IsMatch(Root, OID) || System.Text.RegularExpressions.Regex.IsMatch(Root, UID)))
                    {
                        vb.AddValidationMessage(vb.PathName, null, "Identifiers must be a valid OID or UID");
                    }

                    if (Root != null && Root.Contains("urn:uuid:")) vb.AddValidationMessage(vb.PathName, null, "Identifiers must not contain 'urn:uuid:'");
                }
            }
        }

        /// <summary>
        /// This property returns text that is appropriate for the narrative
        /// </summary>
        public string NarrativeText
        {
            get
            {
                var narrative = String.Empty;

                if (AssigningAuthorityName == HealthIdentifierType.HPIO.GetAttributeValue<NameAttribute, string>(x => x.Code) ||
                    AssigningAuthorityName == HealthIdentifierType.IHI.GetAttributeValue<NameAttribute, string>(x => x.Code) ||
                    AssigningAuthorityName == HealthIdentifierType.HPII.GetAttributeValue<NameAttribute, string>(x => x.Code))
                {
                    narrative += !Root.IsNullOrEmptyWhitespace() ? Root + "." : String.Empty;
                }

                narrative += !Extension.IsNullOrEmptyWhitespace() ? Extension + " " : String.Empty;

                narrative = narrative.Trim(new[] { '.' });

                narrative += !AssigningAuthorityName.IsNullOrEmptyWhitespace() ? " (" + AssigningAuthorityName + ") " : String.Empty;

                if (!narrative.IsNullOrEmptyWhitespace() && !AssigningGeographicArea.IsNullOrEmptyWhitespace())
                {
                   narrative += " - " + (!AssigningGeographicArea.IsNullOrEmptyWhitespace() ? AssigningGeographicArea : String.Empty);
                }

                if (narrative.IsNullOrEmptyWhitespace())
                {
                  narrative += Root;
                }

                narrative = narrative.Replace("urn:uuid:", String.Empty);

                return narrative;
            }
        }
    }
}
