namespace BasicUnit.Tests.Unit
{
	/// <summary>
	///		词接口
	/// </summary>
	public interface IWord
	{
		/// <summary>
		///		词形
		/// </summary>
		string Name { get; set; }

		/// <summary>
		///		词性
		/// </summary>
		string Pos { get; set; }
	}
}