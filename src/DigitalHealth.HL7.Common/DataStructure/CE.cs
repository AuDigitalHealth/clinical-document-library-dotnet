namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Coded element, transmits codes and the text associated with the code.
    /// </summary>
    public class CE
    {
        public string identifier;
        public string text;
        public string nameofcodingsystem;
        public string alternateidentifier;
        public string alternatetext;
        public string nameofalternatecodingsystem;
    }
}