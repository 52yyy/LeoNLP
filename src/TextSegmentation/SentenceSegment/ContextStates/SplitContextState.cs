using System.Linq;

namespace SentenceSegment
{
	/// <summary>
	///		断句状态
	/// </summary>
	internal class SplitContextState : ContextState
	{
		/// <summary>
		///		执行断句的行为
		/// </summary>
		/// <param name="context"></param>
		public override void Execute(Context context)
		{
			context.HasSplitSign = true;

			// to find if next char also be secceed
			while (context.CurrentIndex < context.Text.Length - 1)
			{
				context.CurrentIndex++;
				char tmp = context.Text[context.CurrentIndex];
				if (Sign.SplitSign.Contains(tmp))
				{
					context.CurrentSentenceBuilder.Append(tmp);
				}
				else
				{
					context.CurrentIndex--;
					break;
				}
			}

			// finish current sentence
			if (context.CurrentSentenceBuilder.Length != 0)
			{
				string sen = context.CurrentSentenceBuilder.ToString();
				context.Sentences.Add(sen);
				context.CurrentSentenceBuilder.Clear();
			}
			context.State = ContextStateManager.FinishCheckContextState;
		}
	}
}