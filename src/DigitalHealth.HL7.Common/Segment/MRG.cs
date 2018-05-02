using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class MRG : HL7Segment
    {
        public CX[] PriorPatientIdentifierList;
        public CX[] PriorAlternatePatientID;
        public CX PriorPatientAccountNumber;
        public CX PriorPatientID;
        public CX PriorVisitNumber;
        public CX PriorAlternateVisitID;
        public XPN[] PriorPatientName;
    }
}