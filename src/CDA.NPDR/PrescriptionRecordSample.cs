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
using CDA.Generator.Common.CDAModel.Entities;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.Sample;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.NPDR.Sample
{
  /// <summary>
  /// This project is intended to demonstrate how an PCEHRPrescriptionRecord CDA document can be created.
  /// 
  /// The project contains two samples, the first is designed to create a fully populated CDA document, including
  /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
  /// 
  /// The CDA model is split into three distinct sections, each of which needs to be created via the 
  /// PCEHRPrescriptionRecord class, and then populated with data as appropriate. The three sections that need to be
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
  public class PrescriptionRecordSample
  {
    #region Properties

    public static string OutputFolderPath { get; set; }

    public static String OutputFileNameAndPath
    {
      get
      {
        return OutputFolderPath + @"\PCEHRPrescriptionRecord.xml";
      }
    }

    // Note: Place this in any string field and and this will insert a break
    private const String DELIMITERBREAK = "<BR>";

    #endregion

    /// <summary>
    /// This sample populates only the mandatory Sections / Entries
    /// </summary>
    public XmlDocument MinPopulatedPrescriptionRecordSample(string fileName)
    {
      XmlDocument xmlDoc = null;

      var prescriptionRecord = PopulatePrescriptionRecord(true);

      try
      {
        CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

        //Pass the E-Referral model into the GeneratePrescriptionRecord method 
        xmlDoc = CDAGenerator.GeneratePcehrPrescriptionRecord(prescriptionRecord);

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
    /// each of its optional entries.
    /// </summary>
    public XmlDocument MaxPopulatedPrescriptionRecordSample(string fileName)
    {
      XmlDocument xmlDoc = null;

      var prescriptionRecord = PopulatePrescriptionRecord(false);

      try
      {
        CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

        //Pass the E-Referral model into the GeneratePrescriptionRecord method 
        xmlDoc = CDAGenerator.GeneratePcehrPrescriptionRecord(prescriptionRecord);

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

    #region Populate Methods


    /// <summary>
    /// This method populates an PCEHRPrescriptionRecord model with either the mandatory sections only, or both 
    /// the mandatory and optional sections
    /// </summary>
    /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
    /// <returns>PCEHRPrescriptionRecord</returns>
    internal static PCEHRPrescriptionRecord PopulatePrescriptionRecord(Boolean mandatorySectionsOnly)
    {
      var prescriptionRecord = PCEHRPrescriptionRecord.CreatePrescriptionRecord();

      // Set Creation Time
      prescriptionRecord.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

      // Include Logo
      prescriptionRecord.IncludeLogo = true;
      prescriptionRecord.LogoPath = OutputFolderPath;

      #region Setup and populate the CDA context model

      // Setup and populate the CDA context model
      var cdaContext = PCEHRPrescriptionRecord.CreateCDAContext();
      // Document Id
      cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
      // Set Id  
      cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
      // CDA Context Version
      cdaContext.Version = "2";

      if (mandatorySectionsOnly)
      {
        // Hide Administrative Observations
        prescriptionRecord.ShowAdministrativeObservationsNarrativeAndTitle = false;

        // Set Parent Document
        cdaContext.ParentDocuments = new List<ParentDocument>
                                     {
                                        CreateParentDocument(ReleatedDocumentType.Transform, null, BaseCDAModel.CreateGuid(), null, null, mandatorySectionsOnly)
                                     };
      }
      else
      {
          // Set Parent Document
        cdaContext.ParentDocuments = new List<ParentDocument>
                                     {
                                        CreateParentDocument(ReleatedDocumentType.Transform, null, BaseCDAModel.CreateGuid(), null, "1", mandatorySectionsOnly),
                                        CreateParentDocument(ReleatedDocumentType.Replace, cdaContext.SetId, BaseCDAModel.CreateGuid(), CDADocumentType.PrescriptionRecord, "1", mandatorySectionsOnly),
                                     };
      }

      var prescriberOrganisationId = new Guid(BaseCDAModel.CreateGuid()); 
      var prescriberId = new Guid(BaseCDAModel.CreateGuid()); 

      // Custodian
      cdaContext.Custodian = CreateCustodian(mandatorySectionsOnly, prescriberOrganisationId);

      cdaContext.LegalAuthenticator = CreateLegalAuthenticator(mandatorySectionsOnly, prescriberId);

      prescriptionRecord.CDAContext = cdaContext;

      #endregion

      #region Setup and Populate the SCS Context model
      // Setup and Populate the SCS Context model

      prescriptionRecord.SCSContext = PCEHRPrescriptionRecord.CreateSCSContext();

      prescriptionRecord.SCSContext.Prescriber = CreatePrescriber(mandatorySectionsOnly, prescriberId);
      prescriptionRecord.SCSContext.PrescriberOrganisation = CreatePrescriberOrganisation(mandatorySectionsOnly, prescriberOrganisationId);

      prescriptionRecord.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
      GenericObjectReuseSample.HydrateSubjectofCare(prescriptionRecord.SCSContext.SubjectOfCare, mandatorySectionsOnly, false);

      #endregion

      #region Setup and populate the SCS Content model
      // Setup and populate the SCS Content model

      // Create SCS Content
      prescriptionRecord.SCSContent = PCEHRPrescriptionRecord.CreateSCSContent();

      // Prescription Item
      prescriptionRecord.SCSContent.PrescriptionItem = CreatePrescriptionItem(mandatorySectionsOnly, BaseCDAModel.CreateGuid());

      #endregion

      return prescriptionRecord;
    }


    /// <summary>
    /// Creates and Hydrates an Parent Document Item
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatorySectionsOnly fields</param>
    /// <returns>A Hydrated ParentDocument </returns>
    internal static ParentDocument CreateParentDocument(ReleatedDocumentType documentType, Identifier setID, string parentIdentifier, CDADocumentType? CDAdocumentType, string versionNumber, Boolean mandatorySectionsOnly)
    {
      var parentDocument = BaseCDAModel.CreateParentDocument();

      // Related Document Type Code
      parentDocument.ReleatedDocumentType = documentType;

      // Represents the unique instance identifier of a clinical document.
      parentDocument.DocumentId = BaseCDAModel.CreateIdentifier(null, null, parentIdentifier, CDAdocumentType.HasValue ? "1.2.36.1.2001.1005.35" : "1.2.76.56.4567.6654.45", null);

      // An integer value used to version successive replacement documents.
      parentDocument.VersionNumber = versionNumber;

      if (!mandatorySectionsOnly)
      {
        // The code specifying the particular kind of document (e.g. History and Physical, Discharge Summary, Progress Note)
        parentDocument.DocumentType = CDAdocumentType;

        // Represents an identifier that is common across all document revisions
        parentDocument.SetId = setID;
      }

      return parentDocument;

    }

    /// <summary>
    /// Creates and Hydrates an Prescription Item.
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatorySectionsOnly fields</param>
    /// <param name="prescriptionId">Prescription Identifier </param>
    /// <returns>A Hydrated PrescriptionItem </returns>
    internal static IPCEHRPrescriptionItem CreatePrescriptionItem(Boolean mandatorySectionsOnly, string prescriptionId)
    {
      var prescriptionItem = PCEHRPrescriptionRecord.CreatePrescriptionItem();

      // The Dispensing Information Object
      prescriptionItem.DispensingInformation = CreateDispensingInformation(mandatorySectionsOnly);

      // Prescription Item Identifier - A globally unique object identifier for each instance of a Medication Instruction
      prescriptionItem.PrescriptionItemIdentifier = BaseCDAModel.CreateIdentifier( "1.2.36.1.2001.1005.36", BaseCDAModel.CreateGuid());

      if (!mandatorySectionsOnly)
      {
          // Therapeutic Good Identification - The medicine, vaccine or other therapeutic good being ordered, administered to or used by the subject of care
          prescriptionItem.TherapeuticGoodId = BaseCDAModel.CreateCodableText("04118R", CodingSystem.PBSCode, "ALUMINIUM HYDROXIDE with MAGNESIUM HYDROXIDE and SIMETHICONE Oral suspension 400 mg-400 mg-30 mg per 5 mL, 500 mL, 1", null, null);

          // Therapeutic Good Strength (Additional Therapeutic Good Detail) - Information concerning the strength of the Therapeutic Good
          prescriptionItem.TherapeuticGoodStrength = "Therapeutic Good Strength";

          // Therapeutic Good Generic Name (Additional Therapeutic Good Detail) - The generic name of the Therapeutic Good
          prescriptionItem.TherapeuticGoodGenericName = "Therapeutic Good Generic Name";

          // Directions - A complete narrative description of how much, when and how to use the medicine, vaccine or other therapeutic good
          prescriptionItem.Directions = "Directions";

          // Formula - The recipe for compounding a medicine
          prescriptionItem.Formula = "Formula";

          // Form - The formulation or presentation of the overall substance
          prescriptionItem.Form = BaseCDAModel.CreateCodableText("385057009", CodingSystem.SNOMED, "Film-coated tablet", null, null);

          // ClinicalIndication - A reason for ordering the medicine, vaccine or other therapeutic good 
          prescriptionItem.ClinicalIndication = "Clinical Indication";

          // Route - The route by which the medication is administered
          prescriptionItem.Route = BaseCDAModel.CreateCodableText("26643006", CodingSystem.SNOMED, "Oral route", null, null);

          // Comment - Any additional information that may be needed to ensure the continuity of supply, rationale for current dose and timing, or safe and appropriate use
          prescriptionItem.Comment = "Comment";

          // DateTime Prescription Expires
          prescriptionItem.DateTimePrescriptionExpires = new ISO8601DateTime(DateTime.Now.AddMonths(12));

          // PBS Manufacturer Code (Administrative Manufacturer Code) - Administrative code of the manufacturer of the pharmaceutical item supplied
          prescriptionItem.PBSManufacturerCode = BaseCDAModel.CreateExternalConceptIdentifier(ExternalConcepts.AustralianPBSManufacturerCode, "AB");
      } else
      {
          // Therapeutic Good Identification - The medicine, vaccine or other therapeutic good being ordered, administered to or used by the subject of care
        prescriptionItem.TherapeuticGoodId = BaseCDAModel.CreateCodableText("28237011000036107", CodingSystem.AMTV2, "amoxycillin 500 mg capsule, 20", null, null);
      }
      return prescriptionItem;
    }

    /// <summary>
    /// Creates and Hydrates an Dispensing Object
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatorySectionsOnly fields</param>
    /// <returns>A Hydrated Dispensing Object </returns>
    internal static DispensingInformation CreateDispensingInformation(Boolean mandatorySectionsOnly)
    {
      // The Dispensing Object
      var dispensingInformation = PCEHRPrescriptionRecord.CreateDispensingInformation();

       // Dispensing - Quantity Description - Free text description of the amount which may consist of the quantity and dose unit.
      dispensingInformation.QuantityDescription = "Dispensing Information - Quantity";

      if (!mandatorySectionsOnly)
      {
        // Brand Substitution Permitted - Indicates whether or not the substitution of a prescribed medicine with a different brand name of the same medicine, vaccine or other therapeutic good.
        // which has been determined as bioequivalent, is allowed when the medication is dispensed/supplied
        dispensingInformation.BrandSubstitutionPermitted = true;

        // Dispensing - Quantity Description - Free text description of the amount which may consist of the quantity and dose unit
        dispensingInformation.QuantityDescription = "Dispensing Information - Quantity";

        // Dispensing - Maximum Number Of Repeats - The number of times the expressed quantity of medicine, vaccine or other therapeutic good may be refilled or redispensed without a new prescription
        dispensingInformation.MaximumNumberOfRepeats = 6;

        // Dispensing - Minimum Interval Between Repeats - The minimum time between repeat dispensing of the medicine, vaccine or therapeutic good.
        dispensingInformation.MinimumIntervalBetweenRepeats = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Week);
      }

      return dispensingInformation;
    }

    /// <summary>
    /// Creates and Hydrates an Participation Prescriber(IParticipationPrescriber).
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatory fields</param>
    /// <param name="prescriberId">The Prescriber Id </param>
    /// <returns>A Hydrated IParticipationPrescriber </returns>
    internal static IParticipationPrescriber CreatePrescriber(Boolean mandatorySectionsOnly, Guid prescriberId)
    {
      var prescriber = BaseCDAModel.CreatePrescriber();

      prescriber.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

      // Date Time Prescription Written
      prescriber.Time = new ISO8601DateTime(DateTime.Now);

      prescriber.Participant = BaseCDAModel.CreateParticipantForPrescriber();

      // Prescriber Organisation Identifier
      prescriber.Participant.UniqueIdentifier = prescriberId;

      prescriber.Participant.Person = BaseCDAModel.CreatePersonForPrescriber();

      // Prescriber > Participant > Entity Identifier
      prescriber.Participant.Person.Identifiers = new List<Identifier> { 
          BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118") 
      };

      //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
      var name1 = BaseCDAModel.CreatePersonName();
      name1.GivenNames = new List<string> { "Good" };
      name1.FamilyName = "Doctor";
      name1.Titles = new List<string> { "Doctor" };
      name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

      var name2 = BaseCDAModel.CreatePersonName();
      name2.GivenNames = new List<string> { "Davey" };
      name2.FamilyName = "Wong";
      name2.Titles = new List<string> { "Brother" };
      name2.NameUsages = new List<NameUsage> { NameUsage.NewbornName };

      prescriber.Participant.Person.PersonNames = new List<IPersonName> { name1, name2 };

      prescriber.Participant.Person.Occupation = Occupation.GeneralMedicalPractitioner;

      if (!mandatorySectionsOnly)
      {
        // Prescriber > Participant > Address
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Business;
        address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
        address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
        address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
        address1.AustralianAddress.State = AustralianState.QLD;
        address1.AustralianAddress.PostCode = "5555";
        address1.AustralianAddress.DeliveryPointId = 32568931;

        prescriber.Participant.Addresses = new List<IAddress> { address1 };

        // Prescriber > Participant > Electronic Communication Detail
        var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
            "0345754566",
            ElectronicCommunicationMedium.Telephone,
            ElectronicCommunicationUsage.WorkPlace);

        var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
            "authen@globalauthens.com",
            ElectronicCommunicationMedium.Email,
            ElectronicCommunicationUsage.WorkPlace);

        prescriber.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

        // Prescriber > Participant > Entitlement
        var entitlement1 = BaseCDAModel.CreateEntitlement();

        entitlement1.Id = BaseCDAModel.CreateIdentifier(IdentifierType.PrescriberNumber.GetAttributeValue<NameAttribute, String>(x => x.Name),
                                                              null,
                                                              "1423444",
                                                              IdentifierType.PrescriberNumber.GetAttributeValue<NameAttribute, String>(x => x.Code),
                                                              null);

        entitlement1.Type = EntitlementType.MedicarePrescriberNumber;
        entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

        prescriber.Participant.Person.Entitlements = new List<Entitlement> { entitlement1, entitlement1 };

        prescriber.Participant.Person.Qualifications = "M.B.B.S., F.R.A.C.S.";
      }

      return prescriber;
    }

    /// <summary>
    /// Creates and Hydrates an Prescriber Organisation(IParticipationPrescriberOrganisation).
    /// </summary>
    /// <returns>A Hydrated Prescriber Organisation</returns>
    internal static IParticipationPrescriberOrganisation CreatePrescriberOrganisation(Boolean mandatorySectionsOnly, Guid prescriberOrganisationId)
    {
      IParticipationPrescriberOrganisation participation = BaseCDAModel.CreatePrescriberOrganisation();

      participation.Role = BaseCDAModel.CreateRole(HealthcareFacilityTypeCodes.ChildCareServices);
      participation.Participant = BaseCDAModel.CreateParticipantForPrescriberOrganisation();

      // Prescriber Organisation Identifier
      participation.Participant.UniqueIdentifier = prescriberOrganisationId;

      participation.Participant.Organisation = BaseCDAModel.CreateOrganisation();

      // Prescribe rOrganisation > Participant > Entity Identifier
      participation.Participant.Organisation.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null), 
                BaseCDAModel.CreateHealthIdentifier (HealthIdentifierType.HPIO, "8003620833333789") 
            };

      // Organisation Name
      participation.Participant.Organisation.Name = "West End Healthiness";

      if (!mandatorySectionsOnly)
      {

        // Prescriber Organisation > Address
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Business;
        address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
        address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
        address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
        address1.AustralianAddress.State = AustralianState.QLD;
        address1.AustralianAddress.PostCode = "5555";
        address1.AustralianAddress.DeliveryPointId = 32568931;

        participation.Participant.Addresses = new List<IAddress> { address1 };

        participation.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>()
            {
                BaseCDAModel.CreateElectronicCommunicationDetail("0712341234", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace)
            };

        var identifier = BaseCDAModel.CreateIdentifier
            (
                "SampleAuthority",
                HealthcareIdentifierGeographicArea.StateOrTerritoryIdentifier,
                "455458",
                "1.22.333.444.55575",
                BaseCDAModel.CreateCodableText("1.1.1.1.1.1", CodingSystem.NCTIS, "DisplayName", "Original Text", null)
            );

        // Subject of Care > Participant > Entitlement
        var entitlement1 = BaseCDAModel.CreateEntitlement();
        entitlement1.Id = identifier;
        entitlement1.Type = EntitlementType.MedicareBenefits;
        entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

        var entitlement2 = BaseCDAModel.CreateEntitlement();
        entitlement2.Id = identifier;
        entitlement2.Type = EntitlementType.MedicareBenefits;
        entitlement2.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

        participation.Participant.Entitlements = new List<Entitlement> { entitlement1, entitlement2 };
      }

      return participation;
    }

    /// <summary>
    /// Creates and Hydrates an Authenticator
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <returns>A Hydrated authenticator</returns>
    internal static IParticipationLegalAuthenticator CreateLegalAuthenticator(Boolean mandatorySectionsOnly, Guid legalAuthenticatorId)
    {
      var authenticator = BaseCDAModel.CreateLegalAuthenticator();

      // LegalAuthenticator/assignedEntity
      authenticator.Participant = BaseCDAModel.CreateParticipantForLegalAuthenticator();

      // set the identifier for the Legal Authenticator
      authenticator.Participant.UniqueIdentifier = legalAuthenticatorId;

      // LegalAuthenticator/time/@value
      authenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Minute);

      // LegalAuthenticator/assignedEntity/assignedPerson
      authenticator.Participant.Person = BaseCDAModel.CreatePerson();

      var localAuthorityCode = BaseCDAModel.CreateCodableText("EI", CodingSystem.HL7IdentifierType, null, null, null);

      // LegalAuthenticator/assignedEntity/<Entity Identifier>
      authenticator.Participant.Person.Identifiers = new List<Identifier> 
                { 
                  BaseCDAModel.CreateIdentifier("LocalAuthority", null, "66666", "1.2.3.4.5", localAuthorityCode), 
                  BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, "8003615833334118") 
                };

      //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
      var name1 = BaseCDAModel.CreatePersonName();
      name1.GivenNames = new List<string> { "Good" };
      name1.FamilyName = "Doctor";
      name1.Titles = new List<string> { "Doctor" };
      name1.NameUsages = new List<NameUsage> { NameUsage.Legal };

      authenticator.Participant.Person.PersonNames = new List<IPersonName> { name1 };

      if (!mandatorySectionsOnly)
      {
        // LegalAuthenticator/assignedEntity/code
        authenticator.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

        // LegalAuthenticator/assignedEntity/<Address>
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Residential;
        address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
        address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
        address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
        address1.AustralianAddress.State = AustralianState.QLD;
        address1.AustralianAddress.PostCode = "5555";
        address1.AustralianAddress.DeliveryPointId = 32568931;

        var addressList = new List<IAddress> { address1 };

        authenticator.Participant.Addresses = addressList;

        // LegalAuthenticator/assignedEntity/<Electronic Communication Detail>
        var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
            "0345754566",
            ElectronicCommunicationMedium.Telephone,
            ElectronicCommunicationUsage.WorkPlace);

        var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
            "authen@globalauthens.com",
            ElectronicCommunicationMedium.Email,
            ElectronicCommunicationUsage.WorkPlace);
        authenticator.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

        // LegalAuthenticator/assignedEntity/representedOrganization
        authenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

        // LegalAuthenticator/assignedEntity/representedOrganization/name
        authenticator.Participant.Organisation.Name = "West End Healthiness";

        // LegalAuthenticator/assignedEntity/representedOrganization/<Entity Identifier>
        authenticator.Participant.Organisation.Identifiers = new List<Identifier> 
                { 
                  BaseCDAModel.CreateIdentifier("SampleAuthority", null, null, "1.2.3.4.5.66666", null), 
                  BaseCDAModel.CreateHealthIdentifier (HealthIdentifierType.HPIO, "8003620833333789") 
                };
      }

      return authenticator;
    }

    /// <summary>
    /// Creates and Hydrates a custodian
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatory fields</param>
    /// <param name="custodianId">The Custodian </param>
    /// <returns>A Hydrated IParticipationCustodian </returns> 
    internal static IParticipationCustodian CreateCustodian(Boolean mandatorySectionsOnly, Guid custodianId)
    {
      var participationCustodian = BaseCDAModel.CreateCustodian();
      var custodian = BaseCDAModel.CreateParticipantCustodian();

      custodian.UniqueIdentifier = custodianId;

      // custodian/assignedCustodian
      participationCustodian.Participant = custodian;

      // custodian/assignedCustodian/representedCustodianOrganization
      custodian.Organisation = BaseCDAModel.CreateOrganisationName();

      // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
      custodian.Organisation.Identifiers = new List<Identifier> { 
                BaseCDAModel.CreateHealthIdentifier (HealthIdentifierType.HPIO, "8003620833333789") 
                };

      // custodian/assignedCustodian/representedCustodianOrganization/name
      custodian.Organisation.Name = "General Practice Clinic";

      if (!mandatorySectionsOnly)
      {
        // custodian/assignedCustodian/representedCustodianOrganization/<Address>
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Business;
        address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
        address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
        address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
        address1.AustralianAddress.State = AustralianState.QLD;
        address1.AustralianAddress.PostCode = "5555";
        address1.AustralianAddress.DeliveryPointId = 32568931;

        custodian.Address = address1;

        // custodian/assignedCustodian/representedCustodianOrganization/<Electronic Communication Detail>
        var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail("0712341234", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace);
        custodian.ElectronicCommunicationDetail = coms1;
      }

      return participationCustodian;
    }

    #endregion

  }
}
