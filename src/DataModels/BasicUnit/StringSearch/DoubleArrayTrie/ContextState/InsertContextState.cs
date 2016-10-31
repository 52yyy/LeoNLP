namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Insert状态
	/// </summary>
	internal abstract class InsertContextState : ContextState
	{
		public abstract void Handle(Context context);

		public override void BeInserted(Context context)
		{
			Handle(context);
		}
	}
}