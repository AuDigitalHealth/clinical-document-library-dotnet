using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class PV2 : HL7Segment
    {
        public PL PriorPendingLocation;
        public CE AccommodationCode;
        public CE AdmitReason;
        public CE TransferReason;
        public string[] PatientValuables;
        public string PatientValuablesLocation;
        public string VisitUserCode;
        public TS ExpectedAdmitDateTime;
        public TS ExpectedDischargeDateTime;
        public string EstimatedLengthofInpatientStay;
        public string ActualLengthofInpatientStay;
        public string VisitDescription;
        public XCN[] ReferralSourceCode;
        public string PreviousServiceDate;
        public string EmploymentIllnessRelatedIndicator;
        public string PurgeStatusCode;
        public string PurgeStatusDate;
        public string SpecialProgramCode;
        public string RetentionIndicator;
        public string ExpectedNumberofInsurancePlans;
        public string VisitPublicityCode;
        public string VisitProtectionIndicator;
        public XON[] ClinicOrganizationName;
        public string PatientStatusCode;
        public string VisitPriorityCode;
        public string PreviousTreatmentDate;
        public string ExpectedDischargeDisposition;
        public string SignatureonFileDate;
        public string FirstSimilarIllnessDate;
        public CE PatientChargeAdjustmentCode;
        public string RecurringServiceCode;
        public string BillingMediaCode;
        public TS ExpectedSurgeryDateTime;
        public string MilitaryPartnershipCode;
        public string MilitaryNonAvailabilityCode;
        public string NewbornBabyIndicator;
        public string BabyDetainedIndicator;
    }
}