using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;

namespace DigitalHealth.Hl7ToCdaTransformer.Models
{
    /// <summary>
    /// Custodian
    /// </summary>
    public class Custodian
    {
        /// <summary>
        /// Gets or sets the organisation hpio.
        /// </summary>
        /// <value>
        /// The organisation hpio.
        /// </value>
        public string OrganisationHpio { get; set; }

        /// <summary>
        /// Gets or sets the name of the organisation.
        /// </summary>
        /// <value>
        /// The name of the organisation.
        /// </value>
        public string OrganisationName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public IAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>
        /// The contact details.
        /// </value>
        public ElectronicCommunicationDetail ContactDetails { get; set; }
    }
}
