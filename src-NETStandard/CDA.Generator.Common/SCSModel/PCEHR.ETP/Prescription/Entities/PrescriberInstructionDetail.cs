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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The PrescriberInstructionDetail class contains all the properties that CDA has identified for 
    /// a prescriber instruction detail
    /// 
    /// Please use the CreatePrescriberInstructionDetail() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Person))]
    public class PrescriberInstructionDetail
    {
        #region Properties

        /// <summary>
        /// The recipient of the prescriber instructions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationPrescriberInstructionRecipient PrescriberInstructionRecipient { get; set; }

        /// <summary>
        /// The date / time that the prescriber instructions were received
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime PrescriberInstructionReceived { get; set; }

        /// <summary>
        /// The prescribers instructions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String PrescriberInstruction { get; set; }

        /// <summary>
        /// The source of the prescriber instructions, E.g. MedicalRecord
        /// </summary>
        [CanBeNull]
        [DataMember]
        public PrescriberInstructionSource PrescriberInstructionSource { get; set; }

        /// <summary>
        /// The communication medium associated with the prescriber instructions, E.g. Phone
        /// </summary>
        [CanBeNull]
        [DataMember]
        public PrescriberInstructionCommunicationMedium PrescriberInstructionCommunicationMedium { get; set; }
        #endregion

        #region Constructors
        internal PrescriberInstructionDetail()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this PrescriberInstructionDetail
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("PrescriberInstructionRecipient", PrescriberInstructionRecipient))
            {
                if (PrescriberInstructionRecipient != null) PrescriberInstructionRecipient.Validate(vb.Path + "PrescriberInstructionRecipient", vb.Messages);
            }

            vb.ArgumentRequiredCheck("PrescriberInstructionReceived", PrescriberInstructionReceived);
            vb.ArgumentRequiredCheck("PrescriberInstruction", PrescriberInstruction);
            vb.ArgumentRequiredCheck("PrescriberInstructionSource", PrescriberInstructionSource);
            vb.ArgumentRequiredCheck("PrescriberInstructionCommunicationMedium", PrescriberInstructionCommunicationMedium);
        }

        #endregion

    }
}
