using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.SegmentGroup
{
    /// <summary>
    /// Represents an RGS segment and associated AIS, AIG, AIL and AIP segment groups.
    /// </summary>
    public class ResourceGroup : HL7SegmentGroup
    {
        public RGS ResourceGroupSegment;
        public AppointmentInformationServiceGroup[] Services;
        public AppointmentInformationGeneralGroup[] GeneralResources;
        public AppointmentInformationLocationGroup[] LocationResources;
        public AppointmentInformationPersonnelGroup[] PersonnelResources;
    }
}