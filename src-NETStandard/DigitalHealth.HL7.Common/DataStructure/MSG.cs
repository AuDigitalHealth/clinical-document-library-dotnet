namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Message Type, contains the message type, trigger event, and abstract
    /// message structure code for the message.
    /// </summary>
    public class MSG
    {
        public string messagetype;
        public string triggerevent;
        public string messagestructure;
    }
}