using System;
using System.Linq;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    /// <summary>
    /// Metadata object that encapsulates any external input required. 
    /// <remarks>This may be used to pass in data to the PathologyTransformer Service before transforming. </remarks>
    /// </summary>
    public class PathologyMetadata
    {
        /// <summary>
        /// Gets or sets the report identifier.
        /// </summary>
        /// <value>
        /// The report identifier.
        /// </value>
        public Identifier ReportIdentifier { get; set; }

        // Optional - Check with Phil if we should take this out
        /// <summary>
        /// Gets or sets the requester order identifier.
        /// </summary>
        /// <value>
        /// The requester order identifier.
        /// </value>
        public Identifier RequesterOrderIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the author organisation hpio.
        /// </summary>
        /// <value>
        /// The author organisation hpio.
        /// </value>
        public string AuthorOrganisationHpio { get; set; }

        /// <summary>
        /// Gets or sets the reporting pathologist.
        /// </summary>
        /// <value>
        /// The reporting pathologist.
        /// </value>
        public ReportingPathologist ReportingPathologist { get; set; }
        
    }
}
