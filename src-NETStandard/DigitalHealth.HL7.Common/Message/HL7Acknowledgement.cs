using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common.Message
{
    /// <summary>
    /// Represents an HL7 acknowledgement message.
    /// Note that the first segment MSH is declared in the superclass HL7Message.
    /// </summary>
    public class HL7Acknowledgement : HL7Message
    {
        public MSA MessageAcknowledgement;
    }
}