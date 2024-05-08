# Introduction
To ensure a consistent and structured approach for providing and sharing clinical information, the My Health Record system accepts only documents of supported HL7™ CDA™ document types.

The Clinical Document Library provides sample code for developers of clinical information systems to generate clinical documents for document types supported by the My Health Record system. The library provides developers with an easy-to-use interface for the generation of clinical documents.

It is important to note that use of the library does not ensure conformity of the generated documents to their respective document type specifications. Developers need to perform comprehensive conformance assessments of software using the Clinical Document Library before declaring its conformity to the My Health Record System Operator.

The Agency publishes the Clinical Package Validator, which can help reduce developers’ overall effort for conformance assessments by automating the execution of a substantial number of conformance checks for clinical documents. However, these checks are not comprehensive and hence do not ensure conformity by themselves.
Conformity to the requirements for a particular clinical document type always needs to be declared for a particular version of the conformance profile for the document type. The Agency’s Template Package Directory provides a mapping of conformance profile versions to template packages that can be used with the Validator to perform these conformance checks.

# Content
The Clinical Document Library product consists of two main parts:
- The document generator library;
- Sample code for the utilisation of the library.
The sample code is aimed at demonstrating how to instantiate each of the library features, including the generation of minimum and maximum document instances. A minimum document instance contains all mandatory but none of the optional fields. A maximum document instance contains all mandatory and all optional fields.

This is a .Net software library that aims to make the generation of Clinical Document Architecture (CDA) documents easy and encapsulates validation logic to ensure that the result document conforms to the standards outlines by CDA and HL7 v3.

This software library contains CDA document Implementation objects (Eg. SharedHealthSummary, EReferral etc) these objects wrap up three distinct models. Each model encapsulates either CDA (Clinical Document Architecture) or SDT (Structured Document Template) logic. 

The three models can be loosely described as two context models and one content model. Each of the models is then wrapped within the CDA document Implementation object

	The three CDA / SDT models are:
	1 - CDA Context
	2 - SDT Context
	3 - SDT Content

The CDA document Implementation objects are designed to wrap each of the above models and to expose methods that help with object instantiation and subsequent hydration.

Where possible, it is desirable to use the CDA document Implementation objects to instantiate child objects associated with each implementation; as this ensures the object you receive conform to the correct interface / implementation, while also ensuring that the resulting CDA document model is valid.

# Supported Documents
- Discharge Summary - Template Package .28(1A) .29(1B) .30(2) .31(3A) .32(3B) .33(1A) .34(1B) .35(2) .36(3A) .37(3B)
- e-Referral - Template Package .13(1A) .14(1B) .15(2) .16(3A) .17(3B) .18(1A) .19(1B) .20(2) .21(3A) .22(3B)
- Event Summary - Template Package .12(3A) .13(3B) .14(3A) .15(3B) .16(3A) .17(3B) .18(3A) .19(3B)
- Shared Health Summary - Template Package .9(3A) .9(3B) .10(3A) .11(3B)
- Specialist Letter - Template Package .23(1A) .24(1B) .25(2) .26(3A) .27(3B) .28(1A) .29(1B) .30(2) .31(3A) .32(3B) .33(1A) .34(1B) .35(2) .36(3A) .37(3B)
- Pathology Result Report - Template Package .1(3A) .2(3A) .3(3A) .4(3A) .5(3A) .6(3A)
- PCEHR Diagnostic Imaging - Template Package .1(3A) .2(3A) .3(3A) .4(3A)
- eHealth Dispense Record - Template Package .4(3A) .5(3A)
- eHealth Prescription Record - Template Package .4(3A) .5(3A)
- Advance Care Information - Template Package .4(3A) .4(3A)
- Goals of Care - Template Package .1(3A) .2(3A)
- Pharmicist Shared Medicines List - Template Package .1(1A) .2(1A) .2(3A)
- Residentail Care Health Summary - Template Package .1(1A) .2(1A) 
- Residentail Care Medication Chart - Template Package .1(1A) .2(1A) 
- Residentail Care Transfer Reason - Template Package .1(1B) .2(1B) 

# Setup
- To build the distributable package, Visual Studio must be installed.
- Start up CDA.sln.

# Solution
The solution consists of three projects:

CDA:
The 'Nehta.VendorLibrary.CDA.Sample' project contains sample code for the CDA library, and is designed as an 
introduction to CDA and the NEHTA CDA libraries

The 'Nehta.VendorLibrary.CDA' project contains the CDA and SDT (Structured Document Template) models for each of 
the CDA documents. It also contains a CDA Generator that will convert each model into an valid CDA Document (XMLDocument)

	The library consists of the following distinct classes for use in generating a CDA document:     
	1. composition / factory classes
		- SharedHealthSummary
		- EReferral
		- SpecialistLetter
		- EventSummary
		- EDischargeSummary
		- plus other CDA document types

	2. CDAGenerator
		- this accepts any of the above CDA document Implementation objects and returns a CDA XML document
		
CDA.Sample: Sample code for the CDA library.

Common: The 'Nehta.VendorLibrary.Common' project contains helper libraries common across all NEHTA vendor library components.
 
# Building and using the library
The solution can be built using 'F6'. 

# CDA Logo usage
Each model allows for the inclusion of a logo within the CDA document. To include the logo you need to set the IncludeLogo property on the model and you also need to include a png file with the name "logo.png". 
The default NEHTA style sheet will look for this logo in the same directory as the CDA document.

# Library Usage
Documentation can be found in the sample project.

# Licensing
See [LICENSE](LICENSE.txt) file.