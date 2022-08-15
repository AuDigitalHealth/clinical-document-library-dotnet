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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.CDAModel.Entities;
using CDA.Generator.Common.Common.Time;
using CDA.Generator.Common.Common.Time.Enum;
using CDA.Generator.Common.SCSModel.Common.Entities;
using CDA.Generator.Common.SCSModel.ConsolidatedView.Entities;
using CDA.Generator.Common.SCSModel.DiagnosticImagingReport.Entities;
using CDA.Generator.Common.SCSModel.Entities;
using CDA.Generator.Common.SCSModel.Interfaces;
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.Generator.Helper;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.Common;

using AdverseReactions = Nehta.VendorLibrary.CDA.SCSModel.Common.AdverseReactions;
using DiagnosticInvestigations = Nehta.VendorLibrary.CDA.SCSModel.DiagnosticInvestigations;
using Entitlement = Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement;
using ImageDetails = Nehta.VendorLibrary.CDA.SCSModel.Common.ImageDetails;
using IServiceProvider = Nehta.VendorLibrary.CDA.SCSModel.IServiceProvider;
using ImagingExaminationResult = Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.ImagingExaminationResult;
using Participant = Nehta.VendorLibrary.CDA.SCSModel.Common.Participant;
using Participation = Nehta.VendorLibrary.CDA.SCSModel.Common.Participation;
using PathologyTestResult = Nehta.VendorLibrary.CDA.SCSModel.Common.PathologyTestResult;
using ProblemDiagnosis = Nehta.VendorLibrary.CDA.SCSModel.Common.ProblemDiagnosis;
using ResultGroup = Nehta.VendorLibrary.CDA.SCSModel.Common.ResultGroup;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// This class encapsulates all the properties that are common between the various CDA documents
    /// </summary>
    [Serializable]
    [DataContract]
    public class BaseCDAModel
    {
        #region Constants

        private const String HEALTH_IDENTIFIER_QUALIFIER = "1.2.36.1.2001.1003.0.";
        private const String EXTERNAL_HEALTH_IDENTIFIER_QUALIFIER = "1.2.36.1.2001.1005.41.";

        #endregion

        #region Properties

        /// <summary>
        /// The Document Creation Time of this CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DocumentCreationTime { get; set; }

        /// <summary>
        /// The status of this CDA document
        /// </summary>
        [DataMember]
        public DocumentStatus DocumentStatus { get; set; }

        /// <summary>
        /// The title of this CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// The subtype title of this CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string SubTypeTitle { get; set; }

        /// <summary>
        /// Indicates if the CDA document should include a logo
        /// </summary>
        [DataMember]
        public Boolean IncludeLogo { get; set; }

        /// <summary>
        /// This is optional field, this will copy the logo file to the output directory.
        /// If LogoPath is not populated and IncludeLogo is true the assumption is the user will 
        /// manually copy the image file to the output directory.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string LogoPath { get; set; }

        /// <summary>
        /// If populated this will read the logo file from memory instead of using the file system.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public byte[] LogoByte { get; set; }

        /// <summary>
        /// Show Administration Observations title and narrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? ShowAdministrativeObservationsNarrativeAndTitle { get; set; }

        /// <summary>
        /// Show Administration Observations in the Narrative
        /// NOTE: This currently fails schematron V 3.0.4.0
        ///       This is a valid only for point to point usage
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? ShowAdministrativeObservationsSection { get; set; }

        #endregion

        #region static methods

        /// <summary>
        /// Creates a legal authenticator
        /// </summary>
        /// <returns>An Authenticator Object</returns>
        public static IParticipationLegalAuthenticator CreateLegalAuthenticator()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an authenticator
        /// </summary>
        /// <returns>An Authenticator Object</returns>
        public static IParticipationLegalAuthenticator CreateAuthenticator()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an information recipient
        /// </summary>
        /// <returns>Recipient</returns>
        public static IParticipationInformationRecipient CreateInformationRecipient()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a person that is constrained down to an IPersonName
        /// </summary>
        /// <returns>(IPersonName) person</returns>
        public static IPersonName CreatePersonName()
        {
            return new PersonName();
        }

        /// <summary>
        /// Creates a person constrained for a Person With Organisation
        /// </summary>
        /// <returns>(IPerson) Person With Organisation</returns>
        public static IPersonWithOrganisation CreatePersonWithOrganisation()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a person constrained for a Person Consumer
        /// </summary>
        /// <returns>(IPerson) Person With Organisation</returns>
        public static IPersonConsumer CreatePersonConsumer()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a person constrained for a IPersonWithRelationship
        /// </summary>
        /// <returns>(IPersonWithRelationship) Person With Relationship</returns>
        public static IPersonWithRelationship CreatePersonWithRelationship()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a address that is constrained down to an IAddress
        /// </summary>
        /// <returns>(IAddress) address</returns>
        public static IAddress CreateAddress()
        {
            return new Address();
        }

        /// <summary>
        /// Creates an international address
        /// </summary>
        /// <returns>InternationalAddress</returns>
        public static InternationalAddress CreateInternationalAddress()
        {
            return new InternationalAddress();
        }

        /// <summary>
        /// Creates a recipient
        /// </summary>
        /// <returns>A Recipient Object</returns>
        public static IInformationRecipient CreateRecipient()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an ElectronicCommunicationDetail Object
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="medium">Medium; E.g. Email</param>
        /// <param name="usage">Usage; E.g. Home</param>
        /// <returns>An ElectronicCommunicationDetail Object</returns>
        public static ElectronicCommunicationDetail CreateElectronicCommunicationDetail(string address,
            ElectronicCommunicationMedium medium, ElectronicCommunicationUsage usage)
        {
            return new ElectronicCommunicationDetail
            {
                Address = address,
                Medium = medium,
                Usage = new List<ElectronicCommunicationUsage> {usage}
            };
        }

        /// <summary>
        /// Creates an ElectronicCommunicationDetail Object
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="medium">Medium; E.g. Email</param>
        /// <param name="usage">Usage; E.g. Home</param>
        /// <returns>An ElectronicCommunicationDetail Object</returns>
        public static ElectronicCommunicationDetail CreateElectronicCommunicationDetail(string address,
            ElectronicCommunicationMedium medium, List<ElectronicCommunicationUsage> usage)
        {
            return new ElectronicCommunicationDetail
            {
                Address = address,
                Medium = medium,
                Usage = usage
            };
        }

        /// <summary>
        /// Creates an ElectronicCommunicationDetail Object
        /// </summary>
        /// <returns>An ElectronicCommunicationDetail Object</returns>
        public static ElectronicCommunicationDetail CreateElectronicCommunicationDetail()
        {
            return new ElectronicCommunicationDetail();
        }

        /// <summary>
        /// Creates an Australian Address
        /// </summary>
        /// <returns>(AustralianAddress) Australian Address</returns>
        public static AustralianAddress CreateAustralianAddress()
        {
            return new AustralianAddress();
        }

        /// <summary>
        /// Creates a custodian
        /// </summary>
        /// <returns>(IParticipationCustodian) Custodian</returns>
        public static IParticipationCustodian CreateCustodian()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a custodian
        /// </summary>
        /// <returns>(Custodian) Custodian</returns>
        public static ICustodian CreateParticipantCustodian()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a participation constrained down to an IParticipationSubjectOfCare
        /// </summary>
        /// <returns>(IParticipationSubjectOfCare) Participation</returns>
        public static IParticipationSubjectOfCare CreateSubjectOfCare()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participation constrained down to an IParticipationDocumentAuthor
        /// </summary>
        /// <returns>(IParticipationDocumentAuthor) Participation</returns>
        public static IParticipationDocumentAuthor CreateAuthor()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participation constrained down to an ISubjectOfCare
        /// </summary>
        /// <returns>(ISubjectOfCare) Participation</returns>
        public static ISubjectOfCare CreateParticipantForSubjectOfCare()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a participation constrained down to an ISubjectOfCare
        /// </summary>
        /// <returns>(ISubjectOfCare) Participation</returns>
        public static ILegalAuthenticator CreateParticipantForLegalAuthenticator()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a person constrained down to an IPersonSubjectOfCare
        /// </summary>
        /// <returns>(IPersonSubjectOfCare) Person</returns>
        public static IPersonSubjectOfCare CreatePersonForSubjectOfCare()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a person constrained down to an IPerson
        /// </summary>
        /// <returns>(IPerson) Person</returns>
        public static IPerson CreatePerson()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a person constrained down to an IPerson
        /// </summary>
        /// <returns>(IPerson) Person</returns>
        public static IPersonPrescriber CreatePersonPrescriber()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a person constrained down to an IInformationRecipient
        /// </summary>
        /// <returns>(IInformationRecipient) Participant</returns>
        public static IInformationRecipient CreateParticipantForInformationRecipient()
        {
            return new Participant();
        }

        /// <summary>
        /// Create Participant for a IServiceRequester  
        /// </summary>
        /// <returns>(IServiceRequester) Participant</returns>
        public static IServiceRequester CreateParticipantForServiceRequester()
        {
            return new Participant();
        }

        /// <summary>
        /// Create Participation for a IParticipationServiceProvider
        /// </summary>
        /// <returns>(IParticipationServiceRequester) Participation</returns>
        public static IParticipationServiceRequester CreateServiceRequester()
        {
            return new Participation();
        }

        /// <summary>
        /// Create Participant for a ServiceProvider   
        /// </summary>
        /// <returns>(IServiceProvider) Participant</returns>
        public static IServiceProvider CreateParticipantForServiceProvider()
        {
            return new Participant();
        }

        /// <summary>
        /// Create Participation for a IParticipationServiceProvider
        /// </summary>
        /// <returns>(IParticipationServiceProvider) Participation</returns>
        public static IParticipationServiceProvider CreateServiceProvider()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a Participant constrained down to an IAuthorHealthcareProvider
        /// </summary>
        /// <returns>(IAuthorHealthcareProvider) Participant</returns>
        public static IAuthorHealthcareProvider CreateParticipantForAuthorHealthcareProvider()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a Participant constrained down to an IAuthorNonHealthcareProvider
        /// </summary>
        /// <returns>(IAuthorNonHealthcareProvider) Participant</returns>
        public static IAuthorPerson CreateParticipantForAuthorPerson()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a Participant constrained down to an IHealthcareFacility
        /// </summary>
        /// <returns>(Participant) IAuthorNonHealthcareProvider</returns>
        public static IHealthcareFacility CreateParticipantForHealthcareFacility()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a Participant constrained down to an IInformationProviderHealthcareProvider
        /// </summary>
        /// <returns>(Participant) IInformationProviderHealthcareProvider</returns>
        public static IInformationProviderHealthcareProvider CreateParticipantForInformationProviderHealthcareProvider()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a Participation constrained down to an IInformationProviderHealthcareProvider
        /// </summary>
        /// <returns>(Participation) IInformationProviderHealthcareProvider</returns>
        public static IParticipationAuthorHealthcareProvider CreateAuthorHealthcareProvider()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a Participation constrained down to an IParticipationAuthorNonHealthcareProvider
        /// </summary>
        /// <returns>(Participation) IParticipationAuthorNonHealthcareProvider</returns>
        public static IParticipationAuthorPerson CreateAuthorPerson()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a Person constrained down to an IPersonHealthcareProvider
        /// </summary>
        /// <returns>(Person) IPersonHealthcareProvider</returns>
        public static IPersonHealthcareProvider CreatePersonHealthcareProvider()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a Person constrained down to an IPerson
        /// </summary>
        /// <returns>(Person) IPerson</returns>
        public static IPerson CreatePersonNonHealthcareProvider()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a AuthorAuthoringDevice Participant
        /// </summary>
        /// <returns>AuthorAuthoringDevice</returns>
        public static AuthorAuthoringDevice CreateAuthorAuthoringDevice()
        {
            return new AuthorAuthoringDevice();
        }

        /// <summary>
        /// Creates an entitlement
        /// </summary>
        /// <returns>An Entitlement Object</returns>
        public static Entitlement CreateEntitlement()
        {
            return new Entitlement();
        }

        /// <summary>
        /// Create an MedicalRecordNumber object
        /// </summary>
        /// <param name="medicalRecordNumber">The Medical Record Number</param>
        /// <param name="root">The organisations HPIO</param>
        /// <param name="organisationName">The Organisation Name</param>
        /// <returns>MRN</returns>
        public static Identifier CreateMedicalRecordNumber(String medicalRecordNumber, String root,
            String organisationName)
        {
            return
                CreateIdentifier
                (
                    organisationName,
                    null,
                    medicalRecordNumber,
                    root,
                    CreateCodableText
                    (
                        "MR",
                        CodingSystem.HL7IdentifierType,
                        null,
                        null,
                        null
                    )
                );
        }

        /// <summary>
        /// Create a CreateMedicareNumber object
        /// </summary>
        /// <param name="medicalRecordNumber">MedicareNumberType</param>
        /// <param name="medicareNumber">medicalRecordNumber</param>
        /// <returns>MRN</returns>
        public static Identifier CreateMedicareNumber(MedicareNumberType medicalRecordNumber, string medicareNumber)
        {
            // http://jira.nehta.net.au/browse/IPCDOCS-50
            // Accept a medicare length of 11 or 10 digits

            if (!(medicareNumber.Length == 11 || medicareNumber.Length == 10))
                throw new ArgumentException("An Medicare Card Number must have 10 or 11 digits");

            return
                CreateIdentifier
                (
                    medicalRecordNumber.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    null,
                    medicareNumber,
                    medicalRecordNumber.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    CreateCodableText
                    (
                        "MC",
                        CodingSystem.HL7IdentifierType,
                        null,
                        null,
                        null
                    )
                );
        }

        /// <summary>
        /// Create a CreatePrescriberNumber object
        /// </summary>
        /// <param name="prescriberType">prescriberType</param>
        /// <param name="prescriberNumber">prescriberNumber</param>
        /// <returns>MRN</returns>
        public static Identifier CreatePrescriberNumber(IdentifierType prescriberType, string prescriberNumber)
        {

            return
                CreateIdentifier
                (
                    prescriberType.GetAttributeValue<NameAttribute, string>(x => x.Name),
                    null,
                    prescriberNumber,
                    prescriberType.GetAttributeValue<NameAttribute, string>(x => x.Code),
                    CreateCodableText
                    (
                        "MC",
                        CodingSystem.HL7IdentifierType,
                        null,
                        null,
                        null
                    )
                );
        }

        /// <summary>
        /// Create a CreateMedicareNumber object
        /// </summary>
        /// <param name="medicareNumber">medicalRecordNumber</param>
        /// <returns>MRN</returns>
        public static Identifier CreateIndividualMedicareNumber(string medicareNumber)
        {
            if (medicareNumber.Length != 11)
                throw new ArgumentException("An Individual Medicare Card Number must have 11 digits");

            return
                CreateIdentifier
                (
                    MedicareNumberType.IndividualMedicareCardNumber.GetAttributeValue<NameAttribute, string>(
                        x => x.Name),
                    null,
                    medicareNumber,
                    MedicareNumberType.IndividualMedicareCardNumber.GetAttributeValue<NameAttribute, string>(
                        x => x.Code),
                    CreateCodableText
                    (
                        "MC",
                        CodingSystem.HL7IdentifierType,
                        null,
                        null,
                        null
                    )
                );
        }

        /// <summary>
        /// Creates a DVA entitlement.
        /// </summary>
        /// <param name="dvaNumber">DVA number.</param>
        /// <param name="entitlementType">Entitlement type.</param>
        /// <param name="validityDuration">Entitlement validity duration.</param>
        /// <returns>Entitlement.</returns>
        public static Entitlement CreateDvaEntitlement(string dvaNumber, EntitlementType entitlementType,
            CdaInterval validityDuration)
        {
            var entitlement = new Entitlement
            {
                Id = CreateDvaNumber(dvaNumber, entitlementType),
                Type = entitlementType,
                ValidityDuration = validityDuration
            };

            return entitlement;
        }

        /// <summary>
        /// Create a DVA entitlement with no validity duration.
        /// </summary>
        /// <param name="dvaNumber">DVA number.</param>
        /// <param name="entitlementType">Entitlement type.</param>
        /// <returns>Entitlement.</returns>
        public static Entitlement CreateDvaEntitlement(string dvaNumber, EntitlementType entitlementType)
        {
            return CreateDvaEntitlement(dvaNumber, entitlementType, null);
        }

        /// <summary>
        /// Create a DVA number object
        /// </summary>
        /// <param name="dvaNumber">DVA number</param>
        /// <param name="entitlementType">Entitlement type</param>
        /// <returns>DVA identifier</returns>
        public static Identifier CreateDvaNumber(string dvaNumber, EntitlementType entitlementType)
        {
            if (dvaNumber == null)
            {
                throw new ArgumentException("'dvaNumber' cannot be null");
            }

            if (!(entitlementType == EntitlementType.RepatriationHealthOrangeBenefits ||
                  entitlementType == EntitlementType.RepatriationHealthGoldBenefits ||
                  entitlementType == EntitlementType.RepatriationHealthWhiteBenefits))
            {
                throw new ArgumentException("Entitlement type must be either: RepatriationHealthOrangeBenefits, " +
                                            "RepatriationHealthGoldBenefits or RepatriationHealthWhiteBenefits");
            }

            return
                CreateIdentifier
                (
                    "Department of Veterans' Affairs",
                    null,
                    dvaNumber,
                    "2.16.840.1.113883.3.879.270091",
                    CreateCodableText
                    (
                        entitlementType.GetAttributeValue<NameAttribute, string>(x => x.Code),
                        CodingSystem.NCTISEntitlementTypeValues,
                        entitlementType.GetAttributeValue<NameAttribute, string>(x => x.Name),
                        null,
                        null
                    )
                );
        }

        /// <summary>
        /// Creates an identifier
        /// </summary>
        /// <param name="identifierType">An Identifier Type</param>
        /// <param name="value">A Quantity</param>
        /// <returns>An Identifier Object</returns>
        public static Identifier CreateHealthIdentifier(HealthIdentifierType identifierType, String value)
        {
            return new Identifier
            {
                AssigningAuthorityName = identifierType.GetAttributeValue<NameAttribute, String>(x => x.Code),
                AssigningGeographicArea = identifierType.GetAttributeValue<NameAttribute, String>(x => x.Name),
                Root = identifierType.GetAttributeValue<NameAttribute, String>(x => x.Extension) + value
            };
        }


        /// <summary>
        /// Creates an identifier
        /// </summary>
        /// <param name="nullFlavour">A nullFlavour</param>
        /// <returns>An Identifier Object</returns>
        public static Identifier CreateHealthIdentifier(NullFlavour nullFlavour)
        {
            return new Identifier
            {
                NullFlavour = nullFlavour
            };
        }

        /// <summary>
        /// Creates an identifier
        /// </summary>
        /// <param name="assigningAuthorityName">Assigning Authority Name</param>
        /// <param name="assigningGeographicArea">Assigning Geographic Area</param>
        /// <param name="extension">extension</param>
        /// <param name="root">root</param>
        /// <param name="code">A Code</param>
        /// <returns>An Identifier Object</returns>
        public static Identifier CreateIdentifier(
            string assigningAuthorityName,
            HealthcareIdentifierGeographicArea? assigningGeographicArea,
            string extension,
            string root,
            ICodableText code)
        {
            Identifier identifier = null;

            identifier = new Identifier
            {
                AssigningAuthorityName = assigningAuthorityName,
                AssigningGeographicArea = assigningGeographicArea.HasValue
                    ? assigningGeographicArea.GetAttributeValue<NameAttribute, String>(x => x.Name)
                    : null,
                Extension = extension,
                Root = root,
                Code = code
            };

            return identifier;
        }

        /// <summary>
        /// Allow the creation of a Close The Gap Benefit Number
        /// 
        /// The value SHALL conform to the following format statement: 
        /// The format is "CTGnnX": - The first three characters are "CTG" 
        /// The fourth and fifth characters (nn) are a numeric value representing the incremental number of a CTG script written by a prescriber in a day (01 to 99). 
        /// The sixth character (X) is a validation character based on the PBS code validation. 
        /// PBS Code Validation: 
        /// 1. Link in a series the sequence number (nn) with the date of prescribing, in the format "ddmmyy" to create an 8 digit number "nnddmmyy". 
        /// 2. Divide the number by 19 and obtain the remainder. 
        /// 3. Determine the check digit using the table below, based on the remainder.
        /// 
        /// </summary>
        /// <param name="incrementalNumber">The fourth and fifth characters (nn) are a numeric value representing the incremental number of a CTG script written by a prescriber in a day (01 to 99</param>
        /// <param name="dateOfPrescribing">The the sequence number (nn) with the date of prescribing, in the format "ddmmyy" to create an 8 digit number "nnddmmyy"</param>
        /// <returns>Close The Gap Benefit Number</returns>
        public static string CreateCloseTheGapBenefitNumber(int incrementalNumber, ISO8601DateTime dateOfPrescribing)
        {
            string numberString = incrementalNumber.ToString(CultureInfo.InvariantCulture);

            if (incrementalNumber < 10) numberString = "0" + numberString;

            if (incrementalNumber > 99 || incrementalNumber < 1)
            {
                throw new ArgumentException("IncrementalNumber can not be greater than 99 or less then 0");
            }

            string sequenceNumber = numberString + dateOfPrescribing.DateTime.ToString("ddMMyy");

            var remainder = Convert.ToInt32(sequenceNumber) % 19;

            var checkCharacter = String.Empty;
            switch (remainder.ToString(CultureInfo.InvariantCulture))
            {
                case "0":
                    checkCharacter = "B";
                    break;
                case "1":
                    checkCharacter = "C";
                    break;
                case "2":
                    checkCharacter = "D";
                    break;
                case "3":
                    checkCharacter = "E";
                    break;
                case "4":
                    checkCharacter = "F";
                    break;
                case "5":
                    checkCharacter = "G";
                    break;
                case "6":
                    checkCharacter = "H";
                    break;
                case "7":
                    checkCharacter = "J";
                    break;
                case "8":
                    checkCharacter = "K";
                    break;
                case "9":
                    checkCharacter = "L";
                    break;
                case "10":
                    checkCharacter = "M";
                    break;
                case "11":
                    checkCharacter = "N";
                    break;
                case "12":
                    checkCharacter = "P";
                    break;
                case "13":
                    checkCharacter = "Q";
                    break;
                case "14":
                    checkCharacter = "R";
                    break;
                case "15":
                    checkCharacter = "T";
                    break;
                case "16":
                    checkCharacter = "W";
                    break;
                case "17":
                    checkCharacter = "X";
                    break;
                case "18":
                    checkCharacter = "Y";
                    break;
            }

            // Note: Determine the check digit using the table below, based on the remainder.
            // This table does not exists in the current ATS document 
            // Until this table is release the first reminder digit will be used in this instance.

            return String.Format("CTG{0}{1}", numberString, checkCharacter);
        }

        /// <summary>
        /// Creates an Employee Number Identifier
        /// </summary>
        /// <param name="organisationName">Organisation Name</param>
        /// <param name="extension">extension</param>
        /// <param name="hpio">hpio</param>
        /// <returns>An Identifier Object</returns>
        public static Identifier CreateEmployeeNumberIdentifier(
            string organisationName,
            string extension,
            string hpio)
        {
            Identifier identifier = null;

            identifier = new Identifier
            {
                AssigningAuthorityName = organisationName,
                Extension = extension,
                Root = EXTERNAL_HEALTH_IDENTIFIER_QUALIFIER + hpio,
                Code = CreateCodableText("EI", CodingSystem.HL7IdentifierType, null, null, null)
            };

            return identifier;
        }

        /// <summary>
        /// Creates an identifier
        /// </summary>
        /// <param name="extension">extension</param>
        /// <param name="root">root</param>
        /// <returns>An Identifier Object</returns>
        public static Identifier CreateIdentifier(string root, string extension)
        {
            Identifier identifier = null;

            identifier = new Identifier
            {
                Extension = extension,
                Root = root,
            };

            return identifier;
        }

        /// <summary>
        /// Creates an identifier
        /// </summary>
        /// <param name="root">root</param>
        /// <returns>An Identifier Object</returns>
        public static Identifier CreateIdentifier(string root)
        {
            Identifier identifier = null;

            identifier = new Identifier
            {
                Root = root,
            };

            return identifier;
        }

        /// <summary>
        /// Creates an instance identifier  
        /// </summary>
        /// <param name="root">A UUID or OID</param>
        /// <param name="extension">Unique identifier within the scope of the identifier root</param>
        /// <returns></returns>
        public static InstanceIdentifier CreateInstanceIdentifier(
            string root,
            string extension)
        {
            return new InstanceIdentifier()
            {
                Root = root,
                Extension = extension
            };
        }

        /// <summary>
        /// Creates a person constrained down to an IAuthor
        /// </summary>
        /// <returns>(IAuthor) Person</returns>
        public static IAuthor CreateParticipantForAuthor()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an organisation constrained down to an IOrganisation
        /// </summary>
        /// <returns>(IOrganisationDischargeSummary) Organisation</returns>
        public static IOrganisation CreateOrganisation()
        {
            return new Organisation();
        }

        /// <summary>
        /// Creates an employment organisation.
        /// </summary>
        /// <returns>Employment organisation.</returns>
        public static IEmploymentOrganisation CreateEmploymentOrganisation()
        {
            return new EmploymentOrganisation();
        }

        /// <summary>
        /// Creates an Organisation constrained down to an IOrganisationName
        /// </summary>
        /// <returns>(IOrganisationName) Organisation</returns>
        public static IOrganisationName CreateOrganisationName()
        {
            return new Organisation();
        }

        /// <summary>
        /// Creates a Encapsulated Data Item
        /// </summary>
        /// <returns>An EncapsulatedData</returns>
        public static EncapsulatedData CreateEncapsulatedData()
        {
            return new EncapsulatedData();
        }

        /// <summary>
        /// Creates a ExternalData Attachment
        /// </summary>
        /// <returns>An ExternalData</returns>
        public static ExternalData CreateExternalData()
        {
            return new ExternalData();
        }

        /// <summary>
        /// Creates a ExternalData Attachment
        /// </summary>
        /// <returns>An ExternalData</returns>
        public static ExternalData CreateExternalData(MediaType mediaType, string path, string caption)
        {
            return new ExternalData
            {
                ExternalDataMediaType = mediaType,
                Path = path,
                Caption = caption
            };
        }

        /// <summary>
        /// Creates a ExternalLink Attachment
        /// </summary>
        /// <returns>An ExternalLink</returns>
        public static ExternalLink CreateExternalLink()
        {
            return new ExternalLink();
        }

        /// <summary>
        /// Creates a quantity
        /// </summary>
        /// <returns>A Quantity Object</returns>
        public static Quantity CreateQuantity()
        {
            return new Quantity();
        }

        /// <summary>
        /// Creates a quantity
        /// </summary>
        /// <returns>A Quantity Object</returns>
        public static Quantity CreateQuantity(string value, string unitCode, string unitDisplayName = null)
        {
            return new Quantity
            {
                Units = unitCode,
                UnitDisplayName = unitDisplayName,
                Value = value
            };
        }

        /// <summary>
        /// Creates a ratio.
        /// </summary>
        /// <param name="numerVal"></param>
        /// <param name="numerUnits"></param>
        /// <param name="denomVal"></param>
        /// <param name="denomUnits"></param>
        /// <returns></returns>
        public static Ratio CreateRatio(string numerVal, string numerUnits, string denomVal, string denomUnits)
        {
            return new Ratio
            {
                Numerator = new Quantity
                {
                    Units = numerUnits,
                    Value = numerVal
                },
                Denominator = new Quantity
                {
                    Units = denomUnits,
                    Value = denomVal
                }
            };
        }

        /// <summary>
        /// Creates a quantity
        /// </summary>
        /// <returns>A Quantity Object</returns>
        public static Quantity CreateQuantity(string value, UnitOfMeasure units)
        {
            return new Quantity
            {
                Units = units.GetAttributeValue<NameAttribute, String>(x => x.Code),
                Value = value
            };
        }

        /// <summary>
        /// Creates a FrequencyQuantity
        /// </summary>
        /// <returns>FrequencyQuantity</returns>
        public static FrequencyQuantity CreateFrequencyQuantity()
        {
            return new FrequencyQuantity();
        }

        /// <summary>
        /// Creates a quantity range
        /// </summary>
        /// <returns>QuantityRange</returns>
        public static QuantityRange CreateQuantityRange()
        {
            return new QuantityRange();
        }

        /// <summary>
        /// Creates a quantity range
        /// </summary>
        /// <param name="high">The upper bound</param>
        /// <param name="low">The lower bound</param>
        /// <param name="units">The unit code</param>
        /// <param name="unitDisplayName">The unit display name (optional, falls back to units)</param>
        /// <param name="inclusive">Whether the bounds are included (default true)</param>
        /// <returns>QuantityRange</returns>
        public static QuantityRange CreateQuantityRange(Double? high, Double? low, string units, string unitDisplayName = null, bool inclusive = true)
        {
            return new QuantityRange
            {
                High = high,
                Low = low,
                Inclusive = inclusive,
                Units = units,
                UnitDisplayName = unitDisplayName
            };
        }

        /// <summary>
        /// Creates an electronic communication detail
        /// </summary>
        /// <param name="address">Address, E.g.. Phone number, Email address etc</param>
        /// <param name="medium">Medium, E.g. Telephone</param>
        /// <param name="usage">Usage, E.g. Business</param>
        /// <returns>ElectronicCommunicationDetail</returns>
        public static ElectronicCommunicationDetail CreateElectronicCommunicationDetail(string address,
            ElectronicCommunicationMedium? medium, ElectronicCommunicationUsage? usage)
        {
            var electronicCommunicationDetail = new ElectronicCommunicationDetail
            {
                Address = address
            };

            if (medium.HasValue)
            {
                electronicCommunicationDetail.Medium = medium.Value;
            }

            if (usage.HasValue)
            {
                electronicCommunicationDetail.Usage = new List<ElectronicCommunicationUsage> {usage.Value};
            }

            return electronicCommunicationDetail;
        }

        /// <summary>
        /// Creates an electronic communication detail
        /// </summary>
        /// <param name="address">Address, E.g.. Phone number, Email address etc</param>
        /// <param name="medium">Medium, E.g. Telephone</param>
        /// <param name="usage">Usage, E.g. Business</param>
        /// <returns>ElectronicCommunicationDetail</returns>
        public static ElectronicCommunicationDetail CreateElectronicCommunicationDetail(string address,
            ElectronicCommunicationMedium? medium, List<ElectronicCommunicationUsage> usage)
        {
            var electronicCommunicationDetail = new ElectronicCommunicationDetail
            {
                Address = address
            };

            if (medium.HasValue)
            {
                electronicCommunicationDetail.Medium = medium.Value;
            }

            if (usage != null)
            {
                electronicCommunicationDetail.Usage = usage;
            }

            return electronicCommunicationDetail;
        }

        /// <summary>
        /// Create a valid Guid
        /// </summary>
        /// <returns></returns>
        public static string CreateGuid()
        {
            return
            (
                Guid.NewGuid().ToString()
            );
        }

        /// <summary>
        /// Creates an interval with a start and end date/time.
        /// </summary>
        /// <param name="start">Start date/time.</param>
        /// <param name="end">End date/time.</param>
        /// <returns>Interval.</returns>
        public static CdaInterval CreateInterval(ISO8601DateTime start, ISO8601DateTime end)
        {
            return CdaInterval.CreateLowHigh(start, end);
        }

        /// <summary>
        /// Creates an interval from a width.
        /// </summary>
        /// <param name="widthValue">Interval width.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>Interval.</returns>
        public static CdaInterval CreateInterval(string widthValue, TimeUnitOfMeasure unit)
        {
            return CdaInterval.CreateWidth(widthValue, unit);
        }

        /// <summary>
        /// Creates an interval for a high value.
        /// </summary>
        /// <param name="high">Interval high.</param>
        /// <returns>Interval.</returns>
        public static CdaInterval CreateIntervalHigh(ISO8601DateTime high)
        {
            return CdaInterval.CreateHigh(high);
        }

        /// <summary>
        /// Creates an interval for a high value.
        /// </summary>
        /// <param name="low">Interval low.</param>
        /// <returns>Interval.</returns>
        public static CdaInterval CreateIntervalLow(ISO8601DateTime low)
        {
            return CdaInterval.CreateLow(low);
        }

        /// <summary>
        /// Creates a CreateStructuredBodyFile (External Data)
        /// </summary>
        /// <returns>ExternalData</returns>
        public static ExternalData CreateStructuredBodyFile()
        {
            return new ExternalData();
        }

        /// <summary>
        /// Creates a NarrativeOnlyDocument  
        /// </summary>
        /// <returns>NarrativeOnlyDocument</returns>
        public static NarrativeOnlyDocument CreateNarrativeOnlyDocument()
        {
            return new NarrativeOnlyDocument();
        }

        /// <summary>
        /// Creates and Hydrates a PhysicalDetails object
        /// </summary>
        /// <returns>An Empty PhysicalDetails object</returns>
        public static PhysicalDetails CreatePhysicalDetails()
        {
            return new PhysicalDetails();
        }

        /// <summary>
        /// Creates an Imaging Examination Request
        /// </summary>
        /// <returns>(IImagingExaminationRequest) Request</returns>
        public static IImagingExaminationRequest CreateImagingExaminationRequest()
        {
            return new Request();
        }

        /// <summary>
        /// Creates a pathology test result
        /// </summary>
        /// <returns>PathologyTestResult</returns>
        public static PathologyTestResult CreatePathologyTestResult()
        {
            return new PathologyTestResult();
        }

        /// <summary>
        /// Creates a specimen detail
        /// </summary>
        /// <returns>SpecimenDetail</returns>
        public static SpecimenDetail CreateSpecimenDetail()
        {
            return new SpecimenDetail();
        }

        /// <summary>
        /// Creates an anatomical site
        /// </summary>
        /// <returns>AnatomicalSite</returns>
        public static AnatomicalSite CreateAnatomicalSite()
        {
            return new AnatomicalSite();
        }

        /// <summary>
        /// Creates a Link Object
        /// </summary>
        /// <returns>Link</returns>
        public static Link CreateLink()
        {
            return new Link();
        }

        /// <summary>
        /// Creates and Hydrates a PhysicalDetails object
        /// </summary>
        /// <returns>A Hydrated PhysicalDetails object</returns>
        public static PhysicalDetails CreatePhysicalDetails(string value, string units, ExternalData image = null)
        {
            PhysicalDetails physicalDetails = null;

            if (!value.IsNullOrEmptyWhitespace() || !units.IsNullOrEmptyWhitespace())
            {
                physicalDetails = CreatePhysicalDetails();

                physicalDetails.WeightVolume = CreateQuantity();
                physicalDetails.WeightVolume.Value = value;
                physicalDetails.WeightVolume.Units = units;
            }

            if (image != null)
            {
                physicalDetails.Image = image;
            }

            return physicalDetails;
        }

        /// <summary>
        /// Creates and Hydrates a PhysicalDetails object
        /// </summary>
        /// <param name="physicalDetails">The Physical Details</param>
        /// <param name="image">The ExternalData</param>
        /// <returns>A Hydrated PhysicalDetails object</returns>
        public static PhysicalDetails CreatePhysicalDetails(PhysicalDetails physicalDetails, ExternalData image)
        {
            PhysicalDetails physicalDetail = null;

            if (physicalDetails != null)
            {
                physicalDetail = physicalDetails;
            }

            if (image != null)
            {
                physicalDetail.Image = image;
            }

            return physicalDetail;
        }

        /// <summary>
        /// Creates a OtherTestResult
        /// </summary>
        /// <returns>OtherTestResult</returns>
        public static OtherTestResult CreateOtherTestResult()
        {
            return new OtherTestResult();
        }

        /// <summary>
        /// Creates a External Concept Identifier
        /// </summary>
        /// <returns>Identifier</returns>
        public static Identifier CreateExternalConceptIdentifier(ExternalConcepts externalConcepts, string extention)
        {
            return CreateIdentifier(
                CodingSystem.AustralianPBSManufacturerCode.GetAttributeValue<NameAttribute, String>(x => x.Name),
                null,
                extention,
                String.Format("{0}", externalConcepts.GetAttributeValue<NameAttribute, String>(x => x.Code)),
                null
            );
        }

        #region Authority to Post (DOCUMENT USE AUTHORISATION) - Create Participation and Participant functions

        /// <summary>
        /// Creates an Participation Authoriser
        /// </summary>
        /// <returns>IParticipationAuthoriser</returns>
        public static IParticipationAuthoriser CreateAuthoriser()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Participant For a Reporting Radiologist
        /// </summary>
        /// <returns>IAuthoriser</returns>
        public static IReportingRadiologist CreateParticipantForReportingRadiologist()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an Participant For Requester
        /// </summary>
        /// <returns>IAuthoriser</returns>
        public static IRequester CreateParticipantForRequester()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an Participant For Authoriser
        /// </summary>
        /// <returns>IAuthoriser</returns>
        public static IAuthoriser CreateParticipantForAuthoriser()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an Participation Authorisee
        /// </summary>
        /// <returns>IParticipationAuthorisee</returns>
        public static IParticipationAuthorisee CreateAuthorisee()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Participation Reporting Radiologist
        /// </summary>
        /// <returns>IParticipationReportingRadiologist</returns>
        public static IParticipationReportingRadiologist CreateReportingRadiologist()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Participation Requester
        /// </summary>
        /// <returns>IParticipationRequester</returns>
        public static IParticipationRequester CreateRequester()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Participant For Authorisee
        /// </summary>
        /// <returns>IAuthorisee</returns>
        public static IAuthorisee CreateParticipantForAuthorisee()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an Repository
        /// </summary>
        /// <returns>Repository</returns>
        public static Repository CreateRepository()
        {
            return new Repository();
        }


        /// <summary>
        /// Creates an Participation Subject
        /// </summary>
        /// <returns>IParticipationSubject</returns>
        public static IParticipationSubject CreateSubject()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Participant For Subject
        /// </summary>
        /// <returns>ISubject</returns>
        public static ISubject CreateParticipantForSubject()
        {
            return new Participant();
        }

        /// <summary>
        /// Create Participant a AuthorityToPost
        /// </summary>
        /// <returns>AuthorityToPost</returns>
        public static AuthorityToPost CreateAuthorityToPost()
        {
            return new AuthorityToPost();
        }

        /// <summary>
        /// Creates a result value reference range detail
        /// </summary>
        /// <returns>ResultValueReferenceRangeDetail</returns>
        public static ResultValueReferenceRangeDetail CreateResultValueReferenceRangeDetail()
        {
            return new ResultValueReferenceRangeDetail();
        }

        /// <summary>
        /// Creates a reaction event
        /// </summary>
        /// <returns>ReactionEvent</returns>
        public static ReactionEvent CreateReactionEvent()
        {
            return new ReactionEvent();
        }

        /// <summary>
        /// Creates a reaction event
        /// </summary>
        /// <returns>ReactionEvent</returns>
        public static Reaction CreateReaction()
        {
            return new Reaction();
        }

        /// <summary>
        /// Creates a test request
        /// </summary>
        /// <returns>(ITestRequest) Request</returns>
        public static ITestRequest CreateTestRequest()
        {
            return new Request();
        }

        /// <summary>
        /// Creates an Image Details
        /// </summary>
        /// <returns>(IImageDetails) ImageDetails</returns>
        public static IImageDetails CreateImageDetails()
        {
            return new ImageDetails();
        }

        /// <summary>
        /// Creates an Image Result Group
        /// </summary>
        /// <returns>(IImagingResultGroup) ResultGroup</returns>
        public static IImagingResultGroup CreateImagingResultGroup()
        {
            return new ResultGroup();
        }


        /// <summary>
        /// Creates an Medications object, constrained down to an IMedicationsSML
        /// </summary>
        /// <returns>(For SML) Medications </returns>
        public static IMedicationsSML CreateMedicationsSml()
        {
            return new MedicationsSML();
        }

        /// <summary>
        /// Creates an adverseSubstance reactions object, constrained down to an IAdverseSubstanceReactions
        /// </summary>
        /// <returns>(IAdverseSubstanceReactions) AdverseSubstanceReactions </returns>
        public static IAdverseReactions CreateAdverseSubstanceReactions()
        {
            return new AdverseReactions();
        }

        /// <summary>
        /// Creates a diagnosticInvestigation
        /// </summary>
        /// <returns>DiagnosticInvestigations</returns>
        public static IDiagnosticInvestigations CreateDiagnosticInvestigations()
        {
            return new DiagnosticInvestigations();
        }

        /// <summary>
        /// Creates a test result group
        /// </summary>
        /// <returns>(ITestResultGroup) ResultGroup</returns>
        public static ITestResultGroup CreateTestResultGroup()
        {
            return new ResultGroup();
        }

        /// <summary>
        /// Creates an imaging result
        /// </summary>
        /// <returns>(IImagingResult) Result</returns>
        public static IImagingResult CreateImagingResult()
        {
            return new Result();
        }

        /// <summary>
        /// Creates an anatomical location
        /// </summary>
        /// <returns>AnatomicalLocation</returns>
        public static SpecificLocation CreateAnatomicalLocation()
        {
            return new SpecificLocation();
        }

        /// <summary>
        /// Creates an anatomical location
        /// </summary>
        /// <returns>AnatomicalLocation</returns>
        public static SpecificLocation CreateSpecificLocation()
        {
            return new SpecificLocation();
        }

        /// <summary>
        /// Creates an anatomical location
        /// </summary>
        /// <param name="nameOfLocation">A ICodableText for nameOfLocation</param>
        /// <param name="side">A ICodableText for side</param>
        /// <returns>AnatomicalLocation</returns>
        public static SpecificLocation CreateAnatomicalLocation(ICodableText nameOfLocation, ICodableText side)
        {
            return new SpecificLocation
            {
                NameOfLocation = nameOfLocation,
                Side = side
            };
        }

        /// <summary>
        /// Creates a HealthEventIdentification
        /// </summary>
        /// <returns>A HealthEventIdentification Object</returns>
        public static HealthEventIdentification CreateHealthEventIdentification()
        {
            return new HealthEventIdentification();
        }

        /// <summary>
        /// Creates a result value
        /// </summary>
        /// <returns>A ResultValue Object</returns>
        public static ResultValue CreateResultValue()
        {
            return new ResultValue();
        }

        /// <summary>
        /// Creates a test result
        /// </summary>
        /// <returns>(ITestResult) Result</returns>
        public static ITestResult CreateTestResult()
        {
            return new Result();
        }

        #endregion

        /// <summary>
        /// Creates a ParentDocument  
        /// </summary>
        /// <returns>ParentDocument</returns>
        public static ParentDocument CreateParentDocument()
        {
            return new ParentDocument();
        }

        /// <summary>
        /// Creates a Medicare View Exclusion Statement
        /// </summary>
        /// <returns>(IMedicareOverviewContext) Context</returns>
        public static ExclusionStatement CreateExclusionStatement()
        {
            return new ExclusionStatement();
        }

        /// <summary>
        /// Creates a Medicare View Exclusion Statement
        /// </summary>
        /// <returns>(IMedicareOverviewContext) Context</returns>
        public static ExclusionStatement CreateExclusionStatement(string sectionTitle, string generalStatement)
        {
            return CreateExclusionStatement(sectionTitle, generalStatement, null);
        }

        /// <summary>
        /// Creates a Medicare View Exclusion Statement
        /// </summary>
        /// <returns>(IMedicareOverviewContext) Context</returns>
        public static ExclusionStatement CreateExclusionStatement(string sectionTitle, string generalStatement,
            StrucDocText customNarrative)
        {
            return new ExclusionStatement
            {
                CustomNarrative = customNarrative,
                GeneralStatement = generalStatement,
                SectionTitle = sectionTitle
            };
        }

        /// <summary>
        /// Creates a PBSExtemporaneousIngredient
        /// </summary>
        /// <returns>PBSExtemporaneousIngredient</returns>
        public static PBSExtemporaneousIngredient CreatePBSExtemporaneousIngredient(ICodableText ingredientName,
            Quantity ingredientQuantity)
        {
            return new PBSExtemporaneousIngredient
            {
                IngredientName = ingredientName,
                IngredientQuantity = ingredientQuantity
            };
        }

        /// <summary>
        /// Creates am Observation
        /// </summary>
        /// <returns>(IObservationWeightHeight) Observation</returns>
        public static IObservationWeightHeight CreateObservation()
        {
            return new Observation();
        }

        /// <summary>
        /// Creates a body weight
        /// </summary>
        /// <returns>BodyWeight</returns>
        public static BodyWeight CreateBodyWeight()
        {
            return new BodyWeight();
        }

        /// <summary>
        /// Creates a body height
        /// </summary>
        /// <returns>BodyHeight</returns>
        public static BodyHeight CreateBodyHeight()
        {
            return new BodyHeight();
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateQuantityToDispense()
        {
            return new QuantityUnit();
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateQuantityToDispense(Quantity dose, string quantityDescription)
        {
            return CreateQuantityUnit(dose, quantityDescription);
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateQuantityToDispense(string value, ICodableText doseUnit,
            string quantityDescription)
        {
            return CreateQuantityUnit(value, doseUnit, quantityDescription);
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateStructuredDose()
        {
            return new QuantityUnit();
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateStructuredDose(Quantity dose, string quantityDescription)
        {
            return CreateQuantityUnit(dose, quantityDescription);
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateQuantityUnit()
        {
            return new QuantityUnit();
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateQuantityUnit(Quantity dose, string quantityDescription)
        {
            return new QuantityUnit
            {
                Quantity = dose,
                QuantityDescription = quantityDescription
            };
        }

        /// <summary>
        /// Creates a QuantityUnit
        /// </summary>
        /// <returns>QuantityUnit</returns>
        public static QuantityUnit CreateQuantityUnit(string value, ICodableText doseUnit, string quantityDescription)
        {
            return new QuantityUnit
            {
                Quantity = !value.IsNullOrEmptyWhitespace() ? new Quantity {Value = value} : null,
                Unit = doseUnit,
                QuantityDescription = quantityDescription
            };
        }

        /// <summary>
        /// Creates a Timing object
        /// </summary>
        /// <returns>Timing</returns>
        public static Timing CreateTiming()
        {
            return new Timing();
        }

        /// <summary>
        /// Creates a Related Image
        /// </summary>
        /// <returns>RelatedImage</returns>
        public static RelatedImage CreateRelatedImage()
        {
            return new RelatedImage();
        }

        /// <summary>
        /// Creates a Related Image
        /// </summary>
        /// <returns>RelatedImage</returns>
        public static RelatedImage CreateRelatedImage(string imageLocation)
        {
            return new RelatedImage
            {
                ImageUrl = imageLocation
            };
        }

        /// <summary>
        /// Creates a Related Image
        /// </summary>
        /// <returns>RelatedImage</returns>
        public static RelatedImage CreateRelatedImage(string imageLocation, MediaType mediaType)
        {
            return new RelatedImage
            {
                ImageUrl = imageLocation,
                MediaType = mediaType
            };
        }

        /// <summary>
        /// Create a Document Provenance  
        /// </summary>
        /// <returns>DocumentProvenance</returns>
        public static DocumentDetails CreateDocumentDetails()
        {
            return new DocumentDetails();
        }

        /// <summary>
        /// Creates an ElectronicCommunicationDetail Object
        /// </summary>
        /// <returns>An ElectronicCommunicationDetail Object</returns>
        public static ParticipationPeriod CreateParticipationPeriod(CdaInterval interval)
        {
            return new ParticipationPeriod
            {
                Interval = interval
            };
        }

        /// <summary>
        /// Creates an ElectronicCommunicationDetail Object
        /// </summary>
        /// <returns>An ElectronicCommunicationDetail Object</returns>
        public static ParticipationPeriod CreateParticipationPeriod(ISO8601DateTime value)
        {
            return new ParticipationPeriod
            {
                Value = value
            };
        }

        #region Codable Text - Overloads

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCode(string code)
        {
            return CreateCodableText(code, null, null, null, null, null, null);
        }

        /// <summary>
        /// Creates a codable text nullable object
        /// </summary>
        /// <param name="nullFlavor">The nullFlavor</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(NullFlavour nullFlavor)
        {
            var codableText = new CodableText
            {
                NullFlavour = nullFlavor
            };
            return codableText;
        }

        /// <summary>
        /// Creates a codable text with nullable object, Original Text and CodingSystem
        /// </summary>
        /// <param name="nullFlavor">The nullFlavor</param>
        /// <param name="orginalText">The orginalText</param>
        /// <param name="codingSystemCode">The codingSystemCode</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(NullFlavour nullFlavor, CodingSystem codingSystemCode,
            string orginalText)
        {
            var codableText = new CodableText
            {
                NullFlavour = nullFlavor,
                OriginalText = orginalText,
                CodeSystemCode = codingSystemCode.GetAttributeValue<NameAttribute, string>(a => a.Code)
            };
            return codableText;
        }

        /// <summary>
        /// Creates a codable text with nullable object, Original Text and CodingSystem
        /// </summary>
        /// <param name="nullFlavor">The nullFlavor</param>
        /// <param name="orginalText">The orginalText</param>
        /// <param name="codingSystemCode">The codingSystemCode</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(NullFlavour nullFlavor, string codingSystemCode,
            string orginalText)
        {
            var codableText = new CodableText
            {
                NullFlavour = nullFlavor,
                OriginalText = orginalText,
                CodeSystemCode = codingSystemCode
            };
            return codableText;
        }

        /// <summary>
        /// Creates a empty codableText object
        /// </summary>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText()
        {
            return new CodableText();
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(string originalText)
        {
            return CreateCodableText(null, null, null, originalText, null, null);
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(string code, CodingSystem? codeSystem, string displayName)
        {
            return CreateCodableText(code, codeSystem,
                codeSystem.HasValue ? codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version) : null,
                displayName, null, null, null);
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">The originalText</param>
        /// <param name="qualifierCodes">A list of qualifier codes</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(string code, CodingSystem? codeSystem, string displayName,
            string originalText, List<QualifierCode> qualifierCodes = null)
        {
            return CreateCodableText(code, codeSystem,
                codeSystem.HasValue ? codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version) : null,
                displayName, originalText, null, qualifierCodes);
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this coadable text</param>
        /// <param name="qualifierCodes">A list of qualifier codes</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(string code, CodingSystem? codeSystem, string displayName,
            string originalText, List<ICodableTranslation> translations, List<QualifierCode> qualifierCodes = null)
        {
            return CreateCodableText(code, codeSystem,
                codeSystem.HasValue ? codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version) : null,
                displayName, originalText, translations, qualifierCodes);
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="codeSystemVersion">The Code System version</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this coadable text</param>
        /// <param name="qualifierCodes">A list of Qualifier Codes</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(string code, CodingSystem? codeSystem, string codeSystemVersion,
            string displayName, string originalText, List<ICodableTranslation> translations,
            List<QualifierCode> qualifierCodes = null)
        {
            var codableText = new CodableText();

            if (codeSystem.HasValue)
            {
                codableText.DisplayName = displayName;
                codableText.Code = code;
                codableText.CodeSystemCode = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Code);
                codableText.CodeSystemName = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Name);
                codableText.CodeSystemVersion = codeSystemVersion;
            }

            if (!originalText.IsNullOrEmptyWhitespace())
                codableText.OriginalText = originalText;

            if (translations != null && translations.Any())
                codableText.Translations = translations;

            codableText.QualifierCodes = qualifierCodes;

            return codableText;
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystemCode">The Code for the CodableText</param>
        /// <param name="codeSystemName">The CodeSystemName for the CodableText</param>
        /// <param name="codeSystemVersion">The CodeSystemVersion \ for the CodableText</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this coadable text</param>
        /// <param name="qualifierCodes">A list of qualifier codes</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateCodableText(string code, string codeSystemCode, string codeSystemName,
            string codeSystemVersion, string displayName, string originalText, List<ICodableTranslation> translations,
            List<QualifierCode> qualifierCodes = null)
        {
            var codableText = new CodableText();

            codableText.DisplayName = displayName;
            codableText.Code = code;
            codableText.CodeSystemCode = codeSystemCode;
            codableText.CodeSystemName = codeSystemName;
            codableText.CodeSystemVersion = codeSystemVersion;

            if (!originalText.IsNullOrEmptyWhitespace())
                codableText.OriginalText = originalText;

            if (translations != null && translations.Any())
                codableText.Translations = translations;

            codableText.QualifierCodes = qualifierCodes;

            return codableText;
        }

        /// <summary>
        /// Create a Codable Text form enum
        /// </summary>
        /// <typeparam name="T">An Enum</typeparam>
        /// <param name="defaultValue">Enum</param>
        /// <returns>A CodableText casted from an Enum</returns>
        public static ICodableText CreateCodableText<T>(T defaultValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type which can be mapped to CodingSystem");
            }

            var enumeration = defaultValue as Enum;

            return new CodableText
            {
                DisplayName = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Name),
                Code = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodeSystemCode =
                    ((CodingSystem) Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodeSystemName =
                    ((CodingSystem) Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Name),
                CodeSystemVersion =
                    ((CodingSystem) Enum.Parse(typeof(CodingSystem),
                        enumeration.GetAttributeValue<NameAttribute, string>(x => x.CodeSystem)))
                    .GetAttributeValue<NameAttribute, string>(x => x.Version),
                OriginalText = enumeration.GetAttributeValue<NameAttribute, string>(x => x.Title)
            };
        }

        /// <summary>
        /// Creates a codable text object
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <returns>CodableText</returns>
        public static ICodableTranslation CreateCodableTranslation(String code, CodingSystem? codeSystem,
            String displayName)
        {
            ICodableTranslation codableText = new CodableText();

            if (codeSystem.HasValue)
            {
                codableText.DisplayName = displayName;
                codableText.Code = code;
                codableText.CodeSystemCode = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Code);
                codableText.CodeSystemName = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Name);
                codableText.CodeSystemVersion =
                    codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version);
            }

            return codableText;
        }

        /// <summary>
        /// Creates a codableText object
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystemCode">The Code for the CodableText</param>
        /// <param name="codeSystemName">The CodeSystemName for the CodableText</param>
        /// <param name="codeSystemVersion">The CodeSystemVersion \ for the CodableText</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <returns>CodableText</returns>
        public static ICodableTranslation CreateCodableTranslation(string code, string codeSystemCode,
            string codeSystemName, string codeSystemVersion, string displayName)
        {
            ICodableTranslation codableText = new CodableText();

            codableText.DisplayName = displayName;
            codableText.Code = code;
            codableText.CodeSystemCode = codeSystemCode;
            codableText.CodeSystemName = codeSystemName;
            codableText.CodeSystemVersion = codeSystemVersion;

            return codableText;
        }

        /// <summary>
        /// Creates a Qualifier Code
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="value">The value</param>
        /// <returns>(QualifierCode) QualifierCode</returns>
        public static QualifierCode CreateQualifierCode(ICodableText name, ICodableText value)
        {
            return new QualifierCode
            {
                Value = value,
                Name = name
            };
        }

        /// <summary>
        /// Creates a Therapeutic Good ID
        /// </summary>
        /// <returns>(ICodableText) CodableText</returns>
        public static ICodableText CreateTherapeuticGoodIdentification()
        {
            return new CodableText();
        }

        /// <summary>
        /// Creates a medicine
        /// </summary>
        /// <param name="code">role medicine</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="codeSystemVersion">The current CodeSystemVersion for the AMT SnomedCode</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this medicine</param>
        /// <returns>CodableText defining a medicine</returns>
        public static ICodableText CreateMedicine(string code, CodingSystem codeSystem, string codeSystemVersion,
            string displayName, string originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code,
                codeSystem.GetAttributeValue<NameAttribute, string>(a => a.Code),
                codeSystem.GetAttributeValue<NameAttribute, string>(a => a.Name),
                codeSystemVersion,
                displayName,
                originalText,
                translations);
        }

        /// <summary>
        /// Create a CreateTherapeuticGoodIdentification CodableText
        /// </summary>
        /// <param name="code">CreateTherapeuticGoodIdentification code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CreateTherapeuticGoodIdentification </returns>
        public static ICodableText CreateTherapeuticGoodIdentification(string code, CodingSystem? codeSystem,
            string displayName, string originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a ChangesMade CodableText
        /// </summary>
        /// <param name="code">ChangesMade code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CreateChangesMade </returns>
        public static ICodableText CreateChangesMade(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a result name CodableText
        /// </summary>
        /// <param name="code">result name code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a result name</returns>
        public static ICodableText CreateResultName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates an anatomic location name
        /// </summary>
        /// <param name="code">anatomic location name code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="translations">Any translations that are associated with this anatomic location name</param>
        /// <returns>CodableText defining an anatomic location name</returns>
        public static ICodableText CreateAnatomicLocationName(String code, CodingSystem? codeSystem, String displayName,
            List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, null, translations);
        }

        /// <summary>
        /// Creates a Requested Service Description
        /// </summary>
        /// <param name="code">type of Requested Service Description</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this participation type</param>
        /// <returns>CodableText defining a Requested Service  Description</returns>
        public static ICodableText CreateRequestedServiceDescription(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a CareSetting CodableText
        /// </summary>
        /// <param name="code">Care Setting mode code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateCareSetting(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a ProblemDiagnosisDescription CodableText
        /// </summary>
        /// <param name="code">Problem Diagnosis Description code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateProblemDiagnosisDescription(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a ProblemDiagnosisType CodableText
        /// </summary>
        /// <param name="code">Problem Diagnosis Type code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param> 
        /// <returns>CodableText </returns>
        public static ICodableText CreateProblemDiagnosisType(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a Clinical Intervention Description CodableText
        /// </summary>
        /// <param name="code">Clinical Intervention Type code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateClinicalInterventionDescription(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a Alert Type CodableText
        /// </summary>
        /// <param name="code">Alert Type code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateAlertType(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create an Alert Description CodableText
        /// </summary>
        /// <param name="code">Alert Description code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateAlertDescription(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a Clinical Category CodableText
        /// </summary>
        /// <param name="code">Category code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateCategory(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a CreateTestName CodableText
        /// </summary>
        /// <param name="code">CreateTestName code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateTestName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a CreatePerViewFindings CodableText
        /// </summary>
        /// <param name="code">CreatePerViewFindings code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreatePerViewFindings(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a ViewName CodableText
        /// </summary>
        /// <param name="code">ViewName code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateViewName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a ImagingQuality CodableText
        /// </summary>
        /// <param name="code">ImagingQuality code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateImagingQuality(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a OverallFinding CodableText
        /// </summary>
        /// <param name="code">OverallFinding code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateOverallFinding(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a ItemStatus CodableText
        /// </summary>
        /// <param name="code">ItemStatus code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CreateItemStatus </returns>
        public static ICodableText CreateItemStatus(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a modality
        /// </summary>
        /// <param name="code">modality code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this modality</param>
        /// <returns>CodableText defining a modality</returns>
        public static ICodableText CreateModality(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a pathological diagnosis
        /// </summary>
        /// <param name="code">pathological diagnosis code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this pathological diagnosis</param>
        /// <returns>CodableText defining a pathological diagnosis</returns>
        public static ICodableText CreatePathologicalDiagnois(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a sampling precondition
        /// </summary>
        /// <param name="code">sampling precondition code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this sampling precondition</param>
        /// <returns>CodableText defining a sampling precondition</returns>
        public static ICodableText CreateSamplingPreconditions(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a specimen tissue type
        /// </summary>
        /// <param name="code">specimen tissue type code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this specimen tissue type</param>
        /// <returns>CodableText defining a specimen tissue type</returns>
        public static ICodableText CreateSpecimenTissueType(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates an anatomic location side
        /// </summary>
        /// <param name="code">anatomic location side code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this anatomic location side</param>
        /// <returns>CodableText defining an anatomic location side</returns>
        public static ICodableText CreateAnatomicLocationSide(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates an examination result name
        /// </summary>
        /// <param name="code">examination result name code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this examination result name</param>
        /// <returns>CodableText defining an examination result name</returns>
        public static ICodableText CreateExaminationResultName(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a test request name.
        /// </summary>
        /// <param name="code">Test request code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <returns>CodableText defining a test request name</returns>
        public static ICodableText CreateTestRequestName(String code, CodingSystem? codeSystem, String displayName)
        {
            return CreateCodableText(code, codeSystem, displayName, null, null);
        }

        /// <summary>
        /// Creates a collection procedure
        /// </summary>
        /// <param name="code">collection procedure code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this collection procedure</param>
        /// <returns>CodableText defining a Anatomical Site</returns>
        public static ICodableText CreateCollectionProcedure(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a manifestation
        /// </summary>
        /// <param name="code">role manifestation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this manifestation</param>
        /// <returns>CodableText defining a manifestation</returns>
        public static ICodableText CreateManifestation(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a result value reference range meaning
        /// </summary>
        /// <param name="code">result value reference range meaning code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this result value reference range meaning</param>
        /// <returns>CodableText defining a result value reference range meaning</returns>
        public static ICodableText CreateResultValueReferenceRangeMeaning(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a role
        /// </summary>
        /// <returns>CodableText</returns>
        public static ICodableText CreateRole()
        {
            return new CodableText();
        }

        /// <summary>
        /// Creates a role
        /// </summary>
        /// <returns>CodableText</returns>
        public static ICodableText CreateRole(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a participation Role
        /// </summary>
        /// <param name="code">role participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this participation role</param>
        /// <returns>CodableText defining a participation role</returns>
        public static ICodableText CreateParticipationRole(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role
        /// </summary>
        /// <param name="code">A code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>A codable text representing the role</returns>
        public static ICodableText CreateRole(String code, CodingSystem? codeSystem, String displayName,
            String originalText)
        {
            var codableText = new CodableText();

            if (codeSystem.HasValue)
            {
                codableText.DisplayName = displayName;
                codableText.Code = code;
                codableText.CodeSystemCode = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Code);
                codableText.CodeSystemName = codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Name);
                codableText.CodeSystemVersion =
                    codeSystem.Value.GetAttributeValue<NameAttribute, string>(a => a.Version);
            }

            if (!originalText.IsNullOrEmptyWhitespace())
                codableText.OriginalText = originalText;

            return codableText;
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role as defined by an occupation
        /// </summary>
        /// <param name="occupation">The occupation defining the role</param>
        /// <returns>A codable text representing the role</returns>
        public static ICodableText CreateRole(Occupation occupation)
        {
            // Remove when other documents are updated
            var codingSystem = CodingSystem.ANZSCO;
            if ((new StackTrace()).GetFrames().Any(t =>
                t.GetMethod().Name.Contains(CDADocumentType.EventSummary.ToString()) ||
                t.GetMethod().Name.Contains(CDADocumentType.SharedHeathSummary.ToString())))
            {
                codingSystem = CodingSystem.ANZSCORevision1;
            }

            return new CodableText
            {
                DisplayName = occupation != Occupation.Undefined
                    ? occupation.GetAttributeValue<NameAttribute, String>(x => x.Name)
                    : String.Empty,
                Code = occupation != Occupation.Undefined
                    ? occupation.GetAttributeValue<NameAttribute, String>(x => x.Code)
                    : String.Empty,
                CodeSystem = codingSystem
            };
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role as defined by an occupation
        /// </summary>
        /// <param name="occupation">The occupation defining the role</param>
        /// <param name="codingSystem">The Coding system that will be associated with the occupation</param>
        /// <returns>A codable text representing the role</returns>
        public static ICodableText CreateRole(Occupation occupation, CodingSystem codingSystem)
        {
            return new CodableText
            {
                DisplayName = occupation != Occupation.Undefined
                    ? occupation.GetAttributeValue<NameAttribute, String>(x => x.Name)
                    : String.Empty,
                Code = occupation != Occupation.Undefined
                    ? occupation.GetAttributeValue<NameAttribute, String>(x => x.Code)
                    : String.Empty,
                CodeSystem = codingSystem
            };
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role as defined by an occupation
        /// </summary>
        /// <param name="healthcareFacilityTypeCode">The occupation defining the role</param>
        /// <returns>A codable text representing the role</returns>
        public static ICodableText CreateRole(HealthcareFacilityTypeCodes healthcareFacilityTypeCode)
        {
            return new CodableText
            {
                DisplayName = healthcareFacilityTypeCode.GetAttributeValue<NameAttribute, String>(x => x.Name),
                Code = healthcareFacilityTypeCode.GetAttributeValue<NameAttribute, String>(x => x.Code),
                CodeSystem = CodingSystem.Anzsic2006
            };
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role as defined by a RoleCodeAndRoleClassCodes
        /// </summary>
        /// <param name="roleCodeAndRoleClassCodes">The occupation defining the role</param>
        /// <returns>A codable text representing the role</returns>
        public static ICodableText CreateRole(RoleCodeAndRoleClassCodes roleCodeAndRoleClassCodes)
        {
            return CreateCodableText(roleCodeAndRoleClassCodes);
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role as a nullFlavor
        /// </summary>
        /// <param name="nullFlavor"></param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateRole(NullFlavour nullFlavor)
        {
            var codableText = new CodableText
            {
                NullFlavour = nullFlavor
            };
            return codableText;
        }

        /// <summary>
        /// Creates a codableText object that contains and defines a role as a nullFlavor
        /// </summary>
        /// <param name="originalText">An OriginalText</param>
        /// <returns>CodableText</returns>
        public static ICodableText CreateRole(string originalText)
        {
            return CreateCodableText(originalText);
        }

        /// <summary>
        /// Creates a result group name
        /// </summary>
        /// <param name="code">result group name code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this result group name</param>
        /// <returns>CodableText defining a result group name</returns>
        public static ICodableText CreateResultGroupName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a result group name
        /// </summary>
        /// <param name="nullFlavor">nullFlavor</param>
        /// <returns>CodableText defining a result group name</returns>
        public static ICodableText CreateResultGroupName(NullFlavour nullFlavor)
        {
            return CreateCodableText(nullFlavor);
        }

        /// <summary>
        /// Creates a procedure name
        /// </summary>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>CodableText defining a procedure</returns>
        public static ICodableText CreateProcedureName(String originalText)
        {
            return CreateCodableText(null, null, null, originalText, null);
        }

        /// <summary>
        /// Creates an anatomic location name
        /// </summary>
        /// <param name="code">anatomic location name code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this anatomic location name</param>
        /// <returns>CodableText defining an anatomic location name</returns>
        public static ICodableText CreateAnatomicLocationName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a diagnosis
        /// </summary>
        /// <param name="code">diagnosis code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations associated with this diagnosis</param>
        /// <returns>CodableText defining a diagnosis</returns>
        public static ICodableText CreateDiagnosis(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, null);
        }

        /// <summary>
        /// Creates Reason for Encounter Description
        /// </summary>
        /// <param name="code">Reason for Encounter Description</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this result name</param>
        /// <returns>CodableText defining a Reason for Encounter Description</returns>
        public static ICodableText CreateReasonForEncounterDescription(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates Reason for Encounter Description
        /// </summary>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <returns>CodableText defining a Reason for Encounter Description</returns>
        public static ICodableText CreateReasonForEncounterDescription(String originalText)
        {
            return CreateCodableText(originalText);
        }

        /// <summary>
        /// Creates Reason for Clinical Synopsis Description
        /// </summary>
        /// <returns>CodableText defining a Reason for Encounter Description</returns>
        public static ICodableText CreateClinicalSynopsisDescription(String orginalText)
        {
            return CreateCodableText(orginalText);
        }

        /// <summary>
        /// Creates a Anatomical Side
        /// </summary>
        /// <param name="code">AnatomicalSide code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this medicine</param>
        /// <returns>CodableText defining a AnatomicalSide</returns>
        public static ICodableText CreateAnatomicalSide(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a medicine
        /// </summary>
        /// <returns>CodableText defining a medicine</returns>
        public static ICodableText CreateMedicine()
        {
            return new CodableText();
        }

        /// <summary>
        /// Creates a procedure name
        /// </summary>
        /// <param name="code">Procedure code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a procedure</returns>
        public static ICodableText CreateProcedureName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a substance or agent
        /// </summary>
        /// <param name="code">substance or agent code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this substance or agent</param>
        /// <returns>CodableText defining a substance or agent</returns>
        public static ICodableText CreateSubstanceOrAgent(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a test result name
        /// </summary>
        /// <param name="code">result name</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this result name</param>
        /// <returns>CodableText defining a result name</returns>
        public static ICodableText CreateTestResultName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a test result group name
        /// </summary>
        /// <param name="code">result name</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this result name</param>
        /// <returns>CodableText defining a result name</returns>
        public static ICodableText CreateTestResultGroupName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a medicine CodableText
        /// </summary>
        /// <param name="code">Medicine code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a medicine</returns>
        public static ICodableText CreateMedicine(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a Arranged Service Description CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateArrangedServiceDescription(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a Service Booking Status CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateServiceBookingStatus(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a NoKnownAdverseReactionTo CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateNoKnownAdverseReactionTo(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateNoKnownAllergicReactionTo CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateNoKnownAllergicReactionTo(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateReactionDescriptions CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateReactionDescriptions(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateNoKnownHypersensitivityReactionTo CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateNoKnownHypersensitivityReactionTo(String code, CodingSystem? codeSystem,
            String displayName, String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateNoKnownIntoleranceTo CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateNoKnownIntoleranceTo(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateAgentDescription CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateAgentDescription(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateAdverseReactionType CodableText
        /// </summary>
        /// <param name="code">type of participation</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateAdverseReactionType(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates a CreateAdverseReactionType NullFlavour
        /// </summary>
        /// <param name="nullFlavour">type of participation</param>
        /// <returns>CodableText defining a participation type</returns>
        public static ICodableText CreateAdverseReactionType(NullFlavour nullFlavour)
        {
            return CreateCodableText(nullFlavour);
        }

        /// <summary>
        /// Creates a separation mode CodableText.
        /// </summary>
        /// <param name="separationMode">Separation mode.</param>
        /// <returns>Separation mode ICodableText</returns>
        public static ICodableText CreateSeparationMode(ModeOfSeparation separationMode)
        {
            return CreateCodableText(
                separationMode.GetAttributeValue<NameAttribute, string>(x => x.Code),
                CodingSystem.AIHW,
                separationMode.GetAttributeValue<NameAttribute, string>(x => x.Name),
                null,
                null);
        }

        /// <summary>
        /// Create a Specialty CodableText
        /// </summary>
        /// <param name="code">Specialty mode code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this procedure</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateSpecialty(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Create a Specialty CodableText nullFlavor
        /// </summary>
        /// <param name="nullFlavor">Specialty mode nullflavor</param>
        /// <returns>CodableText </returns>
        public static ICodableText CreateSpecialty(NullFlavour nullFlavor)
        {
            return CreateCodableText(nullFlavor);
        }

        /// <summary>
        /// Creates a Image View Name
        /// </summary>
        /// <param name="code">Image View Name name code</param>
        /// <param name="codeSystem">The code system associated with the code</param>
        /// <param name="displayName">The display name associated with the code</param>
        /// <param name="originalText">Original text, usually applicable in the absence of a code and display name</param>
        /// <param name="translations">Any translations that are associated with this result group name</param>
        /// <returns>CodableText defining a result group name</returns>
        public static ICodableText CreateImageViewName(String code, CodingSystem? codeSystem, String displayName,
            String originalText, List<ICodableTranslation> translations)
        {
            return CreateCodableText(code, codeSystem, displayName, originalText, translations);
        }

        /// <summary>
        /// Creates Hl7V3ResultStatus ICoadable text
        /// </summary>
        /// <param name="resultStatus">The Hl7V3ResultStatus</param>
        /// <returns>A Hl7V3ResultStatus converted to a ICodableText</returns>
        public static ICodableText CreateResultStatus(Hl7V3ResultStatus resultStatus)
        {
            return CreateResultStatus(resultStatus, null);
        }

        /// <summary>
        /// Creates Hl7V3ResultStatus ICoadable text
        /// </summary>
        /// <param name="resultStatus">The Hl7V3ResultStatus</param>
        /// <param name="resultOriginalText">This overrides the long display name in the Narrative</param>
        /// <returns>A Hl7V3ResultStatus converted to a ICodableText</returns>
        public static ICodableText CreateResultStatus(Hl7V3ResultStatus resultStatus, string resultOriginalText)
        {
            return CreateCodableText(resultStatus.GetAttributeValue<NameAttribute, String>(x => x.Code),
                CodingSystem.HL7ResultStatus,
                resultStatus.GetAttributeValue<NameAttribute, String>(x => x.Name),
                resultOriginalText);
        }

        /// <summary>
        /// Creates A StructuredText Element
        /// </summary>
        /// <param name="text">The text for the element</param>
        /// <returns>A StructuredText Element</returns>
        public static StructuredText CreateStructuredText(string text)
        {
            return new StructuredText
            {
                Text = text
            };
        }

        /// <summary>
        /// Creates A StructuredText Element
        /// </summary>
        /// <param name="nullFlavour">The nullFlavour for the element</param>
        /// <returns>A StructuredText Element</returns>
        public static StructuredText CreateStructuredText(NullFlavour? nullFlavour)
        {
            return new StructuredText
            {
                NullFlavour = nullFlavour
            };
        }

        /// <summary>
        /// Creates a reviewed medical history
        /// </summary>
        /// <returns>(IReviewedMedicalHistory) MedicalHistory</returns>
        public static IMedicalHistory CreateMedicalHistory()
        {
            return new MedicalHistory();
        }

        /// <summary>
        /// Creates a medical history item
        /// </summary>
        /// <returns>IMedicalHistoryItem</returns>
        public static IMedicalHistoryItem CreateMedicalHistoryItem()
        {
            return new MedicalHistoryItem();
        }

        /// <summary>
        /// Creates a problem diagnosis
        /// </summary>
        /// <returns>IProblemDiagnosis</returns>
        public static IProblemDiagnosis CreateProblemDiagnosis()
        {
            return new ProblemDiagnosis();
        }

        /// <summary>
        /// Creates a procedure
        /// </summary>
        /// <returns>Procedure</returns>
        public static Procedure CreateProcedure()
        {
            return new Procedure();
        }

        /// <summary>
        /// Creates a statement
        /// </summary>
        /// <returns>Statement</returns>
        public static Statement CreateStatement()
        {
            return new Statement();
        }

        #endregion

        #region Diagnostic Investigations

        /// <summary>
        /// Creates an imaging examination result
        /// </summary>
        /// <returns>(IImagingExaminationResult) Imaging Examination Result Discharge Summary</returns>
        public static IImagingExaminationResult CreateImagingExaminationResult()
        {
            return new ImagingExaminationResult();
        }

        #endregion

        #region Time

        /// <summary>
        /// Creates am StructuredTiming
        /// </summary>
        /// <returns>StructuredTiming</returns>
        public static StructuredTiming CreateStructuredTiming()
        {
            return new StructuredTiming();
        }

        /// <summary>
        /// Creates a EventRelatedIntervalOfTime (EIVL_TS)
        /// </summary>
        /// <returns>EventRelatedIntervalOfTime</returns>
        public static EventRelatedIntervalOfTime CreateEventRelatedIntervalOfTime()
        {
            return new EventRelatedIntervalOfTime();
        }

        /// <summary>
        /// Creates a ParentheticSetExpressionOfTime (SXPR_TS)
        /// </summary>
        /// <returns>ParentheticSetExpressionOfTime</returns>
        public static ParentheticSetExpressionOfTime CreateParentheticSetExpressionOfTime()
        {
            return new ParentheticSetExpressionOfTime();
        }

        /// <summary>
        /// Creates a IPeriodicIntervalOfTime (PIVL_TS)
        /// </summary>
        /// <returns>IPeriodicIntervalOfTime</returns>
        public static PeriodicIntervalOfTime CreatePeriodicIntervalOfTime()
        {
            return new PeriodicIntervalOfTime();
        }

        /// <summary>
        /// Creates a SetComponentTS (SXCM_TS)
        /// </summary>
        /// <returns>SetComponentTS</returns>
        public static SetComponentTS CreateSetComponentTS()
        {
            return new SetComponentTS();
        }

        /// <summary>
        /// Creates a Frequency
        /// </summary>
        /// <returns>Frequency</returns>
        public static Frequency CreateFrequency()
        {
            return new Frequency();
        }

        /// <summary>
        /// Creates an CdaInterval with low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateLow(ISO8601DateTime low)
        {
            return CdaInterval.CreateLow(low);
        }

        /// <summary>
        /// Creates an CdaInterval with low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateLow(ISO8601DateTime low, int? value, OperationTypes? operatorType,
            NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateLow(low, value, operatorType, nullFlavor);
        }

        /// <summary>
        /// Creates an CdaInterval with a width.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateWidth(string width, TimeUnitOfMeasure unit)
        {
            return CdaInterval.CreateWidth(width, unit);
        }

        /// <summary>
        /// Creates an CdaInterval with a width.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="value">Quantity.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param> 
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateWidth(string width, TimeUnitOfMeasure unit, int? value,
            OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateWidth(width, unit, value, operatorType, nullFlavor);
        }

        /// <summary>
        /// Creates an CdaInterval with high.
        /// </summary>
        /// <param name="high">High.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateHigh(ISO8601DateTime high)
        {
            return CdaInterval.CreateHigh(high);
        }

        /// <summary>
        /// Creates an CdaInterval with high.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <param name="high">High.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateHigh(ISO8601DateTime high, int? value, OperationTypes? operatorType,
            NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateHigh(high, value, operatorType, nullFlavor);
        }

        /// <summary>
        /// Creates an CdaInterval with center.
        /// </summary>
        /// <param name="center">Center.</param>
        /// <returns>Interval</returns>
        public static CdaInterval CreateCenter(ISO8601DateTime center)
        {
            return CdaInterval.CreateCenter(center);
        }

        /// <summary>
        /// Creates an CdaInterval with center.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <param name="center">Center.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateCenter(ISO8601DateTime center, int? value, OperationTypes? operatorType,
            NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateCenter(center, value, operatorType, nullFlavor);
        }

        /// <summary>
        /// Creates an CdaInterval with high and low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="high">High.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateLowHigh(ISO8601DateTime low, ISO8601DateTime high)
        {
            return CdaInterval.CreateLowHigh(low, high);
        }


        /// <summary>
        /// Creates an CdaInterval with high and low.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="high">High.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateLowHigh(ISO8601DateTime low, ISO8601DateTime high, int? value,
            OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateLowHigh(low, high, value, operatorType, nullFlavor);
        }

        /// <summary>
        /// Creates an CdaInterval with low and width.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="value">Quantity.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateLowWidth(ISO8601DateTime low, string value, TimeUnitOfMeasure unit)
        {
            return CdaInterval.CreateLowWidth(low, value, unit);
        }

        /// <summary>
        /// Creates an CdaInterval with low and width.
        /// </summary>
        /// <param name="low">Low.</param>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateLowWidth(ISO8601DateTime low, string width, TimeUnitOfMeasure unit, int? value,
            OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateLowWidth(low, width, unit, value, operatorType, nullFlavor);
        }

        /// <summary>
        /// Creates an CdaInterval with high and width.
        /// </summary>
        /// <param name="high">High.</param>
        /// <param name="width">width.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateHighWidth(ISO8601DateTime high, string width, TimeUnitOfMeasure unit)
        {
            return CdaInterval.CreateHighWidth(high, width, unit);
        }

        /// <summary>
        /// Creates an CdaInterval with high and width.
        /// </summary>
        /// <param name="high">High.</param>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateHighWidth(ISO8601DateTime high, string width, TimeUnitOfMeasure unit,
            int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateHighWidth(high, width, unit, value, operatorType, nullFlavor);
        }


        /// <summary>
        /// Creates an CdaInterval with center and width.
        /// </summary>
        /// <param name="center">Center.</param>
        /// <param name="value">Quantity.</param>
        /// <param name="unit">Unit.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateCenterWidth(ISO8601DateTime center, string value, TimeUnitOfMeasure unit)
        {
            return CdaInterval.CreateCenterWidth(center, value, unit);
        }

        /// <summary>
        /// Creates an CdaInterval with center and width.
        /// </summary>
        /// <param name="center">Center.</param>
        /// <param name="width">Width.</param>
        /// <param name="unit">Unit.</param>
        /// <param name="value">value.</param>
        /// <param name="operatorType">operatorType.</param>
        /// <param name="nullFlavor">nullFlavor.</param>
        /// <returns>CdaInterval</returns>
        public static CdaInterval CreateCenterWidth(ISO8601DateTime center, string width, TimeUnitOfMeasure unit,
            int? value, OperationTypes? operatorType, NullFlavor? nullFlavor)
        {
            return CdaInterval.CreateCenterWidth(center, width, unit, value, operatorType, nullFlavor);
        }

        #endregion


        /// <summary>
        /// Creates a reviewed medication
        /// </summary>
        /// <returns>(IReviewedMedication) Medication</returns>
        public static IMedication CreateMedication()
        {
            return new Medication();
        }

        /// <summary>
        /// Creates a reviewed adverse substance reactions
        /// </summary>
        /// <returns>(IAdverseReactions) AdverseSubstanceReactions</returns>
        public static IAdverseReactions CreateAdverseReactions()
        {
            return new AdverseReactions();
        }

        #region Utilities

        /// <summary>
        /// Creates a OID
        /// </summary>
        /// <returns>Creates an OID</returns>
        public static string CreateOid()
        {
            return OIDHelper.UuidToOid(CreateGuid());
        }

        /// <summary>
        /// Convert a file path to a ByteArray
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string filePath)
        {
            byte[] imageData = null;
            var fileInfo = new FileInfo(filePath);
            long imageFileLength = fileInfo.Length;
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            imageData = br.ReadBytes((int) imageFileLength);
            fs.Close();
            br.Close();
            return imageData;
        }

        /// <summary>
        /// Converts a UUID to an OID
        /// </summary>
        /// <returns>Creates an OID</returns>
        public static string ConvertUuidToOid(string uuid)
        {
            return OIDHelper.UuidToOid(uuid);
        }

        /// <summary>
        /// Calculate an age based on the Birth Date
        /// </summary>
        /// <param name="birthdate"></param>
        /// <returns></returns>
        public static int CalculateAge(DateTime birthdate)
        {
            // get the difference in years
            int years = DateTime.Now.Year - birthdate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (DateTime.Now.Month < birthdate.Month ||
                (DateTime.Now.Month == birthdate.Month && DateTime.Now.Day < birthdate.Day))
                years--;

            return years;
        }

        #endregion

        #region ETP

        /// <summary>
        /// Creates a Prescriber
        /// </summary>
        /// <returns></returns>
        public static IParticipationPrescriber CreatePrescriber()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participant for a prescriber
        /// </summary>
        /// <returns></returns>
        public static IPrescriber CreateParticipantForPrescriber()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a person
        /// </summary>
        /// <returns>(IPersonPrescriber) Person</returns>
        public static IPersonPrescriber CreatePersonForPrescriber()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a Prescriber organisation
        /// </summary>
        /// <returns>(IParticipationPrescriberOrganisation) Participation</returns>
        public static IParticipationPrescriberOrganisation CreatePrescriberOrganisation()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participant for a prescriber organisation
        /// </summary>
        /// <returns>(IPrescriberOrganisation) Participant</returns>
        public static IPrescriberOrganisation CreateParticipantForPrescriberOrganisation()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a dispenser
        /// </summary>
        /// <returns></returns>
        public static IParticipationDispenser CreateDispenser()
        {
            return new Participation
            {
                Role = CreateRole(Occupation.Pharmacistsnfd)
            };
        }

        /// <summary>
        /// Creates a participant for a dispenser
        /// </summary>
        /// <returns></returns>
        public static IDispenser CreateParticipantForDispenser()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a person
        /// </summary>
        /// <returns>(IPersonDispenser) Person</returns>
        public static IPersonDispenser CreatePersonForDispenser()
        {
            return new Person();
        }

        /// <summary>
        /// Creates a Dispenser organisation
        /// </summary>
        /// <returns>(IParticipationDispenserOrganisation) Participation</returns>
        public static IParticipationDispenserOrganisation CreateDispenserOrganisation()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participant for a dispenser organisation
        /// </summary>
        /// <returns>(IDispenserOrganisation) Participant</returns>
        public static IDispenserOrganisation CreateParticipantForDispenserOrganisation()
        {
            return new Participant();
        }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseCDAModel()
        {
            ShowAdministrativeObservationsSection = true;
            ShowAdministrativeObservationsNarrativeAndTitle = true;
        }

        #endregion
    }
}
