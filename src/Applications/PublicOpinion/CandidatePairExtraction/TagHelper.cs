using System.Collections.Generic;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		��ǩ������
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
		///		����ģ���еı�Ƿ��ر�ǩ����
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