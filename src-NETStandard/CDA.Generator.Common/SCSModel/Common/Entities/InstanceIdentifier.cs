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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an instance identifier
    /// </summary>
    [Serializable]
    [DataContract]
    public class InstanceIdentifier
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
        /// Extension
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
        /// Validates this identifier
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

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

        /// <summary>
        /// This property returns text that is appropriate for the narrative.
        /// </summary>
        public string NarrativeText
        {
            get
            {
                string narrative = Extension;

                //if (Extension.IsNullOrEmptyWhitespace())
                //    narrative = Root;
                //else
                //    narrative = Root + "." + Extension;

                narrative += !AssigningAuthorityName.IsNullOrEmptyWhitespace() ? " (" + AssigningAuthorityName + ") " : String.Empty;

                return narrative;
            }
        }
    }
}
