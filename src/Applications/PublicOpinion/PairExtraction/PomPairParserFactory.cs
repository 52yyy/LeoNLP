using PublicOpinion.PairExtraction.Check;

namespace PublicOpinion.PairExtraction
{
	/// <summary>
	///		配对解析器工厂
	/// </summary>
	internal class PomPairParserFactory
	{
		/// <summary>
		///		创建一个配对解析器，并且不启用配对检查器
		/// </summary>
		/// <returns></returns>
		public static IPomPairParser Create()
		{
			return new PomPairParser();
		}

		/// <summary>
		///		创建一个配对解析器，并且启用配对检查器
		/// </summary>
		/// <param name="modelPath">配对检查器的模型地址</param>
		/// <returns></returns>
		public static IPomPairParser Create(string modelPath)
		{
			IPublicOpinionPairChecker checker = new ClassifyPairChecker(modelPath);
			return new PomPairParser(checker);
		}
	}
}