namespace NestedClassificationFramework.ResultUpdation
{
    /// <summary>
    ///		对分类结果进行更新的策略类
    /// </summary>
    public interface IResultUpdation
    {
        /// <summary>
        ///		 需要实现的具体策略，即根据之前的结果和当前的结果，综合处更新的结果
        /// </summary>
        ClassificationResult UpdateResult(ClassificationResult lastResult, ClassificationResult currentResult);
    }
}
