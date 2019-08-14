using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an AIG segment and associated NTE segments.
    /// </summary>
    public class AppointmentInformationGeneralGroup : HL7SegmentGroup
    {
        public AIG AppointmentInformationGeneral;
        public NTE[] Notes;
    }
}