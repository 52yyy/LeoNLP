namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Index模式下，处理前缀字符冲突状态
	/// </summary>
	internal class IndexPrefixCollisionUpdateContextState : PrefixCollisionUpdateContextState
	{
		public override void Handle(Context context)
		{
			var tempList = context.Trie.GetChildList(context.PrePosition);
			int toBeAdjust = context.PrePosition;

			if (context.Trie.Dat[toBeAdjust]==null)
			{
				context.Trie.Dat[toBeAdjust] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(toBeAdjust);
			}
			int originBase = context.Trie.Dat[toBeAdjust].Base;
			tempList.Add(context.Text[context.CurrentCharIndex]);
			int availBase = context.Trie.XCheck(tempList.ToArray());
			tempList.RemoveAt(tempList.Count - 1);
			context.Trie.Dat[toBeAdjust].Base = availBase;
			for (int j = 0; j < tempList.Count; ++j)
			{
				int tmp1 = originBase + tempList[j];
				int tmp2 = availBase + tempList[j];
				if (context.Trie.Dat[tmp1]==null)
				{
					context.Trie.Dat[tmp1] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(tmp1);
				}
				if (context.Trie.Dat[tmp2] == null)
				{
					context.Trie.Dat[tmp2] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(tmp2);
				}
				context.Trie.Dat[tmp2].Base = context.Trie.Dat[tmp1].Base;
				context.Trie.Dat[tmp2].Check = context.Trie.Dat[tmp1].Check;
				context.Trie.Dat[tmp2].Status = context.Trie.Dat[tmp1].Status;
				context.Trie.Dat[tmp2].Word = context.Trie.Dat[tmp1].Word;
				context.Trie.Dat[tmp2].Value = context.Trie.Dat[tmp1].Value;
				if (context.Trie.Dat[tmp1].Base > 0)
				{
					var subSequence = context.Trie.GetChildList(tmp1);
					for (int k = 0; k < subSequence.Count; ++k)
					{
						if (context.Trie.Dat[context.Trie.Dat[tmp1].Base + subSequence[k]]==null)
						{
							context.Trie.Dat[context.Trie.Dat[tmp1].Base + subSequence[k]] =
								context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.Trie.Dat[tmp1].Base + subSequence[k]);
						}
						context.Trie.Dat[context.Trie.Dat[tmp1].Base + subSequence[k]].Check = tmp2;
					}
				}
				context.Trie.Dat[tmp1].Base = 0;
				context.Trie.Dat[tmp1].Check = 0;
			}

			if (context.Trie.Dat[context.PrePosition]==null)
			{
				context.Trie.Dat[context.PrePosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.PrePosition);
			}
			context.CurPosition = context.Trie.Dat[context.PrePosition].Base
								+ context.Text[context.CurrentCharIndex];

			if (context.Trie.Dat[context.CurPosition] == null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}
			if (context.Text[context.CurrentCharIndex] == context.Trie.EndChar)
			{
				context.Trie.Dat[context.CurPosition].Base = 0;
			}
			else
			{
				context.Trie.Dat[context.CurPosition].Base = -context.Trie.TailPosition;
			}
			context.Trie.Dat[context.CurPosition].Check = context.PrePosition;
			context.Trie.Dat[context.CurPosition].Status = 3;
			context.Trie.Dat[context.CurPosition].Word = context.Item.Word;
			context.Trie.Dat[context.CurPosition].Value = context.Item.Value;
			context.Trie.TailPosition = context.Trie.CopyToTailArray(context.Text, context.CurrentCharIndex + 1);
			context.Finish();
			context.BeInsertedFlag = true;
			context.BeInsertedPosition = context.CurPosition;
		}
	}
}