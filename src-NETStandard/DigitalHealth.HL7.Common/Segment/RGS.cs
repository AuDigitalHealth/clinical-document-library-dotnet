using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class RGS : HL7Segment
    {
        public string SetIDRGS;
        public string SegmentActionCode;
        public CE ResourceGroupID;
    }
}