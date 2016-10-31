namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列标注计算器接口，负责标注运算
	/// </summary>
	public interface ISequenceCalculator
	{
		/// <summary>
		///		标注运算，使用多层的标记预测新的一层的标记
		/// </summary>
		/// <param name="layers"></param>
		/// <returns></returns>
		SequenceCellCollection Calculate(SequenceCellCollection[] layers);
	}
}