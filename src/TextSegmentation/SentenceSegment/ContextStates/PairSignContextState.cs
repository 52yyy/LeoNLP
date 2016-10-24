namespace SentenceSegment
{
	/// <summary>
	///		引号状态
	/// </summary>
	internal  class PairSignContextState : ContextState
	{
		public override void Execute(Context context)
		{
			if (context.CurrentIndex + 1 == context.Text.Length)
			{
				context.CurrentSentenceBuilder.Append(context.CurrentChar);
			}
			else
			{
				Context pairSignContext = new Context(context.Text, ContextStateManager.CharCheckContextState);
				pairSignContext.Sentences.Add(context.CurrentChar.ToString());

				pairSignContext.CurrentIndex = context.CurrentIndex + 1;
				pairSignContext.PairSign = context.CurrentChar;
				pairSignContext.BackPairSign = PairSign.PairSigns[pairSignContext.PairSign];
				pairSignContext.IsPairSignOpened = true;
				pairSignContext.Execute();
				string[] res = pairSignContext.Sentences.ToArray();

				if (res.Length == 3 && res[2] == pairSignContext.BackPairSign.ToString()
					&& res[0] == pairSignContext.PairSign.ToString() && res[1].Length < 20) // 这里的规则值得推敲
				{
					context.CurrentSentenceBuilder.Append(string.Join("", res));
				}
				else
				{
					// finish current sentence
					if (context.CurrentSentenceBuilder.Length != 0)
					{
						string sen = context.CurrentSentenceBuilder.ToString();
						context.Sentences.Add(sen);
						context.CurrentSentenceBuilder.Clear();
					}
					context.Sentences.AddRange(res);
				}

				context.CurrentIndex = pairSignContext.CurrentIndex;
			}
			context.State = ContextStateManager.FinishCheckContextState;
		}
	}
}