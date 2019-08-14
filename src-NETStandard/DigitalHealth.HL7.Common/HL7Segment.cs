using System;
using System.Reflection;
using System.Text.RegularExpressions;
using DigitalHealth.Hl7.Common;
using DigitalHealth.HL7.Common.Exceptions;
using DigitalHealth.HL7.Common.Segment;

namespace DigitalHealth.HL7.Common
{
    public class HL7Segment
    {
        public static HL7Segment Parse(string segmentForm, HL7Separators sep)
        {
            // Split the segment into fields and create the object for the indicated segment identifier
            string[] fieldForm = segmentForm.Split(sep.FieldSeparator);
            HL7Segment segment = CreateBlankSegment(fieldForm[0]);
            if (segment == null)
            {
                return new UnknownSegment()
                {
                    FieldForm = fieldForm
                };
            }

            // Parse each of the fields according to its declared identifier in the segment structure.
            // Some fields will be repeating, some will be strings and some will have components.
            PopulateSegment(sep, fieldForm, segment);
            return segment;
        }

        internal void Encode(HL7Separators seps, System.Text.StringBuilder sb)
        {
            FieldInfo[] fields = GetType().GetFields();

            sb.Append(GetType().Name);
            sb.Append(seps.FieldSeparator);

            foreach (FieldInfo field in fields)
            {
                if (this is MSH && field.Name.Equals("FieldSeparator"))
                {
                    // A field separator was already added after the segment HL7Name, don't add another.
                }
                else if (this is MSH && field.Name.Equals("EncodingCharacters"))
                {
                    // We want the encoding characters raw, not escaped.
                    sb.Append(field.GetValue(this));
                    sb.Append(seps.FieldSeparator);
                }
                else
                {
                    HL7Field.Encode(seps, sb, field.FieldType, field.GetValue(this));
                    sb.Append(seps.FieldSeparator);
                }
            }
        }

        /// <summary>
        /// Creates a new object of the named segment structure.
        /// </summary>
        /// <param HL7Name="segmentName">segment HL7Name, like "MSH"</param>
        /// <returns>new object of the named segment structure</returns>
        private static HL7Segment CreateBlankSegment(string segmentName)
        {
            // Validate the segment name is 3 alphanumeric characters.
            if (Regex.IsMatch(segmentName, "^[A-Z0-9]{3}$"))
            {
                string typeName = string.Format("DigitalHealth.HL7.Common.Segment.{0}", segmentName);
                return Assembly.GetAssembly(typeof(MSH)).CreateInstance(typeName) as HL7Segment;
            }
            else
            {
                string message = string.Format(ConstantsResource.InvalidSegmentName, segmentName);
                throw new HL7ParseException(message);
            }
        }

        /// <summary>
        /// Parses each of the fields according to its declared identifier in the segment structure.
        /// Ignores any extra fields either in the received segment or in the structure.
        /// </summary>
        /// <param HL7Name="sep">Separators to use for repeating fields, components and subcomponents</param>
        /// <param HL7Name="fieldForm">Encoded segment split into fields, where fieldForm[0] is the segment name</param>
        /// <param HL7Name="segment">Empty object of desired segment structure</param>
        private static void PopulateSegment(HL7Separators sep, string[] fieldForm, HL7Segment segment)
        {
            FieldInfo[] fields = segment.GetType().GetFields();

            // Subtract one from the number of fields in the received segment to avoid counting the segment name.
            int offset = 1;

            // Except when segment is MSH, then have no offset but the first field is the field separator.
            if (fieldForm[0].Equals("MSH"))
            {
                fields[0].SetValue(segment, "" + sep.FieldSeparator);
                offset = 0;
            }

            int n = Math.Min(fields.Length, fieldForm.Length - 1);

            for (int i = 1 - offset; i < n; i++)
            {
                object value = HL7Field.Parse(fields[i].FieldType, fieldForm[i + offset], sep);
                fields[i].SetValue(segment, value);
            }
        }
    }

    public class UnknownSegment : HL7Segment
    {
        public string[] FieldForm;
    }
}