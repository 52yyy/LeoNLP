namespace WordSegment
{
    /// <summary>
    /// 分词单位
    /// </summary>
    public class SegToken
    {
        public string Word { get; set; }
        public string Tag { get; set; }
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }

        public SegToken(string word, int startOffset, int endOffset)
        {
            Word = word;
            StartOffset = startOffset;
            EndOffset = endOffset;
        }

        public SegToken(string word, string tag, int startOffset, int endOffset)
        {
            Word = word;
            Tag = tag;
            StartOffset = startOffset;
            EndOffset = endOffset;
        }

        public  string ToString(bool all)
        {
            return "[" + Word + ", " + Tag + ", " + StartOffset + ", " + EndOffset + "]";
        }

        public override string ToString()
        {
    	    return Word+"/"+Tag;
        }

    }
}
