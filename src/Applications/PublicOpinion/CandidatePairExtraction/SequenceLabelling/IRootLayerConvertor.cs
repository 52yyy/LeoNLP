using BasicUnit;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		��ʼ��ת��������
	/// </summary>
	internal interface IRootLayerConvertor
	{
		/// <summary>
		///		���ַ�����ʽ���ı�ת�������б�ע����ľ���
		/// </summary>
		/// <param name="text">�ַ�����ʽ���ı�</param>
		/// <returns></returns>
		Sentence GetRootSentence(string text);
	}
}