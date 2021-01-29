using DigitalHealth.HL7.Common.Segment;
using DigitalHealth.HL7.Common.SegmentGroup;

namespace DigitalHealth.HL7.Common.Message
{
    /// <summary>
    /// This class represents a generic HL7 message from a Patient Administration System.
    /// This covers all ADT, PMI and EMPI messages.
    /// Note that the first segment MSH is declared in the superclass HL7Message.
    /// </summary>
    public class HL7GenericPasMessage : HL7Message
    {
        public EVN Event;
        public SCH Schedule;
        public NTE[] ScheduleNotes;
        public PID PatientIdentification;
        public NK1[] NextOfKin;
        public MRG MergeInformation;
        public PV1 PatientVisit;
        public PV2 PatientVisitAdditional;
        public AL1[] Allergy;
        public DG1[] Diagnosis;
        public DRG DiagnosesRelatedGroup;
        public IN1[] Insurance;
        public ResourceGroup[] ResourceGroup;
        public ZVI VisitInformationAdditional;
        public ZPD PersonDetailsAdditional;
    }
}