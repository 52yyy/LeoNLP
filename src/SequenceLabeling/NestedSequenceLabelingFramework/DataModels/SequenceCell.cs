namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列单元
	/// </summary>
    public class SequenceCell
    {
		/// <summary>
		///		构造一个新的序列单元
		/// </summary>
		/// <param name="content">设置单元值</param>
	    public SequenceCell(string content)
	    {
		    this.Content = content;
	    }

		/// <summary>
		///		单元值
		/// </summary>
		public string Content { get; private set; }
    }
}
