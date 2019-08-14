namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Extended person name.
    /// </summary>
    public class XPN
    {
        public FN familylastname;
        public string givenname;
        public string middleinitialorname;
        public string suffix;
        public string prefix;
        public string degree;
        public string nametypecode;
        public string NameRepresentationcode;
    }
}