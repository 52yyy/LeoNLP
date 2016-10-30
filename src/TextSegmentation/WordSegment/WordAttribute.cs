namespace WordSegment
{
	/// <summary>
	///		词语属性
	/// </summary>
    public class WordAttribute
    {
		public WordAttribute(double frequency, string tokenType)
		{
			Frequency = frequency;
			TokenType = tokenType;
		}

        public double Frequency { get; set; }

        public string TokenType { get; set; }

		public double Weight { get; set; }
    }
}
