using PublicOpinion.DataModel;
using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		Pom特征抽取器接口
	/// </summary>
	internal interface IPomFeatureExtractor
	{
		/// <summary>
		///		提取行为，生成用于抽取配对的特征
		/// </summary>
		/// <param name="pomSentence">包含元信息的句子</param>
		/// <returns>经过特征标记后待分析的句子</returns>
		PendingAnalysisPomSentence ExtractPomFeature(PomSentence pomSentence);
	}
}
