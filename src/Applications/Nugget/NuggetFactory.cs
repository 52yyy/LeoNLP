using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

using Nugget.Properties;

namespace Nugget
{
	public class NuggetFactory
	{
		private static NuggetExtracter defaultNuggetExtracter;

		public static NuggetExtracter CreateDefaultNuggetExtracter()
		{
			if (defaultNuggetExtracter == null)
			{
				var builder = new NuggetExtracterBuilder();
				defaultNuggetExtracter = builder.BuildByTextLines(GetTextLines(Resources.PairSign));
			}

			return defaultNuggetExtracter;
		}

		public static NuggetExtracter CreateRegexNuggetExtracter()
		{
			if (defaultNuggetExtracter == null)
			{
				defaultNuggetExtracter = new TextUnderstandingUtils();
			}

			return defaultNuggetExtracter;
		}

		private static IEnumerable<string> GetTextLines(byte[] lineContentBytes)
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


		private class NuggetExtracterBuilder
		{
			public NuggetExtracter BuildByTextLines(IEnumerable<string> PairSign)
			{
				Dictionary<char, char> _puncsDic = new Dictionary<char, char>();
				Dictionary<char, char> _rPuncsDic = new Dictionary<char, char>();
				foreach (string line in PairSign)
				{
					string[] info = Regex.Split(line, "	");
					_puncsDic.Add(info[0][0], info[1][0]);
					_rPuncsDic.Add(info[1][0], info[0][0]);
				}
				return new NuggetExtracterByDictionary(_puncsDic, _rPuncsDic);
			}
		}
	}
}
