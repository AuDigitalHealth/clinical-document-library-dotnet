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
using System.Collections.Generic;
using System.Xml;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;

namespace CDA.PSML
{
    /// <summary>
    /// This project is intended to demonstrate how ACSP Sample CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// ACSP class, and then populated with data as appropriate. The three sections that need to be
    /// created and hydrated with data are:
    /// 
    ///     CDA Context (Clinical Document Architecture - Context)
    ///     SCS Context (Structured Document Template - Context)
    ///     SCS Content (Structured Document Template - Content)
    /// 
    /// The CDA Context typically contains information that is to be represented within the header of the document
    /// that is not encapsulated with the SCS context.
    /// E.g. Generic CDA sections or entries; for example custodian.
    /// 
    /// The SCS Context typically contains information that is to be represented within the header of the document
    /// and relates specifically to the type of document that is to be created.
    /// E.g. E-Referral specific CDA sections or entries; for example Subject of Care.
    /// 
    /// The SCS Content typically contains information that is to be represented with the body of the document.
    /// </summary>
  
    public class ACSPSample
    {
        #region Properties

        public static string OutputFolderPath { get; set; }

        public static String StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

        // Note: Place this in any string field and and this will insert a break
        private const String DELIMITERBREAK = "<BR>";

        #endregion

        #region Create and Generate CDA

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries.
        /// </summary>
        public XmlDocument PopulatedACSP_SupportAtHome_Sample_1A(string fileName)
        {
            XmlDocument xmlDoc = null;

            var acsp = PopulateACSP_SAH_1A(CDADocumentType.AgedCareSupportPlanSupportAtHome);

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the Child Parent Questionnaire model into the GeneratePCML method 
                xmlDoc = CDAGenerator.GenerateACSP(acsp, CDADocumentType.AgedCareSupportPlanSupportAtHome);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings { Indent = true }))
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

        #endregion

        #region Populate Methods
        /// <summary>
        /// This method populates an PCML model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>PCML</returns>
        public static Nehta.VendorLibrary.CDA.Common.ACSP PopulateACSP_SAH_1A(CDADocumentType cdaDocumentType)
        {
            var acsp = Nehta.VendorLibrary.CDA.Common.ACSP.CreateACSP();

              // Include Logo
            acsp.IncludeLogo = false;

              // Set Creation Time
            acsp.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            ICDAContextPCML cdaContext = Nehta.VendorLibrary.CDA.Common.PCML.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodianDOHAC(cdaContext.Custodian);

            acsp.CDAContext = cdaContext;
            #endregion

            
            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            acsp.SCSContext = Nehta.VendorLibrary.CDA.Common.PCML.CreateSCSContext();

            // Author
            var authorHealthcareProvider = BaseCDAModel.CreateAuthorAuthoringDevice();
            GenericObjectReuseSample.HydrateAuthorDeviceDOHAC(authorHealthcareProvider);
            acsp.SCSContext.Author = authorHealthcareProvider;
            
            // Individual
            acsp.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCareDOHAC(acsp.SCSContext.SubjectOfCare);


            // Mandatory Participant - TODO: REMOVE REQUIRED
            //acsp.SCSContext.Participant = new List<IParticipationPersonOrOrganisation> {  };

            #endregion


            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model
            acsp.SCSContent = Nehta.VendorLibrary.CDA.Common.PCML.CreateSCSContent();

            acsp.SCSContent.EncapsulatedData = BaseCDAModel.CreateEncapsulatedData();

            ExternalData report = BaseCDAModel.CreateExternalData();
            report.ExternalDataMediaType = MediaType.PDF;
            report.Path = StructuredFileAttachment;
            report.Caption = "Documentation";

            acsp.SCSContent.EncapsulatedData.ExternalData = report;
            #endregion

            return acsp;
        }

        #endregion
    }
}
