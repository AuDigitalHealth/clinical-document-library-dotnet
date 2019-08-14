using DigitalHealth.Hl7.Common;
using DigitalHealth.HL7.Common.Exceptions;

namespace DigitalHealth.HL7.Common
{
    public class HL7Separators
    {
        private const string END_BOLD_TAG = "</b>";
        private const string START_BOLD_TAG = "<b>";
        private char componentSeparator = '^';

        private char escapeCharacter = '\\';

        private char fieldRepeatSeparator = '~';

        private char fieldSeparator = '|';

        // These defaults are used when the parameterless constructor is called.
        private char segmentSeparator = '\r';

        private char subcomponentSeparator = '&';

        /// <summary>
        /// Use typical HL7 separators.
        /// </summary>
        public HL7Separators()
        {
        }

        /// <summary>
        /// Dynamically determine HL7 separators from message form.
        /// </summary>
        /// <param HL7Name="messageForm">HL7 message encoded with HL7 encoding rules</param>
        public HL7Separators(string messageForm)
        {
            if (!messageForm.StartsWith("MSH"))
            {
                throw new HL7ParseException(ConstantsResource.HL7NotStartMsh);
            }

            if (messageForm.Length < 22)
            {
                throw new HL7ParseException(ConstantsResource.HL7TooShort);
            }

            segmentSeparator = '\r';

            // Read the other separators from the message
            fieldSeparator = messageForm[3];
            componentSeparator = messageForm[4];
            fieldRepeatSeparator = messageForm[5];
            escapeCharacter = messageForm[6];
            subcomponentSeparator = messageForm[7];
        }

        public char ComponentSeparator
        {
            get { return componentSeparator; }
        }

        /// <summary>
        /// The four encoding characters (component, field repeat, escape and subcomponent), typically "^~\&", that appear in the message header.
        /// </summary>
        public string EncodingCharacters
        {
            get
            {
                return string.Format("{0}{1}{2}{3}", componentSeparator, fieldRepeatSeparator, escapeCharacter, subcomponentSeparator);
            }
        }

        public char EscapeCharacter
        {
            get { return escapeCharacter; }
        }

        public char FieldRepeatSeparator
        {
            get { return fieldRepeatSeparator; }
        }

        public char FieldSeparator
        {
            get { return fieldSeparator; }
        }

        public char SegmentSeparator
        {
            get { return segmentSeparator; }
        }

        public char SubcomponentSeparator
        {
            get { return subcomponentSeparator; }
        }

        /// <summary>
        /// Decodes an value from the HL7 message by replacing escape sequences
        /// with the characters that they represent. Note that the line break
        /// sequence \.br\ is decoded into CRLF (assuming the escape character
        /// is a backslash).
        /// </summary>
        /// <param HL7Name="value">Encoded value</param>
        /// <returns>Decoded value</returns>
        public string Decode(string value)
        {
            value = value.Replace(escapeCharacter + ".br" + escapeCharacter, "\r\n");
            value = value.Replace(escapeCharacter + "H" + escapeCharacter, START_BOLD_TAG);
            value = value.Replace(escapeCharacter + "N" + escapeCharacter, END_BOLD_TAG);
            value = value.Replace(escapeCharacter + "R" + escapeCharacter, "" + fieldRepeatSeparator);
            value = value.Replace(escapeCharacter + "T" + escapeCharacter, "" + subcomponentSeparator);
            value = value.Replace(escapeCharacter + "S" + escapeCharacter, "" + componentSeparator);
            value = value.Replace(escapeCharacter + "F" + escapeCharacter, "" + fieldSeparator);
            value = value.Replace(escapeCharacter + "E" + escapeCharacter, "" + escapeCharacter);
            return value;
        }

        /// <summary>
        /// Encodes a value to be stored in the HL7 message by replacing
        /// characters with the escape sequences that represent them.
        /// Any CRLF, bare CR or bare LF is encoded into a break sequence
        /// "\.br\" (assuming the escape character is a backslash).
        /// </summary>
        /// <param HL7Name="value">Original value</param>
        /// <returns>Encoded value</returns>
        public string Encode(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            value = value.Replace("" + escapeCharacter, escapeCharacter + "E" + escapeCharacter);
            value = value.Replace("" + fieldSeparator, escapeCharacter + "F" + escapeCharacter);
            value = value.Replace("" + componentSeparator, escapeCharacter + "S" + escapeCharacter);
            value = value.Replace("" + subcomponentSeparator, escapeCharacter + "T" + escapeCharacter);
            value = value.Replace("" + fieldRepeatSeparator, escapeCharacter + "R" + escapeCharacter);
            value = value.Replace(START_BOLD_TAG, escapeCharacter + "H" + escapeCharacter);
            value = value.Replace(END_BOLD_TAG, escapeCharacter + "N" + escapeCharacter);
            value = value.Replace("\r\n", escapeCharacter + ".br" + escapeCharacter);
            value = value.Replace("\r", escapeCharacter + ".br" + escapeCharacter);
            value = value.Replace("\n", escapeCharacter + ".br" + escapeCharacter);
            return value;
        }
    }
}