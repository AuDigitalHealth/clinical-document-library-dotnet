namespace DigitalHealth.HL7.Common.DataStructure
{
    /// <summary>
    /// Quantity/timing describes when a service should be performed and how frequently.
    /// </summary>
    public class TQ
    {
        public CQ quantity;
        public RI interval;
        public string duration;
        public TS startdatetime;
        public TS enddatetime;
        public string priority;
        public string condition;
        public string text;
        public string conjunction;
        public OSD ordersequencing;
        public CE occurrenceduration;
        public string totaloccurences;
    }
}