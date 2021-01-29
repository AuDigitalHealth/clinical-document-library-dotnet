using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// AddressAbsentIndicator
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AddressAbsentIndicator
    {
        /// <summary>
        /// Masked
        /// </summary>
        [EnumMember]
        [Name(Code = "MSK")]
        Masked,

        /// <summary>
        /// No fixed address indicator
        /// </summary>
        [EnumMember]
        [Name(Code = "NA")]
        NoFixedAddressIndicator,

        /// <summary>
        /// Not known
        /// </summary>
        [EnumMember]
        [Name(Code = "UNK")]
        NotKnown,

        /// <summary>
        /// Not known
        /// </summary>
        [EnumMember]
        [Name(Code = "NI")]
        NotIndicated
    }
}

