using System.Collections.Generic;
using System.Text.RegularExpressions;

using PublicOpinion.DataModel;

namespace PublicOpinion.Normalization
{
	/// <summary>
	///		汽车之家正规化
	/// </summary>
	public class AutohomeNormalizer : IPomNormalizable
	{
		private static List<string> _orientDict = new List<string> { "最满意的一点", "最不满意的一点" }; 
		private static List<string> _featureDict = new List<string> {"空间","动力","操控","油耗","舒适性","外观","内饰","性价比"};   // 汽车之家口碑论坛的属性标记集合
		private static Regex _splitRegex = new Regex("【([^【]+)(?!】)");
		private static Regex _extractRegex = new Regex("【([\\w。？]+)】");

		/// <summary>
		///		正规化方法，将从外部获取的原始文本转化成POM格式的数据流
		/// </summary>
		/// <param name="rawText">原始文本</param>
		/// <returns></returns>
		public IEnumerable<PomSentence> Normalize(string rawText)
		{
			MatchCollection matchs = _splitRegex.Matches(rawText);

			foreach (Match match in matchs)
			{
				string tmp = match.Value;
				Match m = _extractRegex.Match(tmp);
				string metainfo = m.Groups[1].Value;
				if (string.IsNullOrEmpty(metainfo))
				{
					continue;
				}
				PomMeta meta = new PomMeta();
				if (_orientDict.Contains(metainfo))
				{
					meta.Orient = metainfo;
				}
				else if (_featureDict.Contains(metainfo))
				{
					meta.Feature = metainfo;
				}

				string s = _extractRegex.Replace(tmp, "");
				PomSentence pomSentence = new PomSentence();
				pomSentence.Meta = meta;
				pomSentence.Content = s;
				yield return pomSentence;
			}
		}
	}
}