namespace PublicOpinion.DataModel
{
	/// <summary>
	///		属性词
	/// </summary>
	public class FeatureWord : UnitWord
	{
		public override UnitWordType Type
		{
			get
			{
				return UnitWordType.Feature;
			}
		}
	}
}