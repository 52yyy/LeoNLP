using System.Collections.Generic;

using BasicUnit;
using BasicUnit.Trie;

namespace WordSegment
{
	public class TrieDictionary
	{
		private Trie<WordAttribute> _trie;

		private List<WordAttribute> _list;

		public TrieDictionary()
		{
			this._trie = new Trie<WordAttribute>();
			this._list = new List<WordAttribute>();
		}

		public TrieDictionary(Trie<WordAttribute> trie, List<WordAttribute> list)
		{
			this._trie = trie;
			this._list = list;
		}

		public IPrefixMatcher<WordAttribute> CreateMatcher()
		{
			return this._trie.CreateMatcher();
		}

		public void Put(string word,WordAttribute attribute)
		{
			this._trie.Put(word, attribute);
			this._list.Add(attribute);
		}

		public IEnumerable<WordAttribute> GetWordAttributeEnumerable()
		{
			return _list;
		}

		public Hit Match(char[] charArray, int begin, int length)
		{
			return Match(charArray, begin, length, null);			
		}

		/// <summary>
		/// 匹配词段
		/// </summary>
		/// <param name="charArray"></param>
		/// <param name="begin"></param>
		/// <param name="length"></param>
		/// <param name="searchHit"></param>
		/// <returns></returns>
		public Hit Match(char[] charArray, int begin, int length, Hit searchHit)
		{
			if (searchHit == null)
			{
				// 如果hit为空，新建
				searchHit = new Hit();
				// 设置hit的其实文本位置
				searchHit.SetBegin(begin);
			}
			else
			{
				// 否则要将HIT状态重置
				searchHit.SetUnmatch();
			}
			// 设置hit的当前处理位置
			searchHit.SetEnd(begin);

			IPrefixMatcher<WordAttribute> matcher = CreateMatcher();
			int temp = 0;
			while (temp < length && matcher.NextMatch(charArray[begin + temp]))
			{
				temp++;
			}

			if (temp == length)
			{
				if (matcher.IsExactMatch()) // 完全匹配
				{
					searchHit.SetMatch();
				}
				if (!matcher.IsLeaf()) // 前缀匹配
				{
					searchHit.SetPrefix();
				}
			}
			// STEP3 没有找到DictSegment， 将HIT设置为不匹配
			return searchHit;
		}

		public bool ContainsWord(string word)
		{
			IPrefixMatcher<WordAttribute> matcher = CreateMatcher();
			int temp = 0;
			int length = word.Length;
			while (temp < length && matcher.NextMatch(word[temp]))
			{
				temp++;
			}

			return temp == length && matcher.IsExactMatch();
		}

		public WordAttribute HitWords(string word)
		{
			int temp = 0;
			int length = word.Length;
			IPrefixMatcher<WordAttribute> matcher = CreateMatcher();
			while (temp < length && matcher.NextMatch(word[temp]))
			{
				temp++;
			}

			if (matcher.IsExactMatch())
			{
				return matcher.GetExactMatch();
			}
			return null;
		}
	}
}
