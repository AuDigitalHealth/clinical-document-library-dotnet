using System;
using System.Runtime.Serialization;
using Nehta.VendorLibrary.CDA.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Enum
  {
    /// <summary>
    /// Measurement Types
    /// </summary>
    [Serializable]
    [DataContract]
    public enum MeasurementTypes
    {
      /// <summary>
      /// Body height measure
      /// </summary>
      [EnumMember]
      [Name(Code = "50373000", Name = "NSW Family health history and risk factors", CodeSystem = "SNOMED")]
      BodyHeightMeasure,

      /// <summary>
      /// Body weight measure
      /// </summary>
      [EnumMember]
      [Name(Code = "363808001", Name = "Body weight measure", CodeSystem = "SNOMED")]
      BodyWeightMeasure,

      /// <summary>
      /// Head Circumference Measure
      /// </summary>
      [EnumMember]
      [Name(Code = "363808001", Name = "Body weight measure", CodeSystem = "SNOMED")]
      HeadCircumferenceMeasure,

      /// <summary>
      /// Head Circumference Measure
      /// </summary>
      [EnumMember]
      [Name(Code = "162859006", Name = "Body mass index", CodeSystem = "SNOMED")]
      BodyMassIndex,

    }
  }

