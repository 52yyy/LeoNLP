namespace NestedClassificationFramework.CalculationRule
{
    /// <summary>
    ///		计算规则
    /// </summary>
    public interface ICalculationRule
    {
        bool HasDone(ClassificationResult result);
    }
}
