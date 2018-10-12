using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// CeHR RecordSections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PcmlSections
    {
        /// <summary>
        /// Physical Measurements
        /// </summary>
        [EnumMember]
        [Name(Code = "56445-0", Name = "Pharmacist Shared Medicines List", Title = "Pharmacist Shared Medicines List", CodeSystem = "LOINC")]
        PharmacistSharedMedicinesList,
    }
}