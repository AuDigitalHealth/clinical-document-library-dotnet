using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// Defines the qualifiers for Health Identifiers.
    /// </summary>
    public static class HIQualifiers
    {
        /// <summary>
        /// Qualifier for an Individual Healthcare Identifier.
        /// </summary>
        public static string IHIQualifier = "http://ns.electronichealth.net.au/id/hi/ihi/1.0/";

        /// <summary>
        /// Qualifier for a Health Provider Identifier - Organisation.
        /// </summary>
        public static string HPIOQualifier = "http://ns.electronichealth.net.au/id/hi/hpio/1.0/";

        /// <summary>
        /// Qualifier for a Health Provider Identifier - Individual.
        /// </summary>
        public static string HPIIQualifier = "http://ns.electronichealth.net.au/id/hi/hpii/1.0/";
    }

    /// <summary>
    /// Defines constants use for ELS (Endpoint Location Service) Cert Refs.
    /// </summary>
    public static class ELSCertRefContants
    {
        /// <summary>
        /// Qualifier for an ELS Cert Ref.
        /// </summary>
        public static string Qualifier = "http://ns.electronichealth.net.au/smd/qcr/use/payload/keyenc/2010";

        /// <summary>
        /// Type of an ELS Cert Ref.
        /// </summary>
        public static string Type = "http://ns.electronichealth.net.au/qcr/type/pem/2010";
    }
}
