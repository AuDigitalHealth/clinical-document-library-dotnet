using System.Xml;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.HL7.Common.Message;
using Nehta.VendorLibrary.CDA.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Interfaces
{
    /// <summary>
    /// Service that transforms Hl7 V2 to CDA Pathology Document
    /// </summary>
    public interface IPathologyTransformer
    {
        /// <summary>
        /// Parses a pipe delimetered HL7 v2 pathology into a message model.
        /// </summary>
        /// <param name="payload">HL7 message.</param>
        /// <returns>HL7 message model.</returns>
        HL7GenericMessage ParseHl7Message(string payload);
        
        /// <summary>
        /// Transforms the HL7 v2 pathology message into a MHR compliant CDA document.
        /// </summary>
        /// <param name="message">HL7 pathology message model.</param>
        /// <param name="metadata">Additional data required from the source system.</param>
        /// <param name="reportData">Report data.</param>
        /// <returns>Transformed pathology report model</returns>
        PathologyTransformResult Transform(HL7GenericMessage message, PathologyMetadata metadata, byte[] reportData = null);

        /// <summary>
        /// Generates an XML document from the pathology report model.
        /// </summary>
        /// <param name="pathologyResultReport">Pathology report model.</param>
        /// <returns>Pathology XML document.</returns>
        XmlDocument Generate(PathologyResultReport pathologyResultReport);
    }
}