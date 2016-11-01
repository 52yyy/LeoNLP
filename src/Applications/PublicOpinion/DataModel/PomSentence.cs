namespace PublicOpinion.DataModel
{
	/// <summary>
	///		POM句子
	/// </summary>
	public class PomSentence
	{
		/// <summary>
		///		元信息
		/// </summary>
		public PomMeta Meta { get; set; }

		/// <summary>
		///		句子内容
		/// </summary>
		public string Content { get; set; }
	}
}