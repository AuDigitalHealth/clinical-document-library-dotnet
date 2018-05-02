/*
 * Copyright 2012 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Security.Cryptography.X509Certificates;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// Helper class containing methods for certificate management.
    /// </summary>
    public static class X509CertificateUtil
    {
        /// <summary>
        /// The Oid which defines a certificate's subject key identifier.
        /// </summary>
        public const string SubjectKeyIdentifierOid = "2.5.29.14";

        /// <summary>
        /// Returns the subject key identifier of a certificate.
        /// </summary>
        /// <param name="certificate">Certificate that contains the subject key identifier.
        /// Cannot be null.</param>
        /// <returns>Subject key identifier or null when the subject key identifier 
        /// does not exist.</returns>   
        public static X509SubjectKeyIdentifierExtension GetSubjectKeyIdentifier(
          X509Certificate2 certificate)
        {
            return (X509SubjectKeyIdentifierExtension)
              certificate.Extensions[SubjectKeyIdentifierOid];
        }

        /// <summary>
        /// Throws an 'ArgumentException' is an certificate collection is null
        /// or empty.
        /// </summary>
        /// <param name="certs">Certificate collection to test. Cannot be null.
        /// </param>
        /// <param name="argumentName">Name of the argument. Cannot be null.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when the collection is
        /// null or empty.</exception>
        public static void CheckNotNullOrEmpty(X509Certificate2Collection certs,
          string argumentName)
        {
            if (certs == null || certs.Count == 0)
            {
                throw new ArgumentException("'" + argumentName +
                  "' cannot be null or empty.");
            }
        }

        /// <summary>
        /// Checks if two certificates are equal.
        /// </summary>
        /// <param name="cert1">Certificate 1.</param>
        /// <param name="cert2">Certificate 2.</param>
        /// <returns>True if equal otherwise false.</returns>
        public static bool AreEqual(X509Certificate2 cert1, X509Certificate2 cert2)
        {
            if (cert1 == cert2)
            {
                return true;
            }

            if (cert1.GetRawCertData().Length != cert2.GetRawCertData().Length)
            {
                return false;
            }

            for (int i = 0; i < cert1.GetRawCertData().Length; i++)
            {
                if (cert1.GetRawCertData()[i] != cert2.GetRawCertData()[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the SKI value of a certificate.
        /// </summary>
        /// <param name="cert">The certificate to obtain the SKI value for.</param>
        /// <returns>String containing the SKI value of the certificate.</returns>
        public static string GetCertificateSKI(X509Certificate2 cert)
        {
            X509SubjectKeyIdentifierExtension ext = new X509SubjectKeyIdentifierExtension();
            if (cert.Extensions["2.5.29.14"] != null)
            {
                X509Extension xex = cert.Extensions["2.5.29.14"];
                ext = (X509SubjectKeyIdentifierExtension)xex;

                if (ext != null) return ext.SubjectKeyIdentifier;
            }

            return null;
        }

        /// <summary>
        /// Gets a certificate from the Windows certificate repository.
        /// </summary>
        /// <param name="findValue">Find value.</param>
        /// <param name="findType">Find type.</param>
        /// <returns>Matching certificate.</returns>
        public static X509Certificate2 GetCertificate(String findValue,
          X509FindType findType)
        {
            return GetCertificate(findValue, findType, StoreName.My,
              StoreLocation.CurrentUser, true);
        }

        /// <summary>
        /// Gets a certificate from the Windows certificate repository.
        /// </summary>
        /// <param name="findValue">Find value.</param>
        /// <param name="findType">Find type.</param>
        /// <param name="storeName">Store name.</param>
        /// <param name="storeLocation">Store location.</param>
        /// <param name="valid">Valid certificate flag.</param>
        /// <returns>Matching certificate.</returns>
        public static X509Certificate2 GetCertificate(String findValue,
          X509FindType findType, StoreName storeName,
          StoreLocation storeLocation, bool valid)
        {
            X509Store certStore = new X509Store(storeName, storeLocation);
            certStore.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection foundCerts =
              certStore.Certificates.Find(findType, findValue, valid);
            certStore.Close();

            // Check if any certificates were found with the criteria
            if (foundCerts.Count == 0)
                throw new ArgumentException("Certificate was not found with criteria '" + findValue + "'");

            // Check if more than one certificate was found with the criteria
            if (foundCerts.Count > 1)
                throw new ArgumentException("More than one certificate found with criteria '" + findValue + "'");

            return foundCerts[0];
        }

        /// <summary>
        /// Gets the PEM string for a particular certificate.
        /// </summary>
        /// <param name="certificate">The certificate to obtain the PEM string from.</param>
        /// <returns>The PEM string of the certificate.</returns>
        public static string GetCertificatePEM(X509Certificate2 certificate)
        {
            Validation.ValidateArgumentRequired("certificate", certificate);
            Validation.ValidateArgumentRequired("certificate.RawData", certificate.RawData);

            return "-----BEGIN CERTIFICATE-----" + Convert.ToBase64String(certificate.RawData) + "-----END CERTIFICATE-----";
        }
    }
}
