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
using CDA.Generator.Common.SCSModel.AdvanceCareInformation.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class encapsulates the Related Information
    /// </summary>
    public class Information : IRelatedInformation, IDocumentDetails
    {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Link Nature
        /// </summary>
        [DataMember]
        public ICodableText LinkNature { get; set; }

        /// <summary>
        /// External Data
        /// </summary>
        [DataMember]
        public ExternalData ExternalData { get; set; }

        /// <summary>
        /// Report Identifier
        /// </summary>
        [DataMember]
        public Identifier ReportIdentifier { get; set; }

        /// <summary>
        /// Report Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ResultStatus? ReportStatus { get; set; }

        /// <summary>
        /// Order Details
        /// </summary>
        [DataMember]
        public DocumentProvenance DocumentProvenance { get; set; }

        /// <summary>
        /// The Section Identifier
        /// </summary>
        [DataMember]
        public InstanceIdentifier SectionIdentifier { get; set; }

        /// <summary>
        /// The Template Id for the Observation Media
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier TemplateId { get; set; }

        /// <summary>
        ///  The Version Number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public int? VersionNumber { get; set; }

        #region Constructors

        internal Information()
        {

        }

        #endregion


        /// <summary>
        /// Validate the Content for this Requested Service
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

           if (vb.ArgumentRequiredCheck("LinkNature", LinkNature))
            {
                LinkNature.Validate(path + "LinkNature", messages);
            }

            if (vb.ArgumentRequiredCheck("ExternalData", ExternalData))
            {
                ExternalData.Validate(path + "ExternalData", messages);
            }

            if (vb.ArgumentRequiredCheck("ReportIdentifier", ReportIdentifier))
            {
                ReportIdentifier.Validate(path + "ReportIdentifier", messages);
            }

            vb.ArgumentRequiredCheck("ReportStatus", ReportStatus);
        }


        /// <summary>
        /// Validate the Content for this Related Document
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IDocumentDetails.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("ExternalData", ExternalData))
            {
                ExternalData.Validate(path + "ExternalData", messages);
            }
            if (vb.ArgumentRequiredCheck("DocumentProvenance", DocumentProvenance))
            {
                DocumentProvenance.Validate(path + "DocumentProvenance", messages);
            }
        }
    }
}
