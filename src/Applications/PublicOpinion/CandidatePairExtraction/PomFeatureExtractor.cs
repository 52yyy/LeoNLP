using System.Collections.Generic;

using PublicOpinion.DataModel;
using PublicOpinion.DataModel.Inner;
using PublicOpinion.Util;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		Pom特征抽取器
	/// </summary>
	internal class PomFeatureExtractor : IPomFeatureExtractor
	{
		private IContentFeatureExtractor<LabelledSentence> _featureExtractor;

		/// <summary>
		///		构造方法，构造一个抽取器
		/// </summary>
		/// <param name="featureExtractor">设置一个用于打标签的装置</param>
		public PomFeatureExtractor(IContentFeatureExtractor<LabelledSentence> featureExtractor)
		{
			this._featureExtractor = featureExtractor;
		}

		/// <summary>
		///		提取行为，生成用于抽取配对的特征
		/// </summary>
		/// <param name="pomSentence">包含元信息的句子</param>
		/// <returns>经过特征标记后待分析的句子</returns>
		public PendingAnalysisPomSentence ExtractPomFeature(PomSentence pomSentence)
		{
			LabelledSentence labelledSentence = this._featureExtractor.ExtractContent(pomSentence.Content);
			IEnumerable<ChunkSentence> chunks = ChunkHelper.ChunkSplit(labelledSentence);
			return new PendingAnalysisPomSentence(pomSentence) { LabelledSentence = labelledSentence, Chunks = chunks };
		}
	}
}