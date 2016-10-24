using System.Collections.Generic;

namespace Tokenization
{
	/// <summary>
	///		�ַ��ı��з���
	/// </summary>
	public class CharacterTokenizer : ITokenizable<string>
	{
		/// <summary>
		///		�޸��ǵĴ��ڳ����з�
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
		///		���и��ǵĴ��ڳ����з�
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
		///		NGram�Ĵ��ڳ����з�
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