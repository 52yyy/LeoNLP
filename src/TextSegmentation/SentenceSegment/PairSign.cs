using System.Collections.Generic;

namespace SentenceSegment
{
	internal static class PairSign
	{
		public static readonly Dictionary<char, char> PairSigns = new Dictionary<char, char>();

		static PairSign()
		{
			PairSigns = new Dictionary<char, char>();
			PairSigns['"'] = '"';
			PairSigns['“'] = '”';
			PairSigns['《'] = '》';
			PairSigns['【'] = '】';
			PairSigns['('] = ')';
			PairSigns['['] = ']';
		}
	}
}