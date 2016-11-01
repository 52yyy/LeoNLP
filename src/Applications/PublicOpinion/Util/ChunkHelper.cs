using System.Collections.Generic;
using System.Text;

using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.Util
{
	internal static class ChunkHelper
	{
		/// <summary>
		///		Chunk切分
		/// </summary>
		/// <param name="labelledSentence"></param>
		/// <returns></returns>
		public static IEnumerable<ChunkSentence> ChunkSplit(LabelledSentence labelledSentence)
		{
			StringBuilder sb = new StringBuilder();
			int b = 0;
			int chunkIndex = 0;
			ChunkSentence chunk = new ChunkSentence();
			foreach (LabelledWord labelledWord in labelledSentence.Words)
			{
				if (labelledWord.ChunkTag == "NULL")
				{
					if (chunk.Words.Count == 0)
					{
						continue;
					}
					yield return chunk;
					chunkIndex = 0;
					chunk = new ChunkSentence();
				}
				else
				{
					ChunkWord chunkWord = new ChunkWord();
					string[] split = labelledWord.NeTag.Split('_');
					if (split[0] == "NULL" || split[0] == "S")
					{
						string pos = split.Length == 2 ? split[1] : labelledWord.PosTag;
						chunkWord.ChunkIndex = chunkIndex++;
						chunkWord.Name = labelledWord.Name;
						chunkWord.Tag = pos;
						chunkWord.Begin = labelledWord.Begin;
						chunkWord.End = labelledWord.End;
					}
					else
					{
						if (split[0] == "B")
						{
							sb.Append(labelledWord.Name);
							b = labelledWord.Begin;
							continue;
						}
						else if (split[0] == "M")
						{
							sb.Append(labelledWord.Name);
							continue;
						}
						else if (split[0] == "E")
						{
							sb.Append(labelledWord.Name);
							string pos = split[1];
							chunkWord.ChunkIndex = chunkIndex++;
							chunkWord.Name = sb.ToString();
							chunkWord.Tag = pos;
							chunkWord.Begin = b;
							chunkWord.End = b + sb.ToString().Length;
							sb.Clear();
							b = 0;
						}
					}
					chunk.Words.Add(chunkWord);
				}
			}
		}
	}
}