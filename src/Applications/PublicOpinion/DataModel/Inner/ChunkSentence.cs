using System.Collections.Generic;

namespace PublicOpinion.DataModel.Inner
{
	internal class ChunkSentence
	{
		public ChunkSentence()
		{
			this.Words = new List<ChunkWord>();
		}

		/// <summary>
		///		Chunk中包含的词集合
		/// </summary>
		public List<ChunkWord> Words { get; set; }

		/// <summary>
		///		Chunk风格，指的是句式类别
		/// </summary>
		public ChunkStyle ChunkStyle { get; set; }
	}
}