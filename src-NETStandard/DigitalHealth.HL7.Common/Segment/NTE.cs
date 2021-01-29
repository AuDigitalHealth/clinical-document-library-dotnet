using DigitalHealth.HL7.Common.DataStructure;

namespace DigitalHealth.HL7.Common.Segment
{
    public class NTE : HL7Segment
    {
        public string SetIDNTE;
        public string SourceofComment;
        public string[] Comment;
        public CE CommentType;
    }
}