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
    /// Implementation for <seealso cref="DigitalHealth.Hl7ToCdaTransformer.Interfaces.IDiagnosticImagingTransformer" />
    /// </summary>
    /// <seealso cref="DigitalHealth.Hl7ToCdaTransformer.Interfaces.IDiagnosticImagingTransformer" />
    public class DiagnosticImagingTransformer : IDiagnosticImagingTransformer
    {
        private readonly DiagnosticImagingMessageTransformer _diagnosticImagingMessageTransformer = new DiagnosticImagingMessageTransformer();
        private readonly DiagnosticImagingMessageValidator _diagnosticImagingMessageValidator = new DiagnosticImagingMessageValidator();

        /// <summary>
        /// Parses a pipe delimetered HL7 v2 diagnostic imaging report into a message model.
        /// </summary>
        /// <param name="payload">HL7 message.</param>
        /// <returns>HL7 diagnostic imaging message model.</returns>
        /// <exception cref="ArgumentNullException">payload</exception>
        /// <exception cref="ArgumentException">Message is not a HL7 result message (ORU_R01)</exception>
        public HL7GenericMessage ParseHl7Message(string payload)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                throw new ArgumentNullException(nameof(payload));
            }

            HL7GenericMessage message = (HL7GenericMessage)HL7Message.Parse(payload);

            if (!(message.MessageHeader.MessageType.messagetype == TransformerConstants.Hl7DiagnosticImagingMessage &&
                  message.MessageHeader.MessageType.triggerevent == TransformerConstants.Hl7DiagnosticImagingMessageType))
            {
                throw new ArgumentException("Message is not a HL7 result message (ORU_R01)");
            }

            return message;
        }

        /// <summary>
        /// Transforms the HL7 v2 diagnostic imaging message into a MHR compliant CDA document.
        /// </summary>
        /// <param name="message">HL7 message model.</param>
        /// <param name="metadata">Additional data required from the source system.</param>
        /// <param name="reportData"></param>
        /// <returns>Transformed diagnostic imaging report model</returns>
        public DiagnosticImagingTransformResult Transform(HL7GenericMessage message, DiagnosticImagingMetadata metadata,
            byte[] reportData = null)
        {
            _diagnosticImagingMessageValidator.Validate(message, metadata, reportData);

            return _diagnosticImagingMessageTransformer.Transform(message, metadata, reportData);
        }

        /// <summary>
        /// Generates an XML document from the diagnostic imaging report model.
        /// </summary>
        /// <param name="diagnosticImagingReport">HL7 diagnostic imaging message model.</param>
        /// <returns>Pathology XML document.</returns>
        public XmlDocument Generate(DiagnosticImagingReport diagnosticImagingReport)
        {
            if (diagnosticImagingReport == null)
            {
                throw new ArgumentNullException(nameof(diagnosticImagingReport));
            }

            return CDAGenerator.GenerateDiagnosticImagingReport(diagnosticImagingReport);
        }
    }
}
