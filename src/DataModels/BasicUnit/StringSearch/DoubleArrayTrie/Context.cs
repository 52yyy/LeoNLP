using System.Security.Principal;

namespace BasicUnit.DoubleArrayTrie
{
	internal class Context
	{
		/// <summary>
		///		����Ƿ����
		/// </summary>
		private bool _isFinish;

		/// <summary>
		///		����ĵ���
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///		�����Item
		/// </summary>
		public DATrieNode Item { get; set; }

		/// <summary>
		///		ǰһ��λ��
		/// </summary>
		public int PrePosition { get; set; }

		/// <summary>
		///		��ǰλ��
		/// </summary>
		public int CurPosition { get; set; }

		/// <summary>
		///		��ǰChar������
		/// </summary>
		public int CurrentCharIndex { get; set; }

		/// <summary>
		///		�ôʴ��ڵ�״̬
		/// </summary>
		public TrieState TrieState { get; set; }

		public DoubleArrayTrie Trie { get; set; }

		public ContextState State { get; set; }

		public Context(string text, ContextState state, DoubleArrayTrie trie, TrieState trieState)
		{
			DATrieNode tpmItem = trie.Creator.CreateNewDoubleArrayTrieNode(0);
			tpmItem.Init(text, null);
			this.Text = text + trie.EndChar;
			this.Item = tpmItem;
			this.PrePosition = 1;
			this.CurPosition = 0;
			this.State = state;
			this.Trie = trie;
			this.TrieState = trieState;
		}

		public Context(DATrieNode item, ContextState state, DoubleArrayTrie trie, TrieState trieState)
		{
			this.Text = item.Word + trie.EndChar;
			this.Item = item;
			this.PrePosition = 1;
			this.CurPosition = 0;
			this.State = state;
			this.Trie = trie;
			this.TrieState = trieState;
		}

		public bool BeInsertedFlag { get; set; }

		public int BeInsertedPosition { get; set; }

		internal void Finish()
		{
			this._isFinish = true;
		}

		public void BeInserted()
		{
			while (!this._isFinish)
			{
				this.State.BeInserted(this);
			}
		}
	}
}