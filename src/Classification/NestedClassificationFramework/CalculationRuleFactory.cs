using NestedClassificationFramework.CalculationRule;

namespace NestedClassificationFramework
{
    /// <summary>
    ///		 构造calculationrule的工厂
    /// </summary>
    public class CalculationRuleFactory
    {
        public static ICalculationRule CreateDefaultConfidenceRule()
        {
            return new ConfidenceCalculationRule();
        }
    }
}
