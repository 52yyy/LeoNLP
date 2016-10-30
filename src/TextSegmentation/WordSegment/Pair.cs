namespace WordSegment
{
    internal class Pair<TKey>
    {
		public Pair(TKey key, double freq)
		{
			Key = key;
			Freq = freq;
		}

		public TKey Key { get; set; }

		public double Freq { get; set; }

        public override string ToString()
        {
            return "Candidate [key=" + Key + ", Frequency=" + Freq + "]";
        }
    }
}
