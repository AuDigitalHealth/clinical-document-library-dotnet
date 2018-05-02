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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an TherapeuticGood class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class TherapeuticGood : ITherapeuticGood, ITherapeuticGoodCeased
    {
        #region Properties

        /// <summary>
        /// Current Medication
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TherapeuticGoodIdentification { get; set; }

        /// <summary>
        /// Dose Instruction
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String DoseInstruction { get; set; }

        /// <summary>
        /// Unit of Use Quantity Dispensed
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String UnitOfUseQuantityDispensed { get; set; }

        /// <summary>
        /// Reason for Therapeutic Good
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String ReasonForTherapeuticGood { get; set; }

        /// <summary>
        /// Additional Comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AdditionalComments { get; set; }

        /// <summary>
        /// Medication History
        /// </summary>
        [CanBeNull]
        [DataMember]
        public MedicationHistory MedicationHistory { get; set; }
        IMedicationHistoryCeased ITherapeuticGoodCeased.MedicationHistory { get; set; }
        IMedicationHistory ITherapeuticGood.MedicationHistory { get; set; }

        #endregion

        #region Constructors
        internal TherapeuticGood()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Therapeutic Good object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void ITherapeuticGood.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedTherapeuticGood = (ITherapeuticGood)this;

            if (vb.ArgumentRequiredCheck("TherapeuticGoodIdentification", TherapeuticGoodIdentification))
            {
                if (TherapeuticGoodIdentification != null) TherapeuticGoodIdentification.ValidateMandatory(path + ".TherapeuticGoodIdentification", messages);
            }

            vb.ArgumentRequiredCheck("DoseInstruction", DoseInstruction);

            if (vb.ArgumentRequiredCheck("MedicationHistory", castedTherapeuticGood.MedicationHistory))
            {
                if (castedTherapeuticGood.MedicationHistory != null) castedTherapeuticGood.MedicationHistory.Validate(vb.Path + "MedicationHistory", vb.Messages);
            }

        }

        /// <summary>
        /// Validates this medications object
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void ITherapeuticGoodCeased.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedTherapeuticGood = (ITherapeuticGoodCeased)this;

            if (vb.ArgumentRequiredCheck("TherapeuticGoodIdentification", TherapeuticGoodIdentification))
            {
                if (TherapeuticGoodIdentification != null) TherapeuticGoodIdentification.Validate(path + ".TherapeuticGoodIdentification", messages);
            }

            if (vb.ArgumentRequiredCheck("MedicationHistory", castedTherapeuticGood.MedicationHistory))
            {
                if (castedTherapeuticGood.MedicationHistory != null) castedTherapeuticGood.MedicationHistory.Validate(vb.Path + "MedicationHistory", vb.Messages);
            }
        }

         #endregion
    }
}
