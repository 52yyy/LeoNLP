using System.Collections.Generic;

namespace PublicOpinion.DataModel
{
	/// <summary>
	///		单位语块
	/// </summary>
	public class UnitChunk : IPosition
	{
		public string Content { get; set; }

		public List<UnitWord> UnitWords { get; set; }

		/// <summary>
		///		开始位置
		/// </summary>
		public int Begin { get; set; }

		/// <summary>
		///		结束位置
		/// </summary>
		public int End { get; set; }
	}
}