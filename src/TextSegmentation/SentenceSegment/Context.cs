using System.Collections.Generic;
using System.Text;

namespace SentenceSegment
{
	/// <summary>
	///		上下文
	/// </summary>
	internal class Context
	{
		/// <summary>
		///		标记是否完成
		/// </summary>
		private bool _isFinish;

		public Context(string text, ContextState state)
		{
			this.Text = text;
			this.State = state;
			this.CurrentSentenceBuilder = new StringBuilder();
			this.Sentences = new List<string>();
		}

		/// <summary>
		///		输入的文本
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		///		上下文所处的状态
		/// </summary>
		public ContextState State { get; set; }

		/// <summary>
		///		当前位置的索引
		/// </summary>
		public int CurrentIndex { get; set; }

		/// <summary>
		///		当前位置的字符
		/// </summary>
		public char CurrentChar
		{
			get
			{
				return Text[CurrentIndex];
			}
		}

		/// <summary>
		///		引号匹配状态是否开启
		/// </summary>
		public bool IsPairSignOpened { get; set; }

		/// <summary>
		///		引号中有断句符号
		/// </summary>
		public bool HasSplitSign { get; set; }

		/// <summary>
		///		锁定的引号
		/// </summary>
		public char PairSign { get; set; }

		/// <summary>
		///		锁定的回引号
		/// </summary>
		public char BackPairSign { get; set; }

		/// <summary>
		///		当前句子
		/// </summary>
		public StringBuilder CurrentSentenceBuilder { get; set; }

		/// <summary>
		///		已经抽取出来的句子
		/// </summary>
		public List<string> Sentences { get; set; }

		internal void Finish()
		{
			this._isFinish = true;
		}

		internal void Execute()
		{
			while (!this._isFinish)
			{
				this.State.Execute(this);
			}
		}
	}
}