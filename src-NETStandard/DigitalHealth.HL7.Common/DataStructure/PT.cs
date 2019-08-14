namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Processing ID and mode. Defines whether the message is part of a
    /// production, training, or debugging system, and whether the message is
    /// part of an archival process or an initial load.
    /// </summary>
    public class PT
    {
        public string processingID;
        public string processingmode;
    }
}