namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		候选配对生成器工厂
	/// </summary>
	internal static class PomFeatureExtractorFactory
	{
		/// <summary>
		///		创建一个候选配对生成器
		/// </summary>
		/// <param name="modelPath">模型文件路径</param>
		/// <returns></returns>
		public static IPomFeatureExtractor Create(string modelPath)
		{
			var featureExtractor = SequenceSentenceFeatureExtractorFactory.Create(modelPath);
			return new PomFeatureExtractor(featureExtractor);
		}
	}
}