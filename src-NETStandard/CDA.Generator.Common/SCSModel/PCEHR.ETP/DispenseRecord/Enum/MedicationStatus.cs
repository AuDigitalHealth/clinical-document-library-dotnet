using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary> 
    /// Dispense Record Sections
    /// </summary>
    [Serializable]
    [DataContract]
    public enum MedicationStatus
    {
      /// <summary>
      /// Active
      /// </summary>
      [EnumMember]
      [Name(Name = "active")]
      Active,
      /// <summary>
      /// Completed
      /// </summary>
      [EnumMember]
      [Name(Name = "completed")]
      Completed

    }
  }

