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
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IRelatedDocument interface contains only the name associated with the procedure
    /// </summary>
    public class RelatedDocument
    {
       /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// External Data
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ExternalData ExaminationResultRepresentation { get; set; }

        /// <summary>
        /// Order Details
        /// </summary>
        [CanBeNull]
        public DocumentDetails DocumentDetails { get; set; }

        /// <summary>
        /// Validates this test procedure name
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ExaminationResultRepresentation", ExaminationResultRepresentation))
            {
                ExaminationResultRepresentation.ValidateNoCaption(path + "ExaminationResultRepresentation", messages);
            }

            if (vb.ArgumentRequiredCheck("DocumentProvenance", DocumentDetails))
            {
                DocumentDetails.Validate(path + "DocumentProvenance", messages);
            }
        }
    }
}
