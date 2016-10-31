namespace BasicUnit.DoubleArrayTrie
{
	internal class DeleteInsertContextState : InsertContextState
	{
		/// <summary>
		/// 	如果context.CurrentCharIndex == context.Text.Length-1说明匹配到了最后的#
		///		到了这里说明在Base里匹配到了Endchar'#'，所以BASE['#']==0进来的，是匹配上的
		///		光修改BASE[]=0不够，但是把CHECK也改了就可以了
		/// </summary>
		/// <param name="context"></param>
		public override void Handle(Context context)
		{
			if (context.CurrentCharIndex == (context.Text.Length - 1))
			{
				if (context.Trie.Dat[context.PrePosition]==null)
				{
					context.Trie.Dat[context.PrePosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.PrePosition);
				}
				if (context.Trie.Dat[context.Trie.Dat[context.PrePosition].Base + context.Trie.EndChar]==null)
				{
					context.Trie.Dat[context.Trie.Dat[context.PrePosition].Base + context.Trie.EndChar]
						= context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.Trie.Dat[context.PrePosition].Base + context.Trie.EndChar);
				}
				if (context.Trie.Dat[context.Trie.Dat[context.PrePosition].Base + context.Trie.EndChar].Check
					== context.PrePosition)
				{
					if (context.Trie.Dat[context.CurPosition] == null)
					{
						context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
					}
					context.Trie.Dat[context.CurPosition].Base = 0;
					context.Trie.Dat[context.CurPosition].Check = 0;
					context.Trie.Dat[context.CurPosition].Status = 2;
					context.Trie.Dat[context.CurPosition].Word = null;
					context.Trie.Dat[context.CurPosition].Value = null;
					context.Finish();
					context.BeInsertedFlag = true;

					if (context.Trie.Dat[context.PrePosition]==null)
					{
						context.Trie.Dat[context.PrePosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.PrePosition);
					}
					context.BeInsertedPosition = context.Trie.Dat[context.PrePosition].Base
												+ context.Trie.EndChar;
				}
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