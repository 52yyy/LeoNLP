namespace PublicOpinion.DataModel.Inner
{
	internal class ChunkWord : BaseWord, IPosition
	{
		public int ChunkIndex { get; set; }

		public string Tag { get; set; }

		public int Begin { get; set; }

		public int End { get; set; }

		public override string ToString()
		{
			return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", this.ChunkIndex, this.Name, this.Tag, this.Begin, this.End);
		}
	}
}