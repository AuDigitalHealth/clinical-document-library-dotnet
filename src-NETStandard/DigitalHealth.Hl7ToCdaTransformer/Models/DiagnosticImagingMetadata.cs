using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    public class DiagnosticImagingMetadata
    {
        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>
        /// The accession number.
        /// </value>
        public Identifier AccessionNumber { get; set; }

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
        /// Gets or sets the reporting radiologist.
        /// </summary>
        /// <value>
        /// The reporting radiologist.
        /// </value>
        public ReportingRadiologist ReportingRadiologist { get; set; }
    }
}
