namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���в�
	/// </summary>
	public class SequenceLayer
	{
		///  <summary>
		/// 		����һ���µ����в�
		///  </summary>
		///  <param name="desc">���ò�����</param>
		/// <param name="cells"></param>
		public SequenceLayer(string desc, SequenceCellCollection cells)
		{
			this.Desc = desc;
			this.Cells = cells;
		}

		/// <summary>
		///		������
		/// </summary>
		public string Desc { get; set; }

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

		/// <summary>
		///		���е�Ԫ������
		/// </summary>
		public SequenceCellCollection Cells { get; set; }
	}
}