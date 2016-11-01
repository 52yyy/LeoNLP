using System.Collections.Generic;

namespace PublicOpinion.DataModel.Inner
{
	/// <summary>
	///		候选配对树
	///		使用树结构是帮助对候选配对进行管理
	/// </summary>
	internal class PendingAnalysisPomSentence
	{
		public PendingAnalysisPomSentence()
		{
			this.Chunks = new List<ChunkSentence>();
		}

		public PendingAnalysisPomSentence(PomSentence pomSentence)
		{
			this.PomSentence = pomSentence;
			this.Chunks = new List<ChunkSentence>();
		}

		public PomSentence PomSentence { get; private set; }

		public LabelledSentence LabelledSentence { get; set; }

		public IEnumerable<ChunkSentence> Chunks { get; set; }
	}
}
