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
using JetBrains.Annotations;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that pertain to a Protocol
    /// </summary>
    [Serializable]
    [DataContract]
    public class Protocol
    {
        #region Properties
        /// <summary>
        /// Study Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier StudyIdentifier { get; set; }

        /// <summary>
        /// Imaging Request Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier ImagingRequestIdentifier { get; set; }

        #endregion

        #region Constructors
        internal Protocol()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Protocol
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (StudyIdentifier != null)
            {
                StudyIdentifier.Validate(vb.Path + ".StudyIdentifier", vb.Messages);
            }

            if (ImagingRequestIdentifier != null)
            {
                ImagingRequestIdentifier.Validate(vb.Path + ".ImagingRequestIdentifier", vb.Messages);
            }
        }
        #endregion

    }
}
