using System.Collections.Generic;
using BasicUnit;

namespace NestedClassificationFramework.ClassificationCalculator
{
    /// <summary>
    ///		分类器的接口
    /// </summary>
    public interface IClassificationCalculator
    {
		/// <summary>
		///		分类器的置信度
		/// </summary>
		int CalculatorConfidence { get; }

        /// <summary>
        ///		分类器需要实现如下的接口，输入是一个句子，返回为其结果。
        /// </summary>
        /// <returns>分类结果</returns>
        Dictionary<string, double> Calculate(Sentence sentence);
        
        CalculateResult CalculateWithRule(Sentence sentence);
    }
}
