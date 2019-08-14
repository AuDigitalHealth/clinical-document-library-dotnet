namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Extended composite ID with check digit.
    /// </summary>
    public class CX
    {
        public string ID;
        public string checkdigit;
        public string codeidentifyingthecheckdigit;
        public HD assigningauthority;
        public string identifiertypecode;
        public HD assigningfacility;
        public TS EffectiveDate;
    }
}