namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// This class contains all the properties for a validation message as requried by the 
    /// common NEHTA validation engine.
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// The name of the property that was validated
        /// </summary>
        public string Name;
        
        /// <summary>
        /// The value of the property at the time of validation
        /// </summary>
        public object Value;

        /// <summary>
        /// The validation message
        /// </summary>
        public string Message;
    }
}
