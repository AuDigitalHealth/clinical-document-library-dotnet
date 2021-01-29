namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Extended composite ID number and name for persons.
    /// </summary>
    public class XCN
    {
        public string IDnumberST;
        public FN familylastname;
        public string givenname;
        public string middleinitialorname;
        public string suffix;
        public string prefix;
        public string degree;
        public string sourcetable;
        public HD assigningauthority;
        public string nametypecode;
        public string identifiercheckdigit;
        public string codeidentifyingthecheckdigit;
        public string identifiertypecode;
        public HD assigningfacility;
        public string NameRepresentationcode;
    }
}