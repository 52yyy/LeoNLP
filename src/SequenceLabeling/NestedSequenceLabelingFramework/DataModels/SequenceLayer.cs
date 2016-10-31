namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列层
	/// </summary>
	public class SequenceLayer
	{
		///  <summary>
		/// 		构造一个新的序列层
		///  </summary>
		///  <param name="desc">设置层描述</param>
		/// <param name="cells"></param>
		public SequenceLayer(string desc, SequenceCellCollection cells)
		{
			this.Desc = desc;
			this.Cells = cells;
		}

		/// <summary>
		///		层描述
		/// </summary>
		public string Desc { get; set; }

		/// <summary>
		///		层长度，即层包含序列单元数
		/// </summary>
		public int Length
		{
			get
			{
				return this.Cells == null ? 0 : this.Cells.Length;
			}
		}

		/// <summary>
		///		序列单元的数组
		/// </summary>
		public SequenceCellCollection Cells { get; set; }
	}
}