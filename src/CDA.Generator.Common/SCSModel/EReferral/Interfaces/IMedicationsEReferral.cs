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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IMedicationsEReferral interface encapsulates medication information that is used to populate medications 
    /// related sections / information with CDA documents
    /// </summary>
    public interface IMedicationsEReferral
    {
        /// <summary>
        /// A list of used medications 
        /// </summary>
        [CanBeNull]
        List<IMedicationInstruction> MedicationsList { get; set; }

        /// <summary>
        /// An exclusion statement
        /// </summary>
        [CanBeNull]
        [OID(Identifier = "DG-16136", OID = "1.2.36.1.2001.1001.101.102.16136")]
        Statement ExclusionStatement { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeMedications { get; set; }

        /// <summary>
        /// Validates this Medications E-Referral object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
