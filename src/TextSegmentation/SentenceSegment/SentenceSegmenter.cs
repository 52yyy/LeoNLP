namespace SentenceSegment
{
	/// <summary>
	///		中文分句
	/// </summary>
    public class SentenceSegmenter : ISentenceSegmenter
	{
		/// <summary>
		///		分句
		/// </summary>
		/// <param name="text">带分句的文本</param>
		/// <returns></returns>
		public string[] Segment(string text)
		{
			Context context = new Context(text, ContextStateManager.CharCheckContextState);
			context.Execute();
			return context.Sentences.ToArray();
		}
	}
}
