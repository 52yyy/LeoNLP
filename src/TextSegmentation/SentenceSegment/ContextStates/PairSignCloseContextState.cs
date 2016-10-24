namespace SentenceSegment
{
	/// <summary>
	///		引号匹配关闭状态
	/// </summary>
	internal class PairSignCloseContextState : ContextState
	{
		/// <summary>
		///		还需要决定引号的处理方式
		/// </summary>
		/// <param name="context"></param>
		public override void Execute(Context context)
		{
			// finish current sentence
			if (context.CurrentSentenceBuilder.Length != 0)
			{
				string sen = context.CurrentSentenceBuilder.ToString();
				context.Sentences.Add(sen);
				context.CurrentSentenceBuilder.Clear();
			}
			context.Sentences.Add(context.CurrentChar.ToString());
			context.Finish();
			return;
		}
	}
}