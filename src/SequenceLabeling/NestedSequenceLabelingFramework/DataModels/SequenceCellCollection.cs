namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���е�Ԫ����
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
		///		���е�Ԫ������
		/// </summary>
		public SequenceCell[] Cells { get; private set; }

		/// <summary>
		///		�㳤�ȣ�����������е�Ԫ��
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