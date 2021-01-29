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
    /// Role Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum RelationshipRoleType
    {
        /// <summary>
        /// Aunt
        /// The player of the role is a sister of the scoping person's mother or father.
        /// </summary>
        [EnumMember]
        [Name(Code = "AUNT", Name = "Aunt")]
        Aunt,

        /// <summary>
        /// Brother
        /// The player of the role is a male sharing one or both parents in common with the scoping entity.
        /// </summary>
        [EnumMember]
        [Name(Code = "BRO", Name = "Brother")]
        Brother,

        /// <summary>
        /// Brother-In-Law
        /// The player of the role is: (1) a brother of the scoping person's spouse, or (2) the husband of the scoping person's sister, or (3) the husband of a sister of the scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "BROINLAW", Name = "Brother-In-Law")]
        BrotherInLaw,

        /// <summary>
        /// Adopted Child
        /// The player of the role is a child taken into a family through legal means and raised by the scoping person (parent) as his or her own child.
        /// </summary>
        [EnumMember]
        [Name(Code = "CHLDADOPT", Name = "Adopted Child")]
        AdoptedChild,

        /// <summary>
        /// Foster Child
        /// The player of the role is a child receiving parental care and nurture from the scoping person (parent) but not related to him or her through legal or blood ties.
        /// </summary>
        [EnumMember]
        [Name(Code = "CHLDFOST", Name = "Foster Child")]
        FosterChild,

        /// <summary>
        /// Child In-Law
        /// The player of the role is the spouse of scoping person's child.
        /// </summary>
        [EnumMember]
        [Name(Code = "CHLDINLAW", Name = "Child In-Law")]
        ChildInLaw,

        /// <summary>
        /// Cousin
        /// The player of the role is a relative of the scoping person descended from a common ancestor, such as a 	grandparent, by two or more steps in a diverging line.
        /// </summary>
        [EnumMember]
        [Name(Code = "COUSN", Name = "Cousin")]
        Cousin,

        /// <summary>
        /// Natural Daughter
        /// The player of the role is a female offspring of the scoping entity (parent).
        /// </summary>
        [EnumMember]
        [Name(Code = "DAU", Name = "Natural Daughter")]
        NaturalDaughter,

        /// <summary>
        /// Adopted Daughter
        /// The player of the role is a female child taken into a family through legal means and raised by the scoping person (parent) as his or her own child.
        /// </summary>
        [EnumMember]
        [Name(Code = "DAUADOPT", Name = "Adopted Daughter")]
        AdoptedDaughter,

        /// <summary>
        /// Daughter
        /// New Concept -  The player of the role is a female child (of any type) of scoping entity (parent).
        /// </summary>
        [EnumMember]
        [Name(Code = "DAUC", Name = "Daughter")]
        Daughter,

        /// <summary>
        /// Foster Daughter
        /// The player of the role is a female child receiving parental care and nurture from the scoping person (parent) but not related to him or her through legal or blood ties.
        /// </summary>
        [EnumMember]
        [Name(Code = "DAUFOST", Name = "Foster Daughter")]
        FosterDaughter,

        /// <summary>
        /// Daughter In-Law
        /// The player of the role is the wife of scoping person's son.
        /// </summary>
        [EnumMember]
        [Name(Code = "DAUINLAW", Name = "Daughter In-Law")]
        DaughterInLaw,

        /// <summary>
        /// Emergency Contact
        /// A contact designated for contact in emergent situations.
        /// </summary>
        [EnumMember]
        [Name(Code = "ECON", Name = "Emergency Contact")]
        EmergencyContact,

        /// <summary>
        /// Family Member
        /// A relationship between two people characterizing their "familial" relationship
        /// </summary>
        [EnumMember]
        [Name(Code = "FAMMEMB", Name = "Family Member")]
        FamilyMember,

        /// <summary>
        /// Child
        /// The player of the role is a child of the scoping entity.
        /// </summary>
        [EnumMember]
        [Name(Code = "CHILD", Name = "Child")]
        Child,

        /// <summary>
        /// Extended Family Member
        /// A family member outside the immediate family unit, not having a direct genetic or legal relationship e.g. Aunt, cousin, great grandparent, grandchild, grandparent, neiceNephew, and uncle
        /// </summary>
        [EnumMember]
        [Name(Code = "EXT", Name = "Extended Family Member")]
        ExtendedFamilyMember,

        /// <summary>
        /// Immediate Family
        /// A member of the immediate family unit by direct genetic or legal relationship
        /// </summary>
        [EnumMember]
        [Name(Code = "IMED", Name = "Immediate Family")]
        ImmediateFamily,

        /// <summary>
        /// Parent
        /// one that begets or brings forth offspring or a person who brings up and cares for another (Webster's Collegiate Dictionary)
        /// </summary>
        [EnumMember]
        [Name(Code = "PRN", Name = "Parent")]
        Parent,

        /// <summary>
        /// Sibling
        /// One person who shares a parent or parents with another.
        /// </summary>
        [EnumMember]
        [Name(Code = "SIB", Name = "Sibling")]
        Sibling,

        /// <summary>
        /// Significant Other
        /// A person who is important to one's well being; especially a spouse or one in a similar relationship. (The player is the one who is important)
        /// </summary>
        [EnumMember]
        [Name(Code = "SIGOTHR", Name = "Significant Other")]
        SignificantOther,

        /// <summary>
        /// Domestic Partner
        /// The player of the role cohabits with the scoping person but is not the scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "DOMPART", Name = "Domestic Partner")]
        DomesticPartner,

        /// <summary>
        /// Spouse
        /// The player of the role is a marriage partner of the scoping person.
        /// </summary>
        [EnumMember]
        [Name(Code = "SPS", Name = "Spouse")]
        Spouse,

        /// <summary>
        /// Unrelated Friend
        /// The player of the role is a person who is known, liked, and trusted by the scoping person.
        /// </summary>
        [EnumMember]
        [Name(Code = "FRND", Name = "Unrelated Friend")]
        UnrelatedFriend,

        /// <summary>
        /// Father
        /// The player of the role is a male who begets or raises or nurtures the scoping entity (child).
        /// </summary>
        [EnumMember]
        [Name(Code = "FTH", Name = "Father")]
        Father,

        /// <summary>
        /// Father-In-Law
        /// The player of the role is the father of the scoping person's husband or wife.
        /// </summary>
        [EnumMember]
        [Name(Code = "FTHINLAW", Name = "Father-In-Law")]
        FatherInLaw,

        /// <summary>
        /// Great Grandfather
        /// The player of the role is the father of the scoping person's grandparent.
        /// </summary>
        [EnumMember]
        [Name(Code = "GGRFTH", Name = "Great Grandfather")]
        GreatGrandfather,

        /// <summary>
        /// Great Grandmother
        /// The player of the role is the mother of the scoping person's grandparent.
        /// </summary>
        [EnumMember]
        [Name(Code = "GGRMTH", Name = "Great Grandmother")]
        GreatGrandmother,

        /// <summary>
        /// Great Grandparent
        /// The player of the role is a parent of the scoping person's grandparent.
        /// </summary>
        [EnumMember]
        [Name(Code = "GGRPRN", Name = "Great Grandparent")]
        GreatGrandparent,

        /// <summary>
        /// Grandfather
        /// The player of the role is the father of the scoping person's mother or father.
        /// </summary>
        [EnumMember]
        [Name(Code = "GRFTH", Name = "Grandfather")]
        Grandfather,

        /// <summary>
        /// Grandmother
        /// The player of the role is the mother of the scoping person's mother or father.
        /// </summary>
        [EnumMember]
        [Name(Code = "GRMTH", Name = "Grandmother")]
        Grandmother,

        /// <summary>
        /// Grandchild
        /// The player of the role is a child of the scoping person's son or daughter.
        /// </summary>
        [EnumMember]
        [Name(Code = "GRNDCHILD", Name = "Grandchild")]
        Grandchild,

        /// <summary>
        /// Granddaughter
        /// The player of the role is a daughter of the scoping person's son or daughter.
        /// </summary>
        [EnumMember]
        [Name(Code = "GRNDDAU", Name = "Granddaughter")]
        Granddaughter,

        /// <summary>
        /// Grandson
        /// The player of the role is a son of the scoping person's son or daughter.
        /// </summary>
        [EnumMember]
        [Name(Code = "GRNDSON", Name = "Grandson")]
        Grandson,

        /// <summary>
        /// Grandparent
        /// parent of a parent of the subject
        /// </summary>
        [EnumMember]
        [Name(Code = "GRPRN", Name = "Grandparent")]
        Grandparent,

        /// <summary>
        /// Guardian
        /// New Concept -  A person recognized by law to make all decisions on behalf of a minor or a person designated as being incompetent to make decisions on their own behalf. NB: GUARD exists in the RoleClass hierarchy but not the RoleCode hierarchy
        /// </summary>
        [EnumMember]
        [Name(Code = "GUARD", Name = "Guardian")]
        Guardian,

        /// <summary>
        /// Half-Brother
        /// The player of the role is a male related to the scoping entity by sharing only one biological parent.
        /// </summary>
        [EnumMember]
        [Name(Code = "HBRO", Name = "Half-Brother")]
        HalfBrother,

        /// <summary>
        /// Half-Sibling
        /// The player of the role is related to the scoping entity by sharing only one biological parent.
        /// </summary>
        [EnumMember]
        [Name(Code = "HSIB", Name = "Half-Sibling")]
        HalfSibling,

        /// <summary>
        /// Half-Sister
        /// The player of the role is a female related to the scoping entity by sharing only one biological parent.
        /// </summary>
        [EnumMember]
        [Name(Code = "HSIS", Name = "Half-Sister")]
        HalfSister,

        /// <summary>
        /// Husband
        /// The player of the role is a man joined to a woman (scoping person) in marriage.
        /// </summary>
        [EnumMember]
        [Name(Code = "HUSB", Name = "Husband")]
        Husband,

        /// <summary>
        /// Mother
        /// The player of the role is a female who conceives, gives birth to, or raises and nurtures the scoping entity (child).
        /// </summary>
        [EnumMember]
        [Name(Code = "MTH", Name = "Mother")]
        Mother,

        /// <summary>
        /// Mother-In-Law
        /// The player of the role is the mother of the scoping person's husband or wife.
        /// </summary>
        [EnumMember]
        [Name(Code = "MTHINLAW", Name = "Mother-In-Law")]
        MotherInLaw,

        /// <summary>
        /// Neighbor
        /// The player of the role lives near or next to the 	scoping person.
        /// </summary>
        [EnumMember]
        [Name(Code = "NBOR", Name = "Neighbor")]
        Neighbor,

        /// <summary>
        /// Natural Brother
        /// The player of the role is a male having the same biological parents as the scoping entity.
        /// </summary>
        [EnumMember]
        [Name(Code = "NBRO", Name = "Natural Brother")]
        NaturalBrother,

        /// <summary>
        /// Natural Child
        /// A child as determined by birth.
        /// </summary>
        [EnumMember]
        [Name(Code = "NCHILD", Name = "Natural Child")]
        NaturalChild,

        /// <summary>
        /// Nephew
        /// The player of the role is a son of the scoping person's brother or sister or of the brother or sister of the 	scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "NEPHEW", Name = "Nephew")]
        Nephew,

        /// <summary>
        /// Natural Father
        /// The player of the role is a male who begets the scoping entity (child).
        /// </summary>
        [EnumMember]
        [Name(Code = "NFTH", Name = "Natural Father")]
        NaturalFather,

        /// <summary>
        /// Natural Father Of Fetus
        /// Indicates the biologic male parent of a fetus.
        /// </summary>
        [EnumMember]
        [Name(Code = "NFTHF", Name = "Natural Father Of Fetus")]
        NaturalFatherOfFetus,

        /// <summary>
        /// Niece
        /// The player of the role is a daughter of the scoping person's brother or sister or of the brother or sister of the 	scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "NIECE", Name = "Niece")]
        Niece,

        /// <summary>
        /// Niece/Nephew
        /// The player of the role is a child of scoping person's brother or sister or of the brother or sister of the 	scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "NIENEPH", Name = "Niece/Nephew")]
        NieceNephew,

        /// <summary>
        /// Natural Mother
        /// The player of the role is a female who conceives or gives birth to the scoping entity (child).
        /// </summary>
        [EnumMember]
        [Name(Code = "NMTH", Name = "Natural Mother")]
        NaturalMother,

        /// <summary>
        /// Next Of Kin
        /// Played by an individual who is designated as the next of kin for another individual which scopes the role.
        /// </summary>
        [EnumMember]
        [Name(Code = "NOK", Name = "Next Of Kin")]
        NextOfKin,

        /// <summary>
        /// Natural Parent
        /// 
        /// </summary>
        [EnumMember]
        [Name(Code = "NPRN", Name = "Natural Parent")]
        NaturalParent,

        /// <summary>
        /// Natural Sibling
        /// The player of the role has both biological parents in common with the scoping entity.
        /// </summary>
        [EnumMember]
        [Name(Code = "NSIB", Name = "Natural Sibling")]
        NaturalSibling,

        /// <summary>
        /// Natural Sister
        /// The player of the role is a female having the same biological parents as the scoping entity.
        /// </summary>
        [EnumMember]
        [Name(Code = "NSIS", Name = "Natural Sister")]
        NaturalSister,

        /// <summary>
        /// Power Of Attorney
        /// New Concept - A legal instrument authorizing one to act as the attorney or agent of the grantor.
        /// </summary>
        [EnumMember]
        [Name(Code = "POWATY", Name = "Power Of Attorney")]
        PowerOfAttorney,

        /// <summary>
        /// Power Of Attorney-Personal
        /// New Concept - A legal instrument authorizing one to act on personal issues as the attorney or agent of the grantor.
        /// </summary>
        [EnumMember]
        [Name(Code = "POWATYPR", Name = "Power Of Attorney-Personal")]
        PowerOfAttorneyPersonal,

        /// <summary>
        /// Power Of Attorney-Property
        /// New Concept - A legal instrument authorizing one to act on property issues as the attorney or agent of the grantor.
        /// </summary>
        [EnumMember]
        [Name(Code = "POWATYPT", Name = "Power Of Attorney-Property")]
        PowerOfAttorneyProperty,

        /// <summary>
        /// Parent In-Law
        /// The player of the role is the parent of scoping person's husband or wife.
        /// </summary>
        [EnumMember]
        [Name(Code = "PRNINLAW", Name = "Parent In-Law")]
        ParentInLaw,

        /// <summary>
        /// Roomate
        /// One who shares living quarters with the subject.
        /// </summary>
        [EnumMember]
        [Name(Code = "ROOM", Name = "Roomate")]
        Roomate,

        /// <summary>
        /// Sibling In-Law
        /// The player of the role is: (1) a sibling of the scoping person's spouse, or (2) the spouse of the scoping person's sibling, or (3) the spouse of a sibling of the scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "SIBINLAW", Name = "Sibling In-Law")]
        SiblingInLaw,

        /// <summary>
        /// Sister
        /// The player of the role is a female sharing one or both parents in common with the scoping entity.
        /// </summary>
        [EnumMember]
        [Name(Code = "SIS", Name = "Sister")]
        Sister,

        /// <summary>
        /// Sister-In-Law
        /// The player of the role is: (1) a sister of the scoping person's spouse, or (2) the wife of the scoping person's brother, or (3) the wife of a brother of the scoping person's spouse.
        /// </summary>
        [EnumMember]
        [Name(Code = "SISINLAW", Name = "Sister-In-Law")]
        SisterInLaw,

        /// <summary>
        /// Natural Son
        /// The player of the role is a male offspring of the scoping entity (parent).
        /// </summary>
        [EnumMember]
        [Name(Code = "SON", Name = "Natural Son")]
        NaturalSon,

        /// <summary>
        /// Adopted Son
        /// The player of the role is a male child taken into a family through legal means and raised by the scoping person (parent) as his or her own child.
        /// </summary>
        [EnumMember]
        [Name(Code = "SONADOPT", Name = "Adopted Son")]
        AdoptedSon,

        /// <summary>
        /// Son
        /// New Concept -  The player of the role is a male child (of any type) of scoping entity (parent).
        /// </summary>
        [EnumMember]
        [Name(Code = "SONC", Name = "Son")]
        Son,

        /// <summary>
        /// Foster Son
        /// The player of the role is a male child receiving parental care and nurture from the scoping person (parent) but not related to him or her through legal or blood ties.
        /// </summary>
        [EnumMember]
        [Name(Code = "SONFOST", Name = "Foster Son")]
        FosterSon,

        /// <summary>
        /// Son In-Law
        /// The player of the role is the husband of scoping person's daughter.
        /// </summary>
        [EnumMember]
        [Name(Code = "SONINLAW", Name = "Son In-Law")]
        SonInLaw,

        /// <summary>
        /// Stepbrother
        /// The player of the role is a son of the scoping person's stepparent.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPBRO", Name = "Stepbrother")]
        Stepbrother,

        /// <summary>
        /// Step Child
        /// The player of the role is a child of the scoping person's spouse by a previous union.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPCHLD", Name = "Step Child")]
        StepChild,

        /// <summary>
        /// Stepdaughter
        /// The player of the role is a daughter of the scoping person's spouse by a previous union.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPDAU", Name = "Stepdaughter")]
        Stepdaughter,

        /// <summary>
        /// Stepfather
        /// The player of the role is the husband of scoping person's mother and not the scoping person's natural father.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPFTH", Name = "Stepfather")]
        Stepfather,

        /// <summary>
        /// Stepmother
        /// The player of the role is the wife of scoping person's father and not the scoping person's natural mother.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPMTH", Name = "Stepmother")]
        Stepmother,

        /// <summary>
        /// Step Parent
        /// The player of the role is the spouse of the scoping person's parent and not the scoping person's natural parent.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPPRN", Name = "Step Parent")]
        StepParent,

        /// <summary>
        /// Step Sibling
        /// The player of the role is a child of the scoping person's stepparent.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPSIB", Name = "Step Sibling")]
        StepSibling,

        /// <summary>
        /// Stepsister
        /// The player of the role is a daughter of the scoping person's stepparent.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPSIS", Name = "Stepsister")]
        Stepsister,

        /// <summary>
        /// Stepson
        /// The player of the role is a son of the scoping person's spouse by a previous union.
        /// </summary>
        [EnumMember]
        [Name(Code = "STPSON", Name = "Stepson")]
        Stepson,

        /// <summary>
        /// Substitute Decision Maker
        /// New Concept - A person authorized to make medical decisions on behalf of the designated person
        /// </summary>
        [EnumMember]
        [Name(Code = "SUBDM", Name = "Substitute Decision Maker")]
        SubstituteDecisionMaker,

        /// <summary>
        /// Uncle
        /// The player of the role is a brother of the scoping person's mother or father.
        /// </summary>
        [EnumMember]
        [Name(Code = "UNCLE", Name = "Uncle")]
        Uncle,

        /// <summary>
        /// Wife
        /// The player of the role is a woman joined to a man (scoping person) in marriage.
        /// </summary>
        [EnumMember]
        [Name(Code = "WIFE", Name = "Wife")]
        Wife,

    }
}
