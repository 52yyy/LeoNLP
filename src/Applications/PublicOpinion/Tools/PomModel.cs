using System.Collections.Generic;

namespace PublicOpinion.Tools
{
	public class PomModel
	{
		public int SentenceId { get; set; }

		public string Content { get; set; }

		public List<PomPair> Pairs { get; set; } 
	}
}