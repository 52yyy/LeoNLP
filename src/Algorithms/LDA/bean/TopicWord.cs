using System;
using System.Diagnostics;

namespace LDA
{
	[Serializable]
	[DebuggerDisplay("Word = {Word}, Probability = {Probability}")]

	public class TopicWord
	{
		public TopicWord(string word, double probability)
		{
			this.Word = word;
			this.Probability = probability;
		}

		public string Word { get; set; }

		public double Probability { get; set; }
	}
}