using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction.Check
{
	/// <summary>
	///		空的配对检查器
	/// </summary>
	internal class EmptyPairChecker:IPublicOpinionPairChecker
	{
		/// <summary>
		///		空方法，什么都不做，用于单元测试
		/// </summary>
		/// <param name="pair">候选配对</param>
		/// <returns>总是返回true</returns>
		public bool Check(CandidatePublicOpinionPair pair)
		{
			return true;
		}
	}
}