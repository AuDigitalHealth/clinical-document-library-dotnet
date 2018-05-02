using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an optional ORC segment, and a set of observation groups, each of which has an OBR segment and a set of OBX segments.
    /// </summary>
    public class OrderGroup : HL7SegmentGroup
    {
        public ORC CommonOrder;
        public ObservationGroup[] Observation;
    }
}
