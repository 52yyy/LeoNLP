using System.Collections.Generic;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		标签帮助类
	/// </summary>
	internal static class TagHelper
	{
		private static Dictionary<string, string> _tagDictionary;

		static TagHelper()
		{
			_tagDictionary = new Dictionary<string, string>();
			_tagDictionary.Add("T", "Target");
			_tagDictionary.Add("F", "Feature");
			_tagDictionary.Add("M", "Modify");
			_tagDictionary.Add("O", "Opinion");
		}

		/// <summary>
		///		根据模型中的标记返回标签名称
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetTag(string key)
		{
			string tag;
			if (_tagDictionary.TryGetValue(key, out tag))
			{
				return tag;
			}
			else
			{
				return null;
			}
		}
	}
}