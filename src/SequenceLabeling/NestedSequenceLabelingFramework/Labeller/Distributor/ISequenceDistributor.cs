namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���з������ӿڣ�
	/// </summary>
	public interface ISequenceDistributor
	{
		/// <summary>
		///		����
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		SequenceCellCollection[] Distribute(SequenceSentence sentence, string[] layerDescs);
	}
}