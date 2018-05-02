namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Parent Result, uniquely identifies the parent result’s OBX segment related to this order.
    /// The value of this OBX segment in the parent result is the organism or chemical species
    /// about which this battery reports.
    /// </summary>
    public class PRL
    {
        public CE OBX3observationidentifierofp;
        public string OBX4subIDofparentresult;
        public string partofOBX5observationresultf;
    }
}