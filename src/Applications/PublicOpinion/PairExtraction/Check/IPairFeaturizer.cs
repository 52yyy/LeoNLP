using System.Collections.Generic;

using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction.Check
{
	/// <summary>
	///		配对特征生成接口
	/// </summary>
	internal interface IPairFeaturizer
	{
		/// <summary>
		///		特征抽取方法
		/// </summary>
		/// <param name="pair">包含上下文信息的候选配对</param>
		/// <returns>该候选配对的候选特征集合</returns>
		List<string> Featurize(CandidatePublicOpinionPair pair);
	}
}