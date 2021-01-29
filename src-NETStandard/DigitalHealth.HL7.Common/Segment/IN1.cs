using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class IN1 : HL7Segment
    {
        public string SetID;
        public CE InsurancePlanID;
        public CX[] InsuranceCompanyID;
        public XON[] InsuranceCompanyName;
        public XAD[] InsuranceCompanyAddress;
        public XPN[] InsuranceCoContactPerson;
        public XTN[] InsuranceCoPhoneNumber;
        public string GroupNumber;
        public XON[] GroupName;
        public CX[] InsuredsGroupEmpID;
        public XON[] InsuredsGroupEmpName;
        public string PlanEffectiveDate;
        public string PlanExpirationDate;
        public AUI AuthorizationInformation;
        public string PlanType;
        public XPN[] NameOfInsured;
        public CE InsuredsRelationshipToPatient;
        public TS InsuredsDateOfBirth;
        public XAD[] InsuredsAddress;
        public string AssignmentOfBenefits;
        public string CoordinationOfBenefits;
        public string CoordOfBenPriority;
        public string NoticeOfAdmissionFlag;
        public string NoticeOfAdmissionDate;
        public string ReportOfEligibilityFlag;
        public string ReportOfEligibilityDate;
        public string ReleaseInformationCode;
        public string PreAdmitCertPAC;
        public TS VerificationDateTime;
        public XCN[] VerificationBy;
        public string TypeOfAgreementCode;
        public string BillingStatus;
        public string LifetimeReserveDays;
        public string DelayBeforeLRDay;
        public string CompanyPlanCode;
        public string PolicyNumber;
        public CP PolicyDeductible;
        public CP PolicyLimitAmount;
        public string PolicyLimitDays;
        public CP RoomRateSemiPrivate;
        public CP RoomRatePrivate;
        public CE InsuredsEmploymentStatus;
        public string InsuredsSex;
        public XAD[] InsuredsEmployersAddress;
        public string VerificationStatus;
        public string PriorInsurancePlanID;
        public string CoverageType;
        public string Handicap;
        public CX[] InsuredsIDNumber;
    }
}