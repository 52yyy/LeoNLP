namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		查询Base表的状态
	/// </summary>
	internal class BCheckContextState : ContextState
	{
		public override void BeInserted(Context context)
		{
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}
			int currentBaseValue = context.Trie.Dat[context.CurPosition].Base;
			// 如果>0，转到查询G状态查询下一个
			if (currentBaseValue > 0)
			{
				context.PrePosition = context.CurPosition;
				context.CurrentCharIndex++;
				context.State = ContextStateManager.GCheckContextState;
			}
			// 如果==0，转到插入状态
			else if (currentBaseValue == 0)
			{
				context.State = ContextStateBuilder.BuildInsertContextState(context.TrieState);
			}
			// 如果<0，转到更新Tail冲突状态
			else
			{
				context.State = ContextStateBuilder.BuildTailCollisionUpdateContextState(context.TrieState);
			}
		}
	}
}