namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Specimen source, identifies the site where the specimen should be obtained
    /// or where the service should be performed.
    /// </summary>
    public class SPS
    {
        public CE specimensourcenameorcode;
        public string additives;
        public string freetext;
        public CE bodysite;
        public CE sitemodifier;
        public CE collectionmodifiermethodcode;
        public CE specimenrole;
    }
}