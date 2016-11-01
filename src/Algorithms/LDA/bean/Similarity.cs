using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
    public class Similarity
    {
        private readonly char[] _sp = { ' ' };
        public Hashtable Wordset;


        public Similarity()
        {
            Wordset = new Hashtable();
        }
        public double GetSimilarity(string wordStr, string sentence)
        {
            if (sentence.Trim().Length == 0)
            {
                return 0;
            }
            IniWordSet(wordStr);

            IniWordSet(sentence);

            double sqdoc1 = 0;
            double sqdoc2 = 0;
            double denominator = 0;

            foreach (DictionaryEntry de in Wordset)
            {
                int[] c = (int[])de.Value;
                denominator += c[0] * c[1];
                sqdoc1 += c[0] * c[0];
                sqdoc2 += c[1] * c[1];
            }

            return denominator / Math.Sqrt(sqdoc1 * sqdoc2);

        }

        private void IniWordSet(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str.ToCharArray()[i];
                if (IsHanZi(ch))
                {
                    int charIndex = GetGbk(ch);

                    if (charIndex != -1)
                    {
                        int[] fq;
                        if (Wordset.ContainsKey(charIndex))
                        {
                            fq = (int[])Wordset[charIndex];
                            fq[0]++;
                        }
                        else
                        {
                            fq = new int[2];
                            fq[0] = 1;
                            fq[1] = 0;
                            Wordset.Add(charIndex, fq);
                        }
                    }
                }
            }
        }

        public bool IsHanZi(char ch)
        {
            return (ch >= 0x4E00 && ch <= 0x9FA5);
        }

        public short GetGbk(char ch)
        {

            byte[] buffer = Encoding.GetEncoding("GBK").GetBytes(ch.ToString());
            if (buffer.Length != 2)
            {
                return -1;
            }
            int b0 = (buffer[0] & 0x0FF) - 161;
            int b1 = (buffer[1] & 0x0FF) - 161;
            return (short)(b0 * 94 + b1);
        }
    }
}
