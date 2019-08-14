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
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IRelatedInformation interface contains only the name assocaited with the procedure
    /// </summary>
    public interface IRelatedInformation
    {
       /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Link Nature
        /// </summary>
        [CanBeNull]
        [DataMember]
        ICodableText LinkNature { get; set; }

        /// <summary>
        /// External Data
        /// </summary>
        [CanBeNull]
        [DataMember]
        ExternalData ExternalData { get; set; }

        /// <summary>
        /// Report Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        Identifier ReportIdentifier { get; set; }

        /// <summary>
        /// Report Status
        /// </summary>
        [DataMember]
        ResultStatus? ReportStatus { get; set; }

        /// <summary>
        /// Validates this test procedure name
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
