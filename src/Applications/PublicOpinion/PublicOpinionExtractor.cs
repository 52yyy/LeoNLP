using System.Collections.Generic;

using PublicOpinion.CandidatePairExtraction;
using PublicOpinion.DataModel;
using PublicOpinion.DataModel.Inner;
using PublicOpinion.Inference;
using PublicOpinion.Normalization;
using PublicOpinion.PairExtraction;

namespace PublicOpinion
{
	/// <summary>
	///		公众意见抽取类
	/// </summary>
	internal class PublicOpinionExtractor : IPublicOpinionExtractor
	{
		private IPomNormalizable _normalizable;

		private IPomFeatureExtractor _generator;

		private IPomPairParser _determiner;

		private IInferrer _inferrer;

		public PublicOpinionExtractor(IPomNormalizable normalizable, IPomFeatureExtractor generator, IPomPairParser determiner, IInferrer inferrer)
		{
			this._normalizable = normalizable;
			this._generator = generator;
			this._determiner = determiner;
			this._inferrer = inferrer;
		}

		public PublicOpinionPairCollection Extract(string text)
		{
			IEnumerable<PomSentence> pomSentences = this._normalizable.Normalize(text);
			PublicOpinionPairCollection totalPopc = new PublicOpinionPairCollection();
			foreach (PomSentence pomSentence in pomSentences)
			{
				PublicOpinionPairCollection popc = this.Extract(pomSentence);
				totalPopc.AddRange(popc);
			}
			return totalPopc;
		}

		public PublicOpinionPairCollection Extract(PomSentence pomSentence)
		{
			PendingAnalysisPomSentence pendingAnalysisPomSentence = this._generator.ExtractPomFeature(pomSentence);
			PublicOpinionPairCollection popc = this._determiner.Parse(pendingAnalysisPomSentence);
			PublicOpinionPairCollection newPopc = new PublicOpinionPairCollection();
			foreach (PublicOpinionPair publicOpinionPair in popc)
			{
				PublicOpinionPair newPair = this._inferrer.Infer(publicOpinionPair);
				newPopc.Add(newPair);
			}
			return popc;
		}
	}
}