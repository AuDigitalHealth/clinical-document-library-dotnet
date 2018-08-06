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
  /// This project is intended to demonstrate how an PCEHRDispenseRecord CDA document can be created.
  /// 
  /// The project contains two samples, the first is designed to create a fully populated CDA document, including
  /// all of the optional sections and entries. The second sample only populates the mandatory sections and entries.
  /// 
  /// The CDA model is split into three distinct sections, each of which needs to be created via the 
  /// PCEHRDispenseRecord class, and then populated with data as appropriate. The three sections that need to be
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
  public class DispenseRecordSample
  {
    #region Properties

    public static string OutputFolderPath { get; set; }

    public static String OutputFileNameAndPath
    {
      get
      {
        return OutputFolderPath + @"\PCEHRDispenseRecord.xml";
      }
    }

    #endregion

    /// <summary>
    /// This sample populates only the mandatory Sections / Entries
    /// </summary>
    public XmlDocument MinPopulatedDispenseRecordSample(string fileName)
    {
      XmlDocument xmlDoc = null;

      var dispenseRecord = PopulateDispenseRecord(true);

      try
      {
        CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

        //Pass the E-Referral model into the GenerateDispenseRecord method 
        xmlDoc = CDAGenerator.GeneratePcehrDispenseRecord(dispenseRecord);

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
    public XmlDocument MaxPopulatedDispenseRecordSample(string fileName)
    {
      XmlDocument xmlDoc = null;

      var dispenseRecord = PopulateDispenseRecord(false);

      try
      {
        CDAGenerator.NarrativeGenerator = new CDANarrativeGenerator();

        //Pass the E-Referral model into the GenerateDispenseRecord method 
        xmlDoc = CDAGenerator.GeneratePcehrDispenseRecord(dispenseRecord);

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
    /// This method populates an PCEHRDispenseRecord model with either the mandatory sections only, or both 
    /// the mandatory and optional sections
    /// </summary>
    /// <param name="mandatorySectionsOnly">mandatorySectionsOnly</param>
    /// <returns>PCEHRDispenseRecord</returns>
    internal static PCEHRDispenseRecord PopulateDispenseRecord(Boolean mandatorySectionsOnly)
    {
      var dispenseRecord = PCEHRDispenseRecord.CreateDispenseRecord();

      // Set Creation Time
      dispenseRecord.DocumentCreationTime = new ISO8601DateTime(DateTime.Now);

      // Include Logo
      dispenseRecord.IncludeLogo = true;

      #region Setup and populate the CDA context model

      // Setup and populate the CDA context model
      var cdaContext = PCEHRDispenseRecord.CreateCDAContext();
      // Document Id
      cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
      // Set Id  
      cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
      // CDA Context Version
      cdaContext.Version = "2";

      if (mandatorySectionsOnly)
      {
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
                                        CreateParentDocument(ReleatedDocumentType.Replace, cdaContext.SetId,BaseCDAModel.CreateGuid(), CDADocumentType.DispenseRecord, "1", mandatorySectionsOnly),
                                     };
      }

      var dispenserOrganisationId = new Guid(BaseCDAModel.CreateGuid()); 
      var dispenserId = new Guid(BaseCDAModel.CreateGuid()); 

      // Custodian
      cdaContext.Custodian = CreateCustodian(mandatorySectionsOnly, dispenserOrganisationId);

      cdaContext.LegalAuthenticator = CreateLegalAuthenticator(mandatorySectionsOnly, dispenserId);

      dispenseRecord.CDAContext = cdaContext;

      #endregion

      #region Setup and Populate the SCS Context model
      // Setup and Populate the SCS Context model

      dispenseRecord.SCSContext = PCEHRDispenseRecord.CreateSCSContext();

      dispenseRecord.SCSContext.Dispenser = CreateDispenser(mandatorySectionsOnly, dispenserId);
      dispenseRecord.SCSContext.DispenserOrganisation = CreateDispenserOrganisation(mandatorySectionsOnly, dispenserOrganisationId);

      dispenseRecord.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
      GenericObjectReuseSample.HydrateSubjectofCare(dispenseRecord.SCSContext.SubjectOfCare, mandatorySectionsOnly);

      #endregion

      #region Setup and populate the SCS Content model
      // Setup and populate the SCS Content model

      // Create SCS Content
      dispenseRecord.SCSContent = PCEHRDispenseRecord.CreateSCSContent();

      // Prescription Item
      dispenseRecord.SCSContent.DispenseItem = CreateDispenseItem(mandatorySectionsOnly, BaseCDAModel.CreateGuid());

      #endregion

      return dispenseRecord;
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
      parentDocument.DocumentId = BaseCDAModel.CreateIdentifier(null, null, parentIdentifier, CDAdocumentType.HasValue ? "1.2.36.1.2001.1005.35" : "1.2.76.56.4567.6543.45", null);

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
    /// Creates and Hydrates an Dispense Item.
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatorySectionsOnly fields</param>
    /// <returns>A Hydrated DispenseItem </returns>
    internal static IPCEHRDispenseItem CreateDispenseItem(Boolean mandatorySectionsOnly, string dispenseDocumentId)
    {
      var dispenseItem = PCEHRDispenseRecord.CreateDispenseItem();

      // Status of the document
      dispenseItem.Status = MedicationStatus.Completed;

      // Dispense Item Identifier (Medication Action Instance Identifier)
      dispenseItem.DispenseItemIdentifier = BaseCDAModel.CreateIdentifier("DispenseItemIdentifierAssigningAuthorityName", null, dispenseDocumentId, "1.2.3.4.5.66666", null);

      // DateTime of Dispense Event (Medication Action DateTime) 
      dispenseItem.DateTimeOfDispenseEvent = new ISO8601DateTime(DateTime.Now);

        if (!mandatorySectionsOnly)
        {
            // Therapeutic Good Identification - The medicine, vaccine or other therapeutic good being ordered, administered to or used by the subject of care
            dispenseItem.TherapeuticGoodId = BaseCDAModel.CreateCodableText("01471K", CodingSystem.PBSCode, "fluconazole 50 mg capsule, 28");

            // Example of translation
            //var translations = new List<ICodableTranslation>();
            //translations.Add(BaseCDAModel.CreateCodableTranslation("01471K", CodingSystem.PBSCode, "fluconazole 50 mg capsule, 28"));

            //dispenseItem.TherapeuticGoodId = BaseCDAModel.CreateTherapeuticGoodIdentification("28190011000036104",
            //    CodingSystem.AMTV3, "fluconazole 50 mg capsule, 28", null, translations);

            // Therapeutic Good Strength (Additional Therapeutic Good Detail) - Information concerning the strength of the Therapeutic Good
            dispenseItem.TherapeuticGoodStrength = "Therapeutic Good Strength (Additional Therapeutic Good Detail)";

            // Therapeutic Good Generic Name (Additional Therapeutic Good Detail) - The generic name of the Therapeutic Good
            dispenseItem.TherapeuticGoodGenericName = "Therapeutic Good Generic Name";

            // Additional Dispensed Item Description (Additional Therapeutic Good Detail)
            dispenseItem.AdditionalDispensedItemDescription =
                "Additional Dispensed Item Description (Additional Therapeutic Good Detail)";

            // Label Instruction (Medication Action Instructions)
            dispenseItem.LabelInstruction = "Label Instruction (Medication Action Instructions)";

            // Formula - The recipe for compounding a medicine
            dispenseItem.Formula = "Formula";

            // Form - The formulation or presentation of the overall substance
            dispenseItem.Form = BaseCDAModel.CreateCodableText("385057009", CodingSystem.SNOMED, "Film-coated tablet");

            // Dispensing - Quantity Description - Free text description of the amount which may consist of the quantity and dose unit.
            dispenseItem.QuantityDescription = "Dispensing Information - Quantity";

            // Comment (Medication Action Comment)
            dispenseItem.Comment = "Comment (Medication Action Comment)";

            // Brand Substitution Permitted - Indicates whether or not the substitution of a prescribed medicine with a different brand name of the same medicine, vaccine or other therapeutic good.
            // which has been determined as bioequivalent, is allowed when the medication is dispensed/supplied
            dispenseItem.BrandSubstitutionOccurred = false;

            // Number of this Dispense
            dispenseItem.NumberOfThisDispense = 1;

            // Maximum Number Of Repeats - The number of times the expressed quantity of medicine, vaccine or other therapeutic good may be refilled or re-dispensed without a new prescription
            dispenseItem.MaximumNumberOfRepeats = 6;

            // PBS Manufacturer Code (Administrative Manufacturer Code) - Administrative code of the manufacturer of the pharmaceutical item supplied
            dispenseItem.PBSManufacturerCode =
                BaseCDAModel.CreateExternalConceptIdentifier(ExternalConcepts.AustralianPBSManufacturerCode, "AB");

            // Unique Pharmacy Prescription Number (Administrative System Identifier)
            dispenseItem.UniquePharmacyPrescriptionNumber = "UniquePharmacyPrescriptionNumber";

            // Prescription Item Identifier - A globally unique object identifier for each instance of a Medication Instruction
            dispenseItem.PrescriptionItemIdentifier = BaseCDAModel.CreateIdentifier(
                "PrescriptionItemIdentifierAssigningAuthorityName", null, BaseCDAModel.CreateGuid(),
                "1.2.36.1.2001.1005.36", null);
        }
        else
        {
            // Therapeutic Good Identification - The medicine, vaccine or other therapeutic good being ordered, administered to or used by the subject of care
            dispenseItem.TherapeuticGoodId = BaseCDAModel.CreateCodableText("23641011000036102", CodingSystem.AMTV2,
                "paracetamol 500 mg + codeine phosphate 30 mg tablet");
        }

        return dispenseItem;
    }

    /// <summary>
    /// Creates and Hydrates an Participation Dispenser(IParticipationDispenser).
    /// 
    /// Note: the data used within this method is intended as a guide and should be replaced.
    /// </summary>
    /// <param name="mandatorySectionsOnly">Only show mandatory fields</param>
    /// <param name="dispenserId">The identifier of the dispenser</param>
    /// <returns>A Hydrated IParticipationDispenser </returns>
    internal static IParticipationDispenser CreateDispenser(Boolean mandatorySectionsOnly, Guid dispenserId)
    {
      var dispenser = BaseCDAModel.CreateDispenser();
      dispenser.Participant = BaseCDAModel.CreateParticipantForDispenser();
      dispenser.Participant.UniqueIdentifier = dispenserId;

      dispenser.Role = BaseCDAModel.CreateRole(Occupation.HospitalPharmacist);

      // Date Time Authored
      dispenser.Time = new ISO8601DateTime(DateTime.Now);

      // Person
      dispenser.Participant.Person = BaseCDAModel.CreatePersonForDispenser();

      // Dispenser > Participant > Entity Identifier
      dispenser.Participant.Person.Identifiers = new List<Identifier> { 
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

      dispenser.Participant.Person.PersonNames = new List<IPersonName> { name1, name2 };

      if (!mandatorySectionsOnly)
      {
        // Dispenser > Participant > Address
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Business;
        address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
        address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
        address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
        address1.AustralianAddress.State = AustralianState.QLD;
        address1.AustralianAddress.PostCode = "5555";
        address1.AustralianAddress.DeliveryPointId = 32568931;

        dispenser.Participant.Addresses = new List<IAddress> { address1 };

        // Dispenser > Participant > Electronic Communication Detail
        var coms1 = BaseCDAModel.CreateElectronicCommunicationDetail(
            "0345754566",
            ElectronicCommunicationMedium.Telephone,
            ElectronicCommunicationUsage.WorkPlace);

        var coms2 = BaseCDAModel.CreateElectronicCommunicationDetail(
            "authen@globalauthens.com",
            ElectronicCommunicationMedium.Email,
            ElectronicCommunicationUsage.WorkPlace);

        dispenser.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { coms1, coms2 };

        dispenser.Participant.Person.Qualifications = "M.B.B.S., F.R.A.C.S.";
      }

      return dispenser;
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

      // LegalAuthenticator/time/@value
      authenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(DateTime.Now, ISO8601DateTime.Precision.Minute);

      if (!mandatorySectionsOnly)
      {
        // LegalAuthenticator/assignedEntity/code
        authenticator.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner);

        // LegalAuthenticator/assignedEntity/<Address>
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Business;
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

        authenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

        authenticator.Participant.Organisation.Name = "West End Healthiness";

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
      custodian.Organisation.Name = "West End Healthiness";

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

    /// <summary>
    /// Creates and Hydrates an Dispenser Organisation(IParticipationDispenserOrganisation).
    /// </summary>
    /// <returns>A Hydrated Dispenser Organisation</returns>
    internal static IParticipationDispenserOrganisation CreateDispenserOrganisation(Boolean mandatorySectionsOnly, Guid dispenserOrganisationId)
    {
      IParticipationDispenserOrganisation participation = BaseCDAModel.CreateDispenserOrganisation();

      participation.Participant = BaseCDAModel.CreateParticipantForDispenserOrganisation();
      participation.Participant.UniqueIdentifier = dispenserOrganisationId;

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
        // Organisation Continued
        participation.Participant.Organisation.NameUsage = OrganisationNameUsage.LocallyUsedName;
        participation.Participant.Organisation.Department = "General Health";

        // Dispenser Organisation > Address
        var address1 = BaseCDAModel.CreateAddress();
        address1.AddressPurpose = AddressPurpose.Business;
        address1.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
        address1.AustralianAddress.UnstructuredAddressLines = new List<string> { "1 Clinician Street" };
        address1.AustralianAddress.SuburbTownLocality = "Nehtaville";
        address1.AustralianAddress.State = AustralianState.QLD;
        address1.AustralianAddress.PostCode = "5555";
        address1.AustralianAddress.DeliveryPointId = 32568931;

        participation.Participant.Addresses = new List<IAddress> { address1 };

        participation.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
            {
                BaseCDAModel.CreateElectronicCommunicationDetail("0712341234", ElectronicCommunicationMedium.Telephone, ElectronicCommunicationUsage.WorkPlace)
            };

        // Prescriber > Participant > Entitlement
        var entitlement1 = BaseCDAModel.CreateEntitlement();

        var code = BaseCDAModel.CreateCodableText("11", CodingSystem.NCTISEntitlementTypeValues,
                                                  "Medicare Pharmacy Approval Number", null, null);


        entitlement1.Id = BaseCDAModel.CreateIdentifier("AGIMO (Australian Government Information Management Office)",
                                                              null,
                                                              "1234567892",
                                                              "1.2.36.174030967.1.3.2.1",
                                                              code);

        entitlement1.Type = EntitlementType.MedicarePharmacyApprovalNumber;
        entitlement1.ValidityDuration = BaseCDAModel.CreateInterval("1", TimeUnitOfMeasure.Year);

        participation.Participant.Entitlements= new List<Entitlement> { entitlement1 };

      }
      return participation;
    }

    #endregion

  }
}
