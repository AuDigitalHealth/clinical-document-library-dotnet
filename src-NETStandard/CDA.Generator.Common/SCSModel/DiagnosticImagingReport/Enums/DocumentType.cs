using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {
    /// <summary>
   /// Result Status
    /// </summary>
   [Serializable]
   [DataContract]
    public enum DocumentType
    {
      /// <summary>
      /// Advance Care Plan/Directive - Overall Plan of Care/Advance Care Directives
      /// </summary>
      [EnumMember]
      [Name(Code = "52521-2", Name = "Advance Care Plan/Directive - Overall Plan of Care/Advance Care Directives", CodeSystem = "LOINC")]
      AdvanceCarePlanDirectiveOverallPlanOfCareAdvanceCareDirectives,

      /// <summary>
      /// Substitute Decision Maker - Decision authority documented
      /// </summary>
      [EnumMember]
      [Name(Code = "52687-1", Name = "Substitute Decision Maker - Decision authority documented", CodeSystem = "LOINC")]
      SubstituteDecisionMaker,

      /// <summary>
      /// Substitute Decision Maker - Decision authority documented
      /// </summary>
      [EnumMember]
      [Name(Code = "100.16998", Name = "Advance Care Planning Document", CodeSystem = "NCTIS")]
      AdvanceCarePlanDirectiveAndSubstituteDecisionMaker
    }
  }

