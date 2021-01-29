using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an AIS segment and associated NTE segments.
    /// </summary>
    public class AppointmentInformationServiceGroup : HL7SegmentGroup
    {
        public AIS AppointmentInformationService;
        public NTE[] Notes;
    }
}