namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Money.
    /// </summary>
    public class MO
    {
        /// <summary>
        /// The first component is a quantity.
        /// </summary>
        public string quantity;

        /// <summary>
        /// The second component is the denomination in which the quantity is expressed.
        /// The values for the denomination component are those specified in ISO-4217.
        /// If the denomination is not specified, MSH-17-country code is used to determine the default.
        /// </summary>
        public string denomination;
    }
}