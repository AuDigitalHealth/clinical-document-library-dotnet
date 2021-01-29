namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Composite price
    /// </summary>
    public class CP
    {
        public MO price;
        public string pricetype;
        public string fromvalue;
        public string tovalue;
        public CE rangeunits;
        public string rangetype;
    }
}