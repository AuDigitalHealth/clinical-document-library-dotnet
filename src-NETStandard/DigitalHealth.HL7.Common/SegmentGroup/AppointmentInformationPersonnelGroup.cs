using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an AIP segment and associated NTE segments.
    /// </summary>
    public class AppointmentInformationPersonnelGroup : HL7SegmentGroup
    {
        public AIP AppointmentInformationPersonnel;
        public NTE[] Notes;
    }
}