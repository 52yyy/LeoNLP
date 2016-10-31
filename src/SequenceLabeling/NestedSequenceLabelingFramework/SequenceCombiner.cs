namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列结果合成器类
	/// </summary>
	public class SequenceCombiner : ISequenceCombiner
	{
		/// <summary>
		///		合并
		/// </summary>
		/// <param name="sentence"></param>
		/// <param name="layer"></param>
		/// <returns></returns>
		public SequenceSentence Combine(SequenceSentence sentence, SequenceLayer layer)
		{
			SequenceSentence newSentence = new SequenceSentence();
			foreach (SequenceLayer sentenceLayer in sentence.GetLayerEnumerator())
			{
				newSentence.AddLayer(sentenceLayer);
			}
			newSentence.AddLayer(layer);
			return newSentence;
		}
	}
}