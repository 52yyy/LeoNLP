namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		更新前缀字符冲突状态
	/// </summary>
	internal abstract class PrefixCollisionUpdateContextState : ContextState
	{
		public abstract void Handle(Context context);


		public override void BeInserted(Context context)
		{
			Handle(context);
		}
	}
}