using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class AIS : HL7Segment
    {
        public string SetIDAIS;
        public string SegmentActionCode;
        public CE UniversalServiceID;
        public TS StartDateTime;
        public string StartDateTimeOffset;
        public CE StartDateTimeOffsetUnits;
        public string Duration;
        public CE DurationUnits;
        public string AllowSubstitutionCode;
        public CE FillerStatusCode;
    }
}