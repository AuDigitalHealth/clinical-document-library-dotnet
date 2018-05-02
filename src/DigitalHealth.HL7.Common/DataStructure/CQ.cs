namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Composite quantity with units.
    /// </summary>
    public class CQ
    {
        public string quantity;
        public CE units;    // problem - too many levels of nesting? change to string
    }
}