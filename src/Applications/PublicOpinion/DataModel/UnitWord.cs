using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicOpinion.DataModel
{
	/// <summary>
	///		单位词
	/// </summary>
	public abstract class UnitWord : BaseWord, IPosition, IUnitWordType
	{
		/// <summary>
		///		词类
		/// </summary>
		public abstract UnitWordType Type { get; }

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
