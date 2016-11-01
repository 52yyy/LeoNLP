using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction.Check
{
	/// <summary>
	///		配对检查器接口
	/// </summary>
	internal interface IPublicOpinionPairChecker
	{
		/// <summary>
		///		根据候选配对的上下文信息检查配对是否存在
		/// </summary>
		/// <param name="pair">候选配对</param>
		/// <returns>true表示配对存在应当保留，false表示配对不存在应当移除</returns>
		bool Check(CandidatePublicOpinionPair pair);
	}
}