using System.Xml;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.HL7.Common.Message;
using Nehta.VendorLibrary.CDA.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Interfaces
{
    /// <summary>
    /// Service that transforms Hl7 V2 to CDA Diagnostic Imaging Document
    /// </summary>
    public interface IDiagnosticImagingTransformer
    {
        /// <summary>
        /// Parses a pipe delimetered HL7 v2 diagnostic imaging report into a message model.
        /// </summary>
        /// <param name="payload">HL7 message.</param>
        /// <returns>HL7 message model.</returns>
        HL7GenericMessage ParseHl7Message(string payload);

        /// <summary>
        /// Transforms the HL7 v2 diagnostic imaging message into a MHR compliant CDA document.
        /// </summary>
        /// <param name="message">HL7 diagnostic imaging message model.</param>
        /// <param name="metadata">Additional data required from the source system.</param>
        /// <param name="reportData">Report data.</param>
        /// <returns>Transformed diagnostic imaging report model</returns>
        DiagnosticImagingTransformResult Transform(HL7GenericMessage message, DiagnosticImagingMetadata metadata, byte[] reportData = null);

        /// <summary>
        /// Generates an XML document from the diagnostic imaging report model.
        /// </summary>
        /// <param name="diagnosticImagingReport">Diagnostic imaging report model.</param>
        /// <returns>Diagnostic Imaging XML document.</returns>
        XmlDocument Generate(DiagnosticImagingReport diagnosticImagingReport);
    }
}