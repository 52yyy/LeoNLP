using System.Text;

namespace PublicOpinion.DataModel.Inner
{
	/// <summary>
	///		序列标记的词
	/// </summary>
	internal class LabelledWord : BaseWord, IPosition
	{
		public string PosTag { get; set; }

		public string NeTag { get; set; }

		public string ChunkTag { get; set; }

		public int Begin { get; set; }

		public int End { get; set; }

		public LabelledWord(string name, string posTag, string neTag, string chunkTag, int begin, int end)
		{
			this.Name = name;
			this.PosTag = posTag;
			this.NeTag = neTag;
			this.ChunkTag = chunkTag;
			this.Begin = begin;
			this.End = end;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Name)
				.Append("\t")
				.Append(this.PosTag)
				.Append("\t")
				.Append(this.NeTag)
				.Append("\t")
				.Append(this.ChunkTag)
				.Append("\t")
				.Append(this.Begin)
				.Append("\t")
				.Append(this.End);
			return sb.ToString();
		}
	}
}