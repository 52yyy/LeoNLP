using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using BasicUnit;

using Nugget.Properties;

namespace Nugget
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class NuggetExtracter
	{
		public abstract NuggetCollection GetNuggets(string str);

		public abstract NuggetCollection GetNuggets(Sentence str);
	}

	/// <summary>
	/// 
	/// </summary>
	public class NuggetExtracterByDictionary : NuggetExtracter
	{
		private const int MaxNuggetItemLen = 32;

		private readonly Dictionary<char, char> _puncsDic;

		private readonly Dictionary<char, char> _rPuncsDic;

		/// <summary>
		/// 
		/// </summary>
		public NuggetExtracterByDictionary()
		{
			this._puncsDic = new Dictionary<char, char>();
			this._rPuncsDic = new Dictionary<char, char>();

			foreach (string line in this.GetTextLines(Resources.PairSign))
			{
				string[] info = Regex.Split(line, "	");
				this._puncsDic.Add(info[0][0], info[1][0]);
				this._rPuncsDic.Add(info[1][0], info[0][0]);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="puncsDic"></param>
		/// <param name="rPuncsDic"></param>
		public NuggetExtracterByDictionary(Dictionary<char, char> puncsDic, Dictionary<char, char> rPuncsDic)
		{
			this._puncsDic = puncsDic;
			this._rPuncsDic = rPuncsDic;
		}

		/// <summary>
		///     该接口返回比较全面的nuget信息，包含pos。待用
		/// </summary>
		/// <param name="str"></param>
		/// <param name="hits"></param>
		/// <returns></returns>
		public int GetNuggets(string str, out List<Nugget> hits)
		{
			List<PuncPairInfo> pairInfos;
			List<PuncPairInfo> rpairInfos;
			this.GetInnerMatch(str, out pairInfos, out rpairInfos);
			hits = new List<Nugget>();
			foreach (PuncPairInfo hit1 in pairInfos)
			{
				foreach (PuncPairInfo hit2 in rpairInfos)
				{
					if (hit1.rWord != hit2.Word || (hit2.Pos - hit1.Pos < 1))
					{
						continue;
					}

					//find out，可能会有嵌套
					string word = str.Substring(hit1.Pos + 1, hit2.Pos - hit1.Pos - 1);

					// 如果word是空字符的话没有统计为nugget的意义
					if (string.IsNullOrEmpty(word.Trim()))
					{
						continue;
					}
					// 如果word是一个字的删除
					if (word.Length < 2)
					{
						continue;
					}
					// 如果没有中文也没有统计为Nugget的意义
					if (!CharacterChecker.CheckStringChineseUn(word))
					{
						continue;
					}
					if (word.Length <= MaxNuggetItemLen)
					{
						hits.Add(new Nugget(hit1.Pos, hit2.Pos, hit1.Word, hit2.Word, word));
					}
					break;
				}
			}
			return hits.Count;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public override NuggetCollection GetNuggets(string str)
		{
			List<PuncPairInfo> pairInfos;
			List<PuncPairInfo> rpairInfos;
			this.GetInnerMatch(str, out pairInfos, out rpairInfos);
			var nuggetDetailInfo = new NuggetCollection();
			foreach (PuncPairInfo hit1 in pairInfos)
			{
				foreach (PuncPairInfo hit2 in rpairInfos)
				{
					if (hit1.rWord != hit2.Word || (hit2.Pos - hit1.Pos < 1))
					{
						continue;
					}

					//find out，可能会有嵌套
					string word = str.Substring(hit1.Pos + 1, hit2.Pos - hit1.Pos - 1);

					// 如果word是空字符的话没有统计为nugget的意义
					if (string.IsNullOrEmpty(word.Trim()))
					{
						continue;
					}

					// 如果word是一个字的删除
					if (word.Length < 2)
					{
						continue;
					}
					// 如果没有中文也没有统计为Nugget的意义
					if (!CharacterChecker.CheckStringChineseUn(word))
					{
						continue;
					}

					if (word.Length <= MaxNuggetItemLen)
					{
						nuggetDetailInfo.Nuggets.Add(new Nugget(hit1.Pos + 1, hit2.Pos, hit1.Word, hit2.Word, word));
					}
					break;
				}
			}
			return nuggetDetailInfo;
		}

		public override NuggetCollection GetNuggets(Sentence str)
		{
			return GetNuggets(str.ToString());
		}

		private void GetInnerMatch(string str, out List<PuncPairInfo> hits, out List<PuncPairInfo> rHits)
		{
			hits = new List<PuncPairInfo>();
			rHits = new List<PuncPairInfo>();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				int temp = i;
				char ch = str[temp];
				if (this._puncsDic.ContainsKey(ch))
				{
					var info = new PuncPairInfo(i, ch, this._puncsDic[ch], 0);
					hits.Add(info);
				}
				else if (this._rPuncsDic.ContainsKey(ch))
				{
					var info = new PuncPairInfo(i, ch, this._rPuncsDic[ch], 1);
					rHits.Add(info);
				}
			}
			hits.Sort((item1, item2) => item1.Pos - item2.Pos);
		}

		private IEnumerable<string> GetTextLines(byte[] lineContentBytes)
		{
			string inputTextLines = Encoding.UTF8.GetString(lineContentBytes);

			var lineReader = new StringReader(inputTextLines);

			while (true)
			{
				string readLine = lineReader.ReadLine();

				if (string.IsNullOrEmpty(readLine))
				{
					yield break;
				}

				yield return readLine;
			}
		}
	}
}