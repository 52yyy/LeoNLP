namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		����Tail��ͻ״̬
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
			// ����Ѿ�û����һ�������ˣ�˵���Ѿ�������ˣ�ֱ�ӳ�
			if (context.Text.Length == context.CurrentCharIndex + 1)
			{
				this.HandleInsertedTerm(context);				
			}
				// �����һ���ǽ�������'#'��˵���Ѿ�������ˣ�ֱ�ӳ�
			else if (context.Trie.Tail[head] == context.Trie.EndChar && context.Text[context.CurrentCharIndex + 1] == context.Trie.EndChar)
			{
				this.HandleInsertedTerm(context);
			}
				// �����һ�����ǽ������ţ�����һλ�ַ���ͬ����ô��Ҫ����׷����һλ�ַ�������������һλ��ת��GCheck״̬
			else if (context.Trie.Tail[head] == context.Text[context.CurrentCharIndex + 1])
			{
				this.HandleCollisionTerm(context);
			}
				// �����һ�����ǽ������ţ�����һλ�ַ���ͬ����ô˵�������λ�ÿ������ֳ�����ӵĴ��ˣ���ô�ͽ�����ӣ����Tail��ͻ
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