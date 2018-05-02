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
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.CDAModel
{
    /// <summary>
    /// This interface encapsulates all the CDA specific context for an PathologyResultReport
    /// </summary>
    public interface ICDAContextPathologyReportWithStructuredContent
    {
        #region Properties

        /// <summary>
        /// The version for this CDA document
        /// </summary>
        String Version { get; set; }

        /// <summary>
        /// The CDA document Identifier
        /// </summary>
        Identifier DocumentId { get; set; }

        /// <summary>
        /// The CDA set Identifier
        /// </summary>
        Identifier SetId { get; set; }

        /// <summary>
        /// The legal authenticator of the CDA document
        /// </summary>
        IParticipationLegalAuthenticator LegalAuthenticator { get; set; }

        /// <summary>
        /// The custodian for this CDA document
        /// </summary>
        IParticipationCustodian Custodian { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Validate the CDA Context for this PathologyReportWithStructuredContent
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        #endregion
    }
}
