using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordSegment
{
	public class JiebaSegmenter
	{
		private readonly WordSegmentDictionary _wordDictionary;

		private readonly FinalSeg _finalSeg;

		public JiebaSegmenter(string modelPath)
		{
			_wordDictionary = new WordSegmentDictionary(modelPath);
			_finalSeg = new FinalSeg(modelPath);
		}

		/// <summary>
		/// 生成DAG图
		/// </summary>
		/// <param name="sentence">句子字符串</param>
		/// <returns>包含所有路径的Hashtable</returns>
		private IDictionary<int, List<int>> CreateDAG(String sentence)
		{
			var dag = new Dictionary<int, List<int>>();
			//获取词典
			TrieDictionary trie = _wordDictionary.GetTrie();
			//获取字符数组
			char[] charArray = sentence.ToCharArray();
			var sentenceLength = charArray.Length;
			int startCharIndex = 0, endCharIndex = 0;
			while (startCharIndex < sentenceLength)
			{
				Hit hit = trie.Match(charArray, startCharIndex, endCharIndex - startCharIndex + 1);
				if (hit.IsPrefix() || hit.IsMatch())
				{
					if (hit.IsMatch())
					{
						if (!dag.ContainsKey(startCharIndex))
						{
							var pathList = new List<int> { endCharIndex };
							dag.Add(startCharIndex, pathList);
						}
						else
						{
							((List<int>)dag[startCharIndex]).Add(endCharIndex);
						}
					}
					endCharIndex += 1;
					if (endCharIndex >= sentenceLength)
					{
						startCharIndex += 1;
						endCharIndex = startCharIndex;
					}
				}
				else
				{
					startCharIndex += 1;
					endCharIndex = startCharIndex;
				}
			}
			for (startCharIndex = 0; startCharIndex < sentenceLength; ++startCharIndex)
			{
				if (!dag.ContainsKey(startCharIndex))
				{
					dag.Add(startCharIndex, new List<int>() { startCharIndex });
				}
			}
			return dag;
		}

		private IDictionary<int, Pair<int>> Calc(String sentence, IDictionary<int, List<int>> dag)
		{
			var sentenceLength = sentence.Length;
			IDictionary<int, Pair<int>> route = new Dictionary<int, Pair<int>> { { sentenceLength, new Pair<int>(0, 0.0) } };
			for (var pointIndex = sentenceLength - 1; pointIndex > -1; pointIndex--)
			{
				Pair<int> candidate = null;
				foreach (var x in (List<int>)dag[pointIndex])
				{
					var tmpPair = (Pair<int>)route[x + 1];

					var freq = _wordDictionary.GetFreq(sentence.Substring(pointIndex, 1)) + tmpPair.Freq;
					if (candidate == null)
					{
						candidate = new Pair<int>(x, freq);
					}
					else if (candidate.Freq < freq)
					{
						candidate.Freq = freq;
						candidate.Key = x;
					}
				}
				route.Add(pointIndex, candidate);
			}
			return route;
		}

		internal static readonly Regex RegexChineseDefault = new Regex(@"([\u4E00-\u9FA5a-zA-Z0-9+#&\._]+)", RegexOptions.Compiled);
		internal static readonly Regex RegexSkipDefault = new Regex(@"(\r\n|\s)", RegexOptions.Compiled);
		public IEnumerable<SegToken> Process(string paragraph, SegMode mode)
		{
			if (mode == SegMode.Search)
			{
				int start = 0;
				Regex reHan = RegexChineseDefault;
				Regex reSkip = RegexSkipDefault;
				List<SegToken> segTokens = new List<SegToken>();

				string[] blocks = reHan.Split(paragraph);

				foreach (string blk in blocks)
				{
					if (string.IsNullOrWhiteSpace(blk))
					{
						continue;
					}

					if (reHan.IsMatch(blk))
					{
						foreach (string word in SentenceProcess(blk))
						{
							int width = word.Length;
							if (width == 0) continue;
							segTokens.Add(new SegToken(word, _wordDictionary.GetTag(word), start, start + width));
							start += width;
						}
					}
					else
					{
						string[] tmp = reSkip.Split(blk);
						foreach (string x in tmp)
						{
							string temp = x.Trim();
							int width = temp.Length;
							if (width == 0) continue;
							segTokens.Add(new SegToken(temp, _wordDictionary.GetTag(temp), start, start + width));
							start += width;
						}
					}
				}
				return segTokens;
			}
			else
			{
				throw new ArgumentException("SegMode should be SegMode.Search ...");
			}
		}

		public IEnumerable<string> SentenceProcess(string sentence)
		{
			var tokenList = new List<string>();
			var sentenceLength = sentence.Length;
			//获取所有可能路径
			var dag = CreateDAG(sentence);
			//选取最优路径
			var route = Calc(sentence, dag);
			Pair<int> tmpPair;

			int x = 0;
			int y = 0;
			string buf = string.Empty;
			while (x < sentenceLength)
			{
				tmpPair = (Pair<int>)route[x];
				y = tmpPair.Key + 1;
				string lWord = sentence.Substring(x, y - x);
				if (y - x == 1)
				{
					buf += lWord;
				}
				// 处理未登录词
				else
				{
					if (buf.Length > 0)
					{
						if (buf.Length == 1)
						{
							tokenList.Add(buf);
							buf = string.Empty;
						}
						else
						{
							if (_wordDictionary.ContainsWord(buf))
							{
								tokenList.Add(buf);
							}
							else
							{
								_finalSeg.Cut(buf, tokenList);
							}
							buf = string.Empty;
						}
					}
					tokenList.Add(lWord);
				}
				x = y;
			}
			if (buf.Length > 0)
			{
				if (buf.Length == 1)
				{
					tokenList.Add(buf);
					buf = string.Empty;
				}
				else
				{
					if (_wordDictionary.ContainsWord(buf))
					{
						tokenList.Add(buf);
					}
					else
					{
						_finalSeg.Cut(buf, tokenList);
					}
					buf = string.Empty;
				}

			}
			return tokenList;
		}


		/// <summary>
		///		加载用户词典
		/// </summary>
		/// <param name="dictPath"></param>
		/// <returns></returns>
		public bool ImportUserDict(string dictPath)
		{
			_wordDictionary.ImportUserDict(dictPath);
			return true;
		}
	}
}
