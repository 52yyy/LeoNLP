using System;

using BasicUnit;

using PublicOpinion.Util;

namespace PublicOpinion.Tools
{
	public class ChunkLayerConvertor : LayerConvertor
	{
		public ChunkLayerConvertor()
			: base("chunk")
		{
		}

		public override Sentence GetLayer(Sentence sentence, PomModel pom)
		{
			Sentence result = new Sentence();
			foreach (Word token in sentence.Words)
			{
				result.Words.Add(new Word(token.Name) { Pos = "NULL", Start = token.Start, End = token.End });
			}
			string content = sentence.ToString();

			foreach (PomPair pomPair in pom.Pairs)
			{
				int start = 0;
				int end = content.Length;
				this.MatchPair(content, pomPair, ref start, ref end);
				this.Combine(result, start, end);
			}
			return result;
		}

		private void Combine(Sentence result, int start, int end)
		{
			foreach (Word token in result.Words)
			{
				if (token.End <= start)
				{
					continue;
				}
				else if (token.Start > end)
				{
					continue;
				}
				else if (token.Start == start && token.End < end)
				{
					if (token.Pos == "NULL" || token.Pos == "B")
					{
						token.Pos = "B";
					}
					else
					{
						token.Pos = "M";
					}
				}
				else if (token.Start > start && token.End == end)
				{
					if (token.Pos == "NULL" || token.Pos == "E")
					{
						token.Pos = "E";
					}
					else
					{
						token.Pos = "M";
					}
				}
				else if (token.Start == start && token.End == end && token.Pos == "NULL")
				{
					token.Pos = "S";
				}
				else if (token.Start > start && token.End < end)
				{
					token.Pos = "M";
				}
			}
		}

		private void MatchWord(string sentence, string word, ref int start, ref int end)
		{
			if (!string.IsNullOrEmpty(word))
			{
				int startTmp = sentence.IndexOf(word, System.StringComparison.Ordinal);
				if (startTmp == -1)
				{
					return;
				}
				int endT = startTmp + word.Length;
				if (startTmp < start)
				{
					start = startTmp;
				}
				if (endT > end)
				{
					end = endT;
				}
			}
		}

		private void MatchPair(string sentence, PomPair pomPair, ref int start, ref int end)
		{
			int s = 9999;
			int e = 0;
			this.MatchWord(sentence, pomPair.Target, ref s, ref e);
			this.MatchWord(sentence, pomPair.Feature, ref s, ref e);
			this.MatchWord(sentence, pomPair.Modify, ref s, ref e);
			this.MatchWord(sentence, pomPair.Opinion, ref s, ref e);

			if (s == 9999)
			{
				return;
			}
			for (int i = s; i >= 0; i--)
			{
				if (!CharacterUtil.CcFind(sentence[i]))
				{
					start = i +1;
					break;
				}
			}

			for (int i = e; i < sentence.Length; i++)
			{
				if (!CharacterUtil.CcFind(sentence[i]))
				{
					end = i;
					break;
				}
			}
		}
	}
}