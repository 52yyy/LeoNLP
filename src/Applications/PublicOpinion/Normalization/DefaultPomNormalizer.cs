using System.Collections.Generic;

using PublicOpinion.DataModel;

namespace PublicOpinion.Normalization
{
	public class DefaultPomNormalizer :IPomNormalizable
	{
		public IEnumerable<PomSentence> Normalize(string rawText)
		{
			PomSentence sentence = new PomSentence();
			sentence.Content = rawText;
			yield return sentence;
		}
	}
}