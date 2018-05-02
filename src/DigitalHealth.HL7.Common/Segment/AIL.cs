using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class AIL : HL7Segment
    {
        public string setIDAIL;
        public string segmentActionCode;
        public PL LocationResourceID;
        public CE LocationTypeAIL;
        public CE LocationGroup;
        public TS StartDateTime;
        public string StartDateTimeOffset;
        public CE StartDateTimeOffsetUnits;
        public string Duration;
        public CE DurationUnits;
        public string AllowSubstitutionCode;
        public CE FillerStatusCode;
    }
}