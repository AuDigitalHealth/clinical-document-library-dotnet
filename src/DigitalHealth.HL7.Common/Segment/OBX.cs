using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class OBX : HL7Segment
    {
        public string SetIDOBX;
        public string ValueType;
        public CE ObservationIdentifier;
        public string ObservationSubID;
        public string[] ObservationValue;
        public CE Units;
        public string ReferencesRange;   // Low and High
        public string[] AbnormalFlags;
        public string[] Probability;
        public string NatureofAbnormalTest;
        public string ObservationResultStatus;
        public TS DateLastObsNormalValues;
        public string UserDefinedAccessChecks;
        public TS DateTimeoftheObservation;
        public CE ProducersID;
        public XCN[] ResponsibleObserver;
        public CE[] ObservationMethod;
    }
}