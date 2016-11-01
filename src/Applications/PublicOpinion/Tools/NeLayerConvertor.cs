using BasicUnit;

namespace PublicOpinion.Tools
{
	public class NeLayerConvertor : LayerConvertor
	{
		public NeLayerConvertor()
			: base("ne")
		{
		}

		public override Sentence GetLayer(Sentence sentence, PomModel pom)
		{
			Sentence result = new Sentence();
			foreach (Word token in sentence.Words)
			{
				result.Words.Add(new Word(token.Name) { Pos = "NULL", Start = token.Start, End = token.End });
			}
			foreach (PomPair pomPair in pom.Pairs)
			{
				Sentence tmp = this.Convert(sentence, pomPair.Target, "T");
				this.Combine(result, tmp);
				tmp = this.Convert(sentence, pomPair.Feature, "F");
				this.Combine(result, tmp);
				tmp = this.Convert(sentence, pomPair.Modify, "M");
				this.Combine(result, tmp);
				tmp = this.Convert(sentence, pomPair.Opinion, "O");
				this.Combine(result, tmp);
			}
			return result;
		}

		private void Combine(Sentence sen1, Sentence sen2)
		{
			if (sen2 == null)
			{
				return;
			}
			if (sen1.Words.Count != sen2.Words.Count)
			{
				return;
			}

			for (int i = 0; i < sen1.Words.Count; i++)
			{
				Word token = sen1.Words[i];
				if (token.Pos == "NULL")
				{
					token.Pos = sen2.Words[i].Pos;
				}
			}
		}

		private Sentence Convert(Sentence sentence, string word, string type)
		{
			Sentence result = new Sentence();
			if (string.IsNullOrEmpty(word))
			{
				return null;
			}

			int start = sentence.ToString().IndexOf(word, System.StringComparison.Ordinal);
			int end = start + word.Length;

			foreach (Word token in sentence.Words)
			{
				string pos = string.Empty;
				if (start < 0)
				{
					pos = "NULL";
				}
				else if (token.Start >= end)
				{
					pos = "NULL";
				}
				else if (token.End <= start)
				{
					pos = "NULL";
				}
				else if (token.Start > start)
				{
					if (token.End > end)
					{
						pos = "EX_" + type;
					}
					else if (token.End == end)
					{
						pos = "E_" + type;
					}
					else
					{
						pos = "M_" + type;
					}
				}
				else if (token.Start == start)
				{
					if (token.End > end)
					{
						pos = "SX_" + type;
					}
					else if (token.End == end)
					{
						pos = "S_" + type;
					}
					else
					{
						pos = "B_" + type;
					}
				}
				else if (token.Start < start)
				{
					if (token.End > end)
					{
						pos = "XSX_" + type;
					}
					else if (token.End == end)
					{
						pos = "XS_" + type;
					}
					else
					{
						pos = "XB_" + type;
					}
				}
				result.Words.Add(new Word(token.Name) { Pos = pos, Start = token.Start, End = token.End });
			}
			return result;
		}
	}
}