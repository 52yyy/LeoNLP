using System.Collections.Generic;

namespace Tokenization
{
	/// <summary>
	///		文本切割接口
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITokenizable<T>
	{
		/// <summary>
		///		无覆盖的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		IEnumerable<T> FixedSizeTokenize(T input, int chunksize);

		/// <summary>
		///		含有覆盖的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		IEnumerable<T> OverlappingTokenize(T input, int chunksize, int shiftsize);

		/// <summary>
		///		NGram的窗口长度切分
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		IEnumerable<T> NGramTokenize(T input, int chunksize);
	}
}