using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class OBR : HL7Segment
    {
        public string SetIDOBR;
        public EI PlacerOrderNumber;
        public EI FillerOrderNumber;
        public CE UniversalServiceID;
        public string PriorityOBR;
        public TS RequestedDatetime;
        public TS ObservationDateTime;
        public TS ObservationEndDateTime;
        public CQ CollectionVolume;
        public XCN[] CollectorIdentifier;
        public string[] SpecimenActionCode;
        public CE[] DangerCode;
        public string RelevantClinicalInfo;
        public TS[] SpecimenReceivedDateTime;
        public SPS SpecimenSource;
        public XCN OrderingProvider;
        public XTN OrderCallbackPhoneNumber;
        public string PlacerField1;
        public string[] PlacerField2;
        public string FillerField1;
        public string[] FillerField2;
        public TS[] ResultsRptStatusChngDateTime;
        public MOC[] ChargetoPractice;
        public string DiagnosticServSectID;
        public string ResultStatus;
        public PRL ParentResult;
        public TQ QuantityTiming;
        public XCN ResultCopiesTo;
        public EIP Parent;
        public string TransportationMode;
        public CE ReasonforStudy;
        public NDL PrincipalResultInterpreter;
        public NDL AssistantResultInterpreter;
        public NDL Technician;
        public NDL Transcriptionist;
        public TS ScheduledDateTime;
        public string NumberofSampleContainers;
        public CE TransportLogisticsofCollectedSample;
        public CE CollectorsComment;
        public CE TransportArrangementResponsibility;
        public string TransportArranged;
        public string EscortRequired;
        public CE PlannedPatientTransportComment;
        public CE[] ProcedureCode;
        public CE ProcedureCodeModifier;
    }
}