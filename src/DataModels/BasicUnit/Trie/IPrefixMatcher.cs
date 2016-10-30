using System;
using System.Collections.Generic;

namespace BasicUnit
{
	public interface IPrefixMatcher<V> where V : class
    {
        String GetPrefix();

		void BackMatch();
        char LastMatch();
        bool NextMatch(char next);
        List<V> GetPrefixMatches();
        bool IsExactMatch();

		bool IsLeaf();
        V GetExactMatch();
    }
}