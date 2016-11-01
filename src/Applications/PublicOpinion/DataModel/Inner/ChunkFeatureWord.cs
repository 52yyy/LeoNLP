namespace PublicOpinion.DataModel.Inner
{
	internal class ChunkFeatureWord : FeatureWord
	{
		public ChunkFeatureWord(string word)
		{
			this.Name = word;
			this.IsInChunk = false;
		}

		public ChunkFeatureWord(ChunkWord word)
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