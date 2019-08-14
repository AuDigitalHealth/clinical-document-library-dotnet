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
    /// This class is designed to encapsulate the properties within a CDA document that make up an Reference Range Details
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReferenceRangeDetails
    {
        #region Properties

        /// <summary>
        /// Reference Range Meaning
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ReferenceRangeMeaning { get; set; }

        /// <summary>
        /// Reference Range
        /// </summary>
        [CanBeNull]
        [DataMember]
        public QuantityRange ReferenceRange { get; set; }

        #endregion

        #region Constructors

        internal ReferenceRangeDetails()
        {
        }
        #endregion

        /// <summary>
        /// This property returns text that is appropriate for the narrative.
        /// </summary>
        public string NarrativeText
        {
          get
          {
            var narrative = String.Empty;

            narrative += ReferenceRangeMeaning.NarrativeText + " - ";
            narrative += ReferenceRange.NarrativeText;

            return narrative;
          }
        }

        #region Validation
        /// <summary>
        /// Validates this ReferenceRangeDetails item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ReferenceRangeMeaning", ReferenceRangeMeaning))
            {
               ReferenceRangeMeaning.Validate(vb.Path + "ReferenceRangeMeaning", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ReferenceRange", ReferenceRange))
            {
               ReferenceRange.Validate(vb.Path + "ReferenceRange", vb.Messages);
            }
        }

        #endregion
    }
}