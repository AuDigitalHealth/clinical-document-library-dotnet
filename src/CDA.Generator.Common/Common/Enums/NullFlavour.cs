using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
  /// <summary>
  /// NullFlavor
  /// </summary>
    [Serializable]
    [DataContract]
    public enum NullFlavour
    {

        /// <summary>
        /// No information
        /// </summary>
        [EnumMember]
        [Name(Code = "NI", Name = "No Information")]
        NoInformation,

        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember]
        [Name(Code = "UNK", Name = "Unknown")]
        Unknown,

        /// <summary>
        /// Asked but unknown
        /// </summary>
        [EnumMember]
        [Name(Code = "ASKU", Name = "Asked But Unknown")]
        AskedButUnknown,

        /// <summary>
        /// Temporarily unavailable
        /// </summary>
        [EnumMember]
        [Name(Code = "NAV", Name = "Temporarily Unavailable")]
        TemporarilyUnavailable,

        /// <summary>
        /// Not asked
        /// </summary>
        [EnumMember]
        [Name(Code = "NASK", Name = "Not Asked")]
        NotAsked,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember]
        [Name(Code = "OTH", Name = "Other")]
        Other,

        /// <summary>
        /// Negative infinity
        /// </summary>
        [EnumMember]
        [Name(Code = "NINF", Name = "Negative Infinity")]
        NegativeInfinity,

        /// <summary>
        /// Positive infinity
        /// </summary>
        [EnumMember]
        [Name(Code = "PINF", Name = "Positive Infinity")]
        PositiveInfinity,

        /// <summary>
        /// Trace
        /// </summary>
        [EnumMember]
        [Name(Code = "TRC", Name = "Trace")]
        Trace,

        /// <summary>
        /// Masked
        /// </summary>
        [EnumMember]
        [Name(Code = "MSK", Name = "Masked")]
        Masked,


        /// <summary>
        /// Not applicable
        /// </summary>
        [EnumMember]
        [Name(Code = "NA", Name = "Not Applicable")]
        NotApplicable,

    }
}
