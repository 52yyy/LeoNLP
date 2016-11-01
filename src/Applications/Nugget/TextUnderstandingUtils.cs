using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using BasicUnit;

namespace Nugget
{
	public class TextUnderstandingUtils : NuggetExtracter
	{
		private static readonly char[] nuggetNormSyms = "“'\"”'《》 []【】.,()（）".ToCharArray();

		public static NuggetCollection GetExtendedDocData(
			string name,
			string sentence,
			string regexpattern,
			string repalcePattern,
			bool ignoreCase)
		{
			Nugget[] matchedStrings = GetMatchedStrings(sentence, regexpattern, repalcePattern, ignoreCase);
			NuggetCollection result;
			if (matchedStrings != null && matchedStrings.Length > 0)
			{
				result = new NuggetCollection { Nuggets = matchedStrings.ToList() };
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static Nugget[] GetMatchedStrings(
			string sentence,
			string regexpattern,
			string normPattern,
			bool ignoreCase)
		{
			Regex regex;
			if (ignoreCase)
			{
				regex = new Regex(regexpattern, RegexOptions.IgnoreCase);
			}
			else
			{
				regex = new Regex(regexpattern);
			}
			string[] groupNames = regex.GetGroupNames();
			MatchCollection matchCollection = regex.Matches(sentence);
			var list = new List<Nugget>();
			if (matchCollection.Count > 0)
			{
				for (int i = 0; i < matchCollection.Count; i++)
				{
					string text = matchCollection[i].Value.Trim();
					if (string.IsNullOrEmpty(text))
					{
						continue;
					}
					if (text.Length < 2) // 限制nugget的长度不能小于2个字
					{
						continue;
					}
					if (text.Length >= 32) // 限制nugget的长度不能大于32个字
					{
						continue;
					}
					if (string.IsNullOrEmpty(normPattern))
					{
						string tmp = matchCollection[i].Value;
						list.Add(
							new Nugget
							{
								Content = text,
								Start = matchCollection[i].Index + 1,
								LeftSign = tmp[0],
								RightSign = tmp[tmp.Length - 1]
							});
					}
					else
					{
						text = normPattern;
						string[] array = groupNames;
						for (int j = 0; j < array.Length; j++)
						{
							string text2 = array[j];
							if (matchCollection[i].Groups[text2] != null)
							{
								text = text.Replace("${" + text2 + "}", matchCollection[i].Groups[text2].Value);
							}
						}
						list.Add(new Nugget { Content = text, Start = matchCollection[i].Index });
					}
				}
			}
			return list.ToArray();
		}

		public static NuggetCollection GetNugget(string s)
		{
			string regexpattern =
				"“((?!”).)*”|\"((?!\").)*\"|\\s'((?!').)*'|《((?!》).)*》|\\[((?!\\]).)*\\]|【((?!】).)*】|\\(((?!\\)).)*\\)|（((?!）).)*）";
			NuggetCollection extendedDocData = GetExtendedDocData("Nugget", s, regexpattern, string.Empty, true);
			if (extendedDocData != null && extendedDocData.Nuggets != null)
			{
				int num = extendedDocData.Nuggets.Count;
				var list = new List<Nugget>();
				for (int i = 0; i < num; i++)
				{
					Nugget nuggetWord = extendedDocData.Nuggets[i];
					nuggetWord.Content = nuggetWord.Content.Trim(nuggetNormSyms).ToLower();
					nuggetWord.Content = NormPhrase(nuggetWord.Content);
					string text = nuggetWord.Content;
					if (!string.IsNullOrEmpty(text) && text != "true" && text != "img" && text != "timg" && text != "微博")
					{
						list.Add(nuggetWord);
					}
				}
				if (list.Count > 0)
				{
					extendedDocData.Nuggets = list;
				}
				else
				{
					extendedDocData = null;
				}
			}
			return extendedDocData;
		}

		public static bool IsChinese(char c)
		{
			return (c >= '一' && c <= '鿿') || (c >= '㐀' && c <= '䶵');
		}

		public static string MergeWords(string[] terms)
		{
			var stringBuilder = new StringBuilder();
			char c = '\0';
			for (int i = 0; i < terms.Length; i++)
			{
				string text = terms[i];
				if (text.Length != 0)
				{
					if (c != '\0' && !IsChinese(c) && !IsChinese(text[0]))
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(text);
					c = text[text.Length - 1];
				}
			}
			return stringBuilder.ToString();
		}

		public static string NormPhrase(string s)
		{
			string result;
			if (string.IsNullOrEmpty(s))
			{
				result = s;
			}
			else
			{
				string[] terms = s.Split(new char[0]);
				result = MergeWords(terms);
			}
			return result;
		}

		public static List<NuggetCollection> ParseAddData(string sentence, Dictionary<string, TextPattern> patterns)
		{
			var list = new List<NuggetCollection>();
			List<NuggetCollection> result;
			if (string.IsNullOrEmpty(sentence))
			{
				result = list;
			}
			else
			{
				foreach (var current in patterns)
				{
					NuggetCollection extendedDocData = GetExtendedDocData(
						current.Key,
						sentence,
						current.Value.Pattern,
						current.Value.NormPattern,
						current.Value.IgnoreCase);
					if (extendedDocData != null)
					{
						list.Add(extendedDocData);
					}
				}
				result = list;
			}
			return result;
		}

		public override NuggetCollection GetNuggets(string str)
		{
			NuggetCollection nuggetDetailInfo = GetNugget(str);
			if (nuggetDetailInfo == null)
			{
				nuggetDetailInfo = new NuggetCollection();
			}
			return nuggetDetailInfo;
		}

		public override NuggetCollection GetNuggets(Sentence str)
		{
			return GetNuggets(str.ToString());
		}
	}
}