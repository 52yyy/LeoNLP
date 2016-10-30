using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordSegment
{
	public class WordSegmentDictionary
	{
		private string _modelPath;
		private double _total = 0.0;
		private static readonly char[] Sp = new char[] { ' ' };
		public const string Unkownword = "x";
		private double _minFreq = double.MaxValue;
		private const string MainDict = "dict.txt";

		public TrieDictionary Trie = new TrieDictionary();

		public WordSegmentDictionary(string modelPath)
		{
			this._modelPath = modelPath;
			LoadDict(modelPath);
        }

		public void LoadDict(string modelPath)
		{
			LoadCommonDictionary(Path.Combine(modelPath, MainDict), Encoding.UTF8);
			this.UpdateTrieWordWeight();
		}

		/// <summary>
		///		添加用户词典
		/// </summary>
		/// <param name="userDictFilename"></param>
		public void ImportUserDict(string userDictFilename)
		{
			this.LoadUserDictionary(userDictFilename, Encoding.UTF8);
			this.UpdateTrieWordWeight();
		}

		/// <summary>
		///		读取词典并填充Trie树
		/// </summary>
		/// <param name="userDict"></param>
		/// <param name="encoding"></param>
		/// <param name="trie"></param>
		public void LoadCommonDictionary(string userDict, Encoding encoding)
		{
			IEnumerable<string> lines = File.ReadLines(userDict, encoding);
			foreach (string line in lines)
			{
				string[] token = line.Split(Sp, StringSplitOptions.RemoveEmptyEntries);
				double freq;
				string word;
				string tag;
				switch (token.Length)
				{
					case 3:
						word = token[0];
						freq = double.Parse(token[1]);
						tag = token[2];
						_total += freq;
						break;
					case 2:
						word = token[0];
						tag = token[1];
						freq = double.Parse(20.ToString());
						_total += freq;
						break;
					case 1:
						word = token[0];
						tag = Unkownword;
						freq = double.Parse(20.ToString());
						_total += freq;
						break;
					default:
						continue;
				}
				WordAttribute attribute = new WordAttribute(freq, tag);
				Trie.Put(word, attribute);
			}
		}

		/// <summary>
		///		读取词典并填充Trie树
		/// </summary>
		/// <param name="userDict"></param>
		/// <param name="encoding"></param>
		/// <param name="trie"></param>
		public void LoadUserDictionary(string userDict, Encoding encoding)
		{
			IEnumerable<string> lines = File.ReadLines(userDict, encoding);
			foreach (string line in lines)
			{
				string[] token = line.Split(Sp, StringSplitOptions.RemoveEmptyEntries);
				double freq;
				string word;
				string tag;
				switch (token.Length)
				{
					case 3:
						word = token[0];
						freq = double.Parse(token[1]);
						tag = token[2];
						break;
					case 2:
						word = token[0];
						tag = token[1];
						freq = _total;
						break;
					case 1:
						word = token[0];
						tag = Unkownword;
						freq = _total;
						break;
					default:
						continue;
				}
				WordAttribute attribute = new WordAttribute(freq, tag);
				Trie.Put(word, attribute);
			}
		}

		/// <summary>
		///		更新Trie树中WordAttribute的Weight值
		/// </summary>
		private void UpdateTrieWordWeight()
		{
			double value;
			foreach (WordAttribute de in Trie.GetWordAttributeEnumerable())
			{
				value = Math.Log(de.Frequency / _total);
				de.Weight = value;
				_minFreq = Math.Min(value, _minFreq);
			}
		}

		public TrieDictionary GetTrie()
		{
			return Trie;
		}

		public bool ContainsWord(string word)
		{
			return Trie.ContainsWord(word);
		}

		private WordAttribute HitWords(string str)
		{
			return Trie.HitWords(str);
		}

		public double GetFreq(string key)
		{
			WordAttribute wa = HitWords(key);
			if (wa==null)
			{
				return _minFreq;
			}
			return wa.Weight;
		}

		public string GetTag(string key)
		{
			WordAttribute wa = HitWords(key);
			if (wa == null)
			{
				if (this.IsNumberTag(key))
				{
					return "m";
				}
				if (this.IsEnglishTag(key))
				{
					return "eng";
				}
				if (IsPunctuationTag(key))
				{
					return "w";
				}
				return Unkownword;
			}
			return wa.TokenType;
		}

		private bool IsNumberTag(string key)
		{
			return key.All(c => c <= 57 && c >= 48);
		}

		private bool IsEnglishTag(string key)
		{
			return key.All(c => c <= 122 && c >= 65 && (c >= 97 || c <= 90));
		}

		private bool IsPunctuationTag(string key)
		{
			if (key.Length == 1)
			{
				return true;
			}
			return false;
		}
	}
}
