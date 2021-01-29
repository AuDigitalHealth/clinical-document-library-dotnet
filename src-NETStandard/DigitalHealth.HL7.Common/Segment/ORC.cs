using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class ORC : HL7Segment
    {
        public string OrderControl;
        public EI PlacerOrderNumber;
        public EI FillerOrderNumber;
        public EI PlacerGroupNumber;
        public string OrderStatus;
        public string ResponseFlag;
        public TQ QuantityTiming;
        public EIP Parent;
        public TS DateTimeofTransaction;
        public XCN[] EnteredBy;
        public XCN[] VerifiedBy;
        public XCN[] OrderingProvider;
        public PL EnterersLocation;
        public XTN[] CallBackPhoneNumber;
        public TS OrderEffectiveDateTime;
        public CE OrderControlCodeReason;
        public CE EnteringOrganization;
        public CE EnteringDevice;
        public XCN[] ActionBy;
        public CE AdvancedBeneficiaryNoticeCode;
        public XON[] OrderingFacilityName;
        public XAD[] OrderingFacilityAddress;
        public XTN[] OrderingFacilityPhoneNumber;
        public XAD[] OrderingProviderAddress;
    }
}