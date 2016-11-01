using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.DataModel.Inner
{
	/// <summary>
	///		候选实体关系配对
	/// </summary>
	internal class CandidatePublicOpinionPair : PublicOpinionPair
	{
		public CandidatePublicOpinionPair()
		{
		}

		public CandidatePublicOpinionPair(ChunkTargetWord target, ChunkFeatureWord feature, ChunkModifyWord modify, ChunkOpinionWord opinion, ChunkSentence chunk)
		{
			this.Target = target;
			this.Feature = feature;
			this.Modify = modify;
			this.Opinion = opinion;
			this.Chunk = chunk;
			this.TargetChunkIndex = target == null ? -1 : target.ChunkIndex;
			this.FeatureChunkIndex = feature == null ? -1 : feature.ChunkIndex;
			this.ModifyChunkIndex = modify == null ? -1 : modify.ChunkIndex;
			this.OpinionChunkIndex = opinion == null ? -1 : opinion.ChunkIndex;
		}

		public int TargetChunkIndex { get; set; }

		public int FeatureChunkIndex { get; set; }
		
		public int ModifyChunkIndex { get; set; }
		
		public int OpinionChunkIndex { get; set; }

		/// <summary>
		///		配对所属的Chunk
		/// </summary>
		public ChunkSentence Chunk { get; set; }
	}
}