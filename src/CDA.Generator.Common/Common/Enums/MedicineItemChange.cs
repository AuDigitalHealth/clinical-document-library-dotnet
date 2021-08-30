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

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Medicine Item Change
    /// </summary>
    [Serializable]
    [DataContract]
    public enum MedicineItemChange
    {
        /// <summary>
        /// no change
        /// </summary>
        [EnumMember]
        [Name(Code = "nochange", Name = "Unchanged", CodeSystem = "HL7MedicineItemChangeCodes")]
        NoChange,

        /// <summary>
        /// New
        /// </summary>
        [EnumMember]
        [Name(Code = "new", Name = "New", CodeSystem = "HL7MedicineItemChangeCodes")]
        New,

        /// <summary>
        /// prescribed
        /// </summary>
        [EnumMember]
        [Name(Code = "prescribed", Name = "Prescribed", CodeSystem = "HL7MedicineItemChangeCodes")]
        Prescribed,

        /// <summary>
        /// ceased
        /// </summary>
        [EnumMember]
        [Name(Code = "ceased", Name = "Ceased", CodeSystem = "HL7MedicineItemChangeCodes")]
        Ceased,

        /// <summary>
        /// suspended
        /// </summary>
        [EnumMember]
        [Name(Code = "suspended", Name = "Suspended", CodeSystem = "HL7MedicineItemChangeCodes")]
        Suspended,

        /// <summary>
        /// cancelled
        /// </summary>
        [EnumMember]
        [Name(Code = "cancelled", Name = "Cancelled", CodeSystem = "HL7MedicineItemChangeCodes")]
        Cancelled,

        /// <summary>
        /// amended
        /// </summary>
        [EnumMember]
        [Name(Code = "amended", Name = "Amended", CodeSystem = "HL7MedicineItemChangeCodes")]
        Amended,

        /// <summary>
        /// new-recommended
        /// </summary>
        [EnumMember]
        [Name(Code = "new-recommended", Name = "New Recommended", CodeSystem = "HL7MedicineItemChangeCodes")]
        NewRecommended,

        /// <summary>
        /// prescription-recommended
        /// </summary>
        [EnumMember]
        [Name(Code = "prescription-recommended", Name = "Prescription Recommended", CodeSystem = "HL7MedicineItemChangeCodes")]
        PrescriptionRecommended,

        /// <summary>
        /// review-recommended
        /// </summary>
        [EnumMember]
        [Name(Code = "review-recommended", Name = "Review Recommended", CodeSystem = "HL7MedicineItemChangeCodes")]
        ReviewRecommended,

        /// <summary>
        /// cessation-recommended
        /// </summary>
        [EnumMember]
        [Name(Code = "cessation-recommended", Name = "Cessation Recommended", CodeSystem = "HL7MedicineItemChangeCodes")]
        CessationRecommended,

        /// <summary>
        /// suspension-recommended
        /// </summary>
        [EnumMember]
        [Name(Code = "suspension-recommended", Name = "Suspension Recommended", CodeSystem = "HL7MedicineItemChangeCodes")]
        SuspensionRecommended,

        /// <summary>
        /// cancellation-recommended
        /// </summary>
        [EnumMember]
        [Name(Code = "cancellation-recommended", Name = "Cancellation Recommended", CodeSystem = "HL7MedicineItemChangeCodes")]
        CancellationRecommended,

    }
}
