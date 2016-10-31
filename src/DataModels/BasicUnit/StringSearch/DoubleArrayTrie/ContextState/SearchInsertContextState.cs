using System;

namespace BasicUnit.DoubleArrayTrie
{
	internal class SearchInsertContextState : InsertContextState
	{
		/// <summary>
		///		如果context.CurrentCharIndex == context.Text.Length-1说明匹配到了最后的#
		///		到了这里说明在Base里匹配到了Endchar'#'，所以BASE['#']==0进来的，是匹配上的
		/// </summary>
		/// <param name="context"></param>
		public override void Handle(Context context)
		{
			if (context.CurrentCharIndex == (context.Text.Length - 1)
				&& context.Trie.Dat[context.Trie.Dat[context.PrePosition].Base + context.Trie.EndChar].Check == context.PrePosition)
			{
				context.Finish();
				context.BeInsertedFlag = true;
				context.BeInsertedPosition = context.Trie.Dat[context.PrePosition].Base + context.Trie.EndChar;
			}
			else
			{
				context.Finish();
				context.BeInsertedFlag = false;
				context.BeInsertedPosition = -1;
			}
		}
	}
}