namespace SentenceSegment
{
	/// <summary>
	///		中文分句接口
	/// </summary>
	public interface ISentenceSegmenter
	{
		/// <summary>
		///		分句
		/// </summary>
		/// <param name="text">带分句的文本</param>
		/// <returns></returns>
		string[] Segment(string text);
	}
}