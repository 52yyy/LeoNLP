using BasicUnit;


namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		细粒度分词转化器，将词长大于两个字的全都逐字拆开
	/// </summary>
	public class SmallSegwordRootLayerConvertor : SegwordRootLayerConvertor
	{
		protected override Sentence SentenceConvert(Sentence sentence)
		{
			Sentence newSentence = new Sentence();
			foreach (Word token in sentence.Words)
			{
				if (token.Name.Length <= 2)
				{
					newSentence.Words.Add(token);
				}
				else
				{
					for (int i = 0; i < token.Name.Length; i++)
					{
						Word tmp = new Word(token.Name[i].ToString());
						tmp.Start = token.Start + i;
						tmp.End = tmp.Start + 1;
						if (i == 0)
						{
							tmp.Pos = "B_" + token.Pos;
						}
						else if (i == token.Name.Length - 1)
						{
							tmp.Pos = "E_" + token.Pos;
						}
						else
						{
							tmp.Pos = "M_" + token.Pos;
						}
						newSentence.Words.Add(tmp);
					}
				}
			}
			return newSentence;
		}
	}
}