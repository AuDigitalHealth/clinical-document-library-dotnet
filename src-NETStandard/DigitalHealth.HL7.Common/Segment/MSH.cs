using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    /// <summary>
    /// Message Header
    /// </summary>
    public class MSH : HL7Segment
    {
        public string FieldSeparator;
        public string EncodingCharacters = string.Empty;
        public HD SendingApplication = new HD();
        public HD SendingFacility;
        public HD ReceivingApplication;
        public HD ReceivingFacility;
        public TS DateTimeOfMessage;
        public string Security;
        public MSG MessageType;
        public string MessageControlID;
        public PT ProcessingID;
        public VID VersionID;
        public string SequenceNumber;
        public string ContinuationPointer;
        public string AcceptAcknowledgmentType;
        public string ApplicationAcknowledgmentType;
        public string CountryCode;
        public string[] CharacterSet;
        public CE PrincipalLanguageOfMessage;
        public string AltCharsetHandlingScheme;
    }
}