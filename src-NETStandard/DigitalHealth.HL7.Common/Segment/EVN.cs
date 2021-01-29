using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class EVN : HL7Segment
    {
        public string EventTypeCode;
        public TS RecordedDateTime;
        public TS DateTimePlannedEvent;
        public string EventReasonCode;
        public XCN[] OperatorID;
        public TS EventOccurred;
    }
}