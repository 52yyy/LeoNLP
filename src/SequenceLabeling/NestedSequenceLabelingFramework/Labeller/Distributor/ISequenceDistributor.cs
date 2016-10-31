namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列分派器接口，
	/// </summary>
	public interface ISequenceDistributor
	{
		/// <summary>
		///		分派
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		SequenceCellCollection[] Distribute(SequenceSentence sentence, string[] layerDescs);
	}
}