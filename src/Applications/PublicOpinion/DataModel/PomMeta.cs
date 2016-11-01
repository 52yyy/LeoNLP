namespace PublicOpinion.DataModel
{
	/// <summary>
	///		POM元信息
	/// </summary>
	public class PomMeta
	{
		/// <summary>
		///		主体词
		/// </summary>
		public string Target { get; set; }
		
		/// <summary>
		///		属性词
		/// </summary>
		public string Feature { get; set; }

		/// <summary>
		///		倾向性词
		/// </summary>
		public string Orient { get; set; }
	}
}