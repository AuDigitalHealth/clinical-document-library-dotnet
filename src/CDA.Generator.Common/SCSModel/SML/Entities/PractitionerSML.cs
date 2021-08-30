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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class PractitionerSML
    {
        /// <summary>
        /// The involvement of the practitioner.
        /// </summary>
        [DataMember]
        public ICodableText Role { get; set; }

        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Identifier> Identifiers { get; set; }
        
        /// <summary>
        /// Person Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IPersonName> PersonNames { get; set; }

        /// <summary>
        /// ElectronicCommunicationDetails
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ElectronicCommunicationDetail> ElectronicCommunicationDetails { get; set; }

        /// <summary>
        /// Addresses
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IAddress> Addresses { get; set; }

        /// <summary>
        /// Qualifications
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Qualifications { get; set; }

        /// <summary>
        /// The languages the practitioner can speak
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<LanguageCommunicationSML> Languages { get; set; }

    }
}