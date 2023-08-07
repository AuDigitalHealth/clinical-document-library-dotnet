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
using System.IO;
using System.Xml;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Newtonsoft.Json;

namespace Nehta.VendorLibrary.CDA.Sample
{
    /// <summary>
    /// This project is intended to demonstrate how a Shared Heath Summary CDA document can be created.
    /// 
    /// The project contains two samples, the first is designed to create a fully populated CDA document, including
    /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
    /// 
    /// The CDA model is split into three distinct sections, each of which needs to be created via the 
    /// SharedHealthSummary class, and then populated with data as appropriate. The three sections that need to be
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
    /// E.g. Shared Health Summary specific CDA sections or entries; for example Subject of Care.
    /// 
    /// The SCS Content typically contains information that is to be represented with the body of the document.
    /// </summary>
    public class SharedHealthSummarySample
    {
        #region Properties

        /// <summary>
        /// The OutputFolderPath for the CDA Document
        /// </summary>
        public static string OutputFolderPath { get; set; }

        /// <summary>
        /// The Output File Name And Path
        /// </summary>
        public static String OutputFileNameAndPath
        {
            get
            {
                return OutputFolderPath + @"\SharedHealthSummary.xml";
            }
        }

        /// <summary>
        /// The Structured File Attachment
        /// </summary>
        public static String StructuredFileAttachment
        {
            get
            {
                return OutputFolderPath + @"\attachment.pdf";
            }
        }

        #endregion

        /// <summary>
        /// This example populates only the mandatory Sections / Entries; as a result this sample omits all
        /// of the content within the body of the CDA document; as each of the sections within the body
        /// are optional.
        /// </summary>
        public XmlDocument PopulateSharedHealthSummarySample_1A(string fileName)
        {
            XmlDocument xmlDoc;

            var sharedHealthSummary = PopulateSharedHealthSummary(true);
            sharedHealthSummary.SCSContent = SharedHealthSummary.CreateSCSContent();

            // Hide Administrative Observations Section 
            sharedHealthSummary.ShowAdministrativeObservationsSection = false;

            sharedHealthSummary.IncludeLogo = false;

            var structuredBodyFileList = new List<ExternalData>();

            var structuredBodyFile = BaseCDAModel.CreateStructuredBodyFile();
            structuredBodyFile.Caption = "Structured Body File";
            structuredBodyFile.ExternalDataMediaType = MediaType.PDF;
            structuredBodyFile.Path = StructuredFileAttachment;
            structuredBodyFileList.Add(structuredBodyFile);

            sharedHealthSummary.SCSContent.StructuredBodyFiles = structuredBodyFileList;

            try
            {
                CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

                //Pass the E-Referral model into the GenerateEReferral method 
                xmlDoc = CDAGenerator.GenerateSharedHealthSummary(sharedHealthSummary);

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

        /// <summary>
        /// This sample populates only the mandatory Sections / Entries;
        /// </summary>
        public XmlDocument MinPopulatedSharedHealthSummarySample(string fileName)
        {
            XmlDocument xmlDoc;

            var sharedHealthSummary = PopulateSharedHealthSummary(true);

            try
            {
                //Pass the Shared health Summary model into the GenerateSharedHealthSummary method 
                xmlDoc = CDAGenerator.GenerateSharedHealthSummary(sharedHealthSummary);

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

        /// <summary>
        /// This sample populates both the mandatory and optional Sections / Entries; as a result this sample
        /// includes all of the sections within the body and each section includes at least one example for 
        /// each of its optional entries
        /// </summary>
        public XmlDocument MaxPopulatedSharedHealthSummarySample(string fileName)
        {
            XmlDocument xmlDoc;

            var sharedHealthSummary = PopulateSharedHealthSummary(false);

            try
            {
                //Pass the Shared health Summary model into the GenerateSharedHealthSummary method 
                xmlDoc = CDAGenerator.GenerateSharedHealthSummary(sharedHealthSummary);

                using (var writer = XmlWriter.Create(OutputFolderPath + @"\" + fileName, new XmlWriterSettings { Indent = true }))
                {
                    if (!fileName.IsNullOrEmptyWhitespace())
                    {
                        xmlDoc.Save(writer);
                        // Save as Json
                        var json = JsonConvert.SerializeObject(xmlDoc);
                        File.WriteAllText(OutputFolderPath + @"\" + fileName+ ".json", json);
                    }
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

        #region Private Methods

        /// <summary>
        /// This method populates a shared health summary model with either the mandatory sections only, or both 
        /// the mandatory and optional sections
        /// </summary>
        /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
        /// <returns>SharedHealthSummary</returns>
        internal static SharedHealthSummary PopulateSharedHealthSummary(Boolean mandatorySectionsOnly)
        {
            var sharedHealthSummary = SharedHealthSummary.CreateSharedHealthSummary(DocumentStatus.Final);

            // Include Logo
            sharedHealthSummary.IncludeLogo = true;
            sharedHealthSummary.LogoPath = OutputFolderPath;

            // Set Creation Time
            sharedHealthSummary.DocumentCreationTime = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Second);

            #region Setup and populate the CDA context model

            // Setup and populate the CDA context model
            var cdaContext = SharedHealthSummary.CreateCDAContext();

            // Document Id
            cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid());

            // CDA Context Version
            if (!mandatorySectionsOnly)
            {
                // Version
                cdaContext.Version = "1";

                // Set Id  
                cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid());
            }

            // Custodian
            cdaContext.Custodian = BaseCDAModel.CreateCustodian();
            GenericObjectReuseSample.HydrateCustodian(cdaContext.Custodian, mandatorySectionsOnly);

            sharedHealthSummary.ShowAdministrativeObservationsSection = !mandatorySectionsOnly;

            cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
            GenericObjectReuseSample.HydrateAuthenticator(cdaContext.LegalAuthenticator, mandatorySectionsOnly);

            sharedHealthSummary.CDAContext = cdaContext;

            #endregion

            #region Setup and Populate the SCS Context model
            // Setup and Populate the SCS Context model

            sharedHealthSummary.SCSContext = SharedHealthSummary.CreateSCSContext();

            sharedHealthSummary.SCSContext.Author = BaseCDAModel.CreateAuthor();
            GenericObjectReuseSample.HydrateAuthorV2(sharedHealthSummary.SCSContext.Author, mandatorySectionsOnly);

            // Subject of Care
            sharedHealthSummary.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
            GenericObjectReuseSample.HydrateSubjectofCare(sharedHealthSummary.SCSContext.SubjectOfCare, mandatorySectionsOnly, true);

            #endregion

            #region Setup and populate the SCS Content model
            // Setup and populate the SCS Content model

            sharedHealthSummary.SCSContent = SharedHealthSummary.CreateSCSContent();

            // Adverse reactions
            sharedHealthSummary.SCSContent.AdverseReactions = CreateAdverseReactions(mandatorySectionsOnly);

            // Reviewed medications
            sharedHealthSummary.SCSContent.Medications = CreateMedications(mandatorySectionsOnly);

            // Reviewed medical history
            sharedHealthSummary.SCSContent.MedicalHistory = CreateMedicalHistory(mandatorySectionsOnly);

            // Reviewed Immunizations
            sharedHealthSummary.SCSContent.Immunisations = CreateImmunisations(mandatorySectionsOnly);

            #endregion

            return sharedHealthSummary;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed medications section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedMedications object</returns>
        private static IMedications CreateMedications(Boolean mandatorySectionsOnly)
        {
            var medicationList = new List<IMedication>();

            var medications = SharedHealthSummary.CreateMedications();

            if (!mandatorySectionsOnly)
            {
                var medication = SharedHealthSummary.CreateMedication();
                medication.ClinicalIndication = "Diuretic induced hypokalemia";
                medication.Comment = "Taken with food";
                medication.Directions = BaseCDAModel.CreateStructuredText("2 tablets once daily oral");
                medication.Medicine = BaseCDAModel.CreateCodableText("5884011000036107", CodingSystem.AMTV3, "Span K 600 mg (potassium 8 mmol) modified release tablet");
                medicationList.Add(medication);                                                                 

                var medication1 = SharedHealthSummary.CreateMedication();
                medication1.ClinicalIndication = "Arthritis pain management";
                medication1.Comment = "Swallow whole";
                medication1.Directions = BaseCDAModel.CreateStructuredText("2 tablets three times per day");
                medication1.Medicine = BaseCDAModel.CreateCodableText("5848011000036106", CodingSystem.AMTV3, "Panadol Osteo 665 mg modified release tablet");
                medicationList.Add(medication1);

                var medication2 = SharedHealthSummary.CreateMedication();
                medication2.ClinicalIndication = "Fluid retention";
                medication2.Comment = "Take in the morning";
                medication2.Directions = BaseCDAModel.CreateStructuredText("1 tablet once daily oral");
                medication2.Medicine = BaseCDAModel.CreateCodableText("40288011000036101", CodingSystem.AMTV3, "Lasix 40 mg/4 mL injection, 4 mL ampoule");
                medicationList.Add(medication2);

                var medication3 = SharedHealthSummary.CreateMedication();
                medication3.ClinicalIndication = "COPD";
                medication3.Directions = BaseCDAModel.CreateStructuredText("1 inhalation per day");
                medication3.Medicine = BaseCDAModel.CreateCodableText("7113011000036100", CodingSystem.AMTV3, "Spiriva 18 microgram powder for inhalation, 1 capsule");
                medicationList.Add(medication3);

                var medication4 = SharedHealthSummary.CreateMedication();
                medication4.ClinicalIndication = "Depression";
                medication4.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: 3 times daily");
                medication4.Medicine = BaseCDAModel.CreateCodableText("32481000036107", CodingSystem.AMTV3, "Exatrust 25 mg tablet");
                medicationList.Add(medication4);

                var medication5 = SharedHealthSummary.CreateMedication();
                medication5.ClinicalIndication = "Depression";
                medication5.Directions = BaseCDAModel.CreateStructuredText("Dose:1, Frequency: as required");
                medication5.Medicine = BaseCDAModel.CreateCodableText("32481000036107", CodingSystem.AMTV3, "Exatrust 25 mg tablet");
                medicationList.Add(medication5);
                medications.Medications = medicationList;

            } else
            {
               medications.ExclusionStatement = SharedHealthSummary.CreateStatement();
               medications.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;  
            }

            return medications;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed medical history section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedMedicalHistory object</returns>
        private static IMedicalHistory CreateMedicalHistory(Boolean mandatorySectionsOnly)
        {
            var medicalHistory = BaseCDAModel.CreateMedicalHistory();

            if (!mandatorySectionsOnly)
            {
                // NOTE: This section demonstrates the different combinations of Procedure's, Medical History Item's & diagnosis
                var procedureList = new List<Procedure>();
                var medicalHistoryItems = new List<IMedicalHistoryItem>();
                var diagnosisList = new List<IProblemDiagnosis>();

                // Procedures
                var procedure1 = BaseCDAModel.CreateProcedure();
                procedure1.Comment = "L Procedure Comment";
                procedure1.ProcedureName = BaseCDAModel.CreateCodableText("301040004", CodingSystem.SNOMED, "Primary closed wire fixation of fracture");
                procedure1.ProcedureDateTime = CdaInterval.CreateLowHigh(new ISO8601DateTime(DateTime.Now.AddDays(-402), ISO8601DateTime.Precision.Day), 
                                                                         new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                procedure1.ShowOngoingInNarrative = true;
                procedureList.Add(procedure1);

                var prodcedure2 = BaseCDAModel.CreateProcedure();
                prodcedure2.Comment = "Comment";
                prodcedure2.ProcedureName = BaseCDAModel.CreateCodableText("388544006", CodingSystem.SNOMED, "Crab specific IgE antibody measurement");
                prodcedure2.ProcedureDateTime = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                procedureList.Add(prodcedure2);

                // Uncategorised Medical History Items
                var medicalHistoryItem = BaseCDAModel.CreateMedicalHistoryItem();
                medicalHistoryItem.DateTimeInterval = CdaInterval.CreateLow(new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day));
                medicalHistoryItem.ShowOngoingInNarrative = true;
                medicalHistoryItem.ItemDescription = "Uncategorised Medical History item description";
                medicalHistoryItem.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem);

                var medicalHistoryItem1 = BaseCDAModel.CreateMedicalHistoryItem();
                medicalHistoryItem1.ShowOngoingInNarrative = true;
                medicalHistoryItem1.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem1.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem1);

                var medicalHistoryItem2 = BaseCDAModel.CreateMedicalHistoryItem();
                var ongoingInterval2 = CdaInterval.CreateLowHigh(
                                       new ISO8601DateTime(DateTime.Now.AddDays(-400), ISO8601DateTime.Precision.Day),
                                       new ISO8601DateTime(DateTime.Now.AddDays(0), ISO8601DateTime.Precision.Day));

                medicalHistoryItem2.DateTimeInterval = ongoingInterval2;
                medicalHistoryItem2.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem2.ItemComment = "Item Comment";
                medicalHistoryItems.Add(medicalHistoryItem2);

                var medicalHistoryItem3 = BaseCDAModel.CreateMedicalHistoryItem();
                var ongoingInterval3 = CdaInterval.CreateHigh(new ISO8601DateTime(DateTime.Now.AddDays(0), ISO8601DateTime.Precision.Day));
                medicalHistoryItem3.DateTimeInterval = ongoingInterval3;
                medicalHistoryItem3.ItemDescription = "Uncategorised Medical History item description here";
                medicalHistoryItem3.ItemComment = "Item Comment 4";
                medicalHistoryItems.Add(medicalHistoryItem3);

                // Problem Diagnosis
                var diagnosis = BaseCDAModel.CreateProblemDiagnosis();

                // This fails Schematron - Ignore
                //diagnosis.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("57883007", CodingSystem.SNOMED, "Renin test diet",
                //"Renin test diet [Left, Right]", // NOTE: Qualifier codes must be provided as [value,value] in the codes original text for the narrative
                //new List<QualifierCode>
                //{
                //    // Qualifier Code
                //    BaseCDAModel.CreateQualifierCode(
                //        // Name
                //        BaseCDAModel.CreateCodableText("272741003", CodingSystem.SNOMED, "Laterality"),
                //        // Value
                //        BaseCDAModel.CreateCodableText("7771000", CodingSystem.SNOMED, "Left")
                //    ),

                //    // Qualifier Code
                //    BaseCDAModel.CreateQualifierCode(
                //        // Name
                //        BaseCDAModel.CreateCodableText("272741003", CodingSystem.SNOMED, "Laterality"),
                //        // Value
                //        BaseCDAModel.CreateCodableText("24028007", CodingSystem.SNOMED, "Right")
                //    )
                //});

                diagnosis.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("57883007", CodingSystem.SNOMED, "Renin test diet", "Renin test diet [Left, Right]");

                diagnosis.DateOfOnset = new ISO8601DateTime(DateTime.Now.AddYears(-2), ISO8601DateTime.Precision.Day);
                diagnosis.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Now.AddYears(-1), ISO8601DateTime.Precision.Day);
                diagnosis.Comment = "Comment";
                diagnosis.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis);

                var diagnosis1 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis1.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("72940007", CodingSystem.SNOMED, "Acute abscess of areola");
                diagnosisList.Add(diagnosis1);

                var diagnosis2 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis2.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("36083008", CodingSystem.SNOMED, "Sick sinus syndrome");
                diagnosisList.Add(diagnosis2);

                var diagnosis3 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis3.Comment = "Diuretic induced";
                diagnosis3.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("162311003", CodingSystem.SNOMED, "Heavy head");
                diagnosisList.Add(diagnosis3);

                var diagnosis4 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis4.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("258245003", CodingSystem.SNOMED, "G4 grade");
                diagnosis4.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("27 Feb 2007"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis4);

                var diagnosis5 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis5.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("396275006", CodingSystem.SNOMED, "Osteoarthritis");
                diagnosis5.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Jan 2000"), ISO8601DateTime.Precision.Day);
                diagnosis5.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis5);

                var diagnosis6 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis6.Comment = "Cementless";
                diagnosis6.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("43408002", CodingSystem.SNOMED, "Red reflex");
                diagnosis6.DateOfOnset = new ISO8601DateTime(DateTime.Parse("27 Feb 2007"), ISO8601DateTime.Precision.Day);
                diagnosis6.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis6);

                var diagnosis7 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis7.Comment = "T-score less than -3";
                diagnosis7.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("286114007", CodingSystem.SNOMED, "Does not do dusting");
                diagnosis7.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Jan 2007"), ISO8601DateTime.Precision.Day);
                diagnosis7.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis7);

                var diagnosis8 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis8.Comment = "Comment";
                diagnosis8.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("248515009", CodingSystem.SNOMED, "Lump in lid margin");
                diagnosis8.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis8.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("01 Sep 2010"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis8);

                var diagnosis9 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis9.Comment = "Diagnosis Comment";
                diagnosis9.ShowOngoingDateInNarrative = true;
                diagnosis9.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("200608005", CodingSystem.SNOMED, "Boil of back");
                diagnosis9.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis9.DateOfResolutionRemission = new ISO8601DateTime(DateTime.Parse("01 Sep 2010"), ISO8601DateTime.Precision.Day);
                diagnosisList.Add(diagnosis9);

                var diagnosis10 = SharedHealthSummary.CreateProblemDiagnosis();
                diagnosis10.ProblemDiagnosisIdentification = BaseCDAModel.CreateCodableText("267032009", CodingSystem.SNOMED, "Tired all the time");
                diagnosis10.DateOfOnset = new ISO8601DateTime(DateTime.Parse("01 Aug 2010"), ISO8601DateTime.Precision.Day);
                diagnosis10.ShowOngoingDateInNarrative = true;
                diagnosisList.Add(diagnosis10);

                medicalHistory.MedicalHistoryItems = medicalHistoryItems;
                medicalHistory.Procedures = procedureList;
                medicalHistory.ProblemDiagnosis = diagnosisList;

            }
            else
            {
                // NOTE : NotAsked is not a valid entry in context of a Shared Health Summary
                medicalHistory.ProblemDiagnosisExclusionStatement = BaseCDAModel.CreateStatement();
                medicalHistory.ProblemDiagnosisExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;

                medicalHistory.ProceduresExclusionStatement = BaseCDAModel.CreateStatement();
                medicalHistory.ProceduresExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }

            return medicalHistory;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed immunisations section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated Immunisations object</returns>
        private static Immunisations CreateImmunisations(Boolean mandatorySectionsOnly)
        {
            var immunisations = SharedHealthSummary.CreateReviewedImmunisations();

            if (!mandatorySectionsOnly)
            {
                var immunisation = SharedHealthSummary.CreateImmunisation();
                var immunisationList = new List<IImmunisation>();

                immunisation.DateTime = new ISO8601DateTime(DateTime.Now);
                immunisation.Medicine = BaseCDAModel.CreateCodableText("1004461000168106", CodingSystem.AMTV3, "M-M-R II powder for injection, 10 vials");
                immunisationList.Add(immunisation);

                var immunisation2 = SharedHealthSummary.CreateImmunisation();
                immunisation2.DateTime = new ISO8601DateTime(DateTime.Now);
                immunisation2.Medicine = BaseCDAModel.CreateCodableText("1004461000168106", CodingSystem.AMTV3, "M-M-R II powder for injection, 10 vials");
                immunisation2.SequenceNumber = 1;
                immunisationList.Add(immunisation2);

                immunisations.AdministeredImmunisation = immunisationList;
            }
            else
            {
                immunisations.ExclusionStatement = SharedHealthSummary.CreateStatement();
                immunisations.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }


            return immunisations;
        }

        /// <summary>
        /// Creates and Hydrates the reviewed adverse substance reactions section for the Shared Health Summary
        /// 
        /// Note: the data used within this method is intended as a guide and should be replaced.
        /// </summary>
        /// <returns>A Hydrated ReviewedAdverseSubstanceReactions object</returns>
        private static IAdverseReactions CreateAdverseReactions(Boolean mandatorySectionsOnly)
        {
            var adverseReactions = SharedHealthSummary.CreateReviewedAdverseSubstanceReactions();

            if (!mandatorySectionsOnly)
            {
                var reaction = SharedHealthSummary.CreateReaction();
                reaction.SubstanceOrAgent = BaseCDAModel.CreateCodableText("391739009", CodingSystem.SNOMED, "Aloe");
                reaction.ReactionEvent = BaseCDAModel.CreateReactionEvent();

                reaction.ReactionEvent.Manifestations = new List<ICodableText>
                                                            {
                                                                BaseCDAModel.CreateCodableText("20262006", CodingSystem.SNOMED, "Ataxia"),
                                                                BaseCDAModel.CreateCodableText("285599002", CodingSystem.SNOMED, "Trunk nerve lesion")
                                                            };

                reaction.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

                var reaction2 = SharedHealthSummary.CreateReaction();
                reaction2.SubstanceOrAgent = BaseCDAModel.CreateCodableText("372725003", CodingSystem.SNOMED, "Phenoxymethylpenicillin");
                reaction2.ReactionEvent = BaseCDAModel.CreateReactionEvent();

                reaction2.ReactionEvent.Manifestations = new List<ICodableText>
                                                         {
                                                                BaseCDAModel.CreateCodableText("20262006", CodingSystem.SNOMED, "Ataxia"),
                                                                BaseCDAModel.CreateCodableText("285599002", CodingSystem.SNOMED, "Trunk nerve lesion")
                                                         };

                reaction2.ReactionEvent.ReactionType = BaseCDAModel.CreateCodableText("419076005", CodingSystem.SNOMED, "Allergic reaction");

                adverseReactions.AdverseSubstanceReaction = new List<Reaction>
                {
                    reaction, 
                    reaction2
                };
            } 
            else
            {
                adverseReactions.ExclusionStatement = BaseCDAModel.CreateStatement();
                adverseReactions.ExclusionStatement.Value = NCTISGlobalStatementValues.NoneSupplied;
            }

            return adverseReactions;
        }

        #endregion
    }
}
