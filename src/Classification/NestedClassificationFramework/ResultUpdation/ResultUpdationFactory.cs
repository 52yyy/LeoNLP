namespace NestedClassificationFramework.ResultUpdation
{
    /// <summary>
    ///		构建分类策略的工厂类
    /// </summary>
    public class ResultUpdationFactory
    {
        /// <summary>
        ///		构建One-Gram分类策略
        /// </summary>
        public static IResultUpdation CreateOneGramResultUpdation()
        {
            return new OneGramResultUpdation();
        }

		/// <summary>
		///		构建Bi-Gram分类策略
		/// </summary>
		public static IResultUpdation CreateBiGramResultUpdation()
		{
			return new BiGramResultUpdation();
		}
    }
}
