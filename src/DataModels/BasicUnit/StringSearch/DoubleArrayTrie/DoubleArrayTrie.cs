using System;
using System.Collections.Generic;

namespace BasicUnit.DoubleArrayTrie
{
	internal class DoubleArrayTrie
	{
		public IDATrieNodeInstanceCreator Creator { get; private set; }

		public char EndChar = '#';

		private const int DefaultLength = 100000;

		public DoubleArrayTrie(IDATrieNodeInstanceCreator creator)
		{
			this.Creator = creator;
			this.TailPosition = 1;
			this.Tail = new char[DefaultLength];
			this.Dat = new DATrieNode[DefaultLength];
			this.Dat[1] = Creator.CreateNewDoubleArrayTrieNode(1);
			this.Dat[1].Base = 1;
		}

		public DATrieNode[] Dat { get; set; }

		public char[] Tail { get; set; }

		public int TailPosition { get; set; }

		public int CopyToTailArray(string str, int p)
		{
			int pos = this.TailPosition;
			while (str.Length - p + 1 > this.Tail.Length - this.TailPosition)
			{
				this.ExtendTail();
			}
			for (int i = p; i < str.Length; ++i)
			{
				this.Tail[pos] = str[i];
				pos++;
			}
			return pos;
		}

		/// <summary>
		///     删除一个词
		/// </summary>
		/// <param name="word"></param>
		public int Delete(DATrieNode word)
		{
			var context = new Context(word, new GCheckContextState(), this, TrieState.Delete);
			context.BeInserted();
			return context.BeInsertedPosition;
		}

		/// <summary>
		///		G函数，=BASE[n]+a
		/// </summary>
		/// <param name="currentPosition"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		public int GFunction(int currentPosition, char c)
		{
			if (this.Dat[currentPosition]==null)
			{
				this.Dat[currentPosition] = Creator.CreateNewDoubleArrayTrieNode(currentPosition);
			}
			int nextPosition = this.Dat[currentPosition].Base + c;
			if (nextPosition >= this.Dat.Length)
			{
				this.ExtendArray();
			}

			return nextPosition;
		}

		public List<int> GetChildList(int position)
		{
			var childList = new List<int>();
			for (int i = 1; i <= 65536; ++i)
			{
				if (this.Dat[position] == null)
				{
					this.Dat[position] = Creator.CreateNewDoubleArrayTrieNode(position);
				}
				if (this.Dat[position].Base + i >= this.Dat.Length)
				{
					break;
				}

				if (this.Dat[this.Dat[position].Base + i] == null)
				{
					this.Dat[this.Dat[position].Base + i] = Creator.CreateNewDoubleArrayTrieNode(this.Dat[position].Base + i);
				}
				if (this.Dat[this.Dat[position].Base + i].Check == position)
				{
					childList.Add(i);
				}
			}
			return childList;
		}

		/// <summary>
		///     添加新的词
		/// </summary>
		/// <param name="word"></param>
		public int Index(DATrieNode word)
		{
			if (word == null)
			{
				throw new Exception("item is null");
			}
			var context = new Context(word, new GCheckContextState(), this, TrieState.Index);
			context.BeInserted();
			return context.BeInsertedPosition;
		}

		/// <summary>
		///     查找新的词
		/// </summary>
		/// <param name="word"></param>
		public int Search(DATrieNode word)
		{
			if (word == null)
			{
				throw new Exception("item is null");
			}
			var context = new Context(word, new GCheckContextState(), this, TrieState.Search);
			context.BeInserted();
			return context.BeInsertedPosition;
		}

		/// <summary>
		///     X_CHECK(LIST), 它返回最小的q。
		///     q满足以下两个条件：
		///     1）q > 0
		///     2）对于在LIST中的所有字母c都满足：CHECK[q + c] = 0。q的值总是从1开始，并且每次只增值1。记住这个重要的条件，就是q要满足LIST中的所有字母
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		public int XCheck(int[] set)
		{
			for (int i = 1; ; ++i)
			{
				bool flag = true;
				for (int j = 0; j < set.Length; ++j)
				{
					int curPosition = i + set[j];
					if (curPosition >= this.Dat.Length)
					{
						this.ExtendArray();
					}
					if (this.Dat[curPosition]==null)
					{
						this.Dat[curPosition] = Creator.CreateNewDoubleArrayTrieNode(curPosition);
					}
					if (this.Dat[curPosition].Base != 0 || this.Dat[curPosition].Check != 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return i;
				}
			}
		}

		/// <summary>
		///		延长申请数组资源的长度
		/// </summary>
		public void ExtendArray()
		{
			DATrieNode[] tmp = new DATrieNode[this.Dat.Length + DefaultLength];
			this.Dat.CopyTo(tmp,0);
			this.Dat = tmp;
		}

		/// <summary>
		///		延长申请Tail资源的长度
		/// </summary>
		public void ExtendTail()
		{
			char[] tmp = new char[this.Tail.Length + DefaultLength];
			this.Tail.CopyTo(tmp, 0);
			this.Tail = tmp;
		}
	}
}