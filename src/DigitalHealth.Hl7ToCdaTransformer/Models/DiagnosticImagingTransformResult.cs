using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nehta.VendorLibrary.CDA.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    /// <summary>
    /// Interim model between transformation and xml document.
    /// </summary>
    public class DiagnosticImagingTransformResult
    {
        /// <summary>
        /// Gets or sets the diagnostic imaging result report.
        /// </summary>
        /// <value>
        /// The diagnostic imaging result report.
        /// </value>
        public DiagnosticImagingReport DiagnosticImagingReport { get; set; }

        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public ReportAttachment Attachment { get; set; }
    }
}
