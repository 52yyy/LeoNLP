using BasicUnit;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		Ĭ�ϵķִ�ת�������ִ���ʲô�����Ҫʲô���
	/// </summary>
	public class DefaultSegwordRootLayerConvertor : SegwordRootLayerConvertor
	{
		protected override Sentence SentenceConvert(Sentence sentence)
		{
			Sentence newSentence = new Sentence(sentence.Words);
			return newSentence;
		}
	}
}