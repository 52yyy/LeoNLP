namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		查询G函数的状态
	/// </summary>
	internal class GCheckContextState : ContextState
	{
		public override void BeInserted(Context context)
		{
			context.CurPosition = context.Trie.GFunction(context.PrePosition, context.Text[context.CurrentCharIndex]);
			// 如果没有这条边，或者有边不冲突，转到查询B状态
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}

			if (context.Trie.Dat[context.CurPosition].Check == 0 || context.Trie.Dat[context.CurPosition].Check == context.PrePosition)
			{
				context.State = ContextStateManager.BCheckContextState;
			}
			// 如果有边且冲突，转到更新重复冲突状态
			else
			{
				context.State = ContextStateBuilder.BuildPrefixCollisionUpdateContextState(context.TrieState);
			}
		}
	}
}