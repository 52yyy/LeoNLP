namespace WordSegment
{
	/// <summary>
	///		�û��Զ���ʵ�ӿ�
	/// </summary>
	public interface ILexcionCustomizable
	{
		/// <summary>
		///		�����û��ʵ�
		/// </summary>
		/// <param name="dictPath"></param>
		/// <returns></returns>
		bool ImportUserDict(string dictPath);
	}
}