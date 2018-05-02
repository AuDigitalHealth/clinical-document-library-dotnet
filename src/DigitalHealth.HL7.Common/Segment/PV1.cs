using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class PV1 : HL7Segment
    {
        public string SetID;
        public string PatientClass;
        public PL AssignedPatientLocation;
        public CE AdmissionType;  // Standard Enterprise HL7 Specification notes deviation from HL7 Standard from IS data identifier to CE data identifier.
        public CX PreadmitNumber;
        public PL PriorPatientLocation;
        public XCN[] AttendingDoctor;
        public XCN[] ReferringDoctor;
        public XCN[] ConsultingDoctor;
        public string HospitalService;
        public PL TemporaryLocation;
        public string PreadmitTestIndicator;
        public string ReadmissionIndicator;
        public string AdmitSource;
        public string[] AmbulatoryStatus;
        public string VIPIndicator;
        public XCN[] AdmittingDoctor;
        public string PatientType;
        public CX VisitNumber;
        public FC[] FinancialClass;
        public string ChargePriceIndicator;
        public string CourtesyCode;
        public string CreditRating;
        public string[] ContractCode;
        public string[] ContractEffectiveDate;
        public string[] ContractAmount;
        public string[] ContractPeriod;
        public string InterestCode;
        public string TransfertoBadDebtCode;
        public string TransfertoBadDebtDate;
        public string BadDebtAgencyCode;
        public string BadDebtTransferAmount;
        public string BadDebtRecoveryAmount;
        public string DeleteAccountIndicator;
        public string DeleteAccountDate;
        public string DischargeDisposition;
        public DLD DischargedtoLocation;
        public CE DietType;
        public string ServicingFacility;
        public string BedStatus;
        public string AccountStatus;
        public PL PendingLocation;
        public PL PriorTemporaryLocation;
        public TS AdmitDateTime;
        public TS DischargeDateTime;
        public string CurrentPatientBalance;
        public string TotalCharges;
        public string TotalAdjustments;
        public string TotalPayments;
        public CX AlternateVisitID;
        public string VisitIndicator;
        public XCN[] OtherHealthcareProvider;
    }
}