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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// A child of SignedXml with an overridden GetIdElement function which searches for elements by
    /// "xml:id" attribute instead of "id".
    /// </summary>
    public class NehtaSignedXml : SignedXml
    {
        const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

        /// <summary>
        /// Instantiates an instance of NehtaSignedXml.
        /// </summary>
        /// <param name="document">The xml document with which to create the NehtaSignedXml instance.</param>
        public NehtaSignedXml(XmlDocument document)
            : base(document)
        { }

        /// <summary>
        /// Gets the element with the ID value.
        /// </summary>
        /// <param name="document">SOAP document.</param>
        /// <param name="idValue">ID value.</param>
        /// <returns>Element with the matching ID.</returns>
        public override XmlElement GetIdElement(XmlDocument document, string idValue)
        {
            // Create the namespace manager for XPathing
            XmlNamespaceManager xnm = new XmlNamespaceManager(document.NameTable);
            xnm.AddNamespace("xml", XmlNamespace);

            // Find the element with the ID
            if (document.DocumentElement != null)
            {
                XmlNode foundElem = document.DocumentElement.SelectSingleNode("//*[@xml:id = '" + idValue + "']", xnm);

                if (foundElem == null)
                {
                    throw new ApplicationException("Error finding an element: Element with xml:id '" + idValue + "' not found");
                }

                // Return the found element
                return (XmlElement)foundElem;
            } else return null;
        }
    }
}
