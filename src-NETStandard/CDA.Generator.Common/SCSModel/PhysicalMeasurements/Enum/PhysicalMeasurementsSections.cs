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
    /// Physical Measurements
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PhysicalMeasurementDocumentSections
    {
      /// <summary>
      /// Physical Measurements
      /// </summary>
      [EnumMember]
      [Name(Code = "101.16491", Name = "Physical Measurements", CodeSystem = "NCTIS", Identifier = "1.2.36.1.2001.1001.101.101.16491")]
      PhysicalMeasurements,

      /// <summary>
      /// Head Circumference
      /// </summary>
      [EnumMember]
      [Name(Code = "363812007", Name = "Head Circumference", CodeSystem = "SNOMEDCT")]
      HeadCircumference,

      /// <summary>
      /// Body Height/Length
      /// </summary>
      [EnumMember]
      [Name(Code = "50373000", Name = "Body Height/Length", CodeSystem = "SNOMEDCT")]
      BodyHeightLength,

      /// <summary>
      /// Head structure
      /// </summary>
      [EnumMember]
      [Name(Code = "69536005", Name = "Head structure", CodeSystem = "SNOMEDCT")]
      HeadStructure,

      /// <summary>
      /// Body Weight
      /// </summary>
      [EnumMember]
      [Name(Code = "27113001", Name = "Body Weight", CodeSystem = "SNOMEDCT")]
      BodyWeight,

      /// <summary>
      /// Body Mass Index
      /// </summary>
      [EnumMember]
      [Name(Code = "60621009", Name = "Body mass index", CodeSystem = "SNOMEDCT")]
      BodyMassIndex,

      /// <summary>
      /// normal range
      /// </summary>
      [EnumMember]
      [Name(Code = "260395002", Name = "normal range", CodeSystem = "SNOMED")]
      NormalRange,

      /// <summary>
      /// Comment
      /// </summary>
      [EnumMember]
      [Name(Code = "103.15600", Name = "Comment", CodeSystem = "NCTIS")]
      Comment,

      /// <summary>
      /// Confounding Factor Name
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16950", Name = "Confounding Factor Name", CodeSystem = "NCTIS")]
      ConfoundingFactorName,

      /// <summary>
      /// State of Dress
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16845", Name = "State of Dress", CodeSystem = "NCTIS")]
      StateOfDress,

      /// <summary>
      /// Pregnant
      /// </summary>
      [EnumMember]
      [Name(Code = "77386006", Name = "pregnant", CodeSystem = "SNOMEDCT")]
      Pregnant,

      /// <summary>
      /// On examination - agitated
      /// </summary>
      [EnumMember]
      [Name(Code = "162721008", Name = "Confounding Factor", CodeSystem = "SNOMED")]
      OnExaminationAgitated,

      /// <summary>
      /// Weight Estimation Formula
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16847", Name = "Weight Estimation Formula", CodeSystem = "NCTIS")]
      WeightEstimationFormula,

      /// <summary>
      /// ASSERTION
      /// </summary>
      [EnumMember]
      [Name(Code = "ASSERTION")]
      ASSERTION,

      /// <summary>
      /// Position
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16519", Name = "Position", CodeSystem = "NCTIS")]
      Position,

      /// <summary>
      /// Position
      /// </summary>
      [EnumMember]
      [Name(Code = "103.16860", Name = "Formula", CodeSystem = "NCTIS")]
      Formula,

    }
  }

