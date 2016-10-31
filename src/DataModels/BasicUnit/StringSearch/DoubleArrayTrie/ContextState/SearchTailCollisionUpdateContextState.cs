using System;

namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Search模式下，处理Tail冲突状态
	/// </summary>
	internal class SearchTailCollisionUpdateContextState : TailCollisionUpdateContextState
	{
		public override void HandleCollisionTerm(Context context)
		{
			int head = -context.Trie.Dat[context.CurPosition].Base;
			for (int i = context.CurrentCharIndex+1; i < context.Text.Length; i++)
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
			context.Finish();
			context.BeInsertedFlag = true;
			context.BeInsertedPosition = context.CurPosition;
			context.Item = context.Trie.Dat[context.CurPosition];
		}

		public override void HandleInsertedTerm(Context context)
		{
			context.Finish();
			context.BeInsertedFlag = true;
			context.BeInsertedPosition = context.CurPosition;
			context.Item = context.Trie.Dat[context.CurPosition];
		}

		public override void HandleNewTerm(Context context)
		{
			context.Finish();
			context.BeInsertedFlag = false;
			context.BeInsertedPosition = -1;
		}
	}
}