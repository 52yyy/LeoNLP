namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		��������������ȡ�ӿ�
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal interface IContentFeatureExtractor<T>
	{
		/// <summary>
		///		�����������г�ȡ����
		/// </summary>
		/// <param name="content">�������ݣ�ͨ��ָ�û���������</param>
		/// <returns></returns>
		T ExtractContent(string content);
	}
}