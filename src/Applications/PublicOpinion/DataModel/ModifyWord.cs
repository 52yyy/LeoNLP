namespace PublicOpinion.DataModel
{
	/// <summary>
	///		程度词
	/// </summary>
	public class ModifyWord : UnitWord
	{
		public override UnitWordType Type
		{
			get
			{
				return UnitWordType.Modify;
			}
		}
	}
}