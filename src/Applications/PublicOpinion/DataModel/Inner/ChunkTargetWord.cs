namespace PublicOpinion.DataModel.Inner
{
	internal class ChunkTargetWord : TargetWord
	{
		public ChunkTargetWord(string word)
		{
			this.Name = word;
			this.IsInChunk = false;
		}

		public ChunkTargetWord(ChunkWord word)
		{
			this.Name = word.Name;
			this.Begin = word.Begin;
			this.End = word.End;
			this.ChunkIndex = word.ChunkIndex;
		}
		public int ChunkIndex { get; set; }

		public bool IsInChunk { get; set; }
	}
}