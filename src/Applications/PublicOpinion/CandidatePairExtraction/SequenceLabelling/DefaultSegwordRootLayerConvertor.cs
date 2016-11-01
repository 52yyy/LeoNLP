using BasicUnit;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		默认的分词转化器，分词是什么结果就要什么结果
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