using BasicUnit;

using WordSegment;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		���÷ִʷ�ʽ�ĳ�ʼ��ת����
	/// </summary>
	public abstract class SegwordRootLayerConvertor : IRootLayerConvertor
	{
		private readonly WordSegmenter _wordSegmenter;

		protected SegwordRootLayerConvertor()
		{
			this._wordSegmenter = new WordSegmenter();
		}

		/// <summary>
		///		���ַ�����ʽ���ı�ת�������б�ע����ľ���
		/// </summary>
		/// <param name="text">�ַ�����ʽ���ı�</param>
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