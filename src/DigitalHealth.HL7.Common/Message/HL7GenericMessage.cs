using DigitalHealth.HL7.Common.Segment;
using DigitalHealth.HL7.Common.SegmentGroup;

namespace DigitalHealth.HL7.Common.Message
{
    /// <summary>
    /// This class represents a generic HL7 message from a Laboratory Information System.
    /// This covers all pathology test result messages.
    /// Note that the first segment MSH is declared in the superclass HL7Message.
    /// </summary>
    public class HL7GenericMessage : HL7Message
    {
        public PID PatientIdentification;
        public PV1 PatientVisit;
        public OrderGroup[] Order;        
    }
}