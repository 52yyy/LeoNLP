using System;
using System.Collections.Generic;

namespace NestedClassificationFramework.ClassificationCalculator
{
    public class CalculateResult
    {
        public CalculateResult(Dictionary<string, double> dict, List<string> matchedRules)
        {
            Category = dict;
            MatchedRules = matchedRules;
        }

        public CalculateResult()
        {
            Category = new Dictionary<string, double>();
            MatchedRules = new List<string>();
        }

        public Dictionary<string, double> Category { get; set; }
        public List<String> MatchedRules { get; set; } 
    }
}
