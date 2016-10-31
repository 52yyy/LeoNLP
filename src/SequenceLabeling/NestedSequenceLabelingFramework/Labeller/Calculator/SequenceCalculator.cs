namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���б�ע��������
	/// </summary>
	public abstract class SequenceCalculator : ISequenceCalculator,IModelInitializable
	{
		public abstract SequenceCellCollection Calculate(SequenceCellCollection[] layers);

		public abstract bool Initialize(string modelPath);
	}
}