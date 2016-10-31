namespace BasicUnit.DoubleArrayTrie
{
	internal class DeletePrefixCollisionUpdateContextState : PrefixCollisionUpdateContextState
	{
		public override void Handle(Context context)
		{
			context.Finish();
			context.BeInsertedFlag = false;
			context.BeInsertedPosition = -1;
		}
	}
}