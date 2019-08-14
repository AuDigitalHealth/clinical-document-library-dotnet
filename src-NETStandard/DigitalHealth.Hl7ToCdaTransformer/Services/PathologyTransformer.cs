using System;
using System.Xml;
using DigitalHealth.Hl7ToCdaTransformer.Interfaces;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.HL7.Common;
using DigitalHealth.HL7.Common.Message;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    /// <summary>
    /// Implementation for <seealso cref="DigitalHealth.Hl7ToCdaTransformer.Interfaces.IPathologyTransformer" />
    /// </summary>
    /// <seealso cref="DigitalHealth.Hl7ToCdaTransformer.Interfaces.IPathologyTransformer" />
    public class PathologyTransformer : IPathologyTransformer
    {
        private readonly PathologyMessageTransformer _pathologyMessageTransformer = new PathologyMessageTransformer();
        private readonly PathologyMessageValidator _pathologyMessageValidator = new PathologyMessageValidator();

        /// <summary>
        /// Parses a pipe delimetered HL7 v2 pathology into a message model.
        /// </summary>
        /// <param name="payload">HL7 message.</param>
        /// <returns>HL7 pathology message model.</returns>
        /// <exception cref="ArgumentNullException">payload</exception>
        /// <exception cref="ArgumentException">Message is not a HL7 result message (ORU_R01)</exception>
        public HL7GenericMessage ParseHl7Message(string payload)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                throw new ArgumentNullException(nameof(payload));
            }

            HL7GenericMessage message = (HL7GenericMessage)HL7Message.Parse(payload);

            if (!(message.MessageHeader.MessageType.messagetype == TransformerConstants.Hl7PathologyMessage && 
                  message.MessageHeader.MessageType.triggerevent == TransformerConstants.Hl7PathologyMessageType))
            {
                throw new ArgumentException("Message is not a HL7 result message (ORU_R01)");
            }

            return message;
        }

        /// <summary>
        /// Transforms the HL7 v2 pathology message into a MHR compliant CDA document.
        /// </summary>
        /// <param name="message">HL7 pathology message model.</param>
        /// <param name="metadata">Additional data required from the source system.</param>
        /// <param name="reportData"></param>
        /// <returns>Transformed pathology report model</returns>
        public PathologyTransformResult Transform(HL7GenericMessage message, PathologyMetadata metadata, byte[] reportData = null)
        {
            _pathologyMessageValidator.Validate(message, metadata, reportData);

            return _pathologyMessageTransformer.Transform(message, metadata, reportData);
        }

        /// <summary>
        /// Generates an XML document from the pathology report model.
        /// </summary>
        /// <param name="pathologyResultReport">HL7 pathology message model.</param>
        /// <returns>Pathology XML document.</returns>
        /// <exception cref="ArgumentNullException">pathologyResultReport</exception>
        public XmlDocument Generate(PathologyResultReport pathologyResultReport)
        {
            if (pathologyResultReport == null)
            {
                throw new ArgumentNullException(nameof(pathologyResultReport));
            }

            return CDAGenerator.GeneratePathologyResultReport(pathologyResultReport);
        }
    }
}