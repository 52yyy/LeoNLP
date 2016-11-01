using System.Collections.Generic;

namespace PublicOpinion.DataModel.Inner
{
	internal class PendingAnalysisPomSentenceExtractedInfo
	{
		public PendingAnalysisPomSentenceExtractedInfo(PendingAnalysisPomSentence pendingAnalysisPomSentence)
		{
			this.PendingAnalysisPomSentence = pendingAnalysisPomSentence;
		}

		public PendingAnalysisPomSentence PendingAnalysisPomSentence { get; private set; }

		public List<CandidatePublicOpinionPair> Candidates { get; set; } 
	}
}