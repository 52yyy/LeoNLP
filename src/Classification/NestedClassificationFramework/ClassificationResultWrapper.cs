namespace NestedClassificationFramework
{
    /// <summary>
    ///	    分类结果的封装
    /// </summary>
    public class ClassificationResultWrapper
    {
        /// <summary>
        ///	    分类结果
        /// </summary>
        public ClassificationResult Result { get; private set; }

        /// <summary>
        ///	    分类结果是否满足CalculationRule
        /// </summary>
        public bool IsCalculationRuleSatisfied { get; private set; }

		/// <summary>
		///		计算器的置信度
		/// </summary>
		public int CalculatorConfidence { get; private set; }
		
	    /// <summary>
	    ///	   构造函数，把result和是否满足封装起来
	    /// </summary>
		public ClassificationResultWrapper(ClassificationResult result, bool isSatisfied, int calculatorConfidence)
	    {
		    this.Result = result;
		    this.IsCalculationRuleSatisfied = isSatisfied;
			this.CalculatorConfidence = calculatorConfidence;
	    }

	    public override string ToString()
        {
            return Result + " " + IsCalculationRuleSatisfied;
        }

    }
}
