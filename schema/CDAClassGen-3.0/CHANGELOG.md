### Change Log/Revision History

Changes made to Schemas

---
### 05/07/2017
---

This change is to add missing elements to the Agency Extensions schema

### EXTENSION.xsd


<!-- subjectPerson.ethnicGroupCode -->
<xs:element name="ethnicGroupCode" type="hl7:CE"/>

<!-- subjectPerson.id -->
<xs:element name="id" type="hl7:II"/>

This change is to add missing extensions to several classes.

### POCD_MT000040-AU-V1_0.xsd

Change in <xs:complexType name="POCD_MT000040.Entity">

2 Agency Extensions added to Entity
<!-- Extension: start asQualifiedEntity -->
<xs:element ref="ext:asQualifiedEntity" minOccurs="0" maxOccurs="1"/>
<!-- Extension: end asQualifiedEntity -->
			
<!-- Extensions: start PersonalRelationShip -->
<xs:element ref="ext:personalRelationship" minOccurs="0" maxOccurs="unbounded"></xs:element>
<!-- Extensions: end PersonalRelationShip -->			

Change in <xs:complexType name="POCD_MT000040.PlayingEntity">

1 Agency Extension added to PlayingEntity
<!-- Extensions: start PersonalRelationShip -->
<xs:element ref="ext:personalRelationship" minOccurs="0" maxOccurs="unbounded"></xs:element>
<!-- Extensions: end PersonalRelationShip -->

Change in <xs:complexType name="POCD_MT000040.SubjectPerson">

9 Agency Extension added to SubjectPerson
<!-- Extension: start asEntityIdentifier -->
<xs:element ref="ext:asEntityIdentifier" minOccurs="0" maxOccurs="unbounded" />
<!-- Extension: end asEntityIdentifier -->

<!-- Extension: start id -->
<xs:element ref="ext:id" minOccurs="0" maxOccurs="unbounded" />
<!-- Extension: end id -->

<!-- Extension: start ethnicGroupCode -->
<xs:element ref="ext:ethnicGroupCode" minOccurs="0" maxOccurs="1" />
<!-- Extension: end ethnicGroupCode -->

<!-- Extension: start asEmployment -->
<xs:element ref="ext:asEmployment" minOccurs="0" maxOccurs="1"/>
<!-- Extension: end asEmployment -->

<!-- Extension: start asQualifications -->
<xs:element ref="ext:asQualifications" minOccurs="0" maxOccurs="1"/>
<!-- Extension: end asQualifications -->
			
<!-- Extension: start multipleBirthInd -->
<xs:element ref="ext:multipleBirthInd" minOccurs="0" maxOccurs="1" />
<!-- Extension: end multipleBirthInd -->
			
<!-- Extension: start multipleBirthOrderNumber -->
<xs:element ref="ext:multipleBirthOrderNumber" minOccurs="0" maxOccurs="1" />
<!-- Extension: end multipleBirthOrderNumber -->
			
<!-- Extension: start deceasedTime -->
<xs:element ref="ext:deceasedInd" minOccurs="0" />
<xs:element ref="ext:deceasedTime" minOccurs="0" />
<!-- extension: end -->

Change in <xs:complexType name="POCD_MT000040.Device">

1 Agency Extensions added to Device
<!-- Extension: start asEntityIdentifier -->
<xs:element ref="ext:asEntityIdentifier" minOccurs="0" maxOccurs="unbounded" />
<!-- Extension: end asEntityIdentifier -->

---
### 02/02/2016
---

This change is to add element: 'templateId' to 18 classes in the Australian Extensions schema.

### EXTENTION.xsd

Changes in:
 <xs:complexType name="EXTENSION.Employment">
 <xs:complexType name="EXTENSION.Qualifications">
 <xs:complexType name="EXTENSION.Coverage2">
 <xs:complexType name="EXTENSION.Entitlement">
 <xs:complexType name="EXTENSION.Participant">
 <xs:complexType name="EXTENSION.ParticipantRole">
 <xs:complexType name="EXTENSION.EntityIdentifier">
 <xs:complexType name="EXTENSION.PersonalRelationship">
 <xs:complexType name="EXTENSION.GeographicArea">
 <xs:complexType name="EXTENSION.SpecimenInContainer">
 <xs:complexType name="EXTENSION.Container">
 <xs:complexType name="EXTENSION.FieldOfPractice">
 <xs:complexType name="EXTENSION.Subject1">
 <xs:complexType name="EXTENSION.Policy">
 <xs:complexType name="EXTENSION.Subject2">
 <xs:complexType name="EXTENSION.SubstitutionPermission">
 <xs:complexType name="EXTENSION.Coverage">
 <xs:complexType name="EXTENSION.PolicyOrAccount">

Element added:
<!-- Extension: start (as per above list) -->
<xs:element name="templateId" type="II" minOccurs="0" maxOccurs="unbounded"/>
<!-- Extension: end (as per above list) -->


---
### 31/08/2015
---

This change is to correct the naming of Qualifications for a PlayingEntity. The element was previously named ext:asQualifications
and has been changed to: ext:asQualifiedEntity

### POCD_MT000040-AU-V1_0.xsd

Change in <xs:complexType name="POCD_MT000040.PlayingEntity">

Name of one entity changed
<!-- Extension: start asQualifiedEntity -->
<xs:element ref="ext:asQualifiedEntity" minOccurs="0" maxOccurs="1"/>
<!-- Extension: end asQualifiedEntity -->
			

---
### 12/06/2015
---

This change is to make use of a Participant at the Clinical Statement level in order to show
that Participant's Employment and Qualifications.

### POCD_MT000040-AU-V1_0.xsd

Change in <xs:complexType name="POCD_MT000040.PlayingEntity">

2 Nehta Extensions added to PlayingEntity
<!-- Extension: start asEmployment -->
<xs:element ref="ext:asEmployment" minOccurs="0" maxOccurs="1"/>
<!-- Extension: end asEmployment -->
			
<!-- Extension: start asQualifications -->
<xs:element ref="ext:asQualifications" minOccurs="0" maxOccurs="1"/>
<!-- Extension: end asQualifications -->
			

---
### 19/02/2014
---
This change is to fix an element namespace from cda to ext (was put in by error)


### POCD_MT000040-AU-V1_0.xsd

Change from:

<!-- Extension: start asOrganizationPartOf -->
<xs:element name="asOrganizationPartOf" type="ext:OrganizationPartOf" minOccurs="0"/>
<!-- Extension: end asOrganizationPartOf -->

to:

<!-- Extension: start asOrganizationPartOf -->
<xs:element ref="ext:asOrganizationPartOf" minOccurs="0" maxOccurs="unbounded"/>
<!-- Extension: end asOrganizationPartOf -->



---
### 16/07/2012
---
This change is to support an addition description field for ETP Prescription


### CDA-AU-V1_0.xsd

1) added version="20120716" into namespace


### EXTENSION.xsd

1) Added new element <xs:element name="desc" type="hl7:ST"/>


### POCD_MT000040-AU-V1_0.xsd


<xs:complexType name="POCD_MT000040.Material">
added new element
	<xs:element ref="ext:desc" minOccurs="0" maxOccurs="1"/>



---
### 30/05/2012
---

These changes are to support the ‘Antigen Description’ field for Medicare ACIR specification.

### POCD_MT000040-AU-V1_0.xsd

Updated the relative path for ‘datatypes-V3_0.xsd’, ‘voc-V3_0.xsd’ and ‘NarrativeBlock.xsd’ xs:include statements.

### EXTENSION.xsd


1) Updated the relative path for ‘datatypes-V3_0.xsd’, ‘voc-V3_0.xsd’ and ‘datatypes-base-V3_0.xsd’ xs:include statements.
2) Updated <xs:complexType name="Ingredient">

i)  Removed <code> and <effectiveTime> elements from the ‘Ingredient’ class and put into new <ingredientManufacturedMaterial>.
ii) Added <ingredientManufacturedMaterial> element to the ‘Ingredient’ class.

3) Added ManufacturedMaterial Entity class as a complexType.

<xs:complexType name="ManufacturedMaterial">
…
</xs:complexType>



---
### 27/04/2012
---

These changes are to support the Consumer Entered Notes and ETP CDA IGs.

### POCD_MT000040-AU-V1_0.xsd


<xs:complexType name="POCD_MT000040.Material">
added new element
	<xs:element ref="ext:formCode" minOccurs="0" maxOccurs="1"/>

<xs:complexType name="POCD_MT000040.Patient">
added new element
	<xs:element ref="ext:personalRelationship" minOccurs="0" maxOccurs="unbounded"></xs:element>

<xs:complexType name="POCD_MT000040.Person">
added new element
	<xs:element ref="ext:personalRelationship" minOccurs="0" maxOccurs="unbounded"></xs:element>

### EXTENSION.xsd

Renamed EXTENSION-V3_0.xsd to EXTENSION.xsd
and Added these elements and complex type

<xs:element name="personalRelationship" type="ext:PersonalRelationship"/>

<xs:element name="formCode" type="hl7:CD"/>

	<xs:complexType name="PersonalRelationship">
		<xs:sequence>			
			<xs:element  name="id"  type="hl7:II"  minOccurs="0"  maxOccurs="1"/>
			<xs:element  name="code"  type="hl7:CD"  minOccurs="1"  maxOccurs="1"/>
			<xs:element  name="addr"  type="hl7:AD"  minOccurs="0"  maxOccurs="1"/>
			<xs:element  name="telecom"  type="hl7:TEL"  minOccurs="0"  maxOccurs="1"/>
			<xs:element  name="statusCode"  type="hl7:CS"  minOccurs="0"  maxOccurs="1"/>
			<xs:element  name="effectiveTime"  type="hl7:IVL_TS"  minOccurs="0"  maxOccurs="1"/>
			<xs:element  name="asPersonalRelationship"  type="hl7:POCD_MT000040.Patient"  nillable="true"  minOccurs="0"  maxOccurs="1"/>
		</xs:sequence>		
		<xs:attribute  name="nullFlavor"  type="hl7:NullFlavor"  use="optional"/>
		<xs:attribute  name="classCode"  type="hl7:RoleClass"  use="optional"  fixed="PRS"/>
		<xs:attribute  name="negationInd"  type="xs:boolean"  use="optional"/>
	</xs:complexType> 


---
### 27/01/2012
---

The IVL_* and URG_PQ datatypes in the datatypes-base-V3_0.xsd and datatypes-V3_0.xsd files
require certain orders to be maintained. When the choices where removed, this fixed the order,
which for certain IG's breaks the schema format. choices put back in for these types.


---
### 15/12/2011
---

### POCD_MT000040-AU-V1_0.xsd

<xs:complexType name="POCD_MT000040.AuthoringDevice">
added new element
	<xs:element ref="ext:asEntityIdentifier" minOccurs="0" maxOccurs="unbounded" />

<xs:complexType name="POCD_MT000040.ManufacturedProduct">
changed <xs:element from maxOccurs="2" to maxOccurs="unbounded"
	<xs:element ref="ext:subjectOf1" minOccurs="0" maxOccurs="unbounded"/>

<xs:complexType name="POCD_MT000040.Supply">
changed <xs:element from maxOccurs="2" to maxOccurs="unbounded"
	<xs:element ref="ext:coverage" minOccurs="0" maxOccurs="unbounded"/>

---
### 09/12/2012
---

### EXTENSION-V3_0.xsd

new elements
	<xs:element name="methodCode" type="hl7:CD"/>
	<xs:element name="asIngredient" type="ext:Ingredient"/>
	<xs:element name="controlAct" type="ext:ControlAct"/>

new complex Types
	<xs:complexType name="Ingredient">
new complex Types
	<xs:complexType name="ControlAct">

<xs:complexType name="Employment">
new element
	<xs:element name="code" type="hl7:CE" minOccurs="0"/>


<xs:complexType name="Qualifications">
removed elements (added in error)
	<xs:element name="effectiveTime" type="hl7:IVL_TS" minOccurs="0"/>
	<xs:element name="jobClassCode" type="hl7:CE" minOccurs="0"/>
	<xs:element name="employerOrganization" type="hl7:POCD_MT000040.Organization" minOccurs="0"/>

<xs:complexType name="Subject2">
changed attribute typeCode from ParticipationTargetSubject to ActRelationshipType and fixed type from SBJ to SUBJ
	<xs:attribute name="typeCode" type="hl7:ActRelationshipType" use="optional" fixed="SUBJ"/>


### POCD_MT000040-AU-V1_0.xsd

<xs:complexType name="POCD_MT000040.Person">
changed minOccurs from 1 to 0
<xs:element ref="ext:asEntityIdentifier" minOccurs="0" maxOccurs="unbounded" />

<xs:complexType name="POCD_MT000040.Patient">
changed minOccurs from 1 to 0
<xs:element ref="ext:asEntityIdentifier" minOccurs="0" maxOccurs="unbounded" />

<xs:complexType name="POCD_MT000040.Entry">
new element
	<xs:element ref="ext:controlAct"/>

<xs:complexType name="POCD_MT000040.EntryRelationship">
new element
	<xs:element ref="ext:controlAct"/>

<xs:complexType name="POCD_MT000040.Material">
new element
	<xs:element ref="ext:asIngredient" minOccurs="0" maxOccurs="unbounded" />

<xs:complexType name="POCD_MT000040.SubstanceAdministration">
new element
	<xs:element ref="ext:methodCode" minOccurs="0" maxOccurs="1"/>


