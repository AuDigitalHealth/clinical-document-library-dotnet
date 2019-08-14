using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class AIG : HL7Segment
    {
        public string SetIDAIG;
        public string SegmentActionCode;
        public CE ResourceID;
        public CE ResourceType;
        public CE[] ResourceGroup;
        public string ResourceQuantity;
        public CE ResourceQuantityUnits;
        public TS StartDateTime;
        public string StartDateTimeOffset;
        public CE StartDateTimeOffsetUnits;
        public string Duration;
        public CE DurationUnits;
        public string AllowSubstitutionCode;
        public CE FillerStatusCode;
    }
}