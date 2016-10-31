namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列标注计算器类
	/// </summary>
	public abstract class SequenceCalculator : ISequenceCalculator,IModelInitializable
	{
		public abstract SequenceCellCollection Calculate(SequenceCellCollection[] layers);

		public abstract bool Initialize(string modelPath);
	}
}