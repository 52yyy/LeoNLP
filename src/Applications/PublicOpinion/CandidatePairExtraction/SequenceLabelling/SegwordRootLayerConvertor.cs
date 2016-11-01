using BasicUnit;

using WordSegment;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		采用分词方式的初始层转换器
	/// </summary>
	public abstract class SegwordRootLayerConvertor : IRootLayerConvertor
	{
		private readonly WordSegmenter _wordSegmenter;

		protected SegwordRootLayerConvertor()
		{
			this._wordSegmenter = new WordSegmenter();
		}

		/// <summary>
		///		将字符串格式的文本转换成序列标注输入的句子
		/// </summary>
		/// <param name="text">字符串格式的文本</param>
		/// <returns></returns>
		public Sentence GetRootSentence(string text)
		{
			Sentence sentence = this._wordSegmenter.SegWord(text);
			Sentence result = this.SentenceConvert(sentence);
			return result;
		}

		protected abstract Sentence SentenceConvert(Sentence sentence);
	}
}