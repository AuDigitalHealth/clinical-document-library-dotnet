## Change Log/Revision History

4.11.3 - Reference Platform Clinical Document Library (June 2023)
----------------------------------------------------
- Removed scopingOrganization and representedOrganization tags in ACTS

4.11.2 - Reference Platform Clinical Document Library (May 2023)
----------------------------------------------------
- Pathology and DI sample clean up to pass IQ rules
- Fix issue for multiple AnatomicalSite not being displayed

4.11.1 - Reference Platform Clinical Document Library (May 2023)
----------------------------------------------------
- Fixed Service Referral. Wasn't populating Medical History.

4.11.0 - Reference Platform Clinical Document Library (May 2023)
----------------------------------------------------
- Added the 3 ACTS documents
- Added support for Custom Narrative and Attachments for 1B docs

4.10.1 - Reference Platform Clinical Document Library (Mar 2023)
----------------------------------------------------
- relaxed address nullflavor

4.10.0 - Reference Platform Clinical Document Library (Feb 2023)
----------------------------------------------------
- Raised min framework platform to 4.5.2

4.9.4 - Reference Platform Clinical Document Library (Feb 2023)
----------------------------------------------------
- Fixed multiple titles in output cda
- Updated Narrative for ACPD
- Improved ICodableText and ICodableTranslation mapping

4.9.3 - Reference Platform Clinical Document Library (Dec 2022)
----------------------------------------------------
- Electronic Comms - support for HTTPS

4.9.2 - Reference Platform Clinical Document Library (Dec 2022)
----------------------------------------------------
- Electronic Comms - throw fault when type is not supported
- Minor code clean up 

4.9.1 - Reference Platform Clinical Document Library (Aug 2022)
----------------------------------------------------
- Fixed nuget package

4.9.0 - Reference Platform Clinical Document Library (Aug 2022)
----------------------------------------------------
- Added support for netstandard2.0
- Allow half-bounded intervals and nullFlavors
- Unit Code and Display Name. Narrative generation for half-bounded int… 
- Make CreateQuantityRange backwards compatible

4.8.0 - Reference Platform Clinical Document Library (Jan 2022)
----------------------------------------------------
- Updated Structured Pathology to meet latest conformance profile
- Changed Specimen OID from "102.16156.220.2.1" to "102.16156"
- Updated Text for OID "2.16.840.1.113883.12.74" to "HL7 Diagnostic service section ID"
- Updated ReportingPathologist in Context from 1..1 to 1..*


4.7.0 - Reference Platform Clinical Document Library (Sept 2021)
----------------------------------------------------
- Added Enum and Coding System for Hl7V3EmployeeJobClass = employment.jobClassCode
  and updated examples. Supported by MyHR. Change to CodingSystem.cs and new file HL7V3EmployeeJobClass.cs
- Added example for SL to put in a logo
- Added new CDA Schema with one additional field interpreterRequiredInd
- Added support for Sub type title for DS,SL,ES and ACI(GOC,ACP)

4.6.2 - Reference Platform Clinical Document Library (Nov 2019)
----------------------------------------------------
- Added Goals of Care document type

4.6.1 - Reference Platform Clinical Document Library (Sep 2019)
----------------------------------------------------
- Removed <paragraph> tag for 1A specialist letter narrative (as TP has not been fixed)
- Removed incorrect template Id - accidently introduced in Jan 2018
- Cleaned up example documents to remove warnings when IQ rules run
- Corrected inversionInd flags when not required
- Renamed project to PSML

4.6.0 - Reference Platform Clinical Document Library (July 2019)
----------------------------------------------------
- Relax the validation check on the role of the document author to allow multiple authors 
  to be specified in the narrative and consequently none in the atomic data in pathology 
  reports and diagnostic imaging reports.
- Relax the validation check on the organisation of the person to allow the requester 
  organisation to be unknown.
- Removed incorrect occupation code - 2515

4.5.3 - Reference Platform Clinical Document Library (Jan 2019)
----------------------------------------------------
- Updated captions to Clinical Summary for DS and ES for Clinical Synopsis and Event Details sections 
- Updated header of table to "Description" for Clinical Synopsis
- Updated caption to Follow-up Appointments for DS for Arranged Services section
- Updated Column headings and order for medicines on discharge + "-" for empty cells
- Updated "Clinical Intervention Description" to "Procedures" for DS and change table to bullet list
- Updated "Recommendations" to just two columns Recommendation and Person responsible
- Updated section text for DS, REF, SPEC, ES, SREF docs.
- Generated summary table for diagnostic investigations in DS documents
- Fixed up Other Test Result, adding renderMultiMedia/observationMedia to section as per FAQ
  Affects DS, ES, EREF, SL
- Changed Other Test Result time to a value rather than low (Affects DS, ES, EREF, SL)

4.5.2 - Reference Platform Clinical Document Library (Dec 2018)
----------------------------------------------------
- Fixed up AddressAbsentIndicator.Masked mapping to  "ASKU" rather than "MSK"

4.5.1 - Reference Platform Clinical Document Library (Oct 2018)
----------------------------------------------------
- Fix up logo path when specifying different OutputFolder
- PCML - added identifier for Participant, plus example custom Narrative (not yet supported though)
- Updated PCML to PSML

4.5.0 - Reference Platform Clinical Document Library (Aug 2018)
----------------------------------------------------
- Added support for PCML generation - aligning to conformance profile
- removed restriction on Address.cs nullflavor
- Added Hl7v2 DI to CDA functionality
- Fixed HL7 requester.ParticipationEndTime to OBR-27.5 rather than OBR-22
- Added InformationRecipient to PCML Library
- Added HealthcareFacility to PCML Library

4.4.2 - Reference Platform Clinical Document Library (Feb 2018)
----------------------------------------------------
- Removed originalText for Diagnostic imaging study - not required.
- Fix UTC offset when using DaylightSavings time

4.4.1 - Reference Platform Clinical Document Library (Dec 2017)
----------------------------------------------------
- Fixed Service Referral to match IG 1.1 dv019 (Template id and Additional Comments)

4.4.0 - Reference Platform Clinical Document Library (Dec 2017)
----------------------------------------------------
- Fixed the Adding of a Blank <content> tags in Discharge Summary (only) section “Record of Recommendations and Information Provided”
- Fixed OID in AIR Document for Australian Immunisation Register Entries (101.17039)
- Added support for additional Media Types (For Service Referral)
- Fixed up DRIV issue if including Interpreter
- Removed double Ref to Path attachments and made them linkHtml
- Added Hl7v2 Path to CDA functionality
- Added updated CDA Schema to support new document types
- Fixed Nullflavor for an organisation with no name for a participant

4.3.1 - Reference Platform Clinical Document Library
----------------------------------------------------
- A Reporting Radiologist Name can now be added to the narrative for a Diagnostic Imaging Examination Result section for a Diagnostic Imaging Report.
- Pathology Report documents author electronic communications details mandatory validation has been removed.
- Removed the fixed Snomed Version for Pathology report status
- Added Caching Class for XMLSerializer as it causes a Memory leak if continuously used (in Common lib)
- Updated any 'new XmlSerializer' with the CachingXmlSerializerFactory.Create method instead (CDAGeneratorHelp.cs, Extension.cs)
- Fixed typo 'Not stated/inadequated Described' to 'Not stated/inadequately described'
- Removed requirement for BouncyCastle.Crypto.dll
- Extensions for laterality (IPCDOCS-98)
- Add fix for Interval 'Create Value' - To populate the value field of IVL_TS


4.3.0 - Reference Platform Clinical Document Library
----------------------------------------------------
- Updated validation of participation field Role to be non-mandatory.
- Updated the field Indigenous Status to be non-mandatory.
- Updated Level 1A narrative generator to put each rendered multimedia item into a separate paragraph.
- Updated problem where OtherDiagnosisEntries was not displaying when no Diagnoses was included 
- Updated to allow ServiceCommencementWindow and ServiceScheduled to be specified together.
- Added Reporting Pathologist Name Field - can now be added to the narrative for a test result.
- Updated Diagnostic Imaging - Examination Result - TargetSitCode to accept a nullflavor.
- Updated R5 documents requested organisation name and person last name to accept a nullflavor.
- Corrected where Medical History - 'date of onset' validation.
- Support added for care agency employee identifiers in clinical documents.
- Updated RecomendationOrChange where both values contained '01'.
- Populated RecomendationOrChange where both values contained '01'.
- Updated validation to allow 10 or 11 Medicare card numbers.
- Removed validation check for telcomm being Null


4.2.1 - Reference Platform Clinical Document Library
----------------------------------------------------
- Prescription Date time prescription expires from low to value

4.2.0 - Reference Platform Clinical Document Library
----------------------------------------------------
- Correct Narrative for Adverse Reactions
- Update the way the Interval narrative displays
- Update Pathology Test Result
- Update Diagnostic Investigations
- Update Diagnostic Imaging Report
- Update Event Summary
- Update Discharge Summary
- Update Shared Health Summary


4.1.0 - Reference Platform CDA Library
----------------------------------------------------
- Removed the template id for Administration Observations Section
- Typo fix changed class Immunisations to Immunisations
- Changed Person Occupation to an CodedText that accepts an enum 
- Provide Medicare number validation. Requires 11 digits for an Individual Medicare Card Number and 10 for Medicare Card Number
- ClinicalSynopsisDescription - Changed from a CodedText to string
- Include Path and DI NPDR Prescription and NPDR Dispense CDA libraries 
- Added the Ability to add logo as an byte[]

4.0.2 - Reference Platform CDA Library
----------------------------------------------------
- 1b support for cda libraries SL, DS & ER.

4.0.1 - Reference Platform CDA Library
----------------------------------------------------
- Added HealthcareFacilityTypeCodes enum to assist in the generation of the healthCareFacility code.
- Updated Medical history items so the most recent dates are now listed higher in the table.
- Added CUP validation for Medical History - exclusion statements in Shared Health Summary.
- Fixed downgradable issue for VS 2008.

4.0.0 - Reference Platform CDA Library
----------------------------------------------------
- Updated the CDA Library to Visual Studio 2010 (Note: Still downgradable to 2008)
- Included exclusion statements for Shared Health Summary - Medical History.
  (Note exclusion statements were removed in the previous version and have been included again for this release)
- Included an example of how CDA documents can be extended.
- Included validation for Event Summary - ClinicalSynopsisDescription. This field only accepts a non-coded entry, 
  Validation has been included to ensure that the entry is not a coded entry.


3.1.0 - Reference Platform CDA Library
----------------------------------------------------
- Updated Clinical Documents to be compliant with the Latest schematrons and conformance profiles.
- Corrected problems with rendering images for Other Test Result (102.16029).
- Updated samples to be 3b compliant.
- Automated the generation of exclusion statements for Medical History across documents.
- Changed the functions in the narrative generator to be public for reuse for custom narratives.
- Allow a byte[] to be provided for External Data

3.0.9 - Reference Platform CDA Library
----------------------------------------------------
- Fix for Medical History where the single interval item in the narrative was represented as '-' changed to '->'
- Added an XML comment for the supported template packages 
- ShowAdministrativeObservation has been split into two variables. 

'ShowAdministrativeObservationsSection – This completely removes the AdministrativeObservations section for Point to Point.
'ShowAdministrativeObservationsNarrativeAndTitle' – This removes the Narrative and Section Title for Administrative Observations.

3.0.8 - Reference Platform CDA Library
----------------------------------------------------
- Fix for Diagnostic Investigations - 'Anatomical Site' xml.
- Fix for image narrative generation for section ‘102.16029 diagnostic investigations’

3.0.7 - Reference Platform CDA Library
----------------------------------------------------
- Updated the narrative in the CDA Library to align with the Best Practice Guidelines for Clinical Document Presentation
- Included a class called OIDHelper to assist in converting an UUID to an OID 
- The ICodableText - CodeSystemName is now an optional field not a mandatory field in CDA Library
- Updated the samples to include valid Snomed and NCTIS codes 
- Removed Date of Resolution from event Summary as this field is not included in the Implementation Guide
- Fixed the bug where an empty string for Medical History Item Comment produced an invalid CDA structure
- Removed all free text (structDoc.Text) from the Narrative and placed it in a (strucDocText.paragraph)
- Added 'Not stated' to omit the address purpose in all addresses


3.0.6 - Compliant with CCA Validator version 1.12.5
----------------------------------------------------
- The Administration Observation sections are optional for point to point delivery
- Updated the Report Content Other Test Result section to allow plain text or a file attachment
- Updated the ‘Mode of Separation’ for discharge summary to display friendly display names
  (note this change will only be compatible with the next release of Schematron)
- Enable the ability to specify the file location for an attached LOGO file


3.0.5 - Compliant with CCA Validator version 1.12.3
----------------------------------------------------
- Include validation to ensure that the Logo.png file is included in the output directory

3.0.4 - Compliant with CCA Validator version 1.12.3
----------------------------------------------------
- Restriction DateOfResolutionRemission must have a precision of 'Day', 'Hour' or 'Year' with no Time Zone specified"
- Restriction ReferralDateTime must have a precision to the 'Second'
- Remove space from tele details for a ElectronicCommunicationDetail
- Added helper function to allow the addition of EI (Employee Number) as an EntityIdentifier
- Include integrity check for logo and 1A attachments

3.0.3 - Compliant with CCA Validator version 1.12.2
----------------------------------------------------
- Updated section display name from 'Diagnostic Test Result’ to 'Diagnostic Investigation'
- Allow multiple 'Diagnostic Investigation' sections

3.0.2 - Compliant with CCA Validator version 1.12.2
----------------------------------------------------
- Added section Diagnostic Test Result to Diagnostic Investigations
- Updated the narrative exclusion statements to appear on one line

3.0.1 - Compliant with CCA Validator version 1.12.2
----------------------------------------------------
- Update the validation message for the object IAdverseReactionsWithoutExclusions where no AdverseReactions exists
- Fixed the Nominated Contact Provider so that person names appear when no address or identifiers are specified 

3.0.0 - Compliant with CCA Validator version 1.12.2
----------------------------------------------------
- Changed narrative Imaging Examination Result Details for Diagnostic Investigations 
- Allow NullFlavours in Discharge Mode of Separation 
- Removed brackets from the Date Time narrative display to be compliant with the rendering specification
- Updated the Discharge Summary disposition code display name to be compliment with the IG 
- Added the ability to add a custom narrative for every section using the 'CustomNarrative' variable
- Removed NarrativeHL7Text because this is overridden by the 'CustomNarrative' 
- Fixed the duplication of the ‘Recipient Name’ in the Narrative
- Removed the conformance Level 1a, 1b and 2 level documents for the consumer because the minimum conformance level for consumer documents is 3A
- Refactored the Time narrative attribute and removed ISO8601DateTimeFormatter to be constant with how are narrative items are generated 
- Allow the ‘Unit of Age’ to be specified for the subject of cares ‘Age’ 
- Merged enums DocumentTemplateType & CDADocumentType because they contained the same values
- Removed DocumentType cdaContext from the each CDA document because the variable was never used in the generator
- Changed the type of setId to Identifier to allow additional elements
- Added DocumentId to cdaContext as an Identifier


2.0.8 - Compliant with CCA Validator version 1.12
--------------------------------------------------
- Update narrative structure for Record of 'Recommendations and Information Provided' &  'Arranged Services '


2.0.7 - Compliant with CCA Validator version 1.12
--------------------------------------------------
- Update for PathologyTestResult model to allow for XPre narrative.
- Added support for nullFlavors for various elements.
+ Support for consumer documents:
  + Consumer Entered Notes
  + Consumer Entered Health Summary
  + Advanced Care Directive custodian Record
- Administrative observations has been moved to the bottom of the CDA document.
- Various updates for compliance with CDA validator version 1.12.


2.0.6 - Compliant with CCA Validator version 1.11
--------------------------------------------------
- Updated the CDA sample code to generate 1a, 1b, 2, 3A min/max conformance documents for each document type.


2.0.5 - Compliant with CCA Validator version 1.10
--------------------------------------------------
- Updated CDA identifiers to enable a root (valid uid or oid) and extension attribute. 
- Updated Coadable text validation to only allow a displayName or code if a codesystem exists.
- Updated 'Result Group Specimen Detail' & 'Test Specimen Detail' code for Specialist Letter to show the correct code.
- Updated the library to show comments at the top of the CDA documents to show the current version and compliant CDA Validator version.
- Added an attribute to allow a XSL Stylesheet to be included in the App.config. This field is empty by default.
- Updated sample code to be compliant to level 3B for the CDA libraries.
- Updated Gender display names.
- Removed validation for HPIO in identifier lists.


2.0.4
-----
- Updated the table view of the Requested Services in the Narrative.


2.0.3
-----
- Updated ISO8601DateTime so that any time that is more specific than a day SHALL include a timezone.
- Removed the duplication of Requested Services in the Narrative.


2.0.2
-----
- Introduced a new type for recording date/time and duration that allows a specified level of precision, and also for a setting for a time span
- Fixed NullFlavor='OTH' issue
- Changed identifiers for specimen type from string to a complex Identifier type


2.0.1
-----
- Corrected the header "Reason for Ceasing" to "Reason for Change" in section "Current Medications"
- Corrected duplicated value for "Quantity Supplied" in section "Current Medications"
- Changed the display name of "Snomed CT-AU" to "SNOMED CT-AU"
- Anatomical location image now renders correctly


2.0.0
-----
- Alignment to NCAP version Implementation Guides.

Known issues:
- Date and time is specified using the .NET type DateTime. This introduces a restriction where general date / time (eg: Jan 2008) cannot be set.
- Duration is represented by the "Duration" type in the CDA library, which allows the specification of a start and end date/time. It is currently not
possible to set a time span (eg: 6 weeks).
- NullFlavor='OTH' is set alongside OriginalText if CodeSystem isn't specified for codable text elements.


1.4.2
-----
- Resolved an issue with the narrative generator incorrectly displaying the difference between two dates.
- Added code that allows the re-use objects inherited from a base class in the header.
- Fixed the narrative for empty Adverse Substance Reactions.
- Fixed PCB enum to show No Benefit.


1.4.1
-----
- Resolved a number of issues with CCA Schematron Validator 1.7
- Updated DischargeSummary to allow for ICodableTranslation
- Fixed the E-Referral 'Other Medical History' to reflect the IG e-Referral CDA Implementation Guide 1.1 - 9 Sep 2011
- Updated the codableText for Shared Health Summary to remove the requirement for a mandatory CodeSystem


1.4.0
-----
- Fixed Typo spelling of E-Referral
- Updated legal authenticator to remove mandatory fields
- Resolved a number level 2 conformance for issues with Schematron for Event Summary, E-Referral and Shared Health Summary 


1.3.1
-----
- All CDA documents changed from ClinicalDocument/typeId/@extension="POCD_HD00040" to ClinicalDocument/typeId/@extension="POCD_HD000040"


1.3.0
-----
- Provide the ability hide Administrative Observations in the narrative
- Include the OID’s for Medicare Identifiers 
- Update code to address known CCA schematron issues
- Update to make HPII and IHI optional fields
- Update to make Date of Birth and Gender optional fields in Author
- Fixed the TimeSpan in Specialist Letter Recommendations section to show both dates


1.2.9
-----
- Updated the CDA Schema to 1.2
- Updated to include Prescription Request CDA document
- Updated to include Dispense Record CDA document
- Removed dormant reference to Shared Health Summary Review
- Fixed the spelling of “Diagnoses” throughout the solution
- Correct the narrative for “Response Details”  
- Added a templateID which references the CDA Rendering Guide


1.2.8
-----
- Split the solution into two seperate libaries 
  - V2 ETP solution this generates ETP documents that refer to 'http://ns.electronichealth.net.au/Ci/Cda/Extensions/2.0'
  - V3 CDA solution this generates CDA documents that refer to 'http://ns.electronichealth.net.au/Ci/Cda/Extensions/3.0'
- Updated to reflect changes from eReferral_Change_Document-1889
- Updated to reflect changes from eDischargeSummary_ChangeDocument-20111031
- Updated to reflect changes from Shared_Health_Summary_Change_Document-20111031
- Updated to reflect changes from Specialist_Letter_Change_Document-20111031
- Include Event Summary CDA document
- Include the ability to add a file into the narrative as a link by setting the media type to text
- Minor changes to the narrative


1.2.7
-----
- Allow multiple Medical Record Numbers (MRN) to be added to the asEntityIdentifier field in the record target
- Fixed all bugs relating to null checks
- Fix entitlements national identifiers for HPIO/HPII/IHI accross all CDA documents to use root and assigningAuthorityName
- Add encompassing encounter for Specialist Letter
- Change the template id to '1.2.36.1.2001.1001.101.100.16565' for Shared Health Summary
- Change the template id to '1.2.36.1.2001.1001.101.100.20000' for Discharge Summary
- Change the extension attribute to '1.1' for the Shared Health Summary
- Change the extension attribute to '1.1' for Referral
- Change the extension attribute to '1.1' for Specialist Letter
- Fixed a bug related to creating a DiagnosticInvestigations.PathologyTestResult.SpecimenDetail.HandlingAndProcessing.ReceivedDateTime the null check was incorrect
- Changed output to UTF-8


1.2.5
-----
- Fixed an issue where the narrative generation engine was using the displayName for codable text enetries, rather than utilizing the Orignial Text (if populated)
- Fixed some null references within the narrative generation engine when a minimal structured body is sent to the CDA generator and subsequently the narrative generation engine.


1.2.4
-----
- Fixed entitlement id to be the identifier and not a GUID (this affects all CDA documents)


1.2.6
-----
- Added ePrescription CDA generator
- Removed extensions from entity identifiers
- Updated discharge summary from change log 20110916
- updated shared health summary sample code to resemble PCEHR SHS long scenario


1.2.3
-----
- Added Discharge Summary 
- Added MRN to the CDA 
- Changed ID to GUIDs for all CDA Documents
- Added LOGO image to CDA document


1.2.2
-----
- Added Ms to the list of possible titles that can be associated with a person
- Added the ability to serialize the model
- Added a SerializeModel method to each of the document factories
- Applied XML Comments to Country and Occupation enums, thus removing all warnings from the solution.


1.2.1
-----
- Changed the display name on the Shared Health Summary CDA Doument to "Patient Summary"
- Changed the CDA Context Model to contain a root ID; this is an optional property allow for a root ID to be passed though to the document generator
- Changed the CodableTexty object to allow for a list of translation codes
- Added an StructedXMLBodyFile property, this allows for a document to be added as the body for the CDA document.
- Added an INarrativeGenerator interface, and modified to the section generation methods to accept a the interface, thus providing a means by which to easily change how the narrative for each section is generated.


1.2.0
-----
- Added SpecialistLetter


1.0.0 (Original release)
-----
- SharedHealthSummary
- E-Referal