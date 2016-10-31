using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		DAT 字典树
	/// </summary>
	public class DATrie
	{
		private IDATrieNodeInstanceCreator _creator;
		private readonly DoubleArrayTrie _trie;

		/// <summary>
		///		新建一个空的DAT树
		/// </summary>
		public DATrie()
			: this(new DefaultNodeInstanceCreator())
		{
		}

		/// <summary>
		///		新建一个自定义Item类型的DAT树，自定义Item时传入ItemCreator
		/// </summary>
		/// <param name="creator"></param>
		public DATrie(IDATrieNodeInstanceCreator creator)
		{
			this._creator = creator;
			this._trie = new DoubleArrayTrie(creator);
		}

		/// <summary>
		///		从磁盘中读取一个DAT树，如果路径不正确，则提示错误并新建一个空的DAT树
		/// </summary>
		/// <param name="path"></param>
		public DATrie(string path)
			: this(path, new DefaultNodeInstanceCreator())
		{
		}

		/// <summary>
		///		从磁盘中读取一个自定义Item类型的DAT树，自定义Item时传入ItemCreator
		/// </summary>
		/// <param name="path"></param>
		/// <param name="creator"></param>
		public DATrie(string path, IDATrieNodeInstanceCreator creator)
		{
			this._creator = creator;
			this._trie = new DoubleArrayTrie(creator);
			LoadFromFile(path);
		}

		/// <summary>
		///		添加一个词
		/// </summary>
		/// <param name="word"></param>
		public int Index(string word)
		{
			DATrieNode item = _creator.CreateNewDoubleArrayTrieNode(word, null);
			return this.Index(item);
		}

		/// <summary>
		///		添加一个词
		/// </summary>
		/// <param name="word"></param>
		public int Index(DATrieNode word)
		{
			return this._trie.Index(word);
		}

		/// <summary>
		///		添加多个词
		/// </summary>
		/// <param name="words"></param>
		public void BulkIndex(IEnumerable<string> words)
		{
			foreach (string word in words)
			{
				this.Index(word);
			}
		}

		/// <summary>
		///		删除一个词
		/// </summary>
		/// <param name="word"></param>
		public int Delete(string word)
		{
			DATrieNode item = _creator.CreateNewDoubleArrayTrieNode(word, null);
			return this.Delete(item);
		}

		/// <summary>
		///		删除一个词
		/// </summary>
		/// <param name="word"></param>
		public int Delete(DATrieNode word)
		{
			return this._trie.Delete(word);
		}

		/// <summary>
		///		查询词是否存在
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public DATrieNode Search(string word)
		{
			DATrieNode item = _creator.CreateNewDoubleArrayTrieNode(word, null);
			return this.Search(item);
		}

		/// <summary>
		///		查询词是否存在
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public DATrieNode Search(DATrieNode word)
		{
			int index = this._trie.Search(word);
			if (index == -1)
			{
				return null;
			}
			return _trie.Dat[index];
		}

		/// <summary>
		///		保存DAT树到磁盘指定路径下
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool SaveToFile(string path)
		{
			try
			{
				StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
				sw.WriteLine("@item");
				for (int i = 0; i < _trie.Dat.Length; i++)
				{
					if (_trie.Dat[i] == null)
					{
						continue;
					}
					int bi = _trie.Dat[i].Base;
					int ci = _trie.Dat[i].Check;
					if (bi == 0 && ci == 0)
					{
						continue;
					}
					sw.WriteLine(_trie.Dat[i].ToText());
				}
				sw.WriteLine("@tail");
				for (int i = 0; i < _trie.Tail.Length; i++)
				{
					char c = _trie.Tail[i];
					if (c != '\0')
					{
						sw.WriteLine("T\t{0}\t{1}", i, c);
					}
				}
				sw.Close();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private void LoadFromFile(string path)
		{
			using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					if (line == "@item")
					{
						break;
					}
				}
				while ((line = sr.ReadLine()) != null)
				{
					if (line == "@tail")
					{
						break;
					}
					string[] segments = line.Split('\t');
					DATrieNode tmp = _creator.CreateNewDoubleArrayTrieNode(0);
					tmp.InitValue(segments);
					while (_trie.Dat.Length <= tmp.Index)
					{
						_trie.ExtendArray();
					}
					_trie.Dat[tmp.Index] = tmp;
				}
				while ((line = sr.ReadLine()) != null)
				{
					string[] segments = line.Split('\t');
					int tailIndex = int.Parse(segments[1]);
					while (_trie.Tail.Length <= tailIndex)
					{
						_trie.ExtendTail();
					}
					_trie.Tail[tailIndex] = char.Parse(segments[2]);
				}
			}
		}
	}
}