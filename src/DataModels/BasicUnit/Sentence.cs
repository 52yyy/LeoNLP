using System.Collections.Generic;
using System.Text;

namespace BasicUnit
{
	/// <summary>
	///		句子
	/// </summary>
	public class Sentence
	{
		public Sentence()
		{
			this.Words = new List<Word>();
		}

		public Sentence(List<Word> words)
		{
			this.Words = words;
		}

		public List<Word> Words { get; set; }

		public Sentence SubSentence(int start, int length)
		{
			var words = this.Words.GetRange(start, length);
			return new Sentence(words);
		}

		public Sentence SubSentence(int start)
		{
			var words = this.Words.GetRange(start, this.Words.Count - start);
			return new Sentence(words);
		}

		public override string ToString()
		{
			return string.Join(string.Empty, this.Words);
		}

		public string ToWordString(bool hasPos = false)
		{
			if (hasPos)
			{
				StringBuilder sb = new StringBuilder();
				foreach (Word word in this.Words)
				{
					sb.Append(word.ToWordString() + " ");
				}
				return sb.ToString().Trim();
			}
			else
			{
				return string.Join(" ", this.Words);
			}
		}
	}
}