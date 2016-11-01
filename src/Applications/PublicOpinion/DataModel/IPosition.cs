namespace PublicOpinion.DataModel
{
	/// <summary>
	///		位置接口
	/// </summary>
	public interface IPosition
	{
		/// <summary>
		///		开始位置
		/// </summary>
		int Begin { get; set; }

		/// <summary>
		///		结束位置
		/// </summary>
		int End { get; set; }
	}
}