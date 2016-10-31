using System;

namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Index模式下，处理Tail冲突状态
	/// </summary>
	internal class IndexTailCollisionUpdateContextState : TailCollisionUpdateContextState
	{
		public override void HandleCollisionTerm(Context context)
		{
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}
			int head = -context.Trie.Dat[context.CurPosition].Base;

			int availBase = context.Trie.XCheck(new int[] { context.Text[context.CurrentCharIndex + 1] });
			context.Trie.Dat[context.CurPosition].Base = availBase;
			context.Trie.Dat[context.CurPosition].Status = 1;
			string lastWord = context.Trie.Dat[context.CurPosition].Word;
			object lastObj = context.Trie.Dat[context.CurPosition].Value;
			context.Trie.Dat[context.CurPosition].Word = null;
			context.Trie.Dat[context.CurPosition].Value = null;

			if (context.Trie.Dat[availBase +context.Text[context.CurrentCharIndex + 1]] == null)
			{
				context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]] =
					context.Trie.Creator.CreateNewDoubleArrayTrieNode(availBase + context.Text[context.CurrentCharIndex + 1]);
			}
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Base = -(head + 1);
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Check = context.CurPosition;
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Status = 3;
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Word = lastWord;
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Value = lastObj;
			context.PrePosition = context.CurPosition;
			context.CurrentCharIndex++;
			context.State = new GCheckContextState();
		}

		public override void HandleInsertedTerm(Context context)
		{
			context.Finish();
			context.BeInsertedFlag = false;
			context.BeInsertedPosition = -1;
			Console.WriteLine("已经存在");
		}

		public override void HandleNewTerm(Context context)
		{
			if (context.Trie.Dat[context.CurPosition]==null)
			{
				context.Trie.Dat[context.CurPosition] = context.Trie.Creator.CreateNewDoubleArrayTrieNode(context.CurPosition);
			}
			int head = -context.Trie.Dat[context.CurPosition].Base;

			int availBase =
				context.Trie.XCheck(
					new int[]
					{
						context.Text[context.CurrentCharIndex + 1],
						context.Trie.Tail[head]
					});
			context.Trie.Dat[context.CurPosition].Base = availBase;
			string lastWord = context.Trie.Dat[context.CurPosition].Word;
			object lastObj = context.Trie.Dat[context.CurPosition].Value;
			context.Trie.Dat[context.CurPosition].Word = null;
			context.Trie.Dat[context.CurPosition].Value = null;
			context.Trie.Dat[context.CurPosition].Status = 1;

			if (context.Trie.Dat[availBase + context.Trie.Tail[head]]==null)
			{
				context.Trie.Dat[availBase + context.Trie.Tail[head]] =
					context.Trie.Creator.CreateNewDoubleArrayTrieNode(availBase + context.Trie.Tail[head]);
			}
			context.Trie.Dat[availBase +context.Trie.Tail[head]].Check = context.CurPosition;
			context.Trie.Dat[availBase + context.Trie.Tail[head]].Word = lastWord;
			context.Trie.Dat[availBase + context.Trie.Tail[head]].Value = lastObj;
			context.Trie.Dat[availBase + context.Trie.Tail[head]].Status = 3;

			if (context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]] == null)
			{
				context.Trie.Dat[availBase +context.Text[context.CurrentCharIndex + 1]] =
					context.Trie.Creator.CreateNewDoubleArrayTrieNode(availBase + context.Text[context.CurrentCharIndex + 1]);
			}
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Check = context.CurPosition;
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Word = context.Item.Word;
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Value = context.Item.Value;
			context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Status = 3;

			if (context.Trie.Tail[head] == context.Trie.EndChar)
			{
				context.Trie.Dat[availBase + context.Trie.Tail[head]].Base = 0;
			}
			else
			{
				context.Trie.Dat[availBase +context.Trie.Tail[head]].Base = -(head + 1);
			}

			if (context.Text[context.CurrentCharIndex + 1] == context.Trie.EndChar)
			{
				context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Base = 0;
			}
			else
			{
				context.Trie.Dat[availBase + context.Text[context.CurrentCharIndex + 1]].Base = -context.Trie.TailPosition;
			}

			context.Trie.TailPosition = context.Trie.CopyToTailArray(context.Text, context.CurrentCharIndex + 2);
			context.Finish();
			context.BeInsertedFlag = true;
			context.BeInsertedPosition = context.CurPosition;
		}
	}
}