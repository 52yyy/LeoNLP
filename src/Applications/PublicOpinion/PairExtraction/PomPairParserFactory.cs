using PublicOpinion.PairExtraction.Check;

namespace PublicOpinion.PairExtraction
{
	/// <summary>
	///		��Խ���������
	/// </summary>
	internal class PomPairParserFactory
	{
		/// <summary>
		///		����һ����Խ����������Ҳ�������Լ����
		/// </summary>
		/// <returns></returns>
		public static IPomPairParser Create()
		{
			return new PomPairParser();
		}

		/// <summary>
		///		����һ����Խ�����������������Լ����
		/// </summary>
		/// <param name="modelPath">��Լ������ģ�͵�ַ</param>
		/// <returns></returns>
		public static IPomPairParser Create(string modelPath)
		{
			IPublicOpinionPairChecker checker = new ClassifyPairChecker(modelPath);
			return new PomPairParser(checker);
		}
	}
}