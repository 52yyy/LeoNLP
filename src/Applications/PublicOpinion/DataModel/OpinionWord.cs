namespace PublicOpinion.DataModel
{
	/// <summary>
	///		评价词
	/// </summary>
	public class OpinionWord : UnitWord
	{
		public override UnitWordType Type
		{
			get
			{
				return UnitWordType.Opinion;
			}
		}
	}
}