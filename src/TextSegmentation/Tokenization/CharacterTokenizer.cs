using System.Collections.Generic;

namespace Tokenization
{
	/// <summary>
	///		字符文本切分器
	/// </summary>
	public class CharacterTokenizer : ITokenizable<string>
	{
		/// <summary>
		///		无覆盖的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public IEnumerable<string> FixedSizeTokenize(string input, int chunksize)
		{
			int offset = 0;
			while (offset < input.Length)
			{
				if (offset + chunksize < input.Length)
				{
					yield return input.Substring(offset, chunksize);
				}
				else
				{
					yield return input.Substring(offset);
				}
				offset += chunksize;
			}
		}

		/// <summary>
		///		含有覆盖的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public IEnumerable<string> OverlappingTokenize(string input, int chunksize, int shiftsize)
		{
			int position = 0;
			while (position < input.Length - chunksize)
			{
				yield return input.Substring(position, chunksize);
				position += chunksize - shiftsize;
			}
		}

		/// <summary>
		///		NGram的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public IEnumerable<string> NGramTokenize(string input, int chunksize)
		{
			for (int i = 0; i < chunksize; i++)
			{
				input += "E";
				input = "B" + input;
			}

			int position = 0;
			while (position < input.Length - chunksize)
			{
				yield return input.Substring(position, chunksize + 1);
				position += 1;
			}
		}
	}
}