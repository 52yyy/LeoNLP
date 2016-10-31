using System;

namespace NestedClassificationFramework.CalculationRule
{
    /// <summary>
    ///		当存在一项的Confidence为100时，会判断为true
    /// </summary>
    class ConfidenceCalculationRule : ICalculationRule
    {
        public bool HasDone(ClassificationResult result)
        {
            bool ret = false;
            foreach (var item in result.TypeList)
            {
                double confidence = item.Value;
                if (Math.Abs(confidence - 100) < 0.01)
                {
                    ret = true;
                }
            }
            return ret;
        }
    }
}
