using PublicOpinion.DataModel;
using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction
{
	/// <summary>
	///		配对解析器接口
	/// </summary>
	internal interface IPomPairParser
	{
		/// <summary>
		///		从候选配对中选出全部评价配对构成集合
		/// </summary>
		/// <param name="pendingAnalysisPomSentence">待分析的PomSentence</param>
		/// <returns>表示公众意见的集合</returns>
		PublicOpinionPairCollection Parse(PendingAnalysisPomSentence pendingAnalysisPomSentence);
	}
}
