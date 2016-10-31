using System;

namespace BasicUnit.DoubleArrayTrie
{
	internal class DeleteTailCollisionUpdateContextState : TailCollisionUpdateContextState
	{
		public override void HandleCollisionTerm(Context context)
		{
			int head = -context.Trie.Dat[context.CurPosition].Base;
			for (int i = context.CurrentCharIndex + 1; i < context.Text.Length; i++)
			{
				if (context.Text[i] == context.Trie.Tail[head++])
				{
					continue;
				}
				context.Finish();
				context.BeInsertedFlag = false;
				context.BeInsertedPosition = -1;
				return;
			}
			context.Trie.Dat[context.CurPosition].Base = 0;
			context.Trie.Dat[context.CurPosition].Check = 0;
			context.Trie.Dat[context.CurPosition].Status = 2;
			context.Trie.Dat[context.CurPosition].Word = null;
			context.Trie.Dat[context.CurPosition].Value = null;
			context.Finish();
			context.BeInsertedFlag = true;
			context.BeInsertedPosition = context.CurPosition;
		}

		public override void HandleInsertedTerm(Context context)
		{
			context.Trie.Dat[context.CurPosition].Base = 0;
			context.Trie.Dat[context.CurPosition].Check = 0;
			context.Trie.Dat[context.CurPosition].Status = 2;
			context.Trie.Dat[context.CurPosition].Word = null;
			context.Trie.Dat[context.CurPosition].Value = null;
			context.Finish();
			context.BeInsertedFlag = true;
			context.BeInsertedPosition = context.CurPosition;
		}

		public override void HandleNewTerm(Context context)
		{
			context.Finish();
			context.BeInsertedFlag = false;
			context.BeInsertedPosition = -1;
		}
	}
}