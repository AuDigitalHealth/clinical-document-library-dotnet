namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Authorisation information. Based on the type of insurance, some coverage
    /// plans require that an authorization number or code be obtained prior to
    /// all non-emergency admissions, and within 48 hours of an emergency
    /// admission. Insurance billing would not be permitted without this number.
    /// The date and source of authorization are the components of this field.
    /// </summary>
    public class AUI
    {
        public string authorizationnumber;
        public TS date;
        public string source;
    }
}