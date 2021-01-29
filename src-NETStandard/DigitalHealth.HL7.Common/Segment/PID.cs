using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class PID : HL7Segment
    {
        public string SetID;
        public CX PatientID;
        public CX[] PatientIdentifierList;
        public CX[] AlternatePatientIDPID;
        public XPN[] PatientName;
        public XPN[] MothersMaidenName;
        public TS DateTimeOfBirth;
        public CE Sex;
        public XPN[] PatientAlias;
        public CE[] Race;   // 10
        public XAD[] PatientAddress;
        public string CountyCode;
        public XTN[] PhoneNumberHome;
        public XTN[] PhoneNumberBusiness;
        public CE PrimaryLanguage;
        public CE MaritalStatus;
        public CE Religion;
        public CX PatientAccountNumber;
        public string SSNNumberPatient;
        public DLN DriversLicenseNumberPatient; // 20
        public CX[] MothersIdentifier; // 21
        public CE[] EthnicGroup; // 22
        public CE BirthPlace; // HL7 v2.3.1 uses ST for this field. CE has been chosen to constrain values to standard countries.
        public string MultipleBirthIndicator; // 24
        public string BirthOrder;
        public CE[] Citizenship;
        public CE VeteransMilitaryStatus;
        public CE Nationality;
        public TS PatientDeathDateandTime;
        public string PatientDeathIndicator; // 30
    }
}