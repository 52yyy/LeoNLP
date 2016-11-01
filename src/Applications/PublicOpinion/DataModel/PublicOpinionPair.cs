namespace PublicOpinion.DataModel
{
	/// <summary>
	///		公众意见关系对类
	/// </summary>
	public class PublicOpinionPair
	{
		public PublicOpinionPair()
		{
			this.Target = new TargetWord();
			this.Feature = new FeatureWord();
			this.Modify = new ModifyWord();
			this.Opinion = new OpinionWord();
			this.Invert = false;
			this.Orient = OrientType.Neu;
		}

		/// <summary>
		///		主体词
		/// </summary>
		public TargetWord Target { get; set; }

		/// <summary>
		///		属性词
		/// </summary>
		public FeatureWord Feature { get; set; }

		/// <summary>
		///		程度词
		/// </summary>
		public ModifyWord Modify { get; set; }

		/// <summary>
		///		评价词
		/// </summary>
		public OpinionWord Opinion { get; set; }

		/// <summary>
		///		表达反转
		/// </summary>
		public bool Invert { get; set; }

		/// <summary>
		///		情绪导向
		/// </summary>
		public OrientType Orient { get; set; }
	}
}