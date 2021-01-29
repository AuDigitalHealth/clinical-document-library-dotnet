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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Role Class Associative
    /// </summary>
    [Serializable]
    [DataContract]
    public enum RoleClassCodeAssociative
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        [EnumMember]
        Undefined,

        /// <summary>
        /// Access
        /// </summary>
        [EnumMember]
        [Name(Code = "ACCESS", Name = "access", CodeSystem = "RoleClassAssociative")]
        Access,

        /// <summary>
        /// Adjacency
        /// </summary>
        [EnumMember]
        [Name(Code = "ADJY", Name = "adjacency", CodeSystem = "RoleClassAssociative")]
        Adjacency,

        /// <summary>
        /// Administerable Material
        /// </summary>
        [EnumMember]
        [Name(Code = "ADMM", Name = "Administerable Material", CodeSystem = "RoleClassAssociative")]
        AdministerableMaterial,

        /// <summary>
        /// Affiliate
        /// </summary>
        [EnumMember]
        [Name(Code = "AFFL", Name = "Affiliate", CodeSystem = "RoleClassAssociative")]
        Affiliate,

         /// <summary>
        /// Agent
        /// </summary>
        [EnumMember]
        [Name(Code = "AGNT", Name = "agent", CodeSystem = "RoleClassAssociative")]
        Agent,

         /// <summary>
        /// Assigned entity
        /// </summary>
        [EnumMember]
        [Name(Code = "ASSIGNED", Name = "assigned entity", CodeSystem = "RoleClassAssociative")]
        AssignedEntity,
 
        /// <summary>
        /// Birthplace
        /// </summary>
        [EnumMember]
        [Name(Code = "BIRTHPL", Name = "birthplace", CodeSystem = "RoleClassAssociative")]
        Birthplace,

        /// <summary>
        /// Molecular bond
        /// </summary>
        [EnumMember]
        [Name(Code = "BOND", Name = "molecular bond", CodeSystem = "RoleClassAssociative")]
        MolecularBond,

         /// <summary>
        /// Caregiver
        /// </summary>
        [EnumMember]
        [Name(Code = "CAREGIVER", Name = "caregiver", CodeSystem = "RoleClassAssociative")]
        Caregiver,

        /// <summary>
        /// Case Subject
        /// </summary>
        [EnumMember]
        [Name(Code = "CASESBJ", Name = "Case Subject", CodeSystem = "RoleClassAssociative")]
        CaseSubject,

        /// <summary>
        /// Citizen
        /// </summary>
        [EnumMember]
        [Name(Code = "CIT", Name = "citizen", CodeSystem = "RoleClassAssociative")]
        Citizen,

        /// <summary>
        /// Claimant
        /// </summary>
        [EnumMember]
        [Name(Code = "CLAIM", Name = "claimant", CodeSystem = "RoleClassAssociative")]
        Claimant,

        /// <summary>
        /// Commissioning party
        /// </summary>
        [EnumMember]
        [Name(Code = "COMPAR", Name = "commissioning party", CodeSystem = "RoleClassAssociative")]
        CommissioningParty,

        /// <summary>
        /// contact
        /// </summary>
        [EnumMember]
        [Name(Code = "CON", Name = "contact", CodeSystem = "RoleClassAssociative")]
        Contact,

        /// <summary>
        /// Connection
        /// </summary>
        [EnumMember]
        [Name(Code = "CONC", Name = "connection", CodeSystem = "RoleClassAssociative")]
        Connection,

        /// <summary>
        /// Continuity
        /// </summary>
        [EnumMember]
        [Name(Code = "CONY", Name = "continuity", CodeSystem = "RoleClassAssociative")]
        Continuity,
 
        /// <summary>
        /// Covered Party
        /// </summary>
        [EnumMember]
        [Name(Code = "COVPTY", Name = "covered party", CodeSystem = "RoleClassAssociative")]
        CoveredParty,

        /// <summary>
        /// clinical research investigator
        /// </summary>
        [EnumMember]
        [Name(Code = "CRINV", Name = "clinical research investigator", CodeSystem = "RoleClassAssociative")]
        ClinicalResearchInvestigator,

        /// <summary>
        /// Clinical Research Sponsor
        /// </summary>
        [EnumMember]
        [Name(Code = "CRSPNSR", Name = "clinical research sponsor", CodeSystem = "RoleClassAssociative")]
        ClinicalResearchSponsor,

        /// <summary>
        /// Place Of Death
        /// </summary>
        [EnumMember]
        [Name(Code = "DEATHPLC", Name = "place of death", CodeSystem = "RoleClassAssociative")]
        PlaceOfDeath,

        /// <summary>
        /// Dependent
        /// </summary>
        [EnumMember]
        [Name(Code = "DEPEN", Name = "dependent", CodeSystem = "RoleClassAssociative")]
        Dependent,

        /// <summary>
        /// Dedicated Service Delivery Location
        /// </summary>
        [EnumMember]
        [Name(Code = "DSDLOC", Name = "dedicated service delivery location", CodeSystem = "RoleClassAssociative")]
        DedicatedServiceDeliveryLocation,

        /// <summary>
        /// Distributed Material
        /// </summary>
        [EnumMember]
        [Name(Code = "DST", Name = "distributed material", CodeSystem = "RoleClassAssociative")]
        DistributedMaterial,

        /// <summary>
        /// Emergency Contact
        /// </summary>
        [EnumMember]
        [Name(Code = "ECON", Name = "emergency contact", CodeSystem = "RoleClassAssociative")]
        EmergencyContact,

        /// <summary>
        /// Employee
        /// </summary>
        [EnumMember]
        [Name(Code = "EMP", Name = "employee", CodeSystem = "RoleClassAssociative")]
        Employee,

        /// <summary>
        /// event location
        /// </summary>
        [EnumMember]
        [Name(Code = "EXLOC", Name = "event location", CodeSystem = "RoleClassAssociative")]
        EventLocation,

        /// <summary>
        /// exposed entity
        /// </summary>
        [EnumMember]
        [Name(Code = "EXPR", Name = "exposed entity", CodeSystem = "RoleClassAssociative")]
        ExposedEntity,

        /// <summary>
        /// guarantor
        /// </summary>
        [EnumMember]
        [Name(Code = "GUAR", Name = "guarantor", CodeSystem = "RoleClassAssociative")]
        Guarantor,

        /// <summary>
        /// Guardian
        /// </summary>
        [EnumMember]
        [Name(Code = "GUARD", Name = "guardian", CodeSystem = "RoleClassAssociative")]
        Guardian,

        /// <summary>
        /// held entity
        /// </summary>
        [EnumMember]
        [Name(Code = "HLD", Name = "held entity", CodeSystem = "RoleClassAssociative")]
        HeldEntity,

        /// <summary>
        /// Health Chart
        /// </summary>
        [EnumMember]
        [Name(Code = "HLTHCHRT", Name = "health chart", CodeSystem = "RoleClassAssociative")]
        HealthChart,

        /// <summary>
        /// Identified Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "IDENT", Name = "identified entity", CodeSystem = "RoleClassAssociative")]
        IdentifiedEntity,

        /// <summary>
        /// Individual
        /// </summary>
        [EnumMember]
        [Name(Code = "INDIV", Name = "individual", CodeSystem = "RoleClassAssociative")]
        Individual,

        /// <summary>
        /// Investigation Subject
        /// </summary>
        [EnumMember]
        [Name(Code = "INVSBJ", Name = "Investigation Subject", CodeSystem = "RoleClassAssociative")]
        InvestigationSubject,
  
        /// <summary>
        /// Incidental Service Delivery Location
        /// </summary>
        [EnumMember]
        [Name(Code = "ISDLOC", Name = "incidental service delivery location", CodeSystem = "RoleClassAssociative")]
        IncidentalServiceDeliveryLocation,

        /// <summary>
        /// Licensed Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "LIC", Name = "licensed entity", CodeSystem = "RoleClassAssociative")]
        LicensedEntity,

        /// <summary>
        /// Manufactured Product
        /// </summary>
        [EnumMember]
        [Name(Code = "MANU", Name = "manufactured product", CodeSystem = "RoleClassAssociative")]
        ManufacturedProduct,

        /// <summary>
        /// Military Person
        /// </summary>
        [EnumMember]
        [Name(Code = "MIL", Name = "military person", CodeSystem = "RoleClassAssociative")]
        MilitaryPerson,

        /// <summary>
        /// Maintained Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "MNT", Name = "maintained entity", CodeSystem = "RoleClassAssociative")]
        MaintainedEntity,

        /// <summary>
        /// Named Insured
        /// </summary>
        [EnumMember]
        [Name(Code = "NAMED", Name = "named insured", CodeSystem = "RoleClassAssociative")]
        NamedInsured,

        /// <summary>
        /// Next Of Kin
        /// </summary>
        [EnumMember]
        [Name(Code = "NOK", Name = "next of kin", CodeSystem = "RoleClassAssociative")]
        NextOfKin,

        /// <summary>
        /// Notary Public
        /// </summary>
        [EnumMember]
        [Name(Code = "NOT", Name = "notary public", CodeSystem = "RoleClassAssociative")]
        NotaryPublic,

        /// <summary>
        /// Owned Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "OWN", Name = "owned entity", CodeSystem = "RoleClassAssociative")]
        OwnedEntity,

        /// <summary>
        /// Patient
        /// </summary>
        [EnumMember]
        [Name(Code = "PAT", Name = "patient", CodeSystem = "RoleClassAssociative")]
        Patient,

        /// <summary>
        /// Payee
        /// </summary>
        [EnumMember]
        [Name(Code = "PAYEE", Name = "payee", CodeSystem = "RoleClassAssociative")]
        Payee,

        /// <summary>
        /// Invoice Payor
        /// </summary>
        [EnumMember]
        [Name(Code = "PAYOR", Name = "invoice payor", CodeSystem = "RoleClassAssociative")]
        InvoicePayor,

        /// <summary>
        /// PolicyHolder
        /// </summary>
        [EnumMember]
        [Name(Code = "POLHOLD", Name = "policy holder", CodeSystem = "RoleClassAssociative")]
        PolicyHolder,

        /// <summary>
        /// Program Eligible
        /// </summary>
        [EnumMember]
        [Name(Code = "PROG", Name = "program eligible", CodeSystem = "RoleClassAssociative")]
        ProgramEligible,

        /// <summary>
        /// Healthcare Provider
        /// </summary>
        [EnumMember]
        [Name(Code = "PROV", Name = "healthcare provider", CodeSystem = "RoleClassAssociative")]
        HealthcareProvider,

        /// <summary>
        /// Personal Relationship
        /// </summary>
        [EnumMember]
        [Name(Code = "PRS", Name = "personal relationship", CodeSystem = "RoleClassAssociative")]
        PersonalRelationship,

        /// <summary>
        /// Qualified Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "QUAL", Name = "qualified entity", CodeSystem = "RoleClassAssociative")]
        QualifiedEntity,

        /// <summary>
        /// Research Subject
        /// </summary>
        [EnumMember]
        [Name(Code = "RESBJ", Name = "research subject", CodeSystem = "RoleClassAssociative")]
        ResearchSubject,

        /// <summary>
        /// Retailed Material
        /// </summary>
        [EnumMember]
        [Name(Code = "RET", Name = "retailed material", CodeSystem = "RoleClassAssociative")]
        RetailedMaterial,

        /// <summary>
        /// regulated product
        /// </summary>
        [EnumMember]
        [Name(Code = "RGPR", Name = "regulated product", CodeSystem = "RoleClassAssociative")]
        RegulatedProduct,

        /// <summary>
        /// Service Delivery Location
        /// </summary>
        [EnumMember]
        [Name(Code = "SDLOC", Name = "service delivery location", CodeSystem = "RoleClassAssociative")]
        ServiceDeliveryLocation,

        /// <summary>
        /// Signing Authority Or Officer
        /// </summary>
        [EnumMember]
        [Name(Code = "SGNOFF", Name = "signing authority or officer", CodeSystem = "RoleClassAssociative")]
        SigningAuthorityOrOfficer,

        /// <summary>
        /// Coverage Sponsor
        /// </summary>
        [EnumMember]
        [Name(Code = "SPNSR", Name = "coverage sponsor", CodeSystem = "RoleClassAssociative")]
        CoverageSponsor,

        /// <summary>
        /// Student
        /// </summary>
        [EnumMember]
        [Name(Code = "STD", Name = "student", CodeSystem = "RoleClassAssociative")]
        Student,

        /// <summary>
        /// Subscriber
        /// </summary>
        [EnumMember]
        [Name(Code = "SUBSCR", Name = "subscriber", CodeSystem = "RoleClassAssociative")]
        Subscriber,

        /// <summary>
        /// Territory Of Authority
        /// </summary>
        [EnumMember]
        [Name(Code = "TERR", Name = "territory of authority", CodeSystem = "RoleClassAssociative")]
        TerritoryOfAuthority,

        /// <summary>
        /// Therapeutic Agent
        /// </summary>
        [EnumMember]
        [Name(Code = "THER", Name = "therapeutic agent", CodeSystem = "RoleClassAssociative")]
        TherapeuticAgent,

        /// <summary>
        /// Underwriter
        /// </summary>
        [EnumMember]
        [Name(Code = "UNDWRT", Name = "underwriter", CodeSystem = "RoleClassAssociative")]
        Underwriter,

        /// <summary>
        /// Used Entity
        /// </summary>
        [EnumMember]
        [Name(Code = "USED", Name = "used entity", CodeSystem = "RoleClassAssociative")]
        UsedEntity,

        /// <summary>
        /// Warranted Product
        /// </summary>
        [EnumMember]
        [Name(Code = "WRTE", Name = "warranted product", CodeSystem = "RoleClassAssociative")]
        WarrantedProduct,
    }
}
