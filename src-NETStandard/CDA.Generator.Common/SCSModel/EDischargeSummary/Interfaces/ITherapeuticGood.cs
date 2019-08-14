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
using System;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces
{
    /// <summary>
    /// The ITherapeuticGood interface encapsulates medication information that is used to populate medications 
    /// related sections / information with CDA documents
    /// </summary>
    public interface ITherapeuticGood 
    {
        /// <summary>
        /// Therapeutic Good Identification
        /// </summary>
        [CanBeNull]
        ICodableText TherapeuticGoodIdentification { get; set; }

        /// <summary>
        /// Dose Instruction
        /// </summary>
        [CanBeNull]
        String DoseInstruction { get; set; }

        /// <summary>
        /// Unit of Use Quantity Dispensed
        /// </summary>
        [CanBeNull]
        String UnitOfUseQuantityDispensed { get; set; }

        /// <summary>
        /// Reason for Therapeutic Good
        /// </summary>
        [CanBeNull]
        String ReasonForTherapeuticGood { get; set; }

        /// <summary>
        /// Additional Comments
        /// </summary>
        [CanBeNull]
        String AdditionalComments { get; set; }

        /// <summary>
        /// Medication History
        /// </summary>
        [CanBeNull]
        IMedicationHistory MedicationHistory { get; set; }

        /// <summary>
        /// Validates this ITherapeuticGood object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
