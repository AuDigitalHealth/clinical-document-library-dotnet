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
using System.Globalization;
using System.IO;
using System.Linq;
using CDA.Generator.Common.SCSModel.DiagnosticImagingReport.Entities;
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.Common.Time;
using System.Collections;
using CDA.Generator.Common.Common.Time.Enum;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// This is a helper class used to assist the CDANarrativeGenerator
    /// </summary>
    public partial class  CDANarrativeGenerator : INarrativeGenerator
    {
        /// <summary>
        /// This method inserts text into a StrucDocParagraph
        /// </summary>
        /// <param name="text">The Text To be inserted into the Paragraph </param>
        /// <returns>StrucDocParagraph</returns>
        
        public static StrucDocParagraph[] CreateParagraph(string text)
        {
          return new []
                {
                  new StrucDocParagraph
                    {
                      Text = new [] { text}
                    }   
                };
        }
		
        /// <summary>
        /// Returns narrative text for list of times
        /// </summary>
        /// <param name="listTime">A list of time </param>
        /// <returns>A narrative text describing the list of times</returns>
        public string CreateTimingEntry(List<ITime> listTime)
        {
          return listTime.Aggregate(string.Empty, (current, time) => current + (time.NarrativeText() + DELIMITERBREAK));
        }

        /// <summary>
        /// Creates EncapsulatedData for the narrative
        /// </summary>
        /// <param name="encapsulatedData">An EncapsulatedData</param>
        /// <param name="renderMultiMediaList">A list of StrucDocRenderMultiMedia</param>
        /// <returns></returns>
        public object CreateEncapsulatedData(EncapsulatedData encapsulatedData, ref List<StrucDocRenderMultiMedia> renderMultiMediaList)
        {
          object result = null;
          if (encapsulatedData != null && !encapsulatedData.Text.IsNullOrEmptyWhitespace())
          {
            result = encapsulatedData.Text;
          }
          else if (encapsulatedData != null && encapsulatedData.ExternalData != null)
          {
            result = CreateEncapsulatedData(encapsulatedData.ExternalData, ref renderMultiMediaList);
          }
          return result;
        }

		    /// <summary>
        /// Physical Measurements
        /// </summary>
        /// <param name="informationProvider">Physical Measurements</param>
        /// <returns>StrucDocText</returns>
        public string CreateInformationProvider(IInformationProviderCollection informationProvider)
        {               
            string narrativeText = string.Empty;
            if (informationProvider != null)
            {
              if (informationProvider is Device)
              {
                var device = informationProvider as Device;
                narrativeText += string.Format("{0}", device.SoftwareName);
              }

              // Both types are of type Participation so use the Participant to determine the type 
              if (informationProvider is Participation)
              {
                var informationProviderHealthcareProvider = informationProvider as IParticipationInformationProviderHealthcareProvider;
                var informationProviderNonHealthcareProvider = informationProvider as IParticipationInformationProviderNonHealthcareProvider;

                if (informationProviderHealthcareProvider.Participant != null)
                {
                   narrativeText = informationProviderHealthcareProvider.Participant.Person != null && informationProviderHealthcareProvider.Participant.Person.PersonNames != null ? BuildPersonNames(informationProviderHealthcareProvider.Participant.Person.PersonNames) : null;

                  if (informationProviderHealthcareProvider.Participant.Person != null && informationProviderHealthcareProvider.Participant.Person.Organisation != null)
                    if (!informationProviderHealthcareProvider.Participant.Person.Organisation.Name.IsNullOrEmptyWhitespace())
                      narrativeText += DELIMITERBREAK + informationProviderHealthcareProvider.Participant.Person.Organisation.Name;

                  narrativeText += DELIMITERBREAK + CreateAddress(informationProviderHealthcareProvider.Participant.Addresses, informationProviderHealthcareProvider.Participant.ElectronicCommunicationDetails);
                }

                if (informationProviderNonHealthcareProvider.Participant != null)
                {
                   narrativeText = informationProviderNonHealthcareProvider.Participant.Person != null && informationProviderNonHealthcareProvider.Participant.Person.PersonNames != null ? BuildPersonNames(informationProviderNonHealthcareProvider.Participant.Person.PersonNames) : null;
                   narrativeText += DELIMITERBREAK + CreateAddress(informationProviderNonHealthcareProvider.Participant.Addresses, informationProviderNonHealthcareProvider.Participant.ElectronicCommunicationDetails);
                }
            }             
        }
          return narrativeText;
        }

        /// <summary>
        /// Create Anatomical Sites
        /// </summary>
        /// <param name="anatomicalSites">A list of anatomicalSites</param>
        /// <param name="renderMultiMediaList">A list of renderMultiMediaList</param>
        /// <returns>This returns a StrucDocTable</returns>
		public static StrucDocTable CreateAnatomicalSites(List<AnatomicalSite> anatomicalSites, ref List<StrucDocRenderMultiMedia> renderMultiMediaList)
        {
            var structDocTable = new StrucDocTable();

            //Anatomical location image
            if (anatomicalSites.Any())
            {
                var narrative = new List<List<Object>>();

                foreach (var anatomicalSite in anatomicalSites)
                {
                  //populate the narrative for each anatomical site
                  if (anatomicalSite != null)
                  {
                    narrative.Add(
                                    new List<Object>
                                              {
                                                  anatomicalSite.Description, 
                                                  anatomicalSite.SpecificLocation != null && anatomicalSite.SpecificLocation.NameOfLocation != null ? anatomicalSite.SpecificLocation.NameOfLocation.NarrativeText : String.Empty, 
                                                  anatomicalSite.SpecificLocation != null && anatomicalSite.SpecificLocation.Side != null ? anatomicalSite.SpecificLocation.Side.NarrativeText : String.Empty ,
                                                  anatomicalSite.Images != null ? CreateEncapsulatedData(anatomicalSite.Images, ref renderMultiMediaList) : null
                                              }
                                 );
                  }
                }

                var headerListAnatomicalSite = new List<string>
                                 {
                                   "Description", "Location", "Side", "Image / File"
                                 };

                StripEmptyColoums(ref headerListAnatomicalSite, ref narrative, null);

                if (narrative.Any())
                {
                    structDocTable =  PopulateTable
                        (
                            "Anatomical Site(s)",
                            null,
                            headerListAnatomicalSite.ToArray(),
                            new[] {""},
                            narrative
                        );
                }
            }

            return structDocTable;
        }
		
        /// <summary>
        /// The function creates a Narrative Entry
        /// </summary>
        /// <param name="examinationRequests">A list of examinationRequests</param>
        /// <param name="renderMultiMediaList">A list of renderMultiMediaList</param>
        /// <returns>An IEnumerable of StrucDocTable</returns>
        public static IEnumerable<StrucDocTable> CreateNarrativeEntry(ICollection<IImagingExaminationRequest> examinationRequests, ref List<StrucDocRenderMultiMedia> renderMultiMediaList)
        {
            var narrative = new List<List<Object>>();
            var strucDocTableList = new List<StrucDocTable>();

            var headerList = new List<string>
                               {
                                 "Requested Examination Name",
                                 "Image Date",
                                 "Identifiers",
                                 "Subject Position",
                                 "Image / File"
                               };

            if (examinationRequests != null && examinationRequests.Any())
            {

                foreach (var imageExaminationRequest in examinationRequests)
                {
                    //Narrative text
                    var examinationRequestedName = String.Empty;
                    if (imageExaminationRequest.ExaminationRequestedName != null)
                        imageExaminationRequest.ExaminationRequestedName.ForEach
                            (
                            name => examinationRequestedName += examinationRequestedName.EndsWith(DELIMITER) ? name : !examinationRequestedName.IsNullOrEmptyWhitespace() ? DELIMITER + name : name
                            );

                    //DICOM study ID
                    var studyIdentifier = String.Empty;
                    if (imageExaminationRequest.StudyIdentifier != null)
                    {
                        studyIdentifier = "DICOM Study ID: " + imageExaminationRequest.StudyIdentifier.NarrativeText;
                    }

                    //Report ID
                    var reportIdentifier = String.Empty;
                    if (imageExaminationRequest.ReportIdentifier != null)
                    {
                        reportIdentifier = "Report ID: " + imageExaminationRequest.ReportIdentifier.NarrativeText;
                    }

                    //Image Details
                    var imageDetailSeriesIdentifier = String.Empty;
                    var imageDetailSubjectPosition = String.Empty;
                    var imageDateTime = String.Empty;

                    var imageLink = String.Empty;

                    if (imageExaminationRequest.ImageDetails != null && imageExaminationRequest.ImageDetails.Any())
                    {
                        foreach (var imageDetail in imageExaminationRequest.ImageDetails)
                        {
                            imageDetailSeriesIdentifier += imageDetail.SeriesIdentifier != null ? "DICOM Series ID: " + imageDetail.SeriesIdentifier.NarrativeText : String.Empty;

                            imageDetailSubjectPosition += !imageDetail.SubjectPosition.IsNullOrEmptyWhitespace() ?
                                                           imageDetailSubjectPosition.EndsWith(DELIMITER) ?
                                                           imageDetail.SubjectPosition :!imageDetailSubjectPosition.IsNullOrEmptyWhitespace() ?
                                                           DELIMITER + imageDetail.SubjectPosition : imageDetail.SubjectPosition : String.Empty;

                            if (imageDetail.Image != null)
                            {
                                if (imageDateTime.IsNullOrEmptyWhitespace())
                                {
                                    imageDateTime += imageDetail.DateTime.NarrativeText();
                                }
                                else
                                {
                                    if (!imageDateTime.EndsWith(DELIMITER))
                                    {
                                        imageDateTime += DELIMITER;
                                    }

                                    imageDateTime += imageDetail.DateTime.NarrativeText() + DELIMITER;
                                }
                            }
                        }
                    }

                    //Build a list of image links to alongside the table.
                  List<StrucDocRenderMultiMedia> narrativeDisplayList = null;
                    if (imageExaminationRequest.ImageDetails != null)
                    {
                        foreach (var imageDetails in imageExaminationRequest.ImageDetails)
                        {
                          if (imageDetails.Image != null)
                          {
                            if (narrativeDisplayList == null)
                                narrativeDisplayList = new List<StrucDocRenderMultiMedia>();

                            narrativeDisplayList.AddRange(CreateEncapsulatedData(imageDetails.Image, ref renderMultiMediaList));
                          }
                        }
                    }

                  narrative.Add(new List<Object>
                                   {
                                     examinationRequestedName,
                                     imageDateTime,
                                     studyIdentifier + DELIMITER + reportIdentifier +
                                     DELIMITER + imageDetailSeriesIdentifier,
                                     imageDetailSubjectPosition,
                                     narrativeDisplayList
                                   });
                }

                StripEmptyColoums(ref headerList, ref narrative, null);

                //DICOM Study ID", "Report ID", "DICOM Series ID
                strucDocTableList.Add
                    (
                        PopulateTable
                            (
                                "Examination Request(s) - Image Details",
                                null,
                                headerList.ToArray(),
                                null,
                                narrative
                            )
                    );
            }

            return strucDocTableList;
        }

        /// <summary>
        /// Narrative Entry
        /// </summary>
        /// <param name="resultGroups">A list of resultGroupsresultGroups</param>
        /// <param name="renderMultiMediaList">A list of renderMultiMediaList</param>
        /// <returns>An IEnumerable of StrucDocTable</returns>
        public static IEnumerable<StrucDocTable> CreateNarrativeEntry(ICollection<IImagingResultGroup> resultGroups, ref List<StrucDocRenderMultiMedia> renderMultiMediaList)
        {
            List<List<String>> narrative = null;
            var strucDocTableList = new List<StrucDocTable>();
            var headerList = new List<string> { "Result Group", "Name", "Value", "Status", "Range", "Comment" };

            if (resultGroups != null && resultGroups.Any())
            {
                foreach (var resultGroup in resultGroups)
                {
                    narrative = new List<List<string>>();

                    foreach (var result in resultGroup.Results)
                    {
                        var resultValueReferenceRangeDetails = String.Empty;
                        var testResultsComments = String.Empty;

                        if (result.ResultValueReferenceRangeDetails != null)
                        {
                            foreach (var resultValueReferenceRange in result.ResultValueReferenceRangeDetails)
                            {
                                resultValueReferenceRangeDetails += resultValueReferenceRange.Range.NarrativeText + (resultValueReferenceRange.ResultValueReferenceRangeMeaning != null ? string.Format(" ({0})", resultValueReferenceRange.ResultValueReferenceRangeMeaning.NarrativeText) : string.Empty) + DELIMITER;
                            }
                        }

                        if (result.Comments != null)
                        {
                            foreach (var comment in result.Comments)
                            {
                                testResultsComments += comment + DELIMITER;
                            }
                        }

                        narrative.Add
                            (
                                new List<String>
                                {
                                    resultGroup.ResultGroupName.NarrativeText.IsNullOrEmptyWhitespace() ? String.Empty : resultGroup.ResultGroupName.NarrativeText,
                                    result.ResultName == null ? String.Empty : result.ResultName.NarrativeText,
                                    result.ResultValue == null  ? String.Empty : (result.ResultValue.ValueAsCodableText == null  ? String.Empty : result.ResultValue.ValueAsCodableText.NarrativeText + DELIMITER) + (result.ResultValue.TestResultValue == null  ? String.Empty : result.ResultValue.TestResultValue.NarrativeText + DELIMITER) + (result.ResultValue.ValueAsQuantityRange == null  ? String.Empty : result.ResultValue.ValueAsQuantityRange.NarrativeText + DELIMITER),    
                                    result.NormalStatus.HasValue ? result.NormalStatus.Value.GetAttributeValue<NameAttribute, string>(x => x.Name) : String.Empty,
                                    resultValueReferenceRangeDetails,
                                    testResultsComments,
                                }
                            );
                    }

                    StripEmptyColoums(ref headerList, ref narrative, null);

                    //Build the table for this result group
                    strucDocTableList.Add
                    (
                         PopulateTable
                             (
                                "Result Group(s)",
                                null,
                                headerList.ToArray(),
                                null,
                                narrative
                             )
                    );


                    if (resultGroup.AnatomicalSite != null)
                        strucDocTableList.Add(CreateAnatomicalSites(new List<AnatomicalSite> { resultGroup.AnatomicalSite }, ref renderMultiMediaList));

                }
            }

            return strucDocTableList;
        }

        /// <summary>
        /// A function for CreateNarrativeEntry
        /// </summary>
        /// <param name="immunisations">A list of immunisations</param>
        /// <returns>An IEnumerable of StrucDocTable</returns>
        public static IEnumerable<StrucDocTable> CreateNarrativeEntry(IEnumerable<IImmunisation> immunisations)
        {
          var narrative = new List<List<String>>();
          var strucDocTableList = new List<StrucDocTable>();

          var list = new List<KeyValuePair<DateTime, List<string>>>();

          if (immunisations != null && immunisations.Any())
          {
            foreach (var imunisation in immunisations)
            {
              list.Add(new KeyValuePair<DateTime, List<string>>
                 (
                   imunisation.DateTime != null ? imunisation.DateTime.DateTime : DateTime.MaxValue,
                   new List<String>
                                {
                                    imunisation.Medicine != null ? imunisation.Medicine.NarrativeText : String.Empty,
                                    imunisation.SequenceNumber.HasValue ? imunisation.SequenceNumber.Value.ToString(CultureInfo.InvariantCulture) : String.Empty,
                                    imunisation.DateTime != null ? imunisation.DateTime.NarrativeText() : String.Empty,
                                }
                 ));
            }
          }

          // Sort List
          list.Sort(Compare);

          narrative = list.Select(item => item.Value).ToList();

          var headerListImmunisations = new List<string>
                           {
                             "Vaccine", "Sequence Number", "Date" 
                           };

          StripEmptyColoums(ref headerListImmunisations, ref narrative, null);

          if (narrative.Any())
          {
            strucDocTableList.Add
                (
                    PopulateTable
                    (
                        "Immunisations - Administered Immunisations",
                        null,
                        headerListImmunisations.ToArray(),
                        new string[] { },
                        narrative
                    )
                );
          }

            return strucDocTableList;
        }

        /// <summary>
        /// Creates Narrative Entry for SpecimenDetail
        /// </summary>
        public static IEnumerable<StrucDocTable> CreateNarrativeEntry(IEnumerable<SpecimenDetail> specimenDetails, String narrativeTitle, ref List<List<Object>> anatomicalSiteNarrative, ref List<StrucDocRenderMultiMedia> renderMultiMediaList)
        {
          var strucDocTableList = new List<StrucDocTable>();

            if (specimenDetails != null)
            {
                foreach (var specimenDetail in specimenDetails)
                {
                    var physicalDetailsOutput = string.Empty;

                    List<StrucDocRenderMultiMedia> narrativeDisplayList = null;
                    if (specimenDetail.PhysicalDetails != null)
                    {
                        foreach (var physicalDetails in specimenDetail.PhysicalDetails)
                        {
                            if (physicalDetails != null)
                            {
                                if (physicalDetails.WeightVolume != null)
                                {
                                    physicalDetailsOutput = physicalDetailsOutput + physicalDetails.WeightVolume.Value + " " + physicalDetails.WeightVolume.Units + DELIMITER;
                                }

                                if (physicalDetails.Image != null)
                                {
                                    narrativeDisplayList = new List<StrucDocRenderMultiMedia>();
                                    var encapsulatedData = CreateEncapsulatedData(physicalDetails.Image,
                                        ref renderMultiMediaList);
                                    narrativeDisplayList.AddRange(encapsulatedData);
                                }

                            }
                        }
                    }

                    List<StrucDocRenderMultiMedia> anatomicalEncapsulatedData = null;
                    var anatomicalDisplay = string.Empty;
                    if (specimenDetail.AnatomicalSite != null)
                    {

                        foreach (var anatomicalSite in specimenDetail.AnatomicalSite)
                        {
                            //populate the narrative for each anatomical site
                            if (!anatomicalSite.Description.IsNullOrEmptyWhitespace() ||
                                anatomicalSite.SpecificLocation != null)
                            {
                                anatomicalDisplay = anatomicalSite.Description;

                                if (anatomicalSite.SpecificLocation != null &&
                                    anatomicalSite.SpecificLocation.NameOfLocation != null)
                                {
                                    if (!anatomicalDisplay.IsNullOrEmptyWhitespace())
                                    {
                                        anatomicalDisplay += " ";
                                    }

                                    anatomicalDisplay += anatomicalSite.SpecificLocation.NameOfLocation.NarrativeText;
                                }

                                if (anatomicalSite.SpecificLocation != null &&
                                    anatomicalSite.SpecificLocation.Side != null)
                                {
                                    if (anatomicalDisplay.IsNullOrEmptyWhitespace())
                                    {
                                        anatomicalDisplay += anatomicalSite.SpecificLocation.Side.NarrativeText;
                                    }
                                    else
                                    {
                                        anatomicalDisplay += " (" + anatomicalSite.SpecificLocation.Side.NarrativeText + ")";
                                    }
                                }

                                if (anatomicalSite.Images != null && anatomicalSite.Images.Count > 0)
                                {
                                    anatomicalEncapsulatedData = new List<StrucDocRenderMultiMedia>();
                                    var encapsulatedData = CreateEncapsulatedData(anatomicalSite.Images,
                                        ref renderMultiMediaList);
                                    anatomicalEncapsulatedData.AddRange(encapsulatedData);
                                }

                            }
                        }
                    }

                    var columnHeaders = new List<string> {"Field", "Value"};

                    var narrativeSpecimenDetails = new List<List<Object>>();

                    if (specimenDetail.ReceivedDateTime != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object>
                                {
                                    "Date and Time of Receipt",
                                    specimenDetail.ReceivedDateTime.NarrativeText()
                                }
                            );

                    if (specimenDetail.SpecimenIdentifier != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object>
                                {
                                    "Specimen Identifier",
                                    specimenDetail.SpecimenIdentifier.NarrativeText
                                }
                            );

                    if (specimenDetail.SpecimenTissueType != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object>
                                {
                                    "Specimen Tissue Type",
                                    specimenDetail.SpecimenTissueType.NarrativeText.IsNullOrEmptyWhitespace()
                                        ? String.Empty
                                        : specimenDetail.SpecimenTissueType.NarrativeText
                                }
                            );

                    if (!specimenDetail.PhysicalDescription.IsNullOrEmptyWhitespace())
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object> {"Physical Description", specimenDetail.PhysicalDescription}
                            );

                    if (!physicalDetailsOutput.IsNullOrEmptyWhitespace())
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object> {"Physical Details", physicalDetailsOutput}
                            );

                    if (specimenDetail.SamplingPreconditions != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object>
                                {
                                    "Sampling Preconditions",
                                    specimenDetail.SamplingPreconditions.NarrativeText.IsNullOrEmptyWhitespace()
                                        ? String.Empty
                                        : specimenDetail.SamplingPreconditions.NarrativeText
                                }
                            );


                    if (specimenDetail.CollectionDateTime != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object>
                                {
                                    "Date and Time of Collection",
                                    specimenDetail.CollectionDateTime.NarrativeText()
                                }
                            );

                    if (specimenDetail.CollectionProcedure != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object>
                                {
                                    "Collection Procedure",
                                    specimenDetail.CollectionProcedure.NarrativeText
                                }
                            );

                    if (!specimenDetail.CollectionSetting.IsNullOrEmptyWhitespace())
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object> {"Collection Setting", specimenDetail.CollectionSetting}
                            );

                    if (narrativeDisplayList != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object> {"Physical Details Image / File", narrativeDisplayList}
                            );

                    if (!anatomicalDisplay.IsNullOrEmptyWhitespace())
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object> { "Anatomical Site", anatomicalDisplay }
                            );

                    if (anatomicalEncapsulatedData != null)
                        narrativeSpecimenDetails.Add
                            (
                                new List<Object> { "Anatomical Site Image", anatomicalEncapsulatedData }
                            );


                    strucDocTableList.Add(
                        PopulateTable
                            (
                                narrativeTitle,
                                null,
                                columnHeaders.ToArray(),
                                new[] {string.Empty},
                                narrativeSpecimenDetails
                            )
                        );
                }
            }
            return strucDocTableList;
        }
        /// <summary>
        /// Creates a Boolean Narrative Entry
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, Boolean? value)
        {
            return new List<String>
                       {
                           !heading.IsNullOrEmptyWhitespace() ? heading: "Undefined",  value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : "Undefined"
                       };
        }

        /// <summary>
        /// Creates a string Narrative Entry
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, String value)
        {
            return new List<String>
                       {
                           !heading.IsNullOrEmptyWhitespace() ? heading: "Undefined", !value.IsNullOrEmptyWhitespace() ? value: "Undefined"
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for entitlements
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, IEnumerable<SCSModel.Common.Entitlement> entitlements)
        {
            // Entitlements
            String entitlementsString = null;

            if (entitlements != null)
            {
                if (entitlements.Count() > 0)
                {
                    foreach (var entitlement in entitlements)
                    {
                        if (entitlement != null && entitlement.Type != EntitlementType.Undefined)
                        {
                            entitlementsString += (entitlement.Id != null ? entitlement.Id.NarrativeText : String.Empty);

                            if (entitlements.Count() > 1) entitlementsString += DELIMITER;
                        }
                    }
                }
            }

            return new List<String>
                       {
                          heading, entitlementsString
                       };
        }

        /// <summary>
        /// Creates an Entitlement
        /// </summary>
        public static String CreateEntitlement(IEnumerable<SCSModel.Common.Entitlement> entitlements)
        {
            // Entitlements
            String entitlementsString = String.Empty;

            if (entitlements != null)
            {
                if (entitlements.Any())
                {
                    foreach (var entitlement in entitlements)
                    {
                        if (entitlement != null && entitlement.Type != EntitlementType.Undefined)
                        {
                            entitlementsString += (entitlement.Id != null ? entitlement.Id.NarrativeText : String.Empty);

                            if (entitlements.Count() > 1) entitlementsString += DELIMITER;
                        }
                    }
                }
            }
            return entitlementsString;
        }
        /// <summary>
        /// Create a Narrative Entry for dateAccuracyIndicator
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, DateAccuracyIndicator dateAccuracyIndicator)
        {
            var narrative = new List<String>();

            if(dateAccuracyIndicator != null)
            {
                narrative.Add(!heading.IsNullOrEmptyWhitespace() ? heading : "Undefined");
                narrative.Add((dateAccuracyIndicator.ConvertToEnum()).GetAttributeValue<NameAttribute, String>(x => x.Code));
            }

            return narrative;
        }

        /// <summary>
        /// Creates a Narrative Entry for a Physical Quantity
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, int? value, string unit)
        {
            return new List<String>
                       {
                            !heading.IsNullOrEmptyWhitespace() ? heading: "Undefined",
                            string.Format
                                    (
                                      "{0}{1}", 
                                       value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : "Undefined",
                                       !unit.IsNullOrEmptyWhitespace() ? unit : string.Empty 
                                    )
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for Entitlement
        /// </summary>
        public static List<String> CreateNarrativeEntry(SCSModel.Common.Entitlement entitlement, ICodableText codableText)
        {
            return new List<String>
                       {
                           codableText != null ? codableText.NarrativeText : "Undefined Entitlement",
                           entitlement.Id != null ? entitlement.Id.NarrativeText : String.Empty
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for Statement
        /// </summary>
        public static List<String> CreateNarrativeEntry(Statement statement)
        {
            return new List<string>
                       {
                           statement.Value != null  ? statement.Value.GetAttributeValue<NameAttribute, string>(x => x.Name): String.Empty
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for Date
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, ISO8601DateTime dateTime)
        {
            return new List<string>
                       {
                           heading,
                           dateTime != null
                               ? dateTime.NarrativeText()
                               : String.Empty
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for an int
        /// </summary>
        public static List<String> CreateNarrativeEntry(String heading, int? value)
        {
          return new List<string>
                       {
                           heading,
                           value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : String.Empty
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for an dateTime
        /// </summary>
        public static List<Object> CreateNarrativeEntry(ISO8601DateTime dateTime)
        {
            return new List<Object>
                       {
                           dateTime != null
                               ? dateTime.NarrativeText()
                               : String.Empty
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry  
        /// </summary>
        public static List<String> CreateNarrativeEntry(ISO8601DateTime dateTime, String heading, String value)
        {
            return new List<string>
                       {
                           dateTime != null
                               ? dateTime.NarrativeText()
                               : String.Empty,
                            heading,
                            !value.IsNullOrEmptyWhitespace() 
                                ? value
                                : String.Empty
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry  
        /// </summary>
        public static List<String> CreateNarrativeEntry(ISO8601DateTime dateTime, ISO8601DateTime dateTime2, String value, String value2)
        {
            return new List<string>
                       {
                           dateTime != null
                               ? dateTime.NarrativeText()
                               : String.Empty,
                           dateTime2 != null
                               ? dateTime2.NarrativeText()
                               : String.Empty,
                           !value.IsNullOrEmptyWhitespace() ? value : String.Empty,
                           !value2.IsNullOrEmptyWhitespace() ? value2 : String.Empty,
                       };
        }

        /// <summary>
        /// Creates a Narrative Entry for procedures 
        /// </summary>
        public static String CreateNarrativeEntry(List<IProcedureName> procedures)
        {
            var narrative = String.Empty;

            if (procedures != null && procedures.Any())
            {
                procedures.ForEach
                    (
                        procedure =>
                        {
                            narrative +=
                               procedure != null && procedure.ProcedureName != null
                                   ? procedure.ProcedureName.NarrativeText + DELIMITER
                                   : String.Empty;
                        }
                    );
            }

            return narrative;
        }

        /// <summary>
        /// Creates a Narrative Entry for a codableText List 
        /// </summary>
        public static String CreateNarrativeEntry(List<ICodableText> codableTextList)
        {
            var narrative = String.Empty;

            if (codableTextList != null && codableTextList.Any())
            {
                codableTextList.ForEach
                    (
                        codableText =>
                        {
                            narrative += codableText != null
                               ? codableText.NarrativeText + DELIMITER
                               : String.Empty;
                        }
                    );

            }

            return narrative;
        }

        /// <summary>
        /// Creates a Narrative Entry for a textList list
        /// </summary>
        public static String CreateNarrativeEntry(List<string> textList)
        {
            var narrative = String.Empty;

            if (textList != null && textList.Any())
            {
                textList.ForEach(a => narrative += a.IsNullOrEmptyWhitespace() ? string.Empty : a + DELIMITER);

            }

            return narrative;
        }

        /// <summary>
        /// Creates a Narrative Entry for a codableText
        /// </summary>
        public static String CreateNarrativeEntry(ICodableText codableText)
        {
            var narrative = String.Empty;

            if (codableText != null)
            {
              narrative += codableText.NarrativeText;
            }

            return narrative;
        }
		
		        /// <summary>
        /// Creates the narrative text.
        /// </summary>
        /// <param name="interval">Interval.</param>
        /// <returns>Interval.</returns>
        public static string CreateIntervalText(CdaInterval interval)
        {
            if (interval == null)
            {
                return string.Empty;
            }

            return CdaIntervalFormatter.Format(interval);
        }
		
		      /// <summary>
        /// Strip Empty Columns Remove empty columns from a narrative List and the headerList
        /// </summary>
        /// <param name="headerList">A string List of headers</param>
        /// <param name="narrative">The narrative</param>
        /// <param name="optionalItems">A list of int's if these are included then only these positional can be removed</param>
        public static void StripEmptyColoums(ref List<String> headerList, ref List<List<Object>> narrative, List<int> optionalItems)
        {
          var tableCellRegister = new BitArray(headerList.Count, false);
          // Detect Empty Cells 
          // Remove Item from Header and remove cells from the table
          foreach (var narrativeEntry in narrative)
          {
            for (int i = 0; i < narrativeEntry.Count; i++)
            {
              if (TryConvertTo<string>(narrativeEntry[i]))
              {
                if (!((string) narrativeEntry[i]).IsNullOrEmptyWhitespace() && !tableCellRegister[i])
                {
                  tableCellRegister[i] = true;
                }
              } else
              {
                tableCellRegister[i] = true;
              }
            }
          }

          var found = false;
          for (int i = 0; i < tableCellRegister.Count; i++)
          {
            if (!tableCellRegister[i])
            {
              if (optionalItems == null || (optionalItems.Contains(i)))
              {

                foreach (var narrativeEntry in narrative)
                {
                  narrativeEntry.RemoveAt(i);
                }

                // Perform a Recursive call to ensure that the items in the list that are removed correspond to the correct Position Location
                headerList.RemoveAt(i);
                found = true;
                break;
              }
            }
          }

          // Perform a Recursive call to ensure that the items in the list that are 
          // removed correspond to the correct Position Location
          if (found)
            StripEmptyColoums(ref headerList, ref narrative, optionalItems);
        }

        /// <summary>
        /// Convert an object to a Boolean
        /// </summary>
        public static Boolean TryConvertTo<T>(object input)
        {
          try
          {
            Convert.ChangeType(input, typeof(T));
            return true;
          }
          catch
          {
          }

          return false;
        }

        /// <summary>
        /// Strip Empty Columns Remove empty Column from a narrative List and the headerList
        /// </summary>
        /// <param name="headerList">A string List of headers</param>
        /// <param name="narrative">The narrative</param>
        /// <param name="optionalItems">A list of int's if these are included then only these positional can be removed</param>
        public static void StripEmptyColoums(ref List<String> headerList, ref List<List<String>> narrative, List<int> optionalItems)
        {
            var tableCellRegister = new BitArray(headerList.Count, false);
            // Detect Empty Cells 
            // Remove Item from Header and remove cells from the table
            foreach (var narrativeEntry in narrative)
            {
              for (int i = 0; i < narrativeEntry.Count; i++)
              {
                if (!narrativeEntry[i].IsNullOrEmptyWhitespace() && !tableCellRegister[i])
                {
                  tableCellRegister[i] = true;
                }
              }
            }

            var found = false;
            for (int i = 0; i < tableCellRegister.Count; i++)
            {
              if (!tableCellRegister[i])
              {
                  if (optionalItems == null || (optionalItems.Contains(i)))
                  {

                  foreach (var narrativeEntry in narrative)
                  {
                    narrativeEntry.RemoveAt(i);
                  }

                  // Perform a Recursive call to ensure that the items in the list that are removed correspond to the correct Position Location
                  headerList.RemoveAt(i);
                  found = true;  
                  break;
                }
              }
            }
      
            // Perform a Recursive call to ensure that the items in the list that are 
            // removed correspond to the correct Position Location
            if (found)
              StripEmptyColoums(ref headerList, ref narrative, optionalItems);
        }
		
		
        /// <summary>
        /// Build the person Name
        /// </summary>
        /// <param name="personNames">The Person</param>
        /// <returns>A formatted person name</returns>
        public static String BuildPersonNames(List<IPersonName> personNames)
        {
            if (personNames == null) return string.Empty;

            var nameString = String.Empty;

            for (int x = 0; x < personNames.Count; x++)
            {
                var pn = personNames[x];

                nameString +=
                    pn.Titles != null && pn.Titles.Any() ?
                    pn.Titles.First() + " " :
                    String.Empty;

                nameString +=
                    pn.NameSuffix != null && pn.NameSuffix.Any() ?
                    pn.NameSuffix.First() + " " :
                    String.Empty;

                if (pn.GivenNames != null)
                {
                    nameString += pn.GivenNames.FirstOrDefault();
                }

                if (!pn.FamilyName.IsNullOrEmptyWhitespace())
                {
                  nameString += nameString.IsNullOrEmptyWhitespace() ? pn.FamilyName.ToUpper() : " " + pn.FamilyName.ToUpper();
                }

                if (x < personNames.Count - 1)
                    nameString += DELIMITERBREAK;
            }

            return nameString;
        }


      /// <summary>
      /// Create Address for Narrative
      /// </summary>
      /// <param name="addresses">A List of addresses</param>
      /// <param name="electronicCommunicationDetails">A List of ElectronicCommunicationDetail</param>
      /// <returns>A formatted Address</returns>
      public static String CreateAddress(IEnumerable<IAddress> addresses, IList<ElectronicCommunicationDetail> electronicCommunicationDetails)
        {
            var stringReturn = String.Empty;

            if (addresses != null && addresses.Any())
            {
              foreach (var address in addresses)
              {
                if (address != null)
                {
                  stringReturn += CreateAddress(address);
                }

                stringReturn += DELIMITERBREAK;
              }
            }

            if (electronicCommunicationDetails != null && electronicCommunicationDetails.Any())
            {
                stringReturn += CreateElectronicCommunicationDetails(electronicCommunicationDetails);
            }

            return stringReturn;
        }

        /// <summary>
        /// Create Email for Narrative
        /// </summary>
        /// <returns>A formatted Address</returns>
        public static String CreateEmail(IList<ElectronicCommunicationDetail> electronicCommunicationDetails)
        {
            var emailList = new List<ElectronicCommunicationDetail>();
            foreach(var electronicCommunicationDetail in  electronicCommunicationDetails)
            {
              if (electronicCommunicationDetail != null && electronicCommunicationDetail.Medium == ElectronicCommunicationMedium.Email)
                {
                  emailList.Add(electronicCommunicationDetail);
                }
            }
            return CreateElectronicCommunicationDetails(emailList);
        }

        /// <summary>
        /// Create Phone for Narrative
        /// </summary>
        /// <returns>A formatted Address</returns>
        public static String CreatePhone(IList<ElectronicCommunicationDetail> electronicCommunicationDetails)
        {
          var emailList = new List<ElectronicCommunicationDetail>();
          foreach (var electronicCommunicationDetail in electronicCommunicationDetails)
          {
            if (electronicCommunicationDetail != null && (electronicCommunicationDetail.Medium == ElectronicCommunicationMedium.Mobile || electronicCommunicationDetail.Medium == ElectronicCommunicationMedium.Telephone))
            {
              emailList.Add(electronicCommunicationDetail);
            }
          }
          return CreateElectronicCommunicationDetails(emailList);
        }

        /// <summary>
        /// Create Address for Narrative
        /// </summary>
        /// <returns>A formatted Address</returns>
        public static String CreateElectronicCommunicationDetails(IList<ElectronicCommunicationDetail> electronicCommunicationDetails)
        {
          var stringReturn = String.Empty;

          if (electronicCommunicationDetails != null && electronicCommunicationDetails.Any())
          {

            for (int i = 0; i < electronicCommunicationDetails.Count(); i++)
            {
              if (electronicCommunicationDetails[i] != null)
              {
                string usages = string.Empty;
                for (int index = 0; index < electronicCommunicationDetails[i].Usage.Count; index++)
                {
                  usages += electronicCommunicationDetails[i].Usage[index];

                  if (index + 1 < electronicCommunicationDetails[i].Usage.Count)
                  {
                    usages += ", ";
                  }
                }

                if (electronicCommunicationDetails[i].Medium == ElectronicCommunicationMedium.Email)
                {
                  stringReturn += string.Format("{0}{1}{2}", DELIMITEREMAILSTART, electronicCommunicationDetails[i].Narrative, DELIMITEREMAILEND);
                }
                else
                {
                  stringReturn += string.Format("{0} ({2})", electronicCommunicationDetails[i].Narrative, electronicCommunicationDetails[i].Medium.GetAttributeValue<NameAttribute, string>(x => x.Name), !usages.IsNullOrEmptyWhitespace() ? usages : string.Empty);
                  if (i + 1 < electronicCommunicationDetails.Count())
                    stringReturn += ", ";

                }
              }
            }
          }

          return stringReturn;
        }

        /// <summary>
        /// Create Address for Narrative
        /// </summary>
        /// <param name="address">An address</param>
        /// <returns>A formatted Address</returns>
        public static String CreateAddress(IAddress address)
        {
            var stringReturn = String.Empty;
            
            if (address != null)
            {
                if (address.AustralianAddress != null)
                {
                    if (address.AustralianAddress.UnstructuredAddressLines != null)
                    {
                        foreach (var unstructuredAddressLines in address.AustralianAddress.UnstructuredAddressLines)
                        {
                            stringReturn += unstructuredAddressLines;
                        }
                    }

                    if (address.AustralianAddress.StreetNumber != null)
                    {
                        stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? " " : String.Empty) +
                                        address.AustralianAddress.StreetNumber.Value.ToString(CultureInfo.InvariantCulture);
                    }

                    if (!address.AustralianAddress.StreetName.IsNullOrEmptyWhitespace())
                    {
                        stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? " " : String.Empty) +
                                        address.AustralianAddress.StreetName;
                    }

                    if (address.AustralianAddress.StreetType != StreetType.Undefined)
                    {
                      stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? " " : String.Empty) +
                                      address.AustralianAddress.StreetType.GetAttributeValue<NameAttribute, String>(x => x.Name);
                    }

                    if (!address.AustralianAddress.SuburbTownLocality.IsNullOrEmptyWhitespace())
                    {
                        stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? ", " : String.Empty) +
                                        address.AustralianAddress.SuburbTownLocality;
                    }

                    if (address.AustralianAddress.State != AustralianState.Undefined)
                    {
                      stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? ", " : String.Empty) +
                                      address.AustralianAddress.State.GetAttributeValue<NameAttribute, String>(x => x.Code);
                    }

                    if (!address.AustralianAddress.PostCode.IsNullOrEmptyWhitespace())
                    {
                      stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? ", " : String.Empty) +
                                        address.AustralianAddress.PostCode;
                    }
                }
                else if (address.InternationalAddress != null)
                {

                    if (address.InternationalAddress.AddressLine != null)
                        foreach (var internationalAddressLine in address.InternationalAddress.AddressLine)
                        {
                            stringReturn += internationalAddressLine;
                        }

                    if (!address.InternationalAddress.StateProvince.IsNullOrEmptyWhitespace())
                    {
                        stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? ", " : String.Empty) +
                                        address.InternationalAddress.StateProvince;
                    }

                    if (!address.InternationalAddress.PostCode.IsNullOrEmptyWhitespace())
                    {
                        stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? ", " : String.Empty) +
                                        address.InternationalAddress.PostCode;
                    }

                    if (address.InternationalAddress.Country != Country.Undefined)
                    {
                        stringReturn += (!stringReturn.IsNullOrEmptyWhitespace() ? ", " : String.Empty) +
                                        address.InternationalAddress.Country.GetAttributeValue<NameAttribute, String>(x => x.Name);
                    }
                }

                stringReturn = stringReturn.IsNullOrEmptyWhitespace() ? string.Empty : address.AddressPurpose.GetAttributeValue<NameAttribute, String>(x => x.Name) + ": " + stringReturn;
            }

            return stringReturn;
        }


        /// <summary>
        /// Converts a duration to a string for the narrative.
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static String CreateDuration(CdaInterval duration)
        {
          return duration == null ? string.Empty : CdaIntervalFormatter.Format(duration);
        }

        /// <summary>
        /// Converts a duration to a string in the narrative where Ongoing is permitted in the Narrative.
        /// </summary>
        /// <param name="duration">The CDA Interval Duration</param>
        /// <param name="showOngoingInNarrative">Whether Ongoing should be shown in the narrative</param>
        /// <returns></returns>
        public static String CreateDuration(CdaInterval duration, Boolean? showOngoingInNarrative)
        {
            if (duration != null && duration.Type == IntervalType.Low && duration.Low != null)
            {
                if (showOngoingInNarrative.HasValue && showOngoingInNarrative.Value )
                {
                    return duration.Low.NarrativeText() + " -> " + "(ongoing)";
                }
                return duration.Low.NarrativeText() + " -> ";
            }

            if (showOngoingInNarrative.HasValue && showOngoingInNarrative.Value)
            {
                return "(ongoing)";
            }

            return CdaIntervalFormatter.Format(duration);
        }

        // Compare strings
        static int Compare(KeyValuePair<DateTime, List<string>> a, KeyValuePair<DateTime, List<string>> b)
        {
          return b.Key.CompareTo(a.Key);
        }
		
        /// <summary>
        /// Gets the service scheduled field or the service commencement window field.
        /// </summary>
        /// <param name="requestedService">Requested service.</param>
        /// <returns></returns>
        public string GetServiceScheduledOrServiceCommencementWindow(RequestedService requestedService)
        {
            if (requestedService.ServiceScheduled != null)
            {
                return requestedService.ServiceScheduled.NarrativeText();
            }
            
            if (requestedService.ServiceCommencementWindow != null)
            {
                return CreateDuration(requestedService.ServiceCommencementWindow);
            }

            return string.Empty;
        }

        /// <summary>
        /// Create an exclusion statement for Exclusion Statement
        /// </summary>
        /// <param name="caption">The caption</param>
        /// <param name="statement">The Statement</param>
        /// <returns>An StrucDocItem</returns>
        private StrucDocItem CreateExclusionStatement(string caption, Statement statement)
        {
            StrucDocItem strucDocItem = new StrucDocItem();

            string exclusionStatement;

            if (statement.Value == NCTISGlobalStatementValues.NoneSupplied)
            {
                exclusionStatement = string.Format("No {0} are supplied", caption);
            }
            else
            {
                exclusionStatement = string.Format("{0}: {1}", caption, statement.Value.GetAttributeValue<NameAttribute, string>(x => x.Name)) ;
            }

            strucDocItem.Text = new[] { exclusionStatement };
            return strucDocItem;
        }

        /// <summary>
        /// Create Exclusion Statement 
        /// </summary>
        public StrucDocParagraph CreateExclusionStatementNarrative(string caption, Statement statements)
        {
          return CreateExclusionStatementNarrative(caption, new List<Statement> { statements });
        }


        /// <summary>
        /// Create Exclusion Statement for a list of Statements
        /// </summary>
        public StrucDocParagraph CreateExclusionStatementNarrative(string caption, List<Statement> statements)
        {
            // HEADER
          string exclusionStatements = string.Empty;

          if (statements.Count > 1)
          {
              foreach (var statement in statements)
              {
                exclusionStatements += ", ";

                if (statement.Value == NCTISGlobalStatementValues.NoneSupplied)
                {
                    exclusionStatements += string.Format("No {0} are supplied", caption);
                }
                else
                {
                    exclusionStatements += statement.Value.GetAttributeValue<NameAttribute, string>(x => x.Name);
                }
              }
          } 
            else
          {
              var statement = statements.First();

              if (statement != null)
              {
                  if (statement.Value == NCTISGlobalStatementValues.NoneSupplied)
                  {
                      exclusionStatements = string.Format("No {0} are supplied", caption);
                  }
                  else
                  {
                      exclusionStatements = statement.Value.GetAttributeValue<NameAttribute, string>(x => x.Name);
                  }
              }





          }

          return new StrucDocParagraph {Text = new[] {string.Format("{0}", exclusionStatements) }};
        }

        /// <summary>
        /// Create Exclusion Statement for a Statements
        /// </summary>
        public StrucDocParagraph CreateExclusionStatementNarrative(string caption, string statements)
        {
          return CreateExclusionStatementNarrative(caption, new List<string> { statements });
        }

        /// <summary>
        /// Create Exclusion Statement for a list of Statements
        /// </summary>
        public StrucDocParagraph CreateExclusionStatementNarrative(string caption, List<string> statements)
        {
          // HEADER
          string exclusionStatements = string.Empty;

          if (statements.Count > 1)
          {
            foreach (var statment in statements)
            {
              exclusionStatements += ", ";
              exclusionStatements += statment;
            }
          }
          else
          {
            exclusionStatements = statements.First();
          }

          return new StrucDocParagraph { Text = new[] { string.Format("{0}", exclusionStatements) } };
        }

        /// <summary>
        /// The function populates a StrucDocTable for the provided parameters 
        /// </summary>
        public static StrucDocTable PopulateTable(string caption, string summary, string[] columnHeaders, string[] rowHeadData, string[][] rowData)
        {
            var strucDocTable = new StrucDocTable();

            if (!caption.IsNullOrEmptyWhitespace())
            {
                strucDocTable.caption = new StrucDocCaption
                {
                    Text = new[] { caption }
                };
            }

            if (!summary.IsNullOrEmptyWhitespace())
            {
                strucDocTable.summary = summary;
            }

            // HEADER
            if (columnHeaders != null && columnHeaders.Length > 0)
            {
                strucDocTable.thead = new StrucDocThead
                {
                    tr = new StrucDocTr[1]
                };
                strucDocTable.thead.tr[0] = new StrucDocTr
                {
                    th = new StrucDocTh[columnHeaders.Length]
                };

                for (var i = 0; i < columnHeaders.Length; i++)
                {
                    strucDocTable.thead.tr[0].th[i] = new StrucDocTh
                    {
                        Text = new[] { columnHeaders[i] }
                    };
                }
            }

            //BODY
            if (rowData != null && rowData.Length > 0)
            {
                strucDocTable.tbody = new StrucDocTbody[1];
                strucDocTable.tbody[0] = new StrucDocTbody
                {
                    tr = new StrucDocTr[rowData.Length]
                };

                for (var i = 0; i < rowData.Length; i++)
                {
                    strucDocTable.tbody[0].tr[i] = new StrucDocTr();

                    strucDocTable.tbody[0].tr[i].td = new StrucDocTd[rowData[i].Length];  // FOR EACH COLUMN on a ROW
                    for (var j = 0; j < rowData[i].Length; j++)
                    {
                        strucDocTable.tbody[0].tr[i].td[j] = new StrucDocTd();
                        // CHECK FOR DELIMITER CHARS
                        if
                            (
                                rowData[i][j] != null && (rowData[i][j].Contains(DELIMITER) || rowData[i][j].Contains(DELIMITERBREAK) || rowData[i][j].Contains(DELIMITEREMAILSTART) || rowData[i][j].Contains(DELIMITEREMAILEND))
                            )
                           {
                             // CHECK FOR DELIMITER CHAR FOR LIST
                             if (rowData[i][j] != null && (rowData[i][j].Contains(DELIMITEREMAILSTART)) || rowData[i][j].Contains(DELIMITEREMAILEND))
                             {
                               var data = rowData[i][j];
                               var linkHTML = new List<StrucDocLinkHtml>();
                               var list = new List<string>();
                               while (!GetSubstringByString(DELIMITEREMAILSTART, DELIMITEREMAILEND, data).IsNullOrEmptyWhitespace() )
                               {
                                 var email = GetSubstringByString(DELIMITEREMAILSTART, DELIMITEREMAILEND, data);

                                 linkHTML.Add(
                                   new StrucDocLinkHtml
                                   {
                                     Text = new[] { email },
                                     href = string.Format("mailto:{0}", email)
                                   }
                                 );

                                 data = data.Replace(DELIMITEREMAILSTART + email + DELIMITEREMAILEND, string.Empty);
                               }

                               var strucDocList = new List<StrucDocContent>();

                                if (data != "")
                                    strucDocList.Add(new StrucDocContent { Text = new[] { data } });


                               if (strucDocList.Any())
                                  strucDocTable.tbody[0].tr[i].td[j].content = strucDocList.ToArray();

                               if (linkHTML.Any())
                                  strucDocTable.tbody[0].tr[i].td[j].linkHtml = linkHTML.ToArray();

                               // ReSet the Row Data without the DELIMITEREMAILSTART & DELIMITEREMAILEND tags for the next section
                               rowData[i][j] = data;
                             }

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[i][j] != null && (rowData[i][j].Contains(DELIMITER)))
                            {
                                var data = rowData[i][j];

                                var listItems = SplitWord(data, DELIMITER);

                                var items = new List<StrucDocItem>();
                                foreach(var item in listItems)
                                {
                                  if (!item.IsNullOrEmptyWhitespace())
                                  items.Add(new StrucDocItem { Text = new [] {item}});

                                  data = data.Replace(item + DELIMITER, string.Empty);
                                }

                                //For Lists for the Cell rather than 
                                strucDocTable.tbody[0].tr[i].td[j].list = new [] { new StrucDocList { item = items.ToArray() }};

                                // ReSet the Row Data without the DELIMITEREMAILSTART & DELIMITEREMAILEND tags for the next section
                                rowData[i][j] = data;
                             }

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[i][j] != null && (rowData[i][j].Contains(DELIMITERBREAK)))
                            {
                                var data = rowData[i][j];

                                var listBreaks = SplitWord(data, DELIMITERBREAK);

                                var strucDocList = new List<StrucDocContent>();
                                var firstTimeThrough = true;
                                foreach (var line in listBreaks)
                                {
                                    if (firstTimeThrough)
                                    {
                                        strucDocList.Add(new StrucDocContent {Text = new [] {line}});
                                        firstTimeThrough = false;
                                    } 
                                    else 
                                    {
                                        strucDocList.Add(new StrucDocContent { Text = new [] { line }, br = new [] { string.Empty } });
                                    }

                                }
                                strucDocTable.tbody[0].tr[i].td[j].content = strucDocList.ToArray();
                            }
                        }
                        else
                        {
                            //No DELIMITERS 
                            strucDocTable.tbody[0].tr[i].td[j].Text = new[] { rowData[i][j] };
                        }
                    }
                }
            }

            return strucDocTable;
          }

          /// <summary>
          /// This function Splits Words using a delimiter
          /// </summary>
          public static IEnumerable<string> SplitWord(string source, string delim)
          {
            // argument null checking etc omitted for brevity

            int oldIndex = 0, newIndex;
            while ((newIndex = source.IndexOf(delim, oldIndex)) != -1)
            {
              yield return source.Substring(oldIndex, newIndex - oldIndex);
              oldIndex = newIndex + delim.Length;
            }
            yield return source.Substring(oldIndex);
          }

          /// <summary>
          /// Get a String from a Substring
          /// </summary>
          public static string GetSubstringByString(string a, string b, string c)
          {
            try
            {
              return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
            }
            catch (Exception)
            {
              return null;
            }
          }

        /// <summary>
        /// The function populates a StrucDocTable for the provided parameters 
        /// </summary>
        public static StrucDocTable PopulateTable(string caption, string summary, string[] columnHeaders, string[] rowHeadData, IList<List<string>> rowData)
        {
            var rowDataAsArray = new string[rowData.Count][];

            for (var i = 0; i < rowData.Count; i++)
            {
                if (rowData[i] != null)
                {
                    rowDataAsArray[i] = rowData[i].ToArray();
                }
            }

            return PopulateTable(caption, summary, columnHeaders, rowHeadData, rowDataAsArray);
        }

        /// <summary>
        /// The function populates a StrucDocTable for the provided parameters 
        /// </summary>
        public static StrucDocTable PopulateTable(string caption, string summary, string[] columnHeaders, string[] rowHeadData, List<List<object>> rowData)
        {
            var strucDocTable = new StrucDocTable();

            if (!caption.IsNullOrEmptyWhitespace())
            {
                strucDocTable.caption = new StrucDocCaption
                {
                    Text = new[] { caption }
                };
            }

            if (!summary.IsNullOrEmptyWhitespace())
            {
                strucDocTable.summary = summary;
            }

            // HEADER
            if (columnHeaders != null && columnHeaders.Length > 0)
            {
                strucDocTable.thead = new StrucDocThead
                {
                    tr = new StrucDocTr[1]
                };
                strucDocTable.thead.tr[0] = new StrucDocTr
                {
                    th = new StrucDocTh[columnHeaders.Length]
                };

                for (var i = 0; i < columnHeaders.Length; i++)
                {
                    strucDocTable.thead.tr[0].th[i] = new StrucDocTh
                    {
                        Text = new[] { columnHeaders[i] }
                    };
                }
            }

            //BODY
            if (rowData != null && rowData.Any())
            {
                strucDocTable.tbody = new StrucDocTbody[1];
                strucDocTable.tbody[0] = new StrucDocTbody
                {
                    tr = new StrucDocTr[rowData.Count()]
                };

                for (var rowCounter = 0; rowCounter < rowData.Count; rowCounter++)
                {
                    strucDocTable.tbody[0].tr[rowCounter] = new StrucDocTr { td = new StrucDocTd[rowData[rowCounter].Count] };

                    // FOR EACH COLUMN on a ROW

                    for (var cellCounter = 0; cellCounter < rowData[rowCounter].Count; cellCounter++)
                    {
                        strucDocTable.tbody[0].tr[rowCounter].td[cellCounter] = new StrucDocTd();

                        // CHECK FOR DELIMITER CHARS
                        if (
                                rowData[rowCounter][cellCounter] is String &&
                                (((string)rowData[rowCounter][cellCounter]).Contains(DELIMITER) || ((string)rowData[rowCounter][cellCounter]).Contains(DELIMITERBREAK) || ((string)rowData[rowCounter][cellCounter]).Contains(DELIMITEREMAILSTART) || ((string)rowData[rowCounter][cellCounter]).Contains(DELIMITEREMAILEND) || ((string)rowData[rowCounter][cellCounter]).Contains(DELIMITERBOLD) || ((string)rowData[rowCounter][cellCounter]).Contains(XCOLWIDTHDATE)))  
                          {

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[rowCounter][cellCounter] != null && (((string)rowData[rowCounter][cellCounter]).Contains(DELIMITEREMAILSTART)) || ((string)rowData[rowCounter][cellCounter]).Contains(DELIMITEREMAILEND))
                            {
                                var data = rowData[rowCounter][cellCounter];
                                var linkHtml = new List<StrucDocLinkHtml>();
                                while (!GetSubstringByString(DELIMITEREMAILSTART, DELIMITEREMAILEND, ((string)data)).IsNullOrEmptyWhitespace())
                                {
                                    var email = GetSubstringByString(DELIMITEREMAILSTART, DELIMITEREMAILEND, ((string)data));

                                    linkHtml.Add(
                                      new StrucDocLinkHtml
                                      {
                                          Text = new[] { email },
                                          href = string.Format("mailto:{0}", email)
                                      }
                                    );

                                    data = ((string)data).Replace(DELIMITEREMAILSTART + email + DELIMITEREMAILEND, string.Empty);
                                }

                                var strucDocList = new List<StrucDocContent>();

                                if (data != "")
                                    strucDocList.Add(new StrucDocContent { Text = new[] { ((string)data) } });

                                if (strucDocList.Any())
                                    strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].content = strucDocList.ToArray();

                                if (linkHtml.Any())
                                    strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].linkHtml = linkHtml.ToArray();

                                // ReSet the Row Data without the DELIMITEREMAILSTART & DELIMITEREMAILEND tags for the next section
                                rowData[rowCounter][cellCounter] = data;
                            }

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[rowCounter][cellCounter] != null && (((string)rowData[rowCounter][cellCounter]).Contains(DELIMITER)))
                            {
                                var data = rowData[rowCounter][cellCounter];
                                var listItems = SplitWord(((string)data), DELIMITER);

                                var items = new List<StrucDocItem>();
                                foreach (var item in listItems)
                                {
                                    if (!item.IsNullOrEmptyWhitespace())
                                        items.Add(new StrucDocItem { Text = new[] { item } });

                                    data = ((string)data).Replace(item + DELIMITER, string.Empty);
                                }

                                //For Lists for the Cell rather than 
                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].list = new[] { new StrucDocList { item = items.ToArray() } };

                                // ReSet the Row Data without the DELIMITEREMAILSTART & DELIMITEREMAILEND tags for the next section
                                rowData[rowCounter][cellCounter] = data;
                            }

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[rowCounter][cellCounter] != null && (((string)rowData[rowCounter][cellCounter]).Contains(DELIMITERBREAK)))
                            {
                                var data = rowData[rowCounter][cellCounter];

                                var listBreaks = SplitWord(((string)data), DELIMITERBREAK);

                                var strucDocList = new List<StrucDocContent>();
                                var firstTimeThrough = true;
                                foreach (var line in listBreaks)
                                {
                                    if (firstTimeThrough)
                                    {
                                        strucDocList.Add(new StrucDocContent { Text = new[] { line } });
                                        firstTimeThrough = false;
                                    }
                                    else
                                    {
                                        strucDocList.Add(new StrucDocContent { Text = new[] { line }, br = new[] { string.Empty } });
                                    }

                                }
                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].content = strucDocList.ToArray();
                            }

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[rowCounter][cellCounter] != null && (((string)rowData[rowCounter][cellCounter]).Contains(DELIMITERBOLD)))
                            {
                                var data = rowData[rowCounter][cellCounter];

                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].Text = new[]
                                {
                                      rowData[rowCounter][cellCounter].ToString().Replace(DELIMITERBOLD,string.Empty)
                                };

                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].styleCode = "Bold";

                                // ReSet the Row Data without the DELIMITEREMAILSTART & DELIMITEREMAILEND tags for the next section
                                rowData[rowCounter][cellCounter] = data;
                            }

                            // CHECK FOR DELIMITER CHAR FOR LIST
                            if (rowData[rowCounter][cellCounter] != null && (((string)rowData[rowCounter][cellCounter]).Contains(XCOLWIDTHDATE)))
                            {
                                var data = rowData[rowCounter][cellCounter];

                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].Text = new[]
                                {
                                      rowData[rowCounter][cellCounter].ToString().Replace(XCOLWIDTHDATE,string.Empty)
                                };

                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].styleCode = XCOLWIDTHDATE;

                                // ReSet the Row Data without the DELIMITEREMAILSTART & DELIMITEREMAILEND tags for the next section
                                rowData[rowCounter][cellCounter] = data;
                            }

                        }
                        else
                        {
                            //No DELIMITERS
                            if (rowData[rowCounter][cellCounter] is String)
                            {
                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].Text = new[] { rowData[rowCounter][cellCounter] as String };
                            }

                            if (rowData[rowCounter][cellCounter] is StrucDocRenderMultiMedia)
                            {
                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].renderMultiMedia = new[] { rowData[rowCounter][cellCounter] as StrucDocRenderMultiMedia };
                            }

                            if (rowData[rowCounter][cellCounter] is StrucDocLinkHtml)
                            {
                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].content = new[]
                                                                                                {
                                                                                                    new StrucDocContent{
                                                                                                    linkHtml = new []
                                                                                                                 {
                                                                                                                     rowData[rowCounter][cellCounter] as StrucDocLinkHtml 
                                                                                                                 } 
                                                                                                    }
                                                                                                };
                            }

                            if (rowData[rowCounter][cellCounter] is List<StrucDocRenderMultiMedia>)
                            {
                                strucDocTable.tbody[0].tr[rowCounter].td[cellCounter].renderMultiMedia = ((List<StrucDocRenderMultiMedia>)rowData[rowCounter][cellCounter]).ToArray();
                            }
                        }
                    }

                    var collectionContainsMultiMedia = false;

                    rowData[rowCounter].ForEach(
                                                    innerItem =>
                                                    {
                                                        if (
                                                                !collectionContainsMultiMedia &&
                                                                    innerItem is StrucDocRenderMultiMedia ||
                                                                    innerItem is List<StrucDocRenderMultiMedia>
                                                            )
                                                        {
                                                            collectionContainsMultiMedia = true;
                                                        }
                                                    }
                                                );

                    // CHECK FOR AN EMPTY IMAGE CELL
                    if (
                            rowData[rowCounter].Count > 1 &&
                            collectionContainsMultiMedia &&
                            (
                                strucDocTable.tbody[0].tr[rowCounter].td[rowData[rowCounter].Count - 1].renderMultiMedia == null ||
                                strucDocTable.tbody[0].tr[rowCounter].td[rowData[rowCounter].Count - 1].renderMultiMedia.Count() == 0
                            ) && strucDocTable.tbody[0].tr[rowCounter].td[rowData[rowCounter].Count - 1].Text == null
                        )
                    {
                        strucDocTable.tbody[0].tr[rowCounter].td[rowData[rowCounter].Count - 1].Text = new[] { "See above.." };
                    }

                }
            }

            return strucDocTable;
        }

        /// <summary>
        /// Creates a Pre Narrative given a string
        /// </summary>
        public static StrucDocTable populateTablexPreNarrative(string xPreNarrative)
        {
            return
            (
                new StrucDocTable
                {
                    tbody = new[]
                    {
                        new StrucDocTbody
                        {
                            tr = new[]
                            {
                                new StrucDocTr
                                {
                                    td = new[]
                                    {
                                        new StrucDocTd
                                        {
                                            paragraph = new[]
                                            {
                                                new StrucDocParagraph
                                                {
                                                    styleCode = "xPre",
                                                    Text = new[] {xPreNarrative},
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            );
        }

        /// <summary>
        /// This function Calculates the  Number Of Delimiters
        /// </summary>
         public static Int32 CalculateNumberOfDelimiters(string search, string delimiter)
        {
            var ctr = 0;

            if (search.Contains(delimiter))
            {
                if (search.StartsWith(delimiter))
                {
                    //Remove the starting delimiter before performing any delimiter checking
                    //Note; the starting delimiter is processed by the base case, where we are 
                    //      left with an empty string at the end of the delimiter processing.
                    search = search.Substring(search.IndexOf(delimiter) + delimiter.Length);
                }

                if (search.Contains(delimiter))
                {
                    do
                    {
                        search = search.Substring(search.IndexOf(delimiter) + delimiter.Length);
                        ctr++;
                    }
                    while 
                    (
                        search.IndexOf(delimiter) != -1
                    );
                }

                if (!search.IsNullOrEmptyWhitespace())
                {
                    //We have some more data, increment the delimiter count; thus ensuring that this
                    //data gets rendered within the populate table method.
                    ctr++;
                }
            }
            return ctr;
        }

        /// <summary>
        /// CreateEncapsulatedData
        /// </summary>
        /// <param name="encapsulatedData">The ExternalData item</param>
        /// <param name="sectionHeaderList">This item gets inserted into the section header </param>
        /// <returns>This item gets inserted into the narrative</returns>
        public static List<StrucDocRenderMultiMedia> CreateEncapsulatedData(ExternalData encapsulatedData, ref List<StrucDocRenderMultiMedia> sectionHeaderList)
        {
            return CreateEncapsulatedData((new List<ExternalData> { encapsulatedData }), ref sectionHeaderList);
        }

        /// <summary>
        /// CreateEncapsulatedData
        /// </summary>
        /// <param name="encapsulatedData">The ExternalData[] items</param>
        /// <param name="sectionHeaderList">This item gets inserted into the section header</param>
        /// <returns>This item gets inserted into the narrative</returns>
        public static List<StrucDocRenderMultiMedia> CreateEncapsulatedData(List<ExternalData> encapsulatedData, ref List<StrucDocRenderMultiMedia> sectionHeaderList)
        {
            var tableEntryRenderMultiMedia = new List<StrucDocRenderMultiMedia>();

            if (encapsulatedData != null)
            {
                foreach (var dataItem in encapsulatedData)
                {
                    if (dataItem != null)
                    {
                      if (dataItem.ExternalDataMediaType != MediaType.TXT && dataItem.ExternalDataMediaType != MediaType.PDF && dataItem.ExternalDataMediaType != MediaType.TIFF)
                        {
                            if (
                                !sectionHeaderList.Select(multiMediaItem => multiMediaItem.referencedObject).Contains(dataItem.ID))
                                {
                                   sectionHeaderList.Add(dataItem.ConvertToStrucDocRenderMultiMedia());
                                }
                        }
                        else
                        {
                            tableEntryRenderMultiMedia.Add(dataItem.ConvertToStrucDocRenderMultiMedia());
                        }
                    } 
                }
            }
            return tableEntryRenderMultiMedia;
        }		
		
        /// <summary>
        /// Creates a link for a StrucDocLinkHtml
        /// </summary>
        public StrucDocLinkHtml CreateLink(Link link, Boolean hideLinkId)
        {
           var narrativeLink = CreateLink(link);

           if (hideLinkId)
           {
             narrativeLink.ID = null;
           }

          return narrativeLink;
        }

        /// <summary>
        /// Creates a link for a StrucDocLinkHtml
        /// </summary>
        public StrucDocLinkHtml CreateLink(Link link, Boolean hideLinkId, string customTitle)
        {
            var narrativeLink = CreateLink(link, customTitle);

            if (hideLinkId)
            {
                narrativeLink.ID = null;
            }

            return narrativeLink;
        }

        /// <summary>
        /// Creates a link for a StrucDocLinkHtml
        /// </summary>
        public StrucDocLinkHtml CreateLink(Link link)
        {
            return CreateLink(link, link.TemplateId.GetAttributeValue<NameAttribute, string>(x => x.Title));
        }

        /// <summary>
        /// Creates a link for a StrucDocLinkHtml
        /// </summary>
        public StrucDocLinkHtml CreateLink(Link link, string title)
        {
            const string extentionIdentifier = "^";

            return new StrucDocLinkHtml
            {
                ID = link.id.ToString(),
                Text = new[] { title },
                href = string.Format("pcehr:{0}/{1}{2}",
                                     link.RepositoryIdentifier,
                                     link.DocumentIdentifier,
                                     link.DocumentIdentifierExtension.IsNullOrEmptyWhitespace()
                                     ? string.Empty
                                     : extentionIdentifier + link.DocumentIdentifierExtension)
            };
        }

        /// <summary>
        /// Creates a link for a ExternalLink
        /// </summary>
        public StrucDocLinkHtml CreateExternalLink(ExternalLink externalLink)
        {
            return new StrucDocLinkHtml
            {
                Text = !externalLink.Description.IsNullOrEmptyWhitespace() ? new[] { externalLink.Description } : null,
                href = !externalLink.Url.IsNullOrEmptyWhitespace() ? externalLink.Url : null
            };
        }

        /// <summary>
        /// Creates a link for a ExternalLink
        /// </summary>
        public StrucDocLinkHtml CreateExternalLink(RelatedImage relatedImage, string description)
        {
            return new StrucDocLinkHtml
            {
                Text = new[] { description },
                href = !relatedImage.ImageUrl.IsNullOrEmptyWhitespace() ? relatedImage.ImageUrl : null
            };
        }

        /// <summary>
        /// Creates a SimpleHtmlLink
        /// </summary>
        /// <returns>A StrucDocLinkHtml</returns>
        private StrucDocLinkHtml CreateSimpleHtmlLink(ExternalData externalData)
        {
            string filePath;
            if (externalData.ByteArrayInput != null)
               filePath = !externalData.ByteArrayInput.FileName.IsNullOrEmptyWhitespace() ? Path.GetFileName(externalData.ByteArrayInput.FileName) : null;
            else
               filePath = !externalData.Path.IsNullOrEmptyWhitespace() ? Path.GetFileName(externalData.Path) : null;

            return new StrucDocLinkHtml
            {
                href = filePath,
                Text = !externalData.Caption.IsNullOrEmptyWhitespace() ? new[] { externalData.Caption } : null,
                ID = !externalData.ID.IsNullOrEmptyWhitespace() ? externalData.ID : null
            };
        }
    }
}
    
