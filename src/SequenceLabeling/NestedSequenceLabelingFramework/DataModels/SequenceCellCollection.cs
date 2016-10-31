namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列单元集合
	/// </summary>
	public class SequenceCellCollection
	{
		public SequenceCellCollection(int length)
		{
			this.Cells = new SequenceCell[length];
		}

		public SequenceCellCollection(SequenceCell[] cells)
		{
			this.Cells = cells;
		}

		/// <summary>
		///		序列单元的数组
		/// </summary>
		public SequenceCell[] Cells { get; private set; }

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
	}
}