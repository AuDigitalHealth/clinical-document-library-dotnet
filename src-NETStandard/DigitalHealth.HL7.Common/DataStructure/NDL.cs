namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Name, date and location, used to identify the result interpreters, technician and transcriptionist.
    /// </summary>
    public class NDL
    {
        public CN name;
        public TS startdatetime;
        public TS enddatetime;
        public string pointofcareIS;
        public string room;
        public string bed;
        public HD facilityHD;
        public string locationstatus;
        public string personlocationtype;
        public string building;
        public string floor;
    }
}