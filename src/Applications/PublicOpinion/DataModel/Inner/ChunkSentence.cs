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
		///		Chunk�а����Ĵʼ���
		/// </summary>
		public List<ChunkWord> Words { get; set; }

		/// <summary>
		///		Chunk���ָ���Ǿ�ʽ���
		/// </summary>
		public ChunkStyle ChunkStyle { get; set; }
	}
}