namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		��ѯG������״̬
	/// </summary>
	internal class GCheckContextState : ContextState
	{
		public override void BeInserted(Context context)
		{
			context.CurPosition = context.Trie.GFunction(context.PrePosition, context.Text[context.CurrentCharIndex]);
			// ���û�������ߣ������б߲���ͻ��ת����ѯB״̬
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}

			if (context.Trie.Dat[context.CurPosition].Check == 0 || context.Trie.Dat[context.CurPosition].Check == context.PrePosition)
			{
				context.State = ContextStateManager.BCheckContextState;
			}
			// ����б��ҳ�ͻ��ת�������ظ���ͻ״̬
			else
			{
				context.State = ContextStateBuilder.BuildPrefixCollisionUpdateContextState(context.TrieState);
			}
		}
	}
}