using System.Collections.Generic;

namespace Nugget
{
	/// <summary>
	///		Nugget抽取组件输出类定义
	/// </summary>
	public class NuggetCollection
	{
		public NuggetCollection()
		{
			Nuggets = new List<Nugget>();
		}

		/// <summary>
		///		Nugget集合，每个Nugget的信息，以及它们出现的位置信息
		/// </summary>
		public List<Nugget> Nuggets { get; set; } 
	}
}
