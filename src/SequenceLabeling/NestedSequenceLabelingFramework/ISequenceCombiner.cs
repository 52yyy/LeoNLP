namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列结果合成器接口
	/// </summary>
	public interface ISequenceCombiner
	{
		/// <summary>
		///		合并
		/// </summary>
		/// <param name="sentence"></param>
		/// <param name="layer"></param>
		/// <returns></returns>
		SequenceSentence Combine(SequenceSentence sentence, SequenceLayer layer);
	}
}