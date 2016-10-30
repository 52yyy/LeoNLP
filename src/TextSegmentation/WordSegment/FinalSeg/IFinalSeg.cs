using System.Collections.Generic;

namespace WordSegment
{
	/// <summary>
	///		未登录词识别的分词
	/// </summary>
	public interface IFinalSeg
	{
		/// <summary>
		///		对未登录词部分进行切分
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		IEnumerable<string> Cut(string sentence);
	}
}