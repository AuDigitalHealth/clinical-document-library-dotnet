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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System;
using System.Runtime.Serialization;
using Nehta.HL7.CDA;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a Alert class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Alert
    {
        #region Properties

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Alert Type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText AlertType { get; set; }

        /// <summary>
        /// Alert Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText AlertDescription { get; set; }

        #endregion

        #region Constructors
        internal Alert()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Alert
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("AlertType", AlertType))
            {
                if (AlertType != null) AlertType.ValidateMandatory(vb.Path + "AlertType", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("AlertDescription", AlertDescription))
            {
              if (AlertDescription != null) AlertDescription.ValidateMandatory(vb.Path + "AlertDescription", vb.Messages);
            }
        }

        #endregion
    }
}