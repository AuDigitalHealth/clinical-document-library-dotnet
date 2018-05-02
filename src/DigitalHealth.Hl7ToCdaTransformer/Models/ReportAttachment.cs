using Nehta.VendorLibrary.CDA.Generator.Enums;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    /// <summary>
    /// Object that represents attachment in the document.
    /// </summary>
    public class ReportAttachment
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public MediaType MediaType { get; set; } = MediaType.PDF;
    }
}
