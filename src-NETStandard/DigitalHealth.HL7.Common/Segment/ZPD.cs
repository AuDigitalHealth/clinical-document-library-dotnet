using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class ZPD : HL7Segment
    {
        public XCN[] LocalMedicalOfficer;
        public XTN[] LMOPhone;
        public XAD LMOAddress;
        public CE[] SpecificDestination;
    }
}