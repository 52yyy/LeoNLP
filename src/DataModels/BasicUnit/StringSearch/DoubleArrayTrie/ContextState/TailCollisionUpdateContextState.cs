namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		更新Tail冲突状态
	/// </summary>
	internal abstract class TailCollisionUpdateContextState : ContextState
	{
		public override void BeInserted(Context context)
		{
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}
			int head = -context.Trie.Dat[context.CurPosition].Base;
			// 如果已经没有下一个符号了，说明已经插入过了，直接出
			if (context.Text.Length == context.CurrentCharIndex + 1)
			{
				this.HandleInsertedTerm(context);				
			}
				// 如果下一个是结束符号'#'，说明已经插入过了，直接出
			else if (context.Trie.Tail[head] == context.Trie.EndChar && context.Text[context.CurrentCharIndex + 1] == context.Trie.EndChar)
			{
				this.HandleInsertedTerm(context);
			}
				// 如果下一个不是结束符号，且下一位字符相同，那么需要继续追加下一位字符的索引，下移一位，转到GCheck状态
			else if (context.Trie.Tail[head] == context.Text[context.CurrentCharIndex + 1])
			{
				this.HandleCollisionTerm(context);
			}
				// 如果下一个不是结束符号，且下一位字符不同，那么说明从这个位置可以区分出新添加的词了，那么就进行添加，解决Tail冲突
			else
			{
				this.HandleNewTerm(context);
			}
		}

		public abstract void HandleCollisionTerm(Context context);

		public abstract void HandleInsertedTerm(Context context);

		public abstract void HandleNewTerm(Context context);
	}
}