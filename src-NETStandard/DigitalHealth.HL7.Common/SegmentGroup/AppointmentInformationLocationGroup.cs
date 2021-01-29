using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an AIL segment and associated NTE segments.
    /// </summary>
    public class AppointmentInformationLocationGroup : HL7SegmentGroup
    {
        public AIL AppointmentInformationLocation;
        public NTE[] Notes;
    }
}