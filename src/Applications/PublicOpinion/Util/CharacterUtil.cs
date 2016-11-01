using System.Linq;

namespace PublicOpinion.Util
{
	public class CharacterUtil
	{
		//public static Regex reSkip = new Regex("(\\d+\\.\\d+|[a-zA-Z0-9]+)");
		public static string ReSkip = @"(\d+\.\d+|[a-zA.-Z0-9]+)";

		private static char[] connectors = new char[] { '+', '#', '&', '.', '_', '-' };

		public static bool IsChineseLetter(char ch)
		{
			return ch >= 0x4E00 && ch <= 0x9FA5;
		}

		public static bool IsEnglishLetter(char ch)
		{
			return (ch >= 0x0041 && ch <= 0x005A) || (ch >= 0x0061 && ch <= 0x007A);
		}

		public static bool IsDigit(char ch)
		{
			return ch >= 0x0030 && ch <= 0x0039;
		}

		public static bool IsConnector(char ch)
		{
			return connectors.Any(connector => ch == connector);
		}

		/// <summary>
		/// 查找匹配的Char
		/// </summary>
		/// <param name="ch">输入的字符</param>
		/// <returns></returns>
		public static bool CcFind(char ch)
		{
			if (IsChineseLetter(ch))
			{
				return true;
			}
			if (IsEnglishLetter(ch))
			{
				return true;
			}
			if (IsDigit(ch))
			{
				return true;
			}
			if (IsConnector(ch))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 全角转半角、大写转小写
		/// </summary>
		/// <param name="input">输入的字符</param>
		/// <returns>转换后的字符</returns>
		public static char Regularize(char input)
		{
			if (input == 12288)
			{
				input = (char)32;
			}
			if (input > 65280 && input < 65375)
			{
				input = (char)(input - 65248);
			}

			return input;
		}
	}
}
