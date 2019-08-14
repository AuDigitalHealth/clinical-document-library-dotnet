namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Extended composite name and identification number for organizations
    /// </summary>
    public class XON
    {
        public string organizationname;
        public string organizationnametypecode;
        public string IDnumberNM;
        public string checkdigit;
        public string codeidentifyingthecheckdigit;
        public HD assigningauthority;
        public string identifiertypecode;
        public HD assigningfacilityID;
        public string NameRepresentationcode;
    }
}