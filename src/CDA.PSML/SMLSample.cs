/*
 * Copyright 2013 NEHTA
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
using System.Xml;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;

namespace CDA.PSML
{
    /// <summary>
    /// This project is intended to demonstrate how a SML (Shared Medicines List) Sample CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    ///
    /// This NEW IG document uses FHIR based models (2020) as opposed to ALL previous CDA documents which
    /// were modeled on DCMs which produced CDA Context, SCS Context, SCS Content.
    /// This example will detail the mapping into these DCM type models still.
    /// 
    /// More details to follow
    /// 
    /// </summary>

    public class SMLSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        #endregion

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries.
        /// </summary>
        public XmlDocument MinPopulatedSMLAuthorHealthcareProviderSample_3A(string fileName, string templatepackageid, string templatepackagever, bool incHpii = true)
        {
            var sml = SmlSampleCreator.PopulateSML_3A(templatepackageid, templatepackagever, true, false, incHpii);
            return GenerateSmlDocument(sml, fileName, incHpii);
        }

        public XmlDocument MaxPopulatedSMLAuthorHealthcareProviderSample_3A(string fileName, string templatepackageid, string templatepackagever, bool incHpii = true)
        {
            var sml = SmlSampleCreator.PopulateSML_3A(templatepackageid, templatepackagever, false, true, incHpii);
            return GenerateSmlDocument(sml, fileName, incHpii);
        }

        public XmlDocument MidMinPopulatedSMLAuthorHealthcareProviderSample_3A(string fileName, string templatepackageid, string templatepackagever, bool incHpii = true)
        {
            var sml = SmlSampleCreator.PopulateSML_3A(templatepackageid, templatepackagever, true, true, incHpii);
            return GenerateSmlDocument(sml, fileName, incHpii);
        }

        public XmlDocument MidMaxPopulatedSMLAuthorHealthcareProviderSample_3A(string fileName, string templatepackageid, string templatepackagever, bool incHpii = true)
        {
            var sml = SmlSampleCreator.PopulateSML_3A(templatepackageid, templatepackagever, false, false, incHpii);
            return GenerateSmlDocument(sml, fileName, incHpii);
        }

        public XmlDocument GenerateSmlDocument(SML sml, string fileName, bool incHpii = true)
        {
            XmlDocument xmlDoc = null;

            try
            {
                OutputFolderPath = ".";
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the SML model into the GenerateSML method 
                xmlDoc = CDAGenerator.GenerateSML(sml, incHpii);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName,
                    new XmlWriterSettings { Indent = true }))
                {
                    if (!fileName.IsNullOrEmptyWhitespace()) xmlDoc.Save(writer);
                }
            }
            catch (ValidationException ex)
            {
                //Catch any validation exceptions
                var validationMessages = ex.GetMessagesString();

                //Handle any validation errors as appropriate.
                throw;
            }

            return xmlDoc;
        }


        #region Populate Methods

        // See SmlSampleCreator.cs

        #endregion
    }
}

