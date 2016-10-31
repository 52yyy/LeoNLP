namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���б�ע�������ӿڣ������ע����
	/// </summary>
	public interface ISequenceCalculator
	{
		/// <summary>
		///		��ע���㣬ʹ�ö��ı��Ԥ���µ�һ��ı��
		/// </summary>
		/// <param name="layers"></param>
		/// <returns></returns>
		SequenceCellCollection Calculate(SequenceCellCollection[] layers);
	}
}