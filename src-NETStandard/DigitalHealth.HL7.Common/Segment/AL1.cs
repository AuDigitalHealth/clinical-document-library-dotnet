using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class AL1 : HL7Segment
    {
        public string SetID;
        public string AllergyType;
        public CE AllergyCodeMnemonicDescription;
        public string AllergySeverity;
        public string[] AllergyReaction;
        public string IdentificationDate;
    }
}