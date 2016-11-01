namespace PublicOpinion.DataModel.Inner
{
	internal class ChunkModifyWord : ModifyWord
	{
		public ChunkModifyWord(ChunkWord word)
		{
			this.Name = word.Name;
			this.Begin = word.Begin;
			this.End = word.End;
			this.ChunkIndex = word.ChunkIndex;
		}
		public int ChunkIndex { get; set; }
	}
}