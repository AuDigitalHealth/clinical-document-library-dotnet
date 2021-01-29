using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class DG1 : HL7Segment
    {
        public string SetID;
        public string DiagnosisCodingMethod;
        public CE DiagnosisCodeDG1;
        public string DiagnosisDescription;
        public TS DiagnosisDateTime;
        public CE DiagnosisType;   // HL7 v2.3.1 uses IS datatype. Enterprise Standard specifies IS; CE has been implemented in Ensemble.
        public CE MajorDiagnosticCategory;
        public CE DiagnosticRelatedGroup;
        public string DRGApprovalIndicator;
        public string DRGGrouperReviewCode;
        public CE OutlierType;
        public string OutlierDays;
        public CP OutlierCost;
        public string GrouperVersionAndType;
        public string DiagnosisPriority;
        public XCN[] DiagnosingClinician;
        public string DiagnosisClassification;
        public string ConfidentialIndicator;
        public TS AttestationDateTime;
    }
}