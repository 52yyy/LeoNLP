using System.Text.RegularExpressions;

namespace Nugget
{
	public class CharacterChecker
	{
		/// <summary>
		///		用ASCII码范围判断字符是不是汉字
		/// </summary>
		/// <param name="text">待判断的字符或者字符串</param>
		/// <returns></returns>
		public static bool CheckStringChinese(string text)
		{
			bool res = false;
			for (int i = 0; i < text.Length; i++)
			{
				if ((int)text[i] > 127)
				{
					res = true;
				}
			}
			return res;
		}

		/// <summary>
		///		用UNICODE编码范围判断字符是不是汉字
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool CheckStringChineseUn(string text)
		{
			char[] c = text.ToCharArray();
			bool res = false;
			for (int i = 0; i < c.Length; i++)
			{
				if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
				{
					res = true;
					break;
				}
			}
			return res;
		}

		/// <summary>
		///		用正则表达式判断字符是不是汉字
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool CheckStringChineseReg(string text)
		{
			bool res = Regex.IsMatch(text, @"[\u4e00-\u9fbb]+$");
			return res;
		}
	}
}
