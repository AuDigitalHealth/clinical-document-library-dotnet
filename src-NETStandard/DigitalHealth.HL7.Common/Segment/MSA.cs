using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class MSA : HL7Segment
    {
        public string AcknowledgmentCode;
        public string MessageControlID;
        public string TextMessage;
        public string ExpectedSequenceNumber;
        public string DelayedAcknowledgmentType;
        public CE ErrorCondition;
    }
}