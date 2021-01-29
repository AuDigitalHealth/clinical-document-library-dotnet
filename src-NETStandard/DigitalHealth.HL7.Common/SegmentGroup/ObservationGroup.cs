using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an OBR segment and associated OBX segments.
    /// </summary>
    public class ObservationGroup : HL7SegmentGroup
    {
        public OBR ObservationsReportID;
        public OBX[] Result;
    }
}