using BasicUnit;

namespace WordSegment
{
	/// <summary>
	///		中文分词接口
	/// </summary>
	public interface IWordSegmenter : IInitializable
	{
		/// <summary>
		///		对输入的字符串进行中文分词
		/// </summary>
		/// <param name="sentenceString">待分词的字符串</param>
		/// <returns>分词结果</returns>
		Sentence SegWord(string sentenceString);
	}
}