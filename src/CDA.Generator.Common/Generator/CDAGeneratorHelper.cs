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
using System.Configuration;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using CDA.Generator.Common.CDAModel.Entities;
using CDA.Generator.Common.Common.Time;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.SCSModel.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.HL7.CDA;

using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;

using CDA.Generator.Common.SCSModel.MedicareOverview.Entities;
using CDA.Generator.Common.SCSModel.ConsumerAchievements.Entities;
using CDA.Generator.Common.SCSModel.ATS.ETP.Enum;
using CDA.Generator.Common.SCSModel.ATS.ETP.Interfaces;
using CDA.Generator.Common.SCSModel.ATS.ETP.Entities;
using CDA.Generator.Common.SCSModel.CeHR.Entities;

using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;
using CDA.Generator.Common.Common.Attributes;
using CDA.Generator.Common.Common.Time.Enum;
using RequestedService = Nehta.VendorLibrary.CDA.SCSModel.Common.RequestedService;
using CDA.Generator.Common.SCSModel.Common.Entities;
using CDA.Generator.Common.SCSModel.ServiceReferral.Entities;
using CDA.Generator.Common.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using AdverseReactions = Nehta.VendorLibrary.CDA.SCSModel.Common.AdverseReactions;
using Medications = Nehta.VendorLibrary.CDA.SCSModel.Common.Medications;
using Participation = Nehta.VendorLibrary.CDA.SCSModel.Common.Participation;
using ProblemDiagnosis = Nehta.VendorLibrary.CDA.SCSModel.Common.ProblemDiagnosis;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This helper class is used to aid in converting the various SCS and CDA model objects 
    /// into an actual CDA Document.  
    /// </summary>
    public static partial class CDAGeneratorHelper
    {
        #region Constants

        private const String HEALTH_IDENTIFIER_QUALIFIER = "1.2.36.1.2001.1003.0.";
        private const String NO_ENTRIES_MESSAGE = "No data recorded for this section.";

        // Dates
        private const String DATE_TIME_FORMAT = "yyyyMMddHHmmsszz";
        private const String DATE_TIME_SHORT_FORMAT = "yyyyMMdd";

        #endregion

        #region internal Methods

        #region Create Document Methods

        /// <summary>
        /// Creates a CDA Clinical Document object and sets the document type, version, ID etc
        /// </summary>
        /// <param name="documentCreationTimeDateTime">The date / time the document is effective from</param>
        /// <param name="cdaDocumentType">The document type, E.g. SharedHealthSummary</param>
        /// <param name="setId">The set ID for this CDA document</param>
        /// <param name="version">The document version</param>
        /// <param name="documentStatus">The document status</param>
        /// <param name="documentId">The ID of the clinical document</param>
        /// <param name="title">The title</param>
        /// <param name="templatePackageId">The templatePackageId optional</param>
        /// <param name="subTypeTitle">For DS,ES,SL subtypes titles can be included which override the Title as they must match/param>
        /// <returns>POCD_MT000040ClinicalDocument</returns>
        internal static POCD_MT000040ClinicalDocument CreateDocument(ISO8601DateTime documentCreationTimeDateTime,
            CDADocumentType cdaDocumentType, Identifier documentId, Identifier setId,
            String version, DocumentStatus? documentStatus, string title, Identifier templatePackageId = null, string subTypeTitle = null)
        {
            var typeID = new POCD_MT000040InfrastructureRoottypeId
            { extension = "POCD_HD000040", root = "2.16.840.1.113883.1.3" };

            var templateIds = new List<II>
            {
                // CDA Spec
                CreateIdentifierElement(
                    cdaDocumentType.GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                    cdaDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Version), null),

                // Rendering Spec
                CreateIdentifierElement(
                    CDADocumentType.CdaRenderingSpecification.GetAttributeValue<NameAttribute, string>(x =>
                        x.TemplateIdentifier),
                    CDADocumentType.CdaRenderingSpecification.GetAttributeValue<NameAttribute, string>(x => x.Version),
                    null),
            };

            //Add another entry here for PSML v1 = Only add this for CDA documents that use DH_CoreLevelOne_CDA_Implementation_Guide_v1.1 dv011 as a base
            if (cdaDocumentType == CDADocumentType.PharmacistSharedMedicinesList)
            {
                templateIds.Add(CreateIdentifierElement(
                    CDADocumentType.CoreLevelOne.GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                    CDADocumentType.CoreLevelOne.GetAttributeValue<NameAttribute, string>(x => x.Version), null));
            }

            // Add 3 entries here for SML = PSMLv2
            if (cdaDocumentType == CDADocumentType.PharmacistSharedMedicinesListV2HPII ||
                cdaDocumentType == CDADocumentType.PharmacistSharedMedicinesListV2NoHPII)
            {
                templateIds.Add(CreateIdentifierElement(
                    CDADocumentType.ClinicalDocumentTemplate.GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                    CDADocumentType.ClinicalDocumentTemplate.GetAttributeValue<NameAttribute, string>(x => x.Version), null));

                templateIds.Add(CreateIdentifierElement(
                    CDADocumentType.CommonConformanceProfileV17.GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                    CDADocumentType.CommonConformanceProfileV17.GetAttributeValue<NameAttribute, string>(x => x.Version), null));

                // HPII or No HPII
                if (cdaDocumentType == CDADocumentType.PharmacistSharedMedicinesListV2HPII)
                {
                    templateIds.Add(CreateIdentifierElement(
                        CDADocumentType.PharmacistSharedMedicinesListV2ConformanceProfileHPII
                            .GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                        CDADocumentType.PharmacistSharedMedicinesListV2ConformanceProfileHPII
                            .GetAttributeValue<NameAttribute, string>(x => x.Version), null));
                }
                else
                {
                    templateIds.Add(CreateIdentifierElement(
                        CDADocumentType.PharmacistSharedMedicinesListV2ConformanceProfileNoHPII
                            .GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                        CDADocumentType.PharmacistSharedMedicinesListV2ConformanceProfileNoHPII
                            .GetAttributeValue<NameAttribute, string>(x => x.Version), null));
                }
            }

            // Add Template Package Id - Just SharedMedsList (SML) for now
            if (templatePackageId != null)
            {
                templateIds.Add(CreateIdentifierElement(templatePackageId.Root, templatePackageId.Extension, null));
            }

            // Add another entry here for AdvanceCareInformation = GofC
            if (cdaDocumentType == CDADocumentType.AdvanceCareInformation && title == "Goals of Care Document")
            {
                // Add another entry here for GofC
                templateIds.Add(CreateIdentifierElement(
                    CDADocumentType.GoalsOfCare.GetAttributeValue<NameAttribute, string>(x => x.TemplateIdentifier),
                    CDADocumentType.GoalsOfCare.GetAttributeValue<NameAttribute, string>(x => x.Version), null));
            }

            // To support DS,ES,SL subtypes, Check if doc subtype title provided
            if (!string.IsNullOrWhiteSpace(subTypeTitle))
            {
                // Force Title to be the same as subTypeTitle
                title = subTypeTitle;
            }
            else
            {
                subTypeTitle = null;
            }

            var clinicalDocument = new POCD_MT000040ClinicalDocument
            {
                typeId = typeID,
                title = title.IsNullOrEmptyWhitespace()
                    ? CreateStructuredText(cdaDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Title))
                    : CreateStructuredText(title),
                templateId = templateIds.ToArray(),
                id = documentId != null
                    ? CreateIdentifierElement(documentId)
                    : CreateIdentifierElement(CreateOid(), null),
                setId = setId != null ? CreateIdentifierElement(setId) : null,

                code = CreateCodedWithExtensionElement(
                    cdaDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    (CodingSystem)Enum.Parse(typeof(CodingSystem),
                        cdaDocumentType.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                    cdaDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Name), subTypeTitle, null, null),

                effectiveTime = documentCreationTimeDateTime != null
                    ? CreateTimeStampElementIso(documentCreationTimeDateTime)
                    : CreateTimeStampElementIso(new ISO8601DateTime(DateTime.Now)),
                confidentialityCode = CreateCodedWithExtensionElement(null, null, null, null, null, null, null,
                    NullFlavour.NotApplicable),
                languageCode = CreateCodeSystem("en-AU", null, null, null, null, null),
                completionCode = documentStatus != null
                    ? CreateCodedWithExtensionElement(
                        documentStatus.GetAttributeValue<NameAttribute, string>(x => x.Code),
                        CodingSystem.NCTISDocumentStatusValues,
                        documentStatus.GetAttributeValue<NameAttribute, string>(x => x.Name), null, null, null)
                    : null,
                component = new POCD_MT000040Component2()
            };

            if (!string.IsNullOrEmpty(version))
            {
                clinicalDocument.versionNumber = new INT { value = version };
            }

            return clinicalDocument;
        }

        /// <summary>
        /// Creates a CDA Clinical Document object and sets the document type, version, ID etc
        /// </summary>
        /// <param name="parentDocument">ParentDocument</param>
        /// <returns>POCD_MT000040ClinicalDocument</returns>
        internal static List<POCD_MT000040RelatedDocument> CreateRelatedDocument(List<ParentDocument> parentDocument)
        {
            List<POCD_MT000040RelatedDocument> relatedDocumentList = null;

            if (parentDocument.Any())
            {
                relatedDocumentList = new List<POCD_MT000040RelatedDocument>();

                foreach (var document in parentDocument)
                {
                    var relatedDocument = new POCD_MT000040RelatedDocument();

                    if (document.ReleatedDocumentType.HasValue)
                        relatedDocument.typeCode = (x_ActRelationshipDocument)Enum.Parse(
                            typeof(x_ActRelationshipDocument),
                            document.ReleatedDocumentType.Value.GetAttributeValue<NameAttribute, string>(x => x.Code));

                    relatedDocument.parentDocument = new POCD_MT000040ParentDocument();

                    if (document.DocumentId != null)
                        relatedDocument.parentDocument.id = new[]
                        {
                            CreateIdentifierElement(document.DocumentId)
                        };

                    if (document.DocumentType.HasValue)
                        relatedDocument.parentDocument.code = CreateCodedWithExtensionElement(
                            document.DocumentType.Value.GetAttributeValue<NameAttribute, string>(x => x.Code),
                            (CodingSystem)Enum.Parse(typeof(CodingSystem),
                                document.DocumentType.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                            document.DocumentType.Value.GetAttributeValue<NameAttribute, string>(x => x.Name), null,
                            null, null);

                    // 0..1
                    relatedDocument.parentDocument.text =
                        new ED { nullFlavor = NullFlavor.NA, nullFlavorSpecified = true };

                    if (document.SetId != null)
                        relatedDocument.parentDocument.setId = CreateIdentifierElement(document.SetId);

                    if (!document.VersionNumber.IsNullOrEmptyWhitespace())
                        relatedDocument.parentDocument.versionNumber = new INT { value = document.VersionNumber };

                    relatedDocumentList.Add(relatedDocument);
                }
            }

            return relatedDocumentList;
        }

        #endregion

        #region Internal Methods - Create Components

        #region InFulfillmentOf

        internal static POCD_MT000040InFulfillmentOf CreateinFulfillmentOf(OrderDetails orderDetails)
        {
            POCD_MT000040InFulfillmentOf inFulfillmentOf = null;

            var identifiers = new List<II>();

            if (orderDetails.RequesterOrderIdentifier != null)
                identifiers.Add(CreateIdentifierElement(orderDetails.RequesterOrderIdentifier));

            if (orderDetails.AccessionNumber != null)
                identifiers.Add(CreateIdentifierElement(orderDetails.AccessionNumber));

            if (identifiers.Any() || orderDetails.RequestedTestName != null)
                inFulfillmentOf = new POCD_MT000040InFulfillmentOf
                {
                    typeCode = ActRelationshipFulfills.FLFS,
                    order = new POCD_MT000040Order
                    {
                        classCode = ActClassRoot.ACT,
                        moodCode = ActMood.RQO,
                        id = identifiers.Any() ? identifiers.ToArray() : null,
                        code = orderDetails.RequestedTestName != null
                            ? CreateCodedWithExtensionElement(orderDetails.RequestedTestName)
                            : null
                    }
                };

            return inFulfillmentOf;
        }

        #endregion

        #region NonXMLBody

        /// <summary>
        /// Creates an XML element that contains a CDA reference to the  eternal data that was passed into this method
        /// </summary>
        /// <param name="externalData">externalData</param>
        /// <returns>an XmlElement containing a CDA reference to the external data</returns>
        internal static POCD_MT000040NonXMLBody CreateNonXmlComponent(ExternalData externalData)
        {
            POCD_MT000040NonXMLBody nonXmlBody = null;

            if (externalData != null)
            {
                nonXmlBody = new POCD_MT000040NonXMLBody { text = CreateEncapsulatedData(externalData) };
            }

            return nonXmlBody;
        }

        #endregion

        #region Component1

        internal static POCD_MT000040Component1 CreateComponentOf(ISO8601DateTime effectiveDateTime)
        {
            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();

            component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(null, null, null, null,
                effectiveDateTime != null ? effectiveDateTime.ToString() : String.Empty, null);
            return component;
        }

        internal static POCD_MT000040Component1 CreateComponentOf(DateTime? effectiveDateTime)
        {
            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();

            component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(null, null, null, null,
                effectiveDateTime.HasValue ? effectiveDateTime.Value.ToString(DATE_TIME_FORMAT) : String.Empty, null);
            return component;
        }

        internal static POCD_MT000040Component1 CreateComponentOf(CdaInterval duration)
        {
            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();

            component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(duration);
            return component;
        }

        internal static POCD_MT000040Component1 CreateComponentOf(CdaInterval duration, NullFlavour? nullFlavour)
        {
            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();

            NullFlavor? nullFlavor = null;
            if (nullFlavour.HasValue)
                nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                    nullFlavour.GetAttributeValue<NameAttribute, string>(x => x.Code));

            component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(duration, nullFlavor);



            return component;
        }

        #endregion

        #region Component3

        /// <summary>
        /// Creates a administration observations component
        /// </summary>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <param name="entitlements">A dictionary of entitlements</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="patientId">Patient ID</param>
        /// <param name="showTemplateId">Show Template Id</param>
        /// <param name="narrativeGenerator">The narrative generator with which to generate the narrative for this section / component</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateAdministrativeObservations(
            IParticipationSubjectOfCare subjectOfCareParticipation, Dictionary<string, List<Entitlement>> entitlements,
            StrucDocText customNarrative, String patientId, Boolean? showTemplateId,
            INarrativeGenerator narrativeGenerator)
        {
            var administrativeObservations = CreateAdministrativeObservations(null, subjectOfCareParticipation,
                customNarrative, patientId, narrativeGenerator);

            List<Coverage2> coverage2 = null;

            if (administrativeObservations.section.coverage2 != null)
            {
                coverage2 = administrativeObservations.section.coverage2.ToList();
            }

            // The default has been to include component/section[admin_obs]/templateId/@root="1.2.36.1.2001.1001.101.102.16080"
            // Therefore this override clears the template id value
            if (showTemplateId.HasValue && showTemplateId.Value == false)
            {
                administrativeObservations.section.templateId = null;
            }

            foreach (KeyValuePair<string, List<Entitlement>> kvp in entitlements)
            {
                if (coverage2 == null)
                    coverage2 = new List<Coverage2>();

                coverage2.AddRange(CreateEntitlements(kvp.Value, kvp.Key, RoleClass.ASSIGNED, ParticipationType.HLD)
                    .ToArray());
            }

            if (coverage2 != null && coverage2.Any())
                administrativeObservations.section.coverage2 = coverage2.ToArray();

            return administrativeObservations;
        }

        /// <summary>
        /// Creates a administration observations component
        /// </summary>
        /// <param name="authorPersonParticipation">IParticipationAuthorPerson</param>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="patientId">Patient ID</param>
        /// <param name="narrativeGenerator">The narrative generator with which to generate the narrative for this section / component</param>
        /// <param name="coverages">Entitlements for other participants </param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateAdministrativeObservations(
            IParticipationDocumentAuthor authorPersonParticipation,
            IParticipationSubjectOfCare subjectOfCareParticipation, StrucDocText customNarrative, string patientId,
            INarrativeGenerator narrativeGenerator, List<Coverage2> coverages = null)
        {
            var component = new POCD_MT000040Component3
            {
                section = CreateSectionCodeTitle("102.16080", CodingSystem.NCTIS, "Administrative Observations"),
            };

            var entryList = new List<POCD_MT000040Entry>();

            //DEMOGRAPHICS
            if (subjectOfCareParticipation != null && subjectOfCareParticipation.Participant != null &&
                subjectOfCareParticipation.Participant.Person != null)
            {
                var person = subjectOfCareParticipation.Participant.Person;

                if (person.DateOfBirthCalculatedFromAge.HasValue || person.DateOfBirthAccuracyIndicator != null ||
                    person.Age.HasValue || person.AgeAccuracyIndicator != null || person.BirthPlurality.HasValue ||
                    person.DateOfDeathAccuracyIndicator != null ||
                    subjectOfCareParticipation.Participant.Entitlements != null)
                {
                    //Date of Birth calculated from age
                    entryList.Add(
                        CreateAdministrationObservationDateOfBirthCalculatedFromAge(person
                            .DateOfBirthCalculatedFromAge));

                    //Date of Birth accuracy indicator
                    entryList.Add(CreateAdministrationObservationAge(person.DateOfBirthAccuracyIndicator,
                        "Date of Birth", "102.16234"));

                    //Age Accuracy Indicator
                    entryList.Add(CreateAdministrationObservationAgeAccuracyIndicator(person.AgeAccuracyIndicator));

                    //Birth Plurality
                    entryList.Add(CreateAdministrationObservationBirthPlurality(person.BirthPlurality));

                    //Age
                    entryList.Add(CreateAdministrationObservationAge(person.Age, person.AgeUnitOfMeasure));

                    //Date of Death accuracy indicator
                    entryList.Add(CreateAdministrationObservationAge(person.DateOfDeathAccuracyIndicator,
                        "Date of Death", "102.16252"));

                    //Mothers Original Family Name
                    entryList.Add(
                        CreateAdministrationObservationMothersOriginalFamilyName(person.MothersOriginalFamilyName));

                    //Source Of Death Notification
                    entryList.Add(
                        CreateAdministrationObservationSourceOfDeathNotification(person.SourceOfDeathNotification));

                    // Interpreter Required Alert
                    if (person.InterpreterRequired?.PreferredLanguage != null &&
                        person.InterpreterRequired.PreferredLanguage.Any())
                    {
                        entryList.Add(CreateInterpreterRequiredAlert(person.InterpreterRequired));
                    }

                    if (coverages == null)
                    {
                        coverages = new List<Coverage2>();
                    }

                    // Entitlements
                    if (patientId != null && subjectOfCareParticipation.Participant.Entitlements != null &&
                        subjectOfCareParticipation.Participant.Entitlements.Any())
                        coverages.AddRange(CreateEntitlements(subjectOfCareParticipation.Participant.Entitlements,
                            patientId, RoleClass.PAT, ParticipationType.BEN));

                    // Entitlements
                    if (authorPersonParticipation != null && authorPersonParticipation.Participant != null &&
                        authorPersonParticipation.Participant.Entitlements != null &&
                        authorPersonParticipation.Participant.Entitlements.Any())
                        coverages.AddRange(CreateEntitlements(authorPersonParticipation.Participant.Entitlements,
                            authorPersonParticipation.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                            ParticipationType.HLD));

                    if (coverages.Any())
                        component.section.coverage2 = coverages.ToArray();
                }

                //NARRATIVE
                if (narrativeGenerator != null)
                {
                    component.section.text = new StrucDocText();
                    component.section.text = customNarrative ??
                                             narrativeGenerator.CreateNarrative(subjectOfCareParticipation, patientId,
                                                 true, null, null);
                }
                else
                {
                    component.section.text = null;
                    component.section.title = null;
                }

                component.section.entry = entryList.ToArray();
            }

            return component;
        }

        /// <summary>
        /// Creates an XML element that contains a CDA reference to the eternal data that was passed into this method
        /// </summary>
        /// <param name="externalDataList">externalData</param>
        /// <param name="narrativeGenerator">narrativeGenerator</param>
        /// <returns>an XmlElement containing a CDA reference to the external data</returns>
        internal static POCD_MT000040Component3 CreateStructuredBodyFileComponent(List<ExternalData> externalDataList,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (externalDataList != null)
            {
                component = new POCD_MT000040Component3 { section = new POCD_MT000040Section() };

                var entryList = new List<POCD_MT000040Entry>();

                externalDataList.ForEach(externalData => entryList.Add(new POCD_MT000040Entry
                { observationMedia = CreateObservationMedia(externalData) }));

                component.section.entry = entryList.ToArray();
                component.section.title = CreateStructuredText("Attached Content", null);
                component.section.text = narrativeGenerator.CreateNarrative(externalDataList);
            }

            return component;
        }

        /// <summary>
        /// Creates an Component narratives from the NarrativeOnlyDocument class
        /// </summary>
        /// <param name="narrativeOnlyDocuments">NarrativeOnlyDocument</param>
        /// <returns>an XmlElement containing a CDA reference to the external data</returns>
        internal static List<POCD_MT000040Component3> CreateNarrativeOnlyDocument(
            List<NarrativeOnlyDocument> narrativeOnlyDocuments)
        {
            List<POCD_MT000040Component3> components = null;

            if (narrativeOnlyDocuments != null && narrativeOnlyDocuments.Any())
            {
                components = narrativeOnlyDocuments.Select(narrativeOnlyDocument => new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        title = CreateStructuredText(narrativeOnlyDocument.Title),
                        text = narrativeOnlyDocument.Narrative
                    }

                }).ToList();
            }

            return components;
        }

        /// <summary>
        /// Creates a component for an E-Referral; specify the reason, time and duration of the referral.
        /// </summary>
        /// <param name="dateTime">The date / time associated with this referral</param>
        /// <param name="duration">The duration of the referral</param>
        /// <param name="reason">The reason for this referral</param>
        /// <param name="customNarrative">Provides the ability to supply a custom narrative </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponentForReferralReason(ISO8601DateTime dateTime,
            CdaInterval duration,
            string reason,
            StrucDocText customNarrative,
            INarrativeGenerator narrativeGenerator)
        {
            //This section will not be generated if the reason for the referral is represented as an external file
            //E.g. if the reason for the referral is contained within a referenced PDF
            POCD_MT000040Component3 component = null;

            if (!string.IsNullOrEmpty(reason))
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("102.16347", CodingSystem.NCTIS, "Referral Detail") };

                var entryList = new List<POCD_MT000040Entry>();

                if (duration != null)
                {
                    var anyList = new List<ANY> { CreateIntervalTimestamp(duration) };

                    entryList.Add(CreateEntryObservationWithDuration(x_ActRelationshipEntry.COMP,
                        CreateConceptDescriptor("103.16622", CodingSystem.NCTIS, "Referral Validity Duration", null),
                        null, anyList, null, null));
                }

                if (dateTime != null)
                {
                    entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                        CreateConceptDescriptor("103.16620", CodingSystem.NCTIS, "Referral DateTime", null), null, null,
                        new List<ANY> { CreateTimeStampElementIso(dateTime) }, null, null));
                }

                var reasonForReferral = new List<ANY>();

                if (!string.IsNullOrEmpty(reason))
                {
                    reasonForReferral.Add(CreateStructuredText(reason, null));
                }

                //Add an entry containing the referral reason and the referenced referral reason document if one was supplied.
                entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                    CreateConceptDescriptor("42349-1", CodingSystem.LOINC, "Reason for referral", null), null,
                    reasonForReferral, null, null));

                component.section.entry = entryList.ToArray();

                if (component.section != null)
                {
                    component.section.text = customNarrative ??
                                             narrativeGenerator.CreateNarrative(dateTime, duration, reason,
                                                 "Reason for Referral");
                }
            }

            return component;
        }

        /// <summary>
        /// Creates a ACD Custodian Entries 
        /// </summary>
        /// <param name="custodianParticipations">A list of IParticipationAcdCustodian</param>
        /// <param name="customNarrative">A StrucDocText</param>
        /// <param name="narrativeGenerator">A narrativeGenerator</param>
        /// <returns>A POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            IList<IParticipationAcdCustodian> custodianParticipations, StrucDocText customNarrative,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (custodianParticipations != null && custodianParticipations.Any())
            {
                component = new POCD_MT000040Component3();

                component.section = new POCD_MT000040Section
                {
                    code = CreateCodedWithExtensionElement("101.16694", CodingSystem.NCTIS, "ACD Custodian Entries",
                        null, null, null),
                    title = new ST { Text = new[] { "ACD Custodian Entries" } }
                };

                var custodianEntries = new List<POCD_MT000040Entry>();

                foreach (var participation in custodianParticipations)
                {
                    if (participation != null)
                    {
                        var entry = new POCD_MT000040Entry();
                        entry.typeCode = x_ActRelationshipEntry.COMP;
                        entry.act = new POCD_MT000040Act()
                        {
                            classCode = x_ActClassDocumentEntryAct.INFRM,
                            moodCode = x_DocumentActMood.EVN,
                            code = CreateConceptDescriptor("102.16690", CodingSystem.NCTIS, "ACD Custodian Entry", null)
                        };

                        var performer = CreatePerformer(participation);

                        entry.act.performer = new[] { performer };

                        custodianEntries.Add(entry);
                    }
                }

                component.section.entry = custodianEntries.ToArray();
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(custodianParticipations);
            }

            return component;
        }

        /// <summary>
        /// Diagnostic Imaging Report
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            IList<IDiagnosticImagingExaminationResult> imagingExaminationResults,
            IParticipationReportingRadiologist reportingRadiologist,
            RelatedDocument relatedDocument,
            StrucDocText diagnosticImagingCustomNarrative,
            INarrativeGenerator narrativeGenerator)
        {

            POCD_MT000040Component3 diagnosticImagingReport = null;
            var componentList = new List<POCD_MT000040Component5>();
            IList<POCD_MT000040Entry> entriesList = new List<POCD_MT000040Entry>();

            if (imagingExaminationResults.Any() || relatedDocument != null)
            {
                // Section Diagnostic Imaging
                diagnosticImagingReport = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(DiagnosticImagingReportSections.DiagnosticImaging),
                };

                // Create Test Results
                if (imagingExaminationResults != null)
                {
                    componentList.AddRange(CreateComponent(imagingExaminationResults, narrativeGenerator));
                }

                var diagnosticImagingStudy = CreateCodableText(DiagnosticImagingReportSections.DiagnosticImagingStudy);

                // Create Related Document
                if (relatedDocument != null)
                {
                    entriesList.Add(CreateEntryActRelatedDocument(relatedDocument, diagnosticImagingStudy));
                }

                // REPORTING PATHOLOGIST
                if (reportingRadiologist != null)
                {
                    diagnosticImagingReport.section.author = new[]
                    {
                        CreateAuthor(reportingRadiologist)
                    };

                    // Entitlements
                    if (reportingRadiologist.Participant != null &&
                        reportingRadiologist.Participant.Entitlements != null &&
                        reportingRadiologist.Participant.Entitlements.Any())
                    {
                        diagnosticImagingReport.section.coverage2 = CreateEntitlements(
                            reportingRadiologist.Participant.Entitlements,
                            reportingRadiologist.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                            ParticipationType.HLD).ToArray();
                    }
                }

                // Set Component List
                if (componentList.Any())
                {
                    diagnosticImagingReport.section.component = componentList.ToArray();
                }

                // Set Pathology Component
                if (diagnosticImagingReport.section != null && diagnosticImagingCustomNarrative != null)
                {
                    // The default narrative is set in CreateSectionCodeTitle at the top of this section
                    diagnosticImagingReport.section.text = diagnosticImagingCustomNarrative;
                }

                // Add entries list
                if (entriesList.Any())
                {
                    diagnosticImagingReport.section.entry = entriesList.ToArray();
                }

                // The default narrative is set in CreateSectionCodeTitle at the top of this section
                diagnosticImagingReport.section.text = diagnosticImagingCustomNarrative ??
                                                       narrativeGenerator.CreateNarrative(reportingRadiologist,
                                                           relatedDocument);
            }

            return diagnosticImagingReport;
        }

        /// <summary>
        /// Creates Advance Care Information
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IDocumentDetails documentDetails,
            StrucDocText customNarrative, INarrativeGenerator narrativeGenerator, DocumentType docType, string title)
        {
            POCD_MT000040Component3 advanceCareInformationComponent = null;
            var componentList = new List<POCD_MT000040Component5>();
            IList<POCD_MT000040Entry> entriesList = new List<POCD_MT000040Entry>();

            if (documentDetails != null)
            {
                // Section ACP
                if (title == "Advance Care Planning Document")
                {
                    advanceCareInformationComponent = new POCD_MT000040Component3
                    {
                        section = CreateSectionCodeTitle(AdvanceCareInformationSections.AdvanceCareInformationSection),
                    };
                }

                // Section GOC
                if (title == "Goals of Care Document")
                {
                    advanceCareInformationComponent = new POCD_MT000040Component3
                    {
                        section = CreateSectionCodeTitle(AdvanceCareInformationSections.GoalsOfCareSection),
                    };
                }

                // Related Document
                var entryRelatedDocument =
                    CreateEntryActEvent(
                        CreateConceptDescriptor(CreateCodableText(AdvanceCareInformationSections.RelatedDocument)),
                        null);

                entryRelatedDocument.act.reference = new[]
                {
                    new POCD_MT000040Reference
                    {
                        seperatableInd = CreateBoolean(true, true),
                        typeCode = x_ActRelationshipExternalReference.REFR,
                        externalDocument = new POCD_MT000040ExternalDocument
                        {
                            classCode = ActClassDocument.DOC,
                            moodCode = ActMood.EVN,
                            id = documentDetails.DocumentProvenance != null
                                ? CreateIdentifierArray(documentDetails.DocumentProvenance.DocumentIdentifier)
                                : null,
                            code = documentDetails.DocumentProvenance != null &&
                                   documentDetails.DocumentProvenance.DocumentType.HasValue
                                ? CreateConceptDescriptor(
                                    CreateCodableText(documentDetails.DocumentProvenance.DocumentType.Value))
                                : null,
                            text = CreateEncapsulatedData(documentDetails.ExternalData)
                        }
                    }
                };

                if (documentDetails.DocumentProvenance != null && documentDetails.DocumentProvenance.Author != null)
                    entryRelatedDocument.act.author = new[]
                    {
                        CreateAuthor(documentDetails.DocumentProvenance.Author)
                    };

                entriesList.Add(entryRelatedDocument);

                // Add entries
                if (entriesList.Any())
                {
                    advanceCareInformationComponent.section.entry = entriesList.ToArray();
                }

                if (componentList.Any())
                {
                    advanceCareInformationComponent.section.component = componentList.ToArray();
                }

                // The default narrative is set in CreateSectionCodeTitle at the top of this section
                advanceCareInformationComponent.section.text =
                    customNarrative ?? narrativeGenerator.CreateNarrative(documentDetails, docType);
            }

            return advanceCareInformationComponent;
        }

        /// <summary>
        /// Creates Pathology component 1A
        /// IList<SCSModel.Common.PathologyTestResult> pathologyTestResults
        /// IParticipationReportingPathologist reportingPathologist
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            IList<SCSModel.Common.PathologyTestResult> pathologyTestResults,
            IParticipationReportingPathologist reportingPathologist,
            RelatedDocument relatedDocument,
            StrucDocText pathologyCustomNarrative,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 pathologyComponent = null;
            var componentList = new List<POCD_MT000040Component5>();
            IList<POCD_MT000040Entry> entriesList = new List<POCD_MT000040Entry>();

            if (pathologyTestResults.Any())
            {
                // Section Pathology
                pathologyComponent = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PatholodyResultReportSections.Pathology),
                };

                // Set classCode and moodCode - NOTE: No need for this code because these are default values
                pathologyComponent.section.classCode = ActClass.DOCSECT;
                pathologyComponent.section.moodCode = ActMood.EVN;

                // REPORTING PATHOLOGIST
                if (reportingPathologist != null)
                {
                    pathologyComponent.section.author = new[]
                    {
                        CreateAuthor(reportingPathologist)
                    };

                    // Entitlements
                    if (reportingPathologist.Participant != null &&
                        reportingPathologist.Participant.Entitlements != null &&
                        reportingPathologist.Participant.Entitlements.Any())
                    {
                        pathologyComponent.section.coverage2 =
                            CreateEntitlements(reportingPathologist.Participant.Entitlements,
                                reportingPathologist.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                                ParticipationType.HLD).ToArray();
                    }
                }

                // Create Pathology Test Results
                if (pathologyTestResults != null)
                {
                    componentList.AddRange(CreateComponent(pathologyTestResults as List<SCSModel.Common.PathologyTestResult>, CDADocumentType.PathologyReportWithStructuredContent, narrativeGenerator));
                }

                // Create Related Document
                if (relatedDocument != null)
                {
                    entriesList.Add(CreateEntryActRelatedDocument(relatedDocument,
                        CreateCodableText(PatholodyResultReportSections.PathologyStudy)));
                }

                // Assign list of arrays
                if (componentList.Any())
                {
                    pathologyComponent.section.component = componentList.ToArray();
                }

                // The default narrative is set in CreateSectionCodeTitle at the top of this section
                pathologyComponent.section.text = pathologyCustomNarrative ??
                                                  narrativeGenerator.CreateNarrative(reportingPathologist,
                                                      relatedDocument);

                // Add entries
                if (entriesList.Any())
                {
                    pathologyComponent.section.entry = entriesList.ToArray();
                }
            }

            return pathologyComponent;
        }

        /// <summary>
        /// Creates Pathology component 1B (NEW)
        /// IList<SCSModel.Common.PathologyTestResult> pathologyTestResults
        /// IList<IParticipationReportingPathologist> reportingPathologists
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            IList<SCSModel.Common.PathologyTestResult> pathologyTestResults,
            IList<IParticipationReportingPathologist> reportingPathologists,
            RelatedDocument relatedDocument,
            StrucDocText pathologyCustomNarrative,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 pathologyComponent = null;
            var componentList = new List<POCD_MT000040Component5>();
            IList<POCD_MT000040Entry> entriesList = new List<POCD_MT000040Entry>();

            if (pathologyTestResults.Any())
            {
                // Section Pathology
                pathologyComponent = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PatholodyResultReportSections.Pathology),
                };

                // Set classCode and moodCode - NOTE: No need for this code because these are default values
                pathologyComponent.section.classCode = ActClass.DOCSECT;
                pathologyComponent.section.moodCode = ActMood.EVN;

                // REPORTING PATHOLOGISTS
                if (reportingPathologists != null)
                {
                    List<POCD_MT000040Author> authors = new List<POCD_MT000040Author>();
                    List<Coverage2> coverages = new List<Coverage2>();

                    foreach (var reportingPathologist in reportingPathologists)
                    {
                        authors.Add(CreateAuthor(reportingPathologist));

                        // Entitlements
                        if (reportingPathologist.Participant != null &&
                            reportingPathologist.Participant.Entitlements != null &&
                            reportingPathologist.Participant.Entitlements.Any())
                        {
                            coverages.AddRange(
                                CreateEntitlements(reportingPathologist.Participant.Entitlements,
                                    reportingPathologist.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                                    ParticipationType.HLD).ToArray());
                        }
                    }

                    pathologyComponent.section.author = authors.ToArray();
                    pathologyComponent.section.coverage2 = coverages.ToArray();
                }

                // Create Pathology Test Results
                if (pathologyTestResults != null)
                {
                    componentList.AddRange(CreateComponent(pathologyTestResults as List<SCSModel.Common.PathologyTestResult>, CDADocumentType.PathologyReportWithStructuredContent, narrativeGenerator));
                }

                // Create Related Document
                if (relatedDocument != null)
                {
                    entriesList.Add(CreateEntryActRelatedDocument(relatedDocument,
                        CreateCodableText(PatholodyResultReportSections.PathologyStudy)));
                }

                // Assign list of arrays
                if (componentList.Any())
                {
                    pathologyComponent.section.component = componentList.ToArray();
                }

                // The default narrative is set in CreateSectionCodeTitle at the top of this section
                pathologyComponent.section.text = pathologyCustomNarrative ?? narrativeGenerator.CreateNarrative(reportingPathologists, relatedDocument);

                // Add entries
                if (entriesList.Any())
                {
                    pathologyComponent.section.entry = entriesList.ToArray();
                }
            }

            return pathologyComponent;
        }

        /// <summary>
        /// Creates Pathology component 2A
        /// IList<SCSModel.Pathology.PathologyTestResult> pathologyTestResults
        /// IParticipationReportingPathologist reportingPathologist
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            IList<SCSModel.Pathology.PathologyTestResult> pathologyTestResults,
            IParticipationReportingPathologist reportingPathologist,
            RelatedDocument relatedDocument,
            StrucDocText pathologyCustomNarrative,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 pathologyComponent = null;
            var componentList = new List<POCD_MT000040Component5>();
            IList<POCD_MT000040Entry> entriesList = new List<POCD_MT000040Entry>();

            if (pathologyTestResults.Any())
            {
                // Section Pathology
                pathologyComponent = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PatholodyResultReportSections.Pathology),
                };

                // Set classCode and moodCode - NOTE: No need for this code because these are default values
                pathologyComponent.section.classCode = ActClass.DOCSECT;
                pathologyComponent.section.moodCode = ActMood.EVN;

                // REPORTING PATHOLOGIST
                if (reportingPathologist != null)
                {
                    pathologyComponent.section.author = new[]
                    {
                        CreateAuthor(reportingPathologist)
                    };

                    // Entitlements
                    if (reportingPathologist.Participant != null &&
                        reportingPathologist.Participant.Entitlements != null &&
                        reportingPathologist.Participant.Entitlements.Any())
                    {
                        pathologyComponent.section.coverage2 =
                            CreateEntitlements(reportingPathologist.Participant.Entitlements,
                                reportingPathologist.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                                ParticipationType.HLD).ToArray();
                    }
                }

                // Create Pathology Test Results
                if (pathologyTestResults != null)
                {
                    componentList.AddRange(CreateComponent(pathologyTestResults, narrativeGenerator));
                }

                // Create Related Document
                if (relatedDocument != null)
                {
                    entriesList.Add(CreateEntryActRelatedDocument(relatedDocument,
                        CreateCodableText(PatholodyResultReportSections.PathologyStudy)));
                }

                // Assign list of arrays
                if (componentList.Any())
                {
                    pathologyComponent.section.component = componentList.ToArray();
                }

                // The default narrative is set in CreateSectionCodeTitle at the top of this section
                pathologyComponent.section.text = pathologyCustomNarrative ??
                                                  narrativeGenerator.CreateNarrative(reportingPathologist,
                                                      relatedDocument);

                // Add entries
                if (entriesList.Any())
                {
                    pathologyComponent.section.entry = entriesList.ToArray();
                }
            }

            return pathologyComponent;
        }

        /// <summary>
        /// Creates Pathology component 2B (NEW)
        /// IList<IParticipationReportingPathologist> reportingPathologists
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            IList<SCSModel.Pathology.PathologyTestResult> pathologyTestResults,
            IList<IParticipationReportingPathologist> reportingPathologists,
            RelatedDocument relatedDocument,
            StrucDocText pathologyCustomNarrative,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 pathologyComponent = null;
            var componentList = new List<POCD_MT000040Component5>();
            IList<POCD_MT000040Entry> entriesList = new List<POCD_MT000040Entry>();

            if (pathologyTestResults.Any())
            {
                // Section Pathology
                pathologyComponent = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PatholodyResultReportSections.Pathology),
                };

                // Set classCode and moodCode - NOTE: No need for this code because these are default values
                pathologyComponent.section.classCode = ActClass.DOCSECT;
                pathologyComponent.section.moodCode = ActMood.EVN;

                // REPORTING PATHOLOGISTS
                if (reportingPathologists != null)
                {
                    List<POCD_MT000040Author> authors = new List<POCD_MT000040Author>();
                    List<Coverage2> coverages = new List<Coverage2>();

                    foreach (var reportingPathologist in reportingPathologists)
                    {
                        authors.Add(CreateAuthor(reportingPathologist));

                        // Entitlements
                        if (reportingPathologist.Participant != null &&
                            reportingPathologist.Participant.Entitlements != null &&
                            reportingPathologist.Participant.Entitlements.Any())
                        {
                            coverages.AddRange(
                                CreateEntitlements(reportingPathologist.Participant.Entitlements,
                                    reportingPathologist.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                                    ParticipationType.HLD).ToArray());
                        }
                    }

                    pathologyComponent.section.author = authors.ToArray();
                    pathologyComponent.section.coverage2 = coverages.ToArray();
                }

                // Create Pathology Test Results
                if (pathologyTestResults != null)
                {
                    componentList.AddRange(CreateComponent(pathologyTestResults, narrativeGenerator));
                }

                // Create Related Document
                if (relatedDocument != null)
                {
                    entriesList.Add(CreateEntryActRelatedDocument(relatedDocument,
                        CreateCodableText(PatholodyResultReportSections.PathologyStudy)));
                }

                // Assign list of arrays
                if (componentList.Any())
                {
                    pathologyComponent.section.component = componentList.ToArray();
                }

                // The default narrative is set in CreateSectionCodeTitle at the top of this section
                pathologyComponent.section.text = pathologyCustomNarrative ?? narrativeGenerator.CreateNarrative(reportingPathologists, relatedDocument);

                // Add entries
                if (entriesList.Any())
                {
                    pathologyComponent.section.entry = entriesList.ToArray();
                }
            }

            return pathologyComponent;
        }

        /// <summary>
        /// Creates an adverse substance reactions component
        /// </summary>
        /// <param name="allergyAndAdverseReactions">List of adverse reactions</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<Reaction> allergyAndAdverseReactions,
            StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();
            if (allergyAndAdverseReactions != null && allergyAndAdverseReactions.Any())
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.20113", CodingSystem.NCTIS, "Adverse Reactions")
                };

                component.section.title = new ST
                {
                    Text = new[] { "Allergies and Adverse Reactions" }
                };

                foreach (var reaction in allergyAndAdverseReactions)
                {
                    var adverseReactionsRelationships = new List<POCD_MT000040EntryRelationship>();

                    if (reaction.ReactionEvent != null && reaction.ReactionEvent.Manifestations != null)
                    {
                        var manifestationRelationships = new List<POCD_MT000040EntryRelationship>();

                        foreach (var manifestation in reaction.ReactionEvent.Manifestations)
                        {
                            manifestationRelationships.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.MFST, true, CreateConceptDescriptor(manifestation)));
                        }

                        List<ANY> values = null;
                        if (reaction.ReactionEvent != null && reaction.ReactionEvent.ReactionType != null)
                        {
                            values = new List<ANY>
                            {
                                CreateConceptDescriptor(reaction.ReactionEvent.ReactionType)
                            };
                        }

                        var entryRelationshipObservation = CreateEntryRelationshipObservation(
                            x_ActRelationshipEntryRelationship.CAUS,
                            ActClassObservation.OBS,
                            x_ActMoodDocumentObservation.EVN,
                            null,
                            null,
                            CreateConceptDescriptor("102.16474", CodingSystem.NCTIS, "Reaction Event", null),
                            null,
                            null,
                            values,
                            manifestationRelationships);

                        adverseReactionsRelationships.Add(entryRelationshipObservation);
                    }

                    if (reaction.SubstanceOrAgent != null)
                        entryList.Add(CreateEntryActEvent(x_ActRelationshipEntry.COMP,
                            x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN,
                            CreateConceptDescriptor("102.15517", CodingSystem.NCTIS, "Adverse Reaction", null),
                            CreateParticipant2Array(CreateCodedWithExtensionElement(reaction.SubstanceOrAgent),
                                null),
                            adverseReactionsRelationships,
                            reaction.Id));
                }

                component.section.entry = entryList.ToArray();
            }

            if (component != null && component.section != null)
            {
                component.section.text =
                    customNarrative ?? narrativeGenerator.CreateNarrative(allergyAndAdverseReactions);
            }

            return component;
        }

        /// <summary>
        /// Creates an adverse substance reactions component
        /// </summary>
        /// <param name="adverseSubstanceReactions">IAdverseSubstanceReactions</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <param name="exclusionStatementCode">Exclusion statement code</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(AdverseReactions adverseSubstanceReactions,
            INarrativeGenerator narrativeGenerator, string exclusionStatementCode)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (adverseSubstanceReactions != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.20113", CodingSystem.NCTIS, "Adverse Reactions") };

                // Adverse Substance Reaction
                if (adverseSubstanceReactions.AdverseSubstanceReaction != null)
                {
                    foreach (var adverserReaction in adverseSubstanceReactions.AdverseSubstanceReaction)
                    {
                        var adverseReactionsRelationships = new List<POCD_MT000040EntryRelationship>();

                        if (adverserReaction.ReactionEvent != null &&
                            adverserReaction.ReactionEvent.Manifestations != null)
                        {
                            var manifestationRelationships = new List<POCD_MT000040EntryRelationship>();

                            foreach (var manifestation in adverserReaction.ReactionEvent.Manifestations)
                            {
                                manifestationRelationships.Add(CreateEntryRelationshipObservation(
                                    x_ActRelationshipEntryRelationship.MFST, true,
                                    CreateConceptDescriptor(manifestation)));
                            }

                            List<ANY> values = null;
                            if (adverserReaction.ReactionEvent != null &&
                                adverserReaction.ReactionEvent.ReactionType != null)
                            {
                                values = new List<ANY>
                                {
                                    CreateConceptDescriptor(adverserReaction.ReactionEvent.ReactionType)
                                };
                            }

                            var entryRelationshipObservation = CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.CAUS,
                                ActClassObservation.OBS,
                                x_ActMoodDocumentObservation.EVN,
                                null,
                                null,
                                CreateConceptDescriptor("102.16474", CodingSystem.NCTIS, "Reaction Event", null),
                                null,
                                null,
                                values,
                                manifestationRelationships);

                            adverseReactionsRelationships.Add(entryRelationshipObservation);
                        }

                        if (adverserReaction.SubstanceOrAgent != null)
                            entryList.Add(CreateEntryActEvent(x_ActRelationshipEntry.COMP,
                                x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN,
                                CreateConceptDescriptor("102.15517", CodingSystem.NCTIS, "Adverse Reaction", null),
                                CreateParticipant2Array(
                                    CreateCodedWithExtensionElement(adverserReaction.SubstanceOrAgent), null),
                                adverseReactionsRelationships,
                                adverserReaction.Id));
                    }
                }
                else
                {
                    // Exclusions
                    if (adverseSubstanceReactions.ExclusionStatement != null)
                    {
                        entryList.Add(CreateExclusionStatement(adverseSubstanceReactions.ExclusionStatement,
                            exclusionStatementCode));
                    }
                }

                component.section.entry = entryList.ToArray();
            }

            if (component != null && component.section != null)
            {
                component.section.text = adverseSubstanceReactions.CustomNarrativeAdverseReactions ??
                                         narrativeGenerator.CreateNarrative(adverseSubstanceReactions);
            }

            return component;
        }

        /// <summary>
        /// Creates an adverse substance reactions component - SML specific
        /// </summary>
        /// <param name="adverseSubstanceReactions">IAdverseSubstanceReactions</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(AdverseReactions adverseSubstanceReactions, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (adverseSubstanceReactions != null)
            {
                component = new POCD_MT000040Component3
                {
                    // Code
                    section = CreateSectionCodeTitle("48765-2", CodingSystem.LOINC, "Allergies &or adverse reactions", "Allergies and Intolerances", "")
                };
                // Template ID
                component.section.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100069");

                // Title
                // NOTE not mapped

                // Text
                // NOTE not mapped

                // Adverse Substance Reaction
                if (adverseSubstanceReactions.AdverseSubstanceReaction != null)
                {
                    foreach (var adverseReaction in adverseSubstanceReactions.AdverseSubstanceReaction)
                    {
                        var adverseReactionsRelationships = new List<POCD_MT000040EntryRelationship>();

                        // Author 
                        // NOTE not mapped

                        // Clinical status
                        var entryClinicalStatus = CreateEntryRelationshipObservation(
                            x_ActRelationshipEntryRelationship.COMP, ActClassObservation.OBS, x_ActMoodDocumentObservation.EVN, null, null,
                            CreateConceptDescriptor("103.32013", CodingSystem.NCTIS, "Clinical Status", null),
                            null, null,
                            new List<ANY> { CreateConceptDescriptor("active", CodingSystem.HL7AllergyIntoleranceClinicalStatusCodes, "Active", null) },
                            null);
                        adverseReactionsRelationships.Add(entryClinicalStatus);

                        // Verification status
                        if (adverseReaction?.ReactionEvent?.VerificationStatus != null)
                        {
                            ICodableText verificationStatus = adverseReaction.ReactionEvent.VerificationStatus;

                            var entryVerificationStatus = CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP, ActClassObservation.OBS, x_ActMoodDocumentObservation.EVN, null, null,
                                CreateConceptDescriptor("103.32012", CodingSystem.NCTIS, "Verification Status", null),
                                null, null,
                                new List<ANY> { CreateConceptDescriptor(verificationStatus.Code, CodingSystem.HL7AllergyIntoleranceVerificationStatusCodes, verificationStatus.DisplayName, null) },
                                null);
                            adverseReactionsRelationships.Add(entryVerificationStatus);
                        }

                        // Type
                        var reactionType = CreateConceptDescriptor("102.15517", CodingSystem.SNOMEDCT, "Adverse Reaction", null); // Default type
                        if (adverseReaction?.ReactionEvent?.ReactionType != null)
                        {
                            // Type has been specified
                            reactionType = CreateConceptDescriptor(adverseReaction.ReactionEvent.ReactionType);
                        }

                        // Patient
                        // NOTE not mapped

                        // Onset[x]
                        ISO8601DateTime onsetDateLow = null;
                        if (adverseReaction.ReactionEvent != null && adverseReaction.ReactionEvent.ReactionOnsetDate != null)
                        {
                            // Onset
                            if (adverseReaction.ReactionEvent.ReactionOnsetDate.Interval != null)
                            {
                                // Low value
                                onsetDateLow = adverseReaction.ReactionEvent.ReactionOnsetDate.Interval.Low;
                            }
                            else if (adverseReaction.ReactionEvent.ReactionOnsetDate.Value != null)
                            {
                                // Age
                                var entryOnsetDateAge = CreateEntryRelationshipObservation(
                                    x_ActRelationshipEntryRelationship.COMP,
                                    ActClassObservation.OBS,
                                    x_ActMoodDocumentObservation.EVN,
                                    null,
                                    null,
                                    CreateConceptDescriptor("445518008", CodingSystem.SNOMEDCT, "Age at onset of clinical finding", null),
                                    null,
                                    null,
                                    new List<ANY> { CreatePhysicalQuantity(adverseReaction.ReactionEvent.ReactionOnsetDate.Unit.GetAttributeValue<NameAttribute, string>(x => x.Code),
                                            adverseReaction.ReactionEvent.ReactionOnsetDate.Value) },
                                    null);

                                adverseReactionsRelationships.Add(entryOnsetDateAge);
                            }
                        }

                        // Recorder
                        // NOTE not mapped

                        // Note
                        if (adverseReaction.AdditionalComments != null && adverseReaction.AdditionalComments.Any())
                        {
                            foreach (NoteSML noteSml in adverseReaction.AdditionalComments)
                            {
                                // Check note exists
                                if (noteSml.NoteText != null)
                                {
                                    POCD_MT000040EntryRelationship noteEntry = new POCD_MT000040EntryRelationship();
                                    noteEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                                    noteEntry.act = new POCD_MT000040Act();
                                    noteEntry.act.classCode = x_ActClassDocumentEntryAct.ACT;
                                    noteEntry.act.moodCode = x_DocumentActMood.EVN;
                                    noteEntry.act.code = new CD
                                    {
                                        code = "103.16044",
                                        codeSystem = "1.2.36.1.2001.1001.101",
                                        displayName = "Additional Comments"
                                    };
                                    // Need to ask about profiling this out - do we need it?
                                    if (noteSml.NoteAuthor != null) noteEntry.act.author = new[] {CreateAuthor(noteSml.NoteAuthor)};
                                    if (noteSml.NoteDateTime != null) noteEntry.act.effectiveTime = CreateIntervalTimestamp(noteSml.NoteDateTime, null);
                                    noteEntry.act.text = CreateStructuredText(noteSml.NoteText, null);

                                    adverseReactionsRelationships.Add(noteEntry);
                                }
                            }
                        }

                        // Reaction
                        if (adverseReaction?.ReactionEvent?.Manifestations != null)
                        {
                            var manifestationRelationships = new List<POCD_MT000040EntryRelationship>();

                            foreach (var manifestation in adverseReaction.ReactionEvent.Manifestations)
                            {
                                manifestationRelationships.Add(CreateEntryRelationshipObservation(
                                    x_ActRelationshipEntryRelationship.MFST, true,
                                    CreateConceptDescriptor(manifestation)));
                            }

                            var entryRelationshipObservation = CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP,
                                ActClassObservation.OBS,
                                x_ActMoodDocumentObservation.EVN,
                                null,
                                null,
                                CreateConceptDescriptor("102.16474", CodingSystem.NCTIS, "Reaction Event", null),
                                null,
                                null,
                                null,
                                manifestationRelationships,
                                null,
                                CreateCodedWithExtensionElement(adverseReaction.ReactionEvent.Substance));

                            adverseReactionsRelationships.Add(entryRelationshipObservation);
                        }

                        // Add the Observation 
                        entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                            reactionType,
                            null, onsetDateLow,
                            new List<ANY> { CreateConceptDescriptor(adverseReaction.SubstanceOrAgent) }, // Code
                            null, adverseReactionsRelationships, "1.2.36.1.2001.1001.102.101.100014"));
                    }
                }
                else
                {
                    // Empty Reason
                    if (adverseSubstanceReactions.EmptyReasonStatement != null)
                    {
                        entryList.Add(CreateEmptyStatement(adverseSubstanceReactions.EmptyReasonStatement));

                        adverseSubstanceReactions.CustomNarrativeAdverseReactions = new StrucDocText()
                        {
                            paragraph = new StrucDocParagraph[]
                            {
                                new StrucDocParagraph()
                                    {Text = new string[] {adverseSubstanceReactions.EmptyReasonStatement.OriginalText}}
                            }
                        };
                    }
                }

                component.section.entry = entryList.ToArray();
            }

            if (component != null && component.section != null)
            {
                component.section.text = adverseSubstanceReactions.CustomNarrativeAdverseReactions ??
                                         narrativeGenerator.CreateNarrative(adverseSubstanceReactions);
            }

            return component;
        }

        /// <summary>
        /// Creates an adverse substance reactions component - SML specific
        /// </summary>
        /// <param name="medications">MedicationsSML</param>
        /// <param name="isCurrent">Indicates if medications is current or ceased</param>
        /// <param name="author"></param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(MedicationListSML medications, bool isCurrent, POCD_MT000040Author author, INarrativeGenerator narrativeGenerator)
        {
            if (medications == null)
            {
                throw new ArgumentException("'medications' is required");
            }

            // Create component and section
            POCD_MT000040Component3 component = new POCD_MT000040Component3();
            if (isCurrent)
            {
                component.section = CreateSectionCodeTitle("101.32009", CodingSystem.NCTIS, "Current Medicines", "Current Medicines", "");
            }
            else
            {
                component.section = CreateSectionCodeTitle("101.32027", CodingSystem.NCTIS, "Ceased Medicines", "Ceased Medicines", "");
            }
            component.section.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100077");

            // Entry
            component.section.entry = new List<POCD_MT000040Entry>() { new POCD_MT000040Entry() }.ToArray();
            component.section.entry[0].act = new POCD_MT000040Act();

            var medsAct = component.section.entry[0].act;

            medsAct.classCode = x_ActClassDocumentEntryAct.ACT;
            medsAct.moodCode = x_DocumentActMood.EVN;
            medsAct.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100067");
            medsAct.id = CreateIdentifierArray(Guid.NewGuid().ToString());

            // Code
            medsAct.code = component.section.code;

            // Status
            medsAct.statusCode = new CS() { code = "active" };

            // Date
            medsAct.effectiveTime = new IVL_TS() { value = author.time.value };

            // Create 
            if (medications.AuthorRole != null)
            {
                POCD_MT000040Author newAuthor = new POCD_MT000040Author
                {
                    templateId = author.templateId,
                    time = author.time,
                    assignedAuthor = CreateAssignedAuthorPractitioner(medications.AuthorRole, component.section)
                };

                medsAct.author = new List<POCD_MT000040Author>() {newAuthor}.ToArray();
            }

            List<POCD_MT000040EntryRelationship> actEntryRelationships = new List<POCD_MT000040EntryRelationship>();


            // Packed in DAA - Mandatory for Profile
            if (medications.PackedInDaa != null)
            {
                POCD_MT000040EntryRelationship packedInDaa = new POCD_MT000040EntryRelationship();
                packedInDaa.typeCode = x_ActRelationshipEntryRelationship.COMP;
                packedInDaa.observation = new POCD_MT000040Observation();
                packedInDaa.observation.classCode = ActClassObservation.OBS;
                packedInDaa.observation.moodCode = x_ActMoodDocumentObservation.EVN;
                packedInDaa.observation.code = new CD
                {
                    code = "1469401000168104",
                    codeSystem = "2.16.840.1.113883.6.96",
                    codeSystemName = "SNOMED CT",
                    displayName = "Medicines packed in dose administration aid indicator"
                };
                packedInDaa.observation.value = new[] {new CD
                {
                    code = medications.PackedInDaa.Code,
                    codeSystem = medications.PackedInDaa.CodeSystemCode,
                    codeSystemName = medications.PackedInDaa.CodeSystemName,
                    displayName = medications.PackedInDaa.DisplayName
                } };

                actEntryRelationships.Add(packedInDaa);
            }

            // Encounter
            if (medications.Encounter != null)
            {
                actEntryRelationships.Add(CreateEncounterForMedicationStatement(medications.Encounter));
            }

            // Note
            if (medications.AdditionalListComments != null && medications.AdditionalListComments.Any())
            {
                foreach (NoteSML noteSml in medications.AdditionalListComments)
                {
                    // Check note exists
                    if (noteSml.NoteText != null)
                    {
                        POCD_MT000040EntryRelationship noteEntry = new POCD_MT000040EntryRelationship();
                        noteEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                        noteEntry.act = new POCD_MT000040Act();
                        noteEntry.act.classCode = x_ActClassDocumentEntryAct.INFRM;
                        noteEntry.act.moodCode = x_DocumentActMood.EVN;
                        noteEntry.act.code = new CD
                        {
                            code = "103.16044",
                            codeSystem = "1.2.36.1.2001.1001.101",
                            displayName = "Additional Comments"
                        };

                        // Need to ask about profiling this out - do we need it?
                        if (noteSml.NoteAuthor != null) noteEntry.act.author = new[] { CreateAuthor(noteSml.NoteAuthor) };
                        if (noteSml.NoteDateTime != null) noteEntry.act.effectiveTime = CreateIntervalTimestamp(noteSml.NoteDateTime, null);
                        noteEntry.act.text = CreateStructuredText(noteSml.NoteText, null);

                        actEntryRelationships.Add(noteEntry);
                    }
                }
            }

            // Medicine entries
            foreach (MedicineItemSML medicineItemSml in medications.MedicineItem)
            {
                List<POCD_MT000040EntryRelationship> substanceAdministrationEntryRelationships = new List<POCD_MT000040EntryRelationship>();

                // List > entry
                POCD_MT000040EntryRelationship listEntry = new POCD_MT000040EntryRelationship();
                listEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                actEntryRelationships.Add(listEntry);

                // List > entry > Flag
                if (medicineItemSml.ChangeTypeFlag != null)
                {
                    POCD_MT000040EntryRelationship flagEntry = new POCD_MT000040EntryRelationship();
                    flagEntry.typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                    flagEntry.inversionInd = true;
                    flagEntry.inversionIndSpecified = true;
                    flagEntry.observation = new POCD_MT000040Observation();
                    flagEntry.observation.classCode = ActClassObservation.OBS;
                    flagEntry.observation.moodCode = x_ActMoodDocumentObservation.EVN;
                    flagEntry.observation.code = new CD
                    {
                        code = "288533004",
                        codeSystem = "2.16.840.1.113883.6.96",
                        displayName = "Change values"
                    };

                    // List > entry > change-description
                    if (medicineItemSml.ChangeDescription != null) flagEntry.observation.text = CreateStructuredText(medicineItemSml.ChangeDescription, null);

                    flagEntry.observation.value = new[] {new CD
                    {
                        code = medicineItemSml.ChangeTypeFlag.Code,
                        codeSystem = medicineItemSml.ChangeTypeFlag.CodeSystemCode,
                        displayName = medicineItemSml.ChangeTypeFlag.DisplayName
                    } };
                    substanceAdministrationEntryRelationships.Add(flagEntry);
                }

                // List > entry > item
                listEntry.substanceAdministration = new POCD_MT000040SubstanceAdministration();
                listEntry.substanceAdministration.classCode = ActClass.SBADM;
                listEntry.substanceAdministration.moodCode = x_DocumentSubstanceMood.EVN;
                listEntry.substanceAdministration.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100066", null);
                
                // MedicationStatement > identifier
                listEntry.substanceAdministration.id = CreateIdentifierArray(medicineItemSml.Id);

                // MedicationStatement > context
                if (medicineItemSml.Encounter != null)
                {
                    POCD_MT000040EntryRelationship contextEntry = CreateEncounterForMedicationStatement(medicineItemSml.Encounter);
                    contextEntry.inversionInd = true;
                    contextEntry.inversionIndSpecified = true;
                    substanceAdministrationEntryRelationships.Add(contextEntry);
                }

                // MedicationStatement > Status code
                if (medicineItemSml.MedicationStatus != null)
                {
                    listEntry.substanceAdministration.statusCode = new CS
                    {
                        code = medicineItemSml.MedicationStatus.Code
                    };
                }
                
                // MedicationStatement > Category
                if (medicineItemSml.WhereAdministeredCategory != null)
                {
                    POCD_MT000040EntryRelationship categoryEntry = new POCD_MT000040EntryRelationship
                    {
                        typeCode = x_ActRelationshipEntryRelationship.COMP,
                        observation = new POCD_MT000040Observation
                        {
                            classCode = ActClassObservation.OBS,
                            moodCode = x_ActMoodDocumentObservation.EVN,
                            code = new CD
                            {
                                code = "276339004",
                                displayName = "Environment",
                                codeSystem = "2.16.840.1.113883.6.96"
                            },
                            value = new[] { new CD
                            {
                                code = medicineItemSml.WhereAdministeredCategory.Code,
                                displayName = medicineItemSml.WhereAdministeredCategory.DisplayName,
                                codeSystem = medicineItemSml.WhereAdministeredCategory.CodeSystemCode,
                                codeSystemName = medicineItemSml.WhereAdministeredCategory.CodeSystemName,
                                originalText = !string.IsNullOrWhiteSpace(medicineItemSml.WhereAdministeredCategory.OriginalText) ?
                                    CreateEncapsulatedData(medicineItemSml.WhereAdministeredCategory.OriginalText) : null
                            } }
                        }
                    };

                    substanceAdministrationEntryRelationships.Add(categoryEntry);
                }

                // MedicationStatement > medication[x] : substanceAdministration/consumable 
                listEntry.substanceAdministration.consumable = CreateSubstanceAdministrationConsumable(medicineItemSml, substanceAdministrationEntryRelationships);

                // MedicationStatement > Effective time
                listEntry.substanceAdministration.effectiveTime = new[] { CreateIntervalTimestamp(medicineItemSml.EffectiveTimeTakenOrNot, null) };

                // MedicationStatement > informationSource TODO: Do we need this?
                if (medicineItemSml.InformantPractitioner != null || medicineItemSml.InformantRelatedPerson != null)
                {
                    listEntry.substanceAdministration.informant = new POCD_MT000040Informant12[]
                    {
                        CreateInformantForSubstanceAdministration(medicineItemSml.InformantRelatedPerson, medicineItemSml.InformantPractitioner)
                    };
                }

                // MedicationStatement > Taken
                if (medicineItemSml.Taken.Code == "n" && medicineItemSml.MedicationStatus.Code != "new" && medicineItemSml.MedicationStatus.Code != "suspended")
                {
                    listEntry.substanceAdministration.negationInd = true;
					listEntry.substanceAdministration.negationIndSpecified = true;
                }
                else if (medicineItemSml.Taken.Code == "unk")
                {
                    listEntry.substanceAdministration.nullFlavor = NullFlavor.UNK;
                }
                else if (medicineItemSml.Taken.Code == "na")
                {
                    listEntry.substanceAdministration.nullFlavor = NullFlavor.NA;
                }

                // MedicationStatement > Reason not taken
                if (medicineItemSml.ReasonNotTaken != null)
                {
                    foreach (ICodableText codableText in medicineItemSml.ReasonNotTaken)
                    {
                        var notTakenEntry = new POCD_MT000040EntryRelationship();
                        notTakenEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                        notTakenEntry.observation = new POCD_MT000040Observation();
                        notTakenEntry.observation.classCode = ActClassObservation.OBS;
                        notTakenEntry.observation.moodCode = x_ActMoodDocumentObservation.EVN;
                        notTakenEntry.observation.code = new CD
                        {
                            code = "103.32024",
                            codeSystem = "1.2.36.1.2001.1001.101",
                            displayName = "Reason for Status"
                        };
                        notTakenEntry.observation.statusCode = new CS
                        {
                            code = "completed"
                        };
                        notTakenEntry.observation.value = new[]{ new CD
                        {
                            code = codableText.Code,
                            codeSystem = codableText.CodeSystemCode,
                            codeSystemName = codableText.CodeSystemName,
                            displayName = codableText.DisplayName,
                            originalText = !string.IsNullOrWhiteSpace(codableText.OriginalText) ?
                                CreateEncapsulatedData(codableText.OriginalText) : null
                        }};

                        substanceAdministrationEntryRelationships.Add(notTakenEntry);
                    }
                }
                                
                // MedicationStatement > Reason code
                if (medicineItemSml.MedicinePurpose != null)
                {
                    foreach (var purpose in medicineItemSml.MedicinePurpose)
                    {
                        var reasonEntry = new POCD_MT000040EntryRelationship();
                        reasonEntry.typeCode = x_ActRelationshipEntryRelationship.RSON;
                        reasonEntry.observation = new POCD_MT000040Observation();
                        reasonEntry.observation.classCode = ActClassObservation.OBS;
                        reasonEntry.observation.moodCode = x_ActMoodDocumentObservation.EVN;
                        reasonEntry.observation.code = new CD
                        {
                            code = "103.10141",
                            codeSystem = "1.2.36.1.2001.1001.101",
                            displayName = "Clinical Indication"
                        };
                        if (medicineItemSml.Taken != null)
                        {
                            reasonEntry.observation.value = new[]{ new CD
                                {
                                    code = purpose.Code,
                                    codeSystem = purpose.CodeSystemCode,
                                    codeSystemName = purpose.CodeSystemName,
                                    displayName = purpose.DisplayName,
                                    originalText = CreateEncapsulatedData(purpose.OriginalText)
                                }
                            };
                        }
                        substanceAdministrationEntryRelationships.Add(reasonEntry);
                    }
                }

                // MedicationStatement > Note
                if (medicineItemSml.AdditionalComments != null)
                {
                    foreach (NoteSML noteSml in medicineItemSml.AdditionalComments)
                    {
                        var noteEntry = new POCD_MT000040EntryRelationship();
                        noteEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                        noteEntry.act = new POCD_MT000040Act();
                        noteEntry.act.classCode = x_ActClassDocumentEntryAct.ACT;
                        noteEntry.act.moodCode = x_DocumentActMood.EVN;
                        noteEntry.act.code = new CD
                        {
                            code = "103.16044",
                            codeSystem = "1.2.36.1.2001.1001.101",
                            displayName = "Additional Comments"
                        };

                        // Need to ask about profiling this out - do we need it?
                        if (noteSml.NoteAuthor != null) noteEntry.act.author = new[]
                        {
                            CreateAuthor(noteSml.NoteAuthor)
                        };
                        if (noteSml.NoteDateTime != null)
                        {
                            noteEntry.act.effectiveTime = CreateIntervalTimestamp(noteSml.NoteDateTime, null);
                        }
                        noteEntry.act.text = CreateStructuredText(noteSml.NoteText, null);

                        substanceAdministrationEntryRelationships.Add(noteEntry);
                    }
                }

                // MedicationStatement > Dosage
                if (medicineItemSml.Dosage != null)
                {
                    if (medicineItemSml.Dosage.Count > 1)
                    {
                        foreach (DosageSML dosageSml in medicineItemSml.Dosage)
                        {
                            var dosageEntry = new POCD_MT000040EntryRelationship
                            {
                                substanceAdministration = new POCD_MT000040SubstanceAdministration
                                {
                                    classCode = ActClass.SBADM,
                                    moodCode = x_DocumentSubstanceMood.INT
                                },
                                typeCode = x_ActRelationshipEntryRelationship.COMP
                            };

                            CreateDosage(dosageEntry, dosageSml);

                            substanceAdministrationEntryRelationships.Add(dosageEntry);

                            // For Multiple Doses, we need to copy the Dose instructions up to the top level SubstanceAdministration (first one)
                            if (dosageSml.Instructions != null && listEntry.substanceAdministration.text == null)
                            {
                                listEntry.substanceAdministration.text = new ST();
                                listEntry.substanceAdministration.text.Text = new string[] { dosageSml.Instructions };
                            }

                        }
                    }
                    else if (medicineItemSml.Dosage.Count == 1)
                    {
                        DosageSML dosageSml = medicineItemSml.Dosage.Single();

                        CreateDosage(listEntry, dosageSml);
                    }
                }

                // MedicationStatement > Effective time
                if (medicineItemSml.EffectiveTimeTakenOrNot != null)
                {
                    listEntry.substanceAdministration.effectiveTime = new[] { CreateIntervalTimestamp(medicineItemSml.EffectiveTimeTakenOrNot, null) };
                }

                // Add substanceAdministratation entryRelationships
                listEntry.substanceAdministration.entryRelationship = substanceAdministrationEntryRelationships.ToArray();
            }

            if (actEntryRelationships.Count > 0)
            {
                medsAct.entryRelationship = actEntryRelationships.ToArray();
            }

            if (component != null && component.section != null)
            {
                component.section.text = medications.CustomNarrativeMedications ??
                                         narrativeGenerator.CreateNarrative(medications, isCurrent);
            }

            return component;
        }

        private static void CreateDosage(POCD_MT000040EntryRelationship entryRelationship, DosageSML dosageSml)
        {
            // Sequence
            if (dosageSml.Sequence != null)
            {
                entryRelationship.sequenceNumber = CreateIntegerElement(dosageSml.Sequence.Value, null, null);
            }

            // MedicationStatement > Dosage > Text / Patient Instructions - THIS SHOULD BE 1..1 - MANDATORY for CURRENT MEDS (NOT CEASED)
            if (dosageSml.Instructions != null)
            {
                entryRelationship.substanceAdministration.text = new ST();
                entryRelationship.substanceAdministration.text.Text = new string[] { dosageSml.Instructions };
            }

            // MedicationStatement > Dosage > Timing
            // TODO check as this maps to the same field as 'substanceAdministration.effectiveTime'

            // MedicationStatement > Dosage > As needed
            entryRelationship.substanceAdministration.precondition = new[]
            {
                new POCD_MT000040Precondition
                {
                    typeCode = ActRelationshipType.PRCN,
                    typeCodeSpecified = true,
                    criterion = new POCD_MT000040Criterion
                    {
                        code = new CD
                        {
                            code = "ASSERTION",
                            codeSystem = "2.16.840.1.113883.5.4"
                        },
                        value = new BL
                        {
                            value = dosageSml.AsNeeded
                        }
                    }
                }
            };

            // Consumable
            if (entryRelationship.substanceAdministration.consumable == null)
            {
                entryRelationship.substanceAdministration.consumable = new POCD_MT000040Consumable
                {
                    manufacturedProduct = new POCD_MT000040ManufacturedProduct
                    {
                        manufacturedMaterial = new POCD_MT000040Material
                        {
                            nullFlavor = NullFlavor.NA,
                            nullFlavorSpecified = true
                        }
                    }
                };
            }

            // MedicationStatement > Dosage > Site
            if (dosageSml.BodySite != null)
            {
                entryRelationship.substanceAdministration.approachSiteCode = new[]
                {
                    new CD
                    {
                        code = dosageSml.BodySite.Code,
                        codeSystem = dosageSml.BodySite.CodeSystemCode,
                        displayName = dosageSml.BodySite.DisplayName
                    }
                };
            }

            // MedicationStatement > Dosage > Route
            if (dosageSml.Route != null)
            {
                entryRelationship.substanceAdministration.routeCode = new CE
                {
                    code = dosageSml.Route.Code,
                    codeSystem = dosageSml.Route.CodeSystemCode,
                    displayName = dosageSml.Route.DisplayName
                };
            }

            // MedicationStatement > Dosage > Method
            if (dosageSml.AdministrationMethod != null)
            {
                entryRelationship.substanceAdministration.methodCode = new CD
                {
                    code = dosageSml.AdministrationMethod.Code,
                    codeSystem = dosageSml.AdministrationMethod.CodeSystemCode,
                    displayName = dosageSml.AdministrationMethod.DisplayName
                };
            }

            // MedicationStatement > Dosage > Dose
            if (dosageSml.Dose != null)
            {
                entryRelationship.substanceAdministration.doseQuantity = new IVL_PQ
                {
                    value = dosageSml.Dose.Value,
                    unit = dosageSml.Dose.Units
                };
            }

            // MedicationStatement > Dosage > Max dose per period
            if (dosageSml.MaxDosePerPeriod != null)
            {
                entryRelationship.substanceAdministration.maxDoseQuantity = new RTO_PQ_PQ
                {
                    denominator = new IVL_PQ
                    {
                        value = dosageSml.MaxDosePerPeriod?.Denominator?.Value,
                        unit = dosageSml.MaxDosePerPeriod?.Denominator?.Units
                    },
                    numerator = new IVL_PQ
                    {
                        value = dosageSml.MaxDosePerPeriod?.Numerator?.Value,
                        unit = dosageSml.MaxDosePerPeriod?.Numerator?.Units
                    }
                };
            }

            // MedicationStatement > Dosage > Rate
            if (dosageSml.Rate != null)
            {
                entryRelationship.substanceAdministration.rateQuantity = new IVL_PQ
                {
                    value = dosageSml.Rate.Value,
                    unit = dosageSml.Rate.Units
                };
            }
        }

        private static POCD_MT000040Informant12 CreateInformantForSubstanceAdministration(RelatedPersonSML relatedPerson, PractitionerSML practitioner)
        {
            var informant = new POCD_MT000040Informant12();

            // RelatedPerson
            if (relatedPerson != null)
            {
                informant.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100052", null);
                informant.relatedEntity = new POCD_MT000040RelatedEntity()
                {
                    classCode = RoleClassMutualRelationship.PRS
                };
                
                    var person = new POCD_MT000040Person();


                    // RelatedPerson > identifier
                if (relatedPerson.Identifiers != null && relatedPerson.Identifiers.Any())
                    {
                        person.asEntityIdentifier = CreateEntityIdentifierArray(relatedPerson.Identifiers);
                    }

                    // RelatedPerson > relationship
                    if (relatedPerson.RelationshipToSubjectOfCare != null)
                    {
                        person.personalRelationship = new PersonalRelationship[]
                        {
                            new PersonalRelationship()
                            {
                                classCode = RoleClass.PRS,
                                id = CreateIdentifierElement(Guid.NewGuid().ToString()),
                                code = CreateConceptDescriptor(relatedPerson.RelationshipToSubjectOfCare),
                                asPersonalRelationship = new POCD_MT000040Patient
                                {
                                    classCode = EntityClass.PSN,
                                    determinerCode = EntityDeterminer.INSTANCE,
                                    determinerCodeSpecified = true,
                                    id = CreateIdentifierElement(Guid.NewGuid().ToString()),
                                    administrativeGenderCode = new CE
                                    {
                                        nullFlavor = NullFlavor.NA ,
                                        nullFlavorSpecified = true
                                    }
                                }
                            }
                        };
                    }

                    // RelatedPerson > name
                    if (relatedPerson.PersonNames != null && relatedPerson.PersonNames.Any())
                    {
                        person.name = CreatePersonNameArray(relatedPerson.PersonNames);
                    }

                    // RelatedPerson > telecom
                    if (relatedPerson.ElectronicCommunicationDetails != null &&
                        relatedPerson.ElectronicCommunicationDetails.Any())
                    {
                        informant.relatedEntity.telecom = CreateTelecomunicationArray(relatedPerson.ElectronicCommunicationDetails);
                    }

                    // RelatedPerson > gender
                    if (relatedPerson.Gender != null)
                    {
                        person.administrativeGenderCode = CreateCodedWithExtensionElement(
                            relatedPerson.Gender.GetAttributeValue<NameAttribute, string>(x => x.Code),
                            CodingSystem.Gender,
                            relatedPerson.Gender.GetAttributeValue<NameAttribute, string>(x => x.Name),
                            null, null,
                            null);
                    }

                    // RelatedPerson > birthDate
                    if (relatedPerson.DateOfBirth != null)
                    {
                        person.birthTime = CreateTimeStampElementIso(relatedPerson.DateOfBirth);
                    }

                    // RelatedPerson > address
                    if (relatedPerson.Addresses != null && relatedPerson.Addresses.Any())
                    {
                        informant.relatedEntity.addr = CreateAddressArray(relatedPerson.Addresses);
                    }

                    informant.relatedEntity.relatedPerson = person;
            }

            // Practitioner
            if (practitioner != null)
            {
                informant.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100053", null);
                informant.assignedEntity = new POCD_MT000040AssignedEntity()
                {
                    id = CreateIdentifierArray(Guid.NewGuid().ToString()),
                    code = CreateCodedWithExtensionElement(practitioner.Role)
                };

                var person = new POCD_MT000040Person();
                informant.assignedEntity.assignedPerson = person;

                    // Practitioner > identifier
                    if (practitioner.Identifiers != null && practitioner.Identifiers.Any())
                    {
                        person.asEntityIdentifier = CreateEntityIdentifierArray(practitioner.Identifiers);
                    }
                    
                    // RelatedPerson > name
                    if (practitioner.PersonNames != null && practitioner.PersonNames.Any())
                    {
                        person.name = CreatePersonNameArray(practitioner.PersonNames);
                    }

                    // RelatedPerson > telecom
                    if (practitioner.ElectronicCommunicationDetails != null &&
                        practitioner.ElectronicCommunicationDetails.Any())
                    {
                        informant.relatedEntity.telecom = CreateTelecomunicationArray(practitioner.ElectronicCommunicationDetails);
                    }
                    
                    // RelatedPerson > address
                    if (practitioner.Addresses != null && practitioner.Addresses.Any())
                    {
                        informant.relatedEntity.addr = CreateAddressArray(practitioner.Addresses);
                    }

                    // Practitioner > qualification
                    if (practitioner.Qualifications != null)
                    {
                        person.asQualifications = new Qualifications()
                        {
                            classCode = EntityClass.QUAL,
                            code = new CE()
                            {
                                originalText = CreateEncapsulatedData(practitioner.Qualifications)
                            }
                        };
                    }

                    // Practitioner > language
                    if (practitioner.Languages != null && practitioner.Languages.Any())
                    {
                        var languageList = new List<POCD_MT000040LanguageCommunication>();

                        foreach (var language in practitioner.Languages)
                        {
                            languageList.Add(new POCD_MT000040LanguageCommunication()
                            {
                                preferenceInd = new BL()
                                {
                                    value = language.PreferenceInd,
                                    valueSpecified = true
                                },
                                languageCode = new CS()
                                {
                                    code = language.LanguageCode.Code,
                                    codeSystem = language.LanguageCode.CodeSystemCode,
                                    displayName = language.LanguageCode.DisplayName,
                                    originalText = CreateEncapsulatedData(language.LanguageCode.OriginalText),
                                    codeSystemName = language.LanguageCode.CodeSystemName
                                }
                            });
                        }

                        person.languageCommunication = languageList.ToArray();
                    }
            }

            return informant;
        }


        private static POCD_MT000040Consumable CreateSubstanceAdministrationConsumable(
            MedicineItemSML medicineItemSml, List<POCD_MT000040EntryRelationship> substanceAdministrationEntryRelationships)
        {
            // MedicationStatement > Medication
            var consumable = new POCD_MT000040Consumable
            {
                manufacturedProduct = new POCD_MT000040ManufacturedProduct
                {
                    templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100068")
                }
            };

            //  MedicationStatement > Medication > Medication brand
            if (medicineItemSml?.Medication?.BrandName != null)
            {
                POCD_MT000040EntryRelationship brandEntry = new POCD_MT000040EntryRelationship();
                brandEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                brandEntry.act = new POCD_MT000040Act();
                brandEntry.act.classCode = x_ActClassDocumentEntryAct.ACT;
                brandEntry.act.moodCode = x_DocumentActMood.EVN;
                brandEntry.act.code = new CD
                {
                    code = "1402141000168102",
                    codeSystem = "2.16.840.1.113883.6.96",
                    displayName = "Branded product name",
                };
                brandEntry.act.text = new ST
                {
                    Text = new[] { medicineItemSml.Medication.BrandName }
                };
                substanceAdministrationEntryRelationships.Add(brandEntry);
            }

            // MedicationStatement > Medication > Generic name
            if (medicineItemSml?.Medication?.GenericName != null)
            {
                POCD_MT000040EntryRelationship genericEntry = new POCD_MT000040EntryRelationship();
                genericEntry.typeCode = x_ActRelationshipEntryRelationship.COMP;
                genericEntry.act = new POCD_MT000040Act();
                genericEntry.act.classCode = x_ActClassDocumentEntryAct.ACT;
                genericEntry.act.moodCode = x_DocumentActMood.EVN;
                genericEntry.act.code = new CD
                {
                    code = "1402131000168106",
                    codeSystem = "2.16.840.1.113883.6.96",
                    displayName = "Generic product name",
                };

                    genericEntry.act.text = new ST
                    {
                        Text = new[] { medicineItemSml.Medication.GenericName }
                    };

                substanceAdministrationEntryRelationships.Add(genericEntry);
            }

            consumable.manufacturedProduct.manufacturedMaterial = new POCD_MT000040Material()
            {
                determinerCode = EntityDeterminerDetermined.KIND,
				determinerCodeSpecified = true
            };

            // MedicationStatement > Medication > Code
            if (medicineItemSml?.Medication?.ItemCode != null)
            {
                consumable.manufacturedProduct.manufacturedMaterial.code = CreateCodedWithExtensionElement(medicineItemSml.Medication.ItemCode);
                // Requirement around Medicine Identifier: 028794
                // The CodingSystem MUST be specified so default to "OTH" and add nullFlavor if not defined
                if (string.IsNullOrEmpty(consumable.manufacturedProduct.manufacturedMaterial.code.code))
                {
                    consumable.manufacturedProduct.manufacturedMaterial.code.nullFlavor = NullFlavor.OTH;
                    consumable.manufacturedProduct.manufacturedMaterial.code.nullFlavorSpecified = true;
                }
                
            }

            // MedicationStatement > Medication > Manufacturer
            if (medicineItemSml.Medication.Manufacturer != null)
            {
                consumable.manufacturedProduct.manufacturerOrganization = CreateOrganisation((IOrganisationName)medicineItemSml.Medication.Manufacturer);
                consumable.manufacturedProduct.manufacturerOrganization.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100071");
                consumable.manufacturedProduct.manufacturerOrganization.id = CreateIdentifierArray(Guid.NewGuid().ToString());
            }

            // MedicationStatement > Medication > Form
            if (medicineItemSml.Medication.Form != null)
            {
                consumable.manufacturedProduct.manufacturedMaterial.formCode = new CD
                {
                    code = medicineItemSml?.Medication.Form.Code,
                    codeSystem = medicineItemSml.Medication.Form.CodeSystemCode,
                    displayName = medicineItemSml.Medication.Form.DisplayName
                };
            }

            // MedicationStatement > Package > Batch > LotNumber
            if (!string.IsNullOrWhiteSpace(medicineItemSml.Medication.BatchLotNumber))
            {
                consumable.manufacturedProduct.manufacturedMaterial.lotNumberText = CreateStructuredText(medicineItemSml.Medication.BatchLotNumber);
            }

            // MedicationStatement > Package > Batch > ExpirationDate
            if (medicineItemSml.Medication.BatchExpirationDate != null)
            {
                consumable.manufacturedProduct.manufacturedMaterial.expirationTime = CreateTimeStampElementIso(medicineItemSml.Medication.BatchExpirationDate);
            }

            // MedicationStatement > Medication > Ingredient
            if (medicineItemSml.Medication.Ingredients != null)
            {
                IList<Ingredient> ingredients = new List<Ingredient>();
                foreach (IngredientsSML ingredientsSml in medicineItemSml.Medication.Ingredients)
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.ingredientManufacturedMaterial = new ManufacturedMaterial();
                    ingredient.ingredientManufacturedMaterial.code = new CV
                    {
                        code = ingredientsSml.IngredientCode.Code,
                        codeSystem = ingredientsSml.IngredientCode.CodeSystemCode,
                        displayName = ingredientsSml.IngredientCode.DisplayName
                    };

                    if (!string.IsNullOrWhiteSpace(ingredientsSml.IngredientDescription))
                    {
                        ingredient.ingredientManufacturedMaterial.code.originalText = CreateEncapsulatedData(ingredientsSml.IngredientDescription);
                    }

                    ingredient.quantity = new RTO_PQ_PQ
                    {
                        numerator = new PQ
                        {
                            value = ingredientsSml.IngredientQuantity.Numerator.Value,
                            unit = ingredientsSml.IngredientQuantity.Numerator.Units
                        },
                        denominator = new PQ
                        {
                            value = ingredientsSml.IngredientQuantity.Denominator.Value,
                            unit = ingredientsSml.IngredientQuantity.Denominator.Units
                        }
                    };

                    ingredients.Add(ingredient);
                }
                consumable.manufacturedProduct.manufacturedMaterial.asIngredient = ingredients.ToArray();
            }

            return consumable;
        }

        private static POCD_MT000040EntryRelationship CreateEncounterForMedicationStatement(EncounterSML encounterSml)
        {
            List<POCD_MT000040EntryRelationship> encounterEntryRelationships = new List<POCD_MT000040EntryRelationship>();

            var encounter = new POCD_MT000040Encounter()
            {
                classCode = ActClass.ENC,
                moodCode =  x_DocumentEncounterMood.EVN,
                templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100062"),
                id = CreateIdentifierArray(encounterSml.EncounterId.ToString())
            };

            // encounter-description
            if (!string.IsNullOrWhiteSpace(encounterSml.EncounterDescription))
            {
                encounter.text = CreateEncapsulatedData(encounterSml.EncounterDescription);
            }

            // status
            encounter.statusCode = new CS
            {
                code = encounterSml.EncounterStatus.Code
            };

            // class
            if (encounterSml.EncounterClass != null)
            {
                encounter.code = CreateConceptDescriptor(encounterSml.EncounterClass);
            }

            // type
            if (encounterSml.EncounterType != null)
            {
                encounterEntryRelationships.Add(new POCD_MT000040EntryRelationship()
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    observation = new POCD_MT000040Observation()
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = new CD()
                        {
                            code = "103.17018",
                            codeSystem = "1.2.36.1.2001.1001.101",
                            displayName = "Category"
                        },
                        value = new ANY[]
                        {
                            CreateConceptDescriptor(encounterSml.EncounterType)
                        }
                    }
                });
            }

            // period
            encounter.effectiveTime = CreateIntervalTimestamp(encounterSml.EncounterPeriod, null);

            // reason
            if (encounterSml.EncounterReason != null)
            {
                encounterEntryRelationships.Add(new POCD_MT000040EntryRelationship()
                {
                    typeCode = x_ActRelationshipEntryRelationship.RSON,
                    observation = new POCD_MT000040Observation()
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = new CD()
                        {
                            code = "103.10141",
                            codeSystem = "1.2.36.1.2001.1001.101",
                            displayName = "Clinical Indication"
                        },
                        statusCode = new CS() { code = "completed" },
                        value = new ANY[]
                        {
                            CreateConceptDescriptor(encounterSml.EncounterReason)
                        }
                    }
                });
            }

            if (encounterEntryRelationships.Any())
            {
                encounter.entryRelationship = encounterEntryRelationships.ToArray();
            }

            return new POCD_MT000040EntryRelationship()
            {
                typeCode = x_ActRelationshipEntryRelationship.COMP,
                encounter = encounter
            };
        }

        private static POCD_MT000040AssignedAuthor CreateAssignedAuthorPractitioner(IParticipationAuthorHealthcareProvider medicationsAuthorRole, POCD_MT000040Section section)
        {
            POCD_MT000040AssignedAuthor assignedAuthor = null;

            if (medicationsAuthorRole != null)
            {
                // practitioner
                assignedAuthor = new POCD_MT000040AssignedAuthor()
                {
                    id = CreateIdentifierArray(CreateGuid(), null),
                    assignedPerson = new POCD_MT000040Person()
                    {
                        templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100040", null, null),

                    }
                };

                if (medicationsAuthorRole.Participant?.Person != null)
                {
                    var participant = medicationsAuthorRole.Participant;
                    var person = medicationsAuthorRole.Participant.Person;

                    // identifier
                    if (person.Identifiers != null)
                    {
                        assignedAuthor.assignedPerson.asEntityIdentifier = CreateEntityIdentifierArray(medicationsAuthorRole.Participant?.Person?.Identifiers);
                    }

                    // name
                    if (person.PersonNames != null && person.PersonNames.Count > 0)
                    {
                        assignedAuthor.assignedPerson.name = CreatePersonNameArray(person.PersonNames);
                    }

                    // telecom
                    if (participant.ElectronicCommunicationDetails != null && participant.ElectronicCommunicationDetails.Count > 0)
                    {
                        assignedAuthor.telecom = CreateTelecomunicationArray(participant.ElectronicCommunicationDetails);
                    }

                    // address
                    if (participant.Addresses != null && participant.Addresses.Count > 0)
                    {
                        assignedAuthor.addr = CreateAddressArray(participant.Addresses);
                    }

                    // qualification
                    if (!string.IsNullOrWhiteSpace(participant.Qualifications))
                    {
                        assignedAuthor.assignedPerson.asQualifications = CreateQualifications(participant.Qualifications);
                    }

                    // communication
                    // TODO: Check with Phil where it is
                }
            }

            return assignedAuthor;
        }


        /// <summary>
        /// Creates a Event Details Component
        /// </summary>
        /// <param name="eventDetails">A event details object</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(EventDetails eventDetails,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (eventDetails != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.16672", CodingSystem.NCTIS, "Event Overview", "Event Details",
                        "")
                };

                // Begin Reason for Encounter Description
                if (eventDetails.ClinicalSynopsisDescription != null)
                    entryList.Add(
                        CreateEntryActEvent(
                            CreateConceptDescriptor("102.15513", CodingSystem.NCTIS, "Clinical Synopsis", null),
                            CreateStructuredText(eventDetails.ClinicalSynopsisDescription, null)));

                component.section.entry = entryList.ToArray();

                component.section.text = eventDetails.CustomNarrativeEventDetails ??
                                         narrativeGenerator.CreateNarrative(eventDetails);
            }

            return component;
        }

        /// <summary>
        /// Creates an Diagnoses Intervention
        /// </summary>
        /// <param name="diagnosesIntervention">Diagnoses Interventions</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(DiagnosesIntervention diagnosesIntervention,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (diagnosesIntervention != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.16117", CodingSystem.NCTIS, "Medical History",
                        "Diagnoses/Interventions", "")
                };

                //PROBLEM / DIAGNOSIS
                if (diagnosesIntervention.ProblemDiagnosis != null && diagnosesIntervention.ProblemDiagnosis.Any())
                {
                    foreach (var diagnosis in diagnosesIntervention.ProblemDiagnosis)
                        CreateProblemDiagnosisEntry(diagnosis as ProblemDiagnosis, ref entryList);
                }

                // PROCEDURES
                if (diagnosesIntervention.Procedures != null && diagnosesIntervention.Procedures.Any())
                {
                    entryList.AddRange(CreateProcedureEntries(diagnosesIntervention.Procedures));
                }

                // MEDICAL HISTORY ITEM
                if (diagnosesIntervention.UncategorisedMedicalHistoryItem != null &&
                    diagnosesIntervention.UncategorisedMedicalHistoryItem.Any())
                {
                    entryList.AddRange(CreateProcedureEntries(diagnosesIntervention.UncategorisedMedicalHistoryItem,
                        new CodableText
                        {
                            Code = "102.16627",
                            CodeSystem = CodingSystem.NCTIS,
                            DisplayName = "Uncategorised Medical History Item"
                        }));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = diagnosesIntervention.CustomNarrativeDiagnosesIntervention ??
                                         narrativeGenerator.CreateNarrative(diagnosesIntervention);
            }

            return component;
        }

        /// <summary>
        /// Creates a reviewed medications component
        /// </summary>
        /// <param name="medications">medications</param>
        /// <param name="customNarrative">Provide a custom Narrative</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<IMedication> medications,
            StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null && medications.Any())
            {

                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medications") };

                var relationshipList = new List<POCD_MT000040EntryRelationship>();

                foreach (var medication in medications)
                {
                    // Medications History
                    relationshipList.Clear();

                    //Clinical Indication
                    if (!String.IsNullOrEmpty(medication.ClinicalIndication))
                    {

                        relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.RSON,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN, false,
                            CreateConceptDescriptor("103.10141",
                                CodingSystem.NCTIS,
                                "Clinical Indication",
                                null),
                            CreateStructuredText(medication.ClinicalIndication, null),
                            null));
                    }

                    //Additional Comments
                    if (!String.IsNullOrEmpty(medication.Comment))
                    {
                        relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN, false,
                            CreateConceptDescriptor(
                                "103.16044",
                                CodingSystem.NCTIS,
                                "Comment",
                                null),
                            CreateStructuredText(medication.Comment, null), null));
                    }

                    //medicine_list + directions_list
                    entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                        x_DocumentSubstanceMood.EVN, false,
                        CreateStructuredText(medication.Directions),
                        null, null, null,
                        medication.Medicine == null
                            ? null
                            : CreateCodedWithExtensionElement(
                                medication.Medicine),
                        null,
                        relationshipList,
                        medication.Id));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(medications);
            }

            return component;
        }

        /// <summary>
        /// Creates a reviewed medications component
        /// </summary>
        /// <param name="medications">medications</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IMedications medications,
            INarrativeGenerator narrativeGenerator, CDADocumentType DocType)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medication Orders",
                        "Medications", "")
                };

                var relationshipList = new List<POCD_MT000040EntryRelationship>();

                if (medications.Medications != null)
                {
                    foreach (var medication in medications.Medications)
                    {
                        // Medications History
                        relationshipList.Clear();

                        //Clinical Indication
                        if (!medication.ClinicalIndication.IsNullOrEmptyWhitespace())
                        {
                            relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.RSON,
                                x_ActClassDocumentEntryAct.INFRM,
                                x_DocumentActMood.EVN, false,
                                CreateConceptDescriptor("103.10141",
                                    CodingSystem.NCTIS,
                                    "Clinical Indication",
                                    null),
                                CreateStructuredText(medication.ClinicalIndication, null),
                                null));
                        }

                        //Additional Comments (or MedicationInstructionComment for Service Referral)
                        if (!medication.Comment.IsNullOrEmptyWhitespace())
                        {

                            relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                x_ActClassDocumentEntryAct.INFRM,
                                x_DocumentActMood.EVN, false,
                                CreateConceptDescriptor(
                                    AdminCodes.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                    CodingSystem.NCTIS,
                                    AdminCodes.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                    null),
                                CreateStructuredText(medication.Comment, null), null));
                        }


                        //medicine_list + directions_list
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                            x_DocumentSubstanceMood.EVN, false,
                            CreateStructuredText(medication.Directions), null,
                            null, null,
                            medication.Medicine == null ? null : CreateCodedWithExtensionElement(medication.Medicine),
                            null, relationshipList, null));
                    }
                }

                // Exclusions
                if (medications.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(medications.ExclusionStatement, "103.16302.120.1.2"));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = medications.CustomNarrativeMedications ??
                                         narrativeGenerator.CreateNarrative(medications);

            }

            return component;
        }

        /// <summary>
        /// Creates a medications component
        /// </summary>
        /// <param name="medications">IMedicationsEReferral</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IMedicationsEReferral medications,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medications") };

                if (medications.MedicationsList != null)
                {
                    foreach (var medication in medications.MedicationsList)
                    {
                        // Medications History
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                            x_DocumentSubstanceMood.EVN, false,
                            CreateStructuredText(medication.Directions),
                            null, null, null,
                            medication.Medicine == null
                                ? null
                                : CreateCodedWithExtensionElement(
                                    medication.Medicine),
                            null, null, null));
                    }
                }

                // Exclusions
                if (medications.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(medications.ExclusionStatement, "103.16302.2.2.1"));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = medications.CustomNarrativeMedications ??
                                         narrativeGenerator.CreateNarrative(medications);
            }

            return component;
        }


        /// <summary>
        /// Creates a medications component
        /// </summary>
        /// <param name="medications">A list of Medication Items</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<IMedicationItem> medications,
            StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null && medications.Any())
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medication Orders",
                        "Medications", "")
                };

                if (medications.Any())
                {
                    foreach (var medication in medications)
                    {
                        var relationshipList = new List<POCD_MT000040EntryRelationship>();

                        relationshipList.AddRange(CreateRelationshipForMedication(medication, "Additional Comments"));

                        // Medications
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                            x_DocumentSubstanceMood.EVN, false,
                            CreateStructuredText(medication.Directions),
                            null, null, null,
                            medication.Medicine == null ? null : CreateCodedWithExtensionElement(medication.Medicine),
                            null, relationshipList,
                            null, "active", null, null, null, null, null, null));
                    }
                }

                component.section.entry = entryList.ToArray();
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(medications);
            }

            return component;
        }

        /// <summary>
        /// Creates a medications component
        /// </summary>
        /// <param name="medications">IMedicationsSpecialistLetter</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IMedicationsSpecialistLetter medications,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medications != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16146", CodingSystem.NCTIS, "Medications") };

                if (medications.MedicationsList != null)
                {
                    foreach (var medication in medications.MedicationsList)
                    {
                        var relationshipList = new List<POCD_MT000040EntryRelationship>();

                        relationshipList.AddRange(CreateRelationshipForMedication(medication, "Additional Comments"));

                        //bool? negationInd = null;
                        //if ((medication.ChangeType != null && medication.ChangeType.Code == ChangeType.Ceased.GetAttributeValue<NameAttribute, string>(x => x.Code)) &&
                        //    (medication.RecommendationOrChange != null && medication.RecommendationOrChange.Code == RecomendationOrChange.TheChangeHasBeenMade.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                        //  negationInd = true;

                        // Medications
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                            x_DocumentSubstanceMood.EVN, false,
                            CreateStructuredText(medication.Directions),
                            null, null, null,
                            medication.Medicine == null ? null : CreateCodedWithExtensionElement(medication.Medicine),
                            null,
                            relationshipList,
                            null,
                            "active",
                            null, null, null, null, null, null));
                    }
                }

                // Exclusions
                if (medications.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(medications.ExclusionStatement, "103.16302.132.1.1"));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = medications.CustomNarrativeMedications ??
                                         narrativeGenerator.CreateNarrative(medications);
            }

            return component;
        }

        /// <summary>
        /// Creates a physical Measurement component
        /// </summary>
        /// <param name="physicalMeasurement">A List of PhysicalMeasurement</param>
        /// <param name="customNarrative">Custom narrative for the list physicalMeasurements  </param>
        /// <param name="narrativeGenerator">INarrativeGenerator</param>
        /// <returns>A List of POCD_MT000040Component3</returns>
        internal static List<POCD_MT000040Component3> CreateComponent(PhysicalMeasurement physicalMeasurement,
            StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            var componentList = new List<POCD_MT000040Component3>();

            if (physicalMeasurement != null)
            {

                POCD_MT000040Component3 component = null;

                var entriesList = new List<POCD_MT000040Entry>();

                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(
                        PhysicalMeasurementDocumentSections.PhysicalMeasurements
                            .GetAttributeValue<NameAttribute, string>(x => x.Code),
                        (CodingSystem)Enum.Parse(typeof(CodingSystem),
                            PhysicalMeasurementDocumentSections.PhysicalMeasurements
                                .GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                        PhysicalMeasurementDocumentSections.PhysicalMeasurements
                            .GetAttributeValue<NameAttribute, string>(x => x.Name),
                        null)
                };

                component.section.templateId = CreateIdentifierArray(
                    PhysicalMeasurementDocumentSections.PhysicalMeasurements.GetAttributeValue<NameAttribute, string>(
                        x => x.Identifier), null);

                if (physicalMeasurement.HeadCircumference != null)
                    entriesList.Add(CreateEntryHeadCircumference(physicalMeasurement.HeadCircumference));

                if (physicalMeasurement.PhysicalMeasurementBodyWeight != null)
                    entriesList.Add(CreateEntryBodyWeight(physicalMeasurement.PhysicalMeasurementBodyWeight));

                if (physicalMeasurement.PhysicalMeasurementBodyHeightLength != null)
                    entriesList.Add(
                        CreateEntryBodyHeightLength(physicalMeasurement.PhysicalMeasurementBodyHeightLength));

                if (physicalMeasurement.PhysicalMeasurementBodyMassIndex != null)
                    entriesList.Add(CreateEntryBodyMassIndex(physicalMeasurement.PhysicalMeasurementBodyMassIndex));


                if (entriesList.Any())
                    component.section.entry = entriesList.ToArray();

                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(physicalMeasurement);

                componentList.Add(component);
            }

            return componentList;
        }


        /// <summary>
        /// Update the title of of the Component Section
        /// </summary>
        /// <param name="component">The component with the title to be replaced </param>
        /// <param name="title">The title to be updated</param>
        /// <returns></returns>
        public static POCD_MT000040Component3 UpdateComponentTitle(POCD_MT000040Component3 component, string title)
        {
            component.section.title = CreateStructuredText(title, null);
            return component;
        }

        /// <summary>
        /// Creates a Medicare View Exclusion Statement
        /// </summary>
        /// <param name="exclusionStatement">The Medicare View Exclusion Statement</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateMedicareViewExclusionStatement(
            ExclusionStatement exclusionStatement, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (exclusionStatement != null && !exclusionStatement.GeneralStatement.IsNullOrEmptyWhitespace())
            {
                var entryList = new List<POCD_MT000040Entry>();

                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(MedicareOverviewSections.MedicareOverviewExclusionStatement),
                };

                component.section.title = CreateStructuredText(exclusionStatement.SectionTitle, null);

                entryList.Add(CreateExclusionStatement(exclusionStatement.GeneralStatement, "103.16135.172.1.3",
                    "General Statement"));
                component.section.entry = entryList.ToArray();

                // Narrative
                component.section.text = exclusionStatement.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(exclusionStatement);
            }

            return component;
        }

        /// <summary>
        /// Creates a Medicare DVA Funded Services 
        /// </summary>
        /// <param name="medicareDvaFundedServicesHistory">The Medicare DVA Funded Services </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            MedicareDVAFundedServicesHistory medicareDvaFundedServicesHistory, INarrativeGenerator narrativeGenerator)
        {
            var entryList = new List<POCD_MT000040Entry>();

            POCD_MT000040Component3 component = null;
            var components = new List<POCD_MT000040Component5>();

            if (medicareDvaFundedServicesHistory != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(MedicareOverviewSections.MedicareDVAFundedServicesHistory)
                };

                if (medicareDvaFundedServicesHistory.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(
                        medicareDvaFundedServicesHistory.ExclusionStatement.GeneralStatement, "103.16135.172.1.5",
                        "General Statement"));

                    var section = CreateSectionCodeTitle
                    (
                        MedicareOverviewSections.MedicareDVAFundedServicesExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Code),
                        CodingSystem.NCTIS,
                        MedicareOverviewSections.MedicareDVAFundedServicesExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Name),
                        medicareDvaFundedServicesHistory.ExclusionStatement.SectionTitle,
                        ""
                    );

                    section.text = medicareDvaFundedServicesHistory.ExclusionStatement.CustomNarrative ??
                                   narrativeGenerator.CreateNarrative(medicareDvaFundedServicesHistory
                                       .ExclusionStatement);

                    section.entry = entryList.ToArray();

                    components.Add(new POCD_MT000040Component5 { section = section });
                }


                if (medicareDvaFundedServicesHistory.MedicarDVAFundedServices != null)
                {
                    var medicarDVAFundedServicesComponent = new POCD_MT000040Component5();

                    medicarDVAFundedServicesComponent.section = CreateSectionCodeTitle
                    (
                        MedicareOverviewSections.MedicareDVAFundedServices
                    );

                    if (medicareDvaFundedServicesHistory.MedicarDVAFundedServices.MedicareDVAFundedService != null &&
                        medicareDvaFundedServicesHistory.MedicarDVAFundedServices.MedicareDVAFundedService.Any()
                    )
                    {
                        entryList.AddRange(
                            medicareDvaFundedServicesHistory.MedicarDVAFundedServices.MedicareDVAFundedService.Select(
                                medicareDVAFundedService => CreateMedicareDVAFundedService(medicareDVAFundedService)));
                    }

                    medicarDVAFundedServicesComponent.section.entry = entryList.ToArray();

                    medicarDVAFundedServicesComponent.section.text =
                        medicareDvaFundedServicesHistory.MedicarDVAFundedServices.CustomNarrative ??
                        narrativeGenerator.CreateNarrative(medicareDvaFundedServicesHistory.MedicarDVAFundedServices);

                    components.Add(medicarDVAFundedServicesComponent);
                }

                // Add components 
                component.section.component = components.ToArray();

                // Narrative
                component.section.text = null;
            }

            return component;
        }

        /// <summary>
        /// Creates a Pharmaceutical Benefit Items Component
        /// </summary>
        /// <param name="pharmaceuticalBenefitsHistory">The PharmaceuticalBenefitsHistory </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            PharmaceuticalBenefitsHistory pharmaceuticalBenefitsHistory, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();
            var components = new List<POCD_MT000040Component5>();

            if (pharmaceuticalBenefitsHistory != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(MedicareOverviewSections.PharmaceuticalBenefitHistory)
                };

                if (pharmaceuticalBenefitsHistory.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(
                        pharmaceuticalBenefitsHistory.ExclusionStatement.GeneralStatement, "103.16135.172.1.4",
                        "General Statement"));

                    var section = CreateSectionCodeTitle
                    (
                        MedicareOverviewSections.PharmaceuticalBenefitItemsExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Code),
                        CodingSystem.NCTIS,
                        MedicareOverviewSections.PharmaceuticalBenefitItemsExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Name),
                        pharmaceuticalBenefitsHistory.ExclusionStatement.SectionTitle,
                        ""
                    );

                    section.text = pharmaceuticalBenefitsHistory.ExclusionStatement.CustomNarrative ??
                                   narrativeGenerator.CreateNarrative(pharmaceuticalBenefitsHistory.ExclusionStatement);

                    section.entry = entryList.ToArray();

                    components.Add(new POCD_MT000040Component5 { section = section });
                }

                if (pharmaceuticalBenefitsHistory.PharmaceuticalBenefitItems != null)
                {
                    var pharmaceuticalBenefitItemsComponent = new POCD_MT000040Component5();

                    pharmaceuticalBenefitItemsComponent.section = CreateSectionCodeTitle
                    (
                        MedicareOverviewSections.PharmaceuticalBenefitsItems
                    );

                    if (pharmaceuticalBenefitsHistory.PharmaceuticalBenefitItems.PharmaceuticalBenefitItemList !=
                        null &&
                        pharmaceuticalBenefitsHistory.PharmaceuticalBenefitItems.PharmaceuticalBenefitItemList.Any())
                    {
                        foreach (var pharmaceuticalBenefitItem in pharmaceuticalBenefitsHistory
                            .PharmaceuticalBenefitItems.PharmaceuticalBenefitItemList)
                        {
                            entryList.Add(
                                CreatePharmaceuticalBenefitItem(pharmaceuticalBenefitItem, narrativeGenerator));
                        }
                    }

                    pharmaceuticalBenefitItemsComponent.section.entry = entryList.ToArray();

                    pharmaceuticalBenefitItemsComponent.section.text =
                        pharmaceuticalBenefitsHistory.PharmaceuticalBenefitItems.CustomNarrative ??
                        narrativeGenerator.CreateNarrative(pharmaceuticalBenefitsHistory.PharmaceuticalBenefitItems);

                    components.Add(pharmaceuticalBenefitItemsComponent);
                }

                // Add components 
                component.section.component = components.ToArray();

                // Narrative
                component.section.text = null;
            }

            return component;
        }

        /// <summary>
        /// Creates a medical history component
        /// </summary>
        /// <param name="medicalHistory">IMedicalHistory</param>
        /// <param name="showExclusionStatements">
        /// This indicates wether exclusion statements should be included abiding 
        /// by the Guidelines for appropriate use of exclusion statements (Supplementary Guidance for Implementers)
        /// </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(MedicalHistory medicalHistory,
            bool showExclusionStatements, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (medicalHistory != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16117", CodingSystem.NCTIS, "Medical History", "") };

                //PROBLEM / DIAGNOSIS
                if (medicalHistory.ProblemDiagnosis != null && medicalHistory.ProblemDiagnosis.Any())
                    entryList.AddRange(CreateProblemDiagnosisEntries(medicalHistory.ProblemDiagnosis));

                if (showExclusionStatements)
                {
                    // PROBLEM DIAGNOSIS EXCLUSIONS
                    if (medicalHistory.ProblemDiagnosisExclusionStatement != null)
                        entryList.Add(CreateExclusionStatement(medicalHistory.ProblemDiagnosisExclusionStatement,
                            "103.16302.120.1.3"));

                    // PROCEDURE EXCLUSIONS
                    if (medicalHistory.ProceduresExclusionStatement != null)
                        entryList.Add(CreateExclusionStatement(medicalHistory.ProceduresExclusionStatement,
                            "103.16302.120.1.4"));
                }

                // PROCEDURES
                if (medicalHistory.Procedures != null && medicalHistory.Procedures.Any())
                    entryList.AddRange(CreateProcedureEntries(medicalHistory.Procedures));

                //OTHER MEDICAL HISTORY
                if (medicalHistory.MedicalHistoryItems != null && medicalHistory.MedicalHistoryItems.Any())
                    entryList.AddRange(CreateProcedureEntries(medicalHistory.MedicalHistoryItems,
                        new CodableText
                        {
                            Code = "102.16627",
                            CodeSystem = CodingSystem.NCTIS,
                            DisplayName = "Uncategorised Medical History Item"
                        }));

                component.section.entry = entryList.ToArray();
                component.section.text = medicalHistory.CustomNarrativeMedicalHistory ??
                                         narrativeGenerator.CreateNarrative(medicalHistory, showExclusionStatements);
            }

            return component;
        }

        /// <summary>
        /// Creates a document component for a list of IDocumentWithHealthEventEnded
        /// </summary>
        /// <param name="documents">List of documents</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="documentSections">An enum representing the section for consolidated view document item </param>
        /// <param name="documentProvenanceEnum">The type of document </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<IDocumentWithHealthEventEnded> documents,
            StrucDocText customNarrative, DocumentSections documentSections,
            DocumentProvenanceEnum documentProvenanceEnum, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            component = new POCD_MT000040Component3
            {
                section = CreateSectionCodeTitle(documentSections.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    (CodingSystem)Enum.Parse(typeof(CodingSystem),
                        documentSections.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                    documentSections.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    documentSections.GetAttributeValue<NameAttribute, string>(x => x.Title),
                    "")
            };

            if (documents != null && documents.Any())
            {
                entryList.AddRange(documents.Select(document =>
                    CreateDocument(document as Document, documentProvenanceEnum)));
                component.section.entry = entryList.ToArray();
            }

            component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(documents);

            return component;
        }

        /// <summary>
        /// Creates a document component for a list of IDocuments
        /// </summary>
        /// <param name="documents">List of documents</param>
        /// <param name="customNarrative">Provide a custom Narrative  </param>
        /// <param name="documentSections">An enum representing the section for consolidated view document item </param>
        /// <param name="documentProvenanceEnum">The type of document </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<IDocument> documents, StrucDocText customNarrative,
            DocumentSections documentSections, DocumentProvenanceEnum documentProvenanceEnum,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            component = new POCD_MT000040Component3
            {
                section = CreateSectionCodeTitle(documentSections.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    (CodingSystem)Enum.Parse(typeof(CodingSystem),
                        documentSections.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                    documentSections.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    documentSections.GetAttributeValue<NameAttribute, string>(x => x.Title),
                    "")
            };
            if (documents != null)
            {
                entryList.AddRange(documents.Select(document =>
                    CreateDocument(document as Document, documentProvenanceEnum)));
                component.section.entry = entryList.ToArray();
            }

            component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(documents);

            return component;
        }

        /// <summary>
        /// Creates a immunisations component
        /// </summary>
        /// <param name="immunisations">List of immunisations</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<IImmunisation> immunisations,
            StrucDocText customNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (immunisations != null && immunisations.Any())
            {

                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16638", CodingSystem.NCTIS, "Immunisations") };

                entryList.AddRange(CreateAdministeredImmunisations(immunisations));

                component.section.entry = entryList.ToArray();
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(immunisations);
            }

            return component;
        }

        /// <summary>
        /// Creates a reviewed immunisations component
        /// </summary>
        /// <param name="immunisations">ReviewedImmunisations</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(Immunisations immunisations,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (immunisations != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16638", CodingSystem.NCTIS, "Immunisations") };

                //ADMINISTERED IMMUNISATION
                if (immunisations.AdministeredImmunisation != null && immunisations.AdministeredImmunisation.Any())
                {
                    entryList.AddRange(CreateAdministeredImmunisations(immunisations.AdministeredImmunisation));
                }


                //EXCLUSION STATEMENT
                if (immunisations.ExclusionStatement != null)
                {
                    // "Immunisations - Exclusion Statement", ref strucDocTableList
                    entryList.Add(CreateExclusionStatement(immunisations.ExclusionStatement, "103.16302.120.1.5"));
                }

                component.section.entry = entryList.ToArray();
                component.section.text = immunisations.CustomNarrativeImmunisation ??
                                         narrativeGenerator.CreateNarrative(immunisations);

            }

            return component;
        }

        /// <summary>
        /// Creates a administration observations component
        /// </summary>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="patientId">Patient ID</param>
        /// <param name="showEntitlements">Show Entitlements</param>
        /// <param name="narrativeGenerator">The narrative generator with which to generate the narrative for this section / component</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateAdministrativeObservations(
            IParticipationSubjectOfCare subjectOfCareParticipation, StrucDocText customNarrative, String patientId,
            bool showEntitlements, INarrativeGenerator narrativeGenerator)
        {
            // Remove the Entitlements section from Subject Of Care
            if (!showEntitlements && subjectOfCareParticipation.Participant != null)
                subjectOfCareParticipation.Participant.Entitlements = null;

            return CreateAdministrativeObservations(null, subjectOfCareParticipation, customNarrative, patientId,
                narrativeGenerator);
        }

        /// <summary>
        /// Creates a administration observations component
        /// </summary>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="patientId">Patient ID</param>
        /// <param name="latestDateForFiltering">Latest Date For Filtering</param>
        /// <param name="narrativeGenerator">The narrative generator with which to generate teh narrative for this section / component</param>
        /// <param name="earliestDateForFiltering">Earliest Date For Filtering</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateAdministrativeObservationsFiltering(
            IParticipationSubjectOfCare subjectOfCareParticipation, StrucDocText customNarrative, String patientId,
            ISO8601DateTime earliestDateForFiltering, ISO8601DateTime latestDateForFiltering,
            INarrativeGenerator narrativeGenerator)
        {
            var component = CreateAdministrativeObservations(null, subjectOfCareParticipation, customNarrative,
                patientId, narrativeGenerator);

            var entry = new List<POCD_MT000040Entry>();
            entry.AddRange(component.section.entry);

            // EarliestDateForFiltering
            entry.Add(CreateEarliestDateForFiltering(earliestDateForFiltering));

            // LatestDateForFiltering
            entry.Add(CreateLatestDateForFiltering(latestDateForFiltering));

            component.section.entry = entry.ToArray();

            //NARRATIVE
            if (narrativeGenerator != null)
            {
                component.section.text = new StrucDocText();
                component.section.text = customNarrative ?? narrativeGenerator.CreateNarrative(
                                             subjectOfCareParticipation, patientId, true, earliestDateForFiltering,
                                             latestDateForFiltering);
            }
            else
            {
                component.section.text = null;
                component.section.title = null;
            }

            return component;
        }

        /// <summary>
        /// Creates a administration observations component
        /// </summary>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <param name="author">A IParticipationAuthorHealthcareProvider </param>
        /// <param name="customNarrative">Provide a custom Narrative </param>
        /// <param name="patientId">Patient ID</param>
        /// <param name="templateId">Template Id </param>
        /// <param name="narrativeGenerator">The narrative generator with which to generate the narrative for this section / component</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateAdministrativeObservations(
            IParticipationSubjectOfCare subjectOfCareParticipation, IParticipationAuthorHealthcareProvider author,
            StrucDocText customNarrative, String patientId, string templateId, INarrativeGenerator narrativeGenerator)
        {
            var component = CreateAdministrativeObservations(null, subjectOfCareParticipation, customNarrative,
                patientId, narrativeGenerator);

            if (templateId != null)
            {
                component.section.templateId = CreateIdentifierArray(templateId, null, null);
            }

            // Entitlements
            if (author != null && author.Participant != null &&
                (author.Participant.Person != null && (author.Participant.Person.Entitlements != null &&
                                                       author.Participant.Person.Entitlements.Any())))
            {
                var listCoverage = new List<Coverage2>(component.section.coverage2);

                listCoverage.AddRange(CreateEntitlements(author.Participant.Person.Entitlements,
                        author.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED, ParticipationType.HLD)
                    .ToArray());
                component.section.coverage2 = listCoverage.ToArray();
            }

            return component;
        }

        /// <summary>
        /// Creates a Pathology Test Results component
        /// </summary>
        /// <param name="imagingExaminationResults">List of IImagingExaminationResult</param>
        /// <param name="cdaDocumentType">The document type </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static List<POCD_MT000040Component5> CreateComponent(
            List<IImagingExaminationResult> imagingExaminationResults, CDADocumentType? cdaDocumentType,
            INarrativeGenerator narrativeGenerator)
        {
            var componentList = new List<POCD_MT000040Component5>();

            //IMAGING EXAMINATION RESULTS
            if (imagingExaminationResults != null && imagingExaminationResults.Any())
            {
                foreach (var imagingExaminationResult in imagingExaminationResults)
                {
                    var relationshipList = new List<POCD_MT000040EntryRelationship>();
                    var entryList = new List<POCD_MT000040Entry>();

                    //Create the Imaging Examination Result Component and section
                    var imagingExaminationResultComponent = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle("102.16145", CodingSystem.NCTIS, "Imaging Examination Result")
                    };

                    //Anatomical location image
                    if (imagingExaminationResult.AnatomicalSite != null &&
                        imagingExaminationResult.AnatomicalSite.Any())
                    {
                        foreach (var anatomicalSite in imagingExaminationResult.AnatomicalSite.Where(anatomicalSite =>
                            anatomicalSite.Images != null && anatomicalSite.Images.Any()))
                        {
                            relationshipList.AddRange(CreateRelationshipsForEachImage(anatomicalSite.Images));
                        }
                    }

                    //Imaging examination result status
                    if (imagingExaminationResult.ExaminationResultStatus != null)
                    {
                        relationshipList.Add(
                            CreateRelationshipForTestResultStatus(imagingExaminationResult.ExaminationResultStatus,
                                cdaDocumentType));
                    }

                    //Clinical information provided
                    if (!String.IsNullOrEmpty(imagingExaminationResult.ClinicalInformationProvided))
                    {
                        relationshipList.Add(
                            CreateRelationshipForProvidedClinicalInformation(imagingExaminationResult
                                .ClinicalInformationProvided));
                    }

                    //Findings
                    if (!String.IsNullOrEmpty(imagingExaminationResult.Findings))
                    {
                        relationshipList.Add(CreateRelationshipForFindings(imagingExaminationResult.Findings));
                    }

                    //Examination result group
                    if (imagingExaminationResult.ResultGroup != null && imagingExaminationResult.ResultGroup.Any())
                    {
                        relationshipList.AddRange(
                            CreateRelationshipForEachImagingResultGroup(imagingExaminationResult.ResultGroup));
                    }

                    //Examination result date / time 
                    if (imagingExaminationResult.ResultDateTime != null)
                    {
                        relationshipList.Add(CreateRelationshipForDateTime(imagingExaminationResult.ResultDateTime));
                    }

                    //Examination request details
                    if (imagingExaminationResult.ExaminationRequestDetails != null)
                    {
                        relationshipList.AddRange(
                            CreateRelationshipForExaminationRequest(imagingExaminationResult
                                .ExaminationRequestDetails));
                    }

                    //Create the observation entry with all the above relationships nested inside the observation
                    entryList.Add(CreateEntryObservation(x_ActRelationshipEntry.COMP,
                        CreateConceptDescriptor(
                            imagingExaminationResult.ExaminationResultName),
                        new[]
                        {
                            CreateCodedWithExtensionElement(imagingExaminationResult.Modality)
                        },
                        imagingExaminationResult.AnatomicalSite == null
                            ? null
                            : CreateConceptDescriptorsForAnatomicalSites(
                                imagingExaminationResult.AnatomicalSite).ToArray(),
                        null,
                        null,
                        null,
                        relationshipList,
                        imagingExaminationResult.ExaminationResultRepresentation,
                        null,
                        null,
                        null,
                        null,
                        null));

                    imagingExaminationResultComponent.section.entry = entryList.ToArray();
                    imagingExaminationResultComponent.section.text =
                        imagingExaminationResult.CustomNarrativeImagingExaminationResult ??
                        narrativeGenerator.CreateNarrative(imagingExaminationResult);

                    componentList.Add(imagingExaminationResultComponent);
                }
            }

            return componentList;
        }

        /// <summary>
        /// Creates a Pathology Test Results component
        /// </summary>
        /// <param name="pathologyTestResults">ListPathologyTestResult</param>
        /// <param name="cdaDocumentType">The document type </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static List<POCD_MT000040Component5> CreateComponent(
            List<SCSModel.Common.PathologyTestResult> pathologyTestResults, CDADocumentType? cdaDocumentType,
            INarrativeGenerator narrativeGenerator)
        {
            var componentList = new List<POCD_MT000040Component5>();

            //PATHOLOGY TEST RESULTS
            if (pathologyTestResults != null)
            {
                foreach (var pathologyTestResult in pathologyTestResults)
                {
                    var relationshipList = new List<POCD_MT000040EntryRelationship>();
                    var entryList = new List<POCD_MT000040Entry>();

                    // Narrative
                    //Create the Pathology Test Result Component and section
                    var pathologyTestResultComponent = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle("102.16144", CodingSystem.NCTIS, "Pathology Test Result")
                    };

                    //Create relationships covering the test Specimen details
                    if (pathologyTestResult.TestSpecimenDetail != null)
                    {
                        relationshipList.AddRange(
                            CreateRelationshipForEachSpecimenDetail(pathologyTestResult.TestSpecimenDetail,
                                cdaDocumentType));
                    }

                    //Create relationships covering the test result groups
                    if (pathologyTestResult.ResultGroup != null)
                    {
                        relationshipList.AddRange(
                            CreateRelationshipForEachTestResultGroup(pathologyTestResult.ResultGroup, cdaDocumentType));
                    }

                    //Create relationships covering the diagnostic Service
                    if (pathologyTestResult.DiagnosticService != null)
                    {
                        var code = CreateConceptDescriptor("310074003", CodingSystem.SNOMED, "pathology service", null,
                            null, null);

                        if (cdaDocumentType.HasValue && cdaDocumentType.Value == CDADocumentType.ServiceReferral)
                        {
                            code.displayName = "Pathology service";
                        }

                        relationshipList.Add(
                            CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP,
                                code,
                                CreateConceptDescriptor(
                                    pathologyTestResult.DiagnosticService.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Code),
                                    CodingSystem.HL7DiagnosticServiceSectionID,
                                    pathologyTestResult.DiagnosticService.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Name), null)));
                    }

                    //Create a relationship containing the status of the pathology test
                    if (pathologyTestResult.OverallTestResultStatus != null)
                    {
                        relationshipList.Add(
                            CreateRelationshipForTestResultStatus(pathologyTestResult.OverallTestResultStatus,
                                cdaDocumentType));
                    }

                    //Create a relationship containing the provided clinical information
                    relationshipList.Add(
                        CreateRelationshipForProvidedClinicalInformation(
                            pathologyTestResult.ClinicalInformationProvided));

                    //pathological diagnosis
                    if (pathologyTestResult.PathologicalDiagnosis != null &&
                        pathologyTestResult.PathologicalDiagnosis.Any())
                    {
                        relationshipList.Add(
                            CreateRelationshipForTestResultPathologicalDiagnosis(pathologyTestResult
                                .PathologicalDiagnosis));
                    }

                    //Test conclusion
                    if (!String.IsNullOrEmpty(pathologyTestResult.Conclusion))
                    {
                        relationshipList.Add(CreateRelationshipForTestResultConclusion(pathologyTestResult.Conclusion,
                            cdaDocumentType));
                    }

                    //test comment
                    if (!string.IsNullOrEmpty(pathologyTestResult.TestComment))
                    {
                        relationshipList.Add(CreateRelationshipForTestResultComment(pathologyTestResult.TestComment));
                    }

                    //Test request details; includes the requested test name(s) and identifier(s)
                    if (pathologyTestResult.TestRequestDetails != null && pathologyTestResult.TestRequestDetails.Any())
                    {
                        relationshipList.AddRange(
                            pathologyTestResult.TestRequestDetails.Select(CreateRelationshipForTestRequestDetails));
                    }

                    //pathology test result date time
                    if (pathologyTestResult.ObservationDateTime != null)
                    {
                        relationshipList.Add(
                            CreateRelationshipForTestResultDateTime(pathologyTestResult.ObservationDateTime));
                    }

                    //Added this relationship so as we can reference and display the test result representation 
                    //data within the narrative

                    // PW (30/5/17): THIS IS WRONG - should only be observation>value>reference
                    //if (pathologyTestResult.TestResultRepresentation != null && pathologyTestResult.TestResultRepresentation.ExternalData != null)
                    //{
                    //    POCD_MT000040EntryRelationship relationShip = CreateEntryRelationshipObservationMedia
                    //        (
                    //            x_ActRelationshipEntryRelationship.REFR,
                    //            pathologyTestResult.TestResultRepresentation.ExternalData
                    //        );

                    //    relationshipList.Add(relationShip);
                    //}

                    POCD_MT000040Participant2 reportingPathologist = null;

                    if (pathologyTestResult.ReportingPathologist != null)
                    {
                        reportingPathologist = CreateParticipant2(pathologyTestResult.ReportingPathologist);
                    }

                    //Create the observation entry with all the above relationships nested inside the observation
                    var observation = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                        CreateConceptDescriptor(pathologyTestResult.TestResultName),
                        null,
                        // Taken out DateTime now, because it did not appear to map to anything
                        new List<ANY>
                        {
                            pathologyTestResult.TestResultRepresentation != null &&
                            pathologyTestResult.TestResultRepresentation.ExternalData != null
                                ? CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation.ExternalData)
                                : (pathologyTestResult.TestResultRepresentation != null && !pathologyTestResult
                                       .TestResultRepresentation.Text.IsNullOrEmptyWhitespace()
                                    ? CreateEncapsulatedData(pathologyTestResult.TestResultRepresentation.Text)
                                    : null)
                        },
                        null,
                        relationshipList,
                        reportingPathologist != null
                            ? new List<POCD_MT000040Participant2> { reportingPathologist }
                            : null);

                    if (pathologyTestResult.ReportingPathologist != null)
                    {
                        // Entitlements
                        if (pathologyTestResult.ReportingPathologist.Participant != null &&
                            pathologyTestResult.ReportingPathologist.Participant.Entitlements != null &&
                            pathologyTestResult.ReportingPathologist.Participant.Entitlements.Any())
                        {
                            pathologyTestResultComponent.section.coverage2 = CreateEntitlements(
                                pathologyTestResult.ReportingPathologist.Participant.Entitlements,
                                pathologyTestResult.ReportingPathologist.Participant.UniqueIdentifier.ToString(),
                                RoleClass.ASSIGNED, ParticipationType.HLD).ToArray();
                        }
                    }

                    entryList.Add(observation);

                    pathologyTestResultComponent.section.entry = entryList.ToArray();

                    pathologyTestResultComponent.section.text =
                        pathologyTestResult.CustomNarrativePathologyTestResult ??
                        narrativeGenerator.CreateNarrative(pathologyTestResult);
                    componentList.Add(pathologyTestResultComponent);
                }
            }

            return componentList;
        }

        /// <summary>
        /// Creates a Diagnostic Investigations component
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigations</param>
        /// <param name="cdaDocumentType">The document type </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IDiagnosticInvestigations diagnosticInvestigations,
            CDADocumentType? cdaDocumentType, INarrativeGenerator narrativeGenerator)
        {
            //ES
            POCD_MT000040Component3 component = null;

            var componentList = new List<POCD_MT000040Component5>();

            if (diagnosticInvestigations != null)
            {
                if (diagnosticInvestigations.OtherTestResult != null && diagnosticInvestigations.OtherTestResult.Any())
                {
                    componentList.AddRange(diagnosticInvestigations.OtherTestResult.Select(otherTestResult =>
                        CreateOtherTestResult(otherTestResult, narrativeGenerator)));
                }

                // Diagnostic Investigations
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.20117", CodingSystem.NCTIS, "Diagnostic Investigations",
                        "This section contains the following subsections: Pathology Test Result, Imaging Examination Result and Requested Service(s)."),
                };

                if (diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations != null)
                    component.section.text = diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations;

                // REQUESTED SERVICE
                if (diagnosticInvestigations.RequestedService != null &&
                    diagnosticInvestigations.RequestedService.Any())
                {
                    componentList.AddRange(
                        CreateComponent(diagnosticInvestigations.RequestedService, narrativeGenerator).ToArray());
                }

                // Pathology Test Result
                if (diagnosticInvestigations.PathologyTestResult != null &&
                    diagnosticInvestigations.PathologyTestResult.Any())
                {
                    componentList.AddRange(CreateComponent(diagnosticInvestigations.PathologyTestResult,
                        cdaDocumentType, narrativeGenerator));
                }

                // Imaging Examination Result
                if (diagnosticInvestigations.ImagingExaminationResult != null &&
                    diagnosticInvestigations.ImagingExaminationResult.Any())
                {
                    componentList.AddRange(CreateComponent(diagnosticInvestigations.ImagingExaminationResult,
                        cdaDocumentType, narrativeGenerator));
                }

                component.section.component = componentList.ToArray();
            }

            return component;
        }

        /// <summary>
        /// Creates a Diagnostic Investigations component
        /// </summary>
        /// <param name="diagnosticInvestigations">IDiagnosticInvestigations</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IDiagnosticInvestigationsV1 diagnosticInvestigations,
            INarrativeGenerator narrativeGenerator)
        {
            //SREF
            POCD_MT000040Component3 component = null;

            var componentList = new List<POCD_MT000040Component5>();

            if (diagnosticInvestigations != null)
            {
                // Diagnostic Investigations
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle("101.20117", CodingSystem.NCTIS, "Diagnostic Investigations",
                        "This section contains the following subsections: Requested Service, Pathology Test Result and Imaging Examination Result.")
                };

                // Pathology Test Result
                if (diagnosticInvestigations.PathologyTestResult != null &&
                    diagnosticInvestigations.PathologyTestResult.Any())
                {
                    componentList.AddRange(CreateComponent(diagnosticInvestigations.PathologyTestResult,
                        CDADocumentType.ServiceReferral, narrativeGenerator));
                }

                // Imaging Examination Result
                if (diagnosticInvestigations.ImagingExaminationResult != null &&
                    diagnosticInvestigations.ImagingExaminationResult.Any())
                {
                    componentList.AddRange(CreateComponent(diagnosticInvestigations.ImagingExaminationResult,
                        CDADocumentType.ServiceReferral, narrativeGenerator));
                }

                // Requested Service
                if (diagnosticInvestigations.RequestedService != null &&
                    diagnosticInvestigations.RequestedService.Any())
                {
                    component.section.entry =
                        CreateEntry(diagnosticInvestigations.RequestedService, narrativeGenerator);
                }

                // Requested Service
                component.section.text = narrativeGenerator.CreateNarrative(
                    diagnosticInvestigations.RequestedService, ""
                );

                component.section.component = componentList.ToArray();

                // Custom Narrative Diagnostic Investigations
                if (diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations != null)
                {
                    component.section.text = diagnosticInvestigations.CustomNarrativeDiagnosticInvestigations;
                }
            }

            return component;
        }

        /// <summary>
        /// Creates a Response Details component
        /// </summary>
        /// <param name="responseDetails">IResponseDetails</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IResponseDetails responseDetails,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            var componetList = new List<POCD_MT000040Component5>();

            if (responseDetails != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16611", CodingSystem.NCTIS, "Response Details") };

                //Procedures
                if (responseDetails.Procedures != null && responseDetails.Procedures.Any())
                {
                    entryList.AddRange(responseDetails.Procedures.Select(procedure =>
                        CreateEntryProcedureEvent(CreateConceptDescriptor(procedure.ProcedureName))));
                }

                //Diagnosis
                if (responseDetails.Diagnoses != null && responseDetails.Diagnoses.Any())
                {
                    foreach (var diagnosis in responseDetails.Diagnoses)
                    {
                        entryList.Add(CreateEntryObservation(
                            CreateConceptDescriptor("282291009", CodingSystem.SNOMED, "Diagnosis interpretation", null),
                            CreateConceptDescriptor(diagnosis)));
                    }
                }

                //Other Diagnosis
                if (responseDetails.OtherDiagnosisEntries != null && responseDetails.OtherDiagnosisEntries.Any())
                {
                    if (responseDetails.OtherDiagnosisEntries != null)
                        foreach (var otherDiagnosis in responseDetails.OtherDiagnosisEntries)
                        {
                            entryList.Add(CreateEntryActEvent(
                                CreateConceptDescriptor("102.15513.132.1.1", CodingSystem.NCTIS,
                                    "Other Diagnosis Procedure Entry", null),
                                CreateStructuredText(otherDiagnosis, null)));
                        }
                }

                //Response narrative
                if (responseDetails.ResponseNarrative != null)
                {
                    entryList.Add(CreateEntryActEvent(
                        CreateConceptDescriptor("102.15513.132.1.2", CodingSystem.NCTIS, "Response Narrative", null),
                        CreateStructuredText(responseDetails.ResponseNarrative, null)));
                }

                component.section.component = componetList.ToArray();
            }

            if (component != null && entryList.Any())
            {
                component.section.entry = entryList.ToArray();
                component.section.text = responseDetails.CustomNarrativeResponseDetails ??
                                         narrativeGenerator.CreateNarrative(responseDetails);
            }

            return component;
        }

        /// <summary>
        /// Creates a Recommendations component
        /// </summary>
        /// <param name="recommendations">IRecommendations</param>
        /// <param name="narrativeGenerator">narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IRecommendations recommendations,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            var componetList = new List<POCD_MT000040Component5>();

            if (recommendations != null)
            {
                component = new POCD_MT000040Component3
                { section = CreateSectionCodeTitle("101.16606", CodingSystem.NCTIS, "Recommendations") };

                //Recommendation
                if (recommendations.Recommendation != null && recommendations.Recommendation.Any())
                {
                    foreach (var recomendation in recommendations.Recommendation)
                    {
                        //performers
                        List<POCD_MT000040Performer2> performers = null;

                        if (recomendation.Addressee != null)
                        {
                            performers = new List<POCD_MT000040Performer2>();

                            var addressee = recomendation.Addressee;

                            if (addressee.Participant != null)
                            {
                                performers.Add(CreatePerformer(addressee));
                            }
                        }

                        entryList.Add(CreateEntryActEventInterval(
                            CreateConceptDescriptor("102.20016", CodingSystem.NCTIS, "Recommendation", null),
                            CreateStructuredText(recomendation.Narrative, null), performers, recomendation.TimeFrame,
                            x_DocumentActMood.PRP));
                    }
                }

                component.section.component = componetList.ToArray();
            }

            // Exclusions
            if (recommendations != null && recommendations.ExclusionStatement != null)
            {
                entryList.Add(CreateExclusionStatement(recommendations.ExclusionStatement, "102.16134",
                    "Recommendations Exclusion"));
            }

            if (component != null && entryList.Any())
            {
                component.section.entry = entryList.ToArray();
                component.section.text = recommendations.CustomNarrativeRecommendations ??
                                         narrativeGenerator.CreateNarrative(recommendations);
            }

            return component;
        }

        /// <summary>
        /// Achievement - A set of consumer entered achievements
        /// </summary>
        /// <param name="achievements">A list of achievements</param>
        /// <param name="sectionTitle">A section title for the Achievement section</param>
        /// <param name="pathologyCustomNarrative">A custom narrative for the section</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<Achievement> achievements, string sectionTitle,
            StrucDocText pathologyCustomNarrative, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            component = new POCD_MT000040Component3
            {
                section = new POCD_MT000040Section
                {
                    code = CreateCodedWithExtensionElement(
                        CreateCodableText(ConsumerEnteredAchievementsSections.Achievements)),
                    id = CreateIdentifierElement(CreateGuid(), null),
                    title = CreateStructuredText(sectionTitle, null),
                    templateId =
                        CreateIdentifierArray(
                            ConsumerEnteredAchievementsSections.Achievements.GetAttributeValue<NameAttribute, string>(
                                x => x.Identifier), null),
                    entry = achievements.Select(achievement => CreateAchievement(achievement)).ToArray()
                }
            };

            component.section.text = pathologyCustomNarrative ?? narrativeGenerator.CreateNarrative(achievements);

            return component;
        }

        /// <summary>
        /// Creates an PrescriptionItem
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="prescriber">IParticipationPrescriber</param>
        /// <param name="participationPrescriberOrganisation">IParticipationPrescriberOrganisation</param>
        /// <param name="subjectOfCare">IParticipationSubjectOfCare</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(PCEHRPrescriptionItem item,
            IParticipationPrescriber prescriber,
            IParticipationPrescriberOrganisation participationPrescriberOrganisation,
            IParticipationSubjectOfCare subjectOfCare, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (item != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        code = CreateCodedWithExtensionElement(CreateCodableText(EtpRecordSections.PrescriptionItem)),
                        title = CreateStructuredText(
                            EtpRecordSections.PrescriptionItem.GetAttributeValue<NameAttribute, string>(x => x.Title),
                            null)
                    }
                };
            }

            var entries = CreatePrescriptionItemEntries(item);

            // List of Coverage2
            var coverages = new List<Coverage2>();
            // Entitlements Subject Of Care
            if (subjectOfCare.Participant != null && (subjectOfCare.Participant.Entitlements != null &&
                                                      subjectOfCare.Participant.Entitlements.Count > 0))
            {
                coverages.AddRange(CreateEntitlements(subjectOfCare.Participant.Entitlements,
                        subjectOfCare.Participant.UniqueIdentifier.ToString(), RoleClass.PAT, ParticipationType.BEN)
                    .ToArray());
            }

            // Entitlements Participation Prescriber
            if (prescriber.Participant != null && (prescriber.Participant.Person != null &&
                                                   (prescriber.Participant != null &&
                                                    (prescriber.Participant.Person.Entitlements != null &&
                                                     prescriber.Participant.Person.Entitlements.Count > 0))))
            {
                coverages.AddRange(CreateEntitlements(prescriber.Participant.Person.Entitlements,
                        prescriber.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED, ParticipationType.HLD)
                    .ToArray());
            }

            // Entitlements Prescriber Organization
            if (participationPrescriberOrganisation != null &&
                participationPrescriberOrganisation.Participant != null &&
                participationPrescriberOrganisation.Participant.Organisation != null &&
                participationPrescriberOrganisation.Participant.Entitlements != null &&
                participationPrescriberOrganisation.Participant.Entitlements.Any())
            {
                coverages.AddRange(CreateEntitlements(participationPrescriberOrganisation.Participant.Entitlements,
                    participationPrescriberOrganisation.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                    ParticipationType.HLD).ToArray());
            }

            if (component != null)
            {
                component.section.coverage2 = coverages.ToArray();

                if (entries != null)
                {
                    component.section.entry = entries.ToArray();
                }

                component.section.text = item.CustomNarrativePrescriptionItem ??
                                         narrativeGenerator.CreateNarrative(item, prescriber, subjectOfCare);
            }

            return component;
        }

        /// <summary>
        /// Creates an Dispense Item for ATS
        /// </summary>
        /// <param name="item">DispenseItem</param>
        /// <param name="subjectOfCare">A subject of care</param>
        /// <param name="dispensingOrganisation">The dispenser organisation</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(DispenseItem item,
            IParticipationSubjectOfCare subjectOfCare, IParticipationDispenserOrganisation dispensingOrganisation,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var entries = new List<POCD_MT000040Entry>();

            var substanceAdministrationEntryRelationships = new List<POCD_MT000040EntryRelationship>();
            var supplyEntryRelationships = new List<POCD_MT000040EntryRelationship>();

            if (item.QuantityToDispense != null &&
                !item.QuantityToDispense.QuantityDescription.IsNullOrEmptyWhitespace())
            {
                supplyEntryRelationships.Add(CreateEntryRelationshipACT
                (
                    x_ActRelationshipEntryRelationship.COMP,
                    x_ActClassDocumentEntryAct.ACT,
                    x_DocumentActMood.EVN,
                    false,
                    CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.QuantitySnomedCt)),
                    CreateEncapsulatedData(item.QuantityToDispense.QuantityDescription),
                    CreateIdentifierArray(new UniqueId())
                ));
            }

            if (item != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(
                        AdminCodes.DispenseItem.GetAttributeValue<NameAttribute, String>(x => x.Code),
                        CodingSystem.NCTIS,
                        AdminCodes.DispenseItem.GetAttributeValue<NameAttribute, String>(x => x.Name))
                };

                // Formula
                if (!String.IsNullOrEmpty(item.Formula))
                {
                    substanceAdministrationEntryRelationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                AdminCodes.Formula.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                AdminCodes.Formula.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            CreateStructuredText(item.Formula, null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Patient Category
                if (item.PatientCategory.HasValue)
                {
                    substanceAdministrationEntryRelationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            // NOTE THE SECTION BELOW IS HIGHLY LIKELY TO CHANGE AT THE NEXT COMMITTEE MEETING
                            CreateConceptDescriptor
                            (
                                item.PatientCategory.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                AdminCodes.PatientCategory.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                AdminCodes.PatientCategory.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                item.PatientCategory.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null,
                                null
                            ),
                            CreateStructuredText(
                                item.PatientCategory.GetAttributeValue<NameAttribute, String>(x => x.Name), null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // RACF Id
                if (!item.RACFId.IsNullOrEmptyWhitespace())
                {
                    substanceAdministrationEntryRelationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                "To be determined",
                                AdminCodes.RACFId.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                AdminCodes.RACFId.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                "To be determined",
                                null,
                                null
                            ),
                            CreateStructuredText(item.RACFId, null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }


                // Label Instruction
                if (!item.LabelInstruction.IsNullOrEmptyWhitespace())
                {
                    supplyEntryRelationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor
                            (
                                AdminCodes.LabelInstruction.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                AdminCodes.LabelInstruction.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            CreateStructuredText(item.LabelInstruction, null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Additional comments
                if (!item.AdditionalComments.IsNullOrEmptyWhitespace())
                {
                    substanceAdministrationEntryRelationships.Add(
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor
                            (
                                AdminCodes.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                AdminCodes.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            CreateStructuredText(item.AdditionalComments, null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Brand Substitution Occurred
                if (item.BrandSubstituteOccurred.HasValue)
                {
                    supplyEntryRelationships.Add
                    (
                        CreateEntryRelationshipObservation
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            false,
                            CreateConceptDescriptor
                            (
                                AdminCodes.BrandSubstitutionOccurred.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                AdminCodes.BrandSubstitutionOccurred.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            new List<ANY> { new BL { value = item.BrandSubstituteOccurred.Value, valueSpecified = true } }
                        )
                    );
                }

                // Create a Supply
                var entryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    supply = new POCD_MT000040Supply
                    {
                        moodCode = x_DocumentSubstanceMood.RQO,
                        // QuantityToDispense
                        quantity = ((item.QuantityToDispense != null) && (item.QuantityToDispense.Quantity != null))
                            ? new PQ
                            {
                                value = item.QuantityToDispense != null && item.QuantityToDispense.Quantity != null &&
                                        !item.QuantityToDispense.Quantity.Value.IsNullOrEmptyWhitespace()
                                    ? item.QuantityToDispense.Quantity.Value
                                    : null,
                                unit = item.QuantityToDispense != null && item.QuantityToDispense.Quantity != null
                                    ? item.QuantityToDispense.Quantity.Units
                                    : null
                            }
                            : null,
                        product = ((item.QuantityToDispense != null && item.QuantityToDispense.Unit != null) ||
                                   (item.PBSRPBSManufacturerCode != null) || (item.PBSExtemporaneousIngredient != null))
                            ? new POCD_MT000040Product
                            {
                                manufacturedProduct = new POCD_MT000040ManufacturedProduct
                                {
                                    manufacturerOrganization = item.PBSRPBSManufacturerCode == null
                                        ? null
                                        : new POCD_MT000040Organization
                                        {
                                            id = CreateIdentifierArray(item.PBSRPBSManufacturerCode)
                                        },
                                    manufacturedMaterial = new POCD_MT000040Material
                                    {
                                        formCode = item.QuantityToDispense != null &&
                                                   item.QuantityToDispense.Unit != null
                                            ? CreateConceptDescriptor(item.QuantityToDispense.Unit)
                                            : null,
                                        asIngredient =
                                            CreatePBSExtemporaneousIngredient(item.PBSExtemporaneousIngredient)
                                    }
                                }
                            }
                            : null,
                        // Dispense Item Id
                        id = item.DispenseItemIdentifier != null
                            ? CreateIdentifierArray(item.DispenseItemIdentifier)
                            : CreateIdentifierArray(CreateGuid(), null),
                        // DateTime of Dispense Event imapping
                        effectiveTime = new[] { new SXCM_TS { value = item.DateTimeOfDispenseEvent.ToString() } },
                        // independent Ind
                        independentInd = new BL { value = false, valueSpecified = true },
                        // Add supplyEntryRelationships
                        entryRelationship = supplyEntryRelationships.ToArray(),
                        // Begin Medical Benefit Category Type,
                        coverage = new[]
                        {
                            // ClaimCategoryType
                            item.ClaimCategoryType == ClaimCategoryType.Undefined
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    null as string,
                                    CreateConceptDescriptor
                                    (
                                        item.ClaimCategoryType.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.ClaimCategoryTypeValues,
                                        item.ClaimCategoryType.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                ),
                            // EarySupplyWithPharmaceuticalBenefit
                            item.EarySupplyWithPharmaceuticalBenefit.HasValue &&
                            item.EarySupplyWithPharmaceuticalBenefit.Value
                                ? CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    CreateGuid(),
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.EarlySupplyWithPharmaceuticalBenefit
                                            .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.EarlySupplyWithPharmaceuticalBenefit
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                )
                                : null,
                            // Under Co-payment
                            item.UnderCoPayment == ClaimCategoryType.Undefined
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    CreateGuid(),
                                    CreateConceptDescriptor
                                    (
                                        item.UnderCoPayment.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.ClaimCategoryTypeValues,
                                        item.UnderCoPayment.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                ),
                            // PBS Close The Gap Benefit
                            item.PBSCloseTheGapBenefit == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    item.PBSCloseTheGapBenefit,
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.PBSCloseTheGapBenefit.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.PBSCloseTheGapBenefit.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Name),
                                        null
                                    )
                                )
                        },
                    },
                    // Sequence Number 
                    sequenceNumber = item.NumberOfThisDispense != null
                        ? CreateIntegerElement(item.NumberOfThisDispense.Value, NullFlavor.NA, false)
                        : null

                };

                substanceAdministrationEntryRelationships.Add(entryRelationship);

                // PBS/RPBS Item Code
                if (item.PBSRPBSItemCode != null)
                {
                    if (item.TherapeuticGoodIdentification != null &&
                        item.TherapeuticGoodIdentification.Translations == null)

                        item.TherapeuticGoodIdentification.Translations = new List<ICodableTranslation>();
                    item.TherapeuticGoodIdentification.Translations.Add(item.PBSRPBSItemCode);
                }

                // Begin Prescription Item entry
                var substanceAdministrationEvent = CreateEntrySubstanceAdministrationEventATS
                (
                    x_ActRelationshipEntry.DRIV,
                    x_DocumentSubstanceMood.RQO,
                    true,
                    item.StatusCode.GetAttributeValue<NameAttribute, String>(x => x.Name),
                    null,
                    null,
                    CreateCodedWithExtensionElement
                    (
                        item.TherapeuticGoodIdentification
                    ),
                    item.MaximumNumberOfRepeats,
                    substanceAdministrationEntryRelationships,
                    null,
                    null,
                    item.PrescriptionItemIdentifier,
                    null,
                    null,
                    null,
                    null
                );

                entries.Add(substanceAdministrationEvent);
            }

            // list of coverage2
            var coverages = new List<Coverage2>();

            // entitlements for prescriber
            if (subjectOfCare != null && subjectOfCare.Participant != null &&
                subjectOfCare.Participant.Entitlements != null && subjectOfCare.Participant.Entitlements.Count > 0)
            {
                coverages.AddRange(CreateEntitlements(subjectOfCare.Participant.Entitlements,
                        subjectOfCare.Participant.UniqueIdentifier.ToString(), RoleClass.PAT, ParticipationType.BEN)
                    .ToArray());
            }

            // entitlements dispenser organisation
            if (dispensingOrganisation.Participant != null && dispensingOrganisation.Participant.Entitlements != null &&
                dispensingOrganisation.Participant.Entitlements.Count > 0)
            {
                coverages.AddRange(CreateEntitlements(dispensingOrganisation.Participant.Entitlements,
                    dispensingOrganisation.Participant.UniqueIdentifier.ToString(), RoleClass.SDLOC,
                    ParticipationType.HLD).ToArray());
            }

            if (component != null)
            {
                component.section.entry = entries.ToArray();
                component.section.coverage2 = coverages.ToArray();
            }

            component.section.text = narrativeGenerator.CreateNarrative(item);

            return component;
        }

        /// <summary>
        /// Creates a Australian Childhood Immunisation Register Component
        /// </summary>
        /// <param name="australianChildhoodImmunisationRegisterHistory">The Medicare DVA Funded Services </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            AustralianChildhoodImmunisationRegisterHistory australianChildhoodImmunisationRegisterHistory,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var components = new List<POCD_MT000040Component5>();


            if (australianChildhoodImmunisationRegisterHistory != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(MedicareOverviewSections
                        .AustralianChildhoodImmunisationRegisterHistory)
                };

                if (australianChildhoodImmunisationRegisterHistory.ExclusionStatement != null)
                {
                    var entries = new List<POCD_MT000040Entry>();
                    entries.Add(CreateExclusionStatement(
                        australianChildhoodImmunisationRegisterHistory.ExclusionStatement.GeneralStatement,
                        "103.16135.172.1.1", "General Statement"));

                    var section = CreateSectionCodeTitle
                    (
                        MedicareOverviewSections.AustralianChildhoodImmunisationRegisterHistoryExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Code),
                        CodingSystem.NCTIS,
                        MedicareOverviewSections.AustralianChildhoodImmunisationRegisterHistoryExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Name),
                        australianChildhoodImmunisationRegisterHistory.ExclusionStatement.SectionTitle,
                        ""
                    );

                    section.text = australianChildhoodImmunisationRegisterHistory.ExclusionStatement.CustomNarrative ??
                                   narrativeGenerator.CreateNarrative(australianChildhoodImmunisationRegisterHistory
                                       .ExclusionStatement);
                    section.entry = entries.ToArray();
                    components.Add(new POCD_MT000040Component5 { section = section });
                }

                if (australianChildhoodImmunisationRegisterHistory.AustralianChildhoodImmunisationRegisterEntries !=
                    null)
                {
                    var entries = new List<POCD_MT000040Entry>();

                    if (australianChildhoodImmunisationRegisterHistory.AustralianChildhoodImmunisationRegisterEntries
                            .AustralianChildhoodImmunisationRegisterDocumentLink != null)
                    {
                        entries.Add(CreateEntryLink(x_DocumentActMood.EVN,
                            CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections
                                .AustralianChildhoodImmunisationRegisterEntriesDocument)),
                            australianChildhoodImmunisationRegisterHistory
                                .AustralianChildhoodImmunisationRegisterEntries
                                .AustralianChildhoodImmunisationRegisterDocumentLink));
                    }

                    var section =
                        CreateSectionCodeTitle(MedicareOverviewSections.AustralianChildhoodImmunisationRegisterEntries);
                    if (
                        australianChildhoodImmunisationRegisterHistory.AustralianChildhoodImmunisationRegisterEntries
                            .AustralianChildhoodImmunisationRegisterEntry != null)
                        foreach (var australianChildhoodImmunisationRegisterEntry in
                            australianChildhoodImmunisationRegisterHistory
                                .AustralianChildhoodImmunisationRegisterEntries
                                .AustralianChildhoodImmunisationRegisterEntry)
                        {
                            if (australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry != null)
                                entries.Add(CreateVaccineAdministrationEntry(
                                    australianChildhoodImmunisationRegisterEntry.VaccineAdministrationEntry));

                            List<POCD_MT000040EntryRelationship> entryRelationship = null;
                            if (australianChildhoodImmunisationRegisterEntry.VaccineCancellationReason != null &&
                                australianChildhoodImmunisationRegisterEntry.VaccineCancellationReason.Any())
                            {
                                entryRelationship = australianChildhoodImmunisationRegisterEntry
                                    .VaccineCancellationReason.Select(CreateVaccineCancellationReason).ToList();
                            }

                            if (australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry != null)
                                entries.Add(CreateVaccineCancellationEntry(
                                    australianChildhoodImmunisationRegisterEntry.VaccineCancellationEntry,
                                    entryRelationship));
                        }

                    section.text =
                        australianChildhoodImmunisationRegisterHistory.AustralianChildhoodImmunisationRegisterEntries
                            .CustomNarrative ??
                        narrativeGenerator.CreateNarrative(australianChildhoodImmunisationRegisterHistory);
                    section.entry = entries.ToArray();

                    components.Add(new POCD_MT000040Component5 { section = section });

                }

                component.section.component = components.ToArray();

                // Narrative
                component.section.text = null;
            }

            return component;
        }

        /// <summary>
        /// Creates a Australian Organ Donor Register Component 
        /// </summary>
        /// <param name="australianOrganDonorRegisterDecisionInformation">The Australian OrganDonor Register Component </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            AustralianOrganDonorRegisterDecisionInformation australianOrganDonorRegisterDecisionInformation,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();
            var components = new List<POCD_MT000040Component5>();

            if (australianOrganDonorRegisterDecisionInformation != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(MedicareOverviewSections.AustralianOrganDonorRegisterComponent),
                };

                if (australianOrganDonorRegisterDecisionInformation.ExclusionStatement != null)
                {
                    entryList.Add(CreateExclusionStatement(
                        australianOrganDonorRegisterDecisionInformation.ExclusionStatement.GeneralStatement,
                        "103.16135.172.1.2", "General Statement"));

                    var section = CreateSectionCodeTitle
                    (
                        MedicareOverviewSections.AustralianOrganDonorRegisterDetailsExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Code),
                        CodingSystem.NCTIS,
                        MedicareOverviewSections.AustralianOrganDonorRegisterDetailsExclusionStatement
                            .GetAttributeValue<NameAttribute, string>(x => x.Name),
                        australianOrganDonorRegisterDecisionInformation.ExclusionStatement.SectionTitle,
                        ""
                    );

                    section.entry = entryList.ToArray();
                    section.text = australianOrganDonorRegisterDecisionInformation.ExclusionStatement.CustomNarrative ??
                                   narrativeGenerator.CreateNarrative(australianOrganDonorRegisterDecisionInformation
                                       .ExclusionStatement);
                    components.Add(new POCD_MT000040Component5 { section = section });
                }

                if (australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails != null)
                {

                    var pharmaceuticalBenefitItemsComponent = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle
                        (
                            MedicareOverviewSections.AustralianOrganDonorRegisterDetails
                        )
                    };


                    if (australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails != null)
                    {
                        entryList.Add(CreateAustralianOrganDonorRegisterEntry(
                            australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails
                                .AustralianOrganDonorRegisterEntry));

                        if (australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails
                                .AustralianOrganDonorRegisterDetailsDocumentLink != null)
                        {
                            entryList.Add(CreateEntryLink(x_DocumentActMood.EVN,
                                CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections
                                    .AustralianOrganDonorRegisterDetailsDocumentLink)),
                                australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails
                                    .AustralianOrganDonorRegisterDetailsDocumentLink));
                        }
                    }

                    pharmaceuticalBenefitItemsComponent.section.entry = entryList.ToArray();

                    pharmaceuticalBenefitItemsComponent.section.text =
                        australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails
                            .CustomNarrative ?? narrativeGenerator.CreateNarrative(
                            australianOrganDonorRegisterDecisionInformation.AustralianOrganDonorRegisterDetails);

                    components.Add(pharmaceuticalBenefitItemsComponent);
                }

                component.section.component = components.ToArray();

                // Narrative
                component.section.text = null;
            }

            return component;
        }

        /// <summary>
        /// Creates an IEPrescriptionItem for ATS
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="prescriber">IParticipationPrescriber</param>
        /// <param name="participationPrescriberOrganisation">IParticipationPrescriberOrganisation</param>
        /// <param name="subjectOfCare">IParticipationSubjectOfCare</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IEPrescriptionItem item,
            IParticipationPrescriber prescriber,
            IParticipationPrescriberOrganisation participationPrescriberOrganisation,
            IParticipationSubjectOfCare subjectOfCare, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var entries = new List<POCD_MT000040Entry>();

            if (item != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(
                        ATSEtpRecordSection.PrescriptionItem.GetAttributeValue<NameAttribute, String>(x => x.Code),
                        CodingSystem.NCTIS,
                        ATSEtpRecordSection.PrescriptionItem.GetAttributeValue<NameAttribute, String>(x => x.Name))
                };

                // DateTime Prescription Written
                component.section.author = new[]
                {
                    new POCD_MT000040Author
                    {
                        time = CreateTimeStampElementIso(item.DateTimePrescriptionWritten),
                        assignedAuthor = prescriber.Participant == null
                            ? null
                            : new POCD_MT000040AssignedAuthor
                            {
                                id = CreateIdentifierArray(prescriber.Participant.UniqueIdentifier.ToString(), null)
                            }
                    }
                };

                // Begin DateTime Prescription Expires
                if (item.DateTimePrescriptionExpires != null)
                {
                    entries.Add
                    (
                        CreateEntryObservationLegacy
                        (
                            x_ActRelationshipEntry.DRIV,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.DateTimePrescriptionExpires
                                    .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.DateTimePrescriptionExpires
                                    .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            item.DateTimePrescriptionExpires,
                            null,
                            null,
                            null
                        )
                    );
                }

                var relationships = new List<POCD_MT000040EntryRelationship>();

                // Long-Term
                if (item.Timing != null && item.Timing.LongTerm.HasValue)
                    relationships.Add
                    (
                        CreateEntryRelationshipObservation
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            ActClassObservation.OBS,
                            x_ActMoodDocumentObservation.EVN,
                            null,
                            null,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.LongTerm.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.LongTerm.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            null,
                            null,
                            new List<ANY> { CreateBoolean(item.Timing.LongTerm.Value, true) },
                            null
                        )
                    );

                // Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    relationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.Formula.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.Formula.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            CreateStructuredText(item.Formula, null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );

                // Supply
                var entryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    supply = new POCD_MT000040Supply
                    {
                        moodCode = x_DocumentSubstanceMood.RQO,
                        quantity = item.QuantityToDispense != null &&
                                   (item.QuantityToDispense.Quantity != null || item.QuantityToDispense.Unit != null)
                            ? new PQ
                            {
                                value = item.QuantityToDispense != null && item.QuantityToDispense.Quantity != null &&
                                        !item.QuantityToDispense.Quantity.Value.IsNullOrEmptyWhitespace()
                                    ? item.QuantityToDispense.Quantity.Value
                                    : null,
                                unit = item.QuantityToDispense != null && item.QuantityToDispense.Quantity != null
                                    ? item.QuantityToDispense.Quantity.Units
                                    : null
                            }
                            : null,
                        product = ((item.QuantityToDispense != null && item.QuantityToDispense.Unit != null) ||
                                   (item.PBSRPBSManufacturerCode != null))
                            ? new POCD_MT000040Product
                            {
                                manufacturedProduct = new POCD_MT000040ManufacturedProduct
                                {
                                    manufacturerOrganization = item.PBSRPBSManufacturerCode == null
                                        ? null
                                        : new POCD_MT000040Organization
                                        {
                                            id = CreateIdentifierArray(item.PBSRPBSManufacturerCode)
                                        },
                                    manufacturedMaterial = new POCD_MT000040Material
                                    {
                                        formCode = item.QuantityToDispense != null &&
                                                   item.QuantityToDispense.Unit != null
                                            ? CreateConceptDescriptor(item.QuantityToDispense.Unit)
                                            : null
                                    }
                                }
                            }
                            : null,
                        id = item.DispenseItemIdentifier != null
                            ? CreateIdentifierArray(item.DispenseItemIdentifier)
                            : null,
                        effectiveTime = item.MinimumIntervalBetweenRepeats == null
                            ? null
                            : new[]
                            {
                                // MinimumIntervalBetweenRepeats
                                new PIVL_TS
                                {
                                    period = new PQ
                                    {
                                        unit = item.MinimumIntervalBetweenRepeats.Units,
                                        value = !item.MinimumIntervalBetweenRepeats.Value.IsNullOrEmptyWhitespace()
                                            ? item.MinimumIntervalBetweenRepeats.Value
                                            : null,
                                    }
                                }
                            },
                        // Begin Brand Substitute allowed
                        subjectOf2 = item.BrandSubstituteNotAllowed == true
                            ? CreateBrandSubstituteAllowed
                            (
                                ActClass.SUBST,
                                ActMood.PERM,
                                CreateCodedWithExtensionElement
                                (
                                    "N",
                                    CodingSystem.HL7SubstanceAdminSubstitution,
                                    "Therapeutic",
                                    null,
                                    null,
                                    null
                                )
                            )
                            : null,
                        coverage = new[]
                        {
                            // PBS Prescription Type
                            item.PBSPrescriptionType.HasValue
                                ? CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    null as string,
                                    CreateConceptDescriptor
                                    (
                                        item.PBSPrescriptionType.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.PrescriptionTypeValues,
                                        item.PBSPrescriptionType.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null,
                                        null,
                                        null
                                    )
                                )
                                : null,
                            // MedicalBenefitCategoryType
                            item.MedicalBenefitCategoryType.HasValue
                                ? CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    null as string,
                                    CreateConceptDescriptor
                                    (
                                        item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Code),
                                        item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Version),
                                        CodingSystem.NCTISMedicalBenefitCategoryTypeValues
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Name),
                                        null,
                                        null
                                    )
                                )
                                : null,
                            // PBS Close The Gap Benefit
                            item.PBSCloseTheGapBenefit == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    item.PBSCloseTheGapBenefit,
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.PBSCloseTheGapBenefit.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.PBSCloseTheGapBenefit.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Name),
                                        null
                                    )
                                ),
                            // PBS RPBS Authority Approval Number
                            item.PBSRPBSAuthorityApprovalNumber == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    new Identifier
                                    {
                                        Root = "1.2.36.1.5001.1.1.3.2.3",
                                        Extension = item.PBSRPBSAuthorityApprovalNumber
                                    },
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.PBSRPBSAuthorityApprovalNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.PBSRPBSAuthorityApprovalNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                )
                        },
                        entryRelationship = item.QuantityToDispense != null &&
                                            !item.QuantityToDispense.QuantityDescription.IsNullOrEmptyWhitespace()
                            ? new[]
                            {
                                CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.INFRM,
                                    x_DocumentActMood.INT,
                                    false,
                                    CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.Quantity)),
                                    CreateEncapsulatedData(item.QuantityToDispense.QuantityDescription),
                                    CreateIdentifierArray(new UniqueId())
                                )
                            }
                            : null
                    },

                };
                relationships.Add(entryRelationship);

                // Timing Description
                if (item.Timing != null && !item.Timing.TimingDescription.IsNullOrEmptyWhitespace())
                {
                    relationships.Add(
                        CreateEntryRelationshipACT(
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.TimingDescription)),
                            CreateEncapsulatedData(item.Timing.TimingDescription),
                            null,
                            null as ISO8601DateTime
                        )
                    );
                }

                // Quantity Description
                if (item.StructuredDose != null && !item.StructuredDose.QuantityDescription.IsNullOrEmptyWhitespace())
                {
                    var test = CreateEntryRelationshipACT(
                        x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.QuantitySnomedCt)),
                        CreateEncapsulatedData(item.StructuredDose.QuantityDescription),
                        null,
                        null as ISO8601DateTime
                    );

                    relationships.Add(test);
                }

                // Reason For Therapeutic Good
                if (!item.ReasonForTherapeuticGood.IsNullOrEmptyWhitespace())
                {
                    relationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.RSON,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.ReasonForTherapeuticGood.GetAttributeValue<NameAttribute, String>(
                                    x => x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.ReasonForTherapeuticGood.GetAttributeValue<NameAttribute, String>(
                                    x => x.Name),
                                null
                            ),
                            CreateStructuredText(item.ReasonForTherapeuticGood, null),
                            null
                        )
                    );
                }

                // Additional comments
                if (!item.AdditionalComments.IsNullOrEmptyWhitespace())
                {
                    relationships.Add(
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.AdditionalComments.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.AdditionalComments.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            CreateStructuredText(item.AdditionalComments, null),
                            CreateIdentifierArray(CreateGuid(), null)
                        )
                    );
                }

                if (item.Timing != null)
                {
                    // Start Criterion
                    if (item.Timing.StopCriterion.HasValue && item.Timing.StopCriterion.Value ||
                        item.Timing.StartDate != null)
                    {
                        relationships.Add(
                            CreateEntryRelationshipControlAct(
                                x_ActRelationshipEntryRelationship.COMP,
                                ActClass.CACT,
                                x_DocumentActMood.EVN,
                                false,
                                CreateConceptDescriptor
                                (
                                    ATSEtpRecordSection.StartCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Code),
                                    CodingSystem.NCTIS,
                                    ATSEtpRecordSection.StartCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Name),
                                    null
                                ),
                                null,
                                null,
                                item.Timing.StartDate ?? null
                            )
                        );
                    }

                    // Stop Criterion
                    if (item.Timing.StopCriterion.HasValue && item.Timing.StopCriterion.Value ||
                        item.Timing.StopDate != null)
                    {
                        relationships.Add(
                            CreateEntryRelationshipControlAct(
                                x_ActRelationshipEntryRelationship.COMP,
                                ActClass.CACT,
                                x_DocumentActMood.EVN,
                                false,
                                CreateConceptDescriptor
                                (
                                    ATSEtpRecordSection.StopCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Code),
                                    CodingSystem.NCTIS,
                                    ATSEtpRecordSection.StopCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Name),
                                    null
                                ),
                                null,
                                null,
                                item.Timing.StopDate ?? null
                            )
                        );
                    }
                }

                // 3 items map to SubstanceAdministration EffectiveTime, this will change next ETP Release
                // SubstanceAdministration EffectiveTime
                var effectiveTimeList = new List<ITime>();
                if (item.Timing != null && item.Timing.StructuredTiming != null &&
                    item.Timing.StructuredTiming.EffectiveTime != null)
                    effectiveTimeList.AddRange(item.Timing.StructuredTiming.EffectiveTime);

                //if (item.Timing != null && item.Timing.DurationOfTreatment != null)
                //  effectiveTimeList.Add(item.Timing.DurationOfTreatment);

                //if (item.AdministrationDetails != null && item.AdministrationDetails.DoseDuration != null)
                //  effectiveTimeList.Add(item.AdministrationDetails.DoseDuration);

                // PBS/RPBS Item Code
                if (item.PBSRPBSItemCode != null)
                {
                    if (item.TherapeuticGoodIdentification.Translations == null)
                        item.TherapeuticGoodIdentification.Translations = new List<ICodableTranslation>();

                    item.TherapeuticGoodIdentification.Translations.Add(item.PBSRPBSItemCode);
                }

                // Begin Prescription Item entry
                var entry = CreateEntrySubstanceAdministrationEventATS
                (
                    x_ActRelationshipEntry.DRIV,
                    x_DocumentSubstanceMood.RQO,
                    true,
                    StatusCode.Active.GetAttributeValue<NameAttribute, String>(x => x.Name),
                    !item.Directions.IsNullOrEmptyWhitespace() ? CreateStructuredText(item.Directions, null) : null,
                    effectiveTimeList.Count > 0 ? effectiveTimeList : null,
                    CreateCodedWithExtensionElement
                    (
                        item.TherapeuticGoodIdentification
                    ),
                    item.MaximumNumberOfRepeats,
                    relationships,
                    item.PBSExtemporaneousIngredient,
                    item.StructuredDose,
                    item.PrescriptionItemIdentifier,
                    item.Timing != null ? item.Timing.PRN : null,
                    item.Timing != null ? item.Timing.NumberOfAdministrations : null,
                    item.MedicationInstructionIdentifier,
                    item.AdministrationDetails
                );

                entries.Add(entry);

                if (item.GroundsForConcurrentSupply != GroundsForConcurrentSupply.Undefined ||
                    item.StateAuthorityNumber != null || item.PBSRPBSAuthorityPrescriptionNumber != null ||
                    item.StreamlinedAuthorityApprovalNumber != null)
                    entry.substanceAdministration.consumable.manufacturedProduct.subjectOf1 = new[]
                    {
                        // GroundsForConcurrentSupply
                        item.GroundsForConcurrentSupply == GroundsForConcurrentSupply.Undefined
                            ? null
                            : CreatePolicy
                            (
                                ActClass.JURISPOL,
                                ActMood.PERM,
                                null as string,
                                CreateConceptDescriptor
                                (
                                    item.GroundsForConcurrentSupply.GetAttributeValue<NameAttribute, String>(
                                        x => x.Code),
                                    CodingSystem.NCTISConcurrentSupplyGroundsValues,
                                    item.GroundsForConcurrentSupply.GetAttributeValue<NameAttribute, String>(
                                        x => x.Name),
                                    item.GroundsForConcurrentSupply.GetAttributeValue<NameAttribute, String>(
                                        x => x.Name)
                                )
                            ),
                        // StateAuthorityNumber
                        item.StateAuthorityNumber != null
                            ? CreatePolicy
                            (
                                ActClass.JURISPOL,
                                ActMood.PERM,
                                item.StateAuthorityNumber,
                                CreateConceptDescriptor
                                (
                                    ATSEtpRecordSection.StateAuthorityNumber.GetAttributeValue<NameAttribute, String>(
                                        x => x.Code),
                                    CodingSystem.NCTIS,
                                    ATSEtpRecordSection.StateAuthorityNumber.GetAttributeValue<NameAttribute, String>(
                                        x => x.Name),
                                    null
                                )
                            )
                            : null,
                        // PBS RPBS Authority Prescription Number
                        item.PBSRPBSAuthorityPrescriptionNumber == null
                            ? null
                            : CreatePolicy
                            (
                                ActClass.JURISPOL,
                                ActMood.PERM,
                                new Identifier
                                {
                                    Root = "1.2.36.1.5001.1.1.3.2.2",
                                    Extension = item.PBSRPBSAuthorityPrescriptionNumber
                                },
                                CreateConceptDescriptor
                                (
                                    AdminCodes.PBSRPBSAuthorityPrescriptionNumber
                                        .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                    CodingSystem.NCTIS,
                                    AdminCodes.PBSRPBSAuthorityPrescriptionNumber
                                        .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                    null
                                )
                            ),

                        // Streamlined Authority Approval Number
                        item.StreamlinedAuthorityApprovalNumber == null
                            ? null
                            : CreatePolicy
                            (
                                ActClass.JURISPOL,
                                ActMood.PERM,
                                new Identifier
                                {
                                    Root = "1.2.36.1.2001.1005.24", Extension = item.StreamlinedAuthorityApprovalNumber
                                },
                                CreateConceptDescriptor
                                (
                                    AdminCodes.StreamlinedAuthorityApprovalNumber
                                        .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                    CodingSystem.NCTIS,
                                    AdminCodes.StreamlinedAuthorityApprovalNumber
                                        .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                    null
                                )
                            ),
                    };

                // Note Detail
                if (!item.NoteDetail.IsNullOrEmptyWhitespace())
                    entries.Add
                    (
                        CreateEntryActEvent
                        (
                            x_ActRelationshipEntry.DRIV,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.PrescriptionNoteDetail.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.PrescriptionNoteDetail.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            item.NoteDetail,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null
                        )
                    );

                if (component != null) component.section.entry = entries.ToArray();

                // List of Coverage2
                var coverages = new List<Coverage2>();

                // Entitlements Subject Of Care
                if (subjectOfCare.Participant.Entitlements != null && subjectOfCare.Participant.Entitlements.Count > 0)
                {
                    coverages.AddRange(CreateEntitlements(subjectOfCare.Participant.Entitlements,
                            subjectOfCare.Participant.UniqueIdentifier.ToString(), RoleClass.PAT, ParticipationType.BEN)
                        .ToArray());
                }

                // Entitlements Participation Prescriber
                if (prescriber.Participant.Person.Entitlements != null &&
                    prescriber.Participant.Person.Entitlements.Count > 0)
                {
                    coverages.AddRange(CreateEntitlements(prescriber.Participant.Person.Entitlements,
                            prescriber.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                            ParticipationType.HLD)
                        .ToArray());
                }

                // Entitlements Prescriber Organization
                if (participationPrescriberOrganisation != null &&
                    participationPrescriberOrganisation.Participant != null &&
                    participationPrescriberOrganisation.Participant.Organisation != null &&
                    participationPrescriberOrganisation.Participant.Entitlements != null &&
                    participationPrescriberOrganisation.Participant.Entitlements.Any())
                {
                    coverages.AddRange(CreateEntitlements(participationPrescriberOrganisation.Participant.Entitlements,
                        participationPrescriberOrganisation.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                        ParticipationType.HLD).ToArray());
                }

                if (component != null)
                {
                    component.section.coverage2 = coverages.ToArray();
                }

                component.section.text = item.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(item, prescriber, subjectOfCare);
            }

            return component;
        }


        /// <summary>
        /// Creates a prescriber Instruction Detail Component
        /// </summary>
        /// <param name="prescriberInstructionDetail">A prescriber instruction detail</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <param name="participationPrescriber">The ParticipationPrescriber</param>
        /// <param name="participationPrescriberOrganisation">The ParticipationPrescriberOrganisation</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(PrescriberInstructionDetail prescriberInstructionDetail,
            IParticipationPrescriber participationPrescriber,
            IParticipationPrescriberOrganisation participationPrescriberOrganisation,
            INarrativeGenerator narrativeGenerator)
        {


            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (prescriberInstructionDetail != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle
                    (
                        AdminCodes.PrescriberInstructionDetail.GetAttributeValue<NameAttribute, String>(x => x.Code),
                        CodingSystem.NCTIS,
                        AdminCodes.PrescriberInstructionDetail.GetAttributeValue<NameAttribute, String>(x => x.Name)
                    )
                };

                var authors = new List<POCD_MT000040Author>();

                authors.Add(CreateAuthor(participationPrescriber, participationPrescriberOrganisation));

                component.section.author = authors.ToArray();

                var relationshipList = new List<POCD_MT000040EntryRelationship>();

                // Prescriber Instruction Detail
                if (prescriberInstructionDetail.PrescriberInstructionSource != PrescriberInstructionSource.Undefined)
                {
                    relationshipList.Add
                    (
                        CreateEntryRelationshipObservation
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            false,
                            CreateConceptDescriptor
                            (
                                AdminCodes.PrescriberInstructionSource.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                AdminCodes.PrescriberInstructionSource.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            new List<ANY>
                            {
                                CreateConceptDescriptor
                                (
                                    null,
                                    null,
                                    null,
                                    prescriberInstructionDetail.PrescriberInstructionSource
                                        .GetAttributeValue<NameAttribute, String>(x => x.Name))
                            }
                        )
                    );
                }

                // Prescriber Instruction Detail
                if (prescriberInstructionDetail.PrescriberInstructionCommunicationMedium !=
                    PrescriberInstructionCommunicationMedium.Undefined)
                {
                    relationshipList.Add
                    (
                        CreateEntryRelationshipObservation
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            false,
                            CreateConceptDescriptor
                            (
                                AdminCodes.PrescriberInstructionCommunicationMedium
                                    .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                AdminCodes.PrescriberInstructionCommunicationMedium
                                    .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            new List<ANY>
                            {
                                CreateConceptDescriptor
                                (
                                    null,
                                    null,
                                    null,
                                    prescriberInstructionDetail.PrescriberInstructionCommunicationMedium
                                        .GetAttributeValue<NameAttribute, String>(x => x.Name))
                            }
                        )
                    );
                }

                // Medications
                var entry = CreateEntrySubstanceAdministrationEvent
                (
                    x_ActRelationshipEntry.DRIV,
                    x_DocumentSubstanceMood.RQO,
                    true,
                    CreateStructuredText(prescriberInstructionDetail.PrescriberInstruction, null),
                    null,
                    null,
                    prescriberInstructionDetail.PrescriberInstructionReceived,
                    null,
                    null,
                    relationshipList,
                    "new"
                );

                if (prescriberInstructionDetail.PrescriberInstructionRecipient != null)
                {
                    entry.substanceAdministration.participant = new[]
                    {
                        CreateParticipant2(prescriberInstructionDetail.PrescriberInstructionRecipient)
                    };
                }

                // List of Coverage2
                var coverages = new List<Coverage2>();

                // Entitlements Prescriber
                if (participationPrescriber != null && participationPrescriber.Participant != null &&
                    participationPrescriber.Participant.Person != null &&
                    participationPrescriber.Participant.Person.Entitlements != null &&
                    participationPrescriber.Participant.Person.Entitlements.Any())
                {
                    coverages.AddRange(CreateEntitlements(participationPrescriber.Participant.Person.Entitlements,
                        participationPrescriber.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                        ParticipationType.HLD).ToArray());
                }

                // Entitlements Prescriber Organization
                if (participationPrescriberOrganisation != null &&
                    participationPrescriberOrganisation.Participant != null &&
                    participationPrescriberOrganisation.Participant.Organisation != null &&
                    participationPrescriberOrganisation.Participant.Entitlements != null &&
                    participationPrescriberOrganisation.Participant.Entitlements.Any())
                {
                    coverages.AddRange(CreateEntitlements(participationPrescriberOrganisation.Participant.Entitlements,
                        participationPrescriberOrganisation.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                        ParticipationType.HLD).ToArray());
                }

                component.section.text = narrativeGenerator.CreateNarrative(prescriberInstructionDetail,
                    participationPrescriber, participationPrescriberOrganisation);

                component.section.coverage2 = coverages.ToArray();

                entryList.Add(entry);
                component.section.entry = entryList.ToArray();
            }

            return component;
        }

        /// <summary>
        /// Creates an IEPrescriptionItem
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="subjectOfCare">A subject of care</param>
        /// <param name="requesterNote">The requester note</param>
        /// <param name="dispensingOrganisation">The dispenser organisation</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(PrescriptionRequestItem item,
            IParticipationSubjectOfCare subjectOfCare, IParticipationDispenserOrganisation dispensingOrganisation,
            String requesterNote, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var entries = new List<POCD_MT000040Entry>();

            if (item != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(
                        ATSEtpRecordSection.PrescriptionRequestItem.GetAttributeValue<NameAttribute, String>(
                            x => x.Code),
                        CodingSystem.NCTIS,
                        ATSEtpRecordSection.PrescriptionRequestItem.GetAttributeValue<NameAttribute, String>(
                            x => x.Name))
                };

                var relationships = new List<POCD_MT000040EntryRelationship>();

                // Long-Term
                if (item.Timing != null && item.Timing.LongTerm.HasValue)
                    relationships.Add
                    (
                        CreateEntryRelationshipObservation
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            ActClassObservation.OBS,
                            x_ActMoodDocumentObservation.EVN,
                            null,
                            null,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.LongTerm.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.LongTerm.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            null,
                            null,
                            new List<ANY> { CreateBoolean(item.Timing.LongTerm.Value, true) },
                            null
                        )
                    );

                // Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                    relationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.Formula.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.Formula.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            CreateStructuredText(item.Formula, null),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );


                // Supply
                var entryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    supply = new POCD_MT000040Supply
                    {
                        moodCode = x_DocumentSubstanceMood.RQO,
                        quantity = item.QuantityToDispense == null || item.QuantityToDispense.Quantity == null
                            ? null
                            : new PQ
                            {
                                value = item.QuantityToDispense != null && item.QuantityToDispense.Quantity != null &&
                                        !item.QuantityToDispense.Quantity.Value.IsNullOrEmptyWhitespace()
                                    ? item.QuantityToDispense.Quantity.Value
                                    : null,
                                unit = item.QuantityToDispense != null && item.QuantityToDispense.Quantity != null
                                    ? item.QuantityToDispense.Quantity.Units
                                    : null
                            },
                        product = ((item.QuantityToDispense != null && item.QuantityToDispense.Unit != null) ||
                                   (item.PBSRPBSManufacturerCode != null))
                            ? new POCD_MT000040Product
                            {
                                manufacturedProduct = new POCD_MT000040ManufacturedProduct
                                {
                                    manufacturerOrganization = item.PBSRPBSManufacturerCode == null
                                        ? null
                                        : new POCD_MT000040Organization
                                        {
                                            id = CreateIdentifierArray(item.PBSRPBSManufacturerCode)
                                        },
                                    manufacturedMaterial = new POCD_MT000040Material
                                    {
                                        formCode = item.QuantityToDispense != null &&
                                                   item.QuantityToDispense.Unit != null
                                            ? CreateConceptDescriptor(item.QuantityToDispense.Unit)
                                            : null

                                    }
                                }
                            }
                            : null,
                        id = item.DispenseItemIdentifier != null
                            ? CreateIdentifierArray(item.DispenseItemIdentifier)
                            : null,
                        // Begin Brand Substitute allowed
                        subjectOf2 = item.BrandSubstituteNotAllowed == true
                            ? CreateBrandSubstituteAllowed
                            (
                                ActClass.SUBST,
                                ActMood.PERM,
                                CreateCodedWithExtensionElement
                                (
                                    "N",
                                    CodingSystem.HL7SubstanceAdminSubstitution,
                                    "Therapeutic",
                                    null,
                                    null,
                                    null
                                )
                            )
                            : null,
                        coverage = new[]
                        {
                            // PBS Prescription Type
                            item.PBSPrescriptionType.HasValue
                                ? CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    null as string,
                                    CreateConceptDescriptor
                                    (
                                        item.PBSPrescriptionType.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.PrescriptionTypeValues,
                                        item.PBSPrescriptionType.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null,
                                        null,
                                        null
                                    )
                                )
                                : null,
                            // MedicalBenefitCategoryType
                            item.MedicalBenefitCategoryType.HasValue
                                ? CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    null as string,
                                    CreateConceptDescriptor
                                    (
                                        item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Code),
                                        item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Version),
                                        CodingSystem.NCTISMedicalBenefitCategoryTypeValues
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        item.MedicalBenefitCategoryType.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Name),
                                        null,
                                        null
                                    )
                                )
                                : null,
                            // PBS Close The Gap Benefit
                            item.PBSCloseTheGapBenefit == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    item.PBSCloseTheGapBenefit,
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.PBSCloseTheGapBenefit.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.PBSCloseTheGapBenefit.GetAttributeValue<NameAttribute, String>(x =>
                                            x.Name),
                                        null
                                    )
                                ),
                            // PBS RPBS Authority Approval Number
                            item.PBSRPBSAuthorityApprovalNumber == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    new Identifier
                                    {
                                        Root = "1.2.36.1.5001.1.1.3.2.3",
                                        Extension = item.PBSRPBSAuthorityApprovalNumber
                                    },
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.PBSRPBSAuthorityApprovalNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.PBSRPBSAuthorityApprovalNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                ),
                            // PBS RPBS Authority Prescription Number
                            item.PBSRPBSAuthorityPrescriptionNumber == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    new Identifier
                                    {
                                        Root = "1.2.36.1.5001.1.1.3.2.2",
                                        Extension = item.PBSRPBSAuthorityPrescriptionNumber
                                    },
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.PBSRPBSAuthorityPrescriptionNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.PBSRPBSAuthorityPrescriptionNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                ),
                            // Streamlined Authority Approval Number
                            item.StreamlinedAuthorityApprovalNumber == null
                                ? null
                                : CreatePolicyOrAccount
                                (
                                    ActClass.COV,
                                    ActMood.PERM,
                                    new Identifier
                                    {
                                        Root = "1.2.36.1.2001.1005.24",
                                        Extension = item.StreamlinedAuthorityApprovalNumber
                                    },
                                    CreateConceptDescriptor
                                    (
                                        AdminCodes.StreamlinedAuthorityApprovalNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                        CodingSystem.NCTIS,
                                        AdminCodes.StreamlinedAuthorityApprovalNumber
                                            .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                        null
                                    )
                                )
                        },
                        entryRelationship = item.QuantityToDispense != null &&
                                            !item.QuantityToDispense.QuantityDescription.IsNullOrEmptyWhitespace()
                            ? new[]
                            {
                                CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.ACT,
                                    x_DocumentActMood.EVN,
                                    false,
                                    CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.QuantitySnomedCt)),
                                    CreateEncapsulatedData(item.QuantityToDispense.QuantityDescription),
                                    CreateIdentifierArray(new UniqueId())
                                )
                            }
                            : null
                    },

                };
                relationships.Add(entryRelationship);

                // Timing Description
                if (item.Timing != null && !item.Timing.TimingDescription.IsNullOrEmptyWhitespace())
                {
                    relationships.Add(
                        CreateEntryRelationshipACT(
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.TimingDescription)),
                            CreateEncapsulatedData(item.Timing.TimingDescription),
                            null,
                            null as ISO8601DateTime
                        )
                    );
                }

                // Quantity Description
                if (item.StructuredDose != null && !item.StructuredDose.QuantityDescription.IsNullOrEmptyWhitespace())
                {

                    var test = CreateEntryRelationshipACT(
                        x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.QuantitySnomedCt)),
                        CreateEncapsulatedData(item.StructuredDose.QuantityDescription),
                        null,
                        null as ISO8601DateTime
                    );

                    relationships.Add(test);
                }

                // Additional comments
                if (!item.AdditionalComments.IsNullOrEmptyWhitespace())
                {
                    relationships.Add(
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor
                            (
                                ATSEtpRecordSection.AdditionalComments.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                ATSEtpRecordSection.AdditionalComments.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            CreateStructuredText(item.AdditionalComments, null),
                            CreateIdentifierArray(CreateGuid(), null)
                        )
                    );
                }

                if (item.Timing != null)
                {
                    // Start Criterion
                    if (item.Timing.StopCriterion.HasValue && item.Timing.StopCriterion.Value ||
                        item.Timing.StartDate != null)
                    {
                        relationships.Add(
                            CreateEntryRelationshipControlAct(
                                x_ActRelationshipEntryRelationship.COMP,
                                ActClass.CACT,
                                x_DocumentActMood.EVN,
                                false,
                                CreateConceptDescriptor
                                (
                                    ATSEtpRecordSection.StartCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Code),
                                    CodingSystem.NCTIS,
                                    ATSEtpRecordSection.StartCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Name),
                                    null
                                ),
                                null,
                                null,
                                item.Timing.StartDate ?? null
                            )
                        );
                    }

                    // Stop Criterion
                    if (item.Timing.StopCriterion.HasValue && item.Timing.StopCriterion.Value ||
                        item.Timing.StopDate != null)
                    {
                        relationships.Add(
                            CreateEntryRelationshipControlAct(
                                x_ActRelationshipEntryRelationship.COMP,
                                ActClass.CACT,
                                x_DocumentActMood.EVN,
                                false,
                                CreateConceptDescriptor
                                (
                                    ATSEtpRecordSection.StopCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Code),
                                    CodingSystem.NCTIS,
                                    ATSEtpRecordSection.StopCriterion.GetAttributeValue<NameAttribute, String>(x =>
                                        x.Name),
                                    null
                                ),
                                null,
                                null,
                                item.Timing.StopDate ?? null
                            )
                        );
                    }
                }

                // 3 items map to SubstanceAdministration EffectiveTime, this will change next ETP Release
                // SubstanceAdministration EffectiveTime
                var effectiveTimeList = new List<ITime>();
                if (item.Timing != null && item.Timing.StructuredTiming != null)
                    effectiveTimeList.AddRange(item.Timing.StructuredTiming.EffectiveTime);

                //if (item.Timing != null && item.Timing.DurationOfTreatment != null)
                //  effectiveTimeList.Add(item.Timing.DurationOfTreatment);

                //if (item.AdministrationDetails != null && item.AdministrationDetails.DoseDuration != null)
                //  effectiveTimeList.Add(item.AdministrationDetails.DoseDuration);

                // PBS/RPBS Item Code
                if (item.PBSRPBSItemCode != null)
                {
                    if (item.TherapeuticGoodIdentification.Translations == null)
                        item.TherapeuticGoodIdentification.Translations = new List<ICodableTranslation>();

                    item.TherapeuticGoodIdentification.Translations.Add(item.PBSRPBSItemCode);
                }

                // Begin Prescription Item entry
                var entry = CreateEntrySubstanceAdministrationEventATS
                (
                    x_ActRelationshipEntry.DRIV,
                    x_DocumentSubstanceMood.RQO,
                    true,
                    "suspended",
                    !item.Directions.IsNullOrEmptyWhitespace() ? CreateStructuredText(item.Directions, null) : null,
                    effectiveTimeList.Count > 0 ? effectiveTimeList : null,
                    CreateCodedWithExtensionElement
                    (
                        item.TherapeuticGoodIdentification
                    ),
                    null,
                    relationships,
                    item.PBSExtemporaneousIngredient,
                    item.StructuredDose,
                    null,
                    item.Timing != null ? item.Timing.PRN : null,
                    item.Timing != null ? item.Timing.NumberOfAdministrations : null,
                    null,
                    item.AdministrationDetails
                );

                entries.Add(entry);

                if (item.StateAuthorityNumber != null)
                    entry.substanceAdministration.consumable.manufacturedProduct.subjectOf1 = new[]
                    {
                        // StateAuthorityNumber
                        item.StateAuthorityNumber != null
                            ? CreatePolicy
                            (
                                ActClass.JURISPOL,
                                ActMood.PERM,
                                item.StateAuthorityNumber,
                                CreateConceptDescriptor
                                (
                                    ATSEtpRecordSection.StateAuthorityNumber.GetAttributeValue<NameAttribute, String>(
                                        x => x.Code),
                                    CodingSystem.NCTIS,
                                    ATSEtpRecordSection.StateAuthorityNumber.GetAttributeValue<NameAttribute, String>(
                                        x => x.Name),
                                    null
                                )
                            )
                            : null
                    };


                // Note Detail
                if (!String.IsNullOrEmpty(requesterNote))
                    entries.Add
                    (
                        CreateEntryActEvent
                        (
                            x_ActRelationshipEntry.DRIV,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.RequesterNote)),
                            requesterNote,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null
                        )
                    );


                if (component != null) component.section.entry = entries.ToArray();

                // List of Coverage2
                var coverages = new List<Coverage2>();

                // Entitlements Subject Of Care
                if (subjectOfCare.Participant.Entitlements != null && subjectOfCare.Participant.Entitlements.Count > 0)
                {
                    coverages.AddRange(CreateEntitlements(subjectOfCare.Participant.Entitlements,
                            subjectOfCare.Participant.UniqueIdentifier.ToString(), RoleClass.PAT, ParticipationType.BEN)
                        .ToArray());
                }

                // entitlements dispenser organisation
                if (dispensingOrganisation != null && dispensingOrganisation.Participant != null &&
                    dispensingOrganisation.Participant.Entitlements != null &&
                    dispensingOrganisation.Participant.Entitlements.Count > 0)
                {
                    coverages.AddRange(CreateEntitlements(dispensingOrganisation.Participant.Entitlements,
                        dispensingOrganisation.Participant.UniqueIdentifier.ToString(), RoleClass.SDLOC,
                        ParticipationType.HLD).ToArray());
                }

                if (component != null)
                {
                    component.section.coverage2 = coverages.ToArray();
                }

                component.section.text = item.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(item, subjectOfCare, dispensingOrganisation,
                                             requesterNote);
            }

            return component;
        }

        /// <summary>
        /// Creates a observation component
        /// </summary>
        /// <param name="observation">IObservationWeightHeight</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IObservationWeightHeight observation,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            var entryList = new List<POCD_MT000040Entry>();

            if (observation != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(
                        ATSEtpRecordSection.Observations.GetAttributeValue<NameAttribute, String>(x => x.Code),
                        CodingSystem.NCTIS,
                        ATSEtpRecordSection.Observations.GetAttributeValue<NameAttribute, String>(x => x.Name))
                };

                component.section.entry = entryList.ToArray();

                // Body Weight
                // Create the observation entry with all the above relationships nested inside the observation
                if (observation.BodyWeight != null)
                    entryList.Add(CreateEntryObservationLegacy(x_ActRelationshipEntry.DRIV,
                        CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.BodyWeight)),
                        observation.BodyWeight.BodyWeightObservationTime, null, null,
                        null, observation.BodyWeight));

                // Body Height
                // Create the observation entry with all the above relationships nested inside the observation
                if (observation.BodyHeight != null)
                    entryList.Add(CreateEntryObservationLegacy(x_ActRelationshipEntry.DRIV,
                        CreateConceptDescriptor(CreateCodableText(ATSEtpRecordSection.BodyHeight)),
                        observation.BodyHeight.BodyHeightObservationTime, null, null,
                        null, observation.BodyHeight, null));

                component.section.entry = entryList.ToArray();
            }


            if (observation != null)
            {
                component.section.text = observation.CustomNarrative ?? narrativeGenerator.CreateNarrative(observation);
            }

            return component;
        }

        /// <summary>
        /// Creates an Dispense Item
        /// </summary>
        /// <param name="item">IParticipationDispenser</param>
        /// <param name="dispenser">IParticipationDispenser</param>
        /// <param name="dispenserOrganisation">IParticipationPrescriber</param>
        /// <param name="subjectOfCare">IParticipationSubjectOfCare</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(PCEHRDispenseItem item,
            IParticipationDispenser dispenser, IParticipationDispenserOrganisation dispenserOrganisation,
            IParticipationSubjectOfCare subjectOfCare, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var entries = new List<POCD_MT000040Entry>();

            if (item != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        code = CreateCodedWithExtensionElement(CreateCodableText(EtpRecordSections.DispenseItem)),
                        title = CreateStructuredText(
                            EtpRecordSections.DispenseItem.GetAttributeValue<NameAttribute, string>(x => x.Title), null)
                    }
                };

                // Add component entry array
                entries.Add(CreateDispenseItemEntryRelationship(item));

                // List of Coverage2
                var coverages = new List<Coverage2>();
                // Entitlements Subject Of Care
                if (subjectOfCare.Participant != null && (subjectOfCare.Participant.Entitlements != null &&
                                                          subjectOfCare.Participant.Entitlements.Count > 0))
                {
                    coverages.AddRange(CreateEntitlements(subjectOfCare.Participant.Entitlements,
                            subjectOfCare.Participant.UniqueIdentifier.ToString(), RoleClass.PAT, ParticipationType.BEN)
                        .ToArray());
                }

                // Entitlements Participation Prescriber
                if (dispenserOrganisation.Participant != null &&
                    (dispenserOrganisation.Participant.Entitlements != null &&
                     dispenserOrganisation.Participant.Entitlements.Count > 0))
                {
                    coverages.AddRange(CreateEntitlements(dispenserOrganisation.Participant.Entitlements,
                        dispenserOrganisation.Participant.UniqueIdentifier.ToString(), RoleClass.SDLOC,
                        ParticipationType.HLD).ToArray());
                }

                component.section.coverage2 = coverages.ToArray();

                component.section.entry = entries.ToArray();
                component.section.text = item.CustomNarrativeDispenseItem ??
                                         narrativeGenerator.CreateNarrative(item, dispenser, dispenserOrganisation,
                                             subjectOfCare);
            }

            return component;
        }

        #endregion

        #region POCD_MT000040Component4

        private static POCD_MT000040Component4 CreateComponentObservation(CD code, List<ANY> any, string textReferance,
            II[] identifier, II[] templateId)
        {
            var component = new POCD_MT000040Component4
            {
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,




                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = identifier,
                        templateId = templateId,
                        text = !textReferance.IsNullOrEmptyWhitespace()
                            ? new ED
                            {
                                reference = new TEL
                                {
                                    value = textReferance
                                }
                            }
                            : null,
                        code = code ?? null,
                        value = any != null ? any.ToArray() : null
                    }
            };

            return component;
        }

        private static POCD_MT000040Component4 CreateComponentObservation(CD code, List<ANY> any)
        {
            return CreateComponentObservation(code, any, null, null, null);
        }

        #endregion

        #region Component5


        /// <summary>
        /// Creates an IDiagnosticImagingReportContent Components
        /// </summary>
        /// <param name="imagingExaminationResults">A list of Pathology Test Results</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5[] CreateComponent(
            IList<IDiagnosticImagingExaminationResult> imagingExaminationResults,
            INarrativeGenerator narrativeGenerator)
        {
            var imagingExaminationResultComponents = new List<POCD_MT000040Component5>();

            if (imagingExaminationResults.Any())
            {
                foreach (var imagingExaminationResult in imagingExaminationResults)
                {
                    var relationshipList = new List<POCD_MT000040EntryRelationship>();
                    var entryList = new List<POCD_MT000040Entry>();

                    // Create the Imaging Examination Result Component and section
                    var imagingExaminationResultComponent = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle(DiagnosticImagingReportSections.ImagingExaminationResult)
                    };

                    // Imaging examination result status
                    if (!imagingExaminationResult.ExaminationProcedure.IsNullOrEmptyWhitespace())
                    {
                        // Examination Procedure
                        relationshipList.Add(CreateEntryRelationshipACT(
                            x_ActRelationshipEntryRelationship.REFR,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            null,
                            CreateConceptDescriptor(
                                CreateCodableText(DiagnosticImagingReportSections.ExaminationProcedure)),
                            CreateStructuredText(imagingExaminationResult.ExaminationProcedure),
                            new[] { CreateIdentifierElement(CreateGuid()) }
                        ));
                    }

                    if (imagingExaminationResult.OverallResultStatus != null)
                        relationshipList.Add(
                            CreateRelationshipForTestResultStatus(imagingExaminationResult.OverallResultStatus, null));

                    if (imagingExaminationResult.RelatedImage != null)
                    {
                        var relatedImages = CreateEntryRelationshipACT(x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            CreateConceptDescriptor(
                                CreateCodableText(DiagnosticImagingReportSections.RelatedInformation)),
                            null, null);

                        relatedImages.typeCode = x_ActRelationshipEntryRelationship.COMP;
                        relatedImages.act.reference = CreatesReferanceLink(
                            imagingExaminationResult.RelatedImage.ImageUrl,
                            imagingExaminationResult.RelatedImage.MediaType);
                        relationshipList.Add(relatedImages);
                    }

                    // Imaging examination result status
                    if (imagingExaminationResult.ExaminationDetails != null)
                    {

                        POCD_MT000040EntryRelationship imageDetails =
                            CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP,
                                ActClassObservation.OBS,
                                x_ActMoodDocumentObservation.EVN,
                                false,
                                imagingExaminationResult.ExaminationDetails.ImageDateTime ?? null,
                                CreateConceptDescriptor(
                                    CreateCodableText(DiagnosticImagingReportSections.ImageDetails)),
                                null,
                                null,
                                null,
                                null);

                        var entryRelationshipAct = CreateEntryRelationshipACT(
                            x_ActRelationshipEntryRelationship.SUBJ,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            true,
                            CreateConceptDescriptor(
                                CreateCodableText(DiagnosticImagingReportSections.ExaminationRequestDetails)),
                            null,
                            null,
                            new List<POCD_MT000040EntryRelationship>
                            {
                                imageDetails
                            });

                        relationshipList.Add(entryRelationshipAct);
                    }

                    // Examination result date / time 
                    if (imagingExaminationResult.ObservationDateTime != null)
                    {
                        relationshipList.Add(
                            CreateRelationshipForDateTime(imagingExaminationResult.ObservationDateTime));
                    }

                    if (imagingExaminationResult.AnatomicalSite != null)
                    {
                        relationshipList.Add(
                            CreateAnatomicalRegionRelationship(imagingExaminationResult.AnatomicalRegion));
                    }

                    // Create the observation entry with all the above relationships nested inside the observation
                    entryList.Add(
                        CreateEntryObservation(x_ActRelationshipEntry.COMP,
                            imagingExaminationResult.ExaminationResultName != null
                                ? CreateConceptDescriptor(imagingExaminationResult.ExaminationResultName)
                                : null,
                            imagingExaminationResult.Modality != null
                                ? new[]
                                {
                                    CreateCodedWithExtensionElement(imagingExaminationResult.Modality)
                                }
                                : null,
                            imagingExaminationResult.AnatomicalSite != null &&
                            imagingExaminationResult.AnatomicalSite.Any()
                                ? CreateConceptDescriptorForAnatomicalSites(
                                    imagingExaminationResult.AnatomicalSite,
                                    CreateCodableText(DiagnosticImagingReportSections.WithLaterality)
                                ).ToArray()
                                : new[]
                                {
                                    CreateConceptDescriptor(NullFlavour.NoInformation)
                                },
                            null,
                            null,
                            null,
                            relationshipList,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null)
                    );

                    if (entryList.Any()) imagingExaminationResultComponent.section.entry = entryList.ToArray();

                    imagingExaminationResultComponent.section.text =
                        imagingExaminationResult.CustomNarrativeImagingExaminationResult ??
                        narrativeGenerator.CreateNarrative(imagingExaminationResult);

                    imagingExaminationResultComponents.Add(imagingExaminationResultComponent);
                }
            }

            return !imagingExaminationResultComponents.Any() ? null : imagingExaminationResultComponents.ToArray();
        }

        /// <summary>
        /// Creates an Pathology Test Result Component
        /// </summary>
        /// <param name="pathologyTestResults">A list of Pathology Test Results</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5[] CreateComponent(
            IList<SCSModel.Pathology.PathologyTestResult> pathologyTestResults, INarrativeGenerator narrativeGenerator)
        {
            var pathologyTestResultComponents = new List<POCD_MT000040Component5>();

            if (pathologyTestResults.Any())
            {

                // Loop through pathologyTestResults
                foreach (var pathologyTestResult in pathologyTestResults)
                {
                    var entryList = new List<POCD_MT000040Entry>();

                    var pathologyTestResultComponent = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle(PatholodyResultReportSections.PathologyTestResult)
                    };

                    // set classCode and moodCode
                    pathologyTestResultComponent.section.classCode = ActClass.DOCSECT;
                    pathologyTestResultComponent.section.moodCode = ActMood.EVN;

                    var entryRelationships = new List<POCD_MT000040EntryRelationship>();

                    //PATHOLOGY TEST RESULT > Test Result Name (Pathology Test Result Name)
                    if (pathologyTestResult.TestResultName != null)
                    {
                        var entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                            CreateConceptDescriptor(pathologyTestResult.TestResultName),
                            null,
                            null,
                            null,
                            null);

                        // PATHOLOGY TEST RESULT > Department Code (Diagnostic Service)
                        if (pathologyTestResult.PathologyDiscipline.HasValue)
                        {
                            entryRelationships.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP,
                                false,
                                CreateConceptDescriptor(
                                    CreateCodableText(PatholodyResultReportSections.PathologyService)),
                                new List<ANY>
                                {
                                    CreateConceptDescriptor(
                                        CreateCodableText(pathologyTestResult.PathologyDiscipline.Value))
                                }
                            ));
                        }

                        // PATHOLOGY TEST RESULT > Overall Test Result Status (Overall Pathology Test Result Status)
                        if (pathologyTestResult.OverallTestResultStatus != null)
                        {
                            entryRelationships.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP,
                                false,
                                CreateConceptDescriptor(
                                    CreateCodableText(PatholodyResultReportSections.OverallTestResultStatus)),
                                new List<ANY>
                                {
                                    CreateConceptDescriptor(pathologyTestResult.OverallTestResultStatus)
                                }
                            ));
                        }

                        // Test Specimen Detail (SPECIMEN)
                        if (pathologyTestResult.TestSpecimenDetail != null)
                            entryRelationships.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.SUBJ,
                                ActClassObservation.OBS,
                                x_ActMoodDocumentObservation.EVN,
                                null,
                                pathologyTestResult.TestSpecimenDetail.CollectionDateTime ?? null,
                                CreateConceptDescriptor(CreateCodableText(PatholodyResultReportSections.Specimen)),
                                null,
                                null,
                                null,
                                null,
                                null,
                                null
                            ));

                        // PATHOLOGY TEST RESULT > Pathology Test Result DateTime
                        if (pathologyTestResult.ObservationDateTime != null)
                        {
                            entryRelationships.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP,
                                null,
                                pathologyTestResult.ObservationDateTime,
                                CreateConceptDescriptor(
                                    CreateCodableText(PatholodyResultReportSections.PathologyTestResultDateTime)),
                                null));
                        }

                        if (entryRelationships.Any())
                        {
                            entry.observation.entryRelationship = entryRelationships.ToArray();
                        }

                        entryList.Add(entry);
                    }

                    if (entryList.Any())
                    {
                        pathologyTestResultComponent.section.entry = entryList.ToArray();
                    }

                    if (pathologyTestResultComponent != null && pathologyTestResultComponent.section != null)
                    {
                        pathologyTestResultComponent.section.text =
                            pathologyTestResult.CustomNarrative ??
                            narrativeGenerator.CreateNarrative(pathologyTestResult);
                    }

                    pathologyTestResultComponents.Add(pathologyTestResultComponent);
                }
            }

            return !pathologyTestResultComponents.Any() ? null : pathologyTestResultComponents.ToArray();
        }

        /// <summary>
        /// Creates an Requested Service Component
        /// </summary>
        /// <param name="requestedServices">A list of Requested Services</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5[] CreateComponent(
            IList<SCSModel.Pathology.RequestedService> requestedServices, INarrativeGenerator narrativeGenerator)
        {
            var requestedServiceComponents = new List<POCD_MT000040Component5>();

            if (requestedServices.Any())
            {
                foreach (var requestedService in requestedServices)
                {
                    var entryRelationships = new List<POCD_MT000040EntryRelationship>();

                    var requestedServiceEntryList = new List<POCD_MT000040Entry>();

                    var requestedServiceComponent = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle(PatholodyResultReportSections.RequestedService)
                    };

                    // REQUESTED SERVICE > Requested Service DateTime
                    if (requestedService.RequestedServiceDateTime != null)
                    {
                        entryRelationships.Add(CreateEntryRelationshipACT(
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor(
                                CreateCodableText(PatholodyResultReportSections.RequestedServiceDateTime)),
                            null,
                            null,
                            requestedService.RequestedServiceDateTime
                        ));
                    }

                    // REQUESTED SERVICE > Requested Service Description
                    if (requestedService.RequestedServiceDescription != null &&
                        requestedService.ServiceBookingStatus.HasValue)
                    {
                        requestedServiceEntryList.Add(
                            CreateEntryActEvent
                            (
                                x_ActRelationshipEntry.COMP,
                                x_ActClassDocumentEntryAct.ACT,
                                ((x_DocumentActMood)Enum.Parse(typeof(x_DocumentActMood),
                                    requestedService.ServiceBookingStatus.GetAttributeValue<NameAttribute, string>(x =>
                                        x.Code))),
                                CreateConceptDescriptor(requestedService.RequestedServiceDescription),
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                entryRelationships
                            ));
                    }

                    // Service Requester
                    if (requestedService.ServiceRequester != null)
                    {
                        requestedServiceComponent.section.author =
                            new[] { CreateAuthor(requestedService.ServiceRequester) };
                    }

                    // Requested Service EntryList
                    if (requestedServiceEntryList.Any())
                    {
                        requestedServiceComponent.section.entry = requestedServiceEntryList.ToArray();
                    }

                    // Requested Service Narrative
                    if (requestedServiceComponent.section != null)
                    {
                        requestedServiceComponent.section.text =
                            requestedService.CustomNarrative ?? narrativeGenerator.CreateNarrative(requestedService);
                    }

                    requestedServiceComponents.Add(requestedServiceComponent);
                }
            }

            return !requestedServiceComponents.Any() ? null : requestedServiceComponents.ToArray();
        }

        /// <summary>
        /// Creates an Authority To Post Component
        /// </summary>
        /// <param name="authorityToPost">The AuthorityToPost class</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <param name="reportStudy">The report study identifier</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(AuthorityToPost authorityToPost,
            ICodableText reportStudy, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 authorityToPostComponent = null;
            var participant = new List<POCD_MT000040Participant2>();

            if (authorityToPost != null)
            {
                authorityToPostComponent = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PatholodyResultReportSections.AuthorityToPost)
                };

                var postDocument = CreateCodableText(PatholodyResultReportSections.PostDocument);

                var entry = CreateEntryObservation(
                    CreateConceptDescriptor(CreateCodableText(PatholodyResultReportSections.AuthoriserInstruction)),
                    new List<ANY>
                    {
                        CreateConceptDescriptor(postDocument)
                    });

                // The modelling for this object looks incorrect here so not creating Custom classes for this, raising issue
                entry.observation.reference = new[]
                {
                    // Report Identifier
                    authorityToPost.ReportIdentifier != null
                        ? new POCD_MT000040Reference
                        {
                            typeCode = x_ActRelationshipExternalReference.REFR,
                            externalDocument = new POCD_MT000040ExternalDocument
                            {
                                id = CreateIdentifierArray(authorityToPost.ReportIdentifier),
                                code = CreateConceptDescriptor(reportStudy),
                            }
                        }
                        : null,

                    // Report Identifier
                    authorityToPost.ServiceRequestIdentifier != null &&
                    authorityToPost.ServiceRequestIdentifierCode != null
                        ? new POCD_MT000040Reference
                        {
                            typeCode = x_ActRelationshipExternalReference.REFR,
                            externalDocument = new POCD_MT000040ExternalDocument
                            {
                                id = CreateIdentifierArray(authorityToPost.ServiceRequestIdentifier),
                                code = CreateConceptDescriptor(authorityToPost.ServiceRequestIdentifierCode)
                            }
                        }
                        : null
                };

                // Authoriser
                if (authorityToPost.Authoriser != null)
                {
                    entry.observation.author = new[] { CreateAuthor(authorityToPost.Authoriser) };

                    // Entitlements
                    if (authorityToPost.Authoriser.Participant != null &&
                        authorityToPost.Authoriser.Participant.Entitlements != null &&
                        authorityToPost.Authoriser.Participant.Entitlements.Any())
                    {
                        authorityToPostComponent.section.coverage2 = CreateEntitlements(
                            authorityToPost.Authoriser.Participant.Entitlements,
                            authorityToPost.Authoriser.Participant.UniqueIdentifier.ToString(), RoleClass.ASSIGNED,
                            ParticipationType.HLD).ToArray();
                    }
                }

                // Authorisee
                if (authorityToPost.Authorisee != null)
                {
                    participant.Add(CreateParticipant2(authorityToPost.Authorisee));
                }

                // Repository
                if (authorityToPost.Repository != null)
                {
                    participant.Add(CreateParticipant2(authorityToPost.Repository));
                }

                // Add Entry
                authorityToPostComponent.section.entry = new[] { entry };

                if (participant.Any())
                    entry.observation.participant = participant.ToArray();

                // Authority To Post Component
                if (authorityToPostComponent.section != null)
                {
                    authorityToPostComponent.section.text =
                        authorityToPost.CustomNarrative ?? narrativeGenerator.CreateNarrative(authorityToPost);
                }

                if (authorityToPost.ExcludeNarrative.HasValue && authorityToPost.ExcludeNarrative.Value)
                {
                    authorityToPostComponent.section.title = null;
                    authorityToPostComponent.section.text = null;
                }
            }

            return authorityToPostComponent;
        }

        /// <summary>
        /// Creates an Other Test Result
        /// </summary>
        /// <param name="otherTestResult">An OtherTestResult object</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateOtherTestResult(OtherTestResult otherTestResult,
            INarrativeGenerator narrativeGenerator)
        {
            var relationshipList = new List<POCD_MT000040EntryRelationship>();
            var entryList = new List<POCD_MT000040Entry>();

            //Create the otherTestResultComponent and section
            var otherTestResultComponent = new POCD_MT000040Component5
            {
                section = CreateSectionCodeTitle("102.16029", CodingSystem.NCTIS, "Diagnostic Investigation")
            };

            if (otherTestResult.ReportStatus != null)
                relationshipList.Add(CreateRelationshipForTestResultStatus(otherTestResult.ReportStatus, null));

            if (otherTestResult.ReportContent != null && otherTestResult.ReportContent.ExternalData != null)
            {
                var imageEntryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    observationMedia = CreateObservationMedia(otherTestResult.ReportContent.ExternalData)
                };
                relationshipList.Add(imageEntryRelationship);
            }

            //Create the observation entry with all the above relationships nested inside the observation
            var entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                CreateConceptDescriptor(otherTestResult.ReportName),
                otherTestResult.ReportDate,
                relationshipList
            );

            // Report Content
            if (otherTestResult.ReportContent != null)
            {
                // Encapsulated Text
                if (!otherTestResult.ReportContent.Text.IsNullOrEmptyWhitespace())
                {
                    entry.observation.value = new ANY[] { CreateEncapsulatedData(otherTestResult.ReportContent.Text) };
                }
                // External Data
                else if (otherTestResult.ReportContent.ExternalData != null)
                {
                    entry.observation.value = new ANY[]
                        {CreateEncapsulatedData(otherTestResult.ReportContent.ExternalData)};
                }
            }

            entryList.Add(entry);

            otherTestResultComponent.section.text =
                otherTestResult.CustomNarrative ?? narrativeGenerator.CreateNarrative(otherTestResult);
            otherTestResultComponent.section.entry = entryList.ToArray();

            return otherTestResultComponent;
        }

        /// <summary>
        /// Creates an PrescriptionItem
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component5 CreateComponent(IPCEHRPrescriptionItemView item,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component5 component = null;

            if (item != null)
            {
                component = new POCD_MT000040Component5
                {
                    section = new POCD_MT000040Section
                    {
                        code = CreateCodedWithExtensionElement(CreateCodableText(EtpRecordSections.PrescriptionItem)),
                        title = CreateStructuredText(
                            EtpRecordSections.PrescriptionItem.GetAttributeValue<NameAttribute, string>(x => x.Title),
                            null)
                    }
                };

                if (item.DateTimePrescriptionWritten != null)
                {
                    component.section.author = new[]
                    {
                        CreateAuthor(item.DateTimePrescriptionWritten)
                    };
                }

                var entries = CreatePrescriptionItemEntries(item);

                entries.Add(CreateEntryLink(x_DocumentActMood.EVN,
                    CreateConceptDescriptor(
                        CreateCodableText(PrescribingAndDispensingViewRecordSections.PrescriptionRecordLink)),
                    item.PrescriptionRecordLink));

                component.section.entry = entries.ToArray();
                component.section.text = item.CustomNarrativePrescriptionItem ??
                                         narrativeGenerator.CreateNarrative(item as PCEHRPrescriptionItem, null, null);
            }

            return component;
        }

        /// <summary>
        /// Creates an Dispense Item
        /// </summary>
        /// <param name="item">IParticipationDispenser</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component5 CreateComponent(PCEHRDispenseItem item,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component5 component = null;
            var entries = new List<POCD_MT000040Entry>();

            if (item != null)
            {
                component = new POCD_MT000040Component5
                {
                    section = new POCD_MT000040Section
                    {
                        code = CreateCodedWithExtensionElement(CreateCodableText(EtpRecordSections.DispenseItem)),
                        title = CreateStructuredText(
                            EtpRecordSections.DispenseItem.GetAttributeValue<NameAttribute, string>(x => x.Title), null)
                    }
                };

                entries.Add(CreateDispenseItemEntryRelationship(item));

                // Create Dispense Record Link
                entries.Add(CreateEntryLink(x_DocumentActMood.EVN,
                    CreateConceptDescriptor(
                        CreateCodableText(PrescribingAndDispensingViewRecordSections.DispenseRecordLink)),
                    item.DispenseRecordLink));

                // Add component entry array
                component.section.entry = entries.ToArray();
                component.section.text = item.CustomNarrativeDispenseItem ??
                                         narrativeGenerator.CreateNarrative(item, null, null, null);
            }

            return component;
        }

        /// <summary>
        /// Creates an Diagnoses Interventions
        /// </summary>
        /// <param name="requestedServices">A list of requested services</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component5[] CreateComponent(List<RequestedService> requestedServices,
            INarrativeGenerator narrativeGenerator)
        {
            var components = new List<POCD_MT000040Component5>();

            if (requestedServices != null && requestedServices.Any())
            {
                foreach (var requestedService in requestedServices)
                {
                    POCD_MT000040Component5 component = null;
                    var entryList = new List<POCD_MT000040Entry>();
                    var relationshipList = new List<POCD_MT000040EntryRelationship>();

                    component = new POCD_MT000040Component5
                    {
                        section = CreateSectionCodeTitle("102.20158", CodingSystem.NCTIS, "Requested Service")
                    };

                    var documentActMood = (x_DocumentActMood)Enum.Parse(typeof(x_DocumentActMood),
                        requestedService.ServiceBookingStatus != EventTypes.Undefined
                            ? requestedService.ServiceBookingStatus.GetAttributeValue<NameAttribute, string>(
                                x => x.Code)
                            : String.Empty);

                    var entry = CreateEntryActEvent(x_ActRelationshipEntry.COMP, x_ActClassDocumentEntryAct.ACT,
                        documentActMood,
                        CreateConceptDescriptor(requestedService.RequestedServiceDescription),
                        null,
                        null);

                    // Service Provider
                    if (requestedService.ServiceProvider != null)
                    {
                        entry.act.performer = new[]
                        {
                            CreatePerformer(requestedService.ServiceProvider)
                        };

                        if (requestedService.ServiceProvider.Participant != null &&
                            requestedService.ServiceProvider.Participant.Person != null &&
                            requestedService.ServiceProvider.Participant.Person.Entitlements != null)
                        {
                            component.section.coverage2 = CreateEntitlements(
                                requestedService.ServiceProvider.Participant.Person.Entitlements,
                                requestedService.ServiceProvider.Participant.UniqueIdentifier.ToString(),
                                RoleClass.ASSIGNED,
                                ParticipationType.HLD).ToArray();
                        }
                    }

                    entryList.Add(entry);

                    if (requestedService.ServiceScheduled != null)
                    {
                        // Simulate TS for new IG - which is impossible
                        entry.act.effectiveTime = new IVL_TS
                        {
                            value = requestedService.ServiceScheduled.ToString()
                        };
                    }

                    // ServiceCommencementWindow
                    if (requestedService.ServiceCommencementWindow != null)
                    {
                        entry.act.effectiveTime = CreateIntervalTimestamp(requestedService.ServiceCommencementWindow);
                    }

                    // Subject Of Care Instruction Description
                    if (requestedService.SubjectOfCareInstructionDescription != null)
                    {
                        relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN, false,
                            CreateConceptDescriptor("103.10146",
                                CodingSystem.NCTIS,
                                "Subject of Care Instruction Description",
                                null),
                            CreateEncapsulatedData(requestedService.SubjectOfCareInstructionDescription, null), null));
                    }

                    // Subject Of Care Instruction Description
                    if (requestedService.RequestedServiceDateTime != null)
                    {
                        relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.ACT,
                            x_DocumentActMood.EVN, false,
                            CreateConceptDescriptor("103.16635", CodingSystem.NCTIS, "Requested Service DateTime",
                                null),
                            null, null,
                            requestedService.RequestedServiceDateTime));
                    }

                    entry.act.entryRelationship = relationshipList.ToArray();

                    component.section.text = requestedService.CustomNarrativeRequestedService ??
                                             narrativeGenerator.CreateNarrative(requestedService);
                    component.section.entry = entryList.ToArray();
                    components.Add(component);
                }
            }

            return components.ToArray();
        }

        #region Service Referral Section

        /// <summary>
        /// Creates a Service Referral Detail component
        /// </summary>
        /// <param name="serviceReferralDetail">service referral detail</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(ServiceReferralDetail serviceReferralDetail,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var entryList = new List<POCD_MT000040Entry>();

            if (serviceReferralDetail != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(ServiceReferralSections.ServiceReferralDetail)
                };

                // Requested Service
                if (serviceReferralDetail.RequestedService != null && serviceReferralDetail.RequestedService.Any())
                {
                    entryList.AddRange(CreateRequestedServices(serviceReferralDetail.RequestedService));
                }

                // Other Alerts
                if (serviceReferralDetail.OtherAlerts != null)
                {
                    entryList.AddRange(CreateOtherAlerts(serviceReferralDetail.OtherAlerts));
                }

                // Related Document
                if (serviceReferralDetail.RelatedDocument != null && serviceReferralDetail.RelatedDocument.Any())
                {
                    entryList.AddRange(CreateRelatedDocument(serviceReferralDetail.RelatedDocument));
                }

                component.section.text = serviceReferralDetail.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(serviceReferralDetail);

                component.section.entry = entryList.Any() ? entryList.ToArray() : null;
            }

            return component;
        }

        /// <summary>
        /// Creates an Interpreter Required Alert
        /// </summary>
        /// <param name="interpreterRequiredAlert">The Interpreter Required Alert</param>
        /// <returns>Returns POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateInterpreterRequiredAlert(
            InterpreterRequiredAlert interpreterRequiredAlert)
        {
            var entry = new POCD_MT000040Entry();

            if (interpreterRequiredAlert?.PreferredLanguage != null && interpreterRequiredAlert.PreferredLanguage.Any())
            {
                var anyList = new List<ANY>();

                foreach (var preferredLanguage in interpreterRequiredAlert.PreferredLanguage)
                {
                    anyList.Add(CreateCodeSystem(preferredLanguage));
                }

                // Needs to be DRIV (31/5/17)
                //entry = CreateEntryObservation(CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.InterpreterRequired)), anyList);
                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.InterpreterRequired)), null,
                    anyList, null, null);

            }

            return entry;
        }

        /// <summary>
        /// Creates Other Alerts
        /// </summary>
        /// <param name="alerts">Other Alerts</param>
        /// <returns>Returns POCD_MT000040Entry</returns>
        internal static List<POCD_MT000040Entry> CreateOtherAlerts(IList<Alert> alerts)
        {
            var entries = new List<POCD_MT000040Entry>();

            // alert
            foreach (var alert in alerts)
            {
                var entry = CreateEntryObservation
                (
                    x_ActRelationshipEntry.COMP,
                    CreateConceptDescriptor
                    (
                        alert.AlertType
                    ),
                    null,
                    CreateConceptDescriptorsForCoadableText(alert.AlertDescription),
                    null,
                    null
                );

                entry.observation.classCode = ActClassObservation.ALRT;

                entries.Add(entry);
            }

            return entries;
        }

        /// <summary>
        /// The Create Request Service Entry
        /// </summary>
        /// <param name="requestedService">The Service</param>
        /// <param name="code">The Code</param>
        /// <param name="subjectOfCareIdentifier">Subject Of Care Identifier</param>
        /// <returns></returns>
        internal static POCD_MT000040Entry CreateRequestServiceEntry(Service requestedService, ICodableText code = null,
            string subjectOfCareIdentifier = null)
        {
            var relationshipList = new List<POCD_MT000040EntryRelationship>();

            // Service Booking Status
            var documentActMood = (x_DocumentActMood)Enum.Parse(typeof(x_DocumentActMood),
                requestedService.ServiceBookingStatus != EventTypes.Undefined
                    ? requestedService.ServiceBookingStatus.GetAttributeValue<NameAttribute, string>(x => x.Code)
                    : String.Empty);

            // Entry
            POCD_MT000040Entry entry = new POCD_MT000040Entry
            {
                typeCode = x_ActRelationshipEntry.COMP,
                act = CreateActEvent(x_ActClassDocumentEntryAct.ACT, documentActMood, null)
            };

            // Service Category
            if (requestedService.ServiceCategory != null)
            {
                entry.act.code = CreateConceptDescriptor(requestedService.ServiceCategory);
            }

            // Code
            if (code != null)
            {
                entry.act.code = CreateConceptDescriptor(code);
            }

            // Reason For Service Description or Reason For Service
            if (requestedService.ReasonForService != null ||
                !string.IsNullOrWhiteSpace(requestedService.ReasonForServiceDescription))
            {
                relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                    x_ActClassDocumentEntryAct.ACT,
                    x_DocumentActMood.EVN,
                    false,
                    requestedService.ReasonForService != null
                        ? CreateConceptDescriptor(requestedService.ReasonForService)
                        : null,
                    !string.IsNullOrWhiteSpace(requestedService.ReasonForServiceDescription)
                        ? CreateStructuredText(requestedService.ReasonForServiceDescription)
                        : null,
                    null
                ));
            }

            // Service Description
            if (requestedService.ServiceDescription != null)
            {
                relationshipList.Add(CreateEntryRelationshipObservation(
                    x_ActRelationshipEntryRelationship.COMP,
                    CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.ServiceDescription)),
                    CreateConceptDescriptor(requestedService.ServiceDescription)
                ));
            }

            // Request Urgency
            if (requestedService.RequestUrgency.HasValue && requestedService.RequestUrgency == true)
            {
                entry.act.priorityCode =
                    CreateCodedWithExtensionElement(CreateCodableText(ServiceReferralSections.RequestUrgency));
            }

            // Request Urgency Notes
            if (requestedService.RequestUrgencyNotes != null)
            {
                relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                    x_ActClassDocumentEntryAct.INFRM,
                    x_DocumentActMood.EVN,
                    false,
                    CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.RequestUrgencyNotes)),
                    CreateStructuredText(requestedService.RequestUrgencyNotes),
                    null
                ));
            }

            // Subject of Care Instruction Description
            if (requestedService.SubjectOfCareInstructionDescription != null)
            {
                foreach (var subjectOfCareInstructionDescription in requestedService.SubjectOfCareInstructionDescription
                )
                {
                    relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor(
                            CreateCodableText(ServiceReferralSections.SubjectOfCareInstructionDescription)),
                        CreateStructuredText(subjectOfCareInstructionDescription),
                        null
                    ));
                }
            }

            // DateTime Service Scheduled
            if (requestedService.DateTimeServiceScheduled != null)
            {
                entry.act.effectiveTime = new IVL_TS
                {
                    value = CreateTimeStampElementIso(requestedService.DateTimeServiceScheduled).value
                };
            }

            // Service Commencement Window
            if (requestedService.ServiceCommencementWindow != null)
            {
                entry.act.effectiveTime = CreateIntervalTimestamp(requestedService.ServiceCommencementWindow);
            }

            // Request Validity Period
            if (requestedService.RequestValidityPeriod != null)
            {
                relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                    x_ActClassDocumentEntryAct.INFRM,
                    x_DocumentActMood.EVN,
                    false,
                    CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.RequestValidityPeriod)),
                    null,
                    null,
                    CreateIntervalTimestamp(requestedService.RequestValidityPeriod)
                ));
            }

            // Requested Service DateTime
            if (requestedService.RequestedServiceDateTime != null)
            {
                relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                    x_ActClassDocumentEntryAct.ACT,
                    x_DocumentActMood.EVN,
                    false,
                    CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.RequestedServiceDatetime)),
                    null,
                    null,
                    requestedService.RequestedServiceDateTime
                ));
            }

            // Service Provider
            if (requestedService.ServiceProvider != null)
            {
                entry.act.performer = new[]
                {
                    CreatePerformer(requestedService.ServiceProvider, subjectOfCareIdentifier)
                };
            }

            // Service Comment
            if (requestedService.ServiceComment != null)
            {
                relationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                    x_ActClassDocumentEntryAct.INFRM,
                    x_DocumentActMood.EVN,
                    false,
                    CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.ServiceComment)),
                    CreateStructuredText(requestedService.ServiceComment),
                    null
                ));
            }

            entry.act.entryRelationship = relationshipList.ToArray();

            return entry;
        }

        /// <summary>
        /// Creates an Requested Service
        /// </summary>
        /// <param name="requestedServices">A list of Requested Service</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static List<POCD_MT000040Entry> CreateRequestedServices(IList<IRequestedService> requestedServices)
        {
            var entryList = new List<POCD_MT000040Entry>();

            if (requestedServices != null)
            {
                entryList.AddRange(from Service requestedService in requestedServices
                                   select CreateRequestServiceEntry(requestedService));
            }

            return entryList;
        }

        /// <summary>
        /// The Related Documents
        /// </summary>
        /// <param name="relatedDocuments">The Related Documents</param>
        /// <returns>List(POCD_MT000040Entry) A list of entries</returns>
        internal static List<POCD_MT000040Entry> CreateRelatedDocument(IList<RelatedDocumentV1> relatedDocuments)
        {

            var entryList = new List<POCD_MT000040Entry>();

            if (relatedDocuments != null && relatedDocuments.Any())
            {
                foreach (var relatedDocument in relatedDocuments)
                {
                    var relationshipList = new List<POCD_MT000040EntryRelationship>();

                    // Entry
                    POCD_MT000040Entry entry = new POCD_MT000040Entry
                    {
                        typeCode = x_ActRelationshipEntry.COMP,
                        act = CreateActEvent(x_ActClassDocumentEntryAct.ACT, x_DocumentActMood.EVN,
                            CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.RelatedDocument)))
                    };

                    // Reference
                    entry.act.reference = new[]
                    {
                        new POCD_MT000040Reference
                        {
                            seperatableInd = CreateBoolean(true, true),
                            typeCode = x_ActRelationshipExternalReference.REFR,
                            externalDocument = new POCD_MT000040ExternalDocument
                            {
                                classCode = ActClassDocument.DOC,
                                moodCode = ActMood.EVN,
                                id = relatedDocument.DocumentTarget?.ID != null
                                    ? CreateIdentifierArray(relatedDocument.DocumentTarget.ID)
                                    : null,
                                code = relatedDocument.DocumentDetails?.DocumentType != null
                                    ? CreateConceptDescriptor(relatedDocument.DocumentDetails.DocumentType)
                                    : null,
                                text = relatedDocument.DocumentTarget != null
                                    ? CreateEncapsulatedData(relatedDocument.DocumentTarget)
                                    : null
                            }
                        }
                    };

                    // Requested Service DateTime
                    if (relatedDocument.DocumentDetails?.DocumentTitle != null)
                    {
                        relationshipList.Add(
                            CreateEntryRelationshipACT(
                                x_ActRelationshipEntryRelationship.COMP,
                                x_ActClassDocumentEntryAct.ACT,
                                x_DocumentActMood.EVN,
                                false,
                                CreateConceptDescriptor(CreateCodableText(ServiceReferralSections.DocumentTitle)),
                                CreateStructuredText(relatedDocument.DocumentDetails.DocumentTitle),
                                null
                            ));
                    }

                    entry.act.entryRelationship = relationshipList.ToArray();

                    entryList.Add(entry);
                }
            }

            return entryList;
        }

        /// <summary>
        /// Creates an Current Service
        /// </summary>
        /// <param name="currentServices">A list of Requested Service</param>
        /// <param name="customNarrativeCurrentService">The StrucDocText</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(IList<ICurrentService> currentServices,
            StrucDocText customNarrativeCurrentService, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var entryList = new List<POCD_MT000040Entry>();

            if (currentServices != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(ServiceReferralSections.CurrentServices)
                };

                entryList.AddRange(from Service service in currentServices select CreateRequestServiceEntry(service));

                component.section.text =
                    customNarrativeCurrentService ?? narrativeGenerator.CreateNarrative(currentServices);

                component.section.entry = entryList.ToArray();
            }

            return component;
        }

        /// <summary>
        /// Creates an Pending Diagnostic Investigation
        /// </summary>
        /// <param name="pendingDiagnosticInvestigation">A list of Requested Service</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry[] CreateEntry(
            List<IPendingDiagnosticInvestigation> pendingDiagnosticInvestigation,
            INarrativeGenerator narrativeGenerator)
        {
            List<POCD_MT000040Entry> entryList = null;

            if (pendingDiagnosticInvestigation != null)
            {
                entryList = new List<POCD_MT000040Entry>();

                foreach (var currentService in pendingDiagnosticInvestigation)
                {
                    if (currentService != null)
                    {
                        entryList.AddRange(from Service service in pendingDiagnosticInvestigation
                                           select CreateRequestServiceEntry(service,
                                               CreateCodableText(ServiceReferralSections.RequestedService)));
                    }
                }
            }

            return entryList != null && entryList.Any() ? entryList.ToArray() : null;
        }

        #endregion

        #endregion

        #endregion

        #region internal Methods - Participants

        #region internal Methods - Context Participants (Custodian, LegalAuthenticator, InformationRecipient)

        /// <summary>
        /// Creates a CDA custodian entry from the Custodian model
        /// </summary>
        /// <param name="custodian">Custodian</param>
        /// <returns>POCD_MT000040Custodian</returns>
        internal static POCD_MT000040Custodian CreateCustodian(IParticipationCustodian custodian)
        {
            if (custodian == null) return null;

            var cdaCustodian = new POCD_MT000040Custodian
            {
                templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100002", null),
                assignedCustodian =
                    new POCD_MT000040AssignedCustodian
                    {
                        representedCustodianOrganization =
                            new POCD_MT000040CustodianOrganization
                            {
                                id = custodian.Participant != null
                                    ? CreateIdentifierArray(custodian.Participant.UniqueIdentifier.ToString(), null,
                                        null)
                                    : null,
                                name = custodian.Participant == null || custodian.Participant.Organisation == null
                                    ? null
                                    : CreateOrganisationName(custodian.Participant.Organisation.Name),
                                asEntityIdentifier =
                                    custodian.Participant == null ||
                                    custodian.Participant.Organisation == null ||
                                    custodian.Participant.Organisation.Identifiers == null
                                        ? null
                                        : CreateEntityIdentifier(custodian.Participant.Organisation.Identifiers)
                                            .ToArray(),
                                addr = custodian.Participant == null
                                    ? null
                                    : CreateAddress(custodian.Participant.Address),
                                telecom = custodian.Participant == null
                                    ? null
                                    : CreateTelecomunication(custodian.Participant.ElectronicCommunicationDetail)
                            }
                    }
            };

            return cdaCustodian;
        }

        /// <summary>Legal Authenticator
        /// Creates a legal authenticator from the Authenticator model
        /// </summary>
        /// <param name="authenticator">Authenticator</param>
        /// <returns>POCD_MT000040LegalAuthenticator</returns>
        internal static POCD_MT000040LegalAuthenticator CreateLegalAuthenticator(
            IParticipationLegalAuthenticator authenticator)
        {
            if (authenticator == null) return null;

            var cdaAuthenticator = new POCD_MT000040LegalAuthenticator
            {
                templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100012", null),
                time = authenticator.Participant == null || authenticator.Participant.DateTimeAuthenticated == null
                    ? null
                    : CreateTimeStampElementIso(authenticator.Participant.DateTimeAuthenticated),
                signatureCode = CreateCodeSystem("S", null, null, null, null, null),
                assignedEntity =
                    new POCD_MT000040AssignedEntity
                    {
                        id = authenticator.Participant != null
                            ? CreateIdentifierArray(authenticator.Participant.UniqueIdentifier.ToString(), null, null)
                            : null,
                        code = CreateCodedWithExtensionElement(authenticator.Role),
                        addr = authenticator.Participant == null
                            ? null
                            : CreateAddressArray(authenticator.Participant.Addresses),
                        telecom = authenticator.Participant == null
                            ? null
                            : CreateTelecomunicationArray(authenticator.Participant.ElectronicCommunicationDetails),
                        assignedPerson =
                            authenticator.Participant != null && authenticator.Participant.Person != null &&
                            (authenticator.Participant.Person.Identifiers != null ||
                             authenticator.Participant.Person.PersonNames != null)
                                ? new POCD_MT000040Person
                                {
                                    // PersonName
                                    name = authenticator.Participant != null &&
                                           authenticator.Participant.Person != null &&
                                           authenticator.Participant.Person.PersonNames != null
                                        ? CreatePersonNameArray(authenticator.Participant.Person.PersonNames)
                                        : null,
                                    // EntityIdentifier
                                    asEntityIdentifier =
                                        authenticator.Participant != null && authenticator.Participant.Person != null &&
                                        authenticator.Participant.Person.Identifiers != null
                                            ? CreateEntityIdentifier(authenticator.Participant.Person.Identifiers)
                                                .ToArray()
                                            : null
                                }
                                : null,
                    }
            };

            if (authenticator.Participant != null && authenticator.Participant.Organisation != null &&
                (authenticator.Participant.Organisation.Identifiers != null ||
                 !String.IsNullOrEmpty(authenticator.Participant.Organisation.Name)))
            {
                cdaAuthenticator.assignedEntity.representedOrganization =
                    CreateOrganisation(authenticator.Participant.Organisation);
            }

            return cdaAuthenticator;
        }

        /// <summary>
        /// Creates an information recipient from the Recipient model
        /// </summary>
        /// <param name="participationRecipient">Recipient</param>
        /// <returns>POCD_MT000040InformationRecipient</returns>
        internal static POCD_MT000040InformationRecipient CreateInformationRecipient(
            IParticipationInformationRecipient participationRecipient)
        {
            POCD_MT000040InformationRecipient cdaRecipient = null;
            var recipient = participationRecipient.Participant;

            if (recipient != null)
            {

                cdaRecipient = new POCD_MT000040InformationRecipient
                {
                    templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100022", null),
                    typeCode =
                        recipient.RecipientType == RecipientType.Primary
                            ? x_InformationRecipient.PRCP
                            : x_InformationRecipient.TRC,
                    intendedRecipient =
                        new POCD_MT000040IntendedRecipient
                        {
                            classCode = x_InformationRecipientRole.ASSIGNED,
                            id = participationRecipient.Participant != null
                                ? CreateIdentifierArray(participationRecipient.Participant.UniqueIdentifier.ToString(),
                                    null)
                                : null,
                            addr = CreateAddressArray(recipient.Addresses),
                        }
                };

                if (recipient.ElectronicCommunicationDetails != null &&
                    recipient.ElectronicCommunicationDetails.Any())
                    cdaRecipient.intendedRecipient.telecom =
                        CreateTelecomunicationArray(recipient.ElectronicCommunicationDetails);

                if (recipient.Person != null && recipient.Person.PersonNames != null)
                {
                    if (cdaRecipient.intendedRecipient.informationRecipient == null)
                        cdaRecipient.intendedRecipient.informationRecipient = new POCD_MT000040Person();

                    cdaRecipient.intendedRecipient.informationRecipient.name =
                        CreatePersonNameArray(recipient.Person.PersonNames);
                }

                if (recipient.Person != null && recipient.Person.Identifiers != null &&
                    recipient.Person.Identifiers.Any())
                {
                    if (cdaRecipient.intendedRecipient.informationRecipient == null)
                        cdaRecipient.intendedRecipient.informationRecipient = new POCD_MT000040Person();

                    cdaRecipient.intendedRecipient.informationRecipient.asEntityIdentifier =
                        CreateEntityIdentifier(recipient.Person.Identifiers).ToArray();
                }

                if (recipient.Organisation != null &&
                    (recipient.Organisation.Identifiers != null || !String.IsNullOrEmpty(recipient.Organisation.Name)))
                {
                    cdaRecipient.intendedRecipient.receivedOrganization = CreateOrganisation(recipient.Organisation);
                }
            }

            return cdaRecipient;
        }

        /// <summary>
        /// Creates an information recipient from the Recipient model
        /// </summary>
        /// <param name="provider">Recipient</param>
        /// <param name="participationType">The Participation Type</param>
        /// <param name="subjectOfCareIdentifier">Subject Of Care Identifier</param>
        /// <returns>POCD_MT000040InformationRecipient</returns>
        internal static POCD_MT000040InformationRecipient CreateInformationRecipient(
            IParticipationPersonOrOrganisation provider, x_InformationRecipient participationType,
            string subjectOfCareIdentifier)
        {
            POCD_MT000040InformationRecipient cdaRecipient = null;
            var recipient = provider.Participant;

            if (recipient != null)
            {
                cdaRecipient = new POCD_MT000040InformationRecipient
                {
                    typeCode = participationType,
                    intendedRecipient =
                        new POCD_MT000040IntendedRecipient
                        {
                            classCode = x_InformationRecipientRole.ASSIGNED,
                            id = provider.Participant != null
                                ? CreateIdentifierArray(provider.Participant.UniqueIdentifier.ToString(), null)
                                : null,
                            addr = CreateAddressArray(recipient.Addresses),
                            code = CreateCodedWithExtensionElement(provider.Role)
                        }
                };

                if (recipient.ElectronicCommunicationDetails != null && recipient.ElectronicCommunicationDetails.Any())
                {
                    cdaRecipient.intendedRecipient.telecom =
                        CreateTelecomunicationArray(recipient.ElectronicCommunicationDetails);
                }

                if (recipient.Person?.PersonNames != null)
                {
                    if (cdaRecipient.intendedRecipient.informationRecipient == null)
                    {
                        cdaRecipient.intendedRecipient.informationRecipient = new POCD_MT000040Person();
                    }

                    cdaRecipient.intendedRecipient.informationRecipient.name =
                        CreatePersonNameArray(recipient.Person.PersonNames);
                }

                if (recipient.Person?.Identifiers != null && recipient.Person.Identifiers.Any())
                {
                    if (cdaRecipient.intendedRecipient.informationRecipient == null)
                    {
                        cdaRecipient.intendedRecipient.informationRecipient = new POCD_MT000040Person();
                    }

                    cdaRecipient.intendedRecipient.informationRecipient.asEntityIdentifier =
                        CreateEntityIdentifier(recipient.Person.Identifiers).ToArray();
                }

                if (recipient.Organisation != null && (recipient.Organisation.Identifiers != null ||
                                                       !string.IsNullOrEmpty(recipient.Organisation.Name)))
                {
                    cdaRecipient.intendedRecipient.receivedOrganization = CreateOrganisation(recipient.Organisation);
                }

                if (recipient.Person?.Organisation != null)
                {
                    cdaRecipient.intendedRecipient.informationRecipient.asEmployment =
                        CreateEmployment(recipient.Person.Organisation);
                }

                if (recipient.RelationshipToSubjectOfCare != null && !subjectOfCareIdentifier.IsNullOrEmptyWhitespace())
                {
                    cdaRecipient.intendedRecipient.informationRecipient.personalRelationship =
                        CreatePersonalRelationship(recipient.RelationshipToSubjectOfCare, subjectOfCareIdentifier);
                }

                if (recipient.Qualifications != null)
                {
                    cdaRecipient.intendedRecipient.informationRecipient.asQualifications = new Qualifications
                    {
                        classCode = EntityClass.QUAL,
                        code = CreateCodedWithExtensionElement(new CodableText
                        {
                            OriginalText = recipient.Qualifications
                        })
                    };
                }
            }

            return cdaRecipient;
        }

        #endregion

        #region internal Methods - POCD_MT000040Author

        /// <summary>
        /// Creates an author 
        /// </summary>
        /// <param name="prescriber">Prescriber</param>
        /// <param name="prescriberOrganisation">IParticipationPrescriberOrganisation</param> 
        /// <returns>POCD_MT00040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationPrescriber prescriber,
            IParticipationPrescriberOrganisation prescriberOrganisation)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (prescriber != null && prescriber.Participant != null)
            {
                var author = prescriber.Participant;

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(prescriber.Participant.UniqueIdentifier.ToString(), null) };

                returnAuthor.time = prescriber.Time != null
                    ? returnAuthor.time = CreateTimeStampElementIso(prescriber.Time)
                    : returnAuthor.time = CreateTimeStampElementIso(NullFlavor.NA);

                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;

                if (prescriber.Role != null)
                {
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(prescriber.Role);
                }

                returnAuthor.assignedAuthor.addr = CreateAddressArray(author.Addresses);

                returnAuthor.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                returnAuthor.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null,
                    asEmployment = author.Person != null && author.Person.Occupation != null
                        ? new Employment
                        {
                            jobCode =
                                CreateCodedWithExtensionElement(
                                    author.Person.Occupation.GetAttributeValue<NameAttribute, string>(x => x.Code),
                                    CodingSystem.ANZSCO,
                                    author.Person.Occupation.GetAttributeValue<NameAttribute, string>(x => x.Name),
                                    null, null, null)
                        }
                        : null,
                    asQualifications = author.Person != null && author.Person.Qualifications != null
                        ? CreateQualifications(author.Person.Qualifications)
                        : null
                };

                if (author.Person != null && author.Person.Identifiers != null && author.Person.Identifiers.Count > 0)
                {
                    returnAuthor.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();
                }
            }

            if (prescriberOrganisation != null && prescriberOrganisation.Participant != null)
            {
                returnAuthor.assignedAuthor.representedOrganization = new POCD_MT000040Organization
                {
                    id = CreateIdentifierArray(prescriberOrganisation.Participant.UniqueIdentifier.ToString(), null),
                    // Department Name
                    name = prescriberOrganisation.Participant.Organisation == null
                        ? null
                        : new[]
                        {
                            CreateOrganisationName
                            (
                                prescriberOrganisation.Participant.Organisation.Department,
                                String.Empty
                            )
                        },
                    // Role
                    standardIndustryClassCode = prescriberOrganisation.Role != null
                        ? CreateCodedWithExtensionElement(prescriberOrganisation.Role)
                        : null,
                    asOrganizationPartOf = new POCD_MT000040OrganizationPartOf
                    {
                        wholeOrganization = new POCD_MT000040Organization
                        {
                            // Organisation Name
                            name = prescriberOrganisation.Participant.Organisation == null
                                ? null
                                : CreateOrganisationNameArray
                                (
                                    prescriberOrganisation.Participant.Organisation.Name,
                                    prescriberOrganisation.Participant.Organisation.NameUsage
                                ),
                            // Identifiers
                            asEntityIdentifier =
                                prescriberOrganisation.Participant.Organisation != null &&
                                prescriberOrganisation.Participant.Organisation.Identifiers != null
                                    ? CreateEntityIdentifier(
                                        prescriberOrganisation.Participant.Organisation.Identifiers).ToArray()
                                    : null,
                            // Address
                            addr = CreateAddressArray(prescriberOrganisation.Participant.Addresses),
                            // ElectronicCommunicationDetails
                            telecom = CreateTelecomunicationArray(prescriberOrganisation.Participant
                                .ElectronicCommunicationDetails)
                        }
                    }
                };
            }

            return returnAuthor;
        }

        /// <summary>
        /// Creates an author from a participation
        /// </summary>
        /// <param name="participationAuthor">IParticipationConsumerAuthor</param>
        /// <param name="subjectOfCareIdentifier">A guid for the subjectOfCareIdentifier </param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationConsumerAuthor participationAuthor,
            Guid? subjectOfCareIdentifier)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (participationAuthor != null && participationAuthor.Participant != null)
            {
                var author = participationAuthor.Participant;

                if (participationAuthor.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    returnAuthor.time =
                        CreateTimeStampElementIso(participationAuthor.AuthorParticipationPeriodOrDateTimeAuthored);
                }
                else
                {
                    returnAuthor.time = CreateTimeStampElementIso(NullFlavor.NA);
                }

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                {
                    id = subjectOfCareIdentifier.HasValue
                        ? CreateIdentifierArray(subjectOfCareIdentifier.ToString(), null)
                        : CreateIdentifierArray(CreateGuid(), null)
                };

                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;

                if (participationAuthor.Role != null)
                {
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(participationAuthor.Role);
                }

                returnAuthor.assignedAuthor.addr = CreateAddressArray(author.Addresses);

                returnAuthor.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                returnAuthor.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null
                };

                if (author.Person != null && author.Person.Identifiers != null)
                {
                    returnAuthor.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();
                }

                // NOTE : RelationshipToSubjectOfCare overrights informationProvider.Role because of a bug in the SPEC
                if (author.Person != null && author.RelationshipToSubjectOfCare != null)
                {
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement
                    (
                        author.RelationshipToSubjectOfCare
                    );
                }
            }

            return returnAuthor;
        }

        /// <summary>
        /// Creates an author from a participation - author and a role
        /// </summary>
        /// <param name="authorAuthoringDevice">AuthorAuthoringDevice</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(AuthorAuthoringDevice authorAuthoringDevice)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (authorAuthoringDevice != null)
            {
                returnAuthor.time = authorAuthoringDevice.DateTimeAuthored != null
                    ? CreateTimeStampElementIso(authorAuthoringDevice.DateTimeAuthored)
                    : CreateTimeStampElementIso(NullFlavor.NA);

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(CreateGuid()) };

                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;

                if (authorAuthoringDevice.Role != null)
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(authorAuthoringDevice.Role);

                returnAuthor.assignedAuthor.assignedAuthoringDevice = new POCD_MT000040AuthoringDevice
                {
                    asEntityIdentifier =
                        (authorAuthoringDevice.Identifiers != null && authorAuthoringDevice.Identifiers.Any())
                            ? CreateEntityIdentifier(authorAuthoringDevice.Identifiers).ToArray()
                            : null,
                    softwareName = !authorAuthoringDevice.SoftwareName.IsNullOrEmptyWhitespace()
                        ? new SC { Text = new[] { authorAuthoringDevice.SoftwareName } }
                        : null
                };
            }

            return returnAuthor;
        }

        /// <summary>
        /// Creates an author from a participation - author and a role
        /// </summary>
        /// <param name="device">AuthorAuthoringDevice</param>
        /// <returns>POCD_MT000040Participant2</returns>
        internal static POCD_MT000040Participant2 CreateDeviceParticipant(Device device)
        {
            var participant = new POCD_MT000040Participant2();

            if (device != null)
            {
                participant.participantRole = new POCD_MT000040ParticipantRole
                { id = CreateIdentifierArray(CreateGuid()) };

                participant.typeCode = ParticipationType.DEV;

                if (device.Role != null)
                    participant.participantRole.code = CreateCodedWithExtensionElement(device.Role);

                participant.participantRole.playingDevice = new POCD_MT000040Device
                {
                    softwareName = !device.SoftwareName.IsNullOrEmptyWhitespace()
                        ? new SC { Text = new[] { device.SoftwareName } }
                        : null
                };
            }

            return participant;
        }

        /// <summary>
        /// Creates an author from a participation - author and a role
        /// </summary>
        /// <param name="device">AuthorAuthoringDevice</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateDevice(InformationProviderDevice device)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (device != null)
            {
                returnAuthor.time = device.ParticipationPeriod != null
                    ? CreateIntervalTimestamp(null, null, null, null, device.ParticipationPeriod.ToString(), null)
                    : CreateTimeStampElementIso(NullFlavor.NA);

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(CreateGuid()) };

                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;

                if (device.Role != null)
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(device.Role);

                returnAuthor.assignedAuthor.assignedAuthoringDevice = new POCD_MT000040AuthoringDevice
                {
                    softwareName = !device.SoftwareName.IsNullOrEmptyWhitespace()
                        ? new SC { Text = new[] { device.SoftwareName } }
                        : null
                };
            }

            return returnAuthor;
        }

        /// <summary>
        /// Creates an author  
        /// </summary>
        /// <param name="author">Author</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IAuthorCollection author)
        {
            var participationAuthor = new POCD_MT000040Author();

            if (author != null)
            {
                if (author is AuthorAuthoringDevice)
                {
                    var device = author as AuthorAuthoringDevice;
                    participationAuthor = CreateAuthor(device);
                }

                // Both types are of type Participation so use the Participant to determine the type 
                if (author is Participation)
                {
                    var authorNonHealthcareProvider = author as IParticipationAuthorPerson;

                    if (authorNonHealthcareProvider.Participant != null)
                    {
                        participationAuthor = CreateAuthor(authorNonHealthcareProvider);
                    }

                    var authorHealthcareProvider = author as IParticipationAuthorHealthcareProvider;

                    if (authorHealthcareProvider.Participant != null)
                    {
                        participationAuthor = CreateAuthor(authorHealthcareProvider);
                    }
                }
            }

            // Add TemplateId
            participationAuthor.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100006", null);

            return participationAuthor;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationAuthorNonHealthcareProvider
        /// </summary>
        /// <param name="participation">IParticipationAuthorNonHealthcareProvider</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationAuthorPerson participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    castedParticipation.AuthorParticipationPeriodOrDateTimeAuthored =
                        participation.AuthorParticipationPeriodOrDateTimeAuthored.Value;
                    castedParticipation.ParticipationPeriod =
                        participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval;
                }

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)

                        castedParticipation.Participant.Person = new Person();
                    castedParticipation.Participant.Person.Identifiers = participation.Participant.Person.Identifiers;
                    castedParticipation.Participant.Person.PersonNames = participation.Participant.Person.PersonNames;
                }

                if (participation.Participant.Addresses != null)
                    castedParticipation.Participant.Addresses = participation.Participant.Addresses;

                returnParticipant = CreateAuthor(castedParticipation, null);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationAuthorHealthcareProvider
        /// </summary>
        /// <param name="participation">IParticipationAuthorHealthcareProvider</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationAuthorHealthcareProvider participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null &&
                    participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval != null)
                    castedParticipation.ParticipationPeriod =
                        participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval;

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null &&
                    participation.AuthorParticipationPeriodOrDateTimeAuthored.Value != null)
                    castedParticipation.AuthorParticipationPeriodOrDateTimeAuthored =
                        participation.AuthorParticipationPeriodOrDateTimeAuthored.Value;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                if (participation.Participant.Person != null)
                    returnParticipant =
                        CreateAuthor(castedParticipation, participation.Participant.Person.Qualifications);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationInformationProviderHealthcareProvider
        /// </summary>
        /// <param name="participation">IParticipationInformationProviderHealthcareProvider</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Author CreateAuthor(
            IParticipationInformationProviderHealthcareProvider participation)
        {

            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateAuthor(castedParticipation, null);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationInformationProviderNonHealthcareProvider
        /// </summary>
        /// <param name="participation">IParticipationInformationProviderNonHealthcareProvider</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Author CreateAuthor(
            IParticipationInformationProviderNonHealthcareProvider participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = new Person();
                        castedParticipation.Participant.Person.Identifiers =
                            participation.Participant.Person.Identifiers;
                        castedParticipation.Participant.Person.PersonNames =
                            participation.Participant.Person.PersonNames;
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateAuthor(castedParticipation, null);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationReportingPathologist
        /// </summary>
        /// <param name="participation">IParticipationReportingPathologist</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationReportingPathologist participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = new Person
                        {
                            Identifiers = participation.Participant.Person.Identifiers,
                            PersonNames = participation.Participant.Person.PersonNames
                        };

                        if (participation.Participant.Person.Organisation != null)
                            castedParticipation.Participant.Person.Organisation =
                                participation.Participant.Person.Organisation;
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateAuthor(castedParticipation, participation.Participant.Qualifications);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationReportingRadiologist
        /// </summary>
        /// <param name="participation">IParticipationReportingRadiologist</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationReportingRadiologist participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = new Person
                        {
                            Identifiers = participation.Participant.Person.Identifiers,
                            PersonNames = participation.Participant.Person.PersonNames
                        };

                        if (participation.Participant.Person.Organisation != null)
                            castedParticipation.Participant.Person.Organisation =
                                participation.Participant.Person.Organisation;
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateAuthor(castedParticipation, participation.Participant.Qualifications);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationAuthoriser
        /// </summary>
        /// <param name="participation">IParticipationAuthoriser</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationAuthoriser participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = new Person
                        {
                            Identifiers = participation.Participant.Person.Identifiers,
                            PersonNames = participation.Participant.Person.PersonNames
                        };

                        if (participation.Participant.Person.Organisation != null)
                            castedParticipation.Participant.Person.Organisation =
                                participation.Participant.Person.Organisation;
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateAuthor(castedParticipation, participation.Participant.Qualifications);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - IParticipationPathologyServiceRequester
        /// </summary>
        /// <param name="participation">IParticipationPathologyServiceRequester</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationPathologyServiceRequester participation)
        {
            POCD_MT000040Author returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = new Person
                        {
                            Identifiers = participation.Participant.Person.Identifiers,
                            PersonNames = participation.Participant.Person.PersonNames
                        };
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateAuthor(castedParticipation, participation.Participant.Qualifications);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates an author for a IParticipation 
        /// </summary>
        /// <param name="participation">IParticipation</param>
        /// <param name="qualifications">Qualifications </param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipation participation, string qualifications)
        {
            var returnInformationProvider = new POCD_MT000040Author();

            if (participation != null && participation.Participant != null)
            {
                // Set up the ParticipationPeriod
                var participationPeriod = CreateTimeStampElementIso(NullFlavor.NA);

                if (participation.ParticipationPeriod != null)
                {
                    participationPeriod = CreateComplexTime(participation.ParticipationPeriod);
                }

                if (participation.ParticipationEndTime != null)
                {
                    participationPeriod = CreateIntervalTimestamp(participation.ParticipationEndTime, null);
                }

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    participationPeriod =
                        CreateIntervalTimestamp(participation.AuthorParticipationPeriodOrDateTimeAuthored, null);
                }

                var author = participation.Participant;

                returnInformationProvider.time = participationPeriod; // Set ParticipationPeriod

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    returnInformationProvider.time = participation.AuthorParticipationPeriodOrDateTimeAuthored != null
                        ? CreateTimeStampElementIso(participation.AuthorParticipationPeriodOrDateTimeAuthored)
                        : CreateTimeStampElementIso(NullFlavor.NA);
                }

                returnInformationProvider.assignedAuthor = new POCD_MT000040AssignedAuthor
                {
                    id = CreateIdentifierArray(participation.Participant.UniqueIdentifier.ToString(), null)
                };

                returnInformationProvider.typeCode = ParticipationType.AUT;
                returnInformationProvider.typeCodeSpecified = true;

                if (participation.Role != null)
                {
                    returnInformationProvider.assignedAuthor.code = CreateCodedWithExtensionElement(participation.Role);
                }

                returnInformationProvider.assignedAuthor.addr = CreateAddressArray(author.Addresses);

                returnInformationProvider.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                returnInformationProvider.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100040", null),
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null
                };

                if (author.Person != null && author.Person.Identifiers != null)
                {
                    returnInformationProvider.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();
                }

                if (participation.Participant.Person != null && !qualifications.IsNullOrEmptyWhitespace())
                {
                    returnInformationProvider.assignedAuthor.assignedPerson.asQualifications =
                        CreateQualifications(qualifications);
                }

                if (participation.Participant.Person != null && participation.Participant.Person.Organisation != null)
                {
                    returnInformationProvider.assignedAuthor.assignedPerson.asEmployment =
                        CreateEmployment(participation.Participant.Person.Organisation);
                }
            }

            return returnInformationProvider;
        }

        /// <summary>
        /// Creates an author from a participation - author and a role
        /// </summary>
        /// <param name="participationAuthor">IParticipationDocumentAuthor</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationDocumentAuthor participationAuthor)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (participationAuthor != null && participationAuthor.Participant != null)
            {
                var author = participationAuthor.Participant;

                if (participationAuthor.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    returnAuthor.time =
                        CreateTimeStampElementIso(participationAuthor.AuthorParticipationPeriodOrDateTimeAuthored);
                }
                else
                {
                    returnAuthor.time = CreateTimeStampElementIso(NullFlavor.NA);
                }

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(participationAuthor.Participant.UniqueIdentifier.ToString(), null) };

                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;

                if (participationAuthor.Role != null)
                {
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(participationAuthor.Role);
                }

                returnAuthor.assignedAuthor.addr = CreateAddressArray(author.Addresses);

                returnAuthor.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                returnAuthor.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null
                };

                if (author.Person != null && author.Person.Identifiers != null)
                {
                    returnAuthor.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();
                }

                if (participationAuthor.Participant.Person != null)
                {
                    if (participationAuthor.Participant.Person.Organisation != null)
                    {
                        returnAuthor.assignedAuthor.assignedPerson.asEmployment =
                            CreateEmployment(participationAuthor.Participant.Person.Organisation);
                    }
                }

                if (!participationAuthor.Participant.Qualifications.IsNullOrEmptyWhitespace())
                {
                    returnAuthor.assignedAuthor.assignedPerson.asQualifications =
                        CreateQualifications(participationAuthor.Participant.Qualifications);
                }

            }

            return returnAuthor;
        }

        /// <summary>
        /// Creates an author from an IParticipationPrescriber
        /// </summary>
        /// <param name="participationAuthor">ParticipationConsumerAuthor</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationPrescriber participationAuthor)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (participationAuthor != null && participationAuthor.Participant != null)
            {
                var author = participationAuthor.Participant;

                if (participationAuthor.Time != null)
                {
                    returnAuthor.time = CreateTimeStampElementIso(participationAuthor.Time);
                }
                else
                {
                    returnAuthor.time = CreateTimeStampElementIso(NullFlavor.NA);
                }

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(participationAuthor.Participant.UniqueIdentifier.ToString(), null) };
                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;
                if (participationAuthor.Role != null)
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(participationAuthor.Role);
                returnAuthor.assignedAuthor.addr = CreateAddressArray(author.Addresses);
                returnAuthor.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                returnAuthor.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null
                };

                if (author.Person != null && author.Person.Identifiers != null)

                    returnAuthor.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();

                if (participationAuthor.Participant.Person != null)
                {
                    if (participationAuthor.Participant.Person.Occupation != null)
                    {
                        returnAuthor.assignedAuthor.assignedPerson.asEmployment = new Employment
                        {
                            jobCode = CreateCodedWithExtensionElement(
                                participationAuthor.Participant.Person.Occupation.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.Code),
                                CodingSystem.ANZSCO,
                                participationAuthor.Participant.Person.Occupation.Value
                                    .GetAttributeValue<NameAttribute, string>(x => x.Name),
                                null,
                                null,
                                null)

                        };
                    }

                    if (participationAuthor.Participant.Person.Qualifications != null)
                        returnAuthor.assignedAuthor.assignedPerson.asQualifications =
                            CreateQualifications(participationAuthor.Participant.Person.Qualifications);
                }
            }

            return returnAuthor;
        }

        /// <summary>
        /// Creates an author from an IParticipationDispenser
        /// </summary>
        /// <param name="participationAuthor">IParticipationDispenser</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateAuthor(IParticipationDispenser participationAuthor)
        {
            var returnAuthor = new POCD_MT000040Author();

            if (participationAuthor != null && participationAuthor.Participant != null)
            {
                var author = participationAuthor.Participant;

                returnAuthor.time = participationAuthor.Time != null
                    ? CreateTimeStampElementIso(participationAuthor.Time)
                    : CreateTimeStampElementIso(NullFlavor.NA);

                returnAuthor.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(participationAuthor.Participant.UniqueIdentifier.ToString(), null) };
                returnAuthor.typeCode = ParticipationType.AUT;
                returnAuthor.typeCodeSpecified = true;
                returnAuthor.assignedAuthor.addr = CreateAddressArray(author.Addresses);
                returnAuthor.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                if (participationAuthor.Role != null)
                    returnAuthor.assignedAuthor.code = CreateCodedWithExtensionElement(participationAuthor.Role);

                returnAuthor.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null
                };

                if (author.Person != null && author.Person.Identifiers != null)
                    returnAuthor.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();


                if (participationAuthor.Participant.Person.Qualifications != null)
                    returnAuthor.assignedAuthor.assignedPerson.asQualifications =
                        CreateQualifications(participationAuthor.Participant.Person.Qualifications);

            }

            return returnAuthor;
        }

        ///// <summary>
        ///// Creates an array of authors
        ///// </summary>
        ///// <param name="createOnDateTime">Date / Time thae document was created</param>
        ///// <returns>POCD_MT000040Author array</returns>
        //internal static POCD_MT000040Author[] CreateAuthors(DateTime? createOnDateTime)
        //{
        //  List<POCD_MT000040Author> returnAuthors = null;

        //  if (createOnDateTime.HasValue)
        //  {
        //    returnAuthors = new List<POCD_MT000040Author>();

        //    returnAuthors.Add(new POCD_MT000040Author
        //    {
        //      time = CreateTimeStampElement(createOnDateTime, null, true),
        //      assignedAuthor =
        //      new POCD_MT000040AssignedAuthor
        //      {
        //        nullFlavor = NullFlavor.NA,
        //        nullFlavorSpecified = true,
        //        id = CreateIdentifierArray(String.Empty, NullFlavor.NA)
        //      },
        //    });
        //  }
        //  if (returnAuthors != null)
        //    return returnAuthors.ToArray();
        //  else
        //    return null;
        //}

        /// <summary>
        /// /Create Author DataTime
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        internal static POCD_MT000040Author CreateAuthor(ISO8601DateTime dataTime)
        {
            POCD_MT000040Author author = null;

            if (dataTime != null)
            {
                author = new POCD_MT000040Author
                {
                    time = CreateTimeStampElementIso(dataTime),
                    assignedAuthor = new POCD_MT000040AssignedAuthor
                    {
                        nullFlavor = NullFlavor.NA,
                        nullFlavorSpecified = true,
                        id = CreateIdentifierArray(null, NullFlavor.NA)
                    }
                };
            }

            return author;
        }

        /// <summary>
        /// Creates an author for a IParticipationInformationProvider 
        /// </summary>
        /// <param name="informationProvider">IParticipationInformationProvider</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateInformationProvider(
            IParticipationInformationProvider informationProvider)
        {
            var returnInformationProvider = new POCD_MT000040Author();

            if (informationProvider != null && informationProvider.Participant != null)
            {
                var author = informationProvider.Participant;

                returnInformationProvider.time = informationProvider.ParticipationPeriod != null
                    ? CreateTimeStampElementIso(informationProvider.ParticipationPeriod.Low)
                    : CreateTimeStampElementIso(NullFlavor.NA);

                returnInformationProvider.assignedAuthor = new POCD_MT000040AssignedAuthor
                { id = CreateIdentifierArray(CreateGuid(), null) };

                returnInformationProvider.typeCode = ParticipationType.AUT;
                returnInformationProvider.typeCodeSpecified = true;

                if (informationProvider.Role != null)
                {
                    returnInformationProvider.assignedAuthor.code =
                        CreateCodedWithExtensionElement(informationProvider.Role);
                }

                returnInformationProvider.assignedAuthor.addr = CreateAddressArray(author.Addresses);

                returnInformationProvider.assignedAuthor.telecom =
                    CreateTelecomunicationArray(author.ElectronicCommunicationDetails);

                returnInformationProvider.assignedAuthor.assignedPerson = new POCD_MT000040Person
                {
                    name = author.Person != null ? CreatePersonNameArray(author.Person.PersonNames) : null
                };

                if (author.Person != null && author.Person.Identifiers != null)
                {
                    returnInformationProvider.assignedAuthor.assignedPerson.asEntityIdentifier =
                        CreateEntityIdentifier(author.Person.Identifiers).ToArray();
                }
            }

            return returnInformationProvider;
        }

        /// <summary>
        /// Creates an Information Provider  
        /// </summary>
        /// <param name="informationProvider">Author</param>
        /// <returns>POCD_MT000040Author</returns>
        internal static POCD_MT000040Author CreateInformationProvider(
            IInformationProviderCollection informationProvider)
        {
            var participationAuthor = new POCD_MT000040Author();

            if (informationProvider != null)
            {
                if (informationProvider is InformationProviderDevice)
                {
                    var device = informationProvider as InformationProviderDevice;
                    participationAuthor = CreateDevice(device);
                }

                // Both types are of type Participation so use the Participant to determin the type 
                if (informationProvider is Participation)
                {
                    var informationProviderHealthcareProvider =
                        informationProvider as IParticipationInformationProviderHealthcareProvider;

                    if (informationProviderHealthcareProvider.Participant != null)
                    {
                        participationAuthor = CreateAuthor(informationProviderHealthcareProvider);
                    }

                    var informationProviderNonHealthcareProvider =
                        informationProvider as IParticipationInformationProviderNonHealthcareProvider;

                    if (informationProviderNonHealthcareProvider.Participant != null)
                    {
                        participationAuthor = CreateAuthor(informationProviderNonHealthcareProvider);
                    }
                }
            }

            return participationAuthor;
        }

        #endregion

        #region internal Methods - POCD_MT000040RecordTarget (SubjectOfCare)

        /// <summary>
        /// Creates a subject of care
        /// </summary>
        /// <param name="subjectOfCareParticipation">IParticipationSubjectOfCare</param>
        /// <returns>POCD_MT000040RecordTarget</returns>
        internal static POCD_MT000040RecordTarget CreateSubjectOfCare(
            IParticipationSubjectOfCare subjectOfCareParticipation)
        {
            POCD_MT000040RecordTarget patient = null;

            if (subjectOfCareParticipation != null)
            {
                var subjectOfCare = subjectOfCareParticipation.Participant;

                if (subjectOfCare != null)
                {
                    var identifier = CreateIdentifierArray(subjectOfCare.UniqueIdentifier.ToString(), null);

                    patient = new POCD_MT000040RecordTarget
                    {
                        templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100091", null),
                        patientRole = new POCD_MT000040PatientRole
                        { id = identifier, classCode = RoleClass.PAT, classCodeSpecified = true },
                        typeCode = ParticipationType.RCT,
                        typeCodeSpecified = true
                    };
                }

                if (patient != null)
                {
                    patient.patientRole.addr = CreateAddressArray(subjectOfCare.Addresses);

                    patient.patientRole.telecom =
                        CreateTelecomunicationArray(subjectOfCare.ElectronicCommunicationDetails);

                    if (subjectOfCare.Person != null)
                    {
                        patient.patientRole.patient = new POCD_MT000040Patient
                        {
                            name = CreatePersonNameArray(subjectOfCare.Person.PersonNames),
                            administrativeGenderCode = subjectOfCare.Person.Gender != null
                                ? CreateCodedWithExtensionElement(
                                    subjectOfCare.Person.Gender.GetAttributeValue<NameAttribute, string>(x => x.Code),
                                    CodingSystem.Gender,
                                    subjectOfCare.Person.Gender.GetAttributeValue<NameAttribute, string>(x => x.Name),
                                    null, null,
                                    null)
                                : null,
                            birthTime = CreateTimeStampElementIso(subjectOfCare.Person.DateOfBirth),
                        };

                        if (subjectOfCare.Person.IndigenousStatus != IndigenousStatus.Undefined)
                        {
                            patient.patientRole.patient.ethnicGroupCode = CreateCodedWithExtensionElement(
                                subjectOfCare.Person.IndigenousStatus.GetAttributeValue<NameAttribute, string>(x =>
                                    x.Code), CodingSystem.METEOR,
                                subjectOfCare.Person.IndigenousStatus.GetAttributeValue<NameAttribute, string>(x =>
                                    x.Name), null, null, null);
                        }

                        if (subjectOfCare.Person.BirthOrder.HasValue)
                        {
                            patient.patientRole.patient.multipleBirthInd =
                                CreateBoolean(true, true, NullFlavor.NA, false);
                            patient.patientRole.patient.multipleBirthOrderNumber =
                                CreateIntegerElement(subjectOfCare.Person.BirthOrder.Value, NullFlavor.NA, false);
                        }

                        if (subjectOfCare.Person.DateOfDeath != null)
                        {
                            patient.patientRole.patient.deceasedInd = CreateBoolean(true, true, NullFlavor.NA, false);
                            patient.patientRole.patient.deceasedTime =
                                CreateTimeStampElementIso(subjectOfCare.Person.DateOfDeath);
                        }

                        if (subjectOfCare.Person.CountryOfBirth != Country.Undefined ||
                            subjectOfCare.Person.StateOfBirth != AustralianState.Undefined)
                        {
                            patient.patientRole.patient.birthplace = new POCD_MT000040Birthplace
                            {
                                place = new POCD_MT000040Place { addr = new AD() }
                            };

                            if (subjectOfCare.Person.CountryOfBirth != Country.Undefined)
                                patient.patientRole.patient.birthplace.place.addr.country = new[]
                                {
                                    new adxpcountry
                                    {
                                        Text = new[]
                                        {
                                            subjectOfCare.Person.CountryOfBirth
                                                .GetAttributeValue<NameAttribute, string>(x => x.Name)
                                        }
                                    }
                                };

                            if (subjectOfCare.Person.StateOfBirth != AustralianState.Undefined)
                                patient.patientRole.patient.birthplace.place.addr.state = new[]
                                {
                                    new adxpstate
                                    {
                                        Text = new[]
                                        {
                                            subjectOfCare.Person.StateOfBirth.GetAttributeValue<NameAttribute, string>(
                                                x => x.Code)
                                        }
                                    }
                                };
                        }

                        if (subjectOfCare.Person.Identifiers != null)
                        {
                            patient.patientRole.patient.asEntityIdentifier =
                                CreateEntityIdentifierArray(subjectOfCare.Person.Identifiers);
                        }
                    }
                }
            }

            return patient;
        }

        #endregion

        #region internal Methods - Employment

        /// <summary>
        /// Creates the employment details.
        /// </summary>
        /// <param name="organisation">Employment organisation details.</param>
        /// <returns>Employment</returns>
        private static Employment CreateEmployment(IEmploymentOrganisation organisation)
        {
            var employment = new Employment
            {
                employerOrganization = CreateOrganisation(organisation),
                classCode = EntityClass.EMP
            };

            if (organisation.Occupation != null)
            {
                employment.jobCode = CreateCodedWithExtensionElement(organisation.Occupation);
            }

            if (organisation.PositionInOrganisation != null)
            {
                employment.code = CreateCodedWithExtensionElement(organisation.PositionInOrganisation);
            }

            if (organisation.EmploymentType != null)
            {
                employment.jobClassCode = CreateCodedWithExtensionElement(organisation.EmploymentType);
            }

            if (organisation.Addresses != null && employment.employerOrganization != null)
            {
                employment.employerOrganization.asOrganizationPartOf.wholeOrganization.addr =
                    CreateAddressArray(organisation.Addresses);
            }

            if (organisation.ElectronicCommunicationDetails != null && employment.employerOrganization != null)
            {
                employment.employerOrganization.asOrganizationPartOf.wholeOrganization.telecom =
                    CreateTelecomunicationArray(organisation.ElectronicCommunicationDetails);
            }

            return employment;
        }

        #endregion

        #region internal Methods - POCD_MT000040Component1 (Healthcare Facility, Dispenser Organisation, Prescriber Organisation )

        internal static POCD_MT000040Component1 CreateComponentOf(IParticipationHealthcareFacility healthcareFacility)
        {
            var castedParticipant = healthcareFacility as IParticipationDispenserOrganisation;

            if (healthcareFacility.Participant != null)
            {
                castedParticipant.Participant = new SCSModel.Common.Participant();

                if (healthcareFacility.Participant.Addresses != null)
                    castedParticipant.Participant.Addresses = healthcareFacility.Participant.Addresses;

                if (healthcareFacility.Participant.ElectronicCommunicationDetails != null)
                    castedParticipant.Participant.ElectronicCommunicationDetails =
                        healthcareFacility.Participant.ElectronicCommunicationDetails;

                if (healthcareFacility.Participant.Organisation != null)
                    castedParticipant.Participant.Organisation = healthcareFacility.Participant.Organisation;

                castedParticipant.Participant.UniqueIdentifier = healthcareFacility.Participant.UniqueIdentifier;
            }

            var component = CreateComponentOf(castedParticipant);

            if (healthcareFacility.ParticipationPeriod != null)
                component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(healthcareFacility.ParticipationPeriod);

            if (healthcareFacility.Role != null)
            {
                // Update the Mandatory Role field
                component.encompassingEncounter.location.healthCareFacility.code = CreateCodedWithExtensionElement(healthcareFacility.Role);
            }

            return component;
        }

        internal static POCD_MT000040Component1 CreateComponentOf(
            IParticipationDispenserOrganisation organisationParticipation)
        {
            var location = new POCD_MT000040Location();
            location.typeCode = ParticipationTargetLocation.LOC;
            location.healthCareFacility = new POCD_MT000040HealthCareFacility();
            location.healthCareFacility.code = CreateCodedWithExtensionElement(
                "PHARM",
                CodingSystem.HL7ServiceDeliveryLocationRoleType,
                "Pharmacy",
                null,
                null,
                null);

            if (organisationParticipation.Participant != null)
            {
                location.healthCareFacility.id =
                    CreateIdentifierArray(organisationParticipation.Participant.UniqueIdentifier.ToString(), null);

                if (organisationParticipation.Participant.Organisation != null)
                {
                    location.healthCareFacility.serviceProviderOrganization =
                        CreateOrganisation(organisationParticipation.Participant.Organisation);

                    if (organisationParticipation.Participant.Addresses != null &&
                        organisationParticipation.Participant.Addresses.Any())
                        location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization
                            .addr = CreateAddressArray(organisationParticipation.Participant.Addresses);

                    if (organisationParticipation.Participant.ElectronicCommunicationDetails != null &&
                        organisationParticipation.Participant.ElectronicCommunicationDetails.Any())
                        location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization
                            .telecom = CreateTelecomunicationArray(organisationParticipation.Participant
                            .ElectronicCommunicationDetails);
                }
            }


            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();
            component.encompassingEncounter.effectiveTime =
                new IVL_TS { nullFlavor = NullFlavor.NA, nullFlavorSpecified = true };
            component.encompassingEncounter.location = location;

            return component;
        }

        internal static POCD_MT000040Component1 CreateComponentOf(
            IParticipationPrescriberOrganisation organisationParticipation)
        {
            var location = new POCD_MT000040Location();
            location.typeCode = ParticipationTargetLocation.LOC;
            location.healthCareFacility = new POCD_MT000040HealthCareFacility();
            location.healthCareFacility.code = CreateCodedWithExtensionElement(organisationParticipation.Role);

            location.healthCareFacility.id = organisationParticipation.Participant != null
                ? CreateIdentifierArray(organisationParticipation.Participant.UniqueIdentifier.ToString(), null)
                : null;

            if (organisationParticipation.Participant != null)
                if (organisationParticipation.Participant.Organisation != null)
                {
                    location.healthCareFacility.serviceProviderOrganization =
                        CreateOrganisation(organisationParticipation.Participant.Organisation);

                    if (organisationParticipation.Participant.Addresses != null &&
                        organisationParticipation.Participant.Addresses.Any())
                        location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization
                            .addr = CreateAddressArray(organisationParticipation.Participant.Addresses);

                    if (organisationParticipation.Participant.ElectronicCommunicationDetails != null &&
                        organisationParticipation.Participant.ElectronicCommunicationDetails.Any())
                        location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization
                            .telecom = CreateTelecomunicationArray(organisationParticipation.Participant
                            .ElectronicCommunicationDetails);
                }

            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();
            component.encompassingEncounter.effectiveTime =
                new IVL_TS { nullFlavor = NullFlavor.NA, nullFlavorSpecified = true };
            component.encompassingEncounter.location = location;

            return component;
        }

        internal static POCD_MT000040Component1 CreateComponentOfForSml(SCSModel.PCML.Entities.Encounter encounter)
        {
            var component = new POCD_MT000040Component1();
            component.encompassingEncounter = new POCD_MT000040EncompassingEncounter();

            // For SML add id and templateid
            component.encompassingEncounter.templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100064");
            component.encompassingEncounter.id = CreateIdentifierArray(encounter.EncounterId.ToString());

            // Optional
            if (encounter != null) component.encompassingEncounter.code = CreateCodedValue(encounter.EncounterClass);
            if (encounter.EncounterPeriod != null) component.encompassingEncounter.effectiveTime = CreateIntervalTimestamp(encounter.EncounterPeriod);

            return component;
        }
        #endregion

        #region internal Methods - POCD_MT000040Participant1

        /// <summary>
        /// Creates a participant from a participation - usualGP
        /// </summary>
        /// <param name="participation">IParticipationUsualGP</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipationUsualGP participation)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                var functionCode =
                    CreateCodedWithExtensionElement("PCP", null, "Primary Care Physician", null, null, null);

                returnParticipant = CreateParticipant(castedParticipation, ParticipationType.PART,
                    RoleClassAssociative.PROV, functionCode);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - Referrer
        /// </summary>
        /// <param name="participation">IParticipationReferrer</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipationReferrer participation)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                var functionCode =
                    CreateCodedWithExtensionElement("PCP", null, "Primary Care Physician", null, null, null);

                returnParticipant = CreateParticipant(castedParticipation, ParticipationType.REFB,
                    RoleClassAssociative.PROV, functionCode);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation for a IParticipationRequester
        /// </summary>
        /// <param name="participation">IParticipationRequester</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipationRequester participation)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateParticipant(castedParticipation, ParticipationType.REF,
                    RoleClassAssociative.ASSIGNED, null);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - referee
        /// </summary>
        /// <param name="participation">IParticipationReferee</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipationReferee participation)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                returnParticipant = CreateParticipant(castedParticipation, ParticipationType.REFT,
                    RoleClassAssociative.PROV, null);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant.
        /// </summary>
        /// <param name="participation">Patient nominated contact.</param>
        /// <returns>CDA participant.</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipationPatientNominatedContact participation)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                Guid guid = ((IParticipant)participation.Participant).UniqueIdentifier;

                returnParticipant = new POCD_MT000040Participant1
                {
                    typeCode = ParticipationType.IRCP,
                    functionCode = null,
                    associatedEntity =
                        new POCD_MT000040AssociatedEntity
                        {
                            classCode = RoleClassAssociative.CON,
                            id = CreateIdentifierArray(guid.ToString(), null, null),
                            associatedPerson = participation.Participant.Person != null &&
                                               (participation.Participant.Person.PersonNames != null ||
                                                participation.Participant.Person.Identifiers != null)
                                ? new POCD_MT000040Person
                                {
                                    // PersonName
                                    name = participation.Participant.Person == null
                                        ? null
                                        : CreatePersonNameArray(participation.Participant.Person.PersonNames),
                                    // EntityIdentifier
                                    asEntityIdentifier =
                                        participation.Participant.Person == null ||
                                        participation.Participant.Person.Identifiers == null
                                            ? null
                                            : CreateEntityIdentifier(participation.Participant.Person.Identifiers)
                                                .ToArray()
                                }
                                : null,
                            // Addresses
                            addr = CreateAddressArray(participation.Participant.Addresses),
                            // Organisation
                            scopingOrganization = participation.Participant.Organisation == null
                                ? null
                                : CreateOrganisation(participation.Participant.Organisation)
                        }
                };

                if (participation.Role != null)
                {
                    returnParticipant.associatedEntity.code = CreateCodedWithExtensionElement(participation.Role);
                }

                if (participation.Participant.Addresses != null && participation.Participant.Addresses.Any())
                {
                    returnParticipant.associatedEntity.addr =
                        CreateAddressArray(participation.Participant.Addresses.ConvertAll(address => address));
                }

                returnParticipant.associatedEntity.telecom =
                    CreateTelecomunicationArray(participation.Participant.ElectronicCommunicationDetails);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant.
        /// </summary>
        /// <param name="participation">Patient nominated contact.</param>
        /// <param name="participationType">Participation Type</param>
        /// <param name="roleClassAssociative">Role Class Associative</param>
        /// <param name="functionCode">Function Code</param>
        /// <param name="subjectOfCareIdentifier">Subject Of Care Identifier</param>
        /// <returns>CDA participant.</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipationPersonOrOrganisation participation,
            ParticipationType participationType, RoleClassAssociative roleClassAssociative, CE functionCode,
            string subjectOfCareIdentifier)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation?.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                returnParticipant = CreateParticipant(castedParticipation, participationType, roleClassAssociative,
                    functionCode, subjectOfCareIdentifier);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a Participant and a role
        /// </summary>
        /// <param name="participation">IParticipation</param>
        /// <param name="roleClassAssociative">roleClassAssociative</param>
        /// <param name="participationType">participationType</param>
        /// <param name="functionCode">functionCode</param>
        /// <param name="subjectOfCareIdentifier">Subject Of Care Identifier</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant1 CreateParticipant(IParticipation participation,
            ParticipationType participationType, RoleClassAssociative roleClassAssociative, CE functionCode,
            string subjectOfCareIdentifier = null)
        {
            POCD_MT000040Participant1 returnParticipant = null;

            if (participation?.Participant != null)
            {
                // Set up the ParticipationPeriod
                IVL_TS participationPeriod = null;

                if (participation.ParticipationPeriod != null)
                {
                    participationPeriod = CreateIntervalTimestamp(participation.ParticipationPeriod);
                }

                if (participation.ParticipationEndTime != null)
                {
                    participationPeriod = CreateIntervalTimestamp(participation.ParticipationEndTime, null);
                }

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    participationPeriod =
                        CreateIntervalTimestamp(participation.AuthorParticipationPeriodOrDateTimeAuthored, null);
                }

                // Set POCD_MT000040Participant1
                returnParticipant = new POCD_MT000040Participant1
                {
                    templateId = CreateIdentifierArray("1.2.36.1.2001.1001.102.101.100037", null),
                    time = participationPeriod,
                    typeCode = participationType,
                    functionCode = functionCode,
                    associatedEntity =
                        new POCD_MT000040AssociatedEntity
                        {
                            classCode = roleClassAssociative,
                            id = CreateIdentifierArray(participation.Participant.UniqueIdentifier.ToString(), null,
                                null),
                            associatedPerson = participation.Participant.Person != null
                                ? new POCD_MT000040Person
                                {
                                    // Personal Relationship
                                    personalRelationship =
                                        participation.Participant?.RelationshipToSubjectOfCare != null &&
                                        !subjectOfCareIdentifier.IsNullOrEmptyWhitespace()
                                            ? CreatePersonalRelationship(
                                                participation.Participant.RelationshipToSubjectOfCare,
                                                subjectOfCareIdentifier)
                                            : null,
                                    // Person Name
                                    name = participation.Participant.Person?.PersonNames != null &&
                                           participation.Participant.Person.PersonNames.Any()
                                        ? CreatePersonNameArray(participation.Participant.Person.PersonNames)
                                        : null,
                                    // Entity Identifier
                                    asEntityIdentifier =
                                        participation.Participant.Person?.Identifiers != null &&
                                        participation.Participant.Person.Identifiers.Any()
                                            ? CreateEntityIdentifier(participation.Participant.Person.Identifiers)
                                                .ToArray()
                                            : null
                                }
                                : null,
                            // Addresses
                            addr = CreateAddressArray(participation.Participant.Addresses),
                            // Organisation
                            scopingOrganization = participation.Participant.Organisation == null
                                ? null
                                : CreateOrganisation(participation.Participant.Organisation)
                        }
                };

                if (participation.Role != null)
                {
                    returnParticipant.associatedEntity.code = CreateCodedWithExtensionElement(participation.Role);
                }

                if (participation.Participant.Addresses != null && participation.Participant.Addresses.Any())
                {
                    returnParticipant.associatedEntity.addr =
                        CreateAddressArray(participation.Participant.Addresses.ConvertAll(address => address));
                }

                returnParticipant.associatedEntity.telecom =
                    CreateTelecomunicationArray(participation.Participant.ElectronicCommunicationDetails);

                // Set the Person Organisation
                if (participation.Participant.Person?.Organisation != null)
                {
                    // PAW: To support nullFlavor without adding complextity to the model, look for null
                    if (!string.IsNullOrWhiteSpace(participation.Participant.Person.Organisation.Name))
                        returnParticipant.associatedEntity.associatedPerson.asEmployment =
                            CreateEmployment(participation.Participant.Person.Organisation);
                    else
                    {
                        // PAW: Fixed to use NullFlavor at the correct level
                        returnParticipant.associatedEntity.associatedPerson.asEmployment = new Employment
                        {
                            employerOrganization = new POCD_MT000040Organization
                            {
                                id = CreateIdentifierArray(new UniqueId()),
                                asOrganizationPartOf = new POCD_MT000040OrganizationPartOf
                                {
                                    wholeOrganization = new POCD_MT000040Organization
                                    {
                                        name = new ON[]
                                        {
                                            new ON
                                            {
                                                use = new EntityNameUse[] {EntityNameUse.ORGX},
                                                nullFlavor = NullFlavor.UNK,
                                                nullFlavorSpecified = true,

                                            }
                                        }
                                    }
                                }
                            }
                        };
                    }
                }

                if (participation.Participant.Qualifications != null)
                    returnParticipant.associatedEntity.associatedPerson.asQualifications =
                        CreateQualifications(participation.Participant.Qualifications);

            }

            return returnParticipant;
        }

        #endregion

        #region internal Methods - POCD_MT000040Participant2

        private static POCD_MT000040Participant2 CreateParticipant2(
            IParticipationPrescriberInstructionRecipient participation)
        {
            POCD_MT000040Participant2 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;
                castedParticipation.Participant.Person = (Person)participation.Participant.Person;

                if (participation.Participant.Person != null)
                {
                    if (participation.Participant.Person.Identifiers != null)
                    {
                        castedParticipation.Participant.Person.Identifiers =
                            participation.Participant.Person.Identifiers;
                    }

                    if (participation.Participant.Person.PersonNames != null)
                    {
                        castedParticipation.Participant.Person.PersonNames =
                            participation.Participant.Person.PersonNames;
                    }

                    // Qualifications can not be mapped
                }

                castedParticipation.Role = new CodableText
                {
                    Code = "2515",
                    CodeSystem = CodingSystem.ANZSCO,
                    DisplayName = "Pharmacists"
                };

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant = CreateParticipant2(castedParticipation, ParticipationType.TRANS, RoleClassRoot.ROL);
            }

            return returnParticipant;
        }

        private static POCD_MT000040Participant2 CreateParticipant2(IParticipationReportingPathologist participation)
        {
            POCD_MT000040Participant2 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;
                castedParticipation.Participant.Person = (Person)participation.Participant.Person;

                if (participation.Participant.Person != null)
                {
                    if (participation.Participant.Person.Identifiers != null)
                    {
                        castedParticipation.Participant.Person.Identifiers =
                            participation.Participant.Person.Identifiers;
                    }

                    if (participation.Participant.Person.PersonNames != null)
                    {
                        castedParticipation.Participant.Person.PersonNames =
                            participation.Participant.Person.PersonNames;
                    }

                    if (participation.Participant.Person.Organisation != null)
                    {
                        castedParticipation.Participant.Person.Organisation =
                            participation.Participant.Person.Organisation;
                    }

                    // Qualifications can not be mapped
                }

                if (participation.Role != null)
                {
                    castedParticipation.Role = participation.Role;
                }

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                }

                returnParticipant =
                    CreateParticipant2(castedParticipation, ParticipationType.RESP, RoleClassRoot.ASSIGNED);
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation and a role
        /// </summary>
        /// <returns>POCD_MT000040Participant2</returns>
        internal static POCD_MT000040Participant2 CreateParticipant2(IParticipation participation,
            ParticipationType participationType, RoleClassRoot roleClassRoot)
        {
            POCD_MT000040Participant2 returnParticipant = null;

            // Set up the ParticipationPeriod
            IVL_TS participationPeriod = null;

            if (participation.ParticipationPeriod != null)
            {
                participationPeriod = CreateIntervalTimestamp(participation.ParticipationPeriod);
            }

            if (participation.ParticipationEndTime != null)
            {
                participationPeriod = CreateIntervalTimestamp(participation.ParticipationEndTime, null);
            }

            if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
            {
                participationPeriod =
                    CreateIntervalTimestamp(participation.AuthorParticipationPeriodOrDateTimeAuthored, null);
            }

            if (participation.Participant != null)
            {
                returnParticipant = new POCD_MT000040Participant2
                {
                    time = participationPeriod, // Set ParticipationPeriod
                    typeCode = participationType,
                    participantRole = new POCD_MT000040ParticipantRole
                    {
                        id = CreateIdentifierArray(participation.Participant.UniqueIdentifier.ToString(), null, null),
                        classCode = roleClassRoot,
                        code = CreateCodedWithExtensionElement(participation.Role),
                        playingEntity = participation.Participant.Person != null
                            ? new POCD_MT000040PlayingEntity
                            {

                                classCode = EntityClassRoot.PSN,
                                asEmployment = participation.Participant.Person.Organisation != null
                                    ? CreateEmployment(participation.Participant.Person.Organisation)
                                    : null,
                                asQualifiedEntity = participation.Participant.Qualifications != null
                                    ? CreateQualifications(participation.Participant.Qualifications)
                                    : null,
                                name = participation.Participant.Person.PersonNames != null &&
                                       participation.Participant.Person.PersonNames.Any()
                                    ? CreatePersonNameArray(participation.Participant.Person.PersonNames)
                                    : null,
                                // EntityIdentifier
                                asEntityIdentifier =
                                    participation.Participant.Person.Identifiers != null &&
                                    participation.Participant.Person.Identifiers.Any()
                                        ? CreateEntityIdentifier(participation.Participant.Person.Identifiers).ToArray()
                                        : null,
                            }
                            : null,
                        // Addresses
                        addr = CreateAddressArray(participation.Participant.Addresses),
                        // Electronic Communication Details
                        telecom = participation.Participant.ElectronicCommunicationDetails != null
                            ? CreateTelecomunicationArray(participation.Participant.ElectronicCommunicationDetails)
                            : null,
                        // Organisation
                        scopingEntity = participation.Participant.Organisation != null
                            ? CreateOrganisationEntity(participation.Participant.Organisation)
                            : null,
                    },
                };
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant.
        /// </summary>
        /// <param name="participation"></param>
        /// <returns>Participant.</returns>
        internal static POCD_MT000040Participant2 CreateServiceRequesterForMedicareXMLMockup(
            IParticipation participation)
        {
            POCD_MT000040Participant2 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                POCD_MT000040Entity scopingEntity = null;

                if (participation.Participant.Person != null && participation.Participant.Person.Organisation != null)
                {
                    var wholeEntity = new Entity
                    {
                        asEntityIdentifier =
                            CreateEntityIdentifierArray(participation.Participant.Person.Organisation.Identifiers),
                        name = new[]
                        {
                            CreateName(participation.Participant.Person.Organisation.Name,
                                participation.Participant.Person.Organisation.NameUsage)
                        }
                    };

                    var asOrganizationPartOf = new OrganizationPartOf
                    {
                        wholeEntity = wholeEntity
                    };

                    scopingEntity = new POCD_MT000040Entity
                    {
                        asOrganizationPartOf = new[] { asOrganizationPartOf },
                        classCode = EntityClassRoot.ORG
                    };
                }

                var playingEntity = new POCD_MT000040PlayingEntity
                {
                    asEntityIdentifier = participation.Participant.Person != null
                        ? CreateEntityIdentifierArray(participation.Participant.Person.Identifiers)
                        : null,
                    name = participation.Participant.Person != null
                        ? CreatePersonNameArray(participation.Participant.Person.PersonNames)
                        : null,
                    classCode = EntityClassRoot.PSN,
                };

                var participantRole = new POCD_MT000040ParticipantRole
                {
                    classCode = (RoleClassRoot)Enum.Parse(typeof(RoleClassRoot),
                        participation.RoleType.GetAttributeValue<NameAttribute, string>(a => a.Code)),
                    code = CreateCodedWithExtensionElement(participation.Role),
                    addr = participation.Participant.Addresses == null
                        ? null
                        : CreateAddressArray(participation.Participant.Addresses),
                    telecom = participation.Participant.ElectronicCommunicationDetails == null
                        ? null
                        : CreateTelecomunicationArray(participation.Participant.ElectronicCommunicationDetails),
                    playingEntity = playingEntity,
                    scopingEntity = scopingEntity
                };

                returnParticipant = new POCD_MT000040Participant2
                {
                    time = CreateIntervalTimestamp(participation.ParticipationPeriod),
                    typeCode = ParticipationType.REFB,
                    participantRole = participantRole
                };
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - Repository
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <returns>POCD_MT000040Participant2</returns>
        internal static POCD_MT000040Participant2 CreateParticipant2(Repository repository)
        {
            var participant = new POCD_MT000040Participant2();

            if (repository != null)
            {
                participant.participantRole = new POCD_MT000040ParticipantRole
                {
                    id = CreateIdentifierArray(CreateGuid(), null),
                    code = CreateCodedWithExtensionElement(NullFlavour.NotApplicable)
                };

                participant.typeCode = ParticipationType.DEV;

                if (repository.Identifiers != null && repository.Identifiers.Any())
                {
                    participant.participantRole.scopingEntity = new POCD_MT000040Entity
                    {
                        asEntityIdentifier = CreateEntityIdentifier(repository.Identifiers).ToArray()
                    };
                }

                participant.participantRole.playingDevice = new POCD_MT000040Device
                {
                    softwareName = !repository.SoftwareName.IsNullOrEmptyWhitespace()
                        ? new SC { Text = new[] { repository.SoftwareName } }
                        : null
                };
            }

            return participant;
        }

        /// <summary>
        /// Creates a participant from a participation - Authorisee
        /// </summary>
        /// <param name="participation">IParticipationAuthorisee</param>
        /// <returns>POCD_MT000040Participant2</returns>
        internal static POCD_MT000040Participant2 CreateParticipant2(IParticipationAuthorisee participation)
        {
            POCD_MT000040Participant2 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (participation.Participant.Organisation != null)
                    castedParticipation.Participant.Organisation = participation.Participant.Organisation;

                returnParticipant = CreateParticipant2(castedParticipation, ParticipationType.PRCP, RoleClassRoot.ROL);

                // Below exists custom mapping for asOrganizationPartOf for ElectronicCommunicationDetails & Addresses
                // Note: If this mapping appears in any other IG then a custom CreateParticipant2 should be created, 
                // it is assumed to be a one off mapping for now

                // Clear ElectronicCommunicationDetails in castedParticipation (custom mapping below)
                returnParticipant.participantRole.telecom = null;
                // Clear Addresses in castedParticipation (custom mapping below)
                returnParticipant.participantRole.addr = null;

                var scopingEntity = returnParticipant.participantRole.scopingEntity;
                if (scopingEntity != null && scopingEntity.asOrganizationPartOf[0].wholeEntity != null)
                {
                    if (participation.Participant.ElectronicCommunicationDetails != null)
                        scopingEntity.asOrganizationPartOf[0].wholeEntity.telecom =
                            CreateTelecomunicationArray(participation.Participant.ElectronicCommunicationDetails);

                    if (participation.Participant.Addresses != null)
                        scopingEntity.asOrganizationPartOf[0].wholeEntity.addr =
                            CreateAddressArray(participation.Participant.Addresses);
                }
            }

            return returnParticipant;
        }

        /// <summary>
        /// Creates a participant from a participation - Service Requester
        /// </summary>
        /// <param name="participation">IParticipationReferee</param>
        /// <returns>POCD_MT000040Participant1</returns>
        internal static POCD_MT000040Participant2 CreateParticipant2(IParticipationServiceRequester participation)
        {
            POCD_MT000040Participant2 returnParticipant = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = new Person();

                        if (participation.Participant.Person.Identifiers != null)
                        {
                            castedParticipation.Participant.Person.Identifiers =
                                participation.Participant.Person.Identifiers;
                        }

                        if (participation.Participant.Person.PersonNames != null)
                        {
                            castedParticipation.Participant.Person.PersonNames =
                                participation.Participant.Person.PersonNames;
                        }
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                var roleClassAssociative = (RoleClassRoot)Enum.Parse(typeof(RoleClassRoot),
                    participation.RoleClassCodeCode.GetAttributeValue<NameAttribute, string>(x => x.Code));
                returnParticipant =
                    CreateParticipant2(castedParticipation, ParticipationType.REFB, roleClassAssociative);
            }

            return returnParticipant;
        }

        private static POCD_MT000040Participant2 CreateParticipant2(CE code, EntityDeterminer? determinerCode)
        {
            // Generate id
            II[] ident = CreateIdentifierArray(CreateGuid(), null);

            var participant = new POCD_MT000040Participant2
            {
                typeCode = ParticipationType.CAGNT,
                participantRole = new POCD_MT000040ParticipantRole
                {
                    id = ident,
                    classCode = RoleClassRoot.ROL,
                    playingEntity = new POCD_MT000040PlayingEntity
                    {
                        classCode = EntityClassRoot.ENT
                    }
                }
            };

            if (determinerCode != null)
            {
                participant.participantRole.playingEntity.determinerCode = (EntityDeterminer)determinerCode;
                participant.participantRole.playingEntity.determinerCodeSpecified = true;
            }

            participant.participantRole.playingEntity.code = code;

            return participant;
        }

        private static POCD_MT000040Participant2[] CreateParticipant2Array(CE code, EntityDeterminer? determinerCode)
        {
            var participantList = new List<POCD_MT000040Participant2>
            {
                CreateParticipant2(code, determinerCode)
            };

            return participantList.ToArray();
        }


        #endregion

        #region internal Methods - POCD_MT000040Performer2


        /// <summary>
        /// Creates a Participation Service Provider
        /// </summary>
        /// <param name="participation">A IParticipationServiceProvider</param>
        /// <returns>POCD_MT000040Performer2</returns>
        internal static POCD_MT000040Performer2 CreatePerformer(IParticipationServiceProvider participation)
        {
            POCD_MT000040Performer2 performer = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = participation.Participant.Person;

                        if (participation.Participant.Person.Entitlements != null)
                            castedParticipation.Participant.Entitlements =
                                participation.Participant.Person.Entitlements;

                        if (participation.Participant.Person.Qualifications != null)
                            castedParticipation.Participant.Qualifications =
                                participation.Participant.Person.Qualifications;
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }


                performer = CreatePerformer(castedParticipation, ParticipationPhysicalPerformer.PRF,
                    castedParticipation.Participant.UniqueIdentifier.ToString());
            }

            return performer;
        }

        /// <summary>
        /// Creates a Participation Service Provider
        /// </summary>
        /// <param name="participation">A IParticipationServiceProvider</param>
        /// <param name="subjectOfCareIdentifier">Subject Of Care Identifier</param>
        /// <returns>POCD_MT000040Performer2</returns>
        internal static POCD_MT000040Performer2 CreatePerformer(IParticipationPersonOrOrganisation participation,
            string subjectOfCareIdentifier)
        {
            POCD_MT000040Performer2 performer = null;

            if (participation?.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = participation.Participant.Person;

                        if (participation.Participant.Entitlements != null)
                            castedParticipation.Participant.Entitlements = participation.Participant.Entitlements;

                        if (participation.Participant.Qualifications != null)
                            castedParticipation.Participant.Qualifications = participation.Participant.Qualifications;
                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                performer = CreatePerformer(castedParticipation, ParticipationPhysicalPerformer.PRF,
                    castedParticipation.Participant.UniqueIdentifier.ToString(), subjectOfCareIdentifier);
            }

            return performer;
        }

        /// <summary>
        /// Creates a Participation Document Author
        /// </summary>
        /// <param name="participation">A IParticipationDocumentAuthor</param>
        /// <returns>POCD_MT000040Performer2</returns>
        internal static POCD_MT000040Performer2 CreatePerformer(IParticipationDocumentAuthor participation)
        {
            POCD_MT000040Performer2 performer = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        castedParticipation.Participant.Person = participation.Participant.Person;

                        if (participation.Participant.Person.Organisation != null)
                            castedParticipation.Participant.Person.Organisation =
                                participation.Participant.Person.Organisation;

                    }

                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;

                }

                performer = CreatePerformer(castedParticipation, ParticipationPhysicalPerformer.PRF);
            }

            return performer;
        }

        /// <summary>
        /// Creates a Participation ACD Custodian Entry Participation
        /// </summary>
        /// <param name="participation">A IParticipationAcdCustodian</param>
        /// <returns>POCD_MT000040Performer2</returns>
        internal static POCD_MT000040Performer2 CreatePerformer(IParticipationAcdCustodian participation)
        {
            POCD_MT000040Performer2 performer = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses =
                            participation.Participant.Addresses.Cast<IAddress>().ToList();
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                performer = CreatePerformer(castedParticipation, ParticipationPhysicalPerformer.PRF);

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                    {
                        if (participation.Participant.Person.Gender != null)
                        {
                            performer.assignedEntity.assignedPerson.administrativeGenderCode =
                                CreateCodedWithExtensionElement(
                                    participation.Participant.Person.Gender.GetAttributeValue<NameAttribute, string>(
                                        x => x.Code),
                                    CodingSystem.Gender,
                                    participation.Participant.Person.Gender.GetAttributeValue<NameAttribute, string>(
                                        x => x.Name), null, null, null);
                        }

                        if (participation.Participant.Person.DateOfBirth != null)
                        {
                            performer.assignedEntity.assignedPerson.birthTime =
                                CreateTimeStampElementIso(participation.Participant.Person.DateOfBirth);
                        }
                    }
                }
            }

            return performer;
        }

        /// <summary>
        /// Creates a Performer
        /// </summary>
        /// <param name="participation">A POCD_MT000040Performer2</param>
        /// <returns>IParticipationAddressee</returns>
        internal static POCD_MT000040Performer2 CreatePerformer(IParticipationAddressee participation)
        {
            POCD_MT000040Performer2 performer = null;

            if (participation != null && participation.Participant != null)
            {
                var castedParticipation = (IParticipation)participation;
                castedParticipation.Participant = (IParticipant)participation.Participant;

                if (castedParticipation.Participant != null)
                {
                    if (participation.Participant.Person != null)
                        castedParticipation.Participant.Person = participation.Participant.Person;
                    if (participation.Participant.Addresses != null)
                        castedParticipation.Participant.Addresses = participation.Participant.Addresses;
                    if (participation.Participant.Organisation != null)
                        castedParticipation.Participant.Organisation = participation.Participant.Organisation;
                }

                performer = CreatePerformer(castedParticipation, ParticipationPhysicalPerformer.PRF);
            }

            return performer;
        }

        /// <summary>
        /// Creates a Participation Service Provider
        /// </summary>
        /// <returns>POCD_MT000040Performer2</returns>
        internal static POCD_MT000040Performer2 CreatePerformer(IParticipation participation,
            ParticipationPhysicalPerformer typeCode, string id = null, string subjectOfCareIdentifier = null)
        {
            POCD_MT000040Performer2 performer = null;

            // Set up the organisation for the Person or Organisation
            if (participation != null)
            {
                // Set up the ParticipationPeriod
                IVL_TS participationPeriod = null;

                if (participation.ParticipationPeriod != null)
                {
                    participationPeriod = CreateIntervalTimestamp(participation.ParticipationPeriod);
                }

                if (participation.ParticipationEndTime != null)
                {
                    participationPeriod = CreateIntervalTimestamp(participation.ParticipationEndTime, null);
                }

                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    participationPeriod =
                        CreateIntervalTimestamp(participation.AuthorParticipationPeriodOrDateTimeAuthored, null);
                }

                // Set up HealthProfessional
                if (participation.Participant != null)
                {
                    performer = new POCD_MT000040Performer2
                    {
                        time = participationPeriod, // Set ParticipationPeriod
                        typeCode = typeCode,
                        typeCodeSpecified = true,
                        assignedEntity =
                            new POCD_MT000040AssignedEntity
                            {
                                code = CreateCodedWithExtensionElement(participation.Role),
                                id = id.IsNullOrEmptyWhitespace()
                                    ? CreateIdentifierArray(CreateGuid(), null, null)
                                    : CreateIdentifierArray(id, null, null),
                                // Create assigned person
                                assignedPerson = participation.Participant.Person == null
                                    ? null
                                    : new POCD_MT000040Person
                                    {
                                        asEntityIdentifier = participation.Participant.Person.Identifiers != null
                                            ? CreateEntityIdentifier(participation.Participant.Person.Identifiers)
                                                .ToArray()
                                            : null,
                                        name = CreatePersonNameArray(participation.Participant.Person.PersonNames),
                                        asEmployment = participation.Participant.Person.Organisation == null
                                            ? null
                                            : CreateEmployment(participation.Participant.Person.Organisation),
                                        asQualifications = participation.Participant.Qualifications == null
                                            ? null
                                            : CreateQualifications(participation.Participant.Qualifications),
                                        personalRelationship =
                                            participation.Participant.RelationshipToSubjectOfCare != null &&
                                            !subjectOfCareIdentifier.IsNullOrEmptyWhitespace()
                                                ? CreatePersonalRelationship(
                                                    participation.Participant.RelationshipToSubjectOfCare,
                                                    subjectOfCareIdentifier)
                                                : null
                                    },
                                // Create Address
                                addr = CreateAddressArray(participation.Participant.Addresses),
                                // Electronic Communication Details
                                telecom = CreateTelecomunicationArray(participation.Participant
                                    .ElectronicCommunicationDetails),
                                // Create Organization
                                representedOrganization = participation.Participant.Organisation == null
                                    ? null
                                    : CreateOrganisation(participation.Participant.Organisation),
                            }
                    };

                }
            }

            return performer;
        }

        private static PersonalRelationship[] CreatePersonalRelationship(ICodableText codableText,
            string subjectOfCareIdentifier)
        {
            PersonalRelationship[] personalRelationship = null;

            if (codableText != null)
            {
                personalRelationship = new[]
                {
                    new PersonalRelationship
                    {
                        classCode = RoleClass.PRS,
                        code = CreateConceptDescriptor(codableText),
                        asPersonalRelationship = new POCD_MT000040Patient()
                        {
                            id = CreateIdentifierElement(subjectOfCareIdentifier),
                            administrativeGenderCode = new CE()
                            {
                                nullFlavor = NullFlavor.NA,
                                nullFlavorSpecified = true
                            }
                        }
                    }
                };
            }

            return personalRelationship;
        }

        #endregion

        #region internal Methods - HL7.CDA.Participant

        private static HL7.CDA.Participant CreateParticipant(ParticipantRole participantRole,
            ParticipationType? participationType)
        {
            var participant = new HL7.CDA.Participant
            {
                participantRole = participantRole
            };

            if (participationType.HasValue)
            {
                participant.typeCode = participationType.Value;
                participant.typeCodeSpecified = true;
            }

            return participant;
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region Private - Address

        /// <summary>
        /// Creates a CDA address array from an address
        /// </summary>
        /// <param name="address">IAddress</param>
        /// <returns>AD array</returns>
        private static AD[] CreateAddressArray(IAddress address)
        {
            AD[] addressArray = null;

            if (address != null)
            {
                addressArray = new[] { CreateAddress(address) };
            }

            return addressArray;
        }

        /// <summary>
        /// Creates a CDA address array from a list of addresses
        /// </summary>
        /// <param name="addresses">IAddress</param>
        /// <returns>AD array</returns>
        private static AD[] CreateAddressArray(IEnumerable<IAddress> addresses)
        {
            AD[] addressArray = null;

            if (addresses != null && addresses.Any())
            {
                addressArray = addresses.Select(CreateAddress).ToArray();
            }

            return addressArray;
        }

        /// <summary>
        /// Creates a CD address array from a list of Australian addresses
        /// </summary>
        /// <param name="addresses">list of Australian addresses</param>
        /// <returns>AD array</returns>
        private static AD[] CreateAddressArray(IEnumerable<IAddressAustralian> addresses)
        {
            AD[] addressArray = null;

            if (addresses != null && addresses.Count() > 0)
            {
                var addressList = new List<AD>();

                foreach (var address in addresses)
                {
                    addressList.Add(CreateAddress((IAddress)address));
                }

                addressArray = addressList.ToArray();
            }


            return addressArray;
        }

        /// <summary>
        /// Creates a CDA address from an address
        /// </summary>
        /// <param name="address">IAddress</param>
        /// <returns>AD</returns>
        private static AD CreateAddress(IAddress address)
        {
            AD returnValue = null;

            if (address != null)
            {
                returnValue = new AD();

                if (address.AddressAbsentIndicator.HasValue)
                {
                    if (address.AddressAbsentIndicator.Value == AddressAbsentIndicator.NoFixedAddressIndicator)
                    {
                        returnValue.nullFlavor = NullFlavor.NA;
                    }
                    else if (address.AddressAbsentIndicator.Value == AddressAbsentIndicator.NotIndicated)
                    {
                        returnValue.nullFlavor = NullFlavor.NI;
                    }
                    else if (address.AddressAbsentIndicator.Value == AddressAbsentIndicator.NotKnown)
                    {
                        returnValue.nullFlavor = NullFlavor.UNK;
                    }
                    else if (address.AddressAbsentIndicator.Value == AddressAbsentIndicator.Masked)
                    {
                        returnValue.nullFlavor = NullFlavor.MSK;
                    }
                    else
                    {
                        returnValue.nullFlavor = NullFlavor.UNK;
                    }

                    returnValue.nullFlavorSpecified = true;

                    // Add support for address rows with Nullflavor
                    if (address.AustralianAddress != null)
                    {
                        FillAustralianAddress(returnValue, address.AustralianAddress);
                    }
                    else if (address.InternationalAddress != null)
                    {
                        FillInternationalAddress(returnValue, address.InternationalAddress);
                    }

                    return returnValue;
                }

                // Use
                if (address.AddressPurpose != AddressPurpose.NotStatedUnknownInadequatelyDescribed)
                {
                    returnValue.use = new[]
                    {
                        (PostalAddressUse) Enum.Parse(typeof(PostalAddressUse),
                            address.AddressPurpose.GetAttributeValue<NameAttribute, string>(a => a.Code))
                    };
                }

                if (address.AustralianAddress != null)
                {
                    FillAustralianAddress(returnValue, address.AustralianAddress);
                }
                else if (address.InternationalAddress != null)
                {
                    FillInternationalAddress(returnValue, address.InternationalAddress);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Takes in an CDA address object and an Australian address, and populates the CDA address object
        /// from the Australian address
        /// </summary>
        /// <param name="address">CDA Address</param>
        /// <param name="ausAddress">Australian Address</param>
        private static void FillAustralianAddress(AD address, AustralianAddress ausAddress)
        {
            var addressLine = String.Empty;

            // Unstructured Australian address line
            if (ausAddress.UnstructuredAddressLines != null)
            {
                address.streetAddressLine = new adxpstreetAddressLine[ausAddress.UnstructuredAddressLines.Count];

                for (var x = 0; x < ausAddress.UnstructuredAddressLines.Count; x++)
                {
                    address.streetAddressLine[x] = new adxpstreetAddressLine
                    { Text = new[] { ausAddress.UnstructuredAddressLines[x] } };
                }
            }

            // Unit type
            if (ausAddress.UnitType != UnitType.Undefined)
            {
                address.unitType = new[]
                {
                    new adxpunitType
                    {
                        Text =
                            new[]
                            {
                                ausAddress.UnitType.GetAttributeValue
                                    <NameAttribute, string>(x => x.Code)
                            }
                    }
                };
            }


            // Unit number
            if (!string.IsNullOrEmpty(ausAddress.UnitNumber))
            {
                address.unitID = new[] { new adxpunitID { Text = new[] { ausAddress.UnitNumber } } };
            }

            // Street number
            if (ausAddress.StreetNumber.HasValue)
            {
                address.houseNumber = new[] { new adxphouseNumber { Text = new[] { ausAddress.StreetNumber.ToString() } } };
            }

            // Street name
            if (!string.IsNullOrEmpty(ausAddress.StreetName))
            {
                address.streetName = new[] { new adxpstreetName { Text = new[] { ausAddress.StreetName } } };
            }

            // Street type
            if (ausAddress.StreetType != StreetType.Undefined)
            {
                address.streetNameType = new[]
                {
                    new adxpstreetNameType
                    {
                        Text =
                            new[]
                            {
                                ausAddress.StreetType.GetAttributeValue
                                    <NameAttribute, string>(x => x.Code)
                            }
                    }
                };

            }

            // Additional locator elements
            var adds = new List<adxpadditionalLocator>();

            if (!string.IsNullOrEmpty(ausAddress.AddressSiteName))
                adds.Add(new adxpadditionalLocator { Text = new[] { ausAddress.AddressSiteName } });

            if (ausAddress.LevelType != AustralianAddressLevelType.Undefined)
                adds.Add(new adxpadditionalLocator
                { Text = new[] { ausAddress.LevelType.GetAttributeValue<NameAttribute, string>(x => x.Code) } });

            if (!string.IsNullOrEmpty(ausAddress.LevelNumber))
                adds.Add(new adxpadditionalLocator { Text = new[] { ausAddress.LevelNumber } });

            if (!string.IsNullOrEmpty(ausAddress.LotNumber))
                adds.Add(new adxpadditionalLocator { Text = new[] { ausAddress.LotNumber } });

            if (ausAddress.DeliveryPointId.HasValue)
                adds.Add(new adxpadditionalLocator { Text = new[] { ausAddress.DeliveryPointId.ToString() } });

            if (adds.Any())
            {
                address.additionalLocator = adds.ToArray();
            }

            // Australian postal delivery type and postal delivery number
            var deliveryLines = new List<adxpdeliveryAddressLine>();

            if (ausAddress.PostalDeliveryType != PostalDeliveryType.Undefined)
            {
                if (ausAddress.PostalDeliveryType != PostalDeliveryType.Undefined)
                    deliveryLines.Add(new adxpdeliveryAddressLine
                    {
                        Text =
                            new[]
                            {
                                ausAddress.PostalDeliveryType.GetAttributeValue<NameAttribute, string>
                                    (x => x.Code),
                                (String.IsNullOrEmpty(ausAddress.PostalDeliveryNumber)
                                    ? String.Empty
                                    : ausAddress.PostalDeliveryNumber)
                            }
                    });
            }

            if (deliveryLines.Any())
                address.deliveryAddressLine = deliveryLines.ToArray();

            // Suburb + town + locality
            if (!string.IsNullOrEmpty(ausAddress.SuburbTownLocality))
                address.city = new[] { new adxpcity { Text = new[] { ausAddress.SuburbTownLocality } } };

            // State
            if (ausAddress.State != AustralianState.Undefined)
            {
                address.state = new[] { new adxpstate { Text = new[] { ausAddress.State.ToString() } } };
            }

            // Postcode
            if (!string.IsNullOrEmpty(ausAddress.PostCode))
            {
                address.postalCode = new[] { new adxppostalCode { Text = new[] { ausAddress.PostCode } } };
            }

            //Country
            address.country = new[] { new adxpcountry { Text = new[] { "Australia" } } };

            if (address.streetAddressLine == null && !String.IsNullOrEmpty(addressLine))
            {
                address.streetAddressLine = new adxpstreetAddressLine[1];

                address.streetAddressLine[0] = new adxpstreetAddressLine { Text = new[] { addressLine } };
            }
        }

        /// <summary>
        /// Takes in an CDA addres object and an International address, and populates the CDA address object
        /// from the International address
        /// </summary>
        /// <param name="address">CDA Address</param>
        /// <param name="intAddress">International Address</param>
        private static void FillInternationalAddress(AD address, InternationalAddress intAddress)
        {
            if (intAddress.AddressLine != null)
            {
                address.streetAddressLine = new adxpstreetAddressLine[intAddress.AddressLine.Count];
                for (var x = 0; x < intAddress.AddressLine.Count; x++)
                {
                    address.streetAddressLine[x] = new adxpstreetAddressLine { Text = new[] { intAddress.AddressLine[x] } };
                }
            }

            if (!string.IsNullOrEmpty(intAddress.StateProvince))
                address.state = new[] { new adxpstate { Text = new[] { intAddress.StateProvince } } };

            if (!string.IsNullOrEmpty(intAddress.PostCode))
                address.postalCode = new[] { new adxppostalCode { Text = new[] { intAddress.PostCode } } };

            if (intAddress.Country != Country.Undefined)
            {
                address.country = new[]
                {
                    new adxpcountry
                    {
                        Text =
                            new[]
                            {
                                intAddress.Country.GetAttributeValue
                                    <NameAttribute, string>(a => a.Name)
                            }
                    }
                };
            }

        }

        #endregion

        #region Private Entry - Entry

        private static POCD_MT000040Entry CreateEntryObservation(CD code, ANY any)
        {
            return CreateEntryObservation(code, new List<ANY> { any });
        }

        internal static POCD_MT000040Entry CreateEntryObservation(CD code, List<ANY> anyList)
        {
            var entry = new POCD_MT000040Entry
            {
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (anyList != null)
            {
                entry.observation.value = anyList.ToArray();
            }

            return entry;
        }

        internal static POCD_MT000040Entry CreateEntryObservation(x_ActRelationshipEntry actRelationshipEntry, CD code,
            CE methodCode, CD targetSiteCode,
            DateTime? effectiveTime, List<ANY> anyList, CE ceCode,
            List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (targetSiteCode != null)
            {
                entry.observation.targetSiteCode = new[] { targetSiteCode };
            }

            if (methodCode != null)
            {
                entry.observation.methodCode = new[] { methodCode };
            }

            if (effectiveTime.HasValue)
            {
                entry.observation.effectiveTime = new IVL_TS { value = effectiveTime.Value.ToString(DATE_TIME_FORMAT) };
            }

            if (anyList != null)
            {
                entry.observation.value = anyList.ToArray();
            }

            if (ceCode != null)
            {
                entry.observation.participant = CreateParticipant2Array(ceCode, EntityDeterminer.INSTANCE);
            }

            if (entryRelationshipList != null)
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryObservation(x_ActRelationshipEntry actRelationshipEntry, CD code,
            CE[] methodCode, CD[] targetSiteCode,
            ISO8601DateTime effectiveTime, List<ANY> anyList, CE ceCode,
            List<POCD_MT000040EntryRelationship>
                entryRelationshipList, string text, string statusCode,
            HL7ObservationInterpretationNormality? normality,
            IEnumerable<ReferenceRangeDetails> referenceRangeDetailsList,
            Identifier templateId,
            Identifier id)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (targetSiteCode != null)
            {
                entry.observation.targetSiteCode = targetSiteCode;
            }

            if (methodCode != null)
            {
                entry.observation.methodCode = methodCode;
            }

            if (effectiveTime != null)
            {
                entry.observation.effectiveTime = CreateIntervalTimestamp(effectiveTime, null);
            }

            if (anyList != null)
            {
                entry.observation.value = anyList.ToArray();
            }

            if (ceCode != null)
            {
                entry.observation.participant = CreateParticipant2Array(ceCode, EntityDeterminer.INSTANCE);
            }

            if (entryRelationshipList != null)
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            if (text != null)
                entry.observation.text = CreateEncapsulatedData(text);

            if (statusCode != null)
                entry.observation.statusCode = new CS { code = statusCode };

            if (normality.HasValue)
            {
                entry.observation.interpretationCode = new[] { CreateCodedWithExtensionElement(normality, null) };
            }

            if (referenceRangeDetailsList != null && referenceRangeDetailsList.Any())
            {
                entry.observation.referenceRange = referenceRangeDetailsList.Select(CreateReferenceRange).ToArray();
            }

            if (templateId != null)
            {
                entry.observation.templateId = CreateIdentifierArray(templateId);
            }

            if (templateId != null)
            {
                entry.observation.templateId = CreateIdentifierArray(templateId);
            }

            if (id != null)
            {
                entry.observation.id = CreateIdentifierArray(id);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryObservation(x_ActRelationshipEntry actRelationshipEntry,
            CD code,
            ISO8601DateTime effectiveTimeValue,
            List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {

            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (effectiveTimeValue != null)
            {
                entry.observation.effectiveTime =
                    CreateIntervalTimestamp(null, null, null, null, effectiveTimeValue.ToString(), null);
            }

            if (entryRelationshipList.Any())
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            return entry;
        }


        private static POCD_MT000040Entry CreateEntryObservation(x_ActRelationshipEntry? actRelationshipEntry, CD code,
            IVL_TS time, List<ANY> anyList, CE ceCode,
            List<POCD_MT000040EntryRelationship> entryRelationshipList,
            List<POCD_MT000040Participant2> participant = null)
        {
            var entry = new POCD_MT000040Entry
            {
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        participant = participant != null && participant.Any() ? participant.ToArray() : null
                    },
            };

            if (actRelationshipEntry.HasValue)
            {
                entry.typeCode = actRelationshipEntry.Value;
            }

            if (code != null)
            {
                entry.observation.code = code;
            }

            if (time != null)
            {
                entry.observation.effectiveTime = time;
            }

            if (anyList != null && anyList.Any())
            {
                entry.observation.value = anyList.ToArray();
            }

            if (ceCode != null)
            {
                entry.observation.participant = CreateParticipant2Array(ceCode, EntityDeterminer.INSTANCE);
            }

            if (entryRelationshipList != null)
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            return entry;
        }


        private static POCD_MT000040Entry CreateEntryObservationWithDuration(
            x_ActRelationshipEntry actRelationshipEntry, CD code, CdaInterval duration, List<ANY> anyList, CE ceCode,
            List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            return CreateEntryObservation(actRelationshipEntry, code, duration != null ? duration.Low : null,
                duration != null ? duration.High : null, anyList, ceCode, entryRelationshipList);
        }

        private static POCD_MT000040Entry CreateEntryObservation(x_ActRelationshipEntry actRelationshipEntry, CD code,
            ISO8601DateTime effectiveTimeHigh, ISO8601DateTime effectiveTimeLow,
            List<ANY> anyList, CE ceCode, List<POCD_MT000040EntryRelationship> entryRelationshipList, string templateId = null)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            // For new FHIR Models
            if (templateId != null)
            {
                entry.observation.templateId = CreateIdentifierArray(templateId);
            }

            if (code != null)
            {
                entry.observation.code = code;
            }

            Dictionary<ItemsChoiceType3, QTY> items = new Dictionary<ItemsChoiceType3, QTY>();
            if (effectiveTimeHigh != null || effectiveTimeLow != null)
            {
                if (effectiveTimeHigh != null)
                {
                    items.Add(ItemsChoiceType3.high,
                        new IVXB_TS
                        {
                            //inclusive = false,
                            nullFlavor = NullFlavor.NA,
                            nullFlavorSpecified = false,
                            value = effectiveTimeHigh.ToString()
                        });
                }

                if (effectiveTimeLow != null)
                {
                    items.Add(ItemsChoiceType3.low,
                        new IVXB_TS
                        {
                            //inclusive = false,
                            nullFlavor = NullFlavor.NA,
                            nullFlavorSpecified = false,
                            value = effectiveTimeLow.ToString()
                        });
                }

                if (items.Any())
                {
                    entry.observation.effectiveTime = new IVL_TS();
                    entry.observation.effectiveTime.ItemsElementName = items.Keys.ToArray();
                    entry.observation.effectiveTime.Items = items.Values.ToArray();
                }
            }

            if (anyList != null && anyList.Any())
            {
                entry.observation.value = anyList.ToArray();
            }

            if (ceCode != null)
            {
                entry.observation.participant = CreateParticipant2Array(ceCode, EntityDeterminer.INSTANCE);
            }

            if (entryRelationshipList != null)
            {
                entry.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            return entry;
        }

        /// <summary>
        /// Create Document
        /// </summary>
        /// <param name="document">The document</param>
        /// <param name="documentProvenanceEnum">The DocumentProvenance of the document </param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateDocument(Document document,
            DocumentProvenanceEnum documentProvenanceEnum)
        {
            var entryRelationships = new List<POCD_MT000040EntryRelationship>();

            var entry = new POCD_MT000040Entry();

            if (document != null)
            {

                // Business Document Type
                entry.act = new POCD_MT000040Act
                {
                    id = CreateIdentifierArray(CreateGuid(), null),
                    classCode = x_ActClassDocumentEntryAct.ACT,
                    moodCode = x_DocumentActMood.EVN,
                    effectiveTime = CreateIntervalTimestamp(document.DateTimeAuthored, null),
                    code = CreateCodedWithExtensionElement(
                        documentProvenanceEnum.GetAttributeValue<NameAttribute, string>(x => x.Code),
                        (CodingSystem)Enum.Parse(typeof(CodingSystem),
                            documentProvenanceEnum.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                        documentProvenanceEnum.GetAttributeValue<NameAttribute, string>(x => x.Name),
                        null, null, null)
                };

                // Author
                if (document.Author != null)
                    entry.act.performer = new[] { CreatePerformer(document.Author) };

                // Date Time Health Event Ended
                if (document.DateTimeHealthEventEnded != null)
                {
                    entryRelationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor("103.15510.160.1.3", CodingSystem.NCTIS, "DateTime Health Event Ended",
                            null),
                        null,
                        null,
                        document.DateTimeHealthEventEnded)
                    );
                }

                // Document Link 
                if (document.Link != null)
                {
                    var referance = new POCD_MT000040Reference
                    {
                        seperatableInd = new BL
                        {
                            value = true,
                            valueSpecified = true
                        },
                        typeCode = x_ActRelationshipExternalReference.REFR
                    };

                    referance.externalDocument = new POCD_MT000040ExternalDocument
                    {
                        classCode = ActClassDocument.DOC,
                        moodCode = ActMood.EVN,
                        id = !document.Link.DocumentIdentifier.IsNullOrEmptyWhitespace()
                            ? new[]
                            {
                                CreateIdentifierElement(document.Link.DocumentIdentifier,
                                    document.Link.DocumentIdentifierExtension, null, null, null)
                            }
                            : null,
                        templateId = new[]
                        {
                            !document.Link.TemplateId.HasValue
                                ? null
                                : CreateIdentifierElement
                                (
                                    document.Link.TemplateId.Value.GetAttributeValue<NameAttribute, string>(x =>
                                        x.Identifier),
                                    document.Link.TemplateId.Value.GetAttributeValue<NameAttribute, string>(x =>
                                        x.Version),
                                    null
                                )
                        },
                        code = CreateCodedWithExtensionElement(
                            document.BusinessDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Code),
                            (CodingSystem)Enum.Parse(typeof(CodingSystem),
                                document.BusinessDocumentType.GetAttributeValue<NameAttribute, string>(
                                    x => x.CodeSystem)),
                            document.BusinessDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Name),
                            document.BusinessDocumentType.GetAttributeValue<NameAttribute, string>(x => x.Title),
                            null,
                            null)
                    };

                    entry.act.entryRelationship = entryRelationships.ToArray();
                    entry.act.reference = new[] { referance };

                }
            }

            return entry;
        }

        private static IEnumerable<POCD_MT000040Entry> CreateProcedureEntries(
            List<IMedicalHistoryItem> medicalHistoryItems, ICodableText medicalHistoryItemDescriptor)
        {
            var entryList = new List<POCD_MT000040Entry>();

            if (medicalHistoryItems != null && medicalHistoryItems.Any())
            {
                foreach (var medicalHistory in medicalHistoryItems)
                {
                    var relationships = new List<POCD_MT000040EntryRelationship>();

                    if (!medicalHistory.ItemComment.IsNullOrEmptyWhitespace())
                    {
                        relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN, false,
                            CreateConceptDescriptor("103.16630",
                                CodingSystem.NCTIS,
                                "Medical History Item Comment",
                                null),
                            CreateStructuredText(medicalHistory.ItemComment, null), null));
                    }

                    entryList.Add(CreateEntryActEvent(x_ActRelationshipEntry.COMP, x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        CreateConceptDescriptor(medicalHistoryItemDescriptor),
                        medicalHistory.ItemDescription, null,
                        medicalHistory.DateTimeInterval,
                        null, null, null, relationships));
                }
            }

            return entryList;
        }

        #endregion

        #region Private Entry - Entry Relationship

        #region Entry Relationship - Observation

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? negationInd,
            Boolean? showNegationInd, CD code,
            List<ANY> values, string statusCode, NullFlavor? effectiveTimeNullflavor)
        {
            var entryRelationship = CreateEntryRelationshipObservation(actRelationshipEntryRelationship,
                ActClassObservation.OBS, x_ActMoodDocumentObservation.EVN, null, null, code, null, null, values, null);

            if (statusCode != null)
            {
                entryRelationship.observation.statusCode = new CS { code = statusCode };
            }

            if (effectiveTimeNullflavor.HasValue)
            {
                entryRelationship.observation.effectiveTime = new IVL_TS
                {
                    nullFlavor = effectiveTimeNullflavor.Value,
                    nullFlavorSpecified = true
                };
            }

            if (negationInd.HasValue && showNegationInd.HasValue)
            {
                entryRelationship.observation.negationInd = negationInd.Value;
                entryRelationship.observation.negationIndSpecified = showNegationInd.Value;
            }

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion, CD code,
            List<ANY> values)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN, inversion, null, code, null, null, values, null);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion, CD code,
            List<ANY> values, Boolean showObservationId)
        {
            var entryRelationshipObservation = CreateEntryRelationshipObservation(actRelationshipEntryRelationship,
                ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN,
                inversion, null, code, null, null,
                values, null);

            if (!showObservationId) entryRelationshipObservation.observation.id = null;

            return entryRelationshipObservation;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion, CD code,
            String text, List<ANY> values, List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            var entryRelationshipObservation = CreateEntryRelationshipObservation(actRelationshipEntryRelationship,
                ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN,
                inversion, null, code, CreateEncapsulatedData(text), null,
                values, entryRelationshipList);

            return entryRelationshipObservation;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, CD code, ANY value)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, null, null, code, null, null,
                value, null);
        }

        internal static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, CD code, ANY value, String text)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, null, null, code, text, null,
                value, null);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, CD code, ANY value, ST text)
        {
            var entryRelationshipObservation = CreateEntryRelationshipObservation(actRelationshipEntryRelationship,
                null,
                null, code, null, null, value, null);

            if (text != null) entryRelationshipObservation.observation.text = text;

            return entryRelationshipObservation;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, CD code,
            List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, null, null, code, null, null,
                null, entryRelationshipList);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion,
            ISO8601DateTime effectiveTime, CD code)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN, inversion, effectiveTime, code,
                null, null, null, null);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion,
            ISO8601DateTime effectiveTime, CD code, ICollection<II> id)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN, inversion, effectiveTime, code,
                null, id, null, null);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion, CD code)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN, inversion, null, code, null,
                null, null, null);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, Boolean? inversion,
            ISO8601DateTime effectiveTime, CD code, String text, ICollection<II> id, ANY value,
            List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, ActClassObservation.OBS,
                x_ActMoodDocumentObservation.EVN, inversion, effectiveTime, code,
                CreateEncapsulatedData(text), id, new List<ANY> { value }, entryRelationshipList);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservationMedia(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, ExternalData externalData)
        {
            POCD_MT000040EntryRelationship imageEntryRelationship = null;

            if (externalData != null)
            {
                imageEntryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = actRelationshipEntryRelationship,
                    observationMedia = CreateObservationMedia(externalData)
                };
            }

            return imageEntryRelationship;
        }


        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, ActClassObservation classCode,
            x_ActMoodDocumentObservation moodCode, CD code, ICollection<II> id)
        {
            return CreateEntryRelationshipObservation(actRelationshipEntryRelationship, classCode, moodCode, false,
                null,
                code, null, id, null, null);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, ActClassObservation classCode,
            x_ActMoodDocumentObservation moodCode, Boolean? inversion, ISO8601DateTime effectiveTime, CD code, ED text,
            ICollection<II> id, List<ANY> values, List<POCD_MT000040EntryRelationship> entryRelationshipList,
            II specimenIdentifier = null, CE ceCode = null)
        {
            II[] ident = null;

            if (id == null || id.Count == 0)
            {
                ident = CreateIdentifierArray(CreateGuid(), null);
            }
            else
            {
                ident = id.ToArray();
            }

            var entityRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationship,
                inversionInd = inversion.HasValue && inversion.Value,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = ident,
                        code = code
                    }
            };

            if (text != null)
            {
                entityRelationship.observation.text = text;
            }

            if (values != null && values.Any())
            {
                entityRelationship.observation.value = values.ToArray();
            }

            if (effectiveTime != null)
            {
                entityRelationship.observation.effectiveTime = CreateIntervalTimestamp(effectiveTime, null);
            }

            if (entryRelationshipList != null)
            {
                entityRelationship.observation.entryRelationship = entryRelationshipList.ToArray();
            }

            if (specimenIdentifier != null)
                entityRelationship.observation.specimen =
                    new List<POCD_MT000040Specimen>
                    {
                        new POCD_MT000040Specimen
                        {
                            specimenRole =
                                new POCD_MT000040SpecimenRole
                                {
                                    id = new[]
                                    {
                                        specimenIdentifier
                                    }
                                }
                        }
                    }.ToArray();

            if (ceCode != null)
            {
                entityRelationship.observation.participant = CreateParticipant2Array(ceCode, EntityDeterminer.INSTANCE);
            }

            return entityRelationship;
        }

        #endregion

        #region Entry Relationship - ACT

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipACT(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryAct classCode, x_DocumentActMood moodCode, Boolean? inversion, CD code, ED text,
            II[] identifier)
        {

            var entryRelationship = CreateEntryRelationshipACT(
                actRelationshipEntryRelationshipTypeCode,
                classCode, moodCode, inversion, code, text,
                identifier, null as IVL_TS);

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipACT(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryAct classCode, x_DocumentActMood moodCode, Boolean? inversion, CD code, ED text,
            II[] identifier, IVL_TS effectiveTime)
        {

            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = identifier,
                        code = code,
                        text = text,
                        effectiveTime = effectiveTime
                    },
            };

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipACT(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryAct classCode, x_DocumentActMood moodCode, Boolean? inversion, CD code, ED text,
            II[] identifier, List<POCD_MT000040EntryRelationship> entryRelationships)
        {

            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue ? inversion.Value : false,
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = identifier,
                        code = code,
                        text = text,
                        entryRelationship = entryRelationships.ToArray()
                    }
            };

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipACT(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryAct classCode, x_DocumentActMood moodCode, Boolean? inversion, CD code, ED text,
            II[] identifier, ISO8601DateTime effectiveDateTime)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = identifier,
                        code = code,
                        text = text,
                        effectiveTime = effectiveDateTime != null
                            ? CreateIntervalTimestamp(null, null, null, null, effectiveDateTime.ToString(), null)
                            : null
                    }
            };

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipACT(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryAct classCode, x_DocumentActMood moodCode, Boolean? inversion, CD code, ED text,
            II[] identifier, DateTime? effectiveDateTime)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = identifier,
                        code = code,
                        text = text,
                        effectiveTime = effectiveDateTime.HasValue
                            ? CreateIntervalTimestamp(null, null, null, null,
                                effectiveDateTime.Value.ToString(DATE_TIME_FORMAT), null)
                            : null
                    }
            };

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipACT(x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode, CD code, ST text, DateTime? effectiveTime)
        {

            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        code = code,
                        text = text
                    }
            };

            if (effectiveTime.HasValue)
            {
                entryRelationship.act.effectiveTime = CreateIntervalTimestamp(effectiveTime, null);
            }

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipControlAct(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            ActClass classCode, x_DocumentActMood moodCode, Boolean? inversion, CD code, ED text,
            II[] identifier, ISO8601DateTime effectiveTime)
        {

            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                controlAct = new ControlAct
                {
                    classCode = classCode,
                    moodCode = moodCode,
                    id = identifier,
                    code = code,
                    text = text,
                    effectiveTime = CreateIntervalTimestamp(effectiveTime, null)
                },

                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
            };

            return entryRelationship;
        }

        #endregion

        #region Entry Relationship - Organizer

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiser(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
            CS testStatus, ITestResultGroup testResultGroup, CDADocumentType? cdaDocumentType)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                organizer =
                    new POCD_MT000040Organizer
                    {
                        author = null,
                        classCode = classCode,
                        code = code,
                        component = null,
                        effectiveTime = null,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        informant = null,
                        moodCode = moodCode,
                        nullFlavor = NullFlavor.NA,
                        nullFlavorSpecified = false,
                        statusCode = testStatus
                    },
            };

            var component4List = new List<POCD_MT000040Component4>();

            if (testResultGroup != null && testResultGroup.ResultGroupSpecimenDetail != null)
            {
                component4List.Add(new POCD_MT000040Component4
                {
                    observation = CreateSpecimenDetail(testResultGroup.ResultGroupSpecimenDetail,
                        CreateResultGroupSpecimenDetailCode(cdaDocumentType))
                });
            }

            if (testResultGroup != null && testResultGroup.Results != null && testResultGroup.Results.Any())
            {
                foreach (var testResult in testResultGroup.Results)
                {
                    var component = new POCD_MT000040Component4
                    {
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                id = CreateIdentifierArray(CreateGuid(), null),
                                code = CreateConceptDescriptor(testResult.ResultName)
                            }
                    };

                    //REFERENCE RANGE DETAILS
                    if ((testResult.ResultValueReferenceRangeDetails != null &&
                         testResult.ResultValueReferenceRangeDetails.Any()) || testResult.ResultValue != null ||
                        testResult.NormalStatus.HasValue)
                    {
                        if (testResult.ResultValue != null)
                            component.observation.value =
                                new List<ANY>
                                {
                                    CreateResultValueAny(testResult.ResultValue, null)
                                }.ToArray();

                        if (testResult.NormalStatus.HasValue)
                            component.observation.interpretationCode =
                                new List<CE>
                                {
                                    CreateCodedWithExtensionElement(testResult.NormalStatus, null)
                                }.ToArray();


                        if (testResult.ResultValueReferenceRangeDetails != null &&
                            testResult.ResultValueReferenceRangeDetails.Any())
                        {
                            var referenceRange = new List<POCD_MT000040ReferenceRange>();

                            foreach (var resultValueReferenceRangeDetail in testResult.ResultValueReferenceRangeDetails)
                            {
                                referenceRange.Add(new POCD_MT000040ReferenceRange
                                {
                                    typeCode = ActRelationshipType.REFV,
                                    typeCodeSpecified = true,
                                    observationRange =
                                        new POCD_MT000040ObservationRange
                                        {
                                            classCode = ActClassObservation.OBS,
                                            moodCode = ActMood.EVNCRT,
                                            moodCodeSpecified = true,
                                            code = CreateConceptDescriptor(resultValueReferenceRangeDetail
                                                .ResultValueReferenceRangeMeaning),
                                            value = CreateIntervalPhysicalQuantity(
                                                resultValueReferenceRangeDetail.Range)
                                        },
                                });
                            }

                            component.observation.referenceRange = referenceRange.ToArray();
                        }
                    }

                    var relationships = new List<POCD_MT000040EntryRelationship>();

                    //Create a relationship containing the status of the pathology test
                    if (testResult.ResultStatus != null)
                    {
                        relationships.Add(
                            CreateRelationshipForTestResultStatus(testResult.ResultStatus, cdaDocumentType));
                    }

                    //COMMENTS
                    if (testResult.Comments != null && testResult.Comments.Any())
                    {
                        var codeResultComment = CreateConceptDescriptor("281296001", CodingSystem.SNOMED,
                            "result comments", null, null, null);

                        if (cdaDocumentType.HasValue && cdaDocumentType.Value == CDADocumentType.ServiceReferral)
                        {
                            codeResultComment.displayName = "Result comments";
                        }

                        foreach (var comment in testResult.Comments)
                        {
                            relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                x_ActClassDocumentEntryAct.INFRM,
                                x_DocumentActMood.EVN, false,
                                codeResultComment,
                                CreateStructuredText(comment, null), null));
                        }
                    }

                    //REFERENCE RANGE GUIDANCE
                    if (testResult.ReferenceRangeGuidance != null)
                    {
                        var codeReferenceRangeComments = CreateConceptDescriptor("281298000", CodingSystem.SNOMED,
                            "reference range comments", null, null, null);

                        if (cdaDocumentType.HasValue && cdaDocumentType.Value == CDADocumentType.ServiceReferral)
                        {
                            codeReferenceRangeComments.displayName = "Reference range comments";
                        }

                        relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN, false,
                            codeReferenceRangeComments,
                            CreateStructuredText(testResult.ReferenceRangeGuidance, null), null));
                    }

                    component.observation.entryRelationship = relationships.ToArray();

                    component4List.Add(component);
                }

                entryRelationship.organizer.component = component4List.ToArray();
            }

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiser(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
            StatusCode overallTestStatus, ITestResultGroup testResultGroup, CDADocumentType? cdaDocumentType)
        {
            var testStatus = CreateCodeSystem(overallTestStatus.GetAttributeValue<NameAttribute, String>(x => x.Name),
                null, null, null, null, null);

            return CreateEntryRelationshipOrganiser(actRelationshipEntryRelationshipTypeCode, classCode, moodCode,
                inversion, code, text, testStatus, testResultGroup, cdaDocumentType);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiser(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
            ICollection<IImagingResult> imagingResults, AnatomicalSite anatomicalSite, CS testStatus)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationshipTypeCode,
                inversionInd = true,
                inversionIndSpecified = inversion.HasValue && inversion.Value,
                organizer =
                    new POCD_MT000040Organizer
                    {
                        author = null,
                        classCode = classCode,
                        code = code,
                        component = null,
                        effectiveTime = null,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        informant = null,
                        moodCode = moodCode,
                        nullFlavor = NullFlavor.NA,
                        nullFlavorSpecified = false,
                        statusCode = testStatus
                    },
            };

            //Imaging Results
            if (imagingResults != null && imagingResults.Any())
            {
                var component4List = new List<POCD_MT000040Component4>();

                foreach (var imagingResult in imagingResults)
                {
                    var component4 = new POCD_MT000040Component4
                    {
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                id = CreateIdentifierArray(CreateGuid(), null),
                                code = CreateConceptDescriptor(imagingResult.ResultName)
                            }
                    };

                    //REFERENCE RANGE DETAILS
                    if ((imagingResult.ResultValueReferenceRangeDetails != null &&
                         imagingResult.ResultValueReferenceRangeDetails.Any()) || imagingResult.ResultValue != null)
                    {
                        component4.observation.value =
                            new List<ANY> { CreateResultValueAny(imagingResult.ResultValue, null) }.ToArray();

                        component4.observation.interpretationCode =
                            new List<CE> { CreateCodedWithExtensionElement(imagingResult.NormalStatus, null) }.ToArray();

                        if (imagingResult.ResultValueReferenceRangeDetails != null &&
                            imagingResult.ResultValueReferenceRangeDetails.Any())
                        {
                            var referenceRange = new List<POCD_MT000040ReferenceRange>();

                            foreach (
                                var resultValueReferenceRangeDetail in imagingResult.ResultValueReferenceRangeDetails)
                            {
                                referenceRange.Add(new POCD_MT000040ReferenceRange
                                {
                                    typeCode = ActRelationshipType.REFV,
                                    typeCodeSpecified = true,
                                    observationRange =
                                        new POCD_MT000040ObservationRange
                                        {
                                            classCode = ActClassObservation.OBS,
                                            moodCode = ActMood.EVNCRT,
                                            moodCodeSpecified = true,
                                            code =
                                                CreateConceptDescriptor(
                                                    resultValueReferenceRangeDetail.ResultValueReferenceRangeMeaning),
                                            value =
                                                CreateIntervalPhysicalQuantity(
                                                    resultValueReferenceRangeDetail.Range
                                                //Result ResultValue Reference Range; the data range for the associated meaning
                                                )
                                        },
                                });
                            }

                            component4.observation.referenceRange = referenceRange.ToArray();
                        }
                    }

                    //COMMENTS
                    var relationships = new List<POCD_MT000040EntryRelationship>();

                    if (imagingResult.Comments != null && imagingResult.Comments.Any())
                    {
                        foreach (var comment in imagingResult.Comments)
                        {
                            relationships.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                                x_ActClassDocumentEntryAct.INFRM,
                                x_DocumentActMood.EVN, false,
                                CreateConceptDescriptor("281296001",
                                    CodingSystem.SNOMED,
                                    "result comments",
                                    null,
                                    null,
                                    null),
                                CreateStructuredText(comment, null), null));
                        }
                    }

                    //ANATOMICAL SITE
                    if (anatomicalSite != null)
                    {
                        component4.observation.targetSiteCode = new[]
                        {
                            CreateConceptDescriptorForAnatomicalSite(anatomicalSite)
                        };

                        if (anatomicalSite.Images != null && anatomicalSite.Images.Any())
                        {
                            relationships.AddRange(anatomicalSite.Images.Select(image =>
                                CreateEntryRelationshipObservationMedia(x_ActRelationshipEntryRelationship.REFR,
                                    image)));
                        }
                    }

                    component4.observation.entryRelationship = relationships.ToArray();

                    component4List.Add(component4);
                }

                entryRelationship.organizer.component = component4List.ToArray();
            }

            return entryRelationship;
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiser(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationshipTypeCode,
            x_ActClassDocumentEntryOrganizer classCode, ActMood moodCode, Boolean? inversion, CD code, ED text,
            ICollection<IImagingResult> imagingResults, AnatomicalSite anatomicalSite, StatusCode overallTestStatus)
        {
            var testStatus = CreateCodeSystem(overallTestStatus.GetAttributeValue<NameAttribute, String>(x => x.Name),
                null, null, null, null, null);

            return CreateEntryRelationshipOrganiser(actRelationshipEntryRelationshipTypeCode, classCode, moodCode,
                inversion, code, text, imagingResults, anatomicalSite, testStatus);
        }

        private static POCD_MT000040EntryRelationship CreateEntryRelationshipOrganiserObservation(
            x_ActRelationshipEntryRelationship actRelationshipEntryRelationship, CD code, string statusCode,
            List<POCD_MT000040Component4> components)
        {
            var entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntryRelationship,
                organizer = new POCD_MT000040Organizer
                {
                    classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                    moodCode = ActMood.EVN,
                    code = code ?? null,
                    component = components != null ? components.ToArray() : null,
                    statusCode = new CS { code = statusCode }
                }
            };

            return entryRelationship;
        }

        #endregion

        #region Entry Relationship

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipsForEachImage(
            ICollection<ExternalData> images)
        {
            List<POCD_MT000040EntryRelationship> relationshipList = null;

            if (images != null && images.Any())
            {
                relationshipList = images.Select(image => new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.REFR,
                    observationMedia = CreateObservationMedia(image)
                }).ToList();
            }

            return relationshipList;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForMedication(
            IMedicationItem medication, string commentDisplayName)
        {

            var changeTypeRelationshipList = new List<POCD_MT000040EntryRelationship>
            {
                CreateEntryRelationshipObservation(
                    x_ActRelationshipEntryRelationship.COMP,
                    CreateConceptDescriptor("103.16595", CodingSystem.NCTIS, "Change Status", null),
                    CreateConceptDescriptor(medication.ChangeStatus)),
                medication.ChangeReason == null
                    ? null
                    : CreateEntryRelationshipACT(
                        x_ActRelationshipEntryRelationship.RSON,
                        x_ActClassDocumentEntryAct.INFRM, x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor("103.10177", CodingSystem.NCTIS, "Change or Recommendation Reason",
                            null),
                        CreateStructuredText(medication.ChangeReason), null),
            };


            var relationshipList = new List<POCD_MT000040EntryRelationship>
            {
                String.IsNullOrEmpty(medication.ClinicalIndication)
                    ? null
                    : CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.RSON,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN, false,
                        CreateConceptDescriptor("103.10141",
                            CodingSystem.NCTIS,
                            "Clinical Indication",
                            null),
                        CreateStructuredText(medication.ClinicalIndication, null),
                        null),
                String.IsNullOrEmpty(medication.Comment)
                    ? null
                    : CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN, false,
                        CreateConceptDescriptor("103.16044",
                            CodingSystem.NCTIS,
                            commentDisplayName,
                            null),
                        CreateStructuredText(medication.Comment,
                            null),
                        CreateIdentifierArray(CreateGuid())),
                CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.SPRT,
                    false,
                    CreateConceptDescriptor("103.16593",
                        CodingSystem.NCTIS,
                        "Change Type",
                        null),
                    medication.ChangeDescription,
                    new List<ANY>
                    {
                        CreateConceptDescriptor(medication.ChangeType)
                    },
                    changeTypeRelationshipList)
            };

            return relationshipList;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForFindings(String findings)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (!String.IsNullOrEmpty(findings))
            {
                relationship = CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.REFR,
                    CreateConceptDescriptor("103.16503", CodingSystem.NCTIS, "Findings", null), null,
                    CreateStructuredText(findings, null));
            }

            return relationship;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForEachImagingResultGroup(
            ICollection<IImagingResultGroup> resultGroups)
        {
            List<POCD_MT000040EntryRelationship> relationshipList = null;

            if (resultGroups != null && resultGroups.Any())
            {
                relationshipList = new List<POCD_MT000040EntryRelationship>();

                foreach (var resultGroup in resultGroups)
                {
                    var relationShip = CreateEntryRelationshipOrganiser(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryOrganizer.BATTERY,
                        ActMood.EVN, null,
                        CreateConceptDescriptor(resultGroup.ResultGroupName), null,
                        resultGroup.Results, resultGroup.AnatomicalSite,
                        StatusCode.Completed);
                    relationshipList.Add(relationShip);
                }
            }

            return relationshipList;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultConclusion(String conclusion,
            CDADocumentType? cdaDocumentType)
        {
            POCD_MT000040EntryRelationship relationship = null;

            string desc = "laboratory findings data interpretation";
            if (cdaDocumentType.HasValue && cdaDocumentType.Value == CDADocumentType.ServiceReferral)
                desc = "laboratory data interpretation";

            if (!String.IsNullOrEmpty(conclusion))
            {
                relationship = CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.REFR, false,
                    CreateConceptDescriptor("386344002",
                        CodingSystem.SNOMED,
                        desc,
                        null,
                        null,
                        null),
                    new List<ANY> { CreateStructuredText(conclusion, null) });
            }

            return relationship;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultComment(String comment)
        {
            return CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP, x_ActClassDocumentEntryAct.INFRM,
                x_DocumentActMood.EVN, null,
                CreateConceptDescriptor("103.16468", CodingSystem.NCTIS, "Test Comment", null),
                CreateStructuredText(comment, null), null);
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultDateTime(ISO8601DateTime dateTime)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (dateTime != null)
            {
                relationship = CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP, false,
                    dateTime,
                    CreateConceptDescriptor("103.16605",
                        CodingSystem.NCTIS,
                        "Pathology Test Result DateTime",
                        null));
            }

            return relationship;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestRequestDetails(ITestRequest request)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (request != null)
            {
                relationship = CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.SUBJ,
                    x_ActClassDocumentEntryAct.ACT,
                    x_DocumentActMood.EVN,
                    true,
                    CreateConceptDescriptor("102.16160", CodingSystem.NCTIS, "Test Request Details", null), null,
                    CreateIdentifierArray(request.RequesterOrderIdentifier));

                var entryRelationships = new List<POCD_MT000040EntryRelationship>();

                if (request.TestsRequestedName != null)
                {
                    foreach (var testRequested in request.TestsRequestedName)
                    {
                        entryRelationships.Add(
                            CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP,
                                ActClassObservation.OBS, x_ActMoodDocumentObservation.RQO,
                                false,
                                null,
                                CreateConceptDescriptor("103.16404", CodingSystem.NCTIS, "Test Requested Name", null),
                                null,
                                CreateIdentifierArray(request.LaboratoryTestResultIdentifier),
                                new List<ANY> { CreateConceptDescriptor(testRequested) },
                                null));
                    }
                }

                relationship.act.entryRelationship = entryRelationships.ToArray();
            }

            return relationship;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForEachTestResultGroup(
            IEnumerable<ITestResultGroup> testResultGroups, CDADocumentType? cdaDocumentType)
        {
            List<POCD_MT000040EntryRelationship> relationshipList = null;

            if (testResultGroups != null)
            {
                relationshipList = new List<POCD_MT000040EntryRelationship>();

                //Create Organizer and relationships, these are nested within the observation entry below
                foreach (var testResultGroup in testResultGroups)
                {
                    var entryRelationshipOrganiser =
                        CreateEntryRelationshipOrganiser(x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryOrganizer.BATTERY, ActMood.EVN, false,
                            CreateConceptDescriptor(testResultGroup.ResultGroupName), null,
                            StatusCode.Completed, testResultGroup, cdaDocumentType);

                    relationshipList.Add(entryRelationshipOrganiser);
                }
            }

            return relationshipList;
        }

        private static IEnumerable<POCD_MT000040EntryRelationship> CreateRelationshipForEachSpecimenDetail(
            ICollection<SpecimenDetail> specimenDetails, CDADocumentType? documentType)
        {
            var relationshipList = new List<POCD_MT000040EntryRelationship>();

            if (specimenDetails != null && specimenDetails.Any())
            {
                foreach (var specimenDetail in specimenDetails)
                {
                    relationshipList.Add(new POCD_MT000040EntryRelationship
                    {
                        typeCode = x_ActRelationshipEntryRelationship.SUBJ,
                        observation =
                            CreateSpecimenDetail(specimenDetail, CreateTestSpecimenDetailCode(documentType))
                    });
                }
            }

            return relationshipList;
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultStatus(ICodableText resultStatus,
            CDADocumentType? cdaDocument)
        {
            var code = CreateConceptDescriptor("308552006", CodingSystem.SNOMED, "report status", null, null, null);

            if (cdaDocument.HasValue && cdaDocument.Value == CDADocumentType.ServiceReferral)
            {
                code.displayName = "Report status";
            }

            return CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP, false,
                code, // PW: Removed Constants.SnomedVersion20110531, null),
                new List<ANY>
                {
                    CreateConceptDescriptor(resultStatus)
                },
                true);
        }

        private static POCD_MT000040EntryRelationship CreateRelationshipForTestResultPathologicalDiagnosis(
            ICollection<ICodableText> pathologicalDiagnosis)
        {
            POCD_MT000040EntryRelationship relationship = null;

            if (pathologicalDiagnosis != null && pathologicalDiagnosis.Any())
            {
                var conceptDescriptorList = pathologicalDiagnosis.Select(CreateConceptDescriptor).Cast<ANY>().ToList();

                relationship = CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.REFR, false,
                    CreateConceptDescriptor("88101002",
                        CodingSystem.SNOMED,
                        "pathology diagnosis",
                        null,
                        null,
                        null),
                    conceptDescriptorList);

            }

            return relationship;
        }


        private static POCD_MT000040EntryRelationship CreateEntryRelationshipSubstanceAdministration(
            x_ActRelationshipEntryRelationship actRelationshipEntry,
            ActClass actClass,
            x_DocumentSubstanceMood documentSubstanceMood,
            ISO8601DateTime effectiveTime,
            int? numberOfRepeats
        )
        {
            POCD_MT000040EntryRelationship entryRelationship = null;

            entryRelationship = new POCD_MT000040EntryRelationship
            {
                typeCode = actRelationshipEntry,
                substanceAdministration = new POCD_MT000040SubstanceAdministration
                {
                    classCode = actClass,
                    moodCode = documentSubstanceMood,
                    effectiveTime = effectiveTime != null
                        ? new[] { new SXCM_TS { value = effectiveTime.ToString() } }
                        : null,
                    repeatNumber = new IVL_INT
                    {
                        ItemsElementName = new[] { ItemsChoiceType5.high },
                        Items = numberOfRepeats.HasValue
                            ? new[]
                            {
                                new IVXB_INT {value = numberOfRepeats.Value.ToString(CultureInfo.InvariantCulture)}
                            }
                            : null,
                    },
                    consumable = new POCD_MT000040Consumable
                    {
                        manufacturedProduct = new POCD_MT000040ManufacturedProduct
                        {
                            manufacturedMaterial = new POCD_MT000040Material()
                        },
                    }
                },
            };

            return entryRelationship;
        }

        private static void SpecimenDetailEntryRelationships(SpecimenDetail specimenDetail,
            ref List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            if (specimenDetail != null)
            {
                if (entryRelationshipList == null)
                {
                    return;
                }

                if (specimenDetail.PhysicalDetails != null)
                    foreach (var physicalDetails in specimenDetail.PhysicalDetails)
                    {
                        if (physicalDetails.Image != null)
                        {
                            var imageEntryRelationship = new POCD_MT000040EntryRelationship
                            {
                                typeCode = x_ActRelationshipEntryRelationship.SPRT,
                                observationMedia = CreateObservationMedia(physicalDetails.Image)
                            };
                            entryRelationshipList.Add(imageEntryRelationship);
                        }
                    }

                //Anatomical location image
                if (specimenDetail.AnatomicalSite != null && specimenDetail.AnatomicalSite.Any())
                {
                    foreach (var anatomicalSite in specimenDetail.AnatomicalSite)
                    {
                        if (anatomicalSite.Images != null && anatomicalSite.Images.Any())
                        {
                            foreach (var image in anatomicalSite.Images)
                            {
                                entryRelationshipList.Add
                                (
                                    new POCD_MT000040EntryRelationship
                                    {
                                        typeCode = x_ActRelationshipEntryRelationship.SPRT,
                                        observationMedia = CreateObservationMedia(image)
                                    }
                                );
                            }
                        }
                    }
                }

                if (specimenDetail.SamplingPreconditions != null)
                {
                    var samplingPreconditionsRelationship = new POCD_MT000040EntryRelationship
                    {
                        typeCode = x_ActRelationshipEntryRelationship.COMP,
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode =
                                    x_ActMoodDocumentObservation.EVN,
                                code = CreateConceptDescriptor("103.16171", CodingSystem.NCTIS,
                                    "Sampling Preconditions", null),
                                value = new List<ANY>
                                {
                                    CreateConceptDescriptor(specimenDetail.SamplingPreconditions)
                                }.ToArray()
                            }
                    };

                    entryRelationshipList.Add(samplingPreconditionsRelationship);
                }

                if (!String.IsNullOrEmpty(specimenDetail.CollectionSetting))
                {
                    var collectionSettingRelationship = new POCD_MT000040EntryRelationship
                    {
                        typeCode = x_ActRelationshipEntryRelationship.COMP,
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                code =
                                    CreateConceptDescriptor("103.16529",
                                        CodingSystem.NCTIS,
                                        "Collection Setting",
                                        null),
                                value =
                                    new ANY[]
                                    {
                                        CreateStructuredText(
                                            specimenDetail.CollectionSetting, null)
                                    }
                            }
                    };
                    entryRelationshipList.Add(collectionSettingRelationship);
                }

                if (specimenDetail.ReceivedDateTime != null)
                {
                    var collectionSettingRelationship = new POCD_MT000040EntryRelationship
                    {
                        typeCode = x_ActRelationshipEntryRelationship.COMP,
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                value =
                                    new ANY[]
                                    {
                                        new TS
                                        {
                                            value =
                                                specimenDetail.ReceivedDateTime
                                                    .ToString()
                                        }
                                    },
                                code =
                                    CreateConceptDescriptor("103.11014",
                                        CodingSystem.NCTIS,
                                        "DateTime Received",
                                        null)
                            },
                    };
                    entryRelationshipList.Add(collectionSettingRelationship);
                }

                if (specimenDetail.ParentSpecimenIdentifier != null)
                {
                    var specimenIdentifierRelationship = new POCD_MT000040EntryRelationship
                    {
                        typeCode = x_ActRelationshipEntryRelationship.COMP,
                        observation =
                            new POCD_MT000040Observation
                            {
                                classCode = ActClassObservation.OBS,
                                moodCode = x_ActMoodDocumentObservation.EVN,
                                code =
                                    CreateConceptDescriptor("103.16187",
                                        CodingSystem.NCTIS,
                                        "Parent Specimen Identifier",
                                        null),
                                specimen =
                                    new List<POCD_MT000040Specimen>
                                    {
                                        new POCD_MT000040Specimen
                                        {
                                            specimenRole =
                                                new POCD_MT000040SpecimenRole
                                                {
                                                    id = new[]
                                                    {
                                                        CreateIdentifierElement(specimenDetail.ParentSpecimenIdentifier)
                                                    }
                                                }
                                        }
                                    }.ToArray()
                            },
                    };

                    entryRelationshipList.Add(specimenIdentifierRelationship);
                }
            }
        }

        #endregion

        #endregion

        #region Private Entry -  Act

        private static POCD_MT000040Act CreateActEvent(x_ActClassDocumentEntryAct classCode, x_DocumentActMood moodCode,
            CD code)
        {
            return new POCD_MT000040Act
            {
                classCode = classCode,
                moodCode = moodCode,
                id = CreateIdentifierArray(CreateGuid(), null),
                code = code
            };
        }

        #endregion

        #region Private Entry - Entry Act

        private static POCD_MT000040Entry CreateEntryActEvent(x_ActRelationshipEntry actRelationshipEntry,
            x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode, CD code, String actText,
            String effectiveTimeValue, CdaInterval interval,
            POCD_MT000040Performer2[] performer,
            POCD_MT000040Participant2[] participant,
            POCD_MT000040Author[] author,
            List<POCD_MT000040EntryRelationship> relationshipList)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.act.code = code;
            }

            if (!String.IsNullOrEmpty(actText)) //Needs to be ST overriding ED
            {
                entry.act.text = CreateStructuredText(actText, null);
            }

            if (!String.IsNullOrEmpty(effectiveTimeValue))
            {
                entry.act.effectiveTime = new IVL_TS { value = effectiveTimeValue };
            }

            if (interval != null)
            {
                entry.act.effectiveTime = CreateIntervalTimestamp(interval);
            }

            if (performer != null)
            {
                entry.act.performer = performer;
            }

            if (participant != null)
            {
                entry.act.participant = participant;
            }

            if (author != null)
            {
                entry.act.author = author;
            }

            if (relationshipList != null)
            {
                entry.act.entryRelationship = relationshipList.ToArray();
            }

            return (entry);
        }


        private static POCD_MT000040Entry CreateEntryActEvent(x_ActRelationshipEntry actRelationshipEntry,
            x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode,
            CD code,
            String actText,
            String effectiveTimeValue,
            String effectiveTimeHigh,
            String effectiveTimeLow,
            POCD_MT000040Performer2[] performer,
            POCD_MT000040Participant2[] participant,
            POCD_MT000040Author[] author,
            List<POCD_MT000040EntryRelationship> relationshipList,
            string id = null)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                act =
                    new POCD_MT000040Act
                    {
                        classCode = classCode,
                        moodCode = moodCode,
                        id = CreateIdentifierArray(id ?? CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.act.code = code;
            }

            if (!String.IsNullOrEmpty(actText)) //Needs to be ST overriding ED
            {
                entry.act.text = CreateStructuredText(actText, null);
            }

            if (!String.IsNullOrEmpty(effectiveTimeValue))
            {
                entry.act.effectiveTime = new IVL_TS { value = effectiveTimeValue };
            }

            if (!String.IsNullOrEmpty(effectiveTimeLow) || !String.IsNullOrEmpty(effectiveTimeHigh))
            {
                entry.act.effectiveTime =
                    CreateIntervalTimestamp(effectiveTimeLow, effectiveTimeHigh, null, null, null, null);
            }

            if (performer != null)
            {
                entry.act.performer = performer;
            }

            if (participant != null)
            {
                entry.act.participant = participant;
            }

            if (author != null)
            {
                entry.act.author = author;
            }

            if (relationshipList != null)
            {
                entry.act.entryRelationship = relationshipList.ToArray();
            }

            return (entry);
        }


        private static POCD_MT000040Entry CreateEntryActEvent(x_ActRelationshipEntry actRelationshipEntry,
            x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode, CD code,
            POCD_MT000040Participant2[] participant,
            List<POCD_MT000040EntryRelationship> relationshipList,
            DateTime? effectiveTime)
        {
            return CreateEntryActEvent(actRelationshipEntry, classCode, moodCode, code, null, null, null,
                effectiveTime.HasValue ? effectiveTime.Value.ToString() : null, null, participant,
                null, relationshipList);
        }


        private static POCD_MT000040Entry CreateEntryActEvent(x_ActRelationshipEntry actRelationshipEntry,
            x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode, CD code,
            POCD_MT000040Participant2[] participant,
            List<POCD_MT000040EntryRelationship> relationshipList,
            string id = null)
        {
            return CreateEntryActEvent(actRelationshipEntry, classCode, moodCode, code, null, null, null, null, null,
                participant, null, relationshipList, id);

        }

        private static POCD_MT000040Entry CreateEntryActEvent(x_ActRelationshipEntry actRelationshipEntry,
            x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode, CD code,
            POCD_MT000040Participant2[] participant)
        {
            return CreateEntryActEvent(actRelationshipEntry, classCode, moodCode, code, null, null, null, null, null,
                participant, null, null);

        }

        private static POCD_MT000040Entry CreateEntryActEvent(x_ActRelationshipEntry actRelationshipEntry,
            x_ActClassDocumentEntryAct classCode,
            x_DocumentActMood moodCode, CD code,
            String effectiveTimeValue)
        {
            return CreateEntryActEvent(actRelationshipEntry, classCode, moodCode, code, null, effectiveTimeValue, null,
                null, null, null, null, null);

        }

        private static POCD_MT000040Entry CreateEntryActEvent(CD code, ED ecapsulatedData)
        {
            return CreateEntryActEvent(code, ecapsulatedData, null, x_DocumentActMood.EVN);
        }

        private static POCD_MT000040Entry CreateEntryActEvent(CD code, ST text)
        {
            var entry = new POCD_MT000040Entry
            {
                act =
                    new POCD_MT000040Act
                    {
                        classCode = x_ActClassDocumentEntryAct.ACT,
                        moodCode = x_DocumentActMood.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.act.code = code;
            }

            if (text != null)
            {
                entry.act.text = text;
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryLink(x_DocumentActMood documentActMood, CD code, Link documentLink)
        {
            var entry = new POCD_MT000040Entry
            {
                act = new POCD_MT000040Act
                {
                    classCode = x_ActClassDocumentEntryAct.ACT,
                    moodCode = documentActMood,
                    id = CreateIdentifierArray(CreateGuid(), null),
                    code = code,
                    text = new ED
                    {
                        reference = new TEL
                        {
                            value = string.Format("#{0}", documentLink.id),
                        }
                    },
                    reference = new[]
                    {
                        new POCD_MT000040Reference
                        {
                            typeCode = x_ActRelationshipExternalReference.REFR,
                            seperatableInd = new BL {value = true, valueSpecified = true},
                            externalDocument = new POCD_MT000040ExternalDocument
                            {
                                classCode = ActClassDocument.DOC,
                                moodCode = ActMood.EVN,
                                id = !documentLink.DocumentIdentifier.IsNullOrEmptyWhitespace()
                                    ? new[]
                                    {
                                        CreateIdentifierElement(documentLink.DocumentIdentifier,
                                            documentLink.DocumentIdentifierExtension, null, null, null)
                                    }
                                    : null,
                                templateId = new[]
                                {
                                    !documentLink.TemplateId.HasValue
                                        ? null
                                        : CreateIdentifierElement
                                        (
                                            documentLink.TemplateId.Value.GetAttributeValue<NameAttribute, string>(x =>
                                                x.TemplateIdentifier),
                                            documentLink.TemplateId.Value.GetAttributeValue<NameAttribute, string>(x =>
                                                x.Version),
                                            null
                                        )
                                }
                            }
                        },
                        new POCD_MT000040Reference
                        {
                            typeCode = x_ActRelationshipExternalReference.REFR,
                            seperatableInd = new BL {value = true, valueSpecified = true},
                            externalAct = new POCD_MT000040ExternalAct
                            {
                                id = CreateIdentifierArray(documentLink.RepositoryIdentifier, null),
                                classCode = ActClassRoot.ACT,
                                code = CreateConceptDescriptor("10", CodingSystem.PCEHRAssignedIdentifierRepository,
                                    CodingSystem.PCEHRAssignedIdentifierRepository
                                        .GetAttributeValue<NameAttribute, string>(x => x.Title), null)
                            }
                        }
                    },

                }
            };
            return entry;
        }


        private static POCD_MT000040Entry CreateEntryActEventInterval(CD code, ED ecapsulatedData,
            List<POCD_MT000040Performer2> addresseePerformers, CdaInterval duration, x_DocumentActMood actMoodCode)
        {
            POCD_MT000040Performer2[] performers = null;

            if (addresseePerformers != null && addresseePerformers.Any())
            {
                performers = addresseePerformers.ToArray();
            }

            var entry = new POCD_MT000040Entry
            {
                act =
                    new POCD_MT000040Act
                    {
                        classCode = x_ActClassDocumentEntryAct.ACT,
                        moodCode = actMoodCode,
                        performer = performers,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.act.code = code;
            }

            if (ecapsulatedData != null)
            {
                entry.act.text = ecapsulatedData;
            }

            if (duration != null)
            {
                entry.act.effectiveTime = CreateIntervalTimestamp(duration);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryActRelatedDocument(RelatedDocument relatedDocument,
            ICodableText studyIdentifier)
        {
            IList<POCD_MT000040Reference> referenceList = new List<POCD_MT000040Reference>();
            IList<POCD_MT000040EntryRelationship> entryRlationshipList = new List<POCD_MT000040EntryRelationship>();
            II reportIdentifier = null;

            // Document Provenance
            if (relatedDocument.DocumentDetails != null)
            {
                // Report Name
                if (relatedDocument.DocumentDetails.ReportDescription != null)
                    entryRlationshipList.Add(CreateEntryRelationshipACT(
                        x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(CreateCodableText(AdvanceCareInformationSections.DocumentTitle)),
                        CreateStructuredText(relatedDocument.DocumentDetails.ReportDescription, null), null));

                // Report Identifier
                if (relatedDocument.DocumentDetails.ReportIdentifier != null)
                    reportIdentifier = CreateIdentifierElement(relatedDocument.DocumentDetails.ReportIdentifier);

                // Report Status
                if (relatedDocument.DocumentDetails.ReportStatus != null)
                    entryRlationshipList.Add(
                        CreateEntryRelationshipObservation(
                            x_ActRelationshipEntryRelationship.COMP,
                            CreateConceptDescriptor(CreateCodableText(AdvanceCareInformationSections.DocumentStatus)),
                            CreateConceptDescriptor(relatedDocument.DocumentDetails.ReportStatus)
                        ));
            }

            if (relatedDocument.ExaminationResultRepresentation != null)
                referenceList.Add(new POCD_MT000040Reference
                {
                    typeCode = x_ActRelationshipExternalReference.XCRPT,
                    seperatableInd = CreateBoolean(true, true),
                    externalDocument = new POCD_MT000040ExternalDocument
                    {
                        classCode = ActClassDocument.DOC,
                        moodCode = ActMood.EVN,
                        id = new[] { reportIdentifier },
                        code = CreateConceptDescriptor(studyIdentifier),
                        text = relatedDocument.ExaminationResultRepresentation != null
                            ? CreateEncapsulatedData(relatedDocument.ExaminationResultRepresentation)
                            : null
                    }
                });

            var entry = new POCD_MT000040Entry
            {
                act =
                    new POCD_MT000040Act
                    {
                        classCode = x_ActClassDocumentEntryAct.ACT,
                        moodCode = x_DocumentActMood.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        code = CreateConceptDescriptor(
                            CreateCodableText(AdvanceCareInformationSections.RelatedDocument)),
                        reference = referenceList.Any() ? referenceList.ToArray() : null,
                        entryRelationship = entryRlationshipList.Any() ? entryRlationshipList.ToArray() : null,
                        effectiveTime =
                            relatedDocument.DocumentDetails != null &&
                            relatedDocument.DocumentDetails.ReportDate != null
                                ? CreateIntervalTimestamp(relatedDocument.DocumentDetails.ReportDate.ToString(), null,
                                    null, null, null, null)
                                : null
                    }
            };

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryActEvent(CD code, ED ecapsulatedData,
            List<POCD_MT000040Performer2> addresseePerformers,
            x_DocumentActMood actMoodCode)
        {
            POCD_MT000040Performer2[] performers = null;

            if (addresseePerformers != null && addresseePerformers.Any())
            {
                performers = addresseePerformers.ToArray();
            }

            var entry = new POCD_MT000040Entry
            {
                act =
                    new POCD_MT000040Act
                    {
                        classCode = x_ActClassDocumentEntryAct.ACT,
                        moodCode = actMoodCode,
                        performer = performers,
                        id = CreateIdentifierArray(CreateGuid(), null)
                    }
            };

            if (code != null)
            {
                entry.act.code = code;
            }

            if (ecapsulatedData != null)
            {
                entry.act.text = ecapsulatedData;
            }

            return entry;
        }

        #endregion

        #region Private Entry - Identifier

        private static II CreateIdentifierElement(InstanceIdentifier identifier)
        {
            var ii = new II();

            ii.root = identifier.Root;

            if (!string.IsNullOrEmpty(identifier.Extension))
                ii.extension = identifier.Extension;

            if (!string.IsNullOrEmpty(identifier.AssigningAuthorityName))
                ii.assigningAuthorityName = identifier.AssigningAuthorityName;

            return ii;
        }

        private static II[] CreateIdentifierArray(InstanceIdentifier identifier)
        {
            if (identifier == null) return null;
            return new II[] { CreateIdentifierElement(identifier) };
        }

        private static II[] CreateIdentifierArray(string root, NullFlavor? nullFlavor)
        {
            var ii = new List<II> { CreateIdentifierElement(root, nullFlavor) };
            return ii.ToArray();
        }

        private static II CreateIdentifierElement(string root, NullFlavor? nullFlavor)
        {
            var ii = new II();

            if (!String.IsNullOrEmpty(root))
            {
                ii.root = root;
            }

            if (nullFlavor.HasValue)
            {
                ii.nullFlavor = nullFlavor.Value;
                ii.nullFlavorSpecified = true;
            }
            else
            {
                ii.nullFlavor = NullFlavor.NA;
                ii.nullFlavorSpecified = false;
            }

            return ii;
        }

        private static II[] CreateIdentifierArray(string root, string extension, NullFlavor? nullFlavor)
        {
            var ii = new List<II> { CreateIdentifierElement(root, extension, nullFlavor) };
            return ii.ToArray();
        }

        private static II CreateIdentifierElement(string root, string extension, NullFlavor? nullFlavor)
        {
            var ii = new II();

            if (!String.IsNullOrEmpty(root))
            {
                ii.root = root;
            }

            if (!String.IsNullOrEmpty(extension))
            {
                ii.extension = extension;
            }

            if (nullFlavor.HasValue)
            {
                ii.nullFlavor = nullFlavor.Value;
                ii.nullFlavorSpecified = true;
            }
            else
            {
                ii.nullFlavor = NullFlavor.NA;
                ii.nullFlavorSpecified = false;
            }

            return ii;
        }

        private static II[] CreateIdentifierArray(string root, string extension, bool? displayable,
            string authorityName, NullFlavor? nullFlavor)
        {
            var ii = new List<II> { CreateIdentifierElement(root, extension, displayable, authorityName, nullFlavor) };
            return ii.ToArray();
        }

        private static II CreateIdentifierElement(string root, string extension, bool? displayable,
            string authorityName, NullFlavor? nullFlavor)
        {
            var ii = new II();

            if (!root.IsNullOrEmptyWhitespace())
            {
                ii.root = root;
            }

            if (!extension.IsNullOrEmptyWhitespace())
            {
                ii.extension = extension;
            }

            if (!authorityName.IsNullOrEmptyWhitespace())
            {
                ii.assigningAuthorityName = authorityName;
            }

            if (displayable.HasValue)
            {
                ii.displayable = displayable.Value;
                ii.displayableSpecified = displayable.Value;
            }

            if (nullFlavor.HasValue)
            {
                ii.nullFlavor = nullFlavor.Value;
                ii.nullFlavorSpecified = true;
            }
            else
            {
                ii.nullFlavor = NullFlavor.NA;
                ii.nullFlavorSpecified = false;
            }

            return ii;
        }

        private static II[] CreateIdentifierArray(UniqueId identifier)
        {
            II[] identifierArray = null;

            if (identifier != null)
            {
                var ii = new List<II> { CreateIdentifierElement(identifier) };

                identifierArray = ii.ToArray();
            }

            return identifierArray;
        }

        private static II[] CreateIdentifierArray(String identifier)
        {
            II[] identifierArray = null;

            if (identifier != null)
            {
                var ii = new List<II> { CreateIdentifierElement(identifier) };

                identifierArray = ii.ToArray();
            }

            return identifierArray;
        }

        internal static II CreateIdentifierElement(UniqueId identifier)
        {
            II ident = null;

            if (identifier != null)
            {
                var ii = new II { root = identifier.ToString().Replace("urn:uuid:", "") };

                ident = ii;
            }

            return ident;
        }

        private static II CreateIdentifierElement(String identifier)
        {
            II ident = null;

            if (identifier != null)
            {
                var ii = new II { root = identifier };

                ident = ii;
            }

            return ident;
        }

        private static II[] CreateIdentifierArray(Identifier identifier)
        {
            II[] identifierArray = null;

            if (identifier != null)
            {
                var ii = new List<II> { CreateIdentifierElement(identifier) };

                identifierArray = ii.ToArray();
            }

            return identifierArray;
        }

        private static II CreateIdentifierElement(Identifier identifier)
        {
            II ident = null;

            if (identifier != null)
            {
                var ii = new II();

                if (!identifier.Root.IsNullOrEmptyWhitespace())
                {
                    ii.root = identifier.Root;
                }

                if (!identifier.Extension.IsNullOrEmptyWhitespace())
                {
                    ii.extension = identifier.Extension;
                }

                if (!identifier.AssigningAuthorityName.IsNullOrEmptyWhitespace())
                {
                    ii.assigningAuthorityName = identifier.AssigningAuthorityName;
                }

                ident = ii;
            }

            return ident;
        }

        #endregion

        #region Private Entry - Coded Entries

        #region Coded Entries -  ICodableText Functions

        /// <summary>
        /// Creates a CodableText from an Enum
        /// </summary>
        public static ICodableText CreateCodableText<T>(T defaultValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type which can be mapped to a CodingSystem");
            }

            var enumeration = defaultValue as Enum;

            return new CodableText
            {
                DisplayName = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Name),
                Code = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodeSystemCode =
                    ((CodingSystem)Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodeSystemName =
                    ((CodingSystem)Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Name),
                CodeSystemVersion = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Version),
                OriginalText = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Title)
            };
        }

        private static ICodableText CreateCodableText(String code, CodingSystem? codeSystem, String displayName,
            String originalText)
        {
            if (codeSystem.HasValue)
            {
                return new CodableText
                {
                    DisplayName = displayName,
                    Code = code,
                    CodeSystemCode = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Code),
                    CodeSystemName = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Name),
                    CodeSystemVersion = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version),
                    OriginalText = originalText
                };
            }

            return new CodableText
            {
                OriginalText = originalText
            };
        }

        private static ICodableText CreateCodableText(String code, String codeSystemCode, String codeSystemName,
            String codeSystemVersion, String displayName, String originalText)
        {
            return new CodableText
            {
                DisplayName = displayName,
                Code = code,
                CodeSystemCode = codeSystemCode,
                CodeSystemName = codeSystemName,
                CodeSystemVersion = codeSystemVersion,
                OriginalText = originalText
            };
        }

        private static ICodableText CreateTestSpecimenDetailCode(CDADocumentType? documentType)
        {
            var code = "102.16156.136.2.1";

            if (documentType.HasValue)
            {
                switch (documentType.Value)
                {
                    case CDADocumentType.SpecialistLetter:
                        code = "102.16156.132.1.1";
                        break;
                    case CDADocumentType.ServiceReferral:
                        code = "102.16156.231.1.1";
                        break;
                    case CDADocumentType.PathologyReportWithStructuredContent:
                        code = "102.16156";
                        break;
                }
            }

            return CreateCodableText(code, CodingSystem.NCTIS, "Specimen", null);
        }

        private static ICodableText CreateResultGroupSpecimenDetailCode(CDADocumentType? documentType)
        {
            var code = "102.16156.136.2.2";

            if (documentType.HasValue)
            {
                switch (documentType.Value)
                {
                    case CDADocumentType.ServiceReferral:
                        code = "102.16156.231.1.2";
                        break;
                    case CDADocumentType.SpecialistLetter:
                        code = "102.16156.132.1.2";
                        break;
                    case CDADocumentType.PathologyReportWithStructuredContent:
                        code = "102.16156.220.2.2";
                        break;

                }
            }

            return CreateCodableText(code, CodingSystem.NCTIS, "Specimen", null);
        }

        #endregion

        #region Coded Entries - Coded With Extension Element

        internal static CE CreateCodedWithExtensionElement(string code, CodingSystem? codingSystem, string displayName,
            string originalText, List<ICodableTranslation> codableTranslations, NullFlavour? nullFlavor,
            List<QualifierCode> qualifierCodes = null)
        {
            if (codingSystem.HasValue)
            {
                return CreateCodedWithExtensionElement(code,
                    codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Version),
                    displayName,
                    originalText,
                    codableTranslations,
                    nullFlavor,
                    qualifierCodes

                );
            }

            return CreateCodedWithExtensionElement(code,
                null,
                null,
                null,
                displayName,
                originalText,
                codableTranslations,
                nullFlavor,
                qualifierCodes
            );
        }

        internal static CE CreateCodedWithExtensionElement(ICodableText code)
        {
            CE ce = null;

            if (code != null)
            {
                ce = CreateCodedWithExtensionElement(code.Code, code.CodeSystemCode, code.CodeSystemName,
                    code.CodeSystemVersion, code.DisplayName, code.OriginalText, code.Translations, code.NullFlavour,
                    code.QualifierCodes);
            }

            return ce;
        }

        private static CE CreateCodedWithExtensionElement(HL7ObservationInterpretationNormality? status,
            NullFlavour? nullflavor)
        {
            CE ce = null;

            if (status.HasValue)
            {
                ce = CreateCodedWithExtensionElement((Enum)status, nullflavor);
            }

            return ce;
        }

        private static CE CreateCodedWithExtensionElement(NullFlavour? nullflavor)
        {
            CE ce = null;

            if (nullflavor.HasValue)
            {
                ce = CreateCodedWithExtensionElement(null, null, null, null, null, nullflavor, null);
            }

            return ce;
        }

        private static CE CreateCodedWithExtensionElement(Enum status, NullFlavour? nullflavor)
        {
            CE ce = null;

            if (status != null)
            {
                ce = CreateCodedWithExtensionElement(status.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    CodingSystem.HL7ObservationInterpretationNormality,
                    status.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    null,
                    null,
                    nullflavor,
                    null);
            }

            return ce;
        }

        internal static CE CreateCodedWithExtensionElement(string code, string codeSystemCode, string codeSystemName,
            string codeSystemVersion, string displayName, string originalText,
            List<ICodableTranslation> codableTranslations, NullFlavour? nullFlavor,
            List<QualifierCode> qualifierCodes = null)
        {
            CE codedWithExtension = null;

            if (!code.IsNullOrEmptyWhitespace() || !codeSystemCode.IsNullOrEmptyWhitespace() ||
                !codeSystemName.IsNullOrEmptyWhitespace() || !codeSystemVersion.IsNullOrEmptyWhitespace() ||
                !displayName.IsNullOrEmptyWhitespace() || !originalText.IsNullOrEmptyWhitespace() ||
                (codableTranslations != null && codableTranslations.Any()) || nullFlavor.HasValue)
            {
                codedWithExtension = new CE
                {
                    code = !code.IsNullOrEmptyWhitespace() ? code : null,
                    codeSystem = !codeSystemCode.IsNullOrEmptyWhitespace() ? codeSystemCode : null,
                    codeSystemName = !codeSystemName.IsNullOrEmptyWhitespace() ? codeSystemName : null,
                    displayName = !displayName.IsNullOrEmptyWhitespace() ? displayName : null
                };

                if (!originalText.IsNullOrEmptyWhitespace())
                {
                    codedWithExtension.originalText = CreateEncapsulatedData(originalText, MediaType.TXT);
                }

                if (!string.IsNullOrEmpty(codeSystemVersion))
                {
                    codedWithExtension.codeSystemVersion = codeSystemVersion;
                }

                if (nullFlavor.HasValue)
                {
                    codedWithExtension.nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                        nullFlavor.GetAttributeValue<NameAttribute, string>(x => x.Code));
                    codedWithExtension.nullFlavorSpecified = true;
                }

                if (codableTranslations != null && codableTranslations.Any())
                {
                    var translations = new List<CD>();

                    codableTranslations.ForEach(translation => translations.Add(CreateConceptDescriptor(translation)));

                    codedWithExtension.translation = translations.ToArray();
                }

                if (qualifierCodes != null)
                {
                    codedWithExtension.qualifier = CreateQualifierCodes(qualifierCodes);
                }
            }

            return codedWithExtension;
        }

        /// <summary>
        /// Create Qualifier Code
        /// </summary>
        /// <param name="qualifierCodes">A list of qualifierCodes</param>
        /// <returns></returns>
        private static CR[] CreateQualifierCodes(List<QualifierCode> qualifierCodes)
        {
            List<CR> qualifers = null;

            if (qualifierCodes != null && qualifierCodes.Any())
            {
                qualifers = new List<CR>();

                foreach (var qualifierCode in qualifierCodes)
                {
                    qualifers.Add(new CR
                    {
                        name = CreateCodedValue(qualifierCode.Name),
                        value = CreateConceptDescriptor(qualifierCode.Value)
                    });
                }
            }

            return qualifers != null ? qualifers.ToArray() : null;
        }


        #endregion

        #region Coded Entries - Code System

        private static CS CreateCodeSystem(string code, string codeSystem, string codeSystemName, string displayName,
            string originalText, NullFlavour? nullFlavor, List<QualifierCode> qualifierCodes = null)
        {
            CS cs = null;

            if (!string.IsNullOrEmpty(code) || !string.IsNullOrEmpty(codeSystem) ||
                !string.IsNullOrEmpty(codeSystemName) || !string.IsNullOrEmpty(displayName) ||
                !string.IsNullOrEmpty(originalText) || nullFlavor.HasValue)
            {
                cs = new CS();

                if (!string.IsNullOrEmpty(code))
                {
                    cs.code = code;
                }

                if (!string.IsNullOrEmpty(codeSystem))
                {
                    cs.codeSystem = codeSystem;
                }

                if (!string.IsNullOrEmpty(codeSystemName))
                {
                    cs.codeSystemName = codeSystemName;
                }

                if (!string.IsNullOrEmpty(displayName))
                {
                    cs.displayName = displayName;
                }

                if (!string.IsNullOrEmpty(originalText))
                {
                    cs.originalText = CreateEncapsulatedData(originalText);
                }

                if (qualifierCodes != null)
                {
                    cs.qualifier = CreateQualifierCodes(qualifierCodes);
                }

            }

            if (nullFlavor.HasValue)
            {
                cs.nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                    nullFlavor.GetAttributeValue<NameAttribute, string>(x => x.Code));
                cs.nullFlavorSpecified = true;
            }

            return cs;
        }

        private static CS CreateCodeSystem(string code)
        {
            CS cs = null;

            if (!string.IsNullOrEmpty(code))
            {
                cs = new CS
                {
                    code = code
                };
            }

            return cs;
        }

        #endregion

        #region Coded Entries - Concept Descriptor

        internal static CD CreateConceptDescriptor(string code, CodingSystem? codingSystem, string displayName,
            string originalText, string codeSystemVersion, NullFlavour? nullFlavor,
            List<QualifierCode> qualifierCodes = null)
        {
            ICodableText codableText = new CodableText();
            codableText.Code = code;

            codableText.CodeSystemCode = codingSystem != null
                ? !String.IsNullOrEmpty(codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Code))
                    ? codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Code)
                    : null
                : null;

            codableText.CodeSystemName = codingSystem != null
                ? !String.IsNullOrEmpty(codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Name))
                    ? codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Name)
                    : null
                : null;

            codableText.CodeSystemVersion = codeSystemVersion;

            codableText.DisplayName = displayName;
            codableText.OriginalText = originalText;
            codableText.NullFlavour = nullFlavor;
            codableText.QualifierCodes = qualifierCodes;

            return CreateConceptDescriptor(codableText);
        }

        internal static CD CreateConceptDescriptor(NullFlavour? nullFlavor)
        {
            ICodableText codableText = new CodableText();
            codableText.NullFlavour = nullFlavor;
            return CreateConceptDescriptor(codableText);
        }

        internal static CD CreateConceptDescriptor(string code, CodingSystem? codingSystem, string displayName,
            string originalText, List<QualifierCode> qualifierCodes = null)
        {
            return CreateConceptDescriptor(code, codingSystem, displayName, originalText, null, null, qualifierCodes);
        }

        private static CD CreateConceptDescriptor(string code, string codeSystem, string codeSystemName,
            string displayName, string originalText, NullFlavour? nullFlavor, List<QualifierCode> qualifierCodes = null)
        {
            ICodableText codableText = new CodableText();
            codableText.Code = code;
            codableText.CodeSystemCode = codeSystem;
            codableText.CodeSystemName = codeSystemName;
            codableText.DisplayName = displayName;
            codableText.OriginalText = originalText;
            codableText.NullFlavour = nullFlavor;
            codableText.QualifierCodes = qualifierCodes;

            return CreateConceptDescriptor(codableText);
        }

        internal static CD CreateConceptDescriptor(ICodableText codableText)
        {
            CD cd = null;

            if (codableText != null)
            {
                if (codableText.NullFlavour.HasValue)
                {
                    return new CD
                    {
                        nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                            codableText.NullFlavour.Value.GetAttributeValue<NameAttribute, string>(x => x.Code)),
                        nullFlavorSpecified = true
                    };
                }

                cd = new CD();

                cd.code = !codableText.Code.IsNullOrEmptyWhitespace() ? codableText.Code : null;
                cd.codeSystem = !codableText.CodeSystemCode.IsNullOrEmptyWhitespace() ? codableText.CodeSystemCode : null;
                cd.codeSystemName = !codableText.CodeSystemName.IsNullOrEmptyWhitespace() ? codableText.CodeSystemName : null;
                cd.codeSystemVersion = !codableText.CodeSystemVersion.IsNullOrEmptyWhitespace() ? codableText.CodeSystemVersion : null;
                cd.displayName = !codableText.DisplayName.IsNullOrEmptyWhitespace() ? codableText.DisplayName : null;

                if (!string.IsNullOrEmpty(codableText.OriginalText))
                {
                    cd.originalText = CreateEncapsulatedData(codableText.OriginalText, MediaType.TXT);
                }

                if (codableText.Translations != null && codableText.Translations.Any())
                {
                    var translations = new List<CD>();
                    codableText.Translations.ForEach(translation => translations.Add(CreateConceptDescriptor(translation)));
                    cd.translation = translations.ToArray();
                }

                if (codableText.QualifierCodes != null)
                {
                    cd.qualifier = CreateQualifierCodes(codableText.QualifierCodes);
                }
            }

            return cd;
        }

        private static CD CreateConceptDescriptor(ICodableTranslation codableTranslation)
        {
            // Add fix here

            CD cd = null;

            if (codableTranslation != null)
            {
                if (codableTranslation.NullFlavour.HasValue)
                {
                    return new CD
                    {
                        nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                            codableTranslation.NullFlavour.Value.GetAttributeValue<NameAttribute, string>(x => x.Code)),
                        nullFlavorSpecified = true
                    };
                }

                cd = new CD();

                if (codableTranslation.HasCodeSystem)
                {
                    cd.code = !codableTranslation.Code.IsNullOrEmptyWhitespace() ? codableTranslation.Code : null;
                    cd.codeSystem = !codableTranslation.CodeSystemCode.IsNullOrEmptyWhitespace() ? codableTranslation.CodeSystemCode : null;
                    cd.codeSystemName = !codableTranslation.CodeSystemName.IsNullOrEmptyWhitespace() ? codableTranslation.CodeSystemName : null;
                    cd.codeSystemVersion = !codableTranslation.CodeSystemVersion.IsNullOrEmptyWhitespace() ? codableTranslation.CodeSystemVersion : null;
                    cd.displayName = !codableTranslation.DisplayName.IsNullOrEmptyWhitespace() ? codableTranslation.DisplayName : null;
                }
                else
                {
                    cd.nullFlavor = NullFlavor.OTH;
                    cd.nullFlavorSpecified = true;
                }
            }

            return cd;
        }

        private static List<CD> CreateConceptDescriptorsForAnatomicalSites(ICollection<AnatomicalSite> anatomicalSites)
        {
            List<CD> componentDescriptionList = null;

            if (anatomicalSites != null && anatomicalSites.Any())
            {
                componentDescriptionList = (from anatomicalSite in anatomicalSites
                                            where anatomicalSite != null
                                            select CreateConceptDescriptorForAnatomicalSite(anatomicalSite)).ToList();
            }

            return componentDescriptionList;
        }

        private static List<CD> CreateConceptDescriptorForAnatomicalSites(
            IEnumerable<IAnatomicalSiteExtended> anatomicalSites, ICodableText name)
        {
            IList<AnatomicalSite> anatomicalsitesList =
                anatomicalSites.Select(anatomicalSite => anatomicalSite as AnatomicalSite).ToList();
            return CreateConceptDescriptorForAnatomicalSites(anatomicalsitesList, name);
        }

        private static List<CD> CreateConceptDescriptorForAnatomicalSites(IList<AnatomicalSite> anatomicalSites,
            ICodableText name)
        {
            List<CD> componentDescriptionList = null;

            if (anatomicalSites != null && anatomicalSites.Any())
            {
                componentDescriptionList = (from anatomicalSite in anatomicalSites
                                            where anatomicalSite != null
                                            select CreateConceptDescriptorForAnatomicalSite(anatomicalSite, name)).ToList();
            }

            return componentDescriptionList;
        }

        private static CD CreateConceptDescriptorForAnatomicalSite(AnatomicalSite anatomicalSite, ICodableText name)
        {
            CD cd = null;

            if (anatomicalSite != null)
            {
                if (anatomicalSite.SpecificLocation != null)
                {
                    if (anatomicalSite.SpecificLocation.NameOfLocation != null)
                    {
                        cd = CreateConceptDescriptor(anatomicalSite.SpecificLocation.NameOfLocation);
                    }

                    if (anatomicalSite.SpecificLocation.Side != null)
                    {
                        if (cd == null) cd = new CD();

                        cd.qualifier = new List<CR>
                        {
                            new CR
                            {
                                name = CreateCodedValue(name),
                                value = CreateConceptDescriptor(anatomicalSite.SpecificLocation.Side)
                            }
                        }.ToArray();
                    }
                }

                if (!string.IsNullOrEmpty(anatomicalSite.Description))
                {
                    if (cd == null) cd = new CD();

                    cd.originalText = CreateEncapsulatedData(anatomicalSite.Description);
                }
            }

            return cd;
        }

        private static CD CreateConceptDescriptorForAnatomicalSite(AnatomicalSite anatomicalSite)
        {
            CD cd = null;

            if (anatomicalSite != null)
            {
                if (anatomicalSite.SpecificLocation != null)
                {
                    if (anatomicalSite.SpecificLocation.NameOfLocation != null)
                    {
                        cd = CreateConceptDescriptor(anatomicalSite.SpecificLocation.NameOfLocation);
                    }

                    var codedValue = CreateCodedValue
                    (
                        "272741003",
                        CodingSystem.SNOMEDCT,
                        "Laterality",
                        null,
                        null,
                        null
                    );

                    if (anatomicalSite.SpecificLocation.Side != null)
                    {
                        if (cd == null) cd = new CD();

                        cd.qualifier = new List<CR>
                        {
                            new CR
                            {
                                name = codedValue,
                                value = CreateConceptDescriptor(anatomicalSite.SpecificLocation.Side)
                            }
                        }.ToArray();
                    }
                }

                if (!string.IsNullOrEmpty(anatomicalSite.Description))
                {
                    if (cd == null)
                        cd = new CD();

                    //                cd.nullFlavor = NullFlavor.NA;
                    //                cd.nullFlavorSpecified = true;
                    //                cd.codeSystem = CodingSystem.AMTV3.GetAttributeValue<NameAttribute, string>(x => x.Code);
                    cd.originalText = CreateEncapsulatedData(anatomicalSite.Description);

                }
            }

            return cd;
        }

        private static POCD_MT000040EntryRelationship CreateAnatomicalRegionRelationship(
            AnatomicalRegion? anatomicalRegion)
        {
            POCD_MT000040EntryRelationship entryRelationship = null;

            if (anatomicalRegion.HasValue)
            {
                entryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.SUBJ,
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid()),
                        code = CreateConceptDescriptor(
                            CreateCodableText(DiagnosticImagingReportSections.AnatomicalRegion)),
                        value = new ANY[] { CreateConceptDescriptor(CreateCodableText(anatomicalRegion.Value)) }
                    }
                };
            }

            return entryRelationship;
        }

        #endregion

        #region Coded Entries - Coded Value

        /// <summary>
        /// Create a CodedValue for the given parameters
        /// </summary>
        /// <param name="code">A string that represents the code</param>
        /// <param name="codingSystem">The CodingSystem for the code</param>
        /// <param name="displayName">The associated display name for the coded text</param>
        /// <param name="originalText">A string for the originalText</param>
        /// <param name="codeSystemVersion">The code system version</param>
        /// <param name="nullFlavor">Whether a null flavour exists</param>
        /// <param name="qualifierCodes">A list of qualifier codes</param>
        /// <returns>A CV coded value object</returns>
        public static CV CreateCodedValue(string code, CodingSystem? codingSystem, string displayName,
            string originalText, string codeSystemVersion, NullFlavour? nullFlavor,
            List<QualifierCode> qualifierCodes = null)
        {
            CV cv = null;

            if (nullFlavor.HasValue)
            {
                return new CV
                {
                    nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                        nullFlavor.GetAttributeValue<NameAttribute, string>(x => x.Code)),
                    nullFlavorSpecified = true
                };
            }

            if (!string.IsNullOrEmpty(code) || codingSystem.HasValue || !string.IsNullOrEmpty(displayName) ||
                !string.IsNullOrEmpty(originalText) || qualifierCodes != null)
            {
                cv = new CV();

                if (!string.IsNullOrEmpty(code))
                {
                    cv.code = code;
                    cv.codeSystem = codingSystem != null
                        ? codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Code)
                        : null;
                    cv.codeSystemName = codingSystem != null
                        ? codingSystem.GetAttributeValue<NameAttribute, string>(x => x.Name)
                        : null;

                    if (!string.IsNullOrEmpty(codeSystemVersion))
                    {
                        cv.codeSystemVersion = codeSystemVersion;
                    }
                }

                if (!string.IsNullOrEmpty(displayName))
                {
                    cv.displayName = displayName;
                }

                if (!string.IsNullOrEmpty(originalText))
                {
                    cv.originalText = CreateEncapsulatedData(originalText, MediaType.TXT);
                }

                if (codingSystem.HasValue == false && string.IsNullOrEmpty(originalText))
                {
                    cv.nullFlavor = NullFlavor.OTH;
                    cv.nullFlavorSpecified = true;
                }

                if (qualifierCodes != null)
                {
                    cv.qualifier = CreateQualifierCodes(qualifierCodes);
                }
            }

            return cv;
        }

        /// <summary>
        /// Create a CodedValue for the given codableText
        /// </summary>
        /// <param name="codableText">A ICodableText item</param>
        /// <returns>A CV coded value object</returns>
        public static CV CreateCodedValue(ICodableText codableText)
        {
            CV cv = null;

            if (codableText != null)
            {

                if (codableText.NullFlavour.HasValue)
                {
                    return new CV
                    {
                        nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                            codableText.NullFlavour.Value.GetAttributeValue<NameAttribute, string>(x => x.Code)),
                        nullFlavorSpecified = true
                    };
                }

                cv = new CV();

                if (codableText.HasCodeSystem)
                {
                    cv.code = codableText.Code;
                    cv.codeSystem = codableText.CodeSystemCode;
                    cv.codeSystemName = codableText.CodeSystemName;
                    var version = codableText.CodeSystemVersion;
                    if (!string.IsNullOrEmpty(version))
                    {
                        cv.codeSystemVersion = version;
                    }
                }

                if (!string.IsNullOrEmpty(codableText.DisplayName))
                {
                    cv.displayName = codableText.DisplayName;
                }

                if (!string.IsNullOrEmpty(codableText.OriginalText))
                {
                    cv.originalText = CreateEncapsulatedData(codableText.OriginalText, MediaType.TXT);
                }

                if (!codableText.HasCodeSystem && string.IsNullOrEmpty(codableText.OriginalText))
                {
                    cv.nullFlavor = NullFlavor.OTH;
                    cv.nullFlavorSpecified = true;
                }

                if (codableText.QualifierCodes != null)
                {
                    cv.qualifier = CreateQualifierCodes(codableText.QualifierCodes);
                }

            }

            return cv;
        }

        #endregion

        # endregion

        #region Private Entry - Any

        private static ANY CreateResultValueAny(ResultValue value, NullFlavor? nullFlavor)
        {
            ANY any = null;

            if (value != null)
            {
                PQ physicalQuantity = null;
                CD conceptDescriptor = null;
                IVL_PQ intervalPhysicalQuantity = null;

                if (value.ValueAsCodableText != null)
                {
                    if (value.ValueAsCodableText != null)
                    {
                        conceptDescriptor = CreateConceptDescriptor(value.ValueAsCodableText);
                    }
                }
                else if (value.TestResultValue != null)
                {
                    physicalQuantity = new PQ { nullFlavor = NullFlavor.NA, nullFlavorSpecified = false };

                    if (value.TestResultValue != null)
                    {
                        physicalQuantity.unit = value.TestResultValue.Units;
                        physicalQuantity.value = !value.TestResultValue.Value.IsNullOrEmptyWhitespace()
                            ? value.TestResultValue.Value
                            : null;
                    }
                }
                else if (value.ValueAsQuantityRange != null)
                {
                    intervalPhysicalQuantity = CreateIntervalPhysicalQuantity(value.ValueAsQuantityRange);
                }

                if (physicalQuantity != null)
                {
                    if (nullFlavor.HasValue)
                    {
                        physicalQuantity.nullFlavor = nullFlavor.Value;
                        physicalQuantity.nullFlavorSpecified = true;
                    }

                    any = physicalQuantity;
                }
                else if (intervalPhysicalQuantity != null)
                {
                    if (nullFlavor.HasValue)
                    {
                        intervalPhysicalQuantity.nullFlavor = nullFlavor.Value;
                        intervalPhysicalQuantity.nullFlavorSpecified = true;
                    }

                    any = intervalPhysicalQuantity;
                }
                else if (conceptDescriptor != null)
                {
                    any = conceptDescriptor;
                }
            }

            return any;
        }

        # endregion

        #region Private Entry - Physical Quantity / Interval Physical Quantity

        internal static PQ CreatePhysicalQuantity(Quantity quantity)
        {
            PQ physicalQuantity = null;

            if (quantity != null)
            {
                if (quantity.Value != null)
                {
                    if (quantity.Value != null || quantity.Units != null)
                    {
                        physicalQuantity = new PQ
                        {
                            nullFlavor = NullFlavor.NA,
                            nullFlavorSpecified = false,
                            unit = quantity.Units,
                            value = quantity.Value
                        };
                    }
                }
            }

            return physicalQuantity;
        }

        private static PQ CreatePhysicalQuantity(String units, String value)
        {
            PQ physicalQuantity = null;

            if (value != null || units != null)
            {
                physicalQuantity = new PQ
                {
                    nullFlavor = NullFlavor.NA,
                    nullFlavorSpecified = false,
                    unit = units,
                    value = value
                };
            }

            return physicalQuantity;
        }

        private static IVL_PQ CreateIntervalPhysicalQuantity(QuantityRange quantityRange)
        {
            IVL_PQ pq = null;

            if (quantityRange != null)
            {
                pq = new IVL_PQ();

                if (quantityRange.Low.HasValue && quantityRange.High.HasValue)
                {
                    pq.ItemsElementName = new ItemsChoiceType[2];
                    pq.Items = new PQ[2];

                    pq.ItemsElementName[0] = ItemsChoiceType.low;
                    pq.Items[0] = new IVXB_PQ
                    {
                        unit = quantityRange.Units,
                        value = quantityRange.Low.HasValue
                            ? quantityRange.Low.Value.ToString(CultureInfo.InvariantCulture)
                            : null
                    };

                    pq.ItemsElementName[1] = ItemsChoiceType.high;
                    pq.Items[1] = new IVXB_PQ
                    {
                        unit = quantityRange.Units,
                        value = quantityRange.High.HasValue
                            ? quantityRange.High.Value.ToString(CultureInfo.InvariantCulture)
                            : null
                    };
                }
                else if (quantityRange.Low.HasValue)
                {
                    pq.ItemsElementName = new ItemsChoiceType[1];
                    pq.Items = new PQ[1];

                    pq.ItemsElementName[0] = ItemsChoiceType.low;
                    pq.Items[0] = new IVXB_PQ
                    {
                        unit = quantityRange.Units,
                        value = quantityRange.Low.HasValue
                            ? quantityRange.Low.Value.ToString(CultureInfo.InvariantCulture)
                            : null,
                        inclusive = quantityRange.Inclusive
                    };
                }
                else if (quantityRange.High.HasValue)
                {
                    pq.ItemsElementName = new ItemsChoiceType[1];
                    pq.Items = new PQ[1];

                    pq.ItemsElementName[0] = ItemsChoiceType.high;
                    pq.Items[0] = new IVXB_PQ
                    {
                        unit = quantityRange.Units,
                        value = quantityRange.High.HasValue
                            ? quantityRange.High.Value.ToString(CultureInfo.InvariantCulture)
                            : null,
                        inclusive = quantityRange.Inclusive
                    };
                }
            }

            return pq;
        }

        # endregion

        #region Private Entry - Time Stamp Element

        private static TS CreateTimeStampElementIso(ParticipationPeriod participationPeriod)
        {
            TS timeStamp = null;

            if (participationPeriod != null && participationPeriod.Value != null)
            {
                timeStamp = CreateTimeStampElementIso(participationPeriod.Value);
            }
            else if (participationPeriod != null && participationPeriod.Interval != null)
            {
                timeStamp = CreateIntervalTimestamp(participationPeriod.Interval);
            }

            return timeStamp;
        }

        private static TS CreateTimeStampElementIso(ISO8601DateTime dateTime)
        {
            TS timeStamp = null;

            if (dateTime != null)
            {
                timeStamp = new TS
                {
                    value = dateTime.ToString()
                };
            }

            return timeStamp;
        }

        private static TS CreateTimeStampElementIso(NullFlavor? nullFlavor)
        {
            TS timeStamp = null;

            if (nullFlavor.HasValue)
            {
                timeStamp = new TS
                {
                    nullFlavor = nullFlavor.Value,
                    nullFlavorSpecified = true
                };
            }

            return timeStamp;
        }

        # endregion

        #region Private Entry - Integer

        private static INT CreateIntegerElement(Int32? value, NullFlavor? nullFlavor, Boolean? nullFlavorSpecified)
        {
            INT integer = null;

            if (value.HasValue)
            {
                integer = new INT { value = value.Value.ToString(CultureInfo.InvariantCulture) };

                if (nullFlavorSpecified.HasValue && nullFlavor.HasValue)
                {
                    integer.nullFlavor = nullFlavor.Value;
                    integer.nullFlavorSpecified = nullFlavorSpecified.Value;
                }
            }

            return integer;
        }

        # endregion

        #region Private Entry - Encapsulated Data

        private static ED CreateEncapsulatedData(String originalText)
        {
            ED returnValue = null;

            if (!String.IsNullOrEmpty(originalText))
            {
                returnValue = CreateEncapsulatedData(originalText, MediaType.TXT);
            }

            return returnValue;
        }

        private static ED CreateEncapsulatedData(ExternalData externalData)
        {
            ED ed = null;

            if (externalData != null)
            {
                var fileName = Path.GetFileName(externalData.Path);

                if (externalData.ByteArrayInput != null)
                    fileName = externalData.ByteArrayInput.FileName;

                ed = new ED
                {
                    mediaType =
                        externalData.ExternalDataMediaType.HasValue
                            ? externalData.ExternalDataMediaType.GetAttributeValue<NameAttribute, string>(x => x.Name)
                            : null,
                    reference =
                        externalData.FileStorageType == FileStorageType.Reference
                            ? new TEL { value = fileName }
                            : null,
                    representation =
                        externalData.FileStorageType == FileStorageType.Embed
                            ? BinaryDataEncoding.B64
                            : BinaryDataEncoding.TXT,
                    Text =
                        externalData.FileStorageType == FileStorageType.Embed
                            ? new[]
                            {
                                externalData.ConvertToBase64String()
                            }
                            : null,
                    integrityCheck = externalData.DigestValue,
                    integrityCheckAlgorithm =
                        externalData.DigestCheckAlgorithm.HasValue &&
                        externalData.DigestCheckAlgorithm.Value == DigestCheckAlgorithm.SHA256
                            ? IntegrityCheckAlgorithm.SHA256
                            : IntegrityCheckAlgorithm.SHA1
                };
            }

            return ed;
        }

        private static ED CreateEncapsulatedData(String text, MediaType? mediaType)
        {
            ED ed = null;

            if (!String.IsNullOrEmpty(text))
            {
                String mediaTypeAsString = null;

                if (mediaType.HasValue)
                {
                    mediaTypeAsString = mediaType.GetAttributeValue<NameAttribute, string>(x => x.Name);
                }

                ed = new ED { Text = new[] { text }, mediaType = mediaTypeAsString };
            }

            return ed;
        }

        # endregion

        #region Private Entry - Telecomunication

        private static TEL[] CreateTelecomunicationArray(
            List<ElectronicCommunicationDetail> electronicCommunicationDetails)
        {
            TEL[] telArray = null;

            if (electronicCommunicationDetails != null && electronicCommunicationDetails.Any())
            {
                var telecomunicationsList = new List<TEL>();

                electronicCommunicationDetails.ForEach(
                    electronicCommunicationDetail =>
                        telecomunicationsList.Add(CreateTelecomunication(electronicCommunicationDetail)));

                telArray = telecomunicationsList.ToArray();
            }

            return telArray;
        }

        private static TEL CreateTelecomunication(ElectronicCommunicationDetail electronicCommunicationDetail)
        {
            TEL telecomunication = null;

            if (electronicCommunicationDetail != null && !String.IsNullOrEmpty(electronicCommunicationDetail.Address))
            {
                telecomunication = new TEL();

                if (electronicCommunicationDetail.Usage != null)
                {
                    var usage = new List<TelecommunicationAddressUse>();
                    foreach (var use in electronicCommunicationDetail.Usage)
                    {
                        usage.Add((TelecommunicationAddressUse)
                            Enum.Parse(typeof(TelecommunicationAddressUse),
                                use.GetAttributeValue<NameAttribute, string>(x => x.Code)));
                    }

                    telecomunication.use = usage.ToArray();
                }

                telecomunication.value = electronicCommunicationDetail.Address;
            }

            return telecomunication;
        }

        private static TEL CreateTelecomunication(TelecommunicationAddressUse? telecommunicationAddressUse,
            string telecom)
        {
            TEL telecomunication = null;

            if (!String.IsNullOrEmpty(telecom))
            {
                if (telecommunicationAddressUse != null)
                    telecomunication = new TEL { use = new[] { telecommunicationAddressUse.Value }, value = telecom };
            }

            return telecomunication;
        }

        # endregion

        #region Private Entry - Person Name

        private static PN[] CreatePersonNameArray(List<IPersonName> personNames)
        {
            List<PN> names = new List<PN>();

            if (personNames != null && personNames.Any())
            {
                foreach (var personName in personNames)
                {
                    names.Add(CreatePersonName(personName));
                }
            }

            return names.ToArray();
        }

        private static PN CreatePersonName(IPersonName personName)
        {
            PN participantName = null;

            if (personName != null)
            {
                participantName = new PN();

                if (personName.NameUsages != null)
                {
                    List<EntityNameUse> uses = new List<EntityNameUse>();
                    foreach (var usage in personName.NameUsages)
                    {
                        uses.Add((EntityNameUse)Enum.Parse(typeof(EntityNameUse),
                            usage.GetAttributeValue<NameAttribute, string>(x => x.Code)));
                    }

                    participantName.use = uses.ToArray();
                }

                if (personName.Titles != null && personName.Titles.Any())
                {
                    participantName.prefix =
                            (from g in personName.Titles select new enprefix { Text = new string[] { g } }).ToArray();
                }

                participantName.family = new enfamily[1];
                if (!string.IsNullOrWhiteSpace(personName.FamilyName))
                    participantName.family[0] = new enfamily { Text = new[] { personName.FamilyName } };
                else
                    participantName.family[0] = new enfamily { nullFlavor = NullFlavor.NI, nullFlavorSpecified = true };

                if (personName.GivenNames != null && personName.GivenNames.Any())
                {
                    participantName.given =
                        (from g in personName.GivenNames select new engiven { Text = new string[] { g } }).ToArray();
                }

                if (personName.NameSuffix != null && personName.NameSuffix.Any())
                {
                    participantName.suffix =
                        (from g in personName.NameSuffix select new ensuffix { Text = new string[] { g } }).ToArray();
                }
            }

            return participantName;
        }

        # endregion

        #region Private Entry - Organisation

        #region Organisation

        private static POCD_MT000040Organization CreateOrganisation(IOrganisationName organisation)
        {
            var org = new POCD_MT000040Organization();

            org.asEntityIdentifier = organisation.Identifiers == null ? null : CreateEntityIdentifierArray(organisation.Identifiers);

            if (!String.IsNullOrEmpty(organisation.Name))
            {
                var on = new ON();
                if (!String.IsNullOrEmpty(organisation.Name))
                {
                    on.Text = new[] { organisation.Name };
                }

                org.name = new[] { on };
            }

            return org;
        }

        private static POCD_MT000040Organization CreateOrganisation(IOrganisation organisation)
        {
            var org = new POCD_MT000040Organization
            {
                id = CreateIdentifierArray(new UniqueId()),
                asOrganizationPartOf =
                    new POCD_MT000040OrganizationPartOf
                    {
                        wholeOrganization =
                            new POCD_MT000040Organization
                            {
                                asEntityIdentifier = organisation.Identifiers == null
                                    ? null
                                    : CreateEntityIdentifierArray(organisation.Identifiers)
                            }
                    }
            };

            if (!String.IsNullOrEmpty(organisation.Department))
                org.name = new[] { new ON { Text = new[] { organisation.Department } } };

            if (!String.IsNullOrEmpty(organisation.Name) ||
                (organisation.NameUsage != null && organisation.NameUsage.Value != OrganisationNameUsage.Undefined))
            {
                var on = new ON();
                if (!String.IsNullOrEmpty(organisation.Name))
                {
                    on.Text = new[] { organisation.Name };
                }

                if (organisation.NameUsage.HasValue && organisation.NameUsage.Value != OrganisationNameUsage.Undefined)
                {
                    on.use = new[]
                    {
                        (EntityNameUse) Enum.Parse(typeof(EntityNameUse),
                            organisation.NameUsage != OrganisationNameUsage.Undefined
                                ? organisation.NameUsage.GetAttributeValue<NameAttribute, string>(x => x.Code)
                                : String.Empty)
                    };
                }

                // PAW:As Organisation name does not support nullFlavor, look for specific string '' and then make org = null
                if (!string.IsNullOrWhiteSpace(organisation.Name))
                    org.asOrganizationPartOf.wholeOrganization.name = new[] { on };
                else
                    org = null;
            }

            return org;
        }

        #endregion

        #region Organisation Name


        private static ON CreateOrganisationName(string name, OrganisationNameUsage? entityNameUse)
        {
            ON returnOrganisationName = null;

            if (!String.IsNullOrEmpty(name))
            {
                returnOrganisationName = new ON { Text = new[] { name } };

                if (entityNameUse.HasValue)
                {
                    returnOrganisationName.use = new EntityNameUse[1];
                    returnOrganisationName.use[0] = (EntityNameUse)Enum.Parse(typeof(EntityNameUse),
                        entityNameUse.Value.GetAttributeValue<NameAttribute, string>(a => a.Code));
                }
            }

            return returnOrganisationName;
        }

        /// <summary>
        /// Creates a name.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="usage">Name usage.</param>
        /// <returns>ON.</returns>
        private static ON CreateName(string name, OrganisationNameUsage? usage)
        {
            ON on = new ON();
            on.Text = new[] { name };

            if (usage != null)
            {
                string nameUsageVal = usage.Value.GetAttributeValue<NameAttribute, string>(a => a.Code);
                EntityNameUse nameUsage = (EntityNameUse)Enum.Parse(typeof(EntityNameUse), nameUsageVal);

                on.use = new[] { nameUsage };
            }

            return on;
        }

        private static ON[] CreateOrganisationNameArray(string name, OrganisationNameUsage? entityNameUse)
        {
            ON[] organisationNameArray = null;

            if (!String.IsNullOrEmpty(name) || entityNameUse.HasValue)
            {
                var organisationNameList = new List<ON> { CreateOrganisationName(name, entityNameUse) };

                organisationNameArray = organisationNameList.ToArray();
            }

            return organisationNameArray;
        }

        private static ON CreateOrganisationName(string name, string nameUseCode)
        {
            ON returnOrganisationName = null;

            if (!String.IsNullOrEmpty(name))
            {
                returnOrganisationName = new ON { Text = new[] { name } };

                if (!string.IsNullOrEmpty(nameUseCode))
                {
                    returnOrganisationName.use = new EntityNameUse[1];
                    returnOrganisationName.use[0] = (EntityNameUse)Enum.Parse(typeof(EntityNameUse), nameUseCode);
                }
            }

            return returnOrganisationName;
        }

        private static ON CreateOrganisationName(string name)
        {
            ON on = null;

            if (!String.IsNullOrEmpty(name))
                on = new ON { Text = new[] { name } };

            return on;
        }

        #endregion

        # endregion

        #region internal Methods - Observation

        private static POCD_MT000040Observation CreateObservation(ActClassObservation actClassObservation,
            x_ActMoodDocumentObservation
                actMoodDocumentObservation, String text)
        {
            return CreateObservation(actClassObservation, actMoodDocumentObservation, null, null, text, null);
        }

        private static POCD_MT000040Observation CreateObservation(ActClassObservation actClassObservation,
            x_ActMoodDocumentObservation
                actMoodDocumentObservation, CD code,
            ISO8601DateTime effectiveTime, String text)
        {
            return CreateObservation(actClassObservation, actMoodDocumentObservation, code, effectiveTime, text, null);
        }

        private static POCD_MT000040Observation CreateObservation(ActClassObservation actClassObservation,
            x_ActMoodDocumentObservation
                actMoodDocumentObservation, CD code,
            ISO8601DateTime effectiveTime, String text, List<POCD_MT000040EntryRelationship> entryRelationship)
        {
            var observation = new POCD_MT000040Observation
            {
                classCode = actClassObservation,
                moodCode = actMoodDocumentObservation,
                id = CreateIdentifierArray(CreateGuid(), null)
            };

            if (code != null)
            {
                observation.code = code;
            }

            if (!String.IsNullOrEmpty(text))
            {
                observation.text = CreateStructuredText(text, null);
            }

            if (effectiveTime != null)
            {
                observation.effectiveTime = CreateIntervalTimestamp(null, null, null, null,
                    effectiveTime.ToString(),
                    null);
            }

            if (entryRelationship != null)
            {
                observation.entryRelationship = entryRelationship.ToArray();
            }

            return observation;
        }


        private static POCD_MT000040Observation CreateSpecimenDetail(SpecimenDetail specimenDetail, ICodableText code)
        {
            POCD_MT000040Observation observation = null;

            if (specimenDetail != null)
            {
                observation = new POCD_MT000040Observation
                {
                    classCode = ActClassObservation.OBS,
                    moodCode = x_ActMoodDocumentObservation.EVN,
                    code = CreateConceptDescriptor(code),
                    id = CreateIdentifierArray(CreateGuid(), null)
                };

                if (specimenDetail.CollectionProcedure != null)
                {
                    observation.methodCode = new List<CE>
                        {CreateCodedWithExtensionElement(specimenDetail.CollectionProcedure)}.ToArray();
                }

                if (specimenDetail.CollectionDateTime != null)
                {
                    observation.effectiveTime = new IVL_TS
                    {
                        value = specimenDetail.CollectionDateTime.ToString()
                    };
                }

                //Anatomical Site
                if (specimenDetail.AnatomicalSite != null && specimenDetail.AnatomicalSite.Any())
                {
                    observation.targetSiteCode =
                        CreateConceptDescriptorsForAnatomicalSites(specimenDetail.AnatomicalSite).ToArray();
                }

                var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

                SpecimenDetailEntryRelationships(specimenDetail, ref entryRelationshipList);

                observation.specimen = new List<POCD_MT000040Specimen> { CreateSpecimenDetailIdentifiers(specimenDetail) }
                    .ToArray();

                //Sub entry Relationship
                if (entryRelationshipList != null)
                {
                    observation.entryRelationship = entryRelationshipList.ToArray();
                }

                if (specimenDetail.CollectionDateTime != null)
                {
                    observation.effectiveTime = CreateIntervalTimestamp(specimenDetail.CollectionDateTime, null);
                }
            }

            return observation;
        }

        #endregion

        #region Private Entry - Boolean

        private static BL CreateBoolean(bool value, bool valueSpecified, NullFlavor nullFlavor,
            bool nullFlavorSpecified)
        {
            var bl = new BL();

            if (valueSpecified)
            {
                bl.value = value;
                bl.valueSpecified = true;
            }

            if (nullFlavorSpecified)
            {
                bl.nullFlavor = nullFlavor;
                bl.nullFlavorSpecified = true;
            }

            return bl;
        }

        internal static BL CreateBoolean(bool value, bool valueSpecified)
        {
            var bl = new BL();

            if (valueSpecified)
            {
                bl.value = value;
                bl.valueSpecified = true;
            }

            return bl;
        }

        # endregion

        #region Private Entry - Entity Identifier


        internal static EntityIdentifier[] CreateEntityIdentifierArray(string root, string extension,
            string assigningAuthorityName, string identifierGeo)
        {
            var entityIdentifierList = new List<EntityIdentifier>
            {
                CreateEntityIdentifier(root, extension, assigningAuthorityName, identifierGeo)
            };

            return entityIdentifierList.ToArray();
        }

        private static EntityIdentifier CreateEntityIdentifier(string root, string extension,
            string assigningAuthorityName, string identifierGeo)
        {
            EntityIdentifier entityidentifier = null;
            if (!String.IsNullOrEmpty(root))
            {
                entityidentifier = new EntityIdentifier { classCode = EntityClass.IDENT, id = new II() };

                if (!String.IsNullOrEmpty(root))
                {
                    entityidentifier.id.root = root;
                }

                if (!String.IsNullOrEmpty(extension))
                {
                    entityidentifier.id.extension = extension;
                }

                if (!String.IsNullOrEmpty(assigningAuthorityName))
                {
                    entityidentifier.id.assigningAuthorityName = assigningAuthorityName;
                }

                entityidentifier.assigningGeographicArea = new GeographicArea
                { name = CreateStructuredText(identifierGeo, null) };
            }

            return entityidentifier;
        }

        /// <summary>
        /// Create Entity Identifier Array
        /// </summary>
        /// <param name="identifiers">A list of Identifiers</param>
        /// <returns>EntityIdentifier array</returns>
        private static EntityIdentifier[] CreateEntityIdentifierArray(IEnumerable<Identifier> identifiers)
        {
            return identifiers.Select(identifier => CreateEntityIdentifier(identifier)).ToArray();
        }

        private static List<EntityIdentifier> CreateEntityIdentifier(IEnumerable<Identifier> identifiers)
        {
            List<EntityIdentifier> entityidentifiers = null;

            if (identifiers != null)
            {
                entityidentifiers = (from identifier in identifiers
                                     where identifier != null
                                     select CreateEntityIdentifier(identifier)).ToList();
            }

            return entityidentifiers;
        }

        internal static EntityIdentifier CreateEntityIdentifier(Identifier identifier)
        {
            EntityIdentifier entityidentifier = null;

            if (!String.IsNullOrEmpty(identifier.Root))
            {
                entityidentifier = new EntityIdentifier { classCode = EntityClass.IDENT, id = new II() };

                if (!String.IsNullOrEmpty(identifier.Root))
                {
                    entityidentifier.id.root = identifier.Root;
                }

                if (!String.IsNullOrEmpty(identifier.Extension))
                {
                    entityidentifier.id.extension = identifier.Extension;
                }

                if (!String.IsNullOrEmpty(identifier.AssigningAuthorityName))
                {
                    entityidentifier.id.assigningAuthorityName = identifier.AssigningAuthorityName;
                }

                if (!String.IsNullOrEmpty(identifier.AssigningGeographicArea))
                {
                    entityidentifier.assigningGeographicArea = new GeographicArea
                    {
                        name = CreateStructuredText(identifier.AssigningGeographicArea, null)
                    };
                }

                if (identifier.Code != null)
                {
                    entityidentifier.code = CreateCodedWithExtensionElement(identifier.Code);
                }
            }

            if (identifier.NullFlavour.HasValue)
            {
                entityidentifier = new EntityIdentifier
                {
                    id = CreateIdentifierElement(null,
                        (NullFlavor)Enum.Parse(typeof(NullFlavor),
                            identifier.NullFlavour.Value.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                };
            }

            return entityidentifier;
        }

        # endregion

        #region Private Entry - Structured Text

        /// <summary>
        /// Create a StructuredText object for a given nullFlavor and value
        /// </summary>
        /// <param name="value">A string representing the value</param>
        /// <returns>The ST to be returned</returns>
        public static ST CreateStructuredText(string value)
        {
            return CreateStructuredText(value, null);
        }

        /// <summary>
        /// Create a StructuredText object for a given nullFlavor and value
        /// </summary>
        /// <returns>The ST to be returned</returns>
        public static ST CreateStructuredText(StructuredText structuredText)
        {
            return CreateStructuredText(structuredText.Text, structuredText.NullFlavour);
        }

        /// <summary>
        /// Create a StructuredText object for a given nullFlavor and value
        /// </summary>
        /// <param name="value">A string representing the value</param>
        /// <param name="nullFlavour">A nullflavor</param>
        /// <returns>The ST to be returned</returns>
        public static ST CreateStructuredText(string value, NullFlavour? nullFlavour)
        {
            NullFlavor? nullFlavor = null;

            if (nullFlavour != null)
                nullFlavor = (NullFlavor)Enum.Parse(typeof(NullFlavor),
                    nullFlavour.GetAttributeValue<NameAttribute, string>(x => x.Code));

            ST st = null;

            if (nullFlavor.HasValue)
            {
                st = new ST();
                st.nullFlavor = nullFlavor.Value;
                st.nullFlavorSpecified = true;
                return st;
            }

            if (!String.IsNullOrEmpty(value))
            {
                st = new ST { Text = new[] { value } };
            }

            return (st);
        }

        # endregion

        #region Private Entry - Subject

        #region Subject1

        private static Subject1 CreatePolicy(ActClass actClass, ActMood actMood, String id, CD conceptDescriptor)
        {
            Subject1 subject1 = null;

            if (conceptDescriptor != null)
            {
                subject1 = new Subject1
                {
                    policy =
                        new Policy
                        {
                            id = String.IsNullOrEmpty(id) ? null : CreateIdentifierElement(id, null, null),
                            classCode = actClass,
                            moodCode = actMood,
                            code = conceptDescriptor
                        }
                };
            }

            return subject1;
        }

        #endregion

        #region Subject2

        private static Subject1 CreatePolicy(ActClass actClass, ActMood actMood, Identifier id, CD conceptDescriptor)
        {
            Subject1 subject1 = null;

            if (conceptDescriptor != null)
            {
                subject1 = new Subject1
                {
                    policy = new Policy
                    {
                        id = id == null ? null : CreateIdentifierElement(id),
                        classCode = actClass,
                        moodCode = actMood,
                        code = conceptDescriptor
                    }
                };
            }

            return subject1;
        }

        private static Subject2 CreateBrandSubstituteAllowed(ActClass actClass, ActMood actMood,
            CE codedWithExtensionElement)
        {
            Subject2 subject2 = null;

            if (codedWithExtensionElement != null)
            {
                subject2 = new Subject2
                {
                    substitutionPermission = new SubstitutionPermission
                    { classCode = actClass, moodCode = actMood, code = codedWithExtensionElement }
                };
            }

            return subject2;
        }

        #endregion

        #endregion

        #region Private Entry - Interval Timestamp

        /// <summary>
        /// The CreateIntervalTimestamp function returns a IVL_TS for a given CdaInterval
        /// </summary>
        /// <param name="interval">A CdaInterval</param>
        /// <returns>The returned IVL_TS</returns>
        public static IVL_TS CreateIntervalTimestamp(CdaInterval interval)
        {
            return CreateIntervalTimestamp(interval, null);
        }

        private static IVL_TS CreateIntervalTimestamp(CdaInterval interval, NullFlavor? nullFlavor)
        {
            IVL_TS timestamp = null;

            if (nullFlavor.HasValue)
            {
                return new IVL_TS
                {
                    nullFlavor = nullFlavor.Value,
                    nullFlavorSpecified = true
                };
            }

            if (interval != null && (interval.Type == IntervalType.Value))
            {
                return new IVL_TS { value = interval.Low.ToString() };
            }

            if (interval != null && (interval.Low != null || interval.High != null || interval.Center != null ||
                                     interval.IntervalWidth != null))
            {
                var values = new Dictionary<ItemsChoiceType3, QTY>();

                timestamp = new IVL_TS();

                if (interval.Low != null)
                {
                    values.Add(ItemsChoiceType3.low, new IVXB_TS { value = interval.Low.ToString() });
                }

                if (interval.Center != null)
                {
                    values.Add(ItemsChoiceType3.center, new TS { value = interval.Center.ToString() });
                }

                if (interval.IntervalWidth != null)
                {
                    values.Add(ItemsChoiceType3.width,
                        new PQ
                        {
                            value = interval.IntervalWidth.Value,
                            unit = interval.IntervalWidth.Unit.GetAttributeValue<NameAttribute, string>(x => x.Code)
                        });
                }

                if (interval.High != null)
                {
                    values.Add(ItemsChoiceType3.high, new IVXB_TS { value = interval.High.ToString() });
                }

                if (values.Any())
                {
                    timestamp.Items = values.Values.ToArray();
                    timestamp.ItemsElementName = values.Keys.ToArray();
                }
            }

            return timestamp;
        }

        internal static IVL_TS CreateIntervalTimestamp(string low, string high, string width, string center,
            string value, NullFlavor? nullFlavor)
        {
            IVL_TS timestamp = null;

            if (!String.IsNullOrEmpty(low) || !String.IsNullOrEmpty(low) || !String.IsNullOrEmpty(high) ||
                !String.IsNullOrEmpty(center) || !String.IsNullOrEmpty(value) || nullFlavor.HasValue)
            {
                Dictionary<ItemsChoiceType3, QTY> values = new Dictionary<ItemsChoiceType3, QTY>();

                timestamp = new IVL_TS();

                if (!String.IsNullOrEmpty(low))
                    values.Add(ItemsChoiceType3.low, new IVXB_TS { value = low });
                if (!String.IsNullOrEmpty(high))
                    values.Add(ItemsChoiceType3.high, new IVXB_TS { value = high });
                if (!String.IsNullOrEmpty(width))
                    values.Add(ItemsChoiceType3.width, new PQ { value = width });
                if (!String.IsNullOrEmpty(center))
                    values.Add(ItemsChoiceType3.center, new TS { value = center });

                if (values.Any())
                {
                    timestamp.Items = values.Values.ToArray();
                    timestamp.ItemsElementName = values.Keys.ToArray();
                }

                if (!String.IsNullOrEmpty(value))
                    timestamp.value = value;

                timestamp.nullFlavor = NullFlavor.NA;
                timestamp.nullFlavorSpecified = false;

                if (nullFlavor.HasValue)
                {
                    timestamp.nullFlavor = nullFlavor.Value;
                    timestamp.nullFlavorSpecified = true;
                }
            }

            return timestamp;
        }

        private static IVL_TS CreateIntervalTimestamp(ISO8601DateTime dateTime, NullFlavor? nullFlavor)
        {
            IVL_TS timestamp = null;

            if (dateTime != null || nullFlavor.HasValue)
            {
                timestamp = new IVL_TS();

                if (dateTime != null)
                {
                    timestamp.value = dateTime.ToString();
                }

                if (nullFlavor.HasValue)
                {
                    timestamp.nullFlavor = nullFlavor.Value;
                    timestamp.nullFlavorSpecified = true;
                }
            }

            return timestamp;
        }

        private static IVL_TS CreateIntervalTimestamp(DateTime? dateTime, NullFlavor? nullFlavor)
        {
            IVL_TS timestamp = null;

            if (dateTime.HasValue || nullFlavor.HasValue)
            {
                timestamp = new IVL_TS();

                if (dateTime.HasValue)
                {
                    timestamp.value = dateTime.Value.ToString(DATE_TIME_FORMAT);
                }

                if (nullFlavor.HasValue)
                {
                    timestamp.nullFlavor = nullFlavor.Value;
                    timestamp.nullFlavorSpecified = true;
                }
            }

            return timestamp;
        }

        # endregion

        #region Private Entry - Section Code & Title

        internal static POCD_MT000040Section CreateSectionCodeTitle<T>(T defaultValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type which can be mapped to a CodingSystem");
            }

            var enumeration = defaultValue as Enum;

            var sectionCodableText = new CodableText
            {
                DisplayName = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Name),
                Code = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodeSystemCode =
                    ((CodingSystem)Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodeSystemName =
                    ((CodingSystem)Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Name),
                CodeSystemVersion =
                    ((CodingSystem)Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Version)
            };

            var section = new POCD_MT000040Section
            {
                code = CreateCodedWithExtensionElement(sectionCodableText),
                title = new ST
                {
                    Text = new[]
                    {
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.Title)
                    }
                },
                text = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Narrative) != null
                    ? new StrucDocText
                    {
                        paragraph = new[]
                        {
                            new StrucDocParagraph
                                {Text = new[] {enumeration.GetAttributeValue<NameAttribute, string>(x => x.Narrative)}}
                        }
                    }
                    : new StrucDocText(),
                id = CreateIdentifierElement(CreateGuid(), null)
            };

            return (section);
        }

        internal static POCD_MT000040Section CreateSectionCodeTitle(string code, CodingSystem? codeSystem, string value)
        {
            return CreateSectionCodeTitle(code, codeSystem, value, value, "");
        }

        internal static POCD_MT000040Section CreateSectionCodeTitle(string code, CodingSystem? codeSystem, string value,
            string narrative)
        {
            return CreateSectionCodeTitle(code, codeSystem, value, value, narrative);
        }

        internal static POCD_MT000040Section CreateSectionCodeTitle(string code, CodingSystem? codeSystem,
            string displayName, string title, string narrative)
        {
            var section = new POCD_MT000040Section
            {
                code = CreateCodedWithExtensionElement(code, codeSystem, displayName, null, null, null),
                title = new ST { Text = new[] { title } },
                text = !string.IsNullOrEmpty(narrative)
                    ? new StrucDocText { paragraph = new[] { new StrucDocParagraph { Text = new[] { narrative } } } }
                    : null,
                //text = !string.IsNullOrEmpty(narrative) ? new StrucDocText { paragraph = new[] { new StrucDocParagraph { Text = new[] { narrative } } } } : new StrucDocText(),
                id = CreateIdentifierElement(CreateGuid(), null)
            };

            return (section);
        }

        internal static POCD_MT000040Section CreateSectionCodeTitle(string code, CodingSystem? codeSystem,
            string displayName, string title, StrucDocText narrative)
        {
            var section = new POCD_MT000040Section
            {
                code = CreateCodedWithExtensionElement(code, codeSystem, displayName, null, null, null),
                title = new ST
                {
                    Text = new[]
                    {
                        title
                    }
                },
                text = narrative,
                id = CreateIdentifierElement(CreateGuid(), null)
            };

            return (section);
        }

        # endregion

        #region Private Entry - Participant Role

        private static ParticipantRole CreateParticipantRole(Identifier identifier, RoleClass? classCode)
        {
            var participantRole = new ParticipantRole
            {
                id = CreateIdentifierElement(identifier)
            };

            if (classCode.HasValue)
            {
                participantRole.classCode = classCode.Value;
                participantRole.classCodeSpecified = true;
            }

            return participantRole;
        }

        # endregion

        #region Private Entry - Substance Administration

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry? actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, string low, string high, ISO8601DateTime effectiveTime, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, string id)
        {
            return CreateEntrySubstanceAdministrationEvent(actRelationshipEntry, substanceMood, showStatusCode, text,
                low, high, effectiveTime, CEcode, repeatNumber, relationships, null, "active", null, id, null, null,
                null, null);
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry? actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, string low, string high, ISO8601DateTime effectiveTime, CE CEcode, int? repeatNumber,
            string genericName, ICodableText formCode, ICodableText routeCode, Identifier manufacturerOrganizationId,
            List<POCD_MT000040EntryRelationship> relationships, string id)
        {
            return CreateEntrySubstanceAdministrationEvent(actRelationshipEntry, substanceMood, showStatusCode, text,
                low, high, effectiveTime, CEcode, repeatNumber, relationships, null, "active", null, id, genericName,
                formCode, routeCode, manufacturerOrganizationId);
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry? actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            string statusCode, ST text, string low, string high, ISO8601DateTime effectiveTime, CE CEcode,
            int? repeatNumber, string genericName, ICodableText formCode, ICodableText routeCode,
            Identifier manufacturerOrganizationId, List<POCD_MT000040EntryRelationship> relationships, string id)
        {
            return CreateEntrySubstanceAdministrationEvent(actRelationshipEntry, substanceMood, showStatusCode, text,
                low, high, effectiveTime, CEcode, repeatNumber, relationships, null, statusCode, null, id, genericName,
                formCode, routeCode, manufacturerOrganizationId);
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry? actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, string low, string high, ISO8601DateTime effectiveTime, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, string statusCode, string id)
        {
            return CreateEntrySubstanceAdministrationEvent(actRelationshipEntry, substanceMood, showStatusCode, text,
                low, high, effectiveTime, CEcode, repeatNumber, relationships, null, statusCode, null, id, null, null,
                null, null);
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, CdaInterval interval, DateTime? effectiveTime, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, string id)
        {
            return CreateEntrySubstanceAdministrationEvent(actRelationshipEntry, substanceMood, showStatusCode, text,
                interval, effectiveTime, CEcode, repeatNumber, relationships, null, "active", null, id);
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry? actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, string low, string high, ISO8601DateTime effectiveTime, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, Boolean? negationIndicator, string statusCode,
            List<Subject1> subjects, string id, string genericName, ICodableText formCode, ICodableText routeCode,
            Identifier manufacturerOrganizationId)
        {
            var entry = new POCD_MT000040Entry
            {
                substanceAdministration = new POCD_MT000040SubstanceAdministration
                {
                    classCode = ActClass.SBADM,
                    moodCode = substanceMood,
                    id = CreateIdentifierArray
                    (
                        String.IsNullOrEmpty(id) ? CreateGuid() : id,
                        null
                    ),
                    negationInd = negationIndicator.HasValue && negationIndicator.Value,
                    negationIndSpecified = negationIndicator.HasValue,
                    consumable = new POCD_MT000040Consumable
                    {
                        manufacturedProduct = new POCD_MT000040ManufacturedProduct
                        {
                            manufacturedMaterial = new POCD_MT000040Material
                            {
                                code = CEcode,
                                name = genericName.IsNullOrEmptyWhitespace()
                                    ? null
                                    : new EN { Text = new[] { genericName } },
                                formCode = CreateConceptDescriptor(formCode)
                            },
                            manufacturerOrganization = manufacturerOrganizationId != null
                                ? new POCD_MT000040Organization
                                {
                                    id = CreateIdentifierArray(manufacturerOrganizationId)
                                }
                                : null
                        }
                    }
                }
            };

            if (actRelationshipEntry.HasValue)
            {
                entry.typeCode = actRelationshipEntry.Value;
            }

            if (showStatusCode)
            {
                entry.substanceAdministration.statusCode = CreateCodeSystem(statusCode, null, null, null, null, null);
            }

            if (text != null)
            {
                entry.substanceAdministration.text = text;
            }

            if (routeCode != null)
            {
                entry.substanceAdministration.routeCode = CreateCodedWithExtensionElement(routeCode);
            }

            if (!String.IsNullOrEmpty(low) || !String.IsNullOrEmpty(high))
            {
                entry.substanceAdministration.effectiveTime = new[]
                {
                    CreateIntervalTimestamp
                    (
                        low,
                        high,
                        null,
                        null,
                        null,
                        null
                    )
                };
            }

            if (effectiveTime != null)
            {
                entry.substanceAdministration.effectiveTime = new[]
                {
                    new SXCM_TS
                    {
                        value = effectiveTime.ToString()
                    }
                };
            }

            if (relationships != null)
            {
                entry.substanceAdministration.entryRelationship = relationships.ToArray();
            }

            if (subjects != null && subjects.Any())
            {
                entry.substanceAdministration.consumable.manufacturedProduct.subjectOf1 = subjects.ToArray();
            }

            if (repeatNumber.HasValue)
            {
                entry.substanceAdministration.repeatNumber = new IVL_INT
                {
                    ItemsElementName = new[] { ItemsChoiceType5.high },
                    Items = new[] { new IVXB_INT { value = repeatNumber.Value.ToString(CultureInfo.InvariantCulture) } }
                };
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEvent(
            x_ActRelationshipEntry actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, CdaInterval interval, DateTime? effectiveTime, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, Boolean? negationIndicator, String statusCode,
            List<Subject1> subjects, String id)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                substanceAdministration = new POCD_MT000040SubstanceAdministration
                {
                    classCode = ActClass.SBADM,
                    moodCode = substanceMood,
                    id = CreateIdentifierArray
                    (
                        String.IsNullOrEmpty(id) ? CreateGuid() : id,
                        null
                    ),
                    negationInd = negationIndicator.HasValue && negationIndicator.Value,
                    negationIndSpecified = negationIndicator.HasValue
                }
            };

            if (showStatusCode)
            {
                entry.substanceAdministration.statusCode = CreateCodeSystem(statusCode, null, null, null, null, null);
            }

            if (text != null)
            {
                entry.substanceAdministration.text = text;
            }

            if (interval != null)
            {
                entry.substanceAdministration.effectiveTime = new SXCM_TS[]
                {
                    CreateIntervalTimestamp
                    (
                        interval
                    )
                };
            }

            if (effectiveTime.HasValue)
            {
                entry.substanceAdministration.effectiveTime = new[]
                {
                    new SXCM_TS
                    {
                        value = effectiveTime.Value.ToString(DATE_TIME_FORMAT)
                    }
                };
            }

            entry.substanceAdministration.consumable = new POCD_MT000040Consumable
            {
                manufacturedProduct = new POCD_MT000040ManufacturedProduct
                {
                    manufacturedMaterial = new POCD_MT000040Material
                    {
                        code = CEcode
                    }
                }
            };

            if (relationships != null)
            {
                entry.substanceAdministration.entryRelationship = relationships.ToArray();
            }

            if (subjects != null && subjects.Any())
            {
                entry.substanceAdministration.consumable.manufacturedProduct.subjectOf1 = subjects.ToArray();
            }

            if (repeatNumber.HasValue)
            {
                entry.substanceAdministration.repeatNumber = new IVL_INT
                {
                    ItemsElementName = new[]
                    {
                        ItemsChoiceType5.high
                    },
                    Items = new[]
                    {
                        new INT
                        {
                            value = repeatNumber.Value.ToString(CultureInfo.InvariantCulture)
                        }
                    }
                };
            }

            return entry;
        }

        /// <summary>
        /// ATS Version of Substance Administration Event
        /// </summary>
        /// <returns></returns>
        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEventATS(
            x_ActRelationshipEntry actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            ST text, IEnumerable<ITime> complextTimeList, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, Boolean? negationIndicator, String statusCode,
            List<Subject1> subjects, Identifier id, List<PBSExtemporaneousIngredient> ingredient,
            QuantityUnit quantityUnit, bool? prn, FrequencyQuantity frequencyQuantity,
            Identifier medicationInstructionIdentifier, AdministrationDetails administrationDetails)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                substanceAdministration = new POCD_MT000040SubstanceAdministration
                {
                    classCode = ActClass.SBADM,
                    moodCode = substanceMood,
                    id = id != null ? CreateIdentifierArray(id) : CreateIdentifierArray(CreateGuid(), null),
                    negationInd = negationIndicator.HasValue && negationIndicator.Value,
                    negationIndSpecified = negationIndicator.HasValue
                }
            };

            if (showStatusCode)
            {
                entry.substanceAdministration.statusCode = CreateCodeSystem(statusCode, null, null, null, null, null);
            }

            // Administration Details
            if (administrationDetails != null)
            {
                if (administrationDetails.Route != null)
                    entry.substanceAdministration.routeCode =
                        CreateCodedWithExtensionElement(administrationDetails.Route);

                if (administrationDetails.AnatomicalSite != null)
                    entry.substanceAdministration.approachSiteCode = new[]
                        {CreateConceptDescriptor(administrationDetails.AnatomicalSite)};

                if (administrationDetails.MedicationDeliveryMethod != null)
                    entry.substanceAdministration.methodCode =
                        CreateConceptDescriptor(administrationDetails.MedicationDeliveryMethod);
            }

            // Substance Administration Text
            if (text != null)
                entry.substanceAdministration.text = text;

            // Effective Time
            entry.substanceAdministration.effectiveTime = CreateComplexTimeList(complextTimeList);

            if (medicationInstructionIdentifier != null)
            {
                entry.substanceAdministration.reference = new[]
                {
                    new POCD_MT000040Reference
                    {
                        typeCode = x_ActRelationshipExternalReference.SPRT,
                        seperatableInd = new BL {value = true, valueSpecified = true},
                        externalAct = new POCD_MT000040ExternalAct
                        {
                            classCode = ActClassRoot.ACT,
                            moodCode = ActMood.EVN,
                            moodCodeSpecified = true,
                            id = CreateIdentifierArray(medicationInstructionIdentifier),
                            code = CreateConceptDescriptor
                            (
                                AdminCodes.MedicationInstructionIdentifier.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                AdminCodes.MedicationInstructionIdentifier.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null,
                                null
                            )
                        }
                    }
                };
            }

            // Prescription Item > Timing > PRN
            if (prn.HasValue)
                entry.substanceAdministration.precondition = new[]
                {
                    new POCD_MT000040Precondition
                    {
                        criterion = new POCD_MT000040Criterion
                        {
                            value = new BL
                            {
                                value = prn.Value,
                                valueSpecified = true
                            }
                        }
                    }
                };

            if (CEcode != null || ingredient != null)
                entry.substanceAdministration.consumable = new POCD_MT000040Consumable
                {
                    manufacturedProduct = new POCD_MT000040ManufacturedProduct
                    {
                        manufacturedMaterial = new POCD_MT000040Material
                        {
                            code = CEcode,
                            asIngredient = CreatePBSExtemporaneousIngredient(ingredient),
                            formCode = quantityUnit != null && quantityUnit.Unit != null
                                ? CreateConceptDescriptor(quantityUnit.Unit)
                                : null
                        }
                    }
                };

            if (frequencyQuantity != null)
                entry.substanceAdministration.maxDoseQuantity = CreateFrequencyQuantity(frequencyQuantity);

            if (relationships != null)
            {
                entry.substanceAdministration.entryRelationship = relationships.ToArray();
            }

            if (subjects != null && subjects.Count > 0)
            {
                entry.substanceAdministration.consumable.manufacturedProduct.subjectOf1 = subjects.ToArray();
            }

            if (repeatNumber.HasValue)
            {
                entry.substanceAdministration.repeatNumber = new IVL_INT();
                entry.substanceAdministration.repeatNumber.ItemsElementName = new[] { ItemsChoiceType5.high };
                entry.substanceAdministration.repeatNumber.Items = new INT[]
                    {new IVXB_INT {value = repeatNumber.Value.ToString(CultureInfo.InvariantCulture)}};
            }

            if (quantityUnit != null)
            {
                if (quantityUnit.Quantity != null)
                    entry.substanceAdministration.doseQuantity = new IVL_PQ
                    {
                        unit = string.IsNullOrEmpty(quantityUnit.Quantity.Units) ? null : quantityUnit.Quantity.Units,
                        value = !quantityUnit.Quantity.Value.IsNullOrEmptyWhitespace()
                            ? quantityUnit.Quantity.Value
                            : null,
                    };
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntrySubstanceAdministrationEventATS(
            x_ActRelationshipEntry actRelationshipEntry, x_DocumentSubstanceMood substanceMood, bool showStatusCode,
            string statusCode, ST text, List<ITime> complextTimeList, CE CEcode, int? repeatNumber,
            List<POCD_MT000040EntryRelationship> relationships, List<PBSExtemporaneousIngredient> ingredient,
            QuantityUnit quantityUnit, Identifier id, bool? prn, FrequencyQuantity frequencyQuantity,
            Identifier medicationInstructionIdentifier, AdministrationDetails administrationDetails)
        {
            return CreateEntrySubstanceAdministrationEventATS(actRelationshipEntry, substanceMood, showStatusCode, text,
                complextTimeList, CEcode, repeatNumber, relationships, null, statusCode, null, id, ingredient,
                quantityUnit, prn, frequencyQuantity, medicationInstructionIdentifier, administrationDetails);
        }

        # endregion

        #region Private Entry - Ingredient

        private static Ingredient[] CreatePBSExtemporaneousIngredient(
            ICollection<PBSExtemporaneousIngredient> pbsExtemporaneousIngredient)
        {
            List<Ingredient> ingredients = null;

            if (pbsExtemporaneousIngredient != null && pbsExtemporaneousIngredient.Count > 0)
            {
                ingredients = new List<Ingredient>();
                foreach (var extemporaneousIngredient in pbsExtemporaneousIngredient)
                {
                    if (extemporaneousIngredient != null)
                        ingredients.Add
                        (
                            new Ingredient
                            {
                                classCode = RoleClassRoot.INGR,
                                ingredientManufacturedMaterial = new ManufacturedMaterial
                                {
                                    classCode = EntityClass.MMAT,
                                    determinerCode = EntityDeterminer.KIND,
                                    code = CreateCodedValue(extemporaneousIngredient.IngredientName),
                                    quantity = new RTO_PQ_PQ
                                    {
                                        numerator = CreatePhysicalQuantity(extemporaneousIngredient.IngredientQuantity),
                                        denominator = new PQ
                                        {
                                            value = "1"
                                        }
                                    }
                                },
                            }
                        );
                }
            }

            return ingredients != null ? ingredients.ToArray() : null;
        }

        /// <summary>
        /// Creates a Medicare Antigen Codes
        /// </summary>
        /// <param name="medicareAntigenCodes">The Medicare Antigen Codes </param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static Ingredient[] CreateMedicareAntigenCodes(List<ICodableText> medicareAntigenCodes)
        {
            List<Ingredient> ingredients = null;

            if (medicareAntigenCodes != null && medicareAntigenCodes.Any())
            {
                ingredients = new List<Ingredient>();
                foreach (var medicareAntigenCode in medicareAntigenCodes)
                {
                    ingredients.Add(new Ingredient
                    {
                        classCode = RoleClassRoot.INGR,
                        ingredientManufacturedMaterial = new ManufacturedMaterial
                        {
                            classCode = EntityClass.MMAT,
                            determinerCode = EntityDeterminer.KIND,
                            code = CreateCodedValue(medicareAntigenCode)
                        }
                    }
                    );
                }
            }

            return ingredients == null ? null : ingredients.ToArray();
        }

        # endregion

        #region Reference Range

        private static POCD_MT000040ReferenceRange CreateReferenceRange(ReferenceRangeDetails referenceRangeDetails)
        {
            POCD_MT000040ReferenceRange referenceRange = null;

            if (referenceRangeDetails != null)
            {
                referenceRange = new POCD_MT000040ReferenceRange
                {
                    typeCodeSpecified = true,
                    observationRange =
                        new POCD_MT000040ObservationRange
                        {
                            classCode = ActClassObservation.OBS,
                            moodCode = ActMood.EVNCRT,
                            moodCodeSpecified = true,
                            code = referenceRangeDetails.ReferenceRangeMeaning != null
                                ? CreateConceptDescriptor(referenceRangeDetails.ReferenceRangeMeaning)
                                : null,
                            value = referenceRangeDetails.ReferenceRange != null
                                ? CreateIntervalPhysicalQuantity(referenceRangeDetails.ReferenceRange)
                                : null
                        },
                };
            }

            return referenceRange;
        }

        #endregion

        #region Private Entry - Coverage

        public static List<Coverage2> CreateEntitlements(ICollection<Entitlement> entitlements, string id,
            RoleClass roleClass, ParticipationType participationType)
        {
            List<Coverage2> coverageList = null;

            if (entitlements != null && entitlements.Any())
            {
                coverageList = new List<Coverage2>();

                foreach (var entitlement in entitlements)
                {
                    ICodableText codeableTextEntry = null;

                    if (entitlement.Type != EntitlementType.Undefined)
                    {
                        codeableTextEntry = new CodableText
                        {
                            Code = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Code),
                            DisplayName = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                            OriginalText = entitlement.Type.GetAttributeValue<NameAttribute, String>(x => x.Name),
                            CodeSystem = CodingSystem.NCTISEntitlementTypeValues
                        };
                    }

                    var cdaEntitlement = new HL7.CDA.Entitlement
                    {
                        classCode = EntityClass.COV,
                        moodCode = ActMood.EVN,
                        id = entitlement.Id == null
                            ? null
                            : CreateIdentifierElement(
                                entitlement.Id.AssigningAuthorityName ==
                                HealthIdentifierType.HPII.GetAttributeValue<NameAttribute, String>(x => x.Code) ||
                                entitlement.Id.AssigningAuthorityName ==
                                HealthIdentifierType.HPIO.GetAttributeValue<NameAttribute, String>(x => x.Code) ||
                                entitlement.Id.AssigningAuthorityName ==
                                HealthIdentifierType.IHI.GetAttributeValue<NameAttribute, String>(x => x.Code)
                                    ? HEALTH_IDENTIFIER_QUALIFIER + entitlement.Id.Root
                                    : entitlement.Id.Root, entitlement.Id.Extension, null,
                                entitlement.Id.AssigningAuthorityName, null),
                        code = CreateConceptDescriptor(codeableTextEntry),
                        effectiveTime = CreateIntervalTimestamp(entitlement.ValidityDuration)
                    };

                    var coverage = new Coverage2 { entitlement = cdaEntitlement, typeCode = "COVBY", };

                    var participantRole =
                        CreateParticipantRole(new Identifier { Root = String.IsNullOrEmpty(id) ? String.Empty : id },
                            roleClass);

                    cdaEntitlement.participant = new[] { CreateParticipant(participantRole, participationType) };

                    coverageList.Add(coverage);
                }
            }

            return coverageList;
        }

        private static Coverage CreatePolicyOrAccount(ActClass actClass, ActMood actMood, String id,
            CD conceptDescriptor)
        {
            Coverage coverage = null;

            if (conceptDescriptor != null)
            {
                coverage = new Coverage
                {
                    policyOrAccount =
                        new PolicyOrAccount
                        {
                            id = String.IsNullOrEmpty(id) ? null : CreateIdentifierElement(id, null, null),
                            classCode = actClass,
                            moodCode = actMood,
                            code = conceptDescriptor
                        }
                };
            }

            return coverage;
        }

        private static Coverage CreatePolicyOrAccount(ActClass actClass, ActMood actMood, Identifier id,
            CD conceptDescriptor)
        {
            Coverage coverage = null;

            if (conceptDescriptor != null)
            {
                coverage = new Coverage
                {
                    policyOrAccount =
                        new PolicyOrAccount
                        {
                            id = id == null ? null : CreateIdentifierElement(id),
                            classCode = actClass,
                            moodCode = actMood,
                            code = conceptDescriptor
                        }
                };
            }

            return coverage;
        }

        #endregion

        #region Private Entry - General Time Specification (ComplexTime)

        private static RTO_PQ_PQ CreateFrequencyQuantity(FrequencyQuantity time)
        {
            RTO_PQ_PQ complexTime = null;

            if (time != null)
            {
                complexTime = new RTO_PQ_PQ();

                if (time.NullFlavor.HasValue)
                {
                    complexTime.nullFlavor = time.NullFlavor.Value;
                    complexTime.nullFlavorSpecified = true;
                }

                if (time.Denominator != null)
                    complexTime.denominator = CreatePhysicalQuantity(time.Denominator);

                if (time.Numerator != null)
                    complexTime.numerator = CreatePhysicalQuantity(time.Numerator);
            }

            return complexTime;
        }

        private static SXCM_TS[] CreateComplexTimeList(IEnumerable<ITime> listComplexTime)
        {
            if (listComplexTime == null) return null;

            var timeList = new List<SXCM_TS>();
            foreach (var time in listComplexTime)
            {
                switch (time.GetType().Name)
                {
                    case "EventRelatedIntervalOfTime":
                        timeList.Add(CreateComplexTime(time as EventRelatedIntervalOfTime));
                        break;
                    case "CdaInterval":
                        timeList.Add(CreateComplexTime(time as CdaInterval));
                        break;
                    case "ParentheticSetExpressionOfTime":
                        timeList.Add(CreateComplexTime(time as ParentheticSetExpressionOfTime));
                        break;
                    case "PeriodicIntervalOfTime":
                        timeList.Add(CreateComplexTime(time as PeriodicIntervalOfTime));
                        break;
                    case "SetComponentTS":
                        timeList.Add(CreateComplexTime(time as SetComponentTS));
                        break;
                    case "ISO8601DateTime":
                        timeList.Add(CreateComplexTime(time as ISO8601DateTime));
                        break;
                }
            }

            return timeList.ToArray();
        }

        private static EIVL_TS CreateComplexTime(EventRelatedIntervalOfTime time)
        {
            EIVL_TS complexTime = null;

            if (time != null)
            {
                complexTime = new EIVL_TS();

                if (time.Operator.HasValue)
                    complexTime.@operator = (SetOperator)Enum.Parse(typeof(SetOperator),
                        time.Operator.GetAttributeValue<NameAttribute, string>(x => x.Code));

                if (time.Event.HasValue)
                    complexTime.@event = new EIVLevent
                    { code = time.Event.Value.GetAttributeValue<NameAttribute, string>(x => x.Code) };

                if (time.Offset != null)
                    complexTime.offset = CreateIntervalPhysicalQuantity(time.Offset);

                if (time.Value != null)
                    complexTime.value = time.Value.ToString();
            }

            return complexTime;
        }

        private static IVL_TS CreateComplexTime(CdaInterval time)
        {
            return CreateIntervalTimestamp(time);
        }

        private static SXPR_TS CreateComplexTime(ParentheticSetExpressionOfTime time)
        {
            SXPR_TS complexTime = null;

            if (time != null)
            {
                complexTime = new SXPR_TS();

                if (time.Operator.HasValue)
                    complexTime.@operator = (SetOperator)Enum.Parse(typeof(SetOperator),
                        time.Operator.GetAttributeValue<NameAttribute, string>(x => x.Code));

                if (time.Comp != null && time.Comp.Count > 0)
                    complexTime.comp = CreateComplexTimeList(time.Comp);

                if (time.Value != null)
                    complexTime.value = time.Value.ToString();
            }

            return complexTime;
        }

        private static PIVL_TS CreateComplexTime(PeriodicIntervalOfTime time)
        {
            PIVL_TS complexTime = null;

            if (time != null)
            {
                complexTime = new PIVL_TS();

                if (time.Operator.HasValue)
                    complexTime.@operator = (SetOperator)Enum.Parse(typeof(SetOperator),
                        time.Operator.GetAttributeValue<NameAttribute, string>(x => x.Code));

                if (time.Alignment.HasValue)
                {
                    complexTime.alignment = (CalendarCycle)Enum.Parse(typeof(CalendarCycle),
                        time.Alignment.GetAttributeValue<NameAttribute, string>(x => x.Code));
                    complexTime.alignmentSpecified = true;
                }

                if (time.Frequency != null)
                    complexTime.frequency = CreateFrequency(time.Frequency);

                if (time.Period != null)
                    complexTime.period = CreatePhysicalQuantity(time.Period);

                if (time.InstitutionSpecified.HasValue)
                    complexTime.institutionSpecified1 = time.InstitutionSpecified.Value;

                if (time.Phase != null)
                    complexTime.phase = CreateComplexTime(time.Phase);

                if (time.Value != null)
                    complexTime.value = time.Value.ToString();
            }

            return complexTime;
        }

        private static RTO CreateFrequency(Frequency time)
        {
            RTO complexTime = null;

            if (time != null)
            {
                complexTime = new RTO();

                if (time.Denominator != null)
                    complexTime.denominator = CreatePhysicalQuantity(time.Denominator);

                if (time.Numerator != null)
                    complexTime.numerator = CreatePhysicalQuantity(time.Numerator);
            }

            return complexTime;
        }

        private static SXCM_TS CreateComplexTime(SetComponentTS time)
        {
            SXCM_TS complexTime = null;

            if (time != null)
            {
                complexTime = new SXCM_TS();

                if (time.Operator.HasValue)
                    complexTime.@operator = (SetOperator)Enum.Parse(typeof(SetOperator),
                        time.Operator.GetAttributeValue<NameAttribute, string>(x => x.Code));

                if (time.Value != null)
                    complexTime.value = time.Value.ToString();
            }

            return complexTime;
        }

        private static SXCM_TS CreateComplexTime(ISO8601DateTime time)
        {
            SXCM_TS complexTime = null;

            if (time != null)
            {
                complexTime = new SXCM_TS();
                complexTime.value = time.ToString();
            }

            return complexTime;
        }

        /// <summary>
        /// ConvertCEtoEIVLevent
        /// </summary>
        /// <param name="code">A CE</param>
        /// <returns>Returns a EIVLevent </returns>
        private static EIVLevent ConvertCEtoEIVLevent(CE code)
        {
            EIVLevent eivlEvent = null;

            if (code != null)
            {
                eivlEvent = new EIVLevent();
                eivlEvent.code = code.code;
                eivlEvent.codeSystem = code.codeSystem;
                eivlEvent.codeSystemName = code.codeSystemName;
                eivlEvent.codeSystemVersion = code.codeSystemVersion;
                eivlEvent.displayName = code.displayName;
                eivlEvent.nullFlavor = code.nullFlavor;
                eivlEvent.nullFlavorSpecified = code.nullFlavorSpecified;
                eivlEvent.originalText = code.originalText;
                eivlEvent.qualifier = code.qualifier;
                eivlEvent.translation = code.translation;
            }

            return eivlEvent;
        }

        # endregion

        #region Private Entry - Specimen Detail Identifier

        private static POCD_MT000040Specimen CreateSpecimenDetailIdentifiers(SpecimenDetail specimenDetail)
        {
            POCD_MT000040Specimen specimen = null;

            if (specimenDetail != null)
            {
                if (specimenDetail.SpecimenIdentifier != null ||
                    specimenDetail.PhysicalDescription != null ||
                    specimenDetail.ParentSpecimenIdentifier != null ||
                    specimenDetail.PhysicalDetails != null ||
                    specimenDetail.ContainerIdentifier != null ||
                    specimenDetail.SpecimenTissueType != null)
                {

                    var physicalDetailsList = new List<List<String>>();

                    specimen = new POCD_MT000040Specimen
                    {
                        specimenRole = new POCD_MT000040SpecimenRole
                        {
                            id = specimenDetail.SpecimenIdentifier == null
                                ? null
                                : CreateIdentifierArray(specimenDetail.SpecimenIdentifier)
                        }
                    };

                    if (
                        specimenDetail.SpecimenIdentifier != null ||
                        specimenDetail.PhysicalDescription != null ||
                        specimenDetail.ContainerIdentifier != null ||
                        specimenDetail.PhysicalDetails != null
                    )
                    {
                        specimen.specimenRole.specimenPlayingEntity = new POCD_MT000040PlayingEntity
                        {
                            code = CreateCodedWithExtensionElement(specimenDetail.SpecimenTissueType),
                            desc = CreateStructuredText(specimenDetail.PhysicalDescription, null),
                            asSpecimenInContainer = specimenDetail.ContainerIdentifier == null
                                ? null
                                : new SpecimenInContainer
                                {
                                    classCode = EntityClass.CONT,
                                    container = new Container
                                    {
                                        id = CreateIdentifierElement(specimenDetail.ContainerIdentifier)
                                    }
                                }
                        };
                    }


                    if (specimenDetail.PhysicalDetails != null)
                    {
                        var pqList = new List<PQ>();

                        foreach (var physicalDetail in specimenDetail.PhysicalDetails)
                        {
                            if (physicalDetail.WeightVolume != null)
                                pqList.Add(CreatePhysicalQuantity(physicalDetail.WeightVolume));

                            // Narrative
                            physicalDetailsList.Add
                            (
                                new List<string>
                                {
                                    physicalDetail.WeightVolume == null
                                        ? (physicalDetail.WeightVolume == null
                                            ? String.Empty
                                            : physicalDetail.WeightVolume.Units)
                                        : physicalDetail.WeightVolume.Units
                                }
                            );
                        }

                        specimen.specimenRole.specimenPlayingEntity.quantity = pqList.ToArray();
                    }
                }
            }

            return specimen;
        }

        # endregion

        #region Private Entry - Entries

        private static POCD_MT000040Entity CreateOrganisationEntity(IOrganisation organisation)
        {
            var org = new POCD_MT000040Entity
            {
                classCode = EntityClassRoot.ORG,
                asOrganizationPartOf = new[]
                {
                    new OrganizationPartOf
                    {
                        id = CreateIdentifierArray(CreateGuid()),
                        wholeEntity =
                            new Entity
                            {
                                asEntityIdentifier = organisation.Identifiers == null
                                    ? null
                                    : CreateEntityIdentifierArray(organisation.Identifiers)
                            }
                    }
                }
            };

            if (!String.IsNullOrEmpty(organisation.Name) ||
                (organisation.NameUsage != null && organisation.NameUsage.Value != OrganisationNameUsage.Undefined))
            {
                var on = new ON();
                if (!String.IsNullOrEmpty(organisation.Name))
                {
                    on.Text = new[] { organisation.Name };
                }

                if (organisation.NameUsage.HasValue && organisation.NameUsage.Value != OrganisationNameUsage.Undefined)
                {
                    on.use = new[]
                    {
                        (EntityNameUse) Enum.Parse(typeof(EntityNameUse),
                            organisation.NameUsage != OrganisationNameUsage.Undefined
                                ? organisation.NameUsage.GetAttributeValue<NameAttribute, string>(x => x.Code)
                                : String.Empty)
                    };
                }

                org.asOrganizationPartOf[0].wholeEntity.name = new[] { on };
            }

            if (!String.IsNullOrEmpty(organisation.Department))
                org.name = new[] { new ON { Text = new[] { organisation.Department } } };

            return org;
        }

        private static IEnumerable<POCD_MT000040Entry> CreateAdministeredImmunisations(
            ICollection<IImmunisation> immunisations)
        {
            var entryList = new List<POCD_MT000040Entry>();

            if (immunisations != null && immunisations.Any())
            {
                entryList = new List<POCD_MT000040Entry>();

                foreach (var imunisation in immunisations)
                {
                    //Build a list of administered immunisations sequence numbers
                    var relationships = new List<POCD_MT000040EntryRelationship>();
                    if (imunisation.SequenceNumber.HasValue)
                    {
                        relationships.Add(CreateEntryRelationshipSupply(ActClassSupply.SPLY,
                            x_ActRelationshipEntryRelationship.COMP,
                            x_DocumentSubstanceMood.EVN, false, null, null,
                            null, imunisation.SequenceNumber, false, null));
                    }

                    //Create the administration event, and pass in the immunisation sequence numbers
                    if (imunisation.Medicine != null)
                    {
                        entryList.Add(CreateEntrySubstanceAdministrationEvent(x_ActRelationshipEntry.COMP,
                            x_DocumentSubstanceMood.EVN, false, null,
                            null, null, imunisation.DateTime,
                            CreateCodedWithExtensionElement(
                                imunisation.Medicine),
                            null, relationships, null));
                    }
                }
            }

            return entryList;
        }

        private static POCD_MT000040Entry CreateEarliestDateForFiltering(ISO8601DateTime dateTime)
        {
            POCD_MT000040Entry entry = null;

            if (dateTime != null)
            {
                entry = new POCD_MT000040Entry
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor("103.15507", CodingSystem.NCTIS, "Earliest Date for Filtering",
                            null),
                        id = CreateIdentifierArray(CreateGuid(), null),
                        value = new ANY[] { CreateTimeStampElementIso(dateTime) }
                    }
                };
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateLatestDateForFiltering(ISO8601DateTime dateTime)
        {
            POCD_MT000040Entry entry = null;

            if (dateTime != null)
            {
                entry = new POCD_MT000040Entry
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor("103.15510", CodingSystem.NCTIS, "Latest Date for Filtering",
                            null),
                        id = CreateIdentifierArray(CreateGuid(), null),
                        value = new ANY[] { CreateTimeStampElementIso(dateTime) }
                    }
                };
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationAge(
            DateAccuracyIndicator dateAccuracyIndicator, String accuracyIndicatorType, String accuracyIndicatorTypeCode)
        {
            POCD_MT000040Entry entry = null;

            if (dateAccuracyIndicator != null)
            {
                var dateAccuracyIndicatorEnum = dateAccuracyIndicator.ConvertToEnum();

                var anyList = new List<ANY>();

                //person.DateOfBirthAccuracyIndicator
                anyList.Clear();
                anyList.Add(CreateCodeSystem(dateAccuracyIndicatorEnum.ToString(), null, null,
                    dateAccuracyIndicatorEnum.GetAttributeValue<NameAttribute, String>(x => x.Name), null, null));
                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor(accuracyIndicatorTypeCode, CodingSystem.NCTIS,
                        accuracyIndicatorType + " Accuracy Indicator", null),
                    null, anyList, null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationMothersOriginalFamilyName(
            IPersonName personName)
        {
            POCD_MT000040Entry entry = null;

            if (personName != null)
            {
                var anyList = new List<ANY>();

                //person.DateOfBirthAccuracyIndicator
                anyList.Clear();
                anyList.Add(CreatePersonName(personName));
                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor("103.10245", CodingSystem.NCTIS, "Mother's Original Family Name", null),
                    null, anyList, null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationSourceOfDeathNotification(
            SourceOfDeathNotification? sourceOfDeathNotification)
        {
            POCD_MT000040Entry entry = null;

            if (sourceOfDeathNotification.HasValue)
            {
                var anyList = new List<ANY>();

                anyList.Clear();
                anyList.Add(CreateConceptDescriptor(
                    sourceOfDeathNotification.GetAttributeValue<NameAttribute, String>(x => x.Code),
                    CodingSystem.HealthCareClientSourceOfDeathNotification,
                    sourceOfDeathNotification.GetAttributeValue<NameAttribute, String>(x => x.Name),
                    null
                ));
                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor("103.10243", CodingSystem.NCTIS, "Source of Death Notification", null),
                    null, anyList, null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationAge(Int32? age,
            AgeUnitOfMeasure? ageUnitOfMeasure)
        {
            POCD_MT000040Entry entry = null;

            if (age.HasValue)
            {
                var anyList = new List<ANY>();

                anyList.Clear();
                anyList.Add(new PQ
                {
                    value = age.Value.ToString(CultureInfo.InvariantCulture),
                    unit = ageUnitOfMeasure.HasValue
                        ? ageUnitOfMeasure.Value.GetAttributeValue<NameAttribute, String>(x => x.Code)
                        : AgeUnitOfMeasure.Year.GetAttributeValue<NameAttribute, String>(x => x.Code),
                });
                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor("103.20109", CodingSystem.NCTIS, "Age", null), null, anyList, null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationBirthPlurality(Int32? birthPlurality)
        {
            POCD_MT000040Entry entry = null;

            if (birthPlurality.HasValue)
            {
                var anyList = new List<ANY>();

                anyList.Clear();
                anyList.Add(CreateIntegerElement(birthPlurality, NullFlavor.NA, false));
                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor("103.16249", CodingSystem.NCTIS, "Birth Plurality", null), null, anyList,
                    null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationAgeAccuracyIndicator(
            Boolean? ageAccuracyIndicator)
        {
            POCD_MT000040Entry entry = null;

            if (ageAccuracyIndicator.HasValue)
            {
                var anyList = new List<ANY>();

                anyList.Clear();
                anyList.Add(CreateBoolean(ageAccuracyIndicator.Value, true));

                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor("103.16279", CodingSystem.NCTIS, "Age Accuracy Indicator", null), null,
                    anyList, null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateAdministrationObservationDateOfBirthCalculatedFromAge(
            Boolean? dateOfBirthCalculatedFromAge)
        {
            POCD_MT000040Entry entry = null;

            if (dateOfBirthCalculatedFromAge.HasValue)
            {
                var anyList = new List<ANY>();

                anyList.Clear();

                anyList.Add(CreateBoolean(dateOfBirthCalculatedFromAge.Value, true, NullFlavor.NA, false));

                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor("103.16233", CodingSystem.NCTIS, "Date of Birth is Calculated From Age",
                        null), null, anyList, null, null);
            }

            return entry;
        }

        private static POCD_MT000040Entry CreateEntryProcedureEvent(x_ActRelationshipEntry actRelationshipEntry,
            CD code,
            CdaInterval cdaInterval, List<POCD_MT000040EntryRelationship> relationships)
        {
            var entry = new POCD_MT000040Entry
            {
                typeCode = actRelationshipEntry,
                procedure = new POCD_MT000040Procedure
                {
                    classCode = ActClass.PROC,
                    moodCode = x_DocumentProcedureMood.EVN,
                    id = CreateIdentifierArray
                    (
                        CreateGuid(),
                        null
                    ),
                    code = code
                }
            };

            if (cdaInterval != null)
            {
                entry.procedure.effectiveTime = CreateIntervalTimestamp(cdaInterval);
            }

            if (relationships != null)
            {
                entry.procedure.entryRelationship = relationships.ToArray();
            }

            return (entry);
        }

        private static POCD_MT000040Entry CreateEntryProcedureEvent(CD code)
        {
            var entry = new POCD_MT000040Entry
            {
                procedure = new POCD_MT000040Procedure
                {
                    classCode = ActClass.PROC,
                    moodCode = x_DocumentProcedureMood.EVN,
                    id = CreateIdentifierArray
                    (
                        CreateGuid(),
                        null
                    ),
                    code = code
                }
            };

            return (entry);
        }

        /// <summary>
        /// Creates a MedicareDVAFundedService
        /// </summary>
        /// <param name="medicareDVAFundedService">The MedicareDVAFundedService </param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateMedicareDVAFundedService(
            MedicareDVAFundedService medicareDVAFundedService)
        {
            POCD_MT000040Entry entry = null;

            POCD_MT000040Performer2 serviceProvider = null;
            POCD_MT000040Participant2 serviceRequester = null;

            if (medicareDVAFundedService != null)
            {
                var entryRelationship = new List<POCD_MT000040EntryRelationship>();

                if (medicareDVAFundedService.ServiceInHospitalIndicator.HasValue)
                    entryRelationship.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.SUBJ,
                        false,
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.ServiceInHospitalIndicator)),
                        new List<ANY>
                            {CreateBoolean(medicareDVAFundedService.ServiceInHospitalIndicator.Value, true)}));


                if (medicareDVAFundedService.ServiceProvider != null)
                {
                    serviceProvider = CreatePerformer(medicareDVAFundedService.ServiceProvider);
                }

                if (medicareDVAFundedService.ServiceRequester != null)
                {
                    serviceRequester = CreateParticipant2(medicareDVAFundedService.ServiceRequester);
                }

                entry = new POCD_MT000040Entry
                {
                    encounter = new POCD_MT000040Encounter
                    {
                        classCode = ActClass.ENC,
                        moodCode = x_DocumentEncounterMood.EVN,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        code = CreateConceptDescriptor(medicareDVAFundedService.MedicareMBSDVAItem),
                        effectiveTime = medicareDVAFundedService.DateOfService != null
                            ? CreateIntervalTimestamp(medicareDVAFundedService.DateOfService, null)
                            : null,
                        performer = serviceProvider != null ? new[] { serviceProvider } : null,
                        participant = serviceRequester != null ? new[] { serviceRequester } : null,
                        entryRelationship = entryRelationship.Any() ? entryRelationship.ToArray() : null,
                        reference = CreatesReferanceLink(
                            CreateCodableText(MedicareOverviewSections.MedicareDVAFundedServicesDocumentLink),
                            medicareDVAFundedService.MedicareDVAFundedServicesDocumentLink)
                    }
                };
            }

            return entry;
        }

        /// <summary>
        /// Creates a Reference Link
        /// </summary>
        /// <returns>POCD_MT000040Reference</returns>
        internal static POCD_MT000040Reference[] CreatesReferanceLink(string imageLocation, MediaType? mediaType)
        {
            POCD_MT000040Reference[] referanceList = null;

            if (!imageLocation.IsNullOrEmptyWhitespace())
            {
                referanceList = new[]
                {
                    new POCD_MT000040Reference
                    {
                        typeCode = x_ActRelationshipExternalReference.REFR,
                        seperatableInd = CreateBoolean(true, true),
                        externalAct = new POCD_MT000040ExternalAct
                        {
                            classCode = ActClassRoot.ACT,
                            moodCode = ActMood.EVN,
                            moodCodeSpecified = true,
                            text = new ED
                            {
                                mediaType = mediaType.HasValue
                                    ? mediaType.Value.GetAttributeValue<NameAttribute, string>(x => x.Name)
                                    : null,
                                reference = new TEL
                                {
                                    value = imageLocation
                                }
                            }
                        }
                    }
                };
            }

            return referanceList;
        }

        /// <summary>
        /// Creates a Referance Link
        /// </summary>
        /// <returns>POCD_MT000040Reference</returns>
        internal static POCD_MT000040Reference[] CreatesReferanceLink(ICodableText sectionCode, Link documentLink)
        {
            POCD_MT000040Reference[] referanceList = null;

            if (sectionCode != null && documentLink != null)
            {
                referanceList = new[]
                {
                    new POCD_MT000040Reference
                    {
                        typeCode = x_ActRelationshipExternalReference.REFR,
                        seperatableInd = CreateBoolean(true, true),
                        externalDocument = new POCD_MT000040ExternalDocument
                        {
                            classCode = ActClassDocument.DOC,
                            moodCode = ActMood.EVN,
                            id = !documentLink.DocumentIdentifier.IsNullOrEmptyWhitespace()
                                ? new[]
                                {
                                    CreateIdentifierElement(documentLink.DocumentIdentifier,
                                        documentLink.DocumentIdentifierExtension, null, null, null)
                                }
                                : null,
                            templateId = new[]
                            {
                                !documentLink.TemplateId.HasValue
                                    ? null
                                    : CreateIdentifierElement
                                    (
                                        documentLink.TemplateId.Value.GetAttributeValue<NameAttribute, string>(x =>
                                            x.TemplateIdentifier),
                                        documentLink.TemplateId.Value.GetAttributeValue<NameAttribute, string>(x =>
                                            x.Version),
                                        null
                                    )
                            }
                        }
                    },
                    new POCD_MT000040Reference
                    {
                        typeCode = x_ActRelationshipExternalReference.REFR,
                        seperatableInd = CreateBoolean(true, true),
                        externalAct = new POCD_MT000040ExternalAct
                        {
                            id = CreateIdentifierArray(documentLink.RepositoryIdentifier, null),
                            classCode = ActClassRoot.ACT,
                            moodCode = ActMood.EVN,
                            moodCodeSpecified = true,
                            code = CreateConceptDescriptor("10", CodingSystem.PCEHRAssignedIdentifierRepository,
                                CodingSystem.PCEHRAssignedIdentifierRepository.GetAttributeValue<NameAttribute, string>(
                                    x => x.Title), null)
                        }
                    }
                };

            }

            return referanceList;
        }

        /// <summary>
        /// Creates a Vaccine Administration Entry
        /// </summary>
        /// <param name="vaccineAdministrationEntry">The Vaccine Administration Entry </param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateVaccineAdministrationEntry(
            VaccineAdministrationEntry vaccineAdministrationEntry)
        {
            POCD_MT000040Entry entry = null;
            List<POCD_MT000040EntryRelationship> entryRelationship = null;

            if (vaccineAdministrationEntry.VaccineDoseNumber.HasValue)
            {
                entryRelationship = new List<POCD_MT000040EntryRelationship>();
                entryRelationship.Add(CreateEntryRelationshipSupply(ActClassSupply.SPLY,
                    x_ActRelationshipEntryRelationship.COMP,
                    x_DocumentSubstanceMood.EVN,
                    false,
                    null,
                    null,
                    null,
                    vaccineAdministrationEntry.VaccineDoseNumber.Value,
                    false,
                    null));
            }

            entry = new POCD_MT000040Entry();
            entry.substanceAdministration = new POCD_MT000040SubstanceAdministration
            {
                moodCode = x_DocumentSubstanceMood.EVN,
                classCode = ActClass.SBADM,
                id = CreateIdentifierArray(CreateGuid(), null, null),
                effectiveTime = vaccineAdministrationEntry.DateVaccinationReceived != null
                    ? new[] { new SXCM_TS { value = vaccineAdministrationEntry.DateVaccinationReceived.ToString() } }
                    : null,
                consumable = new POCD_MT000040Consumable
                {
                    manufacturedProduct = new POCD_MT000040ManufacturedProduct
                    {
                        manufacturedMaterial = new POCD_MT000040Material
                        {
                            code = vaccineAdministrationEntry.VaccineType != null
                                ? CreateCodedWithExtensionElement(vaccineAdministrationEntry.VaccineType)
                                : null,
                            asIngredient = vaccineAdministrationEntry.MedicareAntigenCode != null
                                ? CreateMedicareAntigenCodes(vaccineAdministrationEntry.MedicareAntigenCode)
                                : null
                        }
                    }
                },
                entryRelationship = entryRelationship != null ? entryRelationship.ToArray() : null
            };
            return entry;
        }

        /// <summary>
        /// Creates a Pharmaceutical Benefit Items Component
        /// </summary>
        /// <param name="pharmaceuticalBenefitItem">The PharmaceuticalBenefitItem </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Entry CreatePharmaceuticalBenefitItem(
            PharmaceuticalBenefitItem pharmaceuticalBenefitItem, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Entry entry = null;

            if (pharmaceuticalBenefitItem != null)
            {
                var entryRelationship = new List<POCD_MT000040EntryRelationship>();

                if (!pharmaceuticalBenefitItem.ItemFormAndStrength.IsNullOrEmptyWhitespace())
                    entryRelationship.Add(CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.SUBJ,
                        false,
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.ItemFormAndStrength)),
                        new List<ANY> { CreateStructuredText(pharmaceuticalBenefitItem.ItemFormAndStrength, null) }));

                if (pharmaceuticalBenefitItem.DateOfPrescribing != null ||
                    pharmaceuticalBenefitItem.NumberOfRepeats.HasValue)
                {
                    entryRelationship.Add(CreateEntryRelationshipSubstanceAdministration(
                        x_ActRelationshipEntryRelationship.REFR,
                        ActClass.SBADM,
                        x_DocumentSubstanceMood.RQO,
                        pharmaceuticalBenefitItem.DateOfPrescribing,
                        pharmaceuticalBenefitItem.NumberOfRepeats));
                }

                entry = new POCD_MT000040Entry
                {
                    supply = new POCD_MT000040Supply
                    {
                        classCode = ActClassSupply.SPLY,
                        moodCode = x_DocumentSubstanceMood.EVN,
                        code = CreateConceptDescriptor(
                            CreateCodableText(MedicareOverviewSections.PharmaceuticalBenefitsItem)),
                        effectiveTime = pharmaceuticalBenefitItem.DateOfSupply != null
                            ? new[] { new SXCM_TS { value = pharmaceuticalBenefitItem.DateOfSupply.ToString() } }
                            : null,
                        quantity = pharmaceuticalBenefitItem.Quantity.HasValue
                            ? CreatePhysicalQuantity(null,
                                pharmaceuticalBenefitItem.Quantity.Value.ToString(CultureInfo.InvariantCulture))
                            : null,
                        product = new POCD_MT000040Product
                        {
                            typeCode = ParticipationType.PRD,
                            manufacturedProduct = new POCD_MT000040ManufacturedProduct
                            {
                                classCode = RoleClassManufacturedProduct.MANU,
                                manufacturedMaterial = new POCD_MT000040Material()
                                {
                                    code = pharmaceuticalBenefitItem.PBSRPBSItemCode == null
                                        ? null
                                        : CreateCodedWithExtensionElement(CreateCodableText(
                                            pharmaceuticalBenefitItem.PBSRPBSItemCode, CodingSystem.PBSCode,
                                            pharmaceuticalBenefitItem.ItemGenericName, null)),
                                    name = pharmaceuticalBenefitItem.Brand.IsNullOrEmptyWhitespace()
                                        ? null
                                        : new EN { Text = new[] { pharmaceuticalBenefitItem.Brand } }
                                },
                                manufacturerOrganization = new POCD_MT000040Organization
                                {
                                    id = pharmaceuticalBenefitItem.PBSRPBSManufacturerCode == null
                                        ? null
                                        : CreateIdentifierArray("1.2.36.1.2001.1005.23",
                                            pharmaceuticalBenefitItem.PBSRPBSManufacturerCode, null)
                                },
                            }
                        },
                        reference = CreatesReferanceLink(
                            CreateCodableText(MedicareOverviewSections.PharmaceuticalBenefitItemsDocumentLink),
                            pharmaceuticalBenefitItem.PharmaceuticalBenefitItemDocumentLink),
                        entryRelationship = entryRelationship.ToArray()
                    }
                };
            }

            return entry;
        }

        /// <summary>
        /// Physical Measurement Body Mass Index
        /// </summary>
        /// <param name="bodyMassIndex">Body Mass Index</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateEntryBodyMassIndex(PhysicalMeasurementBodyMassIndex bodyMassIndex)
        {
            POCD_MT000040Entry entry = null;
            var anyList = new List<ANY>();
            var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

            if (bodyMassIndex != null)
            {
                if (bodyMassIndex.BodyMassIndex != null)
                {
                    anyList.Add(CreatePhysicalQuantity(bodyMassIndex.BodyMassIndex));
                }

                if (bodyMassIndex.Comment != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Comment)),
                        CreateStructuredText(bodyMassIndex.Comment, null),
                        null));
                }

                if (bodyMassIndex.Formula != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Formula)),
                        CreateStructuredText(bodyMassIndex.Formula, null),
                        null));
                }

                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.BodyMassIndex)),
                    new[] { CreateCodedWithExtensionElement(bodyMassIndex.Method) },
                    null,
                    bodyMassIndex.BodyMassIndexDateTime,
                    anyList,
                    null,
                    entryRelationshipList,
                    null,
                    StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name),
                    bodyMassIndex.BodyMassIndexNormalStatus,
                    bodyMassIndex.BodyMassIndexReferenceRangeDetails,
                    new Identifier
                    {
                        Root = "1.2.36.1.2001.1001.101.102.16856"
                    },
                    bodyMassIndex.BodyMassIndexInstanceIdentifier);


                if (bodyMassIndex.InformationProvider != null)
                {
                    entry.observation.author = new[] { CreateInformationProvider(bodyMassIndex.InformationProvider) };
                }
            }

            return entry;
        }

        /// <summary>
        /// Physical Measurement Body Height Length
        /// </summary>
        /// <param name="bodyHeightLength">Body Weight</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateEntryBodyHeightLength(
            PhysicalMeasurementBodyHeightLength bodyHeightLength)
        {
            POCD_MT000040Entry entry = null;
            var anyList = new List<ANY>();
            var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

            if (bodyHeightLength != null)
            {
                if (bodyHeightLength.HeightLength != null)
                {
                    anyList.Add(CreatePhysicalQuantity(bodyHeightLength.HeightLength));
                }

                if (bodyHeightLength.Comment != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Comment)),
                        CreateStructuredText(bodyHeightLength.Comment, null),
                        null));
                }

                if (bodyHeightLength.Position != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipObservation(
                        x_ActRelationshipEntryRelationship.COMP,
                        null, null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Position)),
                        new List<ANY> { CreateConceptDescriptor(null, null, null, bodyHeightLength.Position) },
                        StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name), NullFlavor.UNK));

                }

                if (bodyHeightLength.ConfoundingFactor != null && bodyHeightLength.ConfoundingFactor.Any())
                {
                    foreach (var confoundingFactor in bodyHeightLength.ConfoundingFactor)
                    {
                        if (confoundingFactor != null)
                            entryRelationshipList.Add(CreateEntryRelationshipACT(
                                x_ActRelationshipEntryRelationship.SUBJ,
                                x_ActClassDocumentEntryAct.INFRM,
                                x_DocumentActMood.EVN,
                                false,
                                CreateConceptDescriptor(
                                    CreateCodableText(PhysicalMeasurementDocumentSections.ConfoundingFactorName)),
                                CreateStructuredText(confoundingFactor, null),
                                null
                            ));
                    }
                }

                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.BodyHeightLength)),
                    null,
                    null,
                    bodyHeightLength.BodyHeightLengthDateTime,
                    anyList,
                    null,
                    entryRelationshipList,
                    null,
                    StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name),
                    bodyHeightLength.HeightLengthNormalStatus,
                    bodyHeightLength.HeightLengthReferenceRangeDetails,
                    new Identifier
                    {
                        Root = "1.2.36.1.2001.1001.101.102.16123"
                    },
                    bodyHeightLength.BodyHeightLengthInstanceIdentifier);


                if (bodyHeightLength.Device != null)
                {
                    entry.observation.participant = new[] { CreateDeviceParticipant(bodyHeightLength.Device) };
                }

                if (bodyHeightLength.InformationProvider != null)
                {
                    entry.observation.author = new[] { CreateInformationProvider(bodyHeightLength.InformationProvider) };
                }
            }

            return entry;
        }

        /// <summary>
        /// Creates a Body Weight item
        /// </summary>
        /// <param name="bodyWeight">Body Weight</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateEntryBodyWeight(PhysicalMeasurementBodyWeight bodyWeight)
        {
            POCD_MT000040Entry entry = null;
            var anyList = new List<ANY>();
            var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

            if (bodyWeight != null)
            {
                if (bodyWeight.Weight != null)
                {
                    anyList.Add(CreatePhysicalQuantity(bodyWeight.Weight));
                }

                if (bodyWeight.Comment != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Comment)),
                        CreateStructuredText(bodyWeight.Comment, null),
                        null));
                }

                if (bodyWeight.WeightEstimationFormula != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.ACT,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(
                            CreateCodableText(PhysicalMeasurementDocumentSections.WeightEstimationFormula)),
                        CreateEncapsulatedData(bodyWeight.WeightEstimationFormula),
                        null));
                }

                if (!bodyWeight.StateOfDress.IsNullOrEmptyWhitespace())
                {
                    entryRelationshipList.Add(CreateEntryRelationshipObservation(
                        x_ActRelationshipEntryRelationship.COMP,
                        null, null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.StateOfDress)),
                        new List<ANY> { CreateConceptDescriptor(null, null, null, bodyWeight.StateOfDress) },
                        StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name), NullFlavor.NA));

                }

                if (bodyWeight.Pregnant.HasValue)
                {

                    entryRelationshipList.Add(CreateEntryRelationshipObservation(
                        x_ActRelationshipEntryRelationship.COMP,
                        bodyWeight.Pregnant.Value, bodyWeight.Pregnant.Value,
                        CreateConceptDescriptor(CreateCodableText(
                            PhysicalMeasurementDocumentSections.ASSERTION.GetAttributeValue<NameAttribute, string>(x =>
                                x.Code),
                            CodingSystem.ActCode.GetAttributeValue<NameAttribute, string>(x => x.Code), null, null,
                            null, null)),
                        new List<ANY>
                            {CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Pregnant))},
                        StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name), NullFlavor.NA));

                }

                if (bodyWeight.ConfoundingFactor != null && bodyWeight.ConfoundingFactor.Any())
                {
                    foreach (var confoundingFactor in bodyWeight.ConfoundingFactor)
                    {
                        if (confoundingFactor != null)
                            entryRelationshipList.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.COMP,
                                null, null,
                                CreateConceptDescriptor(
                                    CreateCodableText(PhysicalMeasurementDocumentSections.ConfoundingFactorName)),
                                new List<ANY> { CreateConceptDescriptor(confoundingFactor) },
                                StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name),
                                NullFlavor.NA
                            ));
                    }
                }


                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.BodyWeight)),
                    null,
                    null,
                    bodyWeight.BodyWeightDateTime,
                    anyList,
                    null,
                    entryRelationshipList,
                    null,
                    StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name),
                    bodyWeight.WeightNormalStatus,
                    bodyWeight.WeightReferenceRangeDetails,
                    new Identifier
                    {
                        Root = "1.2.36.1.2001.1001.101.102.16124"
                    },
                    bodyWeight.BodyWeightInstanceIdentifier);


                if (bodyWeight.Device != null)
                {
                    entry.observation.participant = new[] { CreateDeviceParticipant(bodyWeight.Device) };
                }

                if (bodyWeight.InformationProvider != null)
                {
                    entry.observation.author = new[] { CreateInformationProvider(bodyWeight.InformationProvider) };
                }
            }

            return entry;
        }

        /// <summary>
        /// Creates a Head Circumference item
        /// </summary>
        /// <param name="headCircumference">Head Circumference</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateEntryHeadCircumference(HeadCircumference headCircumference)
        {
            POCD_MT000040Entry entry = null;
            var anyList = new List<ANY>();
            var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

            if (headCircumference != null)
            {
                if (headCircumference.Circumference != null)
                {
                    anyList.Add(CreatePhysicalQuantity(headCircumference.Circumference));
                }

                if (headCircumference.Comment != null)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        null,
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.Comment)),
                        CreateStructuredText(headCircumference.Comment, null),
                        null));
                }

                if (headCircumference.ConfoundingFactor != null && headCircumference.ConfoundingFactor.Any())
                {
                    foreach (var confoundingFactor in headCircumference.ConfoundingFactor)
                    {
                        if (confoundingFactor != null)
                            entryRelationshipList.Add(CreateEntryRelationshipObservation(
                                x_ActRelationshipEntryRelationship.SUBJ,
                                null, null,
                                CreateConceptDescriptor(
                                    CreateCodableText(PhysicalMeasurementDocumentSections.ConfoundingFactorName)),
                                new List<ANY> { CreateConceptDescriptor(confoundingFactor) },
                                StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name),
                                NullFlavor.NA
                            ));
                    }
                }

                entry = CreateEntryObservation(x_ActRelationshipEntry.DRIV,
                    CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.HeadCircumference)),
                    null,
                    new[]
                    {
                        CreateConceptDescriptor(CreateCodableText(PhysicalMeasurementDocumentSections.HeadStructure))
                    },
                    headCircumference.BodyPartCircumferenceDateTime,
                    anyList,
                    null,
                    entryRelationshipList,
                    null,
                    StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name),
                    headCircumference.CircumferenceNormalStatus,
                    headCircumference.CircumferenceReferenceRangeDetails,
                    new Identifier
                    {
                        Root = "1.2.36.1.2001.1001.101.102.16808"
                    },
                    new Identifier { Root = CreateGuid() });


                if (headCircumference.Device != null)
                {
                    entry.observation.participant = new[] { CreateDeviceParticipant(headCircumference.Device) };
                }

                if (headCircumference.InformationProvider != null)
                {
                    entry.observation.author = new[] { CreateInformationProvider(headCircumference.InformationProvider) };

                    entry.observation.participant = new[]
                    {
                        new POCD_MT000040Participant2
                        {
                            participantRole = new POCD_MT000040ParticipantRole
                            {
                                playingDevice = new POCD_MT000040Device
                                {
                                    manufacturerModelName = new SC
                                    {
                                        Text = new[] {"test"}
                                    }
                                }
                            }
                        }
                    };
                }
            }

            return entry;
        }

        /// <summary>
        /// Creates an Achievements
        /// </summary>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateAchievement(Achievement achievements)
        {
            var entry = new POCD_MT000040Entry();

            var code = CreateCodedWithExtensionElement(
                CreateCodableText(ConsumerEnteredAchievementsSections.Achievement));

            if (!achievements.AchievementTopic.IsNullOrEmptyWhitespace())
                code.originalText = CreateEncapsulatedData(achievements.AchievementTopic);

            entry.observation = new POCD_MT000040Observation
            {
                id = CreateIdentifierArray(CreateGuid(), null),
                classCode = ActClassObservation.OBS,
                moodCode = x_ActMoodDocumentObservation.EVN,
                code = code,
                templateId =
                    CreateIdentifierArray(
                        ConsumerEnteredAchievementsSections.Achievement.GetAttributeValue<NameAttribute, string>(x =>
                            x.Identifier), null),
                value = !achievements.AchievementDescription.IsNullOrEmptyWhitespace()
                    ? new ANY[] { CreateStructuredText(achievements.AchievementDescription, null) }
                    : null,
                effectiveTime = achievements.AchievementDate != null
                    ? CreateIntervalTimestamp(achievements.AchievementDate, null)
                    : null,
                author = achievements.InformationProvider != null
                    ? new[] { CreateInformationProvider(achievements.InformationProvider) }
                    : null,
                statusCode =
                    CreateCodeSystem(StatusCode.Completed.GetAttributeValue<NameAttribute, string>(x => x.Name), null,
                        null, null, null, null)
            };


            return entry;
        }

        /// <summary>
        /// Creates a Vaccine Cancellation Entry
        /// </summary>
        /// <param name="vaccineAdministrationEntry">The Vaccine Cancellation Entry</param>
        /// <param name="entryRelationshipList">EntryRelationshipList</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateVaccineCancellationEntry(
            VaccineCancellationEntry vaccineAdministrationEntry,
            List<POCD_MT000040EntryRelationship> entryRelationshipList)
        {
            POCD_MT000040Entry entry = null;

            if (entryRelationshipList == null)
            {
                entryRelationshipList = new List<POCD_MT000040EntryRelationship>();
            }

            if (vaccineAdministrationEntry.VaccineDoseNumber.HasValue)
            {
                entryRelationshipList.Add(CreateEntryRelationshipSupply(ActClassSupply.SPLY,
                    x_ActRelationshipEntryRelationship.COMP,
                    x_DocumentSubstanceMood.EVN,
                    false,
                    null,
                    null,
                    null,
                    vaccineAdministrationEntry.VaccineDoseNumber.Value,
                    false,
                    null));
            }

            entry = new POCD_MT000040Entry();
            entry.substanceAdministration = new POCD_MT000040SubstanceAdministration
            {
                statusCode = new CS { code = "cancelled" },
                moodCode = x_DocumentSubstanceMood.EVN,
                classCode = ActClass.SBADM,
                id = CreateIdentifierArray(CreateGuid(), null, null),
                effectiveTime = vaccineAdministrationEntry.DateVaccinationCancelled != null
                    ? new[] { new SXCM_TS { value = vaccineAdministrationEntry.DateVaccinationCancelled.ToString() } }
                    : null,
                consumable = new POCD_MT000040Consumable
                {
                    manufacturedProduct = new POCD_MT000040ManufacturedProduct
                    {
                        manufacturedMaterial = new POCD_MT000040Material
                        {
                            code = vaccineAdministrationEntry.VaccineType != null
                                ? CreateCodedWithExtensionElement(vaccineAdministrationEntry.VaccineType)
                                : null,
                            asIngredient = CreateMedicareAntigenCodes(vaccineAdministrationEntry.MedicareAntigenCode)
                        }
                    }
                },
                entryRelationship = entryRelationshipList.ToArray()
            };
            return entry;
        }

        /// <summary>
        /// Create Australian Organ Donor Register Entry
        /// </summary>
        /// <param name="australianOrganDonorRegisterEntry">The Australian Organ Donor Register Entry</param>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateAustralianOrganDonorRegisterEntry(
            AustralianOrganDonorRegisterEntry australianOrganDonorRegisterEntry)
        {
            POCD_MT000040Entry entry = null;

            if (australianOrganDonorRegisterEntry != null)
            {
                var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();
                var components = new List<POCD_MT000040Component4>();

                if (australianOrganDonorRegisterEntry.DonationDecision.HasValue)
                {
                    entryRelationshipList.Add(CreateEntryRelationshipObservation(
                        x_ActRelationshipEntryRelationship.SUBJ,
                        null,
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.DonationDecision)),
                        new List<ANY> { CreateBoolean(australianOrganDonorRegisterEntry.DonationDecision.Value, true) }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails != null &&
                    australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.BoneTissueIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.BoneTissueIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.BoneTissueIndicator
                                    .Value,
                                true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.EyeTissueIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.EyeTissueIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.EyeTissueIndicator
                                    .Value, true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.HeartIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.HeartIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.HeartIndicator.Value,
                                true)
                        }
                    ));
                }


                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.HeartValveIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.HeartValveIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.HeartValveIndicator
                                    .Value, true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.KidneyIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.KidneyIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.KidneyIndicator.Value,
                                true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.LiverIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.LiverIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.LiverIndicator.Value,
                                true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.LungsIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.LungsIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.LungsIndicator.Value,
                                true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.PancreasIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.PancreasIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.PancreasIndicator.Value,
                                true)
                        }
                    ));
                }

                if (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.SkinTissueIndicator.HasValue)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.SkinTissueIndicator)),
                        new List<ANY>
                        {
                            CreateBoolean(
                                australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.SkinTissueIndicator
                                    .Value, true)
                        }
                    ));
                }

                if ((australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.BoneTissueIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.EyeTissueIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.HeartIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.HeartValveIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.KidneyIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.LiverIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.LungsIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.PancreasIndicator.HasValue) ||
                    (australianOrganDonorRegisterEntry.OrganAndTissueDonationDetails.SkinTissueIndicator.HasValue))
                {
                    entryRelationshipList.Add(CreateEntryRelationshipOrganiserObservation(
                        x_ActRelationshipEntryRelationship.SUBJ,
                        CreateConceptDescriptor(
                            CreateCodableText(MedicareOverviewSections.OrganAndTissueDonationDetails)),
                        StatusCode.Completed.GetAttributeValue<NameAttribute, string>(x => x.Name),
                        components)

                    );
                }

                entry = CreateEntryObservation(x_ActRelationshipEntry.COMP,
                    CreateConceptDescriptor(
                        CreateCodableText(MedicareOverviewSections.AustralianOrganDonorRegisterEntry)),
                    null,
                    australianOrganDonorRegisterEntry.DateOfInitialRegistration,
                    null,
                    null,
                    entryRelationshipList);
            }

            return entry;
        }

        /// <summary>
        /// Creates an Prescription Item Entries
        /// </summary>
        /// <returns>POCD_MT000040Component3</returns>
        internal static List<POCD_MT000040Entry> CreatePrescriptionItemEntries(IPCEHRPrescriptionItemView item)
        {
            var entries = new List<POCD_MT000040Entry>();

            // Begin DateTime Prescription Expires
            if (item != null)
            {
                var observation = CreateEntryObservation
                (
                    null,
                    CreateConceptDescriptor
                    (
                        EtpRecordSections.DateTimePrescriptionExpires.GetAttributeValue<NameAttribute, String>(x =>
                            x.Code),
                        CodingSystem.NCTIS,
                        EtpRecordSections.DateTimePrescriptionExpires.GetAttributeValue<NameAttribute, String>(x =>
                            x.Name),
                        null
                    ),
                    item.DateTimePrescriptionExpires != null
                        ? CreateIntervalTimestamp(null, null, null, null, item.DateTimePrescriptionExpires.ToString(),
                            null)
                        : null,
                    null,
                    null,
                    null
                );

                // allow null flavour if DateTime Prescription Expires omitted
                if (item.DateTimePrescriptionExpires == null)
                    observation.observation.effectiveTime = new IVL_TS
                    { nullFlavor = NullFlavor.NA, nullFlavorSpecified = true };

                entries.Add(observation);

                var relationships = new List<POCD_MT000040EntryRelationship>();

                //// Formula
                if (!item.Formula.IsNullOrEmptyWhitespace())
                {
                    relationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                EtpRecordSections.Formula.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                CodingSystem.NCTIS,
                                EtpRecordSections.Formula.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                null
                            ),
                            CreateEncapsulatedData(item.Formula),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Supply
                var entryRelationship = new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    supply = new POCD_MT000040Supply
                    {
                        moodCode = x_DocumentSubstanceMood.RQO,
                        id = CreateIdentifierArray(CreateGuid(), null),
                        independentInd = new BL { value = false, valueSpecified = true },
                        effectiveTime = item.DispensingInformation != null &&
                                        item.DispensingInformation.MinimumIntervalBetweenRepeats != null
                            ? new[]
                            {
                                // MinimumIntervalBetweenRepeats
                                new PIVL_TS
                                {
                                    period = new PQ
                                    {
                                        value = item.DispensingInformation.MinimumIntervalBetweenRepeats.IntervalWidth
                                            .Value,
                                        unit = item.DispensingInformation.MinimumIntervalBetweenRepeats.IntervalWidth
                                            .Unit.GetAttributeValue<NameAttribute, string>(x => x.Code)
                                    }
                                }
                            }
                            : null,
                        // Begin Brand Substitute allowed
                        subjectOf2 =
                            item.DispensingInformation != null &&
                            item.DispensingInformation.BrandSubstitutionPermitted.HasValue
                                ? CreateBrandSubstituteAllowed
                                (
                                    ActClass.SUBST,
                                    ActMood.PERM,
                                    CreateCodedWithExtensionElement
                                    (
                                        "TE",
                                        CodingSystem.HL7SubstanceAdminSubstitution,
                                        "Therapeutic",
                                        null,
                                        null,
                                        null
                                    )
                                )
                                : null,
                        entryRelationship = item.DispensingInformation != null &&
                                            !item.DispensingInformation.QuantityDescription.IsNullOrEmptyWhitespace()
                            ? new[]
                            {
                                CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.INFRM,
                                    x_DocumentActMood.INT,
                                    false,
                                    CreateConceptDescriptor(CreateCodableText(
                                        EtpRecordSections.QuantityDescription.GetAttributeValue<NameAttribute, String>(
                                            x => x.Code),
                                        CodingSystem.SNOMED,
                                        EtpRecordSections.QuantityDescription.GetAttributeValue<NameAttribute, String>(
                                            x => x.Name), null)),
                                    CreateEncapsulatedData(item.DispensingInformation.QuantityDescription),
                                    CreateIdentifierArray(new UniqueId())
                                )
                            }
                            : null

                    },
                };

                relationships.Add(entryRelationship);

                // Reason For Therapeutic Good
                if (item.ClinicalIndication != null)
                {
                    relationships.Add
                    (
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.RSON,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                EtpRecordSections.ReasonForTherapeuticGood.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                EtpRecordSections.ReasonForTherapeuticGood.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            CreateEncapsulatedData(item.ClinicalIndication),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Additional comments
                if (!item.Comment.IsNullOrEmptyWhitespace())
                {
                    relationships.Add(
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.EVN,
                            false,
                            CreateConceptDescriptor
                            (
                                EtpRecordSections.AdditionalComments.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Code),
                                CodingSystem.NCTIS,
                                EtpRecordSections.AdditionalComments.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            CreateEncapsulatedData(item.Comment),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Additional comments
                if (!item.TherapeuticGoodStrength.IsNullOrEmptyWhitespace())
                {
                    relationships.Add(
                        CreateEntryRelationshipACT
                        (
                            x_ActRelationshipEntryRelationship.COMP,
                            x_ActClassDocumentEntryAct.INFRM,
                            x_DocumentActMood.RQO,
                            false,
                            CreateConceptDescriptor
                            (
                                "103.16769.170.1.1",
                                CodingSystem.NCTIS,
                                EtpRecordSections.TherapeuticGoodStrength.GetAttributeValue<NameAttribute, String>(x =>
                                    x.Name),
                                null
                            ),
                            CreateEncapsulatedData(item.TherapeuticGoodStrength),
                            CreateIdentifierArray(new UniqueId())
                        )
                    );
                }

                // Begin Prescription Item entry
                var entry = CreateEntrySubstanceAdministrationEvent
                (
                    null,
                    x_DocumentSubstanceMood.RQO,
                    true,
                    null,
                    null,
                    null,
                    null,
                    CreateCodedWithExtensionElement
                    (
                        item.TherapeuticGoodId
                    ),
                    item.DispensingInformation != null && item.DispensingInformation.MaximumNumberOfRepeats.HasValue
                        ? item.DispensingInformation.MaximumNumberOfRepeats + 1
                        : null,
                    item.TherapeuticGoodGenericName,
                    item.Form,
                    item.Route,
                    item.PBSManufacturerCode,
                    relationships,
                    null
                );

                // Prescription Item Id
                // root = Data Component Register - Set Prescription Item Identifier 
                if (item.PrescriptionItemIdentifier != null)
                    entry.substanceAdministration.id = CreateIdentifierArray(item.PrescriptionItemIdentifier);

                // Directions
                if (!item.Directions.IsNullOrEmptyWhitespace())
                {
                    entry.substanceAdministration.text = CreateEncapsulatedData(item.Directions);
                }

                entries.Add(entry);
            }

            return entries;
        }

        /// <summary>
        /// Creates an Prescription Item Entries
        /// </summary>
        /// <returns>POCD_MT000040Entry</returns>
        internal static POCD_MT000040Entry CreateDispenseItemEntryRelationship(PCEHRDispenseItem item)
        {
            var relationships = new List<POCD_MT000040EntryRelationship>();

            // Supply
            if (item.DateTimeOfDispenseEvent != null)
                relationships.Add(new POCD_MT000040EntryRelationship
                {
                    typeCode = x_ActRelationshipEntryRelationship.COMP,
                    sequenceNumber = item.NumberOfThisDispense.HasValue
                        ? new INT { value = item.NumberOfThisDispense.Value.ToString(CultureInfo.InvariantCulture) }
                        : null,
                    supply = new POCD_MT000040Supply
                    {
                        effectiveTime = new[] { new SXCM_TS { value = item.DateTimeOfDispenseEvent.ToString() } },
                        moodCode = x_DocumentSubstanceMood.EVN,
                        independentInd = new BL { value = false, valueSpecified = true },
                        product = new POCD_MT000040Product
                        {
                            manufacturedProduct = new POCD_MT000040ManufacturedProduct
                            {
                                manufacturedMaterial = new POCD_MT000040Material
                                {
                                    code = item.TherapeuticGoodId != null
                                        ? CreateCodedWithExtensionElement(item.TherapeuticGoodId)
                                        : null,
                                    name = item.TherapeuticGoodGenericName.IsNullOrEmptyWhitespace()
                                        ? null
                                        : new EN { Text = new[] { item.TherapeuticGoodGenericName } },
                                    formCode = item.Form != null ? CreateConceptDescriptor(item.Form) : null,
                                    desc = !item.AdditionalDispensedItemDescription.IsNullOrEmptyWhitespace()
                                        ? CreateStructuredText(item.AdditionalDispensedItemDescription, null)
                                        : null
                                },
                                manufacturerOrganization = item.PBSManufacturerCode != null
                                    ? new POCD_MT000040Organization
                                    {
                                        id = CreateIdentifierArray(item.PBSManufacturerCode)
                                    }
                                    : null
                            },
                        },
                        id = item.DispenseItemIdentifier != null
                            ? CreateIdentifierArray(item.DispenseItemIdentifier)
                            : null,
                        entryRelationship = new[]
                        {
                            !item.QuantityDescription.IsNullOrEmptyWhitespace()
                                ? CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.INFRM,
                                    x_DocumentActMood.EVN,
                                    false,
                                    CreateConceptDescriptor(CreateCodableText(
                                        EtpRecordSections.QuantityDescription.GetAttributeValue<NameAttribute, String>(
                                            x => x.Code), CodingSystem.SNOMED,
                                        EtpRecordSections.QuantityDescription.GetAttributeValue<NameAttribute, String>(
                                            x => x.Name), null)),
                                    CreateEncapsulatedData(item.QuantityDescription),
                                    CreateIdentifierArray(new UniqueId())
                                )
                                : null,

                            !item.UniquePharmacyPrescriptionNumber.IsNullOrEmptyWhitespace()
                                ? CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.ACT,
                                    x_DocumentActMood.EVN,
                                    false,
                                    CreateConceptDescriptor(
                                        CreateCodableText(
                                            EtpRecordSections.UniquePharmacyPrescriptionNumber
                                                .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                            CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                            CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                            null,
                                            EtpRecordSections.UniquePharmacyPrescriptionNumber
                                                .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                            null)
                                    ),
                                    CreateEncapsulatedData(item.UniquePharmacyPrescriptionNumber),
                                    CreateIdentifierArray(new UniqueId())
                                )
                                : null,
                            !item.LabelInstruction.IsNullOrEmptyWhitespace()
                                ? CreateEntryRelationshipACT
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    x_ActClassDocumentEntryAct.INFRM,
                                    x_DocumentActMood.EVN,
                                    false,
                                    CreateConceptDescriptor(
                                        CreateCodableText(
                                            EtpRecordSections.LabelInstruction.GetAttributeValue<NameAttribute, String>(
                                                x => x.Code),
                                            CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                            CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                            null,
                                            EtpRecordSections.LabelInstruction.GetAttributeValue<NameAttribute, String>(
                                                x => x.Name),
                                            null)
                                    ),
                                    CreateStructuredText(item.LabelInstruction, null),
                                    CreateIdentifierArray(new UniqueId())
                                )
                                : null,
                            item.BrandSubstitutionOccurred.HasValue
                                ? CreateEntryRelationshipObservation
                                (
                                    x_ActRelationshipEntryRelationship.COMP,
                                    false,
                                    CreateConceptDescriptor(
                                        CreateCodableText(
                                            EtpRecordSections.BrandSubstitutionOccured
                                                .GetAttributeValue<NameAttribute, String>(x => x.Code),
                                            CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                            CodingSystem.NCTIS.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                            null,
                                            EtpRecordSections.BrandSubstitutionOccured
                                                .GetAttributeValue<NameAttribute, String>(x => x.Name),
                                            null)
                                    ),
                                    new List<ANY>
                                        {new BL {value = item.BrandSubstitutionOccurred.Value, valueSpecified = true}}
                                )
                                : null
                        },
                    },
                });

            // Comments
            if (!item.Comment.IsNullOrEmptyWhitespace())
            {
                relationships.Add(
                    CreateEntryRelationshipACT
                    (
                        x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor
                        (
                            EtpRecordSections.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Code),
                            CodingSystem.NCTIS,
                            EtpRecordSections.AdditionalComments.GetAttributeValue<NameAttribute, String>(x => x.Name),
                            null
                        ),
                        CreateEncapsulatedData(item.Comment),
                        CreateIdentifierArray(new UniqueId())
                    )
                );
            }

            //// Formula
            if (!item.Formula.IsNullOrEmptyWhitespace())
            {
                relationships.Add
                (
                    CreateEntryRelationshipACT
                    (
                        x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor
                        (
                            EtpRecordSections.Formula.GetAttributeValue<NameAttribute, String>(x => x.Code),
                            CodingSystem.NCTIS,
                            EtpRecordSections.Formula.GetAttributeValue<NameAttribute, String>(x => x.Name),
                            null
                        ),
                        CreateEncapsulatedData(item.Formula),
                        CreateIdentifierArray(new UniqueId())
                    )
                );
            }

            // Therapeutic Good Strength
            if (!item.TherapeuticGoodStrength.IsNullOrEmptyWhitespace())
            {
                relationships.Add(
                    CreateEntryRelationshipACT
                    (
                        x_ActRelationshipEntryRelationship.COMP,
                        x_ActClassDocumentEntryAct.INFRM,
                        x_DocumentActMood.EVN,
                        false,
                        CreateConceptDescriptor
                        (
                            EtpRecordSections.TherapeuticGoodStrength.GetAttributeValue<NameAttribute, String>(x =>
                                x.Code),
                            CodingSystem.NCTIS,
                            EtpRecordSections.TherapeuticGoodStrength.GetAttributeValue<NameAttribute, String>(x =>
                                x.Name),
                            null
                        ),
                        CreateEncapsulatedData(item.TherapeuticGoodStrength),
                        CreateIdentifierArray(new UniqueId())
                    )
                );
            }

            // Begin Prescription Item entry
            var entry = CreateEntrySubstanceAdministrationEvent
            (
                null,
                x_DocumentSubstanceMood.RQO,
                true,
                item.Status.HasValue ? item.Status.Value.GetAttributeValue<NameAttribute, String>(x => x.Name) : null,
                null,
                null,
                null,
                null,
                null,
                item.MaximumNumberOfRepeats.HasValue ? item.MaximumNumberOfRepeats + 1 : null,
                null,
                null,
                null,
                null,
                relationships,
                null
            );

            // Prescription Item Id
            // root = Data Component Register - Set Prescription Item Identifier 
            entry.substanceAdministration.id = item.PrescriptionItemIdentifier != null
                ? CreateIdentifierArray(item.PrescriptionItemIdentifier)
                : null;

            return entry;
        }

        # endregion

        #region Private Entry - XML Utilities

        /// <summary>
        /// Converts an XML document to UTF-8.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static XmlDocument ConvertDocumentToUtf8(XmlDocument document)
        {
            XmlDocument convertedDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = convertedDoc.CreateXmlDeclaration("1.0", null, null);
            xmlDeclaration.Encoding = "UTF-8";
            xmlDeclaration.Standalone = "yes";
            convertedDoc.LoadXml(document.DocumentElement.OuterXml);
            XmlElement root = convertedDoc.DocumentElement;
            convertedDoc.InsertBefore(xmlDeclaration, root);

            return convertedDoc;
        }

        /// <summary>
        /// Inserts the XSLT processing instruction into a document.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="stylesheet"></param>
        /// <returns></returns>
        private static XmlDocument InsertXsltProcessingInstruction(XmlDocument document, string stylesheet)
        {
            XmlElement root = document.DocumentElement;
            XmlProcessingInstruction xslt = document.CreateProcessingInstruction("xml-stylesheet",
                "href=\"" + stylesheet + "\" type=\"text/xsl\"");
            document.InsertBefore(xslt, root);

            return document;
        }

        /// <summary>
        /// Serialize the CDA document
        /// </summary>
        private static XmlDocument SerializeXML(Object cdaDocument, Type documentType)
        {
            var returnXmlDocument = new XmlDocument { PreserveWhitespace = true };

            var xmlTypeAttribute =
                (XmlTypeAttribute)Attribute.GetCustomAttribute(cdaDocument.GetType(), typeof(XmlTypeAttribute));
            if (xmlTypeAttribute != null)
            {
                var stringBuilder = new StringBuilder();

                var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(String.Empty, String.Empty);
                xmlSerializerNamespaces.Add(String.Empty, xmlTypeAttribute.Namespace);
                xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xmlSerializerNamespaces.Add("ext", "http://ns.electronichealth.net.au/Ci/Cda/Extensions/3.0");

                var defaultNamespace = xmlTypeAttribute.Namespace;

                //var xmlSerializer = new XmlSerializer(cdaDocument.GetType(), defaultNamespace); -- Use new CachingXmlSerializerFactory class
                var xmlSerializer = CachingXmlSerializerFactory.Create(cdaDocument.GetType(), defaultNamespace);

                using (var xmlWriter = XmlWriter.Create(stringBuilder))
                {
                    if (xmlWriter != null)
                    {
                        try
                        {
                            xmlSerializer.Serialize(xmlWriter, cdaDocument, xmlSerializerNamespaces);

                            returnXmlDocument.LoadXml(stringBuilder.ToString());

                            var styleSheet = ConfigurationManager.AppSettings["XSLStylesheet"];

                            returnXmlDocument = ConvertDocumentToUtf8(returnXmlDocument);

                            // Convert the document to UTF8 and insert the XSLT processing instruction
                            if (!string.IsNullOrEmpty(styleSheet))
                                returnXmlDocument = InsertXsltProcessingInstruction(returnXmlDocument, styleSheet);

                            Assembly assembly = Assembly.Load("CDA.GeneratedCode");
                            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                            // Version number for XML 
                            XmlComment productVersionComment = returnXmlDocument.CreateComment(
                                string.Format(@"Generated using 'NEHTA Vendor Library - CDA' version {0}",
                                    assembly.GetName().Version));

                            //Add the new node to the document.
                            XmlElement root = returnXmlDocument.DocumentElement;
                            returnXmlDocument.InsertBefore(productVersionComment, root);

                            // Handel the generation of the Template package Comment at the top of the CDA documents
                            var templatePackage = GetTemplatePackage(documentType);
                            if (templatePackage != null)
                            {
                                XmlComment templatePackageComment =
                                    returnXmlDocument.CreateComment(string.Format(
                                        @"The following template packages are supported for {0}: {1}",
                                        templatePackage.DocumentName, templatePackage.TemplatePackages));
                                returnXmlDocument.InsertBefore(templatePackageComment, root);
                            }

                        }
                        catch (Exception ex)
                        {
                            if (
                                ex.InnerException != null &&
                                ex.InnerException.Message.Contains("is an invalid character") &&
                                ex.InnerException.TargetSite != null &&
                                ex.InnerException.TargetSite.Name == "InvalidXmlChar"
                            )
                            {
                                //Throw a friendly exception explaining that the cdaDocument to serialize contains some invalid text characters
                                throw new Exception(
                                    "Invalid Xml Character: Please cleanse the data; It is possible that one of the string properties within the CDA Document contains invalid charactrers and can not be serialized",
                                    ex);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            return (returnXmlDocument);
        }

        /// <summary>
        /// Gets the template package for the given document type
        /// </summary>
        /// <param name="type">The document type</param>
        /// <returns>The TemplatePackedgeAttribute </returns>
        public static TemplatePackageAttribute GetTemplatePackage(Type type)
        {
            TemplatePackageAttribute templatePackageAttribute = null;

            if (type != null)
            {
                var documentName = type.GetAttributeValue<TemplatePackageAttribute, string>(x => x.DocumentName);
                var templatePackages =
                    type.GetAttributeValue<TemplatePackageAttribute, string>(x => x.TemplatePackages);

                if (!documentName.IsNullOrEmptyWhitespace() && !templatePackages.IsNullOrEmptyWhitespace())
                {
                    templatePackageAttribute = new TemplatePackageAttribute
                    {
                        DocumentName = documentName,
                        TemplatePackages = templatePackages
                    };
                }
            }

            return templatePackageAttribute;
        }

        /// <summary>
        /// Creates the actual CDA document.
        /// 
        /// This method requires that each of the parameters has been populated.
        /// </summary>
        /// <param name="clinicalDocument">The CDA document</param>
        /// <param name="authors">A list of Authors associated with the document</param>
        /// <param name="legalAuthenticator">The legal authenticator associated with the document</param>
        /// <param name="authenticators">A list of Authenticators</param>
        /// <param name="recipients">A list of Recipients</param>
        /// <param name="participants">A list of Participants</param>
        /// <param name="components">A list of Components</param>.
        /// <param name="nonXmlBody">A referenced file that contains the payload for this CDA document</param>
        /// <param name="includeLogo">Indicates if the logo is to be included as an ObservationMultiMedia entry within this document</param>
        /// <param name="documentType">The type of the clinical Document</param>
        /// <param name="logoByte">A byte array for the logo</param>
        /// <returns>XmlDocument (CDA document)</returns>
        internal static XmlDocument CreateXml(POCD_MT000040ClinicalDocument clinicalDocument,
            List<POCD_MT000040Author> authors,
            POCD_MT000040LegalAuthenticator legalAuthenticator,
            List<POCD_MT000040Authenticator> authenticators,
            List<POCD_MT000040InformationRecipient> recipients,
            List<POCD_MT000040Participant1> participants,
            List<POCD_MT000040Component3> components,
            POCD_MT000040NonXMLBody nonXmlBody,
            Boolean includeLogo,
            Byte[] logoByte,
            Type documentType)

        {
            // Add Authors
            if (authors != null)
            {
                clinicalDocument.author = authors.ToArray();
            }

            // Add Legal Authenticator
            clinicalDocument.legalAuthenticator = legalAuthenticator;

            // Add Authenticators
            if (authenticators != null)
            {
                clinicalDocument.authenticator = authenticators.ToArray();
            }

            // Add Information recipients
            if (recipients != null)
            {
                clinicalDocument.informationRecipient = recipients.ToArray();
            }

            // Add participants
            if (participants != null)
            {
                clinicalDocument.participant = participants.ToArray();
            }

            if (includeLogo)
            {
                if (clinicalDocument.component.structuredBody == null)
                {
                    clinicalDocument.component.structuredBody = new POCD_MT000040StructuredBody();
                }

                var filePathName = string.Empty;
                ByteArrayInput byteArrayInput = null;

                if (logoByte != null)
                {
                    byteArrayInput = new ByteArrayInput()
                    {
                        ByteArray = logoByte,
                        FileName = "logo.png"
                    };
                }
                else
                {
                    filePathName = @".\logo.png";
                }

                var externalData = new ExternalData
                {
                    Caption = "logo",
                    ExternalDataMediaType = MediaType.PNG,
                    ByteArrayInput = byteArrayInput,
                    Path = filePathName,
                    ID = "LOGO"
                };

                components.Add(new POCD_MT000040Component3
                {
                    section =
                        new POCD_MT000040Section
                        {
                            entry = new[]
                            {
                                new POCD_MT000040Entry {observationMedia = CreateObservationMedia(externalData)}
                            }
                        }
                });
            }

            //Add Body parts
            if (nonXmlBody != null)
            {
                clinicalDocument.component.nonXMLBody = nonXmlBody;
            }

            if (components != null)
            {
                if (components.Exists(component => component != null))
                {
                    if (clinicalDocument.component.structuredBody == null)
                    {
                        clinicalDocument.component.structuredBody = new POCD_MT000040StructuredBody();
                    }

                    clinicalDocument.component.structuredBody.component = components.ToArray();
                }
            }

            //Generate CDA document
            var xml = SerializeXML(clinicalDocument, documentType);

            return xml;
        }


        private static string CreateGuid()
        {
            return
            (
                new UniqueId().ToString().Replace("urn:uuid:", "")
            );
        }

        /// <summary>
        /// Creates a OID
        /// </summary>
        /// <returns>Creates an OID</returns>
        public static string CreateOid()
        {
            return Helper.OIDHelper.UuidToOid(CreateGuid());
        }

        # endregion

        #region Private Entry - Observation Media

        private static POCD_MT000040ObservationMedia CreateObservationMedia(ExternalData media)
        {
            POCD_MT000040ObservationMedia observationMedia = null;

            if (media != null)
            {
                var mediaId = CreateGuid();

                observationMedia = new POCD_MT000040ObservationMedia
                {
                    ID = media.ID,
                    classCode = ActClassObservation.OBS,
                    moodCode = ActMood.EVN,
                    id = CreateIdentifierArray(mediaId, null),
                    value = CreateEncapsulatedData(media)
                };
            }

            return observationMedia;
        }

        # endregion

        #region Private Entry - Entry Relationship

        /// <summary>
        /// Creates a Vaccine Cancellation Reason
        /// </summary>
        /// <param name="vaccineAdministrationEntry">The Vaccine Cancellation Reason</param>
        /// <returns>POCD_MT000040EntryRelationship</returns>
        internal static POCD_MT000040EntryRelationship CreateVaccineCancellationReason(
            VaccineCancellationReason vaccineAdministrationEntry)
        {
            var entryRelationshipList = new List<POCD_MT000040EntryRelationship>();

            CD cd;
            if (vaccineAdministrationEntry.VaccineType.HasValue)
            {
                cd = CreateConceptDescriptor(CreateCodableText(
                    vaccineAdministrationEntry.VaccineType.Value.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    CodingSystem.VaccineCancellationReasonTypeValues,
                    vaccineAdministrationEntry.VaccineType.Value.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    vaccineAdministrationEntry.VaccineType.Value.GetAttributeValue<NameAttribute, string>(
                        x => x.Comment))
                );
            }
            else
            {
                cd = CreateConceptDescriptor(null, null as string, null, null, null,
                    vaccineAdministrationEntry.VaccineTypeNullFlavour);
            }


            entryRelationshipList.Add(CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.COMP,
                x_ActClassDocumentEntryAct.INFRM,
                x_DocumentActMood.EVN,
                null,
                cd,
                null,
                null,
                CreateIntervalTimestamp(vaccineAdministrationEntry.VaccineCancellationReasonPeriod)));

            var entryRelationship = CreateEntryRelationshipACT(x_ActRelationshipEntryRelationship.RSON,
                x_ActClassDocumentEntryAct.INFRM,
                x_DocumentActMood.EVN,
                null,
                CreateConceptDescriptor(CreateCodableText(MedicareOverviewSections.VaccineCancellationReason)),
                CreateEncapsulatedData(vaccineAdministrationEntry.VaccineCancellationReasonComment),
                CreateIdentifierArray(CreateGuid()),
                entryRelationshipList);


            return entryRelationship;
        }

        # endregion

        #endregion

        #region Prescription and Dispense View

        /// <summary>
        /// Creates a Medicare View Exclusion Statement
        /// </summary>
        /// <param name="exclusionStatement">The Medicare View Exclusion Statement</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreatePrescriptionAndDispenseViewExclusionStatement(
            ExclusionStatement exclusionStatement, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (exclusionStatement != null && !exclusionStatement.GeneralStatement.IsNullOrEmptyWhitespace())
            {
                var entryList = new List<POCD_MT000040Entry>();

                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PrescribingAndDispensingViewRecordSections.ExclusionStatement),
                };

                component.section.title = CreateStructuredText(exclusionStatement.SectionTitle, null);

                entryList.Add(CreateExclusionStatement(exclusionStatement.GeneralStatement, "103.16135.179.1.1",
                    "General Statement"));
                component.section.entry = entryList.ToArray();

                // Narrative
                component.section.text = exclusionStatement.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(exclusionStatement);
            }

            return component;
        }

        /// <summary>
        /// Creates a Medicare View Exclusion Statement
        /// </summary>
        /// <param name="exclusionStatement">The Medicare View Exclusion Statement</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreatePathologyResultViewExclusionStatement(
            ExclusionStatement exclusionStatement, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (exclusionStatement != null && !exclusionStatement.GeneralStatement.IsNullOrEmptyWhitespace())
            {
                var entryList = new List<POCD_MT000040Entry>();

                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(PrescribingAndDispensingViewRecordSections.ExclusionStatement),
                };

                component.section.title = CreateStructuredText(exclusionStatement.SectionTitle, null);

                entryList.Add(CreateExclusionStatement(exclusionStatement.GeneralStatement, "103.16135.179.1.1",
                    "General Statement"));
                component.section.entry = entryList.ToArray();

                // Narrative
                component.section.text = exclusionStatement.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(exclusionStatement);
            }

            return component;
        }

        /// <summary>
        /// Summary of medication entries - medication entries with summary
        /// </summary>
        /// <param name="prescribingAndDispensingReports">Prescribing And Dispensing Reports</param>
        /// <param name="narrativeGenerator"> The Narrative Generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(
            PrescribingAndDispensingReports prescribingAndDispensingReports, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            var components = new List<POCD_MT000040Component5>();

            if (prescribingAndDispensingReports != null)
            {
                component = new POCD_MT000040Component3();

                component.section = new POCD_MT000040Section
                {
                    code = CreateCodedWithExtensionElement(CreateCodableText(PrescribingAndDispensingViewRecordSections
                        .PrescribingAndDispensingReports)),
                    title = CreateStructuredText(prescribingAndDispensingReports.SectionTitle, null)
                };

                if (prescribingAndDispensingReports.MedicationEntriesWithSummary != null)
                    foreach (var medicationEntryWithSummary in prescribingAndDispensingReports
                        .MedicationEntriesWithSummary)
                    {
                        components.Add(
                            CreateMedicationEntriesWithSummary(medicationEntryWithSummary, narrativeGenerator));
                    }

                component.section.component = components.ToArray();

                component.section.text = prescribingAndDispensingReports.CustomNarrative ??
                                         narrativeGenerator.CreateNarrative(prescribingAndDispensingReports);

            }

            return component;
        }

        /// <summary>
        /// Create Medication Entries With Summary
        /// </summary>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Component5 CreateMedicationEntriesWithSummary(
            MedicationEntriesWithSummary medicationEntriesWithSummary, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component5 component = null;
            var components = new List<POCD_MT000040Component5>();

            var entryList = new List<POCD_MT000040Entry>();

            foreach (var medicationEntries in medicationEntriesWithSummary.MedicationEntries)
            {
                if (medicationEntries.PrescriptionItem != null)
                {
                    components.Add(CreateComponent(medicationEntries.PrescriptionItem as PCEHRPrescriptionItem,
                        narrativeGenerator));
                }

                if (medicationEntries.DispenseItem != null)
                {
                    components.Add(CreateComponent(medicationEntries.DispenseItem as PCEHRDispenseItem,
                        narrativeGenerator));
                }
            }

            if (medicationEntriesWithSummary.SummaryOfMedicationEntries != null)
            {
                entryList.Add(CreateSummaryOfMedicationEntries(medicationEntriesWithSummary.SummaryOfMedicationEntries,
                    narrativeGenerator));
            }

            //Create the Other Test Result Component and section
            component = new POCD_MT000040Component5
            {
                section = new POCD_MT000040Section
                {
                    code = CreateCodedWithExtensionElement(CreateCodableText(PrescribingAndDispensingViewRecordSections
                        .MedicationEntriesWithSummary)),
                    title = CreateStructuredText(medicationEntriesWithSummary.SectionTitle, null),
                    component = components.Any() ? components.ToArray() : null,
                    entry = entryList.Any() ? entryList.ToArray() : null
                }
            };

            return component;
        }

        /// <summary>
        /// Create Summary Of Medication Entries
        /// </summary>
        /// <returns>POCD_MT000040Component5</returns>
        internal static POCD_MT000040Entry CreateSummaryOfMedicationEntries(
            SummaryOfMedicationEntries summaryOfMedicationEntries, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Entry entry = null;
            var components = new List<POCD_MT000040Component4>();

            if (summaryOfMedicationEntries.TherapeuticGoodId != null)
            {
                var component = new POCD_MT000040Component4
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor(CreateCodableText(PrescribingAndDispensingViewRecordSections
                            .TherapeuticGoodIdentification)),
                        value = new ANY[] { CreateConceptDescriptor(summaryOfMedicationEntries.TherapeuticGoodId) }
                    }
                };

                components.Add(component);
            }

            if (summaryOfMedicationEntries.DateTimePrescriptionWritten != null)
            {
                var component = new POCD_MT000040Component4
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor(CreateCodableText(PrescribingAndDispensingViewRecordSections
                            .DateTimePrescriptionWritten)),
                        value = new ANY[]
                            {CreateTimeStampElementIso(summaryOfMedicationEntries.DateTimePrescriptionWritten)}
                    }
                };

                components.Add(component);
            }

            if (summaryOfMedicationEntries.DateTimeOfEarliestDispenseEvent != null)
            {
                var component = new POCD_MT000040Component4
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor(CreateCodableText(PrescribingAndDispensingViewRecordSections
                            .DateTimeOfEarliestDispenseEvent)),
                        value = new ANY[]
                            {CreateTimeStampElementIso(summaryOfMedicationEntries.DateTimeOfEarliestDispenseEvent)}
                    }
                };

                components.Add(component);
            }

            if (summaryOfMedicationEntries.DateTimeOfLatestDispenseEvent != null)
            {
                var component = new POCD_MT000040Component4
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor(CreateCodableText(PrescribingAndDispensingViewRecordSections
                            .DateTimeOfLatestDispenseEvent)),
                        value = new ANY[]
                            {CreateTimeStampElementIso(summaryOfMedicationEntries.DateTimeOfLatestDispenseEvent)}
                    }
                };

                components.Add(component);
            }

            if (summaryOfMedicationEntries.TotalNumberOfKnownSupplies != null)
            {
                var component = new POCD_MT000040Component4
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor(CreateCodableText(PrescribingAndDispensingViewRecordSections
                            .TotalNumberOfKnownSupplies)),
                        value = new ANY[]
                            {CreateIntegerElement(summaryOfMedicationEntries.TotalNumberOfKnownSupplies, null, null)}
                    }
                };

                components.Add(component);
            }

            if (summaryOfMedicationEntries.MaximumNumberOfPermittedSupplies.HasValue)
            {
                var component = new POCD_MT000040Component4
                {
                    observation = new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        code = CreateConceptDescriptor(CreateCodableText(PrescribingAndDispensingViewRecordSections
                            .MaximumNumberOfPermittedSupplies)),
                        value = new ANY[]
                        {
                            CreateIntegerElement(summaryOfMedicationEntries.MaximumNumberOfPermittedSupplies, null,
                                null)
                        }
                    }
                };

                components.Add(component);
            }

            entry = new POCD_MT000040Entry
            {
                organizer = new POCD_MT000040Organizer
                {
                    classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                    moodCode = ActMood.EVN,
                    code = CreateConceptDescriptor(
                        CreateCodableText(PrescribingAndDispensingViewRecordSections.SummaryOfMedicationEntries)),
                    component = components.ToArray(),
                    statusCode = new CS
                    { code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(x => x.Name) }
                }
            };

            return entry;
        }

        #endregion

        #region CeHR

        /// <summary>
        /// Creates an MeasurementInformation
        /// </summary>
        /// <param name="item">IEPrescriptionItem</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(MeasurementInformation item,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (item != null)
            {
                var entries = new List<POCD_MT000040Entry>();

                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        templateId = CreateIdentifierArray("1.2.36.1.2001.1001.101.101.16491", null),
                        code = CreateCodedWithExtensionElement(
                            CreateCodableText(CeHRRecordSections.PhysicalMeasurements)),
                        title = CreateStructuredText(
                            CeHRRecordSections.PhysicalMeasurements.GetAttributeValue<NameAttribute, string>(x =>
                                x.Title), null)
                    }
                };

                var components = new List<POCD_MT000040Component4>();

                if (item.HeadCircumference != null)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTHeadCircumference)),
                        new List<ANY> { CreatePhysicalQuantity(item.HeadCircumference) },
                        null, CreateIdentifierArray(CreateGuid()),
                        CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16808")));
                }

                if (item.BodyHeight != null)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTBodyHeight)),
                        new List<ANY> { CreatePhysicalQuantity(item.BodyHeight) },
                        null, CreateIdentifierArray(CreateGuid()),
                        CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16123")));
                }

                if (item.BodyWeight != null)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTBodyWeight)),
                        new List<ANY> { CreatePhysicalQuantity(item.BodyWeight) },
                        null, CreateIdentifierArray(CreateGuid()),
                        CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16124")));
                }

                if (item.BodyMassIndex != null)
                {
                    components.Add(CreateComponentObservation(
                        CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTBodyMassIndex)),
                        new List<ANY> { CreatePhysicalQuantity(item.BodyMassIndex) },
                        null, CreateIdentifierArray(CreateGuid()),
                        CreateIdentifierArray("1.2.36.1.2001.1001.101.102.161255")));
                }

                var entry = new POCD_MT000040Entry
                {
                    typeCode = x_ActRelationshipEntry.DRIV,
                    organizer = new POCD_MT000040Organizer
                    {
                        code = CreateConceptDescriptor(
                            CreateCodableText(CeHRRecordSections.ProviderEnteredMeasurementInformation)),
                        moodCode = ActMood.EVN,
                        classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                        effectiveTime = CreateIntervalTimestamp(item.ObservationDate, null),
                        statusCode = new CS
                        {
                            code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name)
                        },
                        component = components.ToArray()
                    }
                };

                entries.Add(entry);

                component.section.entry = entries.ToArray();
                component.section.text = item.CustomNarrative ?? narrativeGenerator.CreateNarrative(item);
            }

            return component;
        }

        /// <summary>
        /// Creates an Measurement Information
        /// </summary>
        /// <param name="items">Measurement Information</param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<MeasurementInformation> items,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (items != null)
            {
                var entries = new List<POCD_MT000040Entry>();

                component = new POCD_MT000040Component3
                {
                    section = CreateSectionCodeTitle(
                        PhysicalMeasurementDocumentSections.PhysicalMeasurements
                            .GetAttributeValue<NameAttribute, string>(x => x.Code),
                        (CodingSystem)Enum.Parse(typeof(CodingSystem),
                            PhysicalMeasurementDocumentSections.PhysicalMeasurements
                                .GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)),
                        PhysicalMeasurementDocumentSections.PhysicalMeasurements
                            .GetAttributeValue<NameAttribute, string>(x => x.Name),
                        null)
                };

                component.section.templateId = CreateIdentifierArray(
                    PhysicalMeasurementDocumentSections.PhysicalMeasurements.GetAttributeValue<NameAttribute, string>(
                        x => x.Identifier), null);


                foreach (var item in items)
                {
                    var components = new List<POCD_MT000040Component4>();

                    if (item.HeadCircumference != null)
                    {
                        components.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTHeadCircumference)),
                            new List<ANY> { CreatePhysicalQuantity(item.HeadCircumference) },
                            null, CreateIdentifierArray(CreateGuid()),
                            CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16808")));
                    }

                    if (item.BodyHeight != null)
                    {
                        components.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTBodyHeight)),
                            new List<ANY> { CreatePhysicalQuantity(item.BodyHeight) },
                            null, CreateIdentifierArray(CreateGuid()),
                            CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16123")));
                    }

                    if (item.BodyWeight != null)
                    {
                        components.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTBodyWeight)),
                            new List<ANY> { CreatePhysicalQuantity(item.BodyWeight) },
                            null, CreateIdentifierArray(CreateGuid()),
                            CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16124")));
                    }

                    if (item.BodyMassIndex != null)
                    {
                        components.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.CTBodyMassIndex)),
                            new List<ANY> { CreatePhysicalQuantity(item.BodyMassIndex) },
                            null, CreateIdentifierArray(CreateGuid()),
                            CreateIdentifierArray("1.2.36.1.2001.1001.101.102.161255")));
                    }

                    var entry = new POCD_MT000040Entry
                    {
                        typeCode = x_ActRelationshipEntry.DRIV,
                        organizer = new POCD_MT000040Organizer
                        {
                            id = CreateIdentifierArray(CreateGuid()),
                            code = CreateConceptDescriptor(
                                CreateCodableText(CeHRRecordSections.ConsumerEnteredMeasurementInformation)),
                            moodCode = ActMood.EVN,
                            classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                            statusCode = new CS
                            { code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name) },
                            effectiveTime = CreateIntervalTimestamp(item.ObservationDate, null),
                            component = components.ToArray()
                        }
                    };
                    entries.Add(entry);
                }

                component.section.entry = entries.ToArray();
                component.section.text = narrativeGenerator.CreateNarrative(items);
            }

            return component;
        }

        /// <summary>
        /// Creates an Child Parent Questionnaire
        /// </summary>
        /// <param name="questionnaire">A list of HealthCheckAssesment </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(Questionnaire questionnaire,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (questionnaire != null && questionnaire.SectionCode.HasValue)
            {

                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        code = CreateCodedWithExtensionElement(CreateCodableText(questionnaire.SectionCode.Value)),
                        title = new ST
                        {
                            Text = new[]
                                {questionnaire.SectionCode.Value.GetAttributeValue<NameAttribute, string>(x => x.Name)}
                        }
                    }
                };

                var components = new List<POCD_MT000040Component4>();
                var entries = new List<POCD_MT000040Entry>();

                components.AddRange(CreateComponent(questionnaire.AssessmentItems));

                var entry = new POCD_MT000040Entry
                {
                    typeCode = x_ActRelationshipEntry.DRIV,
                    organizer = new POCD_MT000040Organizer
                    {
                        statusCode = new CS
                        {
                            code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name)
                        },
                        classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                        moodCode = ActMood.EVN,
                        component = components.ToArray()
                    }
                };


                entries.Add(entry);

                component.section.entry = entries.ToArray();
                component.section.text = narrativeGenerator.CreateNarrative(questionnaire);
            }

            return component;
        }

        /// <summary>
        /// Creates an Health Check Assesment
        /// </summary>
        /// <param name="healthCheckAssesment">A list of HealthCheckAssesment </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(HealthCheckAssesment healthCheckAssesment,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (healthCheckAssesment != null && healthCheckAssesment.SectionCode != null)
            {

                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        code = CreateCodedWithExtensionElement(
                            CreateCodableText(healthCheckAssesment.SectionCode.Value)),
                        title = CreateStructuredText(
                            healthCheckAssesment.SectionCode.Value
                                .GetAttributeValue<NameAttribute, string>(x => x.Name), null)
                    }
                };

                var components = new List<POCD_MT000040Component4>();
                var entries = new List<POCD_MT000040Entry>();

                components.AddRange(CreateComponent(healthCheckAssesment.AssessmentItems));

                var entry = new POCD_MT000040Entry
                {
                    typeCode = x_ActRelationshipEntry.DRIV,
                    organizer = new POCD_MT000040Organizer
                    {
                        statusCode = new CS
                        {
                            code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name)
                        },
                        classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                        moodCode = ActMood.EVN,
                        component = components.ToArray()
                    }
                };


                entries.Add(entry);

                component.section.entry = entries.ToArray();
                component.section.text = narrativeGenerator.CreateNarrative(healthCheckAssesment);
            }

            return component;
        }


        /// <summary>
        /// Creates an Assesment component
        /// </summary>
        /// <returns>A list of POCD_MT000040Component4</returns>
        internal static List<POCD_MT000040Component4> CreateComponent(List<AssessmentItem> assessmentItems)
        {
            List<POCD_MT000040Component4> components = null;

            if (assessmentItems != null && assessmentItems.Any())
            {
                components = new List<POCD_MT000040Component4>();

                foreach (var assessmentItem in assessmentItems)
                {
                    ANY any = null;
                    ICodableText code = null;

                    if (assessmentItem.AnswersData.HasValue)
                    {
                        code = CreateCodableText(assessmentItem.AnswersData.Value);
                    }

                    if (!assessmentItem.FreeText.IsNullOrEmptyWhitespace())
                    {
                        any = CreateStructuredText(assessmentItem.FreeText, null);
                    }

                    if (assessmentItem.AnswersValue.HasValue)
                    {
                        any = CreateConceptDescriptor(CreateCodableText(assessmentItem.AnswersValue.Value));
                    }

                    var enttryRelationshipList = new List<POCD_MT000040EntryRelationship>
                    {
                        CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.COMP,
                            null,
                            assessmentItem.DateTime,
                            CreateConceptDescriptor(code), null, null,
                            any, null)
                    };

                    components.Add(new POCD_MT000040Component4
                    {
                        observation = CreateObservation(ActClassObservation.OBS, x_ActMoodDocumentObservation.EVN,
                                CreateConceptDescriptor(assessmentItem.QuestionData), null, null,
                                enttryRelationshipList)
                    }
                    );
                }

            }

            return components;
        }

        /// <summary>
        /// Creates a Birth Details
        /// </summary>
        /// <param name="birthDetails">A list of BirthDetails </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(BirthDetails birthDetails,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (birthDetails != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {

                        code = CreateCodedWithExtensionElement(CreateCodableText(CeHRRecordSections.BirthDetails)),
                        title = CreateStructuredText(
                            CeHRRecordSections.BirthDetails.GetAttributeValue<NameAttribute, string>(x => x.Title),
                            null),
                        templateId = CreateIdentifierArray("1.2.36.1.2001.1001.101.101.16886")
                    }
                };

                if (birthDetails.ExternalData != null && birthDetails.ExternalData.Any())
                {
                    var entryList = new List<POCD_MT000040Entry>();

                    foreach (var encapsulatedData in birthDetails.ExternalData)
                    {
                        //Added this relationship so as we can reference and display the test result representation 
                        //data within the narrative
                        //Create the observation entry with all the above relationships nested inside the observation
                        var entry = new POCD_MT000040Entry
                        {
                            templateId = CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16883"),
                            observationMedia = CreateObservationMedia(encapsulatedData)
                        };

                        entryList.Add(entry);
                    }

                    component.section.entry = entryList.ToArray();
                    component.section.text = narrativeGenerator.CreateNarrative(birthDetails);
                }
                else
                {
                    component.section.text = new StrucDocText
                    {
                        paragraph = new[] { new StrucDocParagraph { Text = new[] { NO_ENTRIES_MESSAGE } } }
                    };
                }
            }

            return component;
        }



        internal static POCD_MT000040Component3 CreateComponent(EncapsulatedData pcmlData,
            INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;

            if (pcmlData != null)
            {
                component = new POCD_MT000040Component3
                {
                    section = new POCD_MT000040Section
                    {
                        id = CreateIdentifierElement(CreateGuid(), null),
                        code = CreateCodedWithExtensionElement(
                            CreateCodableText(PcmlSections.PharmacistSharedMedicinesList)),
                        title = CreateStructuredText(
                            PcmlSections.PharmacistSharedMedicinesList.GetAttributeValue<NameAttribute, string>(x =>
                                x.Title), null),
                        templateId = CreateIdentifierArray("1.2.36.1.2001.1001.101.101.16886")
                    }
                };

                if (pcmlData.ExternalData != null && pcmlData.ExternalData != null)
                {
                    var entryList = new List<POCD_MT000040Entry>();

                    //Added this relationship so as we can reference and display the test result representation 
                    //data within the narrative
                    //Create the observation entry with all the above relationships nested inside the observation
                    var entry = new POCD_MT000040Entry
                    {
                        templateId = CreateIdentifierArray("1.2.36.1.2001.1001.101.102.16883"),
                        observationMedia = CreateObservationMedia(pcmlData.ExternalData)
                    };

                    entryList.Add(entry);

                    component.section.entry = entryList.ToArray();
                    component.section.text = narrativeGenerator.CreateNarrative(pcmlData);
                }
                else
                {
                    component.section.text = new StrucDocText
                    {
                        paragraph = new[] { new StrucDocParagraph { Text = new[] { NO_ENTRIES_MESSAGE } } }
                    };
                }
            }

            return component;
        }

        /// <summary>
        /// Creates a Questionnaire Documents
        /// </summary>
        /// <param name="questionnaireDocuments">The Medicare DVA Funded Services </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static List<POCD_MT000040Component3> CreateComponent(
            List<QuestionnaireDocumentData> questionnaireDocuments, INarrativeGenerator narrativeGenerator)
        {
            var components = new List<POCD_MT000040Component3>();

            if (questionnaireDocuments != null && questionnaireDocuments.Any())
            {
                foreach (var questionnaireDocument in questionnaireDocuments)
                {
                    var organizerComponents = new List<POCD_MT000040Component4>();

                    // POCD_MT000040Component3
                    var component = new POCD_MT000040Component3
                    {

                        typeCode = ActRelationshipHasComponent.COMP,
                        section = questionnaireDocument.QuestionnairesData.HasValue
                            ? new POCD_MT000040Section
                            {
                                id = CreateIdentifierElement(CreateGuid(), null),
                                title = CreateStructuredText(
                                    questionnaireDocument.QuestionnairesData.Value
                                        .GetAttributeValue<NameAttribute, string>(x => x.Name), null),
                                code = CreateCodedWithExtensionElement(
                                    CreateCodableText(questionnaireDocument.QuestionnairesData.Value))
                            }
                            : null

                    };

                    if (questionnaireDocument.DocumentLink != null)
                    {
                        var linkEntry = CreateEntryLink(x_DocumentActMood.EVN,
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.RecordLink)),
                            questionnaireDocument.DocumentLink);

                        organizerComponents.Add(new POCD_MT000040Component4
                        {
                            act = linkEntry.act
                        });
                    }

                    if (questionnaireDocument.Assessment.HasValue)
                    {
                        organizerComponents.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.IncompleteFlag)),
                            new List<ANY> { CreateBoolean(questionnaireDocument.Assessment.Value, true) }));
                    }


                    if (!questionnaireDocument.AuthorName.IsNullOrEmptyWhitespace())
                    {
                        organizerComponents.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.PCEHR_AUTID)),
                            new List<ANY> { CreateStructuredText(questionnaireDocument.AuthorName, null) }));
                    }


                    if (questionnaireDocument.DocumentDate != null)
                    {
                        organizerComponents.Add(CreateComponentObservation(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.PCEHR_DOCTIME)),
                            new List<ANY> { CreateIntervalTimestamp(questionnaireDocument.DocumentDate, null) }));
                    }

                    // Exclusion Statement
                    if (questionnaireDocument.DocumentLink == null && questionnaireDocument.Assessment == null &&
                        questionnaireDocument.AuthorName == null && questionnaireDocument.DocumentDate == null)
                    {
                        const string statementForEmptyEntries = NO_ENTRIES_MESSAGE;

                        var componentExclusionStatment = new POCD_MT000040Component5();

                        var section = CreateSectionCodeTitle
                        (
                            CeHRRecordSections.ExclusionStatement
                        );

                        section.entry = new[]
                        {
                            CreateExclusionStatementStructuredText(CeHRRecordSections.GlobalStatement,
                                statementForEmptyEntries)
                        };

                        section.text = new StrucDocText
                        {
                            paragraph = new[]
                            {
                                new StrucDocParagraph
                                {
                                    Text = new[] {statementForEmptyEntries}
                                }
                            }
                        };

                        componentExclusionStatment.section = section;

                        component.section.component = new[] { componentExclusionStatment };
                    }
                    else
                    {
                        component.section.entry = new[]
                        {
                            new POCD_MT000040Entry
                            {
                                typeCode = x_ActRelationshipEntry.DRIV,
                                organizer = new POCD_MT000040Organizer
                                {
                                    statusCode = new CS
                                    {
                                        code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(
                                            a => a.Name)
                                    },
                                    classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                                    moodCode = ActMood.EVN,
                                    component = organizerComponents.ToArray()
                                }
                            }
                        };
                    }

                    component.section.text = narrativeGenerator.CreateNarrative(questionnaireDocument);

                    components.Add(component);
                }
            }

            return components;
        }

        /// <summary>
        /// Creates an Measurement Entry
        /// </summary>
        /// <param name="measurementEntrys">Measurement Entry</param>
        /// <param name="ceHhrRecordSections"> ceHhrRecordSections </param>
        /// <param name="narrativeGenerator">The narrative generator</param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component3 CreateComponent(List<MeasurementEntry> measurementEntrys,
            CeHRRecordSections ceHhrRecordSections, INarrativeGenerator narrativeGenerator)
        {
            POCD_MT000040Component3 component = null;
            POCD_MT000040Component5 componentExclusionStatment = null;

            if (measurementEntrys != null)
            {
                component = new POCD_MT000040Component3
                {
                    typeCode = ActRelationshipHasComponent.COMP,
                    section = new POCD_MT000040Section
                    {
                        id = CreateIdentifierElement(CreateGuid()),
                        code = CreateCodedWithExtensionElement(CreateCodableText(ceHhrRecordSections)),
                        title = CreateStructuredText(
                            ceHhrRecordSections.GetAttributeValue<NameAttribute, string>(x => x.Name), null)
                    }
                };

                if (!measurementEntrys.Any())
                {
                    const string statementForEmptyEntries = NO_ENTRIES_MESSAGE;

                    componentExclusionStatment = new POCD_MT000040Component5();

                    var section = CreateSectionCodeTitle
                    (
                        CeHRRecordSections.ExclusionStatement
                    );

                    section.entry = new[]
                    {
                        CreateExclusionStatementStructuredText(CeHRRecordSections.GlobalStatement,
                            statementForEmptyEntries)
                    };

                    section.text = new StrucDocText
                    {
                        paragraph = new[]
                        {
                            new StrucDocParagraph
                            {
                                Text = new[] {statementForEmptyEntries}
                            }
                        }
                    };

                    componentExclusionStatment.section = section;
                }

                var entries = new List<POCD_MT000040Entry>();
                foreach (var measurementEntry in measurementEntrys)
                {
                    var organizerComponents = new List<POCD_MT000040Component4>();

                    if (measurementEntry.BodyHeightMeasure != null)
                        organizerComponents.Add(CreateComponent(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.BodyHeightMeasure)),
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.Percentile)),
                            measurementEntry.BodyHeightMeasure));

                    if (measurementEntry.BodyWeightMeasure != null)
                        organizerComponents.Add(CreateComponent(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.BodyWeight)),
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.Percentile)),
                            measurementEntry.BodyWeightMeasure));

                    if (measurementEntry.HeadCircumferenceMeasure != null)
                        organizerComponents.Add(CreateComponent(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.HeadCircumference)),
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.Percentile)),
                            measurementEntry.HeadCircumferenceMeasure));

                    if (measurementEntry.BodyMassIndex != null)
                        organizerComponents.Add(CreateComponent(
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.BodyMassIndex)),
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.Percentile)),
                            measurementEntry.HeadCircumferenceMeasure));

                    if (measurementEntry.DocumentLink != null)
                    {
                        var linkEntry = CreateEntryLink(x_DocumentActMood.EVN,
                            CreateConceptDescriptor(CreateCodableText(CeHRRecordSections.RecordLink)),
                            measurementEntry.DocumentLink);

                        organizerComponents.Add(new POCD_MT000040Component4
                        {
                            act = linkEntry.act
                        });
                    }

                    var entry = new POCD_MT000040Entry
                    {
                        typeCode = x_ActRelationshipEntry.DRIV,
                        organizer = new POCD_MT000040Organizer
                        {
                            id = CreateIdentifierArray(CreateGuid()),
                            code = CreateCodedWithExtensionElement(CreateCodableText(ceHhrRecordSections)),
                            effectiveTime = CreateIntervalTimestamp(measurementEntry.ObservationDate, null),
                            statusCode = new CS
                            {
                                code = StatusCode.Completed.GetAttributeValue<NameAttribute, string>(a => a.Name)
                            },
                            classCode = x_ActClassDocumentEntryOrganizer.CLUSTER,
                            moodCode = ActMood.EVN,
                            component = organizerComponents.ToArray()
                        }
                    };
                    entries.Add(entry);
                }

                if (entries.Any())
                    component.section.entry = entries.ToArray();

                if (componentExclusionStatment != null)
                    component.section.component = new[] { componentExclusionStatment };

                component.section.text = narrativeGenerator.CreateNarrative(measurementEntrys);
            }

            return component;
        }

        /// <summary>
        /// Creates an Measurement Entry
        /// </summary>
        /// <param name="code">CD</param>
        /// <param name="percentileCode">Create the percentile section for the component </param>
        /// <param name="measurementComponent">A list of measurementComponent </param>
        /// <returns>POCD_MT000040Component3</returns>
        internal static POCD_MT000040Component4 CreateComponent(CD code, CD percentileCode,
            MeasurementComponent measurementComponent)
        {
            var component = new POCD_MT000040Component4
            {
                typeCode = ActRelationshipHasComponent.COMP,
                seperatableInd = CreateBoolean(false, true),
                observation =
                    new POCD_MT000040Observation
                    {
                        classCode = ActClassObservation.OBS,
                        moodCode = x_ActMoodDocumentObservation.EVN,
                        id = CreateIdentifierArray(CreateGuid()),
                        code = code,
                        value = measurementComponent.ComponentValue != null
                            ? new ANY[] { CreatePhysicalQuantity(measurementComponent.ComponentValue) }
                            : null,
                        entryRelationship =
                            measurementComponent.PercentileValue != null && measurementComponent.ComponentValue != null
                                ? new[]
                                {
                                    CreateEntryRelationshipObservation(x_ActRelationshipEntryRelationship.MFST,
                                        null,
                                        percentileCode,
                                        new List<ANY>() {CreatePhysicalQuantity(measurementComponent.PercentileValue)})
                                }
                                : null

                    }
            };

            return component;
        }

        #endregion
    }
}