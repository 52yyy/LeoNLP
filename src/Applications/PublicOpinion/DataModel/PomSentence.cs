namespace PublicOpinion.DataModel
{
	/// <summary>
	///		POM����
	/// </summary>
	public class PomSentence
	{
		/// <summary>
		///		Ԫ��Ϣ
		/// </summary>
		public PomMeta Meta { get; set; }

		/// <summary>
		///		��������
		/// </summary>
		public string Content { get; set; }
	}
}