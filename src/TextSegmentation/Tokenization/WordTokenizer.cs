using System.Collections.Generic;

using BasicUnit;

namespace Tokenization
{
	/// <summary>
	///		词汇文本切分器
	/// </summary>
	public class WordTokenizer : ITokenizable<Sentence>
	{
		/// <summary>
		///		无覆盖的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public IEnumerable<Sentence> FixedSizeTokenize(Sentence input, int chunksize)
		{
			int offset = 0;
			while (offset < input.Words.Count)
			{
				Sentence sentence = new Sentence();
				if (offset + chunksize > input.Words.Count)
				{
					sentence.Words = input.Words.GetRange(offset, input.Words.Count - offset);
				}
				else
				{
					sentence.Words = input.Words.GetRange(offset, chunksize);
				}

				yield return sentence;
				offset += chunksize;
			}
		}

		/// <summary>
		///		含有覆盖的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public IEnumerable<Sentence> OverlappingTokenize(Sentence input, int chunksize, int shiftsize)
		{
			int position = 0;
			while (position < input.Words.Count - chunksize)
			{
				yield return new Sentence { Words = input.Words.GetRange(position, chunksize + 1) };
				position += chunksize - shiftsize;
			}
		}

		/// <summary>
		///		NGram的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public IEnumerable<Sentence> NGramTokenize(Sentence input, int chunksize)
		{
			Sentence tmp = new Sentence();
			foreach (var word in input.Words)
			{
				tmp.Words.Add(word);
			}

			for (int i = 0; i < chunksize; i++)
			{
				tmp.Words.Add(new Word("<EOS>"));
				tmp.Words.Insert(i, new Word("<BOS>"));
			}

			int position = 0;
			while (position < tmp.Words.Count - chunksize)
			{
				Sentence sentence = new Sentence { Words = tmp.Words.GetRange(position, chunksize + 1) };
				yield return sentence;
				position += 1;
			}
		}
	}
}