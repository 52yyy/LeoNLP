namespace WordSegment
{
    public class CharacterUtil
    {
        public static string ReSkip = @"(\\d+\\.\\d+|[0-9\.]+|[a-zA-Z]+|[0-9]+)";
        private static readonly char[] Connectors = { '+', '#', '&', '.', '_', '-' };
        /// <summary>
        ///		是否为汉字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsChineseLetter(char ch)
        {
            if (ch >= 0x4E00 && ch <= 0x9FA5)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///		是否为英文字母
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsEnglishLetter(char ch)
        {
            if ((ch >= 0x0041 && ch <= 0x005A) || (ch >= 0x0061 && ch <= 0x007A))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///		是否为数字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsDigit(char ch)
        {
            if (ch >= 0x0030 && ch <= 0x0039)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///		是否是Connector
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsConnector(char ch)
        {
            foreach (char connector in Connectors)
            {
                if (ch == connector)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///		查找匹配的Char
        /// </summary>
        /// <param name="ch">输入的字符</param>
        /// <returns></returns>
        public static bool CharFind(char ch)
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
        ///		全角转半角、大写转小写
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


