namespace PublicOpinion.DataModel
{
	/// <summary>
	///		主体词
	/// </summary>
	public class TargetWord :UnitWord
	{
		public override UnitWordType Type
		{
			get
			{
				return UnitWordType.Target;
			}
		}
	}
}