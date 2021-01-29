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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// Medication class
    /// 
    /// This class encapsulates medication information that is used to populate medication 
    /// related sections / information with CDA documents
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    internal class Medication : IMedicationInstruction, IMedication, IMedicationItem
    {
        #region Properties

        private string _id;
        public string Id
        {
            get { return _id ?? (_id = Guid.NewGuid().ToString()); }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// The medicine as a codableText
        /// </summary>
        [DataMember]
        public ICodableText Medicine { get; set; }

        /// <summary>
        /// The directions of use for this medication
        /// </summary>
        [DataMember]
        public StructuredText Directions { get; set; }

        /// <summary>
        /// The clinical indication associated with this medication
        /// </summary>
        [DataMember]
        public String ClinicalIndication { get; set; }

        /// <summary>
        /// Any comments associated with this medication
        /// </summary>
        [DataMember]
        public String Comment { get; set; }

        /// <summary>
        /// Change type
        /// </summary>
        [DataMember]
        public ICodableText ChangeType { get; set; }

        /// <summary>
        /// Change Status
         /// NOTE: Replaces RecommendationOrChange for SL/ER/DIS
        /// </summary>
        [DataMember]
        public ICodableText ChangeStatus { get; set; }

        /// <summary>
        /// Change description
        /// </summary>
        [DataMember]
        public String ChangeDescription { get; set; }

        /// <summary>
        /// Change reason
        /// </summary>
        [DataMember]
        public StructuredText ChangeReason { get; set; }

        #endregion

        #region Constructors
        internal Medication()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Medication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationInstruction.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Medicine", Medicine))
            {
                if (Medicine != null) Medicine.Validate(vb.Path + "Medicine", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Directions", Directions))
            {
                if (Directions != null) Directions.Validate(vb.Path + "Directions", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this Medication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedication.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Medicine", Medicine))
            {
                Medicine.ValidateMandatory(vb.Path + "Medicine", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Directions", Directions))
            {
                Directions.Validate(vb.Path + "Directions", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this Medication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IMedicationItem.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Medicine", Medicine))
            {
                if (Medicine != null) Medicine.ValidateMandatory(vb.Path, vb.Messages);
            }

            vb.ArgumentRequiredCheck("ChangeType", ChangeType);

            vb.ArgumentRequiredCheck("ChangeStatus", ChangeStatus);

            if (vb.ArgumentRequiredCheck("Directions", Directions))
            {
                if (Directions != null) Directions.Validate(vb.Path + "Directions", vb.Messages);
            }
        }

       #endregion
    }
}