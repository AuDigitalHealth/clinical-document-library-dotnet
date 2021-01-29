namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Person location, used to specify a patient location within a healthcare institution.
    /// </summary>
    public class PL
    {
        public string pointofcare;
        public string room;
        public string bed;
        public HD facilityHD;
        public string locationstatus;
        public string personlocationtype;
        public string building;
        public string floor;
        public string Locationdescription;
    }
}