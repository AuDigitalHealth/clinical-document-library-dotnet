using Nehta.VendorLibrary.CDA.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    /// <summary>
    /// Interim model between transformation and xml document.
    /// </summary>
    public class PathologyTransformResult
    {
        /// <summary>
        /// Gets or sets the pathology result report.
        /// </summary>
        /// <value>
        /// The pathology result report.
        /// </value>
        public PathologyResultReport PathologyResultReport { get; set; }

        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public ReportAttachment Attachment { get; set; }
    }
}