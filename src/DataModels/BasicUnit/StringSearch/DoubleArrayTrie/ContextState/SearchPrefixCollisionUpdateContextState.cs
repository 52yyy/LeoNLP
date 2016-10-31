using System;

namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Search模式下，处理前缀字符冲突状态
	/// </summary>
	internal class SearchPrefixCollisionUpdateContextState : PrefixCollisionUpdateContextState
	{
		public override void Handle(Context context)
		{
			context.Finish();
			context.BeInsertedFlag = false;
			context.BeInsertedPosition = -1;
		}
	}
}