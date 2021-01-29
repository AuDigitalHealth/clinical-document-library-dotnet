namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Version ID, matched by the receiving system to its own version to be
    /// sure the message will be interpreted correctly.
    /// </summary>
    public class VID
    {
        public string versionID;
        public CE internationalizationcode;
        public CE internationalversionID;
    }
}