using System;
using System.Linq;
using System.Text;
using DigitalHealth.Hl7.Common;
using DigitalHealth.Hl7.Common.Message;
using DigitalHealth.HL7.Common.DataStructure;
using DigitalHealth.HL7.Common.Exceptions;
using DigitalHealth.HL7.Common.Message;
using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common
{
    /// <summary>
    /// This is the abstract base class for all HL7 messages.
    /// Specialisations include PAS, Pathology and Acknowledgement messages.
    /// Note that the first segment of any HL7 message must be MSH, and so
    /// the MSH segment is declared in this class. This class defines methods
    /// to parse an HL7 message from the encoded message form, and to encode
    /// an HL7 message into the encoded message form.
    /// </summary>
    public abstract class HL7Message : HL7SegmentGroup
    {
        public MSH MessageHeader;

        public string TypeName
        {
            get
            {
                string typeName = "unknown";
                if (MessageHeader != null)
                {
                    if (MessageHeader.MessageType != null)
                    {
                        typeName = string.Format("{0}-{1}", MessageHeader.MessageType.messagetype, MessageHeader.MessageType.triggerevent);
                    }
                }
                return typeName;
            }
        }

        /// <summary>
        /// Parses the received HL7 message.
        /// </summary>
        /// <param name="messageForm">The raw message encoded in HL7 'pipes and hats' format.</param>
        /// <returns>The parsed HL7 message object</returns>
        /// <exception cref="HL7ParseException">Thrown when parsing fails</exception>
        public static HL7Message Parse(string messageForm)
        {
            // Determine the HL7 separators dynamically from the incoming message (usually "|^~\&")
            HL7Separators sep = new HL7Separators(messageForm);

            // Change CRLF or LF into the standard CR separators
            if (messageForm.Contains("\r\n"))
            {
                messageForm = messageForm.Replace("\r\n", "\r");
            }
            if (messageForm.Contains("\n"))
            {
                messageForm = messageForm.Replace("\n", "\r");
            }

            // Remove consecutive segment separators
            while (messageForm.Contains("\r\r"))
            {
                messageForm = messageForm.Replace("\r\r", "\r");
            }

            // Parse all the segments
            messageForm = messageForm.TrimEnd(new char[] { sep.SegmentSeparator });
            string[] segmentForm = messageForm.Split(sep.SegmentSeparator);
            for (int count = 0; count < segmentForm.Count(); count++)
            {
                segmentForm[count] = segmentForm[count].Replace("\n", string.Empty);
            }
            HL7Segment[] segments = new HL7Segment[segmentForm.Length];
            for (int i = 0; i < segmentForm.Length; i++)
            {
                segments[i] = HL7Segment.Parse(segmentForm[i], sep);
            }

            // Grab the MSH segment in order to determine which message structure to use
            MSH msh = segments[0] as MSH;
            if (msh == null)
            {
                throw new HL7ParseException(ConstantsResource.HL7NoMshSegment);
            }
            if (msh.MessageType == null)
            {
                throw new HL7ParseException(ConstantsResource.NoMessageType);
            }

            // Determine the structure for the indicated message identifier
            Type messageStructure = GetMessageStructure(msh.MessageType);

            // Create the message and populate all the matching segments into its structure
            int segmentIndex = 0;
            HL7Message message = BuildSegmentGroup(messageStructure, segments, ref segmentIndex) as HL7Message;

            return message;
        }

        /// <summary>
        /// Encode message according to HL7 encoding rules with default separators.
        /// </summary>
        /// <returns>Bytes for HL7 message</returns>
        public string Encode()
        {
            return Encode(new HL7Separators());
        }

        /// <summary>
        /// Encode message according to HL7 encoding rules with specified separators.
        /// </summary>
        /// <param HL7Name="seps">Characters to separate segments, fields, field repeats, components and subcomponents</param>
        /// <returns>Bytes for HL7 message</returns>
        public string Encode(HL7Separators seps)
        {
            StringBuilder sb = new StringBuilder();

            // Encode this full message as a segment group
            base.Encode(seps, sb);

            return Cleanup(seps, sb.ToString());
        }

        /// <summary>
        /// Cleans up an HL7 message by removing redundant separators.
        /// </summary>
        /// <param name="seps">The defined separators for the message</param>
        /// <param name="messageForm">The message to be cleaned up</param>
        /// <returns>The cleaned message</returns>
        private static string Cleanup(HL7Separators seps, string messageForm)
        {
            // Save the first 8 characters which contain the HL7 encoding characters that should not be normalised.
            const int PREAMBLE_LENGTH = 8;

            string preamble = messageForm.Substring(0, PREAMBLE_LENGTH);
            messageForm = messageForm.Substring(PREAMBLE_LENGTH);

            // e.g. input:    a&^&~&^&|&^&~&^&\r

            //     step 1:    a^&~^&|^&~^&\r      (removed & before ^)
            //     step 2:    a^~^&|^~^&\r        (removed & before ~)
            //     step 3:    a^~^|^~^&\r         (removed & before |)
            //     step 4:    a^~^|^~^\r          (removed & before \r)

            //     step 5:    a~^|~^\r            (removed ^ before ~)
            //     step 6:    a~|~^\r             (removed ^ before |)
            //     step 7:    a~|~\r              (removed ^ before \r)

            //     step 8:    a|~\r               (removed ~ before |)
            //     step 9:    a|\r                (removed ~ before \r)

            //     step 10:   a\r                 (removed | before \r)

            RemoveRedundantSeparator(ref messageForm, seps.SubcomponentSeparator, seps.ComponentSeparator);
            RemoveRedundantSeparator(ref messageForm, seps.SubcomponentSeparator, seps.FieldRepeatSeparator);
            RemoveRedundantSeparator(ref messageForm, seps.SubcomponentSeparator, seps.FieldSeparator);
            RemoveRedundantSeparator(ref messageForm, seps.SubcomponentSeparator, seps.SegmentSeparator);

            RemoveRedundantSeparator(ref messageForm, seps.ComponentSeparator, seps.FieldRepeatSeparator);
            RemoveRedundantSeparator(ref messageForm, seps.ComponentSeparator, seps.FieldSeparator);
            RemoveRedundantSeparator(ref messageForm, seps.ComponentSeparator, seps.SegmentSeparator);

            RemoveRedundantSeparator(ref messageForm, seps.FieldRepeatSeparator, seps.FieldSeparator);
            RemoveRedundantSeparator(ref messageForm, seps.FieldRepeatSeparator, seps.SegmentSeparator);

            RemoveRedundantSeparator(ref messageForm, seps.FieldSeparator, seps.SegmentSeparator);

            return preamble + messageForm;
        }

        private static Type GetMessageStructure(MSG messageType)
        {
            Type messageStructure;
            string code = messageType.messagetype;
            if (code == MessageTypes.ADT || code == MessageTypes.SIU)
            {
                messageStructure = typeof(HL7GenericPasMessage);
            }
            else if (code == MessageTypes.ORU || code == MessageTypes.ORM)
            {
                messageStructure = typeof(HL7GenericPathMessage);
            }
            else if (code == MessageTypes.ORR || code == MessageTypes.ACK)
            {
                messageStructure = typeof(HL7Acknowledgement);
            }
            else
            {
                string error = string.Format(ConstantsResource.Unhandled_HL7_Message_Type, code);
                throw new HL7ParseException(error);
            }
            return messageStructure;
        }

        /// <summary>
        /// Repeatedly removes a character when it appears before another character, until there are no cases left.
        /// </summary>
        /// <param name="input">The string to modify</param>
        /// <param name="removeCharacter">The character to remove</param>
        /// <param name="beforeCharacter">The character it must be before</param>
        private static void RemoveRedundantSeparator(ref string input, char removeCharacter, char beforeCharacter)
        {
            string pattern = string.Empty + removeCharacter + beforeCharacter;
            string replacement = string.Empty + beforeCharacter;
            string temp = input;
            do
            {
                input = temp;
                temp = input.Replace(pattern, replacement);
            }
            while (!temp.Equals(input));
        }
    }
}