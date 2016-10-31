namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Index模式下，处理Insert状态
	/// </summary>
	internal class IndexInsertContextState : InsertContextState
	{
		public override void Handle(Context context)
		{
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}
			context.Trie.Dat[context.CurPosition].Base = -context.Trie.TailPosition;
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