using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    /// <summary>
    /// Reporting Radiologist.
    /// </summary>
    public class ReportingRadiologist
    {
        /// <summary>
        /// Gets or sets the Hpii. This must be set if the HPII is not available from OBR32 of the HL7 V2 message.
        /// </summary>
        /// <value>
        /// The Hpii.
        /// </value>
        public string Hpii { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Occupation Role { get; set; }

        /// <summary>
        /// Gets or sets the organisation hpio.
        /// </summary>
        /// <value>
        /// The organisation hpio.
        /// </value>
        public string OrganisationHpio { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public List<IAddress> Address { get; set; }

        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>
        /// The contact details.
        /// </value>
        public List<ElectronicCommunicationDetail> ContactDetails { get; set; }
    }
}
