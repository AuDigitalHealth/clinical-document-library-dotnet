using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class NK1 : HL7Segment
    {
        public string SetID;
        public XPN[] Name;
        public CE Relationship;
        public XAD[] Address;
        public XTN[] PhoneNumber;
        public XTN[] BusinessPhoneNumber;
        public CE ContactRole;
        public string StartDate;
        public string EndDate;
        public string NextofKinAssocJobTitle;
        public JCC NextofKinAssocJobCodeClass;
        public CX NextofKinAssocEmployeeNumber;
        public XON[] OrganizationNameNK1;
        public CE MaritalStatus;
        public string Sex;
        public TS DateTimeOfBirth;
        public string[] LivingDependency;
        public string[] AmbulatoryStatus;
        public CE[] Citizenship;
        public CE PrimaryLanguage;
        public string LivingArrangement;
        public CE PublicityCode;
        public string ProtectionIndicator;
        public string StudentIndicator;
        public CE Religion;
        public XPN[] MothersMaidenName;
        public CE Nationality;
        public CE[] EthnicGroup;
        public CE[] ContactReason;
        public XPN[] ContactPersonsName;
        public XTN[] ContactPersonsTelephoneNumber;
        public XAD[] ContactPersonsAddress;
        public CX[] NextofKinAssociatedPartysIdentifiers;
        public string JobStatus;
        public CE[] Race;
        public string Handicap;
        public string ContactPersonSocialSecurityNumber;
    }
}