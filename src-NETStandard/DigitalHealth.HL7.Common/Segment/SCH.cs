using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class SCH : HL7Segment
    {
        public EI PlacerAppointmentID;
        public EI FillerAppointmentID;
        public string OccurrenceNumber;
        public EI PlacerGroupNumber;
        public CE ScheduleID;
        public CE EventReason;
        public CE AppointmentReason;
        public CE AppointmentType;
        public string AppointmentDuration;
        public CE AppointmentDurationUnits;
        public TQ AppointmentTimingQuantity;
        public XCN PlacerContactPerson;
        public XTN[] PlacerContactPhoneNumber;
        public XAD PlacerContactAddress;
        public PL PlacerContactLocation;
        public XCN FillerContactPerson;
        public XTN FillerContactPhoneNumber;
        public XAD FillerContactAddress;
        public PL FillerContactLocation;
        public XCN EnteredbyPerson;
        public XTN EnteredbyPhoneNumber;
        public PL EnteredbyLocation;
        public EI[] ParentPlacerAppointmentID;
        public EI ParentFillerAppointmentID;
        public CE FillerStatusCode;
    }
}