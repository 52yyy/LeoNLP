using BasicUnit;

using NestedSequenceLabelingFramework;

using PublicOpinion.DataModel.Inner;
using PublicOpinion.Util;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		序列标注器
	/// </summary>
	internal class SequenceSentenceFeatureExtractor : IContentFeatureExtractor<LabelledSentence>
	{
		private IRootLayerConvertor _convertor;
		private SequenceScheduler _scheduler;

		public SequenceSentenceFeatureExtractor(SequenceScheduler scheduler, IRootLayerConvertor convertor)
		{
			this._scheduler = scheduler;
			this._convertor = convertor;
		}

		/// <summary>
		///		从正文内容中抽取序列句
		/// </summary>
		/// <param name="content">正文内容</param>
		/// <returns></returns>
		public LabelledSentence ExtractContent(string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				return new LabelledSentence(new LabelledWord[0]);
			}
			Sentence sentence = _convertor.GetRootSentence(content);
			SequenceSentence sequenceSentence = this._scheduler.Label(sentence);
			return LabelledSentenceHelper.ConvertToLabelledSentence(sequenceSentence);
		}
	}
}