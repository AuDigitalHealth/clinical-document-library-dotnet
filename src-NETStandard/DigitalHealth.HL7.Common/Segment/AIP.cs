using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class AIP : HL7Segment
    {
        public string SetIDAIP;
        public string SegmentActionCode;
        public XCN[] PersonnelResourceID;
        public CE ResourceRole;
        public CE ResourceGroup;
        public TS StartDateTime;
        public string StartDateTimeOffset;
        public CE StartDateTimeOffsetUnits;
        public string Duration;
        public CE DurationUnits;
        public string AllowSubstitutionCode;
        public CE FillerStatusCode;
    }
}