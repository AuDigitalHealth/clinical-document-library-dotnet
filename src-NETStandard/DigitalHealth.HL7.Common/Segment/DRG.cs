using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class DRG : HL7Segment
    {
        public CE DiagnosticRelatedGroup;
        public TS DRGAssignedDateTime;
        public string DRGApprovalIndicator;
        public string DRGGrouperReviewCode;
        public CE OutlierType;
        public string OutlierDays;
        public CP OutlierCost;
        public string DRGPayor;
        public CP OutlierReimbursement;
        public string ConfidentialIndicator;
    }
}