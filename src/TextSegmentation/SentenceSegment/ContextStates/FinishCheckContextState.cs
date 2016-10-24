namespace SentenceSegment
{
	/// <summary>
	///		结束检查状态
	/// </summary>
	internal class FinishCheckContextState : ContextState
	{
		/// <summary>
		///		执行当前状态
		/// </summary>
		/// <param name="context"></param>
		public override void Execute(Context context)
		{
			if (context.CurrentIndex + 1 == context.Text.Length)
			{
				// finish current sentence
				if (context.CurrentSentenceBuilder.Length != 0)
				{
					string sen = context.CurrentSentenceBuilder.ToString();
					context.Sentences.Add(sen);
					context.CurrentSentenceBuilder.Clear();
				}
				context.Finish();
				return;
			}
			else
			{
				context.CurrentIndex++;
				context.State = ContextStateManager.CharCheckContextState;
			}
		}
	}
}