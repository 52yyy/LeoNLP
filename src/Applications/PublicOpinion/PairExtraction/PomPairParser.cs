using System.Collections.Generic;
using System.Linq;

using PublicOpinion.DataModel;
using PublicOpinion.DataModel.Inner;
using PublicOpinion.PairExtraction.Check;

namespace PublicOpinion.PairExtraction
{
	/// <summary>
	///		配对解析器
	/// </summary>
	internal class PomPairParser : IPomPairParser
	{
		private CandidatePairExtractor _candidatePairExtractor;
		private IPublicOpinionPairChecker _checker;

		/// <summary>
		///		构造方法，构造一个配对解析器，并且不启用配对检查器
		/// </summary>
		public PomPairParser()
		{
			this._candidatePairExtractor = new CandidatePairExtractor();
			this._checker = new EmptyPairChecker();
		}

		/// <summary>
		///		构造方法，构造一个配对解析器，并且指定一个配对检查器
		/// </summary>
		/// <param name="checker">为解析器指定一个配对检查器</param>
		public PomPairParser(IPublicOpinionPairChecker checker)
		{
			this._candidatePairExtractor = new CandidatePairExtractor();
			this._checker = checker;
		}

		/// <summary>
		///		从候选配对中选出全部评价配对构成集合
		/// </summary>
		/// <param name="pendingAnalysisPomSentence">待分析的PomSentence</param>
		/// <returns>表示公众意见的集合</returns>
		public PublicOpinionPairCollection Parse(PendingAnalysisPomSentence pendingAnalysisPomSentence)
		{
			PendingAnalysisPomSentenceExtractedInfo data = new PendingAnalysisPomSentenceExtractedInfo(
				pendingAnalysisPomSentence);

			List<CandidatePublicOpinionPair> candidates = new List<CandidatePublicOpinionPair>();
			foreach (ChunkSentence chunk in data.PendingAnalysisPomSentence.Chunks)
			{
				candidates.AddRange(_candidatePairExtractor.CandidatePairExtract(chunk, data.PendingAnalysisPomSentence.PomSentence.Meta));
			}
			data.Candidates = candidates;
			PublicOpinionPairCollection result = new PublicOpinionPairCollection();
			result.AddRange(data.Candidates.Where(candidate => this._checker.Check(candidate)));
			return result;
		}
	}
}