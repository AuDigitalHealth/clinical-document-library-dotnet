using System;
using DigitalHealth.HL7.Common.DataStructure;
using DigitalHealth.HL7.Common.Message;
using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common
{
    /// <summary>
    /// This class contains the logic to build acknowledgements for received HL7 messages.
    /// </summary>
    public class HL7AcknowledgementFactory
    {
        /// <summary>
        /// Creates an HL7 acknowledgement for a received message. If there is an
        /// error condition, the acknowledgement will be a negative acknowledgement
        /// (code AE), otherwise it will be a positive acknowedgement (code AA).
        /// </summary>
        /// <param HL7Name="message">The received message</param>
        /// <param HL7Name="facility">The facility that is returning the acknowledgement</param>
        /// <param HL7Name="application">The application that is returning the acknowledgement</param>
        /// <param HL7Name="errorCondition">Null if no error, otherwise a code and description of the error</param>
        /// <returns>The HL7 acknowledgement</returns>
        public static HL7Acknowledgement Acknowledge(HL7Message message, string facility, string application, CE errorCondition)
        {
            HL7Acknowledgement ack = new HL7Acknowledgement();
            ack.MessageHeader = new MSH();
            ack.MessageHeader.FieldSeparator = message.MessageHeader.FieldSeparator;
            ack.MessageHeader.EncodingCharacters = message.MessageHeader.EncodingCharacters;
            ack.MessageHeader.SendingApplication = new HD() { namespaceID = application };
            ack.MessageHeader.SendingFacility = new HD() { namespaceID = facility };
            ack.MessageHeader.ReceivingApplication = message.MessageHeader.SendingApplication;
            ack.MessageHeader.ReceivingFacility = message.MessageHeader.SendingFacility;
            ack.MessageHeader.DateTimeOfMessage = new TS() { TimestampValue = DateTime.Now };
            ack.MessageHeader.Security = message.MessageHeader.Security;
            ack.MessageHeader.MessageType = message.MessageHeader.MessageType;
            ack.MessageHeader.MessageControlID = NewMessageControlID();
            ack.MessageHeader.ProcessingID = message.MessageHeader.ProcessingID;
            ack.MessageHeader.VersionID = message.MessageHeader.VersionID;
            ack.MessageHeader.SequenceNumber = message.MessageHeader.SequenceNumber;
            ack.MessageHeader.ContinuationPointer = message.MessageHeader.ContinuationPointer;
            ack.MessageHeader.AcceptAcknowledgmentType = message.MessageHeader.AcceptAcknowledgmentType;
            ack.MessageHeader.ApplicationAcknowledgmentType = message.MessageHeader.ApplicationAcknowledgmentType;
            ack.MessageHeader.CountryCode = message.MessageHeader.CountryCode;
            ack.MessageHeader.CharacterSet = message.MessageHeader.CharacterSet;
            ack.MessageHeader.PrincipalLanguageOfMessage = message.MessageHeader.PrincipalLanguageOfMessage;
            ack.MessageHeader.AltCharsetHandlingScheme = message.MessageHeader.AltCharsetHandlingScheme;
            ack.MessageAcknowledgement = new MSA();
            if (errorCondition == null)
            {
                ack.MessageAcknowledgement.AcknowledgmentCode = "AA";
            }
            else
            {
                ack.MessageAcknowledgement.AcknowledgmentCode = "AE";
                ack.MessageAcknowledgement.ErrorCondition = errorCondition;
            }
            ack.MessageAcknowledgement.MessageControlID = message.MessageHeader.MessageControlID;
            return ack;
        }

        /// <summary>
        /// Creates a negative HL7 acknowledgement for an unparseable message.
        /// </summary>
        /// <param HL7Name="errorMessage">A description of the error</param>
        /// <returns>The HL7 acknowledgement</returns>
        public static HL7Acknowledgement AcknowledgeUnparseableMessage(string errorMessage)
        {
            HL7Separators typical = new HL7Separators();
            HL7Acknowledgement ack = new HL7Acknowledgement();
            ack.MessageHeader = new MSH();
            ack.MessageHeader.FieldSeparator = typical.FieldSeparator.ToString();
            ack.MessageHeader.EncodingCharacters = typical.EncodingCharacters;
            ack.MessageHeader.SendingApplication = new HD() { namespaceID = "HIPS" };
            ack.MessageHeader.SendingFacility = new HD() { namespaceID = "HIPS" };
            ack.MessageHeader.ReceivingApplication = new HD() { namespaceID = "HIPS" };
            ack.MessageHeader.ReceivingFacility = new HD() { namespaceID = "HIPS" };
            ack.MessageHeader.DateTimeOfMessage = new TS() { TimestampValue = DateTime.Now };
            ack.MessageHeader.MessageControlID = NewMessageControlID();
            ack.MessageAcknowledgement = new MSA();
            ack.MessageAcknowledgement.AcknowledgmentCode = "AE";
            ack.MessageAcknowledgement.ErrorCondition = new CE() { text = errorMessage };
            return ack;
        }

        private static string NewMessageControlID()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 20);
        }
    }
}