using System.Collections.Generic;

namespace LDA
{
    public class Utils
    {
        public static char[] Spliter = {' '};

        public static List<object> WordFilter(string[] words)
        {
            var wordlist = new List<object>();
            foreach (var word in words)
            {
                if (word.Trim().Length > 1)
                {
                    wordlist.Add(word);
                }
            }
            return wordlist;
        }
    }
}
