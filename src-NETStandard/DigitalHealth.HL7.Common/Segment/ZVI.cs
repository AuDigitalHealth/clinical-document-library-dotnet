using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class ZVI : HL7Segment
    {
        public CE EpisodeofCareType;
        public CE DischargeLegalStatus;
        public CE DischargeReferral;
        public CE PatientElection;
        public CE FundingSource;
        public string AdmitDoctorID1;
        public string AttendDoctorID1;
    }
}