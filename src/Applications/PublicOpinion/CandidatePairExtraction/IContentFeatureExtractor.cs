namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		正文内容特征抽取接口
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal interface IContentFeatureExtractor<T>
	{
		/// <summary>
		///		从正文内容中抽取特征
		/// </summary>
		/// <param name="content">正文内容，通常指用户评论内容</param>
		/// <returns></returns>
		T ExtractContent(string content);
	}
}