using System.Collections.Generic;

namespace NestedClassificationFramework.ResultUpdation
{
	/// <summary>
	///		“一元文法”更新策略：即不考虑之前的分类结果，仅计算当前的结果
	/// </summary>
	public class OneGramResultUpdation : IResultUpdation
	{
		public ClassificationResult UpdateResult(ClassificationResult lastResult, ClassificationResult currentResult)
		{
			return currentResult;
		}
	}

	/// <summary>
	///		"二元文法"更新策略：考虑当前状态和上一个状态的结果，并汇总成最终结果
	/// </summary>
	public class BiGramResultUpdation : IResultUpdation
	{
		public ClassificationResult UpdateResult(ClassificationResult lastResult, ClassificationResult currentResult)
		{
			Dictionary<string, double> last = lastResult.TypeList;
			Dictionary<string, double> current = currentResult.TypeList;
			Dictionary<string, double> retList = new Dictionary<string, double>();
			double sum = 0;
			var ret = new ClassificationResult();
			foreach (var item in current)
			{
				string key = item.Key;
				if (last.ContainsKey(key))
				{
					double value = (current[key] + last[key]) / 2;
					retList.Add(key, value);
					sum += value;
				}
				else
				{
					double value = current[key] / 2;
					retList.Add(key, value);
					sum += value;
				}
			}
			foreach (KeyValuePair<string, double> pair in retList)
			{
				double value = sum < 1 ? 0 : pair.Value / sum;
				ret.Put(pair.Key, value * 100);
			}
			return ret;
		}
	}
}
