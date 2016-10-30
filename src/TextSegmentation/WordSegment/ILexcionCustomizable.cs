namespace WordSegment
{
	/// <summary>
	///		用户自定义词典接口
	/// </summary>
	public interface ILexcionCustomizable
	{
		/// <summary>
		///		加载用户词典
		/// </summary>
		/// <param name="dictPath"></param>
		/// <returns></returns>
		bool ImportUserDict(string dictPath);
	}
}